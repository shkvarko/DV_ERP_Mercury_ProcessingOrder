USE [ERP_Mercury]
GO
/****** Object:  StoredProcedure [dbo].[usp_RecalcOrderToIB]    Script Date: 03/29/2012 14:24:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_RecalcOrderToIB]
	@Order_Guid uniqueidentifier,
  @IBLINKEDSERVERNAME dbo.D_NAME = NULL,
  @SupplInfo xml ( DOCUMENT InfoForCalcPriceSchema ) = NULL output,

  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output
AS

BEGIN
	DECLARE @StartProcessSuppl D_DATETIME;
	SET @StartProcessSuppl = GetDate();

  BEGIN TRY
    SET @ERROR_NUM = 0;
    SET @ERROR_MES = '';
    
 	  IF( @IBLINKEDSERVERNAME IS NULL ) SELECT @IBLINKEDSERVERNAME = dbo.GetIBLinkedServerName();

    DECLARE @strIBSQLText nvarchar( 250 );
    DECLARE @SQLString nvarchar( 2048);
    DECLARE @ParmDefinition nvarchar(500);
    DECLARE @SQLStringForDiscount nvarchar( 2048);
    DECLARE @ParmDefinitionForDiscount nvarchar(500);
    
		-- Проверяем, есть ли заказ с указанным идентификатором 
    IF NOT EXISTS ( SELECT Order_Guid FROM dbo.T_Order WHERE Order_Guid = @Order_Guid )
      BEGIN
        SET @ERROR_NUM = 1;
        SET @ERROR_MES = '[usp_RecalcOrderToIB] Не найден заказ с указанным идентификатором.' + nChar(13) + nChar(10) + CONVERT( nvarchar(36), @Order_Guid );
        RETURN @ERROR_NUM;
      END
   
    DECLARE	@Suppl_Id int;
		DECLARE @Suppl_Num int;
		DECLARE @CustomerID int;
		
    SELECT @Suppl_Id = T_Order.Suppl_Id, @Suppl_Num = T_Order.Order_Num, @CustomerID = T_Customer.Customer_Id 
    FROM T_Order, T_Customer  
    WHERE T_Order.Order_Guid = @Order_Guid
			AND T_Order.Customer_Guid = T_Customer.Customer_Guid; 
    
    IF( @SupplInfo IS NULL )
			BEGIN
				SET @SupplInfo = N'<?xml version="1.0" encoding="UTF-16"?>
					<InfoForCalc xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
						<Suppl CustomerID="0" Opt="false" SupplID="00000000-0000-0000-0000-000000000000"/>
						<SupplItms PartsID="0" Quantity="0"/>
						<Price MarkupPercent="0" Price="0" Price0="0" PriceCurrency="0" DiscountPrice="0" DiscountPriceCurrency="0" PriceList_Price0="0" PriceList_Price="0" PriceList_PriceCurrency="0" DiscountPercent="0" NDSPercent="0" Importer="0" DiscountFullPercent="0" DiscountRetroPercent="0" DiscountFixPercent="0" DiscountTradeEqPercent="0" DiscountComActionPercent="0" CurrencyRate="0" BonusSum="0" BonusCurrencySum="0"/>
						<Customer CustomerGuid="00000000-0000-0000-0000-000000000000"/>
						<Company CompanyGuid="00000000-0000-0000-0000-000000000000"/>
						<CreditLimit Money="0" MoneyApproved="0" Days="0" DaysApproved="0" CurrencyGuid="00000000-0000-0000-0000-000000000000"/>
					</InfoForCalc>
					';
		      
				-- пропишем значение кода клиента и признака "оптовый заказ"
				SELECT @SupplInfo = dbo.SetCustomerIDInXml( @SupplInfo, @CustomerID );
				SELECT @SupplInfo = dbo.SetOptInXml( @SupplInfo, 0 );
				SELECT @SupplInfo = dbo.SetSupplIDInXml( @SupplInfo, @Order_Guid );
			END
    
      
		-- займемся содержимым заказа
		DECLARE @SupplItms_Guid D_GUID;
		DECLARE @OrderItmsSplitms_Id D_ID;
		DECLARE @Parts_Guid D_GUID;
		DECLARE @PARTS_ID int; 
		DECLARE @MEASURE_ID int; 
		DECLARE @SPLITMS_QUANTITY int; 
		DECLARE @SPLITMS_PRICE money;
		DECLARE @SPLITMS_DISCOUNT money; 
		DECLARE @DiscountTmp money; 
		DECLARE @SpesialDiscount money; 
		DECLARE @SPLITMS_BASEPRICE money; 
		DECLARE @SUPPL_NUMstr nvarchar( 16);
		DECLARE @SPLITMS_CURRENCYPRICE money; 
		DECLARE @SPLITMS_ORDERQTY int; 
		DECLARE @RETURN_VALUE int;
		DECLARE @return_valuesql int;
		DECLARE @return_valuesql2 int;
		DECLARE @SPLITMS_ID int;
		DECLARE @RETURN_VALUE_DISCOUNT int;
		DECLARE @SPLITMS_NDS money; 
		DECLARE @SPLITMS_MARKUP money ;
		DECLARE @SPLITMS_CALC_MARKUP money ;
		DECLARE @Diff_MarkUps money ;
		DECLARE @SPLITMS_DISCOUNTPRICE money;
		DECLARE @SPLITMS_CURRENCYDISCOUNTPRICE money;
		DECLARE @PriceInfo xml;
		DECLARE @OptSuppl bit;
		DECLARE @docDiscountListInfo xml ( DOCUMENT DiscountList );
		
		DECLARE @DiscountTypeValue int; 
		DECLARE @DiscountPercent decimal(10, 4);
		
		SET @SUPPL_NUMstr = CONVERT( nvarchar( 16), @suppl_num );
		DECLARE @cards_shipdate varchar( 10);
		SET @cards_shipdate = convert( varchar( 10), GetDate(), 104);
		
		CREATE TABLE #SplItms( SupplItms_Guid uniqueidentifier, Splitms_Id int, SplItms_Quantity int, SplItms_OrderQuantity int, QuantityInCollection int, 
			SUPPL_ID int, PARTS_ID int, NewSplItms_Quantity int );
			
    SET @ParmDefinition = N'@return_valuesql int output, @splitms_idsql int output'; 
    SET @ParmDefinitionForDiscount = N'@return_valuesql2 int output';

		-- Пробежим по списку позиций в заказе и попробуем записать в IB
    DECLARE crSupplItms CURSOR 
    FOR SELECT OrderItms_Guid FROM dbo.T_OrderItms WHERE Order_Guid = @Order_Guid;
    OPEN crSupplItms;
    FETCH NEXT FROM crSupplItms INTO @SupplItms_Guid; 
    WHILE @@FETCH_STATUS = 0
      BEGIN
				BEGIN TRY
					SET @OptSuppl = 0;
					SET @docDiscountListInfo = NULL;
					
					SELECT @Parts_Guid = SplItms.Parts_Guid, @PARTS_ID = Product.PARTS_ID, @MEASURE_ID = Measure.MEASURE_ID, 
						@SPLITMS_ORDERQTY = SplItms.OrderItms_QuantityOrdered, @SPLITMS_QUANTITY = SplItms.OrderItms_Quantity, 
						@SPLITMS_PRICE = SplItms.OrderItms_Price, @SPLITMS_CURRENCYPRICE = SplItms.OrderItms_PriceInAccountingCurrency,
						@SPLITMS_DISCOUNT = SplItms.OrderItms_DiscountPercent, @PriceInfo = SplItms.OrderItms_XMLPrice,
						@SPLITMS_DISCOUNTPRICE = SplItms.OrderItms_PriceWithDiscount, 
						@SPLITMS_CURRENCYDISCOUNTPRICE = SplItms.OrderItms_PriceWithDiscountInAccountingCurrency, 
						@docDiscountListInfo = SplItms.OrderItms_XMLDiscount, 
						@SPLITMS_NDS = SplItms.OrderItms_NDSPercent,
						@SPLITMS_BASEPRICE = SplItms.OrderItms_PriceImporter , 
						@OrderItmsSplitms_Id = SplItms.Splitms_Id
					FROM dbo.T_OrderItms as SplItms, dbo.T_MEASURE as Measure, dbo.T_Parts as Product
					WHERE SplItms.OrderItms_Guid = @SupplItms_Guid
						AND SplItms.Parts_Guid = Product.Parts_Guid
						AND SplItms.Measure_Guid = Measure.Measure_Guid;
						
					SET @SPLITMS_MARKUP = 0;
					SET @RETURN_VALUE = -1;
					SET @SPLITMS_ID = 0;
					SET @RETURN_VALUE_DISCOUNT = -1;
					
					-- 2009.05.04 вместо скидки передаем в splitms_discount разность между максимальной надбавкой и расчитанной надбавкой
					SET @SQLString = 'select @return_valuesql = return_value, @splitms_idsql = splitms_id  from openquery( ' + @IBLINKEDSERVERNAME + ', ''select return_value, splitms_id from SP_EDIT_SPLITMS_FROMSQL( ' + 
					cast( @OrderItmsSplitms_Id as nvarchar( 20)) + ', ' + cast( @PARTS_ID as nvarchar( 20)) + ', ' + cast( @MEASURE_ID as nvarchar( 20)) + ', ' + 
					cast( @SPLITMS_PRICE as nvarchar( 56)) + ', ' + 
					cast( @SPLITMS_DISCOUNT as nvarchar( 56)) + ', ' + cast( @SPLITMS_DISCOUNTPRICE as nvarchar( 56)) + ', ' + cast( @SPLITMS_BASEPRICE as nvarchar( 56)) + ', ' + 
					cast( @SPLITMS_MARKUP as nvarchar( 56)) + ', ' + 
					cast( @SPLITMS_CURRENCYPRICE as nvarchar( 56)) + ', ' + cast( @SPLITMS_CURRENCYDISCOUNTPRICE as nvarchar( 56)) + ', ' + 
					cast( @SPLITMS_DISCOUNT as nvarchar( 56 )) + ')'')';

					execute sp_executesql @SQLString, @ParmDefinition, @return_valuesql = @return_value output, @splitms_idsql = @splitms_id output;

					-- пытаемся прописать структуру скидки
					IF( @SPLITMS_DISCOUNT > 0 )
						BEGIN
							SET @DiscountTmp = @SPLITMS_DISCOUNT;
							SET @SpesialDiscount = 0;
							SET @DiscountTypeValue = 4; -- спеццена
							
							SELECT @SpesialDiscount = dbo.GetDiscountPercentByTypeFromXml( @docDiscountListInfo, @DiscountTypeValue ); -- проверим, нет ли в скидке "спеццены"
							IF( ( @SpesialDiscount > 0 ) AND ( @SpesialDiscount = @SPLITMS_DISCOUNT ) )
								BEGIN
									SET @SQLStringForDiscount = 'select @return_valuesql2 = return_value  from openquery( ' + @IBLINKEDSERVERNAME + ', ''select return_value from SP_ADDSPLITMSDISCOUNTFROMSQL( ' + 
									cast( @splitms_id as nvarchar( 20)) + ', ' + cast( @DiscountTypeValue as nvarchar( 20)) + ', ' + cast( @SpesialDiscount as nvarchar( 20)) + ', ' + '0 ' + ')'')';
									
									execute sp_executesql @SQLStringForDiscount, @ParmDefinitionForDiscount, @return_valuesql2 = @return_value_discount output;
									
								END
							ELSE
								BEGIN
									DECLARE crDiscountItms22 CURSOR 
									FOR SELECT DISCOUNTTYPE_ID FROM dbo.T_OrderDiscountType ORDER BY DISCOUNTTYPE_ID;
									OPEN crDiscountItms22;
									FETCH NEXT FROM crDiscountItms22 INTO @DiscountTypeValue; 
									WHILE @@FETCH_STATUS = 0
										BEGIN 
											SELECT @DiscountPercent = dbo.GetDiscountPercentByTypeFromXml( @docDiscountListInfo, @DiscountTypeValue );
											IF( ( @DiscountPercent > 0 ) AND ( @DiscountTmp > 0 ) )
												BEGIN
													IF( ( @DiscountTmp - @DiscountPercent ) >= 0 )
														BEGIN
															SET @DiscountTmp = ( @DiscountTmp - @DiscountPercent );
														END
													ELSE	
														BEGIN
															SET @DiscountPercent = @DiscountTmp;
															SET @DiscountTmp = 0;
														END

													SET @SQLStringForDiscount = 'select @return_valuesql2 = return_value  from openquery( ' + @IBLINKEDSERVERNAME + ', ''select return_value from SP_ADDSPLITMSDISCOUNTFROMSQL( ' + 
													cast( @splitms_id as nvarchar( 20)) + ', ' + cast( @DiscountTypeValue as nvarchar( 20)) + ', ' + cast( @DiscountPercent as nvarchar( 20)) + ', ' + '0 ' + ')'')';
													execute sp_executesql @SQLStringForDiscount, @ParmDefinitionForDiscount, @return_valuesql2 = @return_value_discount output;
												END
										
										 FETCH NEXT FROM crDiscountItms22 INTO @DiscountTypeValue;   
										END 
							    
									CLOSE crDiscountItms22;
									DEALLOCATE crDiscountItms22;
								END	
							

						END
					
				END TRY
				BEGIN CATCH
					CLOSE crSupplItms;
					DEALLOCATE crSupplItms;

					SET @ERROR_NUM = ERROR_NUMBER();
					SET @ERROR_MES = '[usp_RecalcOrderToIB] Текст ошибки: ' + ERROR_MESSAGE();

					IF( @SupplInfo IS NOT NULL )  
						EXEC sp_AddEventLogForCalcPrice @SupplItmsInfo = @SupplInfo, @EventType_Id = 141, @EventDscrpn = @ERROR_MES;

					BREAK;
				END CATCH;
				
       FETCH NEXT FROM crSupplItms INTO @SupplItms_Guid;   
      END 
    
    CLOSE crSupplItms;
    DEALLOCATE crSupplItms;


    -- теперь нужно пересчитать сумму заказа в dbo.T_PDASuppl
    DECLARE @Order_Quantity D_QUANTITY = 0; 
    DECLARE @Order_SumReserved D_MONEY = 0; 
    DECLARE @Order_SumReservedWithDiscount D_MONEY = 0;
    DECLARE @Order_SumReservedInAccountingCurrency D_MONEY = 0; 
    DECLARE @Order_SumReservedWithDiscountInAccountingCurrency D_MONEY = 0;
    
    SELECT @Order_Quantity = SUM( OrderItms_Quantity ), @Order_SumReserved = SUM( Orderitms_SumReserved ), 
			@Order_SumReservedWithDiscount = SUM( Orderitms_SumReservedWithDiscount ), 
			@Order_SumReservedInAccountingCurrency = SUM( Orderitms_SumReservedInAccountingCurrency ),
			@Order_SumReservedWithDiscountInAccountingCurrency = SUM( Orderitms_SumReservedWithDiscountInAccountingCurrency )
		FROM dbo.T_OrderItms
		WHERE Order_Guid = @Order_Guid;	
    
    IF( @Order_Quantity IS NULL ) SET @Order_Quantity = 0; 
    IF( @Order_SumReserved IS NULL ) SET @Order_SumReserved = 0;
		IF( @Order_SumReservedWithDiscount IS NULL ) SET @Order_SumReservedWithDiscount = 0; 
		IF( @Order_SumReservedInAccountingCurrency IS NULL ) SET @Order_SumReservedInAccountingCurrency = 0;
		IF( @Order_SumReservedWithDiscountInAccountingCurrency IS NULL ) SET @Order_SumReservedWithDiscountInAccountingCurrency = 0;

    UPDATE dbo.T_Order SET Order_Quantity = @Order_Quantity, Order_SumReserved = @Order_SumReserved, 
			Order_SumReservedWithDiscount = @Order_SumReservedWithDiscount, 
      Order_SumReservedInAccountingCurrency = @Order_SumReservedInAccountingCurrency, 
      Order_SumReservedWithDiscountInAccountingCurrency = @Order_SumReservedWithDiscountInAccountingCurrency
    WHERE Order_Guid = @Order_Guid;  

   
    -- про IB тоже не забываем
    SET @ParmDefinition = N'@return_valueIB int output'; 
		SET @SQLString = 'select @return_valueIB = return_value from openquery( ' + @IBLINKEDSERVERNAME + ', ''select return_value from SP_RECALCSUPPL( ' + 
		cast( @suppl_id as nvarchar( 20)) + ')'')';

		execute sp_executesql @SQLString, @ParmDefinition, @return_valueIB = @return_value output;
   
		SET @ERROR_MES = @ERROR_MES + nChar(13) + nChar(10) + 'Пересчитана шапка к протоколу.' + nChar(13) + nChar(10) + 
			'Процедура вернула: ' + CONVERT( nvarchar(8), @return_value );

    IF( @SupplInfo IS NOT NULL )  
			EXEC sp_AddEventLogForCalcPrice @SupplItmsInfo = @SupplInfo, @EventType_Id = 144, @EventDscrpn = @ERROR_MES;

   IF( @return_value <> 0 )
		BEGIN
			SET @ERROR_NUM = 4;

			IF( @return_value = 1 ) 
					SET @ERROR_MES = '[usp_RecalcOrderToIB] Не удалось пересчитать шапку протокола в InterBase.' + nChar(13) + nChar(10) + 'УИ протокола: ' + CONVERT( nvarchar(20), @suppl_id );

			IF( @SupplInfo IS NOT NULL )  
				EXEC sp_AddEventLogForCalcPrice @SupplItmsInfo = @SupplInfo, @EventType_Id = 146, @EventDscrpn = @ERROR_MES;
				
			RETURN @ERROR_NUM;
		END
	ELSE
		BEGIN
			SET @ERROR_MES = 'Пересчитана шапка к протоколу.' + nChar(13) + nChar(10) + 
				'№ протокола: ' + CONVERT( nvarchar(8), @suppl_num );
			IF( @SupplInfo IS NOT NULL )  
				EXEC sp_AddEventLogForCalcPrice @SupplItmsInfo = @SupplInfo, @EventType_Id = 144, @EventDscrpn = @ERROR_MES;
		END	

 	END TRY
	BEGIN CATCH
    SET @ERROR_NUM = ERROR_NUMBER();
    SET @ERROR_MES = '[usp_RecalcOrderToIB]: ' + ERROR_MESSAGE();
		IF( @SupplInfo IS NOT NULL )  
			EXEC sp_AddEventLogForCalcPrice @SupplItmsInfo = @SupplInfo, @EventType_Id = 141, @EventDscrpn = @ERROR_MES;

		RETURN @ERROR_NUM;
	END CATCH;

  IF( @ERROR_NUM = 0 )
		BEGIN
			SET @ERROR_MES = '[usp_RecalcOrderToIB] Успешное завершение операции.' + nChar(13) + nChar(10) + 
			  'Завершен пересчет протокола.' + nChar(13) + nChar(10) + 
				'№ протокола: ' + CONVERT( nvarchar(8), @suppl_num ) + nChar(13) + nChar(10) + 
				'УИ в IB: ' + CONVERT( nvarchar(8), @suppl_id ) + nChar(13) + nChar(10) + 
				'УИ в SQL: ' + CONVERT( nvarchar(36), @Order_Guid );
			IF( @SupplInfo IS NOT NULL )  
				EXEC sp_AddEventLogForCalcPrice @SupplItmsInfo = @SupplInfo, @EventType_Id = 140, @EventDscrpn = @ERROR_MES;
		END

	RETURN @ERROR_NUM;

END

GO
GRANT EXECUTE ON [dbo].[usp_RecalcOrderToIB] TO [public]
GO
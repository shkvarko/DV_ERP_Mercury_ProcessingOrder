USE [ERP_Mercury]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE FUNCTION [dbo].[GetChildDepartByCustomerChild] ( @CustomerChild_Guid D_GUID )
RETURNS D_GUID
WITH EXECUTE AS caller
AS
BEGIN
  
	IF( @CustomerChild_Guid IS NULL ) RETURN NULL;
	
	DECLARE @ChildDepart_Guid D_GUID;
	
	SELECT Top 1 @ChildDepart_Guid = ChildDepart_Guid FROM dbo.T_CustomerChild WHERE CustomerChild_Guid = @CustomerChild_Guid;
	
	RETURN @ChildDepart_Guid;
end

GO
GRANT EXECUTE ON [dbo].[GetChildDepartByCustomerChild] TO [public]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetChildDepart]    Script Date: 03/22/2012 16:32:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Возвращает список записей из ( dbo.T_ChildDepart )
--
-- Входящие параметры:
--
-- Выходные параметры:
--
-- Результат:
--    0 - успешное завершение
--    <>0 - ошибка запроса информации из базы данных

CREATE PROCEDURE [dbo].[usp_GetChildDepart2] 
	@ChildDepart_Guid	D_GUID,
	
  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output
AS

BEGIN

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = NULL;



  BEGIN TRY

		SELECT Top 1 ChildDepart_Guid, ChildDepart_Code, ChildDepart_Main, ChildDepart_NotActive, ChildDepart_MaxDebt, 
			ChildDepart_MaxDelay, ChildDepart_Email, ChildDepart_Name
		FROM dbo.ChildDepartView	
		WHERE ChildDepart_Guid  = @ChildDepart_Guid;

	END TRY
	BEGIN CATCH
		SET @ERROR_NUM = ERROR_NUMBER();
		SET @ERROR_MES = ERROR_MESSAGE();
		RETURN @ERROR_NUM;
	END CATCH;

  IF( @ERROR_NUM = 0 )
		SET @ERROR_MES = 'Успешное завершение операции.';
		
  RETURN @ERROR_NUM;
END

GO
GRANT EXECUTE ON [dbo].[usp_GetChildDepart2] TO [public]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetOrder]    Script Date: 03/22/2012 16:29:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Возвращает список записей из ( dbo.T_Order )
--
-- Входящие параметры:
--	@Order_Guid - УИ заказа
--	@Customer_Guid - УИ клиента
--	@Company_Guid - УИ компании
--
-- Выходные параметры:
--
-- Результат:
--    0 - успешное завершение
--    <>0 - ошибка запроса информации из базы данных

ALTER PROCEDURE [dbo].[usp_GetOrder] 
	@Order_Guid D_GUID = NULL,
	@Customer_Guid D_GUID = NULL,
	@Company_Guid D_GUID = NULL,
	@Stock_Guid D_GUID = NULL,
	@PaymentType_Guid D_GUID = NULL,
	@BeginDate D_DATE,
	@EndDate D_DATE, 
	
  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output
AS

BEGIN

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = NULL;



  BEGIN TRY

		IF( @Order_Guid IS NULL )
			BEGIN
				CREATE TABLE #OrderGuid( Order_Guid uniqueidentifier, Customer_Guid uniqueidentifier, Company_Guid uniqueidentifier, 
					Stock_Guid uniqueidentifier, PaymentType_Guid uniqueidentifier );
				
				INSERT INTO #OrderGuid( Order_Guid, Customer_Guid, Company_Guid, Stock_Guid, PaymentType_Guid )
				SELECT dbo.T_Order.Order_Guid, dbo.T_Order.Customer_Guid, dbo.T_Stock.Company_Guid, dbo.T_Order.Stock_Guid, 
					dbo.T_Order.PaymentType_Guid
				FROM dbo.T_Order, dbo.T_Stock
				WHERE dbo.T_Order.Order_BeginDate BETWEEN @BeginDate AND @EndDate
					AND dbo.T_Order.Stock_Guid = dbo.T_Stock.Stock_Guid;
				
				IF( @Customer_Guid IS NOT NULL )
					DELETE FROM #OrderGuid WHERE Customer_Guid <> @Customer_Guid;
				
				IF( @Company_Guid IS NOT NULL )
					DELETE FROM #OrderGuid WHERE Company_Guid <> @Company_Guid;

				IF( @Stock_Guid IS NOT NULL )
					DELETE FROM #OrderGuid WHERE Stock_Guid <> @Stock_Guid;

				IF( @PaymentType_Guid IS NOT NULL )
					DELETE FROM #OrderGuid WHERE PaymentType_Guid <> @PaymentType_Guid;

				SELECT Order_Guid, Order_Num, Order_SubNum, Order_BeginDate, OrderState_Guid, Order_MoneyBonus, 
					Depart_Guid, Salesman_Guid, Customer_Guid, CustomerChild_Guid, OrderType_Guid, PaymentType_Guid, 
					Order_Description, Order_DeliveryDate, Rtt_Guid, Address_Guid, Stock_Guid, Parts_Guid, Customer_Id, 
					Customer_Name, Customer_Code, Customer_UNP, Customer_OKPO, Customer_OKULP, CustomerActiveType_Guid, 
					CustomerStateType_Guid, DepartTeam_Guid, Depart_Code, Depart_IsActive, Salesman_Id, Salesman_IsActive, 
					User_Guid, User_FirstName, User_MiddleName, User_LastName, User_LoginName, User_IsActive, OrderState_Id, 
					OrderState_Name, OrderType_Name, PaymentType_Name, Rtt_Code, Rtt_Name, RttType_Guid, RttActiveType_Guid, 
					RttSpecCode_Guid, Segmentation_Guid, LicenceType_Guid, AddressString, 
					Company_Guid, Warehouse_Guid, Stock_Name, Stock_IsActive, Stock_IsTrade, Stock_Id, 
					Company_Id, Company_Acronym, Company_Description, Company_Name, Company_OKPO, Company_OKULP, 
					Company_UNN, Company_IsActive,CompanyType_name, CompanyType_Guid, CompanyType_Description, 
					CompanyType_IsActive, Warehouse_Id, Warehouse_Name, WarehouseType_Guid, Warehouse_IsActive, 
					WareHouseType_Name, WarehouseType_IsActive,
					Order_Quantity, Order_SumReserved, Order_SumReservedWithDiscount, 
          Order_SumReservedInAccountingCurrency, Order_SumReservedWithDiscountInAccountingCurrency,
          dbo.[GetChildDepartByCustomerChild](CustomerChild_Guid) AS ChildDepart_Guid
				FROM dbo.OrderView	
				WHERE Order_Guid IN ( SELECT Order_Guid FROM #OrderGuid )
				ORDER BY Order_BeginDate, Customer_Name;
				
				DROP TABLE #OrderGuid;
			END
		ELSE	
			BEGIN
				SELECT Order_Guid, Order_Num, Order_SubNum, Order_BeginDate, OrderState_Guid, Order_MoneyBonus, 
					Depart_Guid, Salesman_Guid, Customer_Guid, CustomerChild_Guid, OrderType_Guid, PaymentType_Guid, 
					Order_Description, Order_DeliveryDate, Rtt_Guid, Address_Guid, Stock_Guid, Parts_Guid, Customer_Id, 
					Customer_Name, Customer_Code, Customer_UNP, Customer_OKPO, Customer_OKULP, CustomerActiveType_Guid, 
					CustomerStateType_Guid, DepartTeam_Guid, Depart_Code, Depart_IsActive, Salesman_Id, Salesman_IsActive, 
					User_Guid, User_FirstName, User_MiddleName, User_LastName, User_LoginName, User_IsActive, OrderState_Id, 
					OrderState_Name, OrderType_Name, PaymentType_Name, Rtt_Code, Rtt_Name, RttType_Guid, RttActiveType_Guid, 
					RttSpecCode_Guid, Segmentation_Guid, LicenceType_Guid, AddressString, 
					Company_Guid, Warehouse_Guid, Stock_Name, Stock_IsActive, Stock_IsTrade, Stock_Id, 
					Company_Id, Company_Acronym, Company_Description, Company_Name, Company_OKPO, Company_OKULP, 
					Company_UNN, Company_IsActive,CompanyType_name, CompanyType_Guid, CompanyType_Description, 
					CompanyType_IsActive, Warehouse_Id, Warehouse_Name, WarehouseType_Guid, Warehouse_IsActive, 
					WareHouseType_Name, WarehouseType_IsActive,
					Order_Quantity, Order_SumReserved, Order_SumReservedWithDiscount, 
          Order_SumReservedInAccountingCurrency, Order_SumReservedWithDiscountInAccountingCurrency, 
          dbo.[GetChildDepartByCustomerChild](CustomerChild_Guid) AS ChildDepart_Guid
				FROM dbo.OrderView	
				WHERE Order_Guid = @Order_Guid;
			END
		
		
	END TRY
	BEGIN CATCH
		SET @ERROR_NUM = ERROR_NUMBER();
		SET @ERROR_MES = ERROR_MESSAGE();
		RETURN @ERROR_NUM;
	END CATCH;

  IF( @ERROR_NUM = 0 )
		SET @ERROR_MES = 'Успешное завершение операции.';
		
  RETURN @ERROR_NUM;
END

GO

/****** Object:  StoredProcedure [dbo].[usp_AddOrderToIB]    Script Date: 03/22/2012 13:35:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[usp_AddOrderToIB]
	@Order_Guid uniqueidentifier,
  @IBLINKEDSERVERNAME dbo.D_NAME = NULL,
  @SupplInfo xml ( DOCUMENT InfoForCalcPriceSchema ) = NULL output,

	@Suppl_Id int output, 
	@Suppl_Num int output,
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
    
    
    DECLARE @STOCK_ID int; -- код склада в IB
		DECLARE @CUSTOMER_ID int; -- код клиента в IB
		DECLARE @STMNT_ID int; -- номер договора в IB
		DECLARE @DEPART_CODE nvarchar( 9); -- код подразделения
		DECLARE @Depart_Guid D_GUID;
		declare @childdepart_guid_id uniqueidentifier; -- код дочернего предприятия
		declare @CHILDCUST_ID int; -- код дочернего предприятия в IB
		declare @SUPPL_CURRENCY bit; -- признак, определяющий оптовый заказ или розничный
		declare @CUSTOMERCHILD_GUID_ID uniqueidentifier; -- код дочернего предприятия
		declare @SUPPL_NOTE nvarchar( 1000); -- примечание к заказу
		declare @Stock_Guid uniqueidentifier; -- код склада
		DECLARE @SUPPL_ALLPRICE money;
		DECLARE @SUPPL_ALLDISCOUNT money;
		DECLARE @SUPPL_CURRENCYALLDISCOUNT money;
		DECLARE @SUPPL_CURRENCYALLPRICE money;
		DECLARE @SUPPL_TOTALPRICE money;
		DECLARE @SUPPL_CURRENCYTOTALPRICE money;
		DECLARE @SUPPL_MONEYBONUS2 bit;
		DECLARE @SUPPL_DELIVERYADDRESS varchar(128);
		DECLARE @SUPPL_DELIVERYDATE D_DATE;
		DECLARE @RTT_GUID_ID D_GUID;
		DECLARE @ADDRESS_GUID_ID D_GUID;
		DECLARE @PDASUPPL_PARTS_GUID_ID D_GUID; -- ссылка на идентификатор виртуального набора
		DECLARE @PDASUPPL_PARTS_ID D_ID; 
		DECLARE @SUPPL_STATE int; -- состояние заказа
		DECLARE @SupplIdForBlock int;
		DECLARE @SALE_ID int;
		DECLARE @SUPLL_BEGINDATE D_DATE;
		DECLARE @PAYMENTFORM_ID int;
		DECLARE @PaymentType_Guid D_GUID;
		DECLARE @PaymentTypeForm1Guid D_GUID;
		DECLARE @PaymentTypeForm2Guid D_GUID;
		DECLARE @OrderState_Guid D_GUID;
    
		-- Проверяем, есть ли заказ с указанным идентификатором 
    IF NOT EXISTS ( SELECT Order_Guid FROM dbo.T_Order WHERE Order_Guid = @Order_Guid )
      BEGIN
        SET @ERROR_NUM = 1;
        SET @ERROR_MES = '[usp_AddOrderToIB] Не найден заказ с указанным идентификатором.' + nChar(13) + nChar(10) + CONVERT( nvarchar(36), @Order_Guid );
        RETURN @ERROR_NUM;
      END
      
		-- Проверяем наличие ссылки на склад
		SELECT @Stock_Guid = Stock_Guid FROM dbo.T_Order WHERE Order_Guid = @Order_Guid ;
    IF( @Stock_Guid IS NULL  )
      BEGIN
        SET @ERROR_NUM = 2;
        SET @ERROR_MES = '[usp_AddOrderToIB] В заказе не указан склад.';
        RETURN @ERROR_NUM;
      END

		-- запрашиваем данные, необходимые для создания заказа на стороне IB
		SELECT @Depart_Guid = dbo.T_Order.Depart_Guid,  @STOCK_ID = Stock.STOCK_ID, @CUSTOMER_ID = Customer.CUSTOMER_ID, 
			@depart_code = Depart.Depart_Code, @CUSTOMERCHILD_GUID_ID = dbo.T_Order.CustomerChild_Guid, 
			@SUPPL_NOTE = dbo.T_Order.Order_Description,
			@SUPPL_ALLPRICE = dbo.T_Order.Order_SumReserved, 
			@SUPPL_ALLDISCOUNT = ( dbo.T_Order.Order_SumReserved - dbo.T_Order.Order_SumReservedWithDiscount ), 
			@SUPPL_CURRENCYALLPRICE = dbo.T_Order.Order_SumReservedInAccountingCurrency, 
			@SUPPL_CURRENCYALLDISCOUNT = ( dbo.T_Order.Order_SumReservedInAccountingCurrency - dbo.T_Order.Order_SumReservedWithDiscountInAccountingCurrency ), 
			@SUPPL_MONEYBONUS2 = dbo.T_Order.Order_MoneyBonus,  
			@SUPPL_DELIVERYDATE = dbo.T_Order.Order_DeliveryDate,
			@RTT_GUID_ID = dbo.T_Order.Rtt_Guid, 
			@ADDRESS_GUID_ID = dbo.T_Order.Address_Guid, 
			@PDASUPPL_PARTS_GUID_ID = dbo.T_Order.Parts_Guid, 
			@PaymentType_Guid = dbo.T_Order.PaymentType_Guid, @SUPLL_BEGINDATE = dbo.T_Order.Order_BeginDate, 
			@SUPPL_NOTE = dbo.T_Order.Order_Description
		FROM dbo.T_Order, dbo.T_CUSTOMER as Customer, dbo.T_DEPART as Depart, dbo.T_STOCK as Stock 		 
		WHERE dbo.T_Order.Order_Guid = @Order_Guid
		  AND dbo.T_Order.Customer_Guid = Customer.Customer_Guid
			AND dbo.T_Order.Depart_Guid = Depart.Depart_Guid
			AND dbo.T_Order.Stock_Guid = Stock.Stock_Guid;
			
    IF( @SUPPL_ALLPRICE IS NULL ) SET @SUPPL_ALLPRICE = 0;
    IF( @SUPPL_ALLDISCOUNT IS NULL ) SET @SUPPL_ALLDISCOUNT = 0;
    IF( @SUPPL_CURRENCYALLPRICE IS NULL ) SET @SUPPL_CURRENCYALLPRICE = 0;
    IF( @SUPPL_CURRENCYALLDISCOUNT IS NULL ) SET @SUPPL_CURRENCYALLDISCOUNT = 0;

		IF( @SUPPL_NOTE IS NULL ) 
			SET @SUPPL_NOTE = 'NULL';
		ELSE 
			SET @SUPPL_NOTE = '''''' + @SUPPL_NOTE + '''''';

		-- торговый представитель
		DECLARE @Salesman_Guid D_GUID;
		SELECT Top 1 @Salesman_Guid	= Salesman_Guid FROM dbo.T_SalesmanDepart
		WHERE Depart_Guid = @Depart_Guid;
		IF( @Salesman_Guid IS NULL )
      BEGIN
        SET @ERROR_NUM = 3;
        SET @ERROR_MES = '[usp_AddOrderToIB] Для дочернего подразделения не найдет торговый представитель.';
        RETURN @ERROR_NUM;
      END
    ELSE 
			SELECT @SALE_ID = Salesman_Id  FROM  dbo.T_Salesman WHERE Salesman_Guid = @Salesman_Guid;
		
		-- форма оплаты
		SELECT @PaymentTypeForm1Guid = dbo.GetPaymentTypeForm1Guid();
		SELECT @PaymentTypeForm2Guid = dbo.GetPaymentTypeForm2Guid();
		IF( @PaymentType_Guid = @PaymentTypeForm1Guid )
			SET @PAYMENTFORM_ID = 1;
		ELSE IF( @PaymentType_Guid = @PaymentTypeForm2Guid )
			SET @PAYMENTFORM_ID = 2;

		-- адрес доставки
		DECLARE @RttAddres D_DESCRIPTION;
		SELECT @RttAddres = dbo.GetAddressString_3( @ADDRESS_GUID_ID );
		
		DECLARE @strSupplDeliverAddress nvarchar( 128 );
		SET @strSupplDeliverAddress = cast( @RttAddres as nvarchar( 128)); 
		SET @strSupplDeliverAddress = '''''' + @strSupplDeliverAddress + ''''''; 

		DECLARE @strAddressGuid varchar( 40 );
		IF( @ADDRESS_GUID_ID IS NULL ) SET @strAddressGuid = 'NULL'
		ELSE 
			BEGIN
				SET @strAddressGuid = convert( nvarchar(36), @ADDRESS_GUID_ID);
				SET @strAddressGuid = '''''' + @strAddressGuid + '''''';
			END	
		
		-- дата доставки
		DECLARE @strSupplDeliverDate varchar( 24 );
		IF( @SUPPL_DELIVERYDATE IS NULL ) SET @strSupplDeliverDate = 'NULL'
		ELSE 
			BEGIN
				SET @strSupplDeliverDate = convert( varchar( 10), @SUPPL_DELIVERYDATE, 104);
				SET @strSupplDeliverDate = '''''' + @strSupplDeliverDate + '''''';
			END	
		
		-- дата заказа	
		DECLARE @strSupplBeginDate 	varchar( 24 );
		IF( @SUPLL_BEGINDATE IS NULL ) SET @strSupplBeginDate = 'NULL'
		ELSE 
			BEGIN
				SET @strSupplBeginDate = convert( varchar( 10), @SUPLL_BEGINDATE, 104);
				SET @strSupplBeginDate = '''''' + @strSupplBeginDate + '''''';
			END	
		
		-- РТТ
		DECLARE @strRttGuid varchar( 40 );
		IF( @RTT_GUID_ID IS NULL ) SET @strRttGuid = 'NULL'
		ELSE 
			BEGIN
				SET @strRttGuid = convert( nvarchar(36), @RTT_GUID_ID);
				SET @strRttGuid = '''''' + @strRttGuid + '''''';
			END	

		-- виртуальный набор
		DECLARE @strPartsId varchar( 20 );
		IF( @PDASUPPL_PARTS_GUID_ID IS NULL ) SET @strPartsId = 'NULL'
		ELSE 
			BEGIN
				SELECT @PDASUPPL_PARTS_ID = PARTS_ID FROM dbo.T_Parts WHERE Parts_Guid = @PDASUPPL_PARTS_GUID_ID;
				IF( @PDASUPPL_PARTS_ID IS NOT NULL )
					SET @strPartsId = convert( nvarchar(20), @PDASUPPL_PARTS_ID);
			END	

		-- КОД ДОЧЕРНЕГО ПРЕДПРИЯТИЯ
		SET @CHILDCUST_ID = 0;
		SELECT @CHILDCUST_ID = CustomerChild_Id	FROM dbo.T_CustomerChild
		WHERE ChildDepart_Guid = @CUSTOMERCHILD_GUID_ID

    DECLARE @SQLString nvarchar( 2048);
    DECLARE @ParmDefinition nvarchar(500);
    DECLARE @SQLStringForDiscount nvarchar( 2048);
    DECLARE @ParmDefinitionForDiscount nvarchar(500);
    DECLARE @RETURNVALUE int;
		DECLARE @suppl_updated bit;
		
    SET @ParmDefinition = N'@suppl_numsql int output, @suppl_idsql int output, @error_numbersql int output, @error_textsql nvarchar(480) output'; 

    -- Добавляем запись в таблицу T_SUPPL в InterBase
    SET @SQLString = 'select @suppl_numsql = suppl_num, @suppl_idsql = suppl_id, @error_numbersql = ERROR_NUMBER, @error_textsql = ERROR_TEXT from openquery( ' + 
			@IBLINKEDSERVERNAME + ', ''select suppl_num, suppl_id, ERROR_NUMBER, ERROR_TEXT from SP_ADDPDASUPPL( ' + 
					cast( @CUSTOMER_ID as nvarchar( 20)) + ', ''''' + @DEPART_CODE + ''''', ' + cast( @STOCK_ID as nvarchar( 20)) + ', ' + 
					cast( @SUPPL_ALLPRICE as nvarchar( 56)) + ', ' + cast( @SUPPL_ALLDISCOUNT as nvarchar( 56)) + ', ' + 
					cast( @SUPPL_CURRENCYALLDISCOUNT as nvarchar( 56)) + ', ' + cast( @SUPPL_CURRENCYALLPRICE as nvarchar( 56)) + ', ' + 
					cast( @CHILDCUST_ID as nvarchar( 20)) + ', ' + cast( @SUPPL_MONEYBONUS2 as nvarchar( 1)) + ', ' + 
					@SUPPL_NOTE + ', ' + @strSupplDeliverAddress + ', ' + 
					@strSupplDeliverDate  + ', ' + @strRttGuid  + ', ' + @strAddressGuid  + ', ' + @strPartsId + ', ' + 
					@strSupplBeginDate + ', ' + CAST( @PAYMENTFORM_ID as varchar(2) ) + ', ' + CAST( @SALE_ID as varchar(8) ) + ')'')'; 

		PRINT @SQLString;
    EXECUTE sp_executesql @SQLString, @ParmDefinition, @suppl_numsql = @suppl_num output, 
			@suppl_idsql = @suppl_id output, @error_numbersql = @ERROR_NUM output, @error_textsql = @ERROR_MES output;

    IF( ( @ERROR_NUM <> 0 ) OR ( @suppl_id IS NULL ) OR ( @suppl_id = 0 ) ) -- 1 заголовок протокола успешно сохранен
      BEGIN
     --   IF( @SupplInfo IS NOT NULL )  
					--EXEC sp_AddEventLogForCalcPrice @SupplItmsInfo = @SupplInfo, @EventType_Id = 141, @EventDscrpn = @ERROR_MES;
        RETURN @ERROR_NUM;
	    END
	  ELSE
			BEGIN
				SET @ERROR_MES = 'Создана шапка протокола.' + nChar(13) + nChar(10) + 
					'№ протокола: ' + CONVERT( nvarchar(8), @suppl_num );
				
				-- прописываем в InterBase состояние 100 у заказа, чтобы его никто не мог открыть
				SET @SupplIdForBlock = @suppl_id;
				
				EXEC	[dbo].[SP_IBUPDATESUPPLSTATE]	@suppl_id = @SupplIdForBlock,	@suppl_state = 100,
					@linked_server = @IBLINKEDSERVERNAME,	@suppl_updated = @suppl_updated OUTPUT

     --   IF( @SupplInfo IS NOT NULL )  
					--EXEC sp_AddEventLogForCalcPrice @SupplItmsInfo = @SupplInfo, @EventType_Id = 142, @EventDscrpn = @ERROR_MES;
			END  

		-- нам удалось сохранить шапку протокола, теперь займемся его содержимым
		DECLARE @SupplItms_Guid D_GUID;
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
		DECLARE @RESERVED_QUANTITY int;
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
			
    SET @ParmDefinition = N'@reserved_quantitysql int output, @splitms_idsql int output, @error_numbersql int output, @error_textsql nvarchar(480) output'; 
    SET @ParmDefinitionForDiscount = N'@return_valuesql int output';

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
						@SPLITMS_BASEPRICE = SplItms.OrderItms_PriceImporter 
					FROM dbo.T_OrderItms as SplItms, dbo.T_MEASURE as Measure, dbo.T_Parts as Product
					WHERE SplItms.OrderItms_Guid = @SupplItms_Guid
						AND SplItms.Parts_Guid = Product.Parts_Guid
						AND SplItms.Measure_Guid = Measure.Measure_Guid;
						
					SET @SPLITMS_MARKUP = 0;
					SET @RESERVED_QUANTITY = 0;
					SET @SPLITMS_ID = 0;
					SET @RETURN_VALUE_DISCOUNT = -1;
					
					-- 2009.05.04 вместо скидки передаем в splitms_discount разность между максимальной надбавкой и расчитанной надбавкой
					SET @SQLString = 'select @reserved_quantitysql = reserved_quantity, @splitms_idsql = splitms_id, @error_numbersql = ERROR_NUMBER, @error_textsql = ERROR_TEXT from openquery( ' + @IBLINKEDSERVERNAME + ', ''select reserved_quantity, splitms_id, ERROR_NUMBER, ERROR_TEXT from SP_ADDSPLITMSFROMSQL( ' + 
					cast( @suppl_id as nvarchar( 20)) + ', ' + cast( @PARTS_ID as nvarchar( 20)) + ', ' + cast( @MEASURE_ID as nvarchar( 20)) + ', ' + 
					cast( @SPLITMS_QUANTITY as nvarchar( 56)) + ', ' + cast( @SPLITMS_PRICE as nvarchar( 56)) + ', ' + 
					cast( @SPLITMS_DISCOUNT as nvarchar( 56)) + ', ' + cast( @SPLITMS_DISCOUNTPRICE as nvarchar( 56)) + ', ' + cast( @SPLITMS_BASEPRICE as nvarchar( 56)) + ', ' + 
					cast( @SPLITMS_MARKUP as nvarchar( 56)) + ', ' + 
					cast( @STOCK_ID as nvarchar( 20)) + ', ''''' + @SUPPL_NUMstr + ''''', ' + 
					cast( @SPLITMS_CURRENCYPRICE as nvarchar( 56)) + ', ' + cast( @SPLITMS_CURRENCYDISCOUNTPRICE as nvarchar( 56)) + ', ' + cast( @SPLITMS_ORDERQTY as nvarchar( 56)) + ', ''''' + 
					cast( @cards_shipdate as nvarchar( 10)) + ''''', '  + cast( @SPLITMS_NDS as nvarchar( 56)) + ', ' + 
					cast( @SPLITMS_DISCOUNT as nvarchar( 56 )) + ')'')';

					execute sp_executesql @SQLString, @ParmDefinition, @reserved_quantitysql = @reserved_quantity output, 
						@splitms_idsql = @splitms_id output, @error_numbersql = @ERROR_NUM output, @error_textsql = @ERROR_MES output;
					
					INSERT INTO #SplItms( SupplItms_Guid, Splitms_Id, SplItms_Quantity, SplItms_OrderQuantity, QuantityInCollection, SUPPL_ID, PARTS_ID, NewSplItms_Quantity ) 
					VALUES( @SupplItms_Guid, @splitms_id, @reserved_quantity, @SPLITMS_ORDERQTY, 1, @suppl_id, @PARTS_ID, @reserved_quantity );
					
				END TRY
				BEGIN CATCH
					CLOSE crSupplItms;
					DEALLOCATE crSupplItms;

					SET @ERROR_NUM = ERROR_NUMBER();
					SET @ERROR_MES = '[usp_AddOrderToIB] Текст ошибки: ' + ERROR_MESSAGE();

					--IF( @SupplInfo IS NOT NULL )  
					--	EXEC sp_AddEventLogForCalcPrice @SupplItmsInfo = @SupplInfo, @EventType_Id = 141, @EventDscrpn = @ERROR_MES;

					--BREAK;
				END CATCH;
       FETCH NEXT FROM crSupplItms INTO @SupplItms_Guid;   
      END 
    
    CLOSE crSupplItms;
    DEALLOCATE crSupplItms;


		SET @ERROR_MES = 'Создано приложение к протоколу.' + nChar(13) + nChar(10) + 
			'№ протокола: ' + CONVERT( nvarchar(8), @suppl_num );

   -- IF( @SupplInfo IS NOT NULL )  
			--EXEC sp_AddEventLogForCalcPrice @SupplItmsInfo = @SupplInfo, @EventType_Id = 143, @EventDscrpn = @ERROR_MES;
			
		-- теперь нужно проставить получившееся количество в заказе
		DECLARE @RsrvQty int;
		DECLARE @Suplitms_Id int;

    DECLARE crSupplItmsTemp CURSOR 
    FOR SELECT SupplItms_Guid, Splitms_Id, SplItms_Quantity FROM #SplItms;
    OPEN crSupplItmsTemp;
    FETCH NEXT FROM crSupplItmsTemp INTO @SupplItms_Guid, @Suplitms_Id, @RsrvQty; 
    WHILE @@FETCH_STATUS = 0
      BEGIN
      
				UPDATE dbo.T_OrderItms SET OrderItms_Quantity = @RsrvQty, Splitms_Id = @Suplitms_Id WHERE OrderItms_Guid = @SupplItms_Guid;
				
				FETCH NEXT FROM crSupplItmsTemp INTO @SupplItms_Guid, @Suplitms_Id, @RsrvQty;   
      END 
    
    CLOSE crSupplItmsTemp;
    DEALLOCATE crSupplItmsTemp;
		 
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
    DECLARE @return_value int;
    SET @ParmDefinition = N'@return_valueIB int output'; 
		SET @SQLString = 'select @return_valueIB = return_value from openquery( ' + @IBLINKEDSERVERNAME + ', ''select return_value from SP_RECALCSUPPL( ' + 
		cast( @suppl_id as nvarchar( 20)) + ')'')';

		execute sp_executesql @SQLString, @ParmDefinition, @return_valueIB = @return_value output;
   
		SET @ERROR_MES = @ERROR_MES + nChar(13) + nChar(10) + 'Пересчитана шапка к протоколу.' + nChar(13) + nChar(10) + 
			'Процедура вернула: ' + CONVERT( nvarchar(8), @return_value );

   -- IF( @SupplInfo IS NOT NULL )  
			--EXEC sp_AddEventLogForCalcPrice @SupplItmsInfo = @SupplInfo, @EventType_Id = 144, @EventDscrpn = @ERROR_MES;

   IF( @return_value <> 0 )
		BEGIN
			SET @ERROR_NUM = 4;
			IF( @return_value = 1 ) 
				BEGIN
					SET @ERROR_MES = '[usp_AddOrderToIB] Заказ в InterBase удален, так как количество в заказе равно ноль.' + nChar(13) + nChar(10) + 'УИ протокола: ' + CONVERT( nvarchar(20), @suppl_id );
					--IF( @SupplInfo IS NOT NULL )  
					--	EXEC sp_AddEventLogForCalcPrice @SupplItmsInfo = @SupplInfo, @EventType_Id = 145, @EventDscrpn = @ERROR_MES;

						-- пусть обрабатывают вручную
						SELECT @OrderState_Guid = OrderState_Guid FROM dbo.T_OrderState 
						WHERE OrderState_Id = 80;
						
						UPDATE dbo.T_Order SET OrderState_Guid = @OrderState_Guid, 
							Order_Quantity = 0, Order_SumReserved = 0, Order_SumReservedWithDiscount = 0,
							Order_SumReservedInAccountingCurrency = 0,
							Order_SumReservedWithDiscountInAccountingCurrency = 0
						WHERE Order_Guid = @Order_Guid;
						
						UPDATE dbo.T_OrderItms SET OrderItms_Quantity = OrderItms_QuantityOrdered, 
							OrderItms_PriceImporter = 0, OrderItms_Price = 0, OrderItms_DiscountPercent = 0, 
							OrderItms_PriceWithDiscount = 0, OrderItms_PriceInAccountingCurrency = 0,
							OrderItms_PriceWithDiscountInAccountingCurrency = 0
						WHERE Order_Guid = @Order_Guid;
						
						SET @ERROR_NUM = 44;
						
				END
			ELSE
				BEGIN
					SET @ERROR_MES = '[usp_AddOrderToIB] Не удалось пересчитать шапку протокола в InterBase.' + nChar(13) + nChar(10) + 'УИ протокола: ' + CONVERT( nvarchar(20), @suppl_id );
					--IF( @SupplInfo IS NOT NULL )  
					--	EXEC sp_AddEventLogForCalcPrice @SupplItmsInfo = @SupplInfo, @EventType_Id = 146, @EventDscrpn = @ERROR_MES;
				END	
				
			RETURN @ERROR_NUM;
		END
	ELSE
		BEGIN
			SET @ERROR_MES = 'Пересчитана шапка к протоколу.' + nChar(13) + nChar(10) + 
				'№ протокола: ' + CONVERT( nvarchar(8), @suppl_num );
			--IF( @SupplInfo IS NOT NULL )  
			--	EXEC sp_AddEventLogForCalcPrice @SupplItmsInfo = @SupplInfo, @EventType_Id = 144, @EventDscrpn = @ERROR_MES;

			-- прописываем в InterBase состояние 0 у заказа, чтобы его можно было видеть
			EXEC	[dbo].[SP_IBUPDATESUPPLSTATE]	@suppl_id = @SupplIdForBlock,	@suppl_state = 0,
				@linked_server = @IBLINKEDSERVERNAME,	@suppl_updated = @suppl_updated OUTPUT
		END	

	-- вроде как дошли без потерь
	SELECT @OrderState_Guid = OrderState_Guid FROM dbo.T_OrderState 
	WHERE OrderState_Id = 70;

	UPDATE dbo.T_Order SET OrderState_Guid = @OrderState_Guid, Suppl_Id = @suppl_id, Order_Num = @suppl_num
	WHERE Order_Guid = @Order_Guid; -- заявка обсчитана и переведена в протокол
	SET @ERROR_NUM = 0;

 	END TRY
	BEGIN CATCH
    SET @ERROR_NUM = ERROR_NUMBER();
    SET @ERROR_MES = '[usp_AddOrderToIB]: ' + ERROR_MESSAGE();
		--IF( @SupplInfo IS NOT NULL )  
		--	EXEC sp_AddEventLogForCalcPrice @SupplItmsInfo = @SupplInfo, @EventType_Id = 141, @EventDscrpn = @ERROR_MES;

		RETURN @ERROR_NUM;
	END CATCH;

  IF( @ERROR_NUM = 0 )
		BEGIN
			DECLARE @SecondCount int;
			SELECT @SecondCount = DATEDIFF( second, @StartProcessSuppl, GetDate() );

			SET @ERROR_MES = '[usp_AddOrderToIB] Успешное завершение операции.' + nChar(13) + nChar(10) + 
			  'Завершено создание протокола.' + nChar(13) + nChar(10) + 
				'№ протокола: ' + CONVERT( nvarchar(8), @suppl_num ) + nChar(13) + nChar(10) + 
				'УИ в IB: ' + CONVERT( nvarchar(8), @suppl_id ) + nChar(13) + nChar(10) + 
				'УИ в SQL: ' + CONVERT( nvarchar(36), @Order_Guid )	+ nChar(13) + nChar(10) + 
				'Время создания протокола в IB: ' + CONVERT( nvarchar(8), @SecondCount ) + ' секунд.';				;
			--IF( @SupplInfo IS NOT NULL )  
			--	EXEC sp_AddEventLogForCalcPrice @SupplItmsInfo = @SupplInfo, @EventType_Id = 140, @EventDscrpn = @ERROR_MES;
		END

	RETURN @ERROR_NUM;

END

GO

/****** Object:  StoredProcedure [dbo].[usp_AddOrder]    Script Date: 03/22/2012 12:55:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Добавляет новую запись в таблицу dbo.T_Order
--
-- Входящие параметры:
	--@Order_BeginDate - дата заказа
	--@OrderState_Guid - уи состояния заказа
	--@Order_MoneyBonus - признак "Бонус"
	--@Depart_Guid - уи подразделения
	--@Salesman_Guid - уи торгововго представителя
	--@Customer_Guid - уи клиента
	--@CustomerChild_Guid - уи дочернего клиента
	--@OrderType_Guid - уи типа заказа
	--@PaymentType_Guid - уи формы оплаты
	--@Order_Description - примечание
	--@Order_DeliveryDate - дата доставки
	--@Rtt_Guid - уи РТТ
	--@Address_Guid - уи адреса
	--@Stock_Guid - уи склада
	--@Parts_Guid - уи товара
--
--
-- Выходные параметры:
--  @Order_Guid - уникальный идентификатор записи
--  @ERROR_NUM - номер ошибки
--  @ERROR_MES - текст ошибки
--
-- Результат:
--    0 - Успешное завершение
--    <>0 - ошибка

ALTER PROCEDURE [dbo].[usp_AddOrder] 
	@Order_BeginDate [dbo].[D_DATE] = NULL,
	@OrderState_Guid [dbo].[D_GUID] = NULL,
	@Order_MoneyBonus [dbo].[D_YESNO],
	@Depart_Guid [dbo].[D_GUID],
	@Salesman_Guid [dbo].[D_GUID],
	@Customer_Guid [dbo].[D_GUID],
	@CustomerChild_Guid [dbo].[D_GUID] = NULL,
	@OrderType_Guid [dbo].[D_GUID],
	@PaymentType_Guid [dbo].[D_GUID],
	@Order_Description [dbo].[D_DESCRIPTION] = NULL,
	@Order_DeliveryDate [dbo].[D_DATE],
	@Rtt_Guid [dbo].[D_GUID],
	@Address_Guid [dbo].[D_GUID],
	@Stock_Guid [dbo].[D_GUID] = NULL,
	@Parts_Guid [dbo].[D_GUID] = NULL,
	@tOrderItms dbo.udt_OrderItms READONLY,

  @Order_Guid D_GUID output,
  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output

AS

BEGIN

	BEGIN TRY

    SET @ERROR_NUM = 0;
    SET @ERROR_MES = NULL;
    SET @Order_Guid = NULL;
    
    DECLARE @Order_Num D_ID = 0;
    DECLARE @Order_SubNum D_ID = 0;
    
    IF( @Order_BeginDate IS NULL ) SET @Order_BeginDate = dbo.TrimTime( GETDATE() );
    IF( @OrderState_Guid IS NULL ) SELECT @OrderState_Guid = dbo.GetFirstOrderSate();
    
    -- Проверяем наличие клиента с указанным идентификатором
    IF NOT EXISTS ( SELECT Customer_Guid FROM dbo.T_Customer WHERE Customer_Guid = @Customer_Guid )
      BEGIN
        SET @ERROR_NUM = 1;
        SET @ERROR_MES = 'В базе данных не найден клиент с указанным идетнификатором.' + Char(13) + 
          'УИ: ' + Char(9) + CONVERT( nvarchar(36), @Customer_Guid  );
        RETURN @ERROR_NUM;
      END

    SELECT @Order_Num = MAX( Order_Num ) FROM dbo.T_Order WHERE Customer_Guid = @Customer_Guid;
    IF( @Order_Num IS NULL ) SET @Order_Num = 1;
    ELSE SET @Order_Num = ( @Order_Num + 1 );
    
    -- Проверяем наличие подразделения с указанным идентификатором
    IF NOT EXISTS ( SELECT Depart_Guid FROM dbo.T_Depart WHERE Depart_Guid = @Depart_Guid )
      BEGIN
        SET @ERROR_NUM = 2;
        SET @ERROR_MES = 'В базе данных не найдено подразделение с указанным идетнификатором.' + Char(13) + 
          'УИ: ' + Char(9) + CONVERT( nvarchar(36), @Depart_Guid  );
        RETURN @ERROR_NUM;
      END

    -- Проверяем наличие торгового представителя с указанным идентификатором
    IF NOT EXISTS ( SELECT Salesman_Guid FROM dbo.T_Salesman WHERE Salesman_Guid = @Salesman_Guid )
      BEGIN
        SET @ERROR_NUM = 3;
        SET @ERROR_MES = 'В базе данных не найден торговый представитель с указанным идетнификатором.' + Char(13) + 
          'УИ: ' + Char(9) + CONVERT( nvarchar(36), @Salesman_Guid  );
        RETURN @ERROR_NUM;
      END
      
    -- Проверяем наличие дочернего подраздления с указанным идентификатором
    IF( @CustomerChild_Guid IS NOT NULL )
			BEGIN
				IF NOT EXISTS ( SELECT CustomerChild_Guid FROM dbo.T_CustomerChild WHERE ChildDepart_Guid = @CustomerChild_Guid )
					BEGIN
						SET @ERROR_NUM = 4;
						SET @ERROR_MES = 'В базе данных не найден дочерний клиент с указанным идетнификатором.' + Char(13) + 
							'УИ: ' + Char(9) + CONVERT( nvarchar(36), @CustomerChild_Guid );
						RETURN @ERROR_NUM;
					END
				ELSE	
					SELECT @CustomerChild_Guid = CustomerChild_Guid FROM dbo.T_CustomerChild WHERE ChildDepart_Guid = @CustomerChild_Guid
			END

    -- Проверяем наличие типа заказа с указанным идентификатором
    IF( @OrderType_Guid IS NOT NULL )
			BEGIN
				IF NOT EXISTS ( SELECT OrderType_Guid FROM dbo.T_OrderType WHERE OrderType_Guid = @OrderType_Guid )
					BEGIN
						SET @ERROR_NUM = 5;
						SET @ERROR_MES = 'В базе данных не найден тип заказа с указанным идетнификатором.' + Char(13) + 
							'УИ: ' + Char(9) + CONVERT( nvarchar(36), @OrderType_Guid  );
						RETURN @ERROR_NUM;
					END
			END

    -- Проверяем наличие формы оплаты с указанным идентификатором
    IF( @PaymentType_Guid IS NOT NULL )
			BEGIN
				IF NOT EXISTS ( SELECT PaymentType_Guid FROM dbo.T_PaymentType WHERE PaymentType_Guid = @PaymentType_Guid )
					BEGIN
						SET @ERROR_NUM = 6;
						SET @ERROR_MES = 'В базе данных не найдена форма оплаты с указанным идетнификатором.' + Char(13) + 
							'УИ: ' + Char(9) + CONVERT( nvarchar(36), @PaymentType_Guid  );
						RETURN @ERROR_NUM;
					END
			END

    -- Проверяем наличие розничной точки с указанным идентификатором
    IF( @Rtt_Guid IS NOT NULL )
			BEGIN
				IF NOT EXISTS ( SELECT Rtt_Guid FROM dbo.T_CustomerRtt WHERE Customer_Guid = @Customer_Guid AND Rtt_Guid = @Rtt_Guid )
					BEGIN
						SET @ERROR_NUM = 7;
						SET @ERROR_MES = 'В базе данных не найдена розничная точка с указанным идетнификатором.' + Char(13) + 
							'УИ: ' + Char(9) + CONVERT( nvarchar(36), @PaymentType_Guid  );
						RETURN @ERROR_NUM;
					END
			END

    -- Проверяем наличие адреса доставки с указанным идентификатором
    IF( @Address_Guid IS NOT NULL )
			BEGIN
				IF NOT EXISTS ( SELECT Address_Guid FROM dbo.T_Address WHERE Address_Guid = @Address_Guid )
					BEGIN
						SET @ERROR_NUM = 8;
						SET @ERROR_MES = 'В базе данных не найден адрес доставки с указанным идетнификатором.' + Char(13) + 
							'УИ: ' + Char(9) + CONVERT( nvarchar(36), @Address_Guid  );
						RETURN @ERROR_NUM;
					END
			END

    -- Проверяем наличие товара с указанным идентификатором
    IF( @Parts_Guid IS NOT NULL )
			BEGIN
				IF NOT EXISTS ( SELECT Parts_Guid FROM dbo.T_Parts WHERE Parts_Guid = @Parts_Guid )
					BEGIN
						SET @ERROR_NUM = 9;
						SET @ERROR_MES = 'В базе данных не найден товар с указанным идетнификатором.' + Char(13) + 
							'УИ: ' + Char(9) + CONVERT( nvarchar(36), @Parts_Guid  );
						RETURN @ERROR_NUM;
					END
			END

    -- Проверяем наличие склада с указанным идентификатором
    IF( @Stock_Guid IS NOT NULL )
			BEGIN
				IF NOT EXISTS ( SELECT Stock_Guid FROM dbo.T_Stock WHERE Stock_Guid = @Stock_Guid )
					BEGIN
						SET @ERROR_NUM = 10;
						SET @ERROR_MES = 'В базе данных не найден склад с указанным идетнификатором.' + Char(13) + 
							'УИ: ' + Char(9) + CONVERT( nvarchar(36), @Stock_Guid  );
						RETURN @ERROR_NUM;
					END
			END

    DECLARE @NewID D_GUID;
    SET @NewID = NEWID( );	
    
    IF( @OrderState_Guid IS NULL )
			SELECT @OrderState_Guid = OrderState_Guid FROM dbo.T_OrderState WHERE OrderState_Id = 0;

    BEGIN TRANSACTION UpdateData;
    
    INSERT INTO dbo.T_Order( Order_Guid, Suppl_Id, Order_Num, Order_SubNum, Order_BeginDate, OrderState_Guid, 
			Order_MoneyBonus, Depart_Guid, Salesman_Guid, Customer_Guid, CustomerChild_Guid, OrderType_Guid, 
			PaymentType_Guid, Order_Description, Order_DeliveryDate, Rtt_Guid, Address_Guid, Stock_Guid, Parts_Guid )
    VALUES( @NewID, 0, @Order_Num, @Order_SubNum, @Order_BeginDate, @OrderState_Guid, 
			@Order_MoneyBonus, @Depart_Guid, @Salesman_Guid, @Customer_Guid, @CustomerChild_Guid, @OrderType_Guid, 
			@PaymentType_Guid, @Order_Description, @Order_DeliveryDate, @Rtt_Guid, @Address_Guid, @Stock_Guid, @Parts_Guid );
    
    INSERT INTO dbo.T_OrderItms( OrderItms_Guid, Splitms_Id, Order_Guid, Parts_Guid, Measure_Guid, OrderItms_Quantity, 
			OrderItms_QuantityOrdered, OrderItms_PriceImporter,	OrderItms_Price, OrderItms_DiscountPercent,
			OrderItms_PriceWithDiscount, OrderItms_NDSPercent, OrderItms_PriceInAccountingCurrency,
			OrderItms_PriceWithDiscountInAccountingCurrency )  
    SELECT OrderItms_Guid, 0, @NewID, Parts_Guid, Measure_Guid, OrderItms_Quantity, 
			OrderItms_QuantityOrdered, OrderItms_PriceImporter,	OrderItms_Price, OrderItms_DiscountPercent,
			OrderItms_PriceWithDiscount,	OrderItms_NDSPercent,	OrderItms_PriceInAccountingCurrency,
			OrderItms_PriceWithDiscountInAccountingCurrency
    FROM @tOrderItms;
    
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
		WHERE Order_Guid = @NewID;	
    
    UPDATE dbo.T_Order SET Order_Quantity = @Order_Quantity, Order_SumReserved = @Order_SumReserved, 
			Order_SumReservedWithDiscount = @Order_SumReservedWithDiscount, 
      Order_SumReservedInAccountingCurrency = @Order_SumReservedInAccountingCurrency, 
      Order_SumReservedWithDiscountInAccountingCurrency = @Order_SumReservedWithDiscountInAccountingCurrency
    WHERE Order_Guid = @NewID;  
    
    SET @Order_Guid = @NewID;
    
		DECLARE @Suppl_Id int; 
		DECLARE @Suppl_Num int;
		
    EXEC dbo.usp_AddOrderToIB @Order_Guid = @Order_Guid, @Suppl_Id = @Suppl_Id out, @Suppl_Num = @Suppl_Num out, 
			@ERROR_NUM = @ERROR_NUM out, @ERROR_MES = @ERROR_MES out;

		IF( @ERROR_NUM = 0 )	
			BEGIN
				UPDATE dbo.T_Order SET Suppl_Id = @Suppl_Id, Order_Num = @Suppl_Num 
				WHERE Order_Guid = @Order_Guid;
				
				COMMIT TRANSACTION UpdateData;
			END 
		ELSE	
			ROLLBACK TRANSACTION UpdateData;
    
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION UpdateData;
    SET @ERROR_NUM = ERROR_NUMBER();
    SET @ERROR_MES =  ERROR_MESSAGE();
		RETURN @ERROR_NUM;
	END CATCH;

	IF( @ERROR_NUM = 0 )
		SET @ERROR_MES = 'Успешное завершение операции.';
		
	RETURN @ERROR_NUM;
END

GO
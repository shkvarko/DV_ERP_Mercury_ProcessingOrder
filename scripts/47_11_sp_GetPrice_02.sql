USE [ERP_Mercury]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetPrice_02]    Script Date: 04/01/2012 12:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[sp_GetPrice_02] 
	@Spl_Guid D_GUID = NULL,
	@SupplItms_Guid D_GUID = NULL,
  @SupplItmsInfo xml (DOCUMENT InfoForCalcPriceSchema) = NULL output,
  @SPParamsInfo xml (DOCUMENT RuleParams) = NULL output,
  @RulePool_StepGuid uniqueidentifier = NULL,
  @DiscountListInfo xml ( DOCUMENT DiscountList ) = NULL output,

  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output

AS

BEGIN
SET NOCOUNT ON;

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = '';

	DECLARE @Parts_Guid D_GUID;       -- УИ товара
	DECLARE @Parts_Qty D_QUANTITY;    -- Количество товара в позиции
  DECLARE @Customer_Id D_ID;        -- УИ клиента в InterBase
  DECLARE @OptSuppl bit;            -- Признак "оптовый заказ"
  DECLARE @Suppl_Guid D_GUID;       -- УИ заказа
  DECLARE @Stock_Guid D_GUID;       -- УИ склада
  
	BEGIN TRY

		DECLARE @PaymentTypeForm1Guid D_GUID; 
		DECLARE @PaymentTypeForm2Guid D_GUID; 
		DECLARE @PaymentType_Guid D_GUID; 
		
		SELECT @PaymentTypeForm1Guid = dbo.GetPaymentTypeForm1Guid();
		SELECT @PaymentTypeForm2Guid = dbo.GetPaymentTypeForm2Guid();
		
		IF( @Spl_Guid IS NOT NULL )
			BEGIN
				-- проверка на то, можно ли в принципе "впускать" заказ в это правило
				
				SELECT @PaymentType_Guid = PaymentType_Guid FROM dbo.T_Order WHERE Order_Guid = @Spl_Guid;
				
				IF( @PaymentType_Guid = @PaymentTypeForm2Guid )
					BEGIN
						SET @ERROR_NUM = 777; -- почему бы и нет?
						SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) + '[SP_GetPrice_02] Заказ "форма оплаты №1". Можно проверять на содержимое.';
					END  

				RETURN @ERROR_NUM;

			END

		-- Имя шага, запустившего процедуру
		DECLARE @RulePool_StepName D_NAME;
		SELECT @RulePool_StepName = RulePool_StepName FROM dbo.T_RulePool WHERE RulePool_StepGuid = @RulePool_StepGuid;
		IF( @RulePool_StepName IS NULL ) SET @RulePool_StepName = '';
	
	  IF( @SupplItmsInfo IS NULL )
	    BEGIN
	      BEGIN TRY  
	        -- нужно вернуть информацию о тех параметрах, которые умеет обрабатывать данная хранимая процедура
	        -- вернем мы ее в виде xml файла  
            DECLARE @doc xml ( DOCUMENT RuleParams );
	          SET @doc = N'<?xml version="1.0" encoding="UTF-16"?>
              <SP_Param xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
	              <Param Type="int" Name="Скидка" Value=""/>
	              <Param Type="int" Name="Надбавка" Value=""/>
              </SP_Param>
              ';  
            SET @SPParamsInfo = @doc;  
	      END TRY
	      BEGIN CATCH
		      SET @ERROR_NUM = ERROR_NUMBER();
		      SET @ERROR_MES = '[SP_GetPrice_02], Текст ошибки: ' + ERROR_MESSAGE();
		      RETURN @ERROR_NUM;
	      END CATCH;

	      SET @ERROR_NUM = 0;
	      SET @ERROR_MES = @ERROR_MES + ' [SP_GetPrice_02] Схема дополнительных параметров. Успешное завершение операции.';
	      RETURN @ERROR_NUM;
	    END
	    
	  IF( @SupplItms_Guid IS NOT NULL )
			BEGIN
				SELECT @Suppl_Guid = Suppl_Guid, @OptSuppl = Opt, @Stock_Guid = STOCK_GUID_ID, @Customer_Id = CustomerID, 
					@Parts_Guid = Parts_Guid, @Parts_Qty = SupplItms_Quantity
				FROM #PDASupplItms WHERE SupplItms_Guid = @SupplItms_Guid;	
			END
		ELSE
			BEGIN
		    SELECT @Suppl_Guid = dbo.GetSupplIDFromXml( @SupplItmsInfo );
		    SELECT @OptSuppl = dbo.GetOptFromXml( @SupplItmsInfo );
		    SELECT @Stock_Guid = Stock_Guid FROM dbo.T_Order WHERE Order_Guid = @Suppl_Guid;
				SELECT @Customer_Id = dbo.GetCustomerIDFromXml( @SupplItmsInfo );
				SELECT @Parts_Guid = dbo.GetPartsGuidFromXml( @SupplItmsInfo );
				SELECT @Parts_Qty = dbo.GetPartsQtyFromXml( @SupplItmsInfo );
			END	

		SELECT @PaymentType_Guid = PaymentType_Guid FROM dbo.T_Order WHERE Order_Guid = @Suppl_Guid;

	  -- Уникальный идентификатор заказа  
    IF( @Suppl_Guid IS NULL )
      BEGIN
	      SET @ERROR_NUM = 1;
	      SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) + nChar(13) + nChar(10) +  '[SP_GetPrice_02] не удалось определить идентификатор заказа.';

				EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 101, @RulePool_StepGuid = @RulePool_StepGuid, @EventDscrpn = @ERROR_MES;
	      RETURN @ERROR_NUM;
      END
      
    -- Признак "Оптовый заказ"
    IF( @OptSuppl = 0 )
			BEGIN
	      SET @ERROR_NUM = -1;
	      SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10)  + @RulePool_StepName + nChar(13) + nChar(10) +  ' [SP_GetPrice_02] Заказ не относится к категории "форма оплаты №2". Обработка не будет производится.';

				EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 100, @RulePool_StepGuid = @RulePool_StepGuid, @EventDscrpn = @ERROR_MES;
	      RETURN @ERROR_NUM;
	    END  
	    
    -- Уникальный идентификатор склада
    IF( @Stock_Guid IS NULL )
      BEGIN
	      SET @ERROR_NUM = 2;
	      SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) + '[SP_GetPrice_02] не удалось определить идентификатор склада.';

				EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 99, @RulePool_StepGuid = @RulePool_StepGuid, @EventDscrpn = @ERROR_MES;
	      RETURN @ERROR_NUM;
      END
    
    -- Если мы здесь, значит в структуре XML содержатся все необходимые данные.
    -- Проверим их на допустимость значений.
    IF( (@Customer_Id IS NULL) OR (@Customer_Id = 0) OR (@OptSuppl IS NULL) OR (@Parts_Guid IS NULL)  OR 
         ( @Parts_Qty IS NULL ) OR (@Parts_Qty <= 0) )
      BEGIN
        SET @ERROR_NUM = 3;
        DECLARE @strCustomer_Id varchar(8);
        DECLARE @strOptSuppl varchar(8);
        DECLARE @strParts_Id varchar(8);
        DECLARE @strParts_Qty varchar(8);
        
        IF(@Customer_Id IS NULL)  SET @strCustomer_Id = 'NULL';
        ELSE SET @strCustomer_Id = CONVERT( varchar(8), @Customer_Id );
        IF(@OptSuppl IS NULL)  SET @strOptSuppl = 'NULL';
        IF(@Parts_Guid IS NULL)  SET @strParts_Id = 'NULL';
        ELSE SET @strParts_Id = CONVERT( varchar(36), @Parts_Guid );
        IF(@Parts_Qty IS NULL)  SET @strParts_Qty = 'NULL';
        ELSE SET @strParts_Qty = CONVERT( varchar(8), @Parts_Qty );

        SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10)  + @RulePool_StepName + nChar(13) + nChar(10) + 
					'[SP_GetPrice_02] Недопустимое значение входных параметров.' + nChar(13) + nChar(10) + 
					'Код клиента: ' + @strCustomer_Id  + nChar(13) + nChar(10) +  
          'Опт: ' + @strOptSuppl  + nChar(13) + nChar(10) +  'Код товара: ' + @strParts_Id  + nChar(13) + nChar(10) + 'Количество товара: ' + @strParts_Qty;

				EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 98, @RulePool_StepGuid = @RulePool_StepGuid, @EventDscrpn = @ERROR_MES;
	      RETURN @ERROR_NUM;
      END     
    
    -- Мы по-прежнему в игре... это радует
    -- Проверим, числится ли в справочниках клиент и товар с заданным кодом и допустимое ли у нас количество товара в заказе

    -- Проверяем наличие записи о клиенте в справочнике
    IF NOT EXISTS ( SELECT Customer_Guid FROM dbo.T_Customer WHERE Customer_Id = @Customer_Id )
      BEGIN
        SET @ERROR_NUM = 4;
        SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) + ' [SP_GetPrice_02] Клиент с заданным идентификатором не найден в справочнике.' + nChar(13) + nChar(10) + 
        'Идентификатор: ' + CONVERT( nvarchar(8), @Customer_Id );

				EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 97, @RulePool_StepGuid = @RulePool_StepGuid, @EventDscrpn = @ERROR_MES;
	      RETURN @ERROR_NUM;
      END 

    -- Проверяем наличие записи о товаре в справочнике
    IF NOT EXISTS ( SELECT Parts_Guid FROM dbo.T_Parts WHERE Parts_Guid = @Parts_Guid )
      BEGIN
        SET @ERROR_NUM = 5;
        SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) +  '[SP_GetPrice_02] Товар с заданным идентификатором не найден в справочнике.'  + nChar(13) + nChar(10) +  'Идентификатор: ' + CONVERT( nvarchar(36), @Parts_Guid );

				EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 96, @RulePool_StepGuid = @RulePool_StepGuid, @EventDscrpn = @ERROR_MES;
	      RETURN @ERROR_NUM;
      END 

    -- Скидка
    DECLARE @DiscountPercent decimal(18, 4);
    SELECT @DiscountPercent = dbo.GetDiscountPercentFromXml(@SupplItmsInfo);
    IF( ( @DiscountPercent IS NULL ) OR ( @DiscountPercent < 0 ) )
      SET @DiscountPercent = 0;
    
    -- Запрашиваем цену
    DECLARE	@NDSPercent D_MONEY;
		DECLARE	@PriceImporter D_MONEY;
		DECLARE	@Price D_MONEY;
		DECLARE	@PriceWithDiscount D_MONEY;
		DECLARE	@PriceInAccountingCurrency D_MONEY;
		DECLARE	@PriceWithDiscountInAccountingCurrency D_MONEY;
		DECLARE @IsPartsImporter D_YESNO;
		DECLARE @ChargesPercent D_MONEY;
		DECLARE @PriceImporterFromPriceList D_MONEY;
		DECLARE @PriceFromPriceList D_MONEY;
		DECLARE @PriceInAccountingCurrencyFromPriceList D_MONEY;

		EXEC [dbo].[usp_GetPriceCalculation] @Parts_Guid = @Parts_Guid, @PaymentType_Guid = @PaymentType_Guid, 
			@Stock_Guid = @Stock_Guid, @DiscountPercent = @DiscountPercent, @NDSPercent = @NDSPercent out,
			@PriceImporter = @PriceImporter out, @Price = @Price out,
			@PriceWithDiscount = @PriceWithDiscount out,
			@PriceInAccountingCurrency = @PriceInAccountingCurrency out,
			@PriceWithDiscountInAccountingCurrency = @PriceWithDiscountInAccountingCurrency out,
			@IsPartsImporter = @IsPartsImporter out, @ChargesPercent = @ChargesPercent out,
			@PriceImporterFromPriceList = @PriceImporterFromPriceList out,
			@PriceFromPriceList = @PriceFromPriceList out,
			@PriceInAccountingCurrencyFromPriceList = @PriceInAccountingCurrencyFromPriceList out,
		  @ERROR_NUM = @ERROR_NUM out, @ERROR_MES = @ERROR_MES out ;    
		
		IF( @ERROR_NUM <> 0 )   
			BEGIN
				EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 92, @RulePool_StepGuid = @RulePool_StepGuid, @EventDscrpn = @ERROR_MES;
	      RETURN @ERROR_NUM;
			END

   IF( ( @PriceImporter = 0 ) OR ( @Price = 0 ) OR ( @PriceInAccountingCurrency = 0 ) )
      BEGIN
        SET @ERROR_NUM = 10;
        SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) + 
					'@PriceImporter: ' + CONVERT( nvarchar(15), @PriceImporter ) + nChar(13) + nChar(10) + 
					'@PriceInAccountingCurrency: ' + CONVERT( nvarchar(15), @PriceInAccountingCurrency )  + nChar(13) + nChar(10) + 
					'@Price: ' + CONVERT( nvarchar(15), @Price );

				EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 92, @RulePool_StepGuid = @RulePool_StepGuid, @EventDscrpn = @ERROR_MES;
	      RETURN @ERROR_NUM;
      END 

    -- теперь нужно внести изменения в объект "Цена"
    SELECT @SupplItmsInfo = dbo.SetPrice0InXml( @SupplItmsInfo, @PriceImporter );
    SELECT @SupplItmsInfo = dbo.SetPriceInXml( @SupplItmsInfo, @Price );
    SELECT @SupplItmsInfo = dbo.SetPriceCurrencyInXml( @SupplItmsInfo, @PriceInAccountingCurrency );
    SELECT @SupplItmsInfo = dbo.SetDiscountPriceInXml( @SupplItmsInfo, @PriceWithDiscount );
    SELECT @SupplItmsInfo = dbo.SetDiscountPriceCurrencyInXml( @SupplItmsInfo, @PriceWithDiscountInAccountingCurrency );
    SELECT @SupplItmsInfo = dbo.SetPriceList_Price0InXml( @SupplItmsInfo, @PriceImporterFromPriceList );
    SELECT @SupplItmsInfo = dbo.SetPriceList_PriceInXml( @SupplItmsInfo, @PriceFromPriceList );
    SELECT @SupplItmsInfo = dbo.SetPriceList_PriceCurrencyInXml( @SupplItmsInfo, @PriceInAccountingCurrencyFromPriceList );
    SELECT @SupplItmsInfo = dbo.SetNDSPercentInXml( @SupplItmsInfo, @NDSPercent );
    SELECT @SupplItmsInfo = dbo.SetImporterInXml( @SupplItmsInfo, @IsPartsImporter );
    SELECT @SupplItmsInfo = dbo.SetMarkupPercentInXml( @SupplItmsInfo, @ChargesPercent );
    ---- 2009.05.04
    ---- дописываем рассчитанную оптовую надбавку = скидка 
    ---- это необходимо для того, чтобы цены на стороне IB совпадали с ценами в ERP
    --SELECT @SupplItmsInfo = dbo.SetCalcMarkUpPercentInXml( @SupplItmsInfo, @CalcMarkUpPercent );

		SET @ERROR_MES = ' Цена импортера в прайсе: ' + CONVERT( varchar(16), @PriceImporterFromPriceList )+ nChar(13) + nChar(10) + 
			' Цена импортера: ' + CONVERT( varchar(16), @PriceImporter )+ nChar(13) + nChar(10) + 
			' Цена, руб.: ' +  CONVERT( varchar(16), @Price ) + nChar(13) + nChar(10) + 
			' Цена, вал.: ' + CONVERT( varchar(16), @PriceInAccountingCurrency ) + nChar(13) + nChar(10) + 
			' Скидка, %: ' + CONVERT( varchar(8), @DiscountPercent ) + nChar(13) + nChar(10) + 
			' Надбавка (max), %: ' + CONVERT( varchar(8), @ChargesPercent ) + nChar(13) + nChar(10) + 
			--' Надбавка (расчет.), %: ' + CONVERT( varchar(8), @CalcMarkUpPercent ) + nChar(13) + nChar(10) + 
			' Цена со скидкой, руб.: ' + CONVERT( varchar(16), @PriceWithDiscount ) + nChar(13) + nChar(10) + 
			' Цена со скидкой, вал.: ' + CONVERT( varchar(16), @PriceWithDiscountInAccountingCurrency ) + nChar(13) + nChar(10) + 
			' Ставка НДС, %: ' + CONVERT( varchar(8), @NDSPercent ) + nChar(13) + nChar(10) + 
			' Надбавка, %: ' + CONVERT( varchar(8), @ChargesPercent ) + nChar(13) + nChar(10) + 
			' Признак "Импортер": ' + CONVERT( varchar(8), @IsPartsImporter );
			 --+ nChar(13) + nChar(10) + ' Курс: ' + CONVERT( varchar(16), @CurrencyRate );

		EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 90, @RulePool_StepGuid = @RulePool_StepGuid, @EventDscrpn = @ERROR_MES;
    SET @ERROR_NUM = 0;
    
	END TRY
	BEGIN CATCH
		SET @ERROR_NUM = ERROR_NUMBER();
		SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) +  ' [SP_GetPrice_02] Текст ошибки: ' + ERROR_MESSAGE();

		EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 91, @RulePool_StepGuid = @RulePool_StepGuid, @EventDscrpn = @ERROR_MES;
    RETURN @ERROR_NUM;
	END CATCH;

	IF( @ERROR_NUM = 0 )
		SET @ERROR_MES = @ERROR_MES + ' [SP_GetPrice_02] Успешное завершение операции.';
	RETURN @ERROR_NUM;
END


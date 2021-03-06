USE [ERP_Mercury]
GO
/****** Object:  StoredProcedure [dbo].[usp_AddOrder]    Script Date: 04/01/2012 19:25:45 ******/
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
	@bCalcPrices [dbo].[D_YESNO] = 0,

  @Order_Guid D_GUID output,
  @Suppl_Id D_ID output,
  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output

AS

BEGIN

	BEGIN TRY

    SET @ERROR_NUM = 0;
    SET @ERROR_MES = NULL;
    SET @Order_Guid = NULL;
    SET @Suppl_Id = NULL;
    
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
		DECLARE @Suppl_Num int = NULL;
    
    IF( @bCalcPrices = 1 )
			-- шапка заказа и приложение к заказу сохранены в SQL Server,
			-- приступаем к расчёту цен
			EXEC dbo.sp_ProcessPricesInPDASuppl @Suppl_Guid = @Order_Guid, @ERROR_NUM = @ERROR_NUM out, @ERROR_MES = @ERROR_MES out;
		
		IF( @ERROR_NUM = 0 )
			BEGIN
				-- теперь заказ необходимо "разместить" в InterBase
				EXEC dbo.usp_AddOrderToIB @Order_Guid = @Order_Guid, @Suppl_Id = @Suppl_Id out, @Suppl_Num = @Suppl_Num out, 
					@ERROR_NUM = @ERROR_NUM out, @ERROR_MES = @ERROR_MES out;
			END
		
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
		--ROLLBACK TRANSACTION UpdateData;
    SET @ERROR_NUM = ERROR_NUMBER();
    SET @ERROR_MES =  ERROR_MESSAGE();
		RETURN @ERROR_NUM;
	END CATCH;

	IF( @ERROR_NUM = 0 )
		SET @ERROR_MES = 'Успешное завершение операции.';
		
	RETURN @ERROR_NUM;
END


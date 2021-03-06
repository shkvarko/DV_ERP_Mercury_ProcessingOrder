USE [ERP_Mercury]
GO

CREATE NONCLUSTERED INDEX [INDX_T_Waybill_Waybill_BeginDate_2]
ON [dbo].[T_Waybill] ([Waybill_BeginDate])
INCLUDE ([Waybill_Guid])
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Возвращает список записей из ( [dbo].[T_Waybill] )
--
-- Входные параметры:
--
--		@Waybill_DateBegin		- начало периода
--		@Waybill_DateEnd			- окончание периода
--		@Waybill_CompanyGuid	- уи компании
--		@Waybill_StockGuid		- уи склада отгрузки
--		@Waybill_PaymentTypeGuid	- уи формы оплаты
--		@Waybill_CustomerGuid	- уи клиента
--
-- Выходные параметры:
--
--		@ERROR_NUM						- номер ошибки
--		@ERROR_MES						- сообщение об ошибке
--
-- Результат:
--
--    0 - успешное завершение
--    <>0 - ошибка запроса информации из базы данных
--

CREATE PROCEDURE [dbo].[usp_GetWaybillList] 
  @Waybill_DateBegin		D_DATE,
  @Waybill_DateEnd			D_DATE,
  @Waybill_CompanyGuid	D_GUID = NULL,
  @Waybill_StockGuid		D_GUID = NULL,
	@Waybill_PaymentTypeGuid	D_GUID = NULL,
  @Waybill_CustomerGuid	D_GUID = NULL,
	
  @ERROR_NUM						int output,
  @ERROR_MES						nvarchar(4000) output
AS

BEGIN

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = '';

  BEGIN TRY
  
		CREATE TABLE #tmpWaybillList( Waybill_Guid uniqueidentifier, Waybill_Id int, Suppl_Guid uniqueidentifier, 
			Stock_Guid uniqueidentifier, Stock_Name nvarchar(128), Stock_Id int, Stock_IsActive bit, Stock_IsTrade bit, 
			Warehouse_Guid uniqueidentifier, WarehouseType_Guid uniqueidentifier,  
			Company_Guid uniqueidentifier, Company_Id int,  Company_Acronym nvarchar(16), Company_Name nvarchar(128), 
			Currency_Guid  uniqueidentifier, Currency_Abbr nchar(3), Depart_Guid  uniqueidentifier, Depart_Code nchar(3), 
			Customer_Guid  uniqueidentifier, Customer_Id int, Customer_Name nvarchar(128), 
			CustomerStateType_Guid uniqueidentifier, CustomerStateType_ShortName nvarchar(56), 
			CustomerChild_Guid uniqueidentifier, ChildDepart_Guid uniqueidentifier, ChildDepart_Code nvarchar(56), ChildDepart_Name nvarchar( 128 ),
			Rtt_Guid uniqueidentifier, Rtt_Name nvarchar(128), Address_Guid uniqueidentifier, Address_FullName nvarchar( 256 ), 
			PaymentType_Guid uniqueidentifier, PaymentType_Name nvarchar(128), Waybill_Num nvarchar(56), Waybill_BeginDate date, Waybill_DeliveryDate date, 
			WaybillParent_Guid uniqueidentifier, Waybill_Bonus bit, 
			WaybillState_Guid uniqueidentifier, WaybillState_Id int, WaybillState_Name nvarchar(128), 
			WaybillShipMode_Guid uniqueidentifier, WaybillShipMode_Id int, WaybillShipMode_Name nvarchar(128),
			Waybill_ShipDate date, Waybill_Description nvarchar(512), Waybill_CurrencyRate money, 
			Waybill_AllPrice money, Waybill_RetAllPrice money, Waybill_AllDiscount money, Waybill_TotalPrice money,
			Waybill_AmountPaid money, Waybill_Saldo money, Waybill_CurrencyAllPrice money, Waybill_CurrencyRetAllPrice money, 
			Waybill_CurrencyAllDiscount money, Waybill_CurrencyTotalPrice money, Waybill_CurrencyAmountPaid money, 
			Waybill_CurrencySaldo money, Waybill_Quantity float, Waybill_RetQuantity float, Waybill_LeavQuantity float,
			Waybill_Weight decimal(10, 4), Waybill_ShowInDeliveryList bit	);

		WITH WaybillList ( Waybill_Guid ) AS 
		(
			SELECT Waybill_Guid FROM [dbo].[T_Waybill] WHERE [Waybill_BeginDate] BETWEEN @Waybill_DateBegin AND @Waybill_DateEnd
		)
		INSERT INTO #tmpWaybillList( Waybill_Guid, Waybill_Id, Suppl_Guid, 
			Stock_Guid, Stock_Name, Stock_Id, Stock_IsActive, Stock_IsTrade, 
			Warehouse_Guid, WarehouseType_Guid,  
			Company_Guid, Company_Id,  Company_Acronym, Company_Name, 
			Currency_Guid, Currency_Abbr, Depart_Guid, Depart_Code, 
			Customer_Guid, Customer_Id, Customer_Name, 
			CustomerStateType_Guid, CustomerStateType_ShortName,
			CustomerChild_Guid, ChildDepart_Guid, ChildDepart_Code, ChildDepart_Name,
			Rtt_Guid, Rtt_Name, Address_Guid, Address_FullName, 
			PaymentType_Guid, PaymentType_Name, Waybill_Num, Waybill_BeginDate, Waybill_DeliveryDate, 
			WaybillParent_Guid, Waybill_Bonus, 
			WaybillState_Guid, WaybillState_Id, WaybillState_Name, 
			WaybillShipMode_Guid, WaybillShipMode_Id, WaybillShipMode_Name,
			Waybill_ShipDate, Waybill_Description, Waybill_CurrencyRate, 
			Waybill_AllPrice, Waybill_RetAllPrice, Waybill_AllDiscount, Waybill_TotalPrice,
			Waybill_AmountPaid, Waybill_Saldo, Waybill_CurrencyAllPrice, Waybill_CurrencyRetAllPrice, 
			Waybill_CurrencyAllDiscount, Waybill_CurrencyTotalPrice, Waybill_CurrencyAmountPaid, 
			Waybill_CurrencySaldo, Waybill_Quantity, Waybill_RetQuantity, Waybill_LeavQuantity,
			Waybill_Weight, Waybill_ShowInDeliveryList )
		SELECT WaybillList.Waybill_Guid, Waybill.Waybill_Id, Waybill.Suppl_Guid, 
			Waybill.Stock_Guid, Stock.Stock_Name, Stock.Stock_Id, Stock.Stock_IsActive, Stock.Stock_IsTrade, 
			Stock.Warehouse_Guid, Stock.WarehouseType_Guid,  
			Waybill.Company_Guid, Company.Company_Id,  Company.Company_Acronym, Company.Company_Name, 
			Waybill.Currency_Guid, T_Currency.Currency_Abbr, Waybill.Depart_Guid, Depart.Depart_Code, 
			Waybill.Customer_Guid, Customer.Customer_Id, Customer.Customer_Name, 
			Customer.CustomerStateType_Guid, CustomerStateType.CustomerStateType_ShortName,
			Waybill.CustomerChild_Guid, [dbo].[T_CustomerChild].ChildDepart_Guid, [dbo].[T_ChildDepart].ChildDepart_Code, [dbo].[T_ChildDepart].ChildDepart_Name,
			Waybill.Rtt_Guid, Rtt.Rtt_Name, Waybill.Address_Guid, T_Address.Address_Name AS Address_FullName, 
			Waybill.PaymentType_Guid, PaymentType.PaymentType_Name, Waybill.Waybill_Num, Waybill.Waybill_BeginDate, Waybill.Waybill_DeliveryDate, 
			Waybill.WaybillParent_Guid, Waybill.Waybill_Bonus, 
			Waybill.WaybillState_Guid, WaybillState.WaybillState_Id, WaybillState.WaybillState_Name, 
			Waybill.WaybillShipMode_Guid, WaybillShipMode.WaybillShipMode_Id, WaybillShipMode.WaybillShipMode_Name,
			Waybill.Waybill_ShipDate, Waybill.Waybill_Description, Waybill.Waybill_CurrencyRate, 
			Waybill.Waybill_AllPrice, Waybill.Waybill_RetAllPrice, Waybill.Waybill_AllDiscount, Waybill.Waybill_TotalPrice,
			Waybill.Waybill_AmountPaid, Waybill.Waybill_Saldo, Waybill.Waybill_CurrencyAllPrice, Waybill.Waybill_CurrencyRetAllPrice, 
			Waybill.Waybill_CurrencyAllDiscount, Waybill.Waybill_CurrencyTotalPrice, Waybill.Waybill_CurrencyAmountPaid, 
			Waybill.Waybill_CurrencySaldo, Waybill.Waybill_Quantity, Waybill.Waybill_RetQuantity, Waybill.Waybill_LeavQuantity,
			Waybill.Waybill_Weight, Waybill.Waybill_ShowInDeliveryList
		FROM WaybillList INNER JOIN [dbo].[T_Waybill]	AS Waybill 
			ON WaybillList.Waybill_Guid = Waybill.Waybill_Guid  INNER JOIN T_Stock AS Stock
			ON Waybill.Stock_Guid = Stock.Stock_Guid INNER JOIN T_Company AS Company
			ON Waybill.Company_Guid = Company.Company_Guid INNER JOIN T_Currency
			ON Waybill.Currency_Guid = T_Currency.Currency_Guid INNER JOIN T_Depart AS Depart
			ON Waybill.Depart_Guid = Depart.Depart_Guid INNER JOIN T_Customer AS Customer
			ON Waybill.Customer_Guid = Customer.Customer_Guid LEFT OUTER JOIN [dbo].[T_CustomerStateType] AS CustomerStateType
			ON Customer.CustomerStateType_Guid = CustomerStateType.CustomerStateType_Guid	LEFT OUTER JOIN [dbo].[T_CustomerChild] 
			ON Waybill.CustomerChild_Guid = [dbo].[T_CustomerChild].CustomerChild_Guid LEFT OUTER JOIN [dbo].[T_ChildDepart] 
			ON [dbo].[T_CustomerChild].ChildDepart_Guid = [dbo].[T_ChildDepart].ChildDepart_Guid INNER JOIN T_Rtt AS Rtt
			ON Waybill.Rtt_Guid = Rtt.Rtt_Guid INNER JOIN T_Address
			ON Waybill.Address_Guid = T_Address.Address_Guid INNER JOIN T_PaymentType AS PaymentType
			ON Waybill.PaymentType_Guid = PaymentType.PaymentType_Guid INNER JOIN T_WaybillState AS WaybillState
			ON Waybill.WaybillState_Guid = WaybillState.WaybillState_Guid LEFT OUTER JOIN  T_WaybillShipMode AS  WaybillShipMode
			ON Waybill.WaybillShipMode_Guid = WaybillShipMode.WaybillShipMode_Guid
		ORDER BY Waybill.Waybill_BeginDate;
	
		IF( @Waybill_CompanyGuid IS NOT NULL )
			DELETE FROM #tmpWaybillList WHERE ( Company_Guid IS NOT NULL ) AND ( Company_Guid <> @Waybill_CompanyGuid );

		IF( @Waybill_StockGuid IS NOT NULL )
			DELETE FROM #tmpWaybillList WHERE ( Stock_Guid IS NOT NULL ) AND ( Stock_Guid <> @Waybill_StockGuid );

		IF( @Waybill_PaymentTypeGuid IS NOT NULL )
			DELETE FROM #tmpWaybillList WHERE ( PaymentType_Guid IS NOT NULL ) AND ( PaymentType_Guid <> @Waybill_PaymentTypeGuid );

		IF( @Waybill_CustomerGuid IS NOT NULL )
			DELETE FROM #tmpWaybillList WHERE ( Customer_Guid IS NOT NULL ) AND ( Customer_Guid <> @Waybill_CustomerGuid );

		SELECT Waybill_Guid, Waybill_Id, Suppl_Guid, 
			Stock_Guid, Stock_Name, Stock_Id, Stock_IsActive, Stock_IsTrade, 
			Warehouse_Guid, WarehouseType_Guid,  
			Company_Guid, Company_Id,  Company_Acronym, Company_Name, 
			Currency_Guid, Currency_Abbr, Depart_Guid, Depart_Code, 
			Customer_Guid, Customer_Id, Customer_Name, 
			CustomerStateType_Guid, CustomerStateType_ShortName,
			CustomerChild_Guid, ChildDepart_Guid, ChildDepart_Code, ChildDepart_Name,
			Rtt_Guid, Rtt_Name, 
			Address_Guid, CAST( dbo.GetAddressStringForDeliveryList( Address_Guid ) AS nvarchar(256) ) AS Address_FullName, 
			PaymentType_Guid, PaymentType_Name, Waybill_Num, Waybill_BeginDate, Waybill_DeliveryDate, 
			WaybillParent_Guid, Waybill_Bonus, 
			WaybillState_Guid, WaybillState_Id, WaybillState_Name, 
			WaybillShipMode_Guid, WaybillShipMode_Id, WaybillShipMode_Name,
			Waybill_ShipDate, Waybill_Description, Waybill_CurrencyRate, 
			Waybill_AllPrice, Waybill_RetAllPrice, Waybill_AllDiscount, Waybill_TotalPrice,
			Waybill_AmountPaid, Waybill_Saldo, Waybill_CurrencyAllPrice, Waybill_CurrencyRetAllPrice, 
			Waybill_CurrencyAllDiscount, Waybill_CurrencyTotalPrice, Waybill_CurrencyAmountPaid, 
			Waybill_CurrencySaldo, Waybill_Quantity, Waybill_RetQuantity, Waybill_LeavQuantity,
			Waybill_Weight, Waybill_ShowInDeliveryList
		FROM #tmpWaybillList
		ORDER BY Waybill_BeginDate;

	DROP TABLE #tmpWaybillList;

  END TRY
	BEGIN CATCH
		SET @ERROR_NUM = ERROR_NUMBER();
		SET @ERROR_MES = ERROR_MESSAGE();
	END CATCH;

  IF( @ERROR_NUM = 0 )
		SET @ERROR_MES = 'Успешное завершение операции.';

  RETURN @ERROR_NUM;
END

GO
GRANT EXECUTE ON [dbo].[usp_GetWaybillList] TO [public]
GO

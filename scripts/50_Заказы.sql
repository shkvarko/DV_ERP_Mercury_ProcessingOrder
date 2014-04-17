USE [ERP_Mercury]
GO

CREATE NONCLUSTERED INDEX [INDX_T_SupplItem_Suppl_Guid]
ON [dbo].[T_SupplItem] ([Suppl_Guid])
INCLUDE ([SupplItem_OrderQuantity],[SupplItem_Quantity])
GO

ALTER TABLE [dbo].[T_SupplItem] ADD [Splitms_Id] D_ID NULL
GO

 ALTER TABLE [dbo].[T_Suppl] ADD [OrderType_Guid]	D_GUID NULL;
 ALTER TABLE [dbo].[T_Suppl] ADD [PaymentType_Guid]	D_GUID NULL;
 ALTER TABLE [dbo].[T_Suppl] ADD [AgreementWithCustomer_Guid]	D_GUID NULL;
 ALTER TABLE [dbo].[T_Suppl] ADD [SHOW_IN_DELIVERY]	D_YESNO NULL;

ALTER TABLE [dbo].[T_Suppl]  WITH CHECK ADD  CONSTRAINT [FK_T_Suppl_T_PaymentType] FOREIGN KEY([PaymentType_Guid])
REFERENCES [dbo].[T_PaymentType] ([PaymentType_Guid])
GO

ALTER TABLE [dbo].[T_Suppl] CHECK CONSTRAINT [FK_T_Suppl_T_PaymentType]
GO

ALTER TABLE [dbo].[T_Suppl]  WITH CHECK ADD  CONSTRAINT [FK_T_Suppl_T_OrderType] FOREIGN KEY([OrderType_Guid])
REFERENCES [dbo].[T_OrderType] ([OrderType_Guid])
GO

ALTER TABLE [dbo].[T_Suppl] CHECK CONSTRAINT [FK_T_Suppl_T_OrderType]
GO

ALTER TABLE [dbo].[T_Suppl]  WITH CHECK ADD  CONSTRAINT [FK_T_Suppl_T_AgreementWithCustomer] FOREIGN KEY([AgreementWithCustomer_Guid])
REFERENCES [dbo].[T_AgreementWithCustomer] ([AgreementWithCustomer_Guid])
GO

ALTER TABLE [dbo].[T_Suppl] CHECK CONSTRAINT [FK_T_Suppl_T_AgreementWithCustomer]
GO

/****** Object:  View [dbo].[SupplView]    Script Date: 12.12.2013 11:40:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER VIEW [dbo].[SupplView]
AS
SELECT     dbo.T_Suppl.Suppl_Guid, dbo.T_Suppl.Suppl_Id, dbo.T_Suppl.Stock_Guid, dbo.T_Suppl.Currency_Guid, dbo.T_Suppl.Suppl_Num, dbo.T_Suppl.Suppl_Version, 
                      dbo.T_Suppl.Suppl_BeginDate, dbo.T_Suppl.Suppl_DeliveryDate, dbo.T_Suppl.SupplState_Guid, dbo.T_Suppl.Depart_Guid, dbo.T_Suppl.Customer_Guid, 
                      dbo.T_Suppl.Rtt_Guid, dbo.T_Suppl.Address_Guid, dbo.T_Suppl.CustomerChild_Guid, dbo.T_Suppl.Suppl_AllPrice, dbo.T_Suppl.Suppl_AllDiscount, 
                      dbo.T_Suppl.Suppl_TotalPrice, dbo.T_Suppl.Suppl_AmountPaid, dbo.T_Suppl.Suppl_Saldo, dbo.T_Suppl.Suppl_CurrencyAllPrice, dbo.T_Suppl.Parts_Guid, 
                      dbo.T_Suppl.Suppl_Weight, dbo.T_Suppl.Suppl_Quantity, dbo.T_Suppl.SupplParent_Guid, dbo.T_Suppl.Suppl_Note, dbo.T_Suppl.Suppl_Bonus, 
                      dbo.T_Suppl.Suppl_CurrencySaldo, dbo.T_Suppl.Suppl_CurrencyAmountPaid, dbo.T_Suppl.Suppl_CurrencyTotalPrice, dbo.T_Suppl.Suppl_CurrencyAllDiscount, 
                      dbo.T_SupplState.SupplState_Id, dbo.T_SupplState.SupplState_Name, dbo.T_Customer.Customer_Id, dbo.T_Customer.Customer_Name, dbo.T_Stock.Stock_Name, 
                      dbo.T_Depart.Depart_Code, dbo.T_Company.Company_Acronym, dbo.T_Company.Company_Name, dbo.T_CustomerChild.ChildDepart_Guid, 
                      dbo.T_ChildDepart.ChildDepart_Code, dbo.T_ChildDepart.ChildDepart_Name, dbo.T_Suppl.Suppl_ExcludeFromAdj, dbo.T_CustomerChild.CustomerChild_Id, 
                      dbo.T_Stock.Stock_Id, dbo.T_Parts.Parts_Id, dbo.SalesmanView.Salesman_Guid, dbo.SalesmanView.Salesman_Id, dbo.SalesmanView.User_LastName, 
                      dbo.SalesmanView.User_FirstName, dbo.SalesmanView.User_LoginName, dbo.T_Suppl.SHOW_IN_DELIVERY, dbo.T_Suppl.AgreementWithCustomer_Guid, 
                      dbo.T_Suppl.PaymentType_Guid, dbo.T_Suppl.OrderType_Guid, dbo.T_OrderType.OrderType_Name, dbo.T_OrderType.OrderType_Id, 
                      dbo.T_PaymentType.PaymentType_Name, dbo.T_PaymentType.PaymentType_Id, dbo.T_AgreementWithCustomer.Stmnt_Id, 
                      dbo.T_AgreementWithCustomer.Agreement_Guid, dbo.T_Agreement.Agreement_Num, dbo.T_Agreement.Agreement_EndDate, 
                      dbo.T_Agreement.Agreement_BeginDate,
 										  dbo.T_Customer.Customer_UNP, dbo.T_Customer.Customer_OKPO, dbo.T_Customer.Customer_OKULP, 
										  dbo.T_Stock.Warehouse_Guid, dbo.T_Warehouse.Warehouse_Id, dbo.T_Warehouse.Warehouse_Name, dbo.T_Warehouse.Warehouse_IsForShipping, 
											dbo.T_Stock.Company_Guid
FROM             dbo.T_Suppl INNER JOIN
                      dbo.T_SupplState ON dbo.T_Suppl.SupplState_Guid = dbo.T_SupplState.SupplState_Guid INNER JOIN
                      dbo.T_Customer ON dbo.T_Suppl.Customer_Guid = dbo.T_Customer.Customer_Guid INNER JOIN
                      dbo.T_Depart ON dbo.T_Suppl.Depart_Guid = dbo.T_Depart.Depart_Guid LEFT OUTER JOIN
                      dbo.T_AgreementWithCustomer ON dbo.T_Suppl.AgreementWithCustomer_Guid = dbo.T_AgreementWithCustomer.AgreementWithCustomer_Guid LEFT OUTER JOIN
                      dbo.T_Agreement ON dbo.T_AgreementWithCustomer.Agreement_Guid = dbo.T_Agreement.Agreement_Guid LEFT OUTER JOIN
                      dbo.T_PaymentType ON dbo.T_Suppl.PaymentType_Guid = dbo.T_PaymentType.PaymentType_Guid LEFT OUTER JOIN
                      dbo.T_OrderType ON dbo.T_Suppl.OrderType_Guid = dbo.T_OrderType.OrderType_Guid LEFT OUTER JOIN
                      dbo.T_SalesmanDepart ON dbo.T_Depart.Depart_Guid = dbo.T_SalesmanDepart.Depart_Guid LEFT OUTER JOIN
                      dbo.SalesmanView ON dbo.T_SalesmanDepart.Salesman_Guid = dbo.SalesmanView.Salesman_Guid LEFT OUTER JOIN
                      dbo.T_Parts ON dbo.T_Suppl.Parts_Guid = dbo.T_Parts.Parts_Guid LEFT OUTER JOIN
                      dbo.T_CustomerChild ON dbo.T_Suppl.CustomerChild_Guid = dbo.T_CustomerChild.CustomerChild_Guid LEFT OUTER JOIN
                      dbo.T_ChildDepart ON dbo.T_CustomerChild.ChildDepart_Guid = dbo.T_ChildDepart.ChildDepart_Guid LEFT OUTER JOIN
                      dbo.T_Stock ON dbo.T_Suppl.Stock_Guid = dbo.T_Stock.Stock_Guid LEFT OUTER JOIN
											dbo.T_Warehouse ON dbo.T_Stock.Warehouse_Guid = dbo.T_Warehouse.Warehouse_Guid LEFT OUTER JOIN
                      dbo.T_Company ON dbo.T_Stock.Company_Guid = dbo.T_Company.Company_Guid
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Возвращает список заказов клиентов
--
-- Входные параметры:
--		@BeginDate	D_DATE	- начало периода
--		@EndDate		D_DATE	- конец периода
--
-- Выходные параметры:
--
-- Результат:
--    0 - успешное завершение
--    <>0 - ошибка запроса информации из базы данных

ALTER PROCEDURE [dbo].[usp_GetSuppl] 
	@BeginDate D_DATE = NULL,
	@EndDate D_DATE = NULL,

  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output
AS

BEGIN

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = '';

  BEGIN TRY

		IF( @BeginDate IS NULL )
			BEGIN
				SET @BeginDate = ( SELECT dbo.TrimTime( GetDate() ) );
				SET @EndDate =  DateAdd( day, 1,  @BeginDate );
			END
		ELSE
			IF ( Datediff( day,  @BeginDate, @EndDate ) = 0 )
				SET @EndDate = dbo.TrimTime( DateAdd( day, 1,  @BeginDate ) )
			ELSE 
				SET @EndDate = dbo.TrimTime( DateAdd( day, 1,  @EndDate ) );

		DECLARE @SupplStateSupplIsBasic_Guid	D_GUID;
		SELECT @SupplStateSupplIsBasic_Guid = [SupplState_Guid] FROM [dbo].[T_SupplState] WHERE [SupplState_Id] = 5;	-- Основной
		
		WITH SupplParent AS
		(
			SELECT Suppl_Guid	FROM T_Suppl
			WHERE Suppl_BeginDate BETWEEN @BeginDate AND @EndDate
				AND SupplState_Guid <> @SupplStateSupplIsBasic_Guid
		),
		SupplItem AS
		 (
				SELECT DISTINCT SupplParent.Suppl_Guid,
					SUM( SupplItem.SupplItem_OrderQuantity ) OVER( PARTITION BY SupplItem.Suppl_Guid ) AS ORDERQUANTITY,
					SUM( SupplItem.SupplItem_Quantity ) OVER( PARTITION BY SupplItem.Suppl_Guid ) AS QUANTITY,
					COUNT( SupplItem.Suppl_Guid ) OVER( PARTITION BY SupplItem.Suppl_Guid ) AS POSQUANTITY
				FROM T_SupplItem AS SupplItem, SupplParent
				WHERE SupplParent.Suppl_Guid = SupplItem.Suppl_Guid
		)
			SELECT Suppl.Suppl_Guid, Suppl.Suppl_BeginDate, Suppl.Suppl_DeliveryDate, Suppl.Suppl_Bonus, Suppl.Suppl_Num, Suppl.Suppl_Version, 
				Suppl.SupplState_Guid, Suppl.SupplState_Id, Suppl.SupplState_Name,
				Suppl.Suppl_AllPrice, Suppl.Suppl_AllDiscount, Suppl.Suppl_TotalPrice, 
				Suppl.Suppl_CurrencyAllPrice, Suppl.Suppl_CurrencyAllDiscount, Suppl.Suppl_CurrencyTotalPrice,
				Suppl.Customer_Id, Suppl.Customer_Name, Suppl.ChildDepart_Code,
				Suppl.ChildDepart_Code, Suppl.Depart_Code, Suppl.Stock_Guid, Suppl.Stock_Name,  
				Suppl.Company_Acronym, Suppl.Company_Name, 
				Suppl.Suppl_Weight, Suppl.Suppl_ExcludeFromAdj,
				dbo.GetConditionGroupListForCustomer(Suppl.Customer_Guid) as GroupList,
				SupplItem.ORDERQUANTITY, SupplItem.QUANTITY, SupplItem.POSQUANTITY, 
				Suppl.Depart_Guid, Suppl.Salesman_Guid, Suppl.Salesman_Id, Suppl.User_LastName, Suppl.User_FirstName, Suppl.User_LoginName, 
				Suppl.Customer_Guid, Suppl.CustomerChild_Guid, Suppl.ChildDepart_Guid, Suppl.ChildDepart_Name, 
				Suppl.OrderType_Guid, Suppl.OrderType_Id, Suppl.OrderType_Name,
				Suppl.PaymentType_Guid, Suppl.PaymentType_Id, Suppl.PaymentType_Name,
				Suppl.AgreementWithCustomer_Guid, Suppl.Agreement_Guid, Suppl.Agreement_Num, Suppl.Agreement_BeginDate, Suppl.Agreement_EndDate, 
				Suppl.Rtt_Guid, Suppl.Address_Guid, Suppl.SHOW_IN_DELIVERY, Suppl.Parts_Guid, Suppl.Parts_Id, 
				Suppl.Customer_UNP, Suppl.Customer_OKPO, Suppl.Customer_OKULP, Suppl.Suppl_Id,
				Suppl.Warehouse_Guid, Suppl.Warehouse_Id, Suppl.Warehouse_Name, Suppl.Warehouse_IsForShipping, Suppl.Company_Guid
			FROM [dbo].[SupplView] AS Suppl, SupplParent, SupplItem
			WHERE Suppl.Suppl_Guid = SupplParent.Suppl_Guid 
				AND Suppl.Suppl_Guid = SupplItem.Suppl_Guid
			ORDER BY Suppl.Suppl_BeginDate;
					
					
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

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[SupplView]
AS
SELECT     dbo.T_Suppl.Suppl_Guid, dbo.T_Suppl.Suppl_Id, dbo.T_Suppl.Stock_Guid, dbo.T_Suppl.Currency_Guid, dbo.T_Suppl.Suppl_Num, dbo.T_Suppl.Suppl_Version, 
                      dbo.T_Suppl.Suppl_BeginDate, dbo.T_Suppl.Suppl_DeliveryDate, dbo.T_Suppl.SupplState_Guid, dbo.T_Suppl.Depart_Guid, dbo.T_Suppl.Customer_Guid, 
                      dbo.T_Suppl.Rtt_Guid, dbo.T_Suppl.Address_Guid, dbo.T_Suppl.CustomerChild_Guid, dbo.T_Suppl.Suppl_AllPrice, dbo.T_Suppl.Suppl_AllDiscount, 
                      dbo.T_Suppl.Suppl_TotalPrice, dbo.T_Suppl.Suppl_AmountPaid, dbo.T_Suppl.Suppl_Saldo, dbo.T_Suppl.Suppl_CurrencyAllPrice, dbo.T_Suppl.Parts_Guid, 
                      dbo.T_Suppl.Suppl_Weight, dbo.T_Suppl.Suppl_Quantity, dbo.T_Suppl.SupplParent_Guid, dbo.T_Suppl.Suppl_Note, dbo.T_Suppl.Suppl_Bonus, 
                      dbo.T_Suppl.Suppl_CurrencySaldo, dbo.T_Suppl.Suppl_CurrencyAmountPaid, dbo.T_Suppl.Suppl_CurrencyTotalPrice, dbo.T_Suppl.Suppl_CurrencyAllDiscount, 
                      dbo.T_SupplState.SupplState_Id, dbo.T_SupplState.SupplState_Name, dbo.T_Customer.Customer_Id, dbo.T_Customer.Customer_Name, dbo.T_Stock.Stock_Name, 
                      dbo.T_Depart.Depart_Code, dbo.T_Company.Company_Acronym, dbo.T_Company.Company_Name, dbo.T_CustomerChild.ChildDepart_Guid, 
                      dbo.T_ChildDepart.ChildDepart_Code, dbo.T_ChildDepart.ChildDepart_Name, dbo.T_Suppl.Suppl_ExcludeFromAdj, dbo.T_CustomerChild.CustomerChild_Id, 
                      dbo.T_Stock.Stock_Id, dbo.T_Parts.Parts_Id, dbo.SalesmanView.Salesman_Guid, dbo.SalesmanView.Salesman_Id, dbo.SalesmanView.User_LastName, 
                      dbo.SalesmanView.User_FirstName, dbo.SalesmanView.User_LoginName, dbo.T_Suppl.SHOW_IN_DELIVERY, dbo.T_Suppl.AgreementWithCustomer_Guid, 
                      dbo.T_Suppl.PaymentType_Guid, dbo.T_Suppl.OrderType_Guid, dbo.T_OrderType.OrderType_Name, dbo.T_OrderType.OrderType_Id, 
                      dbo.T_PaymentType.PaymentType_Name, dbo.T_PaymentType.PaymentType_Id, dbo.T_AgreementWithCustomer.Stmnt_Id, 
                      dbo.T_AgreementWithCustomer.Agreement_Guid, dbo.T_Agreement.Agreement_Num, dbo.T_Agreement.Agreement_EndDate, dbo.T_Agreement.Agreement_BeginDate, 
                      dbo.T_Customer.Customer_UNP, dbo.T_Customer.Customer_OKPO, dbo.T_Customer.Customer_OKULP, dbo.T_Stock.Warehouse_Guid, 
                      dbo.T_Warehouse.Warehouse_Id, dbo.T_Warehouse.Warehouse_Name, dbo.T_Warehouse.Warehouse_IsForShipping, dbo.T_Stock.Company_Guid, 
                      dbo.SalesmanView.User_Guid, dbo.T_Rtt.Rtt_Code, dbo.T_Rtt.Rtt_Name
FROM         dbo.T_Suppl INNER JOIN
                      dbo.T_SupplState ON dbo.T_Suppl.SupplState_Guid = dbo.T_SupplState.SupplState_Guid INNER JOIN
                      dbo.T_Customer ON dbo.T_Suppl.Customer_Guid = dbo.T_Customer.Customer_Guid INNER JOIN
                      dbo.T_Depart ON dbo.T_Suppl.Depart_Guid = dbo.T_Depart.Depart_Guid LEFT OUTER JOIN
                      dbo.T_AgreementWithCustomer ON dbo.T_Suppl.AgreementWithCustomer_Guid = dbo.T_AgreementWithCustomer.AgreementWithCustomer_Guid LEFT OUTER JOIN
                      dbo.T_Agreement ON dbo.T_AgreementWithCustomer.Agreement_Guid = dbo.T_Agreement.Agreement_Guid LEFT OUTER JOIN
                      dbo.T_PaymentType ON dbo.T_Suppl.PaymentType_Guid = dbo.T_PaymentType.PaymentType_Guid LEFT OUTER JOIN
                      dbo.T_OrderType ON dbo.T_Suppl.OrderType_Guid = dbo.T_OrderType.OrderType_Guid LEFT OUTER JOIN
                      dbo.T_SalesmanDepart ON dbo.T_Depart.Depart_Guid = dbo.T_SalesmanDepart.Depart_Guid LEFT OUTER JOIN
                      dbo.SalesmanView ON dbo.T_SalesmanDepart.Salesman_Guid = dbo.SalesmanView.Salesman_Guid LEFT OUTER JOIN
                      dbo.T_Parts ON dbo.T_Suppl.Parts_Guid = dbo.T_Parts.Parts_Guid LEFT OUTER JOIN
                      dbo.T_CustomerChild ON dbo.T_Suppl.CustomerChild_Guid = dbo.T_CustomerChild.CustomerChild_Guid LEFT OUTER JOIN
                      dbo.T_ChildDepart ON dbo.T_CustomerChild.ChildDepart_Guid = dbo.T_ChildDepart.ChildDepart_Guid LEFT OUTER JOIN
                      dbo.T_Stock ON dbo.T_Suppl.Stock_Guid = dbo.T_Stock.Stock_Guid LEFT OUTER JOIN
                      dbo.T_Warehouse ON dbo.T_Stock.Warehouse_Guid = dbo.T_Warehouse.Warehouse_Guid LEFT OUTER JOIN
                      dbo.T_Company ON dbo.T_Stock.Company_Guid = dbo.T_Company.Company_Guid LEFT OUTER JOIN
                      dbo.T_Rtt ON dbo.T_Suppl.Rtt_Guid = dbo.T_Rtt.Rtt_Guid

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Возвращает список заказов клиентов
--
-- Входные параметры:
--		@BeginDate	D_DATE	- начало периода
--		@EndDate		D_DATE	- конец периода
--
-- Выходные параметры:
--
-- Результат:
--    0 - успешное завершение
--    <>0 - ошибка запроса информации из базы данных

ALTER PROCEDURE [dbo].[usp_GetSuppl] 
	@Order_Guid D_GUID = NULL,
	@Customer_Guid D_GUID = NULL,
	@Company_Guid D_GUID = NULL,
	@Stock_Guid D_GUID = NULL,
	@PaymentType_Guid D_GUID = NULL,
	@BeginDate D_DATE = NULL,
	@EndDate D_DATE = NULL,

  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output
AS

BEGIN

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = '';

  BEGIN TRY

		IF( @BeginDate IS NULL )
			BEGIN
				SET @BeginDate = ( SELECT dbo.TrimTime( GetDate() ) );
				SET @EndDate =  DateAdd( day, 1,  @BeginDate );
			END
		ELSE
			IF ( Datediff( day,  @BeginDate, @EndDate ) = 0 )
				SET @EndDate = dbo.TrimTime( DateAdd( day, 1,  @BeginDate ) )
			ELSE 
				SET @EndDate = dbo.TrimTime( DateAdd( day, 1,  @EndDate ) );

		DECLARE @SupplStateSupplIsBasic_Guid	D_GUID;
		SELECT @SupplStateSupplIsBasic_Guid = [SupplState_Guid] FROM [dbo].[T_SupplState] WHERE [SupplState_Id] = 5;	-- Основной
		
		CREATE TABLE #SRCSUPPL( Suppl_Guid uniqueidentifier, Customer_Guid uniqueidentifier, Stock_Guid uniqueidentifier, 
			Company_Guid uniqueidentifier, PaymentType_Guid uniqueidentifier, SupplState_Guid uniqueidentifier );
		INSERT INTO #SRCSUPPL( Suppl_Guid, Customer_Guid, Stock_Guid, PaymentType_Guid, SupplState_Guid )
		SELECT Suppl_Guid, Customer_Guid, Stock_Guid, PaymentType_Guid, SupplState_Guid
		FROM T_Suppl WHERE Suppl_BeginDate BETWEEN @BeginDate AND @EndDate;
			--AND SupplState_Guid <> @SupplStateSupplIsBasic_Guid;

		DELETE FROM #SRCSUPPL WHERE ( SupplState_Guid IS NOT NULL ) AND ( SupplState_Guid = @SupplStateSupplIsBasic_Guid );
		
		UPDATE #SRCSUPPL SET Company_Guid = ( SELECT T_Stock.Company_Guid FROM T_Stock WHERE T_Stock.Stock_Guid = #SRCSUPPL.Stock_Guid )
		WHERE #SRCSUPPL.Stock_Guid IS NOT NULL;

		IF( @Customer_Guid IS NOT NULL )
			DELETE FROM #SRCSUPPL WHERE ( Customer_Guid IS NOT NULL ) AND ( Customer_Guid <> @Customer_Guid );
		IF( @Company_Guid IS NOT NULL )
			DELETE FROM #SRCSUPPL WHERE ( Company_Guid IS NOT NULL ) AND ( Company_Guid <> @Company_Guid );
		IF( @Stock_Guid IS NOT NULL )
			DELETE FROM #SRCSUPPL WHERE ( Stock_Guid IS NOT NULL ) AND ( Stock_Guid <> @Stock_Guid );
		IF( @PaymentType_Guid IS NOT NULL )
			DELETE FROM #SRCSUPPL WHERE ( PaymentType_Guid IS NOT NULL ) AND ( PaymentType_Guid <> @PaymentType_Guid );

		WITH SupplParent AS
		(
			SELECT Suppl_Guid	FROM #SRCSUPPL
			--WHERE Suppl_BeginDate BETWEEN @BeginDate AND @EndDate
			--	AND SupplState_Guid <> @SupplStateSupplIsBasic_Guid
		),
		SupplItem AS
		 (
				SELECT DISTINCT SupplParent.Suppl_Guid,
					SUM( SupplItem.SupplItem_OrderQuantity ) OVER( PARTITION BY SupplItem.Suppl_Guid ) AS ORDERQUANTITY,
					SUM( SupplItem.SupplItem_Quantity ) OVER( PARTITION BY SupplItem.Suppl_Guid ) AS QUANTITY,
					COUNT( SupplItem.Suppl_Guid ) OVER( PARTITION BY SupplItem.Suppl_Guid ) AS POSQUANTITY
				FROM T_SupplItem AS SupplItem, SupplParent
				WHERE SupplParent.Suppl_Guid = SupplItem.Suppl_Guid
		)
			SELECT Suppl.Suppl_Guid, Suppl.Suppl_BeginDate, Suppl.Suppl_DeliveryDate, Suppl.Suppl_Bonus, Suppl.Suppl_Num, Suppl.Suppl_Version, 
				Suppl.SupplState_Guid, Suppl.SupplState_Id, Suppl.SupplState_Name,
				Suppl.Suppl_AllPrice, Suppl.Suppl_AllDiscount, Suppl.Suppl_TotalPrice, 
				Suppl.Suppl_CurrencyAllPrice, Suppl.Suppl_CurrencyAllDiscount, Suppl.Suppl_CurrencyTotalPrice,
				Suppl.Customer_Id, Suppl.Customer_Name, Suppl.ChildDepart_Code,
				Suppl.ChildDepart_Code, Suppl.Depart_Code, Suppl.Stock_Guid, Suppl.Stock_Name,  
				Suppl.Company_Acronym, Suppl.Company_Name, 
				Suppl.Suppl_Weight, Suppl.Suppl_ExcludeFromAdj,
				dbo.GetConditionGroupListForCustomer(Suppl.Customer_Guid) as GroupList,
				SupplItem.ORDERQUANTITY, SupplItem.QUANTITY, SupplItem.POSQUANTITY, 
				Suppl.Depart_Guid, Suppl.Salesman_Guid, Suppl.Salesman_Id, Suppl.User_Guid, Suppl.User_LastName, Suppl.User_FirstName, Suppl.User_LoginName, 
				Suppl.Customer_Guid, Suppl.CustomerChild_Guid, Suppl.ChildDepart_Guid, Suppl.ChildDepart_Name, 
				Suppl.OrderType_Guid, Suppl.OrderType_Id, Suppl.OrderType_Name,
				Suppl.PaymentType_Guid, Suppl.PaymentType_Id, Suppl.PaymentType_Name,
				Suppl.AgreementWithCustomer_Guid, Suppl.Agreement_Guid, Suppl.Agreement_Num, Suppl.Agreement_BeginDate, Suppl.Agreement_EndDate, 
				Suppl.Rtt_Guid, Suppl.Address_Guid, Suppl.SHOW_IN_DELIVERY, Suppl.Parts_Guid, Suppl.Parts_Id, 
				Suppl.Customer_UNP, Suppl.Customer_OKPO, Suppl.Customer_OKULP, Suppl.Suppl_Id,
				Suppl.Warehouse_Guid, Suppl.Warehouse_Id, Suppl.Warehouse_Name, Suppl.Warehouse_IsForShipping, Suppl.Company_Guid, Suppl.Suppl_Note, 
				Suppl.Rtt_Code, Suppl.Rtt_Name, Suppl.Stock_Id, 
				[dbo].[AddressView].AddressName
			FROM [dbo].[SupplView] AS Suppl INNER JOIN 
				SupplParent ON Suppl.Suppl_Guid = SupplParent.Suppl_Guid INNER JOIN 
				SupplItem ON Suppl.Suppl_Guid = SupplItem.Suppl_Guid LEFT OUTER JOIN 
				[dbo].[AddressView] ON Suppl.Address_Guid = [dbo].[AddressView].Address_Guid
			ORDER BY Suppl.Suppl_BeginDate;
					
		DROP TABLE #SRCSUPPL;

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


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Возвращает одну запись из ( dbo.T_ChildDepart )
--
-- Входящие параметры:
--
--		@ChildDepart_Guid	- уи дочернего подразделения
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
  SET @ERROR_MES = '';

  BEGIN TRY

		SELECT Top 1 ChildDepart_Guid, ChildDepart_Code, ChildDepart_Main, ChildDepart_NotActive, ChildDepart_MaxDebt, 
			ChildDepart_MaxDelay, ChildDepart_Email, ChildDepart_Name
		FROM dbo.ChildDepartView	
		WHERE ChildDepart_Guid  = @ChildDepart_Guid;

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
GRANT EXECUTE ON [dbo].[usp_GetChildDepart2] TO [public]
GO

 ALTER TABLE [dbo].[T_SupplItem] ADD SupplItem_NDSPercent D_MONEY NULL
 GO

 UPDATE [dbo].[T_SupplItem] SET SupplItem_NDSPercent = 0
 GO

 ALTER TABLE [dbo].[T_SupplItem] ADD SupplItem_PriceImporter D_MONEY NULL
 GO

 UPDATE [dbo].[T_SupplItem] SET SupplItem_PriceImporter = 0
 GO


/****** Object:  View [dbo].[OrderItmsView]    Script Date: 21.12.2013 9:39:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[OrderItmsView]
AS
SELECT     dbo.T_SupplItem.Suppl_Guid AS Order_Guid, dbo.T_Measure.Measure_Id, dbo.T_Measure.Measure_Name, dbo.T_Measure.Measure_ShortName, dbo.T_SupplItem.Parts_Guid, 
	dbo.T_SupplItem.Measure_Guid, dbo.T_SupplItem.SupplItem_Quantity AS OrderItms_Quantity, dbo.T_SupplItem.SupplItem_Guid AS OrderItms_Guid, 
	dbo.PartsView.Parts_Id, dbo.PartsView.Currency_Guid, 
  dbo.PartsView.Owner_Guid, dbo.PartsView.Parttype_Guid, dbo.PartsView.Barcode, dbo.PartsView.Partsubtype_Guid, dbo.PartsView.Country_Guid, 
  dbo.PartsView.Currency_Abbr, dbo.PartsView.Currency_Code, dbo.PartsView.Owner_Id, dbo.PartsView.Owner_Name, dbo.PartsView.Owner_ShortName, 
  dbo.PartsView.Owner_Description, dbo.PartsView.Owner_IsActive, dbo.PartsView.Vtm_Guid, dbo.PartsView.Vtm_Id, dbo.PartsView.Vtm_Name, 
  dbo.PartsView.Vtm_ShortName, dbo.PartsView.Vtm_IsActive, dbo.PartsView.Parttype_Id, dbo.PartsView.Parttype_NDSRate, dbo.PartsView.Parttype_DemandsName, 
  dbo.PartsView.Partsubtype_Id, dbo.PartsView.Partsubtype_Name, dbo.PartsView.Partsubtype_IsActive, dbo.PartsView.PartLine_Guid, dbo.PartsView.Partline_Id, 
  dbo.PartsView.Partline_Name, dbo.PartsView.Partline_IsActive, dbo.PartsView.Parttype_Name, dbo.PartsView.Country_Name, dbo.PartsView.Country_Code, 
  dbo.PartsView.Parttype_IsActive, dbo.PartsView.PartsCategory_Guid, dbo.PartsView.PartsCategory_Id, dbo.PartsView.PartsCategory_Name, 
  dbo.PartsView.Parts_OriginalName, dbo.PartsView.Parts_Name, dbo.PartsView.Parts_ShortName, dbo.PartsView.Parts_BoxQuantity, 
  dbo.PartsView.Parts_PackQuantity, dbo.PartsView.Parts_Weight, dbo.PartsView.Parts_IsActive, dbo.PartsView.Parts_Article, 
  
	( dbo.T_SupplItem.SupplItem_Quantity * dbo.T_SupplItem.SupplItem_CurrencyDiscountPrice ) AS Orderitms_SumReservedWithDiscountInAccountingCurrency, 
	( dbo.T_SupplItem.SupplItem_Quantity * dbo.T_SupplItem.SupplItem_CurrencyPrice ) AS Orderitms_SumReservedInAccountingCurrency, 
	( dbo.T_SupplItem.SupplItem_Quantity * dbo.T_SupplItem.SupplItem_DiscountPrice ) AS Orderitms_SumReservedWithDiscount, 

	( dbo.T_SupplItem.SupplItem_OrderQuantity * dbo.T_SupplItem.SupplItem_CurrencyDiscountPrice ) AS Orderitms_SumOrderedWithDiscountInAccountingCurrency, 

	( dbo.T_SupplItem.SupplItem_Quantity * dbo.T_SupplItem.SupplItem_Price ) AS Orderitms_SumReserved, 

	( dbo.T_SupplItem.SupplItem_OrderQuantity * dbo.T_SupplItem.SupplItem_CurrencyPrice ) AS Orderitms_SumOrderedInAccountingCurrency, 
  
	( dbo.T_SupplItem.SupplItem_OrderQuantity * dbo.T_SupplItem.SupplItem_DiscountPrice ) AS Orderitms_SumOrderedWithDiscount, 

	( dbo.T_SupplItem.SupplItem_OrderQuantity * dbo.T_SupplItem.SupplItem_Price ) AS Orderitms_SumOrdered, 

	dbo.T_SupplItem.SupplItem_DiscountPrice AS OrderItms_PriceWithDiscount,
	dbo.T_SupplItem.SupplItem_CurrencyDiscountPrice AS OrderItms_PriceWithDiscountInAccountingCurrency,
	dbo.T_SupplItem.SupplItem_CurrencyPrice AS OrderItms_PriceInAccountingCurrency,
	dbo.T_SupplItem.SupplItem_NDSPercent AS OrderItms_NDSPercent,
	dbo.T_SupplItem.SupplItem_Discount AS OrderItms_DiscountPercent,
	dbo.T_SupplItem.SupplItem_Price AS OrderItms_Price,
	dbo.T_SupplItem.SupplItem_PriceImporter AS OrderItms_PriceImporter,
	dbo.T_SupplItem.SupplItem_OrderQuantity AS OrderItms_QuantityOrdered
FROM         dbo.T_SupplItem INNER JOIN
                      dbo.T_Measure ON dbo.T_SupplItem.Measure_Guid = dbo.T_Measure.Measure_Guid INNER JOIN
                      dbo.PartsView ON dbo.T_SupplItem.Parts_Guid = dbo.PartsView.Parts_Guid


GO
GRANT SELECT ON [dbo].[OrderItmsView] TO [public] WITH GRANT OPTION 
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Возвращает описание адреса
--
-- Входящие параметры:
--  @Address_Guid - уникальный идентификатор адреса

-- Выходные параметры:
--
-- Результат:
--    0 - успешное завершение
--    <>0 - ошибка запроса информации из базы данных

CREATE PROCEDURE [dbo].[sp_GetAddress] 
  @Address_Guid D_GUID,
  
	@ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output
AS

BEGIN

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = '';

  BEGIN TRY

    SELECT Country.Country_Name, Country.Country_Code, 
      Oblast.Country_Guid, Oblast.Oblast_Name, Oblast.Oblast_Code, 
      Region.Region_Guid, Region.Oblast_Guid, Region.Region_Name, Region.Region_Code, 
      LocalityPrefix.LocalityPrefix_Name, LocalityPrefix.LocalityPrefix_NameShort, LocalityPrefix.LocalityPrefix_IsDefault,
      City.City_Guid, City.City_Name, City.LocalityPrefix_Guid, City.Region_Guid, 
      Addres.Address_Guid, Addres.Address_Postindex, 
      Addres.AddressPrefix_Guid, dbo.GetAddressPrefixName( Addres.AddressPrefix_Guid ) as AddressPrefix_Name, 
      dbo.GetAddressPrefixNameShort( Addres.AddressPrefix_Guid ) as AddressPrefix_NameShort, 
      dbo.GetAddressPrefixIsDefault( Addres.AddressPrefix_Guid ) as AddressPrefix_IsDefault,
      Addres.AddressType_Guid, AddressType.AddressType_Name, AddressType.AddressType_Description, AddressType.AddressType_IsDefault,
			Addres.Address_Name, 
			Addres.Building_Guid, dbo.GetBuildingNameShort( Addres.Building_Guid ) as BuildingNameShort, Addres.Address_BuildCode, 
			Addres.SubBuilding_Guid, dbo.GetSubBuildingNameShort( Addres.SubBuilding_Guid ) as SubBuildingNameShort, Addres.Address_SubBuildingCode, 
			Addres.Flat_Guid, dbo.GetFlatNameShort( Addres.Flat_Guid ) as FlatNameShort, Addres.Address_FlatCode, Addres.Address_Description
    FROM dbo.T_Address as Addres, dbo.T_AddressType as AddressType,
      dbo.T_City as City, dbo.T_Region as Region, dbo.T_Country as Country, dbo.T_LocalityPrefix as LocalityPrefix, dbo.T_Oblast as Oblast
    WHERE Addres.Address_Guid = @Address_Guid
      AND Addres.City_Guid = City.City_Guid
      AND Addres.AddressType_Guid = AddressType.AddressType_Guid
      AND City.Region_Guid = Region.Region_Guid
      AND Region.Oblast_Guid = Oblast.Oblast_Guid
			AND City.LocalityPrefix_Guid = LocalityPrefix.LocalityPrefix_Guid
			AND Oblast.Country_Guid = Country.Country_Guid
    ORDER BY Country.Country_Name, City.City_Name, Addres.Address_Name;

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
GRANT EXECUTE ON [dbo].[sp_GetAddress] TO [public]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Возвращает список записей из ( dbo.OrderItmsView )
--
-- Входящие параметры:
--		@Order_Guid - УИ заказа
--
-- Выходные параметры:
--
-- Результат:
--    0 - успешное завершение
--    <>0 - ошибка запроса информации из базы данных

CREATE PROCEDURE [dbo].[usp_GetOrderItms] 
	@Order_Guid D_GUID,
	
  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output
AS

BEGIN

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = '';



  BEGIN TRY
		
		SELECT Order_Guid, Measure_Id, Measure_Name, Measure_ShortName, Parts_Guid, Measure_Guid, 
			OrderItms_Quantity, OrderItms_Guid, Parts_Id, Currency_Guid, Owner_Guid, Parttype_Guid, 
			Barcode, Partsubtype_Guid, Country_Guid, Currency_Abbr, Currency_Code, Owner_Id, Owner_Name, 
			Owner_ShortName, Owner_Description, Owner_IsActive, Vtm_Guid, Vtm_Id, Vtm_Name, Vtm_ShortName, 
			Vtm_IsActive, Parttype_Id, Parttype_NDSRate, Parttype_DemandsName, Partsubtype_Id, 
			Partsubtype_Name, Partsubtype_IsActive, PartLine_Guid, Partline_Id, Partline_Name, 
			Partline_IsActive, Parttype_Name, Country_Name, Country_Code, Parttype_IsActive, 
			PartsCategory_Guid, PartsCategory_Id, PartsCategory_Name, Parts_OriginalName, Parts_Name, 
			Parts_ShortName, Parts_BoxQuantity, Parts_PackQuantity, Parts_Weight, Parts_IsActive, 
			Parts_Article,
      Orderitms_SumReservedWithDiscountInAccountingCurrency, Orderitms_SumReservedInAccountingCurrency, 
      Orderitms_SumReservedWithDiscount, Orderitms_SumOrderedWithDiscountInAccountingCurrency, 
      Orderitms_SumReserved, Orderitms_SumOrderedInAccountingCurrency, 
      Orderitms_SumOrderedWithDiscount, Orderitms_SumOrdered, 
      OrderItms_PriceWithDiscountInAccountingCurrency, 
      OrderItms_PriceInAccountingCurrency, OrderItms_NDSPercent, OrderItms_PriceWithDiscount, 
      OrderItms_DiscountPercent, OrderItms_Price, OrderItms_PriceImporter, OrderItms_QuantityOrdered
			
		FROM dbo.OrderItmsView
		WHERE Order_Guid = @Order_Guid
		ORDER BY Parts_Name, Parts_Article;
		
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
GRANT EXECUTE ON [dbo].[usp_GetOrderItms] TO [public]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_GetAutoCreatePriceInfo] 
	@iAutoCreatePrice int output,

  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output

AS

BEGIN
SET NOCOUNT ON;

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = '';
  SET @iAutoCreatePrice = 0;

	BEGIN TRY

	  SET @iAutoCreatePrice = 0;

		--DECLARE @strAutoCreatePrice D_NAME = NULL;
		--SELECT @strAutoCreatePrice = Param_Value FROM dbo.T_Tools WHERE Param_Guid = 'E9364768-273A-4D8C-8F23-CDEF3D69C3A2';
		
		--IF( @strAutoCreatePrice IS NOT NULL )
		--	SET @iAutoCreatePrice = CONVERT( int, @strAutoCreatePrice );
		--ELSE
		--	SET @iAutoCreatePrice = 0;

	END TRY
	BEGIN CATCH
		SET @ERROR_NUM = ERROR_NUMBER();
		SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) +  ' [usp_GetAutoCreatePrice] Текст ошибки: ' + ERROR_MESSAGE();

    RETURN @ERROR_NUM;
	END CATCH;

	IF( @ERROR_NUM = 0 )
		SET @ERROR_MES = @ERROR_MES + ' [usp_GetAutoCreatePrice] Успешное завершение операции.';
		
	RETURN @ERROR_NUM;
END

GO
GRANT EXECUTE ON [dbo].[usp_GetAutoCreatePriceInfo] TO [public]
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Возвращает список договоров клиента для заказа
--
-- Входящие параметры:
--
--		@Customer_Guid	- уи клиента
--		@Company_Guid		- уи компании
--		@Order_Guid			- уи заказа

-- Выходные параметры:
--
-- Результат:
--    0 - успешное завершение
--    <>0 - ошибка запроса информации из базы данных

CREATE PROCEDURE [dbo].[usp_GetStmnt] 
	@Customer_Guid	D_GUID = NULL,
	@Company_Guid		D_GUID = NULL,
	@Order_Guid			D_GUID = NULL,
	
  @ERROR_NUM			int output,
  @ERROR_MES			nvarchar(4000) output
AS

BEGIN

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = '';

  BEGIN TRY
		IF( ( @Customer_Guid IS NULL ) AND ( @Company_Guid IS NULL ) AND ( @Order_Guid IS NULL ) )
			BEGIN
				SELECT [dbo].[T_AgreementWithCustomer].[AgreementWithCustomer_Guid], [dbo].[T_AgreementWithCustomer].Stmnt_Id, 
					[dbo].[T_Agreement].Agreement_Num AS Stmnt_FullNum, [dbo].[T_Agreement].Agreement_BeginDate AS Stmnt_BeginDate, [dbo].[T_Agreement].Agreement_EndDate AS Stmnt_EndDate, 
					[dbo].[T_AgreementWithCustomer].Customer_Guid, [dbo].[T_AgreementWithCustomer].Company_Guid, 
					cast( 0 as bit ) AS Stmnt_PrintDiscountInWaybill, 
					cast( 0 as bit ) AS Stmnt_BlockDiscount
				FROM [dbo].[T_AgreementWithCustomer] INNER JOIN [dbo].[T_Agreement] ON [dbo].[T_AgreementWithCustomer].Agreement_Guid = [dbo].[T_Agreement].Agreement_Guid
				ORDER BY Stmnt_BeginDate, Stmnt_FullNum;
			END
		ELSE IF( @Order_Guid IS NOT NULL ) 	
			BEGIN
				DECLARE @AgreementWithCustomer_Guid D_GUID = NULL;
				SELECT @AgreementWithCustomer_Guid = AgreementWithCustomer_Guid
				FROM [dbo].[T_Suppl] WHERE [Suppl_Guid] = @Order_Guid;
				
				IF( @AgreementWithCustomer_Guid IS NOT NULL )
					SELECT [dbo].[T_AgreementWithCustomer].[AgreementWithCustomer_Guid], [dbo].[T_AgreementWithCustomer].Stmnt_Id, 
						[dbo].[T_Agreement].Agreement_Num AS Stmnt_FullNum, [dbo].[T_Agreement].Agreement_BeginDate AS Stmnt_BeginDate, [dbo].[T_Agreement].Agreement_EndDate AS Stmnt_EndDate, 
						[dbo].[T_AgreementWithCustomer].Customer_Guid, [dbo].[T_AgreementWithCustomer].Company_Guid, 
						cast( 0 as bit ) AS Stmnt_PrintDiscountInWaybill, 
						cast( 0 as bit ) AS Stmnt_BlockDiscount
					FROM [dbo].[T_AgreementWithCustomer] INNER JOIN [dbo].[T_Agreement] ON [dbo].[T_AgreementWithCustomer].Agreement_Guid = [dbo].[T_Agreement].Agreement_Guid
					WHERE [dbo].[T_AgreementWithCustomer].[AgreementWithCustomer_Guid] = @AgreementWithCustomer_Guid;
			END
		ELSE IF( ( @Customer_Guid IS NOT NULL ) AND ( @Company_Guid IS NOT NULL ) )	
			BEGIN
				SELECT [dbo].[T_AgreementWithCustomer].[AgreementWithCustomer_Guid], [dbo].[T_AgreementWithCustomer].Stmnt_Id, 
					[dbo].[T_Agreement].Agreement_Num AS Stmnt_FullNum, MAX( [dbo].[T_Agreement].Agreement_BeginDate ) AS Stmnt_BeginDate, [dbo].[T_Agreement].Agreement_EndDate AS Stmnt_EndDate, 
					[dbo].[T_AgreementWithCustomer].Customer_Guid, [dbo].[T_AgreementWithCustomer].Company_Guid, 
					cast( 0 as bit ) AS Stmnt_PrintDiscountInWaybill, 
					cast( 0 as bit ) AS Stmnt_BlockDiscount
				FROM [dbo].[T_AgreementWithCustomer] INNER JOIN [dbo].[T_Agreement] ON [dbo].[T_AgreementWithCustomer].Agreement_Guid = [dbo].[T_Agreement].Agreement_Guid
				WHERE [dbo].[T_AgreementWithCustomer].Customer_Guid = @Customer_Guid
					AND [dbo].[T_AgreementWithCustomer].Company_Guid = @Company_Guid
				GROUP BY [dbo].[T_AgreementWithCustomer].[AgreementWithCustomer_Guid], [dbo].[T_AgreementWithCustomer].Stmnt_Id, 
					[dbo].[T_Agreement].Agreement_Num, [dbo].[T_Agreement].Agreement_EndDate, 
					[dbo].[T_AgreementWithCustomer].Customer_Guid, [dbo].[T_AgreementWithCustomer].Company_Guid
				ORDER BY Stmnt_BeginDate, Stmnt_FullNum;
			END

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
GRANT EXECUTE ON [dbo].[usp_GetStmnt] TO [public]
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Creation date:	
-- Author:			

CREATE FUNCTION [dbo].[GetPriceForParts] ( @Parts_Guid D_GUID, @PartsubtypePriceType_Guid D_GUID )
RETURNS D_MONEY
WITH EXECUTE AS caller
AS
BEGIN
  
	DECLARE @Price D_MONEY = 0;

	SELECT @Price = [Price_Value] FROM [dbo].[T_PartsPriceList] 
	WHERE Parts_Guid = @Parts_Guid
		AND [PartsubtypePriceType_Guid] = @PartsubtypePriceType_Guid;
	
	IF( @Price IS NULL ) SET @Price = 0;

	RETURN @Price;
end

GO
GRANT EXECUTE ON [dbo].[GetPriceForParts] TO [public]
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Возвращает текущие остатки
--
-- Входные параметры:
--
--		@Stock_Guid						уи склада
--		@Order_Guid						уи заказа
--		@IBLINKEDSERVERNAME		LinkedSever к InterBase
--
-- Выходные параметры:
--
--		@ERROR_NUM						номер ошибки
--		@ERROR_MES						текст ошибки
--
-- Результат:
--    0										успешное завершение
--    <>0									ошибка запроса информации из базы данных

CREATE PROCEDURE [dbo].[usp_GetPartInstock] 
	@Stock_Guid						D_GUID,
	@Order_Guid						D_GUID = NULL,
  @IBLINKEDSERVERNAME		D_NAME = NULL,

  @ERROR_NUM						int output,
  @ERROR_MES						nvarchar(4000) output
AS

BEGIN

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = NULL;

  BEGIN TRY
  
	  IF( @IBLINKEDSERVERNAME IS NULL ) SELECT @IBLINKEDSERVERNAME = dbo.GetIBLinkedServerName();

		DECLARE @strSQLText nvarchar(2048);
    DECLARE @Stock_Id int;
		DECLARE @PartsubtypePriceTypePrice0_Guid	D_GUID;
		SELECT @PartsubtypePriceTypePrice0_Guid = PartsubtypePriceType_Guid 
		FROM T_PartsubtypePriceType WHERE PartsubtypePriceType_Abbr = 'PRICE0';
    
    IF( @Order_Guid IS NOT NULL )
			BEGIN
				WITH SupplItem (Parts_Guid)
				AS
				(
						SELECT Parts_Guid FROM [dbo].[T_SupplItem] WHERE [Suppl_Guid] = @Order_Guid
				)
				SELECT SupplItem.Parts_Guid, dbo.PartsView.Parts_Id, Barcode, 
					Parts_OriginalName, Parts_Name, Parts_ShortName, Parts_Article, Parts_PackQuantity, Parts_PackQuantityForCalc, Parts_BoxQuantity, 
					Parts_Weight, Parts_PlasticContainerWeight, Parts_PaperContainerWeight, Parts_AlcoholicContentPercent, 
					Parts_VendorPrice, Parts_IsActive, Parts_ActualNotValid, Parts_NotValid, Parts_Certificate,
					Country_Guid, Country_Name, Country_Code,
					Currency_Guid, Currency_Abbr, Currency_Code, 
					Measure_Guid, Measure_Id, Measure_Name, Measure_ShortName, 
					Record_Updated, Record_UserUdpated, 
					Owner_Guid, Owner_Id, Owner_Name, Owner_ShortName, Owner_Description, Owner_IsActive, Owner_ProcessDaysCount, 
					Vtm_Guid, Vtm_Id, Vtm_Name, Vtm_ShortName, Vtm_Description, Vtm_IsActive, 
					Parttype_Guid, Parttype_Id, Parttype_Name, Parttype_NDSRate, Parttype_Description, Parttype_DemandsName, Parttype_IsActive,
					Partsubtype_Guid, Partsubtype_Id, Partsubtype_Name, Partsubtype_Description, Partsubtype_Image, Partsubtype_IsActive, 
					PartLine_Guid, Partline_Id, Partline_Name, Partline_Description, Partline_IsActive,
					Parts_CodeTNVD, Parts_Reference,
					PartsCategory_Guid, PartsCategory_Id, PartsCategory_Name, PartsCategory_Description, PartsCategory_IsActive, 
					dbo.GetPriceForParts( SupplItem.Parts_Guid, @PartsubtypePriceTypePrice0_Guid ) as Parts0,
					( Parts_Name + ' ' + Parts_Article ) AS PartsFullName, 0 as PartsIsCheck, 
					dbo.IsProductIncludeInStock(SupplItem.Parts_Guid) AS IsProductIncludeInStock, 
					0 as STOCK_QTY, 0 as STOCK_RESQTY, 1 as PARTS_MINRETAILQTY, 1 as PARTS_MINWHOLESALEQTY, Parts_PackQuantity as PARTS_PACKQTY
				FROM SupplItem INNER JOIN dbo.PartsView ON SupplItem.Parts_Guid = dbo.PartsView.Parts_Guid
				--WHERE dbo.PartsView.Parts_Guid IN ( SELECT Parts_Guid FROM [dbo].[T_SupplItem] WHERE [Suppl_Guid] = @Order_Guid )
			END
		ELSE	
			BEGIN
				SELECT @Stock_Id = Stock_Id FROM dbo.T_Stock 
				WHERE Stock_Guid = @Stock_Guid;
				
				CREATE TABLE #INSTOCK( PARTS_ID int, STOCK_QTY float, STOCK_RESQTY float, PARTS_MINRETAILQTY float,
					PARTS_MINWHOLESALEQTY float, PARTS_PACKQTY float );

				SELECT @strSQLText = dbo.GetTextQueryForSelectFromInterbase( null, null, 
				'SELECT PARTS_ID, STOCK_QTY, STOCK_RESQTY, PARTS_MINRETAILQTY, PARTS_MINWHOLESALEQTY, PARTS_PACKQTY 
							FROM SP_GETINSTOCK( ' +  	CAST( @Stock_Id as varchar(20)) +  ' ) WHERE STOCK_QTY <> 0 ' );
				SET @strSQLText = 'INSERT INTO #INSTOCK( PARTS_ID, STOCK_QTY, STOCK_RESQTY, PARTS_MINRETAILQTY, PARTS_MINWHOLESALEQTY, PARTS_PACKQTY ) ' + @strSQLText;

				EXECUTE SP_EXECUTESQL @strSQLText;
				
				--SELECT * FROM #INSTOCK;
				
				SELECT Parts_Guid, dbo.PartsView.Parts_Id, Barcode, 
					Parts_OriginalName, Parts_Name, Parts_ShortName, Parts_Article, Parts_PackQuantity, Parts_PackQuantityForCalc, Parts_BoxQuantity, 
					Parts_Weight, Parts_PlasticContainerWeight, Parts_PaperContainerWeight, Parts_AlcoholicContentPercent, 
					Parts_VendorPrice, Parts_IsActive, Parts_ActualNotValid, Parts_NotValid, Parts_Certificate,
					Country_Guid, Country_Name, Country_Code,
					Currency_Guid, Currency_Abbr, Currency_Code, 
					Measure_Guid, Measure_Id, Measure_Name, Measure_ShortName, 
					Record_Updated, Record_UserUdpated, 
					Owner_Guid, Owner_Id, Owner_Name, Owner_ShortName, Owner_Description, Owner_IsActive, Owner_ProcessDaysCount, 
					Vtm_Guid, Vtm_Id, Vtm_Name, Vtm_ShortName, Vtm_Description, Vtm_IsActive, 
					Parttype_Guid, Parttype_Id, Parttype_Name, Parttype_NDSRate, Parttype_Description, Parttype_DemandsName, Parttype_IsActive,
					Partsubtype_Guid, Partsubtype_Id, Partsubtype_Name, Partsubtype_Description, Partsubtype_Image, Partsubtype_IsActive, 
					PartLine_Guid, Partline_Id, Partline_Name, Partline_Description, Partline_IsActive,
					Parts_CodeTNVD, Parts_Reference,
					PartsCategory_Guid, PartsCategory_Id, PartsCategory_Name, PartsCategory_Description, PartsCategory_IsActive, 
					dbo.GetPriceForParts( Parts_Guid, @PartsubtypePriceTypePrice0_Guid ) as Parts0,
					( Parts_Name + ' ' + Parts_Article ) AS PartsFullName, 0 as PartsIsCheck, 
					dbo.IsProductIncludeInStock(Parts_Guid) AS IsProductIncludeInStock, 
					#INSTOCK.STOCK_QTY, #INSTOCK.STOCK_RESQTY, #INSTOCK.PARTS_MINRETAILQTY, #INSTOCK.PARTS_MINWHOLESALEQTY, #INSTOCK.PARTS_PACKQTY
				FROM dbo.PartsView, #INSTOCK
				WHERE dbo.PartsView.Parts_Id = #INSTOCK.PARTS_ID
				--WHERE Parts_Id IN ( SELECT PARTS_ID FROM #INSTOCK )	
				ORDER BY Parts_Name;  

				DROP TABLE #INSTOCK;
			END
      
		
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
GRANT EXECUTE ON [dbo].[usp_GetPartInstock] TO [public]
GO

GO
CREATE NONCLUSTERED INDEX [INDX_T_Parts_Currency_Guid]
ON [dbo].[T_Parts] ([Currency_Guid])
INCLUDE ([Parts_Guid],[Parts_Id],[Parts_OriginalName],[Parts_Name],[Parts_ShortName],[Parts_Article],[Parts_PackQuantity],[Parts_BoxQuantity],[Parts_Weight],[Parts_IsActive])
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Возвращает список записей из ( dbo.OrderItmsView )
--
-- Входящие параметры:
--		@Order_Guid - УИ заказа
--
-- Выходные параметры:
--
-- Результат:
--    0 - успешное завершение
--    <>0 - ошибка запроса информации из базы данных

ALTER PROCEDURE [dbo].[usp_GetOrderItms] 
	@Order_Guid D_GUID,
	
  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output
AS

BEGIN

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = '';



  BEGIN TRY
		
		IF( @Order_Guid IS NOT NULL )
			BEGIN
				WITH OrderItms ( Order_Guid, Parts_Guid, Measure_Guid, OrderItms_Quantity, OrderItms_Guid, 
							Orderitms_SumReservedWithDiscountInAccountingCurrency, 
							Orderitms_SumReservedInAccountingCurrency, 
							Orderitms_SumReservedWithDiscount, 
							Orderitms_SumOrderedWithDiscountInAccountingCurrency, 
							Orderitms_SumReserved, 
							Orderitms_SumOrderedInAccountingCurrency, 
							Orderitms_SumOrderedWithDiscount, 
							Orderitms_SumOrdered, 
							OrderItms_PriceWithDiscount,
							OrderItms_PriceWithDiscountInAccountingCurrency,
							OrderItms_PriceInAccountingCurrency,
							OrderItms_NDSPercent,
							OrderItms_DiscountPercent,
							OrderItms_Price,
							OrderItms_PriceImporter,
							OrderItms_QuantityOrdered, 
							Measure_Id, Measure_Name, Measure_ShortName)
					AS
					(
							SELECT dbo.T_SupplItem.Suppl_Guid AS Order_Guid,  dbo.T_SupplItem.Parts_Guid, 
							dbo.T_SupplItem.Measure_Guid, dbo.T_SupplItem.SupplItem_Quantity AS OrderItms_Quantity, dbo.T_SupplItem.SupplItem_Guid AS OrderItms_Guid, 
  
							( dbo.T_SupplItem.SupplItem_Quantity * dbo.T_SupplItem.SupplItem_CurrencyDiscountPrice ) AS Orderitms_SumReservedWithDiscountInAccountingCurrency, 
							( dbo.T_SupplItem.SupplItem_Quantity * dbo.T_SupplItem.SupplItem_CurrencyPrice ) AS Orderitms_SumReservedInAccountingCurrency, 
							( dbo.T_SupplItem.SupplItem_Quantity * dbo.T_SupplItem.SupplItem_DiscountPrice ) AS Orderitms_SumReservedWithDiscount, 

							( dbo.T_SupplItem.SupplItem_OrderQuantity * dbo.T_SupplItem.SupplItem_CurrencyDiscountPrice ) AS Orderitms_SumOrderedWithDiscountInAccountingCurrency, 

							( dbo.T_SupplItem.SupplItem_Quantity * dbo.T_SupplItem.SupplItem_Price ) AS Orderitms_SumReserved, 

							( dbo.T_SupplItem.SupplItem_OrderQuantity * dbo.T_SupplItem.SupplItem_CurrencyPrice ) AS Orderitms_SumOrderedInAccountingCurrency, 
  
							( dbo.T_SupplItem.SupplItem_OrderQuantity * dbo.T_SupplItem.SupplItem_DiscountPrice ) AS Orderitms_SumOrderedWithDiscount, 

							( dbo.T_SupplItem.SupplItem_OrderQuantity * dbo.T_SupplItem.SupplItem_Price ) AS Orderitms_SumOrdered, 

							dbo.T_SupplItem.SupplItem_DiscountPrice AS OrderItms_PriceWithDiscount,
							dbo.T_SupplItem.SupplItem_CurrencyDiscountPrice AS OrderItms_PriceWithDiscountInAccountingCurrency,
							dbo.T_SupplItem.SupplItem_CurrencyPrice AS OrderItms_PriceInAccountingCurrency,
							dbo.T_SupplItem.SupplItem_NDSPercent AS OrderItms_NDSPercent,
							dbo.T_SupplItem.SupplItem_Discount AS OrderItms_DiscountPercent,
							dbo.T_SupplItem.SupplItem_Price AS OrderItms_Price,
							dbo.T_SupplItem.SupplItem_PriceImporter AS OrderItms_PriceImporter,
							dbo.T_SupplItem.SupplItem_OrderQuantity AS OrderItms_QuantityOrdered, 
							dbo.T_Measure.Measure_Id, dbo.T_Measure.Measure_Name, dbo.T_Measure.Measure_ShortName
						FROM  dbo.T_SupplItem INNER JOIN
							dbo.T_Measure ON T_SupplItem.Measure_Guid = dbo.T_Measure.Measure_Guid
						WHERE Suppl_Guid = @Order_Guid
					)
					SELECT Order_Guid, OrderItms.Measure_Id, OrderItms.Measure_Name, OrderItms.Measure_ShortName, 
						OrderItms.Parts_Guid, OrderItms.Measure_Guid, 
						OrderItms_Quantity, OrderItms_Guid, Parts_Id, Currency_Guid, Owner_Guid, Parttype_Guid, 
						Barcode, Partsubtype_Guid, Country_Guid, Currency_Abbr, Currency_Code, Owner_Id, Owner_Name, 
						Owner_ShortName, Owner_Description, Owner_IsActive, Vtm_Guid, Vtm_Id, Vtm_Name, Vtm_ShortName, 
						Vtm_IsActive, Parttype_Id, Parttype_NDSRate, Parttype_DemandsName, Partsubtype_Id, 
						Partsubtype_Name, Partsubtype_IsActive, PartLine_Guid, Partline_Id, Partline_Name, 
						Partline_IsActive, Parttype_Name, Country_Name, Country_Code, Parttype_IsActive, 
						PartsCategory_Guid, PartsCategory_Id, PartsCategory_Name, Parts_OriginalName, Parts_Name, 
						Parts_ShortName, Parts_BoxQuantity, Parts_PackQuantity, Parts_Weight, Parts_IsActive, 
						Parts_Article,
						Orderitms_SumReservedWithDiscountInAccountingCurrency, Orderitms_SumReservedInAccountingCurrency, 
						Orderitms_SumReservedWithDiscount, Orderitms_SumOrderedWithDiscountInAccountingCurrency, 
						Orderitms_SumReserved, Orderitms_SumOrderedInAccountingCurrency, 
						Orderitms_SumOrderedWithDiscount, Orderitms_SumOrdered, 
						OrderItms_PriceWithDiscountInAccountingCurrency, 
						OrderItms_PriceInAccountingCurrency, OrderItms_NDSPercent, OrderItms_PriceWithDiscount, 
						OrderItms_DiscountPercent, OrderItms_Price, OrderItms_PriceImporter, OrderItms_QuantityOrdered
					FROM OrderItms INNER JOIN
						--dbo.T_Measure ON OrderItms.Measure_Guid = dbo.T_Measure.Measure_Guid INNER JOIN
						dbo.PartsView ON OrderItms.Parts_Guid = dbo.PartsView.Parts_Guid
					ORDER BY Parts_Name, Parts_Article;
			END
		
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

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- Creation date:	
-- Author:			

CREATE FUNCTION [dbo].[GetPaymentTypeForm1Guid] ()
RETURNS D_GUID
WITH EXECUTE AS caller
AS
BEGIN
  
DECLARE @PaymentType_Guid D_GUID;
SELECT TOP 1 @PaymentType_Guid = [PaymentType_Guid] FROM [dbo].[T_PaymentType] WHERE [PaymentType_Id] = 1;

RETURN @PaymentType_Guid;
end

GO
GRANT EXECUTE ON [dbo].[GetPaymentTypeForm1Guid] TO [public]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[GetPaymentTypeForm2Guid] ()
RETURNS D_GUID
WITH EXECUTE AS caller
AS
BEGIN
  
DECLARE @PaymentType_Guid D_GUID;


SELECT TOP 1 @PaymentType_Guid = [PaymentType_Guid] FROM [dbo].[T_PaymentType] WHERE [PaymentType_Id] = 2;

RETURN @PaymentType_Guid;

end

GO
GRANT EXECUTE ON [dbo].[GetPaymentTypeForm2Guid] TO [public]
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_GetCustomerDebtInfoFromIB]
	@Customer_Guid [dbo].[D_GUID],
	@CustomerChild_Guid [dbo].[D_GUID] = NULL,
	@PaymentType_Guid [dbo].[D_GUID],
	@Company_Guid [dbo].[D_GUID],
	@Order_Guid [dbo].[D_GUID] = NULL,
  @WAYBILL_TOTALPRICE money,
  @WAYBILL_CURRENCYTOTALPRICE money,  
  @WAYBILL_MONEYBONUS bit,
  @IBLINKEDSERVERNAME dbo.D_NAME = NULL,

  @WAYBILL_SHIPPED_SALDO money output,
  @WAYBILL_SHIPPED_DEBTDAYS int output,
  @WAYBILL_SALDO money output,
  @WAYBILL_DEBTDAYS int output,
  @INITIAL_DEBT money output,
  @INITIAL_DEBTDAYS int output,
  @SUPPL_SALDO money output,
  @EARNING_SALDO money output,
  @CUSTOMER_LIMITPRICE money output,
  @CUSTOMER_LIMITDAYS int output,
  @OVERDRAFT money output,
  @CUSTOMER_DEBTPRICE money output,
  @CUSTOMER_DEBTDAYS int output,
  @SUMM_IS_PASS int output,

  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output
AS

BEGIN

  BEGIN TRY
    SET @ERROR_NUM = 0;
    SET @ERROR_MES = '';

		SET @WAYBILL_SHIPPED_SALDO = 0;
		SET @WAYBILL_SHIPPED_DEBTDAYS = 0;
		SET @WAYBILL_SALDO = 0;
		SET @WAYBILL_DEBTDAYS = 0;
		SET @INITIAL_DEBT = 0;
		SET @INITIAL_DEBTDAYS = 0;
		SET @SUPPL_SALDO = 0;
		SET @EARNING_SALDO = 0;
		SET @CUSTOMER_LIMITPRICE = 0;
		SET @CUSTOMER_LIMITDAYS = 0;
		SET @OVERDRAFT = 0;
		SET @CUSTOMER_DEBTPRICE = 0;
		SET @CUSTOMER_DEBTDAYS = 0;
		SET @SUMM_IS_PASS = 0;
  
    DECLARE @CUSTOMER_ID int;
    DECLARE @CHILDCUST_ID int = 0;
    DECLARE @COMPANY_ID int;
    DECLARE @SUPPL_ID int = 0;
    DECLARE @WAYBILL_ID int = 0;
    DECLARE @WAYBILL_CURRENCYCODE varchar(3);
    DECLARE @NullGuid D_GUID;
    SELECT @NullGuid = dbo.GetNullGuid();
    
    
 	  IF( @IBLINKEDSERVERNAME IS NULL ) SELECT @IBLINKEDSERVERNAME = dbo.GetIBLinkedServerName();

    -- Проверяем наличие дочернего подраздления с указанным идентификатором
    IF( ( @CustomerChild_Guid IS NOT NULL ) AND ( @CustomerChild_Guid <> @NullGuid ) )
			BEGIN
				IF NOT EXISTS ( SELECT CustomerChild_Guid FROM dbo.T_CustomerChild WHERE ChildDepart_Guid = @CustomerChild_Guid )
					BEGIN
						SET @ERROR_NUM = 1;
						SET @ERROR_MES = 'В базе данных не найден дочерний клиент с указанным идетнификатором.' + Char(13) + 
							'УИ: ' + Char(9) + CONVERT( nvarchar(36), @CustomerChild_Guid );
						RETURN @ERROR_NUM;
					END
				ELSE	
					SELECT @CustomerChild_Guid = CustomerChild_Guid, @CHILDCUST_ID = CustomerChild_Id FROM dbo.T_CustomerChild WHERE ChildDepart_Guid = @CustomerChild_Guid;
			END
		ELSE
			SET @CHILDCUST_ID	= 0;
			
		SELECT @CUSTOMER_ID = Customer_Id FROM T_Customer WHERE Customer_Guid = @Customer_Guid;
		SELECT @COMPANY_ID = Company_Id FROM dbo.T_Company WHERE Company_Guid = @Company_Guid;
		
		DECLARE @PaymentTypeForm1Guid D_GUID;
		DECLARE	@PaymentTypeForm2Guid D_GUID;
		
		SELECT @PaymentTypeForm1Guid = dbo.GetPaymentTypeForm1Guid();
		SELECT @PaymentTypeForm2Guid = dbo.GetPaymentTypeForm2Guid();
		
		IF( @PaymentType_Guid = @PaymentTypeForm1Guid )
			SET @WAYBILL_CURRENCYCODE = 'BYB';
		ELSE	IF( @PaymentType_Guid = @PaymentTypeForm2Guid )
			SET @WAYBILL_CURRENCYCODE = 'EUR' ;
		
		IF( ( @Order_Guid IS NOT NULL )	 AND ( @Order_Guid <> @NullGuid ) )
			SELECT @SUPPL_ID = Suppl_Id FROM T_Suppl WHERE Suppl_Guid = @Order_Guid;
		
    DECLARE @SQLString nvarchar( 2048);
    DECLARE @ParmDefinition nvarchar(2048);
    
    PRINT 'Check 1';
    
    SET @ParmDefinition = N'@WAYBILL_SHIPPED_SALDOsql money output, @WAYBILL_SHIPPED_DEBTDAYSsql int output, 
			@WAYBILL_SALDOsql money output, @WAYBILL_DEBTDAYSsql int output, @INITIAL_DEBTsql money output, 
			@INITIAL_DEBTDAYSsql int output, @SUPPL_SALDOsql money output, @EARNING_SALDOsql money output, 
			@CUSTOMER_LIMITPRICEsql money output, @CUSTOMER_LIMITDAYSsql int output, @OVERDRAFTsql money output,
			@CUSTOMER_DEBTPRICEsql money output, @CUSTOMER_DEBTDAYSsql int output, @SUMM_IS_PASSsql int output';

    SET @SQLString = 'select @WAYBILL_SHIPPED_SALDOsql = WAYBILL_SHIPPED_SALDO, @WAYBILL_SHIPPED_DEBTDAYSsql = WAYBILL_SHIPPED_DEBTDAYS, 
			@WAYBILL_SALDOsql = WAYBILL_SALDO, @WAYBILL_DEBTDAYSsql = WAYBILL_DEBTDAYS, @INITIAL_DEBTsql = INITIAL_DEBT, 
			@INITIAL_DEBTDAYSsql = INITIAL_DEBTDAYS, @SUPPL_SALDOsql = SUPPL_SALDO, @EARNING_SALDOsql = EARNING_SALDO, 
			@CUSTOMER_LIMITPRICEsql = CUSTOMER_LIMITPRICE, @CUSTOMER_LIMITDAYSsql = CUSTOMER_LIMITDAYS, @OVERDRAFTsql = OVERDRAFT,
			@CUSTOMER_DEBTPRICEsql = CUSTOMER_DEBTPRICE, @CUSTOMER_DEBTDAYSsql = CUSTOMER_DEBTDAYS, @SUMM_IS_PASSsql = SUMM_IS_PASS  from openquery( ' + 
			@IBLINKEDSERVERNAME + ', ''select WAYBILL_SHIPPED_SALDO, WAYBILL_SHIPPED_DEBTDAYS, 
			WAYBILL_SALDO, WAYBILL_DEBTDAYS, INITIAL_DEBT, 
			INITIAL_DEBTDAYS, SUPPL_SALDO, EARNING_SALDO, 
			CUSTOMER_LIMITPRICE, CUSTOMER_LIMITDAYS, OVERDRAFT,
			CUSTOMER_DEBTPRICE, CUSTOMER_DEBTDAYS, SUMM_IS_PASS FROM SP_GETCUSTOMERDEBTINFO( ' +
					cast( @CUSTOMER_ID as nvarchar( 20)) + ', ' + cast( @CHILDCUST_ID as nvarchar( 20)) + ', ' + cast( @COMPANY_ID as nvarchar( 20)) + ', ' +
					cast( @SUPPL_ID as nvarchar( 20 )) + ', ' + cast( @WAYBILL_ID as nvarchar( 20 )) + ', ' +  cast( @WAYBILL_TOTALPRICE as nvarchar( 56)) + ', ' + 
					cast( @WAYBILL_CURRENCYTOTALPRICE as nvarchar( 56)) + ', ''''' + @WAYBILL_CURRENCYCODE + ''''', ' + 
					cast( @WAYBILL_MONEYBONUS as nvarchar( 20)) + ')'')'; 

		PRINT @SQLString;
		
    EXECUTE sp_executesql @SQLString, @ParmDefinition, @WAYBILL_SHIPPED_SALDOsql = @WAYBILL_SHIPPED_SALDO output, 
			@WAYBILL_SHIPPED_DEBTDAYSsql = @WAYBILL_SHIPPED_DEBTDAYS output, 
			@WAYBILL_SALDOsql = @WAYBILL_SALDO output, @WAYBILL_DEBTDAYSsql = @WAYBILL_DEBTDAYS output, 
			@INITIAL_DEBTsql = @INITIAL_DEBT output, 
			@INITIAL_DEBTDAYSsql = @INITIAL_DEBTDAYS output, @SUPPL_SALDOsql = @SUPPL_SALDO output, @EARNING_SALDOsql = @EARNING_SALDO output, 
			@CUSTOMER_LIMITPRICEsql = @CUSTOMER_LIMITPRICE output, @CUSTOMER_LIMITDAYSsql = @CUSTOMER_LIMITDAYS output, @OVERDRAFTsql = @OVERDRAFT output,
			@CUSTOMER_DEBTPRICEsql = @CUSTOMER_DEBTPRICE output, @CUSTOMER_DEBTDAYSsql = @CUSTOMER_DEBTDAYS output, @SUMM_IS_PASSsql = @SUMM_IS_PASS output;


 	END TRY
	BEGIN CATCH
    SET @ERROR_NUM = ERROR_NUMBER();
    SET @ERROR_MES = '[usp_GetCustomerDebtInfoFromIB]: ' + ERROR_MESSAGE();
    PRINT @ERROR_MES;

		RETURN @ERROR_NUM;
	END CATCH;

  IF( @ERROR_NUM = 0 )
		SET @ERROR_MES = '[usp_GetCustomerDebtInfoFromIB] Успешное завершение операции.';

	RETURN @ERROR_NUM;

END

GO
GRANT EXECUTE ON [dbo].[usp_GetCustomerDebtInfoFromIB] TO [public]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_IsCustomerInBL] 
	@Customer_Guid D_GUID, 
	@Company_Guid D_GUID,
	
  @bIsCustomerInBlackList bit output,
  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output
AS

BEGIN

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = '';
  SET @bIsCustomerInBlackList = NULL;

  BEGIN TRY

		SELECT @bIsCustomerInBlackList =  dbo.[IsCustomerInBlackList]( @Customer_Guid, @Company_Guid );

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
GRANT EXECUTE ON [dbo].[usp_IsCustomerInBL] TO [public]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Возвращает цену для заданного товара, формы оплаты и склада отгрузки

--		Входные параметры

--		@Parts_Guid					УИ товара
--		@PaymentType_Guid		УИ формы оплаты
--		@Stock_Guid					УИ склада
--		@DiscountPercent		% скидки
--		
--		Выходные параметры

--		@NDSPercent					ставка НДС, %
--		@PriceImporter			отпускная цена без НДС, BYB
--		@Price							отпускная цена с НДС без учета скидки, BYB
--		@PriceWithDiscount	отпускная цена с НДС с учетом скидки, BYB
--		@PriceInAccountingCurrency							отпускная цена в валюте учёта без учета скидки
--		@PriceWithDiscountInAccountingCurrency	отпускная цена в валюте учёта с учетом скидки
--		@ERROR_NUM					номер ошибки
--		@ERROR_MES					текст ошибки

--		Возвращает
--
--		0			- удачное завершение операции
--		<> 0	- ошибка

CREATE PROCEDURE [dbo].[usp_GetPrice] 
	@Parts_Guid					D_GUID,
	@PaymentType_Guid		D_GUID,
	@Stock_Guid					D_GUID,
	@DiscountPercent		decimal(18, 4) = 0,

	@NDSPercent					D_MONEY	output,
  @PriceImporter			D_MONEY output,
  @Price							D_MONEY output,
  @PriceWithDiscount	D_MONEY output,
  @PriceInAccountingCurrency							D_MONEY output,
  @PriceWithDiscountInAccountingCurrency	D_MONEY output,

  @ERROR_NUM					int output,
  @ERROR_MES					nvarchar(4000) output

AS

BEGIN
SET NOCOUNT ON;

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = '';

  SET @PriceImporter = 0;
  SET @Price = 0;
  SET @PriceWithDiscount = 0;
  SET @PriceInAccountingCurrency = 0;
  SET @PriceWithDiscountInAccountingCurrency = 0;
  SET @NDSPercent = 0;

  DECLARE @IsPartsImporter D_YESNO  -- признак "товар импортера" 
  DECLARE @ChargesPercent D_MONEY;  -- процент надбавки по товару
  
	BEGIN TRY


    -- Признак "товар импортера"
    SELECT @IsPartsImporter = dbo.GetPropertieImporterForStockGuid( @Stock_Guid, @Parts_Guid );
    IF ( @IsPartsImporter IS NULL )
      BEGIN
        SET @ERROR_NUM = 1;
        SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) +  ' [usp_GetPrice] Не удалось определить признак "товар импортера".';

	      RETURN @ERROR_NUM;
      END 

    -- Ставка НДС, %
    SELECT @NDSPercent = dbo.GetNDSPercentForPartsGuid(@Parts_Guid);
    IF ( @NDSPercent IS NULL )
      BEGIN
        SET @ERROR_NUM = 2;
        SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) +  ' [usp_GetPrice] Не удалось определить ставку НДС.';

	      RETURN @ERROR_NUM;
      END 

    -- Размер надбавки по товару, %
    SELECT @ChargesPercent = dbo.GetPropertieChargeForStockAndParts( @Stock_Guid, @Parts_Guid );
    IF ( @ChargesPercent IS NULL )
      BEGIN
        SET @ERROR_NUM = 3;
        SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) +  ' [usp_GetPrice] Не удалось определить размер надбавки.';

	      RETURN @ERROR_NUM;
      END 
      
    -- форма оплаты
    DECLARE @PaymentTypeForm1Guid D_GUID;
    SELECT @PaymentTypeForm1Guid = [dbo].[GetPaymentTypeForm1Guid]();
    
    DECLARE @PaymentTypeForm2Guid D_GUID;
    SELECT @PaymentTypeForm2Guid = [dbo].[GetPaymentTypeForm2Guid]();
     
    -- Не смотря на все препоны и проверки, дело дошло до прайс-листа
    -- Цены из прайс-листа
    DECLARE @Price0 decimal(18, 4);      -- Цена импортера, BYB (прайс-лист)
    DECLARE @Price0_2 decimal(18, 4);    -- отпускная цена в EUR для заказов по форме оплаты №2 (прайс-лист)
    DECLARE @Price2 decimal(18, 4);      -- отпускная цена в BYB для заказов по форме оплаты №2 (прайс-лист) 
    DECLARE @Price11 decimal(18, 4);     -- отпускная цена в BYB для заказов по форме оплаты №1 (прайс-лист)   
    DECLARE @Price0_11 decimal(18, 4);   -- отпускная цена в EUR для заказов по форме оплаты №1 (прайс-лист)
    DECLARE @CurrencyRatePricing float; 
    DECLARE @MarkUpPercent decimal(18, 4);       
    DECLARE @CalcMarkUpPercent decimal(18, 4); -- рассчитанная оптовая надбавка
    
		DECLARE @PartsubtypePriceTypePrice0_Guid	D_GUID;
		DECLARE @PartsubtypePriceTypePrice0_2_Guid	D_GUID;
		DECLARE @PartsubtypePriceTypePrice2_Guid	D_GUID;
		DECLARE @PartsubtypePriceTypePrice0_11_Guid	D_GUID;
		DECLARE @PartsubtypePriceTypePrice11_Guid	D_GUID;

		SELECT Top 1 @PartsubtypePriceTypePrice0_Guid = [PartsubtypePriceType_Guid] FROM [dbo].[T_PartsubtypePriceType] WHERE [PartsubtypePriceType_Abbr] = 'PRICE0';
		SELECT Top 1 @PartsubtypePriceTypePrice0_11_Guid = [PartsubtypePriceType_Guid] FROM [dbo].[T_PartsubtypePriceType] WHERE [PartsubtypePriceType_Abbr] = 'PRICE0_11';
		SELECT Top 1 @PartsubtypePriceTypePrice11_Guid = [PartsubtypePriceType_Guid] FROM [dbo].[T_PartsubtypePriceType] WHERE [PartsubtypePriceType_Abbr] = 'PRICE11';
		SELECT Top 1 @PartsubtypePriceTypePrice0_2_Guid = [PartsubtypePriceType_Guid] FROM [dbo].[T_PartsubtypePriceType] WHERE [PartsubtypePriceType_Abbr] = 'PRICE0_2';
		SELECT Top 1 @PartsubtypePriceTypePrice2_Guid = [PartsubtypePriceType_Guid] FROM [dbo].[T_PartsubtypePriceType] WHERE [PartsubtypePriceType_Abbr] = 'PRICE2';

		SELECT @Price0 = [Price_Value] FROM [dbo].[T_PartsPriceList] WHERE [Parts_Guid] = @Parts_Guid AND [PartsubtypePriceType_Guid] = @PartsubtypePriceTypePrice0_Guid;
		SELECT @Price0_11 = [Price_Value] FROM [dbo].[T_PartsPriceList] WHERE [Parts_Guid] = @Parts_Guid AND [PartsubtypePriceType_Guid] = @PartsubtypePriceTypePrice0_11_Guid;
		SELECT @Price11 = [Price_Value] FROM [dbo].[T_PartsPriceList] WHERE [Parts_Guid] = @Parts_Guid AND [PartsubtypePriceType_Guid] = @PartsubtypePriceTypePrice11_Guid;
		SELECT @Price0_2 = [Price_Value] FROM [dbo].[T_PartsPriceList] WHERE [Parts_Guid] = @Parts_Guid AND [PartsubtypePriceType_Guid] = @PartsubtypePriceTypePrice0_2_Guid;
		SELECT @Price2 = [Price_Value] FROM [dbo].[T_PartsPriceList] WHERE [Parts_Guid] = @Parts_Guid AND [PartsubtypePriceType_Guid] = @PartsubtypePriceTypePrice2_Guid;

    IF( @Price0 IS NULL ) SET @Price0 = 0;
    IF( @Price0_2 IS NULL ) SET @Price0_2 = 0;
    IF( @Price2 IS NULL ) SET @Price2 = 0;
    IF( @Price0_11 IS NULL ) SET @Price0_11 = 0;
    IF( @Price11 IS NULL ) SET @Price11 = 0;

    -- Для определения цены в валюте учета используем курс ценообразования
		DECLARE @CurrencyBYB_GUID	D_GUID;
		DECLARE @CurrencyEUR_GUID	D_GUID;

		SELECT @CurrencyBYB_GUID = [Currency_Guid] FROM [dbo].[T_Currency] WHERE [Currency_Abbr] = 'BYB';
		SELECT @CurrencyEUR_GUID = [Currency_Guid] FROM [dbo].[T_Currency] WHERE [Currency_Abbr] = 'EUR';

		SELECT @CurrencyRatePricing = [dbo].[GetCurrencyRatePricingInOut](@CurrencyEUR_GUID, @CurrencyBYB_GUID, Getdate());
    
    IF( ( @CurrencyRatePricing IS NULL ) OR ( @CurrencyRatePricing = 0 ) )
      BEGIN
        SET @ERROR_NUM = 4;
        SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) +  ' [usp_GetPrice] Не удалось определить курс ценообразования.';

	      RETURN @ERROR_NUM;
      END 
    
    IF( @Price0 IS NULL ) SET @Price0 = 0;
    IF( @Price0_2 IS NULL ) SET @Price0_2 = 0;
    IF( @Price2 IS NULL ) SET @Price2 = 0;
    IF( @Price11 IS NULL ) SET @Price11 = 0;
    IF( @Price0_11 IS NULL ) SET @Price0_11 = 0;
		SET @MarkUpPercent = 0;
		SET @CalcMarkUpPercent = 0;

		-- Расчёт отпускной цены
		IF( @PaymentType_Guid = @PaymentTypeForm1Guid )
			BEGIN
				-- форма оплаты №1

				IF( ( @Price0 = 0 ) OR ( @Price2 = 0 ) )
					BEGIN
				    
						SET @ERROR_NUM = 10;
						SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) + 
							'@Price0: ' + CONVERT( nvarchar(15), @Price0 ) + nChar(13) + nChar(10) + 
							'@Price0_2: ' + CONVERT( nvarchar(15), @Price0_2 )  + nChar(13) + nChar(10) + 
							'@Price2: ' + CONVERT( nvarchar(15), @Price2 );
						RETURN @ERROR_NUM;
					END 
				
				IF( @IsPartsImporter = 0 )
					BEGIN
						-- компания-продавец является оптовым звеном
				
						SET @Price = ( SELECT dbo.GetRoundMoney( @Price0 * ( 1 + ( @ChargesPercent/100 ) ) ) );
						SET @Price = @Price * ( 1 + ( @NDSPercent/100 ) );

						SET @PriceImporter = @Price0;
						SET @PriceWithDiscount = @PriceImporter * ( 1 + ( ( @ChargesPercent * ( 1 - ( @DiscountPercent/100 ) ) ) - @DiscountPercent )/100 );
						SET @PriceWithDiscount = @PriceWithDiscount * ( 1 + ( @NDSPercent/100 ) );
						
						-- корректировка оптовой надбавки и цены со скидкой
						SET @MarkUpPercent =  ( SELECT dbo.GetRoundMoney( (((@PriceWithDiscount/(1 + @NDSPercent/100) )/@PriceImporter) - 1)*100 ) );
						SET @CalcMarkUpPercent = @MarkUpPercent;
						SET @PriceWithDiscount = ( SELECT dbo.GetRoundMoney( @PriceImporter * ( 1 + ( @MarkUpPercent/100 ) ) ) );
						SET @PriceWithDiscount = @PriceWithDiscount * ( 1 + ( @NDSPercent/100 ) );

					END
				ELSE IF( @IsPartsImporter = 1 )	
					BEGIN
						-- товар импортирован непосредственно компанией-продавцом
		        SET @PriceImporter = ( SELECT dbo.GetRoundMoney( @Price0 * ( 1 - ( @DiscountPercent/100 ) ) ) );
				    SET @PriceWithDiscount = @PriceImporter * ( 1 + ( @NDSPercent/100 ) );
						SET @Price = @PriceWithDiscount;
					END

				-- валютный эквивалент отпускной цены
		    SET @PriceInAccountingCurrency = @Price/@CurrencyRatePricing;
		    SET @PriceWithDiscountInAccountingCurrency = @PriceWithDiscount/@CurrencyRatePricing;

			END
		ELSE IF( @PaymentType_Guid = @PaymentTypeForm2Guid )
			BEGIN
				-- форма оплаты №2
				IF( ( @Price0 = 0 ) OR ( @Price0_11 = 0 )  OR ( @Price11 = 0 ))
					BEGIN
						SET @ERROR_NUM = 10;
						SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) + 
							'@Price0: ' + CONVERT( nvarchar(15), @Price0 ) + nChar(13) + nChar(10) + 
							'@Price0_11: ' + CONVERT( nvarchar(15), @Price0_11 )  + nChar(13) + nChar(10) + 
							'@Price11: ' + CONVERT( nvarchar(15), @Price11 );
						RETURN @ERROR_NUM;
					END 
				
				SET @Price = @Price11;
				SET @PriceInAccountingCurrency = @Price0_11;
				SET @PriceImporter = @Price / ( 1 + @NDSPercent/100 );

				IF( @IsPartsImporter = 1 )
					SET @Price = @PriceImporter * ( 1 + ( @NDSPercent/100 ) );
				SET @PriceWithDiscount = @Price * ( 1 - ( @DiscountPercent/100 ) );				
				SET @PriceWithDiscountInAccountingCurrency = @PriceInAccountingCurrency * ( 1 - ( @DiscountPercent/100 ) );
			END


	END TRY
	BEGIN CATCH
		SET @ERROR_NUM = ERROR_NUMBER();
		SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) +  ' [usp_GetPrice] Текст ошибки: ' + ERROR_MESSAGE();

    RETURN @ERROR_NUM;
	END CATCH;

	IF( @ERROR_NUM = 0 )
		SET @ERROR_MES = @ERROR_MES + ' [usp_GetPrice] Успешное завершение операции.';
		
	RETURN @ERROR_NUM;
END

GO
GRANT EXECUTE ON [dbo].[usp_GetPrice] TO [public]
GO


DECLARE @doc xml;
SET @doc = '<ImportDataInOrderSettings xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <ColumnItem TOOLS_ID="10" TOOLS_NAME="STARTROW" TOOLS_USERNAME="Начальная строка" TOOLS_DESCRIPTION="№ строки, с которой начинается импорт данных" TOOLS_VALUE="2" TOOLSTYPE_ID="2" />
  <ColumnItem TOOLS_ID="11" TOOLS_NAME="ARTICLE" TOOLS_USERNAME="Артикул" TOOLS_DESCRIPTION="№ столбца с артикулом товара" TOOLS_VALUE="1" TOOLSTYPE_ID="2" />
  <ColumnItem TOOLS_ID="12" TOOLS_NAME="NAME2" TOOLS_USERNAME="Наименование" TOOLS_DESCRIPTION="№ столбца с наименованием товара" TOOLS_VALUE="2" TOOLSTYPE_ID="2" />
  <ColumnItem TOOLS_ID="13" TOOLS_NAME="QUANTITY" TOOLS_USERNAME="Количество" TOOLS_DESCRIPTION="№ столбца с количеством товара" TOOLS_VALUE="3" TOOLSTYPE_ID="2" />
  <ColumnItem TOOLS_ID="14" TOOLS_NAME="PRICE" TOOLS_USERNAME="Цена" TOOLS_DESCRIPTION="№ столбца с ценой товара" TOOLS_VALUE="4" TOOLSTYPE_ID="2" />
  <ColumnItem TOOLS_ID="15" TOOLS_NAME="MARKUP" TOOLS_USERNAME="Скидка, %" TOOLS_DESCRIPTION="№ столбца с размером скидки по позиции в заказе" TOOLS_VALUE="5" TOOLSTYPE_ID="2" />
  <ColumnItem TOOLS_ID="16" TOOLS_NAME="PARTS_ID" TOOLS_USERNAME="Код товара" TOOLS_DESCRIPTION="№ строки с кодом товара" TOOLS_VALUE="6" TOOLSTYPE_ID="2" />
  <ColumnItem TOOLS_ID="17" TOOLS_NAME="CUSTOMER_ID" TOOLS_USERNAME="Код клиента" TOOLS_DESCRIPTION="№ строки с кодом клиента" TOOLS_VALUE="  2" TOOLSTYPE_ID="2" />
  <ColumnItem TOOLS_ID="18" TOOLS_NAME="RTT_CODE" TOOLS_USERNAME="Код РТТ" TOOLS_DESCRIPTION="№ строки с кодом РТТ" TOOLS_VALUE="3" TOOLSTYPE_ID="3" />
  <ColumnItem TOOLS_ID="19" TOOLS_NAME="DEPART_CODE" TOOLS_USERNAME="Подразделение" TOOLS_DESCRIPTION="№ строки с кодом подразделения" TOOLS_VALUE="  1" TOOLSTYPE_ID="2" />
</ImportDataInOrderSettings>';

INSERT INTO dbo.T_Settings( Settings_Guid, Settings_Name, Settings_XML )
VALUES( newid(), 'ImportDataInOrderSettings', @doc );

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Возвращает список записей из ( dbo.T_Settings )
--
-- Входные параметры:
--
-- Выходные параметры:
--
-- Результат:
--    0 - успешное завершение
--    <>0 - ошибка запроса информации из базы данных

CREATE PROCEDURE [dbo].[usp_GetImportDataInOrderSettings] 
  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output
AS

BEGIN

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = '';

  BEGIN TRY

    SELECT Top 1 Settings_Guid, Settings_Name, Settings_XML
    FROM dbo.T_Settings
    WHERE Settings_Name = 'ImportDataInOrderSettings';

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
GRANT EXECUTE ON [dbo].[usp_GetImportDataInOrderSettings] TO [public]
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Возвращает информацию о товаре: номенклатура, остатки
--
-- Входные параметры:
--
--		@Parts_Name					- наименование товара
--		@Parts_Article			- артикул товара
--		@Stock_Guid					-	УИ склада
--		@PartsIn_Id					- УИ товара в InterBase
--		@IBLINKEDSERVERNAME	- LinkedServer к InterBase
--			
--
-- Выходные параметры:
--
--		@ERROR_NUM					- номер ошикби
--		@ERROR_MES					- текст ошибки
--
-- Результат:
--    0 - успешное завершение
--    <>0 - ошибка запроса информации из базы данных

CREATE PROCEDURE [dbo].[usp_GetPartInstockByPartName] 
	@Parts_Name					D_NAME,
	@Parts_Article			D_PARTSARTICLE,
	@Stock_Guid					D_GUID,
	@PartsIn_Id					D_ID = NULL,
  @IBLINKEDSERVERNAME	D_NAME = NULL,

  @ERROR_NUM					int output,
  @ERROR_MES					nvarchar(4000) output
AS

BEGIN

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = '';

  BEGIN TRY
  
	  IF( @IBLINKEDSERVERNAME IS NULL ) SELECT @IBLINKEDSERVERNAME = dbo.GetIBLinkedServerName();

		DECLARE @strSQLText nvarchar(2048);
    DECLARE @Stock_Id int;
      
		SELECT @Stock_Id = Stock_Id FROM dbo.T_Stock 
		WHERE Stock_Guid = @Stock_Guid;
		
		DECLARE @Parts_Guid D_GUID;
		DECLARE @Parts_Id D_ID;
		
		IF( @PartsIn_Id IS NULL )
			SELECT @Parts_Guid = Parts_Guid, @Parts_Id = Parts_Id
			FROM dbo.T_Parts
			WHERE Parts_Name = @Parts_Name
				AND Parts_Article = @Parts_Article;
		ELSE
			SELECT @Parts_Guid = Parts_Guid, @Parts_Id = Parts_Id
			FROM dbo.T_Parts
			WHERE Parts_Id = @PartsIn_Id;
			
		IF( ( @Parts_Guid IS NOT NULL ) AND ( @Parts_Id IS NOT NULL ) )	
			BEGIN

				DECLARE @PartsubtypePriceTypePrice0_Guid	D_GUID;
				SELECT @PartsubtypePriceTypePrice0_Guid = PartsubtypePriceType_Guid 
				FROM T_PartsubtypePriceType WHERE PartsubtypePriceType_Abbr = 'PRICE0';

				CREATE TABLE #INSTOCK( PARTS_ID int, STOCK_QTY float, STOCK_RESQTY float, PARTS_MINRETAILQTY float,
					PARTS_MINWHOLESALEQTY float, PARTS_PACKQTY float );

				SELECT @strSQLText = dbo.GetTextQueryForSelectFromInterbase( null, null, 
				'SELECT PARTS_ID, STOCK_QTY, STOCK_RESQTY, PARTS_MINRETAILQTY, PARTS_MINWHOLESALEQTY, PARTS_PACKQTY 
							FROM SP_GETINSTOCKPART( ' +  	CAST( @Stock_Id as varchar(20)) + ', ' +  CAST( @Parts_Id as varchar(20)) + ' ) ' );
				SET @strSQLText = 'INSERT INTO #INSTOCK( PARTS_ID, STOCK_QTY, STOCK_RESQTY, PARTS_MINRETAILQTY, PARTS_MINWHOLESALEQTY, PARTS_PACKQTY ) ' + @strSQLText;

				EXECUTE SP_EXECUTESQL @strSQLText;
				
				--SELECT * FROM #INSTOCK;
				
				SELECT Parts_Guid, dbo.PartsView.Parts_Id, Barcode, 
					Parts_OriginalName, Parts_Name, Parts_ShortName, Parts_Article, Parts_PackQuantity, Parts_PackQuantityForCalc, Parts_BoxQuantity, 
					Parts_Weight, Parts_PlasticContainerWeight, Parts_PaperContainerWeight, Parts_AlcoholicContentPercent, 
					Parts_VendorPrice, Parts_IsActive, Parts_ActualNotValid, Parts_NotValid, Parts_Certificate,
					Country_Guid, Country_Name, Country_Code,
					Currency_Guid, Currency_Abbr, Currency_Code, 
					Measure_Guid, Measure_Id, Measure_Name, Measure_ShortName, 
					Record_Updated, Record_UserUdpated, 
					Owner_Guid, Owner_Id, Owner_Name, Owner_ShortName, Owner_Description, Owner_IsActive, Owner_ProcessDaysCount, 
					Vtm_Guid, Vtm_Id, Vtm_Name, Vtm_ShortName, Vtm_Description, Vtm_IsActive, 
					Parttype_Guid, Parttype_Id, Parttype_Name, Parttype_NDSRate, Parttype_Description, Parttype_DemandsName, Parttype_IsActive,
					Partsubtype_Guid, Partsubtype_Id, Partsubtype_Name, Partsubtype_Description, Partsubtype_Image, Partsubtype_IsActive, 
					PartLine_Guid, Partline_Id, Partline_Name, Partline_Description, Partline_IsActive,
					Parts_CodeTNVD, Parts_Reference,
					PartsCategory_Guid, PartsCategory_Id, PartsCategory_Name, PartsCategory_Description, PartsCategory_IsActive, 
					dbo.GetPriceForParts( Parts_Guid, @PartsubtypePriceTypePrice0_Guid ) as Parts0,
					( Parts_Name + ' ' + Parts_Article ) AS PartsFullName, 0 as PartsIsCheck, 
					dbo.IsProductIncludeInStock(Parts_Guid) AS IsProductIncludeInStock, 
					#INSTOCK.STOCK_QTY, #INSTOCK.STOCK_RESQTY, #INSTOCK.PARTS_MINRETAILQTY, #INSTOCK.PARTS_MINWHOLESALEQTY, #INSTOCK.PARTS_PACKQTY
				FROM dbo.PartsView, #INSTOCK
				WHERE dbo.PartsView.Parts_Guid = @Parts_Guid
					AND dbo.PartsView.Parts_Id = #INSTOCK.PARTS_ID;

				DROP TABLE #INSTOCK;

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
GRANT EXECUTE ON [dbo].[usp_GetPartInstockByPartName] TO [public]
GO

/****** Object:  UserDefinedTableType [dbo].[udt_OrderItms]    Script Date: 01.01.2014 14:34:09 ******/
CREATE TYPE [dbo].[udt_OrderItms] AS TABLE(
	[OrderItms_Guid] [uniqueidentifier] NULL,
	[Parts_Guid] [uniqueidentifier] NULL,
	[Measure_Guid] [uniqueidentifier] NULL,
	[OrderItms_Quantity] [float] NULL,
	[OrderItms_QuantityOrdered] [float] NULL,
	[OrderItms_PriceImporter] [money] NULL,
	[OrderItms_Price] [money] NULL,
	[OrderItms_DiscountPercent] [money] NULL,
	[OrderItms_PriceWithDiscount] [money] NULL,
	[OrderItms_NDSPercent] [money] NULL,
	[OrderItms_PriceInAccountingCurrency] [money] NULL,
	[OrderItms_PriceWithDiscountInAccountingCurrency] [money] NULL
)
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Сохраняет заказ в InterBase
--
-- Входные параметры:
--
--		@Suppl_Guid					- УИ заказа
--		@SupplInfo					- структура xml с описанием заказа
--		@IBLINKEDSERVERNAME	- LinkedServer к InterBase
--			
--
-- Выходные параметры:
--
--		@Suppl_Id						- УИ заказа в InterBase (T_Suppl)
--		@Suppl_Num					- номер заказа в InterBase
--		@ERROR_NUM					- номер ошикби
--		@ERROR_MES					- текст ошибки
--
-- Результат:
--    0 - успешное завершение
--    <>0 - ошибка запроса информации из базы данных

CREATE PROCEDURE [dbo].[usp_AddSupplToIB]
	@Suppl_Guid uniqueidentifier,
  @IBLINKEDSERVERNAME dbo.D_NAME = NULL,
  @SupplInfo xml ( DOCUMENT InfoForCalcPriceSchema2 ) = NULL output,

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
		--DECLARE @STMNT_ID int; -- номер договора в IB
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
		DECLARE @CustomerID int;
		DECLARE @Stmnt_Guid D_GUID;
    
		-- Проверяем, есть ли заказ с указанным идентификатором 
    IF NOT EXISTS ( SELECT Suppl_Guid FROM dbo.T_Suppl WHERE Suppl_Guid = @Suppl_Guid )
      BEGIN
        SET @ERROR_NUM = 1;
        SET @ERROR_MES = '[usp_AddSupplToIB] Не найден заказ с указанным идентификатором.' + nChar(13) + nChar(10) + CONVERT( nvarchar(36), @Suppl_Guid );
        RETURN @ERROR_NUM;
      END
      
		-- Проверяем наличие ссылки на склад
		SELECT @Stock_Guid = T_Suppl.Stock_Guid, @CustomerID = T_Customer.Customer_Id 
    FROM T_Suppl, T_Customer  
    WHERE T_Suppl.Suppl_Guid = @Suppl_Guid
			AND T_Suppl.Customer_Guid = T_Customer.Customer_Guid; 
			
    IF( @Stock_Guid IS NULL  )
      BEGIN
        SET @ERROR_NUM = 2;
        SET @ERROR_MES = '[usp_AddSupplToIB] В заказе не указан склад.';
        RETURN @ERROR_NUM;
      END

    IF( @SupplInfo IS NULL )
			BEGIN
				SET @SupplInfo = N'<?xml version="1.0" encoding="UTF-16"?>
					<InfoForCalc xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
						<Suppl CustomerID="0" Opt="false" SupplID="00000000-0000-0000-0000-000000000000"/>
						<SupplItms PartsID="0" Quantity="0"/>
						<Price MarkupPercent="0" Price="0" Price0="0" PriceCurrency="0" DiscountPrice="0" DiscountPriceCurrency="0" PriceList_Price0="0" PriceList_Price="0" PriceList_PriceCurrency="0" DiscountPercent="0" NDSPercent="0" Importer="0" DiscountFullPercent="0" DiscountRetroPercent="0" DiscountFixPercent="0" DiscountTradeEqPercent="0" DiscountComActionPercent="0" CurrencyRate="0" BonusSum="0" BonusCurrencySum="0"/>
					</InfoForCalc>
					';
		      
				--SET @SupplInfo = N'<?xml version="1.0" encoding="UTF-16"?>
				--	<InfoForCalc xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
				--		<Suppl CustomerID="0" Opt="false" SupplID="00000000-0000-0000-0000-000000000000"/>
				--		<SupplItms PartsID="0" Quantity="0"/>
				--		<Price MarkupPercent="0" Price="0" Price0="0" PriceCurrency="0" DiscountPrice="0" DiscountPriceCurrency="0" PriceList_Price0="0" PriceList_Price="0" PriceList_PriceCurrency="0" DiscountPercent="0" NDSPercent="0" Importer="0" DiscountFullPercent="0" DiscountRetroPercent="0" DiscountFixPercent="0" DiscountTradeEqPercent="0" DiscountComActionPercent="0" CurrencyRate="0" BonusSum="0" BonusCurrencySum="0"/>
				--		<Customer CustomerGuid="00000000-0000-0000-0000-000000000000"/>
				--		<Company CompanyGuid="00000000-0000-0000-0000-000000000000"/>
				--		<CreditLimit Money="0" MoneyApproved="0" Days="0" DaysApproved="0" CurrencyGuid="00000000-0000-0000-0000-000000000000"/>
				--	</InfoForCalc>
				--	';

				-- пропишем значение кода клиента и признака "оптовый заказ"
				SELECT @SupplInfo = dbo.SetCustomerIDInXml( @SupplInfo, @CustomerID );
				SELECT @SupplInfo = dbo.SetOptInXml( @SupplInfo, 0 );
				SELECT @SupplInfo = dbo.SetSupplIDInXml( @SupplInfo, @Suppl_Guid );
			END

		-- запрашиваем данные, необходимые для создания заказа на стороне IB
		SELECT @Depart_Guid = dbo.T_Suppl.Depart_Guid,  @STOCK_ID = Stock.STOCK_ID, @CUSTOMER_ID = Customer.CUSTOMER_ID, 
			@depart_code = Depart.Depart_Code, @CUSTOMERCHILD_GUID_ID = dbo.T_Suppl.CustomerChild_Guid, 
			@SUPPL_NOTE = dbo.T_Suppl.Suppl_Note,
			@SUPPL_ALLPRICE = dbo.T_Suppl.Suppl_AllPrice, 
			@SUPPL_ALLDISCOUNT = ( dbo.T_Suppl.Suppl_AllPrice - dbo.T_Suppl.Suppl_TotalPrice ), 
			@SUPPL_CURRENCYALLPRICE = dbo.T_Suppl.Suppl_CurrencyAllPrice, 
			@SUPPL_CURRENCYALLDISCOUNT = ( dbo.T_Suppl.Suppl_CurrencyAllPrice - dbo.T_Suppl.Suppl_CurrencyTotalPrice ), 
			@SUPPL_MONEYBONUS2 = dbo.T_Suppl.Suppl_Bonus,  
			@SUPPL_DELIVERYDATE = dbo.T_Suppl.Suppl_DeliveryDate,
			@RTT_GUID_ID = dbo.T_Suppl.Rtt_Guid, 
			@ADDRESS_GUID_ID = dbo.T_Suppl.Address_Guid, 
			@PDASUPPL_PARTS_GUID_ID = dbo.T_Suppl.Parts_Guid, 
			@PaymentType_Guid = dbo.T_Suppl.PaymentType_Guid, @SUPLL_BEGINDATE = dbo.T_Suppl.Suppl_BeginDate, 
			@SUPPL_NOTE = dbo.T_Suppl.Suppl_Note, @Stmnt_Guid = dbo.T_Suppl.AgreementWithCustomer_Guid
		FROM dbo.T_Suppl, dbo.T_CUSTOMER as Customer, dbo.T_DEPART as Depart, dbo.T_STOCK as Stock 		 
		WHERE dbo.T_Suppl.Suppl_Guid = @Suppl_Guid
		  AND dbo.T_Suppl.Customer_Guid = Customer.Customer_Guid
			AND dbo.T_Suppl.Depart_Guid = Depart.Depart_Guid
			AND dbo.T_Suppl.Stock_Guid = Stock.Stock_Guid;
			
    IF( @SUPPL_ALLPRICE IS NULL ) SET @SUPPL_ALLPRICE = 0;
    IF( @SUPPL_ALLDISCOUNT IS NULL ) SET @SUPPL_ALLDISCOUNT = 0;
    IF( @SUPPL_CURRENCYALLPRICE IS NULL ) SET @SUPPL_CURRENCYALLPRICE = 0;
    IF( @SUPPL_CURRENCYALLDISCOUNT IS NULL ) SET @SUPPL_CURRENCYALLDISCOUNT = 0;

		IF( @SUPPL_NOTE IS NULL ) 
			SET @SUPPL_NOTE = 'NULL';
		ELSE 
			SET @SUPPL_NOTE = '''''' + @SUPPL_NOTE + '''''';
		
		-- договор
		DECLARE @STMNT_ID D_ID = 0;	
		IF( @Stmnt_Guid IS NOT NULL )
			SELECT @STMNT_ID = Stmnt_Id FROM [dbo].[T_AgreementWithCustomer] WHERE [AgreementWithCustomer_Guid] = @Stmnt_Guid

		-- торговый представитель
		DECLARE @Salesman_Guid D_GUID;
		SELECT Top 1 @Salesman_Guid	= Salesman_Guid FROM dbo.T_SalesmanDepart
		WHERE Depart_Guid = @Depart_Guid;
		IF( @Salesman_Guid IS NULL )
      BEGIN
        SET @ERROR_NUM = 3;
        SET @ERROR_MES = '[usp_AddSupplToIB] Для дочернего подразделения не найдет торговый представитель.';
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
		SELECT @RttAddres = dbo.GetAddressStringForDelivery( @ADDRESS_GUID_ID );
		
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
		WHERE CustomerChild_Guid = @CUSTOMERCHILD_GUID_ID

    DECLARE @SQLString nvarchar( 2048);
    DECLARE @ParmDefinition nvarchar(500);
    DECLARE @SQLStringForDiscount nvarchar( 2048);
    DECLARE @ParmDefinitionForDiscount nvarchar(500);
    DECLARE @RETURNVALUE int;
		DECLARE @suppl_updated bit;
		DECLARE @suppl_saved int;
		
		-- из ERP
    SET @ParmDefinition = N'@suppl_savedsql bit output, @suppl_numsql int output, @suppl_idsql int output'; 
    
    -- Добавляем запись в таблицу T_SUPPL в InterBase
    -- 20100714 SP_ADDPDASUPPL_2 поменяли на SP_ADDPDASUPPL
    SET @SQLString = 'select @suppl_savedsql = suppl_saved, @suppl_numsql = suppl_num, @suppl_idsql = suppl_id from openquery( ' + 
			@IBLINKEDSERVERNAME + ', ''select suppl_saved, suppl_num, suppl_id from SP_ADDPDASUPPL( ' + 
					cast( @CUSTOMER_ID as nvarchar( 20)) + ', ''''' + @DEPART_CODE + ''''', ' + cast( @STOCK_ID as nvarchar( 20)) + ', ' + 
					cast( @SUPPL_ALLPRICE as nvarchar( 56)) + ', ' + cast( @SUPPL_ALLDISCOUNT as nvarchar( 56)) + ', ' + 
					cast( @SUPPL_CURRENCYALLDISCOUNT as nvarchar( 56)) + ', ' + cast( @SUPPL_CURRENCYALLPRICE as nvarchar( 56)) + ', ' + 
					cast( @CHILDCUST_ID as nvarchar( 20)) + ', ' + cast( @SUPPL_MONEYBONUS2 as nvarchar( 1)) + ', ' + 
					cast( @SUPPL_CURRENCY as nvarchar( 1)) + ', ''''' + @SUPPL_NOTE + ''''', ' + @strSupplDeliverAddress + ', ' + 
					@strSupplDeliverDate  + ', ' + @strRttGuid  + ', ' + @strAddressGuid  + ', ' + @strPartsId + ')'')'; 
		
		EXECUTE sp_executesql @SQLString, @ParmDefinition, @suppl_savedsql = @suppl_saved output, @suppl_numsql = @suppl_num output, @suppl_idsql = @suppl_id output;


   -- SET @ParmDefinition = N'@suppl_numsql int output, @suppl_idsql int output, @error_numbersql int output, @error_textsql nvarchar(480) output'; 

   -- -- Добавляем запись в таблицу T_SUPPL в InterBase
   -- SET @SQLString = 'select @suppl_numsql = suppl_num, @suppl_idsql = suppl_id, @error_numbersql = ERROR_NUMBER, @error_textsql = ERROR_TEXT from openquery( ' + 
			--@IBLINKEDSERVERNAME + ', ''select suppl_num, suppl_id, ERROR_NUMBER, ERROR_TEXT from SP_ADDPDASUPPL( ' + 
			--		cast( @CUSTOMER_ID as nvarchar( 20)) + ', ''''' + @DEPART_CODE + ''''', ' + cast( @STOCK_ID as nvarchar( 20)) + ', ' + 
			--		cast( @SUPPL_ALLPRICE as nvarchar( 56)) + ', ' + cast( @SUPPL_ALLDISCOUNT as nvarchar( 56)) + ', ' + 
			--		cast( @SUPPL_CURRENCYALLDISCOUNT as nvarchar( 56)) + ', ' + cast( @SUPPL_CURRENCYALLPRICE as nvarchar( 56)) + ', ' + 
			--		cast( @CHILDCUST_ID as nvarchar( 20)) + ', ' + cast( @SUPPL_MONEYBONUS2 as nvarchar( 1)) + ', ' + 
			--		@SUPPL_NOTE + ', ' + @strSupplDeliverAddress + ', ' + 
			--		@strSupplDeliverDate  + ', ' + @strRttGuid  + ', ' + @strAddressGuid  + ', ' + @strPartsId + ', ' + 
			--		@strSupplBeginDate + ', ' + CAST( @PAYMENTFORM_ID as varchar(2) ) + ', ' + CAST( @SALE_ID as varchar(8) ) + ', ' + CAST( @STMNT_ID as varchar(8) ) + ')'')'; 

		--PRINT @SQLString;
  --  EXECUTE sp_executesql @SQLString, @ParmDefinition, @suppl_numsql = @suppl_num output, 
		--	@suppl_idsql = @suppl_id output, @error_numbersql = @ERROR_NUM output, @error_textsql = @ERROR_MES output;

--    IF( ( @ERROR_NUM <> 0 ) OR ( @suppl_id IS NULL ) OR ( @suppl_id = 0 ) ) -- 1 заголовок протокола успешно сохранен
    IF( ( @suppl_saved <> 1 ) OR ( @suppl_id IS NULL ) OR ( @suppl_id = 0 ) ) -- 1 заголовок протокола успешно сохранен
      BEGIN
				SET @ERROR_NUM = 3;
        SET @ERROR_MES = 'Не удалось сохранить протокол в InterBase.';

        IF( @SupplInfo IS NOT NULL )  
					EXEC sp_AddEventLogForCalcPrice @SupplItmsInfo = @SupplInfo, @EventType_Id = 141, @EventDscrpn = @ERROR_MES;
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

        IF( @SupplInfo IS NOT NULL )  
					EXEC sp_AddEventLogForCalcPrice @SupplItmsInfo = @SupplInfo, @EventType_Id = 142, @EventDscrpn = @ERROR_MES;
			END  

		-- нам удалось сохранить шапку протокола, теперь займемся его содержимым
		DECLARE @SupplItms_Guid D_GUID;
		DECLARE @Parts_Guid D_GUID;
		DECLARE @PARTS_ID int; 
		DECLARE @MEASURE_ID int; 
		DECLARE @SPLITMS_QUANTITY int; 
		DECLARE @SPLITMS_PRICE money;
		DECLARE @SPLITMS_DISCOUNT decimal(10, 4); 
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
		
		CREATE TABLE #SplItms( SupplItms_Guid uniqueidentifier, SplItms_Quantity int, SplItms_OrderQuantity int, QuantityInCollection int, 
			SUPPL_ID int, PARTS_ID int, NewSplItms_Quantity int );
    SET @ParmDefinition = N'@reserved_quantitysql int output, @splitms_idsql int output'; 
    SET @ParmDefinitionForDiscount = N'@return_valuesql int output';

		-- Пробежим по списку позиций в заказе и попробуем записать в IB
    DECLARE crSupplItms CURSOR 
    FOR SELECT [SupplItem_Guid] FROM dbo.T_SupplItem WHERE [Suppl_Guid] = @Suppl_Guid;
    OPEN crSupplItms;
    FETCH NEXT FROM crSupplItms INTO @SupplItms_Guid; 
    WHILE @@FETCH_STATUS = 0
      BEGIN
				BEGIN TRY
					SET @OptSuppl = 0;
					SET @docDiscountListInfo = NULL;
					

					
					SELECT @Parts_Guid = SplItms.PARTS_GUID, @PARTS_ID = Product.PARTS_ID, @MEASURE_ID = Measure.MEASURE_ID, 
						@SPLITMS_ORDERQTY = SplItms.[SupplItem_OrderQuantity], @SPLITMS_QUANTITY = SplItms.[SupplItem_Quantity], 
						@SPLITMS_PRICE = SplItms.[SupplItem_Price], @SPLITMS_CURRENCYPRICE = SplItms.[SupplItem_CurrencyPrice],
						@SPLITMS_DISCOUNT = SplItms.[SupplItem_Discount], @PriceInfo = SplItms.[SupplItem_XMLPrice],
						@SPLITMS_DISCOUNTPRICE = SplItms.[SupplItem_DiscountPrice], @SPLITMS_CURRENCYDISCOUNTPRICE = SplItms.[SupplItem_CurrencyDiscountPrice], 
						@docDiscountListInfo =  SplItms.[SupplItem_XMLDiscount]
					FROM  [dbo].[T_SupplItem]as SplItms, dbo.T_MEASURE as Measure, [dbo].[T_Parts] as Product
					WHERE SplItms.SupplItem_Guid = @SupplItms_Guid
						AND SplItms.PARTS_GUID = Product.PARTS_GUID
						AND SplItms.MEASURE_GUID = Measure.MEASURE_GUID;
						
					SELECT @SPLITMS_NDS = dbo.GetNDSPercentFromXml( @PriceInfo );
					SELECT @SPLITMS_MARKUP = dbo.GetMarkupPercentFromXml( @PriceInfo );
					SELECT @SPLITMS_CALC_MARKUP = dbo.GetCalcMarkUpPercentFromXml( @PriceInfo );
					SELECT @SPLITMS_BASEPRICE = dbo.GetPrice0FromXml( @PriceInfo );
					
					-- 2009.05.07
					-- Для оптового заказа нужно передавать не разность максимально надбавки и расчитанной надбавки, а размер фактической скидки
					-- Признак "Оптовый заказ"
					SELECT @OptSuppl = dbo.GetOptFromXml( @PriceInfo );
					IF( @OptSuppl = 1 ) -- оптовый заказ
						BEGIN
							SELECT @Diff_MarkUps = dbo.GetDiscountPercentFromXml(@PriceInfo);
							
							IF( @SPLITMS_MARKUP IS NULL ) SET @SPLITMS_MARKUP = 0;
							IF( ( @Diff_MarkUps IS NULL ) OR ( @Diff_MarkUps < 0 ) ) SET @Diff_MarkUps = 0;
						END  
					ELSE
						BEGIN
							-- розничный заказ
							IF( @SPLITMS_CALC_MARKUP IS NULL ) SET @SPLITMS_CALC_MARKUP = 0;
							IF( @SPLITMS_MARKUP IS NULL ) SET @SPLITMS_MARKUP = 0;
							
							SET @Diff_MarkUps = @SPLITMS_MARKUP - @SPLITMS_CALC_MARKUP;
							IF( @Diff_MarkUps < 0 ) SET @Diff_MarkUps = 0;
						END	
					
					SET @RESERVED_QUANTITY = 0;
					SET @SPLITMS_ID = 0;
					SET @RETURN_VALUE_DISCOUNT = -1;
					
					-- 2009.05.04 вместо скидки передаем в splitms_discount разность между максимальной надбавкой и расчитанной надбавкой
					SET @SQLString = 'select @reserved_quantitysql = reserved_quantity, @splitms_idsql = splitms_id  from openquery( ' + @IBLINKEDSERVERNAME + ', ''select reserved_quantity, splitms_id from SP_ADDSPLITMSFROMSQL( ' + 
					cast( @suppl_id as nvarchar( 20)) + ', ' + cast( @PARTS_ID as nvarchar( 20)) + ', ' + cast( @MEASURE_ID as nvarchar( 20)) + ', ' + 
					cast( @SPLITMS_QUANTITY as nvarchar( 56)) + ', ' + cast( @SPLITMS_PRICE as nvarchar( 56)) + ', ' + 
					cast( /*@SPLITMS_DISCOUNT*/ @Diff_MarkUps as nvarchar( 56)) + ', ' + cast( @SPLITMS_DISCOUNTPRICE as nvarchar( 56)) + ', ' + cast( @SPLITMS_BASEPRICE as nvarchar( 56)) + ', ' + 
					cast( @SPLITMS_MARKUP as nvarchar( 56)) + ', ' + 
					cast( @STOCK_ID as nvarchar( 20)) + ', ''''' + @SUPPL_NUMstr + ''''', ' + 
					cast( @SPLITMS_CURRENCYPRICE as nvarchar( 56)) + ', ' + cast( @SPLITMS_CURRENCYDISCOUNTPRICE as nvarchar( 56)) + ', ' + cast( @SPLITMS_ORDERQTY as nvarchar( 56)) + ', ''''' + 
					cast( @cards_shipdate as nvarchar( 10)) + ''''', '  + cast( @SPLITMS_NDS as nvarchar( 56)) + ', ' + 
					cast( @SPLITMS_DISCOUNT as nvarchar( 56 )) + ')'')';

					execute sp_executesql @SQLString, @ParmDefinition, @reserved_quantitysql = @reserved_quantity output, @splitms_idsql = @splitms_id output;
					
					INSERT INTO #SplItms( SupplItms_Guid, SplItms_Quantity, SplItms_OrderQuantity, QuantityInCollection, SUPPL_ID, PARTS_ID, NewSplItms_Quantity ) 
					VALUES( @SupplItms_Guid, @reserved_quantity, @SPLITMS_ORDERQTY, dbo.GetPartsCountInCollection( @PDASUPPL_PARTS_GUID_ID, @Parts_Guid ), @suppl_id, @PARTS_ID, @reserved_quantity );
					

					-- 2010.12.22
					-- пытаемся прописать структуру скидки
					IF( @SPLITMS_DISCOUNT > 0 )
						BEGIN
							SET @DiscountTmp = @SPLITMS_DISCOUNT;
							SET @SpesialDiscount = 0;
							SET @DiscountTypeValue = 4; -- спеццена
							
							SELECT @SpesialDiscount = dbo.GetDiscountPercentByTypeFromXml( @docDiscountListInfo, @DiscountTypeValue ); -- проверим, нет ли в скидке "спеццены"
							IF( ( @SpesialDiscount > 0 ) AND ( @SpesialDiscount = @SPLITMS_DISCOUNT ) )
								BEGIN
									-- если в структуре скидки есть спеццена и она равна скидке, то поиск закончен
									SET @SQLStringForDiscount = 'select @return_valuesql = return_value  from openquery( ' + @IBLINKEDSERVERNAME + ', ''select return_value from SP_ADDSPLITMSDISCOUNTFROMSQL( ' + 
									cast( @splitms_id as nvarchar( 20)) + ', ' + cast( @DiscountTypeValue as nvarchar( 20)) + ', ' + cast( @SpesialDiscount as nvarchar( 20)) + ', ' + '0 ' + ')'')';
									
									execute sp_executesql @SQLStringForDiscount, @ParmDefinitionForDiscount, @return_valuesql = @return_value_discount output;
								END
							ELSE
								BEGIN
									DECLARE crDiscountItms CURSOR 
									FOR SELECT DISCOUNTTYPE_ID FROM [dbo].[T_DiscountTypeForCalcPrice] ORDER BY DISCOUNTTYPE_ID;
									OPEN crDiscountItms;
									FETCH NEXT FROM crDiscountItms INTO @DiscountTypeValue; 
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

													SET @SQLStringForDiscount = 'select @return_valuesql = return_value  from openquery( ' + @IBLINKEDSERVERNAME + ', ''select return_value from SP_ADDSPLITMSDISCOUNTFROMSQL( ' + 
													cast( @splitms_id as nvarchar( 20)) + ', ' + cast( @DiscountTypeValue as nvarchar( 20)) + ', ' + cast( @DiscountPercent as nvarchar( 20)) + ', ' + '0 ' + ')'')';
													
													--INSERT INTO dbo.T_ProcessPDASuppl2( SQLStringForDiscount ) VALUES( @SQLStringForDiscount );
													execute sp_executesql @SQLStringForDiscount, @ParmDefinitionForDiscount, @return_valuesql = @return_value_discount output;
												END
										
										 FETCH NEXT FROM crDiscountItms INTO @DiscountTypeValue;   
										END 
							    
									CLOSE crDiscountItms;
									DEALLOCATE crDiscountItms;
								END	
							

						END

				END TRY
				BEGIN CATCH
					SET @ERROR_NUM = ERROR_NUMBER();
					SET @ERROR_MES = '[usp_AddSupplToIB] Текст ошибки: ' + ERROR_MESSAGE();
					PRINT @ERROR_MES;

					IF( @SupplInfo IS NOT NULL )  
						EXEC sp_AddEventLogForCalcPrice @SupplItmsInfo = @SupplInfo, @EventType_Id = 141, @EventDscrpn = @ERROR_MES;

					CLOSE crSupplItms;
					DEALLOCATE crSupplItms;


					--BREAK;
				END CATCH;
       FETCH NEXT FROM crSupplItms INTO @SupplItms_Guid;   
      END 
    
    CLOSE crSupplItms;
    DEALLOCATE crSupplItms;

		SET @ERROR_MES = 'Создано приложение к протоколу.' + nChar(13) + nChar(10) + 
			'№ протокола: ' + CONVERT( nvarchar(8), @suppl_num );

    IF( @SupplInfo IS NOT NULL )  
			EXEC sp_AddEventLogForCalcPrice @SupplItmsInfo = @SupplInfo, @EventType_Id = 143, @EventDscrpn = @ERROR_MES;
			
		-- если заказ является виртуальным набором, то нужно пройтись по его содержимому и проверить, все ли позиции удалось зарезервировать
		-- в том случае, если что-то "урезано", нужно скорректировать все позиции в заказе, чтобы получилось целое число наборов
		IF( @PDASUPPL_PARTS_GUID_ID IS NOT NULL )
			BEGIN
				DECLARE @NeedDeleteSupplInInterBase bit;
				SET @NeedDeleteSupplInInterBase = 0;
				DECLARE @SumQuantity int;
				DECLARE @SumOrderQuantity int;
				SELECT @SumQuantity = SUM( SplItms_Quantity ), @SumOrderQuantity = SUM( SplItms_OrderQuantity ) FROM #SplItms;
				IF( @SumQuantity IS NULL ) SET @SumQuantity = 0;
				IF( @SumOrderQuantity IS NULL ) SET @SumOrderQuantity = 0;
				IF( @SumQuantity = 0 ) 
					SET @NeedDeleteSupplInInterBase = 1; -- ни одна позиция в заказе не сохранилась в InterBase 
				
				
				IF( ( @SumQuantity > 0 ) AND ( @SumOrderQuantity > 0 ) AND ( @SumOrderQuantity <> @SumQuantity ) )
					BEGIN
						-- заказанное @SumOrderQuantity и зарезервированное @SumQuantity количество отличаются - нужно скорректировать заказ
						DECLARE @MaxDiff int;
						DECLARE @MaxDiff_SupplItms_Guid D_GUID;
						SELECT @MaxDiff = MAX( @SumOrderQuantity - @SumQuantity ) FROM #SplItms;
						IF( ( @MaxDiff IS NOT NULL ) AND ( @MaxDiff > 0 ) )
							BEGIN
								SELECT Top 1 @MaxDiff_SupplItms_Guid = SupplItms_Guid FROM #SplItms WHERE ( @SumOrderQuantity - @SumQuantity ) = @MaxDiff;
								DECLARE @QtyInColl int;
								DECLARE @NewQtyCollections int; -- новое количество полных наборов
								SET @NewQtyCollections = 0;
								DECLARE @CurrentQty int;
								SELECT @QtyInColl = QuantityInCollection, @CurrentQty = SplItms_Quantity FROM #SplItms WHERE SupplItms_Guid = @MaxDiff_SupplItms_Guid;
								IF( ( @QtyInColl > 0 ) AND ( @CurrentQty > 0 ) ) SET @NewQtyCollections = @CurrentQty/@QtyInColl;
								IF( @NewQtyCollections = 0 ) SET @NeedDeleteSupplInInterBase = 1;
								
								-- проставим новое количество для позиций
								IF( @NewQtyCollections > 0 )
									UPDATE #SplItms SET NewSplItms_Quantity = QuantityInCollection * @NewQtyCollections;
									
								-- теперь в InterBase нужно скорректировать количество в заказе	
								DECLARE @OldQty int;
								DECLARE @NewQty int;
								DECLARE crSplItms CURSOR FOR SELECT SUPPL_ID, PARTS_ID, SplItms_Quantity, NewSplItms_Quantity FROM #SplItms;
								OPEN crSplItms;
								FETCH NEXT FROM crSplItms INTO @suppl_id, @PARTS_ID, @OldQty, @NewQty; 
								WHILE @@FETCH_STATUS = 0
									BEGIN
										BEGIN TRY
											
											SET @RESERVED_QUANTITY = 0;

											SET @ParmDefinition = N'@reserved_quantitysql int output'; 
											SET @SQLString = 'select @reserved_quantitysql = reserved_quantity from openquery( ' + @IBLINKEDSERVERNAME + ', ''select reserved_quantity from SP_EDITSPLITMSFROMSQL( ' + 
											cast( @suppl_id as nvarchar( 20)) + ', ' + cast( @PARTS_ID as nvarchar( 20)) + ', '  + 
											cast( @OldQty as nvarchar( 56)) + ', ' + cast( @NewQty as nvarchar( 56)) + ')'')';

											execute sp_executesql @SQLString, @ParmDefinition, @reserved_quantitysql = @reserved_quantity output;
											
											IF( @reserved_quantity <> @NewQty )
												SET @NeedDeleteSupplInInterBase = 1; -- на стороне InterBaseе НЕ удалось скорректировать количество, удалим заказ
											
					
 										END TRY
										BEGIN CATCH
											SET @ERROR_NUM = ERROR_NUMBER();
											SET @ERROR_MES = '[usp_AddSupplToIB] Текст ошибки: ' + ERROR_MESSAGE();
											PRINT @ERROR_MES;
											
											SET @NeedDeleteSupplInInterBase = 1; -- на стороне InterBaseе удалось скорректировать количество, удалим заказ

											CLOSE crSplItms;
											DEALLOCATE crSplItms;
											

										END CATCH;
									 FETCH NEXT FROM crSplItms INTO @suppl_id, @PARTS_ID, @OldQty, @NewQty;   
									END 
						    
								CLOSE crSplItms;
								DEALLOCATE crSplItms;
								
								-- если в InterBase все получилось ( @NeedDeleteSupplInInterBase = 0 ), то скорректируем SplItms_Quantity в #SplItms;
								IF( @NeedDeleteSupplInInterBase = 0 )
									UPDATE #SplItms SET SplItms_Quantity = NewSplItms_Quantity;
								ELSE IF( @NeedDeleteSupplInInterBase = 1 )	
									BEGIN
										-- попробуем удалить заказ в InterBase, а в SQL Server перевести заказ на ручную обработку
										EXEC SP_DeleteSupplFromIB @Suppl_Id = @Suppl_Id, @IBLINKEDSERVERNAME = @IBLINKEDSERVERNAME, 
											@SupplInfo = @SupplInfo,	@ERROR_NUM = @ERROR_NUM output, @ERROR_MES = @ERROR_MES output;
											
										IF( @ERROR_NUM = 0 )	
											BEGIN
												SELECT @OrderState_Guid = SupplState_Guid FROM dbo.T_SupplState 
												WHERE SupplState_Id = 0;

												UPDATE dbo.T_SUPPL SET SupplState_Guid = @OrderState_Guid, SUPPL_ALLPRICE = 0, SUPPL_ALLDISCOUNT = 0,
															SUPPL_CURRENCYALLPRICE = 0, SUPPL_CURRENCYALLDISCOUNT = 0 
												WHERE Suppl_Guid = @Suppl_Guid;
												
												UPDATE dbo.T_SupplItem SET [SupplItem_Quantity] = [SupplItem_OrderQuantity], 
													[SupplItem_Price] = 0, [SupplItem_Discount] = 0, [SupplItem_DiscountPrice] = 0, 
													[SupplItem_CurrencyPrice] = 0, [SupplItem_CurrencyDiscountPrice] = 0
												WHERE [Suppl_Guid] = @Suppl_Guid;
												
												SET @ERROR_NUM = 25;
												
												SET @ERROR_MES = '[usp_AddSupplToIB] Не удалось сохранить нужное количество в заказе, протокол был удален в InterBase.' + nChar(13) + nChar(10) + 
													'№ протокола: ' + CONVERT( nvarchar(8), @suppl_num ) + nChar(13) + nChar(10) + 
													'УИ в IB: ' + CONVERT( nvarchar(8), @suppl_id ) + nChar(13) + nChar(10) + 
													'УИ в SQL: ' + CONVERT( nvarchar(36), @Suppl_Guid );
													
												RETURN @ERROR_NUM;
											END
										ELSE
											BEGIN
												SET @ERROR_NUM = 26;
												
												SET @ERROR_MES = '[usp_AddSupplToIB] Не удалось сохранить нужное количество в заказе, не удалось удалить протокол в InterBase. Всё плохо!' + nChar(13) + nChar(10) + 
													'№ протокола: ' + CONVERT( nvarchar(8), @suppl_num ) + nChar(13) + nChar(10) + 
													'УИ в IB: ' + CONVERT( nvarchar(8), @suppl_id ) + nChar(13) + nChar(10) + 
													'УИ в SQL: ' + CONVERT( nvarchar(36), @Suppl_Guid );
													
												SELECT @OrderState_Guid = SupplState_Guid FROM dbo.T_SupplState 
												WHERE SupplState_Id = 0;

												UPDATE dbo.T_SUPPL SET SupplState_Guid = @OrderState_Guid, SUPPL_ALLPRICE = 0, SUPPL_ALLDISCOUNT = 0,
															SUPPL_CURRENCYALLPRICE = 0, SUPPL_CURRENCYALLDISCOUNT = 0 
												WHERE Suppl_Guid = @Suppl_Guid;
												
												UPDATE dbo.T_SupplItem SET [SupplItem_Quantity] = [SupplItem_OrderQuantity], 
													[SupplItem_Price] = 0, [SupplItem_Discount] = 0, [SupplItem_DiscountPrice] = 0, 
													[SupplItem_CurrencyPrice] = 0, [SupplItem_CurrencyDiscountPrice] = 0
												WHERE [Suppl_Guid] = @Suppl_Guid;
												
												IF NOT EXISTS ( SELECT Suppl_Guid FROM dbo.T_ProcessPDASupplDelete WHERE Suppl_Guid = @Suppl_Guid )
													INSERT INTO dbo.T_ProcessPDASupplDelete( Suppl_Guid, Suppl_Id, Suppl_IsDeleted, Operation_Date )
													VALUES( @Suppl_Guid, @suppl_id, 0, GetDate() );

												RETURN @ERROR_NUM;
											END	
										

									END
	
								-- 	
							END
					END					
			END			
		
		-- теперь нужно проставить получившееся количество в заказе
		DECLARE @RsrvQty int;
    DECLARE crSupplItmsTemp CURSOR 
    FOR SELECT SupplItms_Guid, SplItms_Quantity FROM #SplItms;
    OPEN crSupplItmsTemp;
    FETCH NEXT FROM crSupplItmsTemp INTO @SupplItms_Guid, @RsrvQty; 
    WHILE @@FETCH_STATUS = 0
      BEGIN
      
				UPDATE dbo.T_SupplItem SET SupplItem_QUANTITY = @RsrvQty WHERE SupplItem_Guid = @SupplItms_Guid;
				
				FETCH NEXT FROM crSupplItmsTemp INTO @SupplItms_Guid, @RsrvQty;   
      END 
    
    CLOSE crSupplItmsTemp;
    DEALLOCATE crSupplItmsTemp;
		 
    -- теперь нужно пересчитать сумму заказа в dbo.T_Suppl
    SELECT @SUPPL_ALLPRICE = SUM( SupplItem_QUANTITY * SupplItem_PRICE ), 
			@SUPPL_ALLDISCOUNT = SUM( ( SupplItem_QUANTITY * SupplItem_PRICE ) - ( SupplItem_QUANTITY * SupplItem_DISCOUNTPRICE ) ),
			@SUPPL_CURRENCYALLPRICE = SUM( SupplItem_QUANTITY * SupplItem_CURRENCYPRICE ),
			@SUPPL_CURRENCYALLDISCOUNT = SUM( ( SupplItem_QUANTITY * SupplItem_CURRENCYPRICE ) - ( SupplItem_QUANTITY * SupplItem_CURRENCYDISCOUNTPRICE ) ),
			@SUPPL_TOTALPRICE = SUM( SupplItem_QUANTITY * SupplItem_DISCOUNTPRICE ),
			@SUPPL_CURRENCYTOTALPRICE = SUM( SupplItem_QUANTITY * SupplItem_CURRENCYDISCOUNTPRICE )			
    FROM dbo.T_SupplItem WHERE SUPPL_GUID = @Suppl_Guid;
    
    IF( @SUPPL_ALLPRICE IS NULL ) SET @SUPPL_ALLPRICE = 0; 
    IF( @SUPPL_ALLDISCOUNT IS NULL ) SET @SUPPL_ALLDISCOUNT = 0;
		IF( @SUPPL_CURRENCYALLPRICE IS NULL ) SET @SUPPL_CURRENCYALLPRICE = 0; 
		IF( @SUPPL_CURRENCYALLDISCOUNT IS NULL ) SET @SUPPL_CURRENCYALLDISCOUNT = 0;
		IF( @SUPPL_TOTALPRICE IS NULL ) SET @SUPPL_TOTALPRICE = 0;
		IF( @SUPPL_CURRENCYTOTALPRICE IS NULL ) SET @SUPPL_CURRENCYTOTALPRICE = 0;
    
    UPDATE dbo.T_SUPPL SET SUPPL_ALLPRICE = @SUPPL_ALLPRICE, SUPPL_ALLDISCOUNT = @SUPPL_ALLDISCOUNT, 
			SUPPL_CURRENCYALLPRICE = @SUPPL_CURRENCYALLPRICE, SUPPL_CURRENCYALLDISCOUNT = @SUPPL_CURRENCYALLDISCOUNT, 
			SUPPL_TOTALPRICE = @SUPPL_TOTALPRICE, SUPPL_CURRENCYTOTALPRICE = @SUPPL_CURRENCYTOTALPRICE
    WHERE Suppl_Guid = @Suppl_Guid;
    
    -- про IB тоже не забываем
    DECLARE @return_value int;
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
				BEGIN
					SET @ERROR_MES = '[usp_AddSupplToIB] Заказ в InterBase удален, так как количество в заказе равно ноль.' + nChar(13) + nChar(10) + 'УИ протокола: ' + CONVERT( nvarchar(20), @suppl_id );
					IF( @SupplInfo IS NOT NULL )  
						EXEC sp_AddEventLogForCalcPrice @SupplItmsInfo = @SupplInfo, @EventType_Id = 145, @EventDscrpn = @ERROR_MES;

						-- пусть обрабатывают вручную
						-- 2010.11.25
						-- ставлю состояние заказа 80 - нет товара на складе
						SELECT @OrderState_Guid = SupplState_Guid FROM dbo.T_SupplState 
						WHERE SupplState_Id = 80;

						UPDATE dbo.T_SUPPL SET SupplState_Guid = @OrderState_Guid, SUPPL_ALLPRICE = 0, SUPPL_ALLDISCOUNT = 0,
									SUPPL_CURRENCYALLPRICE = 0, SUPPL_CURRENCYALLDISCOUNT = 0 
						WHERE Suppl_Guid = @Suppl_Guid;
						
						UPDATE dbo.T_SupplItem SET SupplItem_QUANTITY = SupplItem_OrderQuantity, SupplItem_PRICE = 0, 
							SupplItem_DISCOUNT = 0, SupplItem_DISCOUNTPRICE = 0, 
							SupplItem_CURRENCYPRICE = 0, SupplItem_CURRENCYDISCOUNTPRICE = 0
						WHERE Suppl_Guid = @Suppl_Guid;
						
						SET @ERROR_NUM = 44;
						
				END
			ELSE
				BEGIN
					SET @ERROR_MES = '[usp_AddSupplToIB] Не удалось пересчитать шапку протокола в InterBase.' + nChar(13) + nChar(10) + 'УИ протокола: ' + CONVERT( nvarchar(20), @suppl_id );
					IF( @SupplInfo IS NOT NULL )  
						EXEC sp_AddEventLogForCalcPrice @SupplItmsInfo = @SupplInfo, @EventType_Id = 146, @EventDscrpn = @ERROR_MES;
				END	
				
			RETURN @ERROR_NUM;
		END
	ELSE
		BEGIN
			SET @ERROR_MES = 'Пересчитана шапка к протоколу.' + nChar(13) + nChar(10) + 
				'№ протокола: ' + CONVERT( nvarchar(8), @suppl_num );
			IF( @SupplInfo IS NOT NULL )  
				EXEC sp_AddEventLogForCalcPrice @SupplItmsInfo = @SupplInfo, @EventType_Id = 144, @EventDscrpn = @ERROR_MES;

			-- прописываем в InterBase состояние 0 у заказа, чтобы его можно было видеть
			EXEC	[dbo].[SP_IBUPDATESUPPLSTATE]	@suppl_id = @SupplIdForBlock,	@suppl_state = 0,
				@linked_server = @IBLINKEDSERVERNAME,	@suppl_updated = @suppl_updated OUTPUT
		END	

	-- вроде как дошли без потерь
	SELECT @OrderState_Guid = SupplState_Guid FROM dbo.T_SupplState 
	WHERE SupplState_Id = 70;

	UPDATE dbo.T_SUPPL SET SupplState_Guid = @OrderState_Guid, SUPPL_ID = @suppl_id, [Suppl_Num] = @suppl_num, SUPPL_WEIGHT = dbo.GetSupplWeight( @Suppl_Guid ) 
	WHERE Suppl_Guid = @Suppl_Guid; -- заявка обсчитана и переведена в протокол

	SET @ERROR_NUM = 0;

 	END TRY
	BEGIN CATCH
    SET @ERROR_NUM = ERROR_NUMBER();
    SET @ERROR_MES = '[usp_AddSupplToIB]: ' + ERROR_MESSAGE();
		IF( @SupplInfo IS NOT NULL )  
			EXEC sp_AddEventLogForCalcPrice @SupplItmsInfo = @SupplInfo, @EventType_Id = 141, @EventDscrpn = @ERROR_MES;

		RETURN @ERROR_NUM;
	END CATCH;

  IF( @ERROR_NUM = 0 )
		BEGIN
			DECLARE @SecondCount int;
			SELECT @SecondCount = DATEDIFF( second, @StartProcessSuppl, GetDate() );

			SET @ERROR_MES = '[usp_AddSupplToIB] Успешное завершение операции.' + nChar(13) + nChar(10) + 
			  'Завершено создание протокола.' + nChar(13) + nChar(10) + 
				'№ протокола: ' + CONVERT( nvarchar(8), @suppl_num ) + nChar(13) + nChar(10) + 
				'УИ в IB: ' + CONVERT( nvarchar(8), @suppl_id ) + nChar(13) + nChar(10) + 
				'УИ в SQL: ' + CONVERT( nvarchar(36), @Suppl_Guid )	+ nChar(13) + nChar(10) + 
				'Время создания протокола в IB: ' + CONVERT( nvarchar(8), @SecondCount ) + ' секунд.';				;
			IF( @SupplInfo IS NOT NULL )  
				EXEC sp_AddEventLogForCalcPrice @SupplItmsInfo = @SupplInfo, @EventType_Id = 140, @EventDscrpn = @ERROR_MES;
		END

	RETURN @ERROR_NUM;

END

GO
GRANT EXECUTE ON [dbo].[usp_AddSupplToIB] TO [public]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Creation date:	
-- Author:			

CREATE FUNCTION [dbo].[GetFirstSupplState] ()
RETURNS D_GUID
WITH EXECUTE AS caller
AS
BEGIN
  
	DECLARE @OrderState_Guid D_GUID;
	SET @OrderState_Guid = NULL;
	
	DECLARE @OrderState_Id D_ID;

	SELECT @OrderState_Id = MIN( SupplState_Id ) FROM dbo.T_SupplState;
	IF( @OrderState_Id IS NOT NULL )
		SELECT @OrderState_Guid = SupplState_Guid FROM dbo.T_SupplState WHERE SupplState_Id = @OrderState_Id;

	RETURN @OrderState_Guid;
end


GO
GRANT EXECUTE ON [dbo].[GetFirstSupplState] TO [public]
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Добавляет новую запись в таблицу dbo.T_Suppl
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
	--@Stmnt_Guid	- уи договора 
--
--
-- Выходные параметры:
--  @Suppl_Guid - уникальный идентификатор записи
--  @ERROR_NUM - номер ошибки
--  @ERROR_MES - текст ошибки
--
-- Результат:
--    0 - Успешное завершение
--    <>0 - ошибка

CREATE PROCEDURE [dbo].[usp_AddOrder] 
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
	@Stmnt_Guid [dbo].[D_GUID] = NULL,

  @Order_Guid D_GUID output,
  @Suppl_Id D_ID output,
  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output

AS

BEGIN

	BEGIN TRY

    SET @ERROR_NUM = 0;
    SET @ERROR_MES = '';
    SET @Order_Guid = NULL;
    SET @Suppl_Id = NULL;
    
    DECLARE @Order_Num D_ID = 0;
    DECLARE @Order_SubNum D_ID = 0;
    
    IF( @Order_BeginDate IS NULL ) SET @Order_BeginDate = dbo.TrimTime( GETDATE() );
    IF( @OrderState_Guid IS NULL ) SELECT @OrderState_Guid = dbo.GetFirstSupplState();
    
    -- Проверяем наличие клиента с указанным идентификатором
    IF NOT EXISTS ( SELECT Customer_Guid FROM dbo.T_Customer WHERE Customer_Guid = @Customer_Guid )
      BEGIN
        SET @ERROR_NUM = 1;
        SET @ERROR_MES = 'В базе данных не найден клиент с указанным идетнификатором.' + Char(13) + 
          'УИ: ' + Char(9) + CONVERT( nvarchar(36), @Customer_Guid  );
        RETURN @ERROR_NUM;
      END

    SELECT @Order_Num = MAX( [Suppl_Num] ) FROM dbo.T_Suppl WHERE Customer_Guid = @Customer_Guid;
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
			
    -- Проверяем наличие договора с указанным идентификатором
    IF( @Stmnt_Guid IS NOT NULL )
			BEGIN
				IF NOT EXISTS ( SELECT [AgreementWithCustomer_Guid] FROM [dbo].[T_AgreementWithCustomer] 
												WHERE [AgreementWithCustomer_Guid] = @Stmnt_Guid )
					BEGIN
						SET @ERROR_NUM = 11;
						SET @ERROR_MES = 'В базе данных не найден договор с указанным идетнификатором.' + Char(13) + 
							'УИ: ' + Char(9) + CONVERT( nvarchar(36), @Stmnt_Guid  );
						RETURN @ERROR_NUM;
					END
			END

			

    DECLARE @NewID D_GUID;
    SET @NewID = NEWID( );	
    
    BEGIN TRANSACTION UpdateData;
    
    INSERT INTO dbo.T_Suppl( Suppl_Guid, Suppl_Id, Suppl_Num, Suppl_Version, Suppl_BeginDate, SupplState_Guid, 
			Suppl_Bonus, Depart_Guid, Customer_Guid, CustomerChild_Guid, OrderType_Guid, 
			PaymentType_Guid, Suppl_Note, Suppl_DeliveryDate, Rtt_Guid, Address_Guid, Stock_Guid, 
			Parts_Guid, AgreementWithCustomer_Guid, SHOW_IN_DELIVERY, Suppl_ExcludeFromAdj )
    VALUES( @NewID, 0, @Order_Num, @Order_SubNum, @Order_BeginDate, @OrderState_Guid, 
			@Order_MoneyBonus, @Depart_Guid, @Customer_Guid, @CustomerChild_Guid, @OrderType_Guid, 
			@PaymentType_Guid, @Order_Description, @Order_DeliveryDate, @Rtt_Guid, @Address_Guid, 
			@Stock_Guid, @Parts_Guid, @Stmnt_Guid, 1, 0 );
    
    INSERT INTO [dbo].[T_SupplItem]( SupplItem_Guid, Splitms_Id, Suppl_Guid, Parts_Guid, Measure_Guid, SupplItem_Quantity, 
			SupplItem_OrderQuantity, SupplItem_PriceImporter,	SupplItem_Price, SupplItem_Discount,
			SupplItem_DiscountPrice, SupplItem_NDSPercent, SupplItem_CurrencyPrice,	SupplItem_CurrencyDiscountPrice )  
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
    
    SELECT @Order_Quantity = SUM( [SupplItem_Quantity] ), @Order_SumReserved = SUM( [SupplItem_AllPrice] ), 
			@Order_SumReservedWithDiscount = SUM( [SupplItem_TotalPrice] ), 
			@Order_SumReservedInAccountingCurrency = SUM( [SupplItem_CurrencyAllPrice] ),
			@Order_SumReservedWithDiscountInAccountingCurrency = SUM( [SupplItem_CurrencyTotalPrice] )
		FROM [dbo].[T_SupplItem]
		WHERE [Suppl_Guid] = @NewID;	
    
    UPDATE [dbo].[T_Suppl] SET [Suppl_Quantity]  = @Order_Quantity, [Suppl_AllPrice] = @Order_SumReserved, 
			[Suppl_TotalPrice] = @Order_SumReservedWithDiscount, 
      [Suppl_CurrencyAllPrice] = @Order_SumReservedInAccountingCurrency, 
      [Suppl_CurrencyTotalPrice] = @Order_SumReservedWithDiscountInAccountingCurrency,
			[Suppl_AllDiscount] = ( @Order_SumReserved - @Order_SumReservedWithDiscount ),
			[Suppl_CurrencyAllDiscount] = ( @Order_SumReservedInAccountingCurrency - @Order_SumReservedWithDiscountInAccountingCurrency )
    WHERE Suppl_Guid = @NewID;  
    
    SET @Order_Guid = @NewID;
		DECLARE @Suppl_Num int = NULL;
    
    IF( @bCalcPrices = 1 )
			-- шапка заказа и приложение к заказу сохранены в SQL Server,
			-- приступаем к расчёту цен
			EXEC dbo.sp_ProcessPricesInSuppl @Suppl_Guid = @Order_Guid, @ERROR_NUM = @ERROR_NUM out, @ERROR_MES = @ERROR_MES out;
		
		IF( @ERROR_NUM = 0 )
			BEGIN
				-- теперь заказ необходимо "разместить" в InterBase
				EXEC dbo.usp_AddSupplToIB @Suppl_Guid = @Order_Guid, @Suppl_Id = @Suppl_Id out, @Suppl_Num = @Suppl_Num out, 
					@ERROR_NUM = @ERROR_NUM out, @ERROR_MES = @ERROR_MES out;
			END
		
		IF( @ERROR_NUM = 0 )	
			BEGIN
				UPDATE dbo.T_Suppl SET Suppl_Id = @Suppl_Id, Suppl_Num = @Suppl_Num 
				WHERE Suppl_Guid = @Order_Guid;
				
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

GO
GRANT EXECUTE ON [dbo].[usp_AddOrder] TO [public]
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Переводит заказ в состояние "удален"
--
-- Входящие параметры:
--		@Order_Guid - идентификатор заказа
--
-- Выходные параметры:
--  @ERROR_NUM - код ошибки
--  @ERROR_MES - текст ошибки
--
-- Результат:
--    0 - успешное завершение
--    <>0 - ошибка

CREATE PROCEDURE [dbo].[usp_MakeOrderDeleted] 
  @Order_Guid D_GUID,

  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output

AS

BEGIN

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = '';
  
	BEGIN TRY

		DECLARE @OrderState_Id D_ID;
		DECLARE @OrderState_Name D_NAME;
		DECLARE @Suppl_Id D_ID;
		DECLARE @IBLINKEDSERVERNAME dbo.D_NAME;
		IF( @IBLINKEDSERVERNAME IS NULL ) SELECT @IBLINKEDSERVERNAME = dbo.GetIBLinkedServerName();
		
		SELECT @OrderState_Id = dbo.T_SupplState.[SupplState_Id], 
			@OrderState_Name = dbo.T_SupplState.[SupplState_Name],
			@Suppl_Id = dbo.T_Suppl.Suppl_Id
		FROM dbo.T_Suppl, dbo.T_SupplState
		WHERE dbo.T_Suppl.Suppl_Guid = @Order_Guid
			AND dbo.T_Suppl.SupplState_Guid = dbo.T_SupplState.SupplState_Guid;
		
		IF( @OrderState_Id IN ( 11, 12, 13, 14  ) ) -- печать, подтвержден, ТТН, отгружен
			BEGIN
				SET @ERROR_NUM = 1;
				SET @ERROR_MES = 'Заказ НЕЛЬЗЯ удалить, так как состояние заказа "' + @OrderState_Name + '"';
				RETURN @ERROR_NUM;
			END

    DECLARE @OrderState_Guid D_GUID;
    SELECT @OrderState_Guid = [SupplState_Guid] FROM [dbo].[T_SupplState]
    WHERE [SupplState_Id] = 1;
    
    IF( @OrderState_Guid IS NOT NULL )
			BEGIN
				BEGIN TRANSACTION UpdateData;
				BEGIN TRY
				
				IF( @Suppl_Id IS NOT NULL )
					BEGIN
						-- попробуем удалить заказ в InterBase
						EXEC sp_DeleteSupplFromIB @Suppl_Id = @Suppl_Id, @IBLINKEDSERVERNAME = @IBLINKEDSERVERNAME, 
							@SupplInfo = NULL,	@ERROR_NUM = @ERROR_NUM output, @ERROR_MES = @ERROR_MES output;
					END
					
				IF( @ERROR_NUM = 0 )	
					UPDATE dbo.T_Suppl SET SupplState_Guid = @OrderState_Guid WHERE Suppl_Guid = @Order_Guid;
					
				IF( @ERROR_NUM = 0 ) COMMIT TRANSACTION UpdateData
					ELSE ROLLBACK TRANSACTION UpdateData;

				END TRY
				BEGIN CATCH
					ROLLBACK TRANSACTION UpdateData;
					SET @ERROR_NUM = ERROR_NUMBER();
					SET @ERROR_MES = @ERROR_MES + nChar(13) + nChar(10) +  '[usp_MakeSupplDeleted] Текст ошибки: ' + ERROR_MESSAGE();
					RETURN @ERROR_NUM;
				END CATCH;
					
			END
		ELSE	
			BEGIN
				SET @ERROR_NUM = 3;
				SET @ERROR_MES = 'Не найден идентификатор для состояния "удалён".';
				RETURN @ERROR_NUM;
			END
    
	END TRY
	BEGIN CATCH
		SET @ERROR_NUM = ERROR_NUMBER();
		SET @ERROR_MES = @ERROR_MES + nChar(13) + nChar(10) +  '[usp_MakeSupplDeleted] Текст ошибки: ' + ERROR_MESSAGE();
		RETURN @ERROR_NUM;
	END CATCH;

	IF( @ERROR_NUM = 0 )
		SET @ERROR_MES = '[usp_MakeSupplDeleted] Заказ помечен как удаленный.';

	RETURN @ERROR_NUM;
END

GO
GRANT EXECUTE ON [dbo].[usp_MakeOrderDeleted] TO [public]
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Редактирование записи в таблице dbo.T_Suppl
--
-- Входные параметры:
--  @Suppl_Guid - уникальный идентификатор записи
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
--  @ERROR_NUM - номер ошибки
--  @ERROR_MES - текст ошибки
--
-- Результат:
--    0 - Успешное завершение
--    <>0 - ошибка

CREATE PROCEDURE [dbo].[usp_EditOrder] 
  @Order_Guid D_GUID,
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

  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output

AS

BEGIN

	BEGIN TRY

    SET @ERROR_NUM = 0;
    SET @ERROR_MES = NULL;
    
    IF( @Order_BeginDate IS NULL ) SET @Order_BeginDate = dbo.TrimTime( GETDATE() );
    IF( @OrderState_Guid IS NULL ) SELECT @OrderState_Guid = dbo.GetFirstSupplState();
    
    -- Проверяем наличие заказа с указанным идентификатором
    IF NOT EXISTS ( SELECT Suppl_Guid FROM dbo.T_Suppl WHERE Suppl_Guid = @Order_Guid )
      BEGIN
        SET @ERROR_NUM = 11;
        SET @ERROR_MES = 'В базе данных не найден заказ с указанным идетнификатором.' + Char(13) + 
          'УИ: ' + Char(9) + CONVERT( nvarchar(36), @Order_Guid  );
        RETURN @ERROR_NUM;
      END

    -- Проверяем наличие клиента с указанным идентификатором
    IF NOT EXISTS ( SELECT Customer_Guid FROM dbo.T_Customer WHERE Customer_Guid = @Customer_Guid )
      BEGIN
        SET @ERROR_NUM = 1;
        SET @ERROR_MES = 'В базе данных не найден клиент с указанным идетнификатором.' + Char(13) + 
          'УИ: ' + Char(9) + CONVERT( nvarchar(36), @Customer_Guid  );
        RETURN @ERROR_NUM;
      END

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
			IF NOT EXISTS ( SELECT CustomerChild_Guid FROM dbo.T_CustomerChild WHERE CustomerChild_Guid = @CustomerChild_Guid )
				BEGIN
					SET @ERROR_NUM = 4;
					SET @ERROR_MES = 'В базе данных не найден дочерний клиент с указанным идетнификатором.' + Char(13) + 
						'УИ: ' + Char(9) + CONVERT( nvarchar(36), @CustomerChild_Guid );
					RETURN @ERROR_NUM;
				END
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

    UPDATE dbo.T_Suppl SET Suppl_BeginDate = @Order_BeginDate, SupplState_Guid = @OrderState_Guid, 
			Suppl_Bonus = @Order_MoneyBonus, Depart_Guid = @Depart_Guid, 
			Customer_Guid = @Customer_Guid, CustomerChild_Guid = @CustomerChild_Guid, 
			OrderType_Guid = @OrderType_Guid,	PaymentType_Guid = @PaymentType_Guid, 
			Suppl_Note = @Order_Description, Suppl_DeliveryDate = @Order_DeliveryDate, 
			Rtt_Guid = @Rtt_Guid, Address_Guid = @Address_Guid, Stock_Guid = @Stock_Guid, Parts_Guid = @Parts_Guid
    WHERE Suppl_Guid = @Order_Guid;
    
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
GRANT EXECUTE ON [dbo].[usp_EditOrder] TO [public]
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Удаление элемента из таблицы dbo.T_Suppl
--
-- Входящие параметры:
--		@Order_Guid - уникальный идентификатор записи
--		@bOnlyDeclaration - признак "Удалить только приложение к заказу"
--
-- Результат:
--    0 - успешное завершение
--    <>0 - ошибка

CREATE PROCEDURE [dbo].[usp_DeleteOrder] 
	@Order_Guid D_GUID,
	@bOnlyDeclaration D_YESNO = 0,

  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output

AS

BEGIN

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = NULL;

	BEGIN TRY

		DELETE FROM [dbo].[T_SupplItem] WHERE [Suppl_Guid] = @Order_Guid;

		IF( @bOnlyDeclaration = 0 )
			DELETE FROM [dbo].[T_Suppl] WHERE Suppl_Guid = @Order_Guid
    
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
GRANT EXECUTE ON [dbo].[usp_DeleteOrder] TO [public]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Пересчет цен в заказе
--
-- Входящие параметры:
--  @Order_Guid - уникальный идентификатор записи с таблицами заказов
--
--
-- Выходные параметры:
--  @ERROR_NUM - номер ошибки
--  @ERROR_MES - текст ошибки
--
-- Результат:
--    0 - Успешное завершение
--    <>0 - ошибка

CREATE PROCEDURE [dbo].[usp_RecalcPricesInOrder] 
  @Order_Guid D_GUID,
  
  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output

AS

BEGIN

	BEGIN TRY

    SET @ERROR_NUM = 0;
    SET @ERROR_MES = '';
    
		EXEC [dbo].[sp_RecalcPricesInSuppl]  @Suppl_Guid = @Order_Guid, @ERROR_NUM = @ERROR_NUM out, @ERROR_MES = @ERROR_MES out;

	END TRY
	BEGIN CATCH
    SET @ERROR_NUM = ERROR_NUMBER();
    SET @ERROR_MES =  ERROR_MESSAGE();
	END CATCH;
		
	RETURN @ERROR_NUM;
END

GO
GRANT EXECUTE ON [dbo].[usp_RecalcPricesInOrder] TO [public]
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Creation date:	
-- Author:			

CREATE FUNCTION [dbo].[GetCurrentCurrencyNationalGuid] ()
RETURNS D_GUID
WITH EXECUTE AS caller
AS
BEGIN
  
	DECLARE @Currency_Guid D_GUID = NULL;
	DECLARE @Currency_Code D_CURRENCYCODE = 'BYB';
	

	SELECT Top 1 @Currency_Guid = Currency_Guid FROM dbo.T_Currency WHERE Currency_Abbr = @Currency_Code;

	RETURN @Currency_Guid;

end

GO
GRANT EXECUTE ON [dbo].[GetCurrentCurrencyNationalGuid] TO [public]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Добавляет новую запись в таблицу dbo.T_Suppl
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
	--@Stmnt_Guid	- уи договора 
--
--
-- Выходные параметры:
--  @Suppl_Guid - уникальный идентификатор записи
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
	@Stmnt_Guid [dbo].[D_GUID] = NULL,

  @Order_Guid D_GUID output,
  @Suppl_Id D_ID output,
  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output

AS

BEGIN

	BEGIN TRY

    SET @ERROR_NUM = 0;
    SET @ERROR_MES = '';
    SET @Order_Guid = NULL;
    SET @Suppl_Id = NULL;
    
    DECLARE @Order_Num					D_ID = 0;
    DECLARE @Order_SubNum				D_ID = 0;
		DECLARE @PaymentType_1_Guid	D_GUID = NULL;
		DECLARE @PaymentType_2_Guid	D_GUID = NULL;
		DECLARE @Currency_Guid			D_GUID = NULL;

		SET @PaymentType_1_Guid = ( SELECT dbo.GetPaymentType_1_Guid() );
		SET @PaymentType_2_Guid = ( SELECT dbo.GetPaymentType_2_Guid() );
    
    IF( @Order_BeginDate IS NULL ) SET @Order_BeginDate = dbo.TrimTime( GETDATE() );
    IF( @OrderState_Guid IS NULL ) SELECT @OrderState_Guid = dbo.GetFirstSupplState();
    
    -- Проверяем наличие клиента с указанным идентификатором
    IF NOT EXISTS ( SELECT Customer_Guid FROM dbo.T_Customer WHERE Customer_Guid = @Customer_Guid )
      BEGIN
        SET @ERROR_NUM = 1;
        SET @ERROR_MES = 'В базе данных не найден клиент с указанным идетнификатором.' + Char(13) + 
          'УИ: ' + Char(9) + CONVERT( nvarchar(36), @Customer_Guid  );
        RETURN @ERROR_NUM;
      END

    SELECT @Order_Num = MAX( [Suppl_Num] ) FROM dbo.T_Suppl WHERE Customer_Guid = @Customer_Guid;
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
				ELSE
					BEGIN
						IF(( @PaymentType_2_Guid IS NOT NULL ) AND ( @PaymentType_Guid = @PaymentType_2_Guid ) )
							SET @Currency_Guid = ( SELECT dbo.GetCurrentCurrencyMainGuid() );
						ELSE
							SET @Currency_Guid = ( SELECT dbo.GetCurrentCurrencyNationalGuid() );
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
			
    -- Проверяем наличие договора с указанным идентификатором
    IF( @Stmnt_Guid IS NOT NULL )
			BEGIN
				IF NOT EXISTS ( SELECT [AgreementWithCustomer_Guid] FROM [dbo].[T_AgreementWithCustomer] 
												WHERE [AgreementWithCustomer_Guid] = @Stmnt_Guid )
					BEGIN
						SET @ERROR_NUM = 11;
						SET @ERROR_MES = 'В базе данных не найден договор с указанным идетнификатором.' + Char(13) + 
							'УИ: ' + Char(9) + CONVERT( nvarchar(36), @Stmnt_Guid  );
						RETURN @ERROR_NUM;
					END
			END

			

    DECLARE @NewID D_GUID;
    SET @NewID = NEWID( );	
    
    BEGIN TRANSACTION UpdateData;
    
    INSERT INTO dbo.T_Suppl( Suppl_Guid, Suppl_Id, Suppl_Num, Suppl_Version, Suppl_BeginDate, SupplState_Guid, 
			Suppl_Bonus, Depart_Guid, Customer_Guid, CustomerChild_Guid, OrderType_Guid, 
			PaymentType_Guid, Suppl_Note, Suppl_DeliveryDate, Rtt_Guid, Address_Guid, Stock_Guid, 
			Parts_Guid, AgreementWithCustomer_Guid, SHOW_IN_DELIVERY, Suppl_ExcludeFromAdj, Currency_Guid )
    VALUES( @NewID, 0, @Order_Num, @Order_SubNum, @Order_BeginDate, @OrderState_Guid, 
			@Order_MoneyBonus, @Depart_Guid, @Customer_Guid, @CustomerChild_Guid, @OrderType_Guid, 
			@PaymentType_Guid, @Order_Description, @Order_DeliveryDate, @Rtt_Guid, @Address_Guid, 
			@Stock_Guid, @Parts_Guid, @Stmnt_Guid, 1, 0, @Currency_Guid );
    
    INSERT INTO [dbo].[T_SupplItem]( SupplItem_Guid, Splitms_Id, Suppl_Guid, Parts_Guid, Measure_Guid, SupplItem_Quantity, 
			SupplItem_OrderQuantity, SupplItem_PriceImporter,	SupplItem_Price, SupplItem_Discount,
			SupplItem_DiscountPrice, SupplItem_NDSPercent, SupplItem_CurrencyPrice,	SupplItem_CurrencyDiscountPrice )  
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
    
    SELECT @Order_Quantity = SUM( [SupplItem_Quantity] ), @Order_SumReserved = SUM( [SupplItem_AllPrice] ), 
			@Order_SumReservedWithDiscount = SUM( [SupplItem_TotalPrice] ), 
			@Order_SumReservedInAccountingCurrency = SUM( [SupplItem_CurrencyAllPrice] ),
			@Order_SumReservedWithDiscountInAccountingCurrency = SUM( [SupplItem_CurrencyTotalPrice] )
		FROM [dbo].[T_SupplItem]
		WHERE [Suppl_Guid] = @NewID;	
    
    UPDATE [dbo].[T_Suppl] SET [Suppl_Quantity]  = @Order_Quantity, [Suppl_AllPrice] = @Order_SumReserved, 
			[Suppl_TotalPrice] = @Order_SumReservedWithDiscount, 
      [Suppl_CurrencyAllPrice] = @Order_SumReservedInAccountingCurrency, 
      [Suppl_CurrencyTotalPrice] = @Order_SumReservedWithDiscountInAccountingCurrency,
			[Suppl_AllDiscount] = ( @Order_SumReserved - @Order_SumReservedWithDiscount ),
			[Suppl_CurrencyAllDiscount] = ( @Order_SumReservedInAccountingCurrency - @Order_SumReservedWithDiscountInAccountingCurrency )
    WHERE Suppl_Guid = @NewID;  
    
    SET @Order_Guid = @NewID;
		DECLARE @Suppl_Num int = NULL;
    
    IF( @bCalcPrices = 1 )
			-- шапка заказа и приложение к заказу сохранены в SQL Server,
			-- приступаем к расчёту цен
			EXEC dbo.sp_ProcessPricesInSuppl @Suppl_Guid = @Order_Guid, @ERROR_NUM = @ERROR_NUM out, @ERROR_MES = @ERROR_MES out;
		
		IF( @ERROR_NUM = 0 )
			BEGIN
				-- теперь заказ необходимо "разместить" в InterBase
				EXEC dbo.usp_AddSupplToIB @Suppl_Guid = @Order_Guid, @Suppl_Id = @Suppl_Id out, @Suppl_Num = @Suppl_Num out, 
					@ERROR_NUM = @ERROR_NUM out, @ERROR_MES = @ERROR_MES out;
			END
		
		IF( @ERROR_NUM = 0 )	
			BEGIN
				UPDATE dbo.T_Suppl SET Suppl_Id = @Suppl_Id, Suppl_Num = @Suppl_Num 
				WHERE Suppl_Guid = @Order_Guid;
				
				COMMIT TRANSACTION UpdateData;
			END 
		ELSE	
			ROLLBACK TRANSACTION UpdateData;
    
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION UpdateData;
    SET @ERROR_NUM = ERROR_NUMBER();
    SET @ERROR_MES =  ERROR_MESSAGE();
		
	END CATCH;

	IF( @ERROR_NUM = 0 )
		SET @ERROR_MES = 'Успешное завершение операции.';
		
	RETURN @ERROR_NUM;
END

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Сохраняет заказ в InterBase
--
-- Входные параметры:
--
--		@Suppl_Guid					- УИ заказа
--		@SupplInfo					- структура xml с описанием заказа
--		@IBLINKEDSERVERNAME	- LinkedServer к InterBase
--			
--
-- Выходные параметры:
--
--		@Suppl_Id						- УИ заказа в InterBase (T_Suppl)
--		@Suppl_Num					- номер заказа в InterBase
--		@ERROR_NUM					- номер ошикби
--		@ERROR_MES					- текст ошибки
--
-- Результат:
--    0 - успешное завершение
--    <>0 - ошибка запроса информации из базы данных

ALTER PROCEDURE [dbo].[usp_AddSupplToIB]
	@Suppl_Guid uniqueidentifier,
  @IBLINKEDSERVERNAME dbo.D_NAME = NULL,
  @SupplInfo xml ( DOCUMENT InfoForCalcPriceSchema2 ) = NULL output,

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
		--DECLARE @STMNT_ID int; -- номер договора в IB
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
		DECLARE @CustomerID int;
		DECLARE @Stmnt_Guid D_GUID;
    
		-- Проверяем, есть ли заказ с указанным идентификатором 
    IF NOT EXISTS ( SELECT Suppl_Guid FROM dbo.T_Suppl WHERE Suppl_Guid = @Suppl_Guid )
      BEGIN
        SET @ERROR_NUM = 1;
        SET @ERROR_MES = '[usp_AddSupplToIB] Не найден заказ с указанным идентификатором.' + nChar(13) + nChar(10) + CONVERT( nvarchar(36), @Suppl_Guid );
        RETURN @ERROR_NUM;
      END
      
		-- Проверяем наличие ссылки на склад
		SELECT @Stock_Guid = T_Suppl.Stock_Guid, @CustomerID = T_Customer.Customer_Id 
    FROM T_Suppl, T_Customer  
    WHERE T_Suppl.Suppl_Guid = @Suppl_Guid
			AND T_Suppl.Customer_Guid = T_Customer.Customer_Guid; 
			
    IF( @Stock_Guid IS NULL  )
      BEGIN
        SET @ERROR_NUM = 2;
        SET @ERROR_MES = '[usp_AddSupplToIB] В заказе не указан склад.';
        RETURN @ERROR_NUM;
      END

    IF( @SupplInfo IS NULL )
			BEGIN
				SET @SupplInfo = N'<?xml version="1.0" encoding="UTF-16"?>
					<InfoForCalc xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
						<Suppl CustomerID="0" Opt="false" SupplID="00000000-0000-0000-0000-000000000000"/>
						<SupplItms PartsID="0" Quantity="0"/>
						<Price MarkupPercent="0" Price="0" Price0="0" PriceCurrency="0" DiscountPrice="0" DiscountPriceCurrency="0" PriceList_Price0="0" PriceList_Price="0" PriceList_PriceCurrency="0" DiscountPercent="0" NDSPercent="0" Importer="0" DiscountFullPercent="0" DiscountRetroPercent="0" DiscountFixPercent="0" DiscountTradeEqPercent="0" DiscountComActionPercent="0" CurrencyRate="0" BonusSum="0" BonusCurrencySum="0"/>
					</InfoForCalc>
					';
		      
				--SET @SupplInfo = N'<?xml version="1.0" encoding="UTF-16"?>
				--	<InfoForCalc xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
				--		<Suppl CustomerID="0" Opt="false" SupplID="00000000-0000-0000-0000-000000000000"/>
				--		<SupplItms PartsID="0" Quantity="0"/>
				--		<Price MarkupPercent="0" Price="0" Price0="0" PriceCurrency="0" DiscountPrice="0" DiscountPriceCurrency="0" PriceList_Price0="0" PriceList_Price="0" PriceList_PriceCurrency="0" DiscountPercent="0" NDSPercent="0" Importer="0" DiscountFullPercent="0" DiscountRetroPercent="0" DiscountFixPercent="0" DiscountTradeEqPercent="0" DiscountComActionPercent="0" CurrencyRate="0" BonusSum="0" BonusCurrencySum="0"/>
				--		<Customer CustomerGuid="00000000-0000-0000-0000-000000000000"/>
				--		<Company CompanyGuid="00000000-0000-0000-0000-000000000000"/>
				--		<CreditLimit Money="0" MoneyApproved="0" Days="0" DaysApproved="0" CurrencyGuid="00000000-0000-0000-0000-000000000000"/>
				--	</InfoForCalc>
				--	';

				-- пропишем значение кода клиента и признака "оптовый заказ"
				SELECT @SupplInfo = dbo.SetCustomerIDInXml( @SupplInfo, @CustomerID );
				SELECT @SupplInfo = dbo.SetOptInXml( @SupplInfo, 0 );
				SELECT @SupplInfo = dbo.SetSupplIDInXml( @SupplInfo, @Suppl_Guid );
			END

		-- запрашиваем данные, необходимые для создания заказа на стороне IB
		SELECT @Depart_Guid = dbo.T_Suppl.Depart_Guid,  @STOCK_ID = Stock.STOCK_ID, @CUSTOMER_ID = Customer.CUSTOMER_ID, 
			@depart_code = Depart.Depart_Code, @CUSTOMERCHILD_GUID_ID = dbo.T_Suppl.CustomerChild_Guid, 
			@SUPPL_NOTE = dbo.T_Suppl.Suppl_Note,
			@SUPPL_ALLPRICE = dbo.T_Suppl.Suppl_AllPrice, 
			@SUPPL_ALLDISCOUNT = ( dbo.T_Suppl.Suppl_AllPrice - dbo.T_Suppl.Suppl_TotalPrice ), 
			@SUPPL_CURRENCYALLPRICE = dbo.T_Suppl.Suppl_CurrencyAllPrice, 
			@SUPPL_CURRENCYALLDISCOUNT = ( dbo.T_Suppl.Suppl_CurrencyAllPrice - dbo.T_Suppl.Suppl_CurrencyTotalPrice ), 
			@SUPPL_MONEYBONUS2 = dbo.T_Suppl.Suppl_Bonus,  
			@SUPPL_DELIVERYDATE = dbo.T_Suppl.Suppl_DeliveryDate,
			@RTT_GUID_ID = dbo.T_Suppl.Rtt_Guid, 
			@ADDRESS_GUID_ID = dbo.T_Suppl.Address_Guid, 
			@PDASUPPL_PARTS_GUID_ID = dbo.T_Suppl.Parts_Guid, 
			@PaymentType_Guid = dbo.T_Suppl.PaymentType_Guid, @SUPLL_BEGINDATE = dbo.T_Suppl.Suppl_BeginDate, 
			@SUPPL_NOTE = dbo.T_Suppl.Suppl_Note, @Stmnt_Guid = dbo.T_Suppl.AgreementWithCustomer_Guid
		FROM dbo.T_Suppl, dbo.T_CUSTOMER as Customer, dbo.T_DEPART as Depart, dbo.T_STOCK as Stock 		 
		WHERE dbo.T_Suppl.Suppl_Guid = @Suppl_Guid
		  AND dbo.T_Suppl.Customer_Guid = Customer.Customer_Guid
			AND dbo.T_Suppl.Depart_Guid = Depart.Depart_Guid
			AND dbo.T_Suppl.Stock_Guid = Stock.Stock_Guid;
			
    IF( @SUPPL_ALLPRICE IS NULL ) SET @SUPPL_ALLPRICE = 0;
    IF( @SUPPL_ALLDISCOUNT IS NULL ) SET @SUPPL_ALLDISCOUNT = 0;
    IF( @SUPPL_CURRENCYALLPRICE IS NULL ) SET @SUPPL_CURRENCYALLPRICE = 0;
    IF( @SUPPL_CURRENCYALLDISCOUNT IS NULL ) SET @SUPPL_CURRENCYALLDISCOUNT = 0;

		IF( @SUPPL_NOTE IS NULL ) 
			SET @SUPPL_NOTE = 'NULL';
		ELSE 
			SET @SUPPL_NOTE = '''''' + @SUPPL_NOTE + '''''';
		
		-- договор
		DECLARE @STMNT_ID D_ID = 0;	
		IF( @Stmnt_Guid IS NOT NULL )
			SELECT @STMNT_ID = Stmnt_Id FROM [dbo].[T_AgreementWithCustomer] WHERE [AgreementWithCustomer_Guid] = @Stmnt_Guid

		-- торговый представитель
		DECLARE @Salesman_Guid D_GUID;
		SELECT Top 1 @Salesman_Guid	= Salesman_Guid FROM dbo.T_SalesmanDepart
		WHERE Depart_Guid = @Depart_Guid;
		IF( @Salesman_Guid IS NULL )
      BEGIN
        SET @ERROR_NUM = 3;
        SET @ERROR_MES = '[usp_AddSupplToIB] Для дочернего подразделения не найдет торговый представитель.';
        RETURN @ERROR_NUM;
      END
    ELSE 
			SELECT @SALE_ID = Salesman_Id  FROM  dbo.T_Salesman WHERE Salesman_Guid = @Salesman_Guid;
		
		-- форма оплаты
		SELECT @PaymentTypeForm1Guid = dbo.GetPaymentTypeForm1Guid();
		SELECT @PaymentTypeForm2Guid = dbo.GetPaymentTypeForm2Guid();
		IF( @PaymentType_Guid = @PaymentTypeForm1Guid )
			BEGIN
				SET @PAYMENTFORM_ID = 1;
				SET @SUPPL_CURRENCY = 0;
			END
		ELSE IF( @PaymentType_Guid = @PaymentTypeForm2Guid )
			BEGIN
				SET @PAYMENTFORM_ID = 2;
				SET @SUPPL_CURRENCY = 1;
			END

		-- адрес доставки
		DECLARE @RttAddres D_DESCRIPTION;
		SELECT @RttAddres = dbo.GetAddressStringForDelivery( @ADDRESS_GUID_ID );
		
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
		WHERE CustomerChild_Guid = @CUSTOMERCHILD_GUID_ID

    DECLARE @SQLString nvarchar( 2048);
    DECLARE @ParmDefinition nvarchar(500);
    DECLARE @SQLStringForDiscount nvarchar( 2048);
    DECLARE @ParmDefinitionForDiscount nvarchar(500);
    DECLARE @RETURNVALUE int;
		DECLARE @suppl_updated bit;
		DECLARE @suppl_saved int;
		
		-- из ERP
    SET @ParmDefinition = N'@suppl_savedsql bit output, @suppl_numsql int output, @suppl_idsql int output'; 
    
    -- Добавляем запись в таблицу T_SUPPL в InterBase
    -- 20100714 SP_ADDPDASUPPL_2 поменяли на SP_ADDPDASUPPL
    SET @SQLString = 'select @suppl_savedsql = suppl_saved, @suppl_numsql = suppl_num, @suppl_idsql = suppl_id from openquery( ' + 
			@IBLINKEDSERVERNAME + ', ''select suppl_saved, suppl_num, suppl_id from SP_ADDPDASUPPL( ' + 
					cast( @CUSTOMER_ID as nvarchar( 20)) + ', ''''' + @DEPART_CODE + ''''', ' + cast( @STOCK_ID as nvarchar( 20)) + ', ' + 
					cast( @SUPPL_ALLPRICE as nvarchar( 56)) + ', ' + cast( @SUPPL_ALLDISCOUNT as nvarchar( 56)) + ', ' + 
					cast( @SUPPL_CURRENCYALLDISCOUNT as nvarchar( 56)) + ', ' + cast( @SUPPL_CURRENCYALLPRICE as nvarchar( 56)) + ', ' + 
					cast( @CHILDCUST_ID as nvarchar( 20)) + ', ' + cast( @SUPPL_MONEYBONUS2 as nvarchar( 1)) + ', ' + 
					cast( @SUPPL_CURRENCY as nvarchar( 1)) + ', ''''' + @SUPPL_NOTE + ''''', ' + @strSupplDeliverAddress + ', ' + 
					@strSupplDeliverDate  + ', ' + @strRttGuid  + ', ' + @strAddressGuid  + ', ' + @strPartsId + ')'')'; 

		PRINT @SQLString;
		EXECUTE sp_executesql @SQLString, @ParmDefinition, @suppl_savedsql = @suppl_saved output, @suppl_numsql = @suppl_num output, @suppl_idsql = @suppl_id output;

   -- SET @ParmDefinition = N'@suppl_numsql int output, @suppl_idsql int output, @error_numbersql int output, @error_textsql nvarchar(480) output'; 

   -- -- Добавляем запись в таблицу T_SUPPL в InterBase
   -- SET @SQLString = 'select @suppl_numsql = suppl_num, @suppl_idsql = suppl_id, @error_numbersql = ERROR_NUMBER, @error_textsql = ERROR_TEXT from openquery( ' + 
			--@IBLINKEDSERVERNAME + ', ''select suppl_num, suppl_id, ERROR_NUMBER, ERROR_TEXT from SP_ADDPDASUPPL( ' + 
			--		cast( @CUSTOMER_ID as nvarchar( 20)) + ', ''''' + @DEPART_CODE + ''''', ' + cast( @STOCK_ID as nvarchar( 20)) + ', ' + 
			--		cast( @SUPPL_ALLPRICE as nvarchar( 56)) + ', ' + cast( @SUPPL_ALLDISCOUNT as nvarchar( 56)) + ', ' + 
			--		cast( @SUPPL_CURRENCYALLDISCOUNT as nvarchar( 56)) + ', ' + cast( @SUPPL_CURRENCYALLPRICE as nvarchar( 56)) + ', ' + 
			--		cast( @CHILDCUST_ID as nvarchar( 20)) + ', ' + cast( @SUPPL_MONEYBONUS2 as nvarchar( 1)) + ', ' + 
			--		@SUPPL_NOTE + ', ' + @strSupplDeliverAddress + ', ' + 
			--		@strSupplDeliverDate  + ', ' + @strRttGuid  + ', ' + @strAddressGuid  + ', ' + @strPartsId + ', ' + 
			--		@strSupplBeginDate + ', ' + CAST( @PAYMENTFORM_ID as varchar(2) ) + ', ' + CAST( @SALE_ID as varchar(8) ) + ', ' + CAST( @STMNT_ID as varchar(8) ) + ')'')'; 

		--PRINT @SQLString;
  --  EXECUTE sp_executesql @SQLString, @ParmDefinition, @suppl_numsql = @suppl_num output, 
		--	@suppl_idsql = @suppl_id output, @error_numbersql = @ERROR_NUM output, @error_textsql = @ERROR_MES output;

--    IF( ( @ERROR_NUM <> 0 ) OR ( @suppl_id IS NULL ) OR ( @suppl_id = 0 ) ) -- 1 заголовок протокола успешно сохранен
    IF( ( @suppl_saved <> 1 ) OR ( @suppl_id IS NULL ) OR ( @suppl_id = 0 ) ) -- 1 заголовок протокола успешно сохранен
      BEGIN
				SET @ERROR_NUM = 3;
        SET @ERROR_MES = 'Не удалось сохранить протокол в InterBase.';

        IF( @SupplInfo IS NOT NULL )  
					EXEC sp_AddEventLogForCalcPrice @SupplItmsInfo = @SupplInfo, @EventType_Id = 141, @EventDscrpn = @ERROR_MES;
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

        IF( @SupplInfo IS NOT NULL )  
					EXEC sp_AddEventLogForCalcPrice @SupplItmsInfo = @SupplInfo, @EventType_Id = 142, @EventDscrpn = @ERROR_MES;
			END  

		-- нам удалось сохранить шапку протокола, теперь займемся его содержимым
		DECLARE @SupplItms_Guid D_GUID;
		DECLARE @Parts_Guid D_GUID;
		DECLARE @PARTS_ID int; 
		DECLARE @MEASURE_ID int; 
		DECLARE @SPLITMS_QUANTITY int; 
		DECLARE @SPLITMS_PRICE money;
		DECLARE @SPLITMS_DISCOUNT decimal(10, 4); 
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
		
		CREATE TABLE #SplItms( SupplItms_Guid uniqueidentifier, SplItms_Quantity int, SplItms_OrderQuantity int, QuantityInCollection int, 
			SUPPL_ID int, PARTS_ID int, NewSplItms_Quantity int );
    SET @ParmDefinition = N'@reserved_quantitysql int output, @splitms_idsql int output'; 
    SET @ParmDefinitionForDiscount = N'@return_valuesql int output';

		-- Пробежим по списку позиций в заказе и попробуем записать в IB
    DECLARE crSupplItms CURSOR 
    FOR SELECT [SupplItem_Guid] FROM dbo.T_SupplItem WHERE [Suppl_Guid] = @Suppl_Guid;
    OPEN crSupplItms;
    FETCH NEXT FROM crSupplItms INTO @SupplItms_Guid; 
    WHILE @@FETCH_STATUS = 0
      BEGIN
				BEGIN TRY
					SET @OptSuppl = 0;
					SET @docDiscountListInfo = NULL;
					

					
					SELECT @Parts_Guid = SplItms.PARTS_GUID, @PARTS_ID = Product.PARTS_ID, @MEASURE_ID = Measure.MEASURE_ID, 
						@SPLITMS_ORDERQTY = SplItms.[SupplItem_OrderQuantity], @SPLITMS_QUANTITY = SplItms.[SupplItem_Quantity], 
						@SPLITMS_PRICE = SplItms.[SupplItem_Price], @SPLITMS_CURRENCYPRICE = SplItms.[SupplItem_CurrencyPrice],
						@SPLITMS_DISCOUNT = SplItms.[SupplItem_Discount], @PriceInfo = SplItms.[SupplItem_XMLPrice],
						@SPLITMS_DISCOUNTPRICE = SplItms.[SupplItem_DiscountPrice], @SPLITMS_CURRENCYDISCOUNTPRICE = SplItms.[SupplItem_CurrencyDiscountPrice], 
						@docDiscountListInfo =  SplItms.[SupplItem_XMLDiscount],
						@SPLITMS_BASEPRICE = SplItms.[SupplItem_PriceImporter], 
						@SPLITMS_NDS = SplItms.[SupplItem_NDSPercent]
					FROM  [dbo].[T_SupplItem]as SplItms, dbo.T_MEASURE as Measure, [dbo].[T_Parts] as Product
					WHERE SplItms.SupplItem_Guid = @SupplItms_Guid
						AND SplItms.PARTS_GUID = Product.PARTS_GUID
						AND SplItms.MEASURE_GUID = Measure.MEASURE_GUID;
						
					--SELECT @SPLITMS_NDS = dbo.GetNDSPercentFromXml( @PriceInfo );
					SELECT @SPLITMS_MARKUP = dbo.GetMarkupPercentFromXml( @PriceInfo );
					SELECT @SPLITMS_CALC_MARKUP = dbo.GetCalcMarkUpPercentFromXml( @PriceInfo );
					--SELECT @SPLITMS_BASEPRICE = dbo.GetPrice0FromXml( @PriceInfo );
					
					-- 2009.05.07
					-- Для оптового заказа нужно передавать не разность максимально надбавки и расчитанной надбавки, а размер фактической скидки
					-- Признак "Оптовый заказ"
					SELECT @OptSuppl = dbo.GetOptFromXml( @PriceInfo );
					IF( @OptSuppl = 1 ) -- оптовый заказ
						BEGIN
							SELECT @Diff_MarkUps = dbo.GetDiscountPercentFromXml(@PriceInfo);
							
							IF( @SPLITMS_MARKUP IS NULL ) SET @SPLITMS_MARKUP = 0;
							IF( ( @Diff_MarkUps IS NULL ) OR ( @Diff_MarkUps < 0 ) ) SET @Diff_MarkUps = 0;
						END  
					ELSE
						BEGIN
							-- розничный заказ
							IF( @SPLITMS_CALC_MARKUP IS NULL ) SET @SPLITMS_CALC_MARKUP = 0;
							IF( @SPLITMS_MARKUP IS NULL ) SET @SPLITMS_MARKUP = 0;
							
							SET @Diff_MarkUps = @SPLITMS_MARKUP - @SPLITMS_CALC_MARKUP;
							IF( @Diff_MarkUps < 0 ) SET @Diff_MarkUps = 0;
						END	
					
					SET @RESERVED_QUANTITY = 0;
					SET @SPLITMS_ID = 0;
					SET @RETURN_VALUE_DISCOUNT = -1;
					
					-- 2009.05.04 вместо скидки передаем в splitms_discount разность между максимальной надбавкой и расчитанной надбавкой
					SET @SQLString = 'select @reserved_quantitysql = reserved_quantity, @splitms_idsql = splitms_id  from openquery( ' + @IBLINKEDSERVERNAME + ', ''select reserved_quantity, splitms_id from SP_ADDSPLITMSFROMSQL( ' + 
					cast( @suppl_id as nvarchar( 20)) + ', ' + cast( @PARTS_ID as nvarchar( 20)) + ', ' + cast( @MEASURE_ID as nvarchar( 20)) + ', ' + 
					cast( @SPLITMS_QUANTITY as nvarchar( 56)) + ', ' + cast( @SPLITMS_PRICE as nvarchar( 56)) + ', ' + 
					cast( /*@SPLITMS_DISCOUNT*/ @Diff_MarkUps as nvarchar( 56)) + ', ' + cast( @SPLITMS_DISCOUNTPRICE as nvarchar( 56)) + ', ' + cast( @SPLITMS_BASEPRICE as nvarchar( 56)) + ', ' + 
					cast( @SPLITMS_MARKUP as nvarchar( 56)) + ', ' + 
					cast( @STOCK_ID as nvarchar( 20)) + ', ''''' + @SUPPL_NUMstr + ''''', ' + 
					cast( @SPLITMS_CURRENCYPRICE as nvarchar( 56)) + ', ' + cast( @SPLITMS_CURRENCYDISCOUNTPRICE as nvarchar( 56)) + ', ' + cast( @SPLITMS_ORDERQTY as nvarchar( 56)) + ', ''''' + 
					cast( @cards_shipdate as nvarchar( 10)) + ''''', '  + cast( @SPLITMS_NDS as nvarchar( 56)) + ', ' + 
					cast( @SPLITMS_DISCOUNT as nvarchar( 56 )) + ')'')';

					execute sp_executesql @SQLString, @ParmDefinition, @reserved_quantitysql = @reserved_quantity output, @splitms_idsql = @splitms_id output;
					
					INSERT INTO #SplItms( SupplItms_Guid, SplItms_Quantity, SplItms_OrderQuantity, QuantityInCollection, SUPPL_ID, PARTS_ID, NewSplItms_Quantity ) 
					VALUES( @SupplItms_Guid, @reserved_quantity, @SPLITMS_ORDERQTY, dbo.GetPartsCountInCollection( @PDASUPPL_PARTS_GUID_ID, @Parts_Guid ), @suppl_id, @PARTS_ID, @reserved_quantity );
					

					-- 2010.12.22
					-- пытаемся прописать структуру скидки
					IF( @SPLITMS_DISCOUNT > 0 )
						BEGIN
							SET @DiscountTmp = @SPLITMS_DISCOUNT;
							SET @SpesialDiscount = 0;
							SET @DiscountTypeValue = 4; -- спеццена
							
							SELECT @SpesialDiscount = dbo.GetDiscountPercentByTypeFromXml( @docDiscountListInfo, @DiscountTypeValue ); -- проверим, нет ли в скидке "спеццены"
							IF( ( @SpesialDiscount > 0 ) AND ( @SpesialDiscount = @SPLITMS_DISCOUNT ) )
								BEGIN
									-- если в структуре скидки есть спеццена и она равна скидке, то поиск закончен
									SET @SQLStringForDiscount = 'select @return_valuesql = return_value  from openquery( ' + @IBLINKEDSERVERNAME + ', ''select return_value from SP_ADDSPLITMSDISCOUNTFROMSQL( ' + 
									cast( @splitms_id as nvarchar( 20)) + ', ' + cast( @DiscountTypeValue as nvarchar( 20)) + ', ' + cast( @SpesialDiscount as nvarchar( 20)) + ', ' + '0 ' + ')'')';
									
									execute sp_executesql @SQLStringForDiscount, @ParmDefinitionForDiscount, @return_valuesql = @return_value_discount output;
								END
							ELSE
								BEGIN
									DECLARE crDiscountItms CURSOR 
									FOR SELECT DISCOUNTTYPE_ID FROM [dbo].[T_DiscountTypeForCalcPrice] ORDER BY DISCOUNTTYPE_ID;
									OPEN crDiscountItms;
									FETCH NEXT FROM crDiscountItms INTO @DiscountTypeValue; 
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

													SET @SQLStringForDiscount = 'select @return_valuesql = return_value  from openquery( ' + @IBLINKEDSERVERNAME + ', ''select return_value from SP_ADDSPLITMSDISCOUNTFROMSQL( ' + 
													cast( @splitms_id as nvarchar( 20)) + ', ' + cast( @DiscountTypeValue as nvarchar( 20)) + ', ' + cast( @DiscountPercent as nvarchar( 20)) + ', ' + '0 ' + ')'')';
													
													--INSERT INTO dbo.T_ProcessPDASuppl2( SQLStringForDiscount ) VALUES( @SQLStringForDiscount );
													execute sp_executesql @SQLStringForDiscount, @ParmDefinitionForDiscount, @return_valuesql = @return_value_discount output;
												END
										
										 FETCH NEXT FROM crDiscountItms INTO @DiscountTypeValue;   
										END 
							    
									CLOSE crDiscountItms;
									DEALLOCATE crDiscountItms;
								END	
							

						END

				END TRY
				BEGIN CATCH
					SET @ERROR_NUM = ERROR_NUMBER();
					SET @ERROR_MES = '[usp_AddSupplToIB] Текст ошибки: ' + ERROR_MESSAGE();
					PRINT @ERROR_MES;

					IF( @SupplInfo IS NOT NULL )  
						EXEC sp_AddEventLogForCalcPrice @SupplItmsInfo = @SupplInfo, @EventType_Id = 141, @EventDscrpn = @ERROR_MES;

					CLOSE crSupplItms;
					DEALLOCATE crSupplItms;


					--BREAK;
				END CATCH;
       FETCH NEXT FROM crSupplItms INTO @SupplItms_Guid;   
      END 
    
    CLOSE crSupplItms;
    DEALLOCATE crSupplItms;

		SET @ERROR_MES = 'Создано приложение к протоколу.' + nChar(13) + nChar(10) + 
			'№ протокола: ' + CONVERT( nvarchar(8), @suppl_num );

    IF( @SupplInfo IS NOT NULL )  
			EXEC sp_AddEventLogForCalcPrice @SupplItmsInfo = @SupplInfo, @EventType_Id = 143, @EventDscrpn = @ERROR_MES;
			
		-- если заказ является виртуальным набором, то нужно пройтись по его содержимому и проверить, все ли позиции удалось зарезервировать
		-- в том случае, если что-то "урезано", нужно скорректировать все позиции в заказе, чтобы получилось целое число наборов
		IF( @PDASUPPL_PARTS_GUID_ID IS NOT NULL )
			BEGIN
				DECLARE @NeedDeleteSupplInInterBase bit;
				SET @NeedDeleteSupplInInterBase = 0;
				DECLARE @SumQuantity int;
				DECLARE @SumOrderQuantity int;
				SELECT @SumQuantity = SUM( SplItms_Quantity ), @SumOrderQuantity = SUM( SplItms_OrderQuantity ) FROM #SplItms;
				IF( @SumQuantity IS NULL ) SET @SumQuantity = 0;
				IF( @SumOrderQuantity IS NULL ) SET @SumOrderQuantity = 0;
				IF( @SumQuantity = 0 ) 
					SET @NeedDeleteSupplInInterBase = 1; -- ни одна позиция в заказе не сохранилась в InterBase 
				
				
				IF( ( @SumQuantity > 0 ) AND ( @SumOrderQuantity > 0 ) AND ( @SumOrderQuantity <> @SumQuantity ) )
					BEGIN
						-- заказанное @SumOrderQuantity и зарезервированное @SumQuantity количество отличаются - нужно скорректировать заказ
						DECLARE @MaxDiff int;
						DECLARE @MaxDiff_SupplItms_Guid D_GUID;
						SELECT @MaxDiff = MAX( @SumOrderQuantity - @SumQuantity ) FROM #SplItms;
						IF( ( @MaxDiff IS NOT NULL ) AND ( @MaxDiff > 0 ) )
							BEGIN
								SELECT Top 1 @MaxDiff_SupplItms_Guid = SupplItms_Guid FROM #SplItms WHERE ( @SumOrderQuantity - @SumQuantity ) = @MaxDiff;
								DECLARE @QtyInColl int;
								DECLARE @NewQtyCollections int; -- новое количество полных наборов
								SET @NewQtyCollections = 0;
								DECLARE @CurrentQty int;
								SELECT @QtyInColl = QuantityInCollection, @CurrentQty = SplItms_Quantity FROM #SplItms WHERE SupplItms_Guid = @MaxDiff_SupplItms_Guid;
								IF( ( @QtyInColl > 0 ) AND ( @CurrentQty > 0 ) ) SET @NewQtyCollections = @CurrentQty/@QtyInColl;
								IF( @NewQtyCollections = 0 ) SET @NeedDeleteSupplInInterBase = 1;
								
								-- проставим новое количество для позиций
								IF( @NewQtyCollections > 0 )
									UPDATE #SplItms SET NewSplItms_Quantity = QuantityInCollection * @NewQtyCollections;
									
								-- теперь в InterBase нужно скорректировать количество в заказе	
								DECLARE @OldQty int;
								DECLARE @NewQty int;
								DECLARE crSplItms CURSOR FOR SELECT SUPPL_ID, PARTS_ID, SplItms_Quantity, NewSplItms_Quantity FROM #SplItms;
								OPEN crSplItms;
								FETCH NEXT FROM crSplItms INTO @suppl_id, @PARTS_ID, @OldQty, @NewQty; 
								WHILE @@FETCH_STATUS = 0
									BEGIN
										BEGIN TRY
											
											SET @RESERVED_QUANTITY = 0;

											SET @ParmDefinition = N'@reserved_quantitysql int output'; 
											SET @SQLString = 'select @reserved_quantitysql = reserved_quantity from openquery( ' + @IBLINKEDSERVERNAME + ', ''select reserved_quantity from SP_EDITSPLITMSFROMSQL( ' + 
											cast( @suppl_id as nvarchar( 20)) + ', ' + cast( @PARTS_ID as nvarchar( 20)) + ', '  + 
											cast( @OldQty as nvarchar( 56)) + ', ' + cast( @NewQty as nvarchar( 56)) + ')'')';

											execute sp_executesql @SQLString, @ParmDefinition, @reserved_quantitysql = @reserved_quantity output;
											
											IF( @reserved_quantity <> @NewQty )
												SET @NeedDeleteSupplInInterBase = 1; -- на стороне InterBaseе НЕ удалось скорректировать количество, удалим заказ
											
					
 										END TRY
										BEGIN CATCH
											SET @ERROR_NUM = ERROR_NUMBER();
											SET @ERROR_MES = '[usp_AddSupplToIB] Текст ошибки: ' + ERROR_MESSAGE();
											PRINT @ERROR_MES;
											
											SET @NeedDeleteSupplInInterBase = 1; -- на стороне InterBaseе удалось скорректировать количество, удалим заказ

											CLOSE crSplItms;
											DEALLOCATE crSplItms;
											

										END CATCH;
									 FETCH NEXT FROM crSplItms INTO @suppl_id, @PARTS_ID, @OldQty, @NewQty;   
									END 
						    
								CLOSE crSplItms;
								DEALLOCATE crSplItms;
								
								-- если в InterBase все получилось ( @NeedDeleteSupplInInterBase = 0 ), то скорректируем SplItms_Quantity в #SplItms;
								IF( @NeedDeleteSupplInInterBase = 0 )
									UPDATE #SplItms SET SplItms_Quantity = NewSplItms_Quantity;
								ELSE IF( @NeedDeleteSupplInInterBase = 1 )	
									BEGIN
										-- попробуем удалить заказ в InterBase, а в SQL Server перевести заказ на ручную обработку
										EXEC SP_DeleteSupplFromIB @Suppl_Id = @Suppl_Id, @IBLINKEDSERVERNAME = @IBLINKEDSERVERNAME, 
											@SupplInfo = @SupplInfo,	@ERROR_NUM = @ERROR_NUM output, @ERROR_MES = @ERROR_MES output;
											
										IF( @ERROR_NUM = 0 )	
											BEGIN
												SELECT @OrderState_Guid = SupplState_Guid FROM dbo.T_SupplState 
												WHERE SupplState_Id = 0;

												UPDATE dbo.T_SUPPL SET SupplState_Guid = @OrderState_Guid 
													--SUPPL_ALLPRICE = 0, SUPPL_ALLDISCOUNT = 0,
													--SUPPL_CURRENCYALLPRICE = 0, SUPPL_CURRENCYALLDISCOUNT = 0 
												WHERE Suppl_Guid = @Suppl_Guid;
												
												UPDATE dbo.T_SupplItem SET [SupplItem_Quantity] = [SupplItem_OrderQuantity] 
													--[SupplItem_Price] = 0, [SupplItem_Discount] = 0, [SupplItem_DiscountPrice] = 0, 
													--[SupplItem_CurrencyPrice] = 0, [SupplItem_CurrencyDiscountPrice] = 0
												WHERE [Suppl_Guid] = @Suppl_Guid;
												
												SET @ERROR_NUM = 25;
												
												SET @ERROR_MES = '[usp_AddSupplToIB] Не удалось сохранить нужное количество в заказе, протокол был удален в InterBase.' + nChar(13) + nChar(10) + 
													'№ протокола: ' + CONVERT( nvarchar(8), @suppl_num ) + nChar(13) + nChar(10) + 
													'УИ в IB: ' + CONVERT( nvarchar(8), @suppl_id ) + nChar(13) + nChar(10) + 
													'УИ в SQL: ' + CONVERT( nvarchar(36), @Suppl_Guid );
													
												RETURN @ERROR_NUM;
											END
										ELSE
											BEGIN
												SET @ERROR_NUM = 26;
												
												SET @ERROR_MES = '[usp_AddSupplToIB] Не удалось сохранить нужное количество в заказе, не удалось удалить протокол в InterBase. Всё плохо!' + nChar(13) + nChar(10) + 
													'№ протокола: ' + CONVERT( nvarchar(8), @suppl_num ) + nChar(13) + nChar(10) + 
													'УИ в IB: ' + CONVERT( nvarchar(8), @suppl_id ) + nChar(13) + nChar(10) + 
													'УИ в SQL: ' + CONVERT( nvarchar(36), @Suppl_Guid );
													
												SELECT @OrderState_Guid = SupplState_Guid FROM dbo.T_SupplState 
												WHERE SupplState_Id = 0;

												UPDATE dbo.T_SUPPL SET SupplState_Guid = @OrderState_Guid 
															--SUPPL_ALLPRICE = 0, SUPPL_ALLDISCOUNT = 0,
															--SUPPL_CURRENCYALLPRICE = 0, SUPPL_CURRENCYALLDISCOUNT = 0 
												WHERE Suppl_Guid = @Suppl_Guid;
												
												UPDATE dbo.T_SupplItem SET [SupplItem_Quantity] = [SupplItem_OrderQuantity] 
													--[SupplItem_Price] = 0, [SupplItem_Discount] = 0, [SupplItem_DiscountPrice] = 0, 
													--[SupplItem_CurrencyPrice] = 0, [SupplItem_CurrencyDiscountPrice] = 0
												WHERE [Suppl_Guid] = @Suppl_Guid;
												
												IF NOT EXISTS ( SELECT Suppl_Guid FROM dbo.T_ProcessPDASupplDelete WHERE Suppl_Guid = @Suppl_Guid )
													INSERT INTO dbo.T_ProcessPDASupplDelete( Suppl_Guid, Suppl_Id, Suppl_IsDeleted, Operation_Date )
													VALUES( @Suppl_Guid, @suppl_id, 0, GetDate() );

												RETURN @ERROR_NUM;
											END	
										

									END
	
								-- 	
							END
					END					
			END			
		
		-- теперь нужно проставить получившееся количество в заказе
		DECLARE @RsrvQty int;
    DECLARE crSupplItmsTemp CURSOR 
    FOR SELECT SupplItms_Guid, SplItms_Quantity FROM #SplItms;
    OPEN crSupplItmsTemp;
    FETCH NEXT FROM crSupplItmsTemp INTO @SupplItms_Guid, @RsrvQty; 
    WHILE @@FETCH_STATUS = 0
      BEGIN
      
				UPDATE dbo.T_SupplItem SET SupplItem_QUANTITY = @RsrvQty WHERE SupplItem_Guid = @SupplItms_Guid;
				
				FETCH NEXT FROM crSupplItmsTemp INTO @SupplItms_Guid, @RsrvQty;   
      END 
    
    CLOSE crSupplItmsTemp;
    DEALLOCATE crSupplItmsTemp;
		 
    -- теперь нужно пересчитать сумму заказа в dbo.T_Suppl
    SELECT @SUPPL_ALLPRICE = SUM( SupplItem_QUANTITY * SupplItem_PRICE ), 
			@SUPPL_ALLDISCOUNT = SUM( ( SupplItem_QUANTITY * SupplItem_PRICE ) - ( SupplItem_QUANTITY * SupplItem_DISCOUNTPRICE ) ),
			@SUPPL_CURRENCYALLPRICE = SUM( SupplItem_QUANTITY * SupplItem_CURRENCYPRICE ),
			@SUPPL_CURRENCYALLDISCOUNT = SUM( ( SupplItem_QUANTITY * SupplItem_CURRENCYPRICE ) - ( SupplItem_QUANTITY * SupplItem_CURRENCYDISCOUNTPRICE ) ),
			@SUPPL_TOTALPRICE = SUM( SupplItem_QUANTITY * SupplItem_DISCOUNTPRICE ),
			@SUPPL_CURRENCYTOTALPRICE = SUM( SupplItem_QUANTITY * SupplItem_CURRENCYDISCOUNTPRICE )			
    FROM dbo.T_SupplItem WHERE SUPPL_GUID = @Suppl_Guid;
    
    IF( @SUPPL_ALLPRICE IS NULL ) SET @SUPPL_ALLPRICE = 0; 
    IF( @SUPPL_ALLDISCOUNT IS NULL ) SET @SUPPL_ALLDISCOUNT = 0;
		IF( @SUPPL_CURRENCYALLPRICE IS NULL ) SET @SUPPL_CURRENCYALLPRICE = 0; 
		IF( @SUPPL_CURRENCYALLDISCOUNT IS NULL ) SET @SUPPL_CURRENCYALLDISCOUNT = 0;
		IF( @SUPPL_TOTALPRICE IS NULL ) SET @SUPPL_TOTALPRICE = 0;
		IF( @SUPPL_CURRENCYTOTALPRICE IS NULL ) SET @SUPPL_CURRENCYTOTALPRICE = 0;
    
    UPDATE dbo.T_SUPPL SET SUPPL_ALLPRICE = @SUPPL_ALLPRICE, SUPPL_ALLDISCOUNT = @SUPPL_ALLDISCOUNT, 
			SUPPL_CURRENCYALLPRICE = @SUPPL_CURRENCYALLPRICE, SUPPL_CURRENCYALLDISCOUNT = @SUPPL_CURRENCYALLDISCOUNT, 
			SUPPL_TOTALPRICE = @SUPPL_TOTALPRICE, SUPPL_CURRENCYTOTALPRICE = @SUPPL_CURRENCYTOTALPRICE
    WHERE Suppl_Guid = @Suppl_Guid;
    
    -- про IB тоже не забываем
    DECLARE @return_value int;
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
				BEGIN
					SET @ERROR_MES = '[usp_AddSupplToIB] Заказ в InterBase удален, так как количество в заказе равно ноль.' + nChar(13) + nChar(10) + 'УИ протокола: ' + CONVERT( nvarchar(20), @suppl_id );
					IF( @SupplInfo IS NOT NULL )  
						EXEC sp_AddEventLogForCalcPrice @SupplItmsInfo = @SupplInfo, @EventType_Id = 145, @EventDscrpn = @ERROR_MES;

						-- пусть обрабатывают вручную
						-- 2010.11.25
						-- ставлю состояние заказа 80 - нет товара на складе
						SELECT @OrderState_Guid = SupplState_Guid FROM dbo.T_SupplState 
						WHERE SupplState_Id = 80;

						UPDATE dbo.T_SUPPL SET SupplState_Guid = @OrderState_Guid
						--, SUPPL_ALLPRICE = 0, SUPPL_ALLDISCOUNT = 0,
						--			SUPPL_CURRENCYALLPRICE = 0, SUPPL_CURRENCYALLDISCOUNT = 0 
						WHERE Suppl_Guid = @Suppl_Guid;
						
						UPDATE dbo.T_SupplItem SET SupplItem_QUANTITY = SupplItem_OrderQuantity
						--, SupplItem_PRICE = 0, 
						--	SupplItem_DISCOUNT = 0, SupplItem_DISCOUNTPRICE = 0, 
						--	SupplItem_CURRENCYPRICE = 0, SupplItem_CURRENCYDISCOUNTPRICE = 0
						WHERE Suppl_Guid = @Suppl_Guid;
						
						SET @ERROR_NUM = 44;
						
				END
			ELSE
				BEGIN
					SET @ERROR_MES = '[usp_AddSupplToIB] Не удалось пересчитать шапку протокола в InterBase.' + nChar(13) + nChar(10) + 'УИ протокола: ' + CONVERT( nvarchar(20), @suppl_id );
					IF( @SupplInfo IS NOT NULL )  
						EXEC sp_AddEventLogForCalcPrice @SupplItmsInfo = @SupplInfo, @EventType_Id = 146, @EventDscrpn = @ERROR_MES;
				END	
				
			RETURN @ERROR_NUM;
		END
	ELSE
		BEGIN
			SET @ERROR_MES = 'Пересчитана шапка к протоколу.' + nChar(13) + nChar(10) + 
				'№ протокола: ' + CONVERT( nvarchar(8), @suppl_num );
			IF( @SupplInfo IS NOT NULL )  
				EXEC sp_AddEventLogForCalcPrice @SupplItmsInfo = @SupplInfo, @EventType_Id = 144, @EventDscrpn = @ERROR_MES;

			-- прописываем в InterBase состояние 0 у заказа, чтобы его можно было видеть
			EXEC	[dbo].[SP_IBUPDATESUPPLSTATE]	@suppl_id = @SupplIdForBlock,	@suppl_state = 0,
				@linked_server = @IBLINKEDSERVERNAME,	@suppl_updated = @suppl_updated OUTPUT
		END	

	-- вроде как дошли без потерь
	SELECT @OrderState_Guid = SupplState_Guid FROM dbo.T_SupplState 
	WHERE SupplState_Id = 70;

	UPDATE dbo.T_SUPPL SET SupplState_Guid = @OrderState_Guid, SUPPL_ID = @suppl_id, [Suppl_Num] = @suppl_num, SUPPL_WEIGHT = dbo.GetSupplWeight( @Suppl_Guid ) 
	WHERE Suppl_Guid = @Suppl_Guid; -- заявка обсчитана и переведена в протокол

	SET @ERROR_NUM = 0;

 	END TRY
	BEGIN CATCH
    SET @ERROR_NUM = ERROR_NUMBER();
    SET @ERROR_MES = '[usp_AddSupplToIB]: ' + ERROR_MESSAGE();
		IF( @SupplInfo IS NOT NULL )  
			EXEC sp_AddEventLogForCalcPrice @SupplItmsInfo = @SupplInfo, @EventType_Id = 141, @EventDscrpn = @ERROR_MES;

		RETURN @ERROR_NUM;
	END CATCH;

  IF( @ERROR_NUM = 0 )
		BEGIN
			DECLARE @SecondCount int;
			SELECT @SecondCount = DATEDIFF( second, @StartProcessSuppl, GetDate() );

			SET @ERROR_MES = '[usp_AddSupplToIB] Успешное завершение операции.' + nChar(13) + nChar(10) + 
			  'Завершено создание протокола.' + nChar(13) + nChar(10) + 
				'№ протокола: ' + CONVERT( nvarchar(8), @suppl_num ) + nChar(13) + nChar(10) + 
				'УИ в IB: ' + CONVERT( nvarchar(8), @suppl_id ) + nChar(13) + nChar(10) + 
				'УИ в SQL: ' + CONVERT( nvarchar(36), @Suppl_Guid )	+ nChar(13) + nChar(10) + 
				'Время создания протокола в IB: ' + CONVERT( nvarchar(8), @SecondCount ) + ' секунд.';				;
			IF( @SupplInfo IS NOT NULL )  
				EXEC sp_AddEventLogForCalcPrice @SupplItmsInfo = @SupplInfo, @EventType_Id = 140, @EventDscrpn = @ERROR_MES;
		END

	RETURN @ERROR_NUM;

END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Возвращает данные из T_CSMORDERTYPE
--
-- Входящие параметры:
--
-- Выходные параметры:
--
-- Результат:
--    0 - успешное завершение
--    <> 0 - ошибка выполнения запроса в базу данных

ALTER PROCEDURE [dbo].[usp_GetOrderType]
  
  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output
AS

BEGIN
  
  SET @ERROR_NUM = 0;
  SET @ERROR_MES = '';

  BEGIN TRY

    SELECT OrderType_Guid, OrderType_Name,  [OrderType_Id]
    FROM dbo.T_ORDERTYPE
    ORDER BY OrderType_Id;

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

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Возвращает курс пересчёта (для ценообразования)
--
-- Входящие параметры:
--		@CurrencyIn						- уи валюты (из какой)
--		@CurrencyOut					- уи валюты (в какую)
--		@Currency_Date				- на дату
--
-- Выходные параметры:
--		@CurrencyRatePricing	- курс пересчёта
--		@ERROR_NUM						- номер ошибки
--		@ERROR_MES						- сообщение об ошибке
--
-- Результат:
--    0										- успешное завершение
--    <>0									- ошибка запроса информации из базы данных

ALTER PROCEDURE [dbo].[usp_GetCurrencyRatePricing] 
	@CurrencyIn		D_GUID = NULL, 
	@CurrencyOut	D_GUID = NULL,  
	@Currency_Date		D_DATE = NULL,

  @CurrencyRatePricing		money output,
  @ERROR_NUM		int output,
  @ERROR_MES		nvarchar(4000) output
AS

BEGIN

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = '';
	SET @CurrencyRatePricing = 0;

  BEGIN TRY

		IF( @CurrencyIn IS NULL )
			SET @CurrencyIn = ( SELECT dbo.GetCurrentCurrencyMainGuid() );

		IF( @CurrencyOut IS NULL )
			SET @CurrencyOut = ( SELECT dbo.GetCurrentCurrencyNationalGuid() );

		IF( @Currency_Date IS NULL )
			SET @Currency_Date = ( SELECT( dbo.TrimTime( GetDate() ) ) );

	  SET @CurrencyRatePricing = ( SELECT [dbo].[GetCurrencyRatePricingInOut]( @CurrencyIn, @CurrencyOut, @Currency_Date ) );

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

INSERT INTO [dbo].[T_OrderType]( OrderType_Guid, OrderType_Id, OrderType_Name )
VALUES( NEWID(), 2, 'заборная' )

GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Возвращает значения реквизитов для оформления нового заказа
--
--		Входные параметы
--
--		@In_OrderType_Guid		- УИ типа заказа
--		@In_PaymentType_Guid	- УИ формы оплаты
--
--		Выходные параметры
--
--		@SetChildDepartNull		- признак "необходимо сбросить значение дочернего подразделения"
--		@SetDepartValue				- признак "необходимо установить значение торгового подразделения"
--		@SetChildDepartValue	- признак "необходимо установить значение дочернего подразделения"
--		@Depart_Guid					- УИ торгового подразделения
--		@OrderType_Guid				- УИ типа заказа
--		@PaymentType_Guid			- УИ формы оплаты
--  @ERROR_NUM							- номер ошибки
--  @ERROR_MES							- текст ошибки
--
-- Результат:
--    0 - Успешное завершение
--    <>0 - ошибка
--
CREATE PROCEDURE [dbo].[usp_GetSupplDefParams]
	@In_OrderType_Guid		[dbo].[D_GUID] = NULL,
	@In_PaymentType_Guid	[dbo].[D_GUID] = NULL,

  @SetChildDepartNull		[dbo].[D_YESNO] output,
  @SetDepartValue				[dbo].[D_YESNO] output,
  @Depart_Guid					[dbo].[D_GUID] output,
  @SetChildDepartValue	[dbo].[D_YESNO] output,
	@OrderType_Guid				[dbo].[D_GUID] output,
	@PaymentType_Guid			[dbo].[D_GUID] output,

  @ERROR_NUM						int output,
  @ERROR_MES						nvarchar(4000) output
AS

BEGIN

  BEGIN TRY
    SET @ERROR_NUM = 0;
    SET @ERROR_MES = '';

		SET @SetChildDepartNull = NULL;
		SET @SetDepartValue = NULL;
		SET @Depart_Guid = NULL;
		SET @PaymentType_Guid = NULL;
		SET @OrderType_Guid = NULL;
		SET @SetChildDepartValue = NULL;
  
		IF( @In_OrderType_Guid IS NOT NULL )
			BEGIN
				SET @OrderType_Guid = @In_OrderType_Guid;
				DECLARE @OrderType_Id int;
				SELECT @OrderType_Id = [OrderType_Id] FROM [dbo].[T_OrderType] WHERE [OrderType_Guid] = @In_OrderType_Guid;

				IF( ( @OrderType_Id IS NOT NULL ) AND ( @OrderType_Id = 0 ) )
					BEGIN
						-- розничный заказ - форма оплаты должна быть №1
						SET @PaymentType_Guid = ( SELECT dbo.GetPaymentType_1_Guid() );
						SET @SetChildDepartNull = 1;
						SET @SetChildDepartValue = 0;
					END
				ELSE IF( ( @OrderType_Id IS NOT NULL ) AND ( @OrderType_Id = 1 ) )
					BEGIN
						-- оптовый заказ - форма оплаты должна быть №2
						SET @PaymentType_Guid = ( SELECT dbo.GetPaymentType_2_Guid() );
						SET @SetChildDepartNull = 0;
						SET @SetChildDepartValue = 1;
					END
				ELSE IF( ( @OrderType_Id IS NOT NULL ) AND ( @OrderType_Id = 2 ) )
					BEGIN
						-- заборная - форма оплаты должна быть №2, подразделение "OFF"
						SET @PaymentType_Guid = ( SELECT dbo.GetPaymentType_2_Guid() );
						SET @SetChildDepartNull = 0;
						SET @SetChildDepartValue = 1;
						SET @SetDepartValue = 1;
						SELECT @Depart_Guid = [Depart_Guid] FROM [dbo].[T_Depart] WHERE UPPER( [Depart_Code] ) = 'OFF';
					END
			END

		--IF( @In_PaymentType_Guid IS NOT NULL )
		--	BEGIN
		--		IF( @PaymentType_Guid IS NULL ) SET @PaymentType_Guid = @In_PaymentType_Guid;
				 
		--		DECLARE @PaymentType_1_Guid	D_GUID;
		--		DECLARE @PaymentType_2_Guid	D_GUID;
		--		SET @PaymentType_1_Guid = ( SELECT dbo.GetPaymentType_1_Guid() );
		--		SET @PaymentType_2_Guid = ( SELECT dbo.GetPaymentType_2_Guid() );

		--		IF( @In_PaymentType_Guid = @PaymentType_1_Guid )
		--			BEGIN
		--				-- форма оплаты №1 - заказ должен быть розничным, а дочернее подразделение необходимо отменить
		--				SET @SetChildDepartNull = 1;
		--				SET @SetChildDepartValue = 1;
		--				SELECT @OrderType_Guid = OrderType_Guid FROM [dbo].[T_OrderType] WHERE OrderType_Id = 0;
		--			END
		--		ELSE IF( @In_PaymentType_Guid = @PaymentType_2_Guid )
		--			BEGIN
		--				-- форма оплаты №2 - заказ должен быть оптовым либо заборной, а дочернее подразделение необходимо установить
		--				SET @SetChildDepartNull = 0;
		--				SET @SetChildDepartValue = 1;
		--				SELECT @OrderType_Guid = OrderType_Guid FROM [dbo].[T_OrderType] WHERE OrderType_Id = 1;
		--			END

		--	END

 	END TRY
	BEGIN CATCH
    SET @ERROR_NUM = ERROR_NUMBER();
    SET @ERROR_MES = '[usp_GetSupplDefParams]: ' + ERROR_MESSAGE();

	END CATCH;

  IF( @ERROR_NUM = 0 )
		SET @ERROR_MES = '[usp_GetSupplDefParams] Успешное завершение операции.';

	RETURN @ERROR_NUM;

END

GO
GRANT EXECUTE ON [dbo].[usp_GetSupplDefParams] TO [public]
GO

ALTER TABLE [dbo].[T_Settings] ADD Settings_UserName	D_NAME NULL
	GO

	UPDATE [dbo].[T_Settings] SET Settings_UserName = [Settings_Name]
	GO

	UPDATE [dbo].[T_Settings] SET Settings_UserName = 'Импорт приложения к заказу для магазина-склада'
	WHERE Settings_UserName = 'ImportDataInOrderSettings'
	GO

	DECLARE @doc xml;
	SET @doc = '<ImportDataInOrderSettings xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <ColumnItem TOOLS_ID="10" TOOLS_NAME="STARTROW" TOOLS_USERNAME="Начальная строка" TOOLS_DESCRIPTION="№ строки, с которой начинается импорт данных" TOOLS_VALUE="2" TOOLSTYPE_ID="2" />
  <ColumnItem TOOLS_ID="11" TOOLS_NAME="PARTS_ID" TOOLS_USERNAME="Код товара" TOOLS_DESCRIPTION="№ строки с кодом товара" TOOLS_VALUE="1" TOOLSTYPE_ID="2" />
  <ColumnItem TOOLS_ID="12" TOOLS_NAME="QUANTITY" TOOLS_USERNAME="Количество" TOOLS_DESCRIPTION="№ столбца с количеством товара" TOOLS_VALUE="2" TOOLSTYPE_ID="2" />
  <ColumnItem TOOLS_ID="13" TOOLS_NAME="PRICE" TOOLS_USERNAME="Цена" TOOLS_DESCRIPTION="№ столбца с ценой товара" TOOLS_VALUE="3" TOOLSTYPE_ID="2" />
</ImportDataInOrderSettings>';

	INSERT INTO [dbo].[T_Settings]( Settings_Guid, Settings_Name, Settings_UserName, Settings_XML, Record_Updated, Record_UserUdpated )
	VALUES( NEWID(), 'ImportDataInOrderByIDSettings', 'Импорт приложения к заказу по коду товара (ID)', @doc, GetDate(), 'Admin' );

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Возвращает список записей из ( dbo.T_Settings )
--
-- Входные параметры:
--
-- Выходные параметры:
--
--		@ERROR_NUM	- код ошибки
--		@ERROR_MES	- текст ошибки
--
-- Результат:
--    0 - успешное завершение
--    <>0 - ошибка запроса информации из базы данных

CREATE PROCEDURE [dbo].[usp_GetSettings] 
  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output
AS

BEGIN

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = '';

  BEGIN TRY

    SELECT Settings_Guid, Settings_Name, Settings_XML, Settings_UserName
    FROM dbo.T_Settings
    ORDER BY Settings_UserName;

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
GRANT EXECUTE ON [dbo].[usp_GetSettings] TO [public]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Возвращает запись с настройками для импорта данных в заказ ( dbo.T_Settings )
--
-- Входные параметры:
--
-- Выходные параметры:
--
-- Результат:
--    0 - успешное завершение
--    <>0 - ошибка запроса информации из базы данных

CREATE PROCEDURE [dbo].[usp_GetImportDataInOrderByIDSettings] 
  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output
AS

BEGIN

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = '';

  BEGIN TRY

    SELECT Top 1 Settings_Guid, Settings_Name, Settings_XML
    FROM dbo.T_Settings
    WHERE Settings_Name = 'ImportDataInOrderByIDSettings';

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
GRANT EXECUTE ON [dbo].[usp_GetImportDataInOrderByIDSettings] TO [public]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Добавляет новую запись в таблицу dbo.T_Suppl
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
	--@Stmnt_Guid	- уи договора 
	--@SetOrderInQueue - признак "поместить заказ в очередь для последующей обработки"
--
--
-- Выходные параметры:
--  @Suppl_Guid - уникальный идентификатор записи
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
	@Stmnt_Guid [dbo].[D_GUID] = NULL,
	@SetOrderInQueue	[dbo].[D_YESNO] = 0,	

  @Order_Guid D_GUID output,
  @Suppl_Id D_ID output,
  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output

AS

BEGIN

	BEGIN TRY

    SET @ERROR_NUM = 0;
    SET @ERROR_MES = '';
    SET @Order_Guid = NULL;
    SET @Suppl_Id = NULL;
    
    DECLARE @Order_Num					D_ID = 0;
    DECLARE @Order_SubNum				D_ID = 0;
		DECLARE @PaymentType_1_Guid	D_GUID = NULL;
		DECLARE @PaymentType_2_Guid	D_GUID = NULL;
		DECLARE @Currency_Guid			D_GUID = NULL;

		SET @PaymentType_1_Guid = ( SELECT dbo.GetPaymentType_1_Guid() );
		SET @PaymentType_2_Guid = ( SELECT dbo.GetPaymentType_2_Guid() );
    
    IF( @Order_BeginDate IS NULL ) SET @Order_BeginDate = dbo.TrimTime( GETDATE() );
    IF( @OrderState_Guid IS NULL ) SELECT @OrderState_Guid = dbo.GetFirstSupplState();
    
    -- Проверяем наличие клиента с указанным идентификатором
    IF NOT EXISTS ( SELECT Customer_Guid FROM dbo.T_Customer WHERE Customer_Guid = @Customer_Guid )
      BEGIN
        SET @ERROR_NUM = 1;
        SET @ERROR_MES = 'В базе данных не найден клиент с указанным идетнификатором.' + Char(13) + 
          'УИ: ' + Char(9) + CONVERT( nvarchar(36), @Customer_Guid  );
        RETURN @ERROR_NUM;
      END

    SELECT @Order_Num = MAX( [Suppl_Num] ) FROM dbo.T_Suppl WHERE Customer_Guid = @Customer_Guid;
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
				ELSE
					BEGIN
						IF(( @PaymentType_2_Guid IS NOT NULL ) AND ( @PaymentType_Guid = @PaymentType_2_Guid ) )
							SET @Currency_Guid = ( SELECT dbo.GetCurrentCurrencyMainGuid() );
						ELSE
							SET @Currency_Guid = ( SELECT dbo.GetCurrentCurrencyNationalGuid() );
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
			
    -- Проверяем наличие договора с указанным идентификатором
    IF( @Stmnt_Guid IS NOT NULL )
			BEGIN
				IF NOT EXISTS ( SELECT [AgreementWithCustomer_Guid] FROM [dbo].[T_AgreementWithCustomer] 
												WHERE [AgreementWithCustomer_Guid] = @Stmnt_Guid )
					BEGIN
						SET @ERROR_NUM = 11;
						SET @ERROR_MES = 'В базе данных не найден договор с указанным идетнификатором.' + Char(13) + 
							'УИ: ' + Char(9) + CONVERT( nvarchar(36), @Stmnt_Guid  );
						RETURN @ERROR_NUM;
					END
			END

			

    DECLARE @NewID D_GUID;
    SET @NewID = NEWID( );	
    
    BEGIN TRANSACTION UpdateData;
    
    INSERT INTO dbo.T_Suppl( Suppl_Guid, Suppl_Id, Suppl_Num, Suppl_Version, Suppl_BeginDate, SupplState_Guid, 
			Suppl_Bonus, Depart_Guid, Customer_Guid, CustomerChild_Guid, OrderType_Guid, 
			PaymentType_Guid, Suppl_Note, Suppl_DeliveryDate, Rtt_Guid, Address_Guid, Stock_Guid, 
			Parts_Guid, AgreementWithCustomer_Guid, SHOW_IN_DELIVERY, Suppl_ExcludeFromAdj, Currency_Guid )
    VALUES( @NewID, 0, @Order_Num, @Order_SubNum, @Order_BeginDate, @OrderState_Guid, 
			@Order_MoneyBonus, @Depart_Guid, @Customer_Guid, @CustomerChild_Guid, @OrderType_Guid, 
			@PaymentType_Guid, @Order_Description, @Order_DeliveryDate, @Rtt_Guid, @Address_Guid, 
			@Stock_Guid, @Parts_Guid, @Stmnt_Guid, 1, 0, @Currency_Guid );
    
    INSERT INTO [dbo].[T_SupplItem]( SupplItem_Guid, Splitms_Id, Suppl_Guid, Parts_Guid, Measure_Guid, SupplItem_Quantity, 
			SupplItem_OrderQuantity, SupplItem_PriceImporter,	SupplItem_Price, SupplItem_Discount,
			SupplItem_DiscountPrice, SupplItem_NDSPercent, SupplItem_CurrencyPrice,	SupplItem_CurrencyDiscountPrice )  
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
    
    SELECT @Order_Quantity = SUM( [SupplItem_Quantity] ), @Order_SumReserved = SUM( [SupplItem_AllPrice] ), 
			@Order_SumReservedWithDiscount = SUM( [SupplItem_TotalPrice] ), 
			@Order_SumReservedInAccountingCurrency = SUM( [SupplItem_CurrencyAllPrice] ),
			@Order_SumReservedWithDiscountInAccountingCurrency = SUM( [SupplItem_CurrencyTotalPrice] )
		FROM [dbo].[T_SupplItem]
		WHERE [Suppl_Guid] = @NewID;	
    
    UPDATE [dbo].[T_Suppl] SET [Suppl_Quantity]  = @Order_Quantity, [Suppl_AllPrice] = @Order_SumReserved, 
			[Suppl_TotalPrice] = @Order_SumReservedWithDiscount, 
      [Suppl_CurrencyAllPrice] = @Order_SumReservedInAccountingCurrency, 
      [Suppl_CurrencyTotalPrice] = @Order_SumReservedWithDiscountInAccountingCurrency,
			[Suppl_AllDiscount] = ( @Order_SumReserved - @Order_SumReservedWithDiscount ),
			[Suppl_CurrencyAllDiscount] = ( @Order_SumReservedInAccountingCurrency - @Order_SumReservedWithDiscountInAccountingCurrency )
    WHERE Suppl_Guid = @NewID;  
    
    SET @Order_Guid = @NewID;
		DECLARE @Suppl_Num int = NULL;
    
		IF( @SetOrderInQueue = 1 )
			BEGIN
				-- заказ помещается в очередь для последующей автоматической обработки
				DECLARE @SupplStateStockIsDefine_Guid	D_GUID;
				SELECT @SupplStateStockIsDefine_Guid = [SupplState_Guid] FROM [dbo].[T_SupplState] WHERE [SupplState_Id] = 10;

				UPDATE [dbo].[T_Suppl] SET [SupplState_Guid] = @SupplStateStockIsDefine_Guid;
			END
		ELSE
			BEGIN
				-- заказ обрабатывается немедленно
				IF( @bCalcPrices = 1 )
					-- шапка заказа и приложение к заказу сохранены в SQL Server,
					-- приступаем к расчёту цен
					EXEC dbo.sp_ProcessPricesInSuppl @Suppl_Guid = @Order_Guid, @ERROR_NUM = @ERROR_NUM out, @ERROR_MES = @ERROR_MES out;
		
				IF( @ERROR_NUM = 0 )
					BEGIN
						-- теперь заказ необходимо "разместить" в InterBase
						EXEC dbo.usp_AddSupplToIB @Suppl_Guid = @Order_Guid, @Suppl_Id = @Suppl_Id out, @Suppl_Num = @Suppl_Num out, 
							@ERROR_NUM = @ERROR_NUM out, @ERROR_MES = @ERROR_MES out;
					END
			END

		
		IF( @ERROR_NUM = 0 )	
			BEGIN
				UPDATE dbo.T_Suppl SET Suppl_Id = @Suppl_Id, Suppl_Num = @Suppl_Num 
				WHERE Suppl_Guid = @Order_Guid;
				
				COMMIT TRANSACTION UpdateData;
			END 
		ELSE	
			ROLLBACK TRANSACTION UpdateData;
    
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION UpdateData;
    SET @ERROR_NUM = ERROR_NUMBER();
    SET @ERROR_MES =  ERROR_MESSAGE();
		
	END CATCH;

	IF( @ERROR_NUM = 0 )
		SET @ERROR_MES = 'Успешное завершение операции.';
		
	RETURN @ERROR_NUM;
END

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Возвращает цену для заданного товара, формы оплаты и склада отгрузки

--		Входные параметры

--		@Parts_Guid					УИ товара
--		@PaymentType_Guid		УИ формы оплаты
--		@Stock_Guid					УИ склада
--		@DiscountPercent		% скидки
--		@PriceInput					отпускная цена (для случаев, когда отпускная цена импортируется извне) 
--		@InputPriceIsFixed	признак "отпускная цена задана на входе"
--		
--		Выходные параметры

--		@NDSPercent					ставка НДС, %
--		@PriceImporter			отпускная цена без НДС, BYB
--		@Price							отпускная цена с НДС без учета скидки, BYB
--		@PriceWithDiscount	отпускная цена с НДС с учетом скидки, BYB
--		@PriceInAccountingCurrency							отпускная цена в валюте учёта без учета скидки
--		@PriceWithDiscountInAccountingCurrency	отпускная цена в валюте учёта с учетом скидки
--		@ERROR_NUM					номер ошибки
--		@ERROR_MES					текст ошибки

--		Возвращает
--
--		0			- удачное завершение операции
--		<> 0	- ошибка

ALTER PROCEDURE [dbo].[usp_GetPrice] 
	@Parts_Guid					D_GUID,
	@PaymentType_Guid		D_GUID,
	@Stock_Guid					D_GUID,
	@DiscountPercent		decimal(18, 4) = 0,
  @PriceInput					D_MONEY = 0,
	@InputPriceIsFixed	D_YESNO = 0,

	@NDSPercent					D_MONEY	output,
  @PriceImporter			D_MONEY output,
  @Price							D_MONEY output,
  @PriceWithDiscount	D_MONEY output,
  @PriceInAccountingCurrency							D_MONEY output,
  @PriceWithDiscountInAccountingCurrency	D_MONEY output,

  @ERROR_NUM					int output,
  @ERROR_MES					nvarchar(4000) output

AS

BEGIN
SET NOCOUNT ON;

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = '';

  SET @PriceImporter = 0;
  SET @Price = 0;
  SET @PriceWithDiscount = 0;
  SET @PriceInAccountingCurrency = 0;
  SET @PriceWithDiscountInAccountingCurrency = 0;
  SET @NDSPercent = 0;

  DECLARE @IsPartsImporter D_YESNO  -- признак "товар импортера" 
  DECLARE @ChargesPercent D_MONEY;  -- процент надбавки по товару
  
	BEGIN TRY


    -- Признак "товар импортера"
    SELECT @IsPartsImporter = dbo.GetPropertieImporterForStockGuid( @Stock_Guid, @Parts_Guid );
    IF ( @IsPartsImporter IS NULL )
      BEGIN
        SET @ERROR_NUM = 1;
        SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) +  ' [usp_GetPrice] Не удалось определить признак "товар импортера".';

	      RETURN @ERROR_NUM;
      END 

    -- Ставка НДС, %
    SELECT @NDSPercent = dbo.GetNDSPercentForPartsGuid(@Parts_Guid);
    IF ( @NDSPercent IS NULL )
      BEGIN
        SET @ERROR_NUM = 2;
        SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) +  ' [usp_GetPrice] Не удалось определить ставку НДС.';

	      RETURN @ERROR_NUM;
      END 

    -- Размер надбавки по товару, %
    SELECT @ChargesPercent = dbo.GetPropertieChargeForStockAndParts( @Stock_Guid, @Parts_Guid );
    IF ( @ChargesPercent IS NULL )
      BEGIN
        SET @ERROR_NUM = 3;
        SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) +  ' [usp_GetPrice] Не удалось определить размер надбавки.';

	      RETURN @ERROR_NUM;
      END 
      
    -- форма оплаты
    DECLARE @PaymentTypeForm1Guid D_GUID;
    SELECT @PaymentTypeForm1Guid = [dbo].[GetPaymentTypeForm1Guid]();
    
    DECLARE @PaymentTypeForm2Guid D_GUID;
    SELECT @PaymentTypeForm2Guid = [dbo].[GetPaymentTypeForm2Guid]();
     
    -- Не смотря на все препоны и проверки, дело дошло до прайс-листа
    -- Цены из прайс-листа
    DECLARE @Price0 decimal(18, 4);      -- Цена импортера, BYB (прайс-лист)
    DECLARE @Price0_2 decimal(18, 4);    -- отпускная цена в EUR для заказов по форме оплаты №2 (прайс-лист)
    DECLARE @Price2 decimal(18, 4);      -- отпускная цена в BYB для заказов по форме оплаты №2 (прайс-лист) 
    DECLARE @Price11 decimal(18, 4);     -- отпускная цена в BYB для заказов по форме оплаты №1 (прайс-лист)   
    DECLARE @Price0_11 decimal(18, 4);   -- отпускная цена в EUR для заказов по форме оплаты №1 (прайс-лист)
    DECLARE @CurrencyRatePricing float; 
    DECLARE @MarkUpPercent decimal(18, 4);       
    DECLARE @CalcMarkUpPercent decimal(18, 4); -- рассчитанная оптовая надбавка
    
		DECLARE @PartsubtypePriceTypePrice0_Guid	D_GUID;
		DECLARE @PartsubtypePriceTypePrice0_2_Guid	D_GUID;
		DECLARE @PartsubtypePriceTypePrice2_Guid	D_GUID;
		DECLARE @PartsubtypePriceTypePrice0_11_Guid	D_GUID;
		DECLARE @PartsubtypePriceTypePrice11_Guid	D_GUID;

		SELECT Top 1 @PartsubtypePriceTypePrice0_Guid = [PartsubtypePriceType_Guid] FROM [dbo].[T_PartsubtypePriceType] WHERE [PartsubtypePriceType_Abbr] = 'PRICE0';
		SELECT Top 1 @PartsubtypePriceTypePrice0_11_Guid = [PartsubtypePriceType_Guid] FROM [dbo].[T_PartsubtypePriceType] WHERE [PartsubtypePriceType_Abbr] = 'PRICE0_11';
		SELECT Top 1 @PartsubtypePriceTypePrice11_Guid = [PartsubtypePriceType_Guid] FROM [dbo].[T_PartsubtypePriceType] WHERE [PartsubtypePriceType_Abbr] = 'PRICE11';
		SELECT Top 1 @PartsubtypePriceTypePrice0_2_Guid = [PartsubtypePriceType_Guid] FROM [dbo].[T_PartsubtypePriceType] WHERE [PartsubtypePriceType_Abbr] = 'PRICE0_2';
		SELECT Top 1 @PartsubtypePriceTypePrice2_Guid = [PartsubtypePriceType_Guid] FROM [dbo].[T_PartsubtypePriceType] WHERE [PartsubtypePriceType_Abbr] = 'PRICE2';

		SELECT @Price0 = [Price_Value] FROM [dbo].[T_PartsPriceList] WHERE [Parts_Guid] = @Parts_Guid AND [PartsubtypePriceType_Guid] = @PartsubtypePriceTypePrice0_Guid;
		SELECT @Price0_11 = [Price_Value] FROM [dbo].[T_PartsPriceList] WHERE [Parts_Guid] = @Parts_Guid AND [PartsubtypePriceType_Guid] = @PartsubtypePriceTypePrice0_11_Guid;
		SELECT @Price11 = [Price_Value] FROM [dbo].[T_PartsPriceList] WHERE [Parts_Guid] = @Parts_Guid AND [PartsubtypePriceType_Guid] = @PartsubtypePriceTypePrice11_Guid;
		SELECT @Price0_2 = [Price_Value] FROM [dbo].[T_PartsPriceList] WHERE [Parts_Guid] = @Parts_Guid AND [PartsubtypePriceType_Guid] = @PartsubtypePriceTypePrice0_2_Guid;
		SELECT @Price2 = [Price_Value] FROM [dbo].[T_PartsPriceList] WHERE [Parts_Guid] = @Parts_Guid AND [PartsubtypePriceType_Guid] = @PartsubtypePriceTypePrice2_Guid;

    IF( @Price0 IS NULL ) SET @Price0 = 0;
    IF( @Price0_2 IS NULL ) SET @Price0_2 = 0;
    IF( @Price2 IS NULL ) SET @Price2 = 0;
    IF( @Price0_11 IS NULL ) SET @Price0_11 = 0;
    IF( @Price11 IS NULL ) SET @Price11 = 0;

    -- Для определения цены в валюте учета используем курс ценообразования
		DECLARE @CurrencyBYB_GUID	D_GUID;
		DECLARE @CurrencyEUR_GUID	D_GUID;

		SELECT @CurrencyBYB_GUID = [Currency_Guid] FROM [dbo].[T_Currency] WHERE [Currency_Abbr] = 'BYB';
		SELECT @CurrencyEUR_GUID = [Currency_Guid] FROM [dbo].[T_Currency] WHERE [Currency_Abbr] = 'EUR';

		SELECT @CurrencyRatePricing = [dbo].[GetCurrencyRatePricingInOut](@CurrencyEUR_GUID, @CurrencyBYB_GUID, Getdate());
    
    IF( ( @CurrencyRatePricing IS NULL ) OR ( @CurrencyRatePricing = 0 ) )
      BEGIN
        SET @ERROR_NUM = 4;
        SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) +  ' [usp_GetPrice] Не удалось определить курс ценообразования.';

	      RETURN @ERROR_NUM;
      END 
    
    IF( @Price0 IS NULL ) SET @Price0 = 0;
    IF( @Price0_2 IS NULL ) SET @Price0_2 = 0;
    IF( @Price2 IS NULL ) SET @Price2 = 0;
    IF( @Price11 IS NULL ) SET @Price11 = 0;
    IF( @Price0_11 IS NULL ) SET @Price0_11 = 0;
		SET @MarkUpPercent = 0;
		SET @CalcMarkUpPercent = 0;

		-- Расчёт отпускной цены
		IF( @PaymentType_Guid = @PaymentTypeForm1Guid )
			BEGIN
				-- форма оплаты №1

				IF( ( @Price0 = 0 ) OR ( @Price2 = 0 ) )
					BEGIN
				    
						SET @ERROR_NUM = 10;
						SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) + 
							'@Price0: ' + CONVERT( nvarchar(15), @Price0 ) + nChar(13) + nChar(10) + 
							'@Price0_2: ' + CONVERT( nvarchar(15), @Price0_2 )  + nChar(13) + nChar(10) + 
							'@Price2: ' + CONVERT( nvarchar(15), @Price2 );
						RETURN @ERROR_NUM;
					END 
				
				IF( @IsPartsImporter = 0 )
					BEGIN
						-- компания-продавец является оптовым звеном
						IF( @InputPriceIsFixed = 0 )
							BEGIN
								SET @Price = ( SELECT dbo.GetRoundMoney( @Price0 * ( 1 + ( @ChargesPercent/100 ) ) ) );
								SET @Price = @Price * ( 1 + ( @NDSPercent/100 ) );

								SET @PriceImporter = @Price0;
								SET @PriceWithDiscount = @PriceImporter * ( 1 + ( ( @ChargesPercent * ( 1 - ( @DiscountPercent/100 ) ) ) - @DiscountPercent )/100 );
								SET @PriceWithDiscount = @PriceWithDiscount * ( 1 + ( @NDSPercent/100 ) );
						
								-- корректировка оптовой надбавки и цены со скидкой
								SET @MarkUpPercent =  ( SELECT dbo.GetRoundMoney( (((@PriceWithDiscount/(1 + @NDSPercent/100) )/@PriceImporter) - 1)*100 ) );
								SET @CalcMarkUpPercent = @MarkUpPercent;
								SET @PriceWithDiscount = ( SELECT dbo.GetRoundMoney( @PriceImporter * ( 1 + ( @MarkUpPercent/100 ) ) ) );
								SET @PriceWithDiscount = @PriceWithDiscount * ( 1 + ( @NDSPercent/100 ) );
							END
						ELSE
							BEGIN
								-- отпускная цена задана на входе
								SET @Price = @PriceInput;
								SET @PriceImporter = @Price0;
								SET @PriceWithDiscount = @PriceInput;
							END

					END
				ELSE IF( @IsPartsImporter = 1 )	
					BEGIN
						-- товар импортирован непосредственно компанией-продавцом
						IF( @InputPriceIsFixed = 0 )
							BEGIN
								SET @PriceImporter = ( SELECT dbo.GetRoundMoney( @Price0 * ( 1 - ( @DiscountPercent/100 ) ) ) );
								SET @PriceWithDiscount = @PriceImporter * ( 1 + ( @NDSPercent/100 ) );
								SET @Price = @PriceWithDiscount;
							END
						ELSE
							BEGIN
								-- отпускная цена задана на входе
								SET @PriceImporter = ( SELECT dbo.GetRoundMoney( @Price0 * ( 1 - ( @DiscountPercent/100 ) ) ) );
								SET @PriceWithDiscount = @PriceInput;
								SET @Price = @PriceInput;
							END
					END

				-- валютный эквивалент отпускной цены
		    SET @PriceInAccountingCurrency = @Price/@CurrencyRatePricing;
		    SET @PriceWithDiscountInAccountingCurrency = @PriceWithDiscount/@CurrencyRatePricing;

			END
		ELSE IF( @PaymentType_Guid = @PaymentTypeForm2Guid )
			BEGIN
				-- форма оплаты №2
				IF( ( @Price0 = 0 ) OR ( @Price0_11 = 0 )  OR ( @Price11 = 0 ))
					BEGIN
						SET @ERROR_NUM = 10;
						SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) + 
							'@Price0: ' + CONVERT( nvarchar(15), @Price0 ) + nChar(13) + nChar(10) + 
							'@Price0_11: ' + CONVERT( nvarchar(15), @Price0_11 )  + nChar(13) + nChar(10) + 
							'@Price11: ' + CONVERT( nvarchar(15), @Price11 );
						RETURN @ERROR_NUM;
					END 
				
				IF( @InputPriceIsFixed = 0 )
					BEGIN
						SET @Price = @Price11;
						SET @PriceInAccountingCurrency = @Price0_11;
						SET @PriceImporter = @Price / ( 1 + @NDSPercent/100 );

						IF( @IsPartsImporter = 1 )
							SET @Price = @PriceImporter * ( 1 + ( @NDSPercent/100 ) );
						SET @PriceWithDiscount = @Price * ( 1 - ( @DiscountPercent/100 ) );				
						SET @PriceWithDiscountInAccountingCurrency = @PriceInAccountingCurrency * ( 1 - ( @DiscountPercent/100 ) );
					END
				ELSE
					BEGIN
						-- отпускная цена задана на входе
						SET @Price = @Price11;
						SET @PriceInAccountingCurrency = @PriceInput;
						SET @PriceImporter = @Price / ( 1 + @NDSPercent/100 );

						IF( @IsPartsImporter = 1 )
							SET @Price = @PriceImporter * ( 1 + ( @NDSPercent/100 ) );
						SET @PriceWithDiscount = @Price * ( 1 - ( @DiscountPercent/100 ) );				
						SET @PriceWithDiscountInAccountingCurrency = @PriceInAccountingCurrency * ( 1 - ( @DiscountPercent/100 ) );
					END

			END


	END TRY
	BEGIN CATCH
		SET @ERROR_NUM = ERROR_NUMBER();
		SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) +  ' [usp_GetPrice] Текст ошибки: ' + ERROR_MESSAGE();

    RETURN @ERROR_NUM;
	END CATCH;

	IF( @ERROR_NUM = 0 )
		SET @ERROR_MES = @ERROR_MES + ' [usp_GetPrice] Успешное завершение операции.';
		
	RETURN @ERROR_NUM;
END

GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Возвращает признак того, можно ли редактировать заказ
--
--		Входные параметры
--
--		@Suppl_Guid		УИ заказа
--
--		Выходные параметры
--
--		@IsPossible	признак того, можно ли редактировать заказ (0 - нельзя; 1- можно)
--		@ERROR_NUM	код ошибки
--		@ERROR_MES	текст ошибки
--

CREATE PROCEDURE [dbo].[usp_CheckPossibleEditSuppl]
	@Suppl_Guid	D_GUID,

	@IsPossible	D_YESNO output,
  @ERROR_NUM	int output,
  @ERROR_MES	nvarchar(4000) output

AS

BEGIN

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = '';
  SET @IsPossible = 0;

	BEGIN TRY
	
		DECLARE @SupplStateStockIsDefine_Guid	D_GUID;
		DECLARE @SupplState_Guid	D_GUID;
		SELECT @SupplStateStockIsDefine_Guid = [SupplState_Guid] FROM [dbo].[T_SupplState] WHERE [SupplState_Id] = 10;
		SELECT @SupplState_Guid = [SupplState_Guid] FROM [dbo].[T_Suppl] WHERE [Suppl_Guid] = @Suppl_Guid;

		IF( ( @SupplState_Guid IS NOT NULL ) AND @SupplState_Guid IN ( @SupplStateStockIsDefine_Guid ) )
			SET @IsPossible = 1;

	END TRY
	BEGIN CATCH
		SET @IsPossible = 0;

		SET @ERROR_NUM = ERROR_NUMBER();
		SET @ERROR_MES = ERROR_MESSAGE();
	END CATCH;

	IF( @ERROR_NUM = 0 )
		SET @ERROR_MES = 'Успешное завершение операции.';

	RETURN @ERROR_NUM;
END

GO
GRANT EXECUTE ON [dbo].[usp_CheckPossibleEditSuppl] TO [public]
GO

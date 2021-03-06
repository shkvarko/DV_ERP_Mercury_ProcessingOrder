USE [ERP_Mercury]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetSuppl]    Script Date: 06.03.2014 15:39:47 ******/
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
		
		IF( @Order_Guid IS NULL )
			BEGIN
				INSERT INTO #SRCSUPPL( Suppl_Guid, Customer_Guid, Stock_Guid, PaymentType_Guid, SupplState_Guid )
				SELECT Suppl_Guid, Customer_Guid, Stock_Guid, PaymentType_Guid, SupplState_Guid
				FROM T_Suppl WHERE Suppl_BeginDate BETWEEN @BeginDate AND @EndDate;
					--AND SupplState_Guid <> @SupplStateSupplIsBasic_Guid;
			END
		ELSE
			BEGIN
				INSERT INTO #SRCSUPPL( Suppl_Guid, Customer_Guid, Stock_Guid, PaymentType_Guid, SupplState_Guid )
				SELECT Suppl_Guid, Customer_Guid, Stock_Guid, PaymentType_Guid, SupplState_Guid
				FROM T_Suppl WHERE Suppl_Guid = @Order_Guid;
			END

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


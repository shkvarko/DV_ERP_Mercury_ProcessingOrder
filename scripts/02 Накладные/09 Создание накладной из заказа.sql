USE [ERP_Mercury]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Импортирует информацию о накладной из InterBase
--
-- Входные параметры
--
--		@IN_WAYBILL_ID			- УИ накладной в InterBase
--		@IBLINKEDSERVERNAME	- имя LinkedServer
--
-- Выходные параметры
--
--		@ERROR_NUM					- код ошбики
--		@ERROR_MES					- текст ошибки
--
CREATE PROCEDURE [dbo].[usp_SynchOneWaybillInfoFromIB] 
	@IN_WAYBILL_ID			D_ID,
  @IBLINKEDSERVERNAME	D_NAME = NULL,
  
  @ERROR_NUM					int output,
  @ERROR_MES					nvarchar(4000) output
  
AS
-- процедура предназначена для выборки данных из справочника цен в InterBase
BEGIN
	SET NOCOUNT ON;
	
  SET @ERROR_NUM = 0;
  SET @ERROR_MES = '';

  IF( @IBLINKEDSERVERNAME IS NULL ) SELECT @IBLINKEDSERVERNAME = dbo.GetIBLinkedServerName();

	BEGIN TRY

		CREATE TABLE #WAYBILL( WAYBILL_ID int, STOCK_ID int, CUSTOMER_ID int, DEPART_CODE nvarchar(3),
			WAYBILL_SRCCODE int, WAYBILL_SRCDOC nvarchar(16), WAYBILL_SRCID int,  WAYBILL_SRCDATE date,
			WAYBILL_BEGINDATE date, WAYBILL_ALLPRICE money, WAYBILL_RETALLPRICE money,
			WAYBILL_ENDDATE date, CURRENCY_CODE nvarchar(3), WAYBILL_CURRENCYRATE money,
			WAYBILL_AMOUNTPAID money, WAYBILL_DATELASTPAID date, WAYBILL_SHIPDATE date,
			WAYBILL_SHIPPED int, WAYBILL_NUM nvarchar(16), WAYBILL_ALLDISCOUNT money,
			WAYBILL_TOTALPRICE money, WAYBILL_SALDO money,  WAYBILL_CURRENCYALLPRICE money,
			WAYBILL_CURRENCYRETALLPRICE money, WAYBILL_CURRENCYAMOUNTPAID money, WAYBILL_CURRENCYALLDISCOUNT money,
			WAYBILL_CURRENCYTOTALPRICE money, WAYBILL_CURRENCYSALDO money, CHILDCUST_ID int, WAYBILL_SHIPMODE int,
			WAYBILL_INCASSO int, WAYBILL_BONUS int, COMPANY_ID int, WAYBILL_OPERDATE date, WAYBILL_EXPORTMODE int,
			WAYBILL_REPRESENTATIVE nvarchar(48), WAYBILL_RETURN int, WAYBILL_SEND int, WAYBILL_USDRATE money,
			WAYBILL_MONEYBONUS int, WAYBILL_CURRENCYMAINRATE money, WAYBILL_FORSTOCK int, WAYBILL_DELIVERYADDRESS nvarchar(128),
			WAYBILL_DELIVERYDATE date, WAYBILL_AUTOCREATED int, WAYBILL_DRIVER nvarchar(128), WAYBILL_VEHICLE nvarchar(128),
			WAYBILL_CARRIER nvarchar(128), WAYBILL_ROUTESHEET nvarchar(128), ROUTESHEET_GUID nvarchar(36),
			WAYBILL_EXCLUDEFROM_ADJ int, WAYBILL_CALCSALDO money,  WAYBILL_CURRENCYCALCSALDO money
		);
		
		CREATE TABLE #WAYBITMS( WAYBITMS_ID INT, WAYBILL_ID INT, PARTS_ID INT, MEASURE_ID INT, WAYBITMS_QUANTITY INT,
			WAYBITMS_PACKID INT, WAYBITMS_PRICE money, WAYBITMS_DATELASTPAID DATE,  WAYBITMS_AMOUNTPAID money,
			WAYBITMS_BASEPRICE money, WAYBITMS_MARKUP money, WAYBITMS_DISCOUNT money, WAYBITMS_RETQTY INT, WAYBITMS_ALLPRICE money,
			WAYBITMS_DISCOUNTPRICE money, WAYBITMS_TOTALPRICE money, WAYBITMS_LEAVQTY INT,  WAYBITMS_LEAVTOTALPRICE money,
			WAYBITMS_CURRENCYPRICE money,  WAYBITMS_CURRENCYAMOUNTPAID money, WAYBITMS_CURRENCYALLPRICE money,
			WAYBITMS_CURRENCYDISCOUNTPRICE money, WAYBITMS_CURRENCYTOTALPRICE money, WAYBITMS_CURRENCYLEAVTOTALPRICE  money,
			WAYBITMS_NDS money, WAYBITMS_DISCOUNTFROMPRICE money, SPLITMS_ID int );

		--CREATE INDEX [IX_WAYBITMS_WAYBILL_ID] ON #WAYBITMS
		--(
		--	WAYBILL_ID ASC
		--);


    DECLARE @SQLString nvarchar( 4000 );
    DECLARE @ParmDefinition nvarchar( 500 );

		-- реквизиты накладной

		SELECT @SQLString = dbo.GetTextQueryForSelectFromInterbase( null, null, 
		'SELECT WAYBILL_ID, STOCK_ID, CUSTOMER_ID, DEPART_CODE,
			WAYBILL_SRCCODE, WAYBILL_SRCDOC, WAYBILL_SRCID,  WAYBILL_SRCDATE,
			WAYBILL_BEGINDATE, WAYBILL_ALLPRICE, WAYBILL_RETALLPRICE,
			WAYBILL_ENDDATE, CURRENCY_CODE, WAYBILL_CURRENCYRATE,
			WAYBILL_AMOUNTPAID, WAYBILL_DATELASTPAID, WAYBILL_SHIPDATE,
			WAYBILL_SHIPPED, WAYBILL_NUM, WAYBILL_ALLDISCOUNT,
			WAYBILL_TOTALPRICE, WAYBILL_SALDO,  WAYBILL_CURRENCYALLPRICE,
			WAYBILL_CURRENCYRETALLPRICE, WAYBILL_CURRENCYAMOUNTPAID, WAYBILL_CURRENCYALLDISCOUNT,
			WAYBILL_CURRENCYTOTALPRICE, WAYBILL_CURRENCYSALDO, CHILDCUST_ID, WAYBILL_SHIPMODE,
			WAYBILL_INCASSO, WAYBILL_BONUS, COMPANY_ID, WAYBILL_OPERDATE, WAYBILL_EXPORTMODE,
			WAYBILL_REPRESENTATIVE, WAYBILL_RETURN, WAYBILL_SEND, WAYBILL_USDRATE,
			WAYBILL_MONEYBONUS, WAYBILL_CURRENCYMAINRATE, WAYBILL_FORSTOCK, WAYBILL_DELIVERYADDRESS,
			WAYBILL_DELIVERYDATE, WAYBILL_AUTOCREATED, WAYBILL_DRIVER, WAYBILL_VEHICLE,
			WAYBILL_CARRIER, WAYBILL_ROUTESHEET, ROUTESHEET_GUID,
			WAYBILL_EXCLUDEFROM_ADJ, WAYBILL_CALCSALDO,  WAYBILL_CURRENCYCALCSALDO 
					FROM USP_GETWAYBILLINFO_BYID_FROMSQL( ' + CAST( @IN_WAYBILL_ID as varchar(20)) +  ' ) ' );
		SET @SQLString = 'INSERT INTO #WAYBILL( WAYBILL_ID, STOCK_ID, CUSTOMER_ID, DEPART_CODE,
			WAYBILL_SRCCODE, WAYBILL_SRCDOC, WAYBILL_SRCID,  WAYBILL_SRCDATE,
			WAYBILL_BEGINDATE, WAYBILL_ALLPRICE, WAYBILL_RETALLPRICE,
			WAYBILL_ENDDATE, CURRENCY_CODE, WAYBILL_CURRENCYRATE,
			WAYBILL_AMOUNTPAID, WAYBILL_DATELASTPAID, WAYBILL_SHIPDATE,
			WAYBILL_SHIPPED, WAYBILL_NUM, WAYBILL_ALLDISCOUNT,
			WAYBILL_TOTALPRICE, WAYBILL_SALDO,  WAYBILL_CURRENCYALLPRICE,
			WAYBILL_CURRENCYRETALLPRICE, WAYBILL_CURRENCYAMOUNTPAID, WAYBILL_CURRENCYALLDISCOUNT,
			WAYBILL_CURRENCYTOTALPRICE, WAYBILL_CURRENCYSALDO, CHILDCUST_ID, WAYBILL_SHIPMODE,
			WAYBILL_INCASSO, WAYBILL_BONUS, COMPANY_ID, WAYBILL_OPERDATE, WAYBILL_EXPORTMODE,
			WAYBILL_REPRESENTATIVE, WAYBILL_RETURN, WAYBILL_SEND, WAYBILL_USDRATE,
			WAYBILL_MONEYBONUS, WAYBILL_CURRENCYMAINRATE, WAYBILL_FORSTOCK, WAYBILL_DELIVERYADDRESS,
			WAYBILL_DELIVERYDATE, WAYBILL_AUTOCREATED, WAYBILL_DRIVER, WAYBILL_VEHICLE,
			WAYBILL_CARRIER, WAYBILL_ROUTESHEET, ROUTESHEET_GUID,
			WAYBILL_EXCLUDEFROM_ADJ, WAYBILL_CALCSALDO,  WAYBILL_CURRENCYCALCSALDO ) ' + @SQLString;

		--PRINT @SQLString;

		EXECUTE SP_EXECUTESQL @SQLString;

		-- табличная часть накладной

		SELECT @SQLString = dbo.GetTextQueryForSelectFromInterbase( null, null, 
		'SELECT WAYBITMS_ID, WAYBILL_ID, PARTS_ID, MEASURE_ID, WAYBITMS_QUANTITY,
			WAYBITMS_PACKID, WAYBITMS_PRICE, WAYBITMS_DATELASTPAID,  WAYBITMS_AMOUNTPAID,
			WAYBITMS_BASEPRICE, WAYBITMS_MARKUP, WAYBITMS_DISCOUNT, WAYBITMS_RETQTY, WAYBITMS_ALLPRICE,
			WAYBITMS_DISCOUNTPRICE, WAYBITMS_TOTALPRICE, WAYBITMS_LEAVQTY,  WAYBITMS_LEAVTOTALPRICE,
			WAYBITMS_CURRENCYPRICE,  WAYBITMS_CURRENCYAMOUNTPAID, WAYBITMS_CURRENCYALLPRICE,
			WAYBITMS_CURRENCYDISCOUNTPRICE, WAYBITMS_CURRENCYTOTALPRICE, WAYBITMS_CURRENCYLEAVTOTALPRICE,
			WAYBITMS_NDS, WAYBITMS_DISCOUNTFROMPRICE, SPLITMS_ID 
					FROM USP_GETWAYBITMSINFO_BYID_FROMSQL( ' + CAST( @IN_WAYBILL_ID as varchar(20)) +  ' ) ' );
		SET @SQLString = 'INSERT INTO #WAYBITMS( WAYBITMS_ID, WAYBILL_ID, PARTS_ID, MEASURE_ID, WAYBITMS_QUANTITY,
			WAYBITMS_PACKID, WAYBITMS_PRICE, WAYBITMS_DATELASTPAID,  WAYBITMS_AMOUNTPAID,
			WAYBITMS_BASEPRICE, WAYBITMS_MARKUP, WAYBITMS_DISCOUNT, WAYBITMS_RETQTY, WAYBITMS_ALLPRICE,
			WAYBITMS_DISCOUNTPRICE, WAYBITMS_TOTALPRICE, WAYBITMS_LEAVQTY,  WAYBITMS_LEAVTOTALPRICE,
			WAYBITMS_CURRENCYPRICE,  WAYBITMS_CURRENCYAMOUNTPAID, WAYBITMS_CURRENCYALLPRICE,
			WAYBITMS_CURRENCYDISCOUNTPRICE, WAYBITMS_CURRENCYTOTALPRICE, WAYBITMS_CURRENCYLEAVTOTALPRICE,
			WAYBITMS_NDS, WAYBITMS_DISCOUNTFROMPRICE, SPLITMS_ID ) ' + @SQLString;

		--PRINT @SQLString;

		EXECUTE SP_EXECUTESQL @SQLString;

		DECLARE @WAYBILL_ID int; DECLARE @STOCK_ID int; DECLARE @CUSTOMER_ID int; DECLARE @DEPART_CODE nvarchar(3);
		DECLARE @WAYBILL_SRCCODE int; DECLARE @WAYBILL_SRCDOC nvarchar(16); DECLARE @WAYBILL_SRCID int;  DECLARE @WAYBILL_SRCDATE date;
		DECLARE @WAYBILL_BEGINDATE date; DECLARE @WAYBILL_ALLPRICE money; DECLARE @WAYBILL_RETALLPRICE money;
		DECLARE @WAYBILL_ENDDATE date; DECLARE @CURRENCY_CODE nvarchar(3); DECLARE @WAYBILL_CURRENCYRATE money;
		DECLARE @WAYBILL_AMOUNTPAID money; DECLARE @WAYBILL_DATELASTPAID date; DECLARE @WAYBILL_SHIPDATE date;
		DECLARE @WAYBILL_SHIPPED int; DECLARE @WAYBILL_NUM nvarchar(16); DECLARE @WAYBILL_ALLDISCOUNT money;
		DECLARE @WAYBILL_TOTALPRICE money; DECLARE @WAYBILL_SALDO money;  DECLARE @WAYBILL_CURRENCYALLPRICE money;
		DECLARE @WAYBILL_CURRENCYRETALLPRICE money; DECLARE @WAYBILL_CURRENCYAMOUNTPAID money; DECLARE @WAYBILL_CURRENCYALLDISCOUNT money;
		DECLARE @WAYBILL_CURRENCYTOTALPRICE money; DECLARE @WAYBILL_CURRENCYSALDO money; DECLARE @CHILDCUST_ID int; DECLARE @WAYBILL_SHIPMODE int;
		DECLARE @WAYBILL_INCASSO int; DECLARE @WAYBILL_BONUS int; DECLARE @COMPANY_ID int; DECLARE @WAYBILL_OPERDATE date; DECLARE @WAYBILL_EXPORTMODE int;
		DECLARE @WAYBILL_REPRESENTATIVE nvarchar(48); DECLARE @WAYBILL_RETURN int; DECLARE @WAYBILL_SEND int; DECLARE @WAYBILL_USDRATE money;
		DECLARE @WAYBILL_MONEYBONUS int; DECLARE @WAYBILL_CURRENCYMAINRATE money; DECLARE @WAYBILL_FORSTOCK int; DECLARE @WAYBILL_DELIVERYADDRESS nvarchar(128);
		DECLARE @WAYBILL_DELIVERYDATE date; DECLARE @WAYBILL_AUTOCREATED int; DECLARE @WAYBILL_DRIVER nvarchar(128); DECLARE @WAYBILL_VEHICLE nvarchar(128);
		DECLARE @WAYBILL_CARRIER nvarchar(128); DECLARE @WAYBILL_ROUTESHEET nvarchar(128); DECLARE @ROUTESHEET_GUID nvarchar(36);
		DECLARE @WAYBILL_EXCLUDEFROM_ADJ int; DECLARE @WAYBILL_CALCSALDO money; DECLARE @WAYBILL_CURRENCYCALCSALDO money;

		DECLARE @WAYBITMS_ID INT; DECLARE @PARTS_ID INT; DECLARE @MEASURE_ID INT; DECLARE @WAYBITMS_QUANTITY INT;
			DECLARE @WAYBITMS_PACKID INT; DECLARE @WAYBITMS_PRICE money; DECLARE @WAYBITMS_DATELASTPAID DATE;  DECLARE @WAYBITMS_AMOUNTPAID money;
			DECLARE @WAYBITMS_BASEPRICE money; DECLARE @WAYBITMS_MARKUP money; DECLARE @WAYBITMS_DISCOUNT money; 
			DECLARE @WAYBITMS_RETQTY INT; DECLARE @WAYBITMS_ALLPRICE money;	DECLARE @WAYBITMS_DISCOUNTPRICE money; 
			DECLARE @WAYBITMS_TOTALPRICE money; DECLARE @WAYBITMS_LEAVQTY INT; DECLARE @WAYBITMS_LEAVTOTALPRICE money;
			DECLARE @WAYBITMS_CURRENCYPRICE money; DECLARE @WAYBITMS_CURRENCYAMOUNTPAID money; DECLARE @WAYBITMS_CURRENCYALLPRICE money;
			DECLARE @WAYBITMS_CURRENCYDISCOUNTPRICE money; DECLARE @WAYBITMS_CURRENCYTOTALPRICE money; DECLARE @WAYBITMS_CURRENCYLEAVTOTALPRICE  money;
			DECLARE @WAYBITMS_NDS money; DECLARE @WAYBITMS_DISCOUNTFROMPRICE money; DECLARE @SPLITMS_ID int;

		DECLARE @Waybill_Guid		D_GUID;
		DECLARE @Suppl_Guid			D_GUID;
		DECLARE @SupplSrc_Guid	D_GUID;
		DECLARE @Stock_Guid			D_GUID;
		DECLARE @Company_Guid		D_GUID;
		DECLARE @Currency_Guid	D_GUID;
		DECLARE @Depart_Guid		D_GUID;
		DECLARE @Customer_Guid	D_GUID;
		DECLARE @CustomerChild_Guid	D_GUID;
		DECLARE @Rtt_Guid				D_GUID;
		DECLARE @Address_Guid		D_GUID;
		DECLARE @PaymentType_Guid		D_GUID;
		DECLARE @WaybillParent_Guid	D_GUID;
		DECLARE @WaybillState_Guid	D_GUID;
		DECLARE @WaybillShipMode_Guid	D_GUID;
		DECLARE @Waybill_Quantity			D_QUANTITY;
		DECLARE @Waybill_RetQuantity	D_QUANTITY;
		DECLARE @Waybill_Weight				D_WEIGHT;
		DECLARE @Waybill_ShowInDeliveryList		D_YESNO;
		DECLARE @NationalCurrencyCode  D_CURRENCYCODE;

		DECLARE @SupplItem_Guid	D_GUID;
		DECLARE @Parts_Guid			D_GUID;
		DECLARE @Measure_Guid		D_GUID;
		DECLARE @Parts_Weight		D_WEIGHT;
		
		SET @NationalCurrencyCode = ( SELECT dbo.GetNationalCurrencyAbbr() );

		-- открываем курсор с записями из таблицы реквизитов накладной
		DECLARE crUpdate CURSOR FOR SELECT WAYBILL_ID, STOCK_ID, CUSTOMER_ID, DEPART_CODE,
			WAYBILL_SRCCODE, WAYBILL_SRCDOC, WAYBILL_SRCID,  WAYBILL_SRCDATE,
			WAYBILL_BEGINDATE, WAYBILL_ALLPRICE, WAYBILL_RETALLPRICE,
			WAYBILL_ENDDATE, CURRENCY_CODE, WAYBILL_CURRENCYRATE,
			WAYBILL_AMOUNTPAID, WAYBILL_DATELASTPAID, WAYBILL_SHIPDATE,
			WAYBILL_SHIPPED, WAYBILL_NUM, WAYBILL_ALLDISCOUNT,
			WAYBILL_TOTALPRICE, WAYBILL_SALDO,  WAYBILL_CURRENCYALLPRICE,
			WAYBILL_CURRENCYRETALLPRICE, WAYBILL_CURRENCYAMOUNTPAID, WAYBILL_CURRENCYALLDISCOUNT,
			WAYBILL_CURRENCYTOTALPRICE, WAYBILL_CURRENCYSALDO, CHILDCUST_ID, WAYBILL_SHIPMODE,
			WAYBILL_INCASSO, WAYBILL_BONUS, COMPANY_ID, WAYBILL_OPERDATE, WAYBILL_EXPORTMODE,
			WAYBILL_REPRESENTATIVE, WAYBILL_RETURN, WAYBILL_SEND, WAYBILL_USDRATE,
			WAYBILL_MONEYBONUS, WAYBILL_CURRENCYMAINRATE, WAYBILL_FORSTOCK, WAYBILL_DELIVERYADDRESS,
			WAYBILL_DELIVERYDATE, WAYBILL_AUTOCREATED, WAYBILL_DRIVER, WAYBILL_VEHICLE,
			WAYBILL_CARRIER, WAYBILL_ROUTESHEET, ROUTESHEET_GUID,
			WAYBILL_EXCLUDEFROM_ADJ, WAYBILL_CALCSALDO,  WAYBILL_CURRENCYCALCSALDO
		FROM #WAYBILL WHERE ROUTESHEET_GUID IS NOT NULL;
		OPEN crUpdate;
		fetch next from crUpdate into @WAYBILL_ID, @STOCK_ID, @CUSTOMER_ID, @DEPART_CODE,
			@WAYBILL_SRCCODE, @WAYBILL_SRCDOC, @WAYBILL_SRCID,  @WAYBILL_SRCDATE,
			@WAYBILL_BEGINDATE, @WAYBILL_ALLPRICE, @WAYBILL_RETALLPRICE,
			@WAYBILL_ENDDATE, @CURRENCY_CODE, @WAYBILL_CURRENCYRATE,
			@WAYBILL_AMOUNTPAID, @WAYBILL_DATELASTPAID, @WAYBILL_SHIPDATE,
			@WAYBILL_SHIPPED, @WAYBILL_NUM, @WAYBILL_ALLDISCOUNT,
			@WAYBILL_TOTALPRICE, @WAYBILL_SALDO,  @WAYBILL_CURRENCYALLPRICE,
			@WAYBILL_CURRENCYRETALLPRICE, @WAYBILL_CURRENCYAMOUNTPAID, @WAYBILL_CURRENCYALLDISCOUNT,
			@WAYBILL_CURRENCYTOTALPRICE, @WAYBILL_CURRENCYSALDO, @CHILDCUST_ID, @WAYBILL_SHIPMODE,
			@WAYBILL_INCASSO, @WAYBILL_BONUS, @COMPANY_ID, @WAYBILL_OPERDATE, @WAYBILL_EXPORTMODE,
			@WAYBILL_REPRESENTATIVE, @WAYBILL_RETURN, @WAYBILL_SEND, @WAYBILL_USDRATE,
			@WAYBILL_MONEYBONUS, @WAYBILL_CURRENCYMAINRATE, @WAYBILL_FORSTOCK, @WAYBILL_DELIVERYADDRESS,
			@WAYBILL_DELIVERYDATE, @WAYBILL_AUTOCREATED, @WAYBILL_DRIVER, @WAYBILL_VEHICLE,
			@WAYBILL_CARRIER, @WAYBILL_ROUTESHEET, @ROUTESHEET_GUID,
			@WAYBILL_EXCLUDEFROM_ADJ, @WAYBILL_CALCSALDO,  @WAYBILL_CURRENCYCALCSALDO;
		while @@fetch_status = 0
			begin
				SET @Waybill_Guid = NULL;
				SELECT @Waybill_Guid = [Waybill_Guid] FROM [dbo].[T_Waybill]
				WHERE [Waybill_Id] IS NOT NULL AND [Waybill_Id] = @WAYBILL_ID;

				SET @Suppl_Guid = NULL;
				SET @WaybillParent_Guid = NULL;
				SET @Waybill_Quantity = 0;
				SET @Waybill_RetQuantity= 0;
				SET @Waybill_Weight = 0;
				SET @Waybill_ShowInDeliveryList = 0;
				SET @Rtt_Guid = NULL;
				SET @Address_Guid = NULL;
				SET @Suppl_Guid = NULL;
				SET @SupplSrc_Guid = NULL;

				IF( @CURRENCY_CODE = @NationalCurrencyCode )
					SET @PaymentType_Guid = ( SELECT dbo.GetPaymentType_1_Guid() );
				ELSE
					SET @PaymentType_Guid = ( SELECT dbo.GetPaymentType_2_Guid() );

				SELECT @Stock_Guid = [Stock_Guid] FROM [dbo].[T_Stock] WHERE [Stock_Id] = @STOCK_ID;
				SELECT @Company_Guid = [Company_Guid] FROM [dbo].[T_Company] WHERE [Company_Id] = @COMPANY_ID;
				SELECT @Currency_Guid = [Currency_Guid] FROM [dbo].[T_Currency] WHERE [Currency_Abbr] = @CURRENCY_CODE;
				SELECT @Depart_Guid = [Depart_Guid] FROM [dbo].[T_Depart] WHERE [Depart_Code] = @DEPART_CODE;
				SELECT @Customer_Guid = [Customer_Guid] FROM [dbo].[T_Customer] WHERE [Customer_Id] = @CUSTOMER_ID;
				SELECT @WaybillState_Guid = [WaybillState_Guid] FROM [dbo].[T_WaybillState] WHERE [WaybillState_Id] = @WAYBILL_SHIPPED;
				SELECT @WaybillShipMode_Guid = [WaybillShipMode_Guid] FROM [dbo].[T_WaybillShipMode] WHERE [WaybillShipMode_Id] = @WAYBILL_SHIPMODE;

				SET @CustomerChild_Guid = NULL;
				IF( ( @CHILDCUST_ID IS NOT NULL ) AND ( @CHILDCUST_ID <> 0 ) )
					SELECT @CustomerChild_Guid = [CustomerChild_Guid] FROM [dbo].[T_CustomerChild] WHERE [CustomerChild_Id] = @CHILDCUST_ID;
				
				SET @Rtt_Guid	= NULL;
				SET @Address_Guid	= NULL;

				IF( @ROUTESHEET_GUID IS NOT NULL )
					BEGIN
						SELECT @Suppl_Guid = [Suppl_Guid] FROM [dbo].[T_RouteSheetSuppl] WHERE [Suppl_Id] = @WAYBILL_SRCID;
						
						IF( @Suppl_Guid IS NOT NULL )
							SELECT @SupplSrc_Guid = Suppl_Guid FROM T_Suppl WHERE Suppl_Guid = @Suppl_Guid;

						IF( @SupplSrc_Guid IS NOT NULL )
							SELECT TOP 1 @Rtt_Guid = [Rtt_Guid] FROM [dbo].[T_RouteSheetSuppl] 
							WHERE [RouteSheet_Guid] = @ROUTESHEET_GUID AND [Suppl_Guid] = @SupplSrc_Guid;

						IF( @Rtt_Guid IS NOT NULL )
							SELECT TOP 1 @Address_Guid = [Address_Guid] FROM [dbo].[T_RttAddress] WHERE [Rtt_Guid] = @Rtt_Guid;
					END 
				
				IF( ( @Suppl_Guid IS NOT NULL ) AND ( @Rtt_Guid IS NOT NULL ) )
					BEGIN

						IF( @Waybill_Guid IS NULL )
							BEGIN
								SET @Waybill_Guid = NEWID();

								INSERT INTO T_Waybill( Waybill_Guid, Waybill_Id, Suppl_Guid, Stock_Guid, Company_Guid, 
									Currency_Guid, Depart_Guid, Customer_Guid, CustomerChild_Guid, Rtt_Guid, Address_Guid, 
									PaymentType_Guid, Waybill_Num, Waybill_BeginDate, Waybill_DeliveryDate, 
									WaybillParent_Guid, Waybill_Bonus, WaybillState_Guid, WaybillShipMode_Guid, 
									Waybill_ShipDate,  Waybill_CurrencyRate, Waybill_AllPrice, Waybill_RetAllPrice, Waybill_AllDiscount, 
									Waybill_AmountPaid, Waybill_CurrencyAllPrice, Waybill_CurrencyRetAllPrice, Waybill_CurrencyAllDiscount, 
									Waybill_CurrencyAmountPaid, Waybill_Quantity, Waybill_RetQuantity, Waybill_Weight, Waybill_ShowInDeliveryList )
								VALUES( @Waybill_Guid, @WAYBILL_ID, @SupplSrc_Guid, @Stock_Guid, @Company_Guid, 
									@Currency_Guid, @Depart_Guid, @Customer_Guid, @CustomerChild_Guid, @Rtt_Guid, @Address_Guid, 
									@PaymentType_Guid, @WAYBILL_NUM, @WAYBILL_BEGINDATE, @WAYBILL_DELIVERYDATE, 
									@WaybillParent_Guid, @WAYBILL_BONUS, @WaybillState_Guid, @WaybillShipMode_Guid, 
									@Waybill_ShipDate, @WAYBILL_CURRENCYRATE, @WAYBILL_ALLPRICE, @WAYBILL_RETALLPRICE, @WAYBILL_ALLDISCOUNT, 
									@WAYBILL_AMOUNTPAID, @WAYBILL_CURRENCYALLPRICE, @WAYBILL_CURRENCYRETALLPRICE, @WAYBILL_CURRENCYALLDISCOUNT, 
									@WAYBILL_CURRENCYAMOUNTPAID, @Waybill_Quantity, @Waybill_RetQuantity, @Waybill_Weight, @Waybill_ShowInDeliveryList );
							END
						ELSE
							BEGIN
								UPDATE T_Waybill SET WaybillState_Guid = @WaybillState_Guid, WaybillShipMode_Guid = @WaybillShipMode_Guid, 
									Waybill_ShipDate =  @Waybill_ShipDate, Waybill_CurrencyRate =  @WAYBILL_CURRENCYRATE, 
									Waybill_AllPrice = @WAYBILL_ALLPRICE, Waybill_RetAllPrice = @WAYBILL_RETALLPRICE, 
									Waybill_AllDiscount = @WAYBILL_ALLDISCOUNT, Waybill_AmountPaid = @WAYBILL_AMOUNTPAID, 
									Waybill_CurrencyAllPrice = @WAYBILL_CURRENCYALLPRICE, Waybill_CurrencyRetAllPrice = @WAYBILL_CURRENCYRETALLPRICE, 
									Waybill_CurrencyAllDiscount = @WAYBILL_CURRENCYALLDISCOUNT, Waybill_CurrencyAmountPaid = @WAYBILL_CURRENCYAMOUNTPAID
								WHERE Waybill_Guid = @Waybill_Guid;
							END

						DELETE FROM [dbo].[T_WaybItem] WHERE [Waybill_Guid] = @Waybill_Guid;

						DECLARE crUpdateItem CURSOR FOR SELECT WAYBITMS_ID, PARTS_ID, MEASURE_ID, WAYBITMS_QUANTITY,
							WAYBITMS_PACKID, WAYBITMS_PRICE, WAYBITMS_DATELASTPAID,  WAYBITMS_AMOUNTPAID,
							WAYBITMS_BASEPRICE, WAYBITMS_MARKUP, WAYBITMS_DISCOUNT, WAYBITMS_RETQTY, WAYBITMS_ALLPRICE,
							WAYBITMS_DISCOUNTPRICE, WAYBITMS_TOTALPRICE, WAYBITMS_LEAVQTY,  WAYBITMS_LEAVTOTALPRICE,
							WAYBITMS_CURRENCYPRICE,  WAYBITMS_CURRENCYAMOUNTPAID, WAYBITMS_CURRENCYALLPRICE,
							WAYBITMS_CURRENCYDISCOUNTPRICE, WAYBITMS_CURRENCYTOTALPRICE, WAYBITMS_CURRENCYLEAVTOTALPRICE,
							WAYBITMS_NDS, WAYBITMS_DISCOUNTFROMPRICE, SPLITMS_ID
						FROM #WAYBITMS WHERE WAYBILL_ID = @WAYBILL_ID;
						OPEN crUpdateItem;
						fetch next from crUpdateItem into @WAYBITMS_ID, @PARTS_ID, @MEASURE_ID, @WAYBITMS_QUANTITY,
							@WAYBITMS_PACKID, @WAYBITMS_PRICE, @WAYBITMS_DATELASTPAID,  @WAYBITMS_AMOUNTPAID,
							@WAYBITMS_BASEPRICE, @WAYBITMS_MARKUP, @WAYBITMS_DISCOUNT, @WAYBITMS_RETQTY, @WAYBITMS_ALLPRICE,
							@WAYBITMS_DISCOUNTPRICE, @WAYBITMS_TOTALPRICE, @WAYBITMS_LEAVQTY, @WAYBITMS_LEAVTOTALPRICE,
							@WAYBITMS_CURRENCYPRICE, @WAYBITMS_CURRENCYAMOUNTPAID, @WAYBITMS_CURRENCYALLPRICE,
							@WAYBITMS_CURRENCYDISCOUNTPRICE, @WAYBITMS_CURRENCYTOTALPRICE, @WAYBITMS_CURRENCYLEAVTOTALPRICE,
							@WAYBITMS_NDS, @WAYBITMS_DISCOUNTFROMPRICE, @SPLITMS_ID;
						while @@fetch_status = 0
						begin

							SET @SupplItem_Guid	= NULL;
							SET @Parts_Guid	= NULL;
							SET @Measure_Guid	= NULL;

							SELECT @Parts_Guid = [Parts_Guid] FROM [dbo].[T_Parts] WHERE [Parts_Id] = @PARTS_ID;
							SELECT TOP 1 @Measure_Guid = [Measure_Guid] FROM [dbo].[T_Measure];
							SELECT @SupplItem_Guid = [SupplItem_Guid] FROM [dbo].[T_SupplItem] WHERE [Splitms_Id] = @SPLITMS_ID;

							INSERT INTO [dbo].[T_WaybItem]( WaybItem_Guid, SupplItem_Guid, Waybill_Guid, Parts_Guid, Measure_Guid, 
								WaybItem_Quantity, WaybItem_RetQuantity, WaybItem_Price, WaybItem_Discount, WaybItem_DiscountPrice, 
								WaybItem_CurrencyPrice, WaybItem_CurrencyDiscountPrice, 
								WaybItem_NDSPercent, WaybItem_PriceImporter )
							VALUES( NEWID(), @SupplItem_Guid, @Waybill_Guid, @Parts_Guid, @Measure_Guid, 
								@WAYBITMS_QUANTITY, @WAYBITMS_RETQTY, @WAYBITMS_PRICE,  @WAYBITMS_DISCOUNTFROMPRICE, @WAYBITMS_DISCOUNTPRICE, 
								@WAYBITMS_CURRENCYPRICE, @WAYBITMS_CURRENCYDISCOUNTPRICE, 
								@WAYBITMS_NDS, @WAYBITMS_BASEPRICE );

							fetch next from crUpdateItem into @WAYBITMS_ID, @PARTS_ID, @MEASURE_ID, @WAYBITMS_QUANTITY,
										@WAYBITMS_PACKID, @WAYBITMS_PRICE, @WAYBITMS_DATELASTPAID,  @WAYBITMS_AMOUNTPAID,
										@WAYBITMS_BASEPRICE, @WAYBITMS_MARKUP, @WAYBITMS_DISCOUNT, @WAYBITMS_RETQTY, @WAYBITMS_ALLPRICE,
										@WAYBITMS_DISCOUNTPRICE, @WAYBITMS_TOTALPRICE, @WAYBITMS_LEAVQTY, @WAYBITMS_LEAVTOTALPRICE,
										@WAYBITMS_CURRENCYPRICE, @WAYBITMS_CURRENCYAMOUNTPAID, @WAYBITMS_CURRENCYALLPRICE,
										@WAYBITMS_CURRENCYDISCOUNTPRICE, @WAYBITMS_CURRENCYTOTALPRICE, @WAYBITMS_CURRENCYLEAVTOTALPRICE,
										@WAYBITMS_NDS, @WAYBITMS_DISCOUNTFROMPRICE, @SPLITMS_ID;
						end -- while @@fetch_status = 0

						close crUpdateItem;
						deallocate crUpdateItem;

						SET @Waybill_Quantity = 0;
						SET @Waybill_RetQuantity= 0;
						SET @Waybill_Weight = 0;

						SELECT @Waybill_Quantity = SUM( [WaybItem_Quantity] ), @Waybill_RetQuantity = SUM( [WaybItem_RetQuantity] ), 
							@Waybill_Weight = SUM( [WaybItem_Quantity] * Parts.Parts_Weight  ) 
						FROM [dbo].[T_WaybItem] AS WaybItem INNER JOIN T_Parts AS Parts ON WaybItem.Parts_Guid = Parts.Parts_Guid
						WHERE WaybItem.Waybill_Guid = @Waybill_Guid;

						UPDATE [dbo].[T_Waybill] SET [Waybill_Quantity] = @Waybill_Quantity, 
							[Waybill_RetQuantity] = @Waybill_RetQuantity, [Waybill_Weight] = @Waybill_Weight
						WHERE Waybill_Guid = @Waybill_Guid
							AND ( @Waybill_Quantity IS NOT NULL )
							AND ( @Waybill_RetQuantity IS NOT NULL )
							AND ( @Waybill_Weight IS NOT NULL )
							;

					END

				fetch next from crUpdate into @WAYBILL_ID, @STOCK_ID, @CUSTOMER_ID, @DEPART_CODE,
					@WAYBILL_SRCCODE, @WAYBILL_SRCDOC, @WAYBILL_SRCID,  @WAYBILL_SRCDATE,
					@WAYBILL_BEGINDATE, @WAYBILL_ALLPRICE, @WAYBILL_RETALLPRICE,
					@WAYBILL_ENDDATE, @CURRENCY_CODE, @WAYBILL_CURRENCYRATE,
					@WAYBILL_AMOUNTPAID, @WAYBILL_DATELASTPAID, @WAYBILL_SHIPDATE,
					@WAYBILL_SHIPPED, @WAYBILL_NUM, @WAYBILL_ALLDISCOUNT,
					@WAYBILL_TOTALPRICE, @WAYBILL_SALDO,  @WAYBILL_CURRENCYALLPRICE,
					@WAYBILL_CURRENCYRETALLPRICE, @WAYBILL_CURRENCYAMOUNTPAID, @WAYBILL_CURRENCYALLDISCOUNT,
					@WAYBILL_CURRENCYTOTALPRICE, @WAYBILL_CURRENCYSALDO, @CHILDCUST_ID, @WAYBILL_SHIPMODE,
					@WAYBILL_INCASSO, @WAYBILL_BONUS, @COMPANY_ID, @WAYBILL_OPERDATE, @WAYBILL_EXPORTMODE,
					@WAYBILL_REPRESENTATIVE, @WAYBILL_RETURN, @WAYBILL_SEND, @WAYBILL_USDRATE,
					@WAYBILL_MONEYBONUS, @WAYBILL_CURRENCYMAINRATE, @WAYBILL_FORSTOCK, @WAYBILL_DELIVERYADDRESS,
					@WAYBILL_DELIVERYDATE, @WAYBILL_AUTOCREATED, @WAYBILL_DRIVER, @WAYBILL_VEHICLE,
					@WAYBILL_CARRIER, @WAYBILL_ROUTESHEET, @ROUTESHEET_GUID,
					@WAYBILL_EXCLUDEFROM_ADJ, @WAYBILL_CALCSALDO,  @WAYBILL_CURRENCYCALCSALDO;
			end -- while @@fetch_status = 0

		close crUpdate;
		deallocate crUpdate;


		--SELECT * FROM #WAYBILL;
		--SELECT * FROM #WAYBITMS;

		--DROP INDEX [IX_WAYBITMS_WAYBILL_ID] ON #WAYBITMS;
		DROP TABLE #WAYBITMS;
		DROP TABLE #WAYBILL;

 	END TRY
	BEGIN CATCH
    SET @ERROR_NUM = ERROR_NUMBER();
    SET @ERROR_MES = ERROR_MESSAGE();

		RETURN @ERROR_NUM;
	END CATCH;

	RETURN @ERROR_NUM;

END

GO
GRANT EXECUTE ON [dbo].[usp_SynchOneWaybillInfoFromIB] TO [public]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Регистрирует оплату задолженности в InterBase

-- Входные параметры
-- 
--		@Suppl_Guid						- УИ заказа
--		@Document_Date				- дата накладной
--		@Document_Num					- номер накладной
--		@DocumentSendToStock	- признак "для склада"
--		@IBLINKEDSERVERNAME		- имя LINKEDSERVER для подключения к InterBase
--
-- Выходные параметры
--
--		@Waybill_Guid					- УИ накладной
--		@ERROR_NUM						- номер ошибки
--		@ERROR_MES						- сообщение об ошибке

CREATE PROCEDURE [dbo].[usp_AddWaybillFromSupplInIB]
	@Suppl_Guid						D_GUID,
	@Document_Date				D_DATE,
	@Document_Num					D_NAME,
	@DocumentSendToStock	bit,
	@IBLINKEDSERVERNAME		D_NAME = NULL,

  @Waybill_Guid					uniqueidentifier output,
	@ERROR_NUM						int output,
	@ERROR_MES						nvarchar(4000) output
AS

BEGIN
  BEGIN TRY
    SET @ERROR_NUM = 0;
    SET @ERROR_MES = '';
		SET @Waybill_Guid = NULL;

		IF NOT EXISTS ( SELECT Suppl_Guid FROM dbo.[T_Suppl] WHERE Suppl_Guid = @Suppl_Guid )
			BEGIN
				SET @ERROR_NUM = 1;
				SET @ERROR_MES = 'В базе данных не найден протокол с указанным идентификатором: ' + CAST( @Suppl_Guid as nvarchar(36) );

				RETURN @ERROR_NUM;
			END       
		
    DECLARE @SUPPL_ID integer;
    DECLARE @WAYBILL_NUM nvarchar(16);
    DECLARE @WAYBILL_FORSTOCK int;
		DECLARE @WAYBILL_ID int;

		DECLARE @CARDS_SHIPDATE varchar( 24 );
		IF( @Document_Date IS NULL ) SET @CARDS_SHIPDATE = 'NULL'
		ELSE 
			BEGIN
				SET @CARDS_SHIPDATE = convert( varchar( 10), @Document_Date, 104);
				SET @CARDS_SHIPDATE = '''''' + @CARDS_SHIPDATE + '''''';
			END	

		SET @WAYBILL_NUM = SUBSTRING( @Document_Num, 1, 16 );
		SET @WAYBILL_FORSTOCK = CAST( @DocumentSendToStock AS int );
		SELECT @SUPPL_ID = Suppl_Id FROM dbo.[T_Suppl] WHERE Suppl_Guid = @Suppl_Guid;

    DECLARE @EventID D_ID = NULL;
    DECLARE @ParentEventID D_ID = NULL;
    DECLARE @strMessage D_EVENTMSG = '';
    DECLARE @EventSrc D_NAME = 'Перевод заказа в накладную в IB';

	  IF( @IBLINKEDSERVERNAME IS NULL ) SELECT @IBLINKEDSERVERNAME = dbo.GetIBLinkedServerName();
	  
    DECLARE @SQLString nvarchar( 2048 );
    DECLARE @ParmDefinition nvarchar( 500 );

    SET @ParmDefinition = N' @WAYBILL_ID_Ib int output, @ErrorNumber_Ib int output, @ErrorText_Ib varchar(480) output'; 				

		SET @SQLString = 'SELECT @WAYBILL_ID_Ib = WAYBILL_ID, @ErrorNumber_Ib = ERROR_NUMBER, @ErrorText_Ib = ERROR_TEXT FROM OPENQUERY( ' + 
			@IBLINKEDSERVERNAME + ', ''SELECT WAYBILL_ID, ERROR_NUMBER, ERROR_TEXT FROM USP_ADD_WAYBILL_FROMSQL( ' + cast( @SUPPL_ID as nvarchar( 10 )) + ', ' + +
					@CARDS_SHIPDATE +  ', ''''' + @WAYBILL_NUM + ''''', ' +	cast( @WAYBILL_FORSTOCK as nvarchar(3)) + ' )'' )'; 

    EXECUTE sp_executesql @SQLString, @ParmDefinition, @WAYBILL_ID_Ib = @WAYBILL_ID output, 
			@ErrorNumber_Ib = @ERROR_NUM output, @ErrorText_Ib = @ERROR_MES output;  

		IF( ( @WAYBILL_ID IS NOT NULL ) AND ( @ERROR_NUM = 0 ) )
			BEGIN
				EXEC dbo.usp_SynchOneWaybillInfoFromIB	@IN_WAYBILL_ID	= @WAYBILL_ID, @ERROR_NUM	= @ERROR_NUM output, @ERROR_MES = @ERROR_MES output;

				IF( @ERROR_NUM = 0 )
					SELECT @Waybill_Guid = Waybill_Guid FROM [dbo].[T_Waybill] 
					WHERE ( [Waybill_Id] IS NOT NULL ) AND ( [Waybill_Id] = @WAYBILL_ID );
			END
		
	END TRY
	
	BEGIN CATCH
    SET @ERROR_NUM = ERROR_NUMBER();
    SET @ERROR_MES = @ERROR_MES + ' ' + ERROR_MESSAGE();
    
		EXEC dbo.spAddEventToLog @EVENT_SOURCE = @EventSrc, @EVENT_CATEGORY = 'None', 
      @EVENT_COMPUTER = ' ', @EVENT_TYPE = 'Error', @EVENT_IS_COMPOSITE = 0, 
      @EVENT_DESCRIPTION = @strMessage, @EVENT_PARENTID = @ParentEventID, @EVENT_ID = @EventID output;

		RETURN @ERROR_NUM;
	END CATCH;

	IF( @ERROR_NUM = 0 )
		SET @ERROR_MES = 'Успешное завершение операции.';

	RETURN @ERROR_NUM;

END

GO
GRANT EXECUTE ON [dbo].[usp_AddWaybillFromSupplInIB] TO [public]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Возвращает запись из ( [dbo].[T_Waybill] )
--
-- Входные параметры:
--
--		@Waybill_Guid					- УИ накладной
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

CREATE PROCEDURE [dbo].[usp_GetWaybill] 
  @Waybill_Guid					D_GUID,
	
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
			SELECT Waybill_Guid FROM [dbo].[T_Waybill] WHERE [Waybill_Guid] = @Waybill_Guid
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
		FROM #tmpWaybillList;

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
GRANT EXECUTE ON [dbo].[usp_GetWaybill] TO [public]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Возвращает УИ накладной по заказу
--
-- Входные параметры:
--
--		@Suppl_Guid				УИ заказа
--
-- Выходные параметры:
--
--  @ERROR_NUM					код ошибки
--  @ERROR_MES					текст ошибки
--
-- Результат:
--		0 - Успешное завершение
--		<>0 - ошибка
--

CREATE PROCEDURE [dbo].[usp_GetSupplWaybillGuid] 
	@Suppl_Guid		D_GUID,
	
	@Waybill_Guid	D_GUID output,
  @ERROR_NUM		D_ID output,
  @ERROR_MES		D_ERROR_MESSAGE output
AS

BEGIN

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = '';

  BEGIN TRY

    SELECT @Waybill_Guid = [Waybill_Guid] FROM [dbo].[T_Waybill] 
		WHERE [Suppl_Guid] IS NOT NULL
			AND [Suppl_Guid] = @Suppl_Guid;

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
GRANT EXECUTE ON [dbo].[usp_GetSupplWaybillGuid] TO [public]
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Возвращает признак "Заказ может быть переведен в накладную"
--
-- Входные параметры:
--
--		@Suppl_Guid				УИ заказа
--
-- Выходные параметры:
--
--		@CanCreateWaybillFromSuppl	признак "Заказ может быть переведен в накладную"
--  @ERROR_NUM					код ошибки
--  @ERROR_MES					текст ошибки
--
-- Результат:
--		0 - Успешное завершение
--		<>0 - ошибка
--

CREATE PROCEDURE [dbo].[usp_CanCreateWaybillFromSuppl] 
	@Suppl_Guid									D_GUID,
	
	@CanCreateWaybillFromSuppl	bit output,
  @ERROR_NUM									D_ID output,
  @ERROR_MES									D_ERROR_MESSAGE output
AS

BEGIN

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = '';
	SET @CanCreateWaybillFromSuppl = 0;

  BEGIN TRY

    IF NOT EXISTS( SELECT [Suppl_Guid] FROM [dbo].[T_Suppl] WHERE [Suppl_Guid] = @Suppl_Guid )
			BEGIN
				SET @ERROR_NUM = 1;
				SET @ERROR_MES = 'В базе данных не найден заказ с указанным идентификатором: ' + CAST( @Suppl_Guid as nvarchar(36) );

				RETURN @ERROR_NUM;
			END

		DECLARE @SupplState_Guid	D_GUID;
		DECLARE @SupplState_Id		D_ID;
		DECLARE @SupplState_Name	D_NAME;

		SELECT @SupplState_Guid = Suppl.[SupplState_Guid], 
			@SupplState_Id =  SupplState.SupplState_Id, @SupplState_Name = SupplState.SupplState_Name
		FROM  [dbo].[T_Suppl] AS Suppl INNER JOIN [dbo].[T_SupplState] AS SupplState ON Suppl.SupplState_Guid = SupplState.SupplState_Guid
		WHERE Suppl.[Suppl_Guid] = @Suppl_Guid;

		IF( @SupplState_Id IN ( 3, 11, 12 ) )
			BEGIN
				SET @CanCreateWaybillFromSuppl = 1;
			END
		ELSE
			BEGIN
				SET @CanCreateWaybillFromSuppl = 0;
				
				SET @ERROR_NUM = 2;
				SET @ERROR_MES = 'Заказ не может быть переведен в накладную. Состояние заказа: ' + @SupplState_Name;
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
GRANT EXECUTE ON [dbo].[usp_CanCreateWaybillFromSuppl] TO [public]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetSupplState]    Script Date: 12.04.2014 17:04:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Возвращает список состояний заказа
--
-- Входящие параметры:
--
-- Выходные параметры:
--  @ERROR_NUM - номер ошибки
--  @ERROR_MES - текст ошибки
--
-- Результат:
--    0 - Успешное завершение
--    <>0 - ошибка

ALTER PROCEDURE [dbo].[usp_GetSupplState] 
  
  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output

AS

BEGIN

	BEGIN TRY

    SET @ERROR_NUM = 0;
    SET @ERROR_MES = '';


		SELECT SupplState_Guid, SupplState_Id, SupplState_Name, SupplState_Description, SupplState_IsActive, SupplState_SupplCanBeModified, SupplState_SupplCanBeDeleted
		FROM [dbo].[T_SupplState]
		ORDER BY SupplState_Id;

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

-- Регистрирует оплату задолженности в InterBase

-- Входные параметры
-- 
--		@Suppl_Guid						- УИ заказа
--		@Document_Date				- дата накладной
--		@Document_Num					- номер накладной
--		@DocumentSendToStock	- признак "для склада"
--		@IBLINKEDSERVERNAME		- имя LINKEDSERVER для подключения к InterBase
--
-- Выходные параметры
--
--		@Waybill_Guid					- УИ накладной
--		@ERROR_NUM						- номер ошибки
--		@ERROR_MES						- сообщение об ошибке

ALTER PROCEDURE [dbo].[usp_AddWaybillFromSupplInIB]
	@Suppl_Guid						D_GUID,
	@Document_Date				D_DATE,
	@Document_Num					D_NAME,
	@DocumentSendToStock	bit,
	@IBLINKEDSERVERNAME		D_NAME = NULL,

  @Waybill_Guid					uniqueidentifier output,
	@SupplState_Guid			uniqueidentifier output,
	@ERROR_NUM						int output,
	@ERROR_MES						nvarchar(4000) output
AS

BEGIN
  BEGIN TRY
    SET @ERROR_NUM = 0;
    SET @ERROR_MES = '';
		SET @Waybill_Guid = NULL;
		SET @SupplState_Guid = NULL;

		IF NOT EXISTS ( SELECT Suppl_Guid FROM dbo.[T_Suppl] WHERE Suppl_Guid = @Suppl_Guid )
			BEGIN
				SET @ERROR_NUM = 1;
				SET @ERROR_MES = 'В базе данных не найден протокол с указанным идентификатором: ' + CAST( @Suppl_Guid as nvarchar(36) );

				RETURN @ERROR_NUM;
			END       
		
    DECLARE @SUPPL_ID integer;
    DECLARE @WAYBILL_NUM nvarchar(16);
    DECLARE @WAYBILL_FORSTOCK int;
		DECLARE @WAYBILL_ID int;

		DECLARE @CARDS_SHIPDATE varchar( 24 );
		IF( @Document_Date IS NULL ) SET @CARDS_SHIPDATE = 'NULL'
		ELSE 
			BEGIN
				SET @CARDS_SHIPDATE = convert( varchar( 10), @Document_Date, 104);
				SET @CARDS_SHIPDATE = '''''' + @CARDS_SHIPDATE + '''''';
			END	

		SET @WAYBILL_NUM = SUBSTRING( @Document_Num, 1, 16 );
		SET @WAYBILL_FORSTOCK = CAST( @DocumentSendToStock AS int );
		SELECT @SUPPL_ID = Suppl_Id, @SupplState_Guid = SupplState_Guid 
		FROM dbo.[T_Suppl] WHERE Suppl_Guid = @Suppl_Guid;

    DECLARE @EventID D_ID = NULL;
    DECLARE @ParentEventID D_ID = NULL;
    DECLARE @strMessage D_EVENTMSG = '';
    DECLARE @EventSrc D_NAME = 'Перевод заказа в накладную в IB';

	  IF( @IBLINKEDSERVERNAME IS NULL ) SELECT @IBLINKEDSERVERNAME = dbo.GetIBLinkedServerName();
	  
    DECLARE @SQLString nvarchar( 2048 );
    DECLARE @ParmDefinition nvarchar( 500 );

    SET @ParmDefinition = N' @WAYBILL_ID_Ib int output, @ErrorNumber_Ib int output, @ErrorText_Ib varchar(480) output'; 				

		SET @SQLString = 'SELECT @WAYBILL_ID_Ib = WAYBILL_ID, @ErrorNumber_Ib = ERROR_NUMBER, @ErrorText_Ib = ERROR_TEXT FROM OPENQUERY( ' + 
			@IBLINKEDSERVERNAME + ', ''SELECT WAYBILL_ID, ERROR_NUMBER, ERROR_TEXT FROM USP_ADD_WAYBILL_FROMSQL( ' + cast( @SUPPL_ID as nvarchar( 10 )) + ', ' + +
					@CARDS_SHIPDATE +  ', ''''' + @WAYBILL_NUM + ''''', ' +	cast( @WAYBILL_FORSTOCK as nvarchar(3)) + ' )'' )'; 

    PRINT @SQLString;

		EXECUTE sp_executesql @SQLString, @ParmDefinition, @WAYBILL_ID_Ib = @WAYBILL_ID output, 
			@ErrorNumber_Ib = @ERROR_NUM output, @ErrorText_Ib = @ERROR_MES output;  

		IF( ( @WAYBILL_ID IS NOT NULL ) AND ( @ERROR_NUM = 0 ) )
			BEGIN
				EXEC dbo.usp_SynchOneWaybillInfoFromIB	@IN_WAYBILL_ID	= @WAYBILL_ID, @ERROR_NUM	= @ERROR_NUM output, @ERROR_MES = @ERROR_MES output;

				IF( @ERROR_NUM = 0 )
					BEGIN
						SELECT @Waybill_Guid = Waybill_Guid FROM [dbo].[T_Waybill] 
						WHERE ( [Waybill_Id] IS NOT NULL ) AND ( [Waybill_Id] = @WAYBILL_ID );

						SELECT @SupplState_Guid = [SupplState_Guid] FROM [dbo].[T_SupplState]
						WHERE [SupplState_Id] = 13; -- ТТН

						UPDATE [dbo].[T_Suppl] SET [SupplState_Guid] = @SupplState_Guid
						WHERE [Suppl_Guid] = @Suppl_Guid;
					END
			END
		
	END TRY
	
	BEGIN CATCH
    SET @ERROR_NUM = ERROR_NUMBER();
    SET @ERROR_MES = @ERROR_MES + ' ' + ERROR_MESSAGE();
    
		EXEC dbo.spAddEventToLog @EVENT_SOURCE = @EventSrc, @EVENT_CATEGORY = 'None', 
      @EVENT_COMPUTER = ' ', @EVENT_TYPE = 'Error', @EVENT_IS_COMPOSITE = 0, 
      @EVENT_DESCRIPTION = @strMessage, @EVENT_PARENTID = @ParentEventID, @EVENT_ID = @EventID output;

		RETURN @ERROR_NUM;
	END CATCH;

	IF( @ERROR_NUM = 0 )
		SET @ERROR_MES = 'Успешное завершение операции.';

	RETURN @ERROR_NUM;

END

GO


DECLARE @doc xml;

SET @doc = '<SettingsForWaybillList>
  <WaybillListPaymentType>
    <PaymentType CanViewPaymentType2="0" DefPaymentTypeId="1" BlockOtherPaymentType="1" />
    <PaymentType CanViewPaymentType2="1" DefPaymentTypeId="1" BlockOtherPaymentType="0" />
  </WaybillListPaymentType>
  <WaybillListCompany>
    <Company PaymentType_Id="1" Company_Acronym="FLO" />
    <Company PaymentType_Id="2" Company_Acronym="CTR" />
  </WaybillListCompany>
</SettingsForWaybillList>';

INSERT INTO [dbo].[T_Settings]( Settings_Guid, Settings_Name, Settings_XML, Record_Updated, Record_UserUdpated, Settings_UserName )
VALUES( NEWID(), 'SettingsForWaybillList',  @doc, GetDate(), 'Admin', 'Настройки для журнала накладных' );
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Возвращает настройки по умолчанию для журнала накладных
--
-- Входные параметры:
--
--		@CanViewPaymentType2			признак "имеется доступ на просмотр накладных по форме оплаты №2"
--
-- Выходные параметры:
--
--		@DefPaymentTypeId									код формы оплаты по умолчанию
--		@BlockOtherPaymentType						признак "блокировать остальные формы оплаты"
--		@CompanyAcronymForPaymentType1		сокращенное наименование компании для формы оплаты №1
--		@CompanyAcronymForPaymentType2		сокращенное наименование компании для формы оплаты №2
--		@ERROR_NUM												номер ошибки
--		@ERROR_MES												текст ошибки

-- Результат:
--    0 - успешное завершение
--    <>0 - ошибка запроса информации из базы данных

CREATE PROCEDURE [dbo].[usp_GetWaybillListSettingsDefault] 
	@CanViewPaymentType2						bit,

  @DefPaymentTypeId								int output,
	@BlockOtherPaymentType					bit output,
	@CompanyAcronymForPaymentType1	D_ACRONYM output, 
	@CompanyAcronymForPaymentType2	D_ACRONYM output, 

	@ERROR_NUM											int output,
  @ERROR_MES											nvarchar(4000) output
AS

BEGIN

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = '';
  SET @DefPaymentTypeId = NULL;
	SET @BlockOtherPaymentType = NULL;
	SET @CompanyAcronymForPaymentType1 = NULL;
	SET @CompanyAcronymForPaymentType2 = NULL;

  BEGIN TRY

		DECLARE @Settings_Name D_NAME;
		SET @Settings_Name = 'SettingsForWaybillList';

		DECLARE @doc xml;
		SELECT Top 1 @doc = [Settings_XML] FROM [dbo].[T_Settings]
		WHERE [Settings_Name] = @Settings_Name;

		IF( @doc IS NOT NULL )
			BEGIN
				IF( @CanViewPaymentType2 = 0 )
					BEGIN
						SELECT @DefPaymentTypeId = @doc.value( '(//SettingsForWaybillList/WaybillListPaymentType/PaymentType[@CanViewPaymentType2=0]/@DefPaymentTypeId)[1]', 'int' ) ;
						SELECT @BlockOtherPaymentType = @doc.value( '(//SettingsForWaybillList/WaybillListPaymentType/PaymentType[@CanViewPaymentType2=0]/@BlockOtherPaymentType)[1]', 'bit' ) ;
					END

				IF( @CanViewPaymentType2 = 1 )
					BEGIN
						SELECT @DefPaymentTypeId = @doc.value( '(//SettingsForWaybillList/WaybillListPaymentType/PaymentType[@CanViewPaymentType2=1]/@DefPaymentTypeId)[1]', 'int' ) ;
						SELECT @BlockOtherPaymentType = @doc.value( '(//SettingsForWaybillList/WaybillListPaymentType/PaymentType[@CanViewPaymentType2=1]/@BlockOtherPaymentType)[1]', 'bit' ) ;
					END

				SELECT @CompanyAcronymForPaymentType1 = @doc.value( '(//SettingsForWaybillList/WaybillListCompany/Company[@PaymentType_Id=1]/@Company_Acronym)[1]', 'nvarchar(16)' ) ;
				SELECT @CompanyAcronymForPaymentType2 = @doc.value( '(//SettingsForWaybillList/WaybillListCompany/Company[@PaymentType_Id=2]/@Company_Acronym)[1]', 'nvarchar(16)' ) ;

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
GRANT EXECUTE ON [dbo].[usp_GetWaybillListSettingsDefault] TO [public]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Синхронизация данных заказа (из ERP в ERP_Mercury)
--
-- Входные параметры:
--		@BeginDeliveryDate	- начало периода
--		@EndDeliveryDate		- конец периода
--
-- Выходные параметры:
--  @ERROR_NUM		- код ошибки
--  @ERROR_MES		- текст ошибки
--
-- Результат:
--    0 - успешное завершение
--    <>0 - ошибка

ALTER PROCEDURE [dbo].[usp_Synchronization_T_Suppl_With_T_CSMPDASUPPL] 
	@BeginDeliveryDate D_DATE,
	@EndDeliveryDate D_DATE,
	
  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output
AS

BEGIN

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = '';

  BEGIN TRY

		DECLARE @strBeginDeliveryDate nvarchar(10);
		DECLARE @strEndDeliveryDate nvarchar(10);
		DECLARE @DeliveryDateReport datetime;
		DECLARE @strDeliveryDateReport nvarchar(20);
		SET @DeliveryDateReport = dbo.TrimTime( @EndDeliveryDate );
		
		SET @strBeginDeliveryDate = CONVERT (nvarchar(10), @BeginDeliveryDate );
		SET @strDeliveryDateReport = CONVERT (nvarchar(20), @DeliveryDateReport );

		SET @strEndDeliveryDate = CONVERT (nvarchar(10), @EndDeliveryDate );
		SET @strBeginDeliveryDate = Replace( @strBeginDeliveryDate, '-', '');
		SET @strEndDeliveryDate = Replace( @strEndDeliveryDate, '-', '');
		SET @strDeliveryDateReport = Replace( @strDeliveryDateReport, '-', '');
		PRINT @strBeginDeliveryDate;
		PRINT @strEndDeliveryDate;
		

		CREATE TABLE #SUPPL( GUID_ID uniqueidentifier, SUPPL_NUM int, SUPPL_ALLPRICE money, SUPPL_ALLDISCOUNT money, 
			SUPPL_TOTALPRICE money, SUPPL_CURRENCYALLPRICE money, 
			SUPPL_CURRENCYALLDISCOUNT money, SUPPL_CURRENCYTOTALPRICE money, SUPPL_BEGINDATE datetime, 
			SUPPL_STATE int, SUPPL_MONEYBONUS bit, SUPPL_NOTE nvarchar(4000),  SUPPL_SUBNUM int, SUPPL_ID int, 
			SUPPL_NUM_IB int, SUPPL_WEIGHT float, SUPPL_ALLQUANTITY float, SUPPLSTATE_ID int, 
			SHOW_IN_DELIVERY bit, SUPPL_EXCLUDEFROMADJ bit, SUPPL_IS_IMPORT_FROM_ERP_MERCURY bit, 
			PARTS_ID int, DEPART_CODE nvarchar(3), STOCK_ID int, CUSTOMERCHILD_ID int, SUPPL_DELIVERYDATE datetime, 
			CUSTOMER_ID int, RTT_GUID uniqueidentifier, ADDRESS_GUID uniqueidentifier, SUPPL_PARENT_GUID_ID uniqueidentifier);

		CREATE TABLE #SUPPLITEM( GUID_ID uniqueidentifier, SUPPL_GUID_ID uniqueidentifier, 
			SPLITMS_ORDERQTY float, SPLITMS_QUANTITY float, 
			SPLITMS_PRICE money, SPLITMS_DISCOUNT money, SPLITMS_DISCOUNTPRICE money, 
			SPLITMS_CURRENCYPRICE money, SPLITMS_CURRENCYDISCOUNTPRICE money, 
			SPLITMS_XMLPRICE xml, SPLITMS_XMLDISCOUNT xml, PARTS_ID int, MEASURE_ID int );

		INSERT INTO #SUPPL( GUID_ID, SUPPL_NUM, SUPPL_ALLPRICE, SUPPL_ALLDISCOUNT, 
			SUPPL_TOTALPRICE, SUPPL_CURRENCYALLPRICE, 
			SUPPL_CURRENCYALLDISCOUNT, SUPPL_CURRENCYTOTALPRICE, SUPPL_BEGINDATE, 
			SUPPL_STATE, SUPPL_MONEYBONUS, SUPPL_NOTE,  SUPPL_SUBNUM, SUPPL_ID, 
			SUPPL_NUM_IB, SUPPL_WEIGHT, SUPPL_ALLQUANTITY, SUPPLSTATE_ID, 
			SHOW_IN_DELIVERY, SUPPL_EXCLUDEFROMADJ, SUPPL_IS_IMPORT_FROM_ERP_MERCURY, 
			PARTS_ID, DEPART_CODE, STOCK_ID, CUSTOMERCHILD_ID, SUPPL_DELIVERYDATE, 
			CUSTOMER_ID, RTT_GUID, ADDRESS_GUID, SUPPL_PARENT_GUID_ID )
		SELECT GUID_ID, SUPPL_NUM, SUPPL_ALLPRICE, SUPPL_ALLDISCOUNT, 
			SUPPL_TOTALPRICE, SUPPL_CURRENCYALLPRICE, 
			SUPPL_CURRENCYALLDISCOUNT, SUPPL_CURRENCYTOTALPRICE, SUPPL_BEGINDATE, 
			SUPPL_STATE, SUPPL_MONEYBONUS, SUPPL_NOTE,  SUPPL_SUBNUM, SUPPL_ID, 
			SUPPL_NUM_IB, SUPPL_WEIGHT, SUPPL_ALLQUANTITY, SUPPLSTATE_ID, 
			SHOW_IN_DELIVERY, SUPPL_EXCLUDEFROMADJ, SUPPL_IS_IMPORT_FROM_ERP_MERCURY, 
			PARTS_ID, DEPART_CODE, STOCK_ID, CUSTOMERCHILD_ID, SUPPL_DELIVERYDATE, 
			CUSTOMER_ID, RTT_GUID_ID, ADDRESS_GUID_ID, SUPPL_PARENT_GUID_ID
		FROM [DB01].[ERP].[dbo].CsmPDASuppl_Syhch_With_ERP_Mercury_View
		WHERE	SUPPL_BEGINDATE BETWEEN @strBeginDeliveryDate AND @strEndDeliveryDate
			AND SUPPL_IS_IMPORT_FROM_ERP_MERCURY = 1;
		
		PRINT 'Запрос №1 отработал';
			
		DECLARE @Suppl_Guid uniqueidentifier; 
		DECLARE @SUPPL_ID int;
		DECLARE @SUPPL_NUM_IB int; 
		DECLARE @SUPPLSTATE_ID int;
		DECLARE @SHOW_IN_DELIVERY bit; 
		DECLARE @SUPPL_EXCLUDEFROMADJ bit;
		DECLARE @PARTS_ID int; 
		DECLARE @DEPART_CODE nvarchar(3); 
		DECLARE @STOCK_ID int; 
		DECLARE @CUSTOMER_ID int;
		DECLARE @CUSTOMERCHILD_ID int;
		DECLARE @SUPPL_MONEYBONUS bit; 
		DECLARE @SUPPL_NOTE nvarchar(4000);
		DECLARE @RowCount int;

		DECLARE @SupplItem_Guid uniqueidentifier;
		DECLARE @SUPPL_GUID_ID uniqueidentifier;
		DECLARE @SUPPL_PARENT_GUID_ID uniqueidentifier;
		DECLARE @SPLITMS_ORDERQTY float; 
		DECLARE @SPLITMS_QUANTITY float;
		DECLARE @SPLITMS_PRICE money; 
		DECLARE @SPLITMS_DISCOUNT money; 
		DECLARE @SPLITMS_DISCOUNTPRICE money; 
		DECLARE @SPLITMS_CURRENCYPRICE money; 
		DECLARE @SPLITMS_CURRENCYDISCOUNTPRICE money; 
		DECLARE @SPLITMS_XMLPRICE xml; 
		DECLARE @SPLITMS_XMLDISCOUNT xml; 
		DECLARE @MEASURE_ID int;

		DECLARE @SupplState_Guid	D_GUID;
		DECLARE @Parts_Guid				D_GUID;
		DECLARE @Measure_Guid			D_GUID;
		DECLARE @Stock_Guid				D_GUID;
		DECLARE @Currency_Guid		D_GUID;
		DECLARE @Depart_Guid			D_GUID;
		DECLARE @Customer_Guid		D_GUID;
		DECLARE @Address_Guid			D_GUID;
		DECLARE @Rtt_Guid					D_GUID;
		DECLARE @SUPPL_NUM				D_ID;
		DECLARE @SUPPL_SUBNUM			D_ID;
		DECLARE @SUPPL_BEGINDATE	D_DATE;
		DECLARE @SUPPL_DELIVERYDATE	D_DATE;
 		DECLARE @SUPPL_ALLPRICE money;
		DECLARE @SUPPL_ALLDISCOUNT money;
		DECLARE @SUPPL_CURRENCYALLDISCOUNT money;
		DECLARE @SUPPL_CURRENCYALLPRICE money;
		DECLARE @SUPPL_TOTALPRICE money;
		DECLARE @SUPPL_CURRENCYTOTALPRICE money;
		declare @suppl_allquantity float;
		DECLARE @CustomerChild_Guid	D_GUID;
		DECLARE @OrderType_Guid			D_GUID;
		DECLARE @CurrencyNational_Guid	D_GUID;
		DECLARE @PaymentType_Guid		D_GUID;
		DECLARE @AgreementWithCustomer_Guid	D_GUID;

		DECLARE @AddRowsCount int;
		DECLARE @UpdateRowsCount int;
		DECLARE @CancelRowsCount int;

		SET @CurrencyNational_Guid = ( SELECT dbo.GetCurrentCurrencyNationalGuid() );

		SET @AddRowsCount = 0;
		SET @UpdateRowsCount = 0;
		SET @CancelRowsCount = 0;

		DECLARE crSuppl CURSOR 
    FOR SELECT GUID_ID, SUPPL_ID, SUPPL_NUM_IB,	SUPPLSTATE_ID, SHOW_IN_DELIVERY, SUPPL_EXCLUDEFROMADJ, PARTS_ID, DEPART_CODE, 
			STOCK_ID, CUSTOMERCHILD_ID, SUPPL_NUM, SUPPL_SUBNUM, SUPPL_BEGINDATE, SUPPL_DELIVERYDATE, CUSTOMER_ID, 
			RTT_GUID, ADDRESS_GUID, SUPPL_MONEYBONUS, SUPPL_NOTE, SUPPL_PARENT_GUID_ID, PARTS_ID
		FROM #SUPPL
    OPEN crSuppl;
    FETCH NEXT FROM crSuppl INTO @Suppl_Guid, @SUPPL_ID, @SUPPL_NUM_IB,	@SUPPLSTATE_ID, @SHOW_IN_DELIVERY, @SUPPL_EXCLUDEFROMADJ, 
			@PARTS_ID, @DEPART_CODE, @STOCK_ID, @CUSTOMERCHILD_ID, @SUPPL_NUM, @SUPPL_SUBNUM, @SUPPL_BEGINDATE, @SUPPL_DELIVERYDATE, 
			@CUSTOMER_ID, @Rtt_Guid, @Address_Guid, @SUPPL_MONEYBONUS,  @SUPPL_NOTE, @SUPPL_PARENT_GUID_ID, @PARTS_ID; 
    WHILE @@FETCH_STATUS = 0
      BEGIN

				SET @SupplState_Guid = NULL;
				SELECT Top 1 @SupplState_Guid = [SupplState_Guid] FROM [dbo].[T_SupplState] WHERE [SupplState_Id] = @SUPPLSTATE_ID;

				DELETE FROM #SUPPLITEM;

				INSERT INTO #SUPPLITEM( GUID_ID, SUPPL_GUID_ID, SPLITMS_ORDERQTY, SPLITMS_QUANTITY, SPLITMS_PRICE, 
					SPLITMS_DISCOUNT, SPLITMS_DISCOUNTPRICE, SPLITMS_CURRENCYPRICE, SPLITMS_CURRENCYDISCOUNTPRICE, 
					SPLITMS_XMLPRICE, SPLITMS_XMLDISCOUNT, 
					PARTS_ID, MEASURE_ID )
				SELECT GUID_ID, SUPPL_GUID_ID, SPLITMS_ORDERQTY, SPLITMS_QUANTITY, SPLITMS_PRICE, 
					SPLITMS_DISCOUNT, SPLITMS_DISCOUNTPRICE, SPLITMS_CURRENCYPRICE, SPLITMS_CURRENCYDISCOUNTPRICE, 
					CONVERT( xml, strSPLITMS_XMLPRICE ), CONVERT( xml, strSPLITMS_XMLDISCOUNT ), 
					PARTS_ID, MEASURE_ID
				FROM [DB01].[ERP].[dbo].[CsmPDASupplItem_Syhch_With_ERP_Mercury_View]
				WHERE SUPPL_GUID_ID = @Suppl_Guid;

				SELECT @RowCount = COUNT( GUID_ID ) FROM #SUPPLITEM;

				IF( @RowCount > 0 )
					BEGIN
						
						DELETE FROM [dbo].[T_SupplItem] WHERE [Suppl_Guid] = @Suppl_Guid AND [SupplItem_Guid] NOT IN ( SELECT GUID_ID FROM #SUPPLITEM );

						IF NOT EXISTS( SELECT Suppl_Guid FROM T_Suppl WHERE [Suppl_Guid] = @Suppl_Guid )
							BEGIN
								SELECT @Stock_Guid = Stock_Guid FROM T_Stock WHERE Stock_id = @STOCK_ID;
								SET @Currency_Guid = NULL;
								SET @PaymentType_Guid = NULL;

								SELECT @Depart_Guid = [Depart_Guid] FROM [dbo].[T_Depart] WHERE [Depart_Code] = @DEPART_CODE;
								SELECT @Customer_Guid = Customer_Guid FROM T_Customer WHERE Customer_Id = @CUSTOMER_ID;

								SET @CustomerChild_Guid = NULL;
								IF( ( @CUSTOMERCHILD_ID IS NOT NULL ) AND ( @CUSTOMERCHILD_ID <> 0 ) )
									BEGIN
										SELECT @CustomerChild_Guid = [CustomerChild_Guid] FROM [dbo].[T_CustomerChild] WHERE [CustomerChild_Id] = @CUSTOMERCHILD_ID;
										SET @Currency_Guid = ( SELECT dbo.GetCurrentCurrencyMainGuid() );
									END
								ELSE
									SET @Currency_Guid = @CurrencyNational_Guid;
									
								SET @Parts_Guid = NULL;
								IF( @PARTS_ID IS NOT NULL )
									SELECT @Parts_Guid = Parts_Guid FROM T_Parts WHERE Parts_Id = @PARTS_ID

								IF( @Currency_Guid = @CurrencyNational_Guid ) 
									BEGIN
										SELECT @OrderType_Guid = OrderType_Guid FROM [dbo].[T_OrderType] WHERE OrderType_Id = 0;
										SELECT @PaymentType_Guid = PaymentType_Guid FROM T_PaymentType WHERE PaymentType_Id = 1;
									END
								ELSE
									BEGIN
										SELECT @OrderType_Guid = OrderType_Guid FROM [dbo].[T_OrderType] WHERE OrderType_Id = 1;
										SELECT @PaymentType_Guid = PaymentType_Guid FROM T_PaymentType WHERE PaymentType_Id = 2;
									END

								SET @AgreementWithCustomer_Guid = NULL;
								SELECT TOP 1 @AgreementWithCustomer_Guid = [AgreementWithCustomer_Guid] FROM [dbo].[T_AgreementWithCustomer]
								WHERE [Customer_Guid] = @Customer_Guid
									AND [Company_Guid] IN ( SELECT [Company_Guid] FROM [dbo].[T_Stock] WHERE [Stock_Guid] = @Stock_Guid );

								IF NOT EXISTS( SELECT Address_Guid FROM T_Address WHERE Address_Guid = @Address_Guid )
									SET @Address_Guid = NULL;

								IF NOT EXISTS( SELECT Rtt_Guid FROM T_Rtt WHERE Rtt_Guid = @Rtt_Guid )
									SET @Rtt_Guid = NULL;

								IF( ( @Address_Guid IS NOT NULL ) AND ( @Rtt_Guid IS NOT NULL ) AND ( @SupplState_Guid IS NOT NULL ) )
									BEGIN
										INSERT INTO [dbo].[T_Suppl]( Suppl_Guid, Suppl_Id, Stock_Guid, Currency_Guid, Suppl_Num, Suppl_Version, 
											Suppl_BeginDate, Suppl_DeliveryDate, SupplState_Guid, Depart_Guid, Customer_Guid, 
											Rtt_Guid, Address_Guid, CustomerChild_Guid, 
											Suppl_AllPrice, Suppl_AllDiscount, Suppl_TotalPrice, Suppl_AmountPaid,  
											Suppl_CurrencyAllPrice, Suppl_CurrencyAllDiscount, Suppl_CurrencyTotalPrice, Suppl_CurrencyAmountPaid,  
											Suppl_Bonus, Suppl_Note, SupplParent_Guid, Suppl_Quantity, Suppl_Weight, Parts_Guid, 
											Suppl_ExcludeFromAdj, OrderType_Guid, PaymentType_Guid, AgreementWithCustomer_Guid, SHOW_IN_DELIVERY )
										VALUES( @Suppl_Guid, @SUPPL_ID, @Stock_Guid, @Currency_Guid, @SUPPL_NUM, @SUPPL_SUBNUM, 
											@SUPPL_BEGINDATE, @SUPPL_DELIVERYDATE, @SupplState_Guid, @Depart_Guid, @Customer_Guid, 
											@Rtt_Guid, @Address_Guid, @CustomerChild_Guid, 
											0, 0, 0, 0, 
											0, 0, 0, 0,  
											@SUPPL_MONEYBONUS, @SUPPL_NOTE, @SUPPL_PARENT_GUID_ID, 0, 0, @Parts_Guid,
											@SUPPL_EXCLUDEFROMADJ, @OrderType_Guid, @PaymentType_Guid, @AgreementWithCustomer_Guid, @SHOW_IN_DELIVERY );
										
										SET @AddRowsCount = ( @AddRowsCount + 1 );
									END
								ELSE
									BEGIN
										SET @CancelRowsCount = ( @CancelRowsCount + 1 );
										PRINT 'отменяется вставка записи:'
										PRINT '@SUPPLSTATE_ID = '
										PRINT @SUPPLSTATE_ID;
										PRINT '@Address_Guid = '
										PRINT @Address_Guid;
										PRINT '@Rtt_Guid = ';
										PRINT @Rtt_Guid;
									END

							END

						IF EXISTS( SELECT Suppl_Guid FROM T_Suppl WHERE [Suppl_Guid] = @Suppl_Guid )
							BEGIN
								DECLARE crSupplItem CURSOR 
								FOR SELECT GUID_ID, SUPPL_GUID_ID, SPLITMS_ORDERQTY, SPLITMS_QUANTITY, SPLITMS_PRICE, 
									SPLITMS_DISCOUNT, SPLITMS_DISCOUNTPRICE, SPLITMS_CURRENCYPRICE, SPLITMS_CURRENCYDISCOUNTPRICE, 
									SPLITMS_XMLPRICE, SPLITMS_XMLDISCOUNT, 
									PARTS_ID, MEASURE_ID 
								FROM #SUPPLITEM
								WHERE SUPPL_GUID_ID = @Suppl_Guid;
								OPEN crSupplItem;
								FETCH NEXT FROM crSupplItem INTO @SupplItem_Guid, @SUPPL_GUID_ID, @SPLITMS_ORDERQTY, @SPLITMS_QUANTITY, @SPLITMS_PRICE, 
									@SPLITMS_DISCOUNT, @SPLITMS_DISCOUNTPRICE, @SPLITMS_CURRENCYPRICE, @SPLITMS_CURRENCYDISCOUNTPRICE, 
									@SPLITMS_XMLPRICE, @SPLITMS_XMLDISCOUNT, 
									@PARTS_ID, @MEASURE_ID; 
								WHILE @@FETCH_STATUS = 0
									BEGIN

										SET @Parts_Guid = NULL;
										SET @Measure_Guid = NULL;

										IF( @SPLITMS_QUANTITY IS NULL ) SET @SPLITMS_QUANTITY = 0; 
										IF( @SPLITMS_PRICE IS NULL ) SET @SPLITMS_PRICE = 0;
										IF( @SPLITMS_DISCOUNT IS NULL ) SET @SPLITMS_DISCOUNT = 0;
										IF( @SPLITMS_DISCOUNTPRICE IS NULL ) SET @SPLITMS_DISCOUNTPRICE = 0; 
										IF( @SPLITMS_CURRENCYPRICE IS NULL ) SET @SPLITMS_CURRENCYPRICE = 0; 
										IF( @SPLITMS_CURRENCYDISCOUNTPRICE IS NULL ) SET @SPLITMS_CURRENCYDISCOUNTPRICE = 0;

										SELECT @Parts_Guid = [Parts_Guid] FROM [dbo].[T_Parts] WHERE [Parts_Id] = @PARTS_ID;
										SELECT @Measure_Guid = [Measure_Guid] FROM [dbo].[T_Measure] WHERE [Measure_Id] = @MEASURE_ID;
								
										BEGIN TRY 
											IF NOT EXISTS( SELECT [SupplItem_Guid] FROM [dbo].[T_SupplItem] WHERE [SupplItem_Guid] = @SupplItem_Guid )
												INSERT INTO [dbo].[T_SupplItem]( SupplItem_Guid, Suppl_Guid, Parts_Guid, Measure_Guid, 
													SupplItem_OrderQuantity, SupplItem_Quantity, SupplItem_Price, SupplItem_Discount, SupplItem_DiscountPrice, 
													SupplItem_CurrencyPrice, SupplItem_CurrencyDiscountPrice,
													SupplItem_XMLPrice, SupplItem_XMLDiscount )
												VALUES( @SupplItem_Guid, @Suppl_Guid, @Parts_Guid, @Measure_Guid, 
													@SPLITMS_ORDERQTY, @SPLITMS_QUANTITY, @SPLITMS_PRICE, @SPLITMS_DISCOUNT, @SPLITMS_DISCOUNTPRICE,
													@SPLITMS_CURRENCYPRICE, @SPLITMS_CURRENCYDISCOUNTPRICE, 
													@SPLITMS_XMLPRICE, @SPLITMS_XMLDISCOUNT );
											ELSE 
												UPDATE [dbo].[T_SupplItem] SET [SupplItem_Quantity] = @SPLITMS_QUANTITY, [SupplItem_Price] = @SPLITMS_PRICE, 
													[SupplItem_Discount] = @SPLITMS_DISCOUNT, [SupplItem_DiscountPrice] = @SPLITMS_DISCOUNTPRICE, 
													[SupplItem_CurrencyPrice] = @SPLITMS_CURRENCYPRICE, [SupplItem_CurrencyDiscountPrice] = @SPLITMS_CURRENCYDISCOUNTPRICE,
													[SupplItem_XMLPrice] = @SPLITMS_XMLPRICE, [SupplItem_XMLDiscount] = @SPLITMS_XMLDISCOUNT
												WHERE [SupplItem_Guid] = @SupplItem_Guid;
										END TRY
										BEGIN CATCH
											PRINT 'Ошибка при обновлении позиции в заказе с УИ: ';
											PRINT @Suppl_Guid;
											PRINT 'Позия УИ: ';
											PRINT @SupplItem_Guid;
											PRINT ERROR_MESSAGE();
										END CATCH;

										FETCH NEXT FROM crSupplItem INTO @SupplItem_Guid, @SUPPL_GUID_ID, @SPLITMS_ORDERQTY, @SPLITMS_QUANTITY, @SPLITMS_PRICE, 
											@SPLITMS_DISCOUNT, @SPLITMS_DISCOUNTPRICE, @SPLITMS_CURRENCYPRICE, @SPLITMS_CURRENCYDISCOUNTPRICE, 
											@SPLITMS_XMLPRICE, @SPLITMS_XMLDISCOUNT, 
											@PARTS_ID, @MEASURE_ID;    
									END 
								CLOSE crSupplItem;
								DEALLOCATE crSupplItem;
							END

					END

				-- теперь нужно пересчитать сумму заказа в dbo.T_PDASuppl
				SELECT @SUPPL_ALLPRICE = SUM( SupplItem_Quantity * SupplItem_Price ), 
					@SUPPL_ALLDISCOUNT = SUM( ( SupplItem_Quantity * SupplItem_Price ) - ( SupplItem_Quantity * SupplItem_DiscountPrice ) ),
					@SUPPL_CURRENCYALLPRICE = SUM( SupplItem_Quantity * SupplItem_CurrencyPrice ),
					@SUPPL_CURRENCYALLDISCOUNT = SUM( ( SupplItem_Quantity * SupplItem_CurrencyPrice ) - ( SupplItem_Quantity * SupplItem_CurrencyDiscountprice ) ),
					@SUPPL_TOTALPRICE = SUM( SupplItem_Quantity * SupplItem_DiscountPrice ),
					@SUPPL_CURRENCYTOTALPRICE = SUM( SupplItem_Quantity * SupplItem_CurrencyDiscountprice ),
					@suppl_allquantity = sum( SupplItem_Quantity)
				FROM dbo.T_SupplItem WHERE Suppl_Guid = @Suppl_Guid;
    
				IF( @SUPPL_ALLPRICE IS NULL ) SET @SUPPL_ALLPRICE = 0;
				IF( @SUPPL_ALLDISCOUNT IS NULL ) SET @SUPPL_ALLDISCOUNT = 0;
				IF( @SUPPL_CURRENCYALLPRICE IS NULL ) SET @SUPPL_CURRENCYALLPRICE = 0;
				IF( @SUPPL_CURRENCYALLDISCOUNT IS NULL ) SET @SUPPL_CURRENCYALLDISCOUNT = 0;
				IF( @SUPPL_TOTALPRICE IS NULL ) SET @SUPPL_TOTALPRICE = 0;
				IF( @SUPPL_CURRENCYTOTALPRICE IS NULL ) SET @SUPPL_CURRENCYTOTALPRICE = 0;
    
				BEGIN TRY
					UPDATE dbo.T_Suppl SET SUPPL_ALLPRICE = @SUPPL_ALLPRICE, SUPPL_ALLDISCOUNT = @SUPPL_ALLDISCOUNT, 
						SUPPL_CURRENCYALLPRICE = @SUPPL_CURRENCYALLPRICE, SUPPL_CURRENCYALLDISCOUNT = @SUPPL_CURRENCYALLDISCOUNT, 
						SUPPL_TOTALPRICE = @SUPPL_TOTALPRICE, SUPPL_CURRENCYTOTALPRICE = @SUPPL_CURRENCYTOTALPRICE,
						Suppl_Quantity = @suppl_allquantity, SUPPL_WEIGHT = dbo.GetSupplWeight( @Suppl_Guid ), 
						SupplState_Guid = @SupplState_Guid,  [Suppl_Id] = @SUPPL_ID
					WHERE Suppl_Guid = @Suppl_Guid;
				END TRY
				BEGIN CATCH
					PRINT 'Ошибка при обновлении заказа с УИ: ';
					PRINT @Suppl_Guid;
					PRINT ERROR_MESSAGE();
				END CATCH;
		
				FETCH NEXT FROM crSuppl INTO @Suppl_Guid, @SUPPL_ID, @SUPPL_NUM_IB,	@SUPPLSTATE_ID, @SHOW_IN_DELIVERY, @SUPPL_EXCLUDEFROMADJ, 
					@PARTS_ID, @DEPART_CODE, @STOCK_ID, @CUSTOMERCHILD_ID, @SUPPL_NUM, @SUPPL_SUBNUM, @SUPPL_BEGINDATE, @SUPPL_DELIVERYDATE, 
					@CUSTOMER_ID, @Rtt_Guid, @Address_Guid, @SUPPL_MONEYBONUS, @SUPPL_NOTE, @SUPPL_PARENT_GUID_ID, @PARTS_ID;    
      END 
		CLOSE crSuppl;
		DEALLOCATE crSuppl;
		
		DROP TABLE #SUPPLITEM;
		DROP TABLE #SUPPL;

		PRINT 'добавлено записей:';
		PRINT @AddRowsCount;

		PRINT 'изменено записей:';
		PRINT @UpdateRowsCount;

		PRINT 'отменено записей:';
		PRINT @CancelRowsCount;

	END TRY
	BEGIN CATCH
		SET @ERROR_NUM = ERROR_NUMBER();
		SET @ERROR_MES = ERROR_MESSAGE();
		PRINT ERROR_MESSAGE();
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

-- Возвращает приложение к накладной (отгрузка клиенту) из заказа
-- используется для заполнения приложения к документу при переводе заказа в накладную
--
-- Входные параметры:
--
--		@Suppl_Guid		- УИ заказа
--
-- Выходные параметры:
--
--		@ERROR_NUM		- код ошибки
--		@ERROR_MES		- текст ошибки
--
-- Результат:
--
--    0						- успешное завершение
--    <>0					- ошибка запроса информации из базы данных
--

CREATE PROCEDURE [dbo].[usp_GetWaybItemsFromSuppl] 
	@Suppl_Guid		D_GUID,
  
	@ERROR_NUM			int output,
  @ERROR_MES			nvarchar(4000) output
AS

BEGIN

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = NULL;

  BEGIN TRY
		
		SELECT 	dbo.GetPartsProductName( SupplItem.Parts_Guid ) as ProductOwnerName, Parts.PARTS_NAME, Parts.PARTS_ARTICLE, Measure.Measure_ShortName,
			NULL AS WaybItem_Guid, NULL AS WaybItem_Id, SupplItem.SupplItem_Guid, NULL AS Waybill_Guid, SupplItem.Parts_Guid, SupplItem.Measure_Guid, 
			SupplItem.SupplItem_Quantity AS WaybItem_Quantity, CAST( 0 AS int ) AS WaybItem_RetQuantity, SupplItem.SupplItem_Quantity AS WaybItem_LeavQuantity, 
			SupplItem.SupplItem_Price AS WaybItem_Price, 
			SupplItem.SupplItem_Discount AS WaybItem_Discount, SupplItem.SupplItem_DiscountPrice AS WaybItem_DiscountPrice, 
			SupplItem.SupplItem_AllPrice AS WaybItem_AllPrice, SupplItem.SupplItem_TotalPrice AS WaybItem_TotalPrice, 
			SupplItem.SupplItem_TotalPrice AS WaybItem_LeavTotalPrice, SupplItem.SupplItem_CurrencyPrice AS WaybItem_CurrencyPrice, 
			SupplItem.SupplItem_CurrencyDiscountPrice AS WaybItem_CurrencyDiscountPrice, 
			SupplItem.SupplItem_CurrencyAllPrice AS WaybItem_CurrencyAllPrice, SupplItem.SupplItem_CurrencyTotalPrice AS WaybItem_CurrencyTotalPrice, 
			SupplItem.SupplItem_CurrencyTotalPrice AS WaybItem_CurrencyleavTotalPrice, 
			SupplItem.SupplItem_XMLPrice AS WaybItem_XMLPrice, SupplItem.SupplItem_XMLDiscount AS WaybItem_XMLDiscount, 
			SupplItem.SupplItem_NDSPercent AS WaybItem_NDSPercent, SupplItem.SupplItem_PriceImporter AS WaybItem_PriceImporter
		FROM dbo.T_SupplItem AS SupplItem, dbo.T_Parts as Parts, T_Measure as Measure
		WHERE SupplItem.Suppl_Guid = @Suppl_Guid
			AND SupplItem.Parts_Guid = Parts.Parts_Guid
			AND SupplItem.Measure_Guid = Measure.Measure_Guid
			AND SupplItem.SupplItem_Quantity > 0
		ORDER BY ProductOwnerName	
	
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
GRANT EXECUTE ON [dbo].[usp_GetWaybItemsFromSuppl] TO [public]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Возвращает информацию о шапке накладной на основании заказа к накладной
--
-- Входные параметры:
--
--		@Suppl_Guid					- УИ заказа
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

CREATE PROCEDURE [dbo].[usp_GetWaybillFromSuppl] 
  @Suppl_Guid					D_GUID,
	
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

		DECLARE @WaybillState_Guid uniqueidentifier; 
		DECLARE @WaybillState_Id int; 
		DECLARE @WaybillState_Name nvarchar( 128 );
		DECLARE @WaybillShipMode_Guid uniqueidentifier; 
		DECLARE @WaybillShipMode_Id int; 
		DECLARE @WaybillShipMode_Name nvarchar( 128 );
		
		SELECT @WaybillState_Guid = [WaybillState_Guid], @WaybillState_Id = [WaybillState_Id], @WaybillState_Name = [WaybillState_Name]
		FROM [dbo].[T_WaybillState] WHERE WaybillState_Id = 0;

		SELECT @WaybillShipMode_Guid = WaybillShipMode_Guid, @WaybillShipMode_Id = WaybillShipMode_Id, @WaybillShipMode_Name = WaybillShipMode_Name
		FROM [dbo].[T_WaybillShipMode] WHERE WaybillShipMode_Id = 0;

		WITH WaybillList ( Suppl_Guid ) AS 
		(
			SELECT Suppl_Guid FROM [dbo].[T_Suppl] WHERE [Suppl_Guid] = @Suppl_Guid
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
		SELECT CAST( NULL as uniqueidentifier) AS Waybill_Guid, CAST( NULL AS int ) AS Waybill_Id, Suppl.Suppl_Guid, 
			Suppl.Stock_Guid, Stock.Stock_Name, Stock.Stock_Id, Stock.Stock_IsActive, Stock.Stock_IsTrade, 
			Stock.Warehouse_Guid, Stock.WarehouseType_Guid,  
			Stock.Company_Guid, Company.Company_Id,  Company.Company_Acronym, Company.Company_Name, 
			Suppl.Currency_Guid, T_Currency.Currency_Abbr, Suppl.Depart_Guid, Depart.Depart_Code, 
			Suppl.Customer_Guid, Customer.Customer_Id, Customer.Customer_Name, 
			Customer.CustomerStateType_Guid, CustomerStateType.CustomerStateType_ShortName,
			Suppl.CustomerChild_Guid, [dbo].[T_CustomerChild].ChildDepart_Guid, [dbo].[T_ChildDepart].ChildDepart_Code, [dbo].[T_ChildDepart].ChildDepart_Name,
			Suppl.Rtt_Guid, Rtt.Rtt_Name, Suppl.Address_Guid, T_Address.Address_Name AS Address_FullName, 
			Suppl.PaymentType_Guid, PaymentType.PaymentType_Name,  CAST( Suppl.[Suppl_Num] AS nvarchar(56) ) AS Waybill_Num, Suppl.Suppl_BeginDate, Suppl.Suppl_DeliveryDate, 
			CAST( NULL as uniqueidentifier) AS WaybillParent_Guid, Suppl.Suppl_Bonus AS Waybill_Bonus, 
			@WaybillState_Guid, @WaybillState_Id, @WaybillState_Name, 
			@WaybillShipMode_Guid, @WaybillShipMode_Id, @WaybillShipMode_Name,
			CAST( NULL AS datetime ) AS Waybill_ShipDate, Suppl.Suppl_Note AS Waybill_Description, CAST( 0 as money ) AS Waybill_CurrencyRate, 
			Suppl.Suppl_AllPrice AS Waybill_AllPrice, CAST( 0 AS money ) AS Waybill_RetAllPrice, Suppl.Suppl_AllDiscount AS Waybill_AllDiscount, 
			Suppl.Suppl_TotalPrice AS Waybill_TotalPrice,
			Suppl.Suppl_AmountPaid AS Waybill_AmountPaid, Suppl.Suppl_Saldo AS Waybill_Saldo, Suppl.Suppl_CurrencyAllPrice AS Waybill_CurrencyAllPrice, 
			CAST( 0 AS money ) AS Waybill_CurrencyRetAllPrice, 
			Suppl.Suppl_CurrencyAllDiscount AS Waybill_CurrencyAllDiscount, Suppl.Suppl_CurrencyTotalPrice AS Waybill_CurrencyTotalPrice, 
			Suppl.Suppl_CurrencyAmountPaid AS Waybill_CurrencyAmountPaid, 
			Suppl.Suppl_CurrencySaldo AS Waybill_CurrencySaldo, Suppl.Suppl_Quantity AS Waybill_Quantity, 
			CAST( 0 AS int ) AS Waybill_RetQuantity, Suppl.Suppl_Quantity AS Waybill_LeavQuantity,
			Suppl.Suppl_Weight AS Waybill_Weight, Suppl.SHOW_IN_DELIVERY AS Waybill_ShowInDeliveryList
		FROM WaybillList INNER JOIN [dbo].[T_Suppl]	AS Suppl 
			ON WaybillList.Suppl_Guid = Suppl.Suppl_Guid  INNER JOIN T_Stock AS Stock
			ON Suppl.Stock_Guid = Stock.Stock_Guid INNER JOIN T_Company AS Company
			ON Stock.Company_Guid = Company.Company_Guid INNER JOIN T_Currency
			ON Suppl.Currency_Guid = T_Currency.Currency_Guid INNER JOIN T_Depart AS Depart
			ON Suppl.Depart_Guid = Depart.Depart_Guid INNER JOIN T_Customer AS Customer
			ON Suppl.Customer_Guid = Customer.Customer_Guid LEFT OUTER JOIN [dbo].[T_CustomerStateType] AS CustomerStateType
			ON Customer.CustomerStateType_Guid = CustomerStateType.CustomerStateType_Guid	LEFT OUTER JOIN [dbo].[T_CustomerChild] 
			ON Suppl.CustomerChild_Guid = [dbo].[T_CustomerChild].CustomerChild_Guid LEFT OUTER JOIN [dbo].[T_ChildDepart] 
			ON [dbo].[T_CustomerChild].ChildDepart_Guid = [dbo].[T_ChildDepart].ChildDepart_Guid INNER JOIN T_Rtt AS Rtt
			ON Suppl.Rtt_Guid = Rtt.Rtt_Guid INNER JOIN T_Address
			ON Suppl.Address_Guid = T_Address.Address_Guid INNER JOIN T_PaymentType AS PaymentType
			ON Suppl.PaymentType_Guid = PaymentType.PaymentType_Guid
		ORDER BY Suppl.Suppl_BeginDate;
	
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
		FROM #tmpWaybillList;

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
GRANT EXECUTE ON [dbo].[usp_GetWaybillFromSuppl] TO [public]
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Возвращает текущие остатки для выпадающего списка в приложении к накладной, формируемой из заказа
--
-- Входные параметры:
--
--		@Suppl_Guid						уи заказа
--
-- Выходные параметры:
--
--		@ERROR_NUM						код ошибки
--		@ERROR_MES						текст ошибки
--
-- Результат:
--    0										успешное завершение
--    <>0									ошибка запроса информации из базы данных

CREATE PROCEDURE [dbo].[usp_GetPartInstockForWaybillFromSuppl] 
	@Suppl_Guid						D_GUID,

  @ERROR_NUM						int output,
  @ERROR_MES						nvarchar(4000) output
AS

BEGIN

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = NULL;

  BEGIN TRY
  

    IF( @Suppl_Guid IS NOT NULL )
			BEGIN
				WITH WaybillItem (Parts_Guid, STOCK_QTY, Measure_Guid, SupplItem_PriceImporter )
				AS
				(
						SELECT Parts_Guid, SupplItem_Quantity, Measure_Guid, SupplItem_PriceImporter 
						FROM [dbo].[T_SupplItem] WHERE [Suppl_Guid] = @Suppl_Guid
				)
				SELECT WaybillItem.Parts_Guid, dbo.PartsView.Parts_Id, Barcode, 
					Parts_OriginalName, Parts_Name, Parts_ShortName, Parts_Article, Parts_PackQuantity, Parts_PackQuantityForCalc, Parts_BoxQuantity, 
					Parts_Weight, Parts_PlasticContainerWeight, Parts_PaperContainerWeight, Parts_AlcoholicContentPercent, 
					Parts_VendorPrice, Parts_IsActive, Parts_ActualNotValid, Parts_NotValid, Parts_Certificate,
					Country_Guid, Country_Name, Country_Code,
					Currency_Guid, Currency_Abbr, Currency_Code, 
					WaybillItem.Measure_Guid, Measure_Id, Measure_Name, Measure_ShortName, 
					Record_Updated, Record_UserUdpated, 
					Owner_Guid, Owner_Id, Owner_Name, Owner_ShortName, Owner_Description, Owner_IsActive, Owner_ProcessDaysCount, 
					Vtm_Guid, Vtm_Id, Vtm_Name, Vtm_ShortName, Vtm_Description, Vtm_IsActive, 
					Parttype_Guid, Parttype_Id, Parttype_Name, Parttype_NDSRate, Parttype_Description, Parttype_DemandsName, Parttype_IsActive,
					Partsubtype_Guid, Partsubtype_Id, Partsubtype_Name, Partsubtype_Description, Partsubtype_Image, Partsubtype_IsActive, 
					PartLine_Guid, Partline_Id, Partline_Name, Partline_Description, Partline_IsActive,
					Parts_CodeTNVD, Parts_Reference,
					PartsCategory_Guid, PartsCategory_Id, PartsCategory_Name, PartsCategory_Description, PartsCategory_IsActive, 
					WaybillItem.SupplItem_PriceImporter as Parts0,
					( Parts_Name + ' ' + Parts_Article ) AS PartsFullName, 0 as PartsIsCheck, 
					1  AS IsProductIncludeInStock, 
					STOCK_QTY, 0 as STOCK_RESQTY, 1 as PARTS_MINRETAILQTY, 1 as PARTS_MINWHOLESALEQTY, Parts_PackQuantity as PARTS_PACKQTY
				FROM WaybillItem INNER JOIN dbo.PartsView ON WaybillItem.Parts_Guid = dbo.PartsView.Parts_Guid
				WHERE STOCK_QTY > 0
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
GRANT EXECUTE ON [dbo].[usp_GetPartInstockForWaybillFromSuppl] TO [public]
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Возвращает список записей из ( dbo.T_Salesman )
--
-- Входные параметры:
--
--		@ShowOnlyActiveRows - только активные записи
--		@Depart_Guid				- УИ торгового подразделения
--
-- Выходные параметры:
--
-- Результат:
--    0 - успешное завершение
--    <>0 - ошибка запроса информации из базы данных

ALTER PROCEDURE [dbo].[sp_GetSalesman] 
	@ShowOnlyActiveRows D_YESNO = 0,
	@Depart_Guid				D_GUID = NULL,
	
  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output
AS

BEGIN

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = NULL;

  BEGIN TRY

    IF( @Depart_Guid IS NOT NULL )
			BEGIN
				SELECT Salesman.Salesman_Guid, Salesman.Salesman_Id, Salesman.User_Guid, Salesman.Salesman_Description, Salesman.Salesman_IsActive,
					Salesman.Record_Updated, Salesman.Record_IsActive, Salesman.Record_ParentGuid, Salesman.Record_UserUdpated,
					UserList.User_FirstName, UserList.User_MiddleName, UserList.User_LastName, UserList.User_LoginName, 
					UserList.User_Password, UserList.User_IsActive, UserList.User_Description 
				FROM dbo.T_Salesman as Salesman INNER JOIN dbo.T_User as UserList 
					ON Salesman.User_Guid = UserList.User_Guid INNER JOIN [dbo].[T_SalesmanDepart]	
					ON Salesman.Salesman_Guid = [dbo].[T_SalesmanDepart].Salesman_Guid AND [dbo].[T_SalesmanDepart].Depart_Guid = @Depart_Guid			
				ORDER BY UserList.User_LastName;
			END
		ELSE
			BEGIN
				IF( @ShowOnlyActiveRows = 1 )
					BEGIN
						SELECT Salesman.Salesman_Guid, Salesman.Salesman_Id, Salesman.User_Guid, Salesman.Salesman_Description, Salesman.Salesman_IsActive,
							Salesman.Record_Updated, Salesman.Record_IsActive, Salesman.Record_ParentGuid, Salesman.Record_UserUdpated,
							UserList.User_FirstName, UserList.User_MiddleName, UserList.User_LastName, UserList.User_LoginName, 
							UserList.User_Password, UserList.User_IsActive, UserList.User_Description 
						FROM dbo.T_Salesman as Salesman, dbo.T_User as UserList
						WHERE Salesman.User_Guid = UserList.User_Guid				
							AND ( ( Salesman.Record_IsActive = @ShowOnlyActiveRows ) OR ( Salesman.Record_IsActive IS NULL ) )
						ORDER BY UserList.User_LastName;
					END
				ELSE
					BEGIN
						SELECT Salesman.Salesman_Guid, Salesman.Salesman_Id, Salesman.User_Guid, Salesman.Salesman_Description, Salesman.Salesman_IsActive,
							Salesman.Record_Updated, Salesman.Record_IsActive, Salesman.Record_ParentGuid, Salesman.Record_UserUdpated,
							UserList.User_FirstName, UserList.User_MiddleName, UserList.User_LastName, UserList.User_LoginName, 
							UserList.User_Password, UserList.User_IsActive, UserList.User_Description 
						FROM dbo.T_Salesman as Salesman, dbo.T_User as UserList
						WHERE Salesman.User_Guid = UserList.User_Guid				
						ORDER BY UserList.User_LastName;
					END
			END
		
	END TRY
	BEGIN CATCH
		SET @ERROR_NUM = ERROR_NUMBER();
		SET @ERROR_MES = ERROR_MESSAGE();
		RETURN @ERROR_NUM;
	END CATCH;

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = 'Успешное завершение операции.';
  RETURN @ERROR_NUM;
END

GO

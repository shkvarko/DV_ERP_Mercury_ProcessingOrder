USE [ERP_Mercury]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetCustomerDebtInfoFromIB]    Script Date: 03/29/2012 21:32:04 ******/
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
			SELECT @SUPPL_ID = Suppl_Id FROM T_Order WHERE Order_Guid = @Order_Guid;
		
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
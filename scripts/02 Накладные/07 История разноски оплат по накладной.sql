USE [ERP_Mercury]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Возвращает архив оплат из InterBase
--
-- Входные параметры
--
--		@Doc_Guid								- УИ расходного документа
--		@IBLINKEDSERVERNAME			- имя LinkedServer
--
-- Выходные параметры
--
--		@ERROR_NUM							- код ошбики
--		@ERROR_MES							- сообщение об ошибке
--
CREATE PROCEDURE [dbo].[usp_GetPaymentHistoryFromIB] 
	@Doc_Guid								D_GUID,
	@IBLINKEDSERVERNAME			D_NAME = NULL,
  
  @ERROR_NUM							int output,
  @ERROR_MES							nvarchar(4000) output
  
AS

BEGIN
	SET NOCOUNT ON;
	
  SET @ERROR_NUM = 0;
  SET @ERROR_MES = '';

  IF( @IBLINKEDSERVERNAME IS NULL ) SELECT @IBLINKEDSERVERNAME = dbo.GetIBLinkedServerName();
  
	declare @sql_text nvarchar( 2048);
	
	BEGIN TRY

    DECLARE @WAYBILL_ID int;

		SELECT @WAYBILL_ID = Waybill_Id FROM T_Waybill WHERE Waybill_Guid = @Doc_Guid;

		IF( @WAYBILL_ID IS NULL )
			BEGIN
				SET @ERROR_NUM = 2;
				SET @ERROR_MES = 'В базе данных не найден документ с указанным идентификатором: ' + CAST( @Doc_Guid as nvarchar(36) );
			END


		CREATE TABLE #EarningList( PAYMENTS_ID int, BANKDATE DATE, PAYMENTS_OPERDATE DATE,
			PAYMENTS_VALUE FLOAT, CURRENCY_CODE NVARCHAR(3),
			EARNING_ID int, CUSTOMER_ID INT, CUSTOMER_NAME NVARCHAR(100),  EARNING_DATE DATE,  
			EARNING_VALUE FLOAT,  EARNING_EXPENSE FLOAT,  EARNING_SALDO FLOAT, COMPANY_ID INT, 
			COMPANY_ACRONYM NVARCHAR(8), EARNING_DESCRIPTION NVARCHAR(56) );

		SELECT @sql_text = dbo.GetTextQueryForSelectFromInterbase( null, null, 
			' SELECT PAYMENTS_ID, BANKDATE, PAYMENTS_OPERDATE, PAYMENTS_VALUE, CURRENCY_CODE, 
			EARNING_ID, CUSTOMER_ID, CUSTOMER_NAME,  EARNING_DATE, EARNING_VALUE,  EARNING_EXPENSE,  EARNING_SALDO, COMPANY_ID, COMPANY_ACRONYM, EARNING_DESCRIPTION 
			FROM USP_GETPAYMENTHISTORY( ' + 
			cast( @WAYBILL_ID as nvarchar(10) ) + ' )');
		SET @sql_text = ' INSERT INTO #EarningList( PAYMENTS_ID, BANKDATE, PAYMENTS_OPERDATE, PAYMENTS_VALUE, CURRENCY_CODE, 
			EARNING_ID, CUSTOMER_ID, CUSTOMER_NAME,  EARNING_DATE, 
			EARNING_VALUE,  EARNING_EXPENSE,  EARNING_SALDO, COMPANY_ID, COMPANY_ACRONYM, EARNING_DESCRIPTION ) ' + @sql_text;  

		PRINT @sql_text;

		execute sp_executesql @sql_text;
		
		
		SELECT PAYMENTS_ID, BANKDATE, PAYMENTS_OPERDATE, PAYMENTS_VALUE, CURRENCY_CODE,
			EARNING_ID, CUSTOMER_ID, CUSTOMER_NAME,  EARNING_DATE, 
			EARNING_VALUE,  EARNING_EXPENSE,  EARNING_SALDO, COMPANY_ID, COMPANY_ACRONYM, EARNING_DESCRIPTION
		FROM #EarningList
		ORDER BY PAYMENTS_OPERDATE;
		
		DROP TABLE #EarningList;
		
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
GRANT EXECUTE ON [dbo].[usp_GetPaymentHistoryFromIB] TO [public]
GO

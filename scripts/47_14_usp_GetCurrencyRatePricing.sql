USE [ERP_Mercury]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_GetCurrencyRatePricing] 
	@Currency_Date D_DATE,

	@CurrencyRatePricing float output,
  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output

AS

BEGIN
SET NOCOUNT ON;

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = '';
  
	BEGIN TRY

    SELECT @CurrencyRatePricing = dbo.GetCurrencyRatePricingInOut( dbo.GetCurrencyGuidUSD(), 
			dbo.GetCurrencyGuidBYB(), dbo.TrimTime( @Currency_Date ) );    

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
GRANT EXECUTE ON [dbo].[usp_GetCurrencyRatePricing] TO [public]
GO

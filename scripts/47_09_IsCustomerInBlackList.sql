USE [ERP_Mercury]
GO

/****** Object:  UserDefinedFunction [dbo].[IsCustomerInBlackList]    Script Date: 03/30/2012 13:34:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Creation date:	
-- Author:			

CREATE FUNCTION [dbo].[IsCustomerInBlackList] ( @Customer_Guid D_GUID, @Company_Guid D_GUID )
RETURNS bit
WITH EXECUTE AS caller
AS
BEGIN
  
DECLARE @bIsCustomerInBlackList bit;
SET @bIsCustomerInBlackList = 0;

DECLARE @BlackListGuid D_GUID;
SELECT @BlackListGuid = dbo.GetCustomerBlackListCategoryGuid();

IF( @BlackListGuid IS NOT NULL )
  BEGIN
    IF EXISTS ( SELECT * FROM dbo.T_CustomerCategoryCompany 
      WHERE Customer_Guid = @Customer_Guid AND Company_Guid = @Company_Guid AND CustomerCategory_Guid = @BlackListGuid )
	    SET @bIsCustomerInBlackList = 1;
  END


RETURN @bIsCustomerInBlackList;
end


GO
GRANT EXECUTE ON [dbo].[IsCustomerInBlackList] TO [public]
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
		RETURN @ERROR_NUM;
	END CATCH;

  IF( @ERROR_NUM = 0 )
		SET @ERROR_MES = 'Успешное завершение операции.';
		
  RETURN @ERROR_NUM;
END

GO
GRANT EXECUTE ON [dbo].[usp_IsCustomerInBL] TO [public]
GO
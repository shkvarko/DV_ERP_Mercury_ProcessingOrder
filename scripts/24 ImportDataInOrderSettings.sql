USE [ERP_Mercury]
GO
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
  SET @ERROR_MES = NULL;

  BEGIN TRY

    SELECT Top 1 Settings_Guid, Settings_Name, Settings_XML
    FROM dbo.T_Settings
    WHERE Settings_Name = 'ImportDataInOrderSettings';

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
GRANT EXECUTE ON [dbo].[usp_GetImportDataInOrderSettings] TO [public]
GO

DECLARE @doc xml;
SET @doc = '<ImportDataInOrderSettings xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <ColumnItem TOOLS_ID="10" TOOLS_NAME="STARTROW" TOOLS_USERNAME="Начальная строка" TOOLS_DESCRIPTION="№ строки, с которой начинается импорт данных" TOOLS_VALUE="5" TOOLSTYPE_ID="2" />
  <ColumnItem TOOLS_ID="11" TOOLS_NAME="ARTICLE" TOOLS_USERNAME="Артикул" TOOLS_DESCRIPTION="№ столбца с артикулом товара" TOOLS_VALUE="1" TOOLSTYPE_ID="2" />
  <ColumnItem TOOLS_ID="12" TOOLS_NAME="NAME2" TOOLS_USERNAME="Наименование" TOOLS_DESCRIPTION="№ столбца с наименованием товара" TOOLS_VALUE="2" TOOLSTYPE_ID="2" />
  <ColumnItem TOOLS_ID="13" TOOLS_NAME="QUANTITY" TOOLS_USERNAME="Количество" TOOLS_DESCRIPTION="№ столбца с количеством товара" TOOLS_VALUE="3" TOOLSTYPE_ID="2" />
  <ColumnItem TOOLS_ID="14" TOOLS_NAME="PRICE" TOOLS_USERNAME="Цена" TOOLS_DESCRIPTION="№ столбца с ценой товара" TOOLS_VALUE="4" TOOLSTYPE_ID="2" />
  <ColumnItem TOOLS_ID="15" TOOLS_NAME="MARKUP" TOOLS_USERNAME="Скидка, %" TOOLS_DESCRIPTION="№ столбца с размером скидки по позиции в заказе" TOOLS_VALUE="5" TOOLSTYPE_ID="2" />
  <ColumnItem TOOLS_ID="16" TOOLS_NAME="CUSTOMER_ID" TOOLS_USERNAME="Код клиента" TOOLS_DESCRIPTION="№ строки с кодом клиента" TOOLS_VALUE="2" TOOLSTYPE_ID="2" />
  <ColumnItem TOOLS_ID="17" TOOLS_NAME="RTT_CODE" TOOLS_USERNAME="Код РТТ" TOOLS_DESCRIPTION="№ строки с кодом РТТ" TOOLS_VALUE="2" TOOLSTYPE_ID="3" />
  <ColumnItem TOOLS_ID="18" TOOLS_NAME="DEPART_CODE" TOOLS_USERNAME="Подразделение" TOOLS_DESCRIPTION="№ строки с кодом подразделения" TOOLS_VALUE="1" TOOLSTYPE_ID="2" />
</ImportDataInOrderSettings>';

INSERT INTO dbo.T_Settings( Settings_Guid, Settings_Name, Settings_XML )
VALUES( NEWID(), 'ImportDataInOrderSettings', @doc );

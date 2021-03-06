USE [ERP_Mercury]
GO
CREATE NONCLUSTERED INDEX [INDX_T_PartType_ParttypeName] ON [dbo].[T_Parttype] 
(
	[Parttype_Name] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [INDEX]
GO

CREATE NONCLUSTERED INDEX [INDX_T_Owner_OwnerShortName] ON [dbo].[T_Owner] 
(
	[Owner_ShortName] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [INDEX]
GO

CREATE NONCLUSTERED INDEX [INDX_T_Owner_OwnerName] ON [dbo].[T_Owner] 
(
	[Owner_Name] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [INDEX]
GO

CREATE NONCLUSTERED INDEX [INDX_T_Parts_PartsFullName] ON [dbo].[T_Parts] 
(
	[Parts_Name] ASC,
	[Parts_Article] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [INDEX]
GO

CREATE NONCLUSTERED INDEX [INX_T_Parts_Parts_Article] ON [dbo].[T_Parts] 
(
	[Parts_Article] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [INDEX]
GO

CREATE NONCLUSTERED INDEX [INX_T_Parts_Parts_Name] ON [dbo].[T_Parts] 
(
	[Parts_Name] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [INDEX]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Возвращает список записей из ( dbo.T_Parts )
--
-- Входящие параметры:
--
-- Выходные параметры:
--
-- Результат:
--    0 - успешное завершение
--    <>0 - ошибка запроса информации из базы данных

CREATE PROCEDURE [dbo].[usp_GetPartInstock] 
	@Stock_Guid D_GUID,
  @IBLINKEDSERVERNAME dbo.D_NAME = NULL,

  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output
AS

BEGIN

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = NULL;

  BEGIN TRY
  
	  IF( @IBLINKEDSERVERNAME IS NULL ) SELECT @IBLINKEDSERVERNAME = dbo.GetIBLinkedServerName();

		DECLARE @strSQLText nvarchar(2048);
    DECLARE @Stock_Id int;
      
		SELECT @Stock_Id = Stock_Id FROM dbo.T_Stock 
		WHERE Stock_Guid = @Stock_Guid;
		
		CREATE TABLE #INSTOCK( PARTS_ID int, STOCK_QTY float, STOCK_RESQTY float, PARTS_MINRETAILQTY float,
			PARTS_MINWHOLESALEQTY float, PARTS_PACKQTY float );

		SELECT @strSQLText = dbo.GetTextQueryForSelectFromInterbase( null, null, 
		'SELECT PARTS_ID, STOCK_QTY, STOCK_RESQTY, PARTS_MINRETAILQTY, PARTS_MINWHOLESALEQTY, PARTS_PACKQTY 
					FROM SP_GETINSTOCK( ' +  	CAST( @Stock_Id as varchar(20)) +  ' ) ' );
		SET @strSQLText = 'INSERT INTO #INSTOCK( PARTS_ID, STOCK_QTY, STOCK_RESQTY, PARTS_MINRETAILQTY, PARTS_MINWHOLESALEQTY, PARTS_PACKQTY ) ' + @strSQLText;

		EXECUTE SP_EXECUTESQL @strSQLText;
		
		SELECT * FROM #INSTOCK;

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
			dbo.GetPrice0ForParts( Parts_Guid ) as Parts0,
			( Parts_Name + ' ' + Parts_Article ) AS PartsFullName, 0 as PartsIsCheck, 
			dbo.IsProductIncludeInStock(Parts_Guid) AS IsProductIncludeInStock, 
			#INSTOCK.STOCK_QTY, #INSTOCK.STOCK_RESQTY, #INSTOCK.PARTS_MINRETAILQTY, #INSTOCK.PARTS_MINWHOLESALEQTY, #INSTOCK.PARTS_PACKQTY
		FROM dbo.PartsView, #INSTOCK
		WHERE dbo.PartsView.Parts_Id = #INSTOCK.PARTS_ID
		--WHERE Parts_Id IN ( SELECT PARTS_ID FROM #INSTOCK )	
		ORDER BY Owner_Name, Parttype_Name, Partsubtype_Name, Parts_Name;  

		DROP TABLE #INSTOCK;
		
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

SET ANSI_NULLS ON

GO
GRANT EXECUTE ON [dbo].[usp_GetPartInstock] TO [public]
GO

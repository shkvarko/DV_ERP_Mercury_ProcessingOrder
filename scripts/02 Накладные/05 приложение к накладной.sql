USE [ERP_Mercury]
GO

ALTER TABLE [dbo].[T_WaybItem] ADD [WaybItem_Id] dbo.D_ID NULL
GO

UPDATE [dbo].[T_WaybItem] SET [WaybItem_Id] = 0
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Возвращает приложение к накладной (отгрузка клиенту)
--
-- Входные параметры:
--
--		@Waybill_Guid	- УИ накладной
--
-- Выходные параметры:
--
--		@ERROR_NUM		- код ошибки
--		@ERROR_MES		- текст ошибки
--
-- Результат:
--
--    0 - успешное завершение
--    <>0 - ошибка запроса информации из базы данных
--

CREATE PROCEDURE [dbo].[usp_GetWaybItems] 
	@Waybill_Guid		D_GUID,
  
	@ERROR_NUM			int output,
  @ERROR_MES			nvarchar(4000) output
AS

BEGIN

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = NULL;

  BEGIN TRY
		
		SELECT 	dbo.GetPartsProductName( WaybItem.Parts_Guid ) as ProductOwnerName, Parts.PARTS_NAME, Parts.PARTS_ARTICLE, Measure.Measure_ShortName,
			WaybItem.WaybItem_Guid, WaybItem.WaybItem_Id, WaybItem.SupplItem_Guid, WaybItem.Waybill_Guid, WaybItem.Parts_Guid, WaybItem.Measure_Guid, 
			WaybItem.WaybItem_Quantity, WaybItem.WaybItem_RetQuantity, WaybItem.WaybItem_LeavQuantity, WaybItem.WaybItem_Price, 
			WaybItem.WaybItem_Discount, WaybItem.WaybItem_DiscountPrice, WaybItem.WaybItem_AllPrice, WaybItem.WaybItem_TotalPrice, 
			WaybItem.WaybItem_LeavTotalPrice, WaybItem.WaybItem_CurrencyPrice, WaybItem.WaybItem_CurrencyDiscountPrice, 
			WaybItem.WaybItem_CurrencyAllPrice, WaybItem.WaybItem_CurrencyTotalPrice, WaybItem.WaybItem_CurrencyleavTotalPrice, 
			WaybItem.WaybItem_XMLPrice, WaybItem.WaybItem_XMLDiscount, WaybItem.WaybItem_NDSPercent, WaybItem.WaybItem_PriceImporter
		FROM dbo.T_WaybItem as WaybItem, dbo.T_Parts as Parts, T_Measure as Measure
		WHERE WaybItem.Waybill_Guid = @Waybill_Guid
			AND WaybItem.Parts_Guid = Parts.Parts_Guid
			AND WaybItem.Measure_Guid = Measure.Measure_Guid
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
GRANT EXECUTE ON [dbo].[usp_GetWaybItems] TO [public]
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
--		@Waybill_Guid					уи накладной
--		@IBLINKEDSERVERNAME		LinkedSever к InterBase
--
-- Выходные параметры:
--
--		@ERROR_NUM						код ошибки
--		@ERROR_MES						текст ошибки
--
-- Результат:
--    0										успешное завершение
--    <>0									ошибка запроса информации из базы данных

CREATE PROCEDURE [dbo].[usp_GetPartInstockForWaybill] 
	@Stock_Guid						D_GUID,
	@Waybill_Guid					D_GUID = NULL,
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
    
    IF( @Waybill_Guid IS NOT NULL )
			BEGIN
				WITH WaybillItem (Parts_Guid)
				AS
				(
						SELECT Parts_Guid FROM [dbo].[T_WaybItem] WHERE [Waybill_Guid] = @Waybill_Guid
				)
				SELECT WaybillItem.Parts_Guid, dbo.PartsView.Parts_Id, Barcode, 
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
					dbo.GetPriceForParts( WaybillItem.Parts_Guid, @PartsubtypePriceTypePrice0_Guid ) as Parts0,
					( Parts_Name + ' ' + Parts_Article ) AS PartsFullName, 0 as PartsIsCheck, 
					dbo.IsProductIncludeInStock(WaybillItem.Parts_Guid) AS IsProductIncludeInStock, 
					0 as STOCK_QTY, 0 as STOCK_RESQTY, 1 as PARTS_MINRETAILQTY, 1 as PARTS_MINWHOLESALEQTY, Parts_PackQuantity as PARTS_PACKQTY
				FROM WaybillItem INNER JOIN dbo.PartsView ON WaybillItem.Parts_Guid = dbo.PartsView.Parts_Guid
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
GRANT EXECUTE ON [dbo].[usp_GetPartInstockForWaybill] TO [public]
GO

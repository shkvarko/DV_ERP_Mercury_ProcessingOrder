USE [ERP_Mercury]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[GetSupplIDFromXml] ( @doc xml (DOCUMENT InfoForCalcPriceSchema))
returns uniqueidentifier
with execute as caller
as
begin
 DECLARE @ReturnValue uniqueidentifier;
 SET @ReturnValue = NULL;
    DECLARE @existSupplNode bit;
    SELECT @existSupplNode = @doc.exist( '//InfoForCalc/Suppl' );
    IF( @existSupplNode = 1 )
      BEGIN
        DECLARE @existSupplID bit;
        SELECT @existSupplID = @doc.exist( '(//InfoForCalc/Suppl/@SupplID)[1]' );
        IF( @existSupplID = 1 )
          BEGIN
            DECLARE @strSupplID nvarchar(36);
            SELECT @strSupplID = @doc.value( '(//InfoForCalc/Suppl/@SupplID)[1]', 'nvarchar(36)' );
            SET @ReturnValue = CONVERT( uniqueidentifier, @strSupplID );
          END
      END

 RETURN @ReturnValue;

end

GO
GRANT EXECUTE ON [dbo].[GetSupplIDFromXml] TO [public]
GO

/****** Object:  UserDefinedFunction [dbo].[GetPartsIDFromXml]    Script Date: 03/24/2012 18:24:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[GetPartsIDFromXml] ( @doc xml (DOCUMENT InfoForCalcPriceSchema))
returns int
with execute as caller
as
begin
  DECLARE @ReturnValue int;
  SET @ReturnValue = NULL;
  
  DECLARE @existSupplItmsNode bit;
  SELECT @existSupplItmsNode = @doc.exist( '//InfoForCalc/SupplItms' );
  IF( @existSupplItmsNode = 1 )
    BEGIN
      DECLARE @existPartsID bit;
      SELECT @existPartsID = @doc.exist( '(//InfoForCalc/SupplItms/@PartsID)[1]' );
      IF( @existPartsID = 1 )
        BEGIN
          SELECT @ReturnValue = @doc.value( '(//InfoForCalc/SupplItms/@PartsID)[1]', 'int' );
        END
    END

 RETURN @ReturnValue;

end


GO
GRANT EXECUTE ON [dbo].[GetPartsIDFromXml] TO [public]
GO

/****** Object:  UserDefinedFunction [dbo].[GetPartsGuidFromXml]    Script Date: 03/24/2012 18:26:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[GetPartsGuidFromXml] ( @doc xml (DOCUMENT InfoForCalcPriceSchema))
returns uniqueidentifier
with execute as caller
as
begin
  DECLARE @ReturnValue uniqueidentifier;
  SET @ReturnValue = NULL;
  
  DECLARE @PartsID int;
  SET @PartsID = NULL;
  DECLARE @existSupplItmsNode bit;
  SELECT @existSupplItmsNode = @doc.exist( '//InfoForCalc/SupplItms' );
  IF( @existSupplItmsNode = 1 )
    BEGIN
      DECLARE @existPartsID bit;
      SELECT @existPartsID = @doc.exist( '(//InfoForCalc/SupplItms/@PartsID)[1]' );
      IF( @existPartsID = 1 )
        BEGIN
          SELECT @PartsID = @doc.value( '(//InfoForCalc/SupplItms/@PartsID)[1]', 'int' );
          IF( @PartsID IS NOT NULL )
            BEGIN
              SELECT @ReturnValue = Parts_Guid FROM dbo.T_Parts WHERE Parts_Id = @PartsId;
            END
        END
    END

 RETURN @ReturnValue;

end


GO
GRANT EXECUTE ON [dbo].[GetPartsGuidFromXml] TO [public]
GO

/****** Object:  StoredProcedure [dbo].[sp_AddEventLogForCalcPrice]    Script Date: 03/24/2012 18:07:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_AddEventLogForCalcPrice] 
	@SupplItms_Guid D_GUID = NULL,
  @SupplItmsInfo xml (DOCUMENT InfoForCalcPriceSchema),
  @EventType_Id D_ID,
  @RulePool_StepGuid D_GUID = NULL,
  @EventDscrpn D_EVENTMSG = NULL

  --@ERROR_NUM int output,
  --@ERROR_MES nvarchar(4000) output

AS

BEGIN

  --SET @ERROR_NUM = 0;
  --SET @ERROR_MES = '';
  
  -- Для того, что бы управлять детализацией журнала, можно ограничивать запись @EventType_Id
IF( @EventType_Id < 130  ) RETURN 0;

	DECLARE @Parts_Guid D_GUID;     -- УИ товара
	DECLARE @Parts_Name D_NAME;			-- Наименование товара
  DECLARE @Suppl_Guid D_GUID;     -- УИ заказа
	DECLARE @Customer_Guid D_GUID;  -- УИ клиента
	DECLARE @Customer_Name D_NAME;  -- Наименование клиента
	DECLARE @RulePool_StepName D_NAME;-- Имя шага  в списке правил

  -- Для журнала событий
  DECLARE @EventID D_ID;
  SET @EventID = NULL;
  DECLARE @EventTypeName D_NAME;
  SET @EventTypeName = '';
  DECLARE @EventInfoTypeName D_NAME;
  SET @EventInfoTypeName = '';
  DECLARE @ParentEventID D_ID;
  SET @ParentEventID = NULL;
  DECLARE @strMessage D_EVENTMSG;
  SET @strMessage = '';
  DECLARE @EventSrc D_NAME;
	SET @EventSrc = 'Заказы';
  DECLARE @EventCategory D_NAME;
  SET @EventCategory = '';
  
	BEGIN TRY
		-- Проверка наличия типа сообщения с указанным идентификатором
		IF NOT EXISTS ( SELECT * FROM dbo.T_EVENTTYPE WHERE EVENTTYPE_ID = @EventType_Id )
			BEGIN
				SET @EventType_Id = 0;
				SET @EventTypeName = '';
				SET @EventCategory = '';
			END	
		ELSE
			BEGIN
				SELECT @EventCategory = EventCategory.EVENTCATEGORY_NAME, @EventInfoTypeName = EventInfoType.EVENTINFOTYPE_NAME, 
					@EventTypeName = EventType.EVENTTYPE_NAME
				FROM dbo.T_EVENTTYPE as EventType, dbo.T_EVENTCATEGORY as EventCategory, dbo.T_EVENTINFOTYPE as EventInfoType
				WHERE EventType.EVENTTYPE_ID = @EventType_Id
					AND EventType.EVENTCATEGORY_GUID_ID = EventCategory.GUID_ID
					AND EventType.EVENTINFOTYPE_GUID_ID = EventInfoType.GUID_ID
			END	
			
		-- Имя шага  в списке правил
		IF( @RulePool_StepGuid IS NOT NULL )
			SELECT @RulePool_StepName = RulePool_StepName FROM dbo.T_RulePool WHERE RulePool_StepGuid = @RulePool_StepGuid;
		IF( @RulePool_StepName IS NULL ) SET @RulePool_StepName = '';
		
		-- из @SupplItmsInfo получим информацию о товаре, клиенте и заказе
		IF( @SupplItms_Guid IS NOT NULL )
			BEGIN
				SELECT @Customer_Guid= Customer_Guid, @Suppl_Guid = Suppl_Guid, @Parts_Guid = Parts_Guid 
				FROM #PDASupplItms WHERE SupplItms_Guid = @SupplItms_Guid;
			END
		ELSE	
			BEGIN
				SELECT @Customer_Guid = dbo.GetCustomerGuidFromXml( @SupplItmsInfo ); 
				SELECT @Suppl_Guid = dbo.GetSupplIDFromXml( @SupplItmsInfo );  
				SELECT @Parts_Guid = dbo.GetPartsGuidFromXml( @SupplItmsInfo );  
			END
		SELECT @Customer_Name = CUSTOMER_NAME	FROM dbo.T_CUSTOMER WHERE Customer_Guid = @Customer_Guid;
		SELECT @Parts_Name = ( Parts_Name + ' ' + Parts_Article ) FROM dbo.T_Parts WHERE Parts_Guid = @Parts_Guid;

		IF( @EventInfoTypeName <> '' )
			SET @strMessage = @EventInfoTypeName;
		IF( @EventCategory <> '' )
			SET @strMessage = @strMessage + nChar(13) + nChar(10) +  nChar(13) + nChar(10) + @EventCategory;
		IF( @EventTypeName <> '' )
			SET @strMessage = @strMessage + nChar(13) + nChar(10) + @EventTypeName;
		IF( @RulePool_StepName <> '' )
			SET @strMessage = @strMessage + nChar(13) + nChar(10) + @RulePool_StepName;
		IF( @Customer_Name <> '' )
			SET @strMessage = @strMessage + nChar(13) + nChar(10) + @Customer_Name;
		IF( @Parts_Name <> '' )
			SET @strMessage = @strMessage + nChar(13) + nChar(10) + @Parts_Name;
		IF( @EventDscrpn IS NOT NULL )
			SET @strMessage = @strMessage + nChar(13) + nChar(10) + nChar(13) + nChar(10) + @EventDscrpn;
			
		EXEC dbo.spAddEventToLog @EVENT_SOURCE = @EventSrc, @EVENT_SOURCEID = @Suppl_Guid, @EVENT_CATEGORY = @EventCategory, 
			@EVENT_COMPUTER = ' ', @EVENT_TYPE = @EventTypeName, @EVENT_IS_COMPOSITE = 0, 
			@EVENT_DESCRIPTION = @strMessage, @EVENT_PARENTID = @ParentEventID, @EVENTTYPE_ID = @EventType_Id, @EVENT_ID = @EventID output;
			
	END TRY
	BEGIN CATCH
		--SET @ERROR_NUM = ERROR_NUMBER();
		--SET @ERROR_MES =  ERROR_MESSAGE();
    RETURN ERROR_NUMBER();
	END CATCH;

	RETURN 0;
END


GO
GRANT EXECUTE ON [dbo].[sp_AddEventLogForCalcPrice] TO [public]
GO

/****** Object:  StoredProcedure [dbo].[sp_ExecuteRuleCalculation]    Script Date: 03/24/2012 18:38:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[sp_ExecuteRuleCalculation] 
	@Spl_Guid D_GUID = NULL,
	@SupplItms_Guid D_GUID = NULL,
  @RuleCalculation_Guid D_GUID,
  @RulePool_StepGuid uniqueidentifier,
  @SPParamsInfo xml (DOCUMENT RuleParams) = NULL,
  @SupplItmsInfo xml (DOCUMENT InfoForCalcPriceSchema) output,
  @DiscountListInfo xml ( DOCUMENT DiscountList ) = NULL output,

  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output

AS

BEGIN
SET NOCOUNT ON;

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = NULL;

  DECLARE @Suppl_Guid D_GUID;

	BEGIN TRY
    
		IF( @SupplItms_Guid IS NOT NULL )
			SELECT @Suppl_Guid = Suppl_Guid	FROM #PDASupplItms WHERE SupplItms_Guid = @SupplItms_Guid;
		ELSE	
	    SELECT @Suppl_Guid = dbo.GetSupplIDFromXml( @SupplItmsInfo );
    
    DECLARE @StoredProcedure_Name D_NAME;
    SELECT @StoredProcedure_Name = RuleCalculationStoredProcedure.StoredProcedure_Name
    FROM dbo.T_RuleCalculation as RuleCalculation, dbo.T_RuleCalculationStoredProcedure as RuleCalculationStoredProcedure
    WHERE RuleCalculation.RuleCalculation_Guid = @RuleCalculation_Guid
      AND RuleCalculation.StoredProcedure_Guid = RuleCalculationStoredProcedure.StoredProcedure_Guid;
    IF( ( @StoredProcedure_Name IS NULL ) OR ( @StoredProcedure_Name = '' ) )  
		BEGIN
			SET @ERROR_NUM = 1;
			SET @ERROR_MES = @ERROR_MES + nChar(13) + nChar(10)+  '[SP_ExecuteRuleCalculation], Текст ошибки: Не найдена хранимая процедура для правила с заданным иденификатором.'  + nChar(13) + nChar(10)+ 'Идентификатор: ' + CONVERT( nvarchar(36), @RuleCalculation_Guid );

			EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 122,	@RulePool_StepGuid = @RulePool_StepGuid, @EventDscrpn = @ERROR_MES;

			RETURN @ERROR_NUM;
		END
      
    DECLARE @SQLString nvarchar(500);
    DECLARE @ParmDefinition nvarchar(500);

		IF( @SPParamsInfo IS NULL )
			BEGIN

				SET @StoredProcedure_Name = 'dbo.' + @StoredProcedure_Name;
				EXEC @StoredProcedure_Name @Spl_Guid = @Spl_Guid, @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo output, @RulePool_StepGuid = @RulePool_StepGuid, @DiscountListInfo = @DiscountListInfo output, @ERROR_NUM = @ERROR_NUM output, @ERROR_MES = @ERROR_MES output;
				
			END
		ELSE	
			BEGIN
				SET @StoredProcedure_Name = 'dbo.' + @StoredProcedure_Name;
				EXEC @StoredProcedure_Name @Spl_Guid = @Spl_Guid, @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo output, @SPParamsInfo = @SPParamsInfo output, @RulePool_StepGuid = @RulePool_StepGuid, @DiscountListInfo = @DiscountListInfo output, @ERROR_NUM = @ERROR_NUM output, @ERROR_MES = @ERROR_MES output;

				SET @ERROR_MES = '@SPParamsInfo IS NOT NULL  ';
				
			END

    -- (@ERROR_NUM = 0) - означает, что правило (хранимая процедура) согласилась поискать цену и нашла ее
    -- это стоит учитывать в процедурах верхнего уровня, чтобы знать, отработало ли хотя бы одно правило
    -- запишем результат работы процедуры
    -- (@ERROR_NUM = -1) - означает, что правило не предназначено для заданной входной информации
    IF( (@ERROR_NUM <> 0) AND (@ERROR_NUM <> -1) )
			BEGIN
				EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 122,	@RulePool_StepGuid = @RulePool_StepGuid, @EventDscrpn = @ERROR_MES;
			END

	END TRY
	BEGIN CATCH
		SET @ERROR_NUM = ERROR_NUMBER(); 
		SET @ERROR_MES = @ERROR_MES + ' [SP_ExecuteRuleCalculation], Текст запроса: ' + @SQLString + 'Текст ошибки: ' + ERROR_MESSAGE();

		EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 122,	@RulePool_StepGuid = @RulePool_StepGuid, @EventDscrpn = @ERROR_MES;

		RETURN @ERROR_NUM;
	END CATCH;

	IF( @ERROR_NUM = 0 )
		SET @ERROR_MES = '[SP_ExecuteRuleCalculation] Успешное завершение операции.';
	RETURN @ERROR_NUM;
	
END

GO
GRANT EXECUTE ON [dbo].[sp_ExecuteRuleCalculation] TO [public]
GO

/****** Object:  UserDefinedFunction [dbo].[GetWareHouseGuidFromXml]    Script Date: 03/24/2012 18:55:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[GetWareHouseGuidFromXml] ( @doc xml (DOCUMENT InfoForCalcPriceSchema))
returns uniqueidentifier
with execute as caller
as
begin
 DECLARE @ReturnValue uniqueidentifier;
 SET @ReturnValue = NULL;
    DECLARE @existSupplNode bit;
    SELECT @existSupplNode = @doc.exist( '//InfoForCalc/Suppl' );
    IF( @existSupplNode = 1 )
      BEGIN
        DECLARE @existSupplID bit;
        SELECT @existSupplID = @doc.exist( '(//InfoForCalc/Suppl/@SupplID)[1]' );
        IF( @existSupplID = 1 )
          BEGIN
            DECLARE @strSupplID nvarchar(36);
            SELECT @strSupplID = @doc.value( '(//InfoForCalc/Suppl/@SupplID)[1]', 'nvarchar(36)' );
            
            DECLARE @SupplGuid D_GUID;
            SET @SupplGuid = CONVERT( uniqueidentifier, @strSupplID );
            
            IF( @SupplGuid IS NOT NULL )
							BEGIN
								DECLARE @StockGuid D_GUID;
								SELECT @StockGuid = Stock_Guid FROM dbo.T_Order WHERE Order_Guid = @SupplGuid;
								IF( @StockGuid IS NOT NULL )
									BEGIN
										SELECT @ReturnValue = Warehouse_Guid FROM dbo.T_Stock WHERE Stock_Guid = @StockGuid;
									END
							END
          END
      END

 RETURN @ReturnValue;

end

GO
GRANT EXECUTE ON [dbo].[GetWareHouseGuidFromXml] TO [public]
GO

/****** Object:  StoredProcedure [dbo].[sp_CheckPossibleRunRuleCalculation]    Script Date: 03/24/2012 18:41:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_CheckPossibleRunRuleCalculation]
	@SupplItms_Guid D_GUID = NULL,
  @SupplItmsInfo xml ( DOCUMENT InfoForCalcPriceSchema ),
  @RulePool_StepGuid D_GUID,

	@IsPossible D_YESNO output,
  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output

AS

BEGIN

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = NULL;
  SET @IsPossible = 0;

	BEGIN TRY
    DECLARE @Today D_DATE;
    SET @Today = ( SELECT dbo.TrimTime( GetDate() ) );
    DECLARE @RulePool_BeginDate D_DATE;
    DECLARE @RulePool_EndDate D_DATE;
    SELECT @RulePool_BeginDate = RulePool_BeginDate, @RulePool_EndDate = RulePool_EndDate 
    FROM dbo.T_RulePool WHERE RulePool_StepGuid = @RulePool_StepGuid;
    
    IF( ( @Today < @RulePool_BeginDate ) OR ( @Today > @RulePool_EndDate ) )
      BEGIN
        SET @ERROR_NUM = 1;
				EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 115,	@RulePool_StepGuid = @RulePool_StepGuid;
        RETURN @ERROR_NUM;
      END     

		-- Выполним проверку на то, входят ли объекты из схемы @SupplItmsInfo
		-- в разрешенные списки для отработки правила
		-- Проверка выполняется по следующим типам объектов:
		-- Клиенты
		-- Формы собственности
		-- Категории клиентов
		
		DECLARE @Customer_Guid D_GUID;					-- УИ клиента 
		DECLARE @Company_Guid D_GUID;						-- УИ компании 
		DECLARE @CustomerStateType_Guid D_GUID;	-- УИ формы собственности
		DECLARE @CustomerCategory_Guid D_GUID;	-- УИ категории клиента

		DECLARE @Parts_GuidShema D_GUID;	-- УИ товара
		DECLARE @Suppl_Guid D_GUID;     -- УИ заказа
		DECLARE @WareHouse_Guid D_GUID;	-- УИ хранилища
		DECLARE @Customer_Id D_ID;      -- УИ клиента в InterBase
		DECLARE @Parts_Guid D_GUID;			-- УИ товара
		
		IF( @SupplItms_Guid IS NOT NULL )
			BEGIN
				SELECT @Suppl_Guid = Suppl_Guid, @Parts_GuidShema = Parts_Guid, @WareHouse_Guid = WAREHOUSE_GUID_ID, 
					@Customer_Id = CustomerID, @Parts_Guid = Parts_Guid, @Customer_Guid = Customer_Guid 
				FROM #PDASupplItms 
				WHERE SupplItms_Guid = @SupplItms_Guid;	
			END
		ELSE	
			BEGIN
				SELECT @Suppl_Guid = dbo.GetSupplIDFromXml( @SupplItmsInfo );  
				SELECT @Parts_GuidShema = dbo.GetPartsGuidFromXml( @SupplItmsInfo );  
				SELECT @WareHouse_Guid = dbo.GetWareHouseGuidFromXml( @SupplItmsInfo );  
				SELECT @Customer_Id = dbo.GetCustomerIDFromXml( @SupplItmsInfo );
				SELECT @Parts_Guid = dbo.GetPartsGuidFromXml( @SupplItmsInfo );
				SELECT @Customer_Guid = Customer_Guid FROM dbo.T_Customer WHERE Customer_Id = @Customer_Id;
			END

		SELECT @Company_Guid = dbo.GetCompanyGuidFromXml( @SupplItmsInfo );
		IF( ( @Customer_Guid IS NULL ) OR ( @Company_Guid IS NULL ) )
			BEGIN
				SET @ERROR_NUM = -1;
				SET @IsPossible = 0;
				EXEC sp_AddEventLogForCalcCreditLimit @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 0,	@RulePool_StepGuid = @RulePool_StepGuid;
				RETURN @ERROR_NUM;
	    END  
		
		IF( @Parts_Guid IS NULL )
      BEGIN
        SET @ERROR_NUM = 2;
				EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 114,	@RulePool_StepGuid = @RulePool_StepGuid;
        RETURN @ERROR_NUM;
      END     

		SELECT @CustomerStateType_Guid = CustomerStateType_Guid FROM dbo.T_Customer WHERE Customer_Guid = @Customer_Guid;
		SELECT Top 1 @CustomerCategory_Guid = CustomerCategory_Guid FROM dbo.T_CustomerCategoryCompany WHERE Customer_Guid = @Customer_Guid AND Company_Guid = @Company_Guid;
		
		-- если к правилу группы не привязаны, то считаем, что правило можно запускать
		IF NOT EXISTS ( SELECT * FROM dbo.T_RulePoolConditionGroup WHERE RulePool_StepGuid = @RulePool_StepGuid  )
			BEGIN
				SET @IsPossible = 1;
				SET @ERROR_NUM = 0;
				SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) + 'SP_CheckPossibleRunRuleCalculation правило не связано с группами - можно запускать.';
				EXEC sp_AddEventLogForCalcCreditLimit @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 0,	@RulePool_StepGuid = @RulePool_StepGuid, @EventDscrpn = @ERROR_MES;
				RETURN @ERROR_NUM;
			END
			
		-- добавляю проверку на хранилища
		SELECT @IsPossible = dbo.CheckWareHouseOkConditionForRule ( @WareHouse_Guid, @RulePool_StepGuid );
		IF( @IsPossible = 0 )
			BEGIN
				SET @IsPossible = 0;
				EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 113,	@RulePool_StepGuid = @RulePool_StepGuid;
	    END  
	    
		IF( @IsPossible = 1 )
			BEGIN
				-- мы проверили группы с объектами типа "хранилище", а теперь проверим группы с объектами типа "клиент"
				SELECT @IsPossible = dbo.CheckCustomerOkConditionForRule( @Customer_Guid, @RulePool_StepGuid );
				IF( @IsPossible = 0 )
					BEGIN
						SET @IsPossible = 0;
						EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 113,	@RulePool_StepGuid = @RulePool_StepGuid;
					END  
			END

		IF( @IsPossible = 1 )
			BEGIN
				-- мы проверили группы с объектами типа "клиент", а теперь проверим группы с объектами типа "форма собственности"
				SELECT @IsPossible = dbo.CheckCustomerStateTypeOkConditionForRule( @CustomerStateType_Guid, @RulePool_StepGuid );
				IF( @IsPossible = 0 )
					BEGIN
						SET @IsPossible = 0;
						EXEC sp_AddEventLogForCalcCreditLimit @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 0,	@RulePool_StepGuid = @RulePool_StepGuid;
					END  
			END
		
		IF( @IsPossible = 1 )
			BEGIN
				--теперь проверим группы с объектами типа "Категория клиента"
				SELECT @IsPossible = dbo.CheckCustomerCategoryOkConditionForRule( @CustomerCategory_Guid, @RulePool_StepGuid );
				IF( @IsPossible = 0 )
					BEGIN
						SET @IsPossible = 0;
						EXEC sp_AddEventLogForCalcCreditLimit @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 0,	@RulePool_StepGuid = @RulePool_StepGuid;
					END  
			END

	END TRY
	BEGIN CATCH
		SET @IsPossible = 0;
		SET @ERROR_NUM = ERROR_NUMBER();
		SET @ERROR_MES = ERROR_MESSAGE();
		EXEC sp_AddEventLogForCalcCreditLimit @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 0,	@RulePool_StepGuid = @RulePool_StepGuid, @EventDscrpn = @ERROR_MES;
		RETURN @ERROR_NUM;
	END CATCH;

	SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) + 'Результат: ' + Convert(nvarchar( 8 ), @IsPossible );

	EXEC sp_AddEventLogForCalcCreditLimit @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 0,	@RulePool_StepGuid = @RulePool_StepGuid, @EventDscrpn = @ERROR_MES;

	IF( @ERROR_NUM = 0 )
		SET @ERROR_MES = 'Успешное завершение операции.';
	RETURN @ERROR_NUM;
END
GO

/****** Object:  StoredProcedure [dbo].[sp_ProcessSupplItmsInRuleCalculations]    Script Date: 03/24/2012 19:08:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ProcessSupplItmsInRuleCalculations] 
	@SupplItms_Guid D_GUID = NULL,
  @SupplItmsInfo xml ( DOCUMENT InfoForCalcPriceSchema ) output,
  @DiscountListInfo xml ( DOCUMENT DiscountList ) = NULL output,

  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output

AS

BEGIN
SET NOCOUNT ON;

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = NULL;


	BEGIN TRY

    DECLARE @RulePool_StepGuid D_GUID;
    DECLARE @RulePool_StepId D_ID;
    DECLARE @RuleCalculation_Guid D_GUID;
    DECLARE @SuccessAction_Id D_ID;
    DECLARE @SuccessAction_StepGuid D_GUID;
    DECLARE @FailureAction_Id D_ID;
    DECLARE @FailureAction_StepGuid D_GUID;
    DECLARE @RulePool_StepParamsValue xml (DOCUMENT RuleParams);
    DECLARE @IsPossibleRunRule bit;
		DECLARE @bExit bit;
		DECLARE @CountRulesExec int;	-- счетчик отработанных правил
		DECLARE @RuleIsReadyForCheck bit;
		DECLARE @RulePoolStepName D_NAME;
		SET @bExit = 0;
		SET @CountRulesExec = 0;
		
		DECLARE @NextStepGuid D_GUID;
		SET @NextStepGuid = NULL;		
    
    DECLARE @Today D_DATE;
    SET @Today = ( SELECT GetDate() );
    -- Создаем курсор со списком активных правил, срок действия которых соответствует текущей дате 
    DECLARE crRulePool CURSOR 
    FOR SELECT RulePool_StepGuid, RulePool_StepId, RuleCalculation_Guid, SuccessAction_Id, SuccessAction_StepGuid, 
      FailureAction_Id, FailureAction_StepGuid, RulePool_StepParamsValue, RuleIsReadyForCheck
    FROM #RulePool
    ORDER BY RulePool_StepId;
    OPEN crRulePool;
    FETCH NEXT FROM crRulePool INTO @RulePool_StepGuid, @RulePool_StepId, @RuleCalculation_Guid, @SuccessAction_Id, 
			@SuccessAction_StepGuid, @FailureAction_Id, @FailureAction_StepGuid, @RulePool_StepParamsValue, @RuleIsReadyForCheck; 
    WHILE @@FETCH_STATUS = 0
      BEGIN
				
--				IF( ( ( @bExit = 0 ) AND ( @NextStepGuid = NULL ) ) OR ( ( @bExit = 0 ) AND (@NextStepGuid = @RulePool_StepGuid) ) )
				IF( ( @bExit = 0 ) AND ( ( @NextStepGuid IS NULL ) OR ( @NextStepGuid = @RulePool_StepGuid ) ) )

					BEGIN
						SET @NextStepGuid = NULL;
						
						IF( @RuleIsReadyForCheck = 1 )
							BEGIN
								-- это правило прошло пердварительную проверку, поэтому проверяем конкретную позицию
								EXEC dbo.sp_CheckPossibleRunRuleCalculation  @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @RulePool_StepGuid = @RulePool_StepGuid,
									@IsPossible = @IsPossibleRunRule output,  @ERROR_NUM = @ERROR_NUM output, @ERROR_MES = @ERROR_MES output;
									
							END
						ELSE
							BEGIN
								-- правило не прошло предварительную проверку, поэтому не будем проверять
								SET @IsPossibleRunRule = 0;
							END	
							
						---- проверим, стоит ли запускать хранимую процедуру, связанную с правилом
						--EXEC dbo.SP_CheckPossibleRunRuleCalculation @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @RulePool_StepGuid = @RulePool_StepGuid,
						--	@IsPossible = @IsPossibleRunRule output,  @ERROR_NUM = @ERROR_NUM output, @ERROR_MES = @ERROR_MES output;
					
						IF( @IsPossibleRunRule = 1 )	
							BEGIN
								-- проверка показала, что предварительные условия на выполнение правила выполнены
								-- запускаем правило, а точнее обращаемся к хранимой процедуре, которая с ним связана
								EXEC dbo.sp_ExecuteRuleCalculation @SupplItms_Guid = @SupplItms_Guid, @RuleCalculation_Guid = @RuleCalculation_Guid, @RulePool_StepGuid = @RulePool_StepGuid, @SPParamsInfo = @RulePool_StepParamsValue, 
									@SupplItmsInfo = @SupplItmsInfo output, @DiscountListInfo = @DiscountListInfo  output, @ERROR_NUM = @ERROR_NUM output, @ERROR_MES = @ERROR_MES output;
									
								-- теперь нужно проанализировать, что возвращает процедура, которая высчитывала цену
								IF( @ERROR_NUM = 0 )
									BEGIN

										SET @CountRulesExec = @CountRulesExec + 1;
										-- процедура удачно отработала и что-то вернула ( @SupplItmsInfo )
										-- теперь нам нужно определить, что делать после этого шага
										-- RulePoolAction_Id
										-- 0	Выход
										-- 1	Переход к следующему шагу
										-- 2	Переход к шагу № 
										IF( @SuccessAction_Id = 0 )
											BEGIN
												SET @bExit = 1;
												BREAK;
											END	
										ELSE IF( @SuccessAction_Id = 2 )
											BEGIN
												SET @NextStepGuid = @SuccessAction_StepGuid;
											END	
									END
								ELSE
									BEGIN
									  IF( @ERROR_NUM <> -1 ) -- правило не предназначено для этого случая
									    BEGIN
										    -- правило отработало с ошибкой
										    IF( @FailureAction_Id = 0 )
													BEGIN
														SET @bExit = 1;
														BREAK;
													END	
									    END
									END	
							END
					END
        
        FETCH NEXT FROM crRulePool INTO @RulePool_StepGuid, @RulePool_StepId, @RuleCalculation_Guid, @SuccessAction_Id, 
					@SuccessAction_StepGuid, @FailureAction_Id, @FailureAction_StepGuid, @RulePool_StepParamsValue, @RuleIsReadyForCheck;
      END 
    
    CLOSE crRulePool;
    DEALLOCATE crRulePool;
    
    IF( @CountRulesExec = 0 )
			BEGIN
				EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 120,	@RulePool_StepGuid = @RulePool_StepGuid;
				RETURN @ERROR_NUM;
			END

	END TRY
	BEGIN CATCH
		SET @ERROR_NUM = ERROR_NUMBER();
		SET @ERROR_MES = @ERROR_MES + nChar(13) + nChar(10)+  '[SP_ProcessSupplItmsInRuleCalculations], Текст ошибки: ' + ERROR_MESSAGE();

		EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 121,	@RulePool_StepGuid = @RulePool_StepGuid, @EventDscrpn = @ERROR_MES;
		RETURN @ERROR_NUM;
	END CATCH;

	IF( @ERROR_NUM = 0 )	
		SET @ERROR_MES = '[SP_ProcessSupplItmsInRuleCalculations] Успешное завершение операции.';
	RETURN @ERROR_NUM;
END

GO
GRANT EXECUTE ON [dbo].[sp_ProcessSupplItmsInRuleCalculations] TO [public]
GO

/****** Object:  UserDefinedFunction [dbo].[SetCustomerIDInXml]    Script Date: 03/24/2012 19:26:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[SetCustomerIDInXml] ( @doc xml (DOCUMENT InfoForCalcPriceSchema), @Customer_ID int )
returns xml
with execute as caller
as
begin
    DECLARE @existCustomerNode bit;
    SELECT @existCustomerNode = @doc.exist( '//InfoForCalc/Suppl' );
    IF( @existCustomerNode = 1 )
      BEGIN
        DECLARE @existCustomerID bit;
        SELECT @existCustomerID = @doc.exist( '(//InfoForCalc/Suppl/@CustomerID)[1]' );
        IF( @existCustomerID = 1 )
          BEGIN
            SET @doc.modify('replace value of (//InfoForCalc/Suppl/@CustomerID)[1] with sql:variable("@Customer_ID")');
          END
      END

 RETURN @doc;

end

GO
GRANT EXECUTE ON [dbo].[SetCustomerIDInXml] TO [public]
GO

/****** Object:  UserDefinedFunction [dbo].[SetOptInXml]    Script Date: 03/24/2012 19:27:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[SetOptInXml] ( @doc xml (DOCUMENT InfoForCalcPriceSchema), @Opt bit )
returns xml
with execute as caller
as
begin
    DECLARE @existCustomerNode bit;
    SELECT @existCustomerNode = @doc.exist( '//InfoForCalc/Suppl' );
    IF( @existCustomerNode = 1 )
      BEGIN
        DECLARE @existOpt bit;
        SELECT @existOpt = @doc.exist( '(//InfoForCalc/Suppl/@Opt)[1]' );
        IF( @existOpt = 1 )
          BEGIN
            SET @doc.modify('replace value of (//InfoForCalc/Suppl/@Opt)[1] with sql:variable("@Opt")');
          END
      END

 RETURN @doc;

end

GO
GRANT EXECUTE ON [dbo].[SetOptInXml] TO [public]
GO

/****** Object:  UserDefinedFunction [dbo].[SetSupplIDInXml]    Script Date: 03/24/2012 19:28:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[SetSupplIDInXml] ( @doc xml (DOCUMENT InfoForCalcPriceSchema), @SupplID uniqueidentifier )
returns xml
with execute as caller
as
begin
    DECLARE @existSupplNode bit;
    SELECT @existSupplNode = @doc.exist( '//InfoForCalc/Suppl' );
    IF( @existSupplNode = 1 )
      BEGIN
        DECLARE @existSupplID bit;
        SELECT @existSupplID = @doc.exist( '(//InfoForCalc/Suppl/@SupplID)[1]' );
        IF( @existSupplID = 1 )
          BEGIN
            DECLARE @strSupplId nvarchar(36);
            SET @strSupplId = CONVERT( nvarchar(36), @SupplID );
            SET @doc.modify('replace value of (//InfoForCalc/Suppl/@SupplID)[1] with sql:variable("@strSupplId")');
          END
      END

 RETURN @doc;

end

GO
GRANT EXECUTE ON [dbo].[SetSupplIDInXml] TO [public]
GO


/****** Object:  UserDefinedFunction [dbo].[SetSupplItmsIDInXml]    Script Date: 03/24/2012 19:28:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[SetSupplItmsIDInXml] ( @doc xml (DOCUMENT InfoForCalcPriceSchema), @SupplItmsID uniqueidentifier )
returns xml
with execute as caller
as
begin
    DECLARE @existSupplNode bit;
    SELECT @existSupplNode = @doc.exist( '//InfoForCalc/SupplItms' );
    IF( @existSupplNode = 1 )
      BEGIN
        DECLARE @existSupplID bit;
        SELECT @existSupplID = @doc.exist( '(//InfoForCalc/SupplItms/@SupplItmsID)[1]' );
        IF( @existSupplID = 1 )
          BEGIN
            DECLARE @strSupplId nvarchar(36);
            SET @strSupplId = CONVERT( nvarchar(36), @SupplItmsID );
            SET @doc.modify('replace value of (//InfoForCalc/SupplItms/@SupplItmsID)[1] with sql:variable("@strSupplId")');
          END
      END

 RETURN @doc;

end

GO
GRANT EXECUTE ON [dbo].[SetSupplItmsIDInXml] TO [public]
GO

/****** Object:  UserDefinedFunction [dbo].[SetPartsIDInXml]    Script Date: 03/24/2012 19:48:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[SetPartsIDInXml] ( @doc xml (DOCUMENT InfoForCalcPriceSchema), @Parts_ID int )
returns xml
with execute as caller
as
begin
  DECLARE @existSupplItmsNode bit;
  SELECT @existSupplItmsNode = @doc.exist( '//InfoForCalc/SupplItms' );
  IF( @existSupplItmsNode = 1 )
    BEGIN
      DECLARE @existPartsID bit;
      SELECT @existPartsID = @doc.exist( '(//InfoForCalc/SupplItms/@PartsID)[1]' );
      IF( @existPartsID = 1 )
        BEGIN
          SET @doc.modify('replace value of (//InfoForCalc/SupplItms/@PartsID)[1] with sql:variable("@Parts_ID")');
        END
    END

 RETURN @doc;

end

GO
GRANT EXECUTE ON [dbo].[SetPartsIDInXml] TO [public]
GO

/****** Object:  UserDefinedFunction [dbo].[SetPartsQtyInXml]    Script Date: 03/24/2012 19:49:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE FUNCTION [dbo].[SetPartsQtyInXml] ( @doc xml (DOCUMENT InfoForCalcPriceSchema), @PartsQty D_QUANTITY )
returns xml
with execute as caller
as
begin
  DECLARE @existSupplItmsNode bit;
  SELECT @existSupplItmsNode = @doc.exist( '//InfoForCalc/SupplItms' );
  IF( @existSupplItmsNode = 1 )
    BEGIN
      DECLARE @existQuantity bit;
      SELECT @existQuantity = @doc.exist( '(//InfoForCalc/SupplItms/@Quantity)[1]' );
      IF( @existQuantity = 1 )
        BEGIN
          SET @doc.modify('replace value of (//InfoForCalc/SupplItms/@Quantity)[1] with sql:variable("@PartsQty")');
        END
    END

 RETURN @doc;

end

GO
GRANT EXECUTE ON [dbo].[SetPartsQtyInXml] TO [public]
GO

/****** Object:  UserDefinedFunction [dbo].[SetPriceList_PriceInXml]    Script Date: 03/24/2012 19:53:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[SetPriceList_PriceInXml] ( @doc xml (DOCUMENT InfoForCalcPriceSchema), @PriceList_Price D_MONEY )
returns xml
with execute as caller
as
begin
  DECLARE @existPriceNode bit;
  SELECT @existPriceNode = @doc.exist( '//InfoForCalc/Price' );
  IF( @existPriceNode = 1 )
    BEGIN
      DECLARE @existPrice bit;
      SELECT @existPrice = @doc.exist( '(//InfoForCalc/Price/@PriceList_Price)[1]' );
      IF( @existPrice = 1 )
        BEGIN
          SET @doc.modify('replace value of (//InfoForCalc/Price/@PriceList_Price)[1] with sql:variable("@PriceList_Price")');
        END
    END

 RETURN @doc;

end


GO
GRANT EXECUTE ON [dbo].[SetPriceList_PriceInXml] TO [public]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[SetPriceList_PriceCurrencyInXml] ( @doc xml (DOCUMENT InfoForCalcPriceSchema), @PriceList_PriceCurrency D_MONEY )
returns xml
with execute as caller
as
begin
  DECLARE @existPriceNode bit;
  SELECT @existPriceNode = @doc.exist( '//InfoForCalc/Price' );
  IF( @existPriceNode = 1 )
    BEGIN
      DECLARE @existPriceCurrency bit;
      SELECT @existPriceCurrency = @doc.exist( '(//InfoForCalc/Price/@PriceList_PriceCurrency)[1]' );
      IF( @existPriceCurrency = 1 )
        BEGIN
          SET @doc.modify('replace value of (//InfoForCalc/Price/@PriceList_PriceCurrency)[1] with sql:variable("@PriceList_PriceCurrency")');
        END
    END

 RETURN @doc;

end

GO
GRANT EXECUTE ON [dbo].[SetPriceList_PriceCurrencyInXml] TO [public]
GO

/****** Object:  UserDefinedFunction [dbo].[SetPriceList_Price0InXml]    Script Date: 03/24/2012 19:53:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[SetPriceList_Price0InXml] ( @doc xml (DOCUMENT InfoForCalcPriceSchema), @PriceList_Price0 D_MONEY )
returns xml
with execute as caller
as
begin
  DECLARE @existPriceNode bit;
  SELECT @existPriceNode = @doc.exist( '//InfoForCalc/Price' );
  IF( @existPriceNode = 1 )
    BEGIN
      DECLARE @existPrice0 bit;
      SELECT @existPrice0 = @doc.exist( '(//InfoForCalc/Price/@PriceList_Price0)[1]' );
      IF( @existPrice0 = 1 )
        BEGIN
          SET @doc.modify('replace value of (//InfoForCalc/Price/@PriceList_Price0)[1] with sql:variable("@PriceList_Price0")');
        END
    END

 RETURN @doc;

end

GO
GRANT EXECUTE ON [dbo].[SetPriceList_Price0InXml] TO [public]
GO

/****** Object:  UserDefinedFunction [dbo].[SetPriceInXml]    Script Date: 03/24/2012 19:53:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[SetPriceInXml] ( @doc xml (DOCUMENT InfoForCalcPriceSchema), @Price D_MONEY )
returns xml
with execute as caller
as
begin
  DECLARE @existPriceNode bit;
  SELECT @existPriceNode = @doc.exist( '//InfoForCalc/Price' );
  IF( @existPriceNode = 1 )
    BEGIN
      DECLARE @existPrice bit;
      SELECT @existPrice = @doc.exist( '(//InfoForCalc/Price/@Price)[1]' );
      IF( @existPrice = 1 )
        BEGIN
          SET @doc.modify('replace value of (//InfoForCalc/Price/@Price)[1] with sql:variable("@Price")');
        END
    END

 RETURN @doc;

end

GO
GRANT EXECUTE ON [dbo].[SetPriceInXml] TO [public]
GO

/****** Object:  UserDefinedFunction [dbo].[SetPriceCurrencyInXml]    Script Date: 03/24/2012 19:53:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[SetPriceCurrencyInXml] ( @doc xml (DOCUMENT InfoForCalcPriceSchema), @PriceCurrency D_MONEY )
returns xml
with execute as caller
as
begin
  DECLARE @existPriceNode bit;
  SELECT @existPriceNode = @doc.exist( '//InfoForCalc/Price' );
  IF( @existPriceNode = 1 )
    BEGIN
      DECLARE @existPriceCurrency bit;
      SELECT @existPriceCurrency = @doc.exist( '(//InfoForCalc/Price/@PriceCurrency)[1]' );
      IF( @existPriceCurrency = 1 )
        BEGIN
          SET @doc.modify('replace value of (//InfoForCalc/Price/@PriceCurrency)[1] with sql:variable("@PriceCurrency")');
        END
    END

 RETURN @doc;

end

GO
GRANT EXECUTE ON [dbo].[SetPriceCurrencyInXml] TO [public]
GO

/****** Object:  UserDefinedFunction [dbo].[SetPrice0InXml]    Script Date: 03/24/2012 19:53:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[SetPrice0InXml] ( @doc xml (DOCUMENT InfoForCalcPriceSchema), @Price0 D_MONEY )
returns xml
with execute as caller
as
begin
  DECLARE @existPriceNode bit;
  SELECT @existPriceNode = @doc.exist( '//InfoForCalc/Price' );
  IF( @existPriceNode = 1 )
    BEGIN
      DECLARE @existPrice0 bit;
      SELECT @existPrice0 = @doc.exist( '(//InfoForCalc/Price/@Price0)[1]' );
      IF( @existPrice0 = 1 )
        BEGIN
          SET @doc.modify('replace value of (//InfoForCalc/Price/@Price0)[1] with sql:variable("@Price0")');
        END
    END

 RETURN @doc;

end

GO
GRANT EXECUTE ON [dbo].[SetPrice0InXml] TO [public]
GO

/****** Object:  UserDefinedFunction [dbo].[SetNDSPercentInXml]    Script Date: 03/24/2012 19:52:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[SetNDSPercentInXml] ( @doc xml (DOCUMENT InfoForCalcPriceSchema), @NDSPercent D_MONEY )
returns xml
with execute as caller
as
begin
  DECLARE @existPriceNode bit;
  SELECT @existPriceNode = @doc.exist( '//InfoForCalc/Price' );
  IF( @existPriceNode = 1 )
    BEGIN
      DECLARE @existMarkup bit;
      SELECT @existMarkup = @doc.exist( '(//InfoForCalc/Price/@NDSPercent)[1]' );
      IF( @existMarkup = 1 )
        BEGIN
          SET @doc.modify('replace value of (//InfoForCalc/Price/@NDSPercent)[1] with sql:variable("@NDSPercent")');
        END
    END

 RETURN @doc;

end

GO
GRANT EXECUTE ON [dbo].[SetNDSPercentInXml] TO [public]
GO

/****** Object:  UserDefinedFunction [dbo].[SetMarkupPercentInXml]    Script Date: 03/24/2012 19:52:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[SetMarkupPercentInXml] ( @doc xml (DOCUMENT InfoForCalcPriceSchema), @Markup decimal(10, 4) )
returns xml
with execute as caller
as
begin
  DECLARE @existPriceNode bit;
  SELECT @existPriceNode = @doc.exist( '//InfoForCalc/Price' );
  IF( @existPriceNode = 1 )
    BEGIN
      DECLARE @existMarkup bit;
      SELECT @existMarkup = @doc.exist( '(//InfoForCalc/Price/@MarkupPercent)[1]' );
      IF( @existMarkup = 1 )
        BEGIN
          SET @doc.modify('replace value of (//InfoForCalc/Price/@MarkupPercent)[1] with sql:variable("@Markup")');
        END
    END

 RETURN @doc;

end


GO
GRANT EXECUTE ON [dbo].[SetMarkupPercentInXml] TO [public]
GO

/****** Object:  UserDefinedFunction [dbo].[SetLimitDiscountPercentInXml]    Script Date: 03/24/2012 19:52:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[SetLimitDiscountPercentInXml] ( @doc xml (DOCUMENT InfoForCalcPriceSchema), @LimitDiscountPercent decimal(10, 4) )
returns xml
with execute as caller
as
begin
  DECLARE @existPriceNode bit;
  SELECT @existPriceNode = @doc.exist( '//InfoForCalc/Price' );
  IF( @existPriceNode = 1 )
    BEGIN
      DECLARE @existMarkup bit;
      SELECT @existMarkup = @doc.exist( '(//InfoForCalc/Price/@DiscountRetroPercent)[1]' );
      IF( @existMarkup = 1 )
        BEGIN
          SET @doc.modify('replace value of (//InfoForCalc/Price/@DiscountRetroPercent)[1] with sql:variable("@LimitDiscountPercent")');
        END
    END

 RETURN @doc;

end

GO
GRANT EXECUTE ON [dbo].[SetLimitDiscountPercentInXml] TO [public]
GO

/****** Object:  UserDefinedFunction [dbo].[SetLimitDiscountFullPercentInXml]    Script Date: 03/24/2012 19:52:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[SetLimitDiscountFullPercentInXml] ( @doc xml (DOCUMENT InfoForCalcPriceSchema), @LimitDiscountFullPercent decimal(10, 4) )
returns xml
with execute as caller
as
begin
  DECLARE @existPriceNode bit;
  SELECT @existPriceNode = @doc.exist( '//InfoForCalc/Price' );
  IF( @existPriceNode = 1 )
    BEGIN
      DECLARE @existMarkup bit;
      SELECT @existMarkup = @doc.exist( '(//InfoForCalc/Price/@DiscountFixPercent)[1]' );
      IF( @existMarkup = 1 )
        BEGIN
          SET @doc.modify('replace value of (//InfoForCalc/Price/@DiscountFixPercent)[1] with sql:variable("@LimitDiscountFullPercent")');
        END
    END

 RETURN @doc;

end

GO
GRANT EXECUTE ON [dbo].[SetLimitDiscountFullPercentInXml] TO [public]
GO

/****** Object:  UserDefinedFunction [dbo].[SetImporterInXml]    Script Date: 03/24/2012 19:52:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[SetImporterInXml] ( @doc xml (DOCUMENT InfoForCalcPriceSchema), @Importer bit )
returns xml
with execute as caller
as
begin
  DECLARE @existPriceNode bit;
  SELECT @existPriceNode = @doc.exist( '//InfoForCalc/Price' );
  IF( @existPriceNode = 1 )
    BEGIN
      DECLARE @existMarkup bit;
      SELECT @existMarkup = @doc.exist( '(//InfoForCalc/Price/@Importer)[1]' );
      IF( @existMarkup = 1 )
        BEGIN
          SET @doc.modify('replace value of (//InfoForCalc/Price/@Importer)[1] with sql:variable("@Importer")');
        END
    END

 RETURN @doc;

end


GO
GRANT EXECUTE ON [dbo].[SetImporterInXml] TO [public]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[SetDiscountTradeEqPercentInXml] ( @doc xml (DOCUMENT InfoForCalcPriceSchema), @DiscountTradeEqPercent decimal(10, 4) )
returns xml
with execute as caller
as
begin
  DECLARE @existPriceNode bit;
  SELECT @existPriceNode = @doc.exist( '//InfoForCalc/Price' );
  IF( @existPriceNode = 1 )
    BEGIN
      DECLARE @existMarkup bit;
      SELECT @existMarkup = @doc.exist( '(//InfoForCalc/Price/@DiscountTradeEqPercent)[1]' );
      IF( @existMarkup = 1 )
        BEGIN
          SET @doc.modify('replace value of (//InfoForCalc/Price/@DiscountTradeEqPercent)[1] with sql:variable("@DiscountTradeEqPercent")');
        END
    END

 RETURN @doc;

end


GO
GRANT EXECUTE ON [dbo].[SetDiscountTradeEqPercentInXml] TO [public]
GO

/****** Object:  UserDefinedFunction [dbo].[SetDiscountRetroPercentInXml]    Script Date: 03/24/2012 19:52:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[SetDiscountRetroPercentInXml] ( @doc xml (DOCUMENT InfoForCalcPriceSchema), @DiscountRetroPercent decimal(10, 4) )
returns xml
with execute as caller
as
begin
  DECLARE @existPriceNode bit;
  SELECT @existPriceNode = @doc.exist( '//InfoForCalc/Price' );
  IF( @existPriceNode = 1 )
    BEGIN
      DECLARE @existMarkup bit;
      SELECT @existMarkup = @doc.exist( '(//InfoForCalc/Price/@DiscountRetroPercent)[1]' );
      IF( @existMarkup = 1 )
        BEGIN
          SET @doc.modify('replace value of (//InfoForCalc/Price/@DiscountRetroPercent)[1] with sql:variable("@DiscountRetroPercent")');
        END
    END

 RETURN @doc;

end


GO
GRANT EXECUTE ON [dbo].[SetDiscountTradeEqPercentInXml] TO [public]
GO

/****** Object:  UserDefinedFunction [dbo].[SetDiscountPriceInXml]    Script Date: 03/24/2012 19:52:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[SetDiscountPriceInXml] ( @doc xml (DOCUMENT InfoForCalcPriceSchema), @Price D_MONEY )
returns xml
with execute as caller
as
begin
  DECLARE @existPriceNode bit;
  SELECT @existPriceNode = @doc.exist( '//InfoForCalc/Price' );
  IF( @existPriceNode = 1 )
    BEGIN
      DECLARE @existPrice bit;
      SELECT @existPrice = @doc.exist( '(//InfoForCalc/Price/@DiscountPrice)[1]' );
      IF( @existPrice = 1 )
        BEGIN
          SET @doc.modify('replace value of (//InfoForCalc/Price/@DiscountPrice)[1] with sql:variable("@Price")');
        END
    END

 RETURN @doc;

end


GO
GRANT EXECUTE ON [dbo].[SetDiscountPriceInXml] TO [public]
GO


/****** Object:  UserDefinedFunction [dbo].[SetDiscountPriceCurrencyInXml]    Script Date: 03/24/2012 19:52:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[SetDiscountPriceCurrencyInXml] ( @doc xml (DOCUMENT InfoForCalcPriceSchema), @PriceCurrency D_MONEY )
returns xml
with execute as caller
as
begin
  DECLARE @existPriceNode bit;
  SELECT @existPriceNode = @doc.exist( '//InfoForCalc/Price' );
  IF( @existPriceNode = 1 )
    BEGIN
      DECLARE @existPriceCurrency bit;
      SELECT @existPriceCurrency = @doc.exist( '(//InfoForCalc/Price/@DiscountPriceCurrency)[1]' );
      IF( @existPriceCurrency = 1 )
        BEGIN
          SET @doc.modify('replace value of (//InfoForCalc/Price/@DiscountPriceCurrency)[1] with sql:variable("@PriceCurrency")');
        END
    END

 RETURN @doc;

end


GO
GRANT EXECUTE ON [dbo].[SetDiscountPriceCurrencyInXml] TO [public]
GO

/****** Object:  UserDefinedFunction [dbo].[SetDiscountPercentInXml]    Script Date: 03/24/2012 19:51:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[SetDiscountPercentInXml] ( @doc xml (DOCUMENT InfoForCalcPriceSchema), @DiscountPercent decimal(10, 4) )
returns xml
with execute as caller
as
begin
  DECLARE @existPriceNode bit;
  SELECT @existPriceNode = @doc.exist( '//InfoForCalc/Price' );
  IF( @existPriceNode = 1 )
    BEGIN
      DECLARE @existMarkup bit;
      SELECT @existMarkup = @doc.exist( '(//InfoForCalc/Price/@DiscountPercent)[1]' );
      IF( @existMarkup = 1 )
        BEGIN
          SET @doc.modify('replace value of (//InfoForCalc/Price/@DiscountPercent)[1] with sql:variable("@DiscountPercent")');
        END
    END

 RETURN @doc;

end

GO
GRANT EXECUTE ON [dbo].[SetDiscountPercentInXml] TO [public]
GO

/****** Object:  UserDefinedFunction [dbo].[SetDiscountPercentByTypeInXml]    Script Date: 03/24/2012 19:51:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[SetDiscountPercentByTypeInXml] ( @doc xml (DOCUMENT DiscountList), @DiscountTypeValue int, @DiscountPercent decimal(10, 4) )
returns xml
with execute as caller
as
begin
    DECLARE @existDiscountItemNode bit;
    SELECT @existDiscountItemNode = @doc.exist( '//DiscountList/DiscountItem' );
    IF( @existDiscountItemNode = 1 )
      BEGIN
        IF( @DiscountTypeValue = 0 )
          SET @doc.modify('replace value of (//DiscountList/DiscountItem[@DiscountType=0]/@DiscountPercent)[1] with sql:variable("@DiscountPercent")')
        IF( @DiscountTypeValue = 1 )
          SET @doc.modify('replace value of (//DiscountList/DiscountItem[@DiscountType=1]/@DiscountPercent)[1] with sql:variable("@DiscountPercent")')
        IF( @DiscountTypeValue = 2 )
          SET @doc.modify('replace value of (//DiscountList/DiscountItem[@DiscountType=2]/@DiscountPercent)[1] with sql:variable("@DiscountPercent")')
        IF( @DiscountTypeValue = 3 )
          SET @doc.modify('replace value of (//DiscountList/DiscountItem[@DiscountType=3]/@DiscountPercent)[1] with sql:variable("@DiscountPercent")')
        IF( @DiscountTypeValue = 4 )
          SET @doc.modify('replace value of (//DiscountList/DiscountItem[@DiscountType=4]/@DiscountPercent)[1] with sql:variable("@DiscountPercent")')
        IF( @DiscountTypeValue = 5 )
          SET @doc.modify('replace value of (//DiscountList/DiscountItem[@DiscountType=5]/@DiscountPercent)[1] with sql:variable("@DiscountPercent")')
        IF( @DiscountTypeValue = 6 )
          SET @doc.modify('replace value of (//DiscountList/DiscountItem[@DiscountType=6]/@DiscountPercent)[1] with sql:variable("@DiscountPercent")')

      END

 RETURN @doc;

end

GO
GRANT EXECUTE ON [dbo].[SetDiscountPercentByTypeInXml] TO [public]
GO

/****** Object:  UserDefinedFunction [dbo].[SetDiscountFullPercentInXml]    Script Date: 03/24/2012 19:51:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[SetDiscountFullPercentInXml] ( @doc xml (DOCUMENT InfoForCalcPriceSchema), @DiscountFullPercent decimal(10, 4) )
returns xml
with execute as caller
as
begin
  DECLARE @existPriceNode bit;
  SELECT @existPriceNode = @doc.exist( '//InfoForCalc/Price' );
  IF( @existPriceNode = 1 )
    BEGIN
      DECLARE @existMarkup bit;
      SELECT @existMarkup = @doc.exist( '(//InfoForCalc/Price/@DiscountFullPercent)[1]' );
      IF( @existMarkup = 1 )
        BEGIN
          SET @doc.modify('replace value of (//InfoForCalc/Price/@DiscountFullPercent)[1] with sql:variable("@DiscountFullPercent")');
        END
    END

 RETURN @doc;

end


GO
GRANT EXECUTE ON [dbo].[SetDiscountFullPercentInXml] TO [public]
GO

/****** Object:  UserDefinedFunction [dbo].[SetDiscountFixPercentInXml]    Script Date: 03/24/2012 19:51:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[SetDiscountFixPercentInXml] ( @doc xml (DOCUMENT InfoForCalcPriceSchema), @DiscountFixPercent decimal(10, 4) )
returns xml
with execute as caller
as
begin
  DECLARE @existPriceNode bit;
  SELECT @existPriceNode = @doc.exist( '//InfoForCalc/Price' );
  IF( @existPriceNode = 1 )
    BEGIN
      DECLARE @existMarkup bit;
      SELECT @existMarkup = @doc.exist( '(//InfoForCalc/Price/@DiscountFixPercent)[1]' );
      IF( @existMarkup = 1 )
        BEGIN
          SET @doc.modify('replace value of (//InfoForCalc/Price/@DiscountFixPercent)[1] with sql:variable("@DiscountFixPercent")');
        END
    END

 RETURN @doc;

end

GO
GRANT EXECUTE ON [dbo].[SetDiscountFixPercentInXml] TO [public]
GO

/****** Object:  UserDefinedFunction [dbo].[SetDiscountComActionPercentInXml]    Script Date: 03/24/2012 19:51:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[SetDiscountComActionPercentInXml] ( @doc xml (DOCUMENT InfoForCalcPriceSchema), @DiscountComActionPercent decimal(10, 4) )
returns xml
with execute as caller
as
begin
  DECLARE @existPriceNode bit;
  SELECT @existPriceNode = @doc.exist( '//InfoForCalc/Price' );
  IF( @existPriceNode = 1 )
    BEGIN
      DECLARE @existMarkup bit;
      SELECT @existMarkup = @doc.exist( '(//InfoForCalc/Price/@DiscountComActionPercent)[1]' );
      IF( @existMarkup = 1 )
        BEGIN
          SET @doc.modify('replace value of (//InfoForCalc/Price/@DiscountComActionPercent)[1] with sql:variable("@DiscountComActionPercent")');
        END
    END

 RETURN @doc;

end

GO
GRANT EXECUTE ON [dbo].[SetDiscountComActionPercentInXml] TO [public]
GO

/****** Object:  UserDefinedFunction [dbo].[SetCurrencyRateInXml]    Script Date: 03/24/2012 19:51:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[SetCurrencyRateInXml] ( @doc xml (DOCUMENT InfoForCalcPriceSchema), @CurrencyRate D_MONEY )
returns xml
with execute as caller
as
begin
  DECLARE @existPriceNode bit;
  SELECT @existPriceNode = @doc.exist( '//InfoForCalc/Price' );
  IF( @existPriceNode = 1 )
    BEGIN
      DECLARE @existMarkup bit;
      SELECT @existMarkup = @doc.exist( '(//InfoForCalc/Price/@CurrencyRate)[1]' );
      IF( @existMarkup = 1 )
        BEGIN
          SET @doc.modify('replace value of (//InfoForCalc/Price/@CurrencyRate)[1] with sql:variable("@CurrencyRate")');
        END
    END

 RETURN @doc;

end


GO
GRANT EXECUTE ON [dbo].[SetCurrencyRateInXml] TO [public]
GO

/****** Object:  UserDefinedFunction [dbo].[SetCalcMarkUpPercentInXml]    Script Date: 03/24/2012 19:51:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- записывает размер расчитанной оптовой надбавки
CREATE FUNCTION [dbo].[SetCalcMarkUpPercentInXml] ( @doc xml (DOCUMENT InfoForCalcPriceSchema), @DiscountComActionPercent decimal(10, 4) )
returns xml
with execute as caller
as
begin
  DECLARE @existPriceNode bit;
  SELECT @existPriceNode = @doc.exist( '//InfoForCalc/Price' );
  IF( @existPriceNode = 1 )
    BEGIN
      DECLARE @existMarkup bit;
      SELECT @existMarkup = @doc.exist( '(//InfoForCalc/Price/@DiscountComActionPercent)[1]' );
      IF( @existMarkup = 1 )
        BEGIN
          SET @doc.modify('replace value of (//InfoForCalc/Price/@DiscountComActionPercent)[1] with sql:variable("@DiscountComActionPercent")');
        END
    END

 RETURN @doc;

end


GO
GRANT EXECUTE ON [dbo].[SetCalcMarkUpPercentInXml] TO [public]
GO

/****** Object:  UserDefinedFunction [dbo].[SetBonusSumInXml]    Script Date: 03/24/2012 19:51:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[SetBonusSumInXml] ( @doc xml (DOCUMENT InfoForCalcPriceSchema), @BonusSum D_MONEY )
returns xml
with execute as caller
as
begin
  DECLARE @existPriceNode bit;
  SELECT @existPriceNode = @doc.exist( '//InfoForCalc/Price' );
  IF( @existPriceNode = 1 )
    BEGIN
      DECLARE @existMarkup bit;
      SELECT @existMarkup = @doc.exist( '(//InfoForCalc/Price/@BonusSum)[1]' );
      IF( @existMarkup = 1 )
        BEGIN
          SET @doc.modify('replace value of (//InfoForCalc/Price/@BonusSum)[1] with sql:variable("@BonusSum")');
        END
    END

 RETURN @doc;

end

GO
GRANT EXECUTE ON [dbo].[SetBonusSumInXml] TO [public]
GO

/****** Object:  UserDefinedFunction [dbo].[SetBonusCurrencySumInXml]    Script Date: 03/24/2012 19:51:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[SetBonusCurrencySumInXml] ( @doc xml (DOCUMENT InfoForCalcPriceSchema), @BonusCurrencySum D_MONEY )
returns xml
with execute as caller
as
begin
  DECLARE @existPriceNode bit;
  SELECT @existPriceNode = @doc.exist( '//InfoForCalc/Price' );
  IF( @existPriceNode = 1 )
    BEGIN
      DECLARE @existMarkup bit;
      SELECT @existMarkup = @doc.exist( '(//InfoForCalc/Price/@BonusCurrencySum)[1]' );
      IF( @existMarkup = 1 )
        BEGIN
          SET @doc.modify('replace value of (//InfoForCalc/Price/@BonusCurrencySum)[1] with sql:variable("@BonusCurrencySum")');
        END
    END

 RETURN @doc;

end


GO
GRANT EXECUTE ON [dbo].[SetBonusCurrencySumInXml] TO [public]
GO

/****** Object:  StoredProcedure [dbo].[sp_ProcessPricesInPDASuppl]    Script Date: 03/24/2012 19:13:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- Обрабатывает содержиме заказа (прописывает цену в позициях)
--
-- Входящие параметры:
--	@Suppl_Guid - идентификатор заказа
--
-- Выходные параметры:
--  @ERROR_NUM - код ошибки
--  @ERROR_MES - текст ошибки
--
-- Результат:
--    0 - успешное завершение
--    <>0 - ошибка

CREATE PROCEDURE [dbo].[sp_ProcessPricesInPDASuppl] 
  @Suppl_Guid D_GUID,

  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output

AS

BEGIN
SET NOCOUNT ON;

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = '';
  
	BEGIN TRY

    IF NOT EXISTS ( SELECT * FROM dbo.T_Order WHERE Order_Guid = @Suppl_Guid )
      BEGIN
        SET @ERROR_NUM = 1;
        SET @ERROR_MES = 'Не найден заказ.' + Char(13) + 'Идентификатор заказа: ' + CONVERT( nvarchar(36), @Suppl_Guid );
        RETURN @ERROR_NUM;
      END

    -- определим код клиента и признак "оптовый заказ"
    DECLARE @Customer_Guid D_GUID;
    DECLARE @Company_Guid D_GUID;
    DECLARE @CustomerID D_ID;
    DECLARE @ChildDepart_Guid D_GUID;
    DECLARE @Opt bit;
    DECLARE @STOCK_GUID_ID D_GUID; 
    DECLARE @WAREHOUSE_GUID_ID D_GUID;
    DECLARE @PaymentType_Guid D_GUID;
    DECLARE @OrderState_Guid D_GUID;
    SET @Opt = 0;
    
    SELECT @CustomerID = Customer.Customer_Id, @ChildDepart_Guid = PDASuppl.CustomerChild_Guid, 
			@Customer_Guid = PDASuppl.Customer_Guid, 
			@STOCK_GUID_ID = PDASuppl.Stock_Guid, @WAREHOUSE_GUID_ID = Stock.Warehouse_Guid, 
			@Company_Guid = Stock.Company_Guid, @PaymentType_Guid = PDASuppl.PaymentType_Guid
    FROM dbo.T_Order as PDASuppl, dbo.T_Customer as Customer, dbo.T_Stock as Stock
    WHERE PDASuppl.Order_Guid = @Suppl_Guid
      AND PDASuppl.Customer_Guid = Customer.Customer_Guid
      AND PDASuppl.Stock_Guid = Stock.Stock_Guid
      
    DECLARE @PaymentTypeForm1Guid D_GUID;
    DECLARE @PaymentTypeForm2Guid D_GUID;
    
    SELECT @PaymentTypeForm1Guid = dbo.GetPaymentTypeForm1Guid();
    SELECT @PaymentTypeForm2Guid = dbo.GetPaymentTypeForm2Guid();
    
    IF( @PaymentType_Guid =  @PaymentTypeForm2Guid ) SET @Opt = 1;
    
    --IF( ( @ChildDepart_Guid IS NOT NULL ) AND ( @ChildDepart_Guid <> '00000000-0000-0000-0000-000000000000' ) ) SET @Opt = 1;
      
    DECLARE @doc xml ( DOCUMENT InfoForCalcPriceSchema );
    SET @doc = N'<?xml version="1.0" encoding="UTF-16"?>
      <InfoForCalc xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
	      <Suppl CustomerID="0" Opt="false" SupplID="00000000-0000-0000-0000-000000000000"/>
	      <SupplItms PartsID="0" Quantity="0"/>
	      <Price MarkupPercent="0" Price="0" Price0="0" PriceCurrency="0" DiscountPrice="0" DiscountPriceCurrency="0" PriceList_Price0="0" PriceList_Price="0" PriceList_PriceCurrency="0" DiscountPercent="0" NDSPercent="0" Importer="0" DiscountFullPercent="0" DiscountRetroPercent="0" DiscountFixPercent="0" DiscountTradeEqPercent="0" DiscountComActionPercent="0" CurrencyRate="0" BonusSum="0" BonusCurrencySum="0"/>
	      <Customer CustomerGuid="00000000-0000-0000-0000-000000000000"/>
	      <Company CompanyGuid="00000000-0000-0000-0000-000000000000"/>
	      <CreditLimit Money="0" MoneyApproved="0" Days="0" DaysApproved="0" CurrencyGuid="00000000-0000-0000-0000-000000000000"/>
      </InfoForCalc>
      ';
      
    -- переменная для хранения структуры скидок  
    DECLARE @docDiscountListInfo xml ( DOCUMENT DiscountList );
    SET @docDiscountListInfo = N'<?xml version="1.0" encoding="UTF-16"?>
      <DiscountList xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
	      <DiscountItem DiscountName="Скидка ретро" DiscountType="0" DiscountPercent="0" DiscountSum="0"/>
	      <DiscountItem DiscountName="Скидка фиксированная" DiscountType="1" DiscountPercent="0" DiscountSum="0"/>
	      <DiscountItem DiscountName="Скидка за оборудование" DiscountType="2" DiscountPercent="0" DiscountSum="0"/>
	      <DiscountItem DiscountName="Скидка промо" DiscountType="3" DiscountPercent="0" DiscountSum="0"/>
	      <DiscountItem DiscountName="Скидка по спеццене" DiscountType="4" DiscountPercent="0" DiscountSum="0"/>
	      <DiscountItem DiscountName="Скидка по акции" DiscountType="5" DiscountPercent="0" DiscountSum="0"/>
	      <DiscountItem DiscountName="Скидка за предоплату" DiscountType="6" DiscountPercent="0" DiscountSum="0"/>
      </DiscountList>
      ';
    
    -- пропишем значение кода клиента и признака "оптовый заказ"
    SELECT @doc = dbo.SetCustomerIDInXml( @doc, @CustomerID );
    SELECT @doc = dbo.SetOptInXml( @doc, @Opt );
    SELECT @doc = dbo.SetSupplIDInXml( @doc, @Suppl_Guid );
    
    -- заполним значения в @doc
    SELECT @doc = dbo.SetCustomerGuidInXml( @doc, @Customer_Guid );
    SELECT @doc = dbo.SetCompanyGuidInXml( @doc, @Company_Guid );
    --SELECT @doc = dbo.SetCreditLimitCurrencyInXml( @doc, @Currency_Guid );
    
    DECLARE @CustomerLimit_ApprovedCurrencyValue D_MONEY = 0;
    DECLARE @CustomerLimit_CurrencyValue D_MONEY = 0;
    DECLARE @CustomerLimit_ApprovedDays D_QUANTITY = 0;
    DECLARE @CustomerLimit_Days D_QUANTITY = 0;
    
		-- 2011.02.23
		-- создадим временную таблицу, куда поместим перечень правил, с пометкой "стоит ли использовать для проверки"
	CREATE TABLE #RulePool(  RulePool_StepGuid uniqueidentifier, RulePool_StepId int, RuleCalculation_Guid uniqueidentifier, SuccessAction_Id int, 
		SuccessAction_StepGuid uniqueidentifier, FailureAction_Id int, FailureAction_StepGuid uniqueidentifier, RulePool_StepParamsValue xml, 
		RuleIsReadyForCheck bit );

    DECLARE @RulePool_StepGuid D_GUID;
    DECLARE @RulePool_StepId D_ID;
    DECLARE @RuleCalculation_Guid D_GUID;
    DECLARE @SuccessAction_Id D_ID;
    DECLARE @SuccessAction_StepGuid D_GUID;
    DECLARE @FailureAction_Id D_ID;
    DECLARE @FailureAction_StepGuid D_GUID;
    DECLARE @RulePool_StepParamsValue xml;
    DECLARE @SupplItmsInfo_2 xml;
    DECLARE @SupplItms_Guid_2 D_GUID;
    DECLARE @DiscountListInfo_2 xml;
    DECLARE @RuleIsReadyForCheck bit;
    
    SET @SupplItmsInfo_2 = NULL;
    SET @SupplItms_Guid_2 = NULL;
    SET @DiscountListInfo_2 = NULL;
		
		DECLARE crRules CURSOR 
		FOR SELECT RulePool.RulePool_StepGuid, RulePool.RulePool_StepId, RulePool.RuleCalculation_Guid, RulePool.SuccessAction_Id, RulePool.SuccessAction_StepGuid, 
					RulePool.FailureAction_Id, RulePool.FailureAction_StepGuid, RulePool.RulePool_StepParamsValue
				FROM dbo.T_RulePool as RulePool, dbo.T_RuleCalculation as RuleCalculation
				WHERE RulePool.RulePool_StepEnable = 1 
					AND RulePool.RuleCalculation_Guid = RuleCalculation.RuleCalculation_Guid 
					AND dbo.TrimTime( GetDate() ) BETWEEN RulePool.RulePool_BeginDate AND RulePool.RulePool_EndDate
				ORDER BY RulePool.RulePool_StepId
		OPEN crRules;
		FETCH NEXT FROM crRules INTO @RulePool_StepGuid, @RulePool_StepId, @RuleCalculation_Guid, @SuccessAction_Id, 
			@SuccessAction_StepGuid, @FailureAction_Id, @FailureAction_StepGuid, @RulePool_StepParamsValue; 
		WHILE @@FETCH_STATUS = 0
			BEGIN
		    
				SET @RuleIsReadyForCheck = 0;
				EXEC dbo.SP_ExecuteRuleCalculation @Spl_Guid = @Suppl_Guid, @SupplItms_Guid = @SupplItms_Guid_2, @RuleCalculation_Guid = @RuleCalculation_Guid, @RulePool_StepGuid = @RulePool_StepGuid, @SPParamsInfo = @RulePool_StepParamsValue, 
					@SupplItmsInfo = @SupplItmsInfo_2 output, @DiscountListInfo = @DiscountListInfo_2  output, @ERROR_NUM = @ERROR_NUM output, @ERROR_MES = @ERROR_MES output;
		   
				IF( @ERROR_NUM = 777 ) SET @RuleIsReadyForCheck = 1;
				
				INSERT INTO #RulePool( RulePool_StepGuid , RulePool_StepId , RuleCalculation_Guid , SuccessAction_Id , 
					SuccessAction_StepGuid , FailureAction_Id , FailureAction_StepGuid , RulePool_StepParamsValue , RuleIsReadyForCheck )
				VALUES( @RulePool_StepGuid , @RulePool_StepId , @RuleCalculation_Guid , @SuccessAction_Id , 
					@SuccessAction_StepGuid , @FailureAction_Id , @FailureAction_StepGuid , @RulePool_StepParamsValue , @RuleIsReadyForCheck )	
		   
				FETCH NEXT FROM crRules INTO @RulePool_StepGuid, @RulePool_StepId, @RuleCalculation_Guid, @SuccessAction_Id, 
					@SuccessAction_StepGuid, @FailureAction_Id, @FailureAction_StepGuid, @RulePool_StepParamsValue;
			END 

		CLOSE crRules;
		DEALLOCATE crRules;		

    -- создадим временную таблицу, в которую поместим содержимое заказа
    CREATE TABLE #PDASupplItms( SupplItms_Guid uniqueidentifier, Parts_Guid uniqueidentifier, Parts_ID int,
      SupplItms_OrderQty float, SupplItms_Quantity float, 
      Suppl_Guid uniqueidentifier, Customer_Guid uniqueidentifier, CustomerID int, 
      ChildDepart_Guid uniqueidentifier, Opt bit, STOCK_GUID_ID uniqueidentifier, WAREHOUSE_GUID_ID uniqueidentifier);
    INSERT INTO #PDASupplItms( SupplItms_Guid, Parts_Guid, Parts_ID, SupplItms_OrderQty, SupplItms_Quantity, 
			Suppl_Guid, Customer_Guid, CustomerID, ChildDepart_Guid, Opt, STOCK_GUID_ID, WAREHOUSE_GUID_ID )  

    SELECT PDASupplItms.OrderItms_Guid, PDASupplItms.Parts_Guid, Parts.Parts_Id, 
			PDASupplItms.OrderItms_QuantityOrdered, PDASupplItms.OrderItms_Quantity, 
			@Suppl_Guid, @Customer_Guid, @CustomerID, @ChildDepart_Guid, @Opt, @STOCK_GUID_ID, @WAREHOUSE_GUID_ID
    FROM dbo.T_OrderItms as PDASupplItms, dbo.T_Parts as Parts
    WHERE PDASupplItms.Order_Guid = @Suppl_Guid
      AND PDASupplItms.Parts_Guid = Parts.Parts_Guid;
    
    -- для каждой строки заказа нужно определить цену
    DECLARE @SupplItms_Guid D_GUID;
    DECLARE @Parts_ID D_ID;
    DECLARE @Parts_Qty D_QUANTITY;
    DECLARE @SupplItms_PriceImporter D_MONEY;
    DECLARE @SupplItms_Price D_MONEY;
    DECLARE @SupplItms_CurrencyPrice D_MONEY;
    DECLARE @SupplItms_Discount D_MONEY;
    DECLARE @SupplItms_DiscountPrice D_MONEY;
    DECLARE @SupplItms_CurrencyDiscountPrice D_MONEY;
    DECLARE @CurrencyRate D_MONEY;
 
    DECLARE crSupplItms CURSOR 
    FOR SELECT SupplItms_Guid, Parts_ID, SupplItms_Quantity FROM #PDASupplItms WHERE SupplItms_Quantity <> 0;

    OPEN crSupplItms;
    FETCH NEXT FROM crSupplItms INTO @SupplItms_Guid, @Parts_ID, @Parts_Qty; 
    WHILE @@FETCH_STATUS = 0
      BEGIN
				BEGIN TRY

        SET @SupplItms_Price = 0;
        SET @SupplItms_Discount = 0;
        SET @SupplItms_CurrencyPrice = 0;
        SET @SupplItms_PriceImporter = 0;
        SET @CurrencyRate = 0;

        -- прописывае значение кода товара и количество
        SELECT @doc = dbo.SetPartsIDInXml( @doc, @Parts_ID );
        --SELECT @doc = dbo.SetSupplItmsIDInXml( @doc, @SupplItms_Guid );
        SELECT @doc = dbo.SetPartsQtyInXml( @doc, @Parts_Qty );
        SELECT @doc = dbo.SetDiscountPriceInXml( @doc, 0 );
        SELECT @doc = dbo.SetDiscountPriceCurrencyInXml( @doc, 0 );
        SELECT @doc = dbo.SetLimitDiscountPercentInXml( @doc, 0 );
        SELECT @doc = dbo.SetLimitDiscountFullPercentInXml( @doc, 0 );
        SELECT @doc = dbo.SetDiscountPercentInXml( @doc, 0 );
        SELECT @doc = dbo.SetDiscountFullPercentInXml( @doc, 0 );
        SELECT @doc = dbo.SetCurrencyRateInXml( @doc, 0 );
        SELECT @doc = dbo.SetImporterInXml( @doc, 0 );
        SELECT @doc = dbo.SetMarkupPercentInXml( @doc, 0 );
        SELECT @doc = dbo.SetNDSPercentInXml( @doc, 0 );
        SELECT @doc = dbo.SetPriceInXml( @doc, 0 );
        SELECT @doc = dbo.SetPriceCurrencyInXml( @doc, 0 );
        SELECT @doc = dbo.SetPriceList_Price0InXml( @doc, 0 );
        
        --  в структуре для скидок обнуляем все значения
        SELECT @docDiscountListInfo = dbo.SetDiscountPercentByTypeInXml( @docDiscountListInfo, 0, 0 );
        SELECT @docDiscountListInfo = dbo.SetDiscountPercentByTypeInXml( @docDiscountListInfo, 1, 0 );
        SELECT @docDiscountListInfo = dbo.SetDiscountPercentByTypeInXml( @docDiscountListInfo, 2, 0 );
        SELECT @docDiscountListInfo = dbo.SetDiscountPercentByTypeInXml( @docDiscountListInfo, 3, 0 );
        SELECT @docDiscountListInfo = dbo.SetDiscountPercentByTypeInXml( @docDiscountListInfo, 4, 0 );
        SELECT @docDiscountListInfo = dbo.SetDiscountPercentByTypeInXml( @docDiscountListInfo, 5, 0 );
        SELECT @docDiscountListInfo = dbo.SetDiscountPercentByTypeInXml( @docDiscountListInfo, 6, 0 );
      
        -- здесь нужно запустить хранимую процедуру, которая прогонит xml-структуру через список правил
        -- в итоге, если не возникнет ошибок мы можем запросить из этой xml-структуры цену
        
        --EXEC dbo.SP_ProcessSupplItmsInRuleCalculations @SupplItms_Guid = NULL, @SupplItmsInfo = @doc output, @DiscountListInfo = @docDiscountListInfo output, @ERROR_NUM = @ERROR_NUM output, @ERROR_MES = @ERROR_MES output

        EXEC dbo.SP_ProcessSupplItmsInRuleCalculations @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @doc output, @DiscountListInfo = @docDiscountListInfo output, @ERROR_NUM = @ERROR_NUM output, @ERROR_MES = @ERROR_MES output

        -- ВНИМАНИЕ!!!
        -- В данный момент определены не все атрибуты цены
        -- это значит, что в схеме будут доопределены необходимые атрибуты,
        -- значения которых будут задействованы в позиции заказа
                
        IF( ( @ERROR_NUM = 0 ) OR ( @ERROR_NUM = -1 ) )
          BEGIN
            SELECT @SupplItms_Price = dbo.GetPriceFromXml( @doc );
            SELECT @SupplItms_Discount = dbo.GetDiscountPercentFromXml( @doc );
            SELECT @SupplItms_CurrencyPrice = dbo.GetPriceCurrencyFromXml( @doc );
            SELECT @SupplItms_DiscountPrice = dbo.GetDiscountPriceFromXml( @doc );
            SELECT @SupplItms_CurrencyDiscountPrice = dbo.GetDiscountPriceCurrencyFromXml( @doc );
            SELECT @SupplItms_PriceImporter = dbo.GetPrice0FromXml( @doc );
            SELECT @CurrencyRate = dbo.GetCurrencyRateFromXml( @doc );
           
            -- прописываем расчитанное значение цены
            UPDATE dbo.T_OrderItms SET OrderItms_Price = @SupplItms_Price, 
							OrderItms_PriceInAccountingCurrency = @SupplItms_CurrencyPrice, 
              OrderItms_DiscountPercent = @SupplItms_Discount, 
              OrderItms_PriceWithDiscount = @SupplItms_DiscountPrice, 
							OrderItms_PriceWithDiscountInAccountingCurrency = @SupplItms_CurrencyDiscountPrice, 
							OrderItms_XMLPrice = @doc, 
							OrderItms_XMLDiscount = @docDiscountListInfo
            WHERE OrderItms_Guid = @SupplItms_Guid;

          END 
        ELSE
          BEGIN
            UPDATE dbo.T_OrderItms SET OrderItms_XMLPrice = @doc, OrderItms_Quantity = 0, 
							OrderItms_XMLDiscount = @docDiscountListInfo  WHERE OrderItms_Guid = @SupplItms_Guid;
						EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @doc, @EventType_Id = 134;
          END  

					END TRY
					BEGIN CATCH
						CLOSE crSupplItms;
						DEALLOCATE crSupplItms;

						SET @ERROR_NUM = ERROR_NUMBER();
						SET @ERROR_MES = @ERROR_MES + nChar(13) + nChar(10) +  '[sp_ProcessPricesInPDASuppl] Текст ошибки: ' + ERROR_MESSAGE();

						EXEC sp_AddEventLogForCalcPrice @SupplItmsInfo = @doc, @EventType_Id = 133,	@EventDscrpn = @ERROR_MES;
						RETURN @ERROR_NUM;
					END CATCH;
       FETCH NEXT FROM crSupplItms INTO @SupplItms_Guid, @Parts_ID, @Parts_Qty;   
      END 
    
    CLOSE crSupplItms;
    DEALLOCATE crSupplItms;

		DROP TABLE #PDASupplItms;
		DROP TABLE #RulePool; 

    DECLARE @Order_Quantity D_QUANTITY = 0; 
    DECLARE @Order_SumReserved D_MONEY = 0; 
    DECLARE @Order_SumReservedWithDiscount D_MONEY = 0;
    DECLARE @Order_SumReservedInAccountingCurrency D_MONEY = 0; 
    DECLARE @Order_SumReservedWithDiscountInAccountingCurrency D_MONEY = 0;
    
    SELECT @Order_Quantity = SUM( OrderItms_Quantity ), @Order_SumReserved = SUM( Orderitms_SumReserved ), 
			@Order_SumReservedWithDiscount = SUM( Orderitms_SumReservedWithDiscount ), 
			@Order_SumReservedInAccountingCurrency = SUM( Orderitms_SumReservedInAccountingCurrency ),
			@Order_SumReservedWithDiscountInAccountingCurrency = SUM( Orderitms_SumReservedWithDiscountInAccountingCurrency )
		FROM dbo.T_OrderItms
		WHERE Order_Guid = @Suppl_Guid;	
    
    IF( @Order_Quantity IS NULL ) SET @Order_Quantity = 0;
    IF( @Order_SumReserved IS NULL ) SET @Order_SumReserved = 0;
    IF( @Order_SumReservedWithDiscount IS NULL ) SET @Order_SumReservedWithDiscount = 0;
    IF( @Order_SumReservedInAccountingCurrency IS NULL ) SET @Order_SumReservedInAccountingCurrency = 0;
    IF( @Order_SumReservedWithDiscountInAccountingCurrency IS NULL ) SET @Order_SumReservedWithDiscountInAccountingCurrency = 0;

    UPDATE dbo.T_Order SET Order_Quantity = @Order_Quantity, Order_SumReserved = @Order_SumReserved, 
			Order_SumReservedWithDiscount = @Order_SumReservedWithDiscount, 
      Order_SumReservedInAccountingCurrency = @Order_SumReservedInAccountingCurrency, 
      Order_SumReservedWithDiscountInAccountingCurrency = @Order_SumReservedWithDiscountInAccountingCurrency
    WHERE Order_Guid = @Suppl_Guid;  
    
    
    IF EXISTS( SELECT * FROM dbo.T_OrderItms 
        WHERE Order_Guid = @Suppl_Guid 
					AND ( ( ( OrderItms_Price = 0 ) OR ( OrderItms_PriceInAccountingCurrency = 0 ) ) 
					AND OrderItms_Quantity <> 0 ) )
			BEGIN
				SELECT @OrderState_Guid = OrderState_Guid FROM dbo.T_OrderState WHERE OrderState_Id = 7;
				UPDATE dbo.T_Order SET OrderState_Guid = @OrderState_Guid	WHERE Order_Guid = @Suppl_Guid;

        SET @ERROR_NUM = 2;
        SET @ERROR_MES = @ERROR_MES + nChar(13) + nChar(10) + 'В заказе присутсвуют позиции с нулевой ценой.';
				EXEC sp_AddEventLogForCalcPrice @SupplItmsInfo = @doc, @EventType_Id = 132;
			END
		ELSE
			BEGIN
				SELECT @OrderState_Guid = OrderState_Guid FROM dbo.T_OrderState WHERE OrderState_Id = 60;
				UPDATE dbo.T_Order SET OrderState_Guid = @OrderState_Guid	WHERE Order_Guid = @Suppl_Guid;
				SET @ERROR_NUM = 0;
			END	
	
	

	END TRY
	BEGIN CATCH
		SET @ERROR_NUM = ERROR_NUMBER();
		SET @ERROR_MES = @ERROR_MES + nChar(13) + nChar(10) +  '[sp_ProcessPricesInPDASuppl] Текст ошибки: ' + ERROR_MESSAGE();

		EXEC sp_AddEventLogForCalcPrice @SupplItmsInfo = @doc, @EventType_Id = 131,	@EventDscrpn = @ERROR_MES;
		RETURN @ERROR_NUM;
	END CATCH;

	--SET @ERROR_NUM = 0;
	IF( @ERROR_NUM = 0 )
		BEGIN
			SET @ERROR_MES = '[sp_ProcessPricesInPDASuppl] Успешно расчитаны цены в заказе.';
			EXEC sp_AddEventLogForCalcPrice @SupplItmsInfo = @doc, @EventType_Id = 130;
		END	
	RETURN @ERROR_NUM;
END

GO
GRANT EXECUTE ON [dbo].[sp_ProcessPricesInPDASuppl] TO [public]
GO

INSERT INTO dbo.T_EVENTCATEGORY( GUID_ID, EVENTCATEGORY_NAME )  
VALUES('FC062978-1F6A-42D6-9301-31961C26814D', 'Расчет цены в заказе');

INSERT INTO dbo.T_EVENTCATEGORY( GUID_ID, EVENTCATEGORY_NAME )  
VALUES('2014660C-3592-436A-A7BE-90EF96DD9194', 'Редактирование данных торгового представителя');

INSERT INTO dbo.T_EVENTCATEGORY( GUID_ID, EVENTCATEGORY_NAME )  
VALUES('DA7A80EB-1107-48B1-ACEF-D1D0762FED05', 'Редактирование заказа');

INSERT INTO dbo.T_EVENTCATEGORY( GUID_ID, EVENTCATEGORY_NAME )  
VALUES('A5FD0604-03EB-471F-ABB1-FCB93BDFFF0C', 'Неизвестная категория');

  
INSERT INTO dbo.T_EVENTINFOTYPE( GUID_ID, EVENTINFOTYPE_NAME )
VALUES( '4C761248-DC02-48E2-938B-2CF0468F3C01', 'Предупреждение' );

INSERT INTO dbo.T_EVENTINFOTYPE( GUID_ID, EVENTINFOTYPE_NAME )
VALUES( '5FFA20F2-D5BA-4AB5-B4F3-43B686E4116E', 'Информация' );

INSERT INTO dbo.T_EVENTINFOTYPE( GUID_ID, EVENTINFOTYPE_NAME )
VALUES( 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720', 'Ошибка' );

INSERT INTO dbo.T_EVENTINFOTYPE( GUID_ID, EVENTINFOTYPE_NAME )
VALUES( '028BBC5A-EC9A-4F1C-A74F-C08D6E5218FF', 'None' );  



INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 0, 'Неизвестный тип сообщения', 'Используется для совместимости с более ранней версией журнала сообщений', 'A5FD0604-03EB-471F-ABB1-FCB93BDFFF0C', '028BBC5A-EC9A-4F1C-A74F-C08D6E5218FF');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 1, 'Позия в заказе получает фиксированную скидку', 'Расчет фиксированной скидки', 'FC062978-1F6A-42D6-9301-31961C26814D', '5FFA20F2-D5BA-4AB5-B4F3-43B686E4116E');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 2, 'Ошибка расчета фиксированной скидки', 'Расчет фиксированной скидки', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 4, 'Завершен расчет ретро-скидки', 'Расчет ретро-скидки на месяц', 'FC062978-1F6A-42D6-9301-31961C26814D', '5FFA20F2-D5BA-4AB5-B4F3-43B686E4116E');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 5, 'Ошибка расчета ретро-скидки', 'Расчет ретро-скидки на месяц', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 30, 'Позиция в заказе получает ретро-скидку', 'Расчет ретро-скидки', 'FC062978-1F6A-42D6-9301-31961C26814D', '5FFA20F2-D5BA-4AB5-B4F3-43B686E4116E');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 31, 'Ошибка расчета ретро-скидки', 'Расчет ретро-скидки', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 40, 'Позиция в заказе получает скидку за оборудование', 'Расчет скидки за оборудование', 'FC062978-1F6A-42D6-9301-31961C26814D', '5FFA20F2-D5BA-4AB5-B4F3-43B686E4116E');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 41, 'Ошибка расчета скидки за оборудование', 'Расчет скидки за оборудование', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 50, 'Позиция в заказе получает спец. цену', 'Расчет спец. цены', 'FC062978-1F6A-42D6-9301-31961C26814D', '5FFA20F2-D5BA-4AB5-B4F3-43B686E4116E');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 51, 'Ошибка расчета спец. цены', 'Расчет спец. цены', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 52, 'Заказ не относится к категории "наличный заказ". Обработка не будет производится.', 'Расчет спец. цены', 'FC062978-1F6A-42D6-9301-31961C26814D', '5FFA20F2-D5BA-4AB5-B4F3-43B686E4116E');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 53, 'Позиция не попадает под спеццену.', 'Расчет спец. цены', 'FC062978-1F6A-42D6-9301-31961C26814D', '5FFA20F2-D5BA-4AB5-B4F3-43B686E4116E');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 54, 'Не все дополнительные параметры определены', 'Расчет спец. цены', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 55, 'Не все входные параметры определены.', 'Расчет спец. цены', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 56, 'Клиент не входит в разрешенную группу, связанную с правилом.', 'Расчет спец. цены', 'FC062978-1F6A-42D6-9301-31961C26814D', '5FFA20F2-D5BA-4AB5-B4F3-43B686E4116E');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 60, 'Позиция в заказе получает акционную скидку', 'Расчет скидки по акции', 'FC062978-1F6A-42D6-9301-31961C26814D', '5FFA20F2-D5BA-4AB5-B4F3-43B686E4116E');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 61, 'Позиция в заказе получает акционную скидку к уже имеющимся скидкам', 'Расчет скидки по акции', 'FC062978-1F6A-42D6-9301-31961C26814D', '5FFA20F2-D5BA-4AB5-B4F3-43B686E4116E');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 62, 'Позиция в заказе не получает акционную скидку', 'Расче скидки по акции', 'FC062978-1F6A-42D6-9301-31961C26814D', '4C761248-DC02-48E2-938B-2CF0468F3C01');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 63, 'Клиент не входит в разрешенную группу, связанную с правилом.', 'Расчет скидки по акции', 'FC062978-1F6A-42D6-9301-31961C26814D', '5FFA20F2-D5BA-4AB5-B4F3-43B686E4116E');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 64, 'Ошибка расчета скидки по акции', 'Расчет скидки по акции', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 65, 'Не все дополнительные параметры определены', 'Расчет скидки по акции', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 66, 'Не все входные параметры определены.', 'Расчет скидки по акции', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 67, 'Заказ не относится к категории "безналичный" или клиент не является "розничным". Обработка не будет производится.', 'Расчет скидки по акции', 'FC062978-1F6A-42D6-9301-31961C26814D', '4C761248-DC02-48E2-938B-2CF0468F3C01');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 68, 'Заказ не является "виртуальным набором". Обработка производится не будет.', 'Расчет скидки по акции', 'FC062978-1F6A-42D6-9301-31961C26814D', '4C761248-DC02-48E2-938B-2CF0468F3C01');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 70, 'Для позиции в заказе расчитана цена по форме оплаты №2', 'Расчет цены по форме оплаты №2', 'FC062978-1F6A-42D6-9301-31961C26814D', '5FFA20F2-D5BA-4AB5-B4F3-43B686E4116E');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 71, 'Ошибка расчета цены по форме оплаты №2', 'Расчет цены по форме оплаты №2', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 72, 'Недопустимая цена товара', 'Расчет цены по форме оплаты №2', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 73, 'Недопуситмое количество товара', 'Расчет цены по форме оплаты №2', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 74, 'Не удалось определить ставку НДС', 'Расчет цены по форме оплаты №2', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 75, 'Не удалось определить признак Импортер', 'Расчет цены по форме оплаты №2', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 76, 'Товар с заданным идентификатором не найден в справочнике', 'Расчет цены по форме оплаты №2', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 77, 'Клиент с заданным идентификатором не найден в справочнике', 'Расчет цены по форме оплаты №2', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 78, 'Недопустимое значение входных параметров', 'Расчет цены по форме оплаты №2', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 79, 'Не удалось определить идентификатор склада', 'Расчет цены по форме оплаты №2', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 80, 'Заказ не относится к категории "форма оплаты №2"', 'Расчет цены по форме оплаты №2', 'FC062978-1F6A-42D6-9301-31961C26814D', '4C761248-DC02-48E2-938B-2CF0468F3C01');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 81, 'Не удалось определить идентификатор заказа', 'Расчет цены по форме оплаты №2', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 90, 'Для позиции в заказе расчитана цена по форме оплаты №1', 'Расчет цены по форме оплаты №1', 'FC062978-1F6A-42D6-9301-31961C26814D', '5FFA20F2-D5BA-4AB5-B4F3-43B686E4116E');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 91, 'Ошибка расчета цены по форме оплаты №1', 'Расчет цены по форме оплаты №1', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 92, 'Недопустимая цена товара', 'Расчет цены по форме оплаты №1', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 93, 'Недопуситмое количество товара', 'Расчет цены по форме оплаты №1', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 94, 'Не удалось определить ставку НДС', 'Расчет цены по форме оплаты №1', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 95, 'Не удалось определить признак Импортер', 'Расчет цены по форме оплаты №1', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 96, 'Товар с заданным идентификатором не найден в справочнике', 'Расчет цены по форме оплаты №1', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 97, 'Клиент с заданным идентификатором не найден в справочнике', 'Расчет цены по форме оплаты №1', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 98, 'Недопустимое значение входных параметров', 'Расчет цены по форме оплаты №1', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 99, 'Не удалось определить идентификатор склада', 'Расчет цены по форме оплаты №1', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 100, 'Заказ не относится к категории "форма оплаты №2"', 'Расчет цены по форме оплаты №1', 'FC062978-1F6A-42D6-9301-31961C26814D', '4C761248-DC02-48E2-938B-2CF0468F3C01');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 101, 'Не удалось определить идентификатор заказа', 'Расчет цены по форме оплаты №1', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 102, 'Не удалось определить размер надбавки', 'Расчет цены по форме оплаты №1', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 110, 'Завершена проверка на выполнение условий', 'Проверка на доступ к правилу', 'FC062978-1F6A-42D6-9301-31961C26814D', '5FFA20F2-D5BA-4AB5-B4F3-43B686E4116E');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 111, 'Ошибка проверки на выполнение условий', 'Проверка на доступ к правилу', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 112, 'Товар не входит в разрешенную группу, связанную с правилом.', 'Проверка на доступ к правилу', 'FC062978-1F6A-42D6-9301-31961C26814D', '5FFA20F2-D5BA-4AB5-B4F3-43B686E4116E');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 113, 'Клиент не входит в разрешенную группу, связанную с правилом.', 'Проверка на доступ к правилу', 'FC062978-1F6A-42D6-9301-31961C26814D', '5FFA20F2-D5BA-4AB5-B4F3-43B686E4116E');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 114, 'Не удалось определить идентификатор товара', 'Проверка на доступ к правилу', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 115, 'Не удалось определить идентификатор клиента', 'Проверка на доступ к правилу', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 120, 'Количество отработанных правил для позиции заказа ноль!', 'Обработка позиции заказа', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 121, 'Ошибка при обработке позиции заказа', 'Обработка позиции заказа', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 122, 'Ошибка при обработке позиции заказа', 'Обработка позиции заказа', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 130, 'Успешно расчитаны цены в заказе', 'Обработка заказа (цены)', 'FC062978-1F6A-42D6-9301-31961C26814D', '5FFA20F2-D5BA-4AB5-B4F3-43B686E4116E');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 131, 'Ошибка при расчете цен в заказе', 'Обработка заказа (цены)', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 132, 'В заказе присутсвуют позиции с нулевой ценой.', 'Обработка заказа (цены)', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 133, 'Ошибка при обработке позиции заказа', 'Обработка заказа (цены)', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 134, 'Для позиции заказа не определена цена.', 'Обработка заказа (цены)', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 135, 'Завершена расчет цен в заказе', 'Обработка заказа (цены)', 'FC062978-1F6A-42D6-9301-31961C26814D', '5FFA20F2-D5BA-4AB5-B4F3-43B686E4116E');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 136, 'Результат автоматической переброски отличен от желаемого', 'Обработка заказа (цены)', 'FC062978-1F6A-42D6-9301-31961C26814D', '4C761248-DC02-48E2-938B-2CF0468F3C01');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 137, 'Ошибка обработки заказа', 'Обработка заказа (цены)', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 138, 'Ошибка автоматической переброски', 'Обработка заказа (цены)', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 140, 'Успешно создан протокол в InterBase', 'Обработка заказа (цены)', 'FC062978-1F6A-42D6-9301-31961C26814D', '5FFA20F2-D5BA-4AB5-B4F3-43B686E4116E');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 141, 'Ошибка создания протокола в InterBase', 'Обработка заказа (цены)', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 142, 'Создана "шапка" протокола в InterBase', 'Обработка заказа (цены)', 'FC062978-1F6A-42D6-9301-31961C26814D', '5FFA20F2-D5BA-4AB5-B4F3-43B686E4116E');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 143, 'Создана приложение к протоколу в InterBase', 'Обработка заказа (цены)', 'FC062978-1F6A-42D6-9301-31961C26814D', '5FFA20F2-D5BA-4AB5-B4F3-43B686E4116E');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 144, 'Пересчитана  "шапка" протокола в InterBase', 'Обработка заказа (цены)', 'FC062978-1F6A-42D6-9301-31961C26814D', '5FFA20F2-D5BA-4AB5-B4F3-43B686E4116E');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 145, 'Заказ в InterBase удален, так как количество в заказе равно ноль.', 'Обработка заказа (цены)', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 146, 'Не удалось пересчитать шапку протокола в InterBase.', 'Обработка заказа (цены)', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 147, 'Успешное удаление протокола в InterBase', 'Обработка заказа (цены)', 'FC062978-1F6A-42D6-9301-31961C26814D', '5FFA20F2-D5BA-4AB5-B4F3-43B686E4116E');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 148, 'Ошибка удаления протокола в InterBase', 'Обработка заказа (цены)', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 149, 'Заказ помечен как удаленный', 'Обработка заказа (цены)', 'FC062978-1F6A-42D6-9301-31961C26814D', '5FFA20F2-D5BA-4AB5-B4F3-43B686E4116E');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 150, 'Автоматическая обработка заявок завершена', 'Автоматическая обработка заявок', 'FC062978-1F6A-42D6-9301-31961C26814D', '5FFA20F2-D5BA-4AB5-B4F3-43B686E4116E');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 160, 'В заказе обнулены цены', 'Обработка заказа (цены)', 'FC062978-1F6A-42D6-9301-31961C26814D', '5FFA20F2-D5BA-4AB5-B4F3-43B686E4116E');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 161, 'Ошибка обнуления цен в заказе', 'Обработка заказа (цены)', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 170, 'Кредитный контроль не пройден', 'Обработка заказа (кредитный контроль)', 'FC062978-1F6A-42D6-9301-31961C26814D', '4C761248-DC02-48E2-938B-2CF0468F3C01');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 171, 'Успешно пройден кредитный контроль', 'Обработка заказа (кредитный контроль)', 'FC062978-1F6A-42D6-9301-31961C26814D', '5FFA20F2-D5BA-4AB5-B4F3-43B686E4116E');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 300, 'Успешно создано новое подразделение', 'Подразделение ТП', '2014660C-3592-436A-A7BE-90EF96DD9194', '5FFA20F2-D5BA-4AB5-B4F3-43B686E4116E');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 301, 'Ошибка создания нового подразделения', 'Подразделение ТП', '2014660C-3592-436A-A7BE-90EF96DD9194', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 302, 'Ошибка добавления нового кода в InterBase', 'Подразделение ТП', '2014660C-3592-436A-A7BE-90EF96DD9194', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 305, 'Успешно изменено описание подразделения', 'Подразделение ТП', '2014660C-3592-436A-A7BE-90EF96DD9194', '5FFA20F2-D5BA-4AB5-B4F3-43B686E4116E');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 306, 'Ошибка изменения описания подразделения', 'Подразделение ТП', '2014660C-3592-436A-A7BE-90EF96DD9194', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 307, 'Ошибка изменения кода подразделения в InterBase', 'Подразделение ТП', '2014660C-3592-436A-A7BE-90EF96DD9194', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 308, 'У подразделения отменены все клиенты', 'Подразделение ТП', '2014660C-3592-436A-A7BE-90EF96DD9194', '5FFA20F2-D5BA-4AB5-B4F3-43B686E4116E');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 309, 'Ошибка отмены клиентов у подразделения', 'Подразделение ТП', '2014660C-3592-436A-A7BE-90EF96DD9194', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 310, 'Успешно создан новый торговый представитель', 'Торговй представитель', '2014660C-3592-436A-A7BE-90EF96DD9194', '5FFA20F2-D5BA-4AB5-B4F3-43B686E4116E');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 311, 'Ошибка создания нового торгового представителя', 'Торговй представитель', '2014660C-3592-436A-A7BE-90EF96DD9194', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 312, 'Ошибка добавления нового торгового представителя в InterBase', 'Торговй представитель', '2014660C-3592-436A-A7BE-90EF96DD9194', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 320, 'Завершена привязка/отключение подразделения от клиента', 'Клиент - Подразделение', '2014660C-3592-436A-A7BE-90EF96DD9194', '5FFA20F2-D5BA-4AB5-B4F3-43B686E4116E');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 321, 'Ошибка привязки клиента к подразделению', 'Клиент - Подразделение', '2014660C-3592-436A-A7BE-90EF96DD9194', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 330, 'Успешно изменено описание торгового представителя', 'Торговй представитель', '2014660C-3592-436A-A7BE-90EF96DD9194', '5FFA20F2-D5BA-4AB5-B4F3-43B686E4116E');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 331, 'Ошибка изменения описания торгового представителя', 'Торговй представитель', '2014660C-3592-436A-A7BE-90EF96DD9194', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 332, 'Ошибка изменения описания торгового представителя в InterBase', 'Торговй представитель', '2014660C-3592-436A-A7BE-90EF96DD9194', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 340, 'Успешно удалено описание торгового представителя', 'Торговй представитель', '2014660C-3592-436A-A7BE-90EF96DD9194', '5FFA20F2-D5BA-4AB5-B4F3-43B686E4116E');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 341, 'Ошибка удаления описания торгового представителя', 'Торговй представитель', '2014660C-3592-436A-A7BE-90EF96DD9194', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 342, 'Ошибка удаления описания торгового представителя в InterBase', 'Торговй представитель', '2014660C-3592-436A-A7BE-90EF96DD9194', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 400, 'Создан протокол', 'Работа с заказами', 'DA7A80EB-1107-48B1-ACEF-D1D0762FED05', '5FFA20F2-D5BA-4AB5-B4F3-43B686E4116E');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 401, 'Ошибка создания протокола', 'Работа с заказами', 'DA7A80EB-1107-48B1-ACEF-D1D0762FED05', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 410, 'заказ помещен в очередь на пересчет цен', 'Обработка заказа (цены)', 'FC062978-1F6A-42D6-9301-31961C26814D', '5FFA20F2-D5BA-4AB5-B4F3-43B686E4116E');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 430, 'Ошибка включения/исключения заказа в акта выполненных работ', 'Обработка заказа (цены)', 'FC062978-1F6A-42D6-9301-31961C26814D', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');
INSERT INTO dbo.T_EVENTTYPE( EVENTTYPE_ID, EVENTTYPE_NAME, EVENTTYPE_DESCRIPTION, EVENTCATEGORY_GUID_ID, EVENTINFOTYPE_GUID_ID ) VALUES( 500, 'Тест процедуры', 'Тестрирование хранимой процедуры', 'DA7A80EB-1107-48B1-ACEF-D1D0762FED05', 'B58F3E73-04C2-4EED-B3DE-5B5AFEE61720');


/****** Object:  UserDefinedFunction [dbo].[GetOptFromXml]    Script Date: 03/25/2012 11:04:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[GetOptFromXml] ( @doc xml (DOCUMENT InfoForCalcPriceSchema))
returns bit
with execute as caller
as
begin
 DECLARE @ReturnValue int;
 SET @ReturnValue = NULL;
    DECLARE @existCustomerNode bit;
    SELECT @existCustomerNode = @doc.exist( '//InfoForCalc/Suppl' );
    IF( @existCustomerNode = 1 )
      BEGIN
        DECLARE @existOpt bit;
        SELECT @existOpt = @doc.exist( '(//InfoForCalc/Suppl/@Opt)[1]' );
        IF( @existOpt = 1 )
          BEGIN
            SELECT @ReturnValue = @doc.value( '(//InfoForCalc/Suppl/@Opt)[1]', 'bit' );
          END
      END

 RETURN @ReturnValue;

end


GO
GRANT EXECUTE ON [dbo].[GetOptFromXml] TO [public]
GO

/****** Object:  UserDefinedFunction [dbo].[GetNDSPercentFromXml]    Script Date: 03/25/2012 11:16:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[GetNDSPercentFromXml] ( @doc xml (DOCUMENT InfoForCalcPriceSchema))
returns decimal(10, 4)
with execute as caller
as
begin
 DECLARE @ReturnValue decimal(10, 4);
 SET @ReturnValue = NULL;
    DECLARE @existPriceNode bit;
    SELECT @existPriceNode = @doc.exist( '//InfoForCalc/Price' );
    IF( @existPriceNode = 1 )
      BEGIN
        DECLARE @existMarkupPercent bit;
        SELECT @existMarkupPercent = @doc.exist( '(//InfoForCalc/Price/@NDSPercent)[1]' );
        IF( @existMarkupPercent = 1 )
          BEGIN
            SELECT @ReturnValue = @doc.value( '(//InfoForCalc/Price/@NDSPercent)[1]', 'decimal(10, 4)' );
          END
      END

 RETURN @ReturnValue;

end


GO
GRANT EXECUTE ON [dbo].[GetNDSPercentFromXml] TO [public]
GO

/****** Object:  UserDefinedFunction [dbo].[GetMarkupPercentFromXml]    Script Date: 03/25/2012 11:16:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- возвращает размер максимальной оптовой надбавки
CREATE FUNCTION [dbo].[GetMarkupPercentFromXml] ( @doc xml (DOCUMENT InfoForCalcPriceSchema))
returns decimal
with execute as caller
as
begin
 DECLARE @ReturnValue decimal;
 SET @ReturnValue = NULL;
    DECLARE @existPriceNode bit;
    SELECT @existPriceNode = @doc.exist( '//InfoForCalc/Price' );
    IF( @existPriceNode = 1 )
      BEGIN
        DECLARE @existMarkupPercent bit;
        SELECT @existMarkupPercent = @doc.exist( '(//InfoForCalc/Price/@MarkupPercent)[1]' );
        IF( @existMarkupPercent = 1 )
          BEGIN
            SELECT @ReturnValue = @doc.value( '(//InfoForCalc/Price/@MarkupPercent)[1]', 'decimal(10, 4)' );
          END
      END

 RETURN @ReturnValue;

end


GO
GRANT EXECUTE ON [dbo].[GetMarkupPercentFromXml] TO [public]
GO


/****** Object:  UserDefinedFunction [dbo].[GetLimitDiscountPercentFromXml]    Script Date: 03/25/2012 11:15:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[GetLimitDiscountPercentFromXml] ( @doc xml (DOCUMENT InfoForCalcPriceSchema))
returns decimal(10, 4)
with execute as caller
as
begin
 DECLARE @ReturnValue decimal(10, 4);
 SET @ReturnValue = NULL;
    DECLARE @existPriceNode bit;
    SELECT @existPriceNode = @doc.exist( '//InfoForCalc/Price' );
    IF( @existPriceNode = 1 )
      BEGIN
        DECLARE @existMarkupPercent bit;
        SELECT @existMarkupPercent = @doc.exist( '(//InfoForCalc/Price/@DiscountRetroPercent)[1]' );
        IF( @existMarkupPercent = 1 )
          BEGIN
            SELECT @ReturnValue = @doc.value( '(//InfoForCalc/Price/@DiscountRetroPercent)[1]', 'decimal(10, 4)' );
          END
      END

	IF( @ReturnValue IS NULL ) SET @ReturnValue = 0;

	RETURN @ReturnValue;

end

GO
GRANT EXECUTE ON [dbo].[GetLimitDiscountPercentFromXml] TO [public]
GO

/****** Object:  UserDefinedFunction [dbo].[GetLimitDiscountFullPercentFromXml]    Script Date: 03/25/2012 11:15:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[GetLimitDiscountFullPercentFromXml] ( @doc xml (DOCUMENT InfoForCalcPriceSchema))
returns decimal(10, 4)
with execute as caller
as
begin
 DECLARE @ReturnValue decimal(10, 4);
 SET @ReturnValue = NULL;
    DECLARE @existPriceNode bit;
    SELECT @existPriceNode = @doc.exist( '//InfoForCalc/Price' );
    IF( @existPriceNode = 1 )
      BEGIN
        DECLARE @existMarkupPercent bit;
        SELECT @existMarkupPercent = @doc.exist( '(//InfoForCalc/Price/@DiscountFixPercent)[1]' );
        IF( @existMarkupPercent = 1 )
          BEGIN
            SELECT @ReturnValue = @doc.value( '(//InfoForCalc/Price/@DiscountFixPercent)[1]', 'decimal(10, 4)' );
          END
      END

	IF( @ReturnValue IS NULL ) SET @ReturnValue = 0;

	RETURN @ReturnValue;

end

GO
GRANT EXECUTE ON [dbo].[GetLimitDiscountFullPercentFromXml] TO [public]
GO

/****** Object:  UserDefinedFunction [dbo].[GetDiscountFullPercentFromXml]    Script Date: 03/25/2012 11:15:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[GetDiscountFullPercentFromXml] ( @doc xml (DOCUMENT InfoForCalcPriceSchema))
returns decimal(10, 4)
with execute as caller
as
begin
 DECLARE @ReturnValue decimal(10, 4);
 SET @ReturnValue = NULL;
    DECLARE @existPriceNode bit;
    SELECT @existPriceNode = @doc.exist( '//InfoForCalc/Price' );
    IF( @existPriceNode = 1 )
      BEGIN
        DECLARE @existMarkupPercent bit;
        SELECT @existMarkupPercent = @doc.exist( '(//InfoForCalc/Price/@DiscountFullPercent)[1]' );
        IF( @existMarkupPercent = 1 )
          BEGIN
            SELECT @ReturnValue = @doc.value( '(//InfoForCalc/Price/@DiscountFullPercent)[1]', 'decimal(10, 4)' );
          END
      END
      
	IF( @ReturnValue IS NULL ) SET @ReturnValue = 0;
	
 RETURN @ReturnValue;

end


GO
GRANT EXECUTE ON [dbo].[GetDiscountFullPercentFromXml] TO [public]
GO

/****** Object:  UserDefinedFunction [dbo].[GetDiscountPercentFromXml]    Script Date: 03/25/2012 11:15:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[GetDiscountPercentFromXml] ( @doc xml (DOCUMENT InfoForCalcPriceSchema))
returns decimal(10, 4)
with execute as caller
as
begin
 DECLARE @ReturnValue decimal(10, 4);
 SET @ReturnValue = NULL;
    DECLARE @existPriceNode bit;
    SELECT @existPriceNode = @doc.exist( '//InfoForCalc/Price' );
    IF( @existPriceNode = 1 )
      BEGIN
        DECLARE @existMarkupPercent bit;
        SELECT @existMarkupPercent = @doc.exist( '(//InfoForCalc/Price/@DiscountPercent)[1]' );
        IF( @existMarkupPercent = 1 )
          BEGIN
            SELECT @ReturnValue = @doc.value( '(//InfoForCalc/Price/@DiscountPercent)[1]', 'decimal(10, 4)' );
          END
      END

	IF( @ReturnValue IS NULL ) SET @ReturnValue = 0;
	RETURN @ReturnValue;

end

GO
GRANT EXECUTE ON [dbo].[GetDiscountPercentFromXml] TO [public]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[AddDiscountToSchema] ( @SupplItmsInfo xml (DOCUMENT InfoForCalcPriceSchema), @AddDiscountPercent decimal(10, 4), 
	@ReplaceDiscountPercent bit )
returns xml
with execute as caller
as
begin
	DECLARE @DiscountPercent decimal(10, 4);
	DECLARE @DiscountFullPercent decimal(10, 4);
	DECLARE @LimitDiscountPercent decimal(10, 4);
	DECLARE @LimitDiscountFullPercent decimal(10, 4);
	-- предоставляемая скидка
	SELECT @DiscountPercent = dbo.GetDiscountPercentFromXml(@SupplItmsInfo); 
	-- полная (насчитанная) скидка
	SELECT @DiscountFullPercent = dbo.GetDiscountFullPercentFromXml(@SupplItmsInfo);
	-- предельное значение предоставляемой скидки
	SELECT @LimitDiscountPercent = dbo.GetLimitDiscountPercentFromXml( @SupplItmsInfo );
	-- предельное значение полной (насчитанной) скидки
	SELECT @LimitDiscountFullPercent = dbo.GetLimitDiscountFullPercentFromXml( @SupplItmsInfo );

	IF( @ReplaceDiscountPercent = 1 )
		BEGIN
			SET @DiscountPercent = @AddDiscountPercent;	
			SET @DiscountFullPercent = @AddDiscountPercent;
		END
	ELSE
		BEGIN
			SET @DiscountPercent = @DiscountPercent + @AddDiscountPercent;	
			SET @DiscountFullPercent = @DiscountFullPercent + @AddDiscountPercent;
		END	

	IF( @LimitDiscountPercent <> 0 )
		BEGIN
			-- есть ограничение на предоставляемую скидку
			IF( @DiscountPercent > @LimitDiscountPercent )
				SET @DiscountPercent = @LimitDiscountPercent;
		END
	IF( @LimitDiscountFullPercent <> 0 )
		BEGIN
			IF( @DiscountFullPercent > @LimitDiscountFullPercent )
				SET @DiscountFullPercent = @LimitDiscountFullPercent;
		END

	SELECT @SupplItmsInfo = dbo.SetDiscountPercentInXml( @SupplItmsInfo, @DiscountPercent );
	SELECT @SupplItmsInfo = dbo.SetDiscountFullPercentInXml( @SupplItmsInfo, @DiscountFullPercent );

 RETURN @SupplItmsInfo;

end


GO
GRANT EXECUTE ON [dbo].[AddDiscountToSchema] TO [public]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[GetFixDiscountTypeGuid]()
returns uniqueidentifier
with execute as caller
as
begin
  
  DECLARE @FixDiscountType_Guid uniqueidentifier;
	SET @FixDiscountType_Guid = 'ABDAACBB-B825-45F3-8231-CF892010967E';

	RETURN @FixDiscountType_Guid;

end

GO
GRANT EXECUTE ON [dbo].[GetFixDiscountTypeGuid] TO [public]
GO

/****** Object:  UserDefinedFunction [dbo].[GetDiscountFix]    Script Date: 03/25/2012 10:52:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Входные параметры

-- @Customer_Guid	УИ клиента
-- @Parts_Guid	УИ товара
-- @Company_Guid	УИ компании
-- @PaymentType_Guid	УИ вида платежа

-- Возвращает значение фиксированной скидки

CREATE FUNCTION [dbo].[GetDiscountFix] ( @Customer_Guid uniqueidentifier, @Company_Guid uniqueidentifier, 
  @Parts_Guid uniqueidentifier, @PaymentType_Guid uniqueidentifier)
returns decimal(18, 4)
with execute as caller
as
begin
  DECLARE @ReturnValue decimal(18, 4);
  SET @ReturnValue = NULL;

	DECLARE @FixDiscountType_Guid uniqueidentifier;
  SELECT @FixDiscountType_Guid = dbo.GetFixDiscountTypeGuid();

  DECLARE @ProductOwner_Guid uniqueidentifier;
  SELECT @ProductOwner_Guid = dbo.GetOwnerGuidForParts( @Parts_Guid );
  
  IF( ( @ProductOwner_Guid IS NOT NULL ) AND ( @Company_Guid IS NOT NULL ) AND 
      ( @PaymentType_Guid IS NOT NULL ) AND ( @Customer_Guid IS NOT NULL ) )
    BEGIN
      DECLARE @Discount_Guid uniqueidentifier;
      DECLARE @Discount_BeginDate D_DATE;
      SET @Discount_Guid = NULL;
      SET @Discount_BeginDate = NULL;
     
      SELECT Top 1 @Discount_BeginDate = MAX( Discount_BeginDate ) FROM dbo.T_Discount
      WHERE DiscountType_Guid = @FixDiscountType_Guid
	      AND Discount_BeginDate <= GetDate()
	      AND Discount_Active = 1;
	      
	    IF( @Discount_BeginDate IS NOT NULL )  
	      BEGIN
	        SELECT @Discount_Guid = Discount_Guid FROM dbo.T_Discount 
	        WHERE DiscountType_Guid = @FixDiscountType_Guid AND Discount_Active = 1 AND Discount_BeginDate = @Discount_BeginDate;
	        
	        IF( @Discount_Guid IS NOT NULL )
	          BEGIN
	            SELECT Top 1 @ReturnValue = Discount_Percent FROM dbo.T_DiscountFixItem
	            WHERE Discount_Guid = @Discount_Guid
	              AND Customer_Guid = @Customer_Guid
	              AND ProductOwner_Guid = @ProductOwner_Guid
	              AND Company_Guid = @Company_Guid
	              AND PaymentType_Guid = @PaymentType_Guid;
	          END
	      END
    END

  IF( @ReturnValue IS NULL ) SET @ReturnValue = 0;
  
  RETURN @ReturnValue;

end

GO
GRANT EXECUTE ON [dbo].[GetDiscountFix] TO [public]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_GetPrice_DiscountFixPercent] 
	@Spl_Guid D_GUID = NULL,
	@SupplItms_Guid D_GUID = NULL,
  @SupplItmsInfo xml (DOCUMENT InfoForCalcPriceSchema) = NULL output,
  @SPParamsInfo xml (DOCUMENT RuleParams) = NULL output,
  @RulePool_StepGuid uniqueidentifier = NULL,
  @DiscountListInfo xml ( DOCUMENT DiscountList ) = NULL output,

  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output

AS

BEGIN
SET NOCOUNT ON;

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = '';

	DECLARE @Parts_Guid D_GUID;     -- УИ товара
  DECLARE @Suppl_Guid D_GUID;     -- УИ заказа
 
	BEGIN TRY
		IF( @Spl_Guid IS NOT NULL )
			BEGIN
				-- проверка на то, можно ли в принципе "впускать" заказ в это правило
				SET @ERROR_NUM = 777; -- почему бы и нет?
				SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) + '[sp_GetPrice_DiscountFixPercent] Можно проверять на содержимое.';

				RETURN @ERROR_NUM;

			END

		-- данная хранимая процедура определяет % фиксированной скидки и записывает его в соответсвующем узле @SupplItmsInfo
		-- если в этом узле содержался % скидки, то он будет переписан на процент фиксированной скидки,
		-- а если же найденный % фиксированной скидки будет равен нулю, то мы не трогаем @SupplItmsInfo
		
	  IF( @SupplItmsInfo IS NULL )
	    BEGIN
	      BEGIN TRY  
	        -- нужно вернуть информацию о тех параметрах, которые умеет обрабатывать данная хранимая процедура
	        -- вернем мы ее в виде xml файла  
            DECLARE @doc xml ( DOCUMENT RuleParams );
	          SET @doc = N'<?xml version="1.0" encoding="UTF-16"?>
              <SP_Param xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
	              <Param Type="int" Name="Скидка" Value=""/>
	              <Param Type="int" Name="Надбавка" Value=""/>
              </SP_Param>
              ';  
            SET @SPParamsInfo = @doc;  
	      END TRY
	      BEGIN CATCH
		      SET @ERROR_NUM = ERROR_NUMBER();
		      SET @ERROR_MES = '[sp_GetPrice_DiscountFixPercent], Текст ошибки: ' + ERROR_MESSAGE();
		      RETURN @ERROR_NUM;
	      END CATCH;

	      SET @ERROR_NUM = 0;
	      SET @ERROR_MES = '[sp_GetPrice_DiscountFixPercent] Схема дополнительных параметров. Успешное завершение операции.';
	      RETURN @ERROR_NUM;
	    END

			IF( @SupplItms_Guid IS NOT NULL )
				BEGIN
					SELECT @Suppl_Guid = Suppl_Guid, @Parts_Guid = Parts_Guid
					FROM #PDASupplItms WHERE SupplItms_Guid = @SupplItms_Guid;	
				END
			ELSE
				BEGIN
					SELECT @Suppl_Guid = dbo.GetSupplIDFromXml(@SupplItmsInfo);  
					SELECT @Parts_Guid = dbo.GetPartsGuidFromXml(@SupplItmsInfo);  
				END	

    IF( ( @Parts_Guid IS NOT NULL ) AND ( @Suppl_Guid IS NOT NULL ) )
      BEGIN
        DECLARE @Customer_Guid D_GUID;      -- УИ клиента
        DECLARE @Company_Guid D_GUID;       -- УИ компании
        DECLARE @PaymentType_Guid D_GUID;   -- УИ типа платежа
		    DECLARE @bOpt bit;
		    
		    SELECT @bOpt = dbo.GetOptFromXml( @SupplItmsInfo );  
		    IF( @bOpt = 0 )
					SELECT @PaymentType_Guid = dbo.GetPaymentTypeForm1Guid();
				ELSE IF( @bOpt = 1 )	
					SELECT @PaymentType_Guid = dbo.GetPaymentTypeForm2Guid();

		    SELECT @Customer_Guid = dbo.GetCustomerGuidFromXml( @SupplItmsInfo );  
		    SELECT @Company_Guid = dbo.GetCompanyGuidFromXml( @SupplItmsInfo );  

        IF( ( @Customer_Guid IS NOT NULL ) AND ( @Company_Guid IS NOT NULL ) AND ( @Parts_Guid IS NOT NULL ) AND ( @PaymentType_Guid IS NOT NULL ) )
          BEGIN

            DECLARE @DiscountFixPercent decimal(18, 4);
            SELECT @DiscountFixPercent = dbo.GetDiscountFix( @Customer_Guid, @Company_Guid, @Parts_Guid, @PaymentType_Guid );

            IF( ( @DiscountFixPercent IS NOT NULL ) AND ( @DiscountFixPercent > 0 ) )
              BEGIN
								-- запишем найденное значение скидки в xml-структуру
								SELECT @SupplItmsInfo = dbo.AddDiscountToSchema( @SupplItmsInfo, @DiscountFixPercent, 1 );
								SELECT @DiscountListInfo = dbo.SetDiscountPercentByTypeInXml( @DiscountListInfo, 1, @DiscountFixPercent );

								SET @ERROR_MES = 'Размер скидки: ' + CONVERT( varchar(8), @DiscountFixPercent );
								EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 1, @RulePool_StepGuid = @RulePool_StepGuid, @EventDscrpn = @ERROR_MES;
             END
          END
      END    
    
	END TRY
	BEGIN CATCH
		SET @ERROR_NUM = ERROR_NUMBER();
		SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10)  + '[sp_GetPrice_DiscountFixPercentt] Текст ошибки: ' + ERROR_MESSAGE();

		EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 2, 
			@RulePool_StepGuid = @RulePool_StepGuid, @EventDscrpn = @ERROR_MES;

    RETURN @ERROR_NUM;
	END CATCH;

	IF( @ERROR_NUM = 0 )
		SET @ERROR_MES = @ERROR_MES + ' [sp_GetPrice_DiscountFixPercent] Успешное завершение операции.';
	RETURN @ERROR_NUM;
END

GO
GRANT EXECUTE ON [dbo].[sp_GetPrice_DiscountFixPercent] TO [public]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetPrice]    Script Date: 03/25/2012 11:37:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_GetPriceCalculation] 
	@Parts_Guid D_GUID,
	@PaymentType_Guid D_GUID,
	@Stock_Guid D_GUID,
	@DiscountPercent decimal(18, 4) = 0,

	@NDSPercent D_MONEY output,
  @PriceImporter D_MONEY output,
  @Price D_MONEY output,
  @PriceWithDiscount D_MONEY output,
  @PriceInAccountingCurrency D_MONEY output,
  @PriceWithDiscountInAccountingCurrency D_MONEY output,
  @IsPartsImporter D_YESNO output,-- признак "товар импортера" 
  @ChargesPercent D_MONEY output,  -- процент надбавки по товару

  @PriceImporterFromPriceList D_MONEY output,
  @PriceFromPriceList D_MONEY output,
  @PriceInAccountingCurrencyFromPriceList D_MONEY output,

  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output

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
  SET @IsPartsImporter = 0;
  SET @ChargesPercent = 0;

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
    DECLARE @Price0 decimal(18, 4);      -- Цена импортера, руб.
    DECLARE @Price0_2 decimal(18, 4);    -- Цена отпускная по прейскуранту с НДС, у.е.
    DECLARE @Price2 decimal(18, 4);      -- Цена отпускная по прейскуранту с НДС, руб.  
    DECLARE @Price11 decimal(18, 4);     -- Цена Регионы спец, руб.    
    DECLARE @Price0_11 decimal(18, 4);   -- Цена «Регионы спец, у.е.» 
    DECLARE @CurrencyRatePricing float; 
    
    SELECT @Price0 = CONVERT( decimal(18, 4), Price0 ), 
      @Price0_2 = CONVERT( decimal(18, 4), Price0_2 ), 
      @Price2 = CONVERT( decimal(18, 4), Price2 ),
      @Price11 = CONVERT( decimal(18, 4), Price11 ),
      @Price0_11 = CONVERT( decimal(18, 4), Price0_11 )       
    FROM dbo.T_Prices WHERE Parts_Guid = @Parts_Guid;
    
    SET @CurrencyRatePricing = 9000; -- заменить на запрос из функции
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

		IF( @PaymentType_Guid = @PaymentTypeForm1Guid )
			BEGIN
				-- форма оплаты №1

				SET @PriceImporterFromPriceList = @Price0;
				SET @PriceFromPriceList = @Price2;
				SET @PriceInAccountingCurrencyFromPriceList = @Price0_2;

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
						-- товар отечественный
						SET @PriceImporter = @Price0;
						SET @Price = ( SELECT dbo.GetRoundMoney( @Price0 * ( 1 + ( @ChargesPercent/100 ) ) ) );
						SET @Price = @Price * ( 1 + ( @NDSPercent/100 ) );
						SET @PriceWithDiscount = ( SELECT dbo.GetRoundMoney( @Price0 * ( 1 + ( @ChargesPercent - @DiscountPercent )/100 ) * ( 1 + ( @NDSPercent/100 ) ) ) );
						SET @PriceInAccountingCurrency = @Price/@CurrencyRatePricing;
						SET @PriceWithDiscountInAccountingCurrency = @PriceWithDiscount/@CurrencyRatePricing;
					END
				ELSE IF( @IsPartsImporter = 1 )	
					BEGIN
						-- товар импортный
						SET @PriceImporter = ( SELECT dbo.GetRoundMoney( @Price0 * ( 1 - ( @DiscountPercent/100 ) ) ) );
						SET @PriceWithDiscount = @PriceImporter * ( 1 + ( @NDSPercent/100 ) );
						SET @Price = @PriceWithDiscount;
						SET @PriceInAccountingCurrency = @Price/@CurrencyRatePricing;
						SET @PriceWithDiscountInAccountingCurrency = @PriceWithDiscount/@CurrencyRatePricing;
					END
			END
		ELSE IF( @PaymentType_Guid = @PaymentTypeForm2Guid )
			BEGIN
				-- форма оплаты №2
				SET @PriceImporterFromPriceList = @Price0;
				SET @PriceFromPriceList = @Price11;
				SET @PriceInAccountingCurrencyFromPriceList = @Price0_11;

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
					SET @PriceImporter =  ( SELECT dbo.GetRoundMoney( ( @Price/( 1 + ( @NDSPercent/100 ) ) ) ) );

					IF( @IsPartsImporter = 1 )
						BEGIN
							SET @Price = @PriceImporter * ( 1 + ( @NDSPercent/100 ) );
						END

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
GRANT EXECUTE ON [dbo].[usp_GetPriceCalculation] TO [public]
GO

/****** Object:  StoredProcedure [dbo].[sp_GetPrice_03]    Script Date: 03/25/2012 11:33:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetPrice_03] 
	@Spl_Guid D_GUID = NULL,
	@SupplItms_Guid D_GUID = NULL,
  @SupplItmsInfo xml (DOCUMENT InfoForCalcPriceSchema) = NULL output,
  @SPParamsInfo xml (DOCUMENT RuleParams) = NULL output,
  @RulePool_StepGuid uniqueidentifier = NULL,
  @DiscountListInfo xml ( DOCUMENT DiscountList ) = NULL output,

  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output

AS

BEGIN
SET NOCOUNT ON;

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = '';

	DECLARE @Parts_Guid D_GUID;       -- УИ товара
	DECLARE @Parts_Qty D_QUANTITY;    -- Количество товара в позиции
  DECLARE @Customer_Id D_ID;        -- УИ клиента в InterBase
  DECLARE @OptSuppl bit;            -- Признак "оптовый заказ"
  DECLARE @Suppl_Guid D_GUID;       -- УИ заказа
  DECLARE @Stock_Guid D_GUID;       -- УИ склада
  
	BEGIN TRY

		IF( @Spl_Guid IS NOT NULL )
			BEGIN
				-- проверка на то, можно ли в принципе "впускать" заказ в это правило
				DECLARE @PaymentTypeForm1Guid D_GUID; 
				DECLARE @PaymentTypeForm2Guid D_GUID; 
				DECLARE @PaymentType_Guid D_GUID; 
				
				SELECT @PaymentTypeForm1Guid = dbo.GetPaymentTypeForm1Guid();
				SELECT @PaymentTypeForm2Guid = dbo.GetPaymentTypeForm2Guid();
				
				SELECT @PaymentType_Guid = PaymentType_Guid FROM dbo.T_Order WHERE Order_Guid = @Spl_Guid;
				
				IF( @PaymentType_Guid = @PaymentTypeForm1Guid )
					BEGIN
						SET @ERROR_NUM = 777; -- почему бы и нет?
						SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) + '[SP_GetPrice_03] Заказ "форма оплаты №1". Можно проверять на содержимое.';
					END  

				RETURN @ERROR_NUM;

			END

		-- Имя шага, запустившего процедуру
		DECLARE @RulePool_StepName D_NAME;
		SELECT @RulePool_StepName = RulePool_StepName FROM dbo.T_RulePool WHERE RulePool_StepGuid = @RulePool_StepGuid;
		IF( @RulePool_StepName IS NULL ) SET @RulePool_StepName = '';
	
	  IF( @SupplItmsInfo IS NULL )
	    BEGIN
	      BEGIN TRY  
	        -- нужно вернуть информацию о тех параметрах, которые умеет обрабатывать данная хранимая процедура
	        -- вернем мы ее в виде xml файла  
            DECLARE @doc xml ( DOCUMENT RuleParams );
	          SET @doc = N'<?xml version="1.0" encoding="UTF-16"?>
              <SP_Param xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
	              <Param Type="int" Name="Скидка" Value=""/>
	              <Param Type="int" Name="Надбавка" Value=""/>
              </SP_Param>
              ';  
            SET @SPParamsInfo = @doc;  
	      END TRY
	      BEGIN CATCH
		      SET @ERROR_NUM = ERROR_NUMBER();
		      SET @ERROR_MES = '[SP_GetPrice_03], Текст ошибки: ' + ERROR_MESSAGE();
		      RETURN @ERROR_NUM;
	      END CATCH;

	      SET @ERROR_NUM = 0;
	      SET @ERROR_MES = @ERROR_MES + ' [SP_GetPrice_03] Схема дополнительных параметров. Успешное завершение операции.';
	      RETURN @ERROR_NUM;
	    END
	    
	  IF( @SupplItms_Guid IS NOT NULL )
			BEGIN
				SELECT @Suppl_Guid = Suppl_Guid, @OptSuppl = Opt, @Stock_Guid = STOCK_GUID_ID, @Customer_Id = CustomerID, 
					@Parts_Guid = Parts_Guid, @Parts_Qty = SupplItms_Quantity
				FROM #PDASupplItms WHERE SupplItms_Guid = @SupplItms_Guid;	
			END
		ELSE
			BEGIN
		    SELECT @Suppl_Guid = dbo.GetSupplIDFromXml( @SupplItmsInfo );
		    SELECT @OptSuppl = dbo.GetOptFromXml( @SupplItmsInfo );
		    SELECT @Stock_Guid = Stock_Guid FROM dbo.T_Order WHERE Order_Guid = @Suppl_Guid;
				SELECT @Customer_Id = dbo.GetCustomerIDFromXml( @SupplItmsInfo );
				SELECT @Parts_Guid = dbo.GetPartsGuidFromXml( @SupplItmsInfo );
				SELECT @Parts_Qty = dbo.GetPartsQtyFromXml( @SupplItmsInfo );
			END	

	  -- Уникальный идентификатор заказа  
    IF( @Suppl_Guid IS NULL )
      BEGIN
	      SET @ERROR_NUM = 1;
	      SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) + nChar(13) + nChar(10) +  '[SP_GetPrice_03] не удалось определить идентификатор заказа.';

				EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 101, @RulePool_StepGuid = @RulePool_StepGuid, @EventDscrpn = @ERROR_MES;
	      RETURN @ERROR_NUM;
      END
      
    -- Признак "Розничный заказ"
    IF( @OptSuppl = 1 )
			BEGIN
	      SET @ERROR_NUM = -1;
	      SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10)  + @RulePool_StepName + nChar(13) + nChar(10) +  ' [SP_GetPrice_03] Заказ не относится к категории "форма оплаты №1". Обработка не будет производится.';

				EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 100, @RulePool_StepGuid = @RulePool_StepGuid, @EventDscrpn = @ERROR_MES;
	      RETURN @ERROR_NUM;
	    END  
	    
    -- Уникальный идентификатор склада
    IF( @Stock_Guid IS NULL )
      BEGIN
	      SET @ERROR_NUM = 2;
	      SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) + '[SP_GetPrice_03] не удалось определить идентификатор склада.';

				EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 99, @RulePool_StepGuid = @RulePool_StepGuid, @EventDscrpn = @ERROR_MES;
	      RETURN @ERROR_NUM;
      END
    
    -- Если мы здесь, значит в структуре XML содержатся все необходимые данные.
    -- Проверим их на допустимость значений.
    IF( (@Customer_Id IS NULL) OR (@Customer_Id = 0) OR (@OptSuppl IS NULL) OR (@Parts_Guid IS NULL)  OR 
         ( @Parts_Qty IS NULL ) OR (@Parts_Qty <= 0) )
      BEGIN
        SET @ERROR_NUM = 3;
        DECLARE @strCustomer_Id varchar(8);
        DECLARE @strOptSuppl varchar(8);
        DECLARE @strParts_Id varchar(8);
        DECLARE @strParts_Qty varchar(8);
        
        IF(@Customer_Id IS NULL)  SET @strCustomer_Id = 'NULL';
        ELSE SET @strCustomer_Id = CONVERT( varchar(8), @Customer_Id );
        IF(@OptSuppl IS NULL)  SET @strOptSuppl = 'NULL';
        IF(@Parts_Guid IS NULL)  SET @strParts_Id = 'NULL';
        ELSE SET @strParts_Id = CONVERT( varchar(36), @Parts_Guid );
        IF(@Parts_Qty IS NULL)  SET @strParts_Qty = 'NULL';
        ELSE SET @strParts_Qty = CONVERT( varchar(8), @Parts_Qty );

        SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10)  + @RulePool_StepName + nChar(13) + nChar(10) + 
					'[SP_GetPrice_03] Недопустимое значение входных параметров.' + nChar(13) + nChar(10) + 
					'Код клиента: ' + @strCustomer_Id  + nChar(13) + nChar(10) +  
          'Опт: ' + @strOptSuppl  + nChar(13) + nChar(10) +  'Код товара: ' + @strParts_Id  + nChar(13) + nChar(10) + 'Количество товара: ' + @strParts_Qty;

				EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 98, @RulePool_StepGuid = @RulePool_StepGuid, @EventDscrpn = @ERROR_MES;
	      RETURN @ERROR_NUM;
      END     
    
    -- Мы по-прежнему в игре... это радует
    -- Проверим, числится ли в справочниках клиент и товар с заданным кодом и допустимое ли у нас количество товара в заказе

    -- Проверяем наличие записи о клиенте в справочнике
    IF NOT EXISTS ( SELECT Customer_Guid FROM dbo.T_Customer WHERE Customer_Id = @Customer_Id )
      BEGIN
        SET @ERROR_NUM = 4;
        SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) + ' [SP_GetPrice_03] Клиент с заданным идентификатором не найден в справочнике.' + nChar(13) + nChar(10) + 
        'Идентификатор: ' + CONVERT( nvarchar(8), @Customer_Id );

				EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 97, @RulePool_StepGuid = @RulePool_StepGuid, @EventDscrpn = @ERROR_MES;
	      RETURN @ERROR_NUM;
      END 

    -- Проверяем наличие записи о товаре в справочнике
    IF NOT EXISTS ( SELECT Parts_Guid FROM dbo.T_Parts WHERE Parts_Guid = @Parts_Guid )
      BEGIN
        SET @ERROR_NUM = 5;
        SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) +  '[SP_GetPrice_03] Товар с заданным идентификатором не найден в справочнике.'  + nChar(13) + nChar(10) +  'Идентификатор: ' + CONVERT( nvarchar(36), @Parts_Guid );

				EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 96, @RulePool_StepGuid = @RulePool_StepGuid, @EventDscrpn = @ERROR_MES;
	      RETURN @ERROR_NUM;
      END 

    -- Скидка
    DECLARE @DiscountPercent decimal(18, 4);
    SELECT @DiscountPercent = dbo.GetDiscountPercentFromXml(@SupplItmsInfo);
    IF( ( @DiscountPercent IS NULL ) OR ( @DiscountPercent < 0 ) )
      SET @DiscountPercent = 0;
    
    -- Запрашиваем цену
    DECLARE	@NDSPercent D_MONEY;
		DECLARE	@PriceImporter D_MONEY;
		DECLARE	@Price D_MONEY;
		DECLARE	@PriceWithDiscount D_MONEY;
		DECLARE	@PriceInAccountingCurrency D_MONEY;
		DECLARE	@PriceWithDiscountInAccountingCurrency D_MONEY;
		DECLARE @IsPartsImporter D_YESNO;
		DECLARE @ChargesPercent D_MONEY;
		DECLARE @PriceImporterFromPriceList D_MONEY;
		DECLARE @PriceFromPriceList D_MONEY;
		DECLARE @PriceInAccountingCurrencyFromPriceList D_MONEY;

		EXEC [dbo].[usp_GetPriceCalculation] @Parts_Guid = @Parts_Guid, @PaymentType_Guid = @PaymentType_Guid, 
			@Stock_Guid = @Stock_Guid, @DiscountPercent = @DiscountPercent, @NDSPercent = @NDSPercent,
			@PriceImporter = @PriceImporter out, @Price = @Price out,
			@PriceWithDiscount = @PriceWithDiscount out,
			@PriceInAccountingCurrency = @PriceInAccountingCurrency out,
			@PriceWithDiscountInAccountingCurrency = @PriceWithDiscountInAccountingCurrency out,
			@IsPartsImporter = @IsPartsImporter out, @ChargesPercent = @ChargesPercent out,
			@PriceImporterFromPriceList = @PriceImporterFromPriceList out,
			@PriceFromPriceList = @PriceFromPriceList out,
			@PriceInAccountingCurrencyFromPriceList = @PriceInAccountingCurrencyFromPriceList out,
		  @ERROR_NUM = @ERROR_NUM out, @ERROR_MES = @ERROR_MES out ;    
		
		IF( @ERROR_NUM <> 0 )   
			BEGIN
				EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 92, @RulePool_StepGuid = @RulePool_StepGuid, @EventDscrpn = @ERROR_MES;
	      RETURN @ERROR_NUM;
			END

   IF( ( @PriceImporter = 0 ) OR ( @Price = 0 ) OR ( @PriceInAccountingCurrency = 0 ) )
      BEGIN
        SET @ERROR_NUM = 10;
        SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) + 
					'@PriceImporter: ' + CONVERT( nvarchar(15), @PriceImporter ) + nChar(13) + nChar(10) + 
					'@PriceInAccountingCurrency: ' + CONVERT( nvarchar(15), @PriceInAccountingCurrency )  + nChar(13) + nChar(10) + 
					'@Price: ' + CONVERT( nvarchar(15), @Price );

				EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 92, @RulePool_StepGuid = @RulePool_StepGuid, @EventDscrpn = @ERROR_MES;
	      RETURN @ERROR_NUM;
      END 

    -- теперь нужно внести изменения в объект "Цена"
    SELECT @SupplItmsInfo = dbo.SetPrice0InXml( @SupplItmsInfo, @PriceImporter );
    SELECT @SupplItmsInfo = dbo.SetPriceInXml( @SupplItmsInfo, @Price );
    SELECT @SupplItmsInfo = dbo.SetPriceCurrencyInXml( @SupplItmsInfo, @PriceInAccountingCurrency );
    SELECT @SupplItmsInfo = dbo.SetDiscountPriceInXml( @SupplItmsInfo, @PriceWithDiscount );
    SELECT @SupplItmsInfo = dbo.SetDiscountPriceCurrencyInXml( @SupplItmsInfo, @PriceWithDiscountInAccountingCurrency );
    SELECT @SupplItmsInfo = dbo.SetPriceList_Price0InXml( @SupplItmsInfo, @PriceImporterFromPriceList );
    SELECT @SupplItmsInfo = dbo.SetPriceList_PriceInXml( @SupplItmsInfo, @PriceFromPriceList );
    SELECT @SupplItmsInfo = dbo.SetPriceList_PriceCurrencyInXml( @SupplItmsInfo, @PriceInAccountingCurrencyFromPriceList );
    SELECT @SupplItmsInfo = dbo.SetNDSPercentInXml( @SupplItmsInfo, @NDSPercent );
    SELECT @SupplItmsInfo = dbo.SetImporterInXml( @SupplItmsInfo, @IsPartsImporter );
    SELECT @SupplItmsInfo = dbo.SetMarkupPercentInXml( @SupplItmsInfo, @ChargesPercent );
    ---- 2009.05.04
    ---- дописываем рассчитанную оптовую надбавку = скидка 
    ---- это необходимо для того, чтобы цены на стороне IB совпадали с ценами в ERP
    --SELECT @SupplItmsInfo = dbo.SetCalcMarkUpPercentInXml( @SupplItmsInfo, @CalcMarkUpPercent );

		SET @ERROR_MES = ' Цена импортера в прайсе: ' + CONVERT( varchar(16), @PriceImporterFromPriceList )+ nChar(13) + nChar(10) + 
			' Цена импортера: ' + CONVERT( varchar(16), @PriceImporter )+ nChar(13) + nChar(10) + 
			' Цена, руб.: ' +  CONVERT( varchar(16), @Price ) + nChar(13) + nChar(10) + 
			' Цена, вал.: ' + CONVERT( varchar(16), @PriceInAccountingCurrency ) + nChar(13) + nChar(10) + 
			' Скидка, %: ' + CONVERT( varchar(8), @DiscountPercent ) + nChar(13) + nChar(10) + 
			' Надбавка (max), %: ' + CONVERT( varchar(8), @ChargesPercent ) + nChar(13) + nChar(10) + 
			--' Надбавка (расчет.), %: ' + CONVERT( varchar(8), @CalcMarkUpPercent ) + nChar(13) + nChar(10) + 
			' Цена со скидкой, руб.: ' + CONVERT( varchar(16), @PriceWithDiscount ) + nChar(13) + nChar(10) + 
			' Цена со скидкой, вал.: ' + CONVERT( varchar(16), @PriceWithDiscountInAccountingCurrency ) + nChar(13) + nChar(10) + 
			' Ставка НДС, %: ' + CONVERT( varchar(8), @NDSPercent ) + nChar(13) + nChar(10) + 
			' Надбавка, %: ' + CONVERT( varchar(8), @ChargesPercent ) + nChar(13) + nChar(10) + 
			' Признак "Импортер": ' + CONVERT( varchar(8), @IsPartsImporter );
			 --+ nChar(13) + nChar(10) + ' Курс: ' + CONVERT( varchar(16), @CurrencyRate );

		EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 90, @RulePool_StepGuid = @RulePool_StepGuid, @EventDscrpn = @ERROR_MES;
    SET @ERROR_NUM = 0;
    
	END TRY
	BEGIN CATCH
		SET @ERROR_NUM = ERROR_NUMBER();
		SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) +  ' [SP_GetPrice_03] Текст ошибки: ' + ERROR_MESSAGE();

		EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 91, @RulePool_StepGuid = @RulePool_StepGuid, @EventDscrpn = @ERROR_MES;
    RETURN @ERROR_NUM;
	END CATCH;

	IF( @ERROR_NUM = 0 )
		SET @ERROR_MES = @ERROR_MES + ' [SP_GetPrice_03] Успешное завершение операции.';
	RETURN @ERROR_NUM;
END

GO
GRANT EXECUTE ON [dbo].[sp_GetPrice_03] TO [public]
GO

/****** Object:  StoredProcedure [dbo].[sp_GetPrice_02]    Script Date: 03/25/2012 11:33:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetPrice_02] 
	@Spl_Guid D_GUID = NULL,
	@SupplItms_Guid D_GUID = NULL,
  @SupplItmsInfo xml (DOCUMENT InfoForCalcPriceSchema) = NULL output,
  @SPParamsInfo xml (DOCUMENT RuleParams) = NULL output,
  @RulePool_StepGuid uniqueidentifier = NULL,
  @DiscountListInfo xml ( DOCUMENT DiscountList ) = NULL output,

  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output

AS

BEGIN
SET NOCOUNT ON;

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = '';

	DECLARE @Parts_Guid D_GUID;       -- УИ товара
	DECLARE @Parts_Qty D_QUANTITY;    -- Количество товара в позиции
  DECLARE @Customer_Id D_ID;        -- УИ клиента в InterBase
  DECLARE @OptSuppl bit;            -- Признак "оптовый заказ"
  DECLARE @Suppl_Guid D_GUID;       -- УИ заказа
  DECLARE @Stock_Guid D_GUID;       -- УИ склада
  
	BEGIN TRY

		IF( @Spl_Guid IS NOT NULL )
			BEGIN
				-- проверка на то, можно ли в принципе "впускать" заказ в это правило
				DECLARE @PaymentTypeForm1Guid D_GUID; 
				DECLARE @PaymentTypeForm2Guid D_GUID; 
				DECLARE @PaymentType_Guid D_GUID; 
				
				SELECT @PaymentTypeForm1Guid = dbo.GetPaymentTypeForm1Guid();
				SELECT @PaymentTypeForm2Guid = dbo.GetPaymentTypeForm2Guid();
				
				SELECT @PaymentType_Guid = PaymentType_Guid FROM dbo.T_Order WHERE Order_Guid = @Spl_Guid;
				
				IF( @PaymentType_Guid = @PaymentTypeForm1Guid )
					BEGIN
						SET @ERROR_NUM = 777; -- почему бы и нет?
						SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) + '[SP_GetPrice_02] Заказ "форма оплаты №1". Можно проверять на содержимое.';
					END  

				RETURN @ERROR_NUM;

			END

		-- Имя шага, запустившего процедуру
		DECLARE @RulePool_StepName D_NAME;
		SELECT @RulePool_StepName = RulePool_StepName FROM dbo.T_RulePool WHERE RulePool_StepGuid = @RulePool_StepGuid;
		IF( @RulePool_StepName IS NULL ) SET @RulePool_StepName = '';
	
	  IF( @SupplItmsInfo IS NULL )
	    BEGIN
	      BEGIN TRY  
	        -- нужно вернуть информацию о тех параметрах, которые умеет обрабатывать данная хранимая процедура
	        -- вернем мы ее в виде xml файла  
            DECLARE @doc xml ( DOCUMENT RuleParams );
	          SET @doc = N'<?xml version="1.0" encoding="UTF-16"?>
              <SP_Param xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
	              <Param Type="int" Name="Скидка" Value=""/>
	              <Param Type="int" Name="Надбавка" Value=""/>
              </SP_Param>
              ';  
            SET @SPParamsInfo = @doc;  
	      END TRY
	      BEGIN CATCH
		      SET @ERROR_NUM = ERROR_NUMBER();
		      SET @ERROR_MES = '[SP_GetPrice_02], Текст ошибки: ' + ERROR_MESSAGE();
		      RETURN @ERROR_NUM;
	      END CATCH;

	      SET @ERROR_NUM = 0;
	      SET @ERROR_MES = @ERROR_MES + ' [SP_GetPrice_02] Схема дополнительных параметров. Успешное завершение операции.';
	      RETURN @ERROR_NUM;
	    END
	    
	  IF( @SupplItms_Guid IS NOT NULL )
			BEGIN
				SELECT @Suppl_Guid = Suppl_Guid, @OptSuppl = Opt, @Stock_Guid = STOCK_GUID_ID, @Customer_Id = CustomerID, 
					@Parts_Guid = Parts_Guid, @Parts_Qty = SupplItms_Quantity
				FROM #PDASupplItms WHERE SupplItms_Guid = @SupplItms_Guid;	
			END
		ELSE
			BEGIN
		    SELECT @Suppl_Guid = dbo.GetSupplIDFromXml( @SupplItmsInfo );
		    SELECT @OptSuppl = dbo.GetOptFromXml( @SupplItmsInfo );
		    SELECT @Stock_Guid = Stock_Guid FROM dbo.T_Order WHERE Order_Guid = @Suppl_Guid;
				SELECT @Customer_Id = dbo.GetCustomerIDFromXml( @SupplItmsInfo );
				SELECT @Parts_Guid = dbo.GetPartsGuidFromXml( @SupplItmsInfo );
				SELECT @Parts_Qty = dbo.GetPartsQtyFromXml( @SupplItmsInfo );
			END	

	  -- Уникальный идентификатор заказа  
    IF( @Suppl_Guid IS NULL )
      BEGIN
	      SET @ERROR_NUM = 1;
	      SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) + nChar(13) + nChar(10) +  '[SP_GetPrice_02] не удалось определить идентификатор заказа.';

				EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 101, @RulePool_StepGuid = @RulePool_StepGuid, @EventDscrpn = @ERROR_MES;
	      RETURN @ERROR_NUM;
      END
      
    -- Признак "Оптовый заказ"
    IF( @OptSuppl = 0 )
			BEGIN
	      SET @ERROR_NUM = -1;
	      SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10)  + @RulePool_StepName + nChar(13) + nChar(10) +  ' [SP_GetPrice_02] Заказ не относится к категории "форма оплаты №2". Обработка не будет производится.';

				EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 100, @RulePool_StepGuid = @RulePool_StepGuid, @EventDscrpn = @ERROR_MES;
	      RETURN @ERROR_NUM;
	    END  
	    
    -- Уникальный идентификатор склада
    IF( @Stock_Guid IS NULL )
      BEGIN
	      SET @ERROR_NUM = 2;
	      SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) + '[SP_GetPrice_02] не удалось определить идентификатор склада.';

				EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 99, @RulePool_StepGuid = @RulePool_StepGuid, @EventDscrpn = @ERROR_MES;
	      RETURN @ERROR_NUM;
      END
    
    -- Если мы здесь, значит в структуре XML содержатся все необходимые данные.
    -- Проверим их на допустимость значений.
    IF( (@Customer_Id IS NULL) OR (@Customer_Id = 0) OR (@OptSuppl IS NULL) OR (@Parts_Guid IS NULL)  OR 
         ( @Parts_Qty IS NULL ) OR (@Parts_Qty <= 0) )
      BEGIN
        SET @ERROR_NUM = 3;
        DECLARE @strCustomer_Id varchar(8);
        DECLARE @strOptSuppl varchar(8);
        DECLARE @strParts_Id varchar(8);
        DECLARE @strParts_Qty varchar(8);
        
        IF(@Customer_Id IS NULL)  SET @strCustomer_Id = 'NULL';
        ELSE SET @strCustomer_Id = CONVERT( varchar(8), @Customer_Id );
        IF(@OptSuppl IS NULL)  SET @strOptSuppl = 'NULL';
        IF(@Parts_Guid IS NULL)  SET @strParts_Id = 'NULL';
        ELSE SET @strParts_Id = CONVERT( varchar(36), @Parts_Guid );
        IF(@Parts_Qty IS NULL)  SET @strParts_Qty = 'NULL';
        ELSE SET @strParts_Qty = CONVERT( varchar(8), @Parts_Qty );

        SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10)  + @RulePool_StepName + nChar(13) + nChar(10) + 
					'[SP_GetPrice_02] Недопустимое значение входных параметров.' + nChar(13) + nChar(10) + 
					'Код клиента: ' + @strCustomer_Id  + nChar(13) + nChar(10) +  
          'Опт: ' + @strOptSuppl  + nChar(13) + nChar(10) +  'Код товара: ' + @strParts_Id  + nChar(13) + nChar(10) + 'Количество товара: ' + @strParts_Qty;

				EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 98, @RulePool_StepGuid = @RulePool_StepGuid, @EventDscrpn = @ERROR_MES;
	      RETURN @ERROR_NUM;
      END     
    
    -- Мы по-прежнему в игре... это радует
    -- Проверим, числится ли в справочниках клиент и товар с заданным кодом и допустимое ли у нас количество товара в заказе

    -- Проверяем наличие записи о клиенте в справочнике
    IF NOT EXISTS ( SELECT Customer_Guid FROM dbo.T_Customer WHERE Customer_Id = @Customer_Id )
      BEGIN
        SET @ERROR_NUM = 4;
        SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) + ' [SP_GetPrice_02] Клиент с заданным идентификатором не найден в справочнике.' + nChar(13) + nChar(10) + 
        'Идентификатор: ' + CONVERT( nvarchar(8), @Customer_Id );

				EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 97, @RulePool_StepGuid = @RulePool_StepGuid, @EventDscrpn = @ERROR_MES;
	      RETURN @ERROR_NUM;
      END 

    -- Проверяем наличие записи о товаре в справочнике
    IF NOT EXISTS ( SELECT Parts_Guid FROM dbo.T_Parts WHERE Parts_Guid = @Parts_Guid )
      BEGIN
        SET @ERROR_NUM = 5;
        SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) +  '[SP_GetPrice_02] Товар с заданным идентификатором не найден в справочнике.'  + nChar(13) + nChar(10) +  'Идентификатор: ' + CONVERT( nvarchar(36), @Parts_Guid );

				EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 96, @RulePool_StepGuid = @RulePool_StepGuid, @EventDscrpn = @ERROR_MES;
	      RETURN @ERROR_NUM;
      END 

    -- Скидка
    DECLARE @DiscountPercent decimal(18, 4);
    SELECT @DiscountPercent = dbo.GetDiscountPercentFromXml(@SupplItmsInfo);
    IF( ( @DiscountPercent IS NULL ) OR ( @DiscountPercent < 0 ) )
      SET @DiscountPercent = 0;
    
    -- Запрашиваем цену
    DECLARE	@NDSPercent D_MONEY;
		DECLARE	@PriceImporter D_MONEY;
		DECLARE	@Price D_MONEY;
		DECLARE	@PriceWithDiscount D_MONEY;
		DECLARE	@PriceInAccountingCurrency D_MONEY;
		DECLARE	@PriceWithDiscountInAccountingCurrency D_MONEY;
		DECLARE @IsPartsImporter D_YESNO;
		DECLARE @ChargesPercent D_MONEY;
		DECLARE @PriceImporterFromPriceList D_MONEY;
		DECLARE @PriceFromPriceList D_MONEY;
		DECLARE @PriceInAccountingCurrencyFromPriceList D_MONEY;

		EXEC [dbo].[usp_GetPriceCalculation] @Parts_Guid = @Parts_Guid, @PaymentType_Guid = @PaymentType_Guid, 
			@Stock_Guid = @Stock_Guid, @DiscountPercent = @DiscountPercent, @NDSPercent = @NDSPercent,
			@PriceImporter = @PriceImporter out, @Price = @Price out,
			@PriceWithDiscount = @PriceWithDiscount out,
			@PriceInAccountingCurrency = @PriceInAccountingCurrency out,
			@PriceWithDiscountInAccountingCurrency = @PriceWithDiscountInAccountingCurrency out,
			@IsPartsImporter = @IsPartsImporter out, @ChargesPercent = @ChargesPercent out,
			@PriceImporterFromPriceList = @PriceImporterFromPriceList out,
			@PriceFromPriceList = @PriceFromPriceList out,
			@PriceInAccountingCurrencyFromPriceList = @PriceInAccountingCurrencyFromPriceList out,
		  @ERROR_NUM = @ERROR_NUM out, @ERROR_MES = @ERROR_MES out ;    
		
		IF( @ERROR_NUM <> 0 )   
			BEGIN
				EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 92, @RulePool_StepGuid = @RulePool_StepGuid, @EventDscrpn = @ERROR_MES;
	      RETURN @ERROR_NUM;
			END

   IF( ( @PriceImporter = 0 ) OR ( @Price = 0 ) OR ( @PriceInAccountingCurrency = 0 ) )
      BEGIN
        SET @ERROR_NUM = 10;
        SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) + 
					'@PriceImporter: ' + CONVERT( nvarchar(15), @PriceImporter ) + nChar(13) + nChar(10) + 
					'@PriceInAccountingCurrency: ' + CONVERT( nvarchar(15), @PriceInAccountingCurrency )  + nChar(13) + nChar(10) + 
					'@Price: ' + CONVERT( nvarchar(15), @Price );

				EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 92, @RulePool_StepGuid = @RulePool_StepGuid, @EventDscrpn = @ERROR_MES;
	      RETURN @ERROR_NUM;
      END 

    -- теперь нужно внести изменения в объект "Цена"
    SELECT @SupplItmsInfo = dbo.SetPrice0InXml( @SupplItmsInfo, @PriceImporter );
    SELECT @SupplItmsInfo = dbo.SetPriceInXml( @SupplItmsInfo, @Price );
    SELECT @SupplItmsInfo = dbo.SetPriceCurrencyInXml( @SupplItmsInfo, @PriceInAccountingCurrency );
    SELECT @SupplItmsInfo = dbo.SetDiscountPriceInXml( @SupplItmsInfo, @PriceWithDiscount );
    SELECT @SupplItmsInfo = dbo.SetDiscountPriceCurrencyInXml( @SupplItmsInfo, @PriceWithDiscountInAccountingCurrency );
    SELECT @SupplItmsInfo = dbo.SetPriceList_Price0InXml( @SupplItmsInfo, @PriceImporterFromPriceList );
    SELECT @SupplItmsInfo = dbo.SetPriceList_PriceInXml( @SupplItmsInfo, @PriceFromPriceList );
    SELECT @SupplItmsInfo = dbo.SetPriceList_PriceCurrencyInXml( @SupplItmsInfo, @PriceInAccountingCurrencyFromPriceList );
    SELECT @SupplItmsInfo = dbo.SetNDSPercentInXml( @SupplItmsInfo, @NDSPercent );
    SELECT @SupplItmsInfo = dbo.SetImporterInXml( @SupplItmsInfo, @IsPartsImporter );
    SELECT @SupplItmsInfo = dbo.SetMarkupPercentInXml( @SupplItmsInfo, @ChargesPercent );
    ---- 2009.05.04
    ---- дописываем рассчитанную оптовую надбавку = скидка 
    ---- это необходимо для того, чтобы цены на стороне IB совпадали с ценами в ERP
    --SELECT @SupplItmsInfo = dbo.SetCalcMarkUpPercentInXml( @SupplItmsInfo, @CalcMarkUpPercent );

		SET @ERROR_MES = ' Цена импортера в прайсе: ' + CONVERT( varchar(16), @PriceImporterFromPriceList )+ nChar(13) + nChar(10) + 
			' Цена импортера: ' + CONVERT( varchar(16), @PriceImporter )+ nChar(13) + nChar(10) + 
			' Цена, руб.: ' +  CONVERT( varchar(16), @Price ) + nChar(13) + nChar(10) + 
			' Цена, вал.: ' + CONVERT( varchar(16), @PriceInAccountingCurrency ) + nChar(13) + nChar(10) + 
			' Скидка, %: ' + CONVERT( varchar(8), @DiscountPercent ) + nChar(13) + nChar(10) + 
			' Надбавка (max), %: ' + CONVERT( varchar(8), @ChargesPercent ) + nChar(13) + nChar(10) + 
			--' Надбавка (расчет.), %: ' + CONVERT( varchar(8), @CalcMarkUpPercent ) + nChar(13) + nChar(10) + 
			' Цена со скидкой, руб.: ' + CONVERT( varchar(16), @PriceWithDiscount ) + nChar(13) + nChar(10) + 
			' Цена со скидкой, вал.: ' + CONVERT( varchar(16), @PriceWithDiscountInAccountingCurrency ) + nChar(13) + nChar(10) + 
			' Ставка НДС, %: ' + CONVERT( varchar(8), @NDSPercent ) + nChar(13) + nChar(10) + 
			' Надбавка, %: ' + CONVERT( varchar(8), @ChargesPercent ) + nChar(13) + nChar(10) + 
			' Признак "Импортер": ' + CONVERT( varchar(8), @IsPartsImporter );
			 --+ nChar(13) + nChar(10) + ' Курс: ' + CONVERT( varchar(16), @CurrencyRate );

		EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 90, @RulePool_StepGuid = @RulePool_StepGuid, @EventDscrpn = @ERROR_MES;
    SET @ERROR_NUM = 0;
    
	END TRY
	BEGIN CATCH
		SET @ERROR_NUM = ERROR_NUMBER();
		SET @ERROR_MES = @ERROR_MES  + nChar(13) + nChar(10) +  ' [SP_GetPrice_02] Текст ошибки: ' + ERROR_MESSAGE();

		EXEC sp_AddEventLogForCalcPrice @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo, @EventType_Id = 91, @RulePool_StepGuid = @RulePool_StepGuid, @EventDscrpn = @ERROR_MES;
    RETURN @ERROR_NUM;
	END CATCH;

	IF( @ERROR_NUM = 0 )
		SET @ERROR_MES = @ERROR_MES + ' [SP_GetPrice_02] Успешное завершение операции.';
	RETURN @ERROR_NUM;
END

GO
GRANT EXECUTE ON [dbo].[SP_GetPrice_02] TO [public]
GO

/****** Object:  StoredProcedure [dbo].[sp_GetInfoAboutAdvParams]    Script Date: 03/25/2012 21:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Возвращает информацию о дополнительных параметрах, которые обрабатывает хранимая процедура
--
-- Входящие параметры:
--	@StoredProcedure_Name - имя хранимой процедуры
--
-- Выходные параметры:
--  @SPParamsInfo - xml документ с информацией о дополнительных параметрах
--  @ERROR_NUM - код ошибки
--  @ERROR_MES - текст ошибки
--
-- Результат:
--    0 - успешное завершение
--    <>0 - ошибка

ALTER PROCEDURE [dbo].[sp_GetInfoAboutAdvParams] 
	@SupplItms_Guid D_GUID = NULL,
  @StoredProcedure_Name D_NAME,
  --@SPParamsInfo xml  output,
  @SPParamsInfo xml (DOCUMENT RuleParams) output,

  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output

WITH EXECUTE AS 'ERP_MercuryAdmin' 

AS

BEGIN
 		PRINT 1;

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = NULL;

	BEGIN TRY
     
    DECLARE @SQLString nvarchar(500);
    DECLARE @ParmDefinition nvarchar(500);
    DECLARE @SupplItmsInfo xml (DOCUMENT InfoForCalcPriceSchema);
    DECLARE @RulePool_StepGuid D_GUID;
    DECLARE @Spl_Guid D_GUID;
    SET @Spl_Guid = NULL;

    SET @SQLString = 'EXEC dbo.' + @StoredProcedure_Name +  ' @Spl_Guid, @SupplItms_Guid, @SupplItmsInfo output, @SPParamsInfo output, @RulePool_StepGuid, @DiscountListInfo output, @ERROR_NUM output, @ERROR_MES output';

    --SET @ParmDefinition = N'@SupplItmsInfo xml (DOCUMENT InfoForCalcPriceSchema) output, @SPParamsInfo xml output,  @RulePool_StepGuid uniqueidentifier, @DiscountListInfo xml ( DOCUMENT DiscountList ) output, @ERROR_NUM int output, @ERROR_MES nvarchar(4000) output';

    SET @ParmDefinition = N'@Spl_Guid uniqueidentifier, @SupplItms_Guid uniqueidentifier, @SupplItmsInfo xml (DOCUMENT InfoForCalcPriceSchema) output, @SPParamsInfo xml (DOCUMENT RuleParams) output,  @RulePool_StepGuid uniqueidentifier, @DiscountListInfo xml ( DOCUMENT DiscountList ) output, @ERROR_NUM int output, @ERROR_MES nvarchar(4000) output';

    EXECUTE sp_executesql  @SQLString, @ParmDefinition, 
      @Spl_Guid = @Spl_Guid, @SupplItms_Guid = @SupplItms_Guid, @SupplItmsInfo = @SupplItmsInfo output, @SPParamsInfo = @SPParamsInfo output, 
      @RulePool_StepGuid = @RulePool_StepGuid, @DiscountListInfo = NULL, @ERROR_NUM = @ERROR_NUM output, @ERROR_MES = @ERROR_MES output
      ;
      
    IF( @ERROR_NUM <> 0 )  
      BEGIN
		    RETURN @ERROR_NUM;
      END

	END TRY
	BEGIN CATCH
		SET @ERROR_NUM = ERROR_NUMBER();
		SET @ERROR_MES = '[sp_GetInfoAboutAdvParams], Текст ошибки: ' + ERROR_MESSAGE();
		RETURN @ERROR_NUM;
	END CATCH;

	SET @ERROR_NUM = 0;
	SET @ERROR_MES = 'Успешное завершение операции.';
	RETURN @ERROR_NUM;
END

SET ANSI_NULLS ON

GO


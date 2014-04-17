USE [ERP_Mercury]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_WaybillShipMode](
	[WaybillShipMode_Guid] [dbo].[D_GUID] NOT NULL,
	[WaybillShipMode_Name] [dbo].[D_NAME] NOT NULL,
	[WaybillShipMode_Description] [dbo].[D_DESCRIPTION] NULL,
	[WaybillShipMode_IsActive] [dbo].[D_ISACTIVE] NOT NULL,
	[WaybillShipMode_IsDefault] [dbo].[D_YESNO] NOT NULL,
	[WaybillShipMode_Id] [dbo].[D_ID] NOT NULL,
	[Record_Updated] [dbo].[D_DATETIME] NULL,
	[Record_UserUdpated] [dbo].[D_NAMESHORT] NULL,
 CONSTRAINT [PK_T_WaybillShipMode] PRIMARY KEY CLUSTERED 
(
	[WaybillShipMode_Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE UNIQUE NONCLUSTERED INDEX [INDX_T_WaybillShipMode_WaybillShipModeId] ON [dbo].[T_WaybillShipMode]
(
	[WaybillShipMode_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [INDEX]
GO

CREATE NONCLUSTERED INDEX [INDX_T_WaybillShipMode_WaybillShipModeIsDefault] ON [dbo].[T_WaybillShipMode]
(
	[WaybillShipMode_IsDefault] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [INDEX]
GO

CREATE UNIQUE NONCLUSTERED INDEX [INDX_T_WaybillShipMode_WaybillShipModeName] ON [dbo].[T_WaybillShipMode]
(
	[WaybillShipMode_Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [INDEX]
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Добавляет новую запись в таблицу dbo.T_WaybillShipMode (вид отгрузки)
--
-- Входные параметры:
--
--		@WaybillShipMode_Name					наименование вида отгрузки
--		@WaybillShipMode_Description	примечание
--		@WaybillShipMode_IsActive			признак "запись используется в новых документах"
--		@WaybillShipMode_IsDefault		признак "использовать по умолчанию в документах"
--		@WaybillShipMode_Id						целочисленный код вида отгрузки
--
-- Выходные параметры:
--
--  @WaybillShipMode_Guid					уникальный идентификатор записи
--  @ERROR_NUM											код ошибки
--  @ERROR_MES											текст ошибки
--
-- Результат:
--    0 - Успешное завершение
--    <>0 - ошибка

CREATE PROCEDURE [dbo].[usp_AddWaybillShipMode] 
	@WaybillShipMode_Name					[dbo].[D_NAME],
	@WaybillShipMode_Description	[dbo].[D_DESCRIPTION] = NULL,
	@WaybillShipMode_IsActive			[dbo].[D_ISACTIVE] = 1,
	@WaybillShipMode_IsDefault		[dbo].[D_YESNO] = 0,
	@WaybillShipMode_Id						[dbo].[D_ID],

  @WaybillShipMode_Guid					D_GUID output,
  @ERROR_NUM								D_ID output,
  @ERROR_MES								D_ERROR_MESSAGE output

AS

BEGIN

	BEGIN TRY

    SET @ERROR_NUM = 0;
    SET @ERROR_MES = '';
    SET @WaybillShipMode_Guid = NULL;

    -- Проверяем наличие записи с заданным именем
    IF EXISTS ( SELECT * FROM dbo.T_WaybillShipMode	WHERE WaybillShipMode_Name = @WaybillShipMode_Name )
      BEGIN
        SET @ERROR_NUM = 1;
        SET @ERROR_MES = 'В базе данных уже зарегистрирован вид отгрузки с указанным наименованием.' + Char(13) + 
          'Вид оплаты: ' + Char(9) + @WaybillShipMode_Name;
        RETURN @ERROR_NUM;
      END

    -- Проверяем наличие записи с заданным кодом
    IF EXISTS ( SELECT * FROM dbo.T_WaybillShipMode	WHERE WaybillShipMode_Id = @WaybillShipMode_Id )
      BEGIN
        SET @ERROR_NUM = 2;
        SET @ERROR_MES = 'В базе данных уже зарегистрирован вид отгрузки с указанным кодом.' + Char(13) + 
          'Код оплаты: ' + Char(9) + CONVERT( nvarchar(16),  @WaybillShipMode_Id );
        RETURN @ERROR_NUM;
      END

    DECLARE @NewID D_GUID;
    SET @NewID = NEWID ( );	
    
    INSERT INTO [dbo].[T_WaybillShipMode]( WaybillShipMode_Guid, WaybillShipMode_Name, WaybillShipMode_Description, WaybillShipMode_IsActive, 
			WaybillShipMode_IsDefault, WaybillShipMode_Id, Record_Updated, Record_UserUdpated )
    VALUES( @NewID, @WaybillShipMode_Name, @WaybillShipMode_Description, @WaybillShipMode_IsActive, 
			@WaybillShipMode_IsDefault, @WaybillShipMode_Id, sysutcdatetime(), ( Host_Name() + ': ' + SUSER_SNAME() ) );
    
    SET @WaybillShipMode_Guid = @NewID;

		IF( @WaybillShipMode_IsDefault = 1 )
			UPDATE [dbo].[T_WaybillShipMode] SET WaybillShipMode_IsDefault = 0 WHERE WaybillShipMode_Guid <> @WaybillShipMode_Guid;

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
GRANT EXECUTE ON [dbo].[usp_AddWaybillShipMode] TO [public]
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Удаление элемента из таблицы dbo.T_WaybillShipMode
--
-- Входные параметры:
--
--		@WaybillShipMode_Guid			УИ записи
--
-- Выходные параметры:
--
--  @ERROR_NUM									код ошибки
--  @ERROR_MES									текст ошибки
--
-- Результат:
--		0 - Успешное завершение
--		<>0 - ошибка
--

CREATE PROCEDURE [dbo].[usp_DeleteWaybillShipMode] 
	@WaybillShipMode_Guid		D_GUID,

  @ERROR_NUM							D_ID output,
  @ERROR_MES							D_ERROR_MESSAGE output

AS

BEGIN

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = '';

	BEGIN TRY

    ---- Проверяем наличие записи с заданным именем
    --IF EXISTS ( SELECT * FROM dbo.T_Waybill	WHERE ( WaybillShipMode_Guid IS NOT NULL ) AND ( WaybillShipMode_Guid = @WaybillShipMode_Guid ) )
    --  BEGIN
    --    SET @ERROR_NUM = 1;
    --    SET @ERROR_MES = 'На указанный вид отгрузки есть ссылка в журнале накладных. Операция удаления отменена.';
        
				--RETURN @ERROR_NUM;
    --  END

   DELETE FROM dbo.T_WaybillShipMode WHERE WaybillShipMode_Guid = @WaybillShipMode_Guid;

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
GRANT EXECUTE ON [dbo].[usp_DeleteWaybillShipMode] TO [public]
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Редактирует запись в таблице dbo.T_WaybillShipMode
--
-- Входные параметры:
--
--  @WaybillShipMode_Guid					уникальный идентификатор записи
--		@WaybillShipMode_Name					наименование вида отгрузки
--		@WaybillShipMode_Description	примечание
--		@WaybillShipMode_IsActive			признак "запись используется в новых документах"
--		@WaybillShipMode_IsDefault		признак "использовать по умолчанию в документах"
--		@WaybillShipMode_Id						целочисленный код вида отгрузки
--
-- Выходные параметры:
--
--  @ERROR_NUM											код ошибки
--  @ERROR_MES											текст ошибки
--
-- Результат:
--    0 - Успешное завершение
--    <>0 - ошибка

CREATE PROCEDURE [dbo].[usp_EditWaybillShipMode] 
  @WaybillShipMode_Guid					D_GUID,
	@WaybillShipMode_Name					[dbo].[D_NAME],
	@WaybillShipMode_Description	[dbo].[D_DESCRIPTION] = NULL,
	@WaybillShipMode_IsActive			[dbo].[D_ISACTIVE] = 1,
	@WaybillShipMode_IsDefault		[dbo].[D_YESNO] = 0,
	@WaybillShipMode_Id						[dbo].[D_ID],

  @ERROR_NUM										D_ID output,
  @ERROR_MES										D_ERROR_MESSAGE output

AS

BEGIN

	BEGIN TRY

    SET @ERROR_NUM = 0;
    SET @ERROR_MES = '';

    -- Проверяем наличие записи с заданным именем
    IF EXISTS ( SELECT * FROM dbo.T_WaybillShipMode	WHERE WaybillShipMode_Name = @WaybillShipMode_Name AND WaybillShipMode_Guid <> @WaybillShipMode_Guid )
      BEGIN
        SET @ERROR_NUM = 1;
        SET @ERROR_MES = 'В базе данных уже зарегистрирован вид отгрузки с указанным наименованием.' + Char(13) + 
          'Вид отгрузки: ' + Char(9) + @WaybillShipMode_Name;
        RETURN @ERROR_NUM;
      END

    -- Проверяем наличие записи с заданным кодом
    IF EXISTS ( SELECT * FROM dbo.T_WaybillShipMode	WHERE WaybillShipMode_Id = @WaybillShipMode_Id AND WaybillShipMode_Guid <> @WaybillShipMode_Guid )
      BEGIN
        SET @ERROR_NUM = 2;
        SET @ERROR_MES = 'В базе данных уже зарегистрирован вид отгрузки с указанным кодом.' + Char(13) + 
          'Код вида отгрузки: ' + Char(9) + CONVERT( nvarchar(16),  @WaybillShipMode_Id );
        RETURN @ERROR_NUM;
      END

    UPDATE [dbo].[T_WaybillShipMode]	SET WaybillShipMode_Name = @WaybillShipMode_Name, WaybillShipMode_Description = @WaybillShipMode_Description, 
			WaybillShipMode_IsActive = @WaybillShipMode_IsActive, WaybillShipMode_IsDefault = @WaybillShipMode_IsDefault, 
			WaybillShipMode_Id = @WaybillShipMode_Id, 
			Record_Updated = sysutcdatetime(), Record_UserUdpated = ( Host_Name() + ': ' + SUSER_SNAME() )
		WHERE WaybillShipMode_Guid = @WaybillShipMode_Guid;
    
		IF( @WaybillShipMode_IsDefault = 1 )
			UPDATE [dbo].[T_WaybillShipMode] SET WaybillShipMode_IsDefault = 0 WHERE WaybillShipMode_Guid <> @WaybillShipMode_Guid;

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
GRANT EXECUTE ON [dbo].[usp_EditWaybillShipMode] TO [public]
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Возвращает список записей из таблицы T_WaybillShipMode
--
-- Входные параметры:
--
--		@WaybillShipMode_Guid			УИ записи
--
-- Выходные параметры:
--
--  @ERROR_NUM									код ошибки
--  @ERROR_MES									текст ошибки
--
-- Результат:
--		0 - Успешное завершение
--		<>0 - ошибка
--

CREATE PROCEDURE [dbo].[usp_GetWaybillShipMode] 
	@WaybillShipMode_Guid		D_GUID = NULL,
	
  @ERROR_NUM					D_ID output,
  @ERROR_MES					D_ERROR_MESSAGE output
AS

BEGIN

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = '';

  BEGIN TRY

    IF( @WaybillShipMode_Guid IS NULL )
			BEGIN
				SELECT WaybillShipMode_Guid, WaybillShipMode_Name, WaybillShipMode_Description, WaybillShipMode_IsActive, 
					WaybillShipMode_IsDefault, WaybillShipMode_Id, Record_Updated, Record_UserUdpated
				FROM [dbo].[T_WaybillShipMode]
				ORDER BY WaybillShipMode_Id;
			END
		ELSE	
			BEGIN
				SELECT WaybillShipMode_Guid, WaybillShipMode_Name, WaybillShipMode_Description, WaybillShipMode_IsActive, 
					WaybillShipMode_IsDefault, WaybillShipMode_Id, Record_Updated, Record_UserUdpated
				FROM [dbo].[T_WaybillShipMode]
				WHERE WaybillShipMode_Guid = @WaybillShipMode_Guid;
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
GRANT EXECUTE ON [dbo].[usp_GetWaybillShipMode] TO [public]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_WaybillState](
	[WaybillState_Guid] [dbo].[D_GUID] NOT NULL,
	[WaybillState_Name] [dbo].[D_NAME] NOT NULL,
	[WaybillState_Description] [dbo].[D_DESCRIPTION] NULL,
	[WaybillState_IsActive] [dbo].[D_ISACTIVE] NOT NULL,
	[WaybillState_IsDefault] [dbo].[D_YESNO] NOT NULL,
	[WaybillState_Id] [dbo].[D_ID] NOT NULL,
	[Record_Updated] [dbo].[D_DATETIME] NULL,
	[Record_UserUdpated] [dbo].[D_NAMESHORT] NULL,
 CONSTRAINT [PK_T_WaybillState] PRIMARY KEY CLUSTERED 
(
	[WaybillState_Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE UNIQUE NONCLUSTERED INDEX [INDX_T_WaybillState_WaybillStateId] ON [dbo].[T_WaybillState]
(
	[WaybillState_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [INDEX]
GO

CREATE NONCLUSTERED INDEX [INDX_T_WaybillState_WaybillStateIsDefault] ON [dbo].[T_WaybillState]
(
	[WaybillState_IsDefault] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [INDEX]
GO

CREATE UNIQUE NONCLUSTERED INDEX [INDX_T_WaybillState_WaybillStateName] ON [dbo].[T_WaybillState]
(
	[WaybillState_Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [INDEX]
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Добавляет новую запись в таблицу dbo.T_WaybillState (состояние накладной)
--
-- Входные параметры:
--
--		@WaybillState_Name					наименование состояния накладной
--		@WaybillState_Description		примечание
--		@WaybillState_IsActive			признак "запись используется в новых документах"
--		@WaybillState_IsDefault			признак "использовать по умолчанию в документах"
--		@WaybillState_Id						целочисленный код состояния накладной
--
-- Выходные параметры:
--
--  @WaybillState_Guid						уникальный идентификатор записи
--  @ERROR_NUM										код ошибки
--  @ERROR_MES										текст ошибки
--
-- Результат:
--    0 - Успешное завершение
--    <>0 - ошибка

CREATE PROCEDURE [dbo].[usp_AddWaybillState] 
	@WaybillState_Name						[dbo].[D_NAME],
	@WaybillState_Description			[dbo].[D_DESCRIPTION] = NULL,
	@WaybillState_IsActive				[dbo].[D_ISACTIVE] = 1,
	@WaybillState_IsDefault				[dbo].[D_YESNO] = 0,
	@WaybillState_Id							[dbo].[D_ID],

  @WaybillState_Guid						D_GUID output,
  @ERROR_NUM										D_ID output,
  @ERROR_MES										D_ERROR_MESSAGE output

AS

BEGIN

	BEGIN TRY

    SET @ERROR_NUM = 0;
    SET @ERROR_MES = '';
    SET @WaybillState_Guid = NULL;

    -- Проверяем наличие записи с заданным именем
    IF EXISTS ( SELECT * FROM dbo.T_WaybillState	WHERE WaybillState_Name = @WaybillState_Name )
      BEGIN
        SET @ERROR_NUM = 1;
        SET @ERROR_MES = 'В базе данных уже зарегистрировано состояние накладной с указанным наименованием.' + Char(13) + 
          'Состояние накладной: ' + Char(9) + @WaybillState_Name;
        RETURN @ERROR_NUM;
      END

    -- Проверяем наличие записи с заданным кодом
    IF EXISTS ( SELECT * FROM dbo.T_WaybillState	WHERE WaybillState_Id = @WaybillState_Id )
      BEGIN
        SET @ERROR_NUM = 2;
        SET @ERROR_MES = 'В базе данных уже зарегистрировано состояние накладной с указанным кодом.' + Char(13) + 
          'Код состояния: ' + Char(9) + CONVERT( nvarchar(16),  @WaybillState_Id );
        RETURN @ERROR_NUM;
      END

    DECLARE @NewID D_GUID;
    SET @NewID = NEWID ( );	
    
    INSERT INTO [dbo].[T_WaybillState]( WaybillState_Guid, WaybillState_Name, WaybillState_Description, WaybillState_IsActive, 
			WaybillState_IsDefault, WaybillState_Id, Record_Updated, Record_UserUdpated )
    VALUES( @NewID, @WaybillState_Name, @WaybillState_Description, @WaybillState_IsActive, 
			@WaybillState_IsDefault, @WaybillState_Id, sysutcdatetime(), ( Host_Name() + ': ' + SUSER_SNAME() ) );
    
    SET @WaybillState_Guid = @NewID;

		IF( @WaybillState_IsDefault = 1 )
			UPDATE [dbo].[T_WaybillState] SET WaybillState_IsDefault = 0 WHERE WaybillState_Guid <> @WaybillState_Guid;

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
GRANT EXECUTE ON [dbo].[usp_AddWaybillState] TO [public]
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Удаление элемента из таблицы dbo.T_WaybillState
--
-- Входные параметры:
--
--		@WaybillState_Guid				УИ записи
--
-- Выходные параметры:
--
--  @ERROR_NUM									код ошибки
--  @ERROR_MES									текст ошибки
--
-- Результат:
--		0 - Успешное завершение
--		<>0 - ошибка
--

CREATE PROCEDURE [dbo].[usp_DeleteWaybillState] 
	@WaybillState_Guid		D_GUID,

  @ERROR_NUM							D_ID output,
  @ERROR_MES							D_ERROR_MESSAGE output

AS

BEGIN

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = '';

	BEGIN TRY

    ---- Проверяем наличие записи с заданным именем
    --IF EXISTS ( SELECT * FROM dbo.T_Waybill	WHERE ( WaybillState_Guid IS NOT NULL ) AND ( WaybillState_Guid = @WaybillState_Guid ) )
    --  BEGIN
    --    SET @ERROR_NUM = 1;
    --    SET @ERROR_MES = 'На указанное состояние накладной есть ссылка в журнале накладных. Операция удаления отменена.';
        
				--RETURN @ERROR_NUM;
    --  END

   DELETE FROM dbo.T_WaybillState WHERE WaybillState_Guid = @WaybillState_Guid;

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
GRANT EXECUTE ON [dbo].[usp_DeleteWaybillState] TO [public]
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Редактирует запись в таблице dbo.T_WaybillState
--
-- Входные параметры:
--
--  @WaybillState_Guid						уникальный идентификатор записи
--		@WaybillState_Name					наименование состояния накладной
--		@WaybillState_Description		примечание
--		@WaybillState_IsActive			признак "запись используется в новых документах"
--		@WaybillState_IsDefault			признак "использовать по умолчанию в документах"
--		@WaybillState_Id						целочисленный код состояния накладной
--
-- Выходные параметры:
--
--  @ERROR_NUM											код ошибки
--  @ERROR_MES											текст ошибки
--
-- Результат:
--    0 - Успешное завершение
--    <>0 - ошибка

CREATE PROCEDURE [dbo].[usp_EditWaybillState] 
  @WaybillState_Guid					D_GUID,
	@WaybillState_Name					[dbo].[D_NAME],
	@WaybillState_Description		[dbo].[D_DESCRIPTION] = NULL,
	@WaybillState_IsActive			[dbo].[D_ISACTIVE] = 1,
	@WaybillState_IsDefault			[dbo].[D_YESNO] = 0,
	@WaybillState_Id						[dbo].[D_ID],

  @ERROR_NUM									D_ID output,
  @ERROR_MES									D_ERROR_MESSAGE output

AS

BEGIN

	BEGIN TRY

    SET @ERROR_NUM = 0;
    SET @ERROR_MES = '';

    -- Проверяем наличие записи с заданным именем
    IF EXISTS ( SELECT * FROM dbo.T_WaybillState	WHERE WaybillState_Name = @WaybillState_Name AND WaybillState_Guid <> @WaybillState_Guid )
      BEGIN
        SET @ERROR_NUM = 1;
        SET @ERROR_MES = 'В базе данных уже зарегистрировано состояние накладной с указанным наименованием.' + Char(13) + 
          'Состояние накладной: ' + Char(9) + @WaybillState_Name;
        RETURN @ERROR_NUM;
      END

    -- Проверяем наличие записи с заданным кодом
    IF EXISTS ( SELECT * FROM dbo.T_WaybillState	WHERE WaybillState_Id = @WaybillState_Id AND WaybillState_Guid <> @WaybillState_Guid )
      BEGIN
        SET @ERROR_NUM = 2;
        SET @ERROR_MES = 'В базе данных уже зарегистрировано состояние накладной с указанным кодом.' + Char(13) + 
          'Код состояния накладной: ' + Char(9) + CONVERT( nvarchar(16),  @WaybillState_Id );
        RETURN @ERROR_NUM;
      END

    UPDATE [dbo].[T_WaybillState]	SET WaybillState_Name = @WaybillState_Name, WaybillState_Description = @WaybillState_Description, 
			WaybillState_IsActive = @WaybillState_IsActive, WaybillState_IsDefault = @WaybillState_IsDefault, 
			WaybillState_Id = @WaybillState_Id, 
			Record_Updated = sysutcdatetime(), Record_UserUdpated = ( Host_Name() + ': ' + SUSER_SNAME() )
		WHERE WaybillState_Guid = @WaybillState_Guid;
    
		IF( @WaybillState_IsDefault = 1 )
			UPDATE [dbo].[T_WaybillState] SET WaybillState_IsDefault = 0 WHERE WaybillState_Guid <> @WaybillState_Guid;

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
GRANT EXECUTE ON [dbo].[usp_EditWaybillState] TO [public]
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Возвращает список записей из таблицы T_WaybillState
--
-- Входные параметры:
--
--		@WaybillState_Guid			УИ записи
--
-- Выходные параметры:
--
--  @ERROR_NUM								код ошибки
--  @ERROR_MES								текст ошибки
--
-- Результат:
--		0 - Успешное завершение
--		<>0 - ошибка
--

CREATE PROCEDURE [dbo].[usp_GetWaybillState] 
	@WaybillState_Guid		D_GUID = NULL,
	
  @ERROR_NUM					D_ID output,
  @ERROR_MES					D_ERROR_MESSAGE output
AS

BEGIN

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = '';

  BEGIN TRY

    IF( @WaybillState_Guid IS NULL )
			BEGIN
				SELECT WaybillState_Guid, WaybillState_Name, WaybillState_Description, WaybillState_IsActive, 
					WaybillState_IsDefault, WaybillState_Id, Record_Updated, Record_UserUdpated
				FROM [dbo].[T_WaybillState]
				ORDER BY WaybillState_Id;
			END
		ELSE	
			BEGIN
				SELECT WaybillState_Guid, WaybillState_Name, WaybillState_Description, WaybillState_IsActive, 
					WaybillState_IsDefault, WaybillState_Id, Record_Updated, Record_UserUdpated
				FROM [dbo].[T_WaybillState]
				WHERE WaybillState_Guid = @WaybillState_Guid;
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
GRANT EXECUTE ON [dbo].[usp_GetWaybillState] TO [public]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_Waybill](
	[Waybill_Guid] [dbo].[D_GUID] NOT NULL,
	[Waybill_Id] [dbo].[D_INTEGER] NULL,
	[Suppl_Guid] [dbo].[D_GUID]  NULL,
	[Stock_Guid] [dbo].[D_GUID]  NOT NULL,
	[Company_Guid] [dbo].[D_GUID]  NOT NULL,
	[Currency_Guid] [dbo].[D_GUID] NOT NULL,
	[Depart_Guid] [dbo].[D_GUID] NOT NULL,
	[Customer_Guid] [dbo].[D_GUID] NOT NULL,
	[CustomerChild_Guid] [dbo].[D_GUID] NULL,
	[Rtt_Guid] [dbo].[D_GUID] NOT NULL,
	[Address_Guid] [dbo].[D_GUID] NOT NULL,
	[PaymentType_Guid] [dbo].[D_GUID] NULL,

	[Waybill_Num] [dbo].[D_NAMESHORT] NOT NULL,
	[Waybill_BeginDate] [dbo].[D_DATE] NOT NULL,
	[Waybill_DeliveryDate] [dbo].[D_DATE] NOT NULL,
	[WaybillParent_Guid] [dbo].[D_GUID] NULL,
	
	[Waybill_Bonus] [dbo].[D_YESNO] NOT NULL,
	[WaybillState_Guid] [dbo].[D_GUID] NOT NULL,
	[WaybillShipMode_Guid] [dbo].[D_GUID] NOT NULL,
	[Waybill_ShipDate] [dbo].[D_DATE] NULL,
	[Waybill_Description] [dbo].[D_DESCRIPTION] NULL,
	[Waybill_CurrencyRate] [dbo].[D_MONEY] NOT NULL,

	[Waybill_AllPrice] [dbo].[D_MONEY] NOT NULL,
	[Waybill_RetAllPrice] [dbo].[D_MONEY] NOT NULL,
	[Waybill_AllDiscount] [dbo].[D_MONEY] NOT NULL,
	[Waybill_TotalPrice]  AS ([Waybill_AllPrice]-[Waybill_AllDiscount]),
	[Waybill_AmountPaid] [dbo].[D_MONEY] NOT NULL,
	[Waybill_Saldo]  AS ([Waybill_AllPrice]-[Waybill_AllDiscount] + [Waybill_RetAllPrice] - [Waybill_AmountPaid]),

	[Waybill_CurrencyAllPrice] [dbo].[D_MONEY] NOT NULL,
	[Waybill_CurrencyRetAllPrice] [dbo].[D_MONEY] NOT NULL,
	[Waybill_CurrencyAllDiscount] [dbo].[D_MONEY] NOT NULL,
	[Waybill_CurrencyTotalPrice]  AS ([Waybill_AllPrice]-[Waybill_AllDiscount]),
	[Waybill_CurrencyAmountPaid] [dbo].[D_MONEY] NOT NULL,
	[Waybill_CurrencySaldo]  AS ([Waybill_AllPrice]-[Waybill_AllDiscount] + [Waybill_CurrencyRetAllPrice] - [Waybill_CurrencyAmountPaid]),

	[Waybill_Quantity] [dbo].[D_QUANTITY] NOT NULL,
	[Waybill_RetQuantity] [dbo].[D_QUANTITY] NOT NULL,
	[Waybill_LeavQuantity] AS ([Waybill_Quantity] - [Waybill_RetQuantity]),

	[Waybill_Weight] [dbo].[D_WEIGHT] NOT NULL,
	[Waybill_ShowInDeliveryList] [dbo].[D_YESNO] NULL,

 CONSTRAINT [PK_T_Waybill] PRIMARY KEY CLUSTERED 
(
	[Waybill_Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[T_Waybill] ADD  CONSTRAINT [DF_T_Waybill_Waybill_Bonus]  DEFAULT ((0)) FOR [Waybill_Bonus]
GO

ALTER TABLE [dbo].[T_Waybill] ADD  CONSTRAINT [DF_T_Waybill_Waybill_Quantity]  DEFAULT ((0)) FOR [Waybill_Quantity]
GO

ALTER TABLE [dbo].[T_Waybill] ADD  CONSTRAINT [DF_T_Waybill_Waybill_CurrencyRate]  DEFAULT ((0)) FOR [Waybill_CurrencyRate]
GO

ALTER TABLE [dbo].[T_Waybill] ADD  CONSTRAINT [DF_T_Waybill_Waybill_AllPrice]  DEFAULT ((0)) FOR [Waybill_AllPrice]
GO

ALTER TABLE [dbo].[T_Waybill] ADD  CONSTRAINT [DF_T_Waybill_Waybill_RetAllPrice]  DEFAULT ((0)) FOR [Waybill_RetAllPrice]
GO

ALTER TABLE [dbo].[T_Waybill] ADD  CONSTRAINT [DF_T_Waybill_Waybill_AllDiscount]  DEFAULT ((0)) FOR [Waybill_AllDiscount]
GO

ALTER TABLE [dbo].[T_Waybill] ADD  CONSTRAINT [DF_T_Waybill_Waybill_AmountPaid]  DEFAULT ((0)) FOR [Waybill_AmountPaid]
GO

ALTER TABLE [dbo].[T_Waybill] ADD  CONSTRAINT [DF_T_Waybill_Waybill_CurrencyAllPrice]  DEFAULT ((0)) FOR [Waybill_CurrencyAllPrice]
GO

ALTER TABLE [dbo].[T_Waybill] ADD  CONSTRAINT [DF_T_Waybill_Waybill_CurrencyRetAllPrice]  DEFAULT ((0)) FOR [Waybill_CurrencyRetAllPrice]
GO

ALTER TABLE [dbo].[T_Waybill] ADD  CONSTRAINT [DF_T_Waybill_Waybill_CurrencyAllDiscount]  DEFAULT ((0)) FOR [Waybill_CurrencyAllDiscount]
GO

ALTER TABLE [dbo].[T_Waybill] ADD  CONSTRAINT [DF_T_Waybill_Waybill_CurrencyAmountPaid]  DEFAULT ((0)) FOR [Waybill_CurrencyAmountPaid]
GO

ALTER TABLE [dbo].[T_Waybill] ADD  CONSTRAINT [DF_T_Waybill_Waybill_RetQuantity]  DEFAULT ((0)) FOR [Waybill_RetQuantity]
GO

ALTER TABLE [dbo].[T_Waybill] ADD  CONSTRAINT [DF_T_Waybill_Waybill_Weight]  DEFAULT ((0)) FOR [Waybill_Weight]
GO

ALTER TABLE [dbo].[T_Waybill] ADD  CONSTRAINT [DF_T_Waybill_Waybill_ShowInDeliveryList]  DEFAULT ((0)) FOR [Waybill_ShowInDeliveryList]
GO

ALTER TABLE [dbo].[T_Waybill]  WITH CHECK ADD  CONSTRAINT [FK_T_Waybill_T_Address] FOREIGN KEY([Address_Guid])
REFERENCES [dbo].[T_Address] ([Address_Guid])
GO

ALTER TABLE [dbo].[T_Waybill] CHECK CONSTRAINT [FK_T_Waybill_T_Address]
GO

ALTER TABLE [dbo].[T_Waybill]  WITH CHECK ADD  CONSTRAINT [FK_T_Waybill_T_Company] FOREIGN KEY([Company_Guid])
REFERENCES [dbo].[T_Company] ([Company_Guid])
GO

ALTER TABLE [dbo].[T_Waybill] CHECK CONSTRAINT [FK_T_Waybill_T_Company]
GO

ALTER TABLE [dbo].[T_Waybill]  WITH CHECK ADD  CONSTRAINT [FK_T_Waybill_T_Currency] FOREIGN KEY([Currency_Guid])
REFERENCES [dbo].[T_Currency] ([Currency_Guid])
GO

ALTER TABLE [dbo].[T_Waybill] CHECK CONSTRAINT [FK_T_Waybill_T_Currency]
GO

ALTER TABLE [dbo].[T_Waybill]  WITH CHECK ADD  CONSTRAINT [FK_T_Waybill_T_Customer] FOREIGN KEY([Customer_Guid])
REFERENCES [dbo].[T_Customer] ([Customer_Guid])
GO

ALTER TABLE [dbo].[T_Waybill] CHECK CONSTRAINT [FK_T_Waybill_T_Customer]
GO

ALTER TABLE [dbo].[T_Waybill]  WITH CHECK ADD  CONSTRAINT [FK_T_Waybill_T_CustomerChild] FOREIGN KEY([CustomerChild_Guid])
REFERENCES [dbo].[T_CustomerChild] ([CustomerChild_Guid])
GO

ALTER TABLE [dbo].[T_Waybill] CHECK CONSTRAINT [FK_T_Waybill_T_CustomerChild]
GO

ALTER TABLE [dbo].[T_Waybill]  WITH CHECK ADD  CONSTRAINT [FK_T_Waybill_T_Depart] FOREIGN KEY([Depart_Guid])
REFERENCES [dbo].[T_Depart] ([Depart_Guid])
GO

ALTER TABLE [dbo].[T_Waybill] CHECK CONSTRAINT [FK_T_Waybill_T_Depart]
GO

ALTER TABLE [dbo].[T_Waybill]  WITH CHECK ADD  CONSTRAINT [FK_T_Waybill_T_PaymentType] FOREIGN KEY([PaymentType_Guid])
REFERENCES [dbo].[T_PaymentType] ([PaymentType_Guid])
GO

ALTER TABLE [dbo].[T_Waybill] CHECK CONSTRAINT [FK_T_Waybill_T_PaymentType]
GO

ALTER TABLE [dbo].[T_Waybill]  WITH CHECK ADD  CONSTRAINT [FK_T_Waybill_T_Rtt] FOREIGN KEY([Rtt_Guid])
REFERENCES [dbo].[T_Rtt] ([Rtt_Guid])
GO

ALTER TABLE [dbo].[T_Waybill] CHECK CONSTRAINT [FK_T_Waybill_T_Rtt]
GO

ALTER TABLE [dbo].[T_Waybill]  WITH CHECK ADD  CONSTRAINT [FK_T_Waybill_T_Stock] FOREIGN KEY([Stock_Guid])
REFERENCES [dbo].[T_Stock] ([Stock_Guid])
GO

ALTER TABLE [dbo].[T_Waybill] CHECK CONSTRAINT [FK_T_Waybill_T_Stock]
GO

ALTER TABLE [dbo].[T_Waybill]  WITH CHECK ADD  CONSTRAINT [FK_T_Waybill_T_Suppl] FOREIGN KEY([Suppl_Guid])
REFERENCES [dbo].[T_Suppl] ([Suppl_Guid])
GO

ALTER TABLE [dbo].[T_Waybill] CHECK CONSTRAINT [FK_T_Waybill_T_Suppl]
GO

ALTER TABLE [dbo].[T_Waybill]  WITH CHECK ADD  CONSTRAINT [FK_T_Waybill_T_WaybillShipMode] FOREIGN KEY([WaybillShipMode_Guid])
REFERENCES [dbo].[T_WaybillShipMode] ([WaybillShipMode_Guid])
GO

ALTER TABLE [dbo].[T_Waybill] CHECK CONSTRAINT [FK_T_Waybill_T_WaybillShipMode]
GO

ALTER TABLE [dbo].[T_Waybill]  WITH CHECK ADD  CONSTRAINT [FK_T_Waybill_T_WaybillState] FOREIGN KEY([WaybillState_Guid])
REFERENCES [dbo].[T_WaybillState] ([WaybillState_Guid])
GO

ALTER TABLE [dbo].[T_Waybill] CHECK CONSTRAINT [FK_T_Waybill_T_WaybillState]
GO


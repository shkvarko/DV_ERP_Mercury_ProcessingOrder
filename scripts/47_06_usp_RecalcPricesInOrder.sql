USE [ERP_Mercury]
GO
/****** Object:  StoredProcedure [dbo].[usp_RecalcPricesInOrder]    Script Date: 03/29/2012 15:08:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Пересчет цен в заказе
--
-- Входящие параметры:
--  @Order_Guid - уникальный идентификатор записи
--
--
-- Выходные параметры:
--  @ERROR_NUM - номер ошибки
--  @ERROR_MES - текст ошибки
--
-- Результат:
--    0 - Успешное завершение
--    <>0 - ошибка

CREATE PROCEDURE [dbo].[usp_RecalcPricesInOrder] 
  @Order_Guid D_GUID,
  
  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output

AS

BEGIN

	BEGIN TRY

    SET @ERROR_NUM = 0;
    SET @ERROR_MES = '';
    
		DECLARE @OrderState_Guid D_GUID;
		DECLARE @OrderState_Id D_ID;
		DECLARE @OrderState_Name D_NAME = '';
		DECLARE @Suppl_Id D_ID = NULL;
		DECLARE @Suppl_Num D_ID = NULL;
		
		SELECT @OrderState_Guid = dbo.T_Order.OrderState_Guid, @OrderState_Id = dbo.T_OrderState.OrderState_Id,  @OrderState_Name = dbo.T_OrderState.OrderState_Name, @Suppl_Id = dbo.T_Order.Suppl_Id, 
			@Suppl_Num = dbo.T_Order.Order_Num
		FROM dbo.T_Order, dbo.T_OrderState 
		WHERE dbo.T_Order.Order_Guid = @Order_Guid
			AND dbo.T_Order.OrderState_Guid = dbo.T_OrderState.OrderState_Guid;
		
		IF( ( @OrderState_Id IS NULL ) OR ( @OrderState_Id IN ( 1, 8, 13, 14 ) ) )
			BEGIN
				SET @ERROR_NUM = 1;
				SET @ERROR_MES = 'Заказ НЕЛЬЗЯ отправить на пересчет цен, так как состояние заказа "' + @OrderState_Name + '"';
				RETURN @ERROR_NUM;
			END

    BEGIN TRANSACTION UpdateData;

		-- приступаем к расчёту цен
		EXEC dbo.sp_ProcessPricesInPDASuppl @Suppl_Guid = @Order_Guid, @ERROR_NUM = @ERROR_NUM out, @ERROR_MES = @ERROR_MES out;
		
		IF( @ERROR_NUM = 0 )
			BEGIN
				-- в том случае, если заказа нет в InterBase, его нужно создать
				IF( ( @Suppl_Id IS NULL ) OR ( @Suppl_Id = 0 ) )
					EXEC dbo.usp_AddOrderToIB @Order_Guid = @Order_Guid, @Suppl_Id = @Suppl_Id out, @Suppl_Num = @Suppl_Num out, 
						@ERROR_NUM = @ERROR_NUM out, @ERROR_MES = @ERROR_MES out;
				-- ну а если уже есть, то пересчитать цену и в нём	
				ELSE
					EXEC dbo.usp_RecalcOrderToIB @Order_Guid = @Order_Guid, @ERROR_NUM = @ERROR_NUM out, @ERROR_MES = @ERROR_MES out;					
			END
		
		IF( @ERROR_NUM = 0 )	
			BEGIN
				UPDATE dbo.T_Order SET dbo.T_Order.OrderState_Guid = @OrderState_Guid
				WHERE dbo.T_Order.Order_Guid = @Order_Guid
				COMMIT TRANSACTION UpdateData;
			END 
		ELSE	
			ROLLBACK TRANSACTION UpdateData;

	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION UpdateData;
    SET @ERROR_NUM = ERROR_NUMBER();
    SET @ERROR_MES =  ERROR_MESSAGE();
		RETURN @ERROR_NUM;
	END CATCH;

	IF( @ERROR_NUM = 0 )
		SET @ERROR_MES = 'Успешное завершение операции.';
		
	RETURN @ERROR_NUM;
END

GO
GRANT EXECUTE ON [dbo].[usp_RecalcPricesInOrder] TO [public]
GO
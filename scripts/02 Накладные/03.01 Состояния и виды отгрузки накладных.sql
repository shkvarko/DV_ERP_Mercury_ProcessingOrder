USE [ERP_Mercury]
GO

CREATE NONCLUSTERED INDEX [INDX_T_SupplItem_Splitms_Id_2]
ON [dbo].[T_SupplItem] ([Splitms_Id])
INCLUDE ([SupplItem_Guid])
GO

INSERT INTO [dbo].[T_WaybillState]( WaybillState_Guid, WaybillState_Name, WaybillState_Description, 
	WaybillState_IsActive, WaybillState_IsDefault, WaybillState_Id, Record_Updated, Record_UserUdpated )
VALUES( NEWID(), 'товар в резерве', 'накладная не отгружена', 1, 1, 0, GetDate(), 'Admin' );

INSERT INTO [dbo].[T_WaybillState]( WaybillState_Guid, WaybillState_Name, WaybillState_Description, 
	WaybillState_IsActive, WaybillState_IsDefault, WaybillState_Id, Record_Updated, Record_UserUdpated )
VALUES( NEWID(), 'отгружена', 'товар отгружен клиенту', 1, 0, 1, GetDate(), 'Admin' );

INSERT INTO [dbo].[T_WaybillState]( WaybillState_Guid, WaybillState_Name, WaybillState_Description, 
	WaybillState_IsActive, WaybillState_IsDefault, WaybillState_Id, Record_Updated, Record_UserUdpated )
VALUES( NEWID(), 'аннулирована', 'накладная расформирована', 1, 0, -1, GetDate(), 'Admin' );


INSERT INTO [dbo].[T_WaybillShipMode]( WaybillShipMode_Guid, WaybillShipMode_Name, WaybillShipMode_Description, 
	WaybillShipMode_IsActive, WaybillShipMode_IsDefault, WaybillShipMode_Id, Record_Updated, Record_UserUdpated )
VALUES( NEWID(), 'реализация', 'отгрузка товара клиенту', 1, 0, 0, GetDate(), 'Admin');

INSERT INTO [dbo].[T_WaybillShipMode]( WaybillShipMode_Guid, WaybillShipMode_Name, WaybillShipMode_Description, 
	WaybillShipMode_IsActive, WaybillShipMode_IsDefault, WaybillShipMode_Id, Record_Updated, Record_UserUdpated )
VALUES( NEWID(), 'представительские нужды внешние', '', 1, 0, 9911, GetDate(), 'Admin');

INSERT INTO [dbo].[T_WaybillShipMode]( WaybillShipMode_Guid, WaybillShipMode_Name, WaybillShipMode_Description, 
	WaybillShipMode_IsActive, WaybillShipMode_IsDefault, WaybillShipMode_Id, Record_Updated, Record_UserUdpated )
VALUES( NEWID(), 'представительские нужды внутренние', '', 1, 0, 9912, GetDate(), 'Admin');

INSERT INTO [dbo].[T_WaybillShipMode]( WaybillShipMode_Guid, WaybillShipMode_Name, WaybillShipMode_Description, 
	WaybillShipMode_IsActive, WaybillShipMode_IsDefault, WaybillShipMode_Id, Record_Updated, Record_UserUdpated )
VALUES( NEWID(), 'для сертификации', '', 1, 0, 9921, GetDate(), 'Admin');

INSERT INTO [dbo].[T_WaybillShipMode]( WaybillShipMode_Guid, WaybillShipMode_Name, WaybillShipMode_Description, 
	WaybillShipMode_IsActive, WaybillShipMode_IsDefault, WaybillShipMode_Id, Record_Updated, Record_UserUdpated )
VALUES( NEWID(), 'образцы', '', 1, 0, 9711, GetDate(), 'Admin');

INSERT INTO [dbo].[T_WaybillShipMode]( WaybillShipMode_Guid, WaybillShipMode_Name, WaybillShipMode_Description, 
	WaybillShipMode_IsActive, WaybillShipMode_IsDefault, WaybillShipMode_Id, Record_Updated, Record_UserUdpated )
VALUES( NEWID(), 'тестеры', '', 1, 0, 9721, GetDate(), 'Admin');

INSERT INTO [dbo].[T_WaybillShipMode]( WaybillShipMode_Guid, WaybillShipMode_Name, WaybillShipMode_Description, 
	WaybillShipMode_IsActive, WaybillShipMode_IsDefault, WaybillShipMode_Id, Record_Updated, Record_UserUdpated )
VALUES( NEWID(), 'призы для анимации', '', 1, 0, 9731, GetDate(), 'Admin');

INSERT INTO [dbo].[T_WaybillShipMode]( WaybillShipMode_Guid, WaybillShipMode_Name, WaybillShipMode_Description, 
	WaybillShipMode_IsActive, WaybillShipMode_IsDefault, WaybillShipMode_Id, Record_Updated, Record_UserUdpated )
VALUES( NEWID(), 'реклама', '', 1, 0, 9741, GetDate(), 'Admin');

INSERT INTO [dbo].[T_WaybillShipMode]( WaybillShipMode_Guid, WaybillShipMode_Name, WaybillShipMode_Description, 
	WaybillShipMode_IsActive, WaybillShipMode_IsDefault, WaybillShipMode_Id, Record_Updated, Record_UserUdpated )
VALUES( NEWID(), 'отгрузка по нулевой цене', '', 1, 0, 9751, GetDate(), 'Admin');

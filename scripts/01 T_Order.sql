USE [ERP_Mercury]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_OrderState](
	[OrderState_Guid] [dbo].[D_GUID] NOT NULL,
	[OrderState_Id] [dbo].[D_ID] NOT NULL,
	[OrderState_Name] [dbo].[D_NAME] NOT NULL,
	[OrderState_Description] [dbo].[D_DESCRIPTION] NULL,
	[OrderState_IsActive] [dbo].[D_ISACTIVE] NOT NULL,
	[OrderState_ShowInDeliveryList] [dbo].[D_YESNO] NULL,
	[OrderState_ShowInPDASupplList] [dbo].[D_YESNO] NULL,
	[Record_Updated] [dbo].[D_DATETIME] NULL,
	[Record_UserUdpated] [dbo].[D_NAMESHORT] NULL,
 CONSTRAINT [PK_T_OrderState] PRIMARY KEY CLUSTERED 
(
	[OrderState_Guid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE UNIQUE NONCLUSTERED INDEX [INDX_T_OrderState_OrderState_Id] ON [dbo].[T_OrderState] 
(
	[OrderState_Id] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [INDEX]
GO

CREATE UNIQUE NONCLUSTERED INDEX [INDX_T_OrderState_OrderState_Name] ON [dbo].[T_OrderState] 
(
	[OrderState_Name] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [INDEX]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_OrderState_Archive](
	[OrderState_Guid] [dbo].[D_GUID] NOT NULL,
	[OrderState_Id] [dbo].[D_ID] NOT NULL,
	[OrderState_Name] [dbo].[D_NAME] NOT NULL,
	[OrderState_Description] [dbo].[D_DESCRIPTION] NULL,
	[OrderState_IsActive] [dbo].[D_ISACTIVE] NOT NULL,
	[OrderState_ShowInDeliveryList] [dbo].[D_YESNO] NULL,
	[OrderState_ShowInPDASupplList] [dbo].[D_YESNO] NULL,
	[Record_Updated] [dbo].[D_DATETIME] NOT NULL,
	[Record_UserUdpated] [dbo].[D_NAMESHORT] NOT NULL,
	[Action_TypeId] [dbo].[D_ID] NOT NULL
) ON [PRIMARY]

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Триггер добавляет записи в случае их удаления в таблицу удаленных записей
-- =============================================
CREATE TRIGGER [dbo].[TG_OrderStateAfterDelete]
   ON  [dbo].[T_OrderState]
   AFTER DELETE
AS 
BEGIN
	SET NOCOUNT ON;

		INSERT INTO dbo.T_OrderState_Archive( OrderState_Guid, OrderState_Id, OrderState_Name, OrderState_Description, OrderState_IsActive, OrderState_ShowInDeliveryList, OrderState_ShowInPDASupplList,
			Record_Updated, Record_UserUdpated, Action_TypeId )
		SELECT OrderState_Guid, OrderState_Id, OrderState_Name, OrderState_Description, OrderState_IsActive, OrderState_ShowInDeliveryList, OrderState_ShowInPDASupplList,
			sysutcdatetime(), ( Host_Name() + ': ' + SUSER_SNAME() ), 2
		FROM deleted;

END

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Триггер обновляет время редактирования/вставки записи
-- =============================================
CREATE TRIGGER [dbo].[TG_OrderStateAfterUpdate]
   ON  [dbo].[T_OrderState]
   AFTER INSERT, UPDATE
AS 
BEGIN
	SET NOCOUNT ON;
	
		INSERT INTO dbo.T_OrderState_Archive( OrderState_Guid, OrderState_Id, OrderState_Name, OrderState_Description, OrderState_IsActive, OrderState_ShowInDeliveryList, OrderState_ShowInPDASupplList,
			Record_Updated, Record_UserUdpated, Action_TypeId )
		SELECT OrderState_Guid, OrderState_Id, OrderState_Name, OrderState_Description, OrderState_IsActive, OrderState_ShowInDeliveryList, OrderState_ShowInPDASupplList,
			sysutcdatetime(), ( Host_Name() + ': ' + SUSER_SNAME() ), 0
		FROM inserted;

		UPDATE dbo.[T_OrderState] SET Record_Updated = sysutcdatetime(), Record_UserUdpated = ( Host_Name() + ': ' + SUSER_SNAME() )
		WHERE OrderState_Guid IN ( SELECT OrderState_Guid FROM inserted );
	
END

GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_Order](
	[Order_Guid] [dbo].[D_GUID] NOT NULL,
	[Order_Num] [dbo].[D_ID] NOT NULL,
	[Order_SubNum] [dbo].[D_ID] NOT NULL,
	[Order_BeginDate] [dbo].[D_DATE] NOT NULL,
	[OrderState_Guid] [dbo].[D_GUID] NOT NULL,
	[Order_MoneyBonus] [dbo].[D_YESNO] NOT NULL,
	[Depart_Guid] [dbo].[D_GUID] NOT NULL,
	[Salesman_Guid] [dbo].[D_GUID] NOT NULL,
	[Customer_Guid] [dbo].[D_GUID] NOT NULL,
	[CustomerChild_Guid] [dbo].[D_GUID] NULL,
	[OrderType_Guid] [dbo].[D_GUID] NOT NULL,
	[PaymentType_Guid] [dbo].[D_GUID] NOT NULL,
	[Order_Description] [dbo].[D_DESCRIPTION] NULL,
	[Order_DeliveryDate] [dbo].[D_DATE] NOT NULL,
	[Rtt_Guid] [dbo].[D_GUID] NOT NULL,
	[Address_Guid] [dbo].[D_GUID] NOT NULL,
	[Stock_Guid] [dbo].[D_GUID] NULL,
	[Parts_Guid] [dbo].[D_GUID] NULL,
	[Record_Updated] [dbo].[D_DATETIME] NULL,
	[Record_UserUdpated] [dbo].[D_NAMESHORT] NULL,
 CONSTRAINT [PK_T_Order] PRIMARY KEY CLUSTERED 
(
	[Order_Guid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_Order_Archive](
	[Order_Guid] [dbo].[D_GUID] NOT NULL,
	[Order_Num] [dbo].[D_ID] NOT NULL,
	[Order_SubNum] [dbo].[D_ID] NOT NULL,
	[Order_BeginDate] [dbo].[D_DATE] NOT NULL,
	[OrderState_Guid] [dbo].[D_GUID] NOT NULL,
	[Order_MoneyBonus] [dbo].[D_YESNO] NOT NULL,
	[Depart_Guid] [dbo].[D_GUID] NOT NULL,
	[Salesman_Guid] [dbo].[D_GUID] NOT NULL,
	[Customer_Guid] [dbo].[D_GUID] NOT NULL,
	[CustomerChild_Guid] [dbo].[D_GUID] NULL,
	[OrderType_Guid] [dbo].[D_GUID] NOT NULL,
	[PaymentType_Guid] [dbo].[D_GUID] NOT NULL,
	[Order_Description] [dbo].[D_DESCRIPTION] NULL,
	[Order_DeliveryDate] [dbo].[D_DATE] NOT NULL,
	[Rtt_Guid] [dbo].[D_GUID] NOT NULL,
	[Address_Guid] [dbo].[D_GUID] NOT NULL,
	[Stock_Guid] [dbo].[D_GUID] NULL,
	[Parts_Guid] [dbo].[D_GUID] NULL,
	[Record_Updated] [dbo].[D_DATETIME] NULL,
	[Record_UserUdpated] [dbo].[D_NAMESHORT] NULL,
	[Action_TypeId] [dbo].[D_ID] NOT NULL
	) ON [PRIMARY]

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Триггер добавляет записи в случае их удаления в таблицу удаленных записей
-- =============================================
CREATE TRIGGER [dbo].[TG_OrderAfterDelete]
   ON  [dbo].[T_Order]
   AFTER DELETE
AS 
BEGIN
	SET NOCOUNT ON;

		INSERT INTO dbo.T_Order_Archive( Order_Guid, Order_Num, Order_SubNum, Order_BeginDate, 
			OrderState_Guid, Order_MoneyBonus, Depart_Guid, Salesman_Guid, Customer_Guid, CustomerChild_Guid, OrderType_Guid, 
			PaymentType_Guid, Order_Description, Order_DeliveryDate, Rtt_Guid, Address_Guid, Stock_Guid, Parts_Guid, 
			Record_Updated, Record_UserUdpated, Action_TypeId )
		SELECT Order_Guid, Order_Num, Order_SubNum, Order_BeginDate, 
			OrderState_Guid, Order_MoneyBonus, Depart_Guid, Salesman_Guid, Customer_Guid, CustomerChild_Guid, OrderType_Guid, 
			PaymentType_Guid, Order_Description, Order_DeliveryDate, Rtt_Guid, Address_Guid, Stock_Guid, Parts_Guid,
			sysutcdatetime(), ( Host_Name() + ': ' + SUSER_SNAME() ), 2
		FROM deleted;

END

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Триггер обновляет время редактирования/вставки записи
-- =============================================
CREATE TRIGGER [dbo].[TG_OrderAfterUpdate]
   ON  [dbo].[T_Order]
   AFTER INSERT, UPDATE
AS 
BEGIN
	SET NOCOUNT ON;
	
		INSERT INTO dbo.T_Order_Archive( Order_Guid, Order_Num, Order_SubNum, Order_BeginDate, 
			OrderState_Guid, Order_MoneyBonus, Depart_Guid, Salesman_Guid, Customer_Guid, CustomerChild_Guid, OrderType_Guid, 
			PaymentType_Guid, Order_Description, Order_DeliveryDate, Rtt_Guid, Address_Guid, Stock_Guid, Parts_Guid, 
			Record_Updated, Record_UserUdpated, Action_TypeId )
		SELECT Order_Guid, Order_Num, Order_SubNum, Order_BeginDate, 
			OrderState_Guid, Order_MoneyBonus, Depart_Guid, Salesman_Guid, Customer_Guid, CustomerChild_Guid, OrderType_Guid, 
			PaymentType_Guid, Order_Description, Order_DeliveryDate, Rtt_Guid, Address_Guid, Stock_Guid, Parts_Guid,
			sysutcdatetime(), ( Host_Name() + ': ' + SUSER_SNAME() ), 0
		FROM inserted;

		UPDATE dbo.[T_Order] SET Record_Updated = sysutcdatetime(), Record_UserUdpated = ( Host_Name() + ': ' + SUSER_SNAME() )
		WHERE Order_Guid IN ( SELECT Order_Guid FROM inserted );
	
END

GO

CREATE NONCLUSTERED INDEX [INDX_T_Order_Order_BeginDate] ON [dbo].[T_Order] 
(
	[Order_BeginDate] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [INDEX]
GO

CREATE NONCLUSTERED INDEX [INDX_T_Order_Order_Num] ON [dbo].[T_Order] 
(
	[Order_Num] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [INDEX]
GO

/****** Object:  Index [INDX_T_Order_Order_DeliveryDate]    Script Date: 02/16/2012 21:13:10 ******/
CREATE NONCLUSTERED INDEX [INDX_T_Order_Order_DeliveryDate] ON [dbo].[T_Order] 
(
	[Order_DeliveryDate] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [INDEX]
GO

ALTER TABLE [dbo].[T_Order]  WITH CHECK ADD  CONSTRAINT [FK_T_Order_T_Address] FOREIGN KEY([Address_Guid])
REFERENCES [dbo].[T_Address] ([Address_Guid])
GO

ALTER TABLE [dbo].[T_Order] CHECK CONSTRAINT [FK_T_Order_T_Address]
GO

ALTER TABLE [dbo].[T_Order]  WITH CHECK ADD  CONSTRAINT [FK_T_Order_T_Customer] FOREIGN KEY([Customer_Guid])
REFERENCES [dbo].[T_Customer] ([Customer_Guid])
GO

ALTER TABLE [dbo].[T_Order] CHECK CONSTRAINT [FK_T_Order_T_Customer]
GO

ALTER TABLE [dbo].[T_Order]  WITH CHECK ADD  CONSTRAINT [FK_T_Order_T_CustomerChild] FOREIGN KEY([CustomerChild_Guid])
REFERENCES [dbo].[T_CustomerChild] ([CustomerChild_Guid])
GO

ALTER TABLE [dbo].[T_Order] CHECK CONSTRAINT [FK_T_Order_T_CustomerChild]
GO


ALTER TABLE [dbo].[T_Order]  WITH CHECK ADD  CONSTRAINT [FK_T_Order_T_Depart] FOREIGN KEY([Depart_Guid])
REFERENCES [dbo].[T_Depart] ([Depart_Guid])
GO

ALTER TABLE [dbo].[T_Order] CHECK CONSTRAINT [FK_T_Order_T_Depart]
GO


ALTER TABLE [dbo].[T_Order]  WITH CHECK ADD  CONSTRAINT [FK_T_Order_T_OrderState] FOREIGN KEY([OrderState_Guid])
REFERENCES [dbo].[T_OrderState] ([OrderState_Guid])
GO

ALTER TABLE [dbo].[T_Order] CHECK CONSTRAINT [FK_T_Order_T_OrderState]
GO


ALTER TABLE [dbo].[T_Order]  WITH CHECK ADD  CONSTRAINT [FK_T_Order_T_OrderType] FOREIGN KEY([OrderType_Guid])
REFERENCES [dbo].[T_OrderType] ([OrderType_Guid])
GO

ALTER TABLE [dbo].[T_Order] CHECK CONSTRAINT [FK_T_Order_T_OrderType]
GO


ALTER TABLE [dbo].[T_Order]  WITH CHECK ADD  CONSTRAINT [FK_T_Order_T_PaymentType] FOREIGN KEY([PaymentType_Guid])
REFERENCES [dbo].[T_PaymentType] ([PaymentType_Guid])
GO

ALTER TABLE [dbo].[T_Order] CHECK CONSTRAINT [FK_T_Order_T_PaymentType]
GO

ALTER TABLE [dbo].[T_Order]  WITH CHECK ADD  CONSTRAINT [FK_T_Order_T_Parts] FOREIGN KEY([Parts_Guid])
REFERENCES [dbo].[T_Parts] ([Parts_Guid])
GO

ALTER TABLE [dbo].[T_Order] CHECK CONSTRAINT [FK_T_Order_T_Parts]
GO

ALTER TABLE [dbo].[T_Order]  WITH CHECK ADD  CONSTRAINT [FK_T_Order_T_Rtt] FOREIGN KEY([Rtt_Guid])
REFERENCES [dbo].[T_Rtt] ([Rtt_Guid])
GO

ALTER TABLE [dbo].[T_Order] CHECK CONSTRAINT [FK_T_Order_T_Rtt]
GO


ALTER TABLE [dbo].[T_Order]  WITH CHECK ADD  CONSTRAINT [FK_T_Order_T_Salesman] FOREIGN KEY([Salesman_Guid])
REFERENCES [dbo].[T_Salesman] ([Salesman_Guid])
GO

ALTER TABLE [dbo].[T_Order] CHECK CONSTRAINT [FK_T_Order_T_Salesman]
GO

ALTER TABLE [dbo].[T_Order]  WITH CHECK ADD  CONSTRAINT [FK_T_Order_T_Stock] FOREIGN KEY([Stock_Guid])
REFERENCES [dbo].[T_Stock] ([Stock_Guid])
GO

ALTER TABLE [dbo].[T_Order] CHECK CONSTRAINT [FK_T_Order_T_Stock]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_OrderItms](
	[OrderItms_Guid] [dbo].[D_GUID] NOT NULL,
	[Order_Guid] [dbo].[D_GUID] NOT NULL,
	[Parts_Guid] [dbo].[D_GUID] NOT NULL,
	[Measure_Guid] [dbo].[D_GUID] NOT NULL,
	[OrderItms_Quantity] [dbo].[D_QUANTITY] NOT NULL,
	[Record_Updated] [dbo].[D_DATETIME] NULL,
	[Record_UserUdpated] [dbo].[D_NAMESHORT] NULL,
 CONSTRAINT [PK_T_OrderItms] PRIMARY KEY CLUSTERED 
(
	[OrderItms_Guid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[T_OrderItms_Archive](
	[OrderItms_Guid] [dbo].[D_GUID] NOT NULL,
	[Order_Guid] [dbo].[D_GUID] NOT NULL,
	[Parts_Guid] [dbo].[D_GUID] NOT NULL,
	[Measure_Guid] [dbo].[D_GUID] NOT NULL,
	[OrderItms_Quantity] [dbo].[D_QUANTITY] NOT NULL,
	[Record_Updated] [dbo].[D_DATETIME] NULL,
	[Record_UserUdpated] [dbo].[D_NAMESHORT] NULL,
	[Action_TypeId] [dbo].[D_ID] NOT NULL
	) ON [PRIMARY]

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Триггер добавляет записи в случае их удаления в таблицу удаленных записей
-- =============================================
CREATE TRIGGER [dbo].[TG_OrderItmsAfterDelete]
   ON  [dbo].[T_OrderItms]
   AFTER DELETE
AS 
BEGIN
	SET NOCOUNT ON;

		INSERT INTO dbo.T_OrderItms_Archive( OrderItms_Guid, Order_Guid, Parts_Guid, Measure_Guid, OrderItms_Quantity,  
			Record_Updated, Record_UserUdpated, Action_TypeId )
		SELECT OrderItms_Guid, Order_Guid, Parts_Guid, Measure_Guid, OrderItms_Quantity,
			sysutcdatetime(), ( Host_Name() + ': ' + SUSER_SNAME() ), 2
		FROM deleted;

END

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Триггер обновляет время редактирования/вставки записи
-- =============================================
CREATE TRIGGER [dbo].[TG_OrderItmsAfterUpdate]
   ON  [dbo].[T_OrderItms]
   AFTER INSERT, UPDATE
AS 
BEGIN
	SET NOCOUNT ON;
	
		INSERT INTO dbo.T_OrderItms_Archive( OrderItms_Guid, Order_Guid, Parts_Guid, Measure_Guid, OrderItms_Quantity,  
			Record_Updated, Record_UserUdpated, Action_TypeId )
		SELECT OrderItms_Guid, Order_Guid, Parts_Guid, Measure_Guid, OrderItms_Quantity,
			sysutcdatetime(), ( Host_Name() + ': ' + SUSER_SNAME() ), 0
		FROM inserted;

		UPDATE dbo.[T_OrderItms] SET Record_Updated = sysutcdatetime(), Record_UserUdpated = ( Host_Name() + ': ' + SUSER_SNAME() )
		WHERE OrderItms_Guid IN ( SELECT OrderItms_Guid FROM inserted );
	
END

GO


ALTER TABLE [dbo].[T_OrderItms]  WITH CHECK ADD  CONSTRAINT [FK_T_OrderItms_T_Measure] FOREIGN KEY([Measure_Guid])
REFERENCES [dbo].[T_Measure] ([Measure_Guid])
GO

ALTER TABLE [dbo].[T_OrderItms] CHECK CONSTRAINT [FK_T_OrderItms_T_Measure]
GO

ALTER TABLE [dbo].[T_OrderItms]  WITH CHECK ADD  CONSTRAINT [FK_T_OrderItms_T_Order] FOREIGN KEY([Order_Guid])
REFERENCES [dbo].[T_Order] ([Order_Guid])
GO

ALTER TABLE [dbo].[T_OrderItms] CHECK CONSTRAINT [FK_T_OrderItms_T_Order]
GO

ALTER TABLE [dbo].[T_OrderItms]  WITH CHECK ADD  CONSTRAINT [FK_T_OrderItms_T_Parts] FOREIGN KEY([Parts_Guid])
REFERENCES [dbo].[T_Parts] ([Parts_Guid])
GO

ALTER TABLE [dbo].[T_OrderItms] CHECK CONSTRAINT [FK_T_OrderItms_T_Parts]
GO

/****** Object:  View [dbo].[OrderItmsView]    Script Date: 02/16/2012 21:51:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[OrderItmsView]
AS
SELECT     dbo.T_OrderItms.Order_Guid, dbo.T_Measure.Measure_Id, dbo.T_Measure.Measure_Name, dbo.T_Measure.Measure_ShortName, dbo.T_OrderItms.Parts_Guid, 
                      dbo.T_OrderItms.Measure_Guid, dbo.T_OrderItms.OrderItms_Quantity, dbo.T_OrderItms.OrderItms_Guid, dbo.PartsView.Parts_Id, dbo.PartsView.Currency_Guid, 
                      dbo.PartsView.Owner_Guid, dbo.PartsView.Parttype_Guid, dbo.PartsView.Barcode, dbo.PartsView.Partsubtype_Guid, dbo.PartsView.Country_Guid, 
                      dbo.PartsView.Currency_Abbr, dbo.PartsView.Currency_Code, dbo.PartsView.Owner_Id, dbo.PartsView.Owner_Name, dbo.PartsView.Owner_ShortName, 
                      dbo.PartsView.Owner_Description, dbo.PartsView.Owner_IsActive, dbo.PartsView.Vtm_Guid, dbo.PartsView.Vtm_Id, dbo.PartsView.Vtm_Name, 
                      dbo.PartsView.Vtm_ShortName, dbo.PartsView.Vtm_IsActive, dbo.PartsView.Parttype_Id, dbo.PartsView.Parttype_NDSRate, dbo.PartsView.Parttype_DemandsName, 
                      dbo.PartsView.Partsubtype_Id, dbo.PartsView.Partsubtype_Name, dbo.PartsView.Partsubtype_IsActive, dbo.PartsView.PartLine_Guid, dbo.PartsView.Partline_Id, 
                      dbo.PartsView.Partline_Name, dbo.PartsView.Partline_IsActive, dbo.PartsView.Parttype_Name, dbo.PartsView.Country_Name, dbo.PartsView.Country_Code, 
                      dbo.PartsView.Parttype_IsActive, dbo.PartsView.PartsCategory_Guid, dbo.PartsView.PartsCategory_Id, dbo.PartsView.PartsCategory_Name, 
                      dbo.PartsView.Parts_OriginalName, dbo.PartsView.Parts_Name, dbo.PartsView.Parts_ShortName, dbo.PartsView.Parts_BoxQuantity, 
                      dbo.PartsView.Parts_PackQuantity, dbo.PartsView.Parts_Weight, dbo.PartsView.Parts_IsActive, dbo.PartsView.Parts_Article
FROM         dbo.T_OrderItms INNER JOIN
                      dbo.T_Measure ON dbo.T_OrderItms.Measure_Guid = dbo.T_Measure.Measure_Guid INNER JOIN
                      dbo.PartsView ON dbo.T_OrderItms.Parts_Guid = dbo.PartsView.Parts_Guid
GO
GRANT SELECT ON [dbo].[OrderItmsView] TO [public]
GO

/****** Object:  UserDefinedFunction [dbo].[GetAddressStringForRtt]    Script Date: 02/16/2012 21:58:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- Creation date:	
-- Author:			

CREATE FUNCTION [dbo].[GetAddressString] ( @Adress_Guid D_GUID )
RETURNS D_DESCRIPTION
WITH EXECUTE AS caller
AS
BEGIN
  
DECLARE @AddressString D_DESCRIPTION;
SET @AddressString = '';


				DECLARE @Country_Name D_NAME;
				DECLARE @Region_Name D_NAME;
				DECLARE @LocalityPrefix_NameShort D_NAMETINY;
				DECLARE @City_Name D_NAME;
				DECLARE @Address_Postindex D_NAME;
				DECLARE @AddressPrefix_NameShort D_NAMETINY;
				DECLARE @Address_Name D_NAME;
				DECLARE @Address_NameFull D_NAME;
				DECLARE @Building_NameShort D_NAMETINY;
				DECLARE @Address_BuildCode D_BUILDING;
				DECLARE @SubBuilding_NameShort D_NAMETINY;
				DECLARE @Address_SubBuildingCode D_BUILDING;
				DECLARE @Address_FlatCode D_BUILDING;
				DECLARE @Flat_Guid D_GUID;
				DECLARE @SubBuilding_Guid D_GUID;
				
				DECLARE @Address_FlatCodeFull D_BUILDING;
				DECLARE @Address_BuildCodeFull D_BUILDING;
				DECLARE @Address_SubBuildCodeFull D_NAMETINY;
				
				SET @Building_NameShort = '';
				SET @SubBuilding_NameShort = '';
				SET @Address_BuildCode = '';
				SET @SubBuilding_NameShort = '';
				SET @Address_SubBuildingCode = '';
				SET @Address_FlatCodeFull = '';
				SET @Address_NameFull = '';
				SET @Address_BuildCodeFull = '';
				SET @Address_SubBuildCodeFull = '';

				SELECT 
					@LocalityPrefix_NameShort = LocalityPrefix.LocalityPrefix_NameShort, @City_Name = City.City_Name, 
					@Address_Postindex = Addres.Address_Postindex, 
					@AddressPrefix_NameShort = dbo.GetAddressPrefixNameShort( Addres.AddressPrefix_Guid ), 
					@Address_Name = Addres.Address_Name, 
					@Building_NameShort = dbo.GetBuildingNameShort( Addres.Building_Guid ), 
					@Address_BuildCode = Addres.Address_BuildCode, 
					@Address_SubBuildingCode = Addres.Address_SubBuildingCode,
					@Flat_Guid = Addres.Flat_Guid, @Address_FlatCode = Addres.Address_FlatCode , 
					@SubBuilding_Guid = Addres.SubBuilding_Guid
				FROM dbo.T_Address as Addres, 
					dbo.T_City as City, dbo.T_LocalityPrefix as LocalityPrefix--, dbo.T_Building as Building --, dbo.T_Region as Region, dbo.T_Country as Country
				WHERE 
							Addres.Address_Guid = @Adress_Guid
					--AND Addres.AddressPrefix_Guid = AddressPrefix.AddressPrefix_Guid
					AND Addres.City_Guid = City.City_Guid
					AND City.LocalityPrefix_Guid = LocalityPrefix.LocalityPrefix_Guid
					--AND Addres.Building_Guid = Building.Building_Guid

				IF( @Building_NameShort IS NULL ) SET @Building_NameShort = '';
				IF( @Address_BuildCode IS NULL ) SET @Address_BuildCode = '';

				IF( @Address_SubBuildingCode IS NULL ) SET @Address_SubBuildingCode = '';
				IF( @Address_FlatCode IS NULL ) SET @Address_FlatCode = '';
				
				-- помещение
				IF( @Flat_Guid IS NOT NULL )
					BEGIN
						SELECT @Address_FlatCodeFull = Flat_NameShort FROM dbo.T_Flat WHERE Flat_Guid = @Flat_Guid;
						SET @Address_FlatCodeFull = ' ' + @Address_FlatCodeFull + @Address_FlatCode; 
					END
				ELSE
					BEGIN
						SET @Address_FlatCodeFull = '';
					END	
					
				-- корпус
				IF( ( @SubBuilding_Guid IS NOT NULL ) AND ( @Address_SubBuildingCode IS NOT NULL ) )
					BEGIN
						SELECT @SubBuilding_NameShort = SubBuilding_NameShort FROM dbo.T_SubBuilding WHERE SubBuilding_Guid = @SubBuilding_Guid;
						SET @Address_SubBuildCodeFull = ' ' + @SubBuilding_NameShort + ' ' + @Address_SubBuildingCode; 
					END
				ELSE
					BEGIN
						SET @Address_SubBuildCodeFull = '';
					END	

				-- строение	
				IF( ( @Building_NameShort IS NOT NULL ) AND( @Building_NameShort <> '' ) AND ( @Address_BuildCode IS NOT NULL ) AND( @Address_BuildCode <> '' ) )	
					SET @Address_BuildCodeFull = ', ' + @Building_NameShort + ' ' + @Address_BuildCode;
				ELSE 	SET @Address_BuildCodeFull = '';

				-- улица
				IF( ( @Address_Name IS NULL ) OR ( @AddressPrefix_NameShort = '' ) OR ( Len( dbo.TrimSpace( @Address_Name ) ) < 1 ) )	
					BEGIN
						SET @Address_NameFull = '';
					END
				ELSE	
					BEGIN
						SET @Address_NameFull = ' ' + @AddressPrefix_NameShort + ' ' + Rtrim( @Address_Name );
					END

				IF( @Address_Postindex = '000000' ) SET @Address_Postindex = '';

				SET @AddressString = @Address_Postindex + ' ' + @LocalityPrefix_NameShort + ' ' + @City_Name + ' ' + @Address_NameFull;
					
				IF( ( @Address_NameFull <> '' ) AND ( @Address_BuildCodeFull <> '' ) )
					SET @AddressString = @AddressString + @Address_BuildCodeFull;
					
				IF( ( @Address_NameFull <> '' ) AND ( @Address_BuildCodeFull <> '' ) AND ( @Address_SubBuildCodeFull <> '' )  )
					SET @AddressString = @AddressString + @Address_SubBuildCodeFull;

				IF( ( @Address_NameFull <> '' ) AND ( @Address_BuildCodeFull <> '' ) AND ( @Address_SubBuildCodeFull <> '' ) AND ( @Address_FlatCodeFull <> '' ) )
					SET @AddressString = @AddressString + @Address_FlatCodeFull;
					
					

IF( @AddressString IS NULL ) SET @AddressString = '';

RETURN @AddressString;
end

GO
GRANT EXECUTE ON [dbo].[GetAddressString] TO [public]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[OrderView]
AS
SELECT     dbo.T_Order.Order_Guid, dbo.T_Order.Order_Num, dbo.T_Order.Order_SubNum, dbo.T_Order.Order_BeginDate, dbo.T_Order.OrderState_Guid, 
                      dbo.T_Order.Order_MoneyBonus, dbo.T_Order.Depart_Guid, dbo.T_Order.Salesman_Guid, dbo.T_Order.Customer_Guid, dbo.T_Order.CustomerChild_Guid, 
                      dbo.T_Order.OrderType_Guid, dbo.T_Order.PaymentType_Guid, dbo.T_Order.Order_Description, dbo.T_Order.Order_DeliveryDate, dbo.T_Order.Rtt_Guid, 
                      dbo.T_Order.Address_Guid, dbo.T_Order.Stock_Guid, dbo.T_Order.Parts_Guid, dbo.T_Customer.Customer_Id, dbo.T_Customer.Customer_Name, 
                      dbo.T_Customer.Customer_Code, dbo.T_Customer.Customer_UNP, dbo.T_Customer.Customer_OKPO, dbo.T_Customer.Customer_OKULP, 
                      dbo.T_Customer.CustomerActiveType_Guid, dbo.T_Customer.CustomerStateType_Guid, dbo.T_Depart.DepartTeam_Guid, dbo.T_Depart.Depart_Code, 
                      dbo.T_Depart.Depart_IsActive, dbo.T_Salesman.Salesman_Id, dbo.T_Salesman.Salesman_IsActive, dbo.T_Salesman.User_Guid, dbo.T_User.User_FirstName, 
                      dbo.T_User.User_MiddleName, dbo.T_User.User_LastName, dbo.T_User.User_LoginName, dbo.T_User.User_IsActive, dbo.T_OrderState.OrderState_Id, 
                      dbo.T_OrderState.OrderState_Name, dbo.T_OrderType.OrderType_Name, dbo.T_PaymentType.PaymentType_Name, dbo.T_Rtt.Rtt_Code, dbo.T_Rtt.Rtt_Name, 
                      dbo.T_Rtt.RttType_Guid, dbo.T_Rtt.RttActiveType_Guid, dbo.T_Rtt.RttSpecCode_Guid, dbo.T_Rtt.Segmentation_Guid, dbo.T_Rtt.LicenceType_Guid, 
                      dbo.GetAddressString(dbo.T_Order.Address_Guid) AS AddressString
FROM         dbo.T_Order INNER JOIN
                      dbo.T_Customer ON dbo.T_Order.Customer_Guid = dbo.T_Customer.Customer_Guid INNER JOIN
                      dbo.T_Depart ON dbo.T_Order.Depart_Guid = dbo.T_Depart.Depart_Guid INNER JOIN
                      dbo.T_Salesman ON dbo.T_Order.Salesman_Guid = dbo.T_Salesman.Salesman_Guid INNER JOIN
                      dbo.T_PaymentType ON dbo.T_Order.PaymentType_Guid = dbo.T_PaymentType.PaymentType_Guid INNER JOIN
                      dbo.T_OrderType ON dbo.T_Order.OrderType_Guid = dbo.T_OrderType.OrderType_Guid INNER JOIN
                      dbo.T_OrderState ON dbo.T_Order.OrderState_Guid = dbo.T_OrderState.OrderState_Guid INNER JOIN
                      dbo.T_User ON dbo.T_Salesman.User_Guid = dbo.T_User.User_Guid INNER JOIN
                      dbo.T_Rtt ON dbo.T_Order.Rtt_Guid = dbo.T_Rtt.Rtt_Guid

GO
GRANT SELECT ON [dbo].[OrderView] TO [public]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- Возвращает список записей из ( dbo.T_Order )
--
-- Входящие параметры:
--	@Order_Guid - УИ заказа
--
-- Выходные параметры:
--
-- Результат:
--    0 - успешное завершение
--    <>0 - ошибка запроса информации из базы данных

CREATE PROCEDURE [dbo].[usp_GetOrder] 
	@Order_Guid D_GUID = NULL,
	@Customer_Guid D_GUID = NULL,
	@BeginDate D_DATE,
	@EndDate D_DATE, 
	
  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output
AS

BEGIN

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = NULL;



  BEGIN TRY

		IF( @Order_Guid IS NULL )
			BEGIN
				IF( @Customer_Guid IS NULL )
					BEGIN
						SELECT Order_Guid, Order_Num, Order_SubNum, Order_BeginDate, OrderState_Guid, Order_MoneyBonus, 
							Depart_Guid, Salesman_Guid, Customer_Guid, CustomerChild_Guid, OrderType_Guid, PaymentType_Guid, 
							Order_Description, Order_DeliveryDate, Rtt_Guid, Address_Guid, Stock_Guid, Parts_Guid, Customer_Id, 
							Customer_Name, Customer_Code, Customer_UNP, Customer_OKPO, Customer_OKULP, CustomerActiveType_Guid, 
							CustomerStateType_Guid, DepartTeam_Guid, Depart_Code, Depart_IsActive, Salesman_Id, Salesman_IsActive, 
							User_Guid, User_FirstName, User_MiddleName, User_LastName, User_LoginName, User_IsActive, OrderState_Id, 
							OrderState_Name, OrderType_Name, PaymentType_Name, Rtt_Code, Rtt_Name, RttType_Guid, RttActiveType_Guid, 
							RttSpecCode_Guid, Segmentation_Guid, LicenceType_Guid, AddressString
						FROM dbo.OrderView	
						WHERE Order_BeginDate BETWEEN @BeginDate AND @EndDate
						ORDER BY Order_BeginDate, Customer_Name;
					END
				ELSE	
					BEGIN
						SELECT Order_Guid, Order_Num, Order_SubNum, Order_BeginDate, OrderState_Guid, Order_MoneyBonus, 
							Depart_Guid, Salesman_Guid, Customer_Guid, CustomerChild_Guid, OrderType_Guid, PaymentType_Guid, 
							Order_Description, Order_DeliveryDate, Rtt_Guid, Address_Guid, Stock_Guid, Parts_Guid, Customer_Id, 
							Customer_Name, Customer_Code, Customer_UNP, Customer_OKPO, Customer_OKULP, CustomerActiveType_Guid, 
							CustomerStateType_Guid, DepartTeam_Guid, Depart_Code, Depart_IsActive, Salesman_Id, Salesman_IsActive, 
							User_Guid, User_FirstName, User_MiddleName, User_LastName, User_LoginName, User_IsActive, OrderState_Id, 
							OrderState_Name, OrderType_Name, PaymentType_Name, Rtt_Code, Rtt_Name, RttType_Guid, RttActiveType_Guid, 
							RttSpecCode_Guid, Segmentation_Guid, LicenceType_Guid, AddressString
						FROM dbo.OrderView	
						WHERE Customer_Guid = @Customer_Guid
							AND Order_BeginDate BETWEEN @BeginDate AND @EndDate
						ORDER BY Order_BeginDate, Customer_Name;
					END
			END
		ELSE	
			BEGIN
				SELECT Order_Guid, Order_Num, Order_SubNum, Order_BeginDate, OrderState_Guid, Order_MoneyBonus, 
					Depart_Guid, Salesman_Guid, Customer_Guid, CustomerChild_Guid, OrderType_Guid, PaymentType_Guid, 
					Order_Description, Order_DeliveryDate, Rtt_Guid, Address_Guid, Stock_Guid, Parts_Guid, Customer_Id, 
					Customer_Name, Customer_Code, Customer_UNP, Customer_OKPO, Customer_OKULP, CustomerActiveType_Guid, 
					CustomerStateType_Guid, DepartTeam_Guid, Depart_Code, Depart_IsActive, Salesman_Id, Salesman_IsActive, 
					User_Guid, User_FirstName, User_MiddleName, User_LastName, User_LoginName, User_IsActive, OrderState_Id, 
					OrderState_Name, OrderType_Name, PaymentType_Name, Rtt_Code, Rtt_Name, RttType_Guid, RttActiveType_Guid, 
					RttSpecCode_Guid, Segmentation_Guid, LicenceType_Guid, AddressString
				FROM dbo.OrderView	
				WHERE Order_Guid = @Order_Guid;
			END
		
		
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
GRANT EXECUTE ON [dbo].[usp_GetOrder] TO [public]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- Возвращает список записей из ( dbo.OrderItmsView )
--
-- Входящие параметры:
--	@Order_Guid - УИ заказа
--
-- Выходные параметры:
--
-- Результат:
--    0 - успешное завершение
--    <>0 - ошибка запроса информации из базы данных

CREATE PROCEDURE [dbo].[usp_GetOrderItms] 
	@Order_Guid D_GUID,
	
  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output
AS

BEGIN

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = NULL;



  BEGIN TRY
		
		SELECT Order_Guid, Measure_Id, Measure_Name, Measure_ShortName, Parts_Guid, Measure_Guid, 
			OrderItms_Quantity, OrderItms_Guid, Parts_Id, Currency_Guid, Owner_Guid, Parttype_Guid, 
			Barcode, Partsubtype_Guid, Country_Guid, Currency_Abbr, Currency_Code, Owner_Id, Owner_Name, 
			Owner_ShortName, Owner_Description, Owner_IsActive, Vtm_Guid, Vtm_Id, Vtm_Name, Vtm_ShortName, 
			Vtm_IsActive, Parttype_Id, Parttype_NDSRate, Parttype_DemandsName, Partsubtype_Id, 
			Partsubtype_Name, Partsubtype_IsActive, PartLine_Guid, Partline_Id, Partline_Name, 
			Partline_IsActive, Parttype_Name, Country_Name, Country_Code, Parttype_IsActive, 
			PartsCategory_Guid, PartsCategory_Id, PartsCategory_Name, Parts_OriginalName, Parts_Name, 
			Parts_ShortName, Parts_BoxQuantity, Parts_PackQuantity, Parts_Weight, Parts_IsActive, 
			Parts_Article
		FROM dbo.OrderItmsView
		WHERE Order_Guid = @Order_Guid
		ORDER BY Parts_Name, Parts_Article;
		
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
GRANT EXECUTE ON [dbo].[usp_GetOrderItms] TO [public]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Creation date:	
-- Author:			

CREATE FUNCTION [dbo].[GetFirstOrderSate] ()
RETURNS D_GUID
WITH EXECUTE AS caller
AS
BEGIN
  
	DECLARE @OrderState_Guid D_GUID;
	SET @OrderState_Guid = NULL;
	
	DECLARE @OrderState_Id D_ID;

	SELECT @OrderState_Id = MIN( OrderState_Id ) FROM dbo.T_OrderState;
	IF( @OrderState_Id IS NOT NULL )
		SELECT @OrderState_Guid = OrderState_Guid FROM dbo.T_OrderState WHERE OrderState_Id = @OrderState_Id;

	RETURN @OrderState_Guid;
end

GO
GRANT EXECUTE ON [dbo].[GetFirstOrderSate] TO [public]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Добавляет новую запись в таблицу dbo.T_Order
--
-- Входящие параметры:
	--@Order_BeginDate - дата заказа
	--@OrderState_Guid - уи состояния заказа
	--@Order_MoneyBonus - признак "Бонус"
	--@Depart_Guid - уи подразделения
	--@Salesman_Guid - уи торгововго представителя
	--@Customer_Guid - уи клиента
	--@CustomerChild_Guid - уи дочернего клиента
	--@OrderType_Guid - уи типа заказа
	--@PaymentType_Guid - уи формы оплаты
	--@Order_Description - примечание
	--@Order_DeliveryDate - дата доставки
	--@Rtt_Guid - уи РТТ
	--@Address_Guid - уи адреса
	--@Stock_Guid - уи склада
	--@Parts_Guid - уи товара
--
--
-- Выходные параметры:
--  @Order_Guid - уникальный идентификатор записи
--  @ERROR_NUM - номер ошибки
--  @ERROR_MES - текст ошибки
--
-- Результат:
--    0 - Успешное завершение
--    <>0 - ошибка

CREATE PROCEDURE [dbo].[usp_AddOrder] 
	@Order_BeginDate [dbo].[D_DATE] = NULL,
	@OrderState_Guid [dbo].[D_GUID] = NULL,
	@Order_MoneyBonus [dbo].[D_YESNO],
	@Depart_Guid [dbo].[D_GUID],
	@Salesman_Guid [dbo].[D_GUID],
	@Customer_Guid [dbo].[D_GUID],
	@CustomerChild_Guid [dbo].[D_GUID] = NULL,
	@OrderType_Guid [dbo].[D_GUID],
	@PaymentType_Guid [dbo].[D_GUID],
	@Order_Description [dbo].[D_DESCRIPTION] = NULL,
	@Order_DeliveryDate [dbo].[D_DATE],
	@Rtt_Guid [dbo].[D_GUID],
	@Address_Guid [dbo].[D_GUID],
	@Stock_Guid [dbo].[D_GUID] = NULL,
	@Parts_Guid [dbo].[D_GUID] = NULL,

  @Order_Guid D_GUID output,
  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output

AS

BEGIN

	BEGIN TRY

    SET @ERROR_NUM = 0;
    SET @ERROR_MES = NULL;
    SET @Order_Guid = NULL;
    
    DECLARE @Order_Num D_ID = 0;
    DECLARE @Order_SubNum D_ID = 0;
    
    IF( @Order_BeginDate IS NULL ) SET @Order_BeginDate = dbo.TrimTime( GETDATE() );
    IF( @OrderState_Guid IS NULL ) SELECT @OrderState_Guid = dbo.GetFirstOrderSate();
    
    -- Проверяем наличие клиента с указанным идентификатором
    IF NOT EXISTS ( SELECT Customer_Guid FROM dbo.T_Customer WHERE Customer_Guid = @Customer_Guid )
      BEGIN
        SET @ERROR_NUM = 1;
        SET @ERROR_MES = 'В базе данных не найден клиент с указанным идетнификатором.' + Char(13) + 
          'УИ: ' + Char(9) + CONVERT( nvarchar(36), @Customer_Guid  );
        RETURN @ERROR_NUM;
      END

    SELECT @Order_Num = MAX( Order_Num ) FROM dbo.T_Order WHERE Customer_Guid = @Customer_Guid;
    IF( @Order_Num IS NULL ) SET @Order_Num = 1;
    ELSE SET @Order_Num = ( @Order_Num + 1 );
    
    -- Проверяем наличие подразделения с указанным идентификатором
    IF NOT EXISTS ( SELECT Depart_Guid FROM dbo.T_Depart WHERE Depart_Guid = @Depart_Guid )
      BEGIN
        SET @ERROR_NUM = 2;
        SET @ERROR_MES = 'В базе данных не найдено подразделение с указанным идетнификатором.' + Char(13) + 
          'УИ: ' + Char(9) + CONVERT( nvarchar(36), @Depart_Guid  );
        RETURN @ERROR_NUM;
      END

    -- Проверяем наличие торгового представителя с указанным идентификатором
    IF NOT EXISTS ( SELECT Salesman_Guid FROM dbo.T_Salesman WHERE Salesman_Guid = @Salesman_Guid )
      BEGIN
        SET @ERROR_NUM = 3;
        SET @ERROR_MES = 'В базе данных не найден торговый представитель с указанным идетнификатором.' + Char(13) + 
          'УИ: ' + Char(9) + CONVERT( nvarchar(36), @Salesman_Guid  );
        RETURN @ERROR_NUM;
      END
      
    -- Проверяем наличие дочернего подраздления с указанным идентификатором
    IF( @CustomerChild_Guid IS NOT NULL )
			BEGIN
			IF NOT EXISTS ( SELECT CustomerChild_Guid FROM dbo.T_CustomerChild WHERE CustomerChild_Guid = @CustomerChild_Guid )
				BEGIN
					SET @ERROR_NUM = 4;
					SET @ERROR_MES = 'В базе данных не найден дочерний клиент с указанным идетнификатором.' + Char(13) + 
						'УИ: ' + Char(9) + CONVERT( nvarchar(36), @CustomerChild_Guid );
					RETURN @ERROR_NUM;
				END
			END

    -- Проверяем наличие типа заказа с указанным идентификатором
    IF( @OrderType_Guid IS NOT NULL )
			BEGIN
				IF NOT EXISTS ( SELECT OrderType_Guid FROM dbo.T_OrderType WHERE OrderType_Guid = @OrderType_Guid )
					BEGIN
						SET @ERROR_NUM = 5;
						SET @ERROR_MES = 'В базе данных не найден тип заказа с указанным идетнификатором.' + Char(13) + 
							'УИ: ' + Char(9) + CONVERT( nvarchar(36), @OrderType_Guid  );
						RETURN @ERROR_NUM;
					END
			END

    -- Проверяем наличие формы оплаты с указанным идентификатором
    IF( @PaymentType_Guid IS NOT NULL )
			BEGIN
				IF NOT EXISTS ( SELECT PaymentType_Guid FROM dbo.T_PaymentType WHERE PaymentType_Guid = @PaymentType_Guid )
					BEGIN
						SET @ERROR_NUM = 6;
						SET @ERROR_MES = 'В базе данных не найдена форма оплаты с указанным идетнификатором.' + Char(13) + 
							'УИ: ' + Char(9) + CONVERT( nvarchar(36), @PaymentType_Guid  );
						RETURN @ERROR_NUM;
					END
			END

    -- Проверяем наличие розничной точки с указанным идентификатором
    IF( @Rtt_Guid IS NOT NULL )
			BEGIN
				IF NOT EXISTS ( SELECT Rtt_Guid FROM dbo.T_CustomerRtt WHERE Customer_Guid = @Customer_Guid AND Rtt_Guid = @Rtt_Guid )
					BEGIN
						SET @ERROR_NUM = 7;
						SET @ERROR_MES = 'В базе данных не найдена розничная точка с указанным идетнификатором.' + Char(13) + 
							'УИ: ' + Char(9) + CONVERT( nvarchar(36), @PaymentType_Guid  );
						RETURN @ERROR_NUM;
					END
			END

    -- Проверяем наличие адреса доставки с указанным идентификатором
    IF( @Address_Guid IS NOT NULL )
			BEGIN
				IF NOT EXISTS ( SELECT Address_Guid FROM dbo.T_Address WHERE Address_Guid = @Address_Guid )
					BEGIN
						SET @ERROR_NUM = 8;
						SET @ERROR_MES = 'В базе данных не найден адрес доставки с указанным идетнификатором.' + Char(13) + 
							'УИ: ' + Char(9) + CONVERT( nvarchar(36), @Address_Guid  );
						RETURN @ERROR_NUM;
					END
			END

    -- Проверяем наличие товара с указанным идентификатором
    IF( @Parts_Guid IS NOT NULL )
			BEGIN
				IF NOT EXISTS ( SELECT Parts_Guid FROM dbo.T_Parts WHERE Parts_Guid = @Parts_Guid )
					BEGIN
						SET @ERROR_NUM = 9;
						SET @ERROR_MES = 'В базе данных не найден товар с указанным идетнификатором.' + Char(13) + 
							'УИ: ' + Char(9) + CONVERT( nvarchar(36), @Parts_Guid  );
						RETURN @ERROR_NUM;
					END
			END

    -- Проверяем наличие склада с указанным идентификатором
    IF( @Stock_Guid IS NOT NULL )
			BEGIN
				IF NOT EXISTS ( SELECT Stock_Guid FROM dbo.T_Stock WHERE Stock_Guid = @Stock_Guid )
					BEGIN
						SET @ERROR_NUM = 10;
						SET @ERROR_MES = 'В базе данных не найден склад с указанным идетнификатором.' + Char(13) + 
							'УИ: ' + Char(9) + CONVERT( nvarchar(36), @Stock_Guid  );
						RETURN @ERROR_NUM;
					END
			END

    DECLARE @NewID D_GUID;
    SET @NewID = NEWID( );	
    
    INSERT INTO dbo.T_Order( Order_Guid, Order_Num, Order_SubNum, Order_BeginDate, OrderState_Guid, 
			Order_MoneyBonus, Depart_Guid, Salesman_Guid, Customer_Guid, CustomerChild_Guid, OrderType_Guid, 
			PaymentType_Guid, Order_Description, Order_DeliveryDate, Rtt_Guid, Address_Guid, Stock_Guid, Parts_Guid )
    VALUES( @NewID, @Order_Num, @Order_SubNum, @Order_BeginDate, @OrderState_Guid, 
			@Order_MoneyBonus, @Depart_Guid, @Salesman_Guid, @Customer_Guid, @CustomerChild_Guid, @OrderType_Guid, 
			@PaymentType_Guid, @Order_Description, @Order_DeliveryDate, @Rtt_Guid, @Address_Guid, @Stock_Guid, @Parts_Guid );
    SET @Order_Guid = @NewID;
    
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
GRANT EXECUTE ON [dbo].[usp_AddOrder] TO [public]
GO

/****** Object:  StoredProcedure [dbo].[usp_DeleteVendorContract]    Script Date: 02/19/2012 10:54:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Удаление элемента из таблицы dbo.T_Order
--
-- Входящие параметры:
--	@Order_Guid - уникальный идентификатор записи
--	@bOnlyDeclaration - признак "Удалить только приложение к заказу"
--
-- Результат:
--    0 - успешное завершение
--    <>0 - ошибка

CREATE PROCEDURE [dbo].[usp_DeleteOrder] 
	@Order_Guid D_GUID,
	@bOnlyDeclaration D_YESNO = 0,

  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output

AS

BEGIN

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = NULL;

	BEGIN TRY

		DELETE FROM dbo.T_OrderItms WHERE Order_Guid = @Order_Guid;

		IF( @bOnlyDeclaration = 0 )
			DELETE FROM dbo.T_Order WHERE Order_Guid = @Order_Guid
    
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
GRANT EXECUTE ON [dbo].[usp_DeleteOrder] TO [public]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Редактирование записи в таблице dbo.T_Order
--
-- Входящие параметры:
--  @Order_Guid - уникальный идентификатор записи
	--@Order_BeginDate - дата заказа
	--@OrderState_Guid - уи состояния заказа
	--@Order_MoneyBonus - признак "Бонус"
	--@Depart_Guid - уи подразделения
	--@Salesman_Guid - уи торгововго представителя
	--@Customer_Guid - уи клиента
	--@CustomerChild_Guid - уи дочернего клиента
	--@OrderType_Guid - уи типа заказа
	--@PaymentType_Guid - уи формы оплаты
	--@Order_Description - примечание
	--@Order_DeliveryDate - дата доставки
	--@Rtt_Guid - уи РТТ
	--@Address_Guid - уи адреса
	--@Stock_Guid - уи склада
	--@Parts_Guid - уи товара
--
--
-- Выходные параметры:
--  @ERROR_NUM - номер ошибки
--  @ERROR_MES - текст ошибки
--
-- Результат:
--    0 - Успешное завершение
--    <>0 - ошибка

CREATE PROCEDURE [dbo].[usp_EditOrder] 
  @Order_Guid D_GUID,
	@Order_BeginDate [dbo].[D_DATE] = NULL,
	@OrderState_Guid [dbo].[D_GUID] = NULL,
	@Order_MoneyBonus [dbo].[D_YESNO],
	@Depart_Guid [dbo].[D_GUID],
	@Salesman_Guid [dbo].[D_GUID],
	@Customer_Guid [dbo].[D_GUID],
	@CustomerChild_Guid [dbo].[D_GUID] = NULL,
	@OrderType_Guid [dbo].[D_GUID],
	@PaymentType_Guid [dbo].[D_GUID],
	@Order_Description [dbo].[D_DESCRIPTION] = NULL,
	@Order_DeliveryDate [dbo].[D_DATE],
	@Rtt_Guid [dbo].[D_GUID],
	@Address_Guid [dbo].[D_GUID],
	@Stock_Guid [dbo].[D_GUID] = NULL,
	@Parts_Guid [dbo].[D_GUID] = NULL,

  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output

AS

BEGIN

	BEGIN TRY

    SET @ERROR_NUM = 0;
    SET @ERROR_MES = NULL;
    
    IF( @Order_BeginDate IS NULL ) SET @Order_BeginDate = dbo.TrimTime( GETDATE() );
    IF( @OrderState_Guid IS NULL ) SELECT @OrderState_Guid = dbo.GetFirstOrderSate();
    
    -- Проверяем наличие заказа с указанным идентификатором
    IF NOT EXISTS ( SELECT Order_Guid FROM dbo.T_Order WHERE Order_Guid = @Order_Guid )
      BEGIN
        SET @ERROR_NUM = 11;
        SET @ERROR_MES = 'В базе данных не найден заказ с указанным идетнификатором.' + Char(13) + 
          'УИ: ' + Char(9) + CONVERT( nvarchar(36), @Order_Guid  );
        RETURN @ERROR_NUM;
      END

    -- Проверяем наличие клиента с указанным идентификатором
    IF NOT EXISTS ( SELECT Customer_Guid FROM dbo.T_Customer WHERE Customer_Guid = @Customer_Guid )
      BEGIN
        SET @ERROR_NUM = 1;
        SET @ERROR_MES = 'В базе данных не найден клиент с указанным идетнификатором.' + Char(13) + 
          'УИ: ' + Char(9) + CONVERT( nvarchar(36), @Customer_Guid  );
        RETURN @ERROR_NUM;
      END

    -- Проверяем наличие подразделения с указанным идентификатором
    IF NOT EXISTS ( SELECT Depart_Guid FROM dbo.T_Depart WHERE Depart_Guid = @Depart_Guid )
      BEGIN
        SET @ERROR_NUM = 2;
        SET @ERROR_MES = 'В базе данных не найдено подразделение с указанным идетнификатором.' + Char(13) + 
          'УИ: ' + Char(9) + CONVERT( nvarchar(36), @Depart_Guid  );
        RETURN @ERROR_NUM;
      END

    -- Проверяем наличие торгового представителя с указанным идентификатором
    IF NOT EXISTS ( SELECT Salesman_Guid FROM dbo.T_Salesman WHERE Salesman_Guid = @Salesman_Guid )
      BEGIN
        SET @ERROR_NUM = 3;
        SET @ERROR_MES = 'В базе данных не найден торговый представитель с указанным идетнификатором.' + Char(13) + 
          'УИ: ' + Char(9) + CONVERT( nvarchar(36), @Salesman_Guid  );
        RETURN @ERROR_NUM;
      END
      
    -- Проверяем наличие дочернего подраздления с указанным идентификатором
    IF( @CustomerChild_Guid IS NOT NULL )
			BEGIN
			IF NOT EXISTS ( SELECT CustomerChild_Guid FROM dbo.T_CustomerChild WHERE CustomerChild_Guid = @CustomerChild_Guid )
				BEGIN
					SET @ERROR_NUM = 4;
					SET @ERROR_MES = 'В базе данных не найден дочерний клиент с указанным идетнификатором.' + Char(13) + 
						'УИ: ' + Char(9) + CONVERT( nvarchar(36), @CustomerChild_Guid );
					RETURN @ERROR_NUM;
				END
			END

    -- Проверяем наличие типа заказа с указанным идентификатором
    IF( @OrderType_Guid IS NOT NULL )
			BEGIN
				IF NOT EXISTS ( SELECT OrderType_Guid FROM dbo.T_OrderType WHERE OrderType_Guid = @OrderType_Guid )
					BEGIN
						SET @ERROR_NUM = 5;
						SET @ERROR_MES = 'В базе данных не найден тип заказа с указанным идетнификатором.' + Char(13) + 
							'УИ: ' + Char(9) + CONVERT( nvarchar(36), @OrderType_Guid  );
						RETURN @ERROR_NUM;
					END
			END

    -- Проверяем наличие формы оплаты с указанным идентификатором
    IF( @PaymentType_Guid IS NOT NULL )
			BEGIN
				IF NOT EXISTS ( SELECT PaymentType_Guid FROM dbo.T_PaymentType WHERE PaymentType_Guid = @PaymentType_Guid )
					BEGIN
						SET @ERROR_NUM = 6;
						SET @ERROR_MES = 'В базе данных не найдена форма оплаты с указанным идетнификатором.' + Char(13) + 
							'УИ: ' + Char(9) + CONVERT( nvarchar(36), @PaymentType_Guid  );
						RETURN @ERROR_NUM;
					END
			END

    -- Проверяем наличие розничной точки с указанным идентификатором
    IF( @Rtt_Guid IS NOT NULL )
			BEGIN
				IF NOT EXISTS ( SELECT Rtt_Guid FROM dbo.T_CustomerRtt WHERE Customer_Guid = @Customer_Guid AND Rtt_Guid = @Rtt_Guid )
					BEGIN
						SET @ERROR_NUM = 7;
						SET @ERROR_MES = 'В базе данных не найдена розничная точка с указанным идетнификатором.' + Char(13) + 
							'УИ: ' + Char(9) + CONVERT( nvarchar(36), @PaymentType_Guid  );
						RETURN @ERROR_NUM;
					END
			END

    -- Проверяем наличие адреса доставки с указанным идентификатором
    IF( @Address_Guid IS NOT NULL )
			BEGIN
				IF NOT EXISTS ( SELECT Address_Guid FROM dbo.T_Address WHERE Address_Guid = @Address_Guid )
					BEGIN
						SET @ERROR_NUM = 8;
						SET @ERROR_MES = 'В базе данных не найден адрес доставки с указанным идетнификатором.' + Char(13) + 
							'УИ: ' + Char(9) + CONVERT( nvarchar(36), @Address_Guid  );
						RETURN @ERROR_NUM;
					END
			END

    -- Проверяем наличие товара с указанным идентификатором
    IF( @Parts_Guid IS NOT NULL )
			BEGIN
				IF NOT EXISTS ( SELECT Parts_Guid FROM dbo.T_Parts WHERE Parts_Guid = @Parts_Guid )
					BEGIN
						SET @ERROR_NUM = 9;
						SET @ERROR_MES = 'В базе данных не найден товар с указанным идетнификатором.' + Char(13) + 
							'УИ: ' + Char(9) + CONVERT( nvarchar(36), @Parts_Guid  );
						RETURN @ERROR_NUM;
					END
			END

    -- Проверяем наличие склада с указанным идентификатором
    IF( @Stock_Guid IS NOT NULL )
			BEGIN
				IF NOT EXISTS ( SELECT Stock_Guid FROM dbo.T_Stock WHERE Stock_Guid = @Stock_Guid )
					BEGIN
						SET @ERROR_NUM = 10;
						SET @ERROR_MES = 'В базе данных не найден склад с указанным идетнификатором.' + Char(13) + 
							'УИ: ' + Char(9) + CONVERT( nvarchar(36), @Stock_Guid  );
						RETURN @ERROR_NUM;
					END
			END

    UPDATE dbo.T_Order SET Order_BeginDate = @Order_BeginDate, OrderState_Guid = @OrderState_Guid, 
			Order_MoneyBonus = @Order_MoneyBonus, Depart_Guid = @Depart_Guid, Salesman_Guid = @Salesman_Guid, 
			Customer_Guid = @Customer_Guid, CustomerChild_Guid = @CustomerChild_Guid, OrderType_Guid = @OrderType_Guid,
			PaymentType_Guid = @PaymentType_Guid, Order_Description = @Order_Description, 
			Order_DeliveryDate = @Order_DeliveryDate, 
			Rtt_Guid = @Rtt_Guid, Address_Guid = @Address_Guid, Stock_Guid = @Stock_Guid, Parts_Guid = @Parts_Guid
    WHERE Order_Guid = @Order_Guid;
    
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
GRANT EXECUTE ON [dbo].[usp_EditOrder] TO [public]
GO

CREATE TYPE [dbo].[udt_OrderItms] AS TABLE(
	[Parts_Guid] [uniqueidentifier]  NULL,
	[Measure_Guid] [uniqueidentifier]  NULL,
	[OrderItms_Quantity] [float]  NULL
)

GO
GRANT CONTROL ON TYPE::[dbo].[udt_OrderItms] TO [public]
GO
GRANT REFERENCES ON TYPE::[dbo].[udt_OrderItms] TO [public]
GO
GRANT TAKE OWNERSHIP ON TYPE::[dbo].[udt_OrderItms] TO [public]
GO
GRANT VIEW DEFINITION ON TYPE::[dbo].[udt_OrderItms] TO [public]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Добавляет записи в таблицу dbo.T_OrderItms
--
-- Входящие параметры:
--@Order_Guid - уникальный идентификатор заказа
--@tOrderItms - приложение к заказу
--
--
-- Выходные параметры:
--  @ERROR_NUM - номер ошибки
--  @ERROR_MES - текст ошибки
--
-- Результат:
--    0 - Успешное завершение
--    <>0 - ошибка

CREATE PROCEDURE [dbo].[usp_AddOrderItms] 
	@Order_Guid D_GUID,
	@tOrderItms dbo.udt_OrderItms READONLY,

  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output

AS

BEGIN

	BEGIN TRY

    SET @ERROR_NUM = 0;
    SET @ERROR_MES = NULL;
    
    -- Проверяем наличие заказа с указанным идентификатором
    IF NOT EXISTS ( SELECT Order_Guid FROM dbo.T_Order WHERE Order_Guid = @Order_Guid )
      BEGIN
        SET @ERROR_NUM = 11;
        SET @ERROR_MES = 'В базе данных не найден заказ с указанным идетнификатором.' + Char(13) + 
          'УИ: ' + Char(9) + CONVERT( nvarchar(36), @Order_Guid  );
        RETURN @ERROR_NUM;
      END
      
    INSERT INTO dbo.T_OrderItms( OrderItms_Guid, Order_Guid, Parts_Guid, Measure_Guid, OrderItms_Quantity )  
    SELECT NEWID(), @Order_Guid, Parts_Guid, Measure_Guid, OrderItms_Quantity
    FROM @tOrderItms;
    
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
GRANT EXECUTE ON [dbo].[usp_AddOrderItms] TO [public]
GO

/****** Object:  View [dbo].[OrderStateView]    Script Date: 02/19/2012 12:33:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[OrderStateView]
AS
SELECT     OrderState_Guid, OrderState_Id, OrderState_Name, OrderState_Description, OrderState_IsActive, OrderState_ShowInDeliveryList, 
                      OrderState_ShowInPDASupplList
FROM         dbo.T_OrderState

GO

GO
GRANT SELECT ON [dbo].[OrderStateView] TO [public]
GO

/****** Object:  StoredProcedure [dbo].[usp_AddOwner]    Script Date: 02/19/2012 12:34:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Добавляет новую запись в таблицу dbo.T_OrderState
--
-- Входящие параметры:
--	@OrderState_Name - наименование
--	@OrderState_Id - № п/п в списке
--	@OrderState_Description - примечание
--	@OrderState_IsActive - признак активности
--
--
-- Выходные параметры:
--  @OrderState_Guid - уникальный идентификатор записи
--  @ERROR_NUM - номер ошибки
--  @ERROR_MES - текст ошибки
--
-- Результат:
--    0 - Успешное завершение
--    <>0 - ошибка

CREATE PROCEDURE [dbo].[usp_AddOrderState] 
  @OrderState_Id D_ID, 
  @OrderState_Name D_NAME, 
  @OrderState_Description D_DESCRIPTION = NULL, 
  @OrderState_IsActive D_YESNO, 

  @OrderState_Guid D_GUID output,
  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output

AS

BEGIN

	BEGIN TRY

    SET @ERROR_NUM = 0;
    SET @ERROR_MES = NULL;
    SET @OrderState_Guid = NULL;

    -- Проверяем наличие записи с заданным именем
    IF EXISTS ( SELECT * FROM dbo.T_OrderState WHERE OrderState_Name = @OrderState_Name )
      BEGIN
        SET @ERROR_NUM = 2;
        SET @ERROR_MES = 'В базе данных уже присутствует состояние заказа с указанным наименованием.' + Char(13) + 
          'Имя: ' + Char(9) + @OrderState_Name;
        RETURN @ERROR_NUM;
      END

    DECLARE @NewID D_GUID;
    SET @NewID = NEWID ( );	
    
    INSERT INTO dbo.T_OrderState( OrderState_Guid, OrderState_Id, OrderState_Name, OrderState_Description, OrderState_IsActive, OrderState_ShowInDeliveryList, OrderState_ShowInPDASupplList )
    VALUES( @NewID, @OrderState_Id, @OrderState_Name, @OrderState_Description, @OrderState_IsActive, 0, 0 );
    SET @OrderState_Guid = @NewID;
    
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
GRANT EXECUTE ON [dbo].[usp_AddOrderState] TO [public]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Редактирует запись в таблицу dbo.T_OrderState
--
-- Входящие параметры:
--  @OrderState_Guid - уникальный идентификатор записи
--	@OrderState_Name - наименование
--	@OrderState_Id - № п/п в списке
--	@OrderState_Description - примечание
--	@OrderState_IsActive - признак активности
--
--
-- Выходные параметры:
--  @ERROR_NUM - номер ошибки
--  @ERROR_MES - текст ошибки
--
-- Результат:
--    0 - Успешное завершение
--    <>0 - ошибка

CREATE PROCEDURE [dbo].[usp_EditOrderState] 
  @OrderState_Guid D_GUID,
  @OrderState_Id D_ID, 
  @OrderState_Name D_NAME, 
  @OrderState_Description D_DESCRIPTION = NULL, 
  @OrderState_IsActive D_YESNO, 

  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output

AS

BEGIN

	BEGIN TRY

    SET @ERROR_NUM = 0;
    SET @ERROR_MES = NULL;

    -- Проверяем наличие записи с заданным именем
    IF EXISTS ( SELECT * FROM dbo.T_OrderState WHERE OrderState_Name = @OrderState_Name AND OrderState_Guid <> @OrderState_Guid )
      BEGIN
        SET @ERROR_NUM = 2;
        SET @ERROR_MES = 'В базе данных уже присутствует состояние заказа с указанным наименованием.' + Char(13) + 
          'Имя: ' + Char(9) + @OrderState_Name;
        RETURN @ERROR_NUM;
      END

    UPDATE dbo.T_OrderState SET OrderState_Id = @OrderState_Id, OrderState_Name = @OrderState_Name, 
			OrderState_Description = @OrderState_Description, OrderState_IsActive = @OrderState_IsActive
		WHERE OrderState_Guid = @OrderState_Guid;	
    
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
GRANT EXECUTE ON [dbo].[usp_EditOrderState] TO [public]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Удаление элемента из таблицы dbo.T_OrderState
--
-- Входящие параметры:
--	@OrderState_Guid - уникальный идентификатор товарной марки
--
-- Результат:
--    0 - успешное завершение
--    <>0 - ошибка

CREATE PROCEDURE [dbo].[usp_DeleteOrderState] 
	@OrderState_Guid D_GUID,

  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output

AS

BEGIN

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = NULL;

	BEGIN TRY

   IF NOT EXISTS ( SELECT Order_Guid FROM T_Order WHERE OrderState_Guid = @OrderState_Guid )
		DELETE FROM T_OrderState WHERE OrderState_Guid = @OrderState_Guid;
    
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
GRANT EXECUTE ON [dbo].[usp_DeleteOrderState] TO [public]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- Возвращает список записей из ( OrderStateView )
--
-- Входящие параметры:
--
-- Выходные параметры:
--
-- Результат:
--    0 - успешное завершение
--    <>0 - ошибка запроса информации из базы данных

CREATE PROCEDURE [dbo].[usp_GetOrderState] 
  @ERROR_NUM int output,
  @ERROR_MES nvarchar(4000) output
AS

BEGIN

  SET @ERROR_NUM = 0;
  SET @ERROR_MES = '';

  BEGIN TRY

		SELECT OrderState_Guid, OrderState_Id, OrderState_Name, OrderState_Description, 
			OrderState_IsActive, OrderState_ShowInDeliveryList, OrderState_ShowInPDASupplList
		FROM dbo.OrderStateView
		ORDER BY OrderState_Id;
		
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
GRANT EXECUTE ON [dbo].[usp_GetOrderState] TO [public]
GO

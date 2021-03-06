/*
   5 февраля 2014 г.11:17:13
   User: 
   Server: IT-3
   Database: ERP_Mercury
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.T_Waybill
	DROP CONSTRAINT FK_T_Waybill_T_CustomerChild
GO
ALTER TABLE dbo.T_CustomerChild SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.T_Waybill
	DROP CONSTRAINT FK_T_Waybill_T_PaymentType
GO
ALTER TABLE dbo.T_PaymentType SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.T_Waybill
	DROP CONSTRAINT FK_T_Waybill_T_Suppl
GO
ALTER TABLE dbo.T_Suppl SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.T_Waybill
	DROP CONSTRAINT FK_T_Waybill_T_Stock
GO
ALTER TABLE dbo.T_Stock SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.T_Waybill
	DROP CONSTRAINT FK_T_Waybill_T_Rtt
GO
ALTER TABLE dbo.T_Rtt SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.T_Waybill
	DROP CONSTRAINT FK_T_Waybill_T_WaybillState
GO
ALTER TABLE dbo.T_WaybillState SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.T_Waybill
	DROP CONSTRAINT FK_T_Waybill_T_WaybillShipMode
GO
ALTER TABLE dbo.T_WaybillShipMode SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.T_Waybill
	DROP CONSTRAINT FK_T_Waybill_T_Depart
GO
ALTER TABLE dbo.T_Depart SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.T_Waybill
	DROP CONSTRAINT FK_T_Waybill_T_Customer
GO
ALTER TABLE dbo.T_Customer SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.T_Waybill
	DROP CONSTRAINT FK_T_Waybill_T_Currency
GO
ALTER TABLE dbo.T_Currency SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.T_Waybill
	DROP CONSTRAINT FK_T_Waybill_T_Company
GO
ALTER TABLE dbo.T_Company SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.T_Waybill
	DROP CONSTRAINT FK_T_Waybill_T_Address
GO
ALTER TABLE dbo.T_Address SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.T_Waybill
	DROP CONSTRAINT DF_T_Waybill_Waybill_Bonus
GO
ALTER TABLE dbo.T_Waybill
	DROP CONSTRAINT DF_T_Waybill_Waybill_CurrencyRate
GO
ALTER TABLE dbo.T_Waybill
	DROP CONSTRAINT DF_T_Waybill_Waybill_AllPrice
GO
ALTER TABLE dbo.T_Waybill
	DROP CONSTRAINT DF_T_Waybill_Waybill_RetAllPrice
GO
ALTER TABLE dbo.T_Waybill
	DROP CONSTRAINT DF_T_Waybill_Waybill_AllDiscount
GO
ALTER TABLE dbo.T_Waybill
	DROP CONSTRAINT DF_T_Waybill_Waybill_AmountPaid
GO
ALTER TABLE dbo.T_Waybill
	DROP CONSTRAINT DF_T_Waybill_Waybill_CurrencyAllPrice
GO
ALTER TABLE dbo.T_Waybill
	DROP CONSTRAINT DF_T_Waybill_Waybill_CurrencyRetAllPrice
GO
ALTER TABLE dbo.T_Waybill
	DROP CONSTRAINT DF_T_Waybill_Waybill_CurrencyAllDiscount
GO
ALTER TABLE dbo.T_Waybill
	DROP CONSTRAINT DF_T_Waybill_Waybill_CurrencyAmountPaid
GO
ALTER TABLE dbo.T_Waybill
	DROP CONSTRAINT DF_T_Waybill_Waybill_Quantity
GO
ALTER TABLE dbo.T_Waybill
	DROP CONSTRAINT DF_T_Waybill_Waybill_RetQuantity
GO
ALTER TABLE dbo.T_Waybill
	DROP CONSTRAINT DF_T_Waybill_Waybill_Weight
GO
ALTER TABLE dbo.T_Waybill
	DROP CONSTRAINT DF_T_Waybill_Waybill_ShowInDeliveryList
GO
CREATE TABLE dbo.Tmp_T_Waybill
	(
	Waybill_Guid dbo.D_GUID NOT NULL,
	Waybill_Id dbo.D_INTEGER NULL,
	Suppl_Guid dbo.D_GUID NULL,
	Stock_Guid dbo.D_GUID NOT NULL,
	Company_Guid dbo.D_GUID NOT NULL,
	Currency_Guid dbo.D_GUID NOT NULL,
	Depart_Guid dbo.D_GUID NOT NULL,
	Customer_Guid dbo.D_GUID NOT NULL,
	CustomerChild_Guid dbo.D_GUID NULL,
	Rtt_Guid dbo.D_GUID NOT NULL,
	Address_Guid dbo.D_GUID NOT NULL,
	PaymentType_Guid dbo.D_GUID NULL,
	Waybill_Num dbo.D_NAMESHORT NOT NULL,
	Waybill_BeginDate dbo.D_DATE NOT NULL,
	Waybill_DeliveryDate dbo.D_DATE NOT NULL,
	WaybillParent_Guid dbo.D_GUID NULL,
	Waybill_Bonus dbo.D_YESNO NOT NULL,
	WaybillState_Guid dbo.D_GUID NOT NULL,
	WaybillShipMode_Guid dbo.D_GUID NOT NULL,
	Waybill_ShipDate dbo.D_DATE NULL,
	Waybill_Description dbo.D_DESCRIPTION NULL,
	Waybill_CurrencyRate dbo.D_MONEY NOT NULL,
	Waybill_AllPrice dbo.D_MONEY NOT NULL,
	Waybill_RetAllPrice dbo.D_MONEY NOT NULL,
	Waybill_AllDiscount dbo.D_MONEY NOT NULL,
	Waybill_TotalPrice  AS ([Waybill_AllPrice]-[Waybill_AllDiscount]),
	Waybill_AmountPaid dbo.D_MONEY NOT NULL,
	Waybill_Saldo  AS ( ( [Waybill_AmountPaid] + [Waybill_RetAllPrice] ) - ( [Waybill_AllPrice]-[Waybill_AllDiscount])),
	Waybill_CurrencyAllPrice dbo.D_MONEY NOT NULL,
	Waybill_CurrencyRetAllPrice dbo.D_MONEY NOT NULL,
	Waybill_CurrencyAllDiscount dbo.D_MONEY NOT NULL,
	Waybill_CurrencyTotalPrice  AS ([Waybill_AllPrice]-[Waybill_AllDiscount]),
	Waybill_CurrencyAmountPaid dbo.D_MONEY NOT NULL,
	Waybill_CurrencySaldo  AS ( ( [Waybill_CurrencyAmountPaid] + [Waybill_CurrencyRetAllPrice] ) - ([Waybill_AllPrice]-[Waybill_AllDiscount]) ),
	Waybill_Quantity dbo.D_QUANTITY NOT NULL,
	Waybill_RetQuantity dbo.D_QUANTITY NOT NULL,
	Waybill_LeavQuantity  AS ([Waybill_Quantity]-[Waybill_RetQuantity]),
	Waybill_Weight dbo.D_WEIGHT NOT NULL,
	Waybill_ShowInDeliveryList dbo.D_YESNO NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_T_Waybill SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_T_Waybill ADD CONSTRAINT
	DF_T_Waybill_Waybill_Bonus DEFAULT ((0)) FOR Waybill_Bonus
GO
ALTER TABLE dbo.Tmp_T_Waybill ADD CONSTRAINT
	DF_T_Waybill_Waybill_CurrencyRate DEFAULT ((0)) FOR Waybill_CurrencyRate
GO
ALTER TABLE dbo.Tmp_T_Waybill ADD CONSTRAINT
	DF_T_Waybill_Waybill_AllPrice DEFAULT ((0)) FOR Waybill_AllPrice
GO
ALTER TABLE dbo.Tmp_T_Waybill ADD CONSTRAINT
	DF_T_Waybill_Waybill_RetAllPrice DEFAULT ((0)) FOR Waybill_RetAllPrice
GO
ALTER TABLE dbo.Tmp_T_Waybill ADD CONSTRAINT
	DF_T_Waybill_Waybill_AllDiscount DEFAULT ((0)) FOR Waybill_AllDiscount
GO
ALTER TABLE dbo.Tmp_T_Waybill ADD CONSTRAINT
	DF_T_Waybill_Waybill_AmountPaid DEFAULT ((0)) FOR Waybill_AmountPaid
GO
ALTER TABLE dbo.Tmp_T_Waybill ADD CONSTRAINT
	DF_T_Waybill_Waybill_CurrencyAllPrice DEFAULT ((0)) FOR Waybill_CurrencyAllPrice
GO
ALTER TABLE dbo.Tmp_T_Waybill ADD CONSTRAINT
	DF_T_Waybill_Waybill_CurrencyRetAllPrice DEFAULT ((0)) FOR Waybill_CurrencyRetAllPrice
GO
ALTER TABLE dbo.Tmp_T_Waybill ADD CONSTRAINT
	DF_T_Waybill_Waybill_CurrencyAllDiscount DEFAULT ((0)) FOR Waybill_CurrencyAllDiscount
GO
ALTER TABLE dbo.Tmp_T_Waybill ADD CONSTRAINT
	DF_T_Waybill_Waybill_CurrencyAmountPaid DEFAULT ((0)) FOR Waybill_CurrencyAmountPaid
GO
ALTER TABLE dbo.Tmp_T_Waybill ADD CONSTRAINT
	DF_T_Waybill_Waybill_Quantity DEFAULT ((0)) FOR Waybill_Quantity
GO
ALTER TABLE dbo.Tmp_T_Waybill ADD CONSTRAINT
	DF_T_Waybill_Waybill_RetQuantity DEFAULT ((0)) FOR Waybill_RetQuantity
GO
ALTER TABLE dbo.Tmp_T_Waybill ADD CONSTRAINT
	DF_T_Waybill_Waybill_Weight DEFAULT ((0)) FOR Waybill_Weight
GO
ALTER TABLE dbo.Tmp_T_Waybill ADD CONSTRAINT
	DF_T_Waybill_Waybill_ShowInDeliveryList DEFAULT ((0)) FOR Waybill_ShowInDeliveryList
GO
IF EXISTS(SELECT * FROM dbo.T_Waybill)
	 EXEC('INSERT INTO dbo.Tmp_T_Waybill (Waybill_Guid, Waybill_Id, Suppl_Guid, Stock_Guid, Company_Guid, Currency_Guid, Depart_Guid, Customer_Guid, CustomerChild_Guid, Rtt_Guid, Address_Guid, PaymentType_Guid, Waybill_Num, Waybill_BeginDate, Waybill_DeliveryDate, WaybillParent_Guid, Waybill_Bonus, WaybillState_Guid, WaybillShipMode_Guid, Waybill_ShipDate, Waybill_Description, Waybill_CurrencyRate, Waybill_AllPrice, Waybill_RetAllPrice, Waybill_AllDiscount, Waybill_AmountPaid, Waybill_CurrencyAllPrice, Waybill_CurrencyRetAllPrice, Waybill_CurrencyAllDiscount, Waybill_CurrencyAmountPaid, Waybill_Quantity, Waybill_RetQuantity, Waybill_Weight, Waybill_ShowInDeliveryList)
		SELECT Waybill_Guid, Waybill_Id, Suppl_Guid, Stock_Guid, Company_Guid, Currency_Guid, Depart_Guid, Customer_Guid, CustomerChild_Guid, Rtt_Guid, Address_Guid, PaymentType_Guid, Waybill_Num, Waybill_BeginDate, Waybill_DeliveryDate, WaybillParent_Guid, Waybill_Bonus, WaybillState_Guid, WaybillShipMode_Guid, Waybill_ShipDate, Waybill_Description, Waybill_CurrencyRate, Waybill_AllPrice, Waybill_RetAllPrice, Waybill_AllDiscount, Waybill_AmountPaid, Waybill_CurrencyAllPrice, Waybill_CurrencyRetAllPrice, Waybill_CurrencyAllDiscount, Waybill_CurrencyAmountPaid, Waybill_Quantity, Waybill_RetQuantity, Waybill_Weight, Waybill_ShowInDeliveryList FROM dbo.T_Waybill WITH (HOLDLOCK TABLOCKX)')
GO
ALTER TABLE dbo.T_WaybItem
	DROP CONSTRAINT FK_T_WaybItem_T_Waybill
GO
DROP TABLE dbo.T_Waybill
GO
EXECUTE sp_rename N'dbo.Tmp_T_Waybill', N'T_Waybill', 'OBJECT' 
GO
ALTER TABLE dbo.T_Waybill ADD CONSTRAINT
	PK_T_Waybill PRIMARY KEY CLUSTERED 
	(
	Waybill_Guid
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.T_Waybill ADD CONSTRAINT
	FK_T_Waybill_T_Address FOREIGN KEY
	(
	Address_Guid
	) REFERENCES dbo.T_Address
	(
	Address_Guid
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.T_Waybill ADD CONSTRAINT
	FK_T_Waybill_T_Company FOREIGN KEY
	(
	Company_Guid
	) REFERENCES dbo.T_Company
	(
	Company_Guid
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.T_Waybill ADD CONSTRAINT
	FK_T_Waybill_T_Currency FOREIGN KEY
	(
	Currency_Guid
	) REFERENCES dbo.T_Currency
	(
	Currency_Guid
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.T_Waybill ADD CONSTRAINT
	FK_T_Waybill_T_Customer FOREIGN KEY
	(
	Customer_Guid
	) REFERENCES dbo.T_Customer
	(
	Customer_Guid
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.T_Waybill ADD CONSTRAINT
	FK_T_Waybill_T_Depart FOREIGN KEY
	(
	Depart_Guid
	) REFERENCES dbo.T_Depart
	(
	Depart_Guid
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.T_Waybill ADD CONSTRAINT
	FK_T_Waybill_T_WaybillShipMode FOREIGN KEY
	(
	WaybillShipMode_Guid
	) REFERENCES dbo.T_WaybillShipMode
	(
	WaybillShipMode_Guid
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.T_Waybill ADD CONSTRAINT
	FK_T_Waybill_T_WaybillState FOREIGN KEY
	(
	WaybillState_Guid
	) REFERENCES dbo.T_WaybillState
	(
	WaybillState_Guid
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.T_Waybill ADD CONSTRAINT
	FK_T_Waybill_T_Rtt FOREIGN KEY
	(
	Rtt_Guid
	) REFERENCES dbo.T_Rtt
	(
	Rtt_Guid
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.T_Waybill ADD CONSTRAINT
	FK_T_Waybill_T_Stock FOREIGN KEY
	(
	Stock_Guid
	) REFERENCES dbo.T_Stock
	(
	Stock_Guid
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.T_Waybill ADD CONSTRAINT
	FK_T_Waybill_T_Suppl FOREIGN KEY
	(
	Suppl_Guid
	) REFERENCES dbo.T_Suppl
	(
	Suppl_Guid
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.T_Waybill ADD CONSTRAINT
	FK_T_Waybill_T_PaymentType FOREIGN KEY
	(
	PaymentType_Guid
	) REFERENCES dbo.T_PaymentType
	(
	PaymentType_Guid
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.T_Waybill ADD CONSTRAINT
	FK_T_Waybill_T_CustomerChild FOREIGN KEY
	(
	CustomerChild_Guid
	) REFERENCES dbo.T_CustomerChild
	(
	CustomerChild_Guid
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.T_WaybItem ADD CONSTRAINT
	FK_T_WaybItem_T_Waybill FOREIGN KEY
	(
	Waybill_Guid
	) REFERENCES dbo.T_Waybill
	(
	Waybill_Guid
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.T_WaybItem SET (LOCK_ESCALATION = TABLE)
GO
COMMIT

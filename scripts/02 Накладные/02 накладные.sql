USE [ERP_Mercury]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_WaybItem](
	[WaybItem_Guid] [dbo].[D_GUID] NOT NULL,
	[SupplItem_Guid] [dbo].[D_GUID]  NULL,
	[Waybill_Guid] [dbo].[D_GUID] NOT NULL,
	[Parts_Guid] [dbo].[D_GUID] NOT NULL,
	[Measure_Guid] [dbo].[D_GUID] NOT NULL,
	[WaybItem_Quantity] [dbo].[D_INTEGER] NOT NULL,
	[WaybItem_RetQuantity] [dbo].[D_INTEGER] NOT NULL,
	[WaybItem_LeavQuantity]  AS ([WaybItem_Quantity] - [WaybItem_RetQuantity]),
	
	[WaybItem_Price] [dbo].[D_MONEY] NOT NULL,
	[WaybItem_Discount] [dbo].[D_MONEY] NOT NULL,
	[WaybItem_DiscountPrice] [dbo].[D_MONEY] NOT NULL,
	[WaybItem_AllPrice]  AS ([WaybItem_Price]*[WaybItem_Quantity]),
	[WaybItem_TotalPrice]  AS ([WaybItem_DiscountPrice]*[WaybItem_Quantity]),
	[WaybItem_LeavTotalPrice]  AS ([WaybItem_DiscountPrice]*([WaybItem_Quantity] - [WaybItem_RetQuantity])),

	[WaybItem_CurrencyPrice] [dbo].[D_MONEY] NOT NULL,
	[WaybItem_CurrencyDiscountPrice] [dbo].[D_MONEY] NOT NULL,
	[WaybItem_CurrencyAllPrice]  AS ([WaybItem_CurrencyPrice]*[WaybItem_Quantity]),
	[WaybItem_CurrencyTotalPrice]  AS ([WaybItem_CurrencyDiscountPrice]*[WaybItem_Quantity]),
	[WaybItem_CurrencyleavTotalPrice]  AS ([WaybItem_CurrencyDiscountPrice]*([WaybItem_Quantity] - [WaybItem_RetQuantity])),

	[WaybItem_XMLPrice] [xml] NULL,
	[WaybItem_XMLDiscount] [xml] NULL,

	[WaybItem_NDSPercent] [dbo].[D_MONEY] NULL,
	[WaybItem_PriceImporter] [dbo].[D_MONEY] NULL,
 CONSTRAINT [PK_T_WaybItem] PRIMARY KEY CLUSTERED 
(
	[WaybItem_Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[T_WaybItem] ADD  CONSTRAINT [DF_T_WaybItem_WaybItem_Quantity]  DEFAULT ((0)) FOR [WaybItem_Quantity]
GO

ALTER TABLE [dbo].[T_WaybItem] ADD  CONSTRAINT [DF_T_WaybItem_WaybItem_RetQuantity]  DEFAULT ((0)) FOR [WaybItem_RetQuantity]
GO

ALTER TABLE [dbo].[T_WaybItem] ADD  CONSTRAINT [DF_T_WaybItem_WaybItem_Price]  DEFAULT ((0)) FOR [WaybItem_Price]
GO

ALTER TABLE [dbo].[T_WaybItem] ADD  CONSTRAINT [DF_T_WaybItem_WaybItem_Discount]  DEFAULT ((0)) FOR [WaybItem_Discount]
GO

ALTER TABLE [dbo].[T_WaybItem] ADD  CONSTRAINT [DF_T_WaybItem_WaybItem_DiscountPrice]  DEFAULT ((0)) FOR [WaybItem_DiscountPrice]
GO

ALTER TABLE [dbo].[T_WaybItem] ADD  CONSTRAINT [DF_T_WaybItem_WaybItem_CurrencyPrice]  DEFAULT ((0)) FOR [WaybItem_CurrencyPrice]
GO

ALTER TABLE [dbo].[T_WaybItem] ADD  CONSTRAINT [DF_T_WaybItem_WaybItem_CurrencyDiscountPrice]  DEFAULT ((0)) FOR [WaybItem_CurrencyDiscountPrice]
GO

ALTER TABLE [dbo].[T_WaybItem] ADD  CONSTRAINT [DF_T_WaybItem_WaybItem_NDSPercent]  DEFAULT ((0)) FOR [WaybItem_NDSPercent]
GO

ALTER TABLE [dbo].[T_WaybItem] ADD  CONSTRAINT [DF_T_WaybItem_WaybItem_PriceImporter]  DEFAULT ((0)) FOR [WaybItem_PriceImporter]
GO

ALTER TABLE [dbo].[T_WaybItem]  WITH CHECK ADD  CONSTRAINT [FK_T_WaybItem_T_Measure] FOREIGN KEY([Measure_Guid])
REFERENCES [dbo].[T_Measure] ([Measure_Guid])
GO

ALTER TABLE [dbo].[T_WaybItem] CHECK CONSTRAINT [FK_T_WaybItem_T_Measure]
GO

ALTER TABLE [dbo].[T_WaybItem]  WITH CHECK ADD  CONSTRAINT [FK_T_WaybItem_T_Parts] FOREIGN KEY([Parts_Guid])
REFERENCES [dbo].[T_Parts] ([Parts_Guid])
GO

ALTER TABLE [dbo].[T_WaybItem] CHECK CONSTRAINT [FK_T_WaybItem_T_Parts]
GO

ALTER TABLE [dbo].[T_WaybItem]  WITH CHECK ADD  CONSTRAINT [FK_T_WaybItem_T_Waybill] FOREIGN KEY([Waybill_Guid])
REFERENCES [dbo].[T_Waybill] ([Waybill_Guid])
GO

ALTER TABLE [dbo].[T_WaybItem] CHECK CONSTRAINT [FK_T_WaybItem_T_Waybill]
GO

ALTER TABLE [dbo].[T_WaybItem]  WITH CHECK ADD  CONSTRAINT [FK_T_WaybItem_T_SupplItem] FOREIGN KEY([SupplItem_Guid])
REFERENCES [dbo].[T_SupplItem] ([SupplItem_Guid])
GO

ALTER TABLE [dbo].[T_WaybItem] CHECK CONSTRAINT [FK_T_WaybItem_T_SupplItem]
GO


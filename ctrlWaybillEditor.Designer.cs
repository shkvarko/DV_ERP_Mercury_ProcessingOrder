namespace ERPMercuryProcessingOrder
{
    partial class ctrlWaybillEditor
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition2 = new DevExpress.XtraGrid.StyleFormatCondition();
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition3 = new DevExpress.XtraGrid.StyleFormatCondition();
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition4 = new DevExpress.XtraGrid.StyleFormatCondition();
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition5 = new DevExpress.XtraGrid.StyleFormatCondition();
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition6 = new DevExpress.XtraGrid.StyleFormatCondition();
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition7 = new DevExpress.XtraGrid.StyleFormatCondition();
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition8 = new DevExpress.XtraGrid.StyleFormatCondition();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrlWaybillEditor));
            this.colQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCalcEditOrderedQuantity = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.colPriceImporter = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuantityReturned = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPriceWithDiscount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPriceInAccountingCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPriceWithDiscountInAccountingCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNDSPercent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.toolTipController = new DevExpress.Utils.ToolTipController(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mitemExport = new System.Windows.Forms.ToolStripMenuItem();
            this.mitmsExportToHTML = new System.Windows.Forms.ToolStripMenuItem();
            this.mitmsExportToXML = new System.Windows.Forms.ToolStripMenuItem();
            this.mitmsExportToXLS = new System.Windows.Forms.ToolStripMenuItem();
            this.mitmsExportToTXT = new System.Windows.Forms.ToolStripMenuItem();
            this.mitmsExportToDBF = new System.Windows.Forms.ToolStripMenuItem();
            this.mitmsExportToDBFCurrency = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mitemImport = new System.Windows.Forms.ToolStripMenuItem();
            this.mitmsImportFromExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mitemDeleteSelectedRows = new System.Windows.Forms.ToolStripMenuItem();
            this.mitemClearRows = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanelBackground = new System.Windows.Forms.TableLayoutPanel();
            this.panelProgressBar = new DevExpress.XtraEditors.PanelControl();
            this.progressBarControl1 = new DevExpress.XtraEditors.ProgressBarControl();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.dataSet = new System.Data.DataSet();
            this.OrderItems = new System.Data.DataTable();
            this.OrderItemsID = new System.Data.DataColumn();
            this.ProductID = new System.Data.DataColumn();
            this.MeasureID = new System.Data.DataColumn();
            this.Quantity = new System.Data.DataColumn();
            this.PriceImporter = new System.Data.DataColumn();
            this.Price = new System.Data.DataColumn();
            this.DiscountPercent = new System.Data.DataColumn();
            this.PriceWithDiscount = new System.Data.DataColumn();
            this.NDSPercent = new System.Data.DataColumn();
            this.PriceInAccountingCurrency = new System.Data.DataColumn();
            this.PriceWithDiscountInAccountingCurrency = new System.Data.DataColumn();
            this.QuantityReturned = new System.Data.DataColumn();
            this.Sum = new System.Data.DataColumn();
            this.SumWithDiscount = new System.Data.DataColumn();
            this.SumInAccountingCurrency = new System.Data.DataColumn();
            this.SumWithDiscountInAccountingCurrency = new System.Data.DataColumn();
            this.OrderItems_MeasureName = new System.Data.DataColumn();
            this.OrderPackQty = new System.Data.DataColumn();
            this.OrderItems_PartsArticle = new System.Data.DataColumn();
            this.OrderItems_PartsName = new System.Data.DataColumn();
            this.OrderItems_QuantityInstock = new System.Data.DataColumn();
            this.WaybItem_Id = new System.Data.DataColumn();
            this.SupplItem_Guid = new System.Data.DataColumn();
            this.QuantityWithReturn = new System.Data.DataColumn();
            this.Product = new System.Data.DataTable();
            this.ProductGuid = new System.Data.DataColumn();
            this.ProductFullName = new System.Data.DataColumn();
            this.CustomerOrderStockQty = new System.Data.DataColumn();
            this.CustomerOrderResQty = new System.Data.DataColumn();
            this.CustomerOrderPackQty = new System.Data.DataColumn();
            this.Product_MeasureID = new System.Data.DataColumn();
            this.Product_MeasureName = new System.Data.DataColumn();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colOrderItemsID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEditProduct = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colMeasureID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOrderItems_MeasureName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDiscountPercent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSpinEditDiscount = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colSum = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSumWithDiscount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSumInAccountingCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSumWithDiscountInAccountingCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOrderPackQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOrderItems_PartsName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOrderItems_PartsArticle = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOrderItems_QuantityInstock = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.checkSetOrderInQueue = new DevExpress.XtraEditors.CheckEdit();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.Rtt = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.AddressDelivery = new DevExpress.XtraEditors.ComboBoxEdit();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.Stock = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.Depart = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.SalesMan = new DevExpress.XtraEditors.ComboBoxEdit();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.IsBonus = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.Customer = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.ChildDepart = new DevExpress.XtraEditors.ComboBoxEdit();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.txtDescription = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.DeliveryDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.BeginDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl21 = new DevExpress.XtraEditors.LabelControl();
            this.ShipDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl22 = new DevExpress.XtraEditors.LabelControl();
            this.WaybillShipMode = new DevExpress.XtraEditors.ComboBoxEdit();
            this.checkEditForStock = new DevExpress.XtraEditors.CheckEdit();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.WaybillState = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.PaymentType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl29 = new DevExpress.XtraEditors.LabelControl();
            this.WaybilllNum = new DevExpress.XtraEditors.TextEdit();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.cboxProductTradeMark = new DevExpress.XtraEditors.ComboBoxEdit();
            this.pictureFilter = new DevExpress.XtraEditors.PictureEdit();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.cboxProductType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.checkMultiplicity = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.controlNavigator = new DevExpress.XtraEditors.ControlNavigator();
            this.spinEditDiscount = new DevExpress.XtraEditors.SpinEdit();
            this.btnSetDiscount = new DevExpress.XtraEditors.SimpleButton();
            this.checkEditCalcPrices = new DevExpress.XtraEditors.CheckEdit();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.grboxResultCreditControl = new DevExpress.XtraEditors.GroupControl();
            this.ImageNot = new System.Windows.Forms.PictureBox();
            this.ImageOk = new System.Windows.Forms.PictureBox();
            this.LCreditControlResult = new DevExpress.XtraEditors.LabelControl();
            this.LOutDays = new DevExpress.XtraEditors.LabelControl();
            this.LOutSumma = new DevExpress.XtraEditors.LabelControl();
            this.labelControl23 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl24 = new DevExpress.XtraEditors.LabelControl();
            this.grboxCreditLimit = new DevExpress.XtraEditors.GroupControl();
            this.LEarning = new DevExpress.XtraEditors.LabelControl();
            this.LOverdraftMoney = new DevExpress.XtraEditors.LabelControl();
            this.LCustomerLimitDays = new DevExpress.XtraEditors.LabelControl();
            this.LCustomerLimitMoney = new DevExpress.XtraEditors.LabelControl();
            this.labelControl25 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl26 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl27 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl28 = new DevExpress.XtraEditors.LabelControl();
            this.grboxSaldo = new DevExpress.XtraEditors.GroupControl();
            this.LDebtSuppl = new DevExpress.XtraEditors.LabelControl();
            this.LDebtWaybill = new DevExpress.XtraEditors.LabelControl();
            this.lblDayDebtWaybillShipped = new DevExpress.XtraEditors.LabelControl();
            this.lblDebtWaybillShipped = new DevExpress.XtraEditors.LabelControl();
            this.labelControl20 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl19 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl18 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEditOrderedQuantity)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.tableLayoutPanelBackground.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelProgressBar)).BeginInit();
            this.panelProgressBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrderItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Product)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEditProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditDiscount)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkSetOrderInQueue.Properties)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Rtt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddressDelivery.Properties)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Stock.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Depart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesMan.Properties)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IsBonus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Customer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChildDepart.Properties)).BeginInit();
            this.tableLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            this.tableLayoutPanel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DeliveryDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeliveryDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BeginDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BeginDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShipDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShipDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaybillShipMode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditForStock.Properties)).BeginInit();
            this.tableLayoutPanel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WaybillState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PaymentType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaybilllNum.Properties)).BeginInit();
            this.tableLayoutPanel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboxProductTradeMark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxProductType.Properties)).BeginInit();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkMultiplicity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditDiscount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditCalcPrices.Properties)).BeginInit();
            this.tableLayoutPanel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grboxResultCreditControl)).BeginInit();
            this.grboxResultCreditControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImageNot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageOk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grboxCreditLimit)).BeginInit();
            this.grboxCreditLimit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grboxSaldo)).BeginInit();
            this.grboxSaldo.SuspendLayout();
            this.SuspendLayout();
            // 
            // colQuantity
            // 
            this.colQuantity.AppearanceCell.Options.UseTextOptions = true;
            this.colQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colQuantity.AppearanceHeader.Options.UseTextOptions = true;
            this.colQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colQuantity.Caption = "Кол-во, шт.";
            this.colQuantity.ColumnEdit = this.repositoryItemCalcEditOrderedQuantity;
            this.colQuantity.FieldName = "Quantity";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.SummaryItem.DisplayFormat = "{0:### ### ##0}";
            this.colQuantity.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colQuantity.Visible = true;
            this.colQuantity.VisibleIndex = 2;
            this.colQuantity.Width = 56;
            // 
            // repositoryItemCalcEditOrderedQuantity
            // 
            this.repositoryItemCalcEditOrderedQuantity.AutoHeight = false;
            this.repositoryItemCalcEditOrderedQuantity.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCalcEditOrderedQuantity.DisplayFormat.FormatString = "### ##0";
            this.repositoryItemCalcEditOrderedQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemCalcEditOrderedQuantity.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemCalcEditOrderedQuantity.Name = "repositoryItemCalcEditOrderedQuantity";
            // 
            // colPriceImporter
            // 
            this.colPriceImporter.AppearanceCell.Options.UseTextOptions = true;
            this.colPriceImporter.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colPriceImporter.AppearanceHeader.Options.UseTextOptions = true;
            this.colPriceImporter.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPriceImporter.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colPriceImporter.Caption = "Цена импортера, руб.";
            this.colPriceImporter.DisplayFormat.FormatString = "# ### ### ##0";
            this.colPriceImporter.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPriceImporter.FieldName = "PriceImporter";
            this.colPriceImporter.MinWidth = 100;
            this.colPriceImporter.Name = "colPriceImporter";
            this.colPriceImporter.OptionsColumn.AllowEdit = false;
            this.colPriceImporter.OptionsColumn.ReadOnly = true;
            this.colPriceImporter.Visible = true;
            this.colPriceImporter.VisibleIndex = 4;
            this.colPriceImporter.Width = 100;
            // 
            // colQuantityReturned
            // 
            this.colQuantityReturned.AppearanceCell.Options.UseTextOptions = true;
            this.colQuantityReturned.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colQuantityReturned.AppearanceHeader.Options.UseTextOptions = true;
            this.colQuantityReturned.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colQuantityReturned.Caption = "Возврат, шт.";
            this.colQuantityReturned.ColumnEdit = this.repositoryItemCalcEditOrderedQuantity;
            this.colQuantityReturned.FieldName = "QuantityReturned";
            this.colQuantityReturned.Name = "colQuantityReturned";
            this.colQuantityReturned.OptionsColumn.AllowEdit = false;
            this.colQuantityReturned.OptionsColumn.ReadOnly = true;
            this.colQuantityReturned.SummaryItem.DisplayFormat = "{0:### ### ##0}";
            this.colQuantityReturned.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colQuantityReturned.Visible = true;
            this.colQuantityReturned.VisibleIndex = 3;
            this.colQuantityReturned.Width = 59;
            // 
            // colPrice
            // 
            this.colPrice.AppearanceCell.Options.UseTextOptions = true;
            this.colPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.colPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPrice.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colPrice.Caption = "Цена, руб.";
            this.colPrice.DisplayFormat.FormatString = "# ### ### ##0";
            this.colPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPrice.FieldName = "Price";
            this.colPrice.MinWidth = 100;
            this.colPrice.Name = "colPrice";
            this.colPrice.OptionsColumn.AllowEdit = false;
            this.colPrice.OptionsColumn.ReadOnly = true;
            this.colPrice.Visible = true;
            this.colPrice.VisibleIndex = 5;
            this.colPrice.Width = 103;
            // 
            // colPriceWithDiscount
            // 
            this.colPriceWithDiscount.AppearanceCell.Options.UseTextOptions = true;
            this.colPriceWithDiscount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colPriceWithDiscount.AppearanceHeader.Options.UseTextOptions = true;
            this.colPriceWithDiscount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPriceWithDiscount.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colPriceWithDiscount.Caption = "Цена со скидкой, руб.";
            this.colPriceWithDiscount.DisplayFormat.FormatString = "# ### ### ##0";
            this.colPriceWithDiscount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPriceWithDiscount.FieldName = "PriceWithDiscount";
            this.colPriceWithDiscount.MinWidth = 100;
            this.colPriceWithDiscount.Name = "colPriceWithDiscount";
            this.colPriceWithDiscount.OptionsColumn.AllowEdit = false;
            this.colPriceWithDiscount.OptionsColumn.ReadOnly = true;
            this.colPriceWithDiscount.Visible = true;
            this.colPriceWithDiscount.VisibleIndex = 7;
            this.colPriceWithDiscount.Width = 120;
            // 
            // colPriceInAccountingCurrency
            // 
            this.colPriceInAccountingCurrency.AppearanceCell.Options.UseTextOptions = true;
            this.colPriceInAccountingCurrency.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colPriceInAccountingCurrency.AppearanceHeader.Options.UseTextOptions = true;
            this.colPriceInAccountingCurrency.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPriceInAccountingCurrency.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colPriceInAccountingCurrency.Caption = "Цена в валюте учета";
            this.colPriceInAccountingCurrency.DisplayFormat.FormatString = "# ### ### ##0.000";
            this.colPriceInAccountingCurrency.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPriceInAccountingCurrency.FieldName = "PriceInAccountingCurrency";
            this.colPriceInAccountingCurrency.MinWidth = 50;
            this.colPriceInAccountingCurrency.Name = "colPriceInAccountingCurrency";
            this.colPriceInAccountingCurrency.OptionsColumn.AllowEdit = false;
            this.colPriceInAccountingCurrency.OptionsColumn.ReadOnly = true;
            this.colPriceInAccountingCurrency.Visible = true;
            this.colPriceInAccountingCurrency.VisibleIndex = 10;
            this.colPriceInAccountingCurrency.Width = 68;
            // 
            // colPriceWithDiscountInAccountingCurrency
            // 
            this.colPriceWithDiscountInAccountingCurrency.AppearanceCell.Options.UseTextOptions = true;
            this.colPriceWithDiscountInAccountingCurrency.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colPriceWithDiscountInAccountingCurrency.AppearanceHeader.Options.UseTextOptions = true;
            this.colPriceWithDiscountInAccountingCurrency.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPriceWithDiscountInAccountingCurrency.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colPriceWithDiscountInAccountingCurrency.Caption = "Цена со скидкой в валюте";
            this.colPriceWithDiscountInAccountingCurrency.DisplayFormat.FormatString = "# ### ### ##0.000";
            this.colPriceWithDiscountInAccountingCurrency.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPriceWithDiscountInAccountingCurrency.FieldName = "PriceWithDiscountInAccountingCurrency";
            this.colPriceWithDiscountInAccountingCurrency.MinWidth = 50;
            this.colPriceWithDiscountInAccountingCurrency.Name = "colPriceWithDiscountInAccountingCurrency";
            this.colPriceWithDiscountInAccountingCurrency.OptionsColumn.AllowEdit = false;
            this.colPriceWithDiscountInAccountingCurrency.OptionsColumn.ReadOnly = true;
            this.colPriceWithDiscountInAccountingCurrency.Visible = true;
            this.colPriceWithDiscountInAccountingCurrency.VisibleIndex = 11;
            this.colPriceWithDiscountInAccountingCurrency.Width = 69;
            // 
            // colNDSPercent
            // 
            this.colNDSPercent.AppearanceHeader.Options.UseTextOptions = true;
            this.colNDSPercent.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNDSPercent.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNDSPercent.Caption = "НДС, %";
            this.colNDSPercent.DisplayFormat.FormatString = "# ### ### ##0";
            this.colNDSPercent.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNDSPercent.FieldName = "NDSPercent";
            this.colNDSPercent.Name = "colNDSPercent";
            this.colNDSPercent.OptionsColumn.AllowEdit = false;
            this.colNDSPercent.OptionsColumn.ReadOnly = true;
            this.colNDSPercent.Visible = true;
            this.colNDSPercent.VisibleIndex = 14;
            this.colNDSPercent.Width = 51;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mitemExport,
            this.toolStripSeparator1,
            this.mitemImport,
            this.toolStripSeparator2,
            this.mitemDeleteSelectedRows,
            this.mitemClearRows});
            this.contextMenuStrip.Name = "contextMenuStripLicence";
            this.contextMenuStrip.Size = new System.Drawing.Size(329, 104);
            this.toolTipController.SetSuperTip(this.contextMenuStrip, null);
            // 
            // mitemExport
            // 
            this.mitemExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mitmsExportToHTML,
            this.mitmsExportToXML,
            this.mitmsExportToXLS,
            this.mitmsExportToTXT,
            this.mitmsExportToDBF,
            this.mitmsExportToDBFCurrency});
            this.mitemExport.Name = "mitemExport";
            this.mitemExport.Size = new System.Drawing.Size(328, 22);
            this.mitemExport.Text = "Экспорт";
            // 
            // mitmsExportToHTML
            // 
            this.mitmsExportToHTML.Name = "mitmsExportToHTML";
            this.mitmsExportToHTML.Size = new System.Drawing.Size(233, 22);
            this.mitmsExportToHTML.Text = "в HTML...";
            // 
            // mitmsExportToXML
            // 
            this.mitmsExportToXML.Name = "mitmsExportToXML";
            this.mitmsExportToXML.Size = new System.Drawing.Size(233, 22);
            this.mitmsExportToXML.Text = "в XML...";
            // 
            // mitmsExportToXLS
            // 
            this.mitmsExportToXLS.Name = "mitmsExportToXLS";
            this.mitmsExportToXLS.Size = new System.Drawing.Size(233, 22);
            this.mitmsExportToXLS.Text = "в MS Excel...";
            this.mitmsExportToXLS.Click += new System.EventHandler(this.mitmsExportToXLS_Click);
            // 
            // mitmsExportToTXT
            // 
            this.mitmsExportToTXT.Name = "mitmsExportToTXT";
            this.mitmsExportToTXT.Size = new System.Drawing.Size(233, 22);
            this.mitmsExportToTXT.Text = "в TXT...";
            // 
            // mitmsExportToDBF
            // 
            this.mitmsExportToDBF.Name = "mitmsExportToDBF";
            this.mitmsExportToDBF.Size = new System.Drawing.Size(233, 22);
            this.mitmsExportToDBF.Text = "в DBF (цена в рублях)...";
            // 
            // mitmsExportToDBFCurrency
            // 
            this.mitmsExportToDBFCurrency.Name = "mitmsExportToDBFCurrency";
            this.mitmsExportToDBFCurrency.Size = new System.Drawing.Size(233, 22);
            this.mitmsExportToDBFCurrency.Text = "в DBF (цена в валюте учета)...";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(325, 6);
            // 
            // mitemImport
            // 
            this.mitemImport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mitmsImportFromExcel});
            this.mitemImport.Name = "mitemImport";
            this.mitemImport.Size = new System.Drawing.Size(328, 22);
            this.mitemImport.Text = "Импорт";
            // 
            // mitmsImportFromExcel
            // 
            this.mitmsImportFromExcel.Name = "mitmsImportFromExcel";
            this.mitmsImportFromExcel.Size = new System.Drawing.Size(130, 22);
            this.mitmsImportFromExcel.Text = "из MS Excel";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(325, 6);
            // 
            // mitemDeleteSelectedRows
            // 
            this.mitemDeleteSelectedRows.Name = "mitemDeleteSelectedRows";
            this.mitemDeleteSelectedRows.Size = new System.Drawing.Size(328, 22);
            this.mitemDeleteSelectedRows.Text = "Удалить выделенные записи...";
            this.mitemDeleteSelectedRows.Click += new System.EventHandler(this.mitemDeleteSelectedRows_Click);
            // 
            // mitemClearRows
            // 
            this.mitemClearRows.Name = "mitemClearRows";
            this.mitemClearRows.Size = new System.Drawing.Size(328, 22);
            this.mitemClearRows.Text = "Удалить все записи в приложении к накладной...";
            this.mitemClearRows.Click += new System.EventHandler(this.mitemClearRows_Click);
            // 
            // tableLayoutPanelBackground
            // 
            this.tableLayoutPanelBackground.ColumnCount = 1;
            this.tableLayoutPanelBackground.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelBackground.Controls.Add(this.panelProgressBar, 0, 2);
            this.tableLayoutPanelBackground.Controls.Add(this.gridControl, 0, 5);
            this.tableLayoutPanelBackground.Controls.Add(this.tableLayoutPanel1, 0, 6);
            this.tableLayoutPanelBackground.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanelBackground.Controls.Add(this.tableLayoutPanel8, 0, 3);
            this.tableLayoutPanelBackground.Controls.Add(this.tableLayoutPanel6, 0, 4);
            this.tableLayoutPanelBackground.Controls.Add(this.tableLayoutPanel10, 0, 1);
            this.tableLayoutPanelBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelBackground.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelBackground.Name = "tableLayoutPanelBackground";
            this.tableLayoutPanelBackground.RowCount = 7;
            this.tableLayoutPanelBackground.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 177F));
            this.tableLayoutPanelBackground.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 117F));
            this.tableLayoutPanelBackground.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanelBackground.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanelBackground.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanelBackground.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelBackground.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanelBackground.Size = new System.Drawing.Size(1078, 602);
            this.toolTipController.SetSuperTip(this.tableLayoutPanelBackground, null);
            this.tableLayoutPanelBackground.TabIndex = 1;
            // 
            // panelProgressBar
            // 
            this.panelProgressBar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelProgressBar.Controls.Add(this.progressBarControl1);
            this.panelProgressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelProgressBar.Location = new System.Drawing.Point(3, 297);
            this.panelProgressBar.Name = "panelProgressBar";
            this.panelProgressBar.Size = new System.Drawing.Size(1072, 28);
            this.toolTipController.SetSuperTip(this.panelProgressBar, null);
            this.panelProgressBar.TabIndex = 25;
            this.panelProgressBar.Visible = false;
            // 
            // progressBarControl1
            // 
            this.progressBarControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarControl1.Location = new System.Drawing.Point(4, 5);
            this.progressBarControl1.Name = "progressBarControl1";
            this.progressBarControl1.Size = new System.Drawing.Size(1064, 16);
            this.progressBarControl1.TabIndex = 0;
            // 
            // gridControl
            // 
            this.gridControl.ContextMenuStrip = this.contextMenuStrip;
            this.gridControl.DataMember = "OrderItems";
            this.gridControl.DataSource = this.dataSet;
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.EmbeddedNavigator.Name = "";
            this.gridControl.Location = new System.Drawing.Point(3, 383);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEditProduct,
            this.repositoryItemCalcEditOrderedQuantity,
            this.repositoryItemSpinEditDiscount});
            this.gridControl.Size = new System.Drawing.Size(1072, 185);
            this.gridControl.TabIndex = 23;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // dataSet
            // 
            this.dataSet.DataSetName = "dataSet";
            this.dataSet.Relations.AddRange(new System.Data.DataRelation[] {
            new System.Data.DataRelation("rlOrderItmsProduct", "Product", "OrderItems", new string[] {
                        "ProductID"}, new string[] {
                        "ProductID"}, false)});
            this.dataSet.Tables.AddRange(new System.Data.DataTable[] {
            this.OrderItems,
            this.Product});
            // 
            // OrderItems
            // 
            this.OrderItems.Columns.AddRange(new System.Data.DataColumn[] {
            this.OrderItemsID,
            this.ProductID,
            this.MeasureID,
            this.Quantity,
            this.PriceImporter,
            this.Price,
            this.DiscountPercent,
            this.PriceWithDiscount,
            this.NDSPercent,
            this.PriceInAccountingCurrency,
            this.PriceWithDiscountInAccountingCurrency,
            this.QuantityReturned,
            this.Sum,
            this.SumWithDiscount,
            this.SumInAccountingCurrency,
            this.SumWithDiscountInAccountingCurrency,
            this.OrderItems_MeasureName,
            this.OrderPackQty,
            this.OrderItems_PartsArticle,
            this.OrderItems_PartsName,
            this.OrderItems_QuantityInstock,
            this.WaybItem_Id,
            this.SupplItem_Guid,
            this.QuantityWithReturn});
            this.OrderItems.TableName = "OrderItems";
            // 
            // OrderItemsID
            // 
            this.OrderItemsID.Caption = "ID";
            this.OrderItemsID.ColumnName = "OrderItemsID";
            this.OrderItemsID.DataType = typeof(System.Guid);
            // 
            // ProductID
            // 
            this.ProductID.Caption = "Товар";
            this.ProductID.ColumnName = "ProductID";
            this.ProductID.DataType = typeof(System.Guid);
            // 
            // MeasureID
            // 
            this.MeasureID.Caption = "MeasureID";
            this.MeasureID.ColumnName = "MeasureID";
            this.MeasureID.DataType = typeof(System.Guid);
            // 
            // Quantity
            // 
            this.Quantity.Caption = "Кол-во, шт.";
            this.Quantity.ColumnName = "Quantity";
            this.Quantity.DataType = typeof(decimal);
            // 
            // PriceImporter
            // 
            this.PriceImporter.Caption = "Цена первого поставщика";
            this.PriceImporter.ColumnName = "PriceImporter";
            this.PriceImporter.DataType = typeof(double);
            // 
            // Price
            // 
            this.Price.Caption = "Цена отпускная";
            this.Price.ColumnName = "Price";
            this.Price.DataType = typeof(double);
            // 
            // DiscountPercent
            // 
            this.DiscountPercent.Caption = "Размер скидки, %";
            this.DiscountPercent.ColumnName = "DiscountPercent";
            this.DiscountPercent.DataType = typeof(double);
            // 
            // PriceWithDiscount
            // 
            this.PriceWithDiscount.Caption = "Цена отпускная с учетом скидки";
            this.PriceWithDiscount.ColumnName = "PriceWithDiscount";
            this.PriceWithDiscount.DataType = typeof(double);
            // 
            // NDSPercent
            // 
            this.NDSPercent.Caption = "НДС, %";
            this.NDSPercent.ColumnName = "NDSPercent";
            this.NDSPercent.DataType = typeof(double);
            // 
            // PriceInAccountingCurrency
            // 
            this.PriceInAccountingCurrency.Caption = "Отпускная цена в валюте учета";
            this.PriceInAccountingCurrency.ColumnName = "PriceInAccountingCurrency";
            this.PriceInAccountingCurrency.DataType = typeof(double);
            // 
            // PriceWithDiscountInAccountingCurrency
            // 
            this.PriceWithDiscountInAccountingCurrency.Caption = "Отпускная цена с учетом скидки в валюте учета";
            this.PriceWithDiscountInAccountingCurrency.ColumnName = "PriceWithDiscountInAccountingCurrency";
            this.PriceWithDiscountInAccountingCurrency.DataType = typeof(double);
            // 
            // QuantityReturned
            // 
            this.QuantityReturned.Caption = "Возврат, шт.";
            this.QuantityReturned.ColumnName = "QuantityReturned";
            this.QuantityReturned.DataType = typeof(decimal);
            // 
            // Sum
            // 
            this.Sum.Caption = "Сумма";
            this.Sum.ColumnName = "Sum";
            this.Sum.DataType = typeof(double);
            this.Sum.Expression = "Price * Quantity";
            this.Sum.ReadOnly = true;
            // 
            // SumWithDiscount
            // 
            this.SumWithDiscount.Caption = "Сумма с учетом скидки";
            this.SumWithDiscount.ColumnName = "SumWithDiscount";
            this.SumWithDiscount.DataType = typeof(double);
            this.SumWithDiscount.Expression = "PriceWithDiscount * Quantity";
            this.SumWithDiscount.ReadOnly = true;
            // 
            // SumInAccountingCurrency
            // 
            this.SumInAccountingCurrency.Caption = "Сумма в валюте учета";
            this.SumInAccountingCurrency.ColumnName = "SumInAccountingCurrency";
            this.SumInAccountingCurrency.DataType = typeof(double);
            this.SumInAccountingCurrency.Expression = "PriceInAccountingCurrency * Quantity";
            this.SumInAccountingCurrency.ReadOnly = true;
            // 
            // SumWithDiscountInAccountingCurrency
            // 
            this.SumWithDiscountInAccountingCurrency.Caption = "Сумма в валюте учета со скидкой";
            this.SumWithDiscountInAccountingCurrency.ColumnName = "SumWithDiscountInAccountingCurrency";
            this.SumWithDiscountInAccountingCurrency.DataType = typeof(double);
            this.SumWithDiscountInAccountingCurrency.Expression = "PriceWithDiscountInAccountingCurrency * Quantity";
            this.SumWithDiscountInAccountingCurrency.ReadOnly = true;
            // 
            // OrderItems_MeasureName
            // 
            this.OrderItems_MeasureName.Caption = "Ед. изм.";
            this.OrderItems_MeasureName.ColumnName = "OrderItems_MeasureName";
            // 
            // OrderPackQty
            // 
            this.OrderPackQty.ColumnName = "OrderPackQty";
            this.OrderPackQty.DataType = typeof(decimal);
            // 
            // OrderItems_PartsArticle
            // 
            this.OrderItems_PartsArticle.ColumnName = "OrderItems_PartsArticle";
            // 
            // OrderItems_PartsName
            // 
            this.OrderItems_PartsName.ColumnName = "OrderItems_PartsName";
            // 
            // OrderItems_QuantityInstock
            // 
            this.OrderItems_QuantityInstock.ColumnName = "OrderItems_QuantityInstock";
            this.OrderItems_QuantityInstock.DataType = typeof(decimal);
            // 
            // WaybItem_Id
            // 
            this.WaybItem_Id.ColumnName = "WaybItem_Id";
            this.WaybItem_Id.DataType = typeof(int);
            // 
            // SupplItem_Guid
            // 
            this.SupplItem_Guid.ColumnName = "SupplItem_Guid";
            this.SupplItem_Guid.DataType = typeof(System.Guid);
            // 
            // QuantityWithReturn
            // 
            this.QuantityWithReturn.ColumnName = "QuantityWithReturn";
            this.QuantityWithReturn.DataType = typeof(decimal);
            this.QuantityWithReturn.Expression = "Quantity - QuantityReturned";
            this.QuantityWithReturn.ReadOnly = true;
            // 
            // Product
            // 
            this.Product.Columns.AddRange(new System.Data.DataColumn[] {
            this.ProductGuid,
            this.ProductFullName,
            this.CustomerOrderStockQty,
            this.CustomerOrderResQty,
            this.CustomerOrderPackQty,
            this.Product_MeasureID,
            this.Product_MeasureName});
            this.Product.TableName = "Product";
            // 
            // ProductGuid
            // 
            this.ProductGuid.Caption = "Товар";
            this.ProductGuid.ColumnName = "ProductID";
            this.ProductGuid.DataType = typeof(System.Guid);
            // 
            // ProductFullName
            // 
            this.ProductFullName.Caption = "Товар";
            this.ProductFullName.ColumnName = "ProductFullName";
            // 
            // CustomerOrderStockQty
            // 
            this.CustomerOrderStockQty.Caption = "Остаток";
            this.CustomerOrderStockQty.ColumnName = "CustomerOrderStockQty";
            this.CustomerOrderStockQty.DataType = typeof(decimal);
            // 
            // CustomerOrderResQty
            // 
            this.CustomerOrderResQty.Caption = "резерв";
            this.CustomerOrderResQty.ColumnName = "CustomerOrderResQty";
            this.CustomerOrderResQty.DataType = typeof(decimal);
            // 
            // CustomerOrderPackQty
            // 
            this.CustomerOrderPackQty.Caption = "Количество в упаковке";
            this.CustomerOrderPackQty.ColumnName = "CustomerOrderPackQty";
            this.CustomerOrderPackQty.DataType = typeof(decimal);
            // 
            // Product_MeasureID
            // 
            this.Product_MeasureID.Caption = "MeasureID";
            this.Product_MeasureID.ColumnName = "Product_MeasureID";
            this.Product_MeasureID.DataType = typeof(System.Guid);
            // 
            // Product_MeasureName
            // 
            this.Product_MeasureName.Caption = "Ед. изм.";
            this.Product_MeasureName.ColumnName = "Product_MeasureName";
            // 
            // gridView
            // 
            this.gridView.ColumnPanelRowHeight = 60;
            this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colOrderItemsID,
            this.colProductID,
            this.colMeasureID,
            this.colOrderItems_MeasureName,
            this.colQuantity,
            this.colQuantityReturned,
            this.colPriceImporter,
            this.colPrice,
            this.colDiscountPercent,
            this.colPriceWithDiscount,
            this.colSum,
            this.colSumWithDiscount,
            this.colPriceInAccountingCurrency,
            this.colPriceWithDiscountInAccountingCurrency,
            this.colSumInAccountingCurrency,
            this.colSumWithDiscountInAccountingCurrency,
            this.colNDSPercent,
            this.colOrderPackQty,
            this.colOrderItems_PartsName,
            this.colOrderItems_PartsArticle,
            this.colOrderItems_QuantityInstock});
            this.gridView.CustomizationFormBounds = new System.Drawing.Rectangle(1062, 711, 208, 189);
            styleFormatCondition1.Appearance.BackColor = System.Drawing.Color.Red;
            styleFormatCondition1.Appearance.Options.UseBackColor = true;
            styleFormatCondition1.Column = this.colQuantity;
            styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.LessOrEqual;
            styleFormatCondition1.Value1 = 0D;
            styleFormatCondition2.Appearance.BackColor = System.Drawing.Color.Red;
            styleFormatCondition2.Appearance.Options.UseBackColor = true;
            styleFormatCondition2.Column = this.colPriceImporter;
            styleFormatCondition2.Condition = DevExpress.XtraGrid.FormatConditionEnum.LessOrEqual;
            styleFormatCondition2.Value1 = "0";
            styleFormatCondition3.Appearance.BackColor = System.Drawing.Color.Red;
            styleFormatCondition3.Appearance.Options.UseBackColor = true;
            styleFormatCondition3.Column = this.colQuantityReturned;
            styleFormatCondition3.Condition = DevExpress.XtraGrid.FormatConditionEnum.LessOrEqual;
            styleFormatCondition3.Value1 = "0";
            styleFormatCondition4.Appearance.BackColor = System.Drawing.Color.Red;
            styleFormatCondition4.Appearance.Options.UseBackColor = true;
            styleFormatCondition4.Column = this.colPrice;
            styleFormatCondition4.Condition = DevExpress.XtraGrid.FormatConditionEnum.LessOrEqual;
            styleFormatCondition4.Value1 = "0";
            styleFormatCondition5.Appearance.BackColor = System.Drawing.Color.Red;
            styleFormatCondition5.Appearance.Options.UseBackColor = true;
            styleFormatCondition5.Column = this.colPriceWithDiscount;
            styleFormatCondition5.Condition = DevExpress.XtraGrid.FormatConditionEnum.LessOrEqual;
            styleFormatCondition5.Value1 = "0";
            styleFormatCondition6.Appearance.BackColor = System.Drawing.Color.Red;
            styleFormatCondition6.Appearance.Options.UseBackColor = true;
            styleFormatCondition6.Column = this.colPriceInAccountingCurrency;
            styleFormatCondition6.Condition = DevExpress.XtraGrid.FormatConditionEnum.LessOrEqual;
            styleFormatCondition6.Value1 = "0";
            styleFormatCondition7.Appearance.BackColor = System.Drawing.Color.Red;
            styleFormatCondition7.Appearance.Options.UseBackColor = true;
            styleFormatCondition7.Column = this.colPriceWithDiscountInAccountingCurrency;
            styleFormatCondition7.Condition = DevExpress.XtraGrid.FormatConditionEnum.LessOrEqual;
            styleFormatCondition7.Value1 = "0";
            styleFormatCondition8.Appearance.BackColor = System.Drawing.Color.Red;
            styleFormatCondition8.Appearance.Options.UseBackColor = true;
            styleFormatCondition8.Column = this.colNDSPercent;
            styleFormatCondition8.Condition = DevExpress.XtraGrid.FormatConditionEnum.Less;
            styleFormatCondition8.Value1 = "0";
            this.gridView.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1,
            styleFormatCondition2,
            styleFormatCondition3,
            styleFormatCondition4,
            styleFormatCondition5,
            styleFormatCondition6,
            styleFormatCondition7,
            styleFormatCondition8});
            this.gridView.GridControl = this.gridControl;
            this.gridView.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "OrderID", null, "")});
            this.gridView.IndicatorWidth = 50;
            this.gridView.Name = "gridView";
            this.gridView.OptionsSelection.MultiSelect = true;
            this.gridView.OptionsView.ColumnAutoWidth = false;
            this.gridView.OptionsView.ShowFooter = true;
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView_CustomDrawRowIndicator);
            this.gridView.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridView_CustomDrawCell);
            this.gridView.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gridView_InitNewRow);
            this.gridView.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView_CellValueChanged);
            this.gridView.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView_CellValueChanging);
            this.gridView.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.gridView_InvalidRowException);
            this.gridView.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gridView_ValidateRow);
            this.gridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridView_KeyDown);
            // 
            // colOrderItemsID
            // 
            this.colOrderItemsID.AppearanceHeader.Options.UseTextOptions = true;
            this.colOrderItemsID.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colOrderItemsID.Caption = "OrderItemsID";
            this.colOrderItemsID.FieldName = "OrderItemsID";
            this.colOrderItemsID.Name = "colOrderItemsID";
            // 
            // colProductID
            // 
            this.colProductID.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductID.Caption = "Наименование товара";
            this.colProductID.ColumnEdit = this.repositoryItemLookUpEditProduct;
            this.colProductID.FieldName = "ProductID";
            this.colProductID.Name = "colProductID";
            this.colProductID.Visible = true;
            this.colProductID.VisibleIndex = 0;
            this.colProductID.Width = 295;
            // 
            // repositoryItemLookUpEditProduct
            // 
            this.repositoryItemLookUpEditProduct.Appearance.Options.UseBackColor = true;
            this.repositoryItemLookUpEditProduct.AutoHeight = false;
            this.repositoryItemLookUpEditProduct.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEditProduct.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ProductFullName", "Наименование товара", 127),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CustomerOrderStockQty", "Остаток", 30, DevExpress.Utils.FormatType.Numeric, "### ### ##0", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CustomerOrderResQty", "в Резерве", 30, DevExpress.Utils.FormatType.Numeric, "### ### ##0", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None)});
            this.repositoryItemLookUpEditProduct.DisplayMember = "ProductFullName";
            this.repositoryItemLookUpEditProduct.DropDownRows = 10;
            this.repositoryItemLookUpEditProduct.Name = "repositoryItemLookUpEditProduct";
            this.repositoryItemLookUpEditProduct.NullText = "";
            this.repositoryItemLookUpEditProduct.PopupFormMinSize = new System.Drawing.Size(500, 0);
            this.repositoryItemLookUpEditProduct.PopupWidth = 500;
            this.repositoryItemLookUpEditProduct.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.repositoryItemLookUpEditProduct.ValidateOnEnterKey = true;
            this.repositoryItemLookUpEditProduct.ValueMember = "ProductID";
            // 
            // colMeasureID
            // 
            this.colMeasureID.Caption = "MeasureID";
            this.colMeasureID.FieldName = "MeasureID";
            this.colMeasureID.Name = "colMeasureID";
            // 
            // colOrderItems_MeasureName
            // 
            this.colOrderItems_MeasureName.AppearanceHeader.Options.UseTextOptions = true;
            this.colOrderItems_MeasureName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colOrderItems_MeasureName.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colOrderItems_MeasureName.Caption = "Ед изм.";
            this.colOrderItems_MeasureName.FieldName = "OrderItems_MeasureName";
            this.colOrderItems_MeasureName.Name = "colOrderItems_MeasureName";
            this.colOrderItems_MeasureName.OptionsColumn.AllowEdit = false;
            this.colOrderItems_MeasureName.OptionsColumn.ReadOnly = true;
            this.colOrderItems_MeasureName.Visible = true;
            this.colOrderItems_MeasureName.VisibleIndex = 1;
            this.colOrderItems_MeasureName.Width = 48;
            // 
            // colDiscountPercent
            // 
            this.colDiscountPercent.AppearanceHeader.Options.UseTextOptions = true;
            this.colDiscountPercent.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDiscountPercent.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDiscountPercent.Caption = "Скидка, %";
            this.colDiscountPercent.ColumnEdit = this.repositoryItemSpinEditDiscount;
            this.colDiscountPercent.DisplayFormat.FormatString = "##0";
            this.colDiscountPercent.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDiscountPercent.FieldName = "DiscountPercent";
            this.colDiscountPercent.Name = "colDiscountPercent";
            this.colDiscountPercent.Visible = true;
            this.colDiscountPercent.VisibleIndex = 6;
            this.colDiscountPercent.Width = 62;
            // 
            // repositoryItemSpinEditDiscount
            // 
            this.repositoryItemSpinEditDiscount.AutoHeight = false;
            this.repositoryItemSpinEditDiscount.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEditDiscount.DisplayFormat.FormatString = "# ### ### ##0";
            this.repositoryItemSpinEditDiscount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemSpinEditDiscount.EditFormat.FormatString = "# ### ### ##0";
            this.repositoryItemSpinEditDiscount.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemSpinEditDiscount.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.repositoryItemSpinEditDiscount.Name = "repositoryItemSpinEditDiscount";
            // 
            // colSum
            // 
            this.colSum.AppearanceHeader.Options.UseTextOptions = true;
            this.colSum.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSum.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colSum.Caption = "Сумма, руб.";
            this.colSum.DisplayFormat.FormatString = "# ### ### ##0";
            this.colSum.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSum.FieldName = "Sum";
            this.colSum.Name = "colSum";
            this.colSum.OptionsColumn.AllowEdit = false;
            this.colSum.OptionsColumn.ReadOnly = true;
            this.colSum.SummaryItem.DisplayFormat = "{0:### ### ##0}";
            this.colSum.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colSum.Visible = true;
            this.colSum.VisibleIndex = 8;
            // 
            // colSumWithDiscount
            // 
            this.colSumWithDiscount.AppearanceHeader.Options.UseTextOptions = true;
            this.colSumWithDiscount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSumWithDiscount.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colSumWithDiscount.Caption = "С учетом скидки, руб.";
            this.colSumWithDiscount.DisplayFormat.FormatString = "# ### ### ##0";
            this.colSumWithDiscount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSumWithDiscount.FieldName = "SumWithDiscount";
            this.colSumWithDiscount.Name = "colSumWithDiscount";
            this.colSumWithDiscount.OptionsColumn.AllowEdit = false;
            this.colSumWithDiscount.OptionsColumn.ReadOnly = true;
            this.colSumWithDiscount.SummaryItem.DisplayFormat = "{0:### ### ##0}";
            this.colSumWithDiscount.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colSumWithDiscount.Visible = true;
            this.colSumWithDiscount.VisibleIndex = 9;
            // 
            // colSumInAccountingCurrency
            // 
            this.colSumInAccountingCurrency.AppearanceCell.Options.UseTextOptions = true;
            this.colSumInAccountingCurrency.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colSumInAccountingCurrency.AppearanceHeader.Options.UseTextOptions = true;
            this.colSumInAccountingCurrency.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSumInAccountingCurrency.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colSumInAccountingCurrency.Caption = "Сумма в валюте учета";
            this.colSumInAccountingCurrency.DisplayFormat.FormatString = "# ### ### ##0.000";
            this.colSumInAccountingCurrency.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSumInAccountingCurrency.FieldName = "SumInAccountingCurrency";
            this.colSumInAccountingCurrency.MinWidth = 50;
            this.colSumInAccountingCurrency.Name = "colSumInAccountingCurrency";
            this.colSumInAccountingCurrency.OptionsColumn.AllowEdit = false;
            this.colSumInAccountingCurrency.OptionsColumn.ReadOnly = true;
            this.colSumInAccountingCurrency.SummaryItem.DisplayFormat = "{0:### ### ##0.00}";
            this.colSumInAccountingCurrency.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colSumInAccountingCurrency.Visible = true;
            this.colSumInAccountingCurrency.VisibleIndex = 12;
            this.colSumInAccountingCurrency.Width = 66;
            // 
            // colSumWithDiscountInAccountingCurrency
            // 
            this.colSumWithDiscountInAccountingCurrency.AppearanceCell.Options.UseTextOptions = true;
            this.colSumWithDiscountInAccountingCurrency.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colSumWithDiscountInAccountingCurrency.AppearanceHeader.Options.UseTextOptions = true;
            this.colSumWithDiscountInAccountingCurrency.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSumWithDiscountInAccountingCurrency.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colSumWithDiscountInAccountingCurrency.Caption = "Сумма со скидкой в валюте учета";
            this.colSumWithDiscountInAccountingCurrency.DisplayFormat.FormatString = "# ### ### ##0.000";
            this.colSumWithDiscountInAccountingCurrency.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSumWithDiscountInAccountingCurrency.FieldName = "SumWithDiscountInAccountingCurrency";
            this.colSumWithDiscountInAccountingCurrency.MinWidth = 50;
            this.colSumWithDiscountInAccountingCurrency.Name = "colSumWithDiscountInAccountingCurrency";
            this.colSumWithDiscountInAccountingCurrency.OptionsColumn.AllowEdit = false;
            this.colSumWithDiscountInAccountingCurrency.OptionsColumn.ReadOnly = true;
            this.colSumWithDiscountInAccountingCurrency.SummaryItem.DisplayFormat = "{0:### ### ##0.00}";
            this.colSumWithDiscountInAccountingCurrency.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colSumWithDiscountInAccountingCurrency.Visible = true;
            this.colSumWithDiscountInAccountingCurrency.VisibleIndex = 13;
            this.colSumWithDiscountInAccountingCurrency.Width = 71;
            // 
            // colOrderPackQty
            // 
            this.colOrderPackQty.Caption = "OrderPackQty";
            this.colOrderPackQty.FieldName = "OrderPackQty";
            this.colOrderPackQty.Name = "colOrderPackQty";
            // 
            // colOrderItems_PartsName
            // 
            this.colOrderItems_PartsName.Caption = "colOrderItems_PartsName";
            this.colOrderItems_PartsName.FieldName = "OrderItems_PartsName";
            this.colOrderItems_PartsName.Name = "colOrderItems_PartsName";
            // 
            // colOrderItems_PartsArticle
            // 
            this.colOrderItems_PartsArticle.Caption = "colOrderItems_PartsArticle";
            this.colOrderItems_PartsArticle.FieldName = "OrderItems_PartsArticle";
            this.colOrderItems_PartsArticle.Name = "colOrderItems_PartsArticle";
            // 
            // colOrderItems_QuantityInstock
            // 
            this.colOrderItems_QuantityInstock.Caption = "colOrderItems_QuantityInstock";
            this.colOrderItems_QuantityInstock.FieldName = "OrderItems_QuantityInstock";
            this.colOrderItems_QuantityInstock.Name = "colOrderItems_QuantityInstock";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 121F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 156F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel1.Controls.Add(this.btnCancel, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnEdit, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnPrint, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSave, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.checkSetOrderInQueue, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 571);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1078, 31);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel1, null);
            this.tableLayoutPanel1.TabIndex = 16;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(1001, 4);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Выход";
            this.btnCancel.ToolTip = "Закрыть редактор заказа";
            this.btnCancel.ToolTipController = this.toolTipController;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEdit.Image = global::ERPMercuryProcessingOrder.Properties.Resources.document_edit;
            this.btnEdit.Location = new System.Drawing.Point(84, 4);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(2);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(112, 25);
            this.btnEdit.TabIndex = 0;
            this.btnEdit.Text = "Редактировать";
            this.btnEdit.ToolTipController = this.toolTipController;
            this.btnEdit.Visible = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.Location = new System.Drawing.Point(2, 4);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 25);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Text = "Печать";
            this.btnPrint.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.btnPrint.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(920, 4);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "ОК";
            this.btnSave.ToolTip = "Сохранить изменения";
            this.btnSave.ToolTipController = this.toolTipController;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // checkSetOrderInQueue
            // 
            this.checkSetOrderInQueue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkSetOrderInQueue.Location = new System.Drawing.Point(758, 6);
            this.checkSetOrderInQueue.Name = "checkSetOrderInQueue";
            this.checkSetOrderInQueue.Properties.Caption = "в очередь на обработку";
            this.checkSetOrderInQueue.Size = new System.Drawing.Size(150, 19);
            this.checkSetOrderInQueue.TabIndex = 4;
            this.checkSetOrderInQueue.ToolTip = "заказ будет помещён в очередь для последующей автоматической обработки и формиров" +
    "ания резерва";
            this.checkSetOrderInQueue.ToolTipController = this.toolTipController;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel5, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel7, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel9, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel11, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1078, 177);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel2, null);
            this.tableLayoutPanel2.TabIndex = 17;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 4;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 97F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Controls.Add(this.Rtt, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.labelControl5, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.labelControl6, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.AddressDelivery, 3, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 75);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1078, 25);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel5, null);
            this.tableLayoutPanel5.TabIndex = 2;
            // 
            // Rtt
            // 
            this.Rtt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Rtt.Location = new System.Drawing.Point(103, 3);
            this.Rtt.Name = "Rtt";
            this.Rtt.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Rtt.Properties.PopupSizeable = true;
            this.Rtt.Properties.Sorted = true;
            this.Rtt.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.Rtt.Size = new System.Drawing.Size(434, 20);
            this.Rtt.TabIndex = 28;
            this.Rtt.ToolTip = "Розничная точка клиента";
            this.Rtt.ToolTipController = this.toolTipController;
            this.Rtt.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.Rtt.SelectedValueChanged += new System.EventHandler(this.cboxOrderPropertie_SelectedValueChanged);
            // 
            // labelControl5
            // 
            this.labelControl5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl5.Location = new System.Drawing.Point(3, 6);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(90, 13);
            this.labelControl5.TabIndex = 22;
            this.labelControl5.Text = "Розничная точка:";
            // 
            // labelControl6
            // 
            this.labelControl6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl6.Location = new System.Drawing.Point(543, 6);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(86, 13);
            this.labelControl6.TabIndex = 24;
            this.labelControl6.Text = "Адрес доставки:";
            // 
            // AddressDelivery
            // 
            this.AddressDelivery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.AddressDelivery.Location = new System.Drawing.Point(640, 3);
            this.AddressDelivery.Name = "AddressDelivery";
            this.AddressDelivery.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.AddressDelivery.Properties.PopupSizeable = true;
            this.AddressDelivery.Properties.Sorted = true;
            this.AddressDelivery.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.AddressDelivery.Size = new System.Drawing.Size(435, 20);
            this.AddressDelivery.TabIndex = 29;
            this.AddressDelivery.ToolTip = "Адрес доставки заказа";
            this.AddressDelivery.ToolTipController = this.toolTipController;
            this.AddressDelivery.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.AddressDelivery.SelectedValueChanged += new System.EventHandler(this.cboxOrderPropertie_SelectedValueChanged);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 7;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 138F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 182F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.Stock, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.labelControl9, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.labelControl8, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.Depart, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.labelControl11, 4, 0);
            this.tableLayoutPanel4.Controls.Add(this.SalesMan, 5, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 100);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1078, 25);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel4, null);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // Stock
            // 
            this.Stock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Stock.Location = new System.Drawing.Point(103, 3);
            this.Stock.Name = "Stock";
            this.Stock.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Stock.Properties.PopupSizeable = true;
            this.Stock.Properties.Sorted = true;
            this.Stock.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.Stock.Size = new System.Drawing.Size(194, 20);
            this.Stock.TabIndex = 1;
            this.Stock.ToolTip = "Склад отгрузки";
            this.Stock.ToolTipController = this.toolTipController;
            this.Stock.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.Stock.SelectedValueChanged += new System.EventHandler(this.cboxOrderPropertie_SelectedValueChanged);
            // 
            // labelControl9
            // 
            this.labelControl9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl9.Location = new System.Drawing.Point(303, 6);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(84, 13);
            this.labelControl9.TabIndex = 24;
            this.labelControl9.Text = "Подразделение:";
            // 
            // labelControl8
            // 
            this.labelControl8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl8.Location = new System.Drawing.Point(3, 6);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(85, 13);
            this.labelControl8.TabIndex = 23;
            this.labelControl8.Text = "Склад отгрузки:";
            // 
            // Depart
            // 
            this.Depart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Depart.Location = new System.Drawing.Point(403, 3);
            this.Depart.Name = "Depart";
            this.Depart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Depart.Properties.PopupSizeable = true;
            this.Depart.Properties.Sorted = true;
            this.Depart.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.Depart.Size = new System.Drawing.Size(94, 20);
            this.Depart.TabIndex = 2;
            this.Depart.ToolTip = "Подразделение";
            this.Depart.ToolTipController = this.toolTipController;
            this.Depart.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.Depart.SelectedValueChanged += new System.EventHandler(this.cboxOrderPropertie_SelectedValueChanged);
            // 
            // labelControl11
            // 
            this.labelControl11.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl11.Location = new System.Drawing.Point(503, 6);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(134, 13);
            this.labelControl11.TabIndex = 25;
            this.labelControl11.Text = "Торговый представитель:";
            // 
            // SalesMan
            // 
            this.SalesMan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.SalesMan.Location = new System.Drawing.Point(641, 3);
            this.SalesMan.Name = "SalesMan";
            this.SalesMan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.SalesMan.Properties.PopupSizeable = true;
            this.SalesMan.Properties.Sorted = true;
            this.SalesMan.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.SalesMan.Size = new System.Drawing.Size(176, 20);
            this.SalesMan.TabIndex = 3;
            this.SalesMan.ToolTip = "Торговый представитель";
            this.SalesMan.ToolTipController = this.toolTipController;
            this.SalesMan.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 6;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 101F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 152F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel3.Controls.Add(this.IsBonus, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.labelControl3, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.Customer, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.labelControl1, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.ChildDepart, 3, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 50);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1078, 25);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel3, null);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // IsBonus
            // 
            this.IsBonus.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.IsBonus.Location = new System.Drawing.Point(849, 3);
            this.IsBonus.Name = "IsBonus";
            this.IsBonus.Properties.Caption = "Бонус";
            this.IsBonus.Size = new System.Drawing.Size(75, 19);
            this.IsBonus.TabIndex = 6;
            this.IsBonus.ToolTip = "Заказ является бонусом для клиента";
            this.IsBonus.ToolTipController = this.toolTipController;
            this.IsBonus.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl3.Location = new System.Drawing.Point(3, 6);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(41, 13);
            this.labelControl3.TabIndex = 26;
            this.labelControl3.Text = "Клиент:";
            // 
            // Customer
            // 
            this.Customer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Customer.Location = new System.Drawing.Point(103, 3);
            this.Customer.Name = "Customer";
            this.Customer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Customer.Properties.PopupSizeable = true;
            this.Customer.Properties.Sorted = true;
            this.Customer.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.Customer.Size = new System.Drawing.Size(543, 20);
            this.Customer.TabIndex = 27;
            this.Customer.ToolTip = "Клиент";
            this.Customer.ToolTipController = this.toolTipController;
            this.Customer.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.Customer.SelectedValueChanged += new System.EventHandler(this.cboxOrderPropertie_SelectedValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl1.Location = new System.Drawing.Point(652, 6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(93, 13);
            this.labelControl1.TabIndex = 28;
            this.labelControl1.Text = "Дочерний клиент:";
            // 
            // ChildDepart
            // 
            this.ChildDepart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ChildDepart.Location = new System.Drawing.Point(748, 3);
            this.ChildDepart.Name = "ChildDepart";
            this.ChildDepart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ChildDepart.Properties.PopupSizeable = true;
            this.ChildDepart.Properties.Sorted = true;
            this.ChildDepart.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.ChildDepart.Size = new System.Drawing.Size(95, 20);
            this.ChildDepart.TabIndex = 29;
            this.ChildDepart.ToolTip = "Дочернее подразделение клиента";
            this.ChildDepart.ToolTipController = this.toolTipController;
            this.ChildDepart.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.ChildDepart.SelectedValueChanged += new System.EventHandler(this.cboxOrderPropertie_SelectedValueChanged);
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Controls.Add(this.txtDescription, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.labelControl12, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(0, 125);
            this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(1078, 52);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel7, null);
            this.tableLayoutPanel7.TabIndex = 4;
            // 
            // txtDescription
            // 
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescription.Location = new System.Drawing.Point(103, 3);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(972, 46);
            this.txtDescription.TabIndex = 26;
            this.txtDescription.ToolTip = "Примечание";
            this.txtDescription.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.txtDescription.EditValueChanged += new System.EventHandler(this.cboxOrderPropertie_SelectedValueChanged);
            this.txtDescription.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.txtOrderPropertie_EditValueChanging);
            // 
            // labelControl12
            // 
            this.labelControl12.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl12.Location = new System.Drawing.Point(3, 19);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(65, 13);
            this.labelControl12.TabIndex = 25;
            this.labelControl12.Text = "Примечание:";
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 9;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 101F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 114F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 171F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Controls.Add(this.DeliveryDate, 3, 0);
            this.tableLayoutPanel9.Controls.Add(this.labelControl10, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this.labelControl7, 2, 0);
            this.tableLayoutPanel9.Controls.Add(this.BeginDate, 1, 0);
            this.tableLayoutPanel9.Controls.Add(this.labelControl21, 4, 0);
            this.tableLayoutPanel9.Controls.Add(this.ShipDate, 5, 0);
            this.tableLayoutPanel9.Controls.Add(this.labelControl22, 6, 0);
            this.tableLayoutPanel9.Controls.Add(this.WaybillShipMode, 7, 0);
            this.tableLayoutPanel9.Controls.Add(this.checkEditForStock, 8, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel9.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 1;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(1078, 25);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel9, null);
            this.tableLayoutPanel9.TabIndex = 5;
            // 
            // DeliveryDate
            // 
            this.DeliveryDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.DeliveryDate.EditValue = null;
            this.DeliveryDate.Location = new System.Drawing.Point(289, 3);
            this.DeliveryDate.Name = "DeliveryDate";
            this.DeliveryDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DeliveryDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.DeliveryDate.Size = new System.Drawing.Size(94, 20);
            this.DeliveryDate.TabIndex = 1;
            this.DeliveryDate.ToolTip = "Дата доставки";
            this.DeliveryDate.ToolTipController = this.toolTipController;
            this.DeliveryDate.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.DeliveryDate.EditValueChanged += new System.EventHandler(this.cboxOrderPropertie_SelectedValueChanged);
            this.DeliveryDate.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.repositoryItemCalcEditOrderedQuantity_EditValueChanging);
            // 
            // labelControl10
            // 
            this.labelControl10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl10.Location = new System.Drawing.Point(3, 6);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(88, 13);
            this.labelControl10.TabIndex = 20;
            this.labelControl10.Text = "Дата накладной:";
            // 
            // labelControl7
            // 
            this.labelControl7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl7.Location = new System.Drawing.Point(204, 6);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(81, 13);
            this.labelControl7.TabIndex = 26;
            this.labelControl7.Text = "Дата доставки:";
            // 
            // BeginDate
            // 
            this.BeginDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.BeginDate.EditValue = null;
            this.BeginDate.Location = new System.Drawing.Point(103, 3);
            this.BeginDate.Name = "BeginDate";
            this.BeginDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.BeginDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.BeginDate.Size = new System.Drawing.Size(95, 20);
            this.BeginDate.TabIndex = 21;
            this.BeginDate.ToolTip = "Дата оформления";
            this.BeginDate.ToolTipController = this.toolTipController;
            this.BeginDate.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.BeginDate.EditValueChanged += new System.EventHandler(this.cboxOrderPropertie_SelectedValueChanged);
            this.BeginDate.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.txtOrderPropertie_EditValueChanging);
            // 
            // labelControl21
            // 
            this.labelControl21.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl21.Location = new System.Drawing.Point(389, 6);
            this.labelControl21.Name = "labelControl21";
            this.labelControl21.Size = new System.Drawing.Size(79, 13);
            this.labelControl21.TabIndex = 27;
            this.labelControl21.Text = "Дата отгрузки:";
            // 
            // ShipDate
            // 
            this.ShipDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ShipDate.EditValue = null;
            this.ShipDate.Location = new System.Drawing.Point(471, 3);
            this.ShipDate.Name = "ShipDate";
            this.ShipDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ShipDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.ShipDate.Size = new System.Drawing.Size(108, 20);
            this.ShipDate.TabIndex = 28;
            this.ShipDate.ToolTip = "Дата отгрузки";
            this.ShipDate.ToolTipController = this.toolTipController;
            this.ShipDate.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.ShipDate.EditValueChanged += new System.EventHandler(this.cboxOrderPropertie_SelectedValueChanged);
            this.ShipDate.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.repositoryItemCalcEditOrderedQuantity_EditValueChanging);
            // 
            // labelControl22
            // 
            this.labelControl22.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl22.Location = new System.Drawing.Point(585, 6);
            this.labelControl22.Name = "labelControl22";
            this.labelControl22.Size = new System.Drawing.Size(72, 13);
            this.labelControl22.TabIndex = 29;
            this.labelControl22.Text = "Вид отгрузки:";
            // 
            // WaybillShipMode
            // 
            this.WaybillShipMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.WaybillShipMode.Location = new System.Drawing.Point(664, 3);
            this.WaybillShipMode.Name = "WaybillShipMode";
            this.WaybillShipMode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.WaybillShipMode.Properties.PopupSizeable = true;
            this.WaybillShipMode.Properties.Sorted = true;
            this.WaybillShipMode.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.WaybillShipMode.Size = new System.Drawing.Size(165, 20);
            this.WaybillShipMode.TabIndex = 30;
            this.WaybillShipMode.ToolTip = "Вид отгрузки";
            this.WaybillShipMode.ToolTipController = this.toolTipController;
            this.WaybillShipMode.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.WaybillShipMode.SelectedValueChanged += new System.EventHandler(this.cboxOrderPropertie_SelectedValueChanged);
            // 
            // checkEditForStock
            // 
            this.checkEditForStock.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkEditForStock.Location = new System.Drawing.Point(835, 3);
            this.checkEditForStock.Name = "checkEditForStock";
            this.checkEditForStock.Properties.Caption = "Уведомить склад";
            this.checkEditForStock.Size = new System.Drawing.Size(155, 19);
            this.checkEditForStock.TabIndex = 33;
            this.checkEditForStock.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.ColumnCount = 7;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 186F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 196F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 171F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.Controls.Add(this.labelControl2, 0, 0);
            this.tableLayoutPanel11.Controls.Add(this.WaybillState, 1, 0);
            this.tableLayoutPanel11.Controls.Add(this.labelControl4, 2, 0);
            this.tableLayoutPanel11.Controls.Add(this.PaymentType, 3, 0);
            this.tableLayoutPanel11.Controls.Add(this.labelControl29, 4, 0);
            this.tableLayoutPanel11.Controls.Add(this.WaybilllNum, 5, 0);
            this.tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel11.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanel11.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 1;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(1078, 25);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel11, null);
            this.tableLayoutPanel11.TabIndex = 6;
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl2.Location = new System.Drawing.Point(3, 6);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(58, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Состояние:";
            // 
            // WaybillState
            // 
            this.WaybillState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.WaybillState.Location = new System.Drawing.Point(103, 3);
            this.WaybillState.Name = "WaybillState";
            this.WaybillState.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.WaybillState.Properties.PopupSizeable = true;
            this.WaybillState.Properties.Sorted = true;
            this.WaybillState.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.WaybillState.Size = new System.Drawing.Size(180, 20);
            this.WaybillState.TabIndex = 3;
            this.WaybillState.ToolTip = "Состояние накладной";
            this.WaybillState.ToolTipController = this.toolTipController;
            this.WaybillState.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.WaybillState.SelectedValueChanged += new System.EventHandler(this.cboxOrderPropertie_SelectedValueChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl4.Location = new System.Drawing.Point(289, 6);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(77, 13);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "Форма оплаты:";
            // 
            // PaymentType
            // 
            this.PaymentType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.PaymentType.Location = new System.Drawing.Point(389, 3);
            this.PaymentType.Name = "PaymentType";
            this.PaymentType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.PaymentType.Properties.PopupSizeable = true;
            this.PaymentType.Properties.Sorted = true;
            this.PaymentType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.PaymentType.Size = new System.Drawing.Size(190, 20);
            this.PaymentType.TabIndex = 5;
            this.PaymentType.ToolTip = "Форма оплаты заказа";
            this.PaymentType.ToolTipController = this.toolTipController;
            this.PaymentType.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.PaymentType.SelectedValueChanged += new System.EventHandler(this.cboxOrderPropertie_SelectedValueChanged);
            // 
            // labelControl29
            // 
            this.labelControl29.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl29.Location = new System.Drawing.Point(585, 6);
            this.labelControl29.Name = "labelControl29";
            this.labelControl29.Size = new System.Drawing.Size(73, 13);
            this.labelControl29.TabIndex = 30;
            this.labelControl29.Text = "Номер док-та:";
            // 
            // WaybilllNum
            // 
            this.WaybilllNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.WaybilllNum.Location = new System.Drawing.Point(664, 3);
            this.WaybilllNum.Name = "WaybilllNum";
            this.WaybilllNum.Properties.Appearance.Options.UseTextOptions = true;
            this.WaybilllNum.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.WaybilllNum.Size = new System.Drawing.Size(165, 20);
            this.WaybilllNum.TabIndex = 31;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 7;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel8.Controls.Add(this.labelControl13, 1, 0);
            this.tableLayoutPanel8.Controls.Add(this.cboxProductTradeMark, 2, 0);
            this.tableLayoutPanel8.Controls.Add(this.pictureFilter, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.labelControl14, 2, 0);
            this.tableLayoutPanel8.Controls.Add(this.labelControl15, 4, 0);
            this.tableLayoutPanel8.Controls.Add(this.cboxProductType, 5, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(0, 328);
            this.tableLayoutPanel8.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(1078, 28);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel8, null);
            this.tableLayoutPanel8.TabIndex = 19;
            // 
            // labelControl13
            // 
            this.labelControl13.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl13.Location = new System.Drawing.Point(31, 7);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(148, 13);
            this.labelControl13.TabIndex = 28;
            this.labelControl13.Text = "Фильтрация списка товаров:";
            // 
            // cboxProductTradeMark
            // 
            this.cboxProductTradeMark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboxProductTradeMark.Location = new System.Drawing.Point(311, 4);
            this.cboxProductTradeMark.Name = "cboxProductTradeMark";
            this.cboxProductTradeMark.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboxProductTradeMark.Properties.PopupSizeable = true;
            this.cboxProductTradeMark.Properties.Sorted = true;
            this.cboxProductTradeMark.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboxProductTradeMark.Size = new System.Drawing.Size(318, 20);
            this.cboxProductTradeMark.TabIndex = 29;
            this.cboxProductTradeMark.ToolTip = "Товарная марка";
            this.cboxProductTradeMark.ToolTipController = this.toolTipController;
            this.cboxProductTradeMark.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.cboxProductTradeMark.SelectedValueChanged += new System.EventHandler(this.cboxOrderPropertie_SelectedValueChanged);
            // 
            // pictureFilter
            // 
            this.pictureFilter.EditValue = global::ERPMercuryProcessingOrder.Properties.Resources.filter_data_16;
            this.pictureFilter.Location = new System.Drawing.Point(3, 3);
            this.pictureFilter.Name = "pictureFilter";
            this.pictureFilter.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureFilter.Size = new System.Drawing.Size(22, 20);
            this.pictureFilter.TabIndex = 12;
            // 
            // labelControl14
            // 
            this.labelControl14.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl14.Location = new System.Drawing.Point(211, 7);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(85, 13);
            this.labelControl14.TabIndex = 27;
            this.labelControl14.Text = "Товарная марка:";
            // 
            // labelControl15
            // 
            this.labelControl15.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl15.Location = new System.Drawing.Point(635, 7);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(90, 13);
            this.labelControl15.TabIndex = 28;
            this.labelControl15.Text = "Товарная группа:";
            // 
            // cboxProductType
            // 
            this.cboxProductType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboxProductType.Location = new System.Drawing.Point(735, 4);
            this.cboxProductType.Name = "cboxProductType";
            this.cboxProductType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboxProductType.Properties.PopupSizeable = true;
            this.cboxProductType.Properties.Sorted = true;
            this.cboxProductType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboxProductType.Size = new System.Drawing.Size(318, 20);
            this.cboxProductType.TabIndex = 30;
            this.cboxProductType.ToolTip = "Товарная группа";
            this.cboxProductType.ToolTipController = this.toolTipController;
            this.cboxProductType.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.cboxProductType.SelectedValueChanged += new System.EventHandler(this.cboxOrderPropertie_SelectedValueChanged);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 6;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 316F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 119F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 68F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 73F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.checkMultiplicity, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.labelControl16, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.controlNavigator, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.spinEditDiscount, 2, 0);
            this.tableLayoutPanel6.Controls.Add(this.btnSetDiscount, 3, 0);
            this.tableLayoutPanel6.Controls.Add(this.checkEditCalcPrices, 5, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 356);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(1078, 24);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel6, null);
            this.tableLayoutPanel6.TabIndex = 24;
            // 
            // checkMultiplicity
            // 
            this.checkMultiplicity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.checkMultiplicity.Location = new System.Drawing.Point(319, 3);
            this.checkMultiplicity.Name = "checkMultiplicity";
            this.checkMultiplicity.Properties.Caption = "Кратность кол-ва";
            this.checkMultiplicity.Size = new System.Drawing.Size(113, 19);
            this.checkMultiplicity.TabIndex = 32;
            this.checkMultiplicity.ToolTip = "Количество в заказе должно быть кратно количеству в упаковке";
            this.checkMultiplicity.ToolTipController = this.toolTipController;
            this.checkMultiplicity.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            // 
            // labelControl16
            // 
            this.labelControl16.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelControl16.Location = new System.Drawing.Point(440, 5);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(60, 13);
            this.labelControl16.TabIndex = 28;
            this.labelControl16.Text = "Скидка, %:";
            // 
            // controlNavigator
            // 
            this.controlNavigator.Location = new System.Drawing.Point(3, 3);
            this.controlNavigator.Name = "controlNavigator";
            this.controlNavigator.NavigatableControl = this.gridControl;
            this.controlNavigator.ShowToolTips = true;
            this.controlNavigator.Size = new System.Drawing.Size(280, 19);
            this.controlNavigator.TabIndex = 22;
            this.controlNavigator.Text = "controlNavigator";
            this.controlNavigator.TextLocation = DevExpress.XtraEditors.NavigatorButtonsTextLocation.Center;
            this.controlNavigator.ToolTipController = this.toolTipController;
            // 
            // spinEditDiscount
            // 
            this.spinEditDiscount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.spinEditDiscount.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEditDiscount.Location = new System.Drawing.Point(506, 3);
            this.spinEditDiscount.Name = "spinEditDiscount";
            this.spinEditDiscount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditDiscount.Size = new System.Drawing.Size(67, 20);
            this.spinEditDiscount.TabIndex = 29;
            // 
            // btnSetDiscount
            // 
            this.btnSetDiscount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSetDiscount.Location = new System.Drawing.Point(576, 1);
            this.btnSetDiscount.Margin = new System.Windows.Forms.Padding(0);
            this.btnSetDiscount.Name = "btnSetDiscount";
            this.btnSetDiscount.Size = new System.Drawing.Size(23, 22);
            this.btnSetDiscount.TabIndex = 30;
            this.btnSetDiscount.ToolTip = "Установить скидку на все позиции";
            this.btnSetDiscount.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            // 
            // checkEditCalcPrices
            // 
            this.checkEditCalcPrices.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkEditCalcPrices.Location = new System.Drawing.Point(602, 3);
            this.checkEditCalcPrices.Name = "checkEditCalcPrices";
            this.checkEditCalcPrices.Properties.Caption = "Автоматический расчёт скидок и цен";
            this.checkEditCalcPrices.Size = new System.Drawing.Size(227, 19);
            this.checkEditCalcPrices.TabIndex = 31;
            this.checkEditCalcPrices.ToolTip = "Скидки и цены в заказе будут расчитаны на основании утверждённых правил";
            this.checkEditCalcPrices.ToolTipController = this.toolTipController;
            this.checkEditCalcPrices.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 3;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 365F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 294F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.Controls.Add(this.grboxResultCreditControl, 2, 0);
            this.tableLayoutPanel10.Controls.Add(this.grboxCreditLimit, 1, 0);
            this.tableLayoutPanel10.Controls.Add(this.grboxSaldo, 0, 0);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(3, 180);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 1;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(1072, 111);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel10, null);
            this.tableLayoutPanel10.TabIndex = 26;
            // 
            // grboxResultCreditControl
            // 
            this.grboxResultCreditControl.Controls.Add(this.ImageNot);
            this.grboxResultCreditControl.Controls.Add(this.ImageOk);
            this.grboxResultCreditControl.Controls.Add(this.LCreditControlResult);
            this.grboxResultCreditControl.Controls.Add(this.LOutDays);
            this.grboxResultCreditControl.Controls.Add(this.LOutSumma);
            this.grboxResultCreditControl.Controls.Add(this.labelControl23);
            this.grboxResultCreditControl.Controls.Add(this.labelControl24);
            this.grboxResultCreditControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grboxResultCreditControl.Location = new System.Drawing.Point(662, 3);
            this.grboxResultCreditControl.Name = "grboxResultCreditControl";
            this.grboxResultCreditControl.Size = new System.Drawing.Size(407, 105);
            this.toolTipController.SetSuperTip(this.grboxResultCreditControl, null);
            this.grboxResultCreditControl.TabIndex = 2;
            this.grboxResultCreditControl.Text = "Кредитный контроль итоги";
            // 
            // ImageNot
            // 
            this.ImageNot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ImageNot.Location = new System.Drawing.Point(377, 75);
            this.ImageNot.Name = "ImageNot";
            this.ImageNot.Size = new System.Drawing.Size(25, 25);
            this.toolTipController.SetSuperTip(this.ImageNot, null);
            this.ImageNot.TabIndex = 46;
            this.ImageNot.TabStop = false;
            // 
            // ImageOk
            // 
            this.ImageOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ImageOk.Location = new System.Drawing.Point(348, 75);
            this.ImageOk.Name = "ImageOk";
            this.ImageOk.Size = new System.Drawing.Size(25, 25);
            this.toolTipController.SetSuperTip(this.ImageOk, null);
            this.ImageOk.TabIndex = 45;
            this.ImageOk.TabStop = false;
            // 
            // LCreditControlResult
            // 
            this.LCreditControlResult.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LCreditControlResult.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.LCreditControlResult.Appearance.Options.UseFont = true;
            this.LCreditControlResult.Location = new System.Drawing.Point(5, 87);
            this.LCreditControlResult.Name = "LCreditControlResult";
            this.LCreditControlResult.Size = new System.Drawing.Size(31, 13);
            this.LCreditControlResult.TabIndex = 44;
            this.LCreditControlResult.Text = "Итог:";
            // 
            // LOutDays
            // 
            this.LOutDays.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.LOutDays.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.LOutDays.Appearance.ForeColor = System.Drawing.Color.Red;
            this.LOutDays.Appearance.Options.UseFont = true;
            this.LOutDays.Appearance.Options.UseForeColor = true;
            this.LOutDays.Appearance.Options.UseTextOptions = true;
            this.LOutDays.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.LOutDays.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LOutDays.Location = new System.Drawing.Point(348, 49);
            this.LOutDays.Name = "LOutDays";
            this.LOutDays.Size = new System.Drawing.Size(54, 13);
            this.LOutDays.TabIndex = 43;
            this.LOutDays.Text = "LOutDays";
            // 
            // LOutSumma
            // 
            this.LOutSumma.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.LOutSumma.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.LOutSumma.Appearance.ForeColor = System.Drawing.Color.Red;
            this.LOutSumma.Appearance.Options.UseFont = true;
            this.LOutSumma.Appearance.Options.UseForeColor = true;
            this.LOutSumma.Appearance.Options.UseTextOptions = true;
            this.LOutSumma.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.LOutSumma.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LOutSumma.Location = new System.Drawing.Point(333, 30);
            this.LOutSumma.Name = "LOutSumma";
            this.LOutSumma.Size = new System.Drawing.Size(69, 13);
            this.LOutSumma.TabIndex = 42;
            this.LOutSumma.Text = "LOutSumma";
            // 
            // labelControl23
            // 
            this.labelControl23.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl23.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl23.Appearance.Options.UseFont = true;
            this.labelControl23.Location = new System.Drawing.Point(5, 49);
            this.labelControl23.Name = "labelControl23";
            this.labelControl23.Size = new System.Drawing.Size(157, 13);
            this.labelControl23.TabIndex = 41;
            this.labelControl23.Text = "Превышение лимита, дней:";
            // 
            // labelControl24
            // 
            this.labelControl24.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl24.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl24.Appearance.Options.UseFont = true;
            this.labelControl24.Location = new System.Drawing.Point(5, 30);
            this.labelControl24.Name = "labelControl24";
            this.labelControl24.Size = new System.Drawing.Size(164, 13);
            this.labelControl24.TabIndex = 40;
            this.labelControl24.Text = "Превышение лимита, сумма:";
            // 
            // grboxCreditLimit
            // 
            this.grboxCreditLimit.Appearance.Options.UseTextOptions = true;
            this.grboxCreditLimit.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.grboxCreditLimit.Controls.Add(this.LEarning);
            this.grboxCreditLimit.Controls.Add(this.LOverdraftMoney);
            this.grboxCreditLimit.Controls.Add(this.LCustomerLimitDays);
            this.grboxCreditLimit.Controls.Add(this.LCustomerLimitMoney);
            this.grboxCreditLimit.Controls.Add(this.labelControl25);
            this.grboxCreditLimit.Controls.Add(this.labelControl26);
            this.grboxCreditLimit.Controls.Add(this.labelControl27);
            this.grboxCreditLimit.Controls.Add(this.labelControl28);
            this.grboxCreditLimit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grboxCreditLimit.Location = new System.Drawing.Point(368, 3);
            this.grboxCreditLimit.Name = "grboxCreditLimit";
            this.grboxCreditLimit.Size = new System.Drawing.Size(288, 105);
            this.toolTipController.SetSuperTip(this.grboxCreditLimit, null);
            this.grboxCreditLimit.TabIndex = 1;
            this.grboxCreditLimit.Text = "Кредитный лимит";
            // 
            // LEarning
            // 
            this.LEarning.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.LEarning.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.LEarning.Appearance.ForeColor = System.Drawing.Color.Green;
            this.LEarning.Appearance.Options.UseFont = true;
            this.LEarning.Appearance.Options.UseForeColor = true;
            this.LEarning.Appearance.Options.UseTextOptions = true;
            this.LEarning.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.LEarning.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LEarning.Location = new System.Drawing.Point(235, 87);
            this.LEarning.Name = "LEarning";
            this.LEarning.Size = new System.Drawing.Size(48, 13);
            this.LEarning.TabIndex = 41;
            this.LEarning.Text = "LEarning";
            // 
            // LOverdraftMoney
            // 
            this.LOverdraftMoney.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.LOverdraftMoney.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.LOverdraftMoney.Appearance.ForeColor = System.Drawing.Color.Green;
            this.LOverdraftMoney.Appearance.Options.UseFont = true;
            this.LOverdraftMoney.Appearance.Options.UseForeColor = true;
            this.LOverdraftMoney.Appearance.Options.UseTextOptions = true;
            this.LOverdraftMoney.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.LOverdraftMoney.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LOverdraftMoney.Location = new System.Drawing.Point(184, 68);
            this.LOverdraftMoney.Name = "LOverdraftMoney";
            this.LOverdraftMoney.Size = new System.Drawing.Size(99, 13);
            this.LOverdraftMoney.TabIndex = 40;
            this.LOverdraftMoney.Text = "LOverdraftMoney";
            // 
            // LCustomerLimitDays
            // 
            this.LCustomerLimitDays.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.LCustomerLimitDays.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.LCustomerLimitDays.Appearance.ForeColor = System.Drawing.Color.Green;
            this.LCustomerLimitDays.Appearance.Options.UseFont = true;
            this.LCustomerLimitDays.Appearance.Options.UseForeColor = true;
            this.LCustomerLimitDays.Appearance.Options.UseTextOptions = true;
            this.LCustomerLimitDays.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.LCustomerLimitDays.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LCustomerLimitDays.Location = new System.Drawing.Point(166, 49);
            this.LCustomerLimitDays.Name = "LCustomerLimitDays";
            this.LCustomerLimitDays.Size = new System.Drawing.Size(117, 13);
            this.LCustomerLimitDays.TabIndex = 39;
            this.LCustomerLimitDays.Text = "LCustomerLimitDays";
            // 
            // LCustomerLimitMoney
            // 
            this.LCustomerLimitMoney.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.LCustomerLimitMoney.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.LCustomerLimitMoney.Appearance.ForeColor = System.Drawing.Color.Green;
            this.LCustomerLimitMoney.Appearance.Options.UseFont = true;
            this.LCustomerLimitMoney.Appearance.Options.UseForeColor = true;
            this.LCustomerLimitMoney.Appearance.Options.UseTextOptions = true;
            this.LCustomerLimitMoney.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.LCustomerLimitMoney.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LCustomerLimitMoney.Location = new System.Drawing.Point(156, 30);
            this.LCustomerLimitMoney.Name = "LCustomerLimitMoney";
            this.LCustomerLimitMoney.Size = new System.Drawing.Size(127, 13);
            this.LCustomerLimitMoney.TabIndex = 38;
            this.LCustomerLimitMoney.Text = "LCustomerLimitMoney";
            // 
            // labelControl25
            // 
            this.labelControl25.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl25.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl25.Appearance.Options.UseFont = true;
            this.labelControl25.Location = new System.Drawing.Point(5, 87);
            this.labelControl25.Name = "labelControl25";
            this.labelControl25.Size = new System.Drawing.Size(104, 13);
            this.labelControl25.TabIndex = 37;
            this.labelControl25.Text = "Остаток средств:";
            // 
            // labelControl26
            // 
            this.labelControl26.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl26.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl26.Appearance.Options.UseFont = true;
            this.labelControl26.Location = new System.Drawing.Point(5, 68);
            this.labelControl26.Name = "labelControl26";
            this.labelControl26.Size = new System.Drawing.Size(152, 13);
            this.labelControl26.TabIndex = 36;
            this.labelControl26.Text = "Допустимое превышение:";
            // 
            // labelControl27
            // 
            this.labelControl27.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl27.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl27.Appearance.Options.UseFont = true;
            this.labelControl27.Location = new System.Drawing.Point(5, 49);
            this.labelControl27.Name = "labelControl27";
            this.labelControl27.Size = new System.Drawing.Size(150, 13);
            this.labelControl27.TabIndex = 35;
            this.labelControl27.Text = "Отсрочка платежа, дней:";
            // 
            // labelControl28
            // 
            this.labelControl28.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl28.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl28.Appearance.Options.UseFont = true;
            this.labelControl28.Location = new System.Drawing.Point(5, 30);
            this.labelControl28.Name = "labelControl28";
            this.labelControl28.Size = new System.Drawing.Size(80, 13);
            this.labelControl28.TabIndex = 34;
            this.labelControl28.Text = "Лимит, сумма:";
            // 
            // grboxSaldo
            // 
            this.grboxSaldo.Controls.Add(this.LDebtSuppl);
            this.grboxSaldo.Controls.Add(this.LDebtWaybill);
            this.grboxSaldo.Controls.Add(this.lblDayDebtWaybillShipped);
            this.grboxSaldo.Controls.Add(this.lblDebtWaybillShipped);
            this.grboxSaldo.Controls.Add(this.labelControl20);
            this.grboxSaldo.Controls.Add(this.labelControl19);
            this.grboxSaldo.Controls.Add(this.labelControl18);
            this.grboxSaldo.Controls.Add(this.labelControl17);
            this.grboxSaldo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grboxSaldo.Location = new System.Drawing.Point(3, 3);
            this.grboxSaldo.Name = "grboxSaldo";
            this.grboxSaldo.Size = new System.Drawing.Size(359, 105);
            this.toolTipController.SetSuperTip(this.grboxSaldo, null);
            this.grboxSaldo.TabIndex = 0;
            this.grboxSaldo.Text = "Текущая задолженность";
            // 
            // LDebtSuppl
            // 
            this.LDebtSuppl.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.LDebtSuppl.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.LDebtSuppl.Appearance.ForeColor = System.Drawing.Color.Green;
            this.LDebtSuppl.Appearance.Options.UseFont = true;
            this.LDebtSuppl.Appearance.Options.UseForeColor = true;
            this.LDebtSuppl.Appearance.Options.UseTextOptions = true;
            this.LDebtSuppl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.LDebtSuppl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LDebtSuppl.Location = new System.Drawing.Point(290, 87);
            this.LDebtSuppl.Name = "LDebtSuppl";
            this.LDebtSuppl.Size = new System.Drawing.Size(64, 13);
            this.LDebtSuppl.TabIndex = 33;
            this.LDebtSuppl.Text = "LDebtSuppl";
            // 
            // LDebtWaybill
            // 
            this.LDebtWaybill.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.LDebtWaybill.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.LDebtWaybill.Appearance.ForeColor = System.Drawing.Color.Green;
            this.LDebtWaybill.Appearance.Options.UseFont = true;
            this.LDebtWaybill.Appearance.Options.UseForeColor = true;
            this.LDebtWaybill.Appearance.Options.UseTextOptions = true;
            this.LDebtWaybill.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.LDebtWaybill.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LDebtWaybill.Location = new System.Drawing.Point(280, 68);
            this.LDebtWaybill.Name = "LDebtWaybill";
            this.LDebtWaybill.Size = new System.Drawing.Size(74, 13);
            this.LDebtWaybill.TabIndex = 32;
            this.LDebtWaybill.Text = "LDebtWaybill";
            // 
            // lblDayDebtWaybillShipped
            // 
            this.lblDayDebtWaybillShipped.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblDayDebtWaybillShipped.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblDayDebtWaybillShipped.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblDayDebtWaybillShipped.Appearance.Options.UseFont = true;
            this.lblDayDebtWaybillShipped.Appearance.Options.UseForeColor = true;
            this.lblDayDebtWaybillShipped.Appearance.Options.UseTextOptions = true;
            this.lblDayDebtWaybillShipped.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblDayDebtWaybillShipped.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblDayDebtWaybillShipped.Location = new System.Drawing.Point(250, 49);
            this.lblDayDebtWaybillShipped.Name = "lblDayDebtWaybillShipped";
            this.lblDayDebtWaybillShipped.Size = new System.Drawing.Size(104, 13);
            this.lblDayDebtWaybillShipped.TabIndex = 31;
            this.lblDayDebtWaybillShipped.Text = "lblDayDebtWaybillShipped";
            // 
            // lblDebtWaybillShipped
            // 
            this.lblDebtWaybillShipped.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblDebtWaybillShipped.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblDebtWaybillShipped.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblDebtWaybillShipped.Appearance.Options.UseFont = true;
            this.lblDebtWaybillShipped.Appearance.Options.UseForeColor = true;
            this.lblDebtWaybillShipped.Appearance.Options.UseTextOptions = true;
            this.lblDebtWaybillShipped.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblDebtWaybillShipped.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblDebtWaybillShipped.Location = new System.Drawing.Point(228, 30);
            this.lblDebtWaybillShipped.Name = "lblDebtWaybillShipped";
            this.lblDebtWaybillShipped.Size = new System.Drawing.Size(126, 13);
            this.lblDebtWaybillShipped.TabIndex = 30;
            this.lblDebtWaybillShipped.Text = "lblDebtWaybillShipped";
            // 
            // labelControl20
            // 
            this.labelControl20.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl20.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl20.Appearance.Options.UseFont = true;
            this.labelControl20.Location = new System.Drawing.Point(5, 87);
            this.labelControl20.Name = "labelControl20";
            this.labelControl20.Size = new System.Drawing.Size(226, 13);
            this.labelControl20.TabIndex = 29;
            this.labelControl20.Text = "Неотгруженные протоколы (сальдо):";
            // 
            // labelControl19
            // 
            this.labelControl19.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl19.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl19.Appearance.Options.UseFont = true;
            this.labelControl19.Location = new System.Drawing.Point(5, 68);
            this.labelControl19.Name = "labelControl19";
            this.labelControl19.Size = new System.Drawing.Size(228, 13);
            this.labelControl19.TabIndex = 28;
            this.labelControl19.Text = "Неотгруженные накладные (сальдо):";
            // 
            // labelControl18
            // 
            this.labelControl18.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl18.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl18.Appearance.Options.UseFont = true;
            this.labelControl18.Location = new System.Drawing.Point(5, 49);
            this.labelControl18.Name = "labelControl18";
            this.labelControl18.Size = new System.Drawing.Size(239, 13);
            this.labelControl18.TabIndex = 27;
            this.labelControl18.Text = "Отгруженные накладные (дней долга):";
            // 
            // labelControl17
            // 
            this.labelControl17.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl17.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl17.Appearance.Options.UseFont = true;
            this.labelControl17.Location = new System.Drawing.Point(5, 30);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(214, 13);
            this.labelControl17.TabIndex = 26;
            this.labelControl17.Text = "Отгруженные накладные (сальдо):";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "MS Excel 2010 files (*.xlsx)|*.xlsx|MS Excel 2003 files (*.xls)|*.xls|All files (" +
    "*.*)|*.*";
            // 
            // ctrlWaybillEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanelBackground);
            this.Name = "ctrlWaybillEditor";
            this.Size = new System.Drawing.Size(1078, 602);
            this.toolTipController.SetSuperTip(this, null);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEditOrderedQuantity)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.tableLayoutPanelBackground.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelProgressBar)).EndInit();
            this.panelProgressBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrderItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Product)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEditProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditDiscount)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkSetOrderInQueue.Properties)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Rtt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddressDelivery.Properties)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Stock.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Depart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesMan.Properties)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IsBonus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Customer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChildDepart.Properties)).EndInit();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DeliveryDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeliveryDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BeginDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BeginDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShipDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShipDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaybillShipMode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditForStock.Properties)).EndInit();
            this.tableLayoutPanel11.ResumeLayout(false);
            this.tableLayoutPanel11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WaybillState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PaymentType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaybilllNum.Properties)).EndInit();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboxProductTradeMark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxProductType.Properties)).EndInit();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkMultiplicity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditDiscount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditCalcPrices.Properties)).EndInit();
            this.tableLayoutPanel10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grboxResultCreditControl)).EndInit();
            this.grboxResultCreditControl.ResumeLayout(false);
            this.grboxResultCreditControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImageNot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageOk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grboxCreditLimit)).EndInit();
            this.grboxCreditLimit.ResumeLayout(false);
            this.grboxCreditLimit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grboxSaldo)).EndInit();
            this.grboxSaldo.ResumeLayout(false);
            this.grboxSaldo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.ToolTipController toolTipController;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem mitemExport;
        private System.Windows.Forms.ToolStripMenuItem mitmsExportToHTML;
        private System.Windows.Forms.ToolStripMenuItem mitmsExportToXML;
        private System.Windows.Forms.ToolStripMenuItem mitmsExportToXLS;
        private System.Windows.Forms.ToolStripMenuItem mitmsExportToTXT;
        private System.Windows.Forms.ToolStripMenuItem mitmsExportToDBF;
        private System.Windows.Forms.ToolStripMenuItem mitmsExportToDBFCurrency;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mitemImport;
        private System.Windows.Forms.ToolStripMenuItem mitmsImportFromExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mitemClearRows;
        private System.Data.DataSet dataSet;
        private System.Data.DataTable OrderItems;
        private System.Data.DataColumn OrderItemsID;
        private System.Data.DataColumn ProductID;
        private System.Data.DataColumn MeasureID;
        private System.Data.DataColumn Quantity;
        private System.Data.DataColumn PriceImporter;
        private System.Data.DataColumn Price;
        private System.Data.DataColumn DiscountPercent;
        private System.Data.DataColumn PriceWithDiscount;
        private System.Data.DataColumn NDSPercent;
        private System.Data.DataColumn PriceInAccountingCurrency;
        private System.Data.DataColumn PriceWithDiscountInAccountingCurrency;
        private System.Data.DataColumn QuantityReturned;
        private System.Data.DataColumn Sum;
        private System.Data.DataColumn SumWithDiscount;
        private System.Data.DataColumn SumInAccountingCurrency;
        private System.Data.DataColumn SumWithDiscountInAccountingCurrency;
        private System.Data.DataColumn OrderItems_MeasureName;
        private System.Data.DataColumn OrderPackQty;
        private System.Data.DataColumn OrderItems_PartsArticle;
        private System.Data.DataColumn OrderItems_PartsName;
        private System.Data.DataColumn OrderItems_QuantityInstock;
        private System.Data.DataTable Product;
        private System.Data.DataColumn ProductGuid;
        private System.Data.DataColumn ProductFullName;
        private System.Data.DataColumn CustomerOrderStockQty;
        private System.Data.DataColumn CustomerOrderResQty;
        private System.Data.DataColumn CustomerOrderPackQty;
        private System.Data.DataColumn Product_MeasureID;
        private System.Data.DataColumn Product_MeasureName;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelBackground;
        private DevExpress.XtraEditors.PanelControl panelProgressBar;
        private DevExpress.XtraEditors.ProgressBarControl progressBarControl1;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraGrid.Columns.GridColumn colOrderItemsID;
        private DevExpress.XtraGrid.Columns.GridColumn colProductID;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEditProduct;
        private DevExpress.XtraGrid.Columns.GridColumn colMeasureID;
        private DevExpress.XtraGrid.Columns.GridColumn colOrderItems_MeasureName;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantity;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit repositoryItemCalcEditOrderedQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantityReturned;
        private DevExpress.XtraGrid.Columns.GridColumn colPriceImporter;
        private DevExpress.XtraGrid.Columns.GridColumn colPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colDiscountPercent;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditDiscount;
        private DevExpress.XtraGrid.Columns.GridColumn colPriceWithDiscount;
        private DevExpress.XtraGrid.Columns.GridColumn colPriceInAccountingCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colPriceWithDiscountInAccountingCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colSum;
        private DevExpress.XtraGrid.Columns.GridColumn colSumWithDiscount;
        private DevExpress.XtraGrid.Columns.GridColumn colSumInAccountingCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colSumWithDiscountInAccountingCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colNDSPercent;
        private DevExpress.XtraGrid.Columns.GridColumn colOrderPackQty;
        private DevExpress.XtraGrid.Columns.GridColumn colOrderItems_PartsName;
        private DevExpress.XtraGrid.Columns.GridColumn colOrderItems_PartsArticle;
        private DevExpress.XtraGrid.Columns.GridColumn colOrderItems_QuantityInstock;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnEdit;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.CheckEdit checkSetOrderInQueue;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private DevExpress.XtraEditors.ComboBoxEdit Rtt;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.ComboBoxEdit AddressDelivery;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private DevExpress.XtraEditors.ComboBoxEdit Stock;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.ComboBoxEdit Depart;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.ComboBoxEdit SalesMan;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.ComboBoxEdit Customer;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit ChildDepart;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private DevExpress.XtraEditors.MemoEdit txtDescription;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private DevExpress.XtraEditors.CheckEdit IsBonus;
        private DevExpress.XtraEditors.DateEdit DeliveryDate;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.DateEdit BeginDate;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit PaymentType;
        private DevExpress.XtraEditors.ComboBoxEdit WaybillState;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraEditors.ComboBoxEdit cboxProductTradeMark;
        private DevExpress.XtraEditors.PictureEdit pictureFilter;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        private DevExpress.XtraEditors.ComboBoxEdit cboxProductType;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private DevExpress.XtraEditors.CheckEdit checkMultiplicity;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraEditors.ControlNavigator controlNavigator;
        private DevExpress.XtraEditors.SpinEdit spinEditDiscount;
        private DevExpress.XtraEditors.SimpleButton btnSetDiscount;
        private DevExpress.XtraEditors.CheckEdit checkEditCalcPrices;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private DevExpress.XtraEditors.GroupControl grboxResultCreditControl;
        private System.Windows.Forms.PictureBox ImageNot;
        private System.Windows.Forms.PictureBox ImageOk;
        private DevExpress.XtraEditors.LabelControl LCreditControlResult;
        private DevExpress.XtraEditors.LabelControl LOutDays;
        private DevExpress.XtraEditors.LabelControl LOutSumma;
        private DevExpress.XtraEditors.LabelControl labelControl23;
        private DevExpress.XtraEditors.LabelControl labelControl24;
        private DevExpress.XtraEditors.GroupControl grboxCreditLimit;
        private DevExpress.XtraEditors.LabelControl LEarning;
        private DevExpress.XtraEditors.LabelControl LOverdraftMoney;
        private DevExpress.XtraEditors.LabelControl LCustomerLimitDays;
        private DevExpress.XtraEditors.LabelControl LCustomerLimitMoney;
        private DevExpress.XtraEditors.LabelControl labelControl25;
        private DevExpress.XtraEditors.LabelControl labelControl26;
        private DevExpress.XtraEditors.LabelControl labelControl27;
        private DevExpress.XtraEditors.LabelControl labelControl28;
        private DevExpress.XtraEditors.GroupControl grboxSaldo;
        private DevExpress.XtraEditors.LabelControl LDebtSuppl;
        private DevExpress.XtraEditors.LabelControl LDebtWaybill;
        private DevExpress.XtraEditors.LabelControl lblDayDebtWaybillShipped;
        private DevExpress.XtraEditors.LabelControl lblDebtWaybillShipped;
        private DevExpress.XtraEditors.LabelControl labelControl20;
        private DevExpress.XtraEditors.LabelControl labelControl19;
        private DevExpress.XtraEditors.LabelControl labelControl18;
        private DevExpress.XtraEditors.LabelControl labelControl17;
        private DevExpress.XtraEditors.LabelControl labelControl21;
        private DevExpress.XtraEditors.DateEdit ShipDate;
        private DevExpress.XtraEditors.LabelControl labelControl22;
        private DevExpress.XtraEditors.ComboBoxEdit WaybillShipMode;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
        private System.Data.DataColumn WaybItem_Id;
        private System.Data.DataColumn SupplItem_Guid;
        private System.Data.DataColumn QuantityWithReturn;
        private DevExpress.XtraEditors.LabelControl labelControl29;
        private DevExpress.XtraEditors.TextEdit WaybilllNum;
        private System.Windows.Forms.ToolStripMenuItem mitemDeleteSelectedRows;
        private DevExpress.XtraEditors.CheckEdit checkEditForStock;
    }
}

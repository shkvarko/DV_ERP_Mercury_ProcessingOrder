namespace ERPMercuryProcessingOrder
{
    partial class ctrlIntOrderEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrlIntOrderEditor));
            this.colQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCalcEditOrderedQuantity = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.colPriceImporter = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPriceWithDiscount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPriceRetail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNDSPercent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuantityReturned = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.PriceRetail = new System.Data.DataColumn();
            this.QuantityReturned = new System.Data.DataColumn();
            this.Sum = new System.Data.DataColumn();
            this.SumWithDiscount = new System.Data.DataColumn();
            this.SumRetail = new System.Data.DataColumn();
            this.OrderItems_MeasureName = new System.Data.DataColumn();
            this.OrderItems_PartsArticle = new System.Data.DataColumn();
            this.OrderItems_PartsName = new System.Data.DataColumn();
            this.OrderItems_QuantityInstock = new System.Data.DataColumn();
            this.OrderItem_Id = new System.Data.DataColumn();
            this.QuantityWithReturn = new System.Data.DataColumn();
            this.MarkUp = new System.Data.DataColumn();
            this.Product = new System.Data.DataTable();
            this.ProductGuid = new System.Data.DataColumn();
            this.ProductFullName = new System.Data.DataColumn();
            this.StockQty = new System.Data.DataColumn();
            this.ResQty = new System.Data.DataColumn();
            this.PackQty = new System.Data.DataColumn();
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
            this.colSumRetail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMarkUp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOrderPackQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOrderItems_PartsName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOrderItems_PartsArticle = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOrderItems_QuantityInstock = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.checkSetOrderInQueue = new DevExpress.XtraEditors.CheckEdit();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.Description = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.BeginDate = new DevExpress.XtraEditors.DateEdit();
            this.DocNum = new DevExpress.XtraEditors.TextEdit();
            this.labelControl29 = new DevExpress.XtraEditors.LabelControl();
            this.checkEditForStock = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl21 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl22 = new DevExpress.XtraEditors.LabelControl();
            this.ShipMode = new DevExpress.XtraEditors.ComboBoxEdit();
            this.ShipDate = new DevExpress.XtraEditors.DateEdit();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.DocState = new DevExpress.XtraEditors.ComboBoxEdit();
            this.StockDst = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.StockSrc = new DevExpress.XtraEditors.ComboBoxEdit();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.PaymentType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.SalesMan = new DevExpress.XtraEditors.ComboBoxEdit();
            this.Depart = new DevExpress.XtraEditors.ComboBoxEdit();
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
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.cboxPriceType = new DevExpress.XtraEditors.ComboBoxEdit();
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
            this.tableLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Description.Properties)).BeginInit();
            this.tableLayoutPanel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BeginDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BeginDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditForStock.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShipMode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShipDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShipDate.Properties)).BeginInit();
            this.tableLayoutPanel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DocState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StockDst.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StockSrc.Properties)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PaymentType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesMan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Depart.Properties)).BeginInit();
            this.tableLayoutPanel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboxProductTradeMark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxProductType.Properties)).BeginInit();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkMultiplicity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditDiscount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxPriceType.Properties)).BeginInit();
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
            // colPriceRetail
            // 
            this.colPriceRetail.AppearanceCell.Options.UseTextOptions = true;
            this.colPriceRetail.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colPriceRetail.AppearanceHeader.Options.UseTextOptions = true;
            this.colPriceRetail.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPriceRetail.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colPriceRetail.Caption = "Цена розничная, руб.";
            this.colPriceRetail.DisplayFormat.FormatString = "# ### ### ##0.00";
            this.colPriceRetail.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPriceRetail.FieldName = "PriceRetail";
            this.colPriceRetail.MinWidth = 50;
            this.colPriceRetail.Name = "colPriceRetail";
            this.colPriceRetail.OptionsColumn.AllowEdit = false;
            this.colPriceRetail.OptionsColumn.ReadOnly = true;
            this.colPriceRetail.Visible = true;
            this.colPriceRetail.VisibleIndex = 10;
            this.colPriceRetail.Width = 68;
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
            this.colNDSPercent.VisibleIndex = 12;
            this.colNDSPercent.Width = 51;
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
            this.contextMenuStrip.Size = new System.Drawing.Size(329, 126);
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
            this.mitmsExportToDBF.Text = "в DBF...";
            this.mitmsExportToDBF.Click += new System.EventHandler(this.sbExportToDBF_Click);
            // 
            // mitmsExportToDBFCurrency
            // 
            this.mitmsExportToDBFCurrency.Name = "mitmsExportToDBFCurrency";
            this.mitmsExportToDBFCurrency.Size = new System.Drawing.Size(233, 22);
            this.mitmsExportToDBFCurrency.Text = "в DBF (цена в валюте учета)...";
            this.mitmsExportToDBFCurrency.Visible = false;
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
            this.mitmsImportFromExcel.Size = new System.Drawing.Size(152, 22);
            this.mitmsImportFromExcel.Text = "из MS Excel";
            this.mitmsImportFromExcel.Click += new System.EventHandler(this.mitmsImportFromExcel_Click);
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
            this.mitemDeleteSelectedRows.Click += new System.EventHandler(this.mitemClearRows_Click);
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
            this.tableLayoutPanelBackground.Controls.Add(this.panelProgressBar, 0, 1);
            this.tableLayoutPanelBackground.Controls.Add(this.gridControl, 0, 4);
            this.tableLayoutPanelBackground.Controls.Add(this.tableLayoutPanel1, 0, 5);
            this.tableLayoutPanelBackground.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanelBackground.Controls.Add(this.tableLayoutPanel8, 0, 2);
            this.tableLayoutPanelBackground.Controls.Add(this.tableLayoutPanel6, 0, 3);
            this.tableLayoutPanelBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelBackground.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelBackground.Name = "tableLayoutPanelBackground";
            this.tableLayoutPanelBackground.RowCount = 6;
            this.tableLayoutPanelBackground.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanelBackground.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanelBackground.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanelBackground.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanelBackground.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelBackground.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanelBackground.Size = new System.Drawing.Size(1025, 618);
            this.toolTipController.SetSuperTip(this.tableLayoutPanelBackground, null);
            this.tableLayoutPanelBackground.TabIndex = 2;
            // 
            // panelProgressBar
            // 
            this.panelProgressBar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelProgressBar.Controls.Add(this.progressBarControl1);
            this.panelProgressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelProgressBar.Location = new System.Drawing.Point(3, 133);
            this.panelProgressBar.Name = "panelProgressBar";
            this.panelProgressBar.Size = new System.Drawing.Size(1019, 25);
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
            this.progressBarControl1.Size = new System.Drawing.Size(1011, 16);
            this.progressBarControl1.TabIndex = 0;
            // 
            // gridControl
            // 
            this.gridControl.ContextMenuStrip = this.contextMenuStrip;
            this.gridControl.DataMember = "OrderItems";
            this.gridControl.DataSource = this.dataSet;
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.EmbeddedNavigator.Name = "";
            this.gridControl.Location = new System.Drawing.Point(3, 216);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEditProduct,
            this.repositoryItemCalcEditOrderedQuantity,
            this.repositoryItemSpinEditDiscount});
            this.gridControl.Size = new System.Drawing.Size(1019, 368);
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
            this.PriceRetail,
            this.QuantityReturned,
            this.Sum,
            this.SumWithDiscount,
            this.SumRetail,
            this.OrderItems_MeasureName,
            this.OrderItems_PartsArticle,
            this.OrderItems_PartsName,
            this.OrderItems_QuantityInstock,
            this.OrderItem_Id,
            this.QuantityWithReturn,
            this.MarkUp});
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
            // PriceRetail
            // 
            this.PriceRetail.Caption = "Розничная цена";
            this.PriceRetail.ColumnName = "PriceRetail";
            this.PriceRetail.DataType = typeof(double);
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
            // SumRetail
            // 
            this.SumRetail.Caption = "Сумма розничная";
            this.SumRetail.ColumnName = "SumRetail";
            this.SumRetail.DataType = typeof(double);
            this.SumRetail.Expression = "PriceRetail * Quantity";
            this.SumRetail.ReadOnly = true;
            // 
            // OrderItems_MeasureName
            // 
            this.OrderItems_MeasureName.Caption = "Ед. изм.";
            this.OrderItems_MeasureName.ColumnName = "OrderItems_MeasureName";
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
            // OrderItem_Id
            // 
            this.OrderItem_Id.ColumnName = "OrderItem_Id";
            this.OrderItem_Id.DataType = typeof(int);
            // 
            // QuantityWithReturn
            // 
            this.QuantityWithReturn.ColumnName = "QuantityWithReturn";
            this.QuantityWithReturn.DataType = typeof(decimal);
            this.QuantityWithReturn.Expression = "Quantity - QuantityReturned";
            this.QuantityWithReturn.ReadOnly = true;
            // 
            // MarkUp
            // 
            this.MarkUp.ColumnName = "MarkUp";
            this.MarkUp.DataType = typeof(double);
            // 
            // Product
            // 
            this.Product.Columns.AddRange(new System.Data.DataColumn[] {
            this.ProductGuid,
            this.ProductFullName,
            this.StockQty,
            this.ResQty,
            this.PackQty,
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
            // StockQty
            // 
            this.StockQty.Caption = "Остаток";
            this.StockQty.ColumnName = "StockQty";
            this.StockQty.DataType = typeof(decimal);
            // 
            // ResQty
            // 
            this.ResQty.Caption = "резерв";
            this.ResQty.ColumnName = "ResQty";
            this.ResQty.DataType = typeof(decimal);
            // 
            // PackQty
            // 
            this.PackQty.Caption = "Количество в упаковке";
            this.PackQty.ColumnName = "PackQty";
            this.PackQty.DataType = typeof(decimal);
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
            this.colPriceRetail,
            this.colSum,
            this.colSumWithDiscount,
            this.colSumRetail,
            this.colNDSPercent,
            this.colMarkUp,
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
            styleFormatCondition3.Column = this.colPrice;
            styleFormatCondition3.Condition = DevExpress.XtraGrid.FormatConditionEnum.LessOrEqual;
            styleFormatCondition3.Value1 = "0";
            styleFormatCondition4.Appearance.BackColor = System.Drawing.Color.Red;
            styleFormatCondition4.Appearance.Options.UseBackColor = true;
            styleFormatCondition4.Column = this.colPriceWithDiscount;
            styleFormatCondition4.Condition = DevExpress.XtraGrid.FormatConditionEnum.LessOrEqual;
            styleFormatCondition4.Value1 = "0";
            styleFormatCondition5.Appearance.BackColor = System.Drawing.Color.Red;
            styleFormatCondition5.Appearance.Options.UseBackColor = true;
            styleFormatCondition5.Column = this.colPriceRetail;
            styleFormatCondition5.Condition = DevExpress.XtraGrid.FormatConditionEnum.LessOrEqual;
            styleFormatCondition5.Value1 = "0";
            styleFormatCondition6.Appearance.BackColor = System.Drawing.Color.Red;
            styleFormatCondition6.Appearance.Options.UseBackColor = true;
            styleFormatCondition6.Column = this.colNDSPercent;
            styleFormatCondition6.Condition = DevExpress.XtraGrid.FormatConditionEnum.Less;
            styleFormatCondition6.Value1 = "0";
            this.gridView.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1,
            styleFormatCondition2,
            styleFormatCondition3,
            styleFormatCondition4,
            styleFormatCondition5,
            styleFormatCondition6});
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
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("StockQty", "Остаток", 30, DevExpress.Utils.FormatType.Numeric, "### ### ##0", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ResQty", "в Резерве", 30, DevExpress.Utils.FormatType.Numeric, "### ### ##0", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None)});
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
            // colSumRetail
            // 
            this.colSumRetail.AppearanceCell.Options.UseTextOptions = true;
            this.colSumRetail.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colSumRetail.AppearanceHeader.Options.UseTextOptions = true;
            this.colSumRetail.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSumRetail.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colSumRetail.Caption = "Сумма розничная";
            this.colSumRetail.DisplayFormat.FormatString = "# ### ### ##0";
            this.colSumRetail.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSumRetail.FieldName = "SumRetail";
            this.colSumRetail.MinWidth = 50;
            this.colSumRetail.Name = "colSumRetail";
            this.colSumRetail.OptionsColumn.AllowEdit = false;
            this.colSumRetail.OptionsColumn.ReadOnly = true;
            this.colSumRetail.SummaryItem.DisplayFormat = "{0:### ### ##0.00}";
            this.colSumRetail.SummaryItem.FieldName = "SumWithDiscountInAccountingCurrency";
            this.colSumRetail.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colSumRetail.Visible = true;
            this.colSumRetail.VisibleIndex = 11;
            this.colSumRetail.Width = 71;
            // 
            // colMarkUp
            // 
            this.colMarkUp.Caption = "Надбавка, %";
            this.colMarkUp.FieldName = "MarkUp";
            this.colMarkUp.Name = "colMarkUp";
            this.colMarkUp.Visible = true;
            this.colMarkUp.VisibleIndex = 13;
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
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 118F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 156F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel1.Controls.Add(this.btnCancel, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSave, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.checkSetOrderInQueue, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnPrint, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnEdit, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 587);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1025, 31);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel1, null);
            this.tableLayoutPanel1.TabIndex = 16;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(948, 4);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Выход";
            this.btnCancel.ToolTip = "Закрыть редактор заказа";
            this.btnCancel.ToolTipController = this.toolTipController;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(867, 4);
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
            this.checkSetOrderInQueue.Location = new System.Drawing.Point(705, 6);
            this.checkSetOrderInQueue.Name = "checkSetOrderInQueue";
            this.checkSetOrderInQueue.Properties.Caption = "в очередь на обработку";
            this.checkSetOrderInQueue.Size = new System.Drawing.Size(150, 19);
            this.checkSetOrderInQueue.TabIndex = 4;
            this.checkSetOrderInQueue.ToolTip = "заказ будет помещён в очередь для последующей автоматической обработки и формиров" +
    "ания резерва";
            this.checkSetOrderInQueue.ToolTipController = this.toolTipController;
            this.checkSetOrderInQueue.Visible = false;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.Location = new System.Drawing.Point(120, 4);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 25);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Text = "Печать";
            this.btnPrint.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.btnPrint.Visible = false;
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEdit.Image = global::ERPMercuryProcessingOrder.Properties.Resources.document_edit;
            this.btnEdit.Location = new System.Drawing.Point(2, 4);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(2);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(112, 25);
            this.btnEdit.TabIndex = 0;
            this.btnEdit.Text = "Редактировать";
            this.btnEdit.ToolTipController = this.toolTipController;
            this.btnEdit.Visible = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel7, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel9, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel11, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1025, 130);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel2, null);
            this.tableLayoutPanel2.TabIndex = 17;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 92F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Controls.Add(this.Description, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.labelControl12, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(0, 75);
            this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(1025, 55);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel7, null);
            this.tableLayoutPanel7.TabIndex = 4;
            // 
            // Description
            // 
            this.Description.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Description.Location = new System.Drawing.Point(95, 3);
            this.Description.Name = "Description";
            this.Description.Size = new System.Drawing.Size(927, 49);
            this.Description.TabIndex = 26;
            this.Description.ToolTip = "Примечание";
            this.Description.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.Description.EditValueChanged += new System.EventHandler(this.cboxOrderPropertie_SelectedValueChanged);
            this.Description.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.txtOrderPropertie_EditValueChanging);
            // 
            // labelControl12
            // 
            this.labelControl12.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl12.Location = new System.Drawing.Point(3, 21);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(65, 13);
            this.labelControl12.TabIndex = 25;
            this.labelControl12.Text = "Примечание:";
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 9;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 92F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 109F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 114F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 171F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Controls.Add(this.labelControl10, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this.BeginDate, 1, 0);
            this.tableLayoutPanel9.Controls.Add(this.DocNum, 3, 0);
            this.tableLayoutPanel9.Controls.Add(this.labelControl29, 2, 0);
            this.tableLayoutPanel9.Controls.Add(this.checkEditForStock, 8, 0);
            this.tableLayoutPanel9.Controls.Add(this.labelControl21, 4, 0);
            this.tableLayoutPanel9.Controls.Add(this.labelControl22, 6, 0);
            this.tableLayoutPanel9.Controls.Add(this.ShipMode, 7, 0);
            this.tableLayoutPanel9.Controls.Add(this.ShipDate, 5, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel9.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 1;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(1025, 25);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel9, null);
            this.tableLayoutPanel9.TabIndex = 5;
            // 
            // labelControl10
            // 
            this.labelControl10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl10.Location = new System.Drawing.Point(3, 6);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(67, 13);
            this.labelControl10.TabIndex = 20;
            this.labelControl10.Text = "Дата заказа:";
            // 
            // BeginDate
            // 
            this.BeginDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.BeginDate.EditValue = null;
            this.BeginDate.Location = new System.Drawing.Point(95, 3);
            this.BeginDate.Name = "BeginDate";
            this.BeginDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.BeginDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.BeginDate.Size = new System.Drawing.Size(103, 20);
            this.BeginDate.TabIndex = 21;
            this.BeginDate.ToolTip = "Дата оформления";
            this.BeginDate.ToolTipController = this.toolTipController;
            this.BeginDate.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.BeginDate.EditValueChanged += new System.EventHandler(this.cboxOrderPropertie_SelectedValueChanged);
            this.BeginDate.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.txtOrderPropertie_EditValueChanging);
            // 
            // DocNum
            // 
            this.DocNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.DocNum.Location = new System.Drawing.Point(283, 3);
            this.DocNum.Name = "DocNum";
            this.DocNum.Properties.Appearance.Options.UseTextOptions = true;
            this.DocNum.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.DocNum.Size = new System.Drawing.Size(100, 20);
            this.DocNum.TabIndex = 31;
            this.DocNum.EditValueChanged += new System.EventHandler(this.cboxOrderPropertie_SelectedValueChanged);
            this.DocNum.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.txtOrderPropertie_EditValueChanging);
            // 
            // labelControl29
            // 
            this.labelControl29.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl29.Location = new System.Drawing.Point(204, 6);
            this.labelControl29.Name = "labelControl29";
            this.labelControl29.Size = new System.Drawing.Size(73, 13);
            this.labelControl29.TabIndex = 30;
            this.labelControl29.Text = "Номер док-та:";
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
            // labelControl21
            // 
            this.labelControl21.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl21.Location = new System.Drawing.Point(389, 6);
            this.labelControl21.Name = "labelControl21";
            this.labelControl21.Size = new System.Drawing.Size(79, 13);
            this.labelControl21.TabIndex = 27;
            this.labelControl21.Text = "Дата отгрузки:";
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
            // ShipMode
            // 
            this.ShipMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ShipMode.Location = new System.Drawing.Point(664, 3);
            this.ShipMode.Name = "ShipMode";
            this.ShipMode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ShipMode.Properties.PopupSizeable = true;
            this.ShipMode.Properties.Sorted = true;
            this.ShipMode.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.ShipMode.Size = new System.Drawing.Size(165, 20);
            this.ShipMode.TabIndex = 30;
            this.ShipMode.ToolTip = "Вид отгрузки";
            this.ShipMode.ToolTipController = this.toolTipController;
            this.ShipMode.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.ShipMode.SelectedValueChanged += new System.EventHandler(this.cboxOrderPropertie_SelectedValueChanged);
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
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.ColumnCount = 7;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 92F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 190F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 195F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 171F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.Controls.Add(this.labelControl2, 4, 0);
            this.tableLayoutPanel11.Controls.Add(this.DocState, 5, 0);
            this.tableLayoutPanel11.Controls.Add(this.StockDst, 3, 0);
            this.tableLayoutPanel11.Controls.Add(this.labelControl8, 0, 0);
            this.tableLayoutPanel11.Controls.Add(this.labelControl1, 2, 0);
            this.tableLayoutPanel11.Controls.Add(this.StockSrc, 1, 0);
            this.tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel11.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanel11.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 1;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(1025, 25);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel11, null);
            this.tableLayoutPanel11.TabIndex = 6;
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl2.Location = new System.Drawing.Point(585, 6);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(58, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Состояние:";
            // 
            // DocState
            // 
            this.DocState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.DocState.Location = new System.Drawing.Point(664, 3);
            this.DocState.Name = "DocState";
            this.DocState.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DocState.Properties.PopupSizeable = true;
            this.DocState.Properties.Sorted = true;
            this.DocState.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.DocState.Size = new System.Drawing.Size(165, 20);
            this.DocState.TabIndex = 3;
            this.DocState.ToolTip = "Состояние накладной";
            this.DocState.ToolTipController = this.toolTipController;
            this.DocState.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.DocState.SelectedValueChanged += new System.EventHandler(this.cboxOrderPropertie_SelectedValueChanged);
            // 
            // StockDst
            // 
            this.StockDst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.StockDst.Location = new System.Drawing.Point(390, 3);
            this.StockDst.Name = "StockDst";
            this.StockDst.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.StockDst.Properties.PopupSizeable = true;
            this.StockDst.Properties.Sorted = true;
            this.StockDst.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.StockDst.Size = new System.Drawing.Size(189, 20);
            this.StockDst.TabIndex = 25;
            this.StockDst.ToolTip = "Склад отгрузки";
            this.StockDst.ToolTipController = this.toolTipController;
            this.StockDst.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.StockDst.SelectedValueChanged += new System.EventHandler(this.cboxOrderPropertie_SelectedValueChanged);
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
            // labelControl1
            // 
            this.labelControl1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl1.Location = new System.Drawing.Point(285, 6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(100, 13);
            this.labelControl1.TabIndex = 24;
            this.labelControl1.Text = "Склад-получатель:";
            // 
            // StockSrc
            // 
            this.StockSrc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.StockSrc.Location = new System.Drawing.Point(95, 3);
            this.StockSrc.Name = "StockSrc";
            this.StockSrc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.StockSrc.Properties.PopupSizeable = true;
            this.StockSrc.Properties.Sorted = true;
            this.StockSrc.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.StockSrc.Size = new System.Drawing.Size(184, 20);
            this.StockSrc.TabIndex = 1;
            this.StockSrc.ToolTip = "Склад отгрузки";
            this.StockSrc.ToolTipController = this.toolTipController;
            this.StockSrc.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.StockSrc.SelectedValueChanged += new System.EventHandler(this.cboxOrderPropertie_SelectedValueChanged);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 7;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 92F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 138F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 244F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 171F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.PaymentType, 5, 0);
            this.tableLayoutPanel4.Controls.Add(this.labelControl9, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.labelControl4, 4, 0);
            this.tableLayoutPanel4.Controls.Add(this.labelControl11, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.SalesMan, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.Depart, 1, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 50);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1025, 25);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel4, null);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // PaymentType
            // 
            this.PaymentType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.PaymentType.Location = new System.Drawing.Point(664, 3);
            this.PaymentType.Name = "PaymentType";
            this.PaymentType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.PaymentType.Properties.PopupSizeable = true;
            this.PaymentType.Properties.Sorted = true;
            this.PaymentType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.PaymentType.Size = new System.Drawing.Size(165, 20);
            this.PaymentType.TabIndex = 5;
            this.PaymentType.ToolTip = "Форма оплаты заказа";
            this.PaymentType.ToolTipController = this.toolTipController;
            this.PaymentType.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.PaymentType.Visible = false;
            this.PaymentType.SelectedValueChanged += new System.EventHandler(this.cboxOrderPropertie_SelectedValueChanged);
            // 
            // labelControl9
            // 
            this.labelControl9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl9.Location = new System.Drawing.Point(3, 6);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(84, 13);
            this.labelControl9.TabIndex = 24;
            this.labelControl9.Text = "Подразделение:";
            // 
            // labelControl4
            // 
            this.labelControl4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl4.Location = new System.Drawing.Point(585, 6);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(77, 13);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "Форма оплаты:";
            this.labelControl4.Visible = false;
            // 
            // labelControl11
            // 
            this.labelControl11.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl11.Location = new System.Drawing.Point(203, 6);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(134, 13);
            this.labelControl11.TabIndex = 25;
            this.labelControl11.Text = "Торговый представитель:";
            // 
            // SalesMan
            // 
            this.SalesMan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.SalesMan.Location = new System.Drawing.Point(341, 3);
            this.SalesMan.Name = "SalesMan";
            this.SalesMan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.SalesMan.Properties.PopupSizeable = true;
            this.SalesMan.Properties.Sorted = true;
            this.SalesMan.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.SalesMan.Size = new System.Drawing.Size(238, 20);
            this.SalesMan.TabIndex = 3;
            this.SalesMan.ToolTip = "Торговый представитель";
            this.SalesMan.ToolTipController = this.toolTipController;
            this.SalesMan.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.SalesMan.SelectedValueChanged += new System.EventHandler(this.cboxOrderPropertie_SelectedValueChanged);
            // 
            // Depart
            // 
            this.Depart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Depart.Location = new System.Drawing.Point(95, 3);
            this.Depart.Name = "Depart";
            this.Depart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Depart.Properties.PopupSizeable = true;
            this.Depart.Properties.Sorted = true;
            this.Depart.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.Depart.Size = new System.Drawing.Size(102, 20);
            this.Depart.TabIndex = 2;
            this.Depart.ToolTip = "Подразделение";
            this.Depart.ToolTipController = this.toolTipController;
            this.Depart.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.Depart.SelectedValueChanged += new System.EventHandler(this.cboxOrderPropertie_SelectedValueChanged);
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
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel8.Controls.Add(this.labelControl13, 1, 0);
            this.tableLayoutPanel8.Controls.Add(this.cboxProductTradeMark, 2, 0);
            this.tableLayoutPanel8.Controls.Add(this.pictureFilter, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.labelControl14, 2, 0);
            this.tableLayoutPanel8.Controls.Add(this.labelControl15, 4, 0);
            this.tableLayoutPanel8.Controls.Add(this.cboxProductType, 5, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(0, 161);
            this.tableLayoutPanel8.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(1025, 28);
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
            this.cboxProductTradeMark.Size = new System.Drawing.Size(291, 20);
            this.cboxProductTradeMark.TabIndex = 29;
            this.cboxProductTradeMark.ToolTip = "Товарная марка";
            this.cboxProductTradeMark.ToolTipController = this.toolTipController;
            this.cboxProductTradeMark.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
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
            this.labelControl15.Location = new System.Drawing.Point(608, 7);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(90, 13);
            this.labelControl15.TabIndex = 28;
            this.labelControl15.Text = "Товарная группа:";
            // 
            // cboxProductType
            // 
            this.cboxProductType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboxProductType.Location = new System.Drawing.Point(708, 4);
            this.cboxProductType.Name = "cboxProductType";
            this.cboxProductType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboxProductType.Properties.PopupSizeable = true;
            this.cboxProductType.Properties.Sorted = true;
            this.cboxProductType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboxProductType.Size = new System.Drawing.Size(291, 20);
            this.cboxProductType.TabIndex = 30;
            this.cboxProductType.ToolTip = "Товарная группа";
            this.cboxProductType.ToolTipController = this.toolTipController;
            this.cboxProductType.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 7;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 316F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 119F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 68F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 73F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 104F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.checkMultiplicity, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.labelControl16, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.controlNavigator, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.spinEditDiscount, 2, 0);
            this.tableLayoutPanel6.Controls.Add(this.btnSetDiscount, 3, 0);
            this.tableLayoutPanel6.Controls.Add(this.labelControl3, 5, 0);
            this.tableLayoutPanel6.Controls.Add(this.cboxPriceType, 6, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 189);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(1025, 24);
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
            this.controlNavigator.Size = new System.Drawing.Size(282, 19);
            this.controlNavigator.TabIndex = 22;
            this.controlNavigator.Text = "controlNavigator";
            this.controlNavigator.TextLocation = DevExpress.XtraEditors.NavigatorButtonsTextLocation.Center;
            this.controlNavigator.TextStringFormat = "Запись {0} из {1}";
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
            this.btnSetDiscount.Image = global::ERPMercuryProcessingOrder.Properties.Resources.ok_16;
            this.btnSetDiscount.Location = new System.Drawing.Point(576, 1);
            this.btnSetDiscount.Margin = new System.Windows.Forms.Padding(0);
            this.btnSetDiscount.Name = "btnSetDiscount";
            this.btnSetDiscount.Size = new System.Drawing.Size(23, 22);
            this.btnSetDiscount.TabIndex = 30;
            this.btnSetDiscount.ToolTip = "Установить скидку на все позиции";
            this.btnSetDiscount.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.btnSetDiscount.Click += new System.EventHandler(this.btnSetDiscount_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelControl3.Location = new System.Drawing.Point(626, 5);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(76, 13);
            this.labelControl3.TabIndex = 33;
            this.labelControl3.Text = "Уровень цены:";
            // 
            // cboxPriceType
            // 
            this.cboxPriceType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboxPriceType.Location = new System.Drawing.Point(708, 3);
            this.cboxPriceType.Name = "cboxPriceType";
            this.cboxPriceType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboxPriceType.Properties.PopupSizeable = true;
            this.cboxPriceType.Properties.Sorted = true;
            this.cboxPriceType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboxPriceType.Size = new System.Drawing.Size(291, 20);
            this.cboxPriceType.TabIndex = 34;
            this.cboxPriceType.ToolTip = "Уровень цен для позиций в заказе";
            this.cboxPriceType.ToolTipController = this.toolTipController;
            this.cboxPriceType.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.cboxPriceType.SelectedValueChanged += new System.EventHandler(this.cboxPriceType_SelectedValueChanged);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "MS Excel 2010 files (*.xlsx)|*.xlsx|MS Excel 2003 files (*.xls)|*.xls|All files (" +
    "*.*)|*.*";
            // 
            // ctrlIntOrderEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanelBackground);
            this.Name = "ctrlIntOrderEditor";
            this.Size = new System.Drawing.Size(1025, 618);
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
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Description.Properties)).EndInit();
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BeginDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BeginDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditForStock.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShipMode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShipDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShipDate.Properties)).EndInit();
            this.tableLayoutPanel11.ResumeLayout(false);
            this.tableLayoutPanel11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DocState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StockDst.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StockSrc.Properties)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PaymentType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesMan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Depart.Properties)).EndInit();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboxProductTradeMark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxProductType.Properties)).EndInit();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkMultiplicity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditDiscount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxPriceType.Properties)).EndInit();
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
        private System.Windows.Forms.ToolStripMenuItem mitemDeleteSelectedRows;
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
        private System.Data.DataColumn PriceRetail;
        private System.Data.DataColumn QuantityReturned;
        private System.Data.DataColumn Sum;
        private System.Data.DataColumn SumWithDiscount;
        private System.Data.DataColumn SumRetail;
        private System.Data.DataColumn OrderItems_MeasureName;
        private System.Data.DataColumn OrderItems_PartsArticle;
        private System.Data.DataColumn OrderItems_PartsName;
        private System.Data.DataColumn OrderItems_QuantityInstock;
        private System.Data.DataColumn OrderItem_Id;
        private System.Data.DataColumn QuantityWithReturn;
        private System.Data.DataColumn MarkUp;
        private System.Data.DataTable Product;
        private System.Data.DataColumn ProductGuid;
        private System.Data.DataColumn ProductFullName;
        private System.Data.DataColumn StockQty;
        private System.Data.DataColumn ResQty;
        private System.Data.DataColumn PackQty;
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
        private DevExpress.XtraGrid.Columns.GridColumn colPriceRetail;
        private DevExpress.XtraGrid.Columns.GridColumn colSum;
        private DevExpress.XtraGrid.Columns.GridColumn colSumWithDiscount;
        private DevExpress.XtraGrid.Columns.GridColumn colSumRetail;
        private DevExpress.XtraGrid.Columns.GridColumn colNDSPercent;
        private DevExpress.XtraGrid.Columns.GridColumn colMarkUp;
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private DevExpress.XtraEditors.MemoEdit Description;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.DateEdit BeginDate;
        private DevExpress.XtraEditors.LabelControl labelControl21;
        private DevExpress.XtraEditors.DateEdit ShipDate;
        private DevExpress.XtraEditors.TextEdit DocNum;
        private DevExpress.XtraEditors.LabelControl labelControl29;
        private DevExpress.XtraEditors.LabelControl labelControl22;
        private DevExpress.XtraEditors.ComboBoxEdit ShipMode;
        private DevExpress.XtraEditors.CheckEdit checkEditForStock;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit DocState;
        private DevExpress.XtraEditors.ComboBoxEdit StockDst;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit StockSrc;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private DevExpress.XtraEditors.ComboBoxEdit PaymentType;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.ComboBoxEdit SalesMan;
        private DevExpress.XtraEditors.ComboBoxEdit Depart;
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
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.ComboBoxEdit cboxPriceType;
    }
}

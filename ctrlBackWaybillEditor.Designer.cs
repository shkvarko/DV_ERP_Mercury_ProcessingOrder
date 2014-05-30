namespace ERPMercuryProcessingOrder
{
    partial class ctrlBackWaybillEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrlBackWaybillEditor));
            this.colQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCalcEditOrderedQuantity = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.colPriceImporter = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPriceInAccountingCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.BackWaybItem_Guid = new System.Data.DataColumn();
            this.ProductID = new System.Data.DataColumn();
            this.MeasureID = new System.Data.DataColumn();
            this.Quantity = new System.Data.DataColumn();
            this.PriceImporter = new System.Data.DataColumn();
            this.Price = new System.Data.DataColumn();
            this.PriceInAccountingCurrency = new System.Data.DataColumn();
            this.Sum = new System.Data.DataColumn();
            this.SumInAccountingCurrency = new System.Data.DataColumn();
            this.BackWaybItems_MeasureName = new System.Data.DataColumn();
            this.BackWaybItems_PartsArticle = new System.Data.DataColumn();
            this.BackWaybItems_PartsName = new System.Data.DataColumn();
            this.BackWaybItems_QuantitySrc = new System.Data.DataColumn();
            this.BackWaybItem_Id = new System.Data.DataColumn();
            this.WaybItem_Guid = new System.Data.DataColumn();
            this.BackWaybItems_Waybill_Num = new System.Data.DataColumn();
            this.BackWaybItems_Waybill_BeginDate = new System.Data.DataColumn();
            this.Product = new System.Data.DataTable();
            this.Product_ProductGuid = new System.Data.DataColumn();
            this.Product_ProductFullName = new System.Data.DataColumn();
            this.Product_WaybItem_Quantity = new System.Data.DataColumn();
            this.Product_WaybItem_BasePrice = new System.Data.DataColumn();
            this.Product_WaybItem_Price = new System.Data.DataColumn();
            this.Product_MeasureID = new System.Data.DataColumn();
            this.Product_MeasureName = new System.Data.DataColumn();
            this.Product_WaybItem_PriceInAccountingCurrency = new System.Data.DataColumn();
            this.Product_WaybItem_Guid = new System.Data.DataColumn();
            this.Product_WaybItem_LeavQuantity = new System.Data.DataColumn();
            this.Product_WaybillNum = new System.Data.DataColumn();
            this.Product_WaybillDate = new System.Data.DataColumn();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colBackWaybItem_Guid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEditProduct = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colMeasureID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBackWaybItems_MeasureName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSum = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSumInAccountingCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBackWaybItems_PartsName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBackWaybItems_PartsArticle = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBackWaybItems_QuantitySrc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWaybItem_Guid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBackWaybItems_Waybill_Num = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBackWaybItems_Waybill_BeginDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSpinEditDiscount = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.Stock = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.Depart = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.SalesMan = new DevExpress.XtraEditors.ComboBoxEdit();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.txtDescription = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl21 = new DevExpress.XtraEditors.LabelControl();
            this.Customer = new DevExpress.XtraEditors.ComboBoxEdit();
            this.ChildDepart = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.BeginDate = new DevExpress.XtraEditors.DateEdit();
            this.WaybilllNum = new DevExpress.XtraEditors.TextEdit();
            this.labelControl29 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl22 = new DevExpress.XtraEditors.LabelControl();
            this.WaybillShipMode = new DevExpress.XtraEditors.ComboBoxEdit();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.ShipDate = new DevExpress.XtraEditors.DateEdit();
            this.WaybillBackReason = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.WaybillState = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.PaymentType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.cboxProductTradeMark = new DevExpress.XtraEditors.ComboBoxEdit();
            this.pictureFilter = new DevExpress.XtraEditors.PictureEdit();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.cboxProductType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.controlNavigator = new DevExpress.XtraEditors.ControlNavigator();
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
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Stock.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Depart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesMan.Properties)).BeginInit();
            this.tableLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            this.tableLayoutPanel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Customer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChildDepart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BeginDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BeginDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaybilllNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaybillShipMode.Properties)).BeginInit();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ShipDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShipDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaybillBackReason.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaybillState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PaymentType.Properties)).BeginInit();
            this.tableLayoutPanel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboxProductTradeMark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxProductType.Properties)).BeginInit();
            this.tableLayoutPanel6.SuspendLayout();
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
            this.colPriceImporter.VisibleIndex = 3;
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
            this.colPrice.VisibleIndex = 4;
            this.colPrice.Width = 124;
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
            this.colPriceInAccountingCurrency.VisibleIndex = 6;
            this.colPriceInAccountingCurrency.Width = 68;
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
            this.mitmsExportToHTML.Click += new System.EventHandler(this.sbExportToHTML_Click);
            // 
            // mitmsExportToXML
            // 
            this.mitmsExportToXML.Name = "mitmsExportToXML";
            this.mitmsExportToXML.Size = new System.Drawing.Size(233, 22);
            this.mitmsExportToXML.Text = "в XML...";
            this.mitmsExportToXML.Click += new System.EventHandler(this.sbExportToXML_Click);
            // 
            // mitmsExportToXLS
            // 
            this.mitmsExportToXLS.Name = "mitmsExportToXLS";
            this.mitmsExportToXLS.Size = new System.Drawing.Size(233, 22);
            this.mitmsExportToXLS.Text = "в MS Excel...";
            this.mitmsExportToXLS.Click += new System.EventHandler(this.sbExportToXLS_Click);
            // 
            // mitmsExportToTXT
            // 
            this.mitmsExportToTXT.Name = "mitmsExportToTXT";
            this.mitmsExportToTXT.Size = new System.Drawing.Size(233, 22);
            this.mitmsExportToTXT.Text = "в TXT...";
            this.mitmsExportToTXT.Click += new System.EventHandler(this.sbExportToTXT_Click);
            // 
            // mitmsExportToDBF
            // 
            this.mitmsExportToDBF.Name = "mitmsExportToDBF";
            this.mitmsExportToDBF.Size = new System.Drawing.Size(233, 22);
            this.mitmsExportToDBF.Text = "в DBF (цена в рублях)...";
            this.mitmsExportToDBF.Click += new System.EventHandler(this.sbExportToDBF_Click);
            // 
            // mitmsExportToDBFCurrency
            // 
            this.mitmsExportToDBFCurrency.Name = "mitmsExportToDBFCurrency";
            this.mitmsExportToDBFCurrency.Size = new System.Drawing.Size(233, 22);
            this.mitmsExportToDBFCurrency.Text = "в DBF (цена в валюте учета)...";
            this.mitmsExportToDBFCurrency.Click += new System.EventHandler(this.sbExportToDBFCurrency_Click);
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
            this.tableLayoutPanelBackground.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 159F));
            this.tableLayoutPanelBackground.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanelBackground.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanelBackground.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanelBackground.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelBackground.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanelBackground.Size = new System.Drawing.Size(1078, 602);
            this.toolTipController.SetSuperTip(this.tableLayoutPanelBackground, null);
            this.tableLayoutPanelBackground.TabIndex = 2;
            // 
            // panelProgressBar
            // 
            this.panelProgressBar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelProgressBar.Controls.Add(this.progressBarControl1);
            this.panelProgressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelProgressBar.Location = new System.Drawing.Point(3, 162);
            this.panelProgressBar.Name = "panelProgressBar";
            this.panelProgressBar.Size = new System.Drawing.Size(1072, 26);
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
            this.gridControl.Location = new System.Drawing.Point(3, 246);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEditProduct,
            this.repositoryItemCalcEditOrderedQuantity,
            this.repositoryItemSpinEditDiscount});
            this.gridControl.Size = new System.Drawing.Size(1072, 322);
            this.gridControl.TabIndex = 23;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // dataSet
            // 
            this.dataSet.DataSetName = "dataSet";
            this.dataSet.Relations.AddRange(new System.Data.DataRelation[] {
            new System.Data.DataRelation("rlOrderItmsProduct", "Product", "OrderItems", new string[] {
                        "WaybItem_Guid"}, new string[] {
                        "WaybItem_Guid"}, false)});
            this.dataSet.Tables.AddRange(new System.Data.DataTable[] {
            this.OrderItems,
            this.Product});
            // 
            // OrderItems
            // 
            this.OrderItems.Columns.AddRange(new System.Data.DataColumn[] {
            this.BackWaybItem_Guid,
            this.ProductID,
            this.MeasureID,
            this.Quantity,
            this.PriceImporter,
            this.Price,
            this.PriceInAccountingCurrency,
            this.Sum,
            this.SumInAccountingCurrency,
            this.BackWaybItems_MeasureName,
            this.BackWaybItems_PartsArticle,
            this.BackWaybItems_PartsName,
            this.BackWaybItems_QuantitySrc,
            this.BackWaybItem_Id,
            this.WaybItem_Guid,
            this.BackWaybItems_Waybill_Num,
            this.BackWaybItems_Waybill_BeginDate});
            this.OrderItems.Constraints.AddRange(new System.Data.Constraint[] {
            new System.Data.ForeignKeyConstraint("rlOrderItmsProduct", "Product", new string[] {
                        "WaybItem_Guid"}, new string[] {
                        "WaybItem_Guid"}, System.Data.AcceptRejectRule.None, System.Data.Rule.None, System.Data.Rule.None)});
            this.OrderItems.TableName = "OrderItems";
            // 
            // BackWaybItem_Guid
            // 
            this.BackWaybItem_Guid.Caption = "ID";
            this.BackWaybItem_Guid.ColumnName = "BackWaybItem_Guid";
            this.BackWaybItem_Guid.DataType = typeof(System.Guid);
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
            // PriceInAccountingCurrency
            // 
            this.PriceInAccountingCurrency.Caption = "Отпускная цена в валюте учета";
            this.PriceInAccountingCurrency.ColumnName = "PriceInAccountingCurrency";
            this.PriceInAccountingCurrency.DataType = typeof(double);
            // 
            // Sum
            // 
            this.Sum.Caption = "Сумма";
            this.Sum.ColumnName = "Sum";
            this.Sum.DataType = typeof(double);
            this.Sum.Expression = "Price * Quantity";
            this.Sum.ReadOnly = true;
            // 
            // SumInAccountingCurrency
            // 
            this.SumInAccountingCurrency.Caption = "Сумма в валюте учета";
            this.SumInAccountingCurrency.ColumnName = "SumInAccountingCurrency";
            this.SumInAccountingCurrency.DataType = typeof(double);
            this.SumInAccountingCurrency.Expression = "PriceInAccountingCurrency * Quantity";
            this.SumInAccountingCurrency.ReadOnly = true;
            // 
            // BackWaybItems_MeasureName
            // 
            this.BackWaybItems_MeasureName.Caption = "Ед. изм.";
            this.BackWaybItems_MeasureName.ColumnName = "BackWaybItems_MeasureName";
            // 
            // BackWaybItems_PartsArticle
            // 
            this.BackWaybItems_PartsArticle.ColumnName = "BackWaybItems_PartsArticle";
            // 
            // BackWaybItems_PartsName
            // 
            this.BackWaybItems_PartsName.ColumnName = "BackWaybItems_PartsName";
            // 
            // BackWaybItems_QuantitySrc
            // 
            this.BackWaybItems_QuantitySrc.ColumnName = "BackWaybItems_QuantitySrc";
            this.BackWaybItems_QuantitySrc.DataType = typeof(decimal);
            // 
            // BackWaybItem_Id
            // 
            this.BackWaybItem_Id.ColumnName = "BackWaybItem_Id";
            this.BackWaybItem_Id.DataType = typeof(int);
            // 
            // WaybItem_Guid
            // 
            this.WaybItem_Guid.ColumnName = "WaybItem_Guid";
            this.WaybItem_Guid.DataType = typeof(System.Guid);
            // 
            // BackWaybItems_Waybill_Num
            // 
            this.BackWaybItems_Waybill_Num.Caption = "№ отгрузки";
            this.BackWaybItems_Waybill_Num.ColumnName = "BackWaybItems_Waybill_Num";
            // 
            // BackWaybItems_Waybill_BeginDate
            // 
            this.BackWaybItems_Waybill_BeginDate.Caption = "Дата отгрузки";
            this.BackWaybItems_Waybill_BeginDate.ColumnName = "BackWaybItems_Waybill_BeginDate";
            this.BackWaybItems_Waybill_BeginDate.DataType = typeof(System.DateTime);
            // 
            // Product
            // 
            this.Product.Columns.AddRange(new System.Data.DataColumn[] {
            this.Product_ProductGuid,
            this.Product_ProductFullName,
            this.Product_WaybItem_Quantity,
            this.Product_WaybItem_BasePrice,
            this.Product_WaybItem_Price,
            this.Product_MeasureID,
            this.Product_MeasureName,
            this.Product_WaybItem_PriceInAccountingCurrency,
            this.Product_WaybItem_Guid,
            this.Product_WaybItem_LeavQuantity,
            this.Product_WaybillNum,
            this.Product_WaybillDate});
            this.Product.Constraints.AddRange(new System.Data.Constraint[] {
            new System.Data.UniqueConstraint("Constraint1", new string[] {
                        "WaybItem_Guid"}, false)});
            this.Product.TableName = "Product";
            // 
            // Product_ProductGuid
            // 
            this.Product_ProductGuid.Caption = "Товар";
            this.Product_ProductGuid.ColumnName = "Product_ProductID";
            this.Product_ProductGuid.DataType = typeof(System.Guid);
            // 
            // Product_ProductFullName
            // 
            this.Product_ProductFullName.Caption = "Товар";
            this.Product_ProductFullName.ColumnName = "Product_ProductFullName";
            // 
            // Product_WaybItem_Quantity
            // 
            this.Product_WaybItem_Quantity.Caption = "Отгружено, шт.";
            this.Product_WaybItem_Quantity.ColumnName = "Product_WaybItem_Quantity";
            this.Product_WaybItem_Quantity.DataType = typeof(decimal);
            // 
            // Product_WaybItem_BasePrice
            // 
            this.Product_WaybItem_BasePrice.Caption = "Цена импортера";
            this.Product_WaybItem_BasePrice.ColumnName = "Product_WaybItem_BasePrice";
            this.Product_WaybItem_BasePrice.DataType = typeof(decimal);
            // 
            // Product_WaybItem_Price
            // 
            this.Product_WaybItem_Price.Caption = "Цена, руб.";
            this.Product_WaybItem_Price.ColumnName = "Product_WaybItem_Price";
            this.Product_WaybItem_Price.DataType = typeof(decimal);
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
            // Product_WaybItem_PriceInAccountingCurrency
            // 
            this.Product_WaybItem_PriceInAccountingCurrency.Caption = "Цена в вал. учета";
            this.Product_WaybItem_PriceInAccountingCurrency.ColumnName = "Product_WaybItem_PriceInAccountingCurrency";
            this.Product_WaybItem_PriceInAccountingCurrency.DataType = typeof(decimal);
            // 
            // Product_WaybItem_Guid
            // 
            this.Product_WaybItem_Guid.ColumnName = "WaybItem_Guid";
            this.Product_WaybItem_Guid.DataType = typeof(System.Guid);
            // 
            // Product_WaybItem_LeavQuantity
            // 
            this.Product_WaybItem_LeavQuantity.Caption = "К возврату, шт.";
            this.Product_WaybItem_LeavQuantity.ColumnName = "Product_WaybItem_LeavQuantity";
            this.Product_WaybItem_LeavQuantity.DataType = typeof(decimal);
            // 
            // Product_WaybillNum
            // 
            this.Product_WaybillNum.Caption = "№ отгрузки";
            this.Product_WaybillNum.ColumnName = "Product_WaybillNum";
            // 
            // Product_WaybillDate
            // 
            this.Product_WaybillDate.Caption = "Дата отгрузки";
            this.Product_WaybillDate.ColumnName = "Product_WaybillDate";
            this.Product_WaybillDate.DataType = typeof(System.DateTime);
            // 
            // gridView
            // 
            this.gridView.ColumnPanelRowHeight = 60;
            this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colBackWaybItem_Guid,
            this.colProductID,
            this.colMeasureID,
            this.colBackWaybItems_MeasureName,
            this.colQuantity,
            this.colPriceImporter,
            this.colPrice,
            this.colSum,
            this.colPriceInAccountingCurrency,
            this.colSumInAccountingCurrency,
            this.colBackWaybItems_PartsName,
            this.colBackWaybItems_PartsArticle,
            this.colBackWaybItems_QuantitySrc,
            this.colWaybItem_Guid,
            this.colBackWaybItems_Waybill_Num,
            this.colBackWaybItems_Waybill_BeginDate});
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
            styleFormatCondition3.Condition = DevExpress.XtraGrid.FormatConditionEnum.LessOrEqual;
            styleFormatCondition3.Value1 = "0";
            styleFormatCondition4.Appearance.BackColor = System.Drawing.Color.Red;
            styleFormatCondition4.Appearance.Options.UseBackColor = true;
            styleFormatCondition4.Column = this.colPrice;
            styleFormatCondition4.Condition = DevExpress.XtraGrid.FormatConditionEnum.LessOrEqual;
            styleFormatCondition4.Value1 = "0";
            styleFormatCondition5.Appearance.BackColor = System.Drawing.Color.Red;
            styleFormatCondition5.Appearance.Options.UseBackColor = true;
            styleFormatCondition5.Condition = DevExpress.XtraGrid.FormatConditionEnum.LessOrEqual;
            styleFormatCondition5.Value1 = "0";
            styleFormatCondition6.Appearance.BackColor = System.Drawing.Color.Red;
            styleFormatCondition6.Appearance.Options.UseBackColor = true;
            styleFormatCondition6.Column = this.colPriceInAccountingCurrency;
            styleFormatCondition6.Condition = DevExpress.XtraGrid.FormatConditionEnum.LessOrEqual;
            styleFormatCondition6.Value1 = "0";
            styleFormatCondition7.Appearance.BackColor = System.Drawing.Color.Red;
            styleFormatCondition7.Appearance.Options.UseBackColor = true;
            styleFormatCondition7.Condition = DevExpress.XtraGrid.FormatConditionEnum.LessOrEqual;
            styleFormatCondition7.Value1 = "0";
            styleFormatCondition8.Appearance.BackColor = System.Drawing.Color.Red;
            styleFormatCondition8.Appearance.Options.UseBackColor = true;
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
            // colBackWaybItem_Guid
            // 
            this.colBackWaybItem_Guid.AppearanceHeader.Options.UseTextOptions = true;
            this.colBackWaybItem_Guid.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colBackWaybItem_Guid.Caption = "BackWaybItem_Guid";
            this.colBackWaybItem_Guid.FieldName = "BackWaybItem_Guid";
            this.colBackWaybItem_Guid.Name = "colBackWaybItem_Guid";
            // 
            // colProductID
            // 
            this.colProductID.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductID.Caption = "Наименование товара";
            this.colProductID.ColumnEdit = this.repositoryItemLookUpEditProduct;
            this.colProductID.FieldName = "WaybItem_Guid";
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
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Product_ProductFullName", "Наименование товара", 127),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Product_WaybItem_Quantity", "К возврату", 30, DevExpress.Utils.FormatType.Numeric, "### ### ##0", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Product_WaybItem_Price", "Цена", 30, DevExpress.Utils.FormatType.Numeric, "### ### ##0.00", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Product_WaybillNum", "№ отгрузки", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Product_WaybillDate", "Дата отгрузки", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None)});
            this.repositoryItemLookUpEditProduct.DisplayMember = "Product_ProductFullName";
            this.repositoryItemLookUpEditProduct.DropDownRows = 10;
            this.repositoryItemLookUpEditProduct.Name = "repositoryItemLookUpEditProduct";
            this.repositoryItemLookUpEditProduct.NullText = "";
            this.repositoryItemLookUpEditProduct.PopupFormMinSize = new System.Drawing.Size(500, 0);
            this.repositoryItemLookUpEditProduct.PopupWidth = 500;
            this.repositoryItemLookUpEditProduct.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.repositoryItemLookUpEditProduct.ValidateOnEnterKey = true;
            this.repositoryItemLookUpEditProduct.ValueMember = "WaybItem_Guid";
            this.repositoryItemLookUpEditProduct.CloseUp += new DevExpress.XtraEditors.Controls.CloseUpEventHandler(this.repositoryItemLookUpEditProduct_CloseUp);
            // 
            // colMeasureID
            // 
            this.colMeasureID.Caption = "MeasureID";
            this.colMeasureID.FieldName = "MeasureID";
            this.colMeasureID.Name = "colMeasureID";
            // 
            // colBackWaybItems_MeasureName
            // 
            this.colBackWaybItems_MeasureName.AppearanceHeader.Options.UseTextOptions = true;
            this.colBackWaybItems_MeasureName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBackWaybItems_MeasureName.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colBackWaybItems_MeasureName.Caption = "Ед изм.";
            this.colBackWaybItems_MeasureName.FieldName = "BackWaybItems_MeasureName";
            this.colBackWaybItems_MeasureName.Name = "colBackWaybItems_MeasureName";
            this.colBackWaybItems_MeasureName.OptionsColumn.AllowEdit = false;
            this.colBackWaybItems_MeasureName.OptionsColumn.ReadOnly = true;
            this.colBackWaybItems_MeasureName.Visible = true;
            this.colBackWaybItems_MeasureName.VisibleIndex = 1;
            this.colBackWaybItems_MeasureName.Width = 48;
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
            this.colSum.VisibleIndex = 5;
            this.colSum.Width = 136;
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
            this.colSumInAccountingCurrency.VisibleIndex = 7;
            this.colSumInAccountingCurrency.Width = 100;
            // 
            // colBackWaybItems_PartsName
            // 
            this.colBackWaybItems_PartsName.Caption = "colBackWaybItems_PartsName";
            this.colBackWaybItems_PartsName.FieldName = "BackWaybItems_PartsName";
            this.colBackWaybItems_PartsName.Name = "colBackWaybItems_PartsName";
            // 
            // colBackWaybItems_PartsArticle
            // 
            this.colBackWaybItems_PartsArticle.Caption = "colBackWaybItems_PartsArticle";
            this.colBackWaybItems_PartsArticle.FieldName = "BackWaybItems_PartsArticle";
            this.colBackWaybItems_PartsArticle.Name = "colBackWaybItems_PartsArticle";
            // 
            // colBackWaybItems_QuantitySrc
            // 
            this.colBackWaybItems_QuantitySrc.Caption = "colBackWaybItems_QuantitySrc";
            this.colBackWaybItems_QuantitySrc.FieldName = "BackWaybItems_QuantitySrc";
            this.colBackWaybItems_QuantitySrc.Name = "colBackWaybItems_QuantitySrc";
            // 
            // colWaybItem_Guid
            // 
            this.colWaybItem_Guid.Caption = "colWaybItem_Guid";
            this.colWaybItem_Guid.FieldName = "WaybItem_Guid";
            this.colWaybItem_Guid.Name = "colWaybItem_Guid";
            // 
            // colBackWaybItems_Waybill_Num
            // 
            this.colBackWaybItems_Waybill_Num.Caption = "№ отгрузки";
            this.colBackWaybItems_Waybill_Num.FieldName = "BackWaybItems_Waybill_Num";
            this.colBackWaybItems_Waybill_Num.Name = "colBackWaybItems_Waybill_Num";
            this.colBackWaybItems_Waybill_Num.OptionsColumn.AllowEdit = false;
            this.colBackWaybItems_Waybill_Num.OptionsColumn.ReadOnly = true;
            this.colBackWaybItems_Waybill_Num.Visible = true;
            this.colBackWaybItems_Waybill_Num.VisibleIndex = 8;
            this.colBackWaybItems_Waybill_Num.Width = 80;
            // 
            // colBackWaybItems_Waybill_BeginDate
            // 
            this.colBackWaybItems_Waybill_BeginDate.Caption = "Дата отгрузки";
            this.colBackWaybItems_Waybill_BeginDate.DisplayFormat.FormatString = "d";
            this.colBackWaybItems_Waybill_BeginDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colBackWaybItems_Waybill_BeginDate.FieldName = "BackWaybItems_Waybill_BeginDate";
            this.colBackWaybItems_Waybill_BeginDate.Name = "colBackWaybItems_Waybill_BeginDate";
            this.colBackWaybItems_Waybill_BeginDate.OptionsColumn.AllowEdit = false;
            this.colBackWaybItems_Waybill_BeginDate.OptionsColumn.ReadOnly = true;
            this.colBackWaybItems_Waybill_BeginDate.Visible = true;
            this.colBackWaybItems_Waybill_BeginDate.VisibleIndex = 9;
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
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel7, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel9, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel5, 0, 1);
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
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1078, 159);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel2, null);
            this.tableLayoutPanel2.TabIndex = 17;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 7;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 112F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 138F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 205F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.Stock, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.labelControl9, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.labelControl8, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.Depart, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.labelControl11, 4, 0);
            this.tableLayoutPanel4.Controls.Add(this.SalesMan, 5, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 50);
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
            this.Depart.Location = new System.Drawing.Point(393, 3);
            this.Depart.Name = "Depart";
            this.Depart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Depart.Properties.PopupSizeable = true;
            this.Depart.Properties.Sorted = true;
            this.Depart.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.Depart.Size = new System.Drawing.Size(106, 20);
            this.Depart.TabIndex = 2;
            this.Depart.ToolTip = "Подразделение";
            this.Depart.ToolTipController = this.toolTipController;
            this.Depart.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.Depart.SelectedValueChanged += new System.EventHandler(this.cboxOrderPropertie_SelectedValueChanged);
            // 
            // labelControl11
            // 
            this.labelControl11.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl11.Location = new System.Drawing.Point(505, 6);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(134, 13);
            this.labelControl11.TabIndex = 25;
            this.labelControl11.Text = "Торговый представитель:";
            // 
            // SalesMan
            // 
            this.SalesMan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.SalesMan.Location = new System.Drawing.Point(643, 3);
            this.SalesMan.Name = "SalesMan";
            this.SalesMan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.SalesMan.Properties.PopupSizeable = true;
            this.SalesMan.Properties.Sorted = true;
            this.SalesMan.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.SalesMan.Size = new System.Drawing.Size(199, 20);
            this.SalesMan.TabIndex = 3;
            this.SalesMan.ToolTip = "Торговый представитель";
            this.SalesMan.ToolTipController = this.toolTipController;
            this.SalesMan.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.SalesMan.SelectedValueChanged += new System.EventHandler(this.cboxOrderPropertie_SelectedValueChanged);
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Controls.Add(this.txtDescription, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.labelControl12, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(0, 75);
            this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(1078, 84);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel7, null);
            this.tableLayoutPanel7.TabIndex = 4;
            // 
            // txtDescription
            // 
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescription.Location = new System.Drawing.Point(103, 3);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(972, 78);
            this.txtDescription.TabIndex = 26;
            this.txtDescription.ToolTip = "Примечание";
            this.txtDescription.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.txtDescription.EditValueChanged += new System.EventHandler(this.cboxOrderPropertie_SelectedValueChanged);
            this.txtDescription.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.txtOrderPropertie_EditValueChanging);
            // 
            // labelControl12
            // 
            this.labelControl12.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl12.Location = new System.Drawing.Point(3, 35);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(65, 13);
            this.labelControl12.TabIndex = 25;
            this.labelControl12.Text = "Примечание:";
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 12;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 101F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 141F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel9.Controls.Add(this.labelControl10, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this.labelControl21, 9, 0);
            this.tableLayoutPanel9.Controls.Add(this.Customer, 5, 0);
            this.tableLayoutPanel9.Controls.Add(this.ChildDepart, 7, 0);
            this.tableLayoutPanel9.Controls.Add(this.labelControl1, 6, 0);
            this.tableLayoutPanel9.Controls.Add(this.labelControl3, 4, 0);
            this.tableLayoutPanel9.Controls.Add(this.BeginDate, 1, 0);
            this.tableLayoutPanel9.Controls.Add(this.WaybilllNum, 3, 0);
            this.tableLayoutPanel9.Controls.Add(this.labelControl29, 2, 0);
            this.tableLayoutPanel9.Controls.Add(this.labelControl22, 10, 0);
            this.tableLayoutPanel9.Controls.Add(this.WaybillShipMode, 11, 0);
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
            // labelControl10
            // 
            this.labelControl10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl10.Location = new System.Drawing.Point(3, 6);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(80, 13);
            this.labelControl10.TabIndex = 20;
            this.labelControl10.Text = "Дата возврата:";
            // 
            // labelControl21
            // 
            this.labelControl21.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl21.Location = new System.Drawing.Point(876, 6);
            this.labelControl21.Name = "labelControl21";
            this.labelControl21.Size = new System.Drawing.Size(79, 13);
            this.labelControl21.TabIndex = 27;
            this.labelControl21.Text = "Дата отгрузки:";
            this.labelControl21.Visible = false;
            // 
            // Customer
            // 
            this.Customer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Customer.Location = new System.Drawing.Point(477, 3);
            this.Customer.Name = "Customer";
            this.Customer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Customer.Properties.PopupSizeable = true;
            this.Customer.Properties.Sorted = true;
            this.Customer.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.Customer.Size = new System.Drawing.Size(140, 20);
            this.Customer.TabIndex = 27;
            this.Customer.ToolTip = "Клиент";
            this.Customer.ToolTipController = this.toolTipController;
            this.Customer.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.Customer.SelectedValueChanged += new System.EventHandler(this.cboxOrderPropertie_SelectedValueChanged);
            // 
            // ChildDepart
            // 
            this.ChildDepart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ChildDepart.Location = new System.Drawing.Point(723, 3);
            this.ChildDepart.Name = "ChildDepart";
            this.ChildDepart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ChildDepart.Properties.PopupSizeable = true;
            this.ChildDepart.Properties.Sorted = true;
            this.ChildDepart.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.ChildDepart.Size = new System.Drawing.Size(119, 20);
            this.ChildDepart.TabIndex = 29;
            this.ChildDepart.ToolTip = "Дочернее подразделение клиента";
            this.ChildDepart.ToolTipController = this.toolTipController;
            this.ChildDepart.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.ChildDepart.SelectedValueChanged += new System.EventHandler(this.cboxOrderPropertie_SelectedValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl1.Location = new System.Drawing.Point(623, 6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(93, 13);
            this.labelControl1.TabIndex = 28;
            this.labelControl1.Text = "Дочерний клиент:";
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl3.Location = new System.Drawing.Point(430, 6);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(41, 13);
            this.labelControl3.TabIndex = 26;
            this.labelControl3.Text = "Клиент:";
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
            // WaybilllNum
            // 
            this.WaybilllNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.WaybilllNum.Location = new System.Drawing.Point(289, 3);
            this.WaybilllNum.Name = "WaybilllNum";
            this.WaybilllNum.Properties.Appearance.Options.UseTextOptions = true;
            this.WaybilllNum.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.WaybilllNum.Size = new System.Drawing.Size(135, 20);
            this.WaybilllNum.TabIndex = 31;
            this.WaybilllNum.EditValueChanged += new System.EventHandler(this.cboxOrderPropertie_SelectedValueChanged);
            this.WaybilllNum.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.txtOrderPropertie_EditValueChanging);
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
            // labelControl22
            // 
            this.labelControl22.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl22.Location = new System.Drawing.Point(905, 6);
            this.labelControl22.Name = "labelControl22";
            this.labelControl22.Size = new System.Drawing.Size(72, 13);
            this.labelControl22.TabIndex = 29;
            this.labelControl22.Text = "Вид отгрузки:";
            this.labelControl22.Visible = false;
            // 
            // WaybillShipMode
            // 
            this.WaybillShipMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.WaybillShipMode.Location = new System.Drawing.Point(981, 3);
            this.WaybillShipMode.Name = "WaybillShipMode";
            this.WaybillShipMode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.WaybillShipMode.Properties.PopupSizeable = true;
            this.WaybillShipMode.Properties.Sorted = true;
            this.WaybillShipMode.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.WaybillShipMode.Size = new System.Drawing.Size(94, 20);
            this.WaybillShipMode.TabIndex = 30;
            this.WaybillShipMode.ToolTip = "Вид отгрузки";
            this.WaybillShipMode.ToolTipController = this.toolTipController;
            this.WaybillShipMode.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.WaybillShipMode.Visible = false;
            this.WaybillShipMode.SelectedValueChanged += new System.EventHandler(this.cboxOrderPropertie_SelectedValueChanged);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 7;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 112F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 138F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 205F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.ShipDate, 6, 0);
            this.tableLayoutPanel5.Controls.Add(this.WaybillBackReason, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.labelControl5, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.WaybillState, 5, 0);
            this.tableLayoutPanel5.Controls.Add(this.labelControl2, 4, 0);
            this.tableLayoutPanel5.Controls.Add(this.PaymentType, 3, 0);
            this.tableLayoutPanel5.Controls.Add(this.labelControl4, 2, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1078, 25);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel5, null);
            this.tableLayoutPanel5.TabIndex = 6;
            // 
            // ShipDate
            // 
            this.ShipDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ShipDate.EditValue = null;
            this.ShipDate.Location = new System.Drawing.Point(848, 3);
            this.ShipDate.Name = "ShipDate";
            this.ShipDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ShipDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.ShipDate.Size = new System.Drawing.Size(227, 20);
            this.ShipDate.TabIndex = 28;
            this.ShipDate.ToolTip = "Дата отгрузки";
            this.ShipDate.ToolTipController = this.toolTipController;
            this.ShipDate.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.ShipDate.Visible = false;
            this.ShipDate.EditValueChanged += new System.EventHandler(this.cboxOrderPropertie_SelectedValueChanged);
            this.ShipDate.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.txtOrderPropertie_EditValueChanging);
            // 
            // WaybillBackReason
            // 
            this.WaybillBackReason.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.WaybillBackReason.Location = new System.Drawing.Point(103, 3);
            this.WaybillBackReason.Name = "WaybillBackReason";
            this.WaybillBackReason.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.WaybillBackReason.Properties.PopupSizeable = true;
            this.WaybillBackReason.Properties.Sorted = true;
            this.WaybillBackReason.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.WaybillBackReason.Size = new System.Drawing.Size(194, 20);
            this.WaybillBackReason.TabIndex = 5;
            this.WaybillBackReason.ToolTip = "Причина возврата";
            this.WaybillBackReason.ToolTipController = this.toolTipController;
            this.WaybillBackReason.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            // 
            // labelControl5
            // 
            this.labelControl5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl5.Location = new System.Drawing.Point(3, 6);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(97, 13);
            this.labelControl5.TabIndex = 4;
            this.labelControl5.Text = "Причина возврата:";
            // 
            // WaybillState
            // 
            this.WaybillState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.WaybillState.Location = new System.Drawing.Point(643, 3);
            this.WaybillState.Name = "WaybillState";
            this.WaybillState.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.WaybillState.Properties.PopupSizeable = true;
            this.WaybillState.Properties.Sorted = true;
            this.WaybillState.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.WaybillState.Size = new System.Drawing.Size(199, 20);
            this.WaybillState.TabIndex = 3;
            this.WaybillState.ToolTip = "Состояние накладной";
            this.WaybillState.ToolTipController = this.toolTipController;
            this.WaybillState.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.WaybillState.SelectedValueChanged += new System.EventHandler(this.cboxOrderPropertie_SelectedValueChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl2.Location = new System.Drawing.Point(505, 6);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(116, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Состояние документа:";
            // 
            // PaymentType
            // 
            this.PaymentType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.PaymentType.Location = new System.Drawing.Point(393, 3);
            this.PaymentType.Name = "PaymentType";
            this.PaymentType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.PaymentType.Properties.PopupSizeable = true;
            this.PaymentType.Properties.Sorted = true;
            this.PaymentType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.PaymentType.Size = new System.Drawing.Size(106, 20);
            this.PaymentType.TabIndex = 5;
            this.PaymentType.ToolTip = "Форма оплаты";
            this.PaymentType.ToolTipController = this.toolTipController;
            this.PaymentType.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.PaymentType.SelectedValueChanged += new System.EventHandler(this.cboxOrderPropertie_SelectedValueChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl4.Location = new System.Drawing.Point(303, 6);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(77, 13);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "Форма оплаты:";
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
            this.tableLayoutPanel8.Location = new System.Drawing.Point(0, 191);
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
            this.tableLayoutPanel6.Controls.Add(this.controlNavigator, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 219);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(1078, 24);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel6, null);
            this.tableLayoutPanel6.TabIndex = 24;
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
            // openFileDialog
            // 
            this.openFileDialog.Filter = "MS Excel 2010 files (*.xlsx)|*.xlsx|MS Excel 2003 files (*.xls)|*.xls|All files (" +
    "*.*)|*.*";
            // 
            // ctrlBackWaybillEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanelBackground);
            this.Name = "ctrlBackWaybillEditor";
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
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Stock.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Depart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SalesMan.Properties)).EndInit();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Customer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChildDepart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BeginDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BeginDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaybilllNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaybillShipMode.Properties)).EndInit();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ShipDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShipDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaybillBackReason.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaybillState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PaymentType.Properties)).EndInit();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboxProductTradeMark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxProductType.Properties)).EndInit();
            this.tableLayoutPanel6.ResumeLayout(false);
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
        private System.Data.DataColumn BackWaybItem_Guid;
        private System.Data.DataColumn ProductID;
        private System.Data.DataColumn MeasureID;
        private System.Data.DataColumn Quantity;
        private System.Data.DataColumn PriceImporter;
        private System.Data.DataColumn Price;
        private System.Data.DataColumn PriceInAccountingCurrency;
        private System.Data.DataColumn Sum;
        private System.Data.DataColumn SumInAccountingCurrency;
        private System.Data.DataColumn BackWaybItems_MeasureName;
        private System.Data.DataColumn BackWaybItems_PartsArticle;
        private System.Data.DataColumn BackWaybItems_PartsName;
        private System.Data.DataColumn BackWaybItems_QuantitySrc;
        private System.Data.DataColumn BackWaybItem_Id;
        private System.Data.DataColumn WaybItem_Guid;
        private System.Data.DataTable Product;
        private System.Data.DataColumn Product_ProductGuid;
        private System.Data.DataColumn Product_ProductFullName;
        private System.Data.DataColumn Product_WaybItem_Quantity;
        private System.Data.DataColumn Product_WaybItem_BasePrice;
        private System.Data.DataColumn Product_WaybItem_Price;
        private System.Data.DataColumn Product_MeasureID;
        private System.Data.DataColumn Product_MeasureName;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelBackground;
        private DevExpress.XtraEditors.PanelControl panelProgressBar;
        private DevExpress.XtraEditors.ProgressBarControl progressBarControl1;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraGrid.Columns.GridColumn colBackWaybItem_Guid;
        private DevExpress.XtraGrid.Columns.GridColumn colProductID;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEditProduct;
        private DevExpress.XtraGrid.Columns.GridColumn colMeasureID;
        private DevExpress.XtraGrid.Columns.GridColumn colBackWaybItems_MeasureName;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantity;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit repositoryItemCalcEditOrderedQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colPriceImporter;
        private DevExpress.XtraGrid.Columns.GridColumn colPrice;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditDiscount;
        private DevExpress.XtraGrid.Columns.GridColumn colSum;
        private DevExpress.XtraGrid.Columns.GridColumn colPriceInAccountingCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colSumInAccountingCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colBackWaybItems_PartsName;
        private DevExpress.XtraGrid.Columns.GridColumn colBackWaybItems_PartsArticle;
        private DevExpress.XtraGrid.Columns.GridColumn colBackWaybItems_QuantitySrc;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnEdit;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private DevExpress.XtraEditors.ComboBoxEdit Stock;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.ComboBoxEdit Depart;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.ComboBoxEdit SalesMan;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.ComboBoxEdit Customer;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit ChildDepart;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private DevExpress.XtraEditors.MemoEdit txtDescription;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private DevExpress.XtraEditors.DateEdit ShipDate;
        private DevExpress.XtraEditors.ComboBoxEdit WaybillState;
        private DevExpress.XtraEditors.LabelControl labelControl21;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.DateEdit BeginDate;
        private DevExpress.XtraEditors.TextEdit WaybilllNum;
        private DevExpress.XtraEditors.LabelControl labelControl29;
        private DevExpress.XtraEditors.ComboBoxEdit PaymentType;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl22;
        private DevExpress.XtraEditors.ComboBoxEdit WaybillShipMode;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraEditors.ComboBoxEdit cboxProductTradeMark;
        private DevExpress.XtraEditors.PictureEdit pictureFilter;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        private DevExpress.XtraEditors.ComboBoxEdit cboxProductType;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private DevExpress.XtraEditors.ControlNavigator controlNavigator;
        private System.Data.DataColumn Product_WaybItem_PriceInAccountingCurrency;
        private System.Data.DataColumn Product_WaybItem_Guid;
        private System.Data.DataColumn Product_WaybItem_LeavQuantity;
        private System.Data.DataColumn Product_WaybillNum;
        private System.Data.DataColumn Product_WaybillDate;
        private DevExpress.XtraGrid.Columns.GridColumn colWaybItem_Guid;
        private System.Data.DataColumn BackWaybItems_Waybill_Num;
        private System.Data.DataColumn BackWaybItems_Waybill_BeginDate;
        private DevExpress.XtraGrid.Columns.GridColumn colBackWaybItems_Waybill_Num;
        private DevExpress.XtraGrid.Columns.GridColumn colBackWaybItems_Waybill_BeginDate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private DevExpress.XtraEditors.ComboBoxEdit WaybillBackReason;
        private DevExpress.XtraEditors.LabelControl labelControl5;
    }
}

namespace ERPMercuryProcessingOrder
{
    partial class frmOrderList
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOrderList));
            this.toolTipController = new DevExpress.Utils.ToolTipController(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl = new DevExpress.XtraTab.XtraTabControl();
            this.tabPageViewer = new DevExpress.XtraTab.XtraTabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.panelControlSearchCondition = new DevExpress.XtraEditors.PanelControl();
            this.barBtnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.cboxPaymentType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.cboxStock = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.cboxCompany = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cboxCustomer = new DevExpress.XtraEditors.ComboBoxEdit();
            this.dtEndDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.dtBeginDate = new DevExpress.XtraEditors.DateEdit();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.txtRtt = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtSupplBeginDate = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtOrderState = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtCustomer = new DevExpress.XtraEditors.TextEdit();
            this.txtStock = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtSalesMan = new DevExpress.XtraEditors.TextEdit();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.txtPaymentType = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtChildDepartCode = new DevExpress.XtraEditors.TextEdit();
            this.txtSupplNumVersion = new DevExpress.XtraEditors.TextEdit();
            this.txtDeliveryAddress = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.txtSupplDescrpn = new DevExpress.XtraEditors.MemoEdit();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.txtSupplOrderQty = new DevExpress.XtraEditors.TextEdit();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSupplAllDiscountCurrencyPrice = new DevExpress.XtraEditors.TextEdit();
            this.txtSupplAllCurrencyDiscount = new DevExpress.XtraEditors.TextEdit();
            this.txtSupplAllCurrencyPrice = new DevExpress.XtraEditors.TextEdit();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSupplAllDiscountPrice = new DevExpress.XtraEditors.TextEdit();
            this.txtSupplAllDiscount = new DevExpress.XtraEditors.TextEdit();
            this.txtSupplAllPrice = new DevExpress.XtraEditors.TextEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureEdit = new DevExpress.XtraEditors.PictureEdit();
            this.txtSupplQty = new DevExpress.XtraEditors.TextEdit();
            this.txtSupplPositionCount = new DevExpress.XtraEditors.TextEdit();
            this.txtSupplNum = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gridControlAgreementList = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuCalcPrice = new System.Windows.Forms.ToolStripMenuItem();
            this.menuClearPrices = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReturnSupplStateToAutoProcessPrices = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMakeSupplDeleted = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuEventLog = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuCreateOrderBlank = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuTransformSupplToWaybill = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGoToWaybill = new System.Windows.Forms.ToolStripMenuItem();
            this.gridViewAgreementList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.imageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.barBtnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.barbtnImportProduct = new DevExpress.XtraEditors.SimpleButton();
            this.barBtnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.barBtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.barBtnCopy = new DevExpress.XtraEditors.SimpleButton();
            this.tabPageEditor = new DevExpress.XtraTab.XtraTabPage();
            this.tableLayoutPanelAgreementEditor = new System.Windows.Forms.TableLayoutPanel();
            this.tabPageWaybill = new DevExpress.XtraTab.XtraTabPage();
            this.tableLayoutPanelWaybillEditor = new System.Windows.Forms.TableLayoutPanel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPageViewer.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlSearchCondition)).BeginInit();
            this.panelControlSearchCondition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboxPaymentType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxStock.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEndDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEndDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtBeginDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtBeginDate.Properties)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRtt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSupplBeginDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStock.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSalesMan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPaymentType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChildDepartCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSupplNumVersion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDeliveryAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSupplDescrpn.Properties)).BeginInit();
            this.tableLayoutPanel6.SuspendLayout();
            this.pnlInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSupplOrderQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSupplAllDiscountCurrencyPrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSupplAllCurrencyDiscount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSupplAllCurrencyPrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSupplAllDiscountPrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSupplAllDiscount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSupplAllPrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSupplQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSupplPositionCount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSupplNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlAgreementList)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewAgreementList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection)).BeginInit();
            this.tableLayoutPanel5.SuspendLayout();
            this.tabPageEditor.SuspendLayout();
            this.tabPageWaybill.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tabControl, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(989, 639);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel1, null);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // tabControl
            // 
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(3, 3);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedTabPage = this.tabPageViewer;
            this.tabControl.Size = new System.Drawing.Size(983, 633);
            this.tabControl.TabIndex = 0;
            this.tabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPageViewer,
            this.tabPageEditor,
            this.tabPageWaybill});
            this.tabControl.Text = "xtraTabControl1";
            this.tabControl.ToolTipController = this.toolTipController;
            // 
            // tabPageViewer
            // 
            this.tabPageViewer.Controls.Add(this.tableLayoutPanel4);
            this.tabPageViewer.Name = "tabPageViewer";
            this.tabPageViewer.Size = new System.Drawing.Size(974, 602);
            this.tabPageViewer.Text = "tabPageViewer";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.panelControlSearchCondition, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(974, 602);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel4, null);
            this.tableLayoutPanel4.TabIndex = 2;
            // 
            // panelControlSearchCondition
            // 
            this.panelControlSearchCondition.Controls.Add(this.barBtnRefresh);
            this.panelControlSearchCondition.Controls.Add(this.labelControl14);
            this.panelControlSearchCondition.Controls.Add(this.cboxPaymentType);
            this.panelControlSearchCondition.Controls.Add(this.labelControl12);
            this.panelControlSearchCondition.Controls.Add(this.cboxStock);
            this.panelControlSearchCondition.Controls.Add(this.labelControl11);
            this.panelControlSearchCondition.Controls.Add(this.labelControl10);
            this.panelControlSearchCondition.Controls.Add(this.cboxCompany);
            this.panelControlSearchCondition.Controls.Add(this.cboxCustomer);
            this.panelControlSearchCondition.Controls.Add(this.dtEndDate);
            this.panelControlSearchCondition.Controls.Add(this.labelControl9);
            this.panelControlSearchCondition.Controls.Add(this.labelControl8);
            this.panelControlSearchCondition.Controls.Add(this.dtBeginDate);
            this.panelControlSearchCondition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControlSearchCondition.Location = new System.Drawing.Point(1, 32);
            this.panelControlSearchCondition.Margin = new System.Windows.Forms.Padding(1);
            this.panelControlSearchCondition.Name = "panelControlSearchCondition";
            this.panelControlSearchCondition.Size = new System.Drawing.Size(972, 53);
            this.toolTipController.SetSuperTip(this.panelControlSearchCondition, null);
            this.panelControlSearchCondition.TabIndex = 1;
            // 
            // barBtnRefresh
            // 
            this.barBtnRefresh.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.barBtnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("barBtnRefresh.Image")));
            this.barBtnRefresh.ImageAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.barBtnRefresh.Location = new System.Drawing.Point(7, 14);
            this.barBtnRefresh.Margin = new System.Windows.Forms.Padding(1);
            this.barBtnRefresh.Name = "barBtnRefresh";
            this.barBtnRefresh.Size = new System.Drawing.Size(35, 35);
            this.barBtnRefresh.TabIndex = 8;
            this.barBtnRefresh.ToolTip = "Обновить список заказов";
            this.barBtnRefresh.ToolTipController = this.toolTipController;
            this.barBtnRefresh.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.barBtnRefresh.Click += new System.EventHandler(this.barBtnRefresh_Click);
            // 
            // labelControl14
            // 
            this.labelControl14.Location = new System.Drawing.Point(576, 5);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(73, 13);
            this.labelControl14.TabIndex = 11;
            this.labelControl14.Text = "Форма оплаты";
            // 
            // cboxPaymentType
            // 
            this.cboxPaymentType.Location = new System.Drawing.Point(576, 21);
            this.cboxPaymentType.Name = "cboxPaymentType";
            this.cboxPaymentType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboxPaymentType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboxPaymentType.Size = new System.Drawing.Size(133, 20);
            this.cboxPaymentType.TabIndex = 5;
            this.cboxPaymentType.ToolTip = "Форма оплаты";
            this.cboxPaymentType.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.cboxPaymentType.SelectedValueChanged += new System.EventHandler(this.cboxStock_SelectedValueChanged);
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(408, 4);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(32, 13);
            this.labelControl12.TabIndex = 9;
            this.labelControl12.Text = "Склад";
            // 
            // cboxStock
            // 
            this.cboxStock.Location = new System.Drawing.Point(408, 21);
            this.cboxStock.Name = "cboxStock";
            this.cboxStock.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboxStock.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboxStock.Size = new System.Drawing.Size(162, 20);
            this.cboxStock.TabIndex = 4;
            this.cboxStock.ToolTip = "Склад отгрузки";
            this.cboxStock.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.cboxStock.SelectedValueChanged += new System.EventHandler(this.cboxStock_SelectedValueChanged);
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(715, 5);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(37, 13);
            this.labelControl11.TabIndex = 7;
            this.labelControl11.Text = "Клиент";
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(267, 5);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(49, 13);
            this.labelControl10.TabIndex = 6;
            this.labelControl10.Text = "Компания";
            // 
            // cboxCompany
            // 
            this.cboxCompany.Location = new System.Drawing.Point(267, 21);
            this.cboxCompany.Name = "cboxCompany";
            this.cboxCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboxCompany.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboxCompany.Size = new System.Drawing.Size(133, 20);
            this.cboxCompany.TabIndex = 3;
            this.cboxCompany.ToolTip = "Компания";
            this.cboxCompany.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.cboxCompany.SelectedValueChanged += new System.EventHandler(this.cboxCompany_SelectedValueChanged);
            this.cboxCompany.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtBeginDate_KeyDown);
            // 
            // cboxCustomer
            // 
            this.cboxCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboxCustomer.Location = new System.Drawing.Point(715, 20);
            this.cboxCustomer.Name = "cboxCustomer";
            this.cboxCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboxCustomer.Size = new System.Drawing.Size(251, 20);
            this.cboxCustomer.TabIndex = 6;
            this.cboxCustomer.ToolTip = "Клиент";
            this.cboxCustomer.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.cboxCustomer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtBeginDate_KeyDown);
            // 
            // dtEndDate
            // 
            this.dtEndDate.EditValue = null;
            this.dtEndDate.Location = new System.Drawing.Point(160, 21);
            this.dtEndDate.Name = "dtEndDate";
            this.dtEndDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtEndDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtEndDate.Size = new System.Drawing.Size(100, 20);
            this.dtEndDate.TabIndex = 2;
            this.dtEndDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtBeginDate_KeyDown);
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(160, 5);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(12, 13);
            this.labelControl9.TabIndex = 2;
            this.labelControl9.Text = "по";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(54, 5);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(71, 13);
            this.labelControl8.TabIndex = 0;
            this.labelControl8.Text = "Дата заказа с";
            // 
            // dtBeginDate
            // 
            this.dtBeginDate.EditValue = null;
            this.dtBeginDate.Location = new System.Drawing.Point(54, 21);
            this.dtBeginDate.Name = "dtBeginDate";
            this.dtBeginDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtBeginDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtBeginDate.Size = new System.Drawing.Size(100, 20);
            this.dtBeginDate.TabIndex = 1;
            this.dtBeginDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtBeginDate_KeyDown);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 286F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel6, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(1, 87);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 514F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(972, 514);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel2, null);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.labelControl16, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.labelControl15, 0, 18);
            this.tableLayoutPanel3.Controls.Add(this.txtRtt, 0, 17);
            this.tableLayoutPanel3.Controls.Add(this.labelControl2, 0, 16);
            this.tableLayoutPanel3.Controls.Add(this.labelControl4, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.txtSupplBeginDate, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.labelControl6, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.txtOrderState, 0, 5);
            this.tableLayoutPanel3.Controls.Add(this.labelControl1, 0, 6);
            this.tableLayoutPanel3.Controls.Add(this.txtCustomer, 0, 7);
            this.tableLayoutPanel3.Controls.Add(this.txtStock, 0, 11);
            this.tableLayoutPanel3.Controls.Add(this.labelControl5, 0, 10);
            this.tableLayoutPanel3.Controls.Add(this.labelControl7, 0, 8);
            this.tableLayoutPanel3.Controls.Add(this.txtSalesMan, 0, 9);
            this.tableLayoutPanel3.Controls.Add(this.labelControl13, 0, 12);
            this.tableLayoutPanel3.Controls.Add(this.txtPaymentType, 0, 13);
            this.tableLayoutPanel3.Controls.Add(this.labelControl3, 0, 14);
            this.tableLayoutPanel3.Controls.Add(this.txtChildDepartCode, 0, 15);
            this.tableLayoutPanel3.Controls.Add(this.txtSupplNumVersion, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.txtDeliveryAddress, 0, 19);
            this.tableLayoutPanel3.Controls.Add(this.labelControl17, 0, 20);
            this.tableLayoutPanel3.Controls.Add(this.txtSupplDescrpn, 0, 21);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(686, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 22;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(286, 514);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel3, null);
            this.tableLayoutPanel3.TabIndex = 5;
            // 
            // labelControl16
            // 
            this.labelControl16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl16.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl16.Appearance.Options.UseFont = true;
            this.labelControl16.Location = new System.Drawing.Point(3, 3);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(180, 13);
            this.labelControl16.TabIndex = 24;
            this.labelControl16.Text = "№ заказа/№ версии/внутр. №";
            // 
            // labelControl15
            // 
            this.labelControl15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl15.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl15.Appearance.Options.UseFont = true;
            this.labelControl15.Location = new System.Drawing.Point(3, 393);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(96, 13);
            this.labelControl15.TabIndex = 22;
            this.labelControl15.Text = "Адрес доставки";
            // 
            // txtRtt
            // 
            this.txtRtt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRtt.Location = new System.Drawing.Point(3, 369);
            this.txtRtt.Name = "txtRtt";
            this.txtRtt.Properties.ReadOnly = true;
            this.txtRtt.Size = new System.Drawing.Size(280, 20);
            this.txtRtt.TabIndex = 21;
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(3, 350);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(21, 13);
            this.labelControl2.TabIndex = 20;
            this.labelControl2.Text = "РТТ";
            // 
            // labelControl4
            // 
            this.labelControl4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(3, 46);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(170, 13);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "Дата заказа/Дата доставки";
            // 
            // txtSupplBeginDate
            // 
            this.txtSupplBeginDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSupplBeginDate.Location = new System.Drawing.Point(3, 65);
            this.txtSupplBeginDate.Name = "txtSupplBeginDate";
            this.txtSupplBeginDate.Properties.ReadOnly = true;
            this.txtSupplBeginDate.Size = new System.Drawing.Size(280, 20);
            this.txtSupplBeginDate.TabIndex = 7;
            // 
            // labelControl6
            // 
            this.labelControl6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(3, 89);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(104, 13);
            this.labelControl6.TabIndex = 18;
            this.labelControl6.Text = "Состояние заказа";
            // 
            // txtOrderState
            // 
            this.txtOrderState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOrderState.Location = new System.Drawing.Point(3, 108);
            this.txtOrderState.Name = "txtOrderState";
            this.txtOrderState.Properties.ReadOnly = true;
            this.txtOrderState.Size = new System.Drawing.Size(280, 20);
            this.txtOrderState.TabIndex = 19;
            this.txtOrderState.ToolTipController = this.toolTipController;
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(3, 132);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(41, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Клиент";
            // 
            // txtCustomer
            // 
            this.txtCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCustomer.Location = new System.Drawing.Point(3, 151);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Properties.ReadOnly = true;
            this.txtCustomer.Size = new System.Drawing.Size(280, 20);
            this.txtCustomer.TabIndex = 4;
            // 
            // txtStock
            // 
            this.txtStock.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStock.Location = new System.Drawing.Point(3, 240);
            this.txtStock.Name = "txtStock";
            this.txtStock.Properties.ReadOnly = true;
            this.txtStock.Size = new System.Drawing.Size(280, 20);
            this.txtStock.TabIndex = 9;
            // 
            // labelControl5
            // 
            this.labelControl5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(3, 221);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(93, 13);
            this.labelControl5.TabIndex = 8;
            this.labelControl5.Text = "Склад отгрузки";
            // 
            // labelControl7
            // 
            this.labelControl7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Location = new System.Drawing.Point(3, 178);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(151, 13);
            this.labelControl7.TabIndex = 12;
            this.labelControl7.Text = "Торговый представитель";
            // 
            // txtSalesMan
            // 
            this.txtSalesMan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSalesMan.Location = new System.Drawing.Point(3, 197);
            this.txtSalesMan.Name = "txtSalesMan";
            this.txtSalesMan.Properties.ReadOnly = true;
            this.txtSalesMan.Size = new System.Drawing.Size(280, 20);
            this.txtSalesMan.TabIndex = 13;
            // 
            // labelControl13
            // 
            this.labelControl13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl13.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl13.Appearance.Options.UseFont = true;
            this.labelControl13.Location = new System.Drawing.Point(3, 264);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(86, 13);
            this.labelControl13.TabIndex = 14;
            this.labelControl13.Text = "Форма оплаты";
            // 
            // txtPaymentType
            // 
            this.txtPaymentType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPaymentType.Location = new System.Drawing.Point(3, 283);
            this.txtPaymentType.Name = "txtPaymentType";
            this.txtPaymentType.Properties.ReadOnly = true;
            this.txtPaymentType.Size = new System.Drawing.Size(280, 20);
            this.txtPaymentType.TabIndex = 15;
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(3, 307);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(101, 13);
            this.labelControl3.TabIndex = 16;
            this.labelControl3.Text = "Дочерний клиент";
            // 
            // txtChildDepartCode
            // 
            this.txtChildDepartCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtChildDepartCode.Location = new System.Drawing.Point(3, 326);
            this.txtChildDepartCode.Name = "txtChildDepartCode";
            this.txtChildDepartCode.Properties.ReadOnly = true;
            this.txtChildDepartCode.Size = new System.Drawing.Size(280, 20);
            this.txtChildDepartCode.TabIndex = 17;
            // 
            // txtSupplNumVersion
            // 
            this.txtSupplNumVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSupplNumVersion.Location = new System.Drawing.Point(3, 22);
            this.txtSupplNumVersion.Name = "txtSupplNumVersion";
            this.txtSupplNumVersion.Properties.ReadOnly = true;
            this.txtSupplNumVersion.Size = new System.Drawing.Size(280, 20);
            this.txtSupplNumVersion.TabIndex = 25;
            this.txtSupplNumVersion.ToolTipController = this.toolTipController;
            // 
            // txtDeliveryAddress
            // 
            this.txtDeliveryAddress.Location = new System.Drawing.Point(3, 412);
            this.txtDeliveryAddress.Name = "txtDeliveryAddress";
            this.txtDeliveryAddress.Properties.ReadOnly = true;
            this.txtDeliveryAddress.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtDeliveryAddress.Size = new System.Drawing.Size(280, 44);
            this.txtDeliveryAddress.TabIndex = 23;
            this.txtDeliveryAddress.ToolTipController = this.toolTipController;
            // 
            // labelControl17
            // 
            this.labelControl17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl17.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl17.Appearance.Options.UseFont = true;
            this.labelControl17.Location = new System.Drawing.Point(3, 462);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(70, 13);
            this.labelControl17.TabIndex = 26;
            this.labelControl17.Text = "Примечание";
            // 
            // txtSupplDescrpn
            // 
            this.txtSupplDescrpn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSupplDescrpn.Location = new System.Drawing.Point(3, 481);
            this.txtSupplDescrpn.Name = "txtSupplDescrpn";
            this.txtSupplDescrpn.Properties.ReadOnly = true;
            this.txtSupplDescrpn.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtSupplDescrpn.Size = new System.Drawing.Size(280, 30);
            this.txtSupplDescrpn.TabIndex = 27;
            this.txtSupplDescrpn.ToolTipController = this.toolTipController;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.pnlInfo, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.gridControlAgreementList, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 127F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(686, 514);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel6, null);
            this.tableLayoutPanel6.TabIndex = 4;
            // 
            // pnlInfo
            // 
            this.pnlInfo.BackColor = System.Drawing.SystemColors.Info;
            this.pnlInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlInfo.Controls.Add(this.txtSupplOrderQty);
            this.pnlInfo.Controls.Add(this.label11);
            this.pnlInfo.Controls.Add(this.txtSupplAllDiscountCurrencyPrice);
            this.pnlInfo.Controls.Add(this.txtSupplAllCurrencyDiscount);
            this.pnlInfo.Controls.Add(this.txtSupplAllCurrencyPrice);
            this.pnlInfo.Controls.Add(this.label7);
            this.pnlInfo.Controls.Add(this.label8);
            this.pnlInfo.Controls.Add(this.label9);
            this.pnlInfo.Controls.Add(this.txtSupplAllDiscountPrice);
            this.pnlInfo.Controls.Add(this.txtSupplAllDiscount);
            this.pnlInfo.Controls.Add(this.txtSupplAllPrice);
            this.pnlInfo.Controls.Add(this.label4);
            this.pnlInfo.Controls.Add(this.label5);
            this.pnlInfo.Controls.Add(this.label6);
            this.pnlInfo.Controls.Add(this.pictureEdit);
            this.pnlInfo.Controls.Add(this.txtSupplQty);
            this.pnlInfo.Controls.Add(this.txtSupplPositionCount);
            this.pnlInfo.Controls.Add(this.txtSupplNum);
            this.pnlInfo.Controls.Add(this.label3);
            this.pnlInfo.Controls.Add(this.label2);
            this.pnlInfo.Controls.Add(this.label1);
            this.pnlInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInfo.Location = new System.Drawing.Point(3, 390);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(680, 121);
            this.toolTipController.SetSuperTip(this.pnlInfo, null);
            this.pnlInfo.TabIndex = 3;
            // 
            // txtSupplOrderQty
            // 
            this.txtSupplOrderQty.Location = new System.Drawing.Point(149, 86);
            this.txtSupplOrderQty.Name = "txtSupplOrderQty";
            this.txtSupplOrderQty.Properties.Appearance.Options.UseTextOptions = true;
            this.txtSupplOrderQty.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtSupplOrderQty.Properties.DisplayFormat.FormatString = "### ###";
            this.txtSupplOrderQty.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtSupplOrderQty.Properties.ReadOnly = true;
            this.txtSupplOrderQty.Size = new System.Drawing.Size(62, 20);
            this.txtSupplOrderQty.TabIndex = 22;
            this.txtSupplOrderQty.ToolTipController = this.toolTipController;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(33, 90);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 13);
            this.toolTipController.SetSuperTip(this.label11, null);
            this.label11.TabIndex = 21;
            this.label11.Text = "Зарезервировано:";
            // 
            // txtSupplAllDiscountCurrencyPrice
            // 
            this.txtSupplAllDiscountCurrencyPrice.Location = new System.Drawing.Point(533, 86);
            this.txtSupplAllDiscountCurrencyPrice.Name = "txtSupplAllDiscountCurrencyPrice";
            this.txtSupplAllDiscountCurrencyPrice.Properties.Appearance.Options.UseTextOptions = true;
            this.txtSupplAllDiscountCurrencyPrice.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtSupplAllDiscountCurrencyPrice.Properties.DisplayFormat.FormatString = "### ### ###.00";
            this.txtSupplAllDiscountCurrencyPrice.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtSupplAllDiscountCurrencyPrice.Properties.ReadOnly = true;
            this.txtSupplAllDiscountCurrencyPrice.Size = new System.Drawing.Size(94, 20);
            this.txtSupplAllDiscountCurrencyPrice.TabIndex = 18;
            this.txtSupplAllDiscountCurrencyPrice.ToolTipController = this.toolTipController;
            // 
            // txtSupplAllCurrencyDiscount
            // 
            this.txtSupplAllCurrencyDiscount.Location = new System.Drawing.Point(533, 62);
            this.txtSupplAllCurrencyDiscount.Name = "txtSupplAllCurrencyDiscount";
            this.txtSupplAllCurrencyDiscount.Properties.Appearance.Options.UseTextOptions = true;
            this.txtSupplAllCurrencyDiscount.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtSupplAllCurrencyDiscount.Properties.DisplayFormat.FormatString = "### ### ###.00";
            this.txtSupplAllCurrencyDiscount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtSupplAllCurrencyDiscount.Properties.ReadOnly = true;
            this.txtSupplAllCurrencyDiscount.Size = new System.Drawing.Size(94, 20);
            this.txtSupplAllCurrencyDiscount.TabIndex = 17;
            this.txtSupplAllCurrencyDiscount.ToolTipController = this.toolTipController;
            // 
            // txtSupplAllCurrencyPrice
            // 
            this.txtSupplAllCurrencyPrice.Location = new System.Drawing.Point(533, 38);
            this.txtSupplAllCurrencyPrice.Name = "txtSupplAllCurrencyPrice";
            this.txtSupplAllCurrencyPrice.Properties.Appearance.Options.UseTextOptions = true;
            this.txtSupplAllCurrencyPrice.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtSupplAllCurrencyPrice.Properties.DisplayFormat.FormatString = "### ### ###.00";
            this.txtSupplAllCurrencyPrice.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtSupplAllCurrencyPrice.Properties.ReadOnly = true;
            this.txtSupplAllCurrencyPrice.Size = new System.Drawing.Size(94, 20);
            this.txtSupplAllCurrencyPrice.TabIndex = 16;
            this.txtSupplAllCurrencyPrice.ToolTipController = this.toolTipController;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(438, 80);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 26);
            this.toolTipController.SetSuperTip(this.label7, null);
            this.label7.TabIndex = 15;
            this.label7.Text = "Сумма \r\nсо скидкой, eur:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(438, 66);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 13);
            this.toolTipController.SetSuperTip(this.label8, null);
            this.label8.TabIndex = 14;
            this.label8.Text = "Скидка, eur:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(438, 42);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 13);
            this.toolTipController.SetSuperTip(this.label9, null);
            this.label9.TabIndex = 13;
            this.label9.Text = "Сумма, eur:";
            // 
            // txtSupplAllDiscountPrice
            // 
            this.txtSupplAllDiscountPrice.Location = new System.Drawing.Point(323, 86);
            this.txtSupplAllDiscountPrice.Name = "txtSupplAllDiscountPrice";
            this.txtSupplAllDiscountPrice.Properties.Appearance.Options.UseTextOptions = true;
            this.txtSupplAllDiscountPrice.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtSupplAllDiscountPrice.Properties.DisplayFormat.FormatString = "### ### ###.00";
            this.txtSupplAllDiscountPrice.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtSupplAllDiscountPrice.Properties.ReadOnly = true;
            this.txtSupplAllDiscountPrice.Size = new System.Drawing.Size(94, 20);
            this.txtSupplAllDiscountPrice.TabIndex = 12;
            this.txtSupplAllDiscountPrice.ToolTipController = this.toolTipController;
            // 
            // txtSupplAllDiscount
            // 
            this.txtSupplAllDiscount.Location = new System.Drawing.Point(323, 62);
            this.txtSupplAllDiscount.Name = "txtSupplAllDiscount";
            this.txtSupplAllDiscount.Properties.Appearance.Options.UseTextOptions = true;
            this.txtSupplAllDiscount.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtSupplAllDiscount.Properties.DisplayFormat.FormatString = "### ### ###.00";
            this.txtSupplAllDiscount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtSupplAllDiscount.Properties.ReadOnly = true;
            this.txtSupplAllDiscount.Size = new System.Drawing.Size(94, 20);
            this.txtSupplAllDiscount.TabIndex = 11;
            this.txtSupplAllDiscount.ToolTipController = this.toolTipController;
            // 
            // txtSupplAllPrice
            // 
            this.txtSupplAllPrice.Location = new System.Drawing.Point(323, 38);
            this.txtSupplAllPrice.Name = "txtSupplAllPrice";
            this.txtSupplAllPrice.Properties.Appearance.Options.UseTextOptions = true;
            this.txtSupplAllPrice.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtSupplAllPrice.Properties.DisplayFormat.FormatString = "{0:### ### ##0.00}";
            this.txtSupplAllPrice.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtSupplAllPrice.Properties.ReadOnly = true;
            this.txtSupplAllPrice.Size = new System.Drawing.Size(94, 20);
            this.txtSupplAllPrice.TabIndex = 10;
            this.txtSupplAllPrice.ToolTipController = this.toolTipController;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(228, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 26);
            this.toolTipController.SetSuperTip(this.label4, null);
            this.label4.TabIndex = 9;
            this.label4.Text = "Сумма \r\nсо скидкой, руб.:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(228, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.toolTipController.SetSuperTip(this.label5, null);
            this.label5.TabIndex = 8;
            this.label5.Text = "Скидка, руб.:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(228, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.toolTipController.SetSuperTip(this.label6, null);
            this.label6.TabIndex = 7;
            this.label6.Text = "Сумма, руб.:";
            // 
            // pictureEdit
            // 
            this.pictureEdit.EditValue = global::ERPMercuryProcessingOrder.Properties.Resources.information;
            this.pictureEdit.Location = new System.Drawing.Point(4, 4);
            this.pictureEdit.Name = "pictureEdit";
            this.pictureEdit.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.pictureEdit.Properties.Appearance.ForeColor = System.Drawing.SystemColors.InfoText;
            this.pictureEdit.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit.Properties.Appearance.Options.UseForeColor = true;
            this.pictureEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit.Size = new System.Drawing.Size(23, 23);
            this.pictureEdit.TabIndex = 6;
            this.pictureEdit.ToolTipController = this.toolTipController;
            // 
            // txtSupplQty
            // 
            this.txtSupplQty.Location = new System.Drawing.Point(149, 62);
            this.txtSupplQty.Name = "txtSupplQty";
            this.txtSupplQty.Properties.Appearance.Options.UseTextOptions = true;
            this.txtSupplQty.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtSupplQty.Properties.DisplayFormat.FormatString = "### ###";
            this.txtSupplQty.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtSupplQty.Properties.ReadOnly = true;
            this.txtSupplQty.Size = new System.Drawing.Size(62, 20);
            this.txtSupplQty.TabIndex = 5;
            this.txtSupplQty.ToolTipController = this.toolTipController;
            // 
            // txtSupplPositionCount
            // 
            this.txtSupplPositionCount.Location = new System.Drawing.Point(149, 38);
            this.txtSupplPositionCount.Name = "txtSupplPositionCount";
            this.txtSupplPositionCount.Properties.Appearance.Options.UseTextOptions = true;
            this.txtSupplPositionCount.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtSupplPositionCount.Properties.DisplayFormat.FormatString = "### ###";
            this.txtSupplPositionCount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtSupplPositionCount.Properties.ReadOnly = true;
            this.txtSupplPositionCount.Size = new System.Drawing.Size(62, 20);
            this.txtSupplPositionCount.TabIndex = 4;
            this.txtSupplPositionCount.ToolTipController = this.toolTipController;
            // 
            // txtSupplNum
            // 
            this.txtSupplNum.Location = new System.Drawing.Point(149, 10);
            this.txtSupplNum.Name = "txtSupplNum";
            this.txtSupplNum.Properties.Appearance.Options.UseTextOptions = true;
            this.txtSupplNum.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtSupplNum.Properties.ReadOnly = true;
            this.txtSupplNum.Size = new System.Drawing.Size(62, 20);
            this.txtSupplNum.TabIndex = 3;
            this.txtSupplNum.ToolTipController = this.toolTipController;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 13);
            this.toolTipController.SetSuperTip(this.label3, null);
            this.label3.TabIndex = 2;
            this.label3.Text = "Количество товара:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 13);
            this.toolTipController.SetSuperTip(this.label2, null);
            this.label2.TabIndex = 1;
            this.label2.Text = "Количество позиций:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.toolTipController.SetSuperTip(this.label1, null);
            this.label1.TabIndex = 0;
            this.label1.Text = "Заказ № / версия:";
            // 
            // gridControlAgreementList
            // 
            this.gridControlAgreementList.ContextMenuStrip = this.contextMenuStrip;
            this.gridControlAgreementList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlAgreementList.EmbeddedNavigator.Name = "";
            this.gridControlAgreementList.Location = new System.Drawing.Point(3, 3);
            this.gridControlAgreementList.MainView = this.gridViewAgreementList;
            this.gridControlAgreementList.Name = "gridControlAgreementList";
            this.gridControlAgreementList.Size = new System.Drawing.Size(680, 381);
            this.gridControlAgreementList.TabIndex = 2;
            this.gridControlAgreementList.ToolTipController = this.toolTipController;
            this.gridControlAgreementList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewAgreementList});
            this.gridControlAgreementList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gridControlAgreementGrid_MouseDoubleClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuRefresh,
            this.toolStripSeparator1,
            this.menuCalcPrice,
            this.menuClearPrices,
            this.menuReturnSupplStateToAutoProcessPrices,
            this.menuMakeSupplDeleted,
            this.toolStripSeparator2,
            this.menuEventLog,
            this.toolStripMenuItem1,
            this.menuCreateOrderBlank,
            this.toolStripMenuItem2,
            this.menuTransformSupplToWaybill,
            this.menuGoToWaybill});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(308, 226);
            this.toolTipController.SetSuperTip(this.contextMenuStrip, null);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // menuRefresh
            // 
            this.menuRefresh.Name = "menuRefresh";
            this.menuRefresh.Size = new System.Drawing.Size(307, 22);
            this.menuRefresh.Text = "Обновить";
            this.menuRefresh.Click += new System.EventHandler(this.barBtnRefresh_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(304, 6);
            // 
            // menuCalcPrice
            // 
            this.menuCalcPrice.Name = "menuCalcPrice";
            this.menuCalcPrice.Size = new System.Drawing.Size(307, 22);
            this.menuCalcPrice.Text = "Пересчитать цены в заказе";
            this.menuCalcPrice.Click += new System.EventHandler(this.menuCalcPrice_Click);
            // 
            // menuClearPrices
            // 
            this.menuClearPrices.Name = "menuClearPrices";
            this.menuClearPrices.Size = new System.Drawing.Size(307, 22);
            this.menuClearPrices.Text = "Обнулить цены для ручной обработки";
            this.menuClearPrices.Visible = false;
            // 
            // menuReturnSupplStateToAutoProcessPrices
            // 
            this.menuReturnSupplStateToAutoProcessPrices.Name = "menuReturnSupplStateToAutoProcessPrices";
            this.menuReturnSupplStateToAutoProcessPrices.Size = new System.Drawing.Size(307, 22);
            this.menuReturnSupplStateToAutoProcessPrices.Text = "Вернуть заказ на автоматический расчет цен";
            this.menuReturnSupplStateToAutoProcessPrices.Visible = false;
            // 
            // menuMakeSupplDeleted
            // 
            this.menuMakeSupplDeleted.Name = "menuMakeSupplDeleted";
            this.menuMakeSupplDeleted.Size = new System.Drawing.Size(307, 22);
            this.menuMakeSupplDeleted.Text = "Удалить заказ";
            this.menuMakeSupplDeleted.ToolTipText = "Изменить состояние заказа на \"удален\"";
            this.menuMakeSupplDeleted.Click += new System.EventHandler(this.barBtnDelete_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(304, 6);
            // 
            // menuEventLog
            // 
            this.menuEventLog.Name = "menuEventLog";
            this.menuEventLog.Size = new System.Drawing.Size(307, 22);
            this.menuEventLog.Text = "Журнал событий";
            this.menuEventLog.Click += new System.EventHandler(this.menuEventLog_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(304, 6);
            // 
            // menuCreateOrderBlank
            // 
            this.menuCreateOrderBlank.Name = "menuCreateOrderBlank";
            this.menuCreateOrderBlank.Size = new System.Drawing.Size(307, 22);
            this.menuCreateOrderBlank.Text = "Формирование бланка заказа";
            this.menuCreateOrderBlank.ToolTipText = "Формирование бланка заказа в MS Excel";
            this.menuCreateOrderBlank.Click += new System.EventHandler(this.menuCreateOrderBlank_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(304, 6);
            // 
            // menuTransformSupplToWaybill
            // 
            this.menuTransformSupplToWaybill.Name = "menuTransformSupplToWaybill";
            this.menuTransformSupplToWaybill.Size = new System.Drawing.Size(307, 22);
            this.menuTransformSupplToWaybill.Text = "Перевести заказ в накладную...";
            this.menuTransformSupplToWaybill.Click += new System.EventHandler(this.menuTransformSupplToWaybill_Click);
            // 
            // menuGoToWaybill
            // 
            this.menuGoToWaybill.Name = "menuGoToWaybill";
            this.menuGoToWaybill.Size = new System.Drawing.Size(307, 22);
            this.menuGoToWaybill.Text = "Перейти к накладной...";
            this.menuGoToWaybill.Click += new System.EventHandler(this.menuGoToWaybill_Click);
            // 
            // gridViewAgreementList
            // 
            this.gridViewAgreementList.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridViewAgreementList.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridViewAgreementList.ColumnPanelRowHeight = 40;
            this.gridViewAgreementList.GridControl = this.gridControlAgreementList;
            this.gridViewAgreementList.Images = this.imageCollection;
            this.gridViewAgreementList.Name = "gridViewAgreementList";
            this.gridViewAgreementList.OptionsBehavior.Editable = false;
            this.gridViewAgreementList.OptionsDetail.EnableMasterViewMode = false;
            this.gridViewAgreementList.OptionsDetail.ShowDetailTabs = false;
            this.gridViewAgreementList.OptionsDetail.SmartDetailExpand = false;
            this.gridViewAgreementList.OptionsView.ColumnAutoWidth = false;
            this.gridViewAgreementList.OptionsView.ShowFooter = true;
            this.gridViewAgreementList.OptionsView.ShowGroupPanel = false;
            this.gridViewAgreementList.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridViewAgreementList_CustomDrawCell);
            this.gridViewAgreementList.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewAgreementList_FocusedRowChanged);
            this.gridViewAgreementList.RowCountChanged += new System.EventHandler(this.gridViewAgreementList_RowCountChanged);
            // 
            // imageCollection
            // 
            this.imageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection.ImageStream")));
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 5;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Controls.Add(this.barBtnAdd, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.barbtnImportProduct, 3, 0);
            this.tableLayoutPanel5.Controls.Add(this.barBtnEdit, 4, 0);
            this.tableLayoutPanel5.Controls.Add(this.barBtnDelete, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.barBtnCopy, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(974, 31);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel5, null);
            this.tableLayoutPanel5.TabIndex = 2;
            // 
            // barBtnAdd
            // 
            this.barBtnAdd.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.barBtnAdd.Image = ((System.Drawing.Image)(resources.GetObject("barBtnAdd.Image")));
            this.barBtnAdd.ImageAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.barBtnAdd.Location = new System.Drawing.Point(1, 3);
            this.barBtnAdd.Margin = new System.Windows.Forms.Padding(1);
            this.barBtnAdd.Name = "barBtnAdd";
            this.barBtnAdd.Size = new System.Drawing.Size(25, 25);
            this.barBtnAdd.TabIndex = 9;
            this.barBtnAdd.ToolTip = "Создать новый заказ";
            this.barBtnAdd.ToolTipController = this.toolTipController;
            this.barBtnAdd.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.barBtnAdd.Click += new System.EventHandler(this.barBtnAdd_Click);
            // 
            // barbtnImportProduct
            // 
            this.barbtnImportProduct.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.barbtnImportProduct.Image = ((System.Drawing.Image)(resources.GetObject("barbtnImportProduct.Image")));
            this.barbtnImportProduct.Location = new System.Drawing.Point(82, 3);
            this.barbtnImportProduct.Margin = new System.Windows.Forms.Padding(1);
            this.barbtnImportProduct.Name = "barbtnImportProduct";
            this.barbtnImportProduct.Size = new System.Drawing.Size(25, 25);
            this.barbtnImportProduct.TabIndex = 13;
            this.barbtnImportProduct.ToolTip = "Импорт заказа";
            this.barbtnImportProduct.ToolTipController = this.toolTipController;
            this.barbtnImportProduct.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.barbtnImportProduct.Visible = false;
            this.barbtnImportProduct.Click += new System.EventHandler(this.btnImportProduct_Click);
            // 
            // barBtnEdit
            // 
            this.barBtnEdit.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.barBtnEdit.Image = ((System.Drawing.Image)(resources.GetObject("barBtnEdit.Image")));
            this.barBtnEdit.ImageAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.barBtnEdit.Location = new System.Drawing.Point(109, 3);
            this.barBtnEdit.Margin = new System.Windows.Forms.Padding(1);
            this.barBtnEdit.Name = "barBtnEdit";
            this.barBtnEdit.Size = new System.Drawing.Size(25, 25);
            this.barBtnEdit.TabIndex = 10;
            this.barBtnEdit.ToolTip = "Редактировать заказ";
            this.barBtnEdit.ToolTipController = this.toolTipController;
            this.barBtnEdit.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.barBtnEdit.Visible = false;
            this.barBtnEdit.Click += new System.EventHandler(this.barBtnEdit_Click);
            // 
            // barBtnDelete
            // 
            this.barBtnDelete.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.barBtnDelete.Image = ((System.Drawing.Image)(resources.GetObject("barBtnDelete.Image")));
            this.barBtnDelete.ImageAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.barBtnDelete.Location = new System.Drawing.Point(55, 3);
            this.barBtnDelete.Margin = new System.Windows.Forms.Padding(1);
            this.barBtnDelete.Name = "barBtnDelete";
            this.barBtnDelete.Size = new System.Drawing.Size(25, 25);
            this.barBtnDelete.TabIndex = 11;
            this.barBtnDelete.ToolTip = "Удалить заказ";
            this.barBtnDelete.ToolTipController = this.toolTipController;
            this.barBtnDelete.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.barBtnDelete.Click += new System.EventHandler(this.barBtnDelete_Click);
            // 
            // barBtnCopy
            // 
            this.barBtnCopy.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.barBtnCopy.Image = ((System.Drawing.Image)(resources.GetObject("barBtnCopy.Image")));
            this.barBtnCopy.ImageAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.barBtnCopy.Location = new System.Drawing.Point(28, 3);
            this.barBtnCopy.Margin = new System.Windows.Forms.Padding(1);
            this.barBtnCopy.Name = "barBtnCopy";
            this.barBtnCopy.Size = new System.Drawing.Size(25, 25);
            this.barBtnCopy.TabIndex = 12;
            this.barBtnCopy.ToolTip = "Копировать заказ";
            this.barBtnCopy.ToolTipController = this.toolTipController;
            this.barBtnCopy.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.barBtnCopy.Click += new System.EventHandler(this.barBtnCopy_Click);
            // 
            // tabPageEditor
            // 
            this.tabPageEditor.Controls.Add(this.tableLayoutPanelAgreementEditor);
            this.tabPageEditor.Name = "tabPageEditor";
            this.tabPageEditor.Size = new System.Drawing.Size(974, 602);
            this.tabPageEditor.Text = "tabPageEditor";
            // 
            // tableLayoutPanelAgreementEditor
            // 
            this.tableLayoutPanelAgreementEditor.AutoScroll = true;
            this.tableLayoutPanelAgreementEditor.AutoScrollMinSize = new System.Drawing.Size(0, 10);
            this.tableLayoutPanelAgreementEditor.ColumnCount = 1;
            this.tableLayoutPanelAgreementEditor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelAgreementEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelAgreementEditor.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelAgreementEditor.Name = "tableLayoutPanelAgreementEditor";
            this.tableLayoutPanelAgreementEditor.RowCount = 1;
            this.tableLayoutPanelAgreementEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelAgreementEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 602F));
            this.tableLayoutPanelAgreementEditor.Size = new System.Drawing.Size(974, 602);
            this.toolTipController.SetSuperTip(this.tableLayoutPanelAgreementEditor, null);
            this.tableLayoutPanelAgreementEditor.TabIndex = 1;
            // 
            // tabPageWaybill
            // 
            this.tabPageWaybill.Controls.Add(this.tableLayoutPanelWaybillEditor);
            this.tabPageWaybill.Name = "tabPageWaybill";
            this.tabPageWaybill.Size = new System.Drawing.Size(974, 602);
            this.tabPageWaybill.Text = "tabPageWaybill";
            // 
            // tableLayoutPanelWaybillEditor
            // 
            this.tableLayoutPanelWaybillEditor.ColumnCount = 1;
            this.tableLayoutPanelWaybillEditor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelWaybillEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelWaybillEditor.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelWaybillEditor.Name = "tableLayoutPanelWaybillEditor";
            this.tableLayoutPanelWaybillEditor.RowCount = 1;
            this.tableLayoutPanelWaybillEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelWaybillEditor.Size = new System.Drawing.Size(974, 602);
            this.toolTipController.SetSuperTip(this.tableLayoutPanelWaybillEditor, null);
            this.tableLayoutPanelWaybillEditor.TabIndex = 0;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "MS Excel 2010 files (*.xlsm)|*.xlsm|MS Excel 2003 files (*.xls)|*.xls|All files (" +
    "*.*)|*.*";
            // 
            // frmOrderList
            // 
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 639);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmOrderList";
            this.toolTipController.SetSuperTip(this, null);
            this.Text = "frmOrderList";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmOrderList_FormClosed);
            this.Shown += new System.EventHandler(this.frmOrderList_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPageViewer.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlSearchCondition)).EndInit();
            this.panelControlSearchCondition.ResumeLayout(false);
            this.panelControlSearchCondition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboxPaymentType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxStock.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEndDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEndDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtBeginDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtBeginDate.Properties)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRtt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSupplBeginDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStock.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSalesMan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPaymentType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChildDepartCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSupplNumVersion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDeliveryAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSupplDescrpn.Properties)).EndInit();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.pnlInfo.ResumeLayout(false);
            this.pnlInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSupplOrderQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSupplAllDiscountCurrencyPrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSupplAllCurrencyDiscount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSupplAllCurrencyPrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSupplAllDiscountPrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSupplAllDiscount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSupplAllPrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSupplQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSupplPositionCount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSupplNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlAgreementList)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewAgreementList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection)).EndInit();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tabPageEditor.ResumeLayout(false);
            this.tabPageWaybill.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.ToolTipController toolTipController;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraTab.XtraTabControl tabControl;
        private DevExpress.XtraTab.XtraTabPage tabPageViewer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private DevExpress.XtraEditors.PanelControl panelControlSearchCondition;
        private DevExpress.XtraEditors.SimpleButton barBtnRefresh;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.ComboBoxEdit cboxCompany;
        private DevExpress.XtraEditors.ComboBoxEdit cboxCustomer;
        private DevExpress.XtraEditors.DateEdit dtEndDate;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.DateEdit dtBeginDate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private DevExpress.XtraEditors.SimpleButton barBtnDelete;
        private DevExpress.XtraEditors.SimpleButton barBtnEdit;
        private DevExpress.XtraEditors.SimpleButton barBtnAdd;
        private DevExpress.XtraTab.XtraTabPage tabPageEditor;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelAgreementEditor;
        private DevExpress.XtraEditors.SimpleButton barBtnCopy;
        private DevExpress.Utils.ImageCollection imageCollection;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.ComboBoxEdit cboxStock;
        private DevExpress.XtraEditors.SimpleButton barbtnImportProduct;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.ComboBoxEdit cboxPaymentType;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuCalcPrice;
        private System.Windows.Forms.ToolStripMenuItem menuClearPrices;
        private System.Windows.Forms.ToolStripMenuItem menuReturnSupplStateToAutoProcessPrices;
        private System.Windows.Forms.ToolStripMenuItem menuMakeSupplDeleted;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menuEventLog;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem menuCreateOrderBlank;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private DevExpress.XtraGrid.GridControl gridControlAgreementList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewAgreementList;
        private System.Windows.Forms.Panel pnlInfo;
        private DevExpress.XtraEditors.TextEdit txtSupplOrderQty;
        private System.Windows.Forms.Label label11;
        private DevExpress.XtraEditors.TextEdit txtSupplAllDiscountCurrencyPrice;
        private DevExpress.XtraEditors.TextEdit txtSupplAllCurrencyDiscount;
        private DevExpress.XtraEditors.TextEdit txtSupplAllCurrencyPrice;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private DevExpress.XtraEditors.TextEdit txtSupplAllDiscountPrice;
        private DevExpress.XtraEditors.TextEdit txtSupplAllDiscount;
        private DevExpress.XtraEditors.TextEdit txtSupplAllPrice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.PictureEdit pictureEdit;
        private DevExpress.XtraEditors.TextEdit txtSupplQty;
        private DevExpress.XtraEditors.TextEdit txtSupplPositionCount;
        private DevExpress.XtraEditors.TextEdit txtSupplNum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        private DevExpress.XtraEditors.TextEdit txtRtt;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtSupplBeginDate;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtOrderState;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtCustomer;
        private DevExpress.XtraEditors.TextEdit txtStock;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit txtSalesMan;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraEditors.TextEdit txtPaymentType;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtChildDepartCode;
        private DevExpress.XtraEditors.MemoEdit txtDeliveryAddress;
        private DevExpress.XtraEditors.TextEdit txtSupplNumVersion;
        private DevExpress.XtraEditors.LabelControl labelControl17;
        private DevExpress.XtraEditors.MemoEdit txtSupplDescrpn;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem menuTransformSupplToWaybill;
        private System.Windows.Forms.ToolStripMenuItem menuGoToWaybill;
        private DevExpress.XtraTab.XtraTabPage tabPageWaybill;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelWaybillEditor;
    }
}
namespace ERPMercuryProcessingOrder
{
    partial class frmPDASupplList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPDASupplList));
            this.toolTipController = new DevExpress.Utils.ToolTipController(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.txtSupplOrderQty = new DevExpress.XtraEditors.TextEdit();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSupplState = new DevExpress.XtraEditors.TextEdit();
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
            this.treeList = new DevExpress.XtraTreeList.TreeList();
            this.colSupplDate = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colSupplDeliveryDate = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colMoneyBonus = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repItemCheckEditBonus = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colSupplNum = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colSupplVersion = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colSupplState = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCustomer = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colChildCode = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colDepartCode = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colStockName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCompanyName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colGroupList = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colWeight = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colSupplSateId = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colExcludeFromADJ = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.imageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.EndDate = new DevExpress.XtraEditors.DateEdit();
            this.BeginDate = new DevExpress.XtraEditors.DateEdit();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuCalcPrice = new System.Windows.Forms.ToolStripMenuItem();
            this.menuClearPrices = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReturnSupplStateToAutoProcessPrices = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMakeSupplDeleted = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMakeSupplExcludeFromADJ = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuEventLog = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.treeListSupplItems = new DevExpress.XtraTreeList.TreeList();
            this.colOwnerProductName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colIsImporter = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repItemCheckEdit = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colChargePercent = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colDiscountRetro = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colDiscountFix = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colDiscountTradeEq = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colNDSPercent = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colProductName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colOrderQty = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colQty = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colPrice = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colAllPrice = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCurrencyPrice = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colAllCurrencyPrice = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colDiscountPercent = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colDiscountPrice = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCurrencyDiscountPrice = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colTotalPrice = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCurrencyTotalPrice = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colDiscountList = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colDescription = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.barBtnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.btnToTabSupplList = new DevExpress.XtraEditors.SimpleButton();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barBtnRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnProcess = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnClearPrices = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnPrintList = new DevExpress.XtraBars.BarButtonItem();
            this.tabControl = new DevExpress.XtraTab.XtraTabControl();
            this.tabSupplList = new DevExpress.XtraTab.XtraTabPage();
            this.tabSuppl = new DevExpress.XtraTab.XtraTabPage();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSupplOrderQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSupplState.Properties)).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repItemCheckEditBonus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EndDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EndDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BeginDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BeginDate.Properties)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListSupplItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repItemCheckEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabSupplList.SuspendLayout();
            this.tabSuppl.SuspendLayout();
            this.SuspendLayout();
            // 
            // barDockControlTop
            // 
            this.toolTipController.SetSuperTip(this.barDockControlTop, null);
            // 
            // barDockControlBottom
            // 
            this.toolTipController.SetSuperTip(this.barDockControlBottom, null);
            // 
            // barDockControlLeft
            // 
            this.toolTipController.SetSuperTip(this.barDockControlLeft, null);
            // 
            // barDockControlRight
            // 
            this.toolTipController.SetSuperTip(this.barDockControlRight, null);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.pnlInfo, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.treeList, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panelControl1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(680, 375);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel1, null);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pnlInfo
            // 
            this.pnlInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlInfo.BackColor = System.Drawing.SystemColors.Info;
            this.pnlInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlInfo.Controls.Add(this.txtSupplOrderQty);
            this.pnlInfo.Controls.Add(this.label11);
            this.pnlInfo.Controls.Add(this.label10);
            this.pnlInfo.Controls.Add(this.txtSupplState);
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
            this.pnlInfo.Location = new System.Drawing.Point(3, 263);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(674, 109);
            this.toolTipController.SetSuperTip(this.pnlInfo, null);
            this.pnlInfo.TabIndex = 0;
            // 
            // txtSupplOrderQty
            // 
            this.txtSupplOrderQty.Location = new System.Drawing.Point(149, 79);
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
            this.label11.Location = new System.Drawing.Point(33, 83);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(97, 13);
            this.toolTipController.SetSuperTip(this.label11, null);
            this.label11.TabIndex = 21;
            this.label11.Text = "Заказано товара:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(228, 7);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 13);
            this.toolTipController.SetSuperTip(this.label10, null);
            this.label10.TabIndex = 20;
            this.label10.Text = "Состояние:";
            // 
            // txtSupplState
            // 
            this.txtSupplState.Location = new System.Drawing.Point(323, 3);
            this.txtSupplState.Name = "txtSupplState";
            this.txtSupplState.Properties.ReadOnly = true;
            this.txtSupplState.Size = new System.Drawing.Size(304, 20);
            this.txtSupplState.TabIndex = 0;
            this.txtSupplState.ToolTip = "Состояние заказа";
            this.txtSupplState.ToolTipController = this.toolTipController;
            this.txtSupplState.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            // 
            // txtSupplAllDiscountCurrencyPrice
            // 
            this.txtSupplAllDiscountCurrencyPrice.Location = new System.Drawing.Point(533, 79);
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
            this.txtSupplAllCurrencyDiscount.Location = new System.Drawing.Point(533, 55);
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
            this.txtSupplAllCurrencyPrice.Location = new System.Drawing.Point(533, 31);
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
            this.label7.Location = new System.Drawing.Point(438, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 26);
            this.toolTipController.SetSuperTip(this.label7, null);
            this.label7.TabIndex = 15;
            this.label7.Text = "Сумма \r\nсо скидкой, eur:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(438, 59);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 13);
            this.toolTipController.SetSuperTip(this.label8, null);
            this.label8.TabIndex = 14;
            this.label8.Text = "Скидка, eur:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(438, 35);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 13);
            this.toolTipController.SetSuperTip(this.label9, null);
            this.label9.TabIndex = 13;
            this.label9.Text = "Сумма, eur:";
            // 
            // txtSupplAllDiscountPrice
            // 
            this.txtSupplAllDiscountPrice.Location = new System.Drawing.Point(323, 79);
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
            this.txtSupplAllDiscount.Location = new System.Drawing.Point(323, 55);
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
            this.txtSupplAllPrice.Location = new System.Drawing.Point(323, 31);
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
            this.label4.Location = new System.Drawing.Point(228, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 26);
            this.toolTipController.SetSuperTip(this.label4, null);
            this.label4.TabIndex = 9;
            this.label4.Text = "Сумма \r\nсо скидкой, руб.:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(228, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.toolTipController.SetSuperTip(this.label5, null);
            this.label5.TabIndex = 8;
            this.label5.Text = "Скидка, руб.:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(228, 35);
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
            this.txtSupplQty.Location = new System.Drawing.Point(150, 55);
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
            this.txtSupplPositionCount.Location = new System.Drawing.Point(150, 31);
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
            this.txtSupplNum.Location = new System.Drawing.Point(150, 3);
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
            this.label3.Location = new System.Drawing.Point(33, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 13);
            this.toolTipController.SetSuperTip(this.label3, null);
            this.label3.TabIndex = 2;
            this.label3.Text = "Количество товара:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 13);
            this.toolTipController.SetSuperTip(this.label2, null);
            this.label2.TabIndex = 1;
            this.label2.Text = "Количество позиций:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.toolTipController.SetSuperTip(this.label1, null);
            this.label1.TabIndex = 0;
            this.label1.Text = "Заказ №:";
            // 
            // treeList
            // 
            this.treeList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colSupplDate,
            this.colSupplDeliveryDate,
            this.colMoneyBonus,
            this.colSupplNum,
            this.colSupplVersion,
            this.colSupplState,
            this.colCustomer,
            this.colChildCode,
            this.colDepartCode,
            this.colStockName,
            this.colCompanyName,
            this.colGroupList,
            this.colWeight,
            this.colSupplSateId,
            this.colExcludeFromADJ});
            this.treeList.Location = new System.Drawing.Point(3, 39);
            this.treeList.Name = "treeList";
            this.treeList.OptionsBehavior.Editable = false;
            this.treeList.OptionsView.AutoWidth = false;
            this.treeList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repItemCheckEditBonus});
            this.treeList.SelectImageList = this.imageCollection;
            this.treeList.Size = new System.Drawing.Size(674, 218);
            this.treeList.StateImageList = this.imageCollection;
            this.treeList.TabIndex = 1;
            this.treeList.ToolTipController = this.toolTipController;
            this.treeList.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeList_FocusedNodeChanged);
            this.treeList.CustomDrawNodeCell += new DevExpress.XtraTreeList.CustomDrawNodeCellEventHandler(this.treeList_CustomDrawNodeCell);
            this.treeList.CustomDrawNodeImages += new DevExpress.XtraTreeList.CustomDrawNodeImagesEventHandler(this.treeList_CustomDrawNodeImages);
            this.treeList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeList_MouseClick);
            this.treeList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeList_MouseDoubleClick);
            // 
            // colSupplDate
            // 
            this.colSupplDate.Caption = "Дата заказа";
            this.colSupplDate.FieldName = "Дата заказа";
            this.colSupplDate.MinWidth = 43;
            this.colSupplDate.Name = "colSupplDate";
            this.colSupplDate.OptionsColumn.AllowEdit = false;
            this.colSupplDate.OptionsColumn.AllowFocus = false;
            this.colSupplDate.OptionsColumn.FixedWidth = true;
            this.colSupplDate.OptionsColumn.ReadOnly = true;
            this.colSupplDate.Visible = true;
            this.colSupplDate.VisibleIndex = 0;
            this.colSupplDate.Width = 176;
            // 
            // colSupplDeliveryDate
            // 
            this.colSupplDeliveryDate.Caption = "Дата доставки";
            this.colSupplDeliveryDate.FieldName = "Дата доставки";
            this.colSupplDeliveryDate.Format.FormatString = "d";
            this.colSupplDeliveryDate.Format.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colSupplDeliveryDate.MinWidth = 50;
            this.colSupplDeliveryDate.Name = "colSupplDeliveryDate";
            this.colSupplDeliveryDate.OptionsColumn.AllowEdit = false;
            this.colSupplDeliveryDate.OptionsColumn.AllowFocus = false;
            this.colSupplDeliveryDate.OptionsColumn.FixedWidth = true;
            this.colSupplDeliveryDate.OptionsColumn.ReadOnly = true;
            this.colSupplDeliveryDate.Visible = true;
            this.colSupplDeliveryDate.VisibleIndex = 1;
            this.colSupplDeliveryDate.Width = 150;
            // 
            // colMoneyBonus
            // 
            this.colMoneyBonus.Caption = "Бонус";
            this.colMoneyBonus.ColumnEdit = this.repItemCheckEditBonus;
            this.colMoneyBonus.FieldName = "Бонус";
            this.colMoneyBonus.Name = "colMoneyBonus";
            this.colMoneyBonus.OptionsColumn.AllowEdit = false;
            this.colMoneyBonus.OptionsColumn.AllowFocus = false;
            this.colMoneyBonus.OptionsColumn.ReadOnly = true;
            this.colMoneyBonus.Visible = true;
            this.colMoneyBonus.VisibleIndex = 2;
            this.colMoneyBonus.Width = 50;
            // 
            // repItemCheckEditBonus
            // 
            this.repItemCheckEditBonus.AutoHeight = false;
            this.repItemCheckEditBonus.Name = "repItemCheckEditBonus";
            // 
            // colSupplNum
            // 
            this.colSupplNum.AppearanceCell.Options.UseTextOptions = true;
            this.colSupplNum.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSupplNum.Caption = "Номер заказа";
            this.colSupplNum.FieldName = "Номер заказа";
            this.colSupplNum.MinWidth = 40;
            this.colSupplNum.Name = "colSupplNum";
            this.colSupplNum.OptionsColumn.AllowEdit = false;
            this.colSupplNum.OptionsColumn.AllowFocus = false;
            this.colSupplNum.OptionsColumn.FixedWidth = true;
            this.colSupplNum.OptionsColumn.ReadOnly = true;
            this.colSupplNum.Visible = true;
            this.colSupplNum.VisibleIndex = 3;
            this.colSupplNum.Width = 84;
            // 
            // colSupplVersion
            // 
            this.colSupplVersion.AppearanceCell.Options.UseTextOptions = true;
            this.colSupplVersion.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSupplVersion.Caption = "№ версии";
            this.colSupplVersion.FieldName = "№ версии";
            this.colSupplVersion.MinWidth = 25;
            this.colSupplVersion.Name = "colSupplVersion";
            this.colSupplVersion.OptionsColumn.AllowEdit = false;
            this.colSupplVersion.OptionsColumn.AllowFocus = false;
            this.colSupplVersion.OptionsColumn.FixedWidth = true;
            this.colSupplVersion.OptionsColumn.ReadOnly = true;
            this.colSupplVersion.Visible = true;
            this.colSupplVersion.VisibleIndex = 4;
            this.colSupplVersion.Width = 63;
            // 
            // colSupplState
            // 
            this.colSupplState.Caption = "Состояние";
            this.colSupplState.FieldName = "Сосояние";
            this.colSupplState.MinWidth = 40;
            this.colSupplState.Name = "colSupplState";
            this.colSupplState.OptionsColumn.AllowEdit = false;
            this.colSupplState.OptionsColumn.AllowFocus = false;
            this.colSupplState.OptionsColumn.FixedWidth = true;
            this.colSupplState.OptionsColumn.ReadOnly = true;
            this.colSupplState.Visible = true;
            this.colSupplState.VisibleIndex = 5;
            // 
            // colCustomer
            // 
            this.colCustomer.Caption = "Клиент";
            this.colCustomer.FieldName = "Клиент";
            this.colCustomer.MinWidth = 70;
            this.colCustomer.Name = "colCustomer";
            this.colCustomer.OptionsColumn.AllowEdit = false;
            this.colCustomer.OptionsColumn.AllowFocus = false;
            this.colCustomer.OptionsColumn.ReadOnly = true;
            this.colCustomer.Visible = true;
            this.colCustomer.VisibleIndex = 6;
            this.colCustomer.Width = 209;
            // 
            // colChildCode
            // 
            this.colChildCode.Caption = "Дочерний клиент";
            this.colChildCode.FieldName = "Дочерний клиент";
            this.colChildCode.MinWidth = 50;
            this.colChildCode.Name = "colChildCode";
            this.colChildCode.OptionsColumn.AllowEdit = false;
            this.colChildCode.OptionsColumn.AllowFocus = false;
            this.colChildCode.OptionsColumn.FixedWidth = true;
            this.colChildCode.OptionsColumn.ReadOnly = true;
            this.colChildCode.Visible = true;
            this.colChildCode.VisibleIndex = 7;
            this.colChildCode.Width = 102;
            // 
            // colDepartCode
            // 
            this.colDepartCode.Caption = "Код ТП";
            this.colDepartCode.FieldName = "Код ТП";
            this.colDepartCode.MinWidth = 30;
            this.colDepartCode.Name = "colDepartCode";
            this.colDepartCode.OptionsColumn.AllowEdit = false;
            this.colDepartCode.OptionsColumn.AllowFocus = false;
            this.colDepartCode.OptionsColumn.FixedWidth = true;
            this.colDepartCode.OptionsColumn.ReadOnly = true;
            this.colDepartCode.Visible = true;
            this.colDepartCode.VisibleIndex = 8;
            this.colDepartCode.Width = 61;
            // 
            // colStockName
            // 
            this.colStockName.Caption = "Склад";
            this.colStockName.FieldName = "Склад";
            this.colStockName.MinWidth = 50;
            this.colStockName.Name = "colStockName";
            this.colStockName.OptionsColumn.AllowEdit = false;
            this.colStockName.OptionsColumn.AllowFocus = false;
            this.colStockName.OptionsColumn.FixedWidth = true;
            this.colStockName.OptionsColumn.ReadOnly = true;
            this.colStockName.Visible = true;
            this.colStockName.VisibleIndex = 9;
            this.colStockName.Width = 100;
            // 
            // colCompanyName
            // 
            this.colCompanyName.Caption = "Компания";
            this.colCompanyName.FieldName = "Компания";
            this.colCompanyName.MinWidth = 50;
            this.colCompanyName.Name = "colCompanyName";
            this.colCompanyName.OptionsColumn.AllowEdit = false;
            this.colCompanyName.OptionsColumn.AllowFocus = false;
            this.colCompanyName.OptionsColumn.FixedWidth = true;
            this.colCompanyName.OptionsColumn.ReadOnly = true;
            this.colCompanyName.Visible = true;
            this.colCompanyName.VisibleIndex = 10;
            this.colCompanyName.Width = 125;
            // 
            // colGroupList
            // 
            this.colGroupList.Caption = "Список групп. куда входит клиент";
            this.colGroupList.FieldName = "Список групп. куда входит клиент";
            this.colGroupList.MinWidth = 75;
            this.colGroupList.Name = "colGroupList";
            this.colGroupList.OptionsColumn.AllowEdit = false;
            this.colGroupList.OptionsColumn.AllowFocus = false;
            this.colGroupList.OptionsColumn.FixedWidth = true;
            this.colGroupList.OptionsColumn.ReadOnly = true;
            this.colGroupList.Visible = true;
            this.colGroupList.VisibleIndex = 11;
            this.colGroupList.Width = 150;
            // 
            // colWeight
            // 
            this.colWeight.Caption = "Вес, кг";
            this.colWeight.FieldName = "Вес, кг";
            this.colWeight.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colWeight.Name = "colWeight";
            this.colWeight.OptionsColumn.AllowEdit = false;
            this.colWeight.OptionsColumn.AllowFocus = false;
            this.colWeight.OptionsColumn.FixedWidth = true;
            this.colWeight.OptionsColumn.ReadOnly = true;
            this.colWeight.Visible = true;
            this.colWeight.VisibleIndex = 12;
            this.colWeight.Width = 50;
            // 
            // colSupplSateId
            // 
            this.colSupplSateId.Caption = "SupplSateId";
            this.colSupplSateId.FieldName = "SupplSateId";
            this.colSupplSateId.Name = "colSupplSateId";
            // 
            // colExcludeFromADJ
            // 
            this.colExcludeFromADJ.Caption = "Искл. из АВР";
            this.colExcludeFromADJ.ColumnEdit = this.repItemCheckEditBonus;
            this.colExcludeFromADJ.FieldName = "Искл. из АВР";
            this.colExcludeFromADJ.MinWidth = 50;
            this.colExcludeFromADJ.Name = "colExcludeFromADJ";
            this.colExcludeFromADJ.OptionsColumn.AllowEdit = false;
            this.colExcludeFromADJ.OptionsColumn.AllowFocus = false;
            this.colExcludeFromADJ.OptionsColumn.ReadOnly = true;
            this.colExcludeFromADJ.Visible = true;
            this.colExcludeFromADJ.VisibleIndex = 13;
            this.colExcludeFromADJ.Width = 50;
            // 
            // imageCollection
            // 
            this.imageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection.ImageStream")));
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl1.Controls.Add(this.EndDate);
            this.panelControl1.Controls.Add(this.BeginDate);
            this.panelControl1.Location = new System.Drawing.Point(3, 3);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(674, 30);
            this.toolTipController.SetSuperTip(this.panelControl1, null);
            this.panelControl1.TabIndex = 2;
            // 
            // EndDate
            // 
            this.EndDate.EditValue = null;
            this.EndDate.Location = new System.Drawing.Point(112, 5);
            this.EndDate.Name = "EndDate";
            this.EndDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.EndDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.EndDate.Size = new System.Drawing.Size(100, 20);
            this.EndDate.TabIndex = 1;
            this.EndDate.ToolTip = "Начало периода";
            this.EndDate.ToolTipController = this.toolTipController;
            this.EndDate.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.EndDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BeginDate_KeyPress);
            // 
            // BeginDate
            // 
            this.BeginDate.EditValue = null;
            this.BeginDate.Location = new System.Drawing.Point(6, 5);
            this.BeginDate.Name = "BeginDate";
            this.BeginDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.BeginDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.BeginDate.Size = new System.Drawing.Size(100, 20);
            this.BeginDate.TabIndex = 0;
            this.BeginDate.ToolTip = "Начало периода";
            this.BeginDate.ToolTipController = this.toolTipController;
            this.BeginDate.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.BeginDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BeginDate_KeyPress);
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
            this.menuMakeSupplExcludeFromADJ,
            this.toolStripSeparator2,
            this.menuEventLog});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(308, 170);
            this.toolTipController.SetSuperTip(this.contextMenuStrip, null);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // menuRefresh
            // 
            this.menuRefresh.Name = "menuRefresh";
            this.menuRefresh.Size = new System.Drawing.Size(307, 22);
            this.menuRefresh.Text = "Обновить";
            this.menuRefresh.Click += new System.EventHandler(this.menuRefresh_Click);
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
            this.menuClearPrices.Click += new System.EventHandler(this.menuClearPrices_Click);
            // 
            // menuReturnSupplStateToAutoProcessPrices
            // 
            this.menuReturnSupplStateToAutoProcessPrices.Name = "menuReturnSupplStateToAutoProcessPrices";
            this.menuReturnSupplStateToAutoProcessPrices.Size = new System.Drawing.Size(307, 22);
            this.menuReturnSupplStateToAutoProcessPrices.Text = "Вернуть заказ на автоматический расчет цен";
            this.menuReturnSupplStateToAutoProcessPrices.Click += new System.EventHandler(this.menuReturnSupplStateToAutoProcessPrices_Click);
            // 
            // menuMakeSupplDeleted
            // 
            this.menuMakeSupplDeleted.Name = "menuMakeSupplDeleted";
            this.menuMakeSupplDeleted.Size = new System.Drawing.Size(307, 22);
            this.menuMakeSupplDeleted.Text = "Удалить заказ";
            this.menuMakeSupplDeleted.ToolTipText = "Изменить состояние заказа на \"удален\"";
            this.menuMakeSupplDeleted.Click += new System.EventHandler(this.menuMakeSupplDeleted_Click);
            // 
            // menuMakeSupplExcludeFromADJ
            // 
            this.menuMakeSupplExcludeFromADJ.Name = "menuMakeSupplExcludeFromADJ";
            this.menuMakeSupplExcludeFromADJ.Size = new System.Drawing.Size(307, 22);
            this.menuMakeSupplExcludeFromADJ.Text = "Исключить из акта выполненных работ";
            this.menuMakeSupplExcludeFromADJ.Click += new System.EventHandler(this.menuMakeSupplExcludeFromADJ_Click);
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
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.treeListSupplItems, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.panelControl2, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(680, 375);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel2, null);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // treeListSupplItems
            // 
            this.treeListSupplItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeListSupplItems.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colOwnerProductName,
            this.colIsImporter,
            this.colChargePercent,
            this.colDiscountRetro,
            this.colDiscountFix,
            this.colDiscountTradeEq,
            this.colNDSPercent,
            this.colProductName,
            this.colOrderQty,
            this.colQty,
            this.colPrice,
            this.colAllPrice,
            this.colCurrencyPrice,
            this.colAllCurrencyPrice,
            this.colDiscountPercent,
            this.colDiscountPrice,
            this.colCurrencyDiscountPrice,
            this.colTotalPrice,
            this.colCurrencyTotalPrice,
            this.colDiscountList,
            this.colDescription});
            this.treeListSupplItems.Location = new System.Drawing.Point(3, 40);
            this.treeListSupplItems.Name = "treeListSupplItems";
            this.treeListSupplItems.OptionsBehavior.Editable = false;
            this.treeListSupplItems.OptionsView.AutoWidth = false;
            this.treeListSupplItems.OptionsView.ShowSummaryFooter = true;
            this.treeListSupplItems.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repItemCheckEdit});
            this.treeListSupplItems.Size = new System.Drawing.Size(674, 332);
            this.treeListSupplItems.TabIndex = 0;
            this.treeListSupplItems.ToolTipController = this.toolTipController;
            // 
            // colOwnerProductName
            // 
            this.colOwnerProductName.Caption = "Товарная марка";
            this.colOwnerProductName.FieldName = "Товарная марка";
            this.colOwnerProductName.MinWidth = 75;
            this.colOwnerProductName.Name = "colOwnerProductName";
            this.colOwnerProductName.OptionsColumn.AllowEdit = false;
            this.colOwnerProductName.OptionsColumn.AllowFocus = false;
            this.colOwnerProductName.OptionsColumn.FixedWidth = true;
            this.colOwnerProductName.OptionsColumn.ReadOnly = true;
            this.colOwnerProductName.Visible = true;
            this.colOwnerProductName.VisibleIndex = 0;
            this.colOwnerProductName.Width = 94;
            // 
            // colIsImporter
            // 
            this.colIsImporter.Caption = "Товар импортера";
            this.colIsImporter.ColumnEdit = this.repItemCheckEdit;
            this.colIsImporter.FieldName = "Товар импортера";
            this.colIsImporter.MinWidth = 50;
            this.colIsImporter.Name = "colIsImporter";
            this.colIsImporter.OptionsColumn.AllowEdit = false;
            this.colIsImporter.OptionsColumn.AllowFocus = false;
            this.colIsImporter.OptionsColumn.FixedWidth = true;
            this.colIsImporter.OptionsColumn.ReadOnly = true;
            this.colIsImporter.Visible = true;
            this.colIsImporter.VisibleIndex = 1;
            this.colIsImporter.Width = 97;
            // 
            // repItemCheckEdit
            // 
            this.repItemCheckEdit.AutoHeight = false;
            this.repItemCheckEdit.Name = "repItemCheckEdit";
            // 
            // colChargePercent
            // 
            this.colChargePercent.AppearanceCell.Options.UseTextOptions = true;
            this.colChargePercent.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colChargePercent.Caption = "Надбавка, %";
            this.colChargePercent.FieldName = "Надбавка, %";
            this.colChargePercent.MinWidth = 40;
            this.colChargePercent.Name = "colChargePercent";
            this.colChargePercent.OptionsColumn.AllowEdit = false;
            this.colChargePercent.OptionsColumn.AllowFocus = false;
            this.colChargePercent.OptionsColumn.FixedWidth = true;
            this.colChargePercent.OptionsColumn.ReadOnly = true;
            this.colChargePercent.Visible = true;
            this.colChargePercent.VisibleIndex = 2;
            this.colChargePercent.Width = 84;
            // 
            // colDiscountRetro
            // 
            this.colDiscountRetro.AppearanceCell.Options.UseTextOptions = true;
            this.colDiscountRetro.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDiscountRetro.Caption = "Ретро-скидка, %";
            this.colDiscountRetro.FieldName = "Ретро-скидка, %";
            this.colDiscountRetro.MinWidth = 50;
            this.colDiscountRetro.Name = "colDiscountRetro";
            this.colDiscountRetro.OptionsColumn.AllowEdit = false;
            this.colDiscountRetro.OptionsColumn.AllowFocus = false;
            this.colDiscountRetro.OptionsColumn.FixedWidth = true;
            this.colDiscountRetro.OptionsColumn.ReadOnly = true;
            this.colDiscountRetro.Visible = true;
            this.colDiscountRetro.VisibleIndex = 3;
            this.colDiscountRetro.Width = 101;
            // 
            // colDiscountFix
            // 
            this.colDiscountFix.Caption = "Фикс. скидка, %";
            this.colDiscountFix.FieldName = "Фикс. скидка, %";
            this.colDiscountFix.MinWidth = 50;
            this.colDiscountFix.Name = "colDiscountFix";
            this.colDiscountFix.OptionsColumn.AllowEdit = false;
            this.colDiscountFix.OptionsColumn.AllowFocus = false;
            this.colDiscountFix.OptionsColumn.FixedWidth = true;
            this.colDiscountFix.OptionsColumn.ReadOnly = true;
            this.colDiscountFix.Visible = true;
            this.colDiscountFix.VisibleIndex = 4;
            this.colDiscountFix.Width = 101;
            // 
            // colDiscountTradeEq
            // 
            this.colDiscountTradeEq.Caption = "Скидка оборуд., %";
            this.colDiscountTradeEq.FieldName = "Скидка оборуд., %";
            this.colDiscountTradeEq.MinWidth = 50;
            this.colDiscountTradeEq.Name = "colDiscountTradeEq";
            this.colDiscountTradeEq.OptionsColumn.AllowEdit = false;
            this.colDiscountTradeEq.OptionsColumn.AllowFocus = false;
            this.colDiscountTradeEq.OptionsColumn.FixedWidth = true;
            this.colDiscountTradeEq.OptionsColumn.ReadOnly = true;
            this.colDiscountTradeEq.Visible = true;
            this.colDiscountTradeEq.VisibleIndex = 5;
            this.colDiscountTradeEq.Width = 101;
            // 
            // colNDSPercent
            // 
            this.colNDSPercent.AppearanceCell.Options.UseTextOptions = true;
            this.colNDSPercent.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNDSPercent.Caption = "Ставка НДС, %";
            this.colNDSPercent.FieldName = "Ставка НДС, %";
            this.colNDSPercent.MinWidth = 50;
            this.colNDSPercent.Name = "colNDSPercent";
            this.colNDSPercent.OptionsColumn.AllowEdit = false;
            this.colNDSPercent.OptionsColumn.AllowFocus = false;
            this.colNDSPercent.OptionsColumn.FixedWidth = true;
            this.colNDSPercent.OptionsColumn.ReadOnly = true;
            this.colNDSPercent.Visible = true;
            this.colNDSPercent.VisibleIndex = 6;
            this.colNDSPercent.Width = 98;
            // 
            // colProductName
            // 
            this.colProductName.Caption = "Товар";
            this.colProductName.FieldName = "Товар";
            this.colProductName.MinWidth = 70;
            this.colProductName.Name = "colProductName";
            this.colProductName.OptionsColumn.AllowEdit = false;
            this.colProductName.OptionsColumn.AllowFocus = false;
            this.colProductName.OptionsColumn.FixedWidth = true;
            this.colProductName.OptionsColumn.ReadOnly = true;
            this.colProductName.Visible = true;
            this.colProductName.VisibleIndex = 7;
            this.colProductName.Width = 226;
            // 
            // colOrderQty
            // 
            this.colOrderQty.AppearanceCell.Options.UseTextOptions = true;
            this.colOrderQty.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colOrderQty.Caption = "Заказано, шт.";
            this.colOrderQty.FieldName = "Заказано, шт.";
            this.colOrderQty.Format.FormatString = "{0:### ### ##0}";
            this.colOrderQty.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colOrderQty.MinWidth = 30;
            this.colOrderQty.Name = "colOrderQty";
            this.colOrderQty.OptionsColumn.AllowEdit = false;
            this.colOrderQty.OptionsColumn.AllowFocus = false;
            this.colOrderQty.OptionsColumn.FixedWidth = true;
            this.colOrderQty.OptionsColumn.ReadOnly = true;
            this.colOrderQty.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum;
            this.colOrderQty.SummaryFooterStrFormat = "{0:### ### ##0}";
            this.colOrderQty.Visible = true;
            this.colOrderQty.VisibleIndex = 8;
            this.colOrderQty.Width = 85;
            // 
            // colQty
            // 
            this.colQty.AppearanceCell.Options.UseTextOptions = true;
            this.colQty.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colQty.Caption = "Количество, шт.";
            this.colQty.FieldName = "Количество, шт.";
            this.colQty.Format.FormatString = "{0:### ### ##0}";
            this.colQty.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colQty.MinWidth = 30;
            this.colQty.Name = "colQty";
            this.colQty.OptionsColumn.AllowEdit = false;
            this.colQty.OptionsColumn.AllowFocus = false;
            this.colQty.OptionsColumn.FixedWidth = true;
            this.colQty.OptionsColumn.ReadOnly = true;
            this.colQty.RowFooterSummaryStrFormat = "";
            this.colQty.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum;
            this.colQty.SummaryFooterStrFormat = "{0:### ### ##0}";
            this.colQty.Visible = true;
            this.colQty.VisibleIndex = 9;
            this.colQty.Width = 103;
            // 
            // colPrice
            // 
            this.colPrice.AppearanceCell.Options.UseTextOptions = true;
            this.colPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colPrice.Caption = "Цена, руб.";
            this.colPrice.FieldName = "Цена, руб.";
            this.colPrice.Format.FormatString = "{0:### ### ###.##}";
            this.colPrice.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPrice.MinWidth = 50;
            this.colPrice.Name = "colPrice";
            this.colPrice.OptionsColumn.AllowEdit = false;
            this.colPrice.OptionsColumn.AllowFocus = false;
            this.colPrice.OptionsColumn.FixedWidth = true;
            this.colPrice.OptionsColumn.ReadOnly = true;
            this.colPrice.Visible = true;
            this.colPrice.VisibleIndex = 10;
            // 
            // colAllPrice
            // 
            this.colAllPrice.AppearanceCell.Options.UseTextOptions = true;
            this.colAllPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colAllPrice.Caption = "Сумма, руб.";
            this.colAllPrice.FieldName = "Сумма, руб.";
            this.colAllPrice.Format.FormatString = "{0:### ### ###.##}";
            this.colAllPrice.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAllPrice.MinWidth = 50;
            this.colAllPrice.Name = "colAllPrice";
            this.colAllPrice.OptionsColumn.AllowEdit = false;
            this.colAllPrice.OptionsColumn.AllowFocus = false;
            this.colAllPrice.OptionsColumn.FixedWidth = true;
            this.colAllPrice.OptionsColumn.ReadOnly = true;
            this.colAllPrice.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum;
            this.colAllPrice.SummaryFooterStrFormat = "{0:### ### ##0.00}";
            this.colAllPrice.Visible = true;
            this.colAllPrice.VisibleIndex = 11;
            // 
            // colCurrencyPrice
            // 
            this.colCurrencyPrice.AppearanceCell.Options.UseTextOptions = true;
            this.colCurrencyPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colCurrencyPrice.Caption = "Цена, вал.";
            this.colCurrencyPrice.FieldName = "Цена, вал.";
            this.colCurrencyPrice.Format.FormatString = "{0:### ### ###.###}";
            this.colCurrencyPrice.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colCurrencyPrice.MinWidth = 50;
            this.colCurrencyPrice.Name = "colCurrencyPrice";
            this.colCurrencyPrice.OptionsColumn.AllowEdit = false;
            this.colCurrencyPrice.OptionsColumn.AllowFocus = false;
            this.colCurrencyPrice.OptionsColumn.FixedWidth = true;
            this.colCurrencyPrice.OptionsColumn.ReadOnly = true;
            this.colCurrencyPrice.Visible = true;
            this.colCurrencyPrice.VisibleIndex = 12;
            // 
            // colAllCurrencyPrice
            // 
            this.colAllCurrencyPrice.AppearanceCell.Options.UseTextOptions = true;
            this.colAllCurrencyPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colAllCurrencyPrice.Caption = "Сумма, вал.";
            this.colAllCurrencyPrice.FieldName = "Сумма, вал.";
            this.colAllCurrencyPrice.Format.FormatString = "{0:### ### ###.###}";
            this.colAllCurrencyPrice.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAllCurrencyPrice.MinWidth = 50;
            this.colAllCurrencyPrice.Name = "colAllCurrencyPrice";
            this.colAllCurrencyPrice.OptionsColumn.AllowEdit = false;
            this.colAllCurrencyPrice.OptionsColumn.AllowFocus = false;
            this.colAllCurrencyPrice.OptionsColumn.FixedWidth = true;
            this.colAllCurrencyPrice.OptionsColumn.ReadOnly = true;
            this.colAllCurrencyPrice.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum;
            this.colAllCurrencyPrice.SummaryFooterStrFormat = "{0:### ### ##0.00}";
            this.colAllCurrencyPrice.Visible = true;
            this.colAllCurrencyPrice.VisibleIndex = 13;
            // 
            // colDiscountPercent
            // 
            this.colDiscountPercent.AppearanceCell.Options.UseTextOptions = true;
            this.colDiscountPercent.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colDiscountPercent.Caption = "Скидка, %";
            this.colDiscountPercent.FieldName = "Скидка, %";
            this.colDiscountPercent.MinWidth = 30;
            this.colDiscountPercent.Name = "colDiscountPercent";
            this.colDiscountPercent.OptionsColumn.AllowEdit = false;
            this.colDiscountPercent.OptionsColumn.AllowFocus = false;
            this.colDiscountPercent.OptionsColumn.FixedWidth = true;
            this.colDiscountPercent.OptionsColumn.ReadOnly = true;
            this.colDiscountPercent.Visible = true;
            this.colDiscountPercent.VisibleIndex = 14;
            // 
            // colDiscountPrice
            // 
            this.colDiscountPrice.Caption = "Цена со скидкой, руб.";
            this.colDiscountPrice.FieldName = "Цена со скидкой, руб.";
            this.colDiscountPrice.Format.FormatString = "{0:### ### ###.##}";
            this.colDiscountPrice.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDiscountPrice.MinWidth = 50;
            this.colDiscountPrice.Name = "colDiscountPrice";
            this.colDiscountPrice.OptionsColumn.AllowEdit = false;
            this.colDiscountPrice.OptionsColumn.AllowFocus = false;
            this.colDiscountPrice.OptionsColumn.FixedWidth = true;
            this.colDiscountPrice.OptionsColumn.ReadOnly = true;
            this.colDiscountPrice.Visible = true;
            this.colDiscountPrice.VisibleIndex = 15;
            this.colDiscountPrice.Width = 134;
            // 
            // colCurrencyDiscountPrice
            // 
            this.colCurrencyDiscountPrice.Caption = "Цена со скидкой, вал.";
            this.colCurrencyDiscountPrice.FieldName = "Цена со скидкой, вал.";
            this.colCurrencyDiscountPrice.Format.FormatString = "{0:### ### ###.###}";
            this.colCurrencyDiscountPrice.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colCurrencyDiscountPrice.MinWidth = 50;
            this.colCurrencyDiscountPrice.Name = "colCurrencyDiscountPrice";
            this.colCurrencyDiscountPrice.OptionsColumn.AllowEdit = false;
            this.colCurrencyDiscountPrice.OptionsColumn.AllowFocus = false;
            this.colCurrencyDiscountPrice.OptionsColumn.FixedWidth = true;
            this.colCurrencyDiscountPrice.OptionsColumn.ReadOnly = true;
            this.colCurrencyDiscountPrice.Visible = true;
            this.colCurrencyDiscountPrice.VisibleIndex = 16;
            this.colCurrencyDiscountPrice.Width = 128;
            // 
            // colTotalPrice
            // 
            this.colTotalPrice.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTotalPrice.Caption = "Сумма со скидкой, руб.";
            this.colTotalPrice.FieldName = "Сумма со скидкой, руб.";
            this.colTotalPrice.Format.FormatString = "{0:### ### ###.##}";
            this.colTotalPrice.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotalPrice.MinWidth = 50;
            this.colTotalPrice.Name = "colTotalPrice";
            this.colTotalPrice.OptionsColumn.AllowEdit = false;
            this.colTotalPrice.OptionsColumn.AllowFocus = false;
            this.colTotalPrice.OptionsColumn.FixedWidth = true;
            this.colTotalPrice.OptionsColumn.ReadOnly = true;
            this.colTotalPrice.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum;
            this.colTotalPrice.SummaryFooterStrFormat = "{0:### ### ##0.00}";
            this.colTotalPrice.Visible = true;
            this.colTotalPrice.VisibleIndex = 17;
            this.colTotalPrice.Width = 133;
            // 
            // colCurrencyTotalPrice
            // 
            this.colCurrencyTotalPrice.AppearanceCell.Options.UseTextOptions = true;
            this.colCurrencyTotalPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colCurrencyTotalPrice.Caption = "Сумма со скидкой, вал.";
            this.colCurrencyTotalPrice.FieldName = "Сумма со скидкой, вал.";
            this.colCurrencyTotalPrice.Format.FormatString = "{0:### ### ###.###}";
            this.colCurrencyTotalPrice.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colCurrencyTotalPrice.MinWidth = 50;
            this.colCurrencyTotalPrice.Name = "colCurrencyTotalPrice";
            this.colCurrencyTotalPrice.OptionsColumn.AllowEdit = false;
            this.colCurrencyTotalPrice.OptionsColumn.AllowFocus = false;
            this.colCurrencyTotalPrice.OptionsColumn.FixedWidth = true;
            this.colCurrencyTotalPrice.OptionsColumn.ReadOnly = true;
            this.colCurrencyTotalPrice.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum;
            this.colCurrencyTotalPrice.SummaryFooterStrFormat = "{0:### ### ##0.00}";
            this.colCurrencyTotalPrice.Visible = true;
            this.colCurrencyTotalPrice.VisibleIndex = 18;
            this.colCurrencyTotalPrice.Width = 133;
            // 
            // colDiscountList
            // 
            this.colDiscountList.Caption = "Состав скидки";
            this.colDiscountList.FieldName = "Состав скидки";
            this.colDiscountList.MinWidth = 100;
            this.colDiscountList.Name = "colDiscountList";
            this.colDiscountList.OptionsColumn.AllowEdit = false;
            this.colDiscountList.OptionsColumn.AllowFocus = false;
            this.colDiscountList.OptionsColumn.FixedWidth = true;
            this.colDiscountList.OptionsColumn.ReadOnly = true;
            this.colDiscountList.Visible = true;
            this.colDiscountList.VisibleIndex = 19;
            this.colDiscountList.Width = 150;
            // 
            // colDescription
            // 
            this.colDescription.Caption = "Примечание";
            this.colDescription.FieldName = "Примечание";
            this.colDescription.MinWidth = 100;
            this.colDescription.Name = "colDescription";
            this.colDescription.OptionsColumn.AllowEdit = false;
            this.colDescription.OptionsColumn.AllowFocus = false;
            this.colDescription.OptionsColumn.FixedWidth = true;
            this.colDescription.OptionsColumn.ReadOnly = true;
            this.colDescription.Visible = true;
            this.colDescription.VisibleIndex = 20;
            this.colDescription.Width = 250;
            // 
            // panelControl2
            // 
            this.panelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl2.Controls.Add(this.barBtnPrint);
            this.panelControl2.Controls.Add(this.btnToTabSupplList);
            this.panelControl2.Location = new System.Drawing.Point(3, 3);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(674, 31);
            this.toolTipController.SetSuperTip(this.panelControl2, null);
            this.panelControl2.TabIndex = 1;
            // 
            // barBtnPrint
            // 
            this.barBtnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.barBtnPrint.Image = global::ERPMercuryProcessingOrder.Properties.Resources.printer2;
            this.barBtnPrint.Location = new System.Drawing.Point(636, 4);
            this.barBtnPrint.Margin = new System.Windows.Forms.Padding(1);
            this.barBtnPrint.Name = "barBtnPrint";
            this.barBtnPrint.Size = new System.Drawing.Size(35, 23);
            this.barBtnPrint.TabIndex = 3;
            this.barBtnPrint.ToolTip = "Печать списка продукции";
            this.barBtnPrint.ToolTipController = this.toolTipController;
            this.barBtnPrint.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.barBtnPrint.Click += new System.EventHandler(this.barBtnPrint_Click);
            // 
            // btnToTabSupplList
            // 
            this.btnToTabSupplList.Image = global::ERPMercuryProcessingOrder.Properties.Resources.navigate_left;
            this.btnToTabSupplList.Location = new System.Drawing.Point(4, 4);
            this.btnToTabSupplList.Margin = new System.Windows.Forms.Padding(0);
            this.btnToTabSupplList.Name = "btnToTabSupplList";
            this.btnToTabSupplList.Size = new System.Drawing.Size(35, 23);
            this.btnToTabSupplList.TabIndex = 2;
            this.btnToTabSupplList.ToolTip = "Вернуться назад";
            this.btnToTabSupplList.ToolTipController = this.toolTipController;
            this.btnToTabSupplList.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.btnToTabSupplList.Click += new System.EventHandler(this.btnToTabSupplList_Click);
            // 
            // barManager
            // 
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barBtnRefresh,
            this.barBtnProcess,
            this.barBtnClearPrices,
            this.barBtnPrintList});
            this.barManager.MaxItemId = 4;
            this.barManager.ToolTipController = this.toolTipController;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnRefresh),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnProcess),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnClearPrices),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnPrintList)});
            this.bar1.Text = "Tools";
            // 
            // barBtnRefresh
            // 
            this.barBtnRefresh.Glyph = global::ERPMercuryProcessingOrder.Properties.Resources.refresh;
            this.barBtnRefresh.Hint = "Обновить список";
            this.barBtnRefresh.Id = 0;
            this.barBtnRefresh.Name = "barBtnRefresh";
            this.barBtnRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnRefresh_ItemClick);
            // 
            // barBtnProcess
            // 
            this.barBtnProcess.Glyph = global::ERPMercuryProcessingOrder.Properties.Resources.IMAGES_MONEYSMALL_PNG;
            this.barBtnProcess.Hint = "Рассчитать цены";
            this.barBtnProcess.Id = 1;
            this.barBtnProcess.Name = "barBtnProcess";
            this.barBtnProcess.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnProcess_ItemClick);
            // 
            // barBtnClearPrices
            // 
            this.barBtnClearPrices.Glyph = global::ERPMercuryProcessingOrder.Properties.Resources.brush3;
            this.barBtnClearPrices.Hint = "Обнулить цены";
            this.barBtnClearPrices.Id = 2;
            this.barBtnClearPrices.Name = "barBtnClearPrices";
            this.barBtnClearPrices.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnClearPrices_ItemClick);
            // 
            // barBtnPrintList
            // 
            this.barBtnPrintList.Glyph = global::ERPMercuryProcessingOrder.Properties.Resources.printer2;
            this.barBtnPrintList.Hint = "Печать списка заказов";
            this.barBtnPrintList.Id = 3;
            this.barBtnPrintList.Name = "barBtnPrintList";
            this.barBtnPrintList.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnPrintList_ItemClick);
            // 
            // tabControl
            // 
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 26);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedTabPage = this.tabSupplList;
            this.tabControl.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            this.tabControl.Size = new System.Drawing.Size(689, 384);
            this.tabControl.TabIndex = 4;
            this.tabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabSupplList,
            this.tabSuppl});
            this.tabControl.Text = "xtraTabControl1";
            this.tabControl.ToolTipController = this.toolTipController;
            // 
            // tabSupplList
            // 
            this.tabSupplList.Controls.Add(this.tableLayoutPanel1);
            this.tabSupplList.Name = "tabSupplList";
            this.tabSupplList.Size = new System.Drawing.Size(680, 375);
            this.tabSupplList.Text = "Заявки";
            // 
            // tabSuppl
            // 
            this.tabSuppl.Controls.Add(this.tableLayoutPanel2);
            this.tabSuppl.Name = "tabSuppl";
            this.tabSuppl.Size = new System.Drawing.Size(680, 375);
            this.tabSuppl.Text = "Содержимое заказа";
            // 
            // frmPDASupplList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 410);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmPDASupplList";
            this.toolTipController.SetSuperTip(this, null);
            this.Text = "Список заказов (заявки с КПК)";
            this.Load += new System.EventHandler(this.frmPDASupplList_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnlInfo.ResumeLayout(false);
            this.pnlInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSupplOrderQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSupplState.Properties)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repItemCheckEditBonus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.EndDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EndDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BeginDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BeginDate.Properties)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListSupplItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repItemCheckEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabSupplList.ResumeLayout(false);
            this.tabSuppl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.ToolTipController toolTipController;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barBtnRefresh;
        private DevExpress.XtraBars.BarButtonItem barBtnProcess;
        private DevExpress.XtraBars.BarButtonItem barBtnClearPrices;
        private DevExpress.XtraTab.XtraTabControl tabControl;
        private DevExpress.XtraTab.XtraTabPage tabSupplList;
        private DevExpress.XtraTab.XtraTabPage tabSuppl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnlInfo;
        private DevExpress.XtraEditors.PictureEdit pictureEdit;
        private DevExpress.XtraEditors.TextEdit txtSupplQty;
        private DevExpress.XtraEditors.TextEdit txtSupplPositionCount;
        private DevExpress.XtraEditors.TextEdit txtSupplNum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txtSupplAllDiscountPrice;
        private DevExpress.XtraEditors.TextEdit txtSupplAllDiscount;
        private DevExpress.XtraEditors.TextEdit txtSupplAllPrice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.TextEdit txtSupplAllDiscountCurrencyPrice;
        private DevExpress.XtraEditors.TextEdit txtSupplAllCurrencyDiscount;
        private DevExpress.XtraEditors.TextEdit txtSupplAllCurrencyPrice;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private DevExpress.XtraTreeList.TreeList treeList;
        private DevExpress.Utils.ImageCollection imageCollection;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colSupplDate;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colSupplNum;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCustomer;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDepartCode;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colSupplVersion;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuCalcPrice;
        private System.Windows.Forms.ToolStripMenuItem menuClearPrices;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colChildCode;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colSupplState;
        private System.Windows.Forms.ToolStripMenuItem menuEventLog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private DevExpress.XtraTreeList.TreeList treeListSupplItems;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colProductName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colOrderQty;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colQty;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colPrice;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colAllPrice;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCurrencyPrice;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colAllCurrencyPrice;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDiscountPercent;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colTotalPrice;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCurrencyTotalPrice;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colStockName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCompanyName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colGroupList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colOwnerProductName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colIsImporter;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repItemCheckEdit;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colChargePercent;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDiscountRetro;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colNDSPercent;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDiscountPrice;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCurrencyDiscountPrice;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDiscountFix;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDiscountTradeEq;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.DateEdit BeginDate;
        private DevExpress.XtraEditors.DateEdit EndDate;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton barBtnPrint;
        private DevExpress.XtraEditors.SimpleButton btnToTabSupplList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDescription;
        private System.Windows.Forms.Label label10;
        private DevExpress.XtraEditors.TextEdit txtSupplState;
        private DevExpress.XtraEditors.TextEdit txtSupplOrderQty;
        private System.Windows.Forms.Label label11;
        private DevExpress.XtraBars.BarButtonItem barBtnPrintList;
        private System.Windows.Forms.ToolStripMenuItem menuReturnSupplStateToAutoProcessPrices;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colSupplDeliveryDate;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colMoneyBonus;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repItemCheckEditBonus;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDiscountList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colWeight;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colSupplSateId;
        private System.Windows.Forms.ToolStripMenuItem menuMakeSupplDeleted;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colExcludeFromADJ;
        private System.Windows.Forms.ToolStripMenuItem menuMakeSupplExcludeFromADJ;
    }
}
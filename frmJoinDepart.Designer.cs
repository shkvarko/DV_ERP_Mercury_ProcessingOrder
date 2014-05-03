namespace ERPMercuryProcessingOrder
{
    partial class frmJoinDepart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmJoinDepart));
            this.toolTipController = new DevExpress.Utils.ToolTipController(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuCalcPrice = new System.Windows.Forms.ToolStripMenuItem();
            this.menuClearPrices = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReturnSupplStateToAutoProcessPrices = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMakeSupplDeleted = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEventLog = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuCreateOrderBlank = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGoToSuppl = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemPaymentHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl = new DevExpress.XtraTab.XtraTabControl();
            this.tabPageViewer = new DevExpress.XtraTab.XtraTabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.WaybilllNum = new DevExpress.XtraEditors.TextEdit();
            this.labelControl29 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.Depart = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnUnionWaybill = new DevExpress.XtraEditors.SimpleButton();
            this.controlNavigator = new DevExpress.XtraEditors.ControlNavigator();
            this.panelControlSearchCondition = new DevExpress.XtraEditors.PanelControl();
            this.IsBonus = new DevExpress.XtraEditors.CheckEdit();
            this.barBtnRefresh = new DevExpress.XtraEditors.SimpleButton();
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
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.gridControlList = new DevExpress.XtraGrid.GridControl();
            this.gridViewList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.imageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuStrip.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPageViewer.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WaybilllNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Depart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlSearchCondition)).BeginInit();
            this.panelControlSearchCondition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IsBonus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxStock.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEndDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEndDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtBeginDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtBeginDate.Properties)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection)).BeginInit();
            this.SuspendLayout();
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
            this.menuEventLog,
            this.toolStripMenuItem1,
            this.menuCreateOrderBlank,
            this.menuGoToSuppl,
            this.menuItemPaymentHistory});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(308, 214);
            this.toolTipController.SetSuperTip(this.contextMenuStrip, null);
            // 
            // menuRefresh
            // 
            this.menuRefresh.Name = "menuRefresh";
            this.menuRefresh.Size = new System.Drawing.Size(307, 22);
            this.menuRefresh.Text = "Обновить";
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
            this.menuCalcPrice.Visible = false;
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
            this.menuMakeSupplDeleted.Visible = false;
            // 
            // menuEventLog
            // 
            this.menuEventLog.Name = "menuEventLog";
            this.menuEventLog.Size = new System.Drawing.Size(307, 22);
            this.menuEventLog.Text = "Журнал событий";
            this.menuEventLog.Visible = false;
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(304, 6);
            this.toolStripMenuItem1.Visible = false;
            // 
            // menuCreateOrderBlank
            // 
            this.menuCreateOrderBlank.Name = "menuCreateOrderBlank";
            this.menuCreateOrderBlank.Size = new System.Drawing.Size(307, 22);
            this.menuCreateOrderBlank.Text = "Формирование бланка заказа";
            this.menuCreateOrderBlank.ToolTipText = "Формирование бланка заказа в MS Excel";
            this.menuCreateOrderBlank.Visible = false;
            // 
            // menuGoToSuppl
            // 
            this.menuGoToSuppl.Name = "menuGoToSuppl";
            this.menuGoToSuppl.Size = new System.Drawing.Size(307, 22);
            this.menuGoToSuppl.Text = "Перейти к заказу...";
            // 
            // menuItemPaymentHistory
            // 
            this.menuItemPaymentHistory.Name = "menuItemPaymentHistory";
            this.menuItemPaymentHistory.Size = new System.Drawing.Size(307, 22);
            this.menuItemPaymentHistory.Text = "Журнал оплат...";
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(977, 480);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel1, null);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // tabControl
            // 
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(3, 3);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedTabPage = this.tabPageViewer;
            this.tabControl.Size = new System.Drawing.Size(971, 474);
            this.tabControl.TabIndex = 0;
            this.tabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPageViewer});
            this.tabControl.Text = "xtraTabControl1";
            this.tabControl.ToolTipController = this.toolTipController;
            // 
            // tabPageViewer
            // 
            this.tabPageViewer.Controls.Add(this.tableLayoutPanel4);
            this.tabPageViewer.Name = "tabPageViewer";
            this.tabPageViewer.Size = new System.Drawing.Size(962, 443);
            this.tabPageViewer.Text = "tabPageViewer";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.panelControl1, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.controlNavigator, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.panelControlSearchCondition, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 4;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(962, 443);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel4, null);
            this.tableLayoutPanel4.TabIndex = 2;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.WaybilllNum);
            this.panelControl1.Controls.Add(this.labelControl29);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.Depart);
            this.panelControl1.Controls.Add(this.btnUnionWaybill);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(1, 389);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(1);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(960, 53);
            this.toolTipController.SetSuperTip(this.panelControl1, null);
            this.panelControl1.TabIndex = 24;
            // 
            // WaybilllNum
            // 
            this.WaybilllNum.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.WaybilllNum.Location = new System.Drawing.Point(84, 19);
            this.WaybilllNum.Name = "WaybilllNum";
            this.WaybilllNum.Properties.Appearance.Options.UseTextOptions = true;
            this.WaybilllNum.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.WaybilllNum.Size = new System.Drawing.Size(165, 20);
            this.WaybilllNum.TabIndex = 1;
            // 
            // labelControl29
            // 
            this.labelControl29.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl29.Location = new System.Drawing.Point(5, 23);
            this.labelControl29.Name = "labelControl29";
            this.labelControl29.Size = new System.Drawing.Size(73, 13);
            this.labelControl29.TabIndex = 0;
            this.labelControl29.Text = "Номер док-та:";
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl1.Location = new System.Drawing.Point(256, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(84, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Подразделение:";
            // 
            // Depart
            // 
            this.Depart.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Depart.Location = new System.Drawing.Point(346, 19);
            this.Depart.Name = "Depart";
            this.Depart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Depart.Properties.PopupSizeable = true;
            this.Depart.Properties.Sorted = true;
            this.Depart.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.Depart.Size = new System.Drawing.Size(94, 20);
            this.Depart.TabIndex = 3;
            this.Depart.ToolTip = "Подразделение";
            this.Depart.ToolTipController = this.toolTipController;
            this.Depart.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            // 
            // btnUnionWaybill
            // 
            this.btnUnionWaybill.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnUnionWaybill.ImageAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnUnionWaybill.Location = new System.Drawing.Point(453, 17);
            this.btnUnionWaybill.Margin = new System.Windows.Forms.Padding(1);
            this.btnUnionWaybill.Name = "btnUnionWaybill";
            this.btnUnionWaybill.Size = new System.Drawing.Size(92, 25);
            this.btnUnionWaybill.TabIndex = 4;
            this.btnUnionWaybill.Text = "Объединить";
            this.btnUnionWaybill.ToolTip = "объединить накладные";
            this.btnUnionWaybill.ToolTipController = this.toolTipController;
            this.btnUnionWaybill.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            // 
            // controlNavigator
            // 
            this.controlNavigator.Buttons.Append.Visible = false;
            this.controlNavigator.Buttons.CancelEdit.Visible = false;
            this.controlNavigator.Buttons.Edit.Visible = false;
            this.controlNavigator.Buttons.EndEdit.Visible = false;
            this.controlNavigator.Location = new System.Drawing.Point(3, 58);
            this.controlNavigator.Name = "controlNavigator";
            this.controlNavigator.NavigatableControl = this.gridControlList;
            this.controlNavigator.ShowToolTips = true;
            this.controlNavigator.Size = new System.Drawing.Size(204, 19);
            this.controlNavigator.TabIndex = 23;
            this.controlNavigator.Text = "controlNavigator";
            this.controlNavigator.TextLocation = DevExpress.XtraEditors.NavigatorButtonsTextLocation.Center;
            this.controlNavigator.ToolTipController = this.toolTipController;
            // 
            // panelControlSearchCondition
            // 
            this.panelControlSearchCondition.Controls.Add(this.IsBonus);
            this.panelControlSearchCondition.Controls.Add(this.barBtnRefresh);
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
            this.panelControlSearchCondition.Location = new System.Drawing.Point(1, 1);
            this.panelControlSearchCondition.Margin = new System.Windows.Forms.Padding(1);
            this.panelControlSearchCondition.Name = "panelControlSearchCondition";
            this.panelControlSearchCondition.Size = new System.Drawing.Size(960, 53);
            this.toolTipController.SetSuperTip(this.panelControlSearchCondition, null);
            this.panelControlSearchCondition.TabIndex = 1;
            // 
            // IsBonus
            // 
            this.IsBonus.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.IsBonus.Location = new System.Drawing.Point(810, 22);
            this.IsBonus.Name = "IsBonus";
            this.IsBonus.Properties.Caption = "Бонус";
            this.IsBonus.Size = new System.Drawing.Size(58, 19);
            this.IsBonus.TabIndex = 6;
            this.IsBonus.ToolTip = "Заказ является бонусом для клиента";
            this.IsBonus.ToolTipController = this.toolTipController;
            this.IsBonus.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            // 
            // barBtnRefresh
            // 
            this.barBtnRefresh.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.barBtnRefresh.ImageAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.barBtnRefresh.Location = new System.Drawing.Point(872, 19);
            this.barBtnRefresh.Margin = new System.Windows.Forms.Padding(1);
            this.barBtnRefresh.Name = "barBtnRefresh";
            this.barBtnRefresh.Size = new System.Drawing.Size(75, 25);
            this.barBtnRefresh.TabIndex = 7;
            this.barBtnRefresh.Text = "Поиск";
            this.barBtnRefresh.ToolTip = "Обновить список накладных";
            this.barBtnRefresh.ToolTipController = this.toolTipController;
            this.barBtnRefresh.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.barBtnRefresh.Click += new System.EventHandler(this.barBtnRefresh_Click);
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(359, 4);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(32, 13);
            this.labelControl12.TabIndex = 9;
            this.labelControl12.Text = "Склад";
            // 
            // cboxStock
            // 
            this.cboxStock.Location = new System.Drawing.Point(358, 21);
            this.cboxStock.Name = "cboxStock";
            this.cboxStock.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboxStock.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboxStock.Size = new System.Drawing.Size(133, 20);
            this.cboxStock.TabIndex = 4;
            this.cboxStock.ToolTip = "Склад отгрузки";
            this.cboxStock.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.cboxStock.SelectedValueChanged += new System.EventHandler(this.cboxStock_SelectedValueChanged);
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(497, 5);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(37, 13);
            this.labelControl11.TabIndex = 7;
            this.labelControl11.Text = "Клиент";
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(218, 5);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(49, 13);
            this.labelControl10.TabIndex = 6;
            this.labelControl10.Text = "Компания";
            // 
            // cboxCompany
            // 
            this.cboxCompany.Location = new System.Drawing.Point(218, 21);
            this.cboxCompany.Name = "cboxCompany";
            this.cboxCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboxCompany.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboxCompany.Size = new System.Drawing.Size(133, 20);
            this.cboxCompany.TabIndex = 3;
            this.cboxCompany.ToolTip = "Компания";
            this.cboxCompany.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.cboxCompany.SelectedValueChanged += new System.EventHandler(this.cboxCompany_SelectedValueChanged);
            // 
            // cboxCustomer
            // 
            this.cboxCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboxCustomer.Location = new System.Drawing.Point(497, 21);
            this.cboxCustomer.Name = "cboxCustomer";
            this.cboxCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboxCustomer.Size = new System.Drawing.Size(307, 20);
            this.cboxCustomer.TabIndex = 5;
            this.cboxCustomer.ToolTip = "Клиент";
            this.cboxCustomer.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.cboxCustomer.SelectedIndexChanged += new System.EventHandler(this.cboxStock_SelectedValueChanged);
            // 
            // dtEndDate
            // 
            this.dtEndDate.EditValue = null;
            this.dtEndDate.Location = new System.Drawing.Point(111, 21);
            this.dtEndDate.Name = "dtEndDate";
            this.dtEndDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtEndDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtEndDate.Size = new System.Drawing.Size(100, 20);
            this.dtEndDate.TabIndex = 2;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(111, 5);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(12, 13);
            this.labelControl9.TabIndex = 2;
            this.labelControl9.Text = "по";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(5, 5);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(56, 13);
            this.labelControl8.TabIndex = 0;
            this.labelControl8.Text = "Дата ТТН с";
            // 
            // dtBeginDate
            // 
            this.dtBeginDate.EditValue = null;
            this.dtBeginDate.Location = new System.Drawing.Point(5, 21);
            this.dtBeginDate.Name = "dtBeginDate";
            this.dtBeginDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtBeginDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtBeginDate.Size = new System.Drawing.Size(100, 20);
            this.dtBeginDate.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel6, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(1, 79);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(960, 308);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel2, null);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.gridControlList, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(960, 308);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel6, null);
            this.tableLayoutPanel6.TabIndex = 4;
            // 
            // gridControlList
            // 
            this.gridControlList.ContextMenuStrip = this.contextMenuStrip;
            this.gridControlList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlList.EmbeddedNavigator.Name = "";
            this.gridControlList.Location = new System.Drawing.Point(3, 3);
            this.gridControlList.MainView = this.gridViewList;
            this.gridControlList.Name = "gridControlList";
            this.gridControlList.Size = new System.Drawing.Size(954, 302);
            this.gridControlList.TabIndex = 2;
            this.gridControlList.ToolTipController = this.toolTipController;
            this.gridControlList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewList});
            // 
            // gridViewList
            // 
            this.gridViewList.GridControl = this.gridControlList;
            this.gridViewList.Name = "gridViewList";
            this.gridViewList.OptionsBehavior.Editable = false;
            this.gridViewList.OptionsDetail.EnableMasterViewMode = false;
            this.gridViewList.OptionsDetail.ShowDetailTabs = false;
            this.gridViewList.OptionsDetail.SmartDetailExpand = false;
            this.gridViewList.OptionsView.ShowFooter = true;
            this.gridViewList.OptionsView.ShowGroupPanel = false;
            this.gridViewList.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewList_FocusedRowChanged);
            // 
            // imageCollection
            // 
            this.imageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection.ImageStream")));
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "MS Excel 2010 files (*.xlsm)|*.xlsm|MS Excel 2003 files (*.xls)|*.xls|All files (" +
    "*.*)|*.*";
            // 
            // frmJoinDepart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 480);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmJoinDepart";
            this.toolTipController.SetSuperTip(this, null);
            this.Text = "frmJoinDepart";
            this.Shown += new System.EventHandler(this.frmJoinDepart_Shown);
            this.contextMenuStrip.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPageViewer.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WaybilllNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Depart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlSearchCondition)).EndInit();
            this.panelControlSearchCondition.ResumeLayout(false);
            this.panelControlSearchCondition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IsBonus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxStock.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboxCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEndDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEndDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtBeginDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtBeginDate.Properties)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.ToolTipController toolTipController;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuCalcPrice;
        private System.Windows.Forms.ToolStripMenuItem menuClearPrices;
        private System.Windows.Forms.ToolStripMenuItem menuReturnSupplStateToAutoProcessPrices;
        private System.Windows.Forms.ToolStripMenuItem menuMakeSupplDeleted;
        private System.Windows.Forms.ToolStripMenuItem menuEventLog;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem menuCreateOrderBlank;
        private System.Windows.Forms.ToolStripMenuItem menuGoToSuppl;
        private System.Windows.Forms.ToolStripMenuItem menuItemPaymentHistory;
        private DevExpress.Utils.ImageCollection imageCollection;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraTab.XtraTabControl tabControl;
        private DevExpress.XtraTab.XtraTabPage tabPageViewer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private DevExpress.XtraEditors.PanelControl panelControlSearchCondition;
        private DevExpress.XtraEditors.SimpleButton barBtnRefresh;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.ComboBoxEdit cboxStock;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.ComboBoxEdit cboxCompany;
        private DevExpress.XtraEditors.ComboBoxEdit cboxCustomer;
        private DevExpress.XtraEditors.DateEdit dtEndDate;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.DateEdit dtBeginDate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private DevExpress.XtraGrid.GridControl gridControlList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewList;
        private DevExpress.XtraEditors.ControlNavigator controlNavigator;
        private DevExpress.XtraEditors.CheckEdit IsBonus;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnUnionWaybill;
        private DevExpress.XtraEditors.ComboBoxEdit Depart;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl29;
        private DevExpress.XtraEditors.TextEdit WaybilllNum;
    }
}
namespace ERPMercuryProcessingOrder
{
    partial class frmGroupList
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.treeListGroups = new DevExpress.XtraTreeList.TreeList();
            this.colGroupName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colGroupDscrpn = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colGroupTypeName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.itemAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.itemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.itemProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.checkListGroupType = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.toolTipController = new DevExpress.Utils.ToolTipController(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barButtonItemRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListGroups)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkListGroupType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.treeListGroups, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.checkListGroupType, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 26);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(506, 285);
            this.toolTipController.SetSuperTip(this.tableLayoutPanel, null);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // treeListGroups
            // 
            this.treeListGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeListGroups.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colGroupName,
            this.colGroupDscrpn,
            this.colGroupTypeName});
            this.treeListGroups.ContextMenuStrip = this.contextMenuStrip;
            this.treeListGroups.Location = new System.Drawing.Point(3, 28);
            this.treeListGroups.Name = "treeListGroups";
            this.treeListGroups.OptionsBehavior.Editable = false;
            this.treeListGroups.OptionsBehavior.EnableFiltering = true;
            this.treeListGroups.Size = new System.Drawing.Size(500, 254);
            this.treeListGroups.TabIndex = 1;
            this.treeListGroups.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeListGroups_FocusedNodeChanged);
            this.treeListGroups.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeListGroups_MouseDoubleClick);
            // 
            // colGroupName
            // 
            this.colGroupName.Caption = "Имя";
            this.colGroupName.FieldName = "Имя";
            this.colGroupName.MinWidth = 50;
            this.colGroupName.Name = "colGroupName";
            this.colGroupName.OptionsColumn.AllowEdit = false;
            this.colGroupName.OptionsColumn.AllowFocus = false;
            this.colGroupName.OptionsColumn.AllowMove = false;
            this.colGroupName.OptionsColumn.ReadOnly = true;
            this.colGroupName.Visible = true;
            this.colGroupName.VisibleIndex = 0;
            // 
            // colGroupDscrpn
            // 
            this.colGroupDscrpn.Caption = "Описание";
            this.colGroupDscrpn.FieldName = "Описание";
            this.colGroupDscrpn.MinWidth = 50;
            this.colGroupDscrpn.Name = "colGroupDscrpn";
            this.colGroupDscrpn.OptionsColumn.AllowEdit = false;
            this.colGroupDscrpn.OptionsColumn.AllowFocus = false;
            this.colGroupDscrpn.OptionsColumn.AllowMove = false;
            this.colGroupDscrpn.OptionsColumn.ReadOnly = true;
            this.colGroupDscrpn.Visible = true;
            this.colGroupDscrpn.VisibleIndex = 1;
            // 
            // colGroupTypeName
            // 
            this.colGroupTypeName.Caption = "Тип";
            this.colGroupTypeName.FieldName = "Тип";
            this.colGroupTypeName.MinWidth = 50;
            this.colGroupTypeName.Name = "colGroupTypeName";
            this.colGroupTypeName.OptionsColumn.AllowEdit = false;
            this.colGroupTypeName.OptionsColumn.AllowFocus = false;
            this.colGroupTypeName.OptionsColumn.AllowMove = false;
            this.colGroupTypeName.OptionsColumn.ReadOnly = true;
            this.colGroupTypeName.Visible = true;
            this.colGroupTypeName.VisibleIndex = 2;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemRefresh,
            this.itemAdd,
            this.itemDelete,
            this.itemProperties});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(129, 92);
            this.toolTipController.SetSuperTip(this.contextMenuStrip, null);
            // 
            // itemRefresh
            // 
            this.itemRefresh.Name = "itemRefresh";
            this.itemRefresh.Size = new System.Drawing.Size(128, 22);
            this.itemRefresh.Text = "Обновить";
            this.itemRefresh.Click += new System.EventHandler(this.itemRefresh_Click);
            // 
            // itemAdd
            // 
            this.itemAdd.Name = "itemAdd";
            this.itemAdd.Size = new System.Drawing.Size(128, 22);
            this.itemAdd.Text = "Добавить";
            this.itemAdd.Click += new System.EventHandler(this.itemAdd_Click);
            // 
            // itemDelete
            // 
            this.itemDelete.Name = "itemDelete";
            this.itemDelete.Size = new System.Drawing.Size(128, 22);
            this.itemDelete.Text = "Удалить";
            this.itemDelete.Click += new System.EventHandler(this.itemDelete_Click);
            // 
            // itemProperties
            // 
            this.itemProperties.Name = "itemProperties";
            this.itemProperties.Size = new System.Drawing.Size(128, 22);
            this.itemProperties.Text = "Свойства";
            this.itemProperties.Click += new System.EventHandler(this.itemProperties_Click);
            // 
            // checkListGroupType
            // 
            this.checkListGroupType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.checkListGroupType.Location = new System.Drawing.Point(3, 3);
            this.checkListGroupType.MultiColumn = true;
            this.checkListGroupType.Name = "checkListGroupType";
            this.checkListGroupType.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.checkListGroupType.Size = new System.Drawing.Size(500, 19);
            this.checkListGroupType.TabIndex = 0;
            this.checkListGroupType.ToolTip = "Фильтр типов групп";
            this.checkListGroupType.ToolTipController = this.toolTipController;
            this.checkListGroupType.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
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
            this.barButtonItem1,
            this.barButtonItemAdd,
            this.barButtonItemDelete,
            this.barButtonItemRefresh,
            this.barBtnPrint});
            this.barManager.MaxItemId = 5;
            this.barManager.ToolTipController = this.toolTipController;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemRefresh),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemAdd),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemDelete),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnPrint)});
            this.bar1.Text = "Tools";
            // 
            // barButtonItemRefresh
            // 
            this.barButtonItemRefresh.Description = "Обновить список групп";
            this.barButtonItemRefresh.Glyph = global::ERPMercuryProcessingOrder.Properties.Resources.refresh;
            this.barButtonItemRefresh.Hint = "Обновить список групп";
            this.barButtonItemRefresh.Id = 3;
            this.barButtonItemRefresh.Name = "barButtonItemRefresh";
            this.barButtonItemRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemRefresh_ItemClick);
            // 
            // barButtonItemAdd
            // 
            this.barButtonItemAdd.Description = "Новая группа";
            this.barButtonItemAdd.Glyph = global::ERPMercuryProcessingOrder.Properties.Resources.add2;
            this.barButtonItemAdd.Hint = "Новая группа";
            this.barButtonItemAdd.Id = 1;
            this.barButtonItemAdd.Name = "barButtonItemAdd";
            this.barButtonItemAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemAdd_ItemClick);
            // 
            // barButtonItemDelete
            // 
            this.barButtonItemDelete.Description = "Удалить группу";
            this.barButtonItemDelete.Glyph = global::ERPMercuryProcessingOrder.Properties.Resources.delete2;
            this.barButtonItemDelete.Hint = "Удалить группу";
            this.barButtonItemDelete.Id = 2;
            this.barButtonItemDelete.Name = "barButtonItemDelete";
            this.barButtonItemDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemDelete_ItemClick);
            // 
            // barBtnPrint
            // 
            this.barBtnPrint.Glyph = global::ERPMercuryProcessingOrder.Properties.Resources.printer2;
            this.barBtnPrint.Hint = "Печать";
            this.barBtnPrint.Id = 4;
            this.barBtnPrint.Name = "barBtnPrint";
            this.barBtnPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnPrint_ItemClick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // frmGroupList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 311);
            this.Controls.Add(this.tableLayoutPanel);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmGroupList";
            this.toolTipController.SetSuperTip(this, null);
            this.Text = "Группы объектов (для правил по вычислению цены)";
            this.Load += new System.EventHandler(this.frmGroupList_Load);
            this.tableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListGroups)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkListGroupType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private DevExpress.XtraTreeList.TreeList treeListGroups;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colGroupName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colGroupDscrpn;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colGroupTypeName;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem itemRefresh;
        private System.Windows.Forms.ToolStripMenuItem itemAdd;
        private System.Windows.Forms.ToolStripMenuItem itemDelete;
        private System.Windows.Forms.ToolStripMenuItem itemProperties;
        private DevExpress.Utils.ToolTipController toolTipController;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem barButtonItemAdd;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItemRefresh;
        private DevExpress.XtraBars.BarButtonItem barButtonItemDelete;
        private DevExpress.XtraBars.BarButtonItem barBtnPrint;
        private DevExpress.XtraEditors.CheckedListBoxControl checkListGroupType;
    }
}
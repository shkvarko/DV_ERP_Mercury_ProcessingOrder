namespace ERPMercuryProcessingOrder
{
    partial class ctrlWaybillPaymentsList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrlWaybillPaymentsList));
            this.tableLayoutPanelBackGround = new System.Windows.Forms.TableLayoutPanel();
            this.gridControlPaymentList = new DevExpress.XtraGrid.GridControl();
            this.gridViewPaymentList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnPrintPaymentHistory = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancelViewPaymentHistory = new DevExpress.XtraEditors.SimpleButton();
            this.lblWaybillInfo = new DevExpress.XtraEditors.LabelControl();
            this.tableLayoutPanelBackGround.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlPaymentList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPaymentList)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelBackGround
            // 
            this.tableLayoutPanelBackGround.ColumnCount = 1;
            this.tableLayoutPanelBackGround.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelBackGround.Controls.Add(this.gridControlPaymentList, 0, 1);
            this.tableLayoutPanelBackGround.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanelBackGround.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelBackGround.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelBackGround.Name = "tableLayoutPanelBackGround";
            this.tableLayoutPanelBackGround.RowCount = 2;
            this.tableLayoutPanelBackGround.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanelBackGround.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelBackGround.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelBackGround.Size = new System.Drawing.Size(670, 553);
            this.tableLayoutPanelBackGround.TabIndex = 0;
            // 
            // gridControlPaymentList
            // 
            this.gridControlPaymentList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlPaymentList.EmbeddedNavigator.Name = "";
            this.gridControlPaymentList.Location = new System.Drawing.Point(1, 36);
            this.gridControlPaymentList.MainView = this.gridViewPaymentList;
            this.gridControlPaymentList.Margin = new System.Windows.Forms.Padding(1);
            this.gridControlPaymentList.Name = "gridControlPaymentList";
            this.gridControlPaymentList.Size = new System.Drawing.Size(668, 516);
            this.gridControlPaymentList.TabIndex = 21;
            this.gridControlPaymentList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewPaymentList});
            // 
            // gridViewPaymentList
            // 
            this.gridViewPaymentList.GridControl = this.gridControlPaymentList;
            this.gridViewPaymentList.Name = "gridViewPaymentList";
            this.gridViewPaymentList.OptionsBehavior.Editable = false;
            this.gridViewPaymentList.OptionsDetail.EnableMasterViewMode = false;
            this.gridViewPaymentList.OptionsDetail.ShowDetailTabs = false;
            this.gridViewPaymentList.OptionsDetail.SmartDetailExpand = false;
            this.gridViewPaymentList.OptionsView.ShowFooter = true;
            this.gridViewPaymentList.OptionsView.ShowGroupPanel = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel1.Controls.Add(this.btnPrintPaymentHistory, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnCancelViewPaymentHistory, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblWaybillInfo, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(670, 35);
            this.tableLayoutPanel1.TabIndex = 22;
            // 
            // btnPrintPaymentHistory
            // 
            this.btnPrintPaymentHistory.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnPrintPaymentHistory.Image = global::ERPMercuryProcessingOrder.Properties.Resources.printer2;
            this.btnPrintPaymentHistory.Location = new System.Drawing.Point(592, 5);
            this.btnPrintPaymentHistory.Name = "btnPrintPaymentHistory";
            this.btnPrintPaymentHistory.Size = new System.Drawing.Size(75, 25);
            this.btnPrintPaymentHistory.TabIndex = 6;
            this.btnPrintPaymentHistory.Text = "Печать";
            this.btnPrintPaymentHistory.ToolTip = "Печать журнала оплат";
            this.btnPrintPaymentHistory.Click += new System.EventHandler(this.btnPrintEarningHistory_Click);
            // 
            // btnCancelViewPaymentHistory
            // 
            this.btnCancelViewPaymentHistory.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCancelViewPaymentHistory.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelViewPaymentHistory.Image")));
            this.btnCancelViewPaymentHistory.Location = new System.Drawing.Point(502, 5);
            this.btnCancelViewPaymentHistory.Name = "btnCancelViewPaymentHistory";
            this.btnCancelViewPaymentHistory.Size = new System.Drawing.Size(84, 25);
            this.btnCancelViewPaymentHistory.TabIndex = 5;
            this.btnCancelViewPaymentHistory.Text = "К журналу";
            this.btnCancelViewPaymentHistory.ToolTip = "Выход";
            this.btnCancelViewPaymentHistory.Click += new System.EventHandler(this.btnCancelViewPaymentHistory_Click);
            // 
            // lblWaybillInfo
            // 
            this.lblWaybillInfo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblWaybillInfo.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblWaybillInfo.Appearance.Options.UseFont = true;
            this.lblWaybillInfo.Location = new System.Drawing.Point(3, 11);
            this.lblWaybillInfo.Name = "lblWaybillInfo";
            this.lblWaybillInfo.Size = new System.Drawing.Size(77, 13);
            this.lblWaybillInfo.TabIndex = 0;
            this.lblWaybillInfo.Text = "lblWaybillInfo";
            // 
            // ctrlWaybillPaymentsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanelBackGround);
            this.Name = "ctrlWaybillPaymentsList";
            this.Size = new System.Drawing.Size(670, 553);
            this.tableLayoutPanelBackGround.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlPaymentList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPaymentList)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelBackGround;
        private DevExpress.XtraGrid.GridControl gridControlPaymentList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewPaymentList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.SimpleButton btnPrintPaymentHistory;
        private DevExpress.XtraEditors.SimpleButton btnCancelViewPaymentHistory;
        private DevExpress.XtraEditors.LabelControl lblWaybillInfo;
    }
}

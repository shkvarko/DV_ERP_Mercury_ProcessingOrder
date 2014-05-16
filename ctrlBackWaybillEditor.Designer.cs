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
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrderItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Product)).BeginInit();
            this.SuspendLayout();
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
            // 
            // mitemClearRows
            // 
            this.mitemClearRows.Name = "mitemClearRows";
            this.mitemClearRows.Size = new System.Drawing.Size(328, 22);
            this.mitemClearRows.Text = "Удалить все записи в приложении к накладной...";
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
            // openFileDialog
            // 
            this.openFileDialog.Filter = "MS Excel 2010 files (*.xlsx)|*.xlsx|MS Excel 2003 files (*.xls)|*.xls|All files (" +
    "*.*)|*.*";
            // 
            // ctrlBackWaybillEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ctrlBackWaybillEditor";
            this.Size = new System.Drawing.Size(669, 503);
            this.toolTipController.SetSuperTip(this, null);
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrderItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Product)).EndInit();
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
        private System.Data.DataColumn WaybItem_Id;
        private System.Data.DataColumn SupplItem_Guid;
        private System.Data.DataColumn QuantityWithReturn;
        private System.Data.DataTable Product;
        private System.Data.DataColumn ProductGuid;
        private System.Data.DataColumn ProductFullName;
        private System.Data.DataColumn CustomerOrderStockQty;
        private System.Data.DataColumn CustomerOrderResQty;
        private System.Data.DataColumn CustomerOrderPackQty;
        private System.Data.DataColumn Product_MeasureID;
        private System.Data.DataColumn Product_MeasureName;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}

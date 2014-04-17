using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ERP_Mercury.Common;
using System.Threading;
using OfficeOpenXml;

namespace ERPMercuryProcessingOrder
{
    public partial class ctrlWaybillPaymentsList : UserControl
    {
        #region Свойства класса
        private UniXP.Common.CProfile m_objProfile;
        private UniXP.Common.MENUITEM m_objMenuItem;
        private CWaybill m_objWaybill;
        private List<CEarning> m_objPaymentHistory;

        private DevExpress.XtraGrid.Views.Base.ColumnView ColumnViewPaymentHistory
        {
            get { return gridControlPaymentList.MainView as DevExpress.XtraGrid.Views.Base.ColumnView; }
        }
        public System.Threading.Thread ThreadPaymentHistory { get; set; }
        public System.Threading.ManualResetEvent EventStopThread { get; set; }
        public System.Threading.ManualResetEvent EventThreadStopped { get; set; }
        public delegate void LoadPaymentHistoryDelegate(List<CEarning> objEarningList, System.Int32 iRowCountInList);
        public LoadPaymentHistoryDelegate m_LoadPaymentHistoryDelegate;
        //private System.Boolean m_bThreadFinishJob;
        private const System.Int32 iThreadSleepTime = 1000;
        private const System.String strWaitCustomer = "ждите... идет заполнение списка";
        
        #endregion

        #region Конструктор
        public ctrlWaybillPaymentsList(UniXP.Common.MENUITEM objMenuItem)
        {
            InitializeComponent();

            m_objMenuItem = objMenuItem;
            m_objProfile = objMenuItem.objProfile;
            m_objWaybill = null;
            m_objPaymentHistory = new List<CEarning>();
            AddGridColumns();
            lblWaybillInfo.Text = strWaitCustomer;
            //m_bThreadFinishJob = false;
        }
        #endregion

        #region Настройки грида
        private void AddGridColumns()
        {
            ColumnViewPaymentHistory.Columns.Clear();

            AddGridColumn(ColumnViewPaymentHistory, "InterBaseID", "№ платежа");
            AddGridColumn(ColumnViewPaymentHistory, "Date", "Дата операции");
            AddGridColumn(ColumnViewPaymentHistory, "Value", "Сумма операции");
            AddGridColumn(ColumnViewPaymentHistory, "CurrencyCode", "Валюта");
            AddGridColumn(ColumnViewPaymentHistory, "CustomerName", "Плательщик");
            AddGridColumn(ColumnViewPaymentHistory, "CompanyCode", "Компания");

            


            foreach (DevExpress.XtraGrid.Columns.GridColumn objColumn in ColumnViewPaymentHistory.Columns)
            {
                objColumn.OptionsColumn.AllowEdit = false;
                objColumn.OptionsColumn.AllowFocus = false;
                objColumn.OptionsColumn.ReadOnly = true;

                if ((objColumn.FieldName == "Value") || (objColumn.FieldName == "Expense") || (objColumn.FieldName == "Saldo"))
                {
                    objColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    objColumn.DisplayFormat.FormatString = "### ### ### ##0";
                    objColumn.SummaryItem.FieldName = objColumn.FieldName;
                    objColumn.SummaryItem.DisplayFormat = "Итого: {0:### ### ### ##0}";
                    objColumn.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                }
            }



        }
        private void AddGridColumn(DevExpress.XtraGrid.Views.Base.ColumnView view, string fieldName, string caption) { AddGridColumn(view, fieldName, caption, null); }
        private void AddGridColumn(DevExpress.XtraGrid.Views.Base.ColumnView view, string fieldName, string caption, DevExpress.XtraEditors.Repository.RepositoryItem item) { AddGridColumn(view, fieldName, caption, item, "", DevExpress.Utils.FormatType.None); }
        private void AddGridColumn(DevExpress.XtraGrid.Views.Base.ColumnView view, string fieldName, string caption, DevExpress.XtraEditors.Repository.RepositoryItem item, string format, DevExpress.Utils.FormatType type)
        {
            DevExpress.XtraGrid.Columns.GridColumn column = view.Columns.AddField(fieldName);
            column.Caption = caption;
            column.ColumnEdit = item;
            column.DisplayFormat.FormatType = type;
            column.DisplayFormat.FormatString = format;
            column.VisibleIndex = view.VisibleColumns.Count;
        }

     

        #endregion

        #region События
        // Создаем закрытое поле, ссылающееся на заголовок списка делегатов
        private EventHandler<ChangePaymentsHistoryPropertieEventArgs> m_ChangePaymentHistoryProperties;
        // Создаем в классе член-событие
        public event EventHandler<ChangePaymentsHistoryPropertieEventArgs> ChangePaymentHistoryProperties
        {
            add
            {
                // берем закрытую блокировку и добавляем обработчик
                // (передаваемый по значению) в список делегатов
                m_ChangePaymentHistoryProperties += value;
            }
            remove
            {
                // берем закрытую блокировку и удаляем обработчик
                // (передаваемый по значению) из списка делегатов
                m_ChangePaymentHistoryProperties -= value;
            }
        }
        /// <summary>
        /// Инициирует событие и уведомляет о нем зарегистрированные объекты
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnChangePaymentHistoryProperties(ChangePaymentsHistoryPropertieEventArgs e)
        {
            // Сохраняем поле делегата во временном поле для обеспечение безопасности потока
            EventHandler<ChangePaymentsHistoryPropertieEventArgs> temp = m_ChangePaymentHistoryProperties;
            // Если есть зарегистрированные объектв, уведомляем их
            if (temp != null) temp(this, e);
        }
        public void SimulateChangePaymentHistoryProperties(CWaybill objWaybill, enumActionSaveCancel enActionType, System.Boolean bIsNewWaybill)
        {
            // Создаем объект, хранящий информацию, которую нужно передать
            // объектам, получающим уведомление о событии
            ChangePaymentsHistoryPropertieEventArgs e = new ChangePaymentsHistoryPropertieEventArgs(objWaybill, enActionType, bIsNewWaybill);

            // Вызываем виртуальный метод, уведомляющий наш объект о возникновении события
            // Если нет типа, переопределяющего этот метод, наш объект уведомит все объекты, 
            // подписавшиеся на уведомление о событии
            OnChangePaymentHistoryProperties(e);
        }
        #endregion

        #region Потоки
        /// <summary>
        /// Стартует поток, в котором загружается журнал разноски оплат
        /// </summary>
        public void StartThreadLoadPaymentHistory(CWaybill objWaybill)
        {
            try
            {
                m_objWaybill = objWaybill;

                // инициализируем делегаты
                m_LoadPaymentHistoryDelegate = new LoadPaymentHistoryDelegate(LoadPaymentHistoryListInGrid);
                m_objPaymentHistory.Clear();

                btnCancelViewPaymentHistory.Enabled = false;
                btnPrintPaymentHistory.Enabled = false;
                lblWaybillInfo.Text = strWaitCustomer;
                lblWaybillInfo.Refresh();

                gridControlPaymentList.DataSource = null;
                gridControlPaymentList.RefreshDataSource();
                gridControlPaymentList.Refresh();

                // запуск потока
                this.ThreadPaymentHistory = new System.Threading.Thread(LoadPaymentHistoryInThread);
                this.ThreadPaymentHistory.Start();
                Thread.Sleep(1000);

            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("StartThreadLoadPaymentHistory().\n\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }


        /// <summary>
        /// Загружает журнал разноски платежей
        /// </summary>
        public void LoadPaymentHistoryInThread()
        {
            try
            {
                System.String strErr = "";
                List<CEarning> objPaymentHistory = CEarningDataBaseModel.GetPaymentHistory(m_objProfile, null, m_objWaybill.ID, ref strErr);

                List<CEarning> objAddPaymentHistory = new List<CEarning>();
                if ((objPaymentHistory != null) && (objPaymentHistory.Count > 0))
                {
                    System.Int32 iRecCount = 0;
                    System.Int32 iRecAllCount = 0;
                    foreach (CEarning objItemHistory in objPaymentHistory)
                    {
                        objAddPaymentHistory.Add(objItemHistory);
                        iRecCount++;
                        iRecAllCount++;

                        if (iRecCount == 1000)
                        {
                            iRecCount = 0;
                            Thread.Sleep(1000);
                            this.Invoke(m_LoadPaymentHistoryDelegate, new Object[] { objAddPaymentHistory, iRecAllCount });
                            objAddPaymentHistory.Clear();
                        }

                    }
                    if (iRecCount != 1000)
                    {
                        iRecCount = 0;
                        this.Invoke(m_LoadPaymentHistoryDelegate, new Object[] { objAddPaymentHistory, iRecAllCount });
                        objAddPaymentHistory.Clear();
                    }

                }

                objPaymentHistory = null;
                objAddPaymentHistory = null;
                this.Invoke(m_LoadPaymentHistoryDelegate, new Object[] { null, 0 });
                //this.m_bThreadFinishJob = true;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("LoadEarningHistoryListInThread.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }
            return;
        }

        /// <summary>
        /// загружает в журнал историю разноски оплат
        /// </summary>
        /// <param name="objEarningHistoryList">журнал разноски оплат</param>
        /// <param name="iRowCountInList">количество строк, которые требуется загрузить в журнал</param>
        private void LoadPaymentHistoryListInGrid(List<CEarning> objPaymentHistory, System.Int32 iRowCountInList)
        {
            try
            {
                if ((objPaymentHistory != null) && (objPaymentHistory.Count > 0) && ( gridViewPaymentList.RowCount < iRowCountInList))
                {
                    m_objPaymentHistory.AddRange(objPaymentHistory);
                    if (gridControlPaymentList.DataSource == null)
                    {
                        gridControlPaymentList.DataSource = m_objPaymentHistory;
                    }
                    gridControlPaymentList.RefreshDataSource();
                }
                else
                {
                    Thread.Sleep(1000);

                    gridControlPaymentList.RefreshDataSource();

                    lblWaybillInfo.Text = (String.Format("№{0} от {1}\t\tсумма:\t{2:### ### ##0.00}\tсальдо:\t{3:### ### ##0.00}", m_objWaybill.DocNum, m_objWaybill.BeginDate.ToShortDateString(), m_objWaybill.SumWithDiscount, m_objWaybill.SumSaldo));
                    btnCancelViewPaymentHistory.Enabled = true;
                    btnPrintPaymentHistory.Enabled = true;
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("LoadEarningHistoryListInGrid.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }
            return;
        }
        #endregion

        #region Печать
        /// <summary>
        /// Экспорт журнала разноски оплат в MS Excel
        /// </summary>
        /// <param name="strFileName">имя файла MS Excel</param>
        private void ExportToExcelPaymentHistory(string strFileName)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                System.IO.FileInfo newFile = new System.IO.FileInfo(strFileName);
                if (newFile.Exists)
                {
                    newFile.Delete();
                    newFile = new System.IO.FileInfo(strFileName);
                }

                using (ExcelPackage package = new ExcelPackage(newFile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("журнал оплат");

                    foreach (DevExpress.XtraGrid.Columns.GridColumn objColumn in gridViewPaymentList.Columns)
                    {
                        if (objColumn.Visible == false) { continue; }

                        worksheet.Cells[1, objColumn.VisibleIndex + 1].Value = objColumn.Caption;
                    }

                    using (var range = worksheet.Cells[1, 1, 1, gridViewPaymentList.Columns.Count])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Font.Size = 14;
                    }

                    System.Int32 iCurrentRow = 2;
                    System.Int32 iRowsCount = gridViewPaymentList.RowCount;
                    for (System.Int32 i = 0; i < iRowsCount; i++)
                    {
                        foreach (DevExpress.XtraGrid.Columns.GridColumn objColumn in gridViewPaymentList.Columns)
                        {
                            if (objColumn.Visible == false) { continue; }

                            worksheet.Cells[iCurrentRow, objColumn.VisibleIndex + 1].Value = gridViewPaymentList.GetRowCellValue(i, objColumn);
                            if ((objColumn.FieldName == "Date") || (objColumn.FieldName == "Payment_OperDate") ||
                                (objColumn.FieldName == "Earning_BankDate"))
                            {
                                worksheet.Cells[iCurrentRow, objColumn.VisibleIndex + 1].Style.Numberformat.Format = "DD.MM.YYYY";
                            }
                        }
                        iCurrentRow++;
                    }

                    iCurrentRow--;
                    worksheet.Cells[1, 1, iCurrentRow, gridViewPaymentList.Columns.Count].AutoFitColumns(0);
                    worksheet.Cells[1, 1, iCurrentRow, gridViewPaymentList.Columns.Count].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[1, 1, iCurrentRow, gridViewPaymentList.Columns.Count].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[1, 1, iCurrentRow, gridViewPaymentList.Columns.Count].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[1, 1, iCurrentRow, gridViewPaymentList.Columns.Count].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                    worksheet.PrinterSettings.FitToWidth = 1;

                    worksheet = null;

                    package.Save();

                    try
                    {
                        using (System.Diagnostics.Process process = new System.Diagnostics.Process())
                        {
                            process.StartInfo.FileName = strFileName;
                            process.StartInfo.Verb = "Open";
                            process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                            process.Start();
                        }
                    }
                    catch
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(this, "Системе не удалось найти приложение, чтобы открыть файл.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }

            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Ошибка экспорта в MS Excel.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }
        private void btnPrintEarningHistory_Click(object sender, EventArgs e)
        {
            try
            {
                ExportToExcelPaymentHistory(String.Format("{0}{1}.xlsx", System.IO.Path.GetTempPath(), (String.Format("{0} журнал разноски оплат", this.Text))));
            }
            catch (System.Exception f)
            {
                SendMessageToLog("btnPrint_Click. Текст ошибки: " + f.Message);
            }

            return;

        }
        #endregion

        #region Журнал сообщений
        private void SendMessageToLog(System.String strMessage)
        {
            try
            {
                m_objMenuItem.SimulateNewMessage(strMessage);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "SendMessageToLog.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        #endregion

        #region Выход
        private void Cancel()
        {
            try
            {
                SimulateChangePaymentHistoryProperties(m_objWaybill, enumActionSaveCancel.Cancel, false);
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Cancel. Текст ошибки: " + f.Message);
            }
            finally
            {
            }

            return;

        }
        private void btnCancelViewPaymentHistory_Click(object sender, EventArgs e)
        {
            Cancel();
        }
        #endregion

    }


    /// <summary>
    /// Тип, хранящий информацию, которая передается получателям уведомления о событии
    /// </summary>
    public class ChangePaymentsHistoryPropertieEventArgs : EventArgs
    {
        private readonly CWaybill m_objWaybill;
        public CWaybill Waybill
        { get { return m_objWaybill; } }

        private readonly enumActionSaveCancel m_enActionType;
        public enumActionSaveCancel ActionType
        { get { return m_enActionType; } }

        private readonly System.Boolean m_bIsNewWaybill;
        public System.Boolean IsNewWaybill
        { get { return m_bIsNewWaybill; } }

        public ChangePaymentsHistoryPropertieEventArgs(CWaybill objWaybill, enumActionSaveCancel enActionType, System.Boolean bIsNewWaybill)
        {
            m_objWaybill = objWaybill;
            m_enActionType = enActionType;
            m_bIsNewWaybill = bIsNewWaybill;
        }
    }


}

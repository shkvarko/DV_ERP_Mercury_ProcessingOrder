using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;
using ERP_Mercury.Common;

namespace ERPMercuryProcessingOrder
{
    public partial class frmShippedProducts : DevExpress.XtraEditors.XtraForm
    {
        #region Свойства
        private UniXP.Common.CProfile m_objProfile;
        private UniXP.Common.MENUITEM m_objMenuItem;
        private List<ERP_Mercury.Common.CCustomer> m_objCustomerList;
        private List<ERP_Mercury.Common.CWaybill> m_objWaybillList;
        private List<ERP_Mercury.Common.CIntWaybill> m_objIntWaybillList;
        private List<CWaybillState> m_objWaybillStateList;
        private List<CIntWaybillState> m_objIntWaybillStateList;
        private CWaybill SelectedWaybill { get { return GetSelectedWaybill(); } }
        private CIntWaybill SelectedIntWaybill { get { return GetSelectedIntWaybill(); } }
        private DevExpress.XtraGrid.Views.Base.ColumnView WaybillColumnView
        {
            get { return gridControlWaybill.MainView as DevExpress.XtraGrid.Views.Base.ColumnView; }
        }
        private DevExpress.XtraGrid.Views.Base.ColumnView IntWaybillColumnView
        {
            get { return gridControlIntWaybill.MainView as DevExpress.XtraGrid.Views.Base.ColumnView; }
        }
        // потоки
        private System.Threading.Thread thrStockOrderTypeParts;
        public System.Threading.Thread ThreadStockOrderTypeParts
        {
            get { return thrStockOrderTypeParts; }
        }
        private System.Threading.ManualResetEvent m_EventStopThread;
        public System.Threading.ManualResetEvent EventStopThread
        {
            get { return m_EventStopThread; }

        }
        private System.Threading.ManualResetEvent m_EventThreadStopped;
        public System.Threading.ManualResetEvent EventThreadStopped
        {
            get { return m_EventThreadStopped; }
        }
        public delegate void LoadCustomerListDelegate(List<ERP_Mercury.Common.CCustomer> objCustomerList, System.Int32 iRowCountInLis);
        public LoadCustomerListDelegate m_LoadCustomerListDelegate;
        private const System.Int32 iThreadSleepTime = 1000;
        private const System.String strWaitCustomer = "ждите... идет заполнение списка";
        private System.Boolean m_bThreadFinishJob;
        private const int m_iMinCountWaybillsInUnion = 2;
        private const int m_iHightWarningPanel = 35;
        private const int m_indxWarningPanel = 2;


        private System.Boolean IsAvailableDR_ShippedWaybillPayForm1; // "ТТН ф1 отгрузка";
        private System.Boolean IsAvailableDR_ShippedWaybillPayForm2; // "ТТН ф2 отгрузка";

        public System.Threading.Thread ThreadAutoShipDocumentList { get; set; }

        public delegate void SetInfoInSearchProcessWoringDelegate(System.Int32 iCurrentIndex, System.Int32 iAllObjectCount,
            System.String Waybill_Num, System.Double Waybill_TotalSum, System.String Customer_Name
            );
        public SetInfoInSearchProcessWoringDelegate m_SetInfoInSearchProcessWoringDelegate;

        public delegate void SetResultAutoShipWaybillListDelegate(System.Int32 ERROR_NUM, System.String ERROR_STR,
            System.Guid Waybill_Guid, System.DateTime Waybill_ShipDate, System.Guid WaybillState_Guid,
            System.Int32 WaybillsCount, System.Double WaybillsTotalSum,
            System.Boolean bLastDocument);
        public SetResultAutoShipWaybillListDelegate m_SetResultAutoShipWaybillListDelegate;

        public System.Threading.Thread ThreadAutoShipIntDocumentList { get; set; }

        public delegate void SetInfoInSearchProcessIntWoringDelegate(System.Int32 iCurrentIndex, System.Int32 iAllObjectCount,
            System.String IntWaybill_Num, System.Double IntWaybill_TotalSum, System.String SrcStock_Name, System.String DstStock_Name
            );
        public SetInfoInSearchProcessIntWoringDelegate m_SetInfoInSearchProcessIntWoringDelegate;

        public delegate void SetResultAutoShipIntWaybillListDelegate(System.Int32 ERROR_NUM, System.String ERROR_STR,
            System.Guid IntWaybill_Guid, System.DateTime IntWaybill_ShipDate, 
            System.Guid IntWaybillState_Guid, System.Guid SetIntWaybillShipMode_Guid,
            System.Int32 IntWaybillsCount, System.Double IntWaybillsTotalSum,
            System.Boolean bLastDocument);
        public SetResultAutoShipIntWaybillListDelegate m_SetResultAutoShipIntWaybillListDelegate;

        #endregion

        #region Конструктор
        public frmShippedProducts(UniXP.Common.MENUITEM objMenuItem)
        {
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ru-RU");
            ci.NumberFormat.CurrencyDecimalSeparator = ".";
            ci.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;

            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.AssemblyResolve += new ResolveEventHandler(MyResolveEventHandler);

            InitializeComponent();

            m_objMenuItem = objMenuItem;
            m_objProfile = objMenuItem.objProfile;

            // динамические права
            UniXP.Common.CClientRights objClientRights = m_objProfile.GetClientsRight();
            IsAvailableDR_ShippedWaybillPayForm1 = objClientRights.GetState(ERPMercuryProcessingOrder.Consts.strDR_ShippedWaybillPayForm1);
            IsAvailableDR_ShippedWaybillPayForm2 = objClientRights.GetState(ERPMercuryProcessingOrder.Consts.strDR_ShippedWaybillPayForm2);
            objClientRights = null;

            m_objWaybillList = null;
            m_objIntWaybillList = null;
            m_bThreadFinishJob = false;
            m_objCustomerList = new List<ERP_Mercury.Common.CCustomer>();
            m_objWaybillStateList = null;
            m_objIntWaybillStateList = null;

            AddWaybillGridColumns();
            AddIntWaybillGridColumns();

            dtBeginDate.DateTime = System.DateTime.Today;
            dtEndDate.DateTime = System.DateTime.Today;
            dtWaybill_ShipDate.DateTime = System.DateTime.Today;
            dtIntWaybill_ShipDate.DateTime = System.DateTime.Today;
            dtBeginDateIntWaybill.DateTime = System.DateTime.Today;
            dtEndDateIntWaybill.DateTime = System.DateTime.Today;

            tableLayoutPanel4.RowStyles[m_indxWarningPanel].Height = 0;
            tableLayoutPanelIntWaybill.RowStyles[m_indxWarningPanel].Height = 0;
            SearchProcessWoring.Visible = false;
            SearchProcessWoringIntWaybill.Visible = false;

            checkEditSetWaybillShipMode.CheckState = CheckState.Unchecked;
            checkEditRepresentative.CheckState = CheckState.Unchecked;
            WaybillShipMode.Enabled = false;
            txtRepresentative.Enabled = false;
            checkEditSetIntWaybillShipMode.CheckState = CheckState.Unchecked;
            IntWaybillShipMode.Enabled = false;

            tabControl.ShowTabHeader = DevExpress.Utils.DefaultBoolean.Default;

        }

        private System.Reflection.Assembly MyResolveEventHandler(object sender, ResolveEventArgs args)
        {
            System.Reflection.Assembly MyAssembly = null;
            System.Reflection.Assembly objExecutingAssemblies = System.Reflection.Assembly.GetExecutingAssembly();

            System.Reflection.AssemblyName[] arrReferencedAssmbNames = objExecutingAssemblies.GetReferencedAssemblies();

            //Loop through the array of referenced assembly names.
            System.String strDllName = "";
            foreach (System.Reflection.AssemblyName strAssmbName in arrReferencedAssmbNames)
            {

                //Check for the assembly names that have raised the "AssemblyResolve" event.
                if (strAssmbName.FullName.Substring(0, strAssmbName.FullName.IndexOf(",")) == args.Name.Substring(0, args.Name.IndexOf(",")))
                {
                    strDllName = args.Name.Substring(0, args.Name.IndexOf(",")) + ".dll";
                    break;
                }

            }
            if (strDllName != "")
            {
                System.String strFileFullName = "";
                System.Boolean bError = false;
                foreach (System.String strPath in this.m_objProfile.ResourcePathList)
                {
                    //Load the assembly from the specified path. 
                    strFileFullName = String.Format("{0}\\{1}", strPath, strDllName);
                    if (System.IO.File.Exists(strFileFullName))
                    {
                        try
                        {
                            MyAssembly = System.Reflection.Assembly.LoadFrom(strFileFullName);
                            break;
                        }
                        catch (System.Exception f)
                        {
                            bError = true;
                            DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка загрузки библиотеки: " +
                                strFileFullName + "\n\nТекст ошибки: " + f.Message, "Ошибка",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        }

                    }
                    if (bError) { break; }
                }
            }

            //Return the loaded assembly.
            if (MyAssembly == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Не удалось найти библиотеку: " +
                                strDllName, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

            }
            return MyAssembly;
        }
        #endregion

        #region Потоки
        public void StartThreadWithLoadData()
        {
            try
            {
                // инициализируем события
                this.m_EventStopThread = new System.Threading.ManualResetEvent(false);
                this.m_EventThreadStopped = new System.Threading.ManualResetEvent(false);

                // инициализируем делегаты
                m_LoadCustomerListDelegate = new LoadCustomerListDelegate(LoadCustomerList);

                m_objCustomerList.Clear();

                btnWaybillRefresh.Enabled = false;
                btnIntWaybillRefresh.Enabled = false;

                // запуск потока
                StartThread();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("StartThreadWithLoadData().\n\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }

        private void StartThread()
        {
            try
            {
                // делаем событиям reset
                this.m_EventStopThread.Reset();
                this.m_EventThreadStopped.Reset();

                this.thrStockOrderTypeParts = new System.Threading.Thread(WorkerThreadFunction);
                this.thrStockOrderTypeParts.Start();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("StartThreadWithLoadData().\n\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        public void WorkerThreadFunction()
        {
            try
            {
                Run();
            }
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("WorkerThreadFunction\n" + e.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
            return;
        }

        public void Run()
        {
            try
            {
                LoadCustomerListInThread();

                // пока заполняется список товаров будем проверять, не было ли сигнала прекратить все это
                while (this.m_bThreadFinishJob == false)
                {
                    // проверим, а не попросили ли нас закрыться
                    if (this.m_EventStopThread.WaitOne(iThreadSleepTime, true))
                    {
                        this.m_EventThreadStopped.Set();
                        break;
                    }
                }

                //cboxCustomer.SelectedText = "";
            }
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Run\n" + e.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
            return;
        }
        /// <summary>
        /// Делает пометку о необходимости остановить поток
        /// </summary>
        public void TreadIsFree()
        {
            try
            {
                this.m_bThreadFinishJob = true;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("StopPleaseTread() " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
            return;
        }

        public void LoadCustomerListInThread()
        {
            try
            {
                List<ERP_Mercury.Common.CCustomer> objCustomerList = ERP_Mercury.Common.CCustomer.GetCustomerListWithoutAdvancedProperties(m_objProfile, null, null);


                List<ERP_Mercury.Common.CCustomer> objAddCustomerList = new List<ERP_Mercury.Common.CCustomer>();
                if ((objCustomerList != null) && (objCustomerList.Count > 0))
                {
                    System.Int32 iRecCount = 0;
                    System.Int32 iRecAllCount = 0;
                    foreach (ERP_Mercury.Common.CCustomer objCustomer in objCustomerList)
                    {
                        objAddCustomerList.Add(objCustomer);
                        iRecCount++;
                        iRecAllCount++;

                        if (iRecCount == 1000)
                        {
                            iRecCount = 0;
                            Thread.Sleep(1000);
                            this.Invoke(m_LoadCustomerListDelegate, new Object[] { objAddCustomerList, iRecAllCount });
                            objAddCustomerList.Clear();
                        }

                    }
                    if (iRecCount != 1000)
                    {
                        iRecCount = 0;
                        this.Invoke(m_LoadCustomerListDelegate, new Object[] { objAddCustomerList, iRecAllCount });
                        objAddCustomerList.Clear();
                    }

                }

                objCustomerList = null;
                objAddCustomerList = null;
                this.Invoke(m_LoadCustomerListDelegate, new Object[] { null, 0 });
                this.m_bThreadFinishJob = true;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("LoadCustomerListInThread.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }
            return;
        }
        private void LoadCustomerList(List<ERP_Mercury.Common.CCustomer> objCustomerList, System.Int32 iRowCountInList)
        {
            try
            {
                cboxCustomer.Text = strWaitCustomer;
                if ((objCustomerList != null) && (objCustomerList.Count > 0) && (cboxCustomer.Properties.Items.Count < iRowCountInList))
                {
                    cboxCustomer.Properties.Items.AddRange(objCustomerList);
                    m_objCustomerList.AddRange(objCustomerList);
                }
                else
                {
                    cboxCustomer.Text = "";
                    btnWaybillRefresh.Enabled = true;
                    btnIntWaybillRefresh.Enabled = true;
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("LoadCustomerList.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }
            return;
        }
        private System.Boolean bIsThreadsActive()
        {
            System.Boolean bRet = false;
            try
            {
                bRet = (((thrStockOrderTypeParts != null) && (thrStockOrderTypeParts.IsAlive == true)) &&
                    ((ThreadAutoShipDocumentList != null) && (ThreadAutoShipDocumentList.IsAlive == true)) &&
                    ((ThreadAutoShipIntDocumentList != null) && (ThreadAutoShipIntDocumentList.IsAlive == true)));
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("bIsThreadsActive.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return bRet;
        }

        private void CloseThreadInAddressEditor()
        {
            try
            {
                System.Boolean bNeedDoEvents = false;
                while (bIsThreadsActive() == true)
                {
                    bNeedDoEvents = false;

                    if (System.Threading.WaitHandle.WaitAll((new System.Threading.ManualResetEvent[] { EventThreadStopped }), 100, true))
                    {
                        bNeedDoEvents = true;
                        break;
                    }

                    if (bNeedDoEvents == true)
                    {
                        Application.DoEvents();
                    }
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("CloseThreadInAddressEditor.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }

        private void frmShippedProducts_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (bIsThreadsActive() == true)
                {
                    // присутствуют рабочие потоки
                    CloseThreadInAddressEditor();
                    e.Cancel = false;
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка закрытия формы.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }

        private void btnCloseFromWaybill_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
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

        #region Настройка внешнего вида журналов
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

        private void AddWaybillGridColumns()
        {
            WaybillColumnView.Columns.Clear();
            AddGridColumn(WaybillColumnView, "ID", "Идентификатор");
            AddGridColumn(WaybillColumnView, "WaybillStateName", "Состояние");
            AddGridColumn(WaybillColumnView, "DocNum", "Номер");
            AddGridColumn(WaybillColumnView, "BeginDate", "Дата");
            AddGridColumn(WaybillColumnView, "DeliveryDate", "Дата доставки");
            //AddGridColumn(ColumnView, "ShipDate", "Дата отгрузки");
            AddGridColumn(WaybillColumnView, "WaybillShipModeName", "Вид отгрузки");
            AddGridColumn(WaybillColumnView, "CustomerName", "Клиент");
            AddGridColumn(WaybillColumnView, "ChildDepartCode", "Код дочернего");
            AddGridColumn(WaybillColumnView, "CompanyAcronym", "Компания");
            AddGridColumn(WaybillColumnView, "StockName", "Склад");
            AddGridColumn(WaybillColumnView, "DepartCode", "Подразделение");
            AddGridColumn(WaybillColumnView, "PaymentTypeName", "Форма оплаты");

            AddGridColumn(WaybillColumnView, "Quantity", "Количество, шт.");
            //AddGridColumn(ColumnView, "QuantityReturn", "Возврат, шт.");


            AddGridColumn(WaybillColumnView, "SumWaybill", "Сумма, руб.");
            AddGridColumn(WaybillColumnView, "SumDiscount", "Скидка, руб.");
            AddGridColumn(WaybillColumnView, "SumWithDiscount", "С учетом скидки, руб.");
            //AddGridColumn(ColumnView, "SumReturn", "Возврат, руб.");

            AddGridColumn(WaybillColumnView, "SumPayment", "Оплачено, руб.");
            AddGridColumn(WaybillColumnView, "SumSaldo", "Сальдо, руб.");

            foreach (DevExpress.XtraGrid.Columns.GridColumn objColumn in WaybillColumnView.Columns)
            {
                objColumn.OptionsColumn.AllowEdit = false;
                //objColumn.OptionsColumn.AllowFocus = false;
                objColumn.OptionsColumn.ReadOnly = true;
                if (objColumn.FieldName == "CustomerName")
                {
                    objColumn.BestFit();
                    //objColumn.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                }
                if ((objColumn.FieldName == "ID") || (objColumn.FieldName == "WaybillStateId"))
                {
                    objColumn.Visible = false;
                }
                if (objColumn.FieldName == "BeginDate")
                {
                    objColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    objColumn.DisplayFormat.FormatString = "dd.MM.yyyy HH:MM:ss";
                }
                if ((objColumn.FieldName == "DeliveryDate") || (objColumn.FieldName == "ShipDate"))
                {
                    objColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    objColumn.DisplayFormat.FormatString = "dd.MM.yyyy";
                }
                if ((objColumn.FieldName == "Quantity") || (objColumn.FieldName == "QuantityReturn"))
                {
                    objColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    objColumn.DisplayFormat.FormatString = "### ### ##0";
                    objColumn.SummaryItem.FieldName = objColumn.FieldName;
                    objColumn.SummaryItem.DisplayFormat = "{0:### ### ##0}";
                    objColumn.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                }
                if ((objColumn.FieldName == "SumWaybill") || (objColumn.FieldName == "SumDiscount") ||
                    (objColumn.FieldName == "SumWithDiscount") || (objColumn.FieldName == "SumReturn") ||
                    (objColumn.FieldName == "SumPayment") || (objColumn.FieldName == "SumSaldo"))
                {
                    objColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    objColumn.DisplayFormat.FormatString = "### ### ##0";
                    objColumn.SummaryItem.FieldName = objColumn.FieldName;
                    objColumn.SummaryItem.DisplayFormat = "{0:### ### ##0}";
                    objColumn.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                }
                if ((objColumn.FieldName == "SumReservedInAccountingCurrency") || (objColumn.FieldName == "SumReservedWithDiscountInAccountingCurrency"))
                {
                    objColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    objColumn.DisplayFormat.FormatString = "### ### ##0.00";
                    objColumn.SummaryItem.FieldName = objColumn.FieldName;
                    objColumn.SummaryItem.DisplayFormat = "{0:### ### ##0.00}";
                    objColumn.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                }
            }
        }

        private void AddIntWaybillGridColumns()
        {
            IntWaybillColumnView.Columns.Clear();
            AddGridColumn(IntWaybillColumnView, "ID", "Идентификатор");
            AddGridColumn(IntWaybillColumnView, "DocStateName", "Состояние");
            AddGridColumn(IntWaybillColumnView, "DocNum", "Номер");
            AddGridColumn(IntWaybillColumnView, "BeginDate", "Дата");
            AddGridColumn(IntWaybillColumnView, "WaybillShipModeName", "Вид отгрузки");
            AddGridColumn(IntWaybillColumnView, "CompanySrcAcronym", "Компания-источник");
            AddGridColumn(IntWaybillColumnView, "StockSrcName", "Склад-источник");
            AddGridColumn(IntWaybillColumnView, "CompanyDstAcronym", "Компания-получатель");
            AddGridColumn(IntWaybillColumnView, "StockDstName", "Склад-получатель");
            AddGridColumn(IntWaybillColumnView, "DepartCode", "Подразделение");
            AddGridColumn(IntWaybillColumnView, "PaymentTypeName", "Форма оплаты");

            AddGridColumn(IntWaybillColumnView, "Quantity", "Количество, шт.");

            AddGridColumn(IntWaybillColumnView, "SumWaybill", "Сумма, руб.");
            AddGridColumn(IntWaybillColumnView, "SumRetail", "Сумма розн., руб.");

            foreach (DevExpress.XtraGrid.Columns.GridColumn objColumn in IntWaybillColumnView.Columns)
            {
                objColumn.OptionsColumn.AllowEdit = false;
                //objColumn.OptionsColumn.AllowFocus = false;
                objColumn.OptionsColumn.ReadOnly = true;
                if ((objColumn.FieldName == "ID"))
                {
                    objColumn.Visible = false;
                }
                if (objColumn.FieldName == "BeginDate")
                {
                    objColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    objColumn.DisplayFormat.FormatString = "dd.MM.yyyy HH:MM:ss";
                }
                if ((objColumn.FieldName == "DeliveryDate") || (objColumn.FieldName == "ShipDate"))
                {
                    objColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    objColumn.DisplayFormat.FormatString = "dd.MM.yyyy";
                }
                if ((objColumn.FieldName == "Quantity") || (objColumn.FieldName == "QuantityReturn"))
                {
                    objColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    objColumn.DisplayFormat.FormatString = "### ### ##0";
                    objColumn.SummaryItem.FieldName = objColumn.FieldName;
                    objColumn.SummaryItem.DisplayFormat = "{0:### ### ##0}";
                    objColumn.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                }
                if ((objColumn.FieldName == "SumWaybill") || (objColumn.FieldName == "SumDiscount") ||
                    (objColumn.FieldName == "SumWithDiscount") || (objColumn.FieldName == "SumReturn") ||
                    (objColumn.FieldName == "SumPayment") || (objColumn.FieldName == "SumRetail"))
                {
                    objColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    objColumn.DisplayFormat.FormatString = "### ### ##0";
                    objColumn.SummaryItem.FieldName = objColumn.FieldName;
                    objColumn.SummaryItem.DisplayFormat = "{0:### ### ##0}";
                    objColumn.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                }
                if ((objColumn.FieldName == "SumReservedInAccountingCurrency") || (objColumn.FieldName == "SumReservedWithDiscountInAccountingCurrency"))
                {
                    objColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    objColumn.DisplayFormat.FormatString = "### ### ##0.00";
                    objColumn.SummaryItem.FieldName = objColumn.FieldName;
                    objColumn.SummaryItem.DisplayFormat = "{0:### ### ##0.00}";
                    objColumn.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                }
            }
        }
        #endregion

        #region Загрузка журнала

        private void cboxCompany_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                LoadStockForCompany( cboxStock, cboxCompany );
            }
            catch (System.Exception f)
            {
                SendMessageToLog("cboxCompany_SelectedValueChanged. Текст ошибки: " + f.Message);
            }
            return;
        }

        private void cboxCompanySrc_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                LoadStockForCompany(cboxStockSrc, cboxCompanySrc);
            }
            catch (System.Exception f)
            {
                SendMessageToLog("cboxCompanySrc_SelectedValueChanged. Текст ошибки: " + f.Message);
            }
            return;
        }

        private void cboxCompanyDst_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                LoadStockForCompany(cboxStockDst, cboxCompanyDst);
            }
            catch (System.Exception f)
            {
                SendMessageToLog("cboxCompanyDst_SelectedValueChanged. Текст ошибки: " + f.Message);
            }
            return;
        }

        private void cboxStockSrc_SelectedValueChanged(object sender, EventArgs e)
        {
            ChangeConditionForSearch();
        }


        /// <summary>
        /// загружает склады для выбранной компании
        /// </summary>
        /// <param name="cboxStock">выпадающий список складов</param>
        /// <param name="cboxCompany">выпадающий список компаний</param>
        private void LoadStockForCompany(DevExpress.XtraEditors.ComboBoxEdit cboxStock, 
            DevExpress.XtraEditors.ComboBoxEdit cboxCompany)
        {
            try
            {
                cboxStock.SelectedItem = null;
                cboxStock.Properties.Items.Clear();
                cboxStock.Properties.Items.Add(new ERP_Mercury.Common.CStock());

                ERP_Mercury.Common.CCompany objSelectedCompany = ((cboxCompany.SelectedItem == null) || (((ERP_Mercury.Common.CCompany)cboxCompany.SelectedItem).ID == System.Guid.Empty) ? null : (ERP_Mercury.Common.CCompany)cboxCompany.SelectedItem);

                if (objSelectedCompany != null)
                {
                    List<ERP_Mercury.Common.CStock> objStockList = ERP_Mercury.Common.CStock.GetStockList(m_objProfile, null);
                    List<int> StockIdList = new List<int>();
                    if (objStockList != null)
                    {
                        foreach (ERP_Mercury.Common.CStock objStock in objStockList)
                        {
                            if (objStock.Company.ID == objSelectedCompany.ID)
                            {
                                cboxStock.Properties.Items.Add(objStock);

                                StockIdList.Add(objStock.IBId);
                            }
                        }
                    }
                    if (StockIdList.Count > 0)
                    {
                        cboxStock.SelectedItem = objStockList.Single<ERP_Mercury.Common.CStock>(x => x.IBId == StockIdList.Min<int>());
                    }
                    StockIdList = null;
                }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("LoadStockForCompany. Текст ошибки: " + f.Message);
            }
            return;
        }
        /// <summary>
        /// Заполнение выпадающих списков
        /// </summary>
        private void LoadComboBox()
        {
            try
            {
                System.Boolean CanViewPaymentType2 = m_objProfile.GetClientsRight().GetState(ERPMercuryProcessingOrder.Consts.strDR_ViewWaybillPayForm2);
                System.Int32 DefPaymentTypeId = 0;
                System.Boolean BlockOtherPaymentType = false;
                System.String CompanyAcronymForPaymentType1 = System.String.Empty;
                System.String CompanyAcronymForPaymentType2 = System.String.Empty;
                System.Int32 ERROR_NUM = 0;
                System.String strErr = System.String.Empty;

                CWaybillDataBaseModel.GetWaybillListSettingsDefault(m_objProfile, null, CanViewPaymentType2, ref DefPaymentTypeId,
                    ref BlockOtherPaymentType, ref CompanyAcronymForPaymentType1, ref CompanyAcronymForPaymentType2,
                    ref ERROR_NUM, ref strErr);


                cboxCompany.Properties.Items.Clear();
                cboxCompanySrc.Properties.Items.Clear();
                cboxCompanyDst.Properties.Items.Clear();
                WaybillShipMode.Properties.Items.Clear();
                IntWaybillShipMode.Properties.Items.Clear();

                List<ERP_Mercury.Common.CCompany> objCompanyList = ERP_Mercury.Common.CCompany.GetCompanyList(m_objProfile, null);
                cboxCompany.Properties.Items.AddRange(objCompanyList);
                cboxCompanySrc.Properties.Items.AddRange(objCompanyList);
                cboxCompanyDst.Properties.Items.AddRange(objCompanyList);

                if ((CompanyAcronymForPaymentType1.Length > 0) && (objCompanyList.Count > 0))
                {
                    cboxCompany.SelectedItem = cboxCompany.Properties.Items.Cast<CCompany>().SingleOrDefault<CCompany>(x => x.Abbr == CompanyAcronymForPaymentType1);
                    cboxCompanySrc.SelectedItem = cboxCompanySrc.Properties.Items.Cast<CCompany>().SingleOrDefault<CCompany>(x => x.Abbr == CompanyAcronymForPaymentType1);
                    cboxCompanyDst.SelectedItem = cboxCompanyDst.Properties.Items.Cast<CCompany>().SingleOrDefault<CCompany>(x => x.Abbr == CompanyAcronymForPaymentType1);
                }

                if ((objCompanyList != null) && (objCompanyList.Count > 0))
                {
                    List<int> CompanyIBIdList = new List<int>();

                    foreach (ERP_Mercury.Common.CCompany objCompany in objCompanyList)
                    {
                        CompanyIBIdList.Add(objCompany.InterBaseID);
                    }
                }
                objCompanyList = null;

                cboxCustomer.Properties.Items.Clear();
                LoadStockForCompany(cboxStock, cboxCompany);
                LoadStockForCompany(cboxStockSrc, cboxCompanySrc);
                LoadStockForCompany(cboxStockDst, cboxCompanyDst);

                m_objWaybillStateList = CWaybillState.GetWaybillStateList(m_objProfile, ref strErr);
                m_objIntWaybillStateList = CIntWaybillState.GetIntWaybillStateList(m_objProfile, ref strErr);

                WaybillShipMode.Properties.Items.AddRange( CWaybillShipMode.GetWaybillShipModeList( m_objProfile, ref strErr ) );
                IntWaybillShipMode.Properties.Items.AddRange(CIntWaybillShipMode.GetWaybillShipModeList(m_objProfile, ref strErr));
                
            }
            catch (System.Exception f)
            {
                SendMessageToLog("LoadComboBox. Текст ошибки: " + f.Message);
            }
            return;
        }

        /// <summary>
        /// Загружает журнал неотгруженных накладных
        /// </summary>
        /// <returns>true - удачное завершение операции; false - ошибка</returns>
        public System.Boolean LoadWaybillList()
        {
            System.Boolean bRet = false;
            this.Cursor = Cursors.WaitCursor;
            System.String strErr = System.String.Empty;
            try
            {
                System.Int32 iRowHandle = gridViewWaybill.FocusedRowHandle;

                gridControlWaybill.DataSource = null;
                System.Guid uuidCompanyId = ((cboxCompany.SelectedItem == null) ? System.Guid.Empty : ((ERP_Mercury.Common.CCompany)cboxCompany.SelectedItem).ID);
                System.Guid uuidCustomerId = (((cboxCustomer.SelectedItem == null) || (System.Convert.ToString(cboxCustomer.SelectedItem) == "") || (cboxCustomer.Text == strWaitCustomer)) ? System.Guid.Empty : ((ERP_Mercury.Common.CCustomer)cboxCustomer.SelectedItem).ID);
                System.Guid uuidStockId = ((cboxStock.SelectedItem == null) ? System.Guid.Empty : ((ERP_Mercury.Common.CStock)cboxStock.SelectedItem).ID);
                System.Guid uuidPaymentTypeId = System.Guid.Empty;
                System.Boolean bOnlyUnShippedWaybills = true;

                Depart.Properties.Items.Clear();

                m_objWaybillList = ERP_Mercury.Common.CWaybill.GetWaybillList(m_objProfile, System.Guid.Empty, false, dtBeginDate.DateTime,
                    dtEndDate.DateTime, uuidCompanyId, uuidStockId, uuidPaymentTypeId, uuidCustomerId, ref strErr, bOnlyUnShippedWaybills);

                if (m_objWaybillList != null)
                {
                    if (WaybilllNum.Text.Trim().Length > 0)
                    {
                        m_objWaybillList = m_objWaybillList.Where<CWaybill>(x => x.DocNum.Contains(WaybilllNum.Text.Trim()) == true).ToList<CWaybill>();
                    }
                    if (m_objWaybillList != null)
                    {
                        m_objWaybillList = m_objWaybillList.Where<CWaybill>(x => x.WaybillState.WaybillStateId == 0).ToList<CWaybill>();

                        if (m_objWaybillList != null)
                        {
                            if (m_objWaybillList.Count > 0)
                            {
                                List<CDepart> objDepartList = new List<CDepart>();

                                foreach (CWaybill objWaybill in m_objWaybillList)
                                {
                                    if (objDepartList.SingleOrDefault<CDepart>(x => x.uuidID.CompareTo(objWaybill.Depart.uuidID) == 0) == null)
                                    {
                                        objDepartList.Add(objWaybill.Depart);
                                    }
                                }

                                Depart.Properties.Items.AddRange(objDepartList);

                                objDepartList = null;
                            }
                            gridControlWaybill.DataSource = m_objWaybillList;
                        }
                    }
                }

                if ((gridViewWaybill.RowCount > 0) && (iRowHandle >= 0) && (gridViewWaybill.RowCount > iRowHandle))
                {
                    gridViewWaybill.FocusedRowHandle = iRowHandle;
                }

                CheckOpportunityForShippedParts();

                bRet = true;
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Загрузка журнала не отгруженных накладных. Текст ошибки: " + f.Message);
                //
            }
            finally
            {
                this.Cursor = Cursors.Default;
                tabControl.SelectedTabPage = tabPageWaybill ;
                this.Refresh();
            }

            return bRet;
        }

        /// <summary>
        /// Загружает журнал неотгруженных накладных на внутреннее перемещение
        /// </summary>
        /// <returns>true - удачное завершение операции; false - ошибка</returns>
        public System.Boolean LoadIntWaybillList()
        {
            System.Boolean bRet = false;
            this.Cursor = Cursors.WaitCursor;
            System.String strErr = System.String.Empty;
            try
            {
                System.Int32 iRowHandle = gridViewIntWaybill.FocusedRowHandle;

                gridControlIntWaybill.DataSource = null;
                System.DateTime IntWaybill_DateBegin = dtBeginDateIntWaybill.DateTime;
                System.DateTime IntWaybill_DateEnd = dtEndDateIntWaybill.DateTime;
                System.Guid uuidCompanySrcId = ((cboxCompanySrc.SelectedItem == null) ? System.Guid.Empty : ((ERP_Mercury.Common.CCompany)cboxCompanySrc.SelectedItem).ID);
                System.Guid uuidStockSrcId = ((cboxStockSrc.SelectedItem == null) ? System.Guid.Empty : ((ERP_Mercury.Common.CStock)cboxStockSrc.SelectedItem).ID);
                System.Guid uuidCompanyDstId = ((cboxCompanyDst.SelectedItem == null) ? System.Guid.Empty : ((ERP_Mercury.Common.CCompany)cboxCompanyDst.SelectedItem).ID);
                System.Guid uuidStockDstId = ((cboxStockDst.SelectedItem == null) ? System.Guid.Empty : ((ERP_Mercury.Common.CStock)cboxStockDst.SelectedItem).ID);
                System.Guid uuidPaymentTypeId = System.Guid.Empty;
                System.Boolean bOnlyUnShippedWaybills = false;

                Depart.Properties.Items.Clear();

                m_objIntWaybillList = ERP_Mercury.Common.CIntWaybill.GetWaybillList(m_objProfile, System.Guid.Empty, IntWaybill_DateBegin,
                    IntWaybill_DateEnd, uuidCompanySrcId, uuidStockSrcId, uuidCompanyDstId, uuidStockDstId, uuidPaymentTypeId, 
                    ref strErr, bOnlyUnShippedWaybills);

                if (m_objIntWaybillList != null)
                {
                    if (IntWaybilllNum.Text.Trim().Length > 0)
                    {
                        m_objIntWaybillList = m_objIntWaybillList.Where<CIntWaybill>(x => x.DocNum.Contains(IntWaybilllNum.Text.Trim()) == true).ToList<CIntWaybill>();
                    }
                    if (m_objIntWaybillList != null)
                    {
                        //m_objIntWaybillList = m_objIntWaybillList.Where<CIntWaybill>(x => x.DocState.IntWaybillStateId == 0).ToList<CIntWaybill>();

                        //if (m_objIntWaybillList != null)
                        //{
                            gridControlIntWaybill.DataSource = m_objIntWaybillList;
                        //}
                    }
                }

                if ((gridViewIntWaybill.RowCount > 0) && (iRowHandle >= 0) && (gridViewIntWaybill.RowCount > iRowHandle))
                {
                    gridViewIntWaybill.FocusedRowHandle = iRowHandle;
                }

                CheckOpportunityForShippedParts();

                bRet = true;
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Загрузка журнала не отгруженных накладных на внутреннее перемещение. Текст ошибки: " + f.Message);
                //
            }
            finally
            {
                this.Cursor = Cursors.Default;
                tabControl.SelectedTabPage = tabPageIntWaybill;
                this.Refresh();
            }

            return bRet;
        }
        
        private void btnWaybillRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadWaybillList();
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка обновления списка. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }
        private void dtBeginDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter) { LoadWaybillList(); }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка обновления списка. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }

        private void btnIntWaybillRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadIntWaybillList();
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка обновления списка. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }

        private void dtBeginDateIntWaybill_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter) { LoadIntWaybillList(); }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка обновления списка. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }

        #endregion

        #region Открытие формы
        private void frmShippedProducts_Shown(object sender, EventArgs e)
        {
            try
            {
                LoadComboBox();

                LoadWaybillList();

                LoadIntWaybillList();

                tabControl.ShowTabHeader = DevExpress.Utils.DefaultBoolean.True;
                tabControl.SelectedTabPage = tabPageWaybill;

                SetColorsForSearch();

                StartThreadWithLoadData();
            }
            catch (System.Exception f)
            {
                SendMessageToLog("frmShippedProducts_Shown. Текст ошибки: " + f.Message);
            }
            return;
        }
        #endregion

        #region Перемещение по списку накладных

        /// <summary>
        /// Возвращает ссылку на выбранную в списке накладную
        /// </summary>
        /// <returns>ссылка на накладную</returns>
        private ERP_Mercury.Common.CWaybill GetSelectedWaybill()
        {
            ERP_Mercury.Common.CWaybill objRet = null;

            try
            {
                if ((((DevExpress.XtraGrid.Views.Grid.GridView)gridControlWaybill.MainView).RowCount > 0) &&
                    (((DevExpress.XtraGrid.Views.Grid.GridView)gridControlWaybill.MainView).FocusedRowHandle >= 0))
                {
                    System.Guid uuidID = (System.Guid)(((DevExpress.XtraGrid.Views.Grid.GridView)gridControlWaybill.MainView)).GetFocusedRowCellValue("ID");

                    objRet = m_objWaybillList.Single<ERP_Mercury.Common.CWaybill>(x => x.ID.CompareTo(uuidID) == 0);
                }
            }//try
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка поиска выбранного элемента списка. Текст ошибки: " + f.Message);
            }
            finally
            {
            }

            return objRet;
        }
        /// <summary>
        /// Возвращает ссылку на выбранную в списке накладную
        /// </summary>
        /// <returns>ссылка на накладную</returns>
        private ERP_Mercury.Common.CIntWaybill GetSelectedIntWaybill()
        {
            ERP_Mercury.Common.CIntWaybill objRet = null;

            try
            {
                if ((((DevExpress.XtraGrid.Views.Grid.GridView)gridControlIntWaybill.MainView).RowCount > 0) &&
                    (((DevExpress.XtraGrid.Views.Grid.GridView)gridControlIntWaybill.MainView).FocusedRowHandle >= 0))
                {
                    System.Guid uuidID = (System.Guid)(((DevExpress.XtraGrid.Views.Grid.GridView)gridControlIntWaybill.MainView)).GetFocusedRowCellValue("ID");

                    objRet = m_objIntWaybillList.Single<ERP_Mercury.Common.CIntWaybill>(x => x.ID.CompareTo(uuidID) == 0);
                }
            }//try
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка поиска выбранного элемента списка. Текст ошибки: " + f.Message);
            }
            finally
            {
            }

            return objRet;
        }

        /// <summary>
        /// Проверка возможности начать процедуру объединения накладных
        /// </summary>
        private void CheckOpportunityForShippedParts()
        {
            try
            {
                btnShippedProducts.Enabled = ((IsAvailableDR_ShippedWaybillPayForm1 || IsAvailableDR_ShippedWaybillPayForm2) && ( gridViewWaybill.RowCount > 0) );
                btnShippedProductsByIntWaybill.Enabled = ((IsAvailableDR_ShippedWaybillPayForm1 || IsAvailableDR_ShippedWaybillPayForm2) && (gridViewIntWaybill.RowCount > 0));
            }
            catch (System.Exception f)
            {
                SendMessageToLog("LoadWaybillInfo. Текст ошибки: " + f.Message);
            }
            return;
        }

        private void gridViewWaybill_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                CheckOpportunityForShippedParts();
            }
            catch (System.Exception f)
            {
                SendMessageToLog("gridViewList_FocusedRowChanged. Текст ошибки: " + f.Message);
            }
            return;
        }

        private void gridViewWaybill_RowCountChanged(object sender, EventArgs e)
        {
            try
            {
                CheckOpportunityForShippedParts();
            }
            catch (System.Exception f)
            {
                SendMessageToLog("gridViewList_RowCountChanged. Текст ошибки: " + f.Message);
            }
            return;
        }


        #endregion

        #region Проверка значений для поиска
        private System.Boolean ValidatePropertiesWaybill()
        {
            System.Boolean bRet = true;
            try
            {
                bRet = (cboxStock.SelectedItem != null);
            }
            catch (System.Exception f)
            {
                SendMessageToLog("ValidatePropertiesWaybill. Текст ошибки: " + f.Message);
            }

            return bRet;
        }
        private System.Boolean ValidatePropertiesIntWaybill()
        {
            System.Boolean bRet = true;
            try
            {
                bRet = (cboxStockSrc.SelectedItem != null);
            }
            catch (System.Exception f)
            {
                SendMessageToLog("ValidatePropertiesIntWaybill. Текст ошибки: " + f.Message);
            }

            return bRet;
        }
        private void SetColorsForSearch()
        {
            try
            {
                //cboxCustomer.Properties.Appearance.BackColor = ((cboxCustomer.SelectedItem == null) ? System.Drawing.Color.Tomato : System.Drawing.Color.White);
                cboxStock.Properties.Appearance.BackColor = ((cboxStock.SelectedItem == null) ? System.Drawing.Color.Tomato : System.Drawing.Color.White);
                cboxStockSrc.Properties.Appearance.BackColor = ((cboxStockSrc.SelectedItem == null) ? System.Drawing.Color.Tomato : System.Drawing.Color.White);
            }
            catch (System.Exception f)
            {
                SendMessageToLog("SetColorsForSearch. Текст ошибки: " + f.Message);
            }

            return;
        }

        private void ChangeConditionForSearch()
        {
            SetColorsForSearch();

            btnWaybillRefresh.Enabled = ValidatePropertiesWaybill();
            btnIntWaybillRefresh.Enabled = ValidatePropertiesIntWaybill();
        }

        private void cboxStock_SelectedValueChanged(object sender, EventArgs e)
        {
            ChangeConditionForSearch();
        }
        #endregion

        #region Отгрузка накладных

        public void StartThreadAutoShipWaybillList()
        {
            try
            {
                if( gridViewWaybill.RowCount == 0)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Список накладных пуст.", "Внимание!",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                    Cursor = Cursors.Default;
                    return;
                }

                // список накладных на отгрузку
                List<CWaybill> objWaybillList = new List<CWaybill>();
                for (System.Int32 i = 0; i < gridViewWaybill.RowCount; i++)
                {
                    if (m_objWaybillList[gridViewWaybill.GetDataSourceRowIndex(i)].WaybillState.WaybillStateId == 0)
                    {
                        objWaybillList.Add(m_objWaybillList[gridViewWaybill.GetDataSourceRowIndex(i)]);
                    }
                }

                if (objWaybillList.Count == 0)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Список накладных на отгрузку пуст.", "Внимание!",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                    Cursor = Cursors.Default;
                    return;
                }

                System.Double SumQuantity = objWaybillList.Sum<CWaybill>(x => x.Quantity);
                System.Double SumWithDiscount = objWaybillList.Sum<CWaybill>(x => x.SumWithDiscount);

                if (DevExpress.XtraEditors.XtraMessageBox.Show(String.Format("Общее количество и сумма накладных на отгрузку составляет\nКоличество\t:{0:### ### ### ##0} шт.\t\tСумма:\t{1:### ### ### ##0.00} руб.\n\nПодтвердите начало отгрузки товара.", SumQuantity, SumWithDiscount), "Подтверждение",
                    System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                {
                    Cursor = Cursors.Default;
                    return;
                }

                tableLayoutPanel4.RowStyles[m_indxWarningPanel].Height = m_iHightWarningPanel;
                SearchProcessWoring.Visible = true;
                SearchProcessWoring.Refresh();
                Cursor = Cursors.WaitCursor;

                // инициализируем делегаты
                m_SetInfoInSearchProcessWoringDelegate = new SetInfoInSearchProcessWoringDelegate(SetInfoInSearchProcessWoring);
                m_SetResultAutoShipWaybillListDelegate = new SetResultAutoShipWaybillListDelegate(SetResultAutoShipWaybillList);


                // запуск потока
                this.ThreadAutoShipDocumentList = new System.Threading.Thread(unused => AutoShipWaybillListInThread(objWaybillList,
                    dtWaybill_ShipDate.DateTime, ((WaybillShipMode.SelectedItem == null) ? null : (CWaybillShipMode)WaybillShipMode.SelectedItem), 
                    txtRepresentative.Text.Trim()));
                this.ThreadAutoShipDocumentList.Start();

                Thread.Sleep(1000);

            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("StartThreadAutoPayDebitDocumentList().\n\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }

        public void AutoShipWaybillListInThread(List<CWaybill> objWaybillList,
            System.DateTime Waybill_ShipDate, CWaybillShipMode objWaybillShipMode, System.String strRepresentative)
        {
            try
            {

                if ((objWaybillList == null) || (objWaybillList.Count == 0))
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Список накладных для отгрузки пуст.", "Внимание!",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                    return;
                }

                System.Int32 ERROR_NUM = 0;
	            System.Guid Waybill_Guid = System.Guid.Empty;
	            System.String ShipDescription = ( ( strRepresentative.Trim().Length == 0 ) ? System.String.Empty : strRepresentative );
	            System.Guid SetWaybillShipMode_Guid = System.Guid.Empty;
	            System.Guid WaybillState_Guid = System.Guid.Empty;
                System.Double WaybillsTotalSum = 0;

                System.String strErr = System.String.Empty;
                System.Int32 iCurrentIndex = 0;
                System.Int32 iAllObjectCount = objWaybillList.Count;
                System.Int32 iRet = 0;


                foreach (CWaybill objWaybill in objWaybillList)
                {
                    iCurrentIndex++;

                    Waybill_Guid = objWaybill.ID;
                    //ShipDescription = System.String.Empty;
                    SetWaybillShipMode_Guid = (((objWaybillShipMode == null) || (objWaybillShipMode.ID.CompareTo(System.Guid.Empty) == 0)) ? objWaybill.WaybillShipMode.ID : objWaybillShipMode.ID);
                    WaybillState_Guid = System.Guid.Empty;
                    ERROR_NUM = 0;
                    strErr = System.String.Empty;
                    iRet = 0;

                    if ((objWaybill.WaybillState.WaybillStateId == 0) && (objWaybill.Customer != null))
                    {
                        Thread.Sleep(1000);
                        this.Invoke(m_SetInfoInSearchProcessWoringDelegate, new Object[] { iCurrentIndex, iAllObjectCount, 
                            ( String.Format("{0} от {1}", objWaybill.DocNum, objWaybill.BeginDate.ToShortDateString()) ), 
                        objWaybill.SumWithDiscount, objWaybill.CustomerName });

                        iRet = CWaybill.ShippedProductsByWaybill(m_objProfile, Waybill_Guid, Waybill_ShipDate, 
                            SetWaybillShipMode_Guid, ShipDescription, ref WaybillState_Guid, ref ERROR_NUM, ref strErr );

                        if (iRet == 0)
                        {
                            WaybillsTotalSum += objWaybill.SumWithDiscount;
                            Thread.Sleep(1000);
                            this.Invoke(m_SetResultAutoShipWaybillListDelegate, new Object[] { ERROR_NUM, strErr,
                                Waybill_Guid, Waybill_ShipDate, WaybillState_Guid,
                               iAllObjectCount, WaybillsTotalSum, false });
                        }
                    }

                    if (iCurrentIndex == iAllObjectCount)
                    {
                        // все накладные обработаны, выводится сообщение о завершении процесса 
                        System.String strTmp = System.String.Empty;
                        System.DateTime dtTmp = System.DateTime.Today;
                        System.Guid uuidTmp = System.Guid.Empty;
                        System.Int32 intTmp = 0;

                        Thread.Sleep(2000);
                        this.Invoke(m_SetResultAutoShipWaybillListDelegate, new Object[] { intTmp, strTmp, 
                            uuidTmp, dtTmp,  uuidTmp, 
                            iAllObjectCount, WaybillsTotalSum,  true});
                    }


                }


            }
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка отгрузки. Текст ошибки: " + f.Message);
            }

            return;
        }

        public void SetInfoInSearchProcessWoring(System.Int32 iCurrentIndex, System.Int32 iAllObjectCount,
            System.String Waybill_Num, System.Double Waybill_TotalSum, System.String Customer_Name 
            )
        {
            try
            {
                if (SearchProcessWoring.Visible == false) { SearchProcessWoring.Visible = true; }

                SendMessageToLog(System.String.Format("Обрабатывается запись №{0:### ### ##0}  из {1:### ### ##0}", iCurrentIndex, iAllObjectCount));
                SendMessageToLog(System.String.Format("Отгружается накладная №:\t{0}\t на сумму:\t{1:### ### ##0.00}  Клиент:\t{2}", Waybill_Num, Waybill_TotalSum, Customer_Name));

                lblShippingWaybillProcessInfo.Text = System.String.Format("накладная №:\t{0}\t на сумму:\t{1:### ### ##0.00}  клиент:\t{2}", Waybill_Num, Waybill_TotalSum, Customer_Name);
                lblShippingWaybillProcessInfo.Refresh();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("SetInfoInSearchProcessWoring.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }
            return;
        }

        public void SetResultAutoShipWaybillList(System.Int32 ERROR_NUM, System.String ERROR_STR,
            System.Guid Waybill_Guid, System.DateTime Waybill_ShipDate, System.Guid WaybillState_Guid,
            System.Int32 WaybillsCount, System.Double WaybillsTotalSum,
            System.Boolean bLastDocument
            )
        {
            try
            {
                if (bLastDocument == true)
                {
                    Cursor = Cursors.Default;

                    tableLayoutPanel4.RowStyles[m_indxWarningPanel].Height = 0;
                    SearchProcessWoring.Visible = false;
                    gridControlWaybill.RefreshDataSource();

                    Cursor = Cursors.Default;

                    DevExpress.XtraEditors.XtraMessageBox.Show(System.String.Format("Отгрузка твоара по накладным завершена.\nОтгружено накладных: {0:### ### ##0} на сумму: {1:### ### ##0}  ", WaybillsCount, WaybillsTotalSum), "Внимание!",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
                else
                {
                    if (ERROR_NUM == 0)
                    {
                        if (Waybill_Guid.CompareTo( System.Guid.Empty ) != 0)
                        {
                            CWaybill objItem = m_objWaybillList.SingleOrDefault<CWaybill>(x => x.ID.CompareTo(Waybill_Guid) == 0);

                            if (objItem != null)
                            {
                                objItem.ShipDate = Waybill_ShipDate;
                                objItem.WaybillState = m_objWaybillStateList.SingleOrDefault<CWaybillState>(x=>x.ID.CompareTo(WaybillState_Guid) == 0);
                            }
                            else
                            {
                                m_objMenuItem.SimulateNewMessage(String.Format("Не найдена накладная с УИ: {0}", WaybillState_Guid));
                               // SendMessageToLog(String.Format("Не найдена накладная с УИ: {0}", WaybillState_Guid));
                            }

                            gridControlWaybill.RefreshDataSource();
                        }
                    }
                    else
                    {
                        m_objMenuItem.SimulateNewMessage(ERROR_STR);
                        //SendMessageToLog(ERROR_STR);
                    }

                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("SetResultAutoShipWaybillList.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }
            return;
        }

        private void btnShippedProducts_Click(object sender, EventArgs e)
        {
            StartThreadAutoShipWaybillList();
        }

        private void ShipWaybill(CWaybill objWaybill, System.DateTime Waybill_ShipDate)
        {
            try
            {

                if (objWaybill == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Необходимо указать накладную для отгрузки.", "Внимание!",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                    return;
                }

                if (objWaybill.WaybillState.WaybillStateId != 0)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Накладная не может быть отгружена, так как состояние накладной: " + objWaybill.WaybillState.Name, "Внимание!",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                    return;
                }

                if (objWaybill.Customer == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Накладная не может быть отгружена, так как в документе не указан клиент.", "Внимание!",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                    return;
                }

                System.Int32 ERROR_NUM = 0;
                System.Guid Waybill_Guid = objWaybill.ID;
                System.String ShipDescription = System.String.Empty;
                System.Guid SetWaybillShipMode_Guid = objWaybill.WaybillShipMode.ID;
                System.Guid WaybillState_Guid = System.Guid.Empty;
                System.Double WaybillsTotalSum = objWaybill.SumWithDiscount;

                System.String strErr = System.String.Empty;

                Cursor = Cursors.WaitCursor;

                System.Int32 iRet = CWaybill.ShippedProductsByWaybill(m_objProfile, Waybill_Guid, Waybill_ShipDate,
                    SetWaybillShipMode_Guid, ShipDescription, ref WaybillState_Guid, ref ERROR_NUM, ref strErr);

                Cursor = Cursors.Default;

                if (iRet == 0)
                {
                    SetResultAutoShipWaybillList(ERROR_NUM, strErr, Waybill_Guid, Waybill_ShipDate, WaybillState_Guid,  1, WaybillsTotalSum, false);

                    System.String strTmp = System.String.Empty;
                    System.DateTime dtTmp = System.DateTime.Today;
                    System.Guid uuidTmp = System.Guid.Empty;
                    System.Int32 intTmp = 0;

                    SetResultAutoShipWaybillList(intTmp, strTmp, uuidTmp, dtTmp, uuidTmp, 1, WaybillsTotalSum, true);
                }


            }
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка отгрузки. Текст ошибки: " + f.Message);
            }

            return;
        }

        private void menuItemShipWaybill_Click(object sender, EventArgs e)
        {
            ShipWaybill(SelectedWaybill, dtWaybill_ShipDate.DateTime);
        }

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            try
            {
                menuItemShipWaybill.Enabled = (SelectedWaybill != null);
            }
            catch (System.Exception f)
            {
                SendMessageToLog("contextMenuStrip_Opening. Текст ошибки: " + f.Message);
            }

            return;
        }

        private void checkEditSetWaybillShipMode_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkEditSetWaybillShipMode.CheckState == CheckState.Unchecked)
                {
                    WaybillShipMode.SelectedItem = null;
                    WaybillShipMode.Enabled = false;
                }
                else if (checkEditSetWaybillShipMode.CheckState == CheckState.Checked)
                {
                    WaybillShipMode.SelectedItem = ((WaybillShipMode.Properties.Items.Count > 0) ? WaybillShipMode.Properties.Items[0] : null);
                    WaybillShipMode.Enabled = true;
                }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("checkEditSetWaybillShipMode_CheckStateChanged. Текст ошибки: " + f.Message);
            }

            return;
        }

        private void checkEditRepresentative_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkEditRepresentative.CheckState == CheckState.Unchecked)
                {
                    txtRepresentative.Text = System.String.Empty;
                    txtRepresentative.Enabled = false;
                }
                else if (checkEditRepresentative.CheckState == CheckState.Checked)
                {
                    txtRepresentative.Enabled = true;
                }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("checkEditSetWaybillShipMode_CheckStateChanged. Текст ошибки: " + f.Message);
            }

            return;
        }

        
        #endregion

        #region Отгрузка накладных на внутреннее перемещение

        private void btnShippedProductsByIntWaybill_Click(object sender, EventArgs e)
        {
            StartThreadAutoShipIntWaybillList();
        }

        public void StartThreadAutoShipIntWaybillList()
        {
            try
            {
                if (gridViewIntWaybill.RowCount == 0)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Список накладных пуст.", "Внимание!",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                    Cursor = Cursors.Default;
                    return;
                }

                // список накладных на отгрузку
                List<CIntWaybill> objIntWaybillList = new List<CIntWaybill>();
                for (System.Int32 i = 0; i < gridViewIntWaybill.RowCount; i++)
                {
                    if (m_objIntWaybillList[gridViewIntWaybill.GetDataSourceRowIndex(i)].DocState.IntWaybillStateId == 0)
                    {
                        objIntWaybillList.Add(m_objIntWaybillList[gridViewIntWaybill.GetDataSourceRowIndex(i)]);
                    }
                }

                if (objIntWaybillList.Count == 0)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Список накладных на отгрузку пуст.", "Внимание!",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                    Cursor = Cursors.Default;
                    return;
                }

                System.Double SumQuantity = objIntWaybillList.Sum<CIntWaybill>(x => x.Quantity);
                System.Double SumWithDiscount = objIntWaybillList.Sum<CIntWaybill>(x => x.SumWaybill);

                if (DevExpress.XtraEditors.XtraMessageBox.Show(String.Format("Общее количество и сумма накладных на отгрузку составляет\nКоличество\t:{0:### ### ### ##0} шт.\t\tСумма:\t{1:### ### ### ##0.00} руб.\n\nПодтвердите начало отгрузки товара.", SumQuantity, SumWithDiscount), "Подтверждение",
                    System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                {
                    Cursor = Cursors.Default;
                    return;
                }

                tableLayoutPanelIntWaybill.RowStyles[m_indxWarningPanel].Height = m_iHightWarningPanel;
                SearchProcessWoringIntWaybill.Visible = true;
                SearchProcessWoringIntWaybill.Refresh();
                Cursor = Cursors.WaitCursor;

                // инициализируем делегаты
                m_SetInfoInSearchProcessIntWoringDelegate = new SetInfoInSearchProcessIntWoringDelegate(SetInfoInSearchProcessIntWoring);
                m_SetResultAutoShipIntWaybillListDelegate = new SetResultAutoShipIntWaybillListDelegate(SetResultAutoShipIntWaybillList);


                // запуск потока
                this.ThreadAutoShipIntDocumentList = new System.Threading.Thread(unused => AutoShipIntWaybillListInThread(objIntWaybillList, 
                    dtIntWaybill_ShipDate.DateTime, 
                    ((IntWaybillShipMode.SelectedItem == null) ? null : (CIntWaybillShipMode)IntWaybillShipMode.SelectedItem) ));
                this.ThreadAutoShipIntDocumentList.Start();

                Thread.Sleep(1000);

            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("StartThreadAutoShipIntWaybillList().\n\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }

        public void AutoShipIntWaybillListInThread(List<CIntWaybill> objIntWaybillList, 
            System.DateTime IntWaybill_ShipDate, CIntWaybillShipMode objIntWaybillShipMode)
        {
            try
            {

                if ((objIntWaybillList == null) || (objIntWaybillList.Count == 0))
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Список накладных для отгрузки пуст.", "Внимание!",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                    return;
                }

                System.Int32 ERROR_NUM = 0;
                System.Guid IntWaybill_Guid = System.Guid.Empty;
                System.Guid SetIntWaybillShipMode_Guid = System.Guid.Empty;
                System.Guid IntWaybillState_Guid = System.Guid.Empty;
                System.Double IntWaybillsTotalSum = 0;

                System.String strErr = System.String.Empty;
                System.Int32 iCurrentIndex = 0;
                System.Int32 iAllObjectCount = objIntWaybillList.Count;
                System.Int32 iRet = 0;


                foreach (CIntWaybill objIntWaybill in objIntWaybillList)
                {
                    iCurrentIndex++;

                    IntWaybill_Guid = objIntWaybill.ID;
                    SetIntWaybillShipMode_Guid = (((objIntWaybillShipMode == null) || (objIntWaybillShipMode.ID.CompareTo(System.Guid.Empty) == 0)) ? objIntWaybill.WaybillShipMode.ID : objIntWaybillShipMode.ID);
                    IntWaybillState_Guid = System.Guid.Empty;
                    ERROR_NUM = 0;
                    strErr = System.String.Empty;
                    iRet = 0;

                    if( objIntWaybill.DocState.IntWaybillStateId == 0)
                    {
                        Thread.Sleep(1000);
                        this.Invoke(m_SetInfoInSearchProcessIntWoringDelegate, new Object[] { iCurrentIndex, iAllObjectCount, 
                            ( String.Format("{0} от {1}", objIntWaybill.DocNum, objIntWaybill.BeginDate.ToShortDateString()) ), 
                        objIntWaybill.SumWithDiscount, objIntWaybill.StockSrcName, objIntWaybill.StockDstName });

                        iRet = CIntWaybill.ShippedProductsByIntWaybill(m_objProfile, IntWaybill_Guid, IntWaybill_ShipDate,
                            SetIntWaybillShipMode_Guid, ref IntWaybillState_Guid, ref ERROR_NUM, ref strErr);

                        if (iRet == 0)
                        {
                            IntWaybillsTotalSum += objIntWaybill.SumWaybill;
                            Thread.Sleep(1000);
                            this.Invoke(m_SetResultAutoShipIntWaybillListDelegate, new Object[] { ERROR_NUM, strErr,
                                IntWaybill_Guid, IntWaybill_ShipDate, IntWaybillState_Guid, SetIntWaybillShipMode_Guid, 
                               iAllObjectCount, IntWaybillsTotalSum, false });
                        }
                    }

                    if (iCurrentIndex == iAllObjectCount)
                    {
                        // все накладные обработаны, выводится сообщение о завершении процесса 
                        System.String strTmp = System.String.Empty;
                        System.DateTime dtTmp = System.DateTime.Today;
                        System.Guid uuidTmp = System.Guid.Empty;
                        System.Int32 intTmp = 0;

                        Thread.Sleep(2000);
                        this.Invoke(m_SetResultAutoShipIntWaybillListDelegate, new Object[] { intTmp, strTmp, 
                            uuidTmp, dtTmp,  uuidTmp, uuidTmp,
                            iAllObjectCount, IntWaybillsTotalSum,  true});
                    }


                }


            }
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка отгрузки. Текст ошибки: " + f.Message);
            }

            return;
        }

        
        public void SetInfoInSearchProcessIntWoring(System.Int32 iCurrentIndex, System.Int32 iAllObjectCount,
            System.String IntWaybill_Num, System.Double IntWaybill_TotalSum, System.String SrcStock_Name, System.String DstStock_Name
            )
        {
            try
            {
                if (SearchProcessWoringIntWaybill.Visible == false) { SearchProcessWoringIntWaybill.Visible = true; }

                SendMessageToLog(System.String.Format("Обрабатывается запись №{0:### ### ##0}  из {1:### ### ##0}", iCurrentIndex, iAllObjectCount));
                SendMessageToLog(System.String.Format("Отгружается накладная №:\t{0}\t на сумму:\t{1:### ### ##0.00} со склада:\t{2}\tна склад:\t{3}", IntWaybill_Num, IntWaybill_TotalSum, SrcStock_Name, DstStock_Name));

                lblShippingIntWaybillProcessInfo.Text = System.String.Format("накладная №:\t{0}\t на сумму:\t{1:### ### ##0.00} со склада:\t{2}\tна склад:\t{3}", IntWaybill_Num, IntWaybill_TotalSum, SrcStock_Name, DstStock_Name);
                lblShippingIntWaybillProcessInfo.Refresh();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("SetInfoInSearchProcessIntWoring.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }
            return;
        }

        public void SetResultAutoShipIntWaybillList(System.Int32 ERROR_NUM, System.String ERROR_STR,
            System.Guid IntWaybill_Guid, System.DateTime IntWaybill_ShipDate, 
            System.Guid IntWaybillState_Guid, System.Guid SetIntWaybillShipMode_Guid,
            System.Int32 IntWaybillsCount, System.Double IntWaybillsTotalSum,
            System.Boolean bLastDocument
            )
        {
            try
            {
                if (bLastDocument == true)
                {
                    Cursor = Cursors.Default;

                    tableLayoutPanelIntWaybill.RowStyles[m_indxWarningPanel].Height = 0;
                    SearchProcessWoringIntWaybill.Visible = false;
                    gridControlIntWaybill.RefreshDataSource();

                    Cursor = Cursors.Default;

                    DevExpress.XtraEditors.XtraMessageBox.Show(System.String.Format("Отгрузка товара по накладным на внутреннее перемещение завершена.\nОтгружено накладных: {0:### ### ##0} на сумму: {1:### ### ##0}  ", IntWaybillsCount, IntWaybillsTotalSum), "Внимание!",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
                else
                {
                    if (ERROR_NUM == 0)
                    {
                        if (IntWaybill_Guid.CompareTo(System.Guid.Empty) != 0)
                        {
                            CIntWaybill objItem = m_objIntWaybillList.SingleOrDefault<CIntWaybill>(x => x.ID.CompareTo(IntWaybill_Guid) == 0);

                            if (objItem != null)
                            {
                                objItem.ShipDate = IntWaybill_ShipDate;
                                objItem.DocState = m_objIntWaybillStateList.SingleOrDefault<CIntWaybillState>(x => x.ID.CompareTo(IntWaybillState_Guid) == 0);
                                if (objItem.WaybillShipMode.ID.CompareTo(SetIntWaybillShipMode_Guid) != 0)
                                {
                                    objItem.WaybillShipMode = IntWaybillShipMode.Properties.Items.Cast<CIntWaybillShipMode>().SingleOrDefault<CIntWaybillShipMode>(x => x.ID.CompareTo(SetIntWaybillShipMode_Guid) == 0);
                                }
                            }
                            else
                            {
                                m_objMenuItem.SimulateNewMessage(String.Format("Не найдена накладная с УИ: {0}", IntWaybillState_Guid));
                                // SendMessageToLog(String.Format("Не найдена накладная с УИ: {0}", WaybillState_Guid));
                            }

                            gridControlIntWaybill.RefreshDataSource();
                        }
                    }
                    else
                    {
                        m_objMenuItem.SimulateNewMessage(ERROR_STR);
                        //SendMessageToLog(ERROR_STR);
                    }

                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("SetResultAutoShipIntWaybillList.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }
            return;
        }
        
        private void contextMenuStripInt_Opening(object sender, CancelEventArgs e)
        {
            try
            {
                menuItemShipInt.Enabled = (SelectedIntWaybill != null);
            }
            catch (System.Exception f)
            {
                SendMessageToLog("contextMenuStripInt_Opening. Текст ошибки: " + f.Message);
            }

            return;

        }

        private void checkEditSetIntWaybillShipMode_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkEditSetIntWaybillShipMode.CheckState == CheckState.Unchecked)
                {
                    IntWaybillShipMode.SelectedItem = null;
                    IntWaybillShipMode.Enabled = false;
                }
                else if (checkEditSetIntWaybillShipMode.CheckState == CheckState.Checked)
                {
                    IntWaybillShipMode.SelectedItem = ((IntWaybillShipMode.Properties.Items.Count > 0) ? IntWaybillShipMode.Properties.Items[0] : null);
                    IntWaybillShipMode.Enabled = true;
                }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("checkEditSetWaybillShipMode_CheckStateChanged. Текст ошибки: " + f.Message);
            }

            return;
        }

        private void ShipIntWaybill(CIntWaybill objIntWaybill, System.DateTime IntWaybill_ShipDate, CIntWaybillShipMode objIntWaybillShipMode)
        {
            try
            {

                if (objIntWaybill == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Необходимо указать накладную для отгрузки.", "Внимание!",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                    return;
                }

                if (objIntWaybill.DocState.IntWaybillStateId != 0)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Накладная не может быть отгружена, так как состояние накладной: " + objIntWaybill.DocState.Name, "Внимание!",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                    return;
                }

                System.Int32 ERROR_NUM = 0;
                System.Guid IntWaybill_Guid = objIntWaybill.ID;
                System.Guid SetIntWaybillShipMode_Guid = ((objIntWaybillShipMode == null) ? objIntWaybill.WaybillShipMode.ID : objIntWaybillShipMode.ID );
                System.Guid IntWaybillState_Guid = System.Guid.Empty;
                System.Double IntWaybillsTotalSum = objIntWaybill.SumWaybill;

                System.String strErr = System.String.Empty;

                Cursor = Cursors.WaitCursor;

                System.Int32 iRet = CIntWaybill.ShippedProductsByIntWaybill(m_objProfile, IntWaybill_Guid, IntWaybill_ShipDate,
                    SetIntWaybillShipMode_Guid, ref IntWaybillState_Guid, ref ERROR_NUM, ref strErr);

                Cursor = Cursors.Default;

                if (iRet == 0)
                {
                    SetResultAutoShipIntWaybillList(ERROR_NUM, strErr, IntWaybill_Guid, IntWaybill_ShipDate,
                        IntWaybillState_Guid, SetIntWaybillShipMode_Guid, 1, IntWaybillsTotalSum, false);

                    System.String strTmp = System.String.Empty;
                    System.DateTime dtTmp = System.DateTime.Today;
                    System.Guid uuidTmp = System.Guid.Empty;
                    System.Int32 intTmp = 0;

                    SetResultAutoShipIntWaybillList(intTmp, strTmp, uuidTmp, dtTmp, uuidTmp, uuidTmp, 1, IntWaybillsTotalSum, true);
                }


            }
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка отгрузки. Текст ошибки: " + f.Message);
            }

            return;
        }

        private void menuItemShipInt_Click(object sender, EventArgs e)
        {
            ShipIntWaybill( SelectedIntWaybill, dtIntWaybill_ShipDate.DateTime, 
                ( ( IntWaybillShipMode.SelectedItem == null ) ? null : (CIntWaybillShipMode)IntWaybillShipMode.SelectedItem ));
        }
        #endregion


    }

    public class ViewShippedProducts : PlugIn.IClassTypeView
    {
        public override void Run(UniXP.Common.MENUITEM objMenuItem, System.String strCaption)
        {
            frmShippedProducts obj = new frmShippedProducts(objMenuItem);
            obj.Text = strCaption;
            obj.MdiParent = objMenuItem.objProfile.m_objMDIManager.MdiParent;
            obj.Visible = true;
        }
    }


}

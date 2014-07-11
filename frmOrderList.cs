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
using Excel = Microsoft.Office.Interop.Excel;

namespace ERPMercuryProcessingOrder
{
    public partial class frmOrderList : DevExpress.XtraEditors.XtraForm
    {
        #region Свойства
        private UniXP.Common.CProfile m_objProfile;
        private UniXP.Common.MENUITEM m_objMenuItem;
        private List<ERP_Mercury.Common.CCustomer> m_objCustomerList;
        private List<ERP_Mercury.Common.COrder> m_objOrderList;
        private List<ERP_Mercury.Common.COrderState> m_objOrderStateList;
        private ERP_Mercury.Common.COrder m_objSelectedOrder;
        private ERP_Mercury.Common.ctrlOrderForCustomer frmOrderEditor;
        private ctrlWaybillEditor frmItemEditor;
        private DevExpress.XtraGrid.Views.Base.ColumnView ColumnView
        {
            get { return gridControlAgreementList.MainView as DevExpress.XtraGrid.Views.Base.ColumnView; }
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
        //private const System.String strCompany = "Валгон";
        private System.Boolean m_bThreadFinishJob;
        private const System.String m_strOrderBlankFileName = "Бланк заявки.xlsm";

        // используемые динамические права 
        private System.Boolean IsAvailableDR_ViewSupplPayForm1;             // "Заказ ф1 просмотр";
        private System.Boolean IsAvailableDR_ViewSupplPayForm2;             // "Заказ ф2 просмотр";
        private System.Boolean IsAvailableDR_EditSupplPayForm1;             // "Заказ ф1 редактировать";
        private System.Boolean IsAvailableDR_EditSupplPayForm2;             // "Заказ ф2 редактировать";
        private System.Boolean IsAvailableDR_MoveSupplToWaybillPayForm1;    // "Заказ ф1 перевод в ТТН";
        private System.Boolean IsAvailableDR_MoveSupplToWaybillPayForm2;    // "Заказ ф2 перевод в ТТН";
        private System.Boolean IsAvailableDR_UserCanEditOrder;            // "Обнуление цен и запуск расчета цен в заказе";
        private System.Boolean IsAvailableDR_UserCanExcludeOrderFromADJ;  // "Исключение заказа из акта выполненных работ";
        private System.Boolean IsAvailableDR_IncludeOrderInADJ;             // "Включить заказ в акт выполненных работ";
        private System.Boolean IsAvailableDR_ExcludeOrderInADJ;             // "Исключить заказ из акта выполненных работ";
        
        #endregion

        #region Конструктор
        public frmOrderList(UniXP.Common.MENUITEM objMenuItem)
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
            IsAvailableDR_ViewSupplPayForm1 = objClientRights.GetState(ERPMercuryProcessingOrder.Consts.strDR_ViewSupplPayForm1);
            IsAvailableDR_ViewSupplPayForm2 = objClientRights.GetState(ERPMercuryProcessingOrder.Consts.strDR_ViewSupplPayForm2);
            IsAvailableDR_EditSupplPayForm1 = objClientRights.GetState(ERPMercuryProcessingOrder.Consts.strDR_EditSupplPayForm1);
            IsAvailableDR_EditSupplPayForm2 = objClientRights.GetState(ERPMercuryProcessingOrder.Consts.strDR_EditSupplPayForm2);
            IsAvailableDR_MoveSupplToWaybillPayForm1 = objClientRights.GetState(ERPMercuryProcessingOrder.Consts.strDR_MoveSupplToWaybillPayForm1);
            IsAvailableDR_MoveSupplToWaybillPayForm2 = objClientRights.GetState(ERPMercuryProcessingOrder.Consts.strDR_MoveSupplToWaybillPayForm2);

            IsAvailableDR_UserCanEditOrder = objClientRights.GetState(ERPMercuryProcessingOrder.Consts.strDRUserCanEditOrder);
            IsAvailableDR_UserCanExcludeOrderFromADJ = objClientRights.GetState(ERPMercuryProcessingOrder.Consts.strDRUserCanExcludeOrderFromADJ);
            IsAvailableDR_IncludeOrderInADJ = objClientRights.GetState(ERPMercuryProcessingOrder.Consts.strIncludeOrderInADJ);
            IsAvailableDR_ExcludeOrderInADJ = objClientRights.GetState(ERPMercuryProcessingOrder.Consts.strExcludeOrderInADJ);
            
            objClientRights = null;
            
            m_objOrderList = null;
            frmItemEditor = null;
            m_objSelectedOrder = null;
            m_bThreadFinishJob = false;
            m_objCustomerList = new List<ERP_Mercury.Common.CCustomer>();
            m_objOrderStateList = null;

            AddGridColumns();

            dtBeginDate.DateTime = System.DateTime.Today.AddDays(-7); // new DateTime(System.DateTime.Today.Year, 1, 1);
            dtEndDate.DateTime = System.DateTime.Today; //new DateTime(System.DateTime.Today.Year, 12, 31);

        }

        private void OnChangeOrderPropertie(Object sender, ERP_Mercury.Common.ChangeOrderForCustomerPropertieEventArgs e)
        {
            try
            {
                switch (e.ActionType)
                {
                    case ERP_Mercury.Common.enumActionSaveCancel.Cancel:
                        {
                            CancelChangesInOrder();
                            break;
                        }
                    case ERP_Mercury.Common.enumActionSaveCancel.Save:
                        {
                            if (e.IsNewOrder == true)
                            {
                                m_objSelectedOrder = null;
                                LoadOrderList();
                            }
                            else
                            {
                                SaveChangesInOrder();
                            }
                            break;
                        }
                }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("OnChangeOrderPropertie. Текст ошибки: " + f.Message);
            }
            finally // очищаем занимаемые ресурсы
            {
            }

            return;
        }

        private void OnChangeWaybillPropertie(Object sender, ChangeWaybillPropertieEventArgs e)
        {
            try
            {
                tabControl.SelectedTabPage = tabPageViewer;
                if (e.ActionType == ERP_Mercury.Common.enumActionSaveCancel.Save)
                {
                    ERP_Mercury.Common.COrder objOrder = GetSelectedOrder();
                    if( (objOrder != null) && ( e.OrderCurrentStateGuid.CompareTo( System.Guid.Empty) != 0 ) )
                    {
                        if (objOrder.OrderState.ID.CompareTo(e.OrderCurrentStateGuid) != 0)
                        {
                            if ((m_objOrderStateList != null) && (m_objOrderStateList.Count > 0))
                            {
                                objOrder.OrderState = m_objOrderStateList.SingleOrDefault<ERP_Mercury.Common.COrderState>(x => x.ID.CompareTo(e.OrderCurrentStateGuid) == 0);
                                gridControlAgreementList.RefreshDataSource();
                            }
                        }

                    }
                }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("OnChangeWaybillPropertie. Текст ошибки: " + f.Message);
            }
            finally // очищаем занимаемые ресурсы
            {
            }

            return;
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
                    strFileFullName = strPath + "\\" + strDllName;
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

                barBtnAdd.Enabled = false;
                barBtnEdit.Enabled = false;
                barBtnDelete.Enabled = false;
                barBtnCopy.Enabled = false;
                barbtnImportProduct.Enabled = false;

                gridControlAgreementList.MouseDoubleClick -= new MouseEventHandler(gridControlAgreementGrid_MouseDoubleClick);

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
                    barBtnAdd.Enabled = ( IsAvailableDR_EditSupplPayForm1 || IsAvailableDR_EditSupplPayForm2 );
                    barBtnEdit.Enabled = ( ( IsAvailableDR_EditSupplPayForm1 || IsAvailableDR_EditSupplPayForm2 ) && ( gridViewAgreementList.FocusedRowHandle >= 0 ) );
                    barBtnCopy.Enabled = ( ( IsAvailableDR_EditSupplPayForm1 || IsAvailableDR_EditSupplPayForm2 ) && (gridViewAgreementList.FocusedRowHandle >= 0) );
                    barBtnDelete.Enabled = ((IsAvailableDR_EditSupplPayForm1 || IsAvailableDR_EditSupplPayForm2) && (gridViewAgreementList.FocusedRowHandle >= 0));
                    //barbtnImportProduct.Enabled = true;
                    if (IsAvailableDR_ViewSupplPayForm1 || IsAvailableDR_ViewSupplPayForm2)
                    {
                        gridControlAgreementList.MouseDoubleClick += new MouseEventHandler(gridControlAgreementGrid_MouseDoubleClick);
                    }
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
                if( (frmOrderEditor == null) && (frmItemEditor == null) ) { return bRet; }

                bRet = ((frmOrderEditor.ThreadAddress != null) && (frmOrderEditor.ThreadAddress.IsAlive == true) &&
                    (frmItemEditor.ThreadAddress != null) && (frmItemEditor.ThreadAddress.IsAlive == true));
            
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

                    if (System.Threading.WaitHandle.WaitAll((new System.Threading.ManualResetEvent[] { frmOrderEditor.EventThreadStopped, frmItemEditor.EventThreadStopped }), 100, true))
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

        private void frmOrderList_FormClosing(object sender, FormClosingEventArgs e)
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

        #region Отмена изменений в редакторе заказа
        private void CancelChangesInOrder()
        {
            try
            {
                ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
                this.SuspendLayout();

                tabControl.SelectedTabPage = tabPageViewer;
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Отмена изменений. Текст ошибки: " + f.Message);
            }
            finally // очищаем занимаемые ресурсы
            {
                ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
                this.ResumeLayout(false);
            }

            return;
        }
        #endregion

        #region Изменения в редакторе заказа подтверждены
        private void SaveChangesInOrder()
        {
            try
            {
                ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
                this.SuspendLayout();

                tabControl.SelectedTabPage = tabPageViewer;
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Подтверждение изменений. Текст ошибки: " + f.Message);
            }
            finally // очищаем занимаемые ресурсы
            {
                ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
                this.ResumeLayout(false);
            }

            return;
        }
        #endregion

        #region Редактировать заказ
        /// <summary>
        /// Загружает свойства заказа для редактирования
        /// </summary>
        /// <param name="objAgreement">заказ</param>
        private void EditOrder(ERP_Mercury.Common.COrder objOrder)
        {
            if (objOrder == null) { return; }

            if ((IsAvailableDR_ViewSupplPayForm1 || IsAvailableDR_ViewSupplPayForm2) == false)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("У Вас нет прав на операцию \"просмотр заказа\".", "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                return;
            }

            try
            {
                m_objSelectedOrder = objOrder;

                ERP_Mercury.Common.CAddress.Init(m_objProfile, null, m_objSelectedOrder.AddressDelivery, m_objSelectedOrder.AddressDelivery.ID);
                m_objSelectedOrder.OrderItemList = ERP_Mercury.Common.COrderRepository.GetOrderItemList(m_objProfile, m_objSelectedOrder.ID);

                //((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
                //this.SuspendLayout();

                if (frmOrderEditor == null)
                {
                    frmOrderEditor = new ERP_Mercury.Common.ctrlOrderForCustomer(m_objProfile, m_objMenuItem, m_objCustomerList);
                    tableLayoutPanelAgreementEditor.Controls.Add(frmOrderEditor, 0, 0);
                    frmOrderEditor.Dock = DockStyle.Fill;
                    frmOrderEditor.ChangeOrderForCustomerProperties += OnChangeOrderPropertie;
                }

                frmOrderEditor.EditOrder(objOrder, false);

                tabControl.SelectedTabPage = tabPageEditor;

            }
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка редактирования заказа. Текст ошибки: " + f.Message);
            }
            finally
            {
                //((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
                //this.ResumeLayout(false);
                //tabControl.SelectedTabPage = tabPageEditor;
            }
            return;
        }
        private void barBtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                EditOrder(GetSelectedOrder());

            }//try
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка редактирования заказа. Текст ошибки: " + f.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            return;
        }

        private void gridControlAgreementGrid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                EditOrder(GetSelectedOrder());

            }//try
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка редактирования заказа. Текст ошибки: " + f.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            return;
        }
        /// <summary>
        /// Возвращает ссылку на выбранный в списке заказ
        /// </summary>
        /// <returns>ссылка на заказ</returns>
        private ERP_Mercury.Common.COrder GetSelectedOrder()
        {
            ERP_Mercury.Common.COrder objRet = null;
            try
            {
                if ((((DevExpress.XtraGrid.Views.Grid.GridView)gridControlAgreementList.MainView).RowCount > 0) &&
                    (((DevExpress.XtraGrid.Views.Grid.GridView)gridControlAgreementList.MainView).FocusedRowHandle >= 0))
                {
                    System.Guid uuidID = (System.Guid)(((DevExpress.XtraGrid.Views.Grid.GridView)gridControlAgreementList.MainView)).GetFocusedRowCellValue("ID");

                    objRet = m_objOrderList.Single<ERP_Mercury.Common.COrder>(x => x.ID.CompareTo(uuidID) == 0);
                }
            }//try
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка поиска выбранного заказа. Текст ошибки: " + f.Message);
            }
            finally
            {
            }

            return objRet;
        }

        #endregion

        #region Копировать заказ
        /// <summary>
        /// Копирует свойства заказа в новый и открывает его для редактирования
        /// </summary>
        /// <param name="objOrder">заказ</param>
        private void CopyOrder(ERP_Mercury.Common.COrder objOrder)
        {
            if (objOrder == null) { return; }
            if ((IsAvailableDR_EditSupplPayForm1 || IsAvailableDR_EditSupplPayForm2) == false)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("У Вас нет прав на операцию \"копирование заказа\".", "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                return;
            }
            try
            {
                m_objSelectedOrder = objOrder;

                ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
                this.SuspendLayout();

                if (frmOrderEditor == null)
                {
                    frmOrderEditor = new ERP_Mercury.Common.ctrlOrderForCustomer(m_objProfile, m_objMenuItem, m_objCustomerList);
                    tableLayoutPanelAgreementEditor.Controls.Add(frmOrderEditor, 0, 0);
                    frmOrderEditor.Dock = DockStyle.Fill;
                    frmOrderEditor.ChangeOrderForCustomerProperties += OnChangeOrderPropertie;
                }

                frmOrderEditor.CopyOrder(objOrder);

                tabControl.SelectedTabPage = tabPageEditor;

            }
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка копирования заказа. Текст ошибки: " + f.Message);
            }
            finally
            {
                ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
                this.ResumeLayout(false);
                tabControl.SelectedTabPage = tabPageEditor;
            }
            return;
        }

        private void barBtnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                CopyOrder(GetSelectedOrder());

            }//try
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка копирования заказа. Текст ошибки: " + f.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            return;
        }

        #endregion

        #region Удалить заказ
        /// <summary>
        /// Удаляет заказ
        /// </summary>
        /// <param name="objOrder">заказ</param>
        private void DeleteOrder(ERP_Mercury.Common.COrder objOrder)
        {
            if (objOrder == null) { return; }
            if ((IsAvailableDR_EditSupplPayForm1 || IsAvailableDR_EditSupplPayForm2) == false)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("У Вас нет прав на операцию \"удаление заказа\".", "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                return;
            }
            try
            {
                System.Int32 iFocusedRowHandle = gridViewAgreementList.FocusedRowHandle;
                if (DevExpress.XtraEditors.XtraMessageBox.Show("Подтвердите, пожалуйста, удаление заказа.\n\n№" + objOrder.Num.ToString() + "\nКлиент: " + objOrder.Customer.FullName, "Подтверждение",
                    System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == DialogResult.No) { return; }

                System.String strErr = "";
                if (ERP_Mercury.Common.COrderRepository.MakeDeletedDB(m_objProfile, null, objOrder.ID, ref strErr) == true)
                {
                    LoadOrderList();
                    //this.tableLayoutPanel2.SuspendLayout();
                    //((System.ComponentModel.ISupportInitialize)(this.gridControlAgreementList)).BeginInit();

                    //objOrder = ERP_Mercury.Common.COrderRepository.GetOrderList(m_objProfile, dtBeginDate.DateTime,
                    //    dtEndDate.DateTime, objOrder.Customer.ID, System.Guid.Empty, objOrder.Stock.ID,
                    //    objOrder.PaymentType.ID, objOrder.ID)[0];

                    //gridViewAgreementList.RefreshRow(iFocusedRowHandle);

                    //gridControlAgreementList.RefreshDataSource();
                    //gridViewAgreementList.RefreshData();

                    //this.tableLayoutPanel2.ResumeLayout(false);
                    //((System.ComponentModel.ISupportInitialize)(this.gridControlAgreementList)).EndInit();
                }
                else
                {
                    SendMessageToLog("Удаление заказа. Текст ошибки: " + strErr);
                }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Удаление заказа. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }
        private void barBtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteOrder(GetSelectedOrder());
            }//try
            catch (System.Exception f)
            {
                SendMessageToLog("Удаление заказа. Текст ошибки: " + f.Message);
            }
            finally
            {
            }

            return;
        }
        #endregion

        #region Новый заказ
        /// <summary>
        /// Открывает редактор заказа в режиме "Новый заказ"
        /// </summary>
        private void NewOrder()
        {
            if ((IsAvailableDR_EditSupplPayForm1 || IsAvailableDR_EditSupplPayForm2) == false)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("У Вас нет прав на операцию \"создание нового заказа\".", "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                return;
            }

            try
            {
                ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
                this.SuspendLayout();

                if (frmOrderEditor == null)
                {
                    frmOrderEditor = new ERP_Mercury.Common.ctrlOrderForCustomer(m_objProfile, m_objMenuItem, m_objCustomerList);
                    tableLayoutPanelAgreementEditor.Controls.Add(frmOrderEditor, 0, 0);
                    frmOrderEditor.Dock = DockStyle.Fill;
                    frmOrderEditor.ChangeOrderForCustomerProperties += OnChangeOrderPropertie;
                }

                ERP_Mercury.Common.CCompany objSelectedCompany = ( ((cboxCompany.SelectedItem == null) || (System.Convert.ToString(cboxCompany.SelectedItem) == "") ) ? null : ((ERP_Mercury.Common.CCompany)cboxCompany.SelectedItem));
                ERP_Mercury.Common.CStock objSelectedStock = (((cboxStock.SelectedItem == null) || (System.Convert.ToString(cboxStock.SelectedItem) == "")) ? null : ((ERP_Mercury.Common.CStock)cboxStock.SelectedItem));
                ERP_Mercury.Common.CCustomer objSelectedCustomer = (((cboxCustomer.SelectedItem == null) || (System.Convert.ToString(cboxCustomer.SelectedItem) == "") || (cboxCustomer.Text == strWaitCustomer)) ? null : ((ERP_Mercury.Common.CCustomer)cboxCustomer.SelectedItem));
                ERP_Mercury.Common.CPaymentType objSelectedPayment = (((cboxPaymentType.SelectedItem == null) || (System.Convert.ToString(cboxPaymentType.SelectedItem) == "")) ? null : ((ERP_Mercury.Common.CPaymentType)cboxPaymentType.SelectedItem));

                frmOrderEditor.NewOrder(objSelectedCustomer, objSelectedCompany, objSelectedStock, objSelectedPayment);

                tabControl.SelectedTabPage = tabPageEditor;

            }
            catch (System.Exception f)
            {
                SendMessageToLog("Создание заказа. Текст ошибки: " + f.Message);
            }
            finally
            {
                ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
                this.ResumeLayout(false);
                tabControl.SelectedTabPage = tabPageEditor;
            }
            return;
        }
        private void barBtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                NewOrder();
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Создание заказа. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }

        #endregion

        #region Импорт заказа
        private void btnImportProduct_Click(object sender, EventArgs e)
        {
            try
            {
                ImportDataFromExcel();
            }
            catch (System.Exception f)
            {
                SendMessageToLog("btnImportProduct_Click. Текст ошибки: " + f.Message);
            }
            return;
        }
        /// <summary>
        /// Импорт данных из файла MS Excel
        /// </summary>
        private void ImportDataFromExcel()
        {
            if ((IsAvailableDR_EditSupplPayForm1 || IsAvailableDR_EditSupplPayForm2) == false)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("У Вас нет прав на операцию \"импорт заказа\".", "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                return;
            }
            try
            {
                if (cboxCompany.SelectedItem == null) { return; }
                if (cboxStock.SelectedItem == null) { return; }
                NewOrder();
                if (frmOrderEditor != null)
                { frmOrderEditor.ImportFromExcel(); }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("ImportDataFromExcel. Текст ошибки: " + f.Message);
            }
            return;
        }

        #endregion

        #region Список заказов

        private void LoadStockForCompany()
        {
            try
            {
                cboxStock.SelectedItem = null;
                cboxStock.Properties.Items.Clear();
                cboxStock.Properties.Items.Add( new ERP_Mercury.Common.CStock() );
                
                ERP_Mercury.Common.CCompany objSelectedCompany = ((cboxCompany.SelectedItem == null) || ( ((ERP_Mercury.Common.CCompany)cboxCompany.SelectedItem).ID == System.Guid.Empty ) ? null : (ERP_Mercury.Common.CCompany)cboxCompany.SelectedItem);
                
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

                                //if (objStock.ToString().IndexOf(strCompany) > 0)
                                //{ StockIdList.Add(objStock.IBId); }
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

        private void LoadComboBox()
        {
            try
            {
                cboxCompany.Properties.Items.Clear();
                cboxCompany.Properties.Items.Add(new ERP_Mercury.Common.CCompany());
                List<ERP_Mercury.Common.CCompany> objCompanyList = ERP_Mercury.Common.CCompany.GetCompanyList(m_objProfile, null);
                cboxCompany.Properties.Items.AddRange(objCompanyList);

                if( ( objCompanyList != null ) && ( objCompanyList.Count > 0 ) )
                {
                    List<int> CompanyIBIdList = new List<int>();

                    foreach( ERP_Mercury.Common.CCompany objCompany in objCompanyList )
                    {
                        CompanyIBIdList.Add( objCompany.InterBaseID );
                    }

                    //cboxCompany.SelectedItem = objCompanyList.Single<ERP_Mercury.Common.CCompany>(x => x.InterBaseID == CompanyIBIdList.Min<int>() );
                }
                objCompanyList = null;

                cboxPaymentType.Properties.Items.Clear();
                cboxPaymentType.Properties.Items.Add(new ERP_Mercury.Common.CPaymentType( System.Guid.Empty, "" ) );
                cboxPaymentType.Properties.Items.AddRange(ERP_Mercury.Common.CPaymentType.GetPaymentTypeList(m_objProfile, null, System.Guid.Empty));

                cboxCustomer.Properties.Items.Clear();
                cboxCustomer.Properties.Items.Add(new ERP_Mercury.Common.CCustomer());

                m_objOrderStateList = ERP_Mercury.Common.COrderState.GetOrderStateList(m_objProfile, null);

                LoadStockForCompany();

            }
            catch (System.Exception f)
            {
                SendMessageToLog("LoadComboBox. Текст ошибки: " + f.Message);
            }
            return;
        }

        private void AddGridColumns()
        {
            ColumnView.Columns.Clear();
            AddGridColumn(ColumnView, "OrderStateName", "Состояние");
            AddGridColumn(ColumnView, "Num", "Номер");
            AddGridColumn(ColumnView, "BeginDate", "Дата заказа");
            AddGridColumn(ColumnView, "DeliveryDate", "Дата достаки");
            AddGridColumn(ColumnView, "CustomerName", "Клиент");
            AddGridColumn(ColumnView, "CompanyAbbr", "Компания");
            AddGridColumn(ColumnView, "StockName", "Склад");
            AddGridColumn(ColumnView, "DepartCode", "Подразделение");
            AddGridColumn(ColumnView, "SalesManName", "Торговый представитель");
            AddGridColumn(ColumnView, "OrderTypeName", "Тип заказа");
            AddGridColumn(ColumnView, "PaymentTypeName", "Форма оплаты");
            AddGridColumn(ColumnView, "ID", "Идентификатор");
            AddGridColumn(ColumnView, "OrderStateId", "Код состояния");
            AddGridColumn(ColumnView, "QuantityReserved", "Кол-во, шт.");
            AddGridColumn(ColumnView, "SumReserved", "Сумма, руб.");
            AddGridColumn(ColumnView, "SumReservedWithDiscount", "Сумма со скидкой, руб.");
            AddGridColumn(ColumnView, "SumReservedInAccountingCurrency", "Сумма, вал.");
            AddGridColumn(ColumnView, "SumReservedWithDiscountInAccountingCurrency", "Сумма со скидкой, вал.");
            AddGridColumn(ColumnView, "DirectionDeliveryName", "Направление доставки");
            AddGridColumn(ColumnView, "DirectionDeliveryCityName", "Населенный пункт");
            AddGridColumn(ColumnView, "IsBonus", "Бонус");
            
            foreach (DevExpress.XtraGrid.Columns.GridColumn objColumn in ColumnView.Columns)
            {
                objColumn.OptionsColumn.AllowEdit = false;
                //objColumn.OptionsColumn.AllowFocus = false;
                objColumn.OptionsColumn.ReadOnly = true;
                if (objColumn.FieldName == "CustomerName")
                {
                    objColumn.BestFit();
                    //objColumn.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                }
                if ((objColumn.FieldName == "ID") || (objColumn.FieldName == "OrderStateId"))
                {
                    objColumn.Visible = false;
                }
                if (objColumn.FieldName == "BeginDate")
                {
                    objColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    objColumn.DisplayFormat.FormatString = "dd.MM.yyyy HH:MM:ss";
                }
                if (objColumn.FieldName == "QuantityReserved")
                {
                    objColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    objColumn.DisplayFormat.FormatString = "### ### ##0.000";
                    objColumn.SummaryItem.FieldName = objColumn.FieldName;
                    objColumn.SummaryItem.DisplayFormat = "{0:### ### ##0.000}";
                    objColumn.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                }
                if ((objColumn.FieldName == "SumReserved") || (objColumn.FieldName == "SumReservedWithDiscount"))
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
         /// <summary>
        /// Загружает список заказов
        /// </summary>
        /// <returns>true - удачное завершение операции; false - ошибка</returns>
        public System.Boolean LoadOrderList()
        {
            System.Boolean bRet = false;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                //this.tableLayoutPanel2.SuspendLayout();
                //((System.ComponentModel.ISupportInitialize)(this.gridControlAgreementList)).BeginInit();
                System.Int32 iRowHandle = gridViewAgreementList.FocusedRowHandle;

                gridControlAgreementList.DataSource = null;
                System.Guid uuidCompanyId = ((cboxCompany.SelectedItem == null) ? System.Guid.Empty : ((ERP_Mercury.Common.CCompany)cboxCompany.SelectedItem).ID);
                System.Guid uuidCustomerId = (((cboxCustomer.SelectedItem == null) || (System.Convert.ToString(cboxCustomer.SelectedItem) == "") || (cboxCustomer.Text == strWaitCustomer)) ? System.Guid.Empty : ((ERP_Mercury.Common.CCustomer)cboxCustomer.SelectedItem).ID);
                System.Guid uuidStockId = ((cboxStock.SelectedItem == null) ? System.Guid.Empty : ((ERP_Mercury.Common.CStock)cboxStock.SelectedItem).ID);
                System.Guid uuidPaymentTypeId = ((cboxPaymentType.SelectedItem == null) ? System.Guid.Empty : ((ERP_Mercury.Common.CPaymentType)cboxPaymentType.SelectedItem).ID);
                System.Boolean bSearchByOrderDeliveryDate = (radioGroupSearchByDeliveryDate.SelectedIndex > 0);

                m_objOrderList = ERP_Mercury.Common.COrderRepository.GetOrderList(m_objProfile, dtBeginDate.DateTime,
                    dtEndDate.DateTime, uuidCustomerId, uuidCompanyId, uuidStockId, uuidPaymentTypeId,
                    System.Guid.Empty, bSearchByOrderDeliveryDate);

                if (m_objOrderList != null)
                {
                    gridControlAgreementList.DataSource = m_objOrderList;
                }

                if ((gridViewAgreementList.RowCount > 0) && (iRowHandle >= 0) && (gridViewAgreementList.RowCount > iRowHandle))
                {
                    gridViewAgreementList.FocusedRowHandle = iRowHandle;
                }

                //this.tableLayoutPanel2.ResumeLayout(false);
                //((System.ComponentModel.ISupportInitialize)(this.gridControlAgreementList)).EndInit();

                bRet = true;
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Запрос списка заказов. Текст ошибки: " + f.Message);
                //
            }
            finally
            {
                this.Cursor = Cursors.Default;
                tabControl.SelectedTabPage = tabPageViewer;
                this.Refresh();
            }

            return bRet;
        }
        private void barBtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadOrderList();
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
                if (e.KeyCode == Keys.Enter) { LoadOrderList(); }                
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

        #region Свойства заказа
        private void gridViewAgreementList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                ShowOrderProperties(GetSelectedOrder());

                barBtnAdd.Enabled = (IsAvailableDR_EditSupplPayForm1 || IsAvailableDR_EditSupplPayForm2);
                barBtnEdit.Enabled = ( ( IsAvailableDR_EditSupplPayForm1 || IsAvailableDR_EditSupplPayForm2 ) && (e.FocusedRowHandle >= 0) );
                barBtnCopy.Enabled = ( ( IsAvailableDR_EditSupplPayForm1 || IsAvailableDR_EditSupplPayForm2 ) && (e.FocusedRowHandle >= 0) );
                barBtnDelete.Enabled = ((IsAvailableDR_EditSupplPayForm1 || IsAvailableDR_EditSupplPayForm2) && (e.FocusedRowHandle >= 0));
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Отображение свойств заказа. Текст ошибки: " + f.Message);
            }
            return;
        }
        private void gridViewAgreementList_RowCountChanged(object sender, EventArgs e)
        {
            try
            {
                ShowOrderProperties(GetSelectedOrder());

                barBtnAdd.Enabled = (IsAvailableDR_EditSupplPayForm1 || IsAvailableDR_EditSupplPayForm2);
                barBtnEdit.Enabled = ( ( IsAvailableDR_EditSupplPayForm1 || IsAvailableDR_EditSupplPayForm2 ) && (gridViewAgreementList.FocusedRowHandle >= 0) );
                barBtnCopy.Enabled = ( ( IsAvailableDR_EditSupplPayForm1 || IsAvailableDR_EditSupplPayForm2 ) && (gridViewAgreementList.FocusedRowHandle >= 0) );
                barBtnDelete.Enabled = ((IsAvailableDR_EditSupplPayForm1 || IsAvailableDR_EditSupplPayForm2) && (gridViewAgreementList.FocusedRowHandle >= 0));
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Отображение свойств заказа. Текст ошибки: " + f.Message);
            }
            return;
        }

        /// <summary>
        /// Отображает свойства заказа
        /// </summary>
        /// <param name="objOrder">заказ</param>
        private void ShowOrderProperties(ERP_Mercury.Common.COrder objOrder)
        {
            try
            {
                this.tableLayoutPanel3.SuspendLayout();

                txtCustomer.Text = "";
                txtSupplBeginDate.Text = "";
                txtSupplNumVersion.Text = "";
                txtSalesMan.Text = "";
                txtStock.Text = "";
                txtPaymentType.Text = "";
                txtOrderState.Text = "";
                txtChildDepartCode.Text = "";
                txtRtt.Text = "";
                txtDeliveryAddress.Text = "";
                txtSupplDescrpn.Text = "";

                txtSupplQty.Text = "";
                txtSupplOrderQty.Text = "";
                txtSupplPositionCount.Text = "";
                txtSupplNum.Text = "";
                txtSupplAllPrice.Text = "";
                txtSupplAllDiscountPrice.Text = "";
                txtSupplAllDiscountCurrencyPrice.Text = "";
                txtSupplAllDiscount.Text = "";
                txtSupplAllCurrencyPrice.Text = "";
                txtSupplAllCurrencyDiscount.Text = "";

                if (objOrder != null)
                {
                    txtCustomer.Text = objOrder.CustomerName;
                    txtSupplBeginDate.Text = String.Format("{0} доставка на {1}", objOrder.BeginDate.ToShortDateString(), objOrder.DeliveryDate.ToShortDateString());
                    txtSupplNumVersion.Text = String.Format("{0}/{1}/{2}", objOrder.Num, objOrder.SubNum, objOrder.Ib_ID);
                    txtSalesMan.Text = (String.Format("{0} {1}", objOrder.DepartCode, objOrder.SalesManName));
                    txtStock.Text = String.Format("{0} [{1}]", objOrder.StockName, objOrder.CompanyAbbr);
                    txtPaymentType.Text = objOrder.PaymentTypeName;
                    txtOrderState.Text = objOrder.OrderStateName;
                    txtChildDepartCode.Text = objOrder.ChildDepartCode;
                    txtRtt.Text = ((objOrder.Rtt == null) ? System.String.Empty : objOrder.Rtt.FullName);
                    txtDeliveryAddress.Text = ((objOrder.AddressDelivery == null) ? System.String.Empty : objOrder.AddressDelivery.Name);
                    txtSupplDescrpn.Text = objOrder.Description;

                    txtSupplNum.Text = String.Format("{0}/{1}", objOrder.Num, objOrder.SubNum);
                    txtSupplQty.Text = System.String.Format("{0:### ### ##0}", objOrder.QuantityOrdered);
                    txtSupplOrderQty.Text = System.String.Format("{0:### ### ##0}", objOrder.QuantityReserved);
                    txtSupplPositionCount.Text = System.String.Format("{0:### ### ##0}", objOrder.PosQuantity);
                    txtSupplAllPrice.Text = System.String.Format("{0:### ### ##0.00}", objOrder.SumReserved);
                    txtSupplAllDiscountPrice.Text = System.String.Format("{0:### ### ##0.00}", objOrder.SumReservedWithDiscount);
                    
                    txtSupplAllCurrencyPrice.Text = System.String.Format("{0:### ### ##0.00}", objOrder.SumReservedInAccountingCurrency);
                    txtSupplAllDiscountCurrencyPrice.Text = System.String.Format("{0:### ### ##0.00}", objOrder.SumReservedWithDiscountInAccountingCurrency);

                    txtSupplAllDiscount.Text = System.String.Format("{0:### ### ##0.00}", ( objOrder.SumReserved - objOrder.SumReservedWithDiscount ));
                    txtSupplAllCurrencyDiscount.Text = System.String.Format("{0:### ### ##0.00}", (objOrder.SumReservedInAccountingCurrency - objOrder.SumReservedWithDiscountInAccountingCurrency));
                }

                this.tableLayoutPanel3.ResumeLayout(false);
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Отображение свойств заказа. Текст ошибки: " + f.Message);
            }
            return;
        }
        #endregion

        #region Настройки внешнего вида журналов
        /// <summary>
        /// Считывает настройки журналов из реестра
        /// </summary>
        public void RestoreLayoutFromRegistry()
        {
            System.String strReestrPath = this.m_objProfile.GetRegKeyBase();
            strReestrPath += ("\\Tools\\");
            try
            {
                gridViewAgreementList.RestoreLayoutFromRegistry(strReestrPath + gridViewAgreementList.Name);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка загрузки настроек списка заказов.\n\nТекст ошибки : " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // очищаем занимаемые ресурсы
            {
            }

            return;
        }
        /// <summary>
        /// Записывает настройки журналов в реестр
        /// </summary>
        public void SaveLayoutToRegistry()
        {
            System.String strReestrPath = this.m_objProfile.GetRegKeyBase();
            strReestrPath += ("\\Tools\\");
            try
            {
                gridViewAgreementList.SaveLayoutToRegistry(strReestrPath + gridViewAgreementList.Name);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка записи настроек списка заказов.\n\nТекст ошибки : " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // очищаем занимаемые ресурсы
            {
            }

            return;
        }
        #endregion

        #region Журнал событий
        /// <summary>
        /// Загружает журнал событий
        /// </summary>
        private void ShowEventLog()
        {
            try
            {
                if ((gridViewAgreementList.RowCount == 0) || (gridViewAgreementList.FocusedRowHandle < 0)) { return; }
                ERP_Mercury.Common.COrder objOrder = GetSelectedOrder();

                if (objOrder != null)
                {
                    this.Cursor = Cursors.WaitCursor;
                    UniXP.Common.frmEventLog objfrmEventLog = new UniXP.Common.frmEventLog(m_objProfile, objOrder.ID);
                    if (objfrmEventLog != null)
                    {
                        objfrmEventLog.ShowDialog();
                    }
                    objfrmEventLog.Dispose();
                    objfrmEventLog = null;
                }

            }
            catch (System.Exception f)
            {
                SendMessageToLog("ShowEventLog. Текст ошибки: " + f.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
            return;
        }

        private void menuEventLog_Click(object sender, EventArgs e)
        {
            try
            {
                ShowEventLog();
            }
            catch (System.Exception f)
            {
                SendMessageToLog("ShowEventLog. Текст ошибки: " + f.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
            return;
        }

        #endregion

        #region Пересчет цен в заказе
        /// <summary>
        /// Пересчет цен в заказе
        /// </summary>
        /// <param name="objOrder">заказ</param>
        private void RecalcPricesInOrderOrder(ERP_Mercury.Common.COrder objOrder)
        {
            if (objOrder == null) { return; }

            if (IsAvailableDR_UserCanEditOrder == false)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("У Вас нет прав на операцию \"пересчет цен в заказе заказа\".", "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                return;
            }
            
            try
            {
                Cursor = Cursors.WaitCursor;

                System.Int32 iFocusedRowHandle = gridViewAgreementList.FocusedRowHandle;
                if (DevExpress.XtraEditors.XtraMessageBox.Show("Подтвердите, пожалуйста, начало процедуры пересчета цен в заказе.\n\n№" + objOrder.Num.ToString() + "\nКлиент: " + objOrder.Customer.FullName, "Подтверждение",
                    System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == DialogResult.No) { return; }

                System.String strErr = "";
                if (ERP_Mercury.Common.COrderRepository.RecalcPricesInOrder( m_objProfile, null, objOrder.ID, ref strErr) == true)
                {
                    SendMessageToLog("Пересчитаны цены в заказе №" + objOrder.Num.ToString() + " Клиент: " + objOrder.Customer.FullName);
                    LoadOrderList();
                }
                else
                {
                    SendMessageToLog("Пересчет цен в заказе. Текст ошибки: " + strErr);
                }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Пересчет цен в заказе. Текст ошибки: " + f.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            return;
        }

        private void menuCalcPrice_Click(object sender, EventArgs e)
        {
            try
            {
                RecalcPricesInOrderOrder( GetSelectedOrder() );
            }//try
            catch (System.Exception f)
            {
                SendMessageToLog("Пересчет цен в заказе. Текст ошибки: " + f.Message);
            }
            finally
            {
            }

            return;
        }

        #endregion

        #region Отрисовка ячеек грида
        private void gridViewAgreementList_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                System.Drawing.Image img = null;
                if (e.Column.FieldName == "OrderStateName")
                {
                    ERP_Mercury.Common.enPDASupplState SupplState = (ERP_Mercury.Common.enPDASupplState)gridViewAgreementList.GetRowCellValue(e.RowHandle, gridViewAgreementList.Columns["OrderStateId"]);
                    System.Int32 iImgIndx = -1;
                    switch (SupplState)
                    {
                        case ERP_Mercury.Common.enPDASupplState.CalcPricesOk:
                            {
                                iImgIndx = 0;

                                break;
                            }
                        case ERP_Mercury.Common.enPDASupplState.AutoCalcPricesOk:
                            {
                                iImgIndx = 0;
                                break;
                            }
                        case ERP_Mercury.Common.enPDASupplState.Print:
                            {
                                iImgIndx = 3;
                                break;
                            }
                        case ERP_Mercury.Common.enPDASupplState.Confirm:
                            {
                                iImgIndx = 4;
                                break;
                            }
                        case ERP_Mercury.Common.enPDASupplState.TTN:
                            {
                                iImgIndx = 5;
                                break;
                            }
                        case ERP_Mercury.Common.enPDASupplState.Shipped:
                            {
                                iImgIndx = 6;
                                break;
                            }
                        case ERP_Mercury.Common.enPDASupplState.CalcPricesFalse:
                            {
                                iImgIndx = 1;
                                break;
                            }
                        case ERP_Mercury.Common.enPDASupplState.CreateSupplInIBFalse:
                            {
                                iImgIndx = 1;
                                break;
                            }
                        case ERP_Mercury.Common.enPDASupplState.OutPartsOfStock:
                            {
                                iImgIndx = 1;
                                break;
                            }
                        case ERP_Mercury.Common.enPDASupplState.MakedForRecalcPrices:
                            {
                                iImgIndx = 7;
                                break;
                            }
                        case ERP_Mercury.Common.enPDASupplState.Deleted:
                            {
                                iImgIndx = 8;
                                break;
                            }
                        case ERP_Mercury.Common.enPDASupplState.CreditControl:
                            {
                                iImgIndx = 1;
                                break;
                            }
                        case ERP_Mercury.Common.enPDASupplState.FindStock:
                            {
                                iImgIndx = 2;
                                break;
                            }
                        case ERP_Mercury.Common.enPDASupplState.Created:
                            {
                                iImgIndx = 2;
                                break;
                            }
                        default:
                            {
                                iImgIndx = 0;
                                break;
                            }
                    }

                    if (iImgIndx >= imageCollection.Images.Count)
                    {
                        iImgIndx = 0;
                    }

                    img = imageCollection.Images[iImgIndx];
                }

                // собственно отрисовка
                if (img != null)
                {
                    Rectangle rImg = new Rectangle(e.Bounds.X + 3, e.Bounds.Y + (e.Bounds.Height - img.Size.Height) / 2, img.Width, img.Height);
                    e.Graphics.DrawImage(img, rImg);
                    Rectangle r = e.Bounds;

                    //r.Inflate(-1, -1);
                    //e.Graphics.FillRectangle(e.Appearance.brush, r);

                    r.Inflate(-( rImg.Width + 5 ), 0);
                    e.Appearance.DrawString(e.Cache, e.DisplayText, r);

                    e.Handled = true;
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("gridViewAgreementList_CustomDrawCell\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return;
        }
        #endregion

        #region Формирование бланка заказа
        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            try
            {
                menuCreateOrderBlank.Enabled = (cboxCompany.Text.Trim() != "" );

                menuCalcPrice.Enabled = ((gridViewAgreementList.FocusedRowHandle >= 0) && (IsAvailableDR_UserCanEditOrder == true));
                menuClearPrices.Enabled = ((gridViewAgreementList.FocusedRowHandle >= 0) && (IsAvailableDR_UserCanEditOrder == true));
                menuMakeSupplDeleted.Enabled = ((gridViewAgreementList.FocusedRowHandle >= 0) && (IsAvailableDR_UserCanEditOrder == true));
                menuReturnSupplStateToAutoProcessPrices.Enabled = ((gridViewAgreementList.FocusedRowHandle >= 0) && (IsAvailableDR_UserCanEditOrder == true));
                menuTransformSupplToWaybillInManualMode.Enabled = ((gridViewAgreementList.FocusedRowHandle >= 0) && (IsAvailableDR_MoveSupplToWaybillPayForm1 || IsAvailableDR_MoveSupplToWaybillPayForm2));
                menuTransformSupplToWaybillInAutoMode.Enabled = ((gridViewAgreementList.FocusedRowHandle >= 0) && (IsAvailableDR_MoveSupplToWaybillPayForm1 || IsAvailableDR_MoveSupplToWaybillPayForm2));
            }
            catch (System.Exception f)
            {
                SendMessageToLog("contextMenuStrip_Opening. Текст ошибки: " + f.Message);
            }
            return;
        }

        private void menuCreateOrderBlank_Click(object sender, EventArgs e)
        {
            try
            {
                CreateOrderBlankInExcel();
            }
            catch (System.Exception f)
            {
                SendMessageToLog("menuCreateOrderBlank_Click. Текст ошибки: " + f.Message);
            }
            return;
        }

        private void CreateOrderBlankInExcel()
        {
            Excel.Application oXL = null;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;
            System.Int32 iStartRow = 10;
            System.Int32 iCurrentRow = iStartRow;
            object m = Type.Missing;
            //Excel.Range oRng;

            try
            {
                if ( cboxCompany.Text.Trim() == "")
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Укажите, пожалуйста, компанию.", "Сообщение",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    return;
                }
                ERP_Mercury.Common.CCompany objSelectedCompany = ((cboxCompany.SelectedItem == null) ? null : (ERP_Mercury.Common.CCompany)cboxCompany.SelectedItem);
                if ((objSelectedCompany == null) || (objSelectedCompany.ID.CompareTo(System.Guid.Empty) == 0))
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Укажите, пожалуйста, компанию.", "Сообщение",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    return;
                }

                System.Guid uuidCompanyId = objSelectedCompany.ID;
                //objSelectedCompany = null;

                System.String strFileName = "";
                System.String strDLLPath = Application.StartupPath;
                strDLLPath += ("\\" + ERP_Mercury.Global.Consts.strReportsDirectory + "\\");

                strFileName = strDLLPath + m_strOrderBlankFileName;

                if (System.IO.File.Exists(strFileName) == false)
                {
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        this.Refresh();
                        if ((openFileDialog.FileName != "") && (System.IO.File.Exists(openFileDialog.FileName) == true))
                        {
                            strFileName = openFileDialog.FileName;
                        }
                    }
                    else
                    {
                        return;
                    }
                }

                SendMessageToLog("Идёт экспорт данных в MS Excel... ");
                this.Cursor = Cursors.WaitCursor;
                oXL = new Excel.Application();
                oWB = (Excel._Workbook)(oXL.Workbooks.Open(strFileName, 0, true, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value));
                oSheet = (Excel._Worksheet)oWB.Worksheets[1];


                // товары
                List<ERP_Mercury.Common.OrderBlankProductInfo> OrderBlankProductInfo = ERP_Mercury.Common.COrderRepository.GetOrderBlankProductInfo(m_objProfile, null, uuidCompanyId);
                if (OrderBlankProductInfo != null)
                {
                    iCurrentRow = 5;
                    foreach (ERP_Mercury.Common.OrderBlankProductInfo objProduct in OrderBlankProductInfo)
                    {

                        oSheet.Cells[iCurrentRow, 1] = objProduct.ProductArticle;
                        oSheet.Cells[iCurrentRow, 2] = objProduct.ProductName;
                        oSheet.Cells[iCurrentRow, 3] = objProduct.ProductStockQty;
                        oSheet.Cells[iCurrentRow, 4] = objProduct.ProductPackQty;
                        oSheet.Cells[iCurrentRow, 6] = objProduct.ProductPrice;
                        iCurrentRow++;
                    }
                    oSheet.get_Range("A4", "G4").AutoFilter(1, Type.Missing, Excel.XlAutoFilterOperator.xlAnd, Type.Missing, true);
                }
                OrderBlankProductInfo = null;

                // торговые представители
                oSheet = (Excel._Worksheet)oWB.Worksheets[4];
                iCurrentRow = 2;

                List<ERP_Mercury.Common.CDepart> objDepartList  = ERP_Mercury.Common.CDepart.GetDepartList(m_objProfile);
                if( objDepartList != null )
                {
                    foreach( ERP_Mercury.Common.CDepart objDepart in  objDepartList )
                    {
                        oSheet.Cells[iCurrentRow, 2] = objDepart.DepartCode;
                        oSheet.Cells[iCurrentRow, 3] = ( objDepart.SalesMan.User.LastName + " " + objDepart.SalesMan.User.FirstName );
                        iCurrentRow++;
                    }
                }
                objDepartList = null;

                // РТТ
                oSheet = (Excel._Worksheet)oWB.Worksheets[3];
                iCurrentRow = 2;
                List<ERP_Mercury.Common.OrderBlankCustomerInfo> OrderBlankCustomerInfo = ERP_Mercury.Common.COrderRepository.GetOrderBlankCustomerInfo(m_objProfile, null);
                if (OrderBlankCustomerInfo != null)
                {
                    foreach (ERP_Mercury.Common.OrderBlankCustomerInfo objCustomer in OrderBlankCustomerInfo)
                    {

                        oSheet.Cells[iCurrentRow, 2] = objCustomer.CustomerName;
                        oSheet.Cells[iCurrentRow, 3] = objCustomer.CustomerId;
                        oSheet.Cells[iCurrentRow, 4] = objCustomer.RttAddress;
                        oSheet.Cells[iCurrentRow, 5] = objCustomer.RttCode;
                        iCurrentRow++;
                    }
               }
               
                OrderBlankCustomerInfo = null;



                ((Excel._Worksheet)oWB.Worksheets[1]).Activate();

                oXL.Visible = true;
                oXL.UserControl = true;

            }
            catch (System.Exception f)
            {
                oXL = null;
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Ошибка экспорта в MS Excel.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                oSheet = null;
                oWB = null;
                oXL = null;
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }


        #endregion

        #region Закрытие формы
        private void frmOrderList_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                SaveLayoutToRegistry();
            }
            catch (System.Exception f)
            {
                SendMessageToLog("frmOrderList_FormClosed. Текст ошибки: " + f.Message);
            }
            return;
        }
        #endregion

        #region Открытие формы
        private void frmOrderList_Shown(object sender, EventArgs e)
        {
            try
            {
                RestoreLayoutFromRegistry();

                LoadComboBox();

                LoadOrderList();

                tabControl.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;

                StartThreadWithLoadData();
            }
            catch (System.Exception f)
            {
                SendMessageToLog("frmOrderList_Shown. Текст ошибки: " + f.Message);
            }
            return;
        }
        #endregion

        private void cboxCompany_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                LoadStockForCompany();
            }
            catch (System.Exception f)
            {
                SendMessageToLog("cboxCompany_SelectedValueChanged. Текст ошибки: " + f.Message);
            }
            return;
        }

        private void cboxStock_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                System.Boolean bEnableImport = (
                    (((cboxStock.SelectedItem != null) && (System.Convert.ToString(cboxStock.SelectedItem) != ""))) &&
                    ((cboxPaymentType.SelectedItem != null) && (System.Convert.ToString(cboxPaymentType.SelectedItem) != ""))
                    );
                barbtnImportProduct.Enabled = bEnableImport;
            }
            catch (System.Exception f)
            {
                SendMessageToLog("cboxStock_SelectedValueChanged. Текст ошибки: " + f.Message);
            }
            return;
        }

        #region Накладная

        /// <summary>
        /// Загружает информацию о накладной в редактор
        /// </summary>
        /// <param name="Waybill_Guid">УИ накладной</param>
        private void LoadWaybill( System.Guid Waybill_Guid )
        {
            if (Waybill_Guid.CompareTo( System.Guid.Empty ) == 0) { return; }
            System.String strErr = System.String.Empty;

            try
            {
                ERP_Mercury.Common.CWaybill objItem = ERP_Mercury.Common.CWaybill.GetWaybill(m_objProfile, Waybill_Guid, ref strErr);
                if (objItem == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( strErr, "Ошибка",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                    return;
                }
                Cursor = Cursors.WaitCursor;

                ERP_Mercury.Common.CAddress.Init(m_objProfile, null, objItem.AddressDelivery, objItem.AddressDelivery.ID);
                objItem.WaybillItemList = ERP_Mercury.Common.CWaybillItem.GetWaybillTablePart(m_objProfile, objItem.ID, ref strErr);

                if (objItem.SalesMan == null)
                {
                    List<ERP_Mercury.Common.CSalesMan> objSalesManList = ERP_Mercury.Common.CSalesMan.GetSalesManListForDepart(m_objProfile, null, objItem.Depart.uuidID, ref strErr);
                    if ((objSalesManList != null) && (objSalesManList.Count > 0))
                    {
                        objItem.SalesMan = objSalesManList[0];
                    }
                }

                if (frmItemEditor == null)
                {
                    frmItemEditor = new ctrlWaybillEditor(m_objProfile, m_objMenuItem, m_objCustomerList);
                    tableLayoutPanelWaybillEditor.Controls.Add(frmItemEditor, 0, 0);
                    frmItemEditor.Dock = DockStyle.Fill;
                    frmItemEditor.ChangeWaybillForCustomerProperties += this.OnChangeWaybillPropertie;
                }

                frmItemEditor.EditWaybill(objItem, false);

                tabControl.SelectedTabPage = tabPageWaybill;

            }
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка загрузки накладной. Текст ошибки: " + f.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            return;
        }
        /// <summary>
        /// Загружает информацию о накладной для заказа
        /// </summary>
        /// <param name="objOrder">Заказ</param>
        private void LoadWaybillForSuppl(ERP_Mercury.Common.COrder objOrder)
        {
            if (objOrder == null) { return; }
            try
            {
                System.String strErr = System.String.Empty;

                System.Guid Waybill_Guid = ERP_Mercury.Common.CWaybill.GetWaybillGuidForSuppl(m_objProfile, objOrder.ID, ref strErr);

                if (Waybill_Guid.CompareTo(System.Guid.Empty) == 0)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Для заказа нет накладной.", "Информация",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    LoadWaybill(Waybill_Guid);
                }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("LoadWaybillForSuppl. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }

        private void menuGoToWaybill_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewAgreementList.FocusedRowHandle < 0) { return; }
                if (GetSelectedOrder() == null) { return; }

                LoadWaybillForSuppl(GetSelectedOrder());
            }
            catch (System.Exception f)
            {
                SendMessageToLog("menuGoToWaybill_Click. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }

        private void menuTransformSupplToWaybill_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewAgreementList.FocusedRowHandle < 0) { return; }
                if (GetSelectedOrder() == null) { return; }

                CreateWaybillForSuppl(GetSelectedOrder());

            }
            catch (System.Exception f)
            {
                SendMessageToLog("menuTransformSupplToWaybill_Click. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }
        private void CreateWaybillForSuppl(ERP_Mercury.Common.COrder objOrder)
        {
            if (objOrder == null) { return; }
            
            
            try
            {
                System.String strErr = System.String.Empty;

                if (ERP_Mercury.Common.CWaybill.CanCreateWaybillFromSuppl(m_objProfile, objOrder.ID, ref strErr) == false)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(strErr, "Информация",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    DialogResult dlgRes = System.Windows.Forms.DialogResult.Cancel;
                    System.Guid Waybill_Guid = System.Guid.Empty;
                    System.Guid OrderState_Guid = System.Guid.Empty;
                    System.Boolean bNeedOpenWaybill = false;

                    using (frmCreateWaybillFromSuppl objfrmCreateWaybillFromSuppl = new frmCreateWaybillFromSuppl(m_objProfile))
                    {
                        objfrmCreateWaybillFromSuppl.OpenForm(objOrder.ID, (String.Format("№{0} от {1}", objOrder.Num, objOrder.BeginDate.ToShortDateString())),
                            objOrder.CustomerName, (String.Format("{0}  бс", objOrder.Num)), System.DateTime.Today);

                        dlgRes = objfrmCreateWaybillFromSuppl.ShowDialog();
                        Waybill_Guid = objfrmCreateWaybillFromSuppl.Waybill_Guid;
                        OrderState_Guid = objfrmCreateWaybillFromSuppl.OrderState_Guid;
                        bNeedOpenWaybill = objfrmCreateWaybillFromSuppl.NeedOpenWaybill;
                    }

                    if ((dlgRes == System.Windows.Forms.DialogResult.OK) && (Waybill_Guid.CompareTo(System.Guid.Empty) != 0))
                    {
                        if (objOrder.OrderState.ID.CompareTo(OrderState_Guid) != 0)
                        {
                            if ((m_objOrderStateList != null) && (m_objOrderStateList.Count > 0))
                            {
                                objOrder.OrderState = m_objOrderStateList.SingleOrDefault<ERP_Mercury.Common.COrderState>(x => x.ID.CompareTo(OrderState_Guid) == 0);
                                gridControlAgreementList.RefreshDataSource();
                            }
                        }

                        if (bNeedOpenWaybill == true)
                        {
                            LoadWaybill(Waybill_Guid);
                        }
                    }

                }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("CreateWaybillForSuppl. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }

        /// <summary>
        /// Перевод заказа в накладную
        /// </summary>
        /// <param name="objOrder">Заказ</param>
        private void TransformSupplToWaybill(ERP_Mercury.Common.COrder objOrder)
        {
            if (objOrder == null) { return; }

            if ((IsAvailableDR_MoveSupplToWaybillPayForm1 || IsAvailableDR_MoveSupplToWaybillPayForm2) == false)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("У Вас нет прав на операцию \"перевод заказа в накладную\".", "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                return;
            }
            
            try
            {
                System.String strErr = System.String.Empty;

                List<ERP_Mercury.Common.CWaybill> objWaybillList = ERP_Mercury.Common.CWaybill.GetWaybillList(m_objProfile, objOrder.ID, null, true, System.DateTime.Today,
                    System.DateTime.Today, System.Guid.Empty, System.Guid.Empty, System.Guid.Empty, System.Guid.Empty, ref strErr);
                if ((objWaybillList == null) || (objWaybillList.Count == 0))
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Запрос информации по заказу для накладной.\n\nТекст ошибки: " + strErr, "Ошибка",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                    return;
                }

                ERP_Mercury.Common.CWaybill objItem = objWaybillList[0];
                objItem.SupplID = objOrder.ID;
                objItem.DocNum = ( String.Format("{0}  бс", objOrder.Num ) );
                List<ERP_Mercury.Common.CSalesMan> objSalesManList = ERP_Mercury.Common.CSalesMan.GetSalesManListForDepart(m_objProfile, null, objItem.Depart.uuidID, ref strErr);
                if ((objSalesManList != null) && (objSalesManList.Count > 0))
                {
                    objItem.SalesMan = objSalesManList[0];
                }

                Cursor = Cursors.WaitCursor;

                ERP_Mercury.Common.CAddress.Init(m_objProfile, null, objItem.AddressDelivery, objItem.AddressDelivery.ID);
                objItem.WaybillItemList = ERP_Mercury.Common.CWaybillItem.GetWaybillTablePart(m_objProfile, objOrder.ID, ref strErr, true);

                if (frmItemEditor == null)
                {
                    frmItemEditor = new ctrlWaybillEditor(m_objProfile, m_objMenuItem, m_objCustomerList);
                    tableLayoutPanelWaybillEditor.Controls.Add(frmItemEditor, 0, 0);
                    frmItemEditor.Dock = DockStyle.Fill;
                    frmItemEditor.ChangeWaybillForCustomerProperties += this.OnChangeWaybillPropertie;
                }

                frmItemEditor.NewWaybillFromSuppl( objItem );

                tabControl.SelectedTabPage = tabPageWaybill;

            }
            catch (System.Exception f)
            {
                SendMessageToLog("CreateWaybillForSuppl. Текст ошибки: " + f.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            return;
        }
        private void menuTransformSupplToWaybillInManualMode_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewAgreementList.FocusedRowHandle < 0) { return; }
                ERP_Mercury.Common.COrder objOrder = GetSelectedOrder();
                if (objOrder == null) { return; }

                System.String strErr = System.String.Empty;

                if (ERP_Mercury.Common.CWaybill.CanCreateWaybillFromSuppl(m_objProfile, objOrder.ID, ref strErr) == false)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(strErr, "Информация",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    TransformSupplToWaybill(objOrder);
                }

            }
            catch (System.Exception f)
            {
                SendMessageToLog("menuTransformSupplToWaybillInManualMode_Click. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;

        }
        #endregion



    }

    public class OrderListEditor : PlugIn.IClassTypeView
    {
        public override void Run(UniXP.Common.MENUITEM objMenuItem, System.String strCaption)
        {
            frmOrderList obj = new frmOrderList( objMenuItem );
            obj.Text = strCaption;
            obj.MdiParent = objMenuItem.objProfile.m_objMDIManager.MdiParent;
            obj.Visible = true;
        }

    }

}

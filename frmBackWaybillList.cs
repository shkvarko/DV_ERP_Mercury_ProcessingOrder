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
using ERP_Mercury.Common;

namespace ERPMercuryProcessingOrder
{
    public partial class frmBackWaybillList : DevExpress.XtraEditors.XtraForm
    {
        #region Свойства
        private UniXP.Common.CProfile m_objProfile;
        private UniXP.Common.MENUITEM m_objMenuItem;
        private List<ERP_Mercury.Common.CCustomer> m_objCustomerList;
        private List<ERP_Mercury.Common.CBackWaybill> m_objList;
        private ERP_Mercury.Common.CBackWaybill m_objSelectedItem;
        private ctrlBackWaybillEditor frmItemEditor;
        private ctrlWaybillEditor m_frmParentDocument;
        private DevExpress.XtraGrid.Views.Base.ColumnView ColumnView
        {
            get { return gridControlList.MainView as DevExpress.XtraGrid.Views.Base.ColumnView; }
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

        private System.Boolean IsAvailableDR_ViewBackWaybillPayForm1; // "Возврат ф1 просмотр";
        private System.Boolean IsAvailableDR_ViewBackWaybillPayForm2; // "Возврат ф2 просмотр";

        private System.Boolean IsAvailableDR_EditBackWaybillPayForm1; // "Возврат ф1 редактировать";
        private System.Boolean IsAvailableDR_EditBackWaybillPayForm2; // "Возврат ф2 редактировать";

        #endregion

        #region Конструктор
        public frmBackWaybillList(UniXP.Common.MENUITEM objMenuItem)
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
            IsAvailableDR_ViewBackWaybillPayForm1 = objClientRights.GetState(ERPMercuryProcessingOrder.Consts.strDR_ViewBackWaybillPayForm1);
            IsAvailableDR_ViewBackWaybillPayForm2 = objClientRights.GetState(ERPMercuryProcessingOrder.Consts.strDR_ViewBackWaybillPayForm2);
            IsAvailableDR_EditBackWaybillPayForm1 = objClientRights.GetState(ERPMercuryProcessingOrder.Consts.strDR_EditBackWaybillPayForm1);
            IsAvailableDR_EditBackWaybillPayForm2 = objClientRights.GetState(ERPMercuryProcessingOrder.Consts.strDR_EditBackWaybillPayForm2);
            objClientRights = null;


            m_objList = null;
            m_objSelectedItem = null;
            m_bThreadFinishJob = false;
            m_objCustomerList = new List<ERP_Mercury.Common.CCustomer>();

            m_frmParentDocument = null;
            frmItemEditor = null;

            AddGridColumns();

            dtBeginDate.DateTime = System.DateTime.Today;
            dtEndDate.DateTime = System.DateTime.Today;
            tabControl.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            tabControlItemDetail.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
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

        private void OnChangeItemPropertie(Object sender, ChangeBackWaybillPropertieEventArgs e)
        {
            try
            {
                switch (e.ActionType)
                {
                    case ERP_Mercury.Common.enumActionSaveCancel.Cancel:
                        {
                            CancelChangesInBackWaybill();
                            break;
                        }
                    case ERP_Mercury.Common.enumActionSaveCancel.Save:
                        {
                            if (e.IsNewDocument == true)
                            {
                                m_objSelectedItem = null;
                                LoadList();
                            }
                            else
                            {
                                SaveChangesInItem();
                            }
                            break;
                        }
                }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("OnChangeItemPropertie. Текст ошибки: " + f.Message);
            }
            finally // очищаем занимаемые ресурсы
            {
            }

            return;
        }
        private void OnChangeParentDocPropertie(Object sender, ChangeWaybillPropertieEventArgs e)
        {
            try
            {
                CancelChangesInBackWaybill();
            }
            catch (System.Exception f)
            {
                SendMessageToLog("OnChangeParentDocPropertie. Текст ошибки: " + f.Message);
            }
            finally // очищаем занимаемые ресурсы
            {
            }

            return;
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

                gridControlList.MouseDoubleClick -= new MouseEventHandler(gridControlGrid_MouseDoubleClick);

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
                    barBtnAdd.Enabled = ( IsAvailableDR_EditBackWaybillPayForm1 || IsAvailableDR_EditBackWaybillPayForm2 );
                    barBtnEdit.Enabled = ( ( IsAvailableDR_EditBackWaybillPayForm1 || IsAvailableDR_EditBackWaybillPayForm2 ) && (gridViewList.FocusedRowHandle >= 0) );
                    barBtnCopy.Enabled = ( ( IsAvailableDR_EditBackWaybillPayForm1 || IsAvailableDR_EditBackWaybillPayForm2 ) && (gridViewList.FocusedRowHandle >= 0) );
                    barBtnDelete.Enabled = ((IsAvailableDR_EditBackWaybillPayForm1 || IsAvailableDR_EditBackWaybillPayForm2) && (gridViewList.FocusedRowHandle >= 0));
                    
                    if (IsAvailableDR_ViewBackWaybillPayForm1 || IsAvailableDR_ViewBackWaybillPayForm2)
                    {
                        gridControlList.MouseDoubleClick += new MouseEventHandler(gridControlGrid_MouseDoubleClick);
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
                if (frmItemEditor == null) { return bRet; }
                bRet = ((frmItemEditor.ThreadAddress != null) && (frmItemEditor.ThreadAddress.IsAlive == true));
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

                    if (System.Threading.WaitHandle.WaitAll((new System.Threading.ManualResetEvent[] { frmItemEditor.EventThreadStopped }), 100, true))
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

        private void frmList_FormClosing(object sender, FormClosingEventArgs e)
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

        #region Отмена изменений в редакторе записи
        private void CancelChangesInBackWaybill()
        {
            try
            {
                //((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
                //this.SuspendLayout();

                tabControl.SelectedTabPage = tabPageViewer;
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Отмена изменений. Текст ошибки: " + f.Message);
            }
            finally // очищаем занимаемые ресурсы
            {
                //((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
                //this.ResumeLayout(false);
            }

            return;
        }
        #endregion

        #region Изменения в редакторе записи подтверждены
        private void SaveChangesInItem()
        {
            try
            {
                //((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
                //this.SuspendLayout();

                tabControl.SelectedTabPage = tabPageViewer;
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Подтверждение изменений. Текст ошибки: " + f.Message);
            }
            finally // очищаем занимаемые ресурсы
            {
                //((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
                //this.ResumeLayout(false);
            }

            return;
        }
        #endregion

        #region Редактировать запись
        /// <summary>
        /// Загружает свойства записи в редактор
        /// </summary>
        /// <param name="objItem">запись журнала</param>
        private void EditItem(ERP_Mercury.Common.CBackWaybill objItem)
        {
            if (objItem == null) { return; }
            System.String strErr = System.String.Empty;

            try
            {
                m_objSelectedItem = objItem;

                m_objSelectedItem.WaybillItemList = ERP_Mercury.Common.CBackWaybillItem.GetWaybillTablePart( m_objProfile, m_objSelectedItem.ID, 
                    System.DateTime.MinValue, System.DateTime.MinValue, ref strErr, false );

                if (m_objSelectedItem.SalesMan == null)
                {
                    List<ERP_Mercury.Common.CSalesMan> objSalesManList = ERP_Mercury.Common.CSalesMan.GetSalesManListForDepart(m_objProfile,
                        null, m_objSelectedItem.Depart.uuidID, ref strErr);
                    if ((objSalesManList != null) && (objSalesManList.Count > 0))
                    {
                        m_objSelectedItem.SalesMan = objSalesManList[0];
                    }
                    objSalesManList = null;
                }


                if (frmItemEditor == null)
                {
                    frmItemEditor = new ctrlBackWaybillEditor( m_objMenuItem );
                    tableLayoutPanelItemEditor.Controls.Add(frmItemEditor, 0, 0);
                    frmItemEditor.Dock = DockStyle.Fill;
                    frmItemEditor.ChangeBackWaybillForCustomerProperties += this.OnChangeItemPropertie;
                }

                frmItemEditor.EditBackWaybill(objItem);

                tabControl.SelectedTabPage = tabPageEditor;
                tabControlItemDetail.SelectedTabPage = tabPageItemEditor;

            }
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка редактирования записи. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }
        private void barBtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                EditItem(GetSelectedItem());

            }//try
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка редактирования записи. Текст ошибки: " + f.Message);
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
        /// <returns>ссылка на накладную на возврат товара</returns>
        private ERP_Mercury.Common.CBackWaybill GetSelectedItem()
        {
            ERP_Mercury.Common.CBackWaybill objRet = null;
            try
            {
                if ((((DevExpress.XtraGrid.Views.Grid.GridView)gridControlList.MainView).RowCount > 0) &&
                    (((DevExpress.XtraGrid.Views.Grid.GridView)gridControlList.MainView).FocusedRowHandle >= 0))
                {
                    System.Guid uuidID = (System.Guid)(((DevExpress.XtraGrid.Views.Grid.GridView)gridControlList.MainView)).GetFocusedRowCellValue("ID");

                    objRet = m_objList.Single<ERP_Mercury.Common.CBackWaybill>(x => x.ID.CompareTo(uuidID) == 0);
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

        #endregion

        #region Просмотр документа в редакторе
        /// <summary>
        /// Загружает свойства записи в редактор для просмотра
        /// </summary>
        /// <param name="objItem">запись журнала</param>
        private void ViewItem(ERP_Mercury.Common.CBackWaybill objItem)
        {
            if (objItem == null) { return; }

            if ((IsAvailableDR_ViewBackWaybillPayForm1 || IsAvailableDR_ViewBackWaybillPayForm2) == false)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "У Вас недостаточно прав для операции \"просмотр накладной на возврат товара от клиента\"", "Внимание!",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                return;
            }
            
            System.String strErr = System.String.Empty;

            try
            {
                m_objSelectedItem = objItem;

                //m_objSelectedItem.WaybillItemList = ERP_Mercury.Common.CBackWaybillItem.GetWaybillTablePart(m_objProfile, m_objSelectedItem.ID,
                //    System.DateTime.MinValue, System.DateTime.MinValue, ref strErr, false);

                if (m_objSelectedItem.SalesMan == null)
                {
                    List<ERP_Mercury.Common.CSalesMan> objSalesManList = ERP_Mercury.Common.CSalesMan.GetSalesManListForDepart(m_objProfile,
                        null, m_objSelectedItem.Depart.uuidID, ref strErr);
                    if ((objSalesManList != null) && (objSalesManList.Count > 0))
                    {
                        m_objSelectedItem.SalesMan = objSalesManList[0];
                    }
                    objSalesManList = null;
                }


                if (frmItemEditor == null)
                {
                    frmItemEditor = new ctrlBackWaybillEditor(m_objMenuItem);
                    tableLayoutPanelItemEditor.Controls.Add(frmItemEditor, 0, 0);
                    frmItemEditor.Dock = DockStyle.Fill;
                    frmItemEditor.ChangeBackWaybillForCustomerProperties += this.OnChangeItemPropertie;
                }

                frmItemEditor.ViewBackWaybill(objItem);

                tabControl.SelectedTabPage = tabPageEditor;
                tabControlItemDetail.SelectedTabPage = tabPageItemEditor;

            }
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка просмотра документа. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }

        private void gridControlGrid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                ViewItem(GetSelectedItem());

            }//try
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка просмотра документа. Текст ошибки: " + f.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            return;
        }

        #endregion

        #region Удалить запись в списке
        /// <summary>
        /// Удаляет запись в списке
        /// </summary>
        /// <param name="objItem">запись</param>
        private void DeleteItem(ERP_Mercury.Common.CBackWaybill objItem)
        {
            if (objItem == null) { return; }

            if ((IsAvailableDR_EditBackWaybillPayForm1 || IsAvailableDR_EditBackWaybillPayForm2) == false)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "У Вас недостаточно прав для операции \"аннулирование возврата от клиента\"", "Внимание!",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                return;
            }

            try
            {
                System.Int32 iFocusedRowHandle = gridViewList.FocusedRowHandle;
                if (DevExpress.XtraEditors.XtraMessageBox.Show(String.Format("Подтвердите, пожалуйста, аннулирование накладной.\n\n№{0} от {1}\nКлиент: {2}", objItem.DocNum, objItem.BeginDate.ToShortDateString(), objItem.Customer.FullName), "Подтверждение",
                    System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == DialogResult.No) { return; }

                System.String strErr = System.String.Empty;
                System.Guid NewBackWaybillState_Guid = System.Guid.Empty;

                if (CBackWaybill.DeleteBackWaybill(m_objProfile, objItem.ID, ref NewBackWaybillState_Guid, ref strErr) == true)
                {
                    if (NewBackWaybillState_Guid.CompareTo(System.Guid.Empty) != 0)
                    {
                        List<CBackWaybillState> objBackWaybillStateList = CBackWaybillState.GetBackWaybillStateList(m_objProfile, ref strErr);
                        if (objBackWaybillStateList != null)
                        {
                            CBackWaybillState objNewBackWaybillState = objBackWaybillStateList.SingleOrDefault<CBackWaybillState>(x => x.ID.CompareTo(NewBackWaybillState_Guid) == 0);
                            if (objNewBackWaybillState != null)
                            {
                                objItem.BackWaybillState = objNewBackWaybillState;
                            }
                        }
                        objBackWaybillStateList = null;
                    }

                    
                    gridControlList.RefreshDataSource();
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(strErr, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Удаление записи. Текст ошибки: " + f.Message);
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
                DeleteItem(GetSelectedItem());
            }//try
            catch (System.Exception f)
            {
                SendMessageToLog("Удаление записи. Текст ошибки: " + f.Message);
            }
            finally
            {
            }

            return;
        }
        private void menuDeleteWaybill_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteItem(GetSelectedItem());
            }//try
            catch (System.Exception f)
            {
                SendMessageToLog("Удаление записи. Текст ошибки: " + f.Message);
            }
            finally
            {
            }

            return;
        }

        #endregion

        #region Новая запись
        /// <summary>
        /// Открывает редактор записи в режиме "Новый запись"
        /// </summary>
        private void NewItem()
        {
            try
            {
                //((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
                //this.SuspendLayout();

                if ((IsAvailableDR_EditBackWaybillPayForm1 || IsAvailableDR_EditBackWaybillPayForm2) == false)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                        "У Вас недостаточно прав для операции \"возврат товара от клиента\"", "Внимание!",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                    return;
                }

                // причина возврата
                DialogResult objRet = System.Windows.Forms.DialogResult.None;
                CWaybillBackReason objWaybillBackReason = null;

                CCompany objSelectedCompany = null;
                CStock objSelectedStock = null;
                CCustomer objSelectedCustomer = null;
                CChildDepart objSelectedChildDepart = null;
                CPaymentType objSelectedPaymentType = null;
                CCurrency objSelectedCurrency = null;
                System.DateTime dtBeginWaybillDate = System.DateTime.Today;
                System.DateTime dtEndWaybillDate = System.DateTime.Today;

                using (frmWaybillBackReason objFrmWaybillBackReason = new frmWaybillBackReason(m_objMenuItem))
                {
                    objFrmWaybillBackReason.SetSrcForBackWaybillByWaybillList( this.m_objCustomerList );
                    objRet = objFrmWaybillBackReason.ShowDialog();
                    objWaybillBackReason = objFrmWaybillBackReason.WaybillBackReason;
                    objSelectedCustomer = objFrmWaybillBackReason.Customer;
                    objSelectedChildDepart = objFrmWaybillBackReason.ChildDepart;
                    objSelectedStock = objFrmWaybillBackReason.Stock;
                    objSelectedCompany = objFrmWaybillBackReason.Stock.Company;
                    objSelectedPaymentType = objFrmWaybillBackReason.PaymentType;
                    objSelectedCurrency = objFrmWaybillBackReason.Currency;
                    dtBeginWaybillDate = objFrmWaybillBackReason.WaybillBeginDate;
                    dtEndWaybillDate = objFrmWaybillBackReason.WaybillEndDate;
                }

                if ((objRet == System.Windows.Forms.DialogResult.OK) && (objWaybillBackReason != null))
                {
                    if (frmItemEditor == null)
                    {
                        frmItemEditor = new ctrlBackWaybillEditor(m_objMenuItem);
                        tableLayoutPanelItemEditor.Controls.Add(frmItemEditor, 0, 0);
                        frmItemEditor.Dock = DockStyle.Fill;
                        frmItemEditor.ChangeBackWaybillForCustomerProperties += OnChangeItemPropertie;
                    }

                    frmItemEditor.NewBackWaybillFromWaybillList(objSelectedCustomer,
                        objSelectedChildDepart, objSelectedCompany,
                        objSelectedStock, objSelectedPaymentType, objSelectedCurrency,
                        dtBeginWaybillDate, dtEndWaybillDate, objWaybillBackReason);

                    tabControl.SelectedTabPage = tabPageEditor;
                }

            }
            catch (System.Exception f)
            {
                SendMessageToLog("Создание новой записи. Текст ошибки: " + f.Message);
            }
            finally
            {
                //((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
                //this.ResumeLayout(false);
                tabControl.SelectedTabPage = tabPageEditor;
            }
            return;
        }
        private void barBtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                NewItem();
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Создание новой записи. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }

        #endregion

        #region Загрузка журнала

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

        private void LoadStockForCompany()
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

                System.Boolean CanViewPaymentType2 = m_objProfile.GetClientsRight().GetState(ERP_Mercury.Global.Consts.strDR_ViewWaybillPayForm2);

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
                cboxCompany.Properties.Items.Add(new ERP_Mercury.Common.CCompany());
                List<ERP_Mercury.Common.CCompany> objCompanyList = ERP_Mercury.Common.CCompany.GetCompanyList(m_objProfile, null);
                cboxCompany.Properties.Items.AddRange(objCompanyList);

                if ((CompanyAcronymForPaymentType1.Length > 0) && (cboxCompany.Properties.Items.Count > 0))
                {
                    cboxCompany.SelectedItem = cboxCompany.Properties.Items.Cast<CCompany>().SingleOrDefault<CCompany>(x => x.Abbr == CompanyAcronymForPaymentType1);
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

                cboxPaymentType.Properties.Items.Clear();

                if (CanViewPaymentType2 == true)
                {
                    cboxPaymentType.Properties.Items.Add(new ERP_Mercury.Common.CPaymentType(System.Guid.Empty, ""));
                    cboxPaymentType.Properties.Items.AddRange(ERP_Mercury.Common.CPaymentType.GetPaymentTypeList(m_objProfile, null, System.Guid.Empty));

                    if (cboxPaymentType.Properties.Items.Count > 0)
                    {
                        cboxPaymentType.SelectedItem = cboxPaymentType.Properties.Items.Cast<CPaymentType>().SingleOrDefault<CPaymentType>(x => x.Payment_Id == DefPaymentTypeId);
                    }
                }
                else
                {
                    List<CPaymentType> PaymentTypeList = ERP_Mercury.Common.CPaymentType.GetPaymentTypeList(m_objProfile, null, System.Guid.Empty);
                    if (PaymentTypeList != null)
                    {
                        if (PaymentTypeList.SingleOrDefault<CPaymentType>(x => x.Payment_Id == DefPaymentTypeId) != null)
                        {
                            cboxPaymentType.Properties.Items.Add(PaymentTypeList.SingleOrDefault<CPaymentType>(x => x.Payment_Id == DefPaymentTypeId));
                            cboxPaymentType.SelectedItem = cboxPaymentType.Properties.Items.Cast<CPaymentType>().SingleOrDefault<CPaymentType>(x => x.Payment_Id == DefPaymentTypeId);
                        }

                        if (BlockOtherPaymentType == false)
                        {
                            cboxPaymentType.Properties.Items.Add(new ERP_Mercury.Common.CPaymentType(System.Guid.Empty, ""));
                            cboxPaymentType.Properties.Items.Add(PaymentTypeList.SingleOrDefault<CPaymentType>(x => x.Payment_Id != DefPaymentTypeId));
                        }

                    }
                }

                cboxCustomer.Properties.Items.Clear();
                cboxCustomer.Properties.Items.Add(new ERP_Mercury.Common.CCustomer());

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
            AddGridColumn(ColumnView, "ID", "Идентификатор");
            AddGridColumn(ColumnView, "DocNum", "Номер");
            AddGridColumn(ColumnView, "BeginDate", "Дата");
            AddGridColumn(ColumnView, "WaybillBackReasonName", "Причина возврата");
  //          AddGridColumn(ColumnView, "ShipDate", "Дата отгрузки");
            AddGridColumn(ColumnView, "WaybillShipModeName", "Вид отгрузки");
            AddGridColumn(ColumnView, "CustomerName", "Клиент");
            AddGridColumn(ColumnView, "ChildDepartCode", "Код дочернего");
            AddGridColumn(ColumnView, "CompanyAcronym", "Компания");
            AddGridColumn(ColumnView, "StockName", "Склад");
            AddGridColumn(ColumnView, "DepartCode", "Подразделение");
            AddGridColumn(ColumnView, "PaymentTypeName", "Форма оплаты");

            AddGridColumn(ColumnView, "Quantity", "Возврат, шт.");
            AddGridColumn(ColumnView, "SumWaybill", "Возврат, руб.");

            foreach (DevExpress.XtraGrid.Columns.GridColumn objColumn in ColumnView.Columns)
            {
                objColumn.OptionsColumn.AllowEdit = false;
                //objColumn.OptionsColumn.AllowFocus = false;
                objColumn.OptionsColumn.ReadOnly = true;
                if ((objColumn.FieldName == "CustomerName") || (objColumn.FieldName == "DocNum") || (objColumn.FieldName == "WaybillBackReasonName"))
                {
                    objColumn.BestFit();
                    //objColumn.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                }
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
        /// Загружает журнал
        /// </summary>
        /// <returns>true - удачное завершение операции; false - ошибка</returns>
        public System.Boolean LoadList()
        {
            System.Boolean bRet = false;
            this.Cursor = Cursors.WaitCursor;
            System.String strErr = System.String.Empty;
            try
            {
                System.Int32 iRowHandle = gridViewList.FocusedRowHandle;

                gridControlList.DataSource = null;
                System.Guid uuidCompanyId = ((cboxCompany.SelectedItem == null) ? System.Guid.Empty : ((ERP_Mercury.Common.CCompany)cboxCompany.SelectedItem).ID);
                System.Guid uuidCustomerId = (((cboxCustomer.SelectedItem == null) || (System.Convert.ToString(cboxCustomer.SelectedItem) == "") || (cboxCustomer.Text == strWaitCustomer)) ? System.Guid.Empty : ((ERP_Mercury.Common.CCustomer)cboxCustomer.SelectedItem).ID);
                System.Guid uuidStockId = ((cboxStock.SelectedItem == null) ? System.Guid.Empty : ((ERP_Mercury.Common.CStock)cboxStock.SelectedItem).ID);
                System.Guid uuidPaymentTypeId = ((cboxPaymentType.SelectedItem == null) ? System.Guid.Empty : ((ERP_Mercury.Common.CPaymentType)cboxPaymentType.SelectedItem).ID);

                m_objList = ERP_Mercury.Common.CBackWaybill.GetBackWaybillList(m_objProfile, System.Guid.Empty, System.Guid.Empty, dtBeginDate.DateTime,
                    dtEndDate.DateTime, uuidCompanyId, uuidStockId, uuidPaymentTypeId, uuidCustomerId, ref strErr, false);

                if (m_objList != null)
                {
                    gridControlList.DataSource = m_objList;
                }

                if ((gridViewList.RowCount > 0) && (iRowHandle >= 0) && (gridViewList.RowCount > iRowHandle))
                {
                    gridViewList.FocusedRowHandle = iRowHandle;
                }

                bRet = true;
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Загрузка журнала. Текст ошибки: " + f.Message);
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
                LoadList();
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
                if (e.KeyCode == Keys.Enter) { LoadList(); }
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

        #region Свойства записи
        private void gridViewList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                ShowItemProperties(GetSelectedItem());

                barBtnAdd.Enabled = ( IsAvailableDR_EditBackWaybillPayForm1 || IsAvailableDR_EditBackWaybillPayForm2 );
                barBtnEdit.Enabled = ( ( IsAvailableDR_EditBackWaybillPayForm1 || IsAvailableDR_EditBackWaybillPayForm2 ) && (e.FocusedRowHandle >= 0) );
                barBtnCopy.Enabled = ( ( IsAvailableDR_EditBackWaybillPayForm1 || IsAvailableDR_EditBackWaybillPayForm2 ) && (e.FocusedRowHandle >= 0) );
                barBtnDelete.Enabled = ((IsAvailableDR_EditBackWaybillPayForm1 || IsAvailableDR_EditBackWaybillPayForm2) && (e.FocusedRowHandle >= 0));
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Отображение свойств записи. Текст ошибки: " + f.Message);
            }
            return;
        }
        private void gridViewAgreementList_RowCountChanged(object sender, EventArgs e)
        {
            try
            {
                ShowItemProperties(GetSelectedItem());

                barBtnAdd.Enabled = (IsAvailableDR_EditBackWaybillPayForm1 || IsAvailableDR_EditBackWaybillPayForm2);
                barBtnEdit.Enabled = ( ( IsAvailableDR_EditBackWaybillPayForm1 || IsAvailableDR_EditBackWaybillPayForm2 ) && (gridViewList.FocusedRowHandle >= 0));
                barBtnCopy.Enabled = ( ( IsAvailableDR_EditBackWaybillPayForm1 || IsAvailableDR_EditBackWaybillPayForm2 ) && (gridViewList.FocusedRowHandle >= 0) );
                barBtnDelete.Enabled = ((IsAvailableDR_EditBackWaybillPayForm1 || IsAvailableDR_EditBackWaybillPayForm2) && (gridViewList.FocusedRowHandle >= 0));
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Отображение свойств записи. Текст ошибки: " + f.Message);
            }
            return;
        }

        /// <summary>
        /// Отображает свойства записи
        /// </summary>
        /// <param name="objItem">запись журанала</param>
        private void ShowItemProperties(ERP_Mercury.Common.CBackWaybill objItem)
        {
            try
            {
                this.tableLayoutPanel3.SuspendLayout();

                txtCustomer.Text = "";
                txtDocNum.Text = "";
                txtDepart.Text = "";
                txtStock.Text = "";
                txtPaymentType.Text = "";
                txtWaybillStateName.Text = "";
                txtChildDepartCode.Text = "";
                txtDescrpn.Text = "";
                txtWaybillBackReason.Text = "";

                txtQuantityReturn.Text = "";
                txtSupplNum.Text = "";
                txtSumReturn.Text = "";

                if (objItem != null)
                {
                    txtCustomer.Text = objItem.CustomerName;
                    txtDocNum.Text = String.Format("{0} от {1}", objItem.DocNum, objItem.BeginDate.ToShortDateString());
                    txtDepart.Text = (String.Format("{0} {1}", objItem.DepartCode, objItem.SalesManName));
                    txtStock.Text = String.Format("{0} [{1}]", objItem.StockName, objItem.CompanyAcronym);
                    txtPaymentType.Text = objItem.PaymentTypeName;
                    txtWaybillStateName.Text = objItem.BackWaybillStateName;
                    txtChildDepartCode.Text = objItem.ChildDepartCode;
                    txtWaybillBackReason.Text = objItem.WaybillBackReasonName;
                    txtDescrpn.Text = objItem.Description;

                    txtSupplNum.Text = String.Format("{0} от {1}", objItem.DocNum, objItem.BeginDate.ToShortDateString());
                    txtQuantityReturn.Text = System.String.Format("{0:### ### ##0}", objItem.Quantity);

                    txtSumReturn.Text = System.String.Format("{0:### ### ##0.00}", objItem.SumWaybill);

                }
                else
                {
                }

                this.tableLayoutPanel3.ResumeLayout(false);
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Отображение свойств записи журнала. Текст ошибки: " + f.Message);
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
                gridViewList.RestoreLayoutFromRegistry(strReestrPath + gridViewList.Name);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка загрузки настроек журнала.\n\nТекст ошибки : " + f.Message, "Внимание",
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
                gridViewList.SaveLayoutToRegistry(strReestrPath + gridViewList.Name);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка записи настроек журнала.\n\nТекст ошибки : " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // очищаем занимаемые ресурсы
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
                if (e.Column.FieldName == "BackWaybillStateName")
                {
                    ERP_Mercury.Common.enPDASupplState SupplState = (ERP_Mercury.Common.enPDASupplState)gridViewList.GetRowCellValue(e.RowHandle, gridViewList.Columns["BackWaybillStateId"]);
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

                    r.Inflate(-(rImg.Width + 5), 0);
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

        #region Закрытие формы
        private void frmList_FormClosed(object sender, FormClosedEventArgs e)
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
        private void frmList_Shown(object sender, EventArgs e)
        {
            try
            {
                RestoreLayoutFromRegistry();

                LoadComboBox();

                LoadList();

                tabControl.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;

                StartThreadWithLoadData();
            }
            catch (System.Exception f)
            {
                SendMessageToLog("frmList_Shown. Текст ошибки: " + f.Message);
            }
            return;
        }
        #endregion

        #region Родительский документ
        /// <summary>
        /// Отображает содержимое документа, на основании которого был сформирован текущий документ
        /// </summary>
        /// <param name="objItem">Сылка на выбранную в журнале запись</param>
        private void ShowParentDocument(ERP_Mercury.Common.CBackWaybill objItem)
        {
            if (objItem == null) { return; }

            try
            {
                System.String strErr = System.String.Empty;

                ERP_Mercury.Common.CWaybill objParentDoc = ERP_Mercury.Common.CWaybill.GetWaybill( m_objProfile, objItem.WaybillID, ref strErr);

                if (objParentDoc == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Для накладной не найдена накладная на отгрузку!", "Внимание!",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return;
                }

                ERP_Mercury.Common.CAddress.Init(m_objProfile, null, objParentDoc.AddressDelivery, objParentDoc.AddressDelivery.ID);
                objParentDoc.WaybillItemList = ERP_Mercury.Common.CWaybillItem.GetWaybillTablePart(m_objProfile, objParentDoc.ID, ref strErr);
                
                if (m_frmParentDocument == null)
                {
                    m_frmParentDocument = new ctrlWaybillEditor( m_objProfile, m_objMenuItem, m_objCustomerList );
                    tableLayoutPanelItemParentDocument.Controls.Add(m_frmParentDocument, 0, 0);
                    m_frmParentDocument.Dock = DockStyle.Fill;
                    m_frmParentDocument.ChangeWaybillForCustomerProperties += OnChangeParentDocPropertie;
                }

                m_frmParentDocument.EditWaybill(objParentDoc, false);

                tabControl.SelectedTabPage = tabPageEditor;
                tabControlItemDetail.SelectedTabPage = tabPageItemParentDocument;

            }
            catch (System.Exception f)
            {
                SendMessageToLog("ShowParentDocument. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }
        private void menuGoToSuppl_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                ShowParentDocument(GetSelectedItem());

            }//try
            catch (System.Exception f)
            {
                SendMessageToLog("menuGoToSuppl_Click. Текст ошибки: " + f.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            return;
        }
        #endregion

        #region Постановка накладной на возврат товара на приход
        /// <summary>
        /// Постановка товара на приход
        /// </summary>
        /// <param name="objBackWaybill"></param>
        private void SetBackWaybillToStock(CBackWaybill objBackWaybill)
        {
            if (objBackWaybill == null) { return; }
            if ((IsAvailableDR_ViewBackWaybillPayForm1 || IsAvailableDR_ViewBackWaybillPayForm2) == false)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "У Вас недостаточно прав для операции \"постановка возврата на приход\"", "Внимание!",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                return;
            }
            try
            {
                DialogResult objRet = System.Windows.Forms.DialogResult.None;
                using (frmSetBackWaybillToStock objFrmSetBackWaybillToStock = new frmSetBackWaybillToStock() )
                {
                    objFrmSetBackWaybillToStock.SetParamsForSetBackWaybillToStock(m_objProfile, objBackWaybill);
                    objRet = objFrmSetBackWaybillToStock.ShowDialog();
                }

                if(objRet == System.Windows.Forms.DialogResult.OK)
                {
                    gridControlList.RefreshDataSource();
                }

            }
            catch (System.Exception f)
            {
                SendMessageToLog("SetBackWaybillToStock. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }
        private void menuSetBackWaybillToStock_Click(object sender, EventArgs e)
        {
            try
            {
                SetBackWaybillToStock(GetSelectedItem());
            }//try
            catch (System.Exception f)
            {
                SendMessageToLog("Постановка твоара на приход. Текст ошибки: " + f.Message);
            }
            finally
            {
            }

            return;

        }
        #endregion

        #region Контекстное меню
        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            try
            {
                if (gridViewList.RowCount == 0)
                {
                    menuDeleteWaybill.Enabled = false;
                    menuSetBackWaybillToStock.Enabled = false;
                }
                else
                {
                    CBackWaybill objSelectedItem = GetSelectedItem();
                    if ((objSelectedItem == null) || ( objSelectedItem.BackWaybillState == null ))
                    {
                        menuDeleteWaybill.Enabled = false;
                        menuSetBackWaybillToStock.Enabled = false;
                    }
                    else
                    {
                        menuDeleteWaybill.Enabled = ( ( objSelectedItem.BackWaybillState.BackWaybillStateId == 0 ) && 
                            ( IsAvailableDR_EditBackWaybillPayForm1 || IsAvailableDR_EditBackWaybillPayForm2 ) );
                        menuSetBackWaybillToStock.Enabled = ((objSelectedItem.BackWaybillState.BackWaybillStateId == 0) &&
                            (IsAvailableDR_EditBackWaybillPayForm1 || IsAvailableDR_EditBackWaybillPayForm2));
                    }
                }

            }
            catch (System.Exception f)
            {
                SendMessageToLog("contextMenuStrip_Opening. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }
        #endregion

    }

    public class ViewBackWaybillList : PlugIn.IClassTypeView
    {
        public override void Run(UniXP.Common.MENUITEM objMenuItem, System.String strCaption)
        {
            frmBackWaybillList obj = new frmBackWaybillList(objMenuItem);
            obj.Text = strCaption;
            obj.MdiParent = objMenuItem.objProfile.m_objMDIManager.MdiParent;
            obj.Visible = true;
        }
    }

}

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
using DevExpress.XtraGrid.Export;
using DevExpress.XtraExport;
using System.Data.OleDb;
using OfficeOpenXml;

namespace ERPMercuryProcessingOrder
{
    public partial class ctrlBackWaybillEditor : UserControl
    {
        #region Свойства
        private UniXP.Common.CProfile m_objProfile;
        private UniXP.Common.MENUITEM m_objMenuItem;
        private List<CSrcBackWaybillItem> m_objSrcBackWaybillItemList;

        private CBackWaybill m_objSelectedWaybill;

        private System.Guid m_uuidSrcWaybillID;
        private System.Guid m_uuidSrcStockID;
        private System.Guid m_uuidSrcCustomerID;
        private System.Guid m_uuidSrcPaymentID;
        private System.DateTime m_dtSrcBeginDate;
        private System.DateTime m_dtSrcEndDate;


        private System.Boolean m_bIsChanged;

        private System.Boolean m_bDisableEvents;
        private System.Boolean m_bNewObject;
        private System.Boolean m_bIsReadOnly;
        //private System.String m_strXLSImportFilePath;
        //private System.Int32 m_iXLSSheetImport;
        //private List<System.String> m_SheetList;
        //private System.Double m_CurratePricing;

        // потоки
        private System.Threading.Thread thrAddress;
        public System.Threading.Thread ThreadAddress
        {
            get { return thrAddress; }
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
        public delegate void LoadPartsListDelegate();
        public LoadPartsListDelegate m_LoadPartsListDelegate;

        public delegate void SendMessageToLogDelegate(System.String strMessage);
        public SendMessageToLogDelegate m_SendMessageToLogDelegate;

        public delegate void SetProductListToFormDelegate(List<CSrcBackWaybillItem> objProductNewList);
        public SetProductListToFormDelegate m_SetProductListToFormDelegate;

        private const System.Int32 iThreadSleepTime = 1000;
        private System.Boolean m_bThreadFinishJob;

        private const System.Double dblNDS = 20;
        private const System.String m_strReportsDirectory = "templates";
        private const System.Int32 m_iDebtorInfoPanelHeight = 117;
        private const System.Int32 m_iDebtorInfoPanelIndex = 1;
        #endregion

        #region События
        // Создаем закрытое поле, ссылающееся на заголовок списка делегатов
        private EventHandler<ChangeBackWaybillPropertieEventArgs> m_ChangeBackWaybillForCustomerProperties;
        // Создаем в классе член-событие
        public event EventHandler<ChangeBackWaybillPropertieEventArgs> ChangeBackWaybillForCustomerProperties
        {
            add
            {
                // берем закрытую блокировку и добавляем обработчик
                // (передаваемый по значению) в список делегатов
                m_ChangeBackWaybillForCustomerProperties += value;
            }
            remove
            {
                // берем закрытую блокировку и удаляем обработчик
                // (передаваемый по значению) из списка делегатов
                m_ChangeBackWaybillForCustomerProperties -= value;
            }
        }
        /// <summary>
        /// Инициирует событие и уведомляет о нем зарегистрированные объекты
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnChangeBackWaybillForCustomerProperties(ChangeBackWaybillPropertieEventArgs e)
        {
            // Сохраняем поле делегата во временном поле для обеспечение безопасности потока
            EventHandler<ChangeBackWaybillPropertieEventArgs> temp = m_ChangeBackWaybillForCustomerProperties;
            // Если есть зарегистрированные объектв, уведомляем их
            if (temp != null) temp(this, e);
        }
        public void SimulateChangeBackWaybillProperties(CBackWaybill objBackWaybill, enumActionSaveCancel enActionType,
            System.Boolean bIsNewBackWaybill, System.Guid uuidWaybillCurrentStateGuid)
        {
            // Создаем объект, хранящий информацию, которую нужно передать
            // объектам, получающим уведомление о событии
            ChangeBackWaybillPropertieEventArgs e = new ChangeBackWaybillPropertieEventArgs(objBackWaybill, enActionType, bIsNewBackWaybill, uuidWaybillCurrentStateGuid);

            // Вызываем виртуальный метод, уведомляющий наш объект о возникновении события
            // Если нет типа, переопределяющего этот метод, наш объект уведомит все объекты, 
            // подписавшиеся на уведомление о событии
            OnChangeBackWaybillForCustomerProperties(e);
        }
        #endregion

        #region Конструктор
        public ctrlBackWaybillEditor(UniXP.Common.MENUITEM objMenuItem)
        {
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ru-RU");
            ci.NumberFormat.CurrencyDecimalSeparator = ".";
            ci.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;

            m_objProfile = objMenuItem.objProfile;
            m_objMenuItem = objMenuItem;

            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.AssemblyResolve += new ResolveEventHandler(MyResolveEventHandler);

            InitializeComponent();


            m_bIsChanged = false;
            m_bDisableEvents = false;
            m_bNewObject = false;

            m_uuidSrcWaybillID = System.Guid.Empty;
            m_uuidSrcStockID = System.Guid.Empty;
            m_uuidSrcCustomerID = System.Guid.Empty;
            m_uuidSrcPaymentID = System.Guid.Empty;
            m_dtSrcBeginDate = System.DateTime.MinValue;
            m_dtSrcEndDate = System.DateTime.MinValue;

            m_objSelectedWaybill = null;
            m_objSrcBackWaybillItemList = null;
            
            //m_strXLSImportFilePath = "";
            //m_iXLSSheetImport = 0;

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

        #region Журнал сообщений
        /// <summary>
        /// Выводит сообщение в журнал событий приложения
        /// </summary>
        /// <param name="strMessage">сообщение</param>
        private void SendMessageToLog(System.String strMessage)
        {
            try
            {
                m_objMenuItem.SimulateNewMessage(strMessage);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( String.Format("SendMessageToLog.\n Текст ошибки: {0}", f.Message) , "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        #endregion

        #region Индикация изменений
        /// <summary>
        /// Проверяет содержимое элементов управления
        /// </summary>
        private System.Boolean ValidateProperties()
        {
            System.Boolean bRet = true;
            try
            {
                Customer.Properties.Appearance.BackColor = ((Customer.SelectedItem == null) ? System.Drawing.Color.Tomato : System.Drawing.Color.White);
                WaybillState.Properties.Appearance.BackColor = ((WaybillState.SelectedItem == null) ? System.Drawing.Color.Tomato : System.Drawing.Color.White);
                PaymentType.Properties.Appearance.BackColor = ((PaymentType.SelectedItem == null) ? System.Drawing.Color.Tomato : System.Drawing.Color.White);
                Depart.Properties.Appearance.BackColor = ((Depart.SelectedItem == null) ? System.Drawing.Color.Tomato : System.Drawing.Color.White);
                SalesMan.Properties.Appearance.BackColor = ((SalesMan.SelectedItem == null) ? System.Drawing.Color.Tomato : System.Drawing.Color.White);
                Stock.Properties.Appearance.BackColor = ((Stock.SelectedItem == null) ? System.Drawing.Color.Tomato : System.Drawing.Color.White);

                BeginDate.Properties.Appearance.BackColor = ((BeginDate.DateTime == System.DateTime.MinValue) ? System.Drawing.Color.Tomato : System.Drawing.Color.White);

                if (Customer.SelectedItem == null) { bRet = false; }
                if (WaybillState.SelectedItem == null) { bRet = false; }
                if (PaymentType.SelectedItem == null) { bRet = false; }
                if (SalesMan.SelectedItem == null) { bRet = false; }
                if (Depart.SelectedItem == null) { bRet = false; }
                if (BeginDate.DateTime == System.DateTime.MinValue) { bRet = false; }
                if (dataSet.Tables["OrderItems"].Rows.Count == 0) { bRet = false; }

            }
            catch (System.Exception f)
            {
                SendMessageToLog("ValidateProperties. Текст ошибки: " + f.Message);
            }

            return bRet;
        }
        private void SetPropertiesModified(System.Boolean bModified)
        {
            try
            {
                m_bIsChanged = bModified;
                btnSave.Enabled = (m_bIsChanged && (ValidateProperties() == true));
                btnCancel.Enabled = m_bIsChanged;
                //if (m_bIsChanged == true)
                //{
                //    SimulateChangeWaybillProperties(m_objSelectedWaybill, enumActionSaveCancel.Unkown, m_bNewObject, System.Guid.Empty);
                //}
            }
            catch (System.Exception f)
            {
                SendMessageToLog("SetPropertiesModified. Текст ошибки: " + f.Message);
            }
            finally
            {
            }

            return;
        }
        private void OrderItems_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            try
            {
                if (m_bDisableEvents == true) { return; }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("OrderItems_RowChanged. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }

        private void cboxOrderPropertie_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_bDisableEvents == true) { return; }

                SetPropertiesModified(true);
            }//try
            catch (System.Exception f)
            {
                SendMessageToLog(String.Format("Ошибка изменения свойств {0}. Текст ошибки: {1}", ((DevExpress.XtraEditors.ComboBoxEdit)sender).ToolTip, f.Message));
            }
            finally
            {
            }

            return;
        }

        private void txtOrderPropertie_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            try
            {
                if (m_bDisableEvents == true) { return; }
                if (e.NewValue != null)
                {
                    SetPropertiesModified(true);
                    if ((sender.GetType().Name == "TextEdit") &&
                        (((DevExpress.XtraEditors.TextEdit)sender).Properties.ReadOnly == false))
                    {
                        System.String strValue = (System.String)e.NewValue;
                        ((DevExpress.XtraEditors.TextEdit)sender).Properties.Appearance.BackColor = (strValue == "") ? System.Drawing.Color.Tomato : System.Drawing.Color.White;
                    }
                }
            }//try
            catch (System.Exception f)
            {
                SendMessageToLog("txtOrderPropertie_EditValueChanging. Текст ошибки: " + f.Message);
            }
            finally
            {
            }

            return;
        }
        private void gridViewProductList_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (m_bDisableEvents == true) { return; }

                SetPropertiesModified(true);

            }//try
            catch (System.Exception f)
            {
                SendMessageToLog("gridViewProductList_CellValueChanged. Текст ошибки: " + f.Message);
            }
            finally
            {
            }

            return;
        }
        private void gridViewProductList_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (m_bDisableEvents == true) { return; }

                SetPropertiesModified(true);
            }//try
            catch (System.Exception f)
            {
                SendMessageToLog("gridViewProductList_CellValueChanging. Текст ошибки: " + f.Message);
            }
            finally
            {
            }

            return;
        }

        private void gridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (m_bDisableEvents == true) { return; }

                if ((e.Column == colWaybItem_Guid) && (m_objSrcBackWaybillItemList != null) && (e.Value != null))
                {

                    CSrcBackWaybillItem objItem = null;
                    try
                    {
                        objItem = m_objSrcBackWaybillItemList.Cast<CSrcBackWaybillItem>().Single<CSrcBackWaybillItem>(x => x.WaybItemID.CompareTo((System.Guid)e.Value) == 0);

                        gridView.SetRowCellValue(e.RowHandle, colProductID, objItem.Product.ID);
                        gridView.SetRowCellValue(e.RowHandle, colMeasureID, objItem.Measure.ID);
                        gridView.SetRowCellValue(e.RowHandle, colBackWaybItems_MeasureName, objItem.Measure.ShortName);
                        gridView.SetRowCellValue(e.RowHandle, colBackWaybItems_QuantitySrc, objItem.LeavQuantity);
                        gridView.SetRowCellValue(e.RowHandle, colQuantity, objItem.LeavQuantity);
                        gridView.SetRowCellValue(e.RowHandle, colBackWaybItems_PartsArticle, objItem.Product.Article);
                        gridView.SetRowCellValue(e.RowHandle, colBackWaybItems_PartsName, objItem.Product.Name);
                        gridView.SetRowCellValue(e.RowHandle, colPriceImporter, objItem.PriceImporter);
                        gridView.SetRowCellValue(e.RowHandle, colPrice, objItem.Price);
                        gridView.SetRowCellValue(e.RowHandle, colPriceInAccountingCurrency, objItem.PriceInAccountingCurrency);

                        gridView.UpdateCurrentRow();

                    }
                    catch
                    {
                    }
                    finally
                    {
                        objItem = null;
                    }
                }
                else if ((e.Column == colQuantity) && (e.Value != null) && (gridView.GetRowCellValue(e.RowHandle, colProductID) != null) &&
                    (gridView.GetRowCellValue(e.RowHandle, colBackWaybItems_QuantitySrc) != System.DBNull.Value))
                {
                    System.Decimal dclOrderedQty = System.Convert.ToDecimal(e.Value);
                    System.Decimal dclQuantitySrc = System.Convert.ToDecimal(gridView.GetRowCellValue(e.RowHandle, colBackWaybItems_QuantitySrc));

                    if (dclOrderedQty > dclQuantitySrc)
                    {
                        gridView.SetRowCellValue(e.RowHandle, colQuantity, dclQuantitySrc);
                    }
                    
                    gridView.UpdateCurrentRow();

                }
                SetPropertiesModified(true);
            }
            catch (System.Exception f)
            {
                SendMessageToLog("gridView_CellValueChanged. Текст ошибки: " + f.Message);
            }
            finally
            {

            }
            return;
        }

        private void gridView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            try
            {
                e.Info.DisplayText = ((e.RowHandle < 0) ? "" : ("№ " + System.String.Format("{0:### ### ##0}", (e.RowHandle + 1))));
            }
            catch (System.Exception f)
            {
                SendMessageToLog("gridView_CustomDrawRowIndicator. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }

        private void repositoryItemLookUpEditProduct_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            try
            {
                if (m_bDisableEvents == true) { return; }

                if ((e.AcceptValue == true) && (e.Value != null) && (e.Value.GetType().Name != "DBNull"))
                {
                    gridView.SetRowCellValue(gridView.FocusedRowHandle, colWaybItem_Guid, (System.Guid)e.Value);
                }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("repositoryItemLookUpEditProduct_CloseUp. Текст ошибки: " + f.Message);
            }
            finally
            {
                ValidateProperties();
            }
            return;
        }

        private void repositoryItemCalcEditOrderedQuantity_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            try
            {
                if (m_bDisableEvents == true) { return; }

                if (e.NewValue != null)
                {
                    System.Decimal dblQuantity = System.Convert.ToDecimal(e.NewValue);
                    System.Decimal dblQuantitySrc = System.Convert.ToDecimal(gridView.GetRowCellValue(gridView.FocusedRowHandle, colBackWaybItems_QuantitySrc));

                    if (System.Convert.ToDecimal(e.NewValue) > dblQuantitySrc)
                    {
                        e.NewValue = dblQuantitySrc;
                        OrderItems.Rows[gridView.FocusedRowHandle][Quantity] = dblQuantitySrc;
                        OrderItems.AcceptChanges();

                        //  gridView.SetRowCellValue(gridView.FocusedRowHandle, colOrderedQuantity, System.Convert.ToDecimal(dclOrderedQty));
                    }
                    //gridView.SetRowCellValue(gridView.FocusedRowHandle, colQuantityReserved, System.Convert.ToDecimal(dclOrderedQty));
                    //gridView.UpdateCurrentRow();
                }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("repositoryItemCalcEditOrderedQuantity_EditValueChanged. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }

        private void gridView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if ((gridView.GetRowCellValue(e.RowHandle, colBackWaybItems_QuantitySrc) != null) &&
                    (gridView.GetRowCellValue(e.RowHandle, colQuantity) != null))
                {
                    System.Double dblQuantitySrc = System.Convert.ToDouble(gridView.GetRowCellValue(e.RowHandle, colBackWaybItems_QuantitySrc));
                    System.Double dblQuantity = System.Convert.ToDouble(gridView.GetRowCellValue(e.RowHandle, colQuantity));
                    if (dblQuantitySrc != dblQuantity)
                    {
                        if ((e.Column == colQuantity) || (e.Column == colBackWaybItems_QuantitySrc))
                        {
                            Rectangle r = e.Bounds;
                            e.Appearance.DrawString(e.Cache, e.DisplayText, r);

                            e.Handled = true;
                        }
                    }

                }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("gridView_CustomDrawCell. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }
        #endregion

        #region Выпадающие списки
        /// <summary>
        /// Обновление выпадающих списков
        /// </summary>
        /// <returns>true - все списки успешно обновлены; false - ошибка</returns>
        private System.Boolean LoadComboBoxItems()
        {
            System.Boolean bRet = false;
            System.String strErr = System.String.Empty;
            try
            {

                Customer.Properties.Items.Clear();
                ChildDepart.Properties.Items.Clear();
                Depart.Properties.Items.Clear();
                cboxProductTradeMark.Properties.Items.Clear();
                cboxProductType.Properties.Items.Clear();

                // Склады
                Stock.Properties.Items.Clear();

                // Состояние накладной
                WaybillState.Properties.Items.Clear();
                WaybillState.Properties.Items.AddRange(CWaybillState.GetWaybillStateList(m_objProfile, ref strErr));

                // Вид отгрузки
                WaybillShipMode.Properties.Items.Clear();
                WaybillShipMode.Properties.Items.AddRange(CWaybillShipMode.GetWaybillShipModeList(m_objProfile, ref strErr));

                // Форма оплаты
                PaymentType.Properties.Items.Clear();

                // Подразделение
                Depart.Properties.Items.Clear();
                Depart.Properties.Items.AddRange(CDepart.GetDepartList(m_objProfile, null));

                // Товары
                dataSet.Tables["Product"].Clear();
                repositoryItemLookUpEditProduct.DataSource = dataSet.Tables["Product"];

                bRet = true;
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка обновления выпадающих списков. Текст ошибки: " + f.Message);
            }
            finally
            {
            }

            return bRet;
        }
        /// <summary>
        /// Загружает список дочерних для клиента
        /// </summary>
        /// <param name="uuidCustomerId">идентификатор клиента</param>
        private void LoadChildDeprtForCustomer(System.Guid uuidCustomerId)
        {
            try
            {
                ChildDepart.SelectedItem = null;
                ChildDepart.Properties.Items.Clear();

                if (uuidCustomerId.CompareTo(System.Guid.Empty) != 0)
                {
                    ChildDepart.Properties.Items.AddRange(CChildDepart.GetChildDepartList(m_objProfile, null, uuidCustomerId));
                }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("LoadChildDeprtForCustomer. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }
        /// <summary>
        /// Загружает значения в выпадающий список с торговыми представителями
        /// </summary>
        /// <param name="objDepart">Подразделение</param>
        private void LoadSalesManListForDepart(CDepart objDepart)
        {
            try
            {
                SalesMan.Properties.Items.Clear();
                if (objDepart != null)
                {
                    SalesMan.Properties.Items.Add(objDepart.SalesMan);
                    SalesMan.SelectedItem = SalesMan.Properties.Items[0];
                }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("LoadSalesManListForDepart. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }
        /// <summary>
        /// Фильтрует выпадающий список с товарами
        /// </summary>
        private void SetFilterForPartsCombo()
        {
            try
            {
                if (gridView.RowCount > 0)
                {
                    if (DevExpress.XtraEditors.XtraMessageBox.Show("В результате применения фильтра будут удалены товары в заказе,\nне соответствующие заданным условиям.\nУстановить фильтр?", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        cboxProductTradeMark.SelectedValueChanged -= new EventHandler(cboxOrderPropertie_SelectedValueChanged);
                        cboxProductType.SelectedValueChanged -= new EventHandler(cboxOrderPropertie_SelectedValueChanged);

                        cboxProductTradeMark.SelectedItem = null;
                        cboxProductType.SelectedItem = null;

                        cboxProductTradeMark.SelectedValueChanged += new EventHandler(cboxOrderPropertie_SelectedValueChanged);
                        cboxProductType.SelectedValueChanged += new EventHandler(cboxOrderPropertie_SelectedValueChanged);

                        return;
                    }
                }
                System.Guid TradeMarkId = ((cboxProductTradeMark.SelectedItem == null) ? System.Guid.Empty : ((CProductTradeMark)cboxProductTradeMark.SelectedItem).ID);
                System.Guid PartTypeId = ((cboxProductType.SelectedItem == null) ? System.Guid.Empty : ((CProductType)cboxProductType.SelectedItem).ID);

                //tableLayoutPanelBackground.SuspendLayout();

                List<CSrcBackWaybillItem> FilteredProductList = m_objSrcBackWaybillItemList;

                if (TradeMarkId != System.Guid.Empty)
                {
                    FilteredProductList = FilteredProductList.Where<CSrcBackWaybillItem>(x => x.Product.ProductTradeMark.ID == TradeMarkId).ToList<CSrcBackWaybillItem>();
                }

                if (PartTypeId != System.Guid.Empty)
                {
                    FilteredProductList = FilteredProductList.Where<CSrcBackWaybillItem>(x => x.Product.ProductType.ID == PartTypeId).ToList<CSrcBackWaybillItem>();
                }

                //if (FilteredProductList.Count != m_objPartsList.Count)
                //{
                dataSet.Tables["Product"].Clear();

                System.Data.DataRow newRowProduct = null;
                dataSet.Tables["Product"].Clear();
                foreach (CSrcBackWaybillItem objItem in FilteredProductList)
                {
                    newRowProduct = dataSet.Tables["Product"].NewRow();

                    newRowProduct["Product_WaybItem_Guid"] = objItem.WaybItemID;
                    newRowProduct["Product_ProductID"] = objItem.Product.ID;
                    newRowProduct["Product_ProductFullName"] = objItem.ProductFullName;
                    newRowProduct["Product_WaybItem_Quantity"] = objItem.Quantity;
                    newRowProduct["Product_WaybItem_BasePrice"] = objItem.PriceImporter;
                    newRowProduct["Product_WaybItem_Price"] = objItem.Price;
                    newRowProduct["Product_MeasureID"] = objItem.Measure.ID;
                    newRowProduct["Product_MeasureName"] = objItem.Measure.ShortName;
                    newRowProduct["Product_WaybItem_PriceInAccountingCurrency"] = objItem.PriceInAccountingCurrency;
                    newRowProduct["Product_WaybItem_LeavQuantity"] = objItem.LeavQuantity;
                    newRowProduct["Product_WaybillNum"] = objItem.WaybillNum;
                    newRowProduct["Product_WaybillDate"] = objItem.WaybillDate;

                    dataSet.Tables["Product"].Rows.Add(newRowProduct);
                }
                newRowProduct = null;
                dataSet.Tables["Product"].AcceptChanges();
                //}

                // теперь нужно проверить содержимое заказа на предмет товараов, не попадающих под условия фильтра
                if ((gridView.RowCount > 0) && ((TradeMarkId != System.Guid.Empty) || (PartTypeId != System.Guid.Empty)))
                {
                    System.Guid PartsGuid = System.Guid.Empty;
                    CSrcBackWaybillItem objSrcBackWaybillItem = null;
                    System.Boolean bOk = true;
                    for (System.Int32 i = (gridView.RowCount - 1); i >= 0; i--)
                    {
                        PartsGuid = (System.Guid)(gridView.GetDataRow(i)["ProductID"]);
                        bOk = true;
                        try
                        {
                            objSrcBackWaybillItem = m_objSrcBackWaybillItemList.SingleOrDefault<CSrcBackWaybillItem>(x => x.Product.ID == PartsGuid);
                        }
                        catch
                        {
                            objSrcBackWaybillItem = null;
                        }
                        if (objSrcBackWaybillItem != null)
                        {
                            // проверяем на соответствие марке
                            if (TradeMarkId != System.Guid.Empty)
                            {
                                bOk = (objSrcBackWaybillItem.Product.ProductTradeMark.ID == TradeMarkId);
                            }
                            if ((bOk == true) && (PartTypeId != System.Guid.Empty))
                            {
                                bOk = (objSrcBackWaybillItem.Product.ProductTradeMark.ID == PartTypeId);
                            }
                        }
                        if (bOk == false)
                        {
                            gridView.DeleteRow(i);
                        }
                    }

                    objSrcBackWaybillItem = null;

                }

            }
            catch (System.Exception f)
            {
                SendMessageToLog("SetFilterForPartsCombo. Текст ошибки: " + f.Message);
            }
            finally
            {
                //this.tableLayoutPanelBackground.ResumeLayout( false );
            }
            return;
        }

        #endregion

        #region Потоки
        /// <summary>
        /// Создает форму со списком товара
        /// </summary>
        /// <param name="objSrcBackWaybillItemList">список позиций к возврату</param>
        private void SetProductListToForm(List<CSrcBackWaybillItem> objSrcBackWaybillItemList)
        {
            try
            {
                m_objSrcBackWaybillItemList = objSrcBackWaybillItemList;

                System.Data.DataRow newRowProduct = null;
                dataSet.Tables["OrderItems"].Clear();
                dataSet.Tables["Product"].Clear();
                foreach (CSrcBackWaybillItem objItem in m_objSrcBackWaybillItemList)
                {
                    newRowProduct = dataSet.Tables["Product"].NewRow();

                    newRowProduct["Product_ProductID"] = objItem.Product.ID;
                    newRowProduct["Product_ProductFullName"] = objItem.ProductFullName;
                    newRowProduct["Product_WaybItem_Quantity"] = objItem.Quantity;
                    newRowProduct["Product_WaybItem_BasePrice"] = objItem.PriceImporter;
                    newRowProduct["Product_WaybItem_Price"] = objItem.Price;
                    newRowProduct["Product_MeasureID"] = objItem.Measure.ID;
                    newRowProduct["Product_MeasureName"] = objItem.Measure.ShortName;
                    newRowProduct["Product_WaybItem_PriceInAccountingCurrency"] = objItem.PriceInAccountingCurrency;

                    newRowProduct["Product_WaybItem_Guid"] = objItem.WaybItemID;
                    newRowProduct["Product_WaybItem_LeavQuantity"] = objItem.LeavQuantity;
                    newRowProduct["Product_WaybillNum"] = objItem.WaybillNum;
                    newRowProduct["Product_WaybillDate"] = objItem.WaybillDate;

                    dataSet.Tables["Product"].Rows.Add(newRowProduct);
                }
                newRowProduct = null;
                dataSet.Tables["Product"].AcceptChanges();


                //repItemComboProduct.Items.Clear();
                //repItemComboProduct.Items.AddRange(m_objPartsList);
                //colProduct.ColumnEdit = repItemComboProduct;
            }
            catch (System.Exception f)
            {
                SendMessageToLog("SetProductListToForm. Текст ошибки: " + f.Message);
            }
            finally
            {
            }

            return;
        }

        /// <summary>
        /// загружает список товаров и список новых товаров
        /// </summary>
        private void LoadPartssList()
        {
            try
            {
                // товары
                System.String strErr = System.String.Empty;

                m_objSrcBackWaybillItemList = CSrcBackWaybillItem.GetSrcBackWaybillItemList( m_objProfile, m_uuidSrcWaybillID, m_uuidSrcCustomerID, m_uuidSrcStockID, m_uuidSrcPaymentID, m_dtSrcBeginDate, m_dtSrcEndDate, ref strErr);
                if (m_objSrcBackWaybillItemList != null)
                {
                    this.Invoke(m_SetProductListToFormDelegate, new Object[] { m_objSrcBackWaybillItemList });
                }
            }
            catch (System.Exception f)
            {
                this.Invoke(m_SendMessageToLogDelegate, new Object[] { ("Ошибка обновления списка товаров. Текст ошибки: " + f.Message) });
            }
            finally
            {
                EventStopThread.Set();
            }

            return;
        }

        public void StartThreadWithLoadData()
        {
            try
            {
                // инициализируем события
                this.m_EventStopThread = new System.Threading.ManualResetEvent(false);
                this.m_EventThreadStopped = new System.Threading.ManualResetEvent(false);

                // инициализируем делегаты
                m_LoadPartsListDelegate = new LoadPartsListDelegate(LoadPartssList);
                m_SetProductListToFormDelegate = new SetProductListToFormDelegate(SetProductListToForm);
                m_SendMessageToLogDelegate = new SendMessageToLogDelegate(SendMessageToLog);

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
                System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ru-RU");
                ci.NumberFormat.CurrencyDecimalSeparator = ".";
                ci.NumberFormat.NumberDecimalSeparator = ".";

                // делаем событиям reset
                this.m_EventStopThread.Reset();
                this.m_EventThreadStopped.Reset();

                this.thrAddress = new System.Threading.Thread(WorkerThreadFunction);
                this.thrAddress.CurrentCulture = ci;
                this.thrAddress.Start();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("StartThread().\n\nТекст ошибки: " + f.Message, "Ошибка",
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
                LoadPartssList();

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

        private System.Boolean bIsThreadsActive()
        {
            System.Boolean bRet = false;
            try
            {
                bRet = (
                    ((ThreadAddress != null) && (ThreadAddress.IsAlive == true))
                    );
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
                if (bIsThreadsActive() == true)
                {
                    if ((ThreadAddress != null) && (ThreadAddress.IsAlive == true))
                    {
                        EventStopThread.Set();
                    }
                }
                while (bIsThreadsActive() == true)
                {
                    if (System.Threading.WaitHandle.WaitAll((new System.Threading.ManualResetEvent[] { EventThreadStopped }), 100, true))
                    {
                        break;
                    }
                    Application.DoEvents();
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("bIsThreadsActive.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }

        #endregion

        #region Режим просмотра/редактирования
        /// <summary>
        /// Устанавливает режим просмотра/редактирования
        /// </summary>
        /// <param name="bSet">true - режим просмотра; false - режим редактирования</param>
        private void SetModeReadOnly(System.Boolean bSet)
        {
            try
            {
                BeginDate.Properties.ReadOnly = bSet;
                ShipDate.Properties.ReadOnly = bSet;
                txtDescription.Properties.ReadOnly = bSet;
                WaybilllNum.Properties.ReadOnly = bSet;

                Customer.Properties.ReadOnly = bSet;
                ChildDepart.Properties.ReadOnly = bSet;
                PaymentType.Properties.ReadOnly = bSet;
                WaybillState.Properties.ReadOnly = bSet;
                WaybillShipMode.Properties.ReadOnly = bSet;
                Depart.Properties.ReadOnly = bSet;
                SalesMan.Properties.ReadOnly = bSet;
                Stock.Properties.ReadOnly = bSet;

                cboxProductTradeMark.Properties.ReadOnly = bSet;
                cboxProductType.Properties.ReadOnly = bSet;

                checkSetOrderInQueue.Enabled = !bSet;

                gridView.OptionsBehavior.Editable = !bSet;
                controlNavigator.Enabled = !bSet;

                mitemImport.Enabled = !bSet;

                m_bIsReadOnly = bSet;

                btnEdit.Enabled = bSet;
            }
            catch (System.Exception f)
            {
                SendMessageToLog("SetModeReadOnly. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }
        private void SetModeNewBackWaybillFromWaybill()
        {
            try
            {
                BeginDate.Properties.ReadOnly = true;
                ShipDate.Properties.ReadOnly = true;
                txtDescription.Properties.ReadOnly = false;
                WaybilllNum.Properties.ReadOnly = false;

                Customer.Properties.ReadOnly = true;
                ChildDepart.Properties.ReadOnly = true;
                PaymentType.Properties.ReadOnly = true;
                WaybillState.Properties.ReadOnly = true;
                WaybillShipMode.Properties.ReadOnly = false;
                Depart.Properties.ReadOnly = true;
                SalesMan.Properties.ReadOnly = true;
                Stock.Properties.ReadOnly = true;

                cboxProductTradeMark.Properties.ReadOnly = true;
                cboxProductType.Properties.ReadOnly = true;

                checkSetOrderInQueue.Enabled = false;

                gridView.OptionsBehavior.Editable = false;
                controlNavigator.Enabled = true;
                controlNavigator.Buttons.Append.Enabled = false;
                controlNavigator.Buttons.Edit.Enabled = false;
                controlNavigator.Buttons.CancelEdit.Enabled = false;
                controlNavigator.Buttons.EndEdit.Enabled = false;

                mitemImport.Enabled = false;

                m_bIsReadOnly = false;

                btnEdit.Enabled = false;
            }
            catch (System.Exception f)
            {
                SendMessageToLog("SetModeNewBackWaybillFromWaybill. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Refresh();

                SetModeReadOnly(false);
                btnEdit.Enabled = false;
                SetPropertiesModified(true);
            }
            catch (System.Exception f)
            {
                SendMessageToLog("SetModeReadOnly. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }
        #endregion
        
        #region Редактировать накладную
        /// <summary>
        /// очистка содержимого элементов управления
        /// </summary>
        private void ClearControls()
        {
            try
            {
                BeginDate.EditValue = null;
                ShipDate.EditValue = null;
                txtDescription.Text = "";
                WaybilllNum.Text = "";

                Customer.SelectedItem = null;
                ChildDepart.SelectedItem = null;
                PaymentType.SelectedItem = null;
                WaybillShipMode.SelectedItem = null;
                WaybillState.SelectedItem = null;
                Depart.SelectedItem = null;
                SalesMan.SelectedItem = null;
                Stock.SelectedItem = null;
                cboxProductTradeMark.SelectedItem = null;
                cboxProductType.SelectedItem = null;

                dataSet.Tables["OrderItems"].Clear();
            }
            catch (System.Exception f)
            {
                SendMessageToLog("ClearControls. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }
  
        /// <summary>
        /// Загружает свойства накладной для редактирования
        /// </summary>
        /// <param name="objBackWaybill">накладная</param>
        public void EditBackWaybill(CBackWaybill objBackWaybill)
        {
            if (objBackWaybill == null) { return; }

            m_bDisableEvents = true;
            m_bNewObject = false;

            try
            {
                m_objSelectedWaybill = objBackWaybill;

                ClearControls();

                // шапка накладной
                LoadComboBoxAndSetValues(m_objSelectedWaybill);

                // заполнение выпадающего списка и табличной части
                // список необходим даже в том случае, если документ открывается только на просмотр
                // наименование товара в табличной части подтягивается по ссылке из выпадающего списка
                if (m_objSrcBackWaybillItemList == null)
                {
                    m_objSrcBackWaybillItemList = new List<CSrcBackWaybillItem>();
                }
                else
                {
                    m_objSrcBackWaybillItemList.Clear();
                }
                
                CSrcBackWaybillItem objSrcBackWaybillItem = null;
                dataSet.Tables["OrderItems"].Clear();
                System.Data.DataRow newRowOrderItems = null;

                foreach (CBackWaybillItem objItem in m_objSelectedWaybill.WaybillItemList)
                {
                    objSrcBackWaybillItem = new CSrcBackWaybillItem();

                    objSrcBackWaybillItem.WaybItemID = objItem.WaybItemID;
                    objSrcBackWaybillItem.WaybillNum = System.String.Empty;
                    objSrcBackWaybillItem.Product = objItem.Product;
                    objSrcBackWaybillItem.Measure = objItem.Measure;

                    objSrcBackWaybillItem.Quantity = 0;
                    objSrcBackWaybillItem.LeavQuantity = 0;

                    objSrcBackWaybillItem.NDSPercent = objItem.NDSPercent;
                    objSrcBackWaybillItem.PriceImporter = objItem.PriceImporter;
                    objSrcBackWaybillItem.Price = objItem.Price;
                    objSrcBackWaybillItem.DiscountPercent = objItem.DiscountPercent;
                    objSrcBackWaybillItem.PriceWithDiscount = objItem.PriceWithDiscount;
                    objSrcBackWaybillItem.PriceInAccountingCurrency = objItem.PriceInAccountingCurrency;
                    objSrcBackWaybillItem.PriceWithDiscountInAccountingCurrency = objItem.PriceWithDiscountInAccountingCurrency;

                    m_objSrcBackWaybillItemList.Add(objSrcBackWaybillItem);

                    newRowOrderItems = dataSet.Tables["OrderItems"].NewRow();
                    newRowOrderItems["BackWaybItem_Guid"] = objItem.ID;
                    newRowOrderItems["BackWaybItem_Id"] = objItem.Ib_ID;
                    newRowOrderItems["WaybItem_Guid"] = objItem.WaybItemID;
                    newRowOrderItems["ProductID"] = objItem.Product.ID;
                    newRowOrderItems["MeasureID"] = objItem.Measure.ID;
                    newRowOrderItems["Quantity"] = objItem.Quantity;
                    newRowOrderItems["PriceImporter"] = objItem.PriceImporter;
                    newRowOrderItems["Price"] = objItem.Price;
                    newRowOrderItems["PriceInAccountingCurrency"] = objItem.PriceInAccountingCurrency;
                    newRowOrderItems["BackWaybItems_MeasureName"] = objItem.Measure.ShortName;
                    newRowOrderItems["BackWaybItems_PartsName"] = objItem.Product.Name;
                    newRowOrderItems["BackWaybItems_PartsArticle"] = objItem.Product.Article;
                    newRowOrderItems["BackWaybItems_QuantitySrc"] = objItem.Quantity;

                    dataSet.Tables["OrderItems"].Rows.Add(newRowOrderItems);
                }

                SetProductListToForm(m_objSrcBackWaybillItemList);
                objSrcBackWaybillItem = null;
                newRowOrderItems = null;

                dataSet.Tables["OrderItems"].AcceptChanges();

                SetPropertiesModified(false);
                btnCancel.Enabled = true;
                btnCancel.Focus();
                ValidateProperties();

                SetModeReadOnly(true);
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка редактирования документа. Текст ошибки: " + f.Message);
            }
            finally
            {
                m_bDisableEvents = false;

                Cursor = Cursors.Default;
            }
            return;
        }
        #endregion

        #region Новая накладная

        private void LoadComboBoxAndSetValues(CBackWaybill objBackWaybill)
        {
            if (objBackWaybill == null) { return; }
            try
            {
                m_uuidSrcCustomerID = ((objBackWaybill.Customer != null) ? objBackWaybill.Customer.ID : System.Guid.Empty);
                m_uuidSrcStockID = ((objBackWaybill.Stock != null) ? objBackWaybill.Stock.ID : System.Guid.Empty);
                m_uuidSrcPaymentID = ((objBackWaybill.PaymentType != null) ? objBackWaybill.PaymentType.ID : System.Guid.Empty);

                Stock.Properties.Items.Clear();
                if (m_objSelectedWaybill.Stock != null)
                {
                    Stock.Properties.Items.Add(m_objSelectedWaybill.Stock);
                    Stock.SelectedItem = Stock.Properties.Items[0];
                }

                Customer.Properties.Items.Clear();
                if (m_objSelectedWaybill.Customer != null)
                {
                    Customer.Properties.Items.Add(m_objSelectedWaybill.Customer);
                    Customer.SelectedItem = Customer.Properties.Items[0];
                    LoadChildDeprtForCustomer(m_objSelectedWaybill.Customer.ID);
                    ChildDepart.SelectedItem = (m_objSelectedWaybill.ChildDepart == null) ? null : ChildDepart.Properties.Items.Cast<CChildDepart>().SingleOrDefault<CChildDepart>(x => x.ID.CompareTo(m_objSelectedWaybill.ChildDepart.ID) == 0);
                }

                PaymentType.Properties.Items.Clear();
                if (m_objSelectedWaybill.PaymentType != null)
                {
                    PaymentType.Properties.Items.Add(m_objSelectedWaybill.PaymentType);
                    PaymentType.SelectedItem = PaymentType.Properties.Items[0];
                }

                BeginDate.DateTime = m_objSelectedWaybill.BeginDate;
                ShipDate.DateTime = m_objSelectedWaybill.ShipDate;
                txtDescription.Text = m_objSelectedWaybill.Description;
                WaybilllNum.Text = m_objSelectedWaybill.DocNum;

                WaybillShipMode.SelectedItem = (m_objSelectedWaybill.WaybillShipMode == null) ? null : WaybillShipMode.Properties.Items.Cast<CWaybillShipMode>().SingleOrDefault<CWaybillShipMode>(x => x.ID.CompareTo(m_objSelectedWaybill.WaybillShipMode.ID) == 0);
                WaybillState.SelectedItem = (m_objSelectedWaybill.WaybillState == null) ? null : WaybillState.Properties.Items.Cast<CWaybillState>().SingleOrDefault<CWaybillState>(x => x.ID.CompareTo(m_objSelectedWaybill.WaybillState.ID) == 0);
                Depart.SelectedItem = (m_objSelectedWaybill.Depart == null) ? null : Depart.Properties.Items.Cast<CDepart>().SingleOrDefault<CDepart>(x => x.uuidID.CompareTo(m_objSelectedWaybill.Depart.uuidID) == 0);
                LoadSalesManListForDepart((CDepart)Depart.SelectedItem);

                SalesMan.SelectedItem = (m_objSelectedWaybill.SalesMan == null) ? null : SalesMan.Properties.Items.Cast<CSalesMan>().SingleOrDefault<CSalesMan>(x => x.uuidID.CompareTo(m_objSelectedWaybill.SalesMan.uuidID) == 0);

                cboxProductTradeMark.Properties.Items.Add(new CProductTradeMark() { ID = System.Guid.Empty, Name = "" });
                cboxProductTradeMark.Properties.Items.AddRange(CProductTradeMark.GetProductTradeMarkList(m_objProfile, null));
                cboxProductType.Properties.Items.Add(new CProductType() { ID = System.Guid.Empty, Name = "" });
                cboxProductType.Properties.Items.AddRange(CProductType.GetProductTypeList(m_objProfile, null));

                Stock.Properties.ReadOnly = true;
                PaymentType.Properties.ReadOnly = true;
                Customer.Properties.ReadOnly = true;
                ChildDepart.Properties.ReadOnly = true;

            }
            catch (System.Exception f)
            {
                SendMessageToLog("LoadComboBox. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }

        /// <summary>
        /// Новая накладная
        /// </summary>
        /// <param name="Waybill_Guid">УИ накладной на отгрузку</param>
        /// <param name="objCustomer"></param>
        /// <param name="objCompany"></param>
        /// <param name="objStock"></param>
        /// <param name="objPaymentType"></param>
        /// <param name="dtWaybillBeginDate"></param>
        /// <param name="dtWaybillEndDate"></param>
        public void NewBackWaybill( System.Guid Waybill_Guid, CCustomer objCustomer, CCompany objCompany, CStock objStock, CPaymentType objPaymentType,
            System.DateTime dtWaybillBeginDate, System.DateTime dtWaybillEndDate)
        {
            try
            {
                m_bNewObject = true;
                m_bDisableEvents = true;

                m_dtSrcBeginDate = dtWaybillBeginDate;
                m_dtSrcEndDate = dtWaybillEndDate;

                m_objSelectedWaybill = new CBackWaybill()
                {
                    BeginDate = System.DateTime.Today,
                    WaybillItemList = new List<CBackWaybillItem>(),
                    Customer = objCustomer,
                    PaymentType = objPaymentType, 
                    Stock = objStock,
                    Company = objCompany
                };

                ClearControls();

                LoadComboBoxAndSetValues(m_objSelectedWaybill);

                // выпадающий список для табличной части
                StartThreadWithLoadData();

                btnEdit.Enabled = false;
                btnCancel.Enabled = true;
                btnCancel.Focus();

                SetModeReadOnly(false);

                SetPropertiesModified(true);
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка создания накладной. Текст ошибки: " + f.Message);
            }
            finally
            {
                tableLayoutPanelBackground.ResumeLayout(false);
                m_bDisableEvents = false;
            }
            return;
        }

        #endregion

        #region Отмена
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Cancel();
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка отмены изменений. Текст ошибки: " + f.Message);
            }
            finally
            {
            }

            return;
        }
        /// <summary>
        /// Отмена внесенных изменений
        /// </summary>
        private void Cancel()
        {
            try
            {
                SimulateChangeBackWaybillProperties(m_objSelectedWaybill, enumActionSaveCancel.Cancel, m_bNewObject, System.Guid.Empty);
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка отмены изменений. Текст ошибки: " + f.Message);
            }
            finally
            {
            }

            return;

        }
        #endregion

        #region запись в DBGrid

        private void gridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down)
                {
                    if (gridView.FocusedRowHandle >= 0)
                    {
                        if (IsValidDataInRow(gridView.FocusedRowHandle) == true)
                        {
                            if (gridView.FocusedRowHandle == (gridView.RowCount - 1))
                            {
                                gridView.AddNewRow();
                                gridView.FocusedColumn = colProductID;
                                e.Handled = true;
                            }
                        }
                    }
                    else
                    {
                        gridView.AddNewRow();
                        gridView.FocusedColumn = colProductID;
                        e.Handled = true;
                    }
                }
                else if (e.KeyCode == Keys.Up)
                {
                    if (gridView.GetDataRow(gridView.FocusedRowHandle)["ProductID"] == System.DBNull.Value)
                    {
                        gridView.CancelUpdateCurrentRow();
                        e.Handled = true;
                    }
                }
                else if (e.KeyCode == Keys.Delete && e.Control)
                {
                    gridView.DeleteSelectedRows();
                }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("gridView_KeyDown. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;

        }

        private void gridView_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            try
            {
                DataRow row = gridView.GetDataRow(e.RowHandle);

                //row["OrderItemsID"] = System.Guid.NewGuid();
                //row["Quantity"] = 1;
                //row["PriceImporter"] = 0;
                //row["Price"] = 0;
                //row["DiscountPercent"] = spinEditDiscount.Value;
                //row["PriceWithDiscount"] = 0;
                //row["NDSPercent"] = 0;
                //row["PriceInAccountingCurrency"] = 0;
                //row["PriceWithDiscountInAccountingCurrency"] = 0;
            }
            catch (System.Exception f)
            {
                SendMessageToLog("gridView_InitNewRow. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }

        private void gridView_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            try
            {
                e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
            }
            catch (System.Exception f)
            {
                SendMessageToLog("gridView_InvalidRowException. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }

        private void gridView_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                System.Boolean bOK = true;
                if ((gridView.GetDataRow(e.RowHandle)["Quantity"] == System.DBNull.Value) ||
                    (System.Convert.ToDouble(gridView.GetDataRow(e.RowHandle)["Quantity"]) < 1))
                {
                    bOK = false;
                    gridView.SetColumnError(colQuantity, "недопустимое количество", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning);
                }
                if (gridView.GetDataRow(e.RowHandle)["ProductID"] == System.DBNull.Value)
                {
                    bOK = false;
                    gridView.SetColumnError(colProductID, "укажите, пожалуйста, товар", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning);
                }
                if (gridView.GetDataRow(e.RowHandle)["MeasureID"] == System.DBNull.Value)
                {
                    bOK = false;
                    gridView.SetColumnError(colBackWaybItems_MeasureName, "укажите, пожалуйста, единицу измерения", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning);
                }
                e.Valid = bOK;

            }
            catch (System.Exception f)
            {
                SendMessageToLog("gridView_ValidateRow. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }

        private System.Boolean IsValidDataInRow(System.Int32 iRowHandle)
        {
            System.Boolean bRet = true;
            try
            {
                if ((iRowHandle < 0) || (gridView.RowCount < iRowHandle)) { return bRet; }
                else
                {
                    if ((gridView.GetDataRow(iRowHandle)["Quantity"] == System.DBNull.Value) ||
                        (System.Convert.ToDouble(gridView.GetDataRow(iRowHandle)["Quantity"]) < 1))
                    {
                        bRet = false;
                    }
                    if (gridView.GetDataRow(iRowHandle)["ProductID"] == System.DBNull.Value)
                    {
                        bRet = false;
                    }
                    if (gridView.GetDataRow(iRowHandle)["MeasureID"] == System.DBNull.Value)
                    {
                        bRet = false;
                    }
                }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("IsValidDataInRow. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return bRet;

        }

        private void gridView_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (m_bDisableEvents == true) { return; }
                SetPropertiesModified(true);
            }
            catch (System.Exception f)
            {
                SendMessageToLog("gridView_CellValueChanging. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }
        #endregion

        #region Экспорт приложения к накладной
        //<sbExportToHTML>
        private void sbExportToHTML_Click(object sender, System.EventArgs e)
        {
            try
            {
                panelProgressBar.Visible = true;

                string fileName = ShowSaveFileDialog("HTML документ", "HTML Documents|*.html");
                if (fileName != "")
                {
                    ExportTo(new DevExpress.XtraExport.ExportHtmlProvider(fileName));
                    OpenFile(fileName);
                }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("sbExportToHTML_Click. Текст ошибки: " + f.Message);
            }
            finally
            {
                panelProgressBar.Visible = false;
            }
            return;

        }
        //</sbExportToHTML>

        //<sbExportToXML>
        private void sbExportToXML_Click(object sender, System.EventArgs e)
        {
            try
            {
                panelProgressBar.Visible = true;

                string fileName = ShowSaveFileDialog("XML документ", "XML Documents|*.xml");
                if (fileName != "")
                {
                    ExportTo(new ExportXmlProvider(fileName));
                    OpenFile(fileName);
                }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("sbExportToXML_Click. Текст ошибки: " + f.Message);
            }
            finally
            {
                panelProgressBar.Visible = false;
            }

            return;
        }
        //</sbExportToXML>

        //<sbExportToXLS>
        private void sbExportToXLS_Click(object sender, System.EventArgs e)
        {
            try
            {
                panelProgressBar.Visible = true;
                string fileName = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xlsx");
                if (fileName != "")
                {
                    ExportTo(new ExportXlsProvider(fileName));
                    OpenFile(fileName);
                }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("sbExportToXLS_Click. Текст ошибки: " + f.Message);
            }
            finally
            {
                panelProgressBar.Visible = false;
            }

            return;
        }
        //</sbExportToXLS>

        //<sbExportToTXT>
        private void sbExportToTXT_Click(object sender, System.EventArgs e)
        {
            try
            {
                panelProgressBar.Visible = true;
                string fileName = ShowSaveFileDialog("Text Document", "Text Files|*.txt");
                if (fileName != "")
                {
                    ExportTo(new ExportTxtProvider(fileName));
                    OpenFile(fileName);
                }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("sbExportToTXT_Click. Текст ошибки: " + f.Message);
            }
            finally
            {
                panelProgressBar.Visible = false;
            }

            return;
        }
        //</sbExportToTXT>

        //<sbExportToTXT>
        private void sbExportToDBF_Click(object sender, System.EventArgs e)
        {
            try
            {
                panelProgressBar.Visible = true;
                string fileName = ShowSaveFileDialog("документ DBF", "DBF Files|*.dbf");
                if (fileName != "")
                {
                    if (EхportDBF(ref fileName, false) == true)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("Экспорт данных успешно завершён.\n" + fileName, "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    }
                }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("sbExportToTXT_Click. Текст ошибки: " + f.Message);
            }
            finally
            {
                panelProgressBar.Visible = false;
            }

            return;
        }

        private void sbExportToDBFCurrency_Click(object sender, System.EventArgs e)
        {
            try
            {
                panelProgressBar.Visible = true;
                string fileName = ShowSaveFileDialog("документ DBF", "DBF Files|*.dbf");
                if (fileName != "")
                {
                    if (EхportDBF(ref fileName, true) == true)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("Экспорт данных успешно завершён.\n" + fileName, "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    }
                }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("sbExportToTXT_Click. Текст ошибки: " + f.Message);
            }
            finally
            {
                panelProgressBar.Visible = false;
            }

            return;
        }

        private bool EхportDBF(ref string fileName, System.Boolean bCurrencyPrice)
        {
            string tableName = string.Empty;
            bool returnStatus = false;
            try
            {
                System.Int32 imaxLenghtFilename = 8;
                if (System.IO.Path.GetFileNameWithoutExtension(fileName).Length > imaxLenghtFilename)
                {
                    fileName = (System.IO.Path.GetDirectoryName(fileName) + "\\" + System.IO.Path.GetFileNameWithoutExtension(fileName).Trim().Replace(" ", "").Substring(0, imaxLenghtFilename) + ".dbf");
                }
                System.IO.File.Delete(fileName);


                string jetOleDbConString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0}; Extended Properties=dBASE IV";
                OleDbConnection conn = new System.Data.OleDb.OleDbConnection();
                conn.ConnectionString = String.Format(jetOleDbConString, System.IO.Path.GetDirectoryName(fileName));

                System.Data.OleDb.OleDbCommand oleDbCommandCreateTable = new System.Data.OleDb.OleDbCommand();
                System.Data.OleDb.OleDbCommand oleDbJetInsertCommand = new System.Data.OleDb.OleDbCommand();
                oleDbCommandCreateTable.Connection = conn;
                oleDbJetInsertCommand.Connection = conn;

                System.String strTableName = System.IO.Path.GetFileNameWithoutExtension(fileName);
                oleDbCommandCreateTable.CommandText = "CREATE TABLE " + strTableName + "(ARTICLE CHAR (20), NAME2 CHAR (52), QUANTITY Integer, PRICE Double, MARKUP Double, PARTS_ID Integer )";

                conn.Open();

                oleDbCommandCreateTable.ExecuteNonQuery();

                oleDbJetInsertCommand.CommandText = "INSERT INTO " + strTableName + " (ARTICLE, NAME2, QUANTITY, PRICE, MARKUP, PARTS_ID) VALUES (?, ?, ?, ?, ?, ?)";
                oleDbJetInsertCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("ARTICLE", System.Data.OleDb.OleDbType.VarWChar, 20, "ARTICLE"));
                oleDbJetInsertCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("NAME2", System.Data.OleDb.OleDbType.VarWChar, 52, "NAME2"));
                oleDbJetInsertCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("QUANTITY", System.Data.OleDb.OleDbType.Integer, 0, "QUANTITY"));
                oleDbJetInsertCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("PRICE", System.Data.OleDb.OleDbType.Double, 0, "PRICE"));
                oleDbJetInsertCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("MARKUP", System.Data.OleDb.OleDbType.Double, 0, "MARKUP"));
                oleDbJetInsertCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter("PARTS_ID", System.Data.OleDb.OleDbType.Integer, 0, "PARTS_ID"));

                foreach (CBackWaybillItem objItem in m_objSelectedWaybill.WaybillItemList)
                {
                    oleDbJetInsertCommand.Parameters["ARTICLE"].Value = System.Convert.ToString(objItem.Product.Article);
                    oleDbJetInsertCommand.Parameters["NAME2"].Value = System.Convert.ToString(objItem.Product.Name);
                    oleDbJetInsertCommand.Parameters["QUANTITY"].Value = System.Convert.ToInt32(objItem.Quantity);
                    oleDbJetInsertCommand.Parameters["PRICE"].Value = ((bCurrencyPrice == true) ? System.Convert.ToDouble(objItem.PriceInAccountingCurrency) : System.Convert.ToDouble(objItem.Price));
                    oleDbJetInsertCommand.Parameters["MARKUP"].Value = 0;
                    oleDbJetInsertCommand.Parameters["PARTS_ID"].Value = System.Convert.ToInt32(objItem.Product.ID_Ib);

                    oleDbJetInsertCommand.ExecuteNonQuery();
                }

                //for (System.Int32 i = 0; i < gridView.RowCount; i++)
                //{
                //    oleDbJetInsertCommand.Parameters["ARTICLE"].Value = System.Convert.ToString(gridView.GetRowCellValue(i, colOrderItems_PartsArticle));
                //    oleDbJetInsertCommand.Parameters["NAME2"].Value = System.Convert.ToString(gridView.GetRowCellValue(i, colOrderItems_PartsName));
                //    oleDbJetInsertCommand.Parameters["QUANTITY"].Value = System.Convert.ToInt32(gridView.GetRowCellValue(i, colQuantityReserved));
                //    oleDbJetInsertCommand.Parameters["PRICE"].Value = ((bCurrencyPrice == true) ?  System.Convert.ToDouble(gridView.GetRowCellValue(i, colPriceInAccountingCurrency) ) : System.Convert.ToDouble(gridView.GetRowCellValue(i, colPrice)) );
                //    oleDbJetInsertCommand.Parameters["MARKUP"].Value = 0;
                //    oleDbJetInsertCommand.Parameters["PARTS_ID"].Value = System.Convert.ToInt32(gridView.GetRowCellValue(i, colProductID));

                //    oleDbJetInsertCommand.ExecuteNonQuery();

                //}

                conn.Close();
                returnStatus = true;

            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось произвести экспорт заказа в файл dbf.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }

            return returnStatus;
        } // close function

        //</sbExportToTXT>

        private void OpenFile(string fileName)
        {
            if (DevExpress.XtraEditors.XtraMessageBox.Show("Хотите открыть этот файл?", "Экспорт в...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    process.StartInfo.FileName = fileName;
                    process.StartInfo.Verb = "Open";
                    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                    process.Start();
                }
                catch
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(this, "Системе не удалось найти приложение, с помощью которого можно было бы открыть файл.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            progressBarControl1.Position = 0;
        }

        //<sbExportToHTML>
        private void ExportTo(IExportProvider provider)
        {
            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            this.FindForm().Refresh();
            BaseExportLink link = gridView.CreateExportLink(provider);
            (link as GridViewExportLink).ExpandAll = false;
            link.Progress += new DevExpress.XtraGrid.Export.ProgressEventHandler(Export_Progress);
            link.ExportTo(true);
            provider.Dispose();
            link.Progress -= new DevExpress.XtraGrid.Export.ProgressEventHandler(Export_Progress);

            Cursor.Current = currentCursor;
        }
        //</sbExportToHTML>

        private string ShowSaveFileDialog(string title, string filter)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            string name = Application.ProductName;
            int n = name.LastIndexOf(".") + 1;
            if (n > 0) name = name.Substring(n, name.Length - n);
            dlg.Title = "Экспорт данных в " + title;
            dlg.FileName = name;
            dlg.Filter = filter;
            if (dlg.ShowDialog() == DialogResult.OK) return dlg.FileName;
            return "";
        }

        //<sbExportToHTML>
        private void Export_Progress(object sender, DevExpress.XtraGrid.Export.ProgressEventArgs e)
        {
            if (e.Phase == DevExpress.XtraGrid.Export.ExportPhase.Link)
            {
                progressBarControl1.Position = e.Position;
                this.Update();
            }
        }
        //</sbExportToHTML>

        #endregion

        #region Контекстное меню
        private void mitemClearRows_Click(object sender, EventArgs e)
        {
            dataSet.Tables["OrderItems"].Clear();
            SetPropertiesModified(true);
        }

        /// <summary>
        /// Удаляет выбранные в приложении записи
        /// </summary>
        private void DeleteSelectedRows()
        {
            try
            {
                int[] arr = gridView.GetSelectedRows();

                if (arr.Length > 0)
                {
                    gridView.DeleteSelectedRows();

                    SetPropertiesModified(true);
                }

            }
            catch (System.Exception f)
            {
                SendMessageToLog("DeleteSelectedRows. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }

        private void mitemDeleteSelectedRows_Click(object sender, EventArgs e)
        {
            DeleteSelectedRows();
        }

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            try
            {
                mitemExport.Enabled = (gridView.RowCount > 0);

                // накладная формируется из заказа, записи можно только удалять
                //mitemImport.Enabled = (m_bIsReadOnly == false);
                mitemImport.Enabled = false;

                mitemClearRows.Enabled = ((gridView.RowCount > 0) && (m_bIsReadOnly == false));
                mitemDeleteSelectedRows.Enabled = ((gridView.GetSelectedRows().Count<int>() > 0) && (m_bIsReadOnly == false));
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

    /// <summary>
    /// Тип, хранящий информацию, которая передается получателям уведомления о событии
    /// </summary>
    public class ChangeBackWaybillPropertieEventArgs : EventArgs
    {
        private readonly CBackWaybill m_objWaybill;
        public CBackWaybill Waybill
        { get { return m_objWaybill; } }

        private readonly enumActionSaveCancel m_enActionType;
        public enumActionSaveCancel ActionType
        { get { return m_enActionType; } }

        private readonly System.Boolean m_bIsNewDocument;
        public System.Boolean IsNewDocument
        { get { return m_bIsNewDocument; } }

        private readonly System.Guid m_uuidDocumentCurrentStateGuid;
        public System.Guid DocumentCurrentStateGuid
        { get { return m_uuidDocumentCurrentStateGuid; } }

        public ChangeBackWaybillPropertieEventArgs(CBackWaybill objWaybill, enumActionSaveCancel enActionType,
            System.Boolean bIsNewDocument, System.Guid uuidDocumentCurrentStateGuid)
        {
            m_objWaybill = objWaybill;
            m_enActionType = enActionType;
            m_bIsNewDocument = bIsNewDocument;
            m_uuidDocumentCurrentStateGuid = uuidDocumentCurrentStateGuid;
        }
    }

}

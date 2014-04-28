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
    public partial class ctrlWaybillEditor : UserControl
    {
        #region Свойства
        private UniXP.Common.CProfile m_objProfile;
        private UniXP.Common.MENUITEM m_objMenuItem;
        private List<CProduct> m_objPartsList;
        private List<CCustomer> m_objCustomerList;

        private CWaybill m_objSelectedWaybill;
        private System.Guid m_uuidSelectedStockID;

        private System.Boolean m_bIsChanged;
        private System.Boolean m_bCustomerInBlackList;

        private System.Boolean m_bDisableEvents;
        private System.Boolean m_bNewObject;
        private System.Boolean m_bIsReadOnly;
        private System.String m_strXLSImportFilePath;
        private System.Int32 m_iXLSSheetImport;
        private List<System.String> m_SheetList;
        //private System.Double m_CurratePricing;
        private System.Int32 m_SUMM_IS_PASS;
        private System.Boolean m_bIsAutoCreatePriceMode;

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

        public delegate void SetProductListToFormDelegate(List<CProduct> objProductNewList);
        public SetProductListToFormDelegate m_SetProductListToFormDelegate;

        private const System.Int32 iThreadSleepTime = 1000;
        private System.Boolean m_bThreadFinishJob;

        private const System.Double dblNDS = 20;
        private System.Guid m_iPaymentType1;
        private System.Guid m_iPaymentType2;
        private const System.String m_strPaymentType1 = "58636EC5-F64A-462C-90B1-7686ADFE70F9";
        private const System.String m_strPaymentType2 = "E872B5E3-83FF-4B1A-925D-0F1B3C4D5C85";
        private const System.String m_strReportsDirectory = "templates";
        private const System.String m_strReportSuppl = "Suppl.xlsx";
        private const System.Int32 m_iDebtorInfoPanelHeight = 117;
        private const System.Int32 m_iDebtorInfoPanelIndex = 1;
        #endregion

        #region События
        // Создаем закрытое поле, ссылающееся на заголовок списка делегатов
        private EventHandler<ChangeWaybillPropertieEventArgs> m_ChangeWaybillForCustomerProperties;
        // Создаем в классе член-событие
        public event EventHandler<ChangeWaybillPropertieEventArgs> ChangeWaybillForCustomerProperties
        {
            add
            {
                // берем закрытую блокировку и добавляем обработчик
                // (передаваемый по значению) в список делегатов
                m_ChangeWaybillForCustomerProperties += value;
            }
            remove
            {
                // берем закрытую блокировку и удаляем обработчик
                // (передаваемый по значению) из списка делегатов
                m_ChangeWaybillForCustomerProperties -= value;
            }
        }
        /// <summary>
        /// Инициирует событие и уведомляет о нем зарегистрированные объекты
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnChangeWaybillForCustomerProperties(ChangeWaybillPropertieEventArgs e)
        {
            // Сохраняем поле делегата во временном поле для обеспечение безопасности потока
            EventHandler<ChangeWaybillPropertieEventArgs> temp = m_ChangeWaybillForCustomerProperties;
            // Если есть зарегистрированные объектв, уведомляем их
            if (temp != null) temp(this, e);
        }
        public void SimulateChangeWaybillProperties( CWaybill objWaybill, enumActionSaveCancel enActionType,
            System.Boolean bIsNewWaybill, System.Guid uuidOrderCurrentStateGuid )
        {
            // Создаем объект, хранящий информацию, которую нужно передать
            // объектам, получающим уведомление о событии
            ChangeWaybillPropertieEventArgs e = new ChangeWaybillPropertieEventArgs(objWaybill, enActionType, bIsNewWaybill, uuidOrderCurrentStateGuid);

            // Вызываем виртуальный метод, уведомляющий наш объект о возникновении события
            // Если нет типа, переопределяющего этот метод, наш объект уведомит все объекты, 
            // подписавшиеся на уведомление о событии
            OnChangeWaybillForCustomerProperties(e);
        }
        #endregion

        #region Конструктор

        public ctrlWaybillEditor(UniXP.Common.CProfile objProfile, UniXP.Common.MENUITEM objMenuItem, List<CCustomer> objCustomer)
        {
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ru-RU");
            ci.NumberFormat.CurrencyDecimalSeparator = ".";
            ci.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;

            m_objProfile = objProfile;
            m_objMenuItem = objMenuItem;
            m_objCustomerList = objCustomer;

            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.AssemblyResolve += new ResolveEventHandler(MyResolveEventHandler);

            InitializeComponent();


            m_bIsChanged = false;
            m_bDisableEvents = false;
            m_bNewObject = false;
            m_bIsAutoCreatePriceMode = false;
            m_uuidSelectedStockID = System.Guid.Empty;

            m_objSelectedWaybill = null;
            m_objPartsList = null;
            m_iPaymentType1 = new Guid(m_strPaymentType1);
            m_iPaymentType2 = new Guid(m_strPaymentType2);
            m_strXLSImportFilePath = "";
            m_iXLSSheetImport = 0;
            m_bCustomerInBlackList = false;
            //m_CurratePricing = 0;
            m_SUMM_IS_PASS = 0;

            BeginDate.DateTime = System.DateTime.Today;
            DeliveryDate.DateTime = System.DateTime.Today;

            checkMultiplicity.CheckState = CheckState.Unchecked;

            LoadComboBoxItems();
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
        private void SendMessageToLog(System.String strMessage)
        {
            try
            {
                m_objMenuItem.SimulateNewMessage(strMessage);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "SendMessageToLog.\n Текст ошибки: " + f.Message, "Ошибка",
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
                Rtt.Properties.Appearance.BackColor = ((Rtt.SelectedItem == null) ? System.Drawing.Color.Tomato : System.Drawing.Color.White);
                AddressDelivery.Properties.Appearance.BackColor = ((AddressDelivery.SelectedItem == null) ? System.Drawing.Color.Tomato : System.Drawing.Color.White);
                Depart.Properties.Appearance.BackColor = ((Depart.SelectedItem == null) ? System.Drawing.Color.Tomato : System.Drawing.Color.White);
                SalesMan.Properties.Appearance.BackColor = ((SalesMan.SelectedItem == null) ? System.Drawing.Color.Tomato : System.Drawing.Color.White);
                Stock.Properties.Appearance.BackColor = ((Stock.SelectedItem == null) ? System.Drawing.Color.Tomato : System.Drawing.Color.White);

                BeginDate.Properties.Appearance.BackColor = ((BeginDate.DateTime == System.DateTime.MinValue) ? System.Drawing.Color.Tomato : System.Drawing.Color.White);
                DeliveryDate.Properties.Appearance.BackColor = ((DeliveryDate.DateTime == System.DateTime.MinValue ) ? System.Drawing.Color.Tomato : System.Drawing.Color.White);

                if (Customer.SelectedItem == null) { bRet = false; }
                if (WaybillState.SelectedItem == null) { bRet = false; }
                if (PaymentType.SelectedItem == null) { bRet = false; }
                if (Rtt.SelectedItem == null) { bRet = false; }
                if (AddressDelivery.SelectedItem == null) { bRet = false; }
                if (SalesMan.SelectedItem == null) { bRet = false; }
                if (Depart.SelectedItem == null) { bRet = false; }
                if (BeginDate.DateTime == System.DateTime.MinValue) { bRet = false; }
                if ((DeliveryDate.DateTime == System.DateTime.MinValue) || (DeliveryDate.DateTime.CompareTo(BeginDate.DateTime) < 0)) { bRet = false; }
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
                GetCustomerDebtInfo(false);
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
                System.String strErr = "";

                if ((sender == Customer) && (Customer.SelectedItem != null))
                {
                    CCustomer objCustomer = (CCustomer)Customer.SelectedItem;
                    // список РТТ
                    LoadRttListForCustomer(objCustomer.ID);
                    // список дочерних подразделений
                    LoadChildDeprtForCustomer(objCustomer.ID);

                    // телефоны клиента
                    objCustomer.PhoneList = CPhone.GetPhoneListForCustomer(m_objProfile, null, objCustomer.ID, ref strErr);

                    objCustomer = null;
                    if (AddressDelivery.SelectedItem != null)
                    {
                        Stock.Focus();
                        Stock.SelectAll();
                    }
                }

                if (sender == Rtt)
                {
                    AddressDelivery.Properties.Items.Clear();
                    if (Rtt.SelectedItem != null)
                    {
                        AddressDelivery.Properties.Items.AddRange((((CRtt)Rtt.SelectedItem).AddressList));
                        AddressDelivery.SelectedItem = ((AddressDelivery.Properties.Items.Count == 0) ? null : AddressDelivery.Properties.Items[0]);
                    }
                }

                if ((sender == AddressDelivery) || (sender == PaymentType))
                {
                    ReloadDscrpnByAddress();
                }

                if (sender == Depart)
                {
                    LoadSalesManListForDepart((CDepart)Depart.SelectedItem);
                }

                if (sender == Stock)
                {
                    if (Stock.SelectedItem != null)
                    {
                        m_uuidSelectedStockID = ((CStock)Stock.SelectedItem).ID;

                        StartThreadWithLoadData();
                    }

                    GetCustomerDebtInfo(true);
                }

                if ((sender == cboxProductTradeMark) || (sender == cboxProductType))
                {
                    SetFilterForPartsCombo();
                }

                if (sender == Customer)
                {
                    ChildDepart.SelectedItem = null;

                    if ((ChildDepart.Properties.Items.Count > 0) && (PaymentType.SelectedItem != null))
                    {
                        if (((CPaymentType)PaymentType.SelectedItem).ID == m_iPaymentType2)
                        {
                            ChildDepart.SelectedItem = ChildDepart.Properties.Items[0];
                        }
                    }

                    GetCustomerDebtInfo(true);
                }

                SetPropertiesModified(true);
            }//try
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка изменения свойств " + ((DevExpress.XtraEditors.ComboBoxEdit)sender).ToolTip + ". Текст ошибки: " + f.Message);
            }
            finally
            {
            }

            return;
        }

        private void ReloadDscrpnByAddress()
        {
            try
            {
                if ((AddressDelivery.SelectedItem != null) && (PaymentType.SelectedItem != null) && (((CPaymentType)PaymentType.SelectedItem).ID == m_iPaymentType2))
                {
                    txtDescription.Text = (AddressDelivery.Text);
                    if ((Customer.SelectedItem != null) && (((CCustomer)Customer.SelectedItem).PhoneList != null))
                    {
                        List<CPhone> objPhoneList = ((CCustomer)Customer.SelectedItem).PhoneList;
                        foreach (CPhone objPhone in objPhoneList)
                        {
                            txtDescription.Text += ("  " + objPhone.PhoneNumber);
                        }
                    }
                }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("ReloadDscrpnByAddress. Текст ошибки: " + f.Message);
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
                SendMessageToLog("Ошибка изменения свойств ПСЦ. Текст ошибки: " + f.Message);
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
        private void SetPriceInRow(System.Int32 iRowHandle, System.Guid uuidPartsId, System.Guid uuidStockId, System.Guid uuidPaymentTypeId, System.Double dblDiscountPercent)
        {
            try
            {
                System.Double PriceImporter = 0;
                System.Double Price = 0;
                System.Double PriceWithDiscount = 0;
                System.Double NDSPercent = 0;
                System.Double PriceInAccountingCurrency = 0;
                System.Double PriceWithDiscountInAccountingCurrency = 0;
                System.String strErr = "";

                if (COrderRepository.GetPriceForOrderItem(m_objProfile, uuidPartsId, uuidStockId, uuidPaymentTypeId, dblDiscountPercent,
                    ref PriceImporter, ref Price, ref PriceWithDiscount, ref NDSPercent, ref PriceInAccountingCurrency,
                    ref PriceWithDiscountInAccountingCurrency, ref strErr) == true)
                {
                    gridView.SetRowCellValue(iRowHandle, colNDSPercent, NDSPercent);
                    gridView.SetRowCellValue(iRowHandle, colPriceImporter, PriceImporter);
                    gridView.SetRowCellValue(iRowHandle, colPrice, Price);
                    gridView.SetRowCellValue(iRowHandle, colPriceWithDiscount, PriceWithDiscount);
                    gridView.SetRowCellValue(iRowHandle, colPriceInAccountingCurrency, PriceInAccountingCurrency);
                    gridView.SetRowCellValue(iRowHandle, colPriceWithDiscountInAccountingCurrency, PriceWithDiscountInAccountingCurrency);
                }
                else
                {
                    SendMessageToLog("Запрос цены для позиции: " + strErr);
                }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("SetPriceInRow. Текст ошибки: " + f.Message);
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

                if ((e.Column == colProductID) && (m_objPartsList != null) && (e.Value != null))
                {

                    CProduct objItem = null;
                    try
                    {
                        objItem = m_objPartsList.Cast<CProduct>().Single<CProduct>(x => x.ID.CompareTo((System.Guid)e.Value) == 0);

                        gridView.SetRowCellValue(e.RowHandle, colMeasureID, objItem.Measure.ID);
                        gridView.SetRowCellValue(e.RowHandle, colOrderItems_MeasureName, objItem.Measure.ShortName);
                        gridView.SetRowCellValue(e.RowHandle, colOrderItems_QuantityInstock, objItem.CustomerOrderStockQty);

                        if (objItem.CustomerOrderStockQty < objItem.CustomerOrderMinRetailQty)
                        {
                            gridView.SetRowCellValue(e.RowHandle, colQuantity, objItem.CustomerOrderStockQty);
                            gridView.SetRowCellValue(e.RowHandle, colQuantityReturned, objItem.CustomerOrderStockQty);
                        }
                        else
                        {
                            gridView.SetRowCellValue(e.RowHandle, colQuantity, objItem.CustomerOrderMinRetailQty);
                            gridView.SetRowCellValue(e.RowHandle, colQuantityReturned, objItem.CustomerOrderMinRetailQty);
                        }

                        gridView.SetRowCellValue(e.RowHandle, colNDSPercent, objItem.ProductType.NDSRate);
                        gridView.SetRowCellValue(e.RowHandle, colOrderPackQty, objItem.CustomerOrderMinRetailQty);
                        gridView.SetRowCellValue(e.RowHandle, colOrderItems_PartsArticle, objItem.Article);
                        gridView.SetRowCellValue(e.RowHandle, colOrderItems_PartsName, objItem.Name);

                        System.Guid uuidStockId = ((Stock.SelectedItem == null) ? System.Guid.Empty : ((CStock)Stock.SelectedItem).ID);
                        System.Guid uuidPaymentTypeId = ((PaymentType.SelectedItem == null) ? System.Guid.Empty : ((CPaymentType)PaymentType.SelectedItem).ID);
                        if ((uuidStockId != System.Guid.Empty) && (uuidPaymentTypeId != System.Guid.Empty))
                        {
                            SetPriceInRow(e.RowHandle, objItem.ID, uuidStockId, uuidPaymentTypeId, System.Convert.ToDouble(gridView.GetRowCellValue(e.RowHandle, colDiscountPercent)));
                        }

                        //gridView.FocusedColumn = colOrderedQuantity;

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
                else if ((e.Column == colDiscountPercent) && (e.Value != null))
                {
                    if ((m_bIsAutoCreatePriceMode == true) && (m_bIsReadOnly == false))
                    {
                        if (System.Convert.ToDouble(gridView.GetRowCellValue(e.RowHandle, colDiscountPercent)) != 0)
                        {
                            gridView.SetRowCellValue(e.RowHandle, colDiscountPercent, 0);
                        }
                    }
                    else
                    {
                        System.Guid uuidStockId = ((Stock.SelectedItem == null) ? System.Guid.Empty : ((CStock)Stock.SelectedItem).ID);
                        System.Guid uuidPaymentTypeId = ((PaymentType.SelectedItem == null) ? System.Guid.Empty : ((CPaymentType)PaymentType.SelectedItem).ID);
                        if ((uuidStockId != System.Guid.Empty) && (uuidPaymentTypeId != System.Guid.Empty))
                        {
                            SetPriceInRow(e.RowHandle, (System.Guid)gridView.GetRowCellValue(e.RowHandle, colProductID), uuidStockId, uuidPaymentTypeId, System.Convert.ToDouble(gridView.GetRowCellValue(e.RowHandle, colDiscountPercent)));
                        }
                    }

                }
                else if ((e.Column == colQuantity) && (e.Value != null) && (gridView.GetRowCellValue(e.RowHandle, colProductID) != null) &&
                    (gridView.GetRowCellValue(e.RowHandle, colOrderPackQty) != System.DBNull.Value) &&
                    (gridView.GetRowCellValue(e.RowHandle, colOrderItems_QuantityInstock) != System.DBNull.Value))
                {
                    System.Decimal dclOrderedQty = System.Convert.ToDecimal(e.Value);
                    System.Decimal dclmultiplicity = System.Convert.ToDecimal(gridView.GetRowCellValue(e.RowHandle, colOrderPackQty));
                    System.Decimal dclInstockQty = System.Convert.ToDecimal(gridView.GetRowCellValue(e.RowHandle, colOrderItems_QuantityInstock));

                    if (checkMultiplicity.CheckState == CheckState.Checked)
                    {
                        if ((dclOrderedQty % dclmultiplicity) != 0)
                        {
                            dclOrderedQty = (((int)dclOrderedQty / (int)dclmultiplicity) * dclmultiplicity) + dclmultiplicity;
                            if (dclOrderedQty > dclInstockQty)
                            {
                                dclOrderedQty = dclInstockQty;
                            }
                        }
                    }

                    if (System.Convert.ToDecimal(System.Convert.ToDecimal(e.Value)) != dclOrderedQty)
                    {
                        gridView.SetRowCellValue(e.RowHandle, colQuantity, System.Convert.ToDecimal(dclOrderedQty));
                    }
                    gridView.SetRowCellValue(e.RowHandle, colQuantityReturned, System.Convert.ToDecimal(dclOrderedQty));
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
                    gridView.SetRowCellValue(gridView.FocusedRowHandle, colProductID, (System.Guid)e.Value);
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
            //try
            //{
            //    if (m_bDisableEvents == true) { return; }

            //    if (e.NewValue != null)
            //    {
            //        System.Decimal dclOrderedQty = System.Convert.ToDecimal(e.NewValue);
            //        System.Decimal dclmultiplicity = System.Convert.ToDecimal(gridView.GetRowCellValue(gridView.FocusedRowHandle, colOrderPackQty));
            //        System.Decimal dclInstockQty = System.Convert.ToDecimal(gridView.GetRowCellValue(gridView.FocusedRowHandle, colOrderItems_QuantityInstock));

            //        if ((dclOrderedQty % dclmultiplicity) != 0)
            //        {
            //            dclOrderedQty = (((int)dclOrderedQty / (int)dclmultiplicity) * dclmultiplicity) + dclmultiplicity;
            //            if (dclOrderedQty > dclInstockQty)
            //            {
            //                dclOrderedQty = dclInstockQty;
            //            }
            //        }

            //        if (System.Convert.ToDecimal(e.NewValue) != dclOrderedQty)
            //        {
            //            e.NewValue = dclOrderedQty;
            //            OrderItems.Rows[gridView.FocusedRowHandle][OrderedQuantity] = dclOrderedQty;
            //            OrderItems.Rows[gridView.FocusedRowHandle][QuantityReserved] = dclOrderedQty;
            //            OrderItems.AcceptChanges();

            //          //  gridView.SetRowCellValue(gridView.FocusedRowHandle, colOrderedQuantity, System.Convert.ToDecimal(dclOrderedQty));
            //        }
            //        //gridView.SetRowCellValue(gridView.FocusedRowHandle, colQuantityReserved, System.Convert.ToDecimal(dclOrderedQty));
            //        //gridView.UpdateCurrentRow();
            //    }
            //}
            //catch (System.Exception f)
            //{
            //    SendMessageToLog("repositoryItemCalcEditOrderedQuantity_EditValueChanged. Текст ошибки: " + f.Message);
            //}
            //finally
            //{
            //}
            //return;
        }

        private void gridView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if ((gridView.GetRowCellValue(e.RowHandle, colQuantityReturned) != null) &&
                    (gridView.GetRowCellValue(e.RowHandle, colQuantity) != null))
                {
                    System.Double dblQuantityReserved = System.Convert.ToDouble(gridView.GetRowCellValue(e.RowHandle, colQuantityReturned));
                    System.Double dblOrderedQuantity = System.Convert.ToDouble(gridView.GetRowCellValue(e.RowHandle, colQuantity));
                    if (dblQuantityReserved != dblOrderedQuantity)
                    {
                        if ((e.Column == colQuantity) || (e.Column == colQuantityReturned))
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
        /// <summary>
        /// Устанавливает указанный процент скидки на все позиции в заказе
        /// </summary>
        /// <param name="dblDiscountPercent">размер скидки в процентах</param>
        private void SenDiscountPercent(System.Double dblDiscountPercent)
        {
            try
            {
                tableLayoutPanelBackground.SuspendLayout();

                if (gridView.RowCount == 0) { return; }

                Cursor = Cursors.WaitCursor;
                System.Guid uuidStockId = ((Stock.SelectedItem == null) ? System.Guid.Empty : ((CStock)Stock.SelectedItem).ID);
                System.Guid uuidPaymentTypeId = ((PaymentType.SelectedItem == null) ? System.Guid.Empty : ((CPaymentType)PaymentType.SelectedItem).ID);
                if ((uuidStockId != System.Guid.Empty) && (uuidPaymentTypeId != System.Guid.Empty))
                {
                    for (System.Int32 i = 0; i < gridView.RowCount; i++)
                    {
                        gridView.SetRowCellValue(i, colDiscountPercent, dblDiscountPercent);
                        //SetPriceInRow(i, (System.Guid)gridView.GetRowCellValue(i, colProductID), uuidStockId, uuidPaymentTypeId, System.Convert.ToDouble(gridView.GetRowCellValue(i, colDiscountPercent)));
                    }
                }

            }
            catch (System.Exception f)
            {
                SendMessageToLog("SenDiscountPercent. Текст ошибки: " + f.Message);
            }
            finally
            {
                this.tableLayoutPanelBackground.ResumeLayout(false);
                gridView.RefreshData();
                Cursor = Cursors.Default;
            }
            return;
        }
        private void btnSetDiscount_Click(object sender, EventArgs e)
        {
            try
            {
                SenDiscountPercent(System.Convert.ToDouble(spinEditDiscount.Value));
            }
            catch (System.Exception f)
            {
                SendMessageToLog("btnSetDiscount_Click. Текст ошибки: " + f.Message);
            }
            finally
            {
                this.tableLayoutPanelBackground.ResumeLayout(false);
                gridView.RefreshData();
                Cursor = Cursors.Default;
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
                Customer.Properties.Items.AddRange(m_objCustomerList);

                Rtt.Properties.Items.Clear();
                AddressDelivery.Properties.Items.Clear();
                ChildDepart.Properties.Items.Clear();
                Depart.Properties.Items.Clear();
                cboxProductTradeMark.Properties.Items.Clear();
                cboxProductType.Properties.Items.Clear();

                // Склады
                Stock.Properties.Items.Clear();
                Stock.Properties.Items.AddRange(CStock.GetStockList(m_objProfile, null)); //.Where<CStock>(x=>x.IsTrade == true).ToList<CStock>());

                // Состояние накладной
                WaybillState.Properties.Items.Clear();
                WaybillState.Properties.Items.AddRange( CWaybillState.GetWaybillStateList( m_objProfile, ref strErr ) );

                // Вид отгрузки
                WaybillShipMode.Properties.Items.Clear();
                WaybillShipMode.Properties.Items.AddRange( CWaybillShipMode.GetWaybillShipModeList( m_objProfile, ref strErr ) );

                // Форма оплаты
                PaymentType.Properties.Items.Clear();
                PaymentType.Properties.Items.AddRange(CPaymentType.GetPaymentTypeList(m_objProfile, null, System.Guid.Empty));

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
        /// Загружает список РТТ для клиента с указанным идентификатором
        /// </summary>
        /// <param name="uuidCustomerId">идентификатор клиента</param>
        private void LoadRttListForCustomer(System.Guid uuidCustomerId)
        {
            try
            {
                Rtt.SelectedItem = null;
                AddressDelivery.SelectedItem = null;

                Rtt.Properties.Items.Clear();
                AddressDelivery.Properties.Items.Clear();

                if (uuidCustomerId.CompareTo(System.Guid.Empty) != 0)
                {
                    Rtt.Properties.Items.AddRange(CRtt.GetRttList(m_objProfile, null, uuidCustomerId));
                    Rtt.SelectedItem = ((Rtt.Properties.Items.Count == 0) ? null : Rtt.Properties.Items[0]);
                }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("LoadRttListForCustomer. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
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

                List<CProduct> FilteredProductList = m_objPartsList;

                if (TradeMarkId != System.Guid.Empty)
                {
                    FilteredProductList = FilteredProductList.Where<CProduct>(x => x.ProductTradeMark.ID == TradeMarkId).ToList<CProduct>();
                }

                if (PartTypeId != System.Guid.Empty)
                {
                    FilteredProductList = FilteredProductList.Where<CProduct>(x => x.ProductType.ID == PartTypeId).ToList<CProduct>();
                }

                //if (FilteredProductList.Count != m_objPartsList.Count)
                //{
                dataSet.Tables["Product"].Clear();

                System.Data.DataRow newRowProduct = null;
                dataSet.Tables["Product"].Clear();
                foreach (CProduct objItem in FilteredProductList)
                {
                    newRowProduct = dataSet.Tables["Product"].NewRow();

                    newRowProduct["ProductID"] = objItem.ID;
                    newRowProduct["Product_MeasureID"] = objItem.Measure.ID;
                    newRowProduct["Product_MeasureName"] = objItem.Measure.ShortName;
                    newRowProduct["ProductFullName"] = objItem.ProductFullName;
                    newRowProduct["CustomerOrderStockQty"] = objItem.CustomerOrderStockQty;
                    newRowProduct["CustomerOrderResQty"] = objItem.CustomerOrderResQty;
                    newRowProduct["CustomerOrderPackQty"] = objItem.CustomerOrderPackQty;

                    dataSet.Tables["Product"].Rows.Add(newRowProduct);
                }
                newRowProduct = null;
                dataSet.Tables["Product"].AcceptChanges();
                //}

                // теперь нужно проверить содержимое заказа на предмет товараов, не попадающих под условия фильтра
                if ((gridView.RowCount > 0) && ((TradeMarkId != System.Guid.Empty) || (PartTypeId != System.Guid.Empty)))
                {
                    System.Guid PartsGuid = System.Guid.Empty;
                    CProduct objProduct = null;
                    System.Boolean bOk = true;
                    for (System.Int32 i = (gridView.RowCount - 1); i >= 0; i--)
                    {
                        PartsGuid = (System.Guid)(gridView.GetDataRow(i)["ProductID"]);
                        bOk = true;
                        try
                        {
                            objProduct = m_objPartsList.SingleOrDefault<CProduct>(x => x.ID == PartsGuid);
                        }
                        catch
                        {
                            objProduct = null;
                        }
                        if (objProduct != null)
                        {
                            // проверяем на соответствие марке
                            if (TradeMarkId != System.Guid.Empty)
                            {
                                bOk = (objProduct.ProductTradeMark.ID == TradeMarkId);
                            }
                            if ((bOk == true) && (PartTypeId != System.Guid.Empty))
                            {
                                bOk = (objProduct.ProductTradeMark.ID == PartTypeId);
                            }
                        }
                        if (bOk == false)
                        {
                            gridView.DeleteRow(i);
                        }
                    }

                    objProduct = null;

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
        /// <param name="objPartsList">списком товара</param>
        private void SetProductListToForm(List<CProduct> objPartsList)
        {
            try
            {
                m_objPartsList = objPartsList;

                System.Data.DataRow newRowProduct = null;
                dataSet.Tables["OrderItems"].Clear();
                dataSet.Tables["Product"].Clear();
                foreach (CProduct objItem in m_objPartsList)
                {
                    newRowProduct = dataSet.Tables["Product"].NewRow();
                    newRowProduct["ProductID"] = objItem.ID;
                    newRowProduct["Product_MeasureID"] = objItem.Measure.ID;
                    newRowProduct["Product_MeasureName"] = objItem.Measure.ShortName;
                    newRowProduct["ProductFullName"] = objItem.ProductFullName;
                    newRowProduct["CustomerOrderStockQty"] = objItem.CustomerOrderStockQty;
                    newRowProduct["CustomerOrderResQty"] = objItem.CustomerOrderResQty;
                    newRowProduct["CustomerOrderPackQty"] = objItem.CustomerOrderPackQty;

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
                m_objPartsList = COrderRepository.GetPartsInstockList(m_objProfile, null, m_uuidSelectedStockID, System.Guid.Empty);
                if (m_objPartsList != null)
                {
                    this.Invoke(m_SetProductListToFormDelegate, new Object[] { m_objPartsList });
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
                DeliveryDate.Properties.ReadOnly = bSet;
                ShipDate.Properties.ReadOnly = bSet;
                txtDescription.Properties.ReadOnly = bSet;
                WaybilllNum.Properties.ReadOnly = bSet;

                Customer.Properties.ReadOnly = bSet;
                ChildDepart.Properties.ReadOnly = bSet;
                PaymentType.Properties.ReadOnly = bSet;
                WaybillState.Properties.ReadOnly = bSet;
                WaybillShipMode.Properties.ReadOnly = bSet;
                IsBonus.Properties.ReadOnly = bSet;
                Rtt.Properties.ReadOnly = bSet;
                AddressDelivery.Properties.ReadOnly = bSet;
                Depart.Properties.ReadOnly = bSet;
                SalesMan.Properties.ReadOnly = bSet;
                Stock.Properties.ReadOnly = bSet;

                cboxProductTradeMark.Properties.ReadOnly = bSet;
                cboxProductType.Properties.ReadOnly = bSet;
                spinEditDiscount.Properties.ReadOnly = bSet;

                checkSetOrderInQueue.Enabled = !bSet;

                gridView.OptionsBehavior.Editable = !bSet;
                controlNavigator.Enabled = !bSet;
                if (m_bIsAutoCreatePriceMode == false)
                {
                    btnSetDiscount.Enabled = !bSet;
                    checkEditCalcPrices.Enabled = !bSet;
                }

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
        private void SetModeNewWaybillFromSuppl()
        {
            try
            {
                BeginDate.Properties.ReadOnly = true;
                DeliveryDate.Properties.ReadOnly = true;
                ShipDate.Properties.ReadOnly = true;
                txtDescription.Properties.ReadOnly = false;
                WaybilllNum.Properties.ReadOnly = false;

                Customer.Properties.ReadOnly = true;
                ChildDepart.Properties.ReadOnly = true;
                PaymentType.Properties.ReadOnly = true;
                WaybillState.Properties.ReadOnly = true;
                WaybillShipMode.Properties.ReadOnly = false;
                IsBonus.Properties.ReadOnly = true;
                Rtt.Properties.ReadOnly = true;
                AddressDelivery.Properties.ReadOnly = true;
                Depart.Properties.ReadOnly = true;
                SalesMan.Properties.ReadOnly = true;
                Stock.Properties.ReadOnly = true;

                cboxProductTradeMark.Properties.ReadOnly = true;
                cboxProductType.Properties.ReadOnly = true;
                spinEditDiscount.Properties.ReadOnly = true;

                checkSetOrderInQueue.Enabled = false;

                gridView.OptionsBehavior.Editable = false;
                controlNavigator.Enabled = true;
                controlNavigator.Buttons.Append.Enabled = false;
                controlNavigator.Buttons.Edit.Enabled = false;
                controlNavigator.Buttons.CancelEdit.Enabled = false;
                controlNavigator.Buttons.EndEdit.Enabled = false;

                if (m_bIsAutoCreatePriceMode == false)
                {
                    btnSetDiscount.Enabled = false;
                    checkEditCalcPrices.Enabled = false;
                }

                mitemImport.Enabled = false;

                m_bIsReadOnly = false;

                btnEdit.Enabled = false;
            }
            catch (System.Exception f)
            {
                SendMessageToLog("SetModeNewWaybillFromSuppl. Текст ошибки: " + f.Message);
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
                DeliveryDate.EditValue = null;
                ShipDate.EditValue = null;
                txtDescription.Text = "";
                WaybilllNum.Text = "";

                Customer.SelectedItem = null;
                ChildDepart.SelectedItem = null;
                PaymentType.SelectedItem = null;
                WaybillShipMode.SelectedItem = null;
                WaybillState.SelectedItem = null;
                IsBonus.CheckState = CheckState.Unchecked;
                Rtt.SelectedItem = null;
                AddressDelivery.SelectedItem = null;
                Depart.SelectedItem = null;
                SalesMan.SelectedItem = null;
                Stock.SelectedItem = null;
                cboxProductTradeMark.SelectedItem = null;
                cboxProductType.SelectedItem = null;
                spinEditDiscount.Value = 0;

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
        /// <param name="objWaybill">накладная</param>
        public void EditWaybill(CWaybill objWaybill, System.Boolean bNewObject)
        {
            if (objWaybill == null) { return; }
            m_bDisableEvents = true;
            m_bNewObject = bNewObject;
            SetCreatePriceMode();
            try
            {
                m_objSelectedWaybill = objWaybill;
                if (m_objSelectedWaybill.ChildDepart != null)
                {
                    m_objSelectedWaybill.ChildDepart = CChildDepart.GetChildDepart(m_objProfile, null, m_objSelectedWaybill.ChildDepart.ID);
                }

                this.tableLayoutPanelBackground.SuspendLayout();

                ClearControls();


                BeginDate.DateTime = m_objSelectedWaybill.BeginDate;
                DeliveryDate.DateTime = m_objSelectedWaybill.DeliveryDate;
                if( System.DateTime.Compare( m_objSelectedWaybill.ShipDate, System.DateTime.MinValue ) != 0 )
                {
                    ShipDate.DateTime = m_objSelectedWaybill.ShipDate;
                }
                txtDescription.Text = m_objSelectedWaybill.Description;
                WaybilllNum.Text = m_objSelectedWaybill.DocNum;

                Customer.SelectedItem = (m_objSelectedWaybill.Customer == null) ? null : Customer.Properties.Items.Cast<CCustomer>().Single<CCustomer>(x => x.ID.CompareTo(m_objSelectedWaybill.Customer.ID) == 0);

                AddressDelivery.Properties.Items.Clear();

                LoadRttListForCustomer(m_objSelectedWaybill.Customer.ID);
                LoadChildDeprtForCustomer(m_objSelectedWaybill.Customer.ID);
                System.String strErr = "";
                m_objSelectedWaybill.Customer.PhoneList = CPhone.GetPhoneListForCustomer(m_objProfile, null,
                    m_objSelectedWaybill.Customer.ID, ref strErr);


                Rtt.SelectedItem = (m_objSelectedWaybill.Rtt == null) ? null : Rtt.Properties.Items.Cast<CRtt>().Single<CRtt>(x => x.ID.CompareTo(m_objSelectedWaybill.Rtt.ID) == 0);
                if (Rtt.SelectedItem != null)
                {
                    AddressDelivery.Properties.Items.AddRange((((CRtt)Rtt.SelectedItem).AddressList));
                }

                ChildDepart.SelectedItem = (m_objSelectedWaybill.ChildDepart == null) ? null : ChildDepart.Properties.Items.Cast<CChildDepart>().SingleOrDefault<CChildDepart>(x => x.ID.CompareTo(m_objSelectedWaybill.ChildDepart.ID) == 0);
                PaymentType.SelectedItem = (m_objSelectedWaybill.PaymentType == null) ? null : PaymentType.Properties.Items.Cast<CPaymentType>().SingleOrDefault<CPaymentType>(x => x.ID.CompareTo(m_objSelectedWaybill.PaymentType.ID) == 0);
                WaybillShipMode.SelectedItem = (m_objSelectedWaybill.WaybillShipMode == null) ? null : WaybillShipMode.Properties.Items.Cast<CWaybillShipMode>().SingleOrDefault<CWaybillShipMode>(x => x.ID.CompareTo(m_objSelectedWaybill.WaybillShipMode.ID) == 0);
                WaybillState.SelectedItem = (m_objSelectedWaybill.WaybillState == null) ? null : WaybillState.Properties.Items.Cast<CWaybillState>().SingleOrDefault<CWaybillState>(x => x.ID.CompareTo(m_objSelectedWaybill.WaybillState.ID) == 0);

                IsBonus.CheckState = ((m_objSelectedWaybill.IsBonus == true) ? CheckState.Checked : CheckState.Unchecked);
                AddressDelivery.SelectedItem = (m_objSelectedWaybill.AddressDelivery == null) ? null : AddressDelivery.Properties.Items.Cast<CAddress>().SingleOrDefault<CAddress>(x => x.ID.CompareTo(m_objSelectedWaybill.AddressDelivery.ID) == 0);
                Depart.SelectedItem = (m_objSelectedWaybill.Depart == null) ? null : Depart.Properties.Items.Cast<CDepart>().SingleOrDefault<CDepart>(x => x.uuidID.CompareTo(m_objSelectedWaybill.Depart.uuidID) == 0);

                LoadSalesManListForDepart((CDepart)Depart.SelectedItem);

                SalesMan.SelectedItem = (m_objSelectedWaybill.SalesMan == null) ? null : SalesMan.Properties.Items.Cast<CSalesMan>().SingleOrDefault<CSalesMan>(x => x.uuidID.CompareTo(m_objSelectedWaybill.SalesMan.uuidID) == 0);
                Stock.SelectedItem = (m_objSelectedWaybill.Stock == null) ? null : Stock.Properties.Items.Cast<CStock>().SingleOrDefault<CStock>(x => x.ID.CompareTo(m_objSelectedWaybill.Stock.ID) == 0);

                cboxProductTradeMark.Properties.Items.Add(new CProductTradeMark() { ID = System.Guid.Empty, Name = "" });
                cboxProductTradeMark.Properties.Items.AddRange(CProductTradeMark.GetProductTradeMarkList(m_objProfile, null));
                cboxProductType.Properties.Items.Add(new CProductType() { ID = System.Guid.Empty, Name = "" });
                cboxProductType.Properties.Items.AddRange(CProductType.GetProductTypeList(m_objProfile, null));

                if (Stock.SelectedItem != null)
                {
                    m_uuidSelectedStockID = ((CStock)Stock.SelectedItem).ID;
                    m_objPartsList = COrderRepository.GetPartsInstockList(m_objProfile, null, m_uuidSelectedStockID, m_objSelectedWaybill.ID, true);
                    if (m_objPartsList != null)
                    {
                        SetProductListToForm(m_objPartsList);
                    }
                }

                dataSet.Tables["OrderItems"].Clear();
                System.Data.DataRow newRowOrderItems = null;
                CProduct objProduct = null;
                foreach (CWaybillItem objItem in m_objSelectedWaybill.WaybillItemList)
                {
                    newRowOrderItems = dataSet.Tables["OrderItems"].NewRow();

                    newRowOrderItems["OrderItemsID"] = objItem.ID;
                    newRowOrderItems["WaybItem_Id"] = objItem.Ib_ID;
                    newRowOrderItems["SupplItem_Guid"] = objItem.SupplItemID;

                    newRowOrderItems["ProductID"] = objItem.Product.ID;
                    newRowOrderItems["MeasureID"] = objItem.Measure.ID;
                    newRowOrderItems["Quantity"] = objItem.Quantity;
                    newRowOrderItems["QuantityReturned"] = objItem.QuantityReturn;

                    if (m_objPartsList != null)
                    {
                        try
                        {
                            objProduct = m_objPartsList.Single<CProduct>(x => x.ID.CompareTo(objItem.Product.ID) == 0);
                        }
                        catch
                        {
                            objProduct = null;
                        }
                        if (objProduct != null)
                        {
                            newRowOrderItems["OrderPackQty"] = objProduct.CustomerOrderMinRetailQty;
                            newRowOrderItems["OrderItems_QuantityInstock"] = objProduct.CustomerOrderStockQty;
                        }
                    }

                    newRowOrderItems["OrderItems_MeasureName"] = objItem.Measure.ShortName;
                    newRowOrderItems["OrderItems_PartsName"] = objItem.Product.Name;
                    newRowOrderItems["OrderItems_PartsArticle"] = objItem.Product.Article;
                    newRowOrderItems["PriceImporter"] = objItem.PriceImporter;
                    newRowOrderItems["Price"] = objItem.Price;

                    if (m_bNewObject == false)
                    {
                        newRowOrderItems["DiscountPercent"] = objItem.DiscountPercent;
                    }
                    else
                    {
                        if ((m_bNewObject == true) && (m_bIsAutoCreatePriceMode == false))
                        {
                            newRowOrderItems["DiscountPercent"] = objItem.DiscountPercent;
                        }
                        else
                        {
                            newRowOrderItems["DiscountPercent"] = 0;
                        }
                    }
                    newRowOrderItems["PriceWithDiscount"] = objItem.PriceWithDiscount;
                    newRowOrderItems["NDSPercent"] = objItem.NDSPercent;
                    newRowOrderItems["PriceInAccountingCurrency"] = objItem.PriceInAccountingCurrency;
                    newRowOrderItems["PriceWithDiscountInAccountingCurrency"] = objItem.PriceWithDiscountInAccountingCurrency;

                    dataSet.Tables["OrderItems"].Rows.Add(newRowOrderItems);
                }
                newRowOrderItems = null;
                objProduct = null;
                dataSet.Tables["OrderItems"].AcceptChanges();

                ReloadDscrpnByAddress();

                checkEditForStock.Visible = false;
                checkEditForStock.Checked = true;

                SetPropertiesModified(false);
                btnCancel.Enabled = true;
                btnCancel.Focus();
                ValidateProperties();

                SetModeReadOnly(true);
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка редактирования заказа клиенту. Текст ошибки: " + f.Message);
            }
            finally
            {
                this.tableLayoutPanelBackground.ResumeLayout(false);
                m_bDisableEvents = false;

                GetCustomerDebtInfo(true);
                Cursor = Cursors.Default;
            }
            return;
        }
        #endregion

        #region Копирование накладной
        /// <summary>
        /// Копирует свойства накладной в новую накладную и открывает ее для редактирования
        /// </summary>
        /// <param name="objWaybill">накладная</param>
        public void CopyWaybill(CWaybill objWaybill)
        {
            if (objWaybill == null) { return; }

            try
            {
                SetCreatePriceMode();

                m_objSelectedWaybill = new CWaybill();
                m_objSelectedWaybill.BeginDate = System.DateTime.Today;
                m_objSelectedWaybill.DeliveryDate = System.DateTime.Today;
                m_objSelectedWaybill.Description = objWaybill.Description;
                m_objSelectedWaybill.Customer = objWaybill.Customer;
                m_objSelectedWaybill.Rtt = objWaybill.Rtt;
                m_objSelectedWaybill.ChildDepart = objWaybill.ChildDepart;
                m_objSelectedWaybill.PaymentType = objWaybill.PaymentType;
                m_objSelectedWaybill.WaybillState = objWaybill.WaybillState;
                m_objSelectedWaybill.WaybillShipMode = objWaybill.WaybillShipMode;
                m_objSelectedWaybill.IsBonus = objWaybill.IsBonus;
                m_objSelectedWaybill.Depart = objWaybill.Depart;
                m_objSelectedWaybill.SalesMan = objWaybill.SalesMan;
                m_objSelectedWaybill.Stock = objWaybill.Stock;
                m_objSelectedWaybill.DocNum = objWaybill.DocNum;

                m_objSelectedWaybill.AddressDelivery = new CAddress() { ID = objWaybill.AddressDelivery.ID };
                CAddress.Init(m_objProfile, null, m_objSelectedWaybill.AddressDelivery, m_objSelectedWaybill.AddressDelivery.ID);

                System.String strErr = System.String.Empty;

                m_objSelectedWaybill.WaybillItemList = CWaybillItem.GetWaybillTablePart(m_objProfile, objWaybill.ID, ref strErr);

                EditWaybill(m_objSelectedWaybill, true);

                if (m_objSelectedWaybill.PaymentType != null)
                {
                    for (System.Int32 i = 0; i < gridView.RowCount; i++)
                    {
                        SetPriceInRow(i, (System.Guid)gridView.GetRowCellValue(i, colProductID), m_objSelectedWaybill.Stock.ID,
                            m_objSelectedWaybill.PaymentType.ID, System.Convert.ToDouble(gridView.GetRowCellValue(i, colDiscountPercent)));
                    }
                }

                SetModeReadOnly(false);
                gridControl.RefreshDataSource();
                gridView.RefreshData();

            }
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка копирования накладной. Текст ошибки: " + f.Message);
            }
            finally
            {
                m_bNewObject = true;
                m_objSelectedWaybill.ID = System.Guid.Empty;
                btnSave.Enabled = (ValidateProperties() == true);
            }
            return;
        }

        #endregion

        #region Новая накладная
        /// <summary>
        /// Новая накладная
        /// </summary>
        public void NewWaybill(CCustomer objCustomer, CCompany objCompany, CStock objStock, CPaymentType objPaymentType)
        {
            try
            {
                m_bNewObject = true;
                m_bDisableEvents = true;
                SetCreatePriceMode();

                m_objSelectedWaybill = new CWaybill()
                {
                    BeginDate = System.DateTime.Today /*  BeginDate.DateTime; // System.DateTime.Today;*/,
                    DeliveryDate = System.DateTime.Today.AddDays(1) /* DeliveryDate.DateTime; // System.DateTime.Today;*/,
                    WaybillItemList = new List<CWaybillItem>(),
                    Customer = objCustomer,
                    PaymentType = objPaymentType
                };
                if (objCompany != null)
                {
                    if (Stock.Properties.Items.Count > 0)
                    {
                        for (System.Int32 i = 0; i < Stock.Properties.Items.Count; i++)
                        {
                            if (((CStock)Stock.Properties.Items[i]).Company.ID == objCompany.ID)
                            {
                                m_objSelectedWaybill.Stock = (CStock)Stock.Properties.Items[i];
                                break;
                            }
                        }
                    }
                }

                if (objStock != null)
                {
                    if (Stock.Properties.Items.Count > 0)
                    {
                        for (System.Int32 i = 0; i < Stock.Properties.Items.Count; i++)
                        {
                            if (((CStock)Stock.Properties.Items[i]).ID == objStock.ID)
                            {
                                m_objSelectedWaybill.Stock = (CStock)Stock.Properties.Items[i];
                                break;
                            }
                        }
                    }
                }

                this.tableLayoutPanelBackground.SuspendLayout();

                ClearControls();

                if (m_objSelectedWaybill.Customer != null)
                {
                    LoadRttListForCustomer(m_objSelectedWaybill.Customer.ID);
                    if (Rtt.Properties.Items.Count > 0)
                    {
                        m_objSelectedWaybill.Rtt = (CRtt)Rtt.Properties.Items[0];
                        AddressDelivery.Properties.Items.Clear();
                        if (Rtt.SelectedItem != null)
                        {
                            AddressDelivery.Properties.Items.AddRange((((CRtt)Rtt.SelectedItem).AddressList));
                            AddressDelivery.SelectedItem = ((AddressDelivery.Properties.Items.Count == 0) ? null : AddressDelivery.Properties.Items[0]);
                            m_objSelectedWaybill.AddressDelivery = (CAddress)AddressDelivery.SelectedItem;
                        }
                    }
                    LoadChildDeprtForCustomer(m_objSelectedWaybill.Customer.ID);

                    System.String strErr = "";
                    m_objSelectedWaybill.Customer.PhoneList = CPhone.GetPhoneListForCustomer(m_objProfile, null,
                        m_objSelectedWaybill.Customer.ID, ref strErr);
                }

                BeginDate.DateTime = m_objSelectedWaybill.BeginDate;
                DeliveryDate.DateTime = m_objSelectedWaybill.DeliveryDate;

                if( System.DateTime.Compare(m_objSelectedWaybill.ShipDate, System.DateTime.MinValue) != 0 )
                {
                    ShipDate.DateTime = m_objSelectedWaybill.ShipDate;
                }

                txtDescription.Text = m_objSelectedWaybill.Description;
                WaybilllNum.Text = m_objSelectedWaybill.DocNum;
                Customer.SelectedItem = (m_objSelectedWaybill.Customer == null) ? null : Customer.Properties.Items.Cast<CCustomer>().Single<CCustomer>(x => x.ID.CompareTo(m_objSelectedWaybill.Customer.ID) == 0);
                ChildDepart.SelectedItem = (m_objSelectedWaybill.ChildDepart == null) ? null : ChildDepart.Properties.Items.Cast<CChildDepart>().Single<CChildDepart>(x => x.ID.CompareTo(m_objSelectedWaybill.ChildDepart.ID) == 0);
                PaymentType.SelectedItem = (m_objSelectedWaybill.PaymentType == null) ? ((PaymentType.Properties.Items.Count > 0) ? PaymentType.Properties.Items[0] : null) : (PaymentType.Properties.Items.Cast<CPaymentType>().Single<CPaymentType>(x => x.ID == m_objSelectedWaybill.PaymentType.ID));

                WaybillShipMode.SelectedItem = (m_objSelectedWaybill.WaybillShipMode == null) ? ((WaybillShipMode.Properties.Items.Count > 0) ? WaybillShipMode.Properties.Items[0] : null) : (WaybillShipMode.Properties.Items.Cast<CWaybillShipMode>().Single<CWaybillShipMode>(x => x.ID == m_objSelectedWaybill.WaybillShipMode.ID));
                WaybillState.SelectedItem = (m_objSelectedWaybill.WaybillState == null) ? ((WaybillState.Properties.Items.Count > 0) ? WaybillState.Properties.Items[0] : null) : (WaybillState.Properties.Items.Cast<CWaybillState>().Single<CWaybillState>(x => x.ID == m_objSelectedWaybill.WaybillState.ID));

                IsBonus.CheckState = ((m_objSelectedWaybill.IsBonus == true) ? CheckState.Checked : CheckState.Unchecked);
                Rtt.SelectedItem = (m_objSelectedWaybill.Rtt == null) ? null : Rtt.Properties.Items.Cast<CRtt>().Single<CRtt>(x => x.ID.CompareTo(m_objSelectedWaybill.Rtt.ID) == 0);
                AddressDelivery.SelectedItem = (m_objSelectedWaybill.AddressDelivery == null) ? null : AddressDelivery.Properties.Items.Cast<CAddress>().Single<CAddress>(x => x.ID.CompareTo(m_objSelectedWaybill.AddressDelivery.ID) == 0);
                Depart.SelectedItem = (m_objSelectedWaybill.Depart == null) ? null : Depart.Properties.Items.Cast<CDepart>().Single<CDepart>(x => x.uuidID.CompareTo(m_objSelectedWaybill.Depart.uuidID) == 0);
                SalesMan.SelectedItem = (m_objSelectedWaybill.SalesMan == null) ? null : SalesMan.Properties.Items.Cast<CSalesMan>().Single<CSalesMan>(x => x.ID.CompareTo(m_objSelectedWaybill.SalesMan.ID) == 0);
                Stock.SelectedItem = (m_objSelectedWaybill.Stock == null) ? null : Stock.Properties.Items.Cast<CStock>().Single<CStock>(x => x.ID.CompareTo(m_objSelectedWaybill.Stock.ID) == 0);

                ReloadDscrpnByAddress();

                cboxProductTradeMark.Properties.Items.Add(new CProductTradeMark() { ID = System.Guid.Empty, Name = "" });
                cboxProductTradeMark.Properties.Items.AddRange(CProductTradeMark.GetProductTradeMarkList(m_objProfile, null));
                cboxProductType.Properties.Items.Add(new CProductType() { ID = System.Guid.Empty, Name = "" });
                cboxProductType.Properties.Items.AddRange(CProductType.GetProductTypeList(m_objProfile, null));


                if (Stock.SelectedItem != null)
                {
                    m_uuidSelectedStockID = ((CStock)Stock.SelectedItem).ID;
                    m_objPartsList = COrderRepository.GetPartsInstockList(m_objProfile, null, m_uuidSelectedStockID, System.Guid.Empty);
                    if (m_objPartsList != null)
                    {
                        SetProductListToForm(m_objPartsList);
                    }
                }

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

                GetCustomerDebtInfo(true);
            }
            return;
        }

        #endregion

        #region Новая накладная (заказ переводится в накладную)
        /// <summary>
        /// Создание накладной на основе заказа
        /// </summary>
        /// <param name="Suppl_Guid">УИ заказа</param>
        public void NewWaybillFromSuppl(CWaybill objWaybill)
        {
            if (objWaybill == null) { return; }
            m_bDisableEvents = true;
            m_bNewObject = true;
            SetCreatePriceMode();
            try
            {
                System.String strErr = System.String.Empty;
                m_objSelectedWaybill = objWaybill;

                if (m_objSelectedWaybill.ChildDepart != null)
                {
                    m_objSelectedWaybill.ChildDepart = CChildDepart.GetChildDepart(m_objProfile, null, m_objSelectedWaybill.ChildDepart.ID);
                }

                //this.tableLayoutPanelBackground.SuspendLayout();

                ClearControls();


                BeginDate.DateTime = m_objSelectedWaybill.BeginDate;
                DeliveryDate.DateTime = m_objSelectedWaybill.DeliveryDate;
                if (System.DateTime.Compare(m_objSelectedWaybill.ShipDate, System.DateTime.MinValue) != 0)
                {
                    ShipDate.DateTime = m_objSelectedWaybill.ShipDate;
                }
                txtDescription.Text = m_objSelectedWaybill.Description;
                WaybilllNum.Text = m_objSelectedWaybill.DocNum;

                Customer.SelectedItem = (m_objSelectedWaybill.Customer == null) ? null : Customer.Properties.Items.Cast<CCustomer>().Single<CCustomer>(x => x.ID.CompareTo(m_objSelectedWaybill.Customer.ID) == 0);

                AddressDelivery.Properties.Items.Clear();

                LoadRttListForCustomer(m_objSelectedWaybill.Customer.ID);
                LoadChildDeprtForCustomer(m_objSelectedWaybill.Customer.ID);
                m_objSelectedWaybill.Customer.PhoneList = CPhone.GetPhoneListForCustomer(m_objProfile, null,
                    m_objSelectedWaybill.Customer.ID, ref strErr);


                Rtt.SelectedItem = (m_objSelectedWaybill.Rtt == null) ? null : Rtt.Properties.Items.Cast<CRtt>().Single<CRtt>(x => x.ID.CompareTo(m_objSelectedWaybill.Rtt.ID) == 0);
                if (Rtt.SelectedItem != null)
                {
                    AddressDelivery.Properties.Items.AddRange((((CRtt)Rtt.SelectedItem).AddressList));
                }

                ChildDepart.SelectedItem = (m_objSelectedWaybill.ChildDepart == null) ? null : ChildDepart.Properties.Items.Cast<CChildDepart>().SingleOrDefault<CChildDepart>(x => x.ID.CompareTo(m_objSelectedWaybill.ChildDepart.ID) == 0);
                PaymentType.SelectedItem = (m_objSelectedWaybill.PaymentType == null) ? null : PaymentType.Properties.Items.Cast<CPaymentType>().SingleOrDefault<CPaymentType>(x => x.ID.CompareTo(m_objSelectedWaybill.PaymentType.ID) == 0);
                WaybillShipMode.SelectedItem = (m_objSelectedWaybill.WaybillShipMode == null) ? null : WaybillShipMode.Properties.Items.Cast<CWaybillShipMode>().SingleOrDefault<CWaybillShipMode>(x => x.ID.CompareTo(m_objSelectedWaybill.WaybillShipMode.ID) == 0);
                WaybillState.SelectedItem = (m_objSelectedWaybill.WaybillState == null) ? null : WaybillState.Properties.Items.Cast<CWaybillState>().SingleOrDefault<CWaybillState>(x => x.ID.CompareTo(m_objSelectedWaybill.WaybillState.ID) == 0);

                IsBonus.CheckState = ((m_objSelectedWaybill.IsBonus == true) ? CheckState.Checked : CheckState.Unchecked);
                AddressDelivery.SelectedItem = (m_objSelectedWaybill.AddressDelivery == null) ? null : AddressDelivery.Properties.Items.Cast<CAddress>().SingleOrDefault<CAddress>(x => x.ID.CompareTo(m_objSelectedWaybill.AddressDelivery.ID) == 0);
                Depart.SelectedItem = (m_objSelectedWaybill.Depart == null) ? null : Depart.Properties.Items.Cast<CDepart>().SingleOrDefault<CDepart>(x => x.uuidID.CompareTo(m_objSelectedWaybill.Depart.uuidID) == 0);

                LoadSalesManListForDepart((CDepart)Depart.SelectedItem);

                SalesMan.SelectedItem = (m_objSelectedWaybill.SalesMan == null) ? null : SalesMan.Properties.Items.Cast<CSalesMan>().SingleOrDefault<CSalesMan>(x => x.uuidID.CompareTo(m_objSelectedWaybill.SalesMan.uuidID) == 0);
                Stock.SelectedItem = (m_objSelectedWaybill.Stock == null) ? null : Stock.Properties.Items.Cast<CStock>().SingleOrDefault<CStock>(x => x.ID.CompareTo(m_objSelectedWaybill.Stock.ID) == 0);

                cboxProductTradeMark.Properties.Items.Add(new CProductTradeMark() { ID = System.Guid.Empty, Name = "" });
                cboxProductTradeMark.Properties.Items.AddRange(CProductTradeMark.GetProductTradeMarkList(m_objProfile, null));
                cboxProductType.Properties.Items.Add(new CProductType() { ID = System.Guid.Empty, Name = "" });
                cboxProductType.Properties.Items.AddRange(CProductType.GetProductTypeList(m_objProfile, null));

                if (Stock.SelectedItem != null)
                {
                    m_uuidSelectedStockID = ((CStock)Stock.SelectedItem).ID;
                    m_objPartsList = COrderRepository.GetPartsInstockList(m_objProfile, null, m_uuidSelectedStockID, m_objSelectedWaybill.SupplID, true, true);
                    if (m_objPartsList != null)
                    {
                        SetProductListToForm(m_objPartsList);
                    }
                }

                dataSet.Tables["OrderItems"].Clear();
                System.Data.DataRow newRowOrderItems = null;
                CProduct objProduct = null;
                foreach (CWaybillItem objItem in m_objSelectedWaybill.WaybillItemList)
                {
                    newRowOrderItems = dataSet.Tables["OrderItems"].NewRow();

                    newRowOrderItems["OrderItemsID"] = objItem.ID;
                    newRowOrderItems["WaybItem_Id"] = objItem.Ib_ID;
                    newRowOrderItems["SupplItem_Guid"] = objItem.SupplItemID;

                    newRowOrderItems["ProductID"] = objItem.Product.ID;
                    newRowOrderItems["MeasureID"] = objItem.Measure.ID;
                    newRowOrderItems["Quantity"] = objItem.Quantity;
                    newRowOrderItems["QuantityReturned"] = objItem.QuantityReturn;

                    if (m_objPartsList != null)
                    {
                        try
                        {
                            objProduct = m_objPartsList.Single<CProduct>(x => x.ID.CompareTo(objItem.Product.ID) == 0);
                        }
                        catch
                        {
                            objProduct = null;
                        }
                        if (objProduct != null)
                        {
                            newRowOrderItems["OrderPackQty"] = objProduct.CustomerOrderMinRetailQty;
                            newRowOrderItems["OrderItems_QuantityInstock"] = objProduct.CustomerOrderStockQty;
                        }
                    }

                    newRowOrderItems["OrderItems_MeasureName"] = objItem.Measure.ShortName;
                    newRowOrderItems["OrderItems_PartsName"] = objItem.Product.Name;
                    newRowOrderItems["OrderItems_PartsArticle"] = objItem.Product.Article;
                    newRowOrderItems["PriceImporter"] = objItem.PriceImporter;
                    newRowOrderItems["Price"] = objItem.Price;
                    newRowOrderItems["DiscountPercent"] = objItem.DiscountPercent;
                    newRowOrderItems["PriceWithDiscount"] = objItem.PriceWithDiscount;
                    newRowOrderItems["NDSPercent"] = objItem.NDSPercent;
                    newRowOrderItems["PriceInAccountingCurrency"] = objItem.PriceInAccountingCurrency;
                    newRowOrderItems["PriceWithDiscountInAccountingCurrency"] = objItem.PriceWithDiscountInAccountingCurrency;

                    dataSet.Tables["OrderItems"].Rows.Add(newRowOrderItems);
                }
                newRowOrderItems = null;
                objProduct = null;
                dataSet.Tables["OrderItems"].AcceptChanges();

                ReloadDscrpnByAddress();

                SetPropertiesModified(true);
                
                btnCancel.Enabled = true;
                btnCancel.Focus();

                checkEditForStock.Visible = true;
                checkEditForStock.Checked = false;
                
                btnSave.Enabled =  ValidateProperties();

                SetModeNewWaybillFromSuppl();
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка формирования накладной из заказа. Текст ошибки: " + f.Message);
            }
            finally
            {
                //this.tableLayoutPanelBackground.ResumeLayout(false);
                m_bDisableEvents = false;

                GetCustomerDebtInfo(true);
                Cursor = Cursors.Default;
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
                SimulateChangeWaybillProperties(m_objSelectedWaybill, enumActionSaveCancel.Cancel, m_bNewObject, System.Guid.Empty);
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

        #region Сохранить изменения
        /// <summary>
        /// Сохраняет изменения в базе данных
        /// </summary>
        /// <returns>true - удачное завершение операции;false - ошибка</returns>
        private System.Boolean bSaveChanges(ref System.Guid SupplState_Guid, ref System.String strErr)
        {
            System.Boolean bRet = false;
            System.Boolean bOkSave = false;
            Cursor = Cursors.WaitCursor;
            try
            {

                System.DateTime Waybill_BeginDate = BeginDate.DateTime;
                System.DateTime Waybill_DeliveryDate = DeliveryDate.DateTime;
                System.DateTime Waybill_ShipDate = ShipDate.DateTime;

                System.Guid Suppl_Guid = m_objSelectedWaybill.SupplID;
                System.Guid WaybillState_Guid = ((WaybillState.SelectedItem == null) ? System.Guid.Empty : ((CWaybillState)WaybillState.SelectedItem).ID);
                System.Guid WaybillShipMode_Guid = ((WaybillShipMode.SelectedItem == null) ? System.Guid.Empty : ((CWaybillShipMode)WaybillShipMode.SelectedItem).ID);

                System.Boolean Waybill_Bonus = (this.IsBonus.CheckState == CheckState.Checked);
                System.Guid Depart_Guid = ((Depart.SelectedItem == null) ? (System.Guid.Empty) : ((CDepart)Depart.SelectedItem).uuidID);
                System.Guid uuidSalesman_Guid = ((SalesMan.SelectedItem == null) ? (System.Guid.Empty) : ((CSalesMan)SalesMan.SelectedItem).uuidID);
                System.Guid Customer_Guid = ((Customer.SelectedItem == null) ? (System.Guid.Empty) : ((CCustomer)Customer.SelectedItem).ID);
                System.Guid CustomerChild_Guid = ((ChildDepart.SelectedItem == null) ? (System.Guid.Empty) : ((CChildDepart)ChildDepart.SelectedItem).ID);

                System.Guid PaymentType_Guid = ((PaymentType.SelectedItem == null) ? (System.Guid.Empty) : ((CPaymentType)PaymentType.SelectedItem).ID);
                System.String Waybill_Description = txtDescription.Text;
                System.String Waybill_Num = WaybilllNum.Text;


                System.Guid Rtt_Guid = ((Rtt.SelectedItem == null) ? (System.Guid.Empty) : ((CRtt)Rtt.SelectedItem).ID);
                System.Guid Address_Guid = ((AddressDelivery.SelectedItem == null) ? (System.Guid.Empty) : ((CAddress)AddressDelivery.SelectedItem).ID);
                System.Guid Stock_Guid = ((Stock.SelectedItem == null) ? (System.Guid.Empty) : ((CStock)Stock.SelectedItem).ID);
                System.Guid Company_Guid = ((Stock.SelectedItem == null) ? (System.Guid.Empty) : ((CStock)Stock.SelectedItem).Company.ID);
                System.Boolean Waybill_ShowInDeliveryList = true;
                System.Double Waybill_CurrencyRate = m_objSelectedWaybill.PricingCurrencyRate;
                System.Guid WaybillParent_Guid = System.Guid.Empty;
                

                List<CWaybillItem> objItemList = new List<CWaybillItem>();
                System.Guid uuidItemID = System.Guid.Empty;
                System.Guid uuidSupplItemID = System.Guid.Empty;
                System.Guid uuidProductID = System.Guid.Empty;
                System.Guid uuidMeasureID = System.Guid.Empty;
                System.Double dblQuantity = 0;

                System.Double dblPriceImporter = 0;
                System.Double dblPrice = 0;
                System.Double dblDiscountPercent = 0;
                System.Double dblPriceWithDiscount = 0;
                System.Double dblNDSPercent = 0;
                System.Double dblPriceInAccountingCurrency = 0;
                System.Double dblPriceWithDiscountInAccountingCurrency = 0;

                dataSet.Tables["OrderItems"].AcceptChanges();

                for (System.Int32 i = 0; i < dataSet.Tables["OrderItems"].Rows.Count; i++)
                {
                    if ((dataSet.Tables["OrderItems"].Rows[i]["ProductID"] == System.DBNull.Value) ||
                        (dataSet.Tables["OrderItems"].Rows[i]["MeasureID"] == System.DBNull.Value) ||
                        (dataSet.Tables["OrderItems"].Rows[i]["Quantity"] == System.DBNull.Value))
                    {
                        continue;
                    }
                    if (m_bNewObject == true)
                    {
                        uuidItemID = System.Guid.NewGuid();
                    }
                    else
                    {
                        uuidItemID = ((dataSet.Tables["OrderItems"].Rows[i]["OrderItemsID"] == System.DBNull.Value) ? System.Guid.NewGuid() : (System.Guid)(dataSet.Tables["OrderItems"].Rows[i]["OrderItemsID"]));
                    }
                    uuidSupplItemID = (System.Guid)(dataSet.Tables["OrderItems"].Rows[i]["SupplItem_Guid"]);
                    uuidProductID = (System.Guid)(dataSet.Tables["OrderItems"].Rows[i]["ProductID"]);
                    uuidMeasureID = (System.Guid)(dataSet.Tables["OrderItems"].Rows[i]["MeasureID"]);
                    dblQuantity = System.Convert.ToDouble(dataSet.Tables["OrderItems"].Rows[i]["Quantity"]);
                    dblPriceImporter = System.Convert.ToDouble(dataSet.Tables["OrderItems"].Rows[i]["PriceImporter"]);
                    dblPrice = System.Convert.ToDouble(dataSet.Tables["OrderItems"].Rows[i]["Price"]);
                    dblDiscountPercent = System.Convert.ToDouble(dataSet.Tables["OrderItems"].Rows[i]["DiscountPercent"]);
                    dblPriceWithDiscount = System.Convert.ToDouble(dataSet.Tables["OrderItems"].Rows[i]["PriceWithDiscount"]);
                    dblNDSPercent = System.Convert.ToDouble(dataSet.Tables["OrderItems"].Rows[i]["NDSPercent"]);
                    dblPriceInAccountingCurrency = System.Convert.ToDouble(dataSet.Tables["OrderItems"].Rows[i]["PriceInAccountingCurrency"]);
                    dblPriceWithDiscountInAccountingCurrency = System.Convert.ToDouble(dataSet.Tables["OrderItems"].Rows[i]["PriceWithDiscountInAccountingCurrency"]);

                    objItemList.Add(new CWaybillItem()
                    {
                        ID = uuidItemID, 
                        SupplItemID = uuidSupplItemID,
                        Product = new CProduct() { ID = uuidProductID },
                        Measure = new CMeasure() { ID = uuidMeasureID },
                        Quantity = dblQuantity,
                        PriceImporter = dblPriceImporter,
                        Price = dblPrice,
                        DiscountPercent = dblDiscountPercent,
                        NDSPercent = dblNDSPercent,
                        PriceWithDiscount = dblPriceWithDiscount,
                        PriceInAccountingCurrency = dblPriceInAccountingCurrency,
                        PriceWithDiscountInAccountingCurrency = dblPriceWithDiscountInAccountingCurrency
                    });
                }
                System.Data.DataTable WaybillTablePart = ((gridControl.DataSource == null) ? null : CWaybillItem.ConvertListToTable(objItemList, ref strErr));
                objItemList = null;

                // проверка значений
                if (CWaybill.CheckAllPropertiesForSave(Suppl_Guid, Stock_Guid, Company_Guid, Depart_Guid,
                        Customer_Guid, Rtt_Guid, Address_Guid, PaymentType_Guid, Waybill_Num,
                        Waybill_BeginDate, Waybill_DeliveryDate, WaybillShipMode_Guid, WaybillTablePart, ref strErr) == true)
                {
                    if (m_bNewObject == true)
                    {
                        // новый накладная
                        System.Guid Waybill_Guid = System.Guid.Empty;
                        System.Int32 Waybill_Id = 0;

                        bOkSave = CWaybill.AddNewWaybillToDB(m_objProfile, null, Suppl_Guid, Stock_Guid,
                            Company_Guid, Depart_Guid, Customer_Guid, CustomerChild_Guid, Rtt_Guid, Address_Guid,
                            PaymentType_Guid, Waybill_Num, Waybill_BeginDate, Waybill_DeliveryDate, WaybillParent_Guid, Waybill_Bonus, 
                            WaybillState_Guid, WaybillShipMode_Guid, Waybill_ShipDate, 
                            Waybill_Description, Waybill_CurrencyRate, Waybill_ShowInDeliveryList, WaybillTablePart,
                            ref Waybill_Guid, ref Waybill_Id, ref SupplState_Guid, ref strErr, 
                            checkSetOrderInQueue.Checked, checkEditForStock.Checked);
                        if (bOkSave == true)
                        {
                            m_objSelectedWaybill.ID = Waybill_Guid;
                            m_objSelectedWaybill.Ib_ID = Waybill_Id;
                        }
                    }
                    //else
                    //{
                    //    bOkSave = COrderRepository.EditOrderInDB(m_objProfile, null, Order_BeginDate, OrderState_Guid,
                    //        Order_MoneyBonus, Depart_Guid, Salesman_Guid, Customer_Guid, CustomerChild_Guid, OrderType_Guid,
                    //        PaymentType_Guid, Order_Description, Order_DeliveryDate, Rtt_Guid, Address_Guid, Stock_Guid,
                    //        Parts_Guid, addedCategories, m_objSelectedOrder.ID, ref strErr);
                    //}
                }

                if (bOkSave == true)
                {
                    m_objSelectedWaybill.BeginDate = Waybill_BeginDate;
                    m_objSelectedWaybill.DeliveryDate = Waybill_DeliveryDate;
                    m_objSelectedWaybill.ShipDate = Waybill_ShipDate;
                    m_objSelectedWaybill.IsBonus = Waybill_Bonus;
                    m_objSelectedWaybill.Depart = ((Depart.SelectedItem == null) ? null : (CDepart)Depart.SelectedItem);
                    m_objSelectedWaybill.SalesMan = ((SalesMan.SelectedItem == null) ? null : (CSalesMan)SalesMan.SelectedItem);
                    m_objSelectedWaybill.Customer = ((Customer.SelectedItem == null) ? null : (CCustomer)Customer.SelectedItem);
                    m_objSelectedWaybill.ChildDepart = ((ChildDepart.SelectedItem == null) ? null : (CChildDepart)ChildDepart.SelectedItem);
                    m_objSelectedWaybill.WaybillState = ((WaybillState.SelectedItem == null) ? null : (CWaybillState)WaybillState.SelectedItem);
                    m_objSelectedWaybill.WaybillShipMode = ((WaybillShipMode.SelectedItem == null) ? null : (CWaybillShipMode)WaybillShipMode.SelectedItem);
                    m_objSelectedWaybill.PaymentType = ((PaymentType.SelectedItem == null) ? null : (CPaymentType)PaymentType.SelectedItem);
                    m_objSelectedWaybill.Description = txtDescription.Text;
                    m_objSelectedWaybill.DocNum = Waybill_Num;
                    m_objSelectedWaybill.DeliveryDate = DeliveryDate.DateTime;
                    m_objSelectedWaybill.Rtt = ((Rtt.SelectedItem == null) ? null : (CRtt)Rtt.SelectedItem);
                    m_objSelectedWaybill.AddressDelivery = ((AddressDelivery.SelectedItem == null) ? null : (CAddress)AddressDelivery.SelectedItem);
                    m_objSelectedWaybill.Stock = ((Stock.SelectedItem == null) ? null : (CStock)Stock.SelectedItem);
                }

                bRet = bOkSave;
            }
            catch (System.Exception f)
            {
                strErr = f.Message;
                SendMessageToLog("Ошибка сохранения изменений в заказе. Текст ошибки: " + f.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            return bRet;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkSetOrderInQueue.Checked == true)
                {
                    if (DevExpress.XtraEditors.XtraMessageBox.Show("В процессе сохранения накладной будет производиться резерв товара.\nПроцедура может занять длительное время.\nПодтвердите, пожалуйста, начало операции.", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.YesNoCancel, System.Windows.Forms.MessageBoxIcon.Information) != DialogResult.Yes)
                    {
                        return;
                    }
                }

                System.String strErr = System.String.Empty;
                System.Guid SupplState_Guid = System.Guid.Empty;
                if (bSaveChanges(ref SupplState_Guid, ref strErr) == true)
                {
                    SimulateChangeWaybillProperties(m_objSelectedWaybill, enumActionSaveCancel.Save, m_bNewObject, SupplState_Guid);
                    DevExpress.XtraEditors.XtraMessageBox.Show(strErr, "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(strErr, "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                }

            }
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка сохранения изменений в накладной. Текст ошибки: " + f.Message);
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
                row["Quantity"] = 1;
                row["QuantityReturned"] = 0;
                row["PriceImporter"] = 0;
                row["Price"] = 0;
                row["DiscountPercent"] = spinEditDiscount.Value;
                row["PriceWithDiscount"] = 0;
                row["NDSPercent"] = 0;
                row["PriceInAccountingCurrency"] = 0;
                row["PriceWithDiscountInAccountingCurrency"] = 0;
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
                    gridView.SetColumnError(colOrderItems_MeasureName, "укажите, пожалуйста, единицу измерения", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning);
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

                foreach (CWaybillItem objItem in m_objSelectedWaybill.WaybillItemList)
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

        #region Импорт приложения к накладной
        private void mitmsImportFromExcel_Click(object sender, EventArgs e)
        {
            try
            {
                ImportFromExcel();
            }
            catch (System.Exception f)
            {
                SendMessageToLog("ImportFromExcel. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }

        public void ImportFromExcel()
        {
            try
            {
                gridView.CellValueChanged -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gridView_CellValueChanged);

                if (Stock.SelectedItem == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Укажите, пожалуйста, склад отгрузки.", "Внимание",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    return;
                }
                List<System.String> DepartCodeList = new List<string>();
                for (System.Int32 i = 0; i < Depart.Properties.Items.Count; i++)
                {
                    DepartCodeList.Add(System.Convert.ToString(Depart.Properties.Items[i]));
                }

                CCustomer objSelectedCustomer = ((Customer.SelectedItem == null) ? null : (CCustomer)Customer.SelectedItem);
                CRtt objSelectedRtt = ((Rtt.SelectedItem == null) ? null : (CRtt)Rtt.SelectedItem);
                CDepart objSelectedDepart = ((Depart.SelectedItem == null) ? null : (CDepart)Depart.SelectedItem);
                CStock objSelectedStock = ((Stock.SelectedItem == null) ? null : (CStock)Stock.SelectedItem);
                CPaymentType objSelectedPaymentType = ((PaymentType.SelectedItem == null) ? null : (CPaymentType)PaymentType.SelectedItem);

                System.Int32 iPaymentType = ((objSelectedPaymentType == null) ? 0 : (objSelectedPaymentType.ID == m_iPaymentType1) ? 1 : 2);

                frmImportXLSData objFrmImportXLSData = new frmImportXLSData(m_objProfile, m_objMenuItem, DepartCodeList, m_objCustomerList);

                objFrmImportXLSData.OpenForImportPartsInSuppl(objSelectedCustomer, objSelectedRtt,
                    (objSelectedDepart == null ? "" : objSelectedDepart.DepartCode), iPaymentType, objSelectedStock, objSelectedPaymentType,
                    ((m_bIsAutoCreatePriceMode == true) ? 0 : System.Convert.ToDouble(spinEditDiscount.Value)), dataSet.Tables["OrderItems"], m_strXLSImportFilePath,
                    m_iXLSSheetImport, m_SheetList, checkMultiplicity.Checked);

                DialogResult dlgRes = objFrmImportXLSData.DialogResult;

                m_strXLSImportFilePath = objFrmImportXLSData.FileFullName;
                m_iXLSSheetImport = objFrmImportXLSData.SelectedSheetId;
                m_SheetList = objFrmImportXLSData.SheetList;

                if ((objFrmImportXLSData.SelectedCustomer != null) && (objSelectedCustomer != null) &&
                    (objFrmImportXLSData.SelectedCustomer.ID.CompareTo(objSelectedCustomer.ID) == 0) &&
                    (objFrmImportXLSData.SelectedRtt != null))
                {
                    Rtt.SelectedItem = Rtt.Properties.Items.Cast<CRtt>().Single<CRtt>(x => x.ID.CompareTo(objFrmImportXLSData.SelectedRtt.ID) == 0);
                    if (Rtt.SelectedItem != null)
                    {
                        AddressDelivery.Properties.Items.AddRange((((CRtt)Rtt.SelectedItem).AddressList));
                    }
                }

                if ((Customer.SelectedItem == null) && (objFrmImportXLSData.SelectedCustomer != null))
                {
                    Customer.SelectedItem = Customer.Properties.Items.Cast<CCustomer>().Single<CCustomer>(x => x.ID == objFrmImportXLSData.SelectedCustomer.ID);
                    Rtt.SelectedItem = Rtt.Properties.Items.Cast<CRtt>().Single<CRtt>(x => x.ID.CompareTo(objFrmImportXLSData.SelectedRtt.ID) == 0);
                    if (Rtt.SelectedItem != null)
                    {
                        AddressDelivery.Properties.Items.AddRange((((CRtt)Rtt.SelectedItem).AddressList));
                    }

                    if (((CPaymentType)PaymentType.SelectedItem).ID == m_iPaymentType2)
                    {
                        if (ChildDepart.Properties.Items.Count > 0)
                        {
                            ChildDepart.SelectedIndex = 0;
                        }
                    }

                    GetCustomerDebtInfo(true);
                }

                if ((Depart.SelectedItem == null) && (objFrmImportXLSData.DeparCode != ""))
                {
                    Depart.SelectedItem = Depart.Properties.Items.Cast<CDepart>().Single<CDepart>(x => x.DepartCode == objFrmImportXLSData.DeparCode);
                }

                objFrmImportXLSData.Dispose();
                objFrmImportXLSData = null;
                objSelectedCustomer = null;
                objSelectedRtt = null;
                objSelectedDepart = null;
                objSelectedStock = null;
            }
            catch (System.Exception f)
            {
                SendMessageToLog("ImportFromExcel. Текст ошибки: " + f.Message);
            }
            finally
            {
                gridView.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gridView_CellValueChanged);
                btnSave.Enabled = (ValidateProperties() == true);
            }
            return;
        }

        #endregion

        #region Печать
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (System.Exception f)
            {
                SendMessageToLog("btnPrint_Click. Текст ошибки: " + f.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
            return;
        }
        #endregion

        #region Информация о дебиторской задолженности
        private void ClearLabelsCriditControl()
        {
            try
            {
                lblDebtWaybillShipped.Text = "";
                lblDayDebtWaybillShipped.Text = "";
                LDebtWaybill.Text = "";
                LDebtSuppl.Text = "";
                LCustomerLimitMoney.Text = "";
                LCustomerLimitDays.Text = "";
                LOverdraftMoney.Text = "";
                LEarning.Text = "";
                LOutSumma.Text = "";
                LOutDays.Text = "";
                LCreditControlResult.Text = "";
            }
            catch (System.Exception f)
            {
                SendMessageToLog("ClearLabelsCriditControl. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }

        private void GetCustomerDebtInfo(System.Boolean bReloadInfoFromDB = false)
        {
            try
            {
                ClearLabelsCriditControl();

                if (Customer.SelectedItem == null) { return; }
                if (Stock.SelectedItem == null) { return; }
                if (m_bDisableEvents == true) { return; }

                // необходимо запросить состояние заказа
                // если заказ удален или отгружен, то запрашивать информацию о дебиторке смысла нет
                enPDASupplState enumOrderState = (enPDASupplState)m_objSelectedWaybill.WaybillState.WaybillStateId;

                if ((enumOrderState == enPDASupplState.Deleted) || (enumOrderState == enPDASupplState.Shipped) || (enumOrderState == enPDASupplState.TTN))
                {
                    tableLayoutPanelBackground.RowStyles[m_iDebtorInfoPanelIndex].Height = 0;
                    return;
                }
                else
                {
                    tableLayoutPanelBackground.RowStyles[m_iDebtorInfoPanelIndex].Height = m_iDebtorInfoPanelHeight;
                }


                System.Double WAYBILL_TOTALPRICE = System.Convert.ToDouble( colSumWithDiscount.SummaryItem.SummaryValue);
                System.Double WAYBILL_CURRENCYTOTALPRICE = System.Convert.ToDouble(colSumWithDiscountInAccountingCurrency.SummaryItem.SummaryValue);

                System.Boolean bRes = true;

                System.Double WAYBILL_SHIPPED_SALDO = 0;
                System.Int32 WAYBILL_SHIPPED_DEBTDAYS = 0;
                System.Double WAYBILL_SALDO = 0;
                System.Int32 WAYBILL_DEBTDAYS = 0;
                System.Double INITIAL_DEBT = 0;
                System.Int32 INITIAL_DEBTDAYS = 0;
                System.Double SUPPL_SALDO = 0;
                System.Double EARNING_SALDO = 0;
                System.Double CUSTOMER_LIMITPRICE = 0;
                System.Int32 CUSTOMER_LIMITDAYS = 0;
                System.Double OVERDRAFT = 0;
                System.Double CUSTOMER_DEBTPRICE = 0;
                System.Int32 CUSTOMER_DEBTDAYS = 0;

                System.String strErr = "";
                System.Double dblOutSumma = 0;
                System.Int32 iOutDays = 0;
                System.Boolean bCustomerInBlackList = false;

                System.Guid PaymentType_Guid = ((PaymentType.SelectedItem == null) ? (System.Guid.Empty) : ((CPaymentType)PaymentType.SelectedItem).ID);

                if (bReloadInfoFromDB == true)
                {
                    // скорее всего выбрали другого клиента или склад, поэтому информацию о задолженности нужно обновить
                    System.Guid Order_Guid = m_objSelectedWaybill.ID;
                    System.Guid OrderState_Guid = ((m_objSelectedWaybill.WaybillState == null) ? System.Guid.Empty : m_objSelectedWaybill.WaybillState.ID);
                    System.Boolean Order_MoneyBonus = (this.IsBonus.CheckState == CheckState.Checked);
                    System.Guid Customer_Guid = ((Customer.SelectedItem == null) ? (System.Guid.Empty) : ((CCustomer)Customer.SelectedItem).ID);
                    System.Guid CustomerChild_Guid = ((ChildDepart.SelectedItem == null) ? (System.Guid.Empty) : ((CChildDepart)ChildDepart.SelectedItem).ID);
                    System.Guid Company_Guid = ((Stock.SelectedItem == null) ? (System.Guid.Empty) : ((CStock)Stock.SelectedItem).Company.ID);
                    m_SUMM_IS_PASS = 0;

                    bRes = COrderRepository.GetCustomerDebtInfoFromIB(m_objProfile, null, Customer_Guid, CustomerChild_Guid,
                        PaymentType_Guid, Company_Guid, Order_Guid, WAYBILL_TOTALPRICE, WAYBILL_CURRENCYTOTALPRICE,
                        Order_MoneyBonus, ref WAYBILL_SHIPPED_SALDO, ref WAYBILL_SHIPPED_DEBTDAYS, ref WAYBILL_SALDO,
                        ref WAYBILL_DEBTDAYS, ref INITIAL_DEBT, ref INITIAL_DEBTDAYS, ref SUPPL_SALDO, ref EARNING_SALDO,
                        ref CUSTOMER_LIMITPRICE, ref CUSTOMER_LIMITDAYS, ref OVERDRAFT, ref CUSTOMER_DEBTPRICE,
                        ref CUSTOMER_DEBTDAYS, ref m_SUMM_IS_PASS, ref strErr);

                    if (bRes == true)
                    {
                        lblDebtWaybillShipped.Tag = WAYBILL_SHIPPED_SALDO;
                        lblDayDebtWaybillShipped.Tag = WAYBILL_SHIPPED_DEBTDAYS;
                        LDebtWaybill.Tag = WAYBILL_SALDO;
                        LDebtSuppl.Tag = SUPPL_SALDO;
                        LCustomerLimitMoney.Tag = CUSTOMER_LIMITPRICE;
                        LCustomerLimitDays.Tag = CUSTOMER_LIMITDAYS;
                        LOverdraftMoney.Tag = OVERDRAFT;
                        LEarning.Tag = EARNING_SALDO;
                        LOutSumma.Tag = CUSTOMER_DEBTPRICE;
                        LOutDays.Tag = CUSTOMER_DEBTDAYS;

                        if (COrderRepository.IsCustomerInBL(m_objProfile, null, Customer_Guid, Company_Guid,
                            ref bCustomerInBlackList, ref strErr) == false)
                        {
                            SendMessageToLog("IsCustomerInBL. Текст ошибки: " + strErr);
                        }
                        else
                        {
                            m_bCustomerInBlackList = bCustomerInBlackList;
                        }

                    }
                    else
                    {
                        SendMessageToLog("GetCustomerDebtInfo. Текст ошибки: " + strErr);
                    }
                }
                else
                {
                    // зачитываем информацию о дебиторке непосредственно из элементов управления
                    WAYBILL_SHIPPED_SALDO = ((lblDebtWaybillShipped.Tag != null) ? System.Convert.ToDouble(lblDebtWaybillShipped.Tag) : 0);
                    WAYBILL_SHIPPED_DEBTDAYS = ((lblDayDebtWaybillShipped.Tag != null) ? System.Convert.ToInt32(lblDayDebtWaybillShipped.Tag) : 0);
                    WAYBILL_SALDO = ((LDebtWaybill.Tag != null) ? System.Convert.ToDouble(LDebtWaybill.Tag) : 0);
                    SUPPL_SALDO = ((LDebtSuppl.Tag != null) ? System.Convert.ToDouble(LDebtSuppl.Tag) : 0);
                    CUSTOMER_LIMITPRICE = ((LCustomerLimitMoney.Tag != null) ? System.Convert.ToDouble(LCustomerLimitMoney.Tag) : 0);
                    CUSTOMER_LIMITDAYS = ((LCustomerLimitDays.Tag != null) ? System.Convert.ToInt32(LCustomerLimitDays.Tag) : 0);
                    OVERDRAFT = ((LOverdraftMoney.Tag != null) ? System.Convert.ToDouble(LOverdraftMoney.Tag) : 0);
                    EARNING_SALDO = ((LEarning.Tag != null) ? System.Convert.ToDouble(LEarning.Tag) : 0);
                    CUSTOMER_DEBTPRICE = ((LOutSumma.Tag != null) ? System.Convert.ToDouble(LOutSumma.Tag) : 0);
                    CUSTOMER_DEBTDAYS = ((LOutDays.Tag != null) ? System.Convert.ToInt32(LOutDays.Tag) : 0);
                }

                // рисуем итоги
                lblDebtWaybillShipped.Text = System.String.Format("{0:### ### ##0}", WAYBILL_SHIPPED_SALDO);
                lblDayDebtWaybillShipped.Text = System.String.Format("{0:### ### ##0}", WAYBILL_SHIPPED_DEBTDAYS);
                LDebtWaybill.Text = System.String.Format("{0:### ### ##0}", WAYBILL_SALDO);
                LDebtSuppl.Text = System.String.Format("{0:### ### ##0}", SUPPL_SALDO);
                LCustomerLimitMoney.Text = System.String.Format("{0:### ### ##0}", CUSTOMER_LIMITPRICE);
                LCustomerLimitDays.Text = System.String.Format("{0:### ### ##0}", CUSTOMER_LIMITDAYS);
                LOverdraftMoney.Text = System.String.Format("{0:### ### ##0}", OVERDRAFT);
                LEarning.Text = System.String.Format("{0:### ### ##0}", EARNING_SALDO);

                if (PaymentType_Guid == m_iPaymentType2)
                {
                    dblOutSumma = (CUSTOMER_LIMITPRICE + OVERDRAFT) - (WAYBILL_CURRENCYTOTALPRICE + Math.Abs(CUSTOMER_DEBTPRICE));
                }
                else
                {
                    dblOutSumma = (CUSTOMER_LIMITPRICE + OVERDRAFT) - (WAYBILL_TOTALPRICE + Math.Abs(CUSTOMER_DEBTPRICE));
                }
                iOutDays = (CUSTOMER_LIMITDAYS - Math.Abs(CUSTOMER_DEBTDAYS));

                if (dblOutSumma > 0) { dblOutSumma = 0; }
                if (iOutDays > 0) { iOutDays = 0; }

                if (bReloadInfoFromDB == false)
                {
                    m_SUMM_IS_PASS = (((dblOutSumma == 0) && (iOutDays == 0)) ? 1 : 0);
                }

                if ((m_SUMM_IS_PASS == 1) && (m_bCustomerInBlackList == false))
                {
                    LCreditControlResult.Text = "заказ проходит кредитный контроль";
                    LCreditControlResult.ForeColor = Color.Green;
                    ImageOk.Visible = true;
                    ImageNot.Visible = false;
                }
                else
                {
                    if (m_bCustomerInBlackList == true)
                    { LCreditControlResult.Text = "клиент в черном списке"; }
                    else
                    { LCreditControlResult.Text = "кредитные условия не выполняются"; }

                    LCreditControlResult.ForeColor = Color.Red;
                    ImageOk.Visible = false;
                    ImageNot.Visible = true;
                }

                LOutSumma.Text = System.String.Format("{0:### ### ##0}", dblOutSumma);
                LOutDays.Text = iOutDays.ToString();
                if (dblOutSumma < 0)
                { LOutSumma.ForeColor = Color.Red; }
                else
                { LOutSumma.ForeColor = Color.Green; }
                if (iOutDays < 0)
                { LOutDays.ForeColor = Color.Red; }
                else
                { LOutDays.ForeColor = Color.Green; }

            }
            catch (System.Exception f)
            {
                SendMessageToLog("GetCustomerDebtInfo. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }
        #endregion

        #region Установка режима расчёта цен
        /// <summary>
        /// Установка режима расчёта цен
        /// </summary>
        private void SetCreatePriceMode()
        {
            try
            {
                m_bIsAutoCreatePriceMode = COrderRepository.IsAutoCreatePriceMode(m_objProfile);

                checkEditCalcPrices.Checked = m_bIsAutoCreatePriceMode;
                checkEditCalcPrices.Enabled = (m_bIsAutoCreatePriceMode == false);
                spinEditDiscount.Enabled = (m_bIsAutoCreatePriceMode == false);
                btnSetDiscount.Enabled = (m_bIsAutoCreatePriceMode == false);
            }
            catch (System.Exception f)
            {
                SendMessageToLog("SetCreatePriceMode. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }
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
        
        #region Экспорт в MS Excel

        private void ExportToExcelWaybitmsList_1(string strFileName, string strSheetName, string strDocName,
            List<string> ColumnCaptionList, List<WaybillItemForExport> WaybillItemList)
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
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(strSheetName);

                    System.Int32 iColumnIndex = 1;
                    foreach (string strColumnName in ColumnCaptionList)
                    {
                        worksheet.Cells[1, iColumnIndex].Value = strColumnName;
                        iColumnIndex++;
                    }

                    using (var range = worksheet.Cells[1, 1, 1, iColumnIndex])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Font.Size = 12;
                    }

                    System.Int32 iCurrentRow = 2;
                    worksheet.Cells[iCurrentRow, 1].Value = strDocName;

                    iCurrentRow++;
                    System.Int32 iRowsCount = WaybillItemList.Count;

                    foreach (WaybillItemForExport objWaybillItemForExport in WaybillItemList)
                    {
                        worksheet.Cells[iCurrentRow, 1].Value = objWaybillItemForExport.PARTS_FULLNAME;
                        worksheet.Cells[iCurrentRow, 2].Value = objWaybillItemForExport.COUNTRY_NAME;
                        worksheet.Cells[iCurrentRow, 3].Value = objWaybillItemForExport.MEASURE_SHORTNAME;
                        worksheet.Cells[iCurrentRow, 4].Value = objWaybillItemForExport.WAYBITMS_QUANTITY;
                        worksheet.Cells[iCurrentRow, 5].Value = (objWaybillItemForExport.WAYBITMS_TOTALPRICEWITHOUTNDS / objWaybillItemForExport.WAYBITMS_QUANTITY);
                        //worksheet.Cells[iCurrentRow, 5].Value = objWaybillItemForExport.WAYBITMS_BASEPRICE;
                        worksheet.Cells[iCurrentRow, 6].Value = objWaybillItemForExport.WAYBITMS_DOUBLEPERCENT;
                        worksheet.Cells[iCurrentRow, 7].Value = objWaybillItemForExport.WAYBITMS_TOTALPRICEWITHOUTNDS;
                        worksheet.Cells[iCurrentRow, 8].Value = objWaybillItemForExport.WAYBITMS_NDS;
                        worksheet.Cells[iCurrentRow, 9].Value = objWaybillItemForExport.WAYBITMS_NDSTOTALPRICE;
                        worksheet.Cells[iCurrentRow, 10].Value = objWaybillItemForExport.WAYBITMS_TOTALPRICE;
                        worksheet.Cells[iCurrentRow, 11].Value = objWaybillItemForExport.WAYBITMS_TARA;
                        worksheet.Cells[iCurrentRow, 12].Value = objWaybillItemForExport.WAYBITMS_QTYINPLACE;
                        worksheet.Cells[iCurrentRow, 13].Value = 0;
                        worksheet.Cells[iCurrentRow, 14].Value = objWaybillItemForExport.WAYBITMS_WEIGHT;
                        worksheet.Cells[iCurrentRow, 15].Value = System.String.Empty;
                        worksheet.Cells[iCurrentRow, 16].Value = objWaybillItemForExport.Parts_Barcode;
                        worksheet.Cells[iCurrentRow, 17].Value = System.String.Empty;
                        worksheet.Cells[iCurrentRow, 18].Value = System.String.Empty;
                        worksheet.Cells[iCurrentRow, 19].Value = objWaybillItemForExport.PARTS_CERTIFICATE;
                        worksheet.Cells[iCurrentRow, 20].Value = System.String.Empty;
                        worksheet.Cells[iCurrentRow, 21].Value = System.String.Empty;
                        
                        iCurrentRow++;
                    }



                    iCurrentRow--;
                    worksheet.Cells[1, 1, iCurrentRow, 21].AutoFitColumns(0);
                    //worksheet.Cells[1, 1, iCurrentRow, 21].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    //worksheet.Cells[1, 1, iCurrentRow, 21].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    //worksheet.Cells[1, 1, iCurrentRow, 21].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    //worksheet.Cells[1, 1, iCurrentRow, 21].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                   // worksheet.PrinterSettings.FitToWidth = 1;

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

        private void ExportToExcelWaybitmsList_1()
        {
            try
            {
                System.String strErr = System.String.Empty;

                Cursor = Cursors.WaitCursor;
                List<WaybillItemForExport> WaybillItemList = CWaybillItem.GetWaybillTablePartForExportToExcel(m_objProfile, m_objSelectedWaybill.ID, ref strErr);
                if ((WaybillItemList == null) || (WaybillItemList.Count == 0))
                {
                    Cursor = Cursors.Default;
                    DevExpress.XtraEditors.XtraMessageBox.Show(strErr, "Внимание!",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
                else
                {
                    System.String strFileName = String.Format("{0}{1}.xlsx", System.IO.Path.GetTempPath(), m_objSelectedWaybill.DocNum);
                    System.String strSheetName = m_objSelectedWaybill.DocNum;
                    System.String strDocName = String.Format("Накладная  {0} от {1}", m_objSelectedWaybill.DocNum, m_objSelectedWaybill.BeginDate.ToShortDateString());
                    List<string> ColumnCaptionList = new List<string>();
                    ColumnCaptionList.Add("NAIM");
                    ColumnCaptionList.Add("STRANA");
                    ColumnCaptionList.Add("ED_IZM");
                    ColumnCaptionList.Add("KOL_VO");
                    ColumnCaptionList.Add("CENA");
                    ColumnCaptionList.Add("OPT_NADB");
                    ColumnCaptionList.Add("STOIM");
                    ColumnCaptionList.Add("STAVKA_NDS");
                    ColumnCaptionList.Add("SUMA_NDS");
                    ColumnCaptionList.Add("ITOGO");
                    ColumnCaptionList.Add("VID_TARY");
                    ColumnCaptionList.Add("KOL_MEST");
                    ColumnCaptionList.Add("KOL_NA_MES");
                    ColumnCaptionList.Add("BRUTTO");
                    ColumnCaptionList.Add("KOD");
                    ColumnCaptionList.Add("BAR_KOD");
                    ColumnCaptionList.Add("UDOST");
                    ColumnCaptionList.Add("VET");
                    ColumnCaptionList.Add("DECLAR");
                    ColumnCaptionList.Add("SERTIF");
                    ColumnCaptionList.Add("GGR");

                    ExportToExcelWaybitmsList_1(strFileName, strSheetName, strDocName, ColumnCaptionList, WaybillItemList);
                }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("ExportToExcelWaybitmsList_1. Текст ошибки: " + f.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            return;
        }

        private void mitmsExportToXLS_Click(object sender, EventArgs e)
        {
            try
            {
                ExportToExcelWaybitmsList_1();
            }
            catch (System.Exception f)
            {
                SendMessageToLog("mitmsExportToXLS_Click. Текст ошибки: " + f.Message);
            }

            return;

        }

        #endregion

    }

    /// <summary>
    /// Тип, хранящий информацию, которая передается получателям уведомления о событии
    /// </summary>
    public class ChangeWaybillPropertieEventArgs : EventArgs
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

        private readonly System.Guid m_uuidOrderCurrentStateGuid;
        public System.Guid OrderCurrentStateGuid
        { get { return m_uuidOrderCurrentStateGuid; } }

        public ChangeWaybillPropertieEventArgs(CWaybill objWaybill, enumActionSaveCancel enActionType,
            System.Boolean bIsNewWaybill, System.Guid uuidOrderCurrentStateGuid)
        {
            m_objWaybill = objWaybill;
            m_enActionType = enActionType;
            m_bIsNewWaybill = bIsNewWaybill;
            m_uuidOrderCurrentStateGuid = uuidOrderCurrentStateGuid;
        }
    }


}

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
using DevExpress.XtraExport;
using DevExpress.XtraGrid.Export;
using DevExpress.XtraEditors;
using System.Data.OleDb;
using OfficeOpenXml;

namespace ERPMercuryProcessingOrder
{
    public partial class ctrlIntOrderEditor : UserControl
    {
        #region Свойства
        private UniXP.Common.CProfile m_objProfile;
        private UniXP.Common.MENUITEM m_objMenuItem;
        private List<CProduct> m_objPartsList;

        private CIntOrder m_objSelectedOrder;
        private System.Guid m_uuidSelectedSrcStockID;
        private System.Guid m_uuidSelectedDstStockID;
        private System.Boolean m_bIsChanged;

        private System.Boolean m_bDisableEvents;
        private System.Boolean m_bNewObject;
        private System.Boolean m_bIsReadOnly;
        private System.String m_strXLSImportFilePath;
        private System.Int32 m_iXLSSheetImport;
        private List<System.String> m_SheetList;

        // потоки
        public System.Threading.Thread ThreadLoadOrderTablePart {get; set;}

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
        private const System.String m_strReportSuppl = "IntOrder.xlsx";
        private const System.Int32 m_iDebtorInfoPanelHeight = 117;
        private const System.Int32 m_iDebtorInfoPanelIndex = 1;
        #endregion

        #region Конструктор
        public ctrlIntOrderEditor(UniXP.Common.MENUITEM objMenuItem)
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
            m_uuidSelectedSrcStockID = System.Guid.Empty;
            m_uuidSelectedDstStockID = System.Guid.Empty;

            m_objSelectedOrder = null;
            m_objPartsList = null;
            m_iPaymentType1 = new Guid(m_strPaymentType1);
            m_iPaymentType2 = new Guid(m_strPaymentType2);
            m_strXLSImportFilePath = "";
            m_iXLSSheetImport = 0;

            BeginDate.DateTime = System.DateTime.Today;

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

        #region События
        // Создаем закрытое поле, ссылающееся на заголовок списка делегатов
        private EventHandler<ChangeIntOrderPropertieEventArgs> m_ChangeIntOrderProperties;
        // Создаем в классе член-событие
        public event EventHandler<ChangeIntOrderPropertieEventArgs> ChangeIntOrderProperties
        {
            add
            {
                // берем закрытую блокировку и добавляем обработчик
                // (передаваемый по значению) в список делегатов
                m_ChangeIntOrderProperties += value;
            }
            remove
            {
                // берем закрытую блокировку и удаляем обработчик
                // (передаваемый по значению) из списка делегатов
                m_ChangeIntOrderProperties -= value;
            }
        }
        /// <summary>
        /// Инициирует событие и уведомляет о нем зарегистрированные объекты
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnChangeIntOrderProperties(ChangeIntOrderPropertieEventArgs e)
        {
            // Сохраняем поле делегата во временном поле для обеспечение безопасности потока
            EventHandler<ChangeIntOrderPropertieEventArgs> temp = m_ChangeIntOrderProperties;
            // Если есть зарегистрированные объектв, уведомляем их
            if (temp != null) temp(this, e);
        }
        public void SimulateChangeIntOrderProperties(CIntOrder objOrder, enumActionSaveCancel enActionType, System.Boolean bIsNewOrder)
        {
            // Создаем объект, хранящий информацию, которую нужно передать
            // объектам, получающим уведомление о событии
            ChangeIntOrderPropertieEventArgs e = new ChangeIntOrderPropertieEventArgs(objOrder, enActionType, bIsNewOrder);

            // Вызываем виртуальный метод, уведомляющий наш объект о возникновении события
            // Если нет типа, переопределяющего этот метод, наш объект уведомит все объекты, 
            // подписавшиеся на уведомление о событии
            OnChangeIntOrderProperties(e);
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
        /// Проверка наличия значений в полях ввода
        /// Поля, обязательные к заполнению помечаются цветом
        /// </summary>
        private void CheckFillInEditors()
        {
            try
            {
                ShipMode.Properties.Appearance.BackColor = ((ShipMode.SelectedItem == null) ? System.Drawing.Color.Tomato : System.Drawing.Color.White);
                PaymentType.Properties.Appearance.BackColor = ((PaymentType.SelectedItem == null) ? System.Drawing.Color.Tomato : System.Drawing.Color.White);
                Depart.Properties.Appearance.BackColor = ((Depart.SelectedItem == null) ? System.Drawing.Color.Tomato : System.Drawing.Color.White);
                SalesMan.Properties.Appearance.BackColor = ((SalesMan.SelectedItem == null) ? System.Drawing.Color.Tomato : System.Drawing.Color.White);
                StockSrc.Properties.Appearance.BackColor = ((StockSrc.SelectedItem == null) ? System.Drawing.Color.Tomato : System.Drawing.Color.White);
                StockDst.Properties.Appearance.BackColor = ((StockDst.SelectedItem == null) ? System.Drawing.Color.Tomato : System.Drawing.Color.White);
                BeginDate.Properties.Appearance.BackColor = ((BeginDate.EditValue == null) ? System.Drawing.Color.Tomato : System.Drawing.Color.White);
                ShipMode.Properties.Appearance.BackColor = ((ShipMode.SelectedItem == null) ? System.Drawing.Color.Tomato : System.Drawing.Color.White);
                DocState.Properties.Appearance.BackColor = ((DocState.SelectedItem == null) ? System.Drawing.Color.Tomato : System.Drawing.Color.White);
                cboxPriceType.Properties.Appearance.BackColor = (((cboxPriceType.SelectedItem == null) && (m_bIsReadOnly == false)) ? System.Drawing.Color.Tomato : System.Drawing.Color.White);
            }
            catch (System.Exception f)
            {
                SendMessageToLog("CheckFillInEditors. Текст ошибки: " + f.Message);
            }
            return;
        }

        /// <summary>
        /// Проверяет содержимое элементов управления
        /// </summary>
        /// <returns>true - все обязательные поля заполнены; false - не все обязательные значения указаны</returns>
        private System.Boolean ValidateProperties()
        {
            System.Boolean bRet = true;
            try
            {
                if (ShipMode.SelectedItem == null) { bRet = false; }
                if (PaymentType.SelectedItem == null) { bRet = false; }
                if (SalesMan.SelectedItem == null) { bRet = false; }
                if (Depart.SelectedItem == null) { bRet = false; }
                if (StockSrc.SelectedItem == null) { bRet = false; }
                if (StockDst.SelectedItem == null) { bRet = false; }
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
                if (m_bIsChanged == true)
                {
                    SimulateChangeIntOrderProperties(m_objSelectedOrder, enumActionSaveCancel.Unkown, m_bNewObject);
                }

                CheckFillInEditors();
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

                if ((sender == StockSrc) && (StockSrc.SelectedItem != null))
                {
                    SetDefParamForOrder();

                    m_uuidSelectedSrcStockID = ((CStock)StockSrc.SelectedItem).ID;
                    StartThreadWithLoadData();
                }


                if (sender == Depart)
                {
                    LoadSalesManListForDepart((CDepart)Depart.SelectedItem);
                }


                if ((sender == cboxProductTradeMark) || (sender == cboxProductType))
                {
                    SetFilterForPartsCombo();
                }


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
        private void SetPriceInRow(System.Int32 iRowHandle, System.Guid uuidPartsId, System.Guid uuidStockId, 
            System.Guid uuidPriceTypeId, System.Double dblDiscountPercent )
        {
            try
            {
                if (uuidPriceTypeId.CompareTo(System.Guid.Empty) == 0)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                        "Укажите, пожалуйста, уровень цены для заказа.", "Внимание",
                         System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    cboxPriceType.Focus();

                    return;
                }
                
                System.Double PriceImporter = 0;
                System.Double Price = 0;
                System.Double PriceWithDiscount = 0;
                System.Double NDSPercent = 0;
                System.Double MarkUpPercent = 0;
                System.Double PriceRetail = 0;
                System.String strErr = System.String.Empty;

                if (CIntOrder.GetPriceForOrderItem(m_objProfile, uuidPartsId, uuidStockId, uuidPriceTypeId, dblDiscountPercent,
                    ref PriceImporter, ref Price, ref PriceWithDiscount, ref PriceRetail, ref NDSPercent, ref MarkUpPercent, ref strErr) == true)
                {
                    gridView.SetRowCellValue(iRowHandle, colNDSPercent, NDSPercent);
                    gridView.SetRowCellValue(iRowHandle, colMarkUp, MarkUpPercent);
                    gridView.SetRowCellValue(iRowHandle, colPriceImporter, PriceImporter);
                    gridView.SetRowCellValue(iRowHandle, colPrice, Price);
                    gridView.SetRowCellValue(iRowHandle, colPriceWithDiscount, PriceWithDiscount);
                    gridView.SetRowCellValue(iRowHandle, colPriceRetail, PriceRetail);
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

                        gridView.SetRowCellValue(e.RowHandle, colNDSPercent, objItem.ProductType.NDSRate);
                        gridView.SetRowCellValue(e.RowHandle, colOrderPackQty, objItem.CustomerOrderMinRetailQty);
                        gridView.SetRowCellValue(e.RowHandle, colOrderItems_PartsArticle, objItem.Article);
                        gridView.SetRowCellValue(e.RowHandle, colOrderItems_PartsName, objItem.Name);

                        System.Guid uuidStockId = ((StockSrc.SelectedItem == null) ? System.Guid.Empty : ((CStock)StockSrc.SelectedItem).ID);
                        System.Guid uuidPaymentTypeId = ((PaymentType.SelectedItem == null) ? System.Guid.Empty : ((CPaymentType)PaymentType.SelectedItem).ID);
                        System.Guid uuidPartsubtypePriceTypeId = ((cboxPriceType.SelectedItem == null) ? System.Guid.Empty : (( CPriceType )cboxPriceType.SelectedItem).ID); 
                        if ((uuidStockId != System.Guid.Empty) && (uuidPaymentTypeId != System.Guid.Empty))
                        {
                            SetPriceInRow(e.RowHandle, objItem.ID, uuidStockId, uuidPartsubtypePriceTypeId, 
                                System.Convert.ToDouble(gridView.GetRowCellValue(e.RowHandle, colDiscountPercent)));
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
                    System.Guid uuidStockId = ((StockSrc.SelectedItem == null) ? System.Guid.Empty : ((CStock)StockSrc.SelectedItem).ID);
                    System.Guid uuidPaymentTypeId = ((PaymentType.SelectedItem == null) ? System.Guid.Empty : ((CPaymentType)PaymentType.SelectedItem).ID);
                    if ((uuidStockId != System.Guid.Empty) && (uuidPaymentTypeId != System.Guid.Empty))
                    {
                        SetPriceInRow(e.RowHandle, (System.Guid)gridView.GetRowCellValue(e.RowHandle, colProductID), uuidStockId, uuidPaymentTypeId, System.Convert.ToDouble(gridView.GetRowCellValue(e.RowHandle, colDiscountPercent)));
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


        private void gridView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
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
                System.Guid uuidStockId = ((StockSrc.SelectedItem == null) ? System.Guid.Empty : ((CStock)StockSrc.SelectedItem).ID);
                System.Guid uuidPartsubtypePriceTypeId = ((cboxPriceType.SelectedItem == null) ? System.Guid.Empty : ((CPriceType)cboxPriceType.SelectedItem).ID);
                if ((uuidStockId != System.Guid.Empty) && (uuidPartsubtypePriceTypeId != System.Guid.Empty))
                {
                    for (System.Int32 i = 0; i < gridView.RowCount; i++)
                    {
                        gridView.SetRowCellValue(i, colDiscountPercent, dblDiscountPercent);
                        SetPriceInRow(i, (System.Guid)gridView.GetRowCellValue(i, colProductID), uuidStockId, uuidPartsubtypePriceTypeId, 
                            System.Convert.ToDouble(gridView.GetRowCellValue(i, colDiscountPercent)));
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
                if (cboxPriceType.SelectedItem == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                        "Укажите, пожалуйста, уровень цены.", "Внимание!",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                    cboxPriceType.Focus();
                    return;
                }
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

        /// <summary>
        /// Установка реквизитов при оформлении нового заказа
        /// </summary>
        private void SetDefParamForOrder()
        {
            try
            {
                System.Guid In_PaymentType_Guid = ((PaymentType.SelectedItem == null) ? System.Guid.Empty : ((CPaymentType)PaymentType.SelectedItem).ID);

                System.Boolean SetDepartValue = false;
                System.Guid Depart_Guid = System.Guid.Empty;
                System.Guid PaymentType_Guid = System.Guid.Empty;
                System.String strErr = System.String.Empty;

                if (CIntOrder.GetOrderDefParams(m_objProfile, In_PaymentType_Guid,  ref SetDepartValue, 
                    ref Depart_Guid, ref PaymentType_Guid, ref strErr) == true)
                {
                    m_bDisableEvents = true;
                    if (In_PaymentType_Guid.CompareTo(PaymentType_Guid) != 0)
                    {
                        PaymentType.SelectedItem = PaymentType.Properties.Items.Cast<CPaymentType>().SingleOrDefault<CPaymentType>(x => x.ID.CompareTo(PaymentType_Guid) == 0);
                    }
                    if (SetDepartValue == true)
                    {
                        Depart.SelectedItem = Depart.Properties.Items.Cast<CDepart>().SingleOrDefault<CDepart>(x => x.uuidID.CompareTo(Depart_Guid) == 0);
                    }

                    m_bDisableEvents = false;
                }

            }

            catch (System.Exception f)
            {
                SendMessageToLog("SetDefParamForOrder. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }
        private void cboxPriceType_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                CheckFillInEditors();

                if ((cboxPriceType.SelectedItem != null) && (m_bIsReadOnly == false))
                {
                    RecalcPricesForPriceType( ( (CPriceType)cboxPriceType.SelectedItem ).ID );
                }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("cboxPriceType_SelectedValueChanged. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }

        /// <summary>
        /// Устанавливает вид цены на все позиции в заказе
        /// </summary>
        /// <param name="uuidPartsubtypePriceTypeId">УИ уровня цены</param>
        private void RecalcPricesForPriceType(System.Guid uuidPartsubtypePriceTypeId)
        {
            if (gridView.RowCount == 0) { return; }

            try
            {
                tableLayoutPanelBackground.SuspendLayout();

                Cursor = Cursors.WaitCursor;
                
                System.Guid uuidStockId = ((StockSrc.SelectedItem == null) ? System.Guid.Empty : ((CStock)StockSrc.SelectedItem).ID);
                if ((uuidStockId != System.Guid.Empty) && (uuidPartsubtypePriceTypeId != System.Guid.Empty))
                {
                    for (System.Int32 i = 0; i < gridView.RowCount; i++)
                    {
                        SetPriceInRow(i, (System.Guid)gridView.GetRowCellValue(i, colProductID), uuidStockId, 
                            uuidPartsubtypePriceTypeId, System.Convert.ToDouble(gridView.GetRowCellValue(i, colDiscountPercent)));
                    }
                }

            }
            catch (System.Exception f)
            {
                SendMessageToLog("RecalcPricesForPriceType. Текст ошибки: " + f.Message);
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

                Depart.Properties.Items.Clear();
                cboxProductTradeMark.Properties.Items.Clear();
                cboxProductType.Properties.Items.Clear();

                // Склады
                StockSrc.Properties.Items.Clear();
                StockDst.Properties.Items.Clear();

                StockSrc.Properties.Items.AddRange(CStock.GetStockList(m_objProfile, null)); //.Where<CStock>(x=>x.IsTrade == true).ToList<CStock>());
                StockDst.Properties.Items.AddRange(StockSrc.Properties.Items);

                // Форма оплаты
                PaymentType.Properties.Items.Clear();
                PaymentType.Properties.Items.AddRange(CPaymentType.GetPaymentTypeList(m_objProfile, null, System.Guid.Empty));

                // Подразделение
                Depart.Properties.Items.Clear();
                Depart.Properties.Items.AddRange(CDepart.GetDepartList(m_objProfile, null));

                // Товары
                dataSet.Tables["Product"].Clear();
                repositoryItemLookUpEditProduct.DataSource = dataSet.Tables["Product"];

                // Вид отгрузки
                ShipMode.Properties.Items.Clear();
                ShipMode.Properties.Items.AddRange( CIntOrderShipMode.GetIntOrderShipModeList( m_objProfile, ref strErr ) );

                // Состояния заказа
                DocState.Properties.Items.Clear();
                DocState.Properties.Items.AddRange( CIntOrderState.GetIntOrderStateList( m_objProfile, ref strErr ) );

                // Уровень цен
                cboxPriceType.Properties.Items.Clear();
                cboxPriceType.Properties.Items.AddRange(CPriceType.GetPriceTypeList(m_objProfile, null));

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

                    if ((SalesMan.SelectedItem != null) && (m_objSelectedOrder != null))
                    {
                        m_objSelectedOrder.SalesMan = (CSalesMan)SalesMan.SelectedItem;
                    }
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

                List<CProduct> FilteredProductList = m_objPartsList;

                if (TradeMarkId != System.Guid.Empty)
                {
                    FilteredProductList = FilteredProductList.Where<CProduct>(x => x.ProductTradeMark.ID == TradeMarkId).ToList<CProduct>();
                }

                if (PartTypeId != System.Guid.Empty)
                {
                    FilteredProductList = FilteredProductList.Where<CProduct>(x => x.ProductType.ID == PartTypeId).ToList<CProduct>();
                }

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
                    newRowProduct["OrderStockQty"] = objItem.CustomerOrderStockQty;
                    newRowProduct["OrderResQty"] = objItem.CustomerOrderResQty;
                    newRowProduct["OrderPackQty"] = objItem.CustomerOrderPackQty;

                    dataSet.Tables["Product"].Rows.Add(newRowProduct);
                }
                newRowProduct = null;
                dataSet.Tables["Product"].AcceptChanges();

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
                    newRowProduct["StockQty"] = objItem.CustomerOrderStockQty;
                    newRowProduct["ResQty"] = objItem.CustomerOrderResQty;
                    newRowProduct["PackQty"] = objItem.CustomerOrderPackQty;

                    dataSet.Tables["Product"].Rows.Add(newRowProduct);
                }
                newRowProduct = null;
                dataSet.Tables["Product"].AcceptChanges();

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
                m_objPartsList = COrderRepository.GetPartsInstockList( m_objProfile, null, m_uuidSelectedSrcStockID, System.Guid.Empty );
                if (m_objPartsList != null)
                {
                    this.Invoke( m_SetProductListToFormDelegate, new Object[] { m_objPartsList } );
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
                System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ru-RU") ;
                ci.NumberFormat.CurrencyDecimalSeparator = ".";
                ci.NumberFormat.NumberDecimalSeparator = ".";

                // делаем событиям reset
                this.m_EventStopThread.Reset();
                this.m_EventThreadStopped.Reset();

                this.ThreadLoadOrderTablePart = new System.Threading.Thread(WorkerThreadFunction);
                this.ThreadLoadOrderTablePart.CurrentCulture = ci;
                this.ThreadLoadOrderTablePart.Start();
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
                    ((ThreadLoadOrderTablePart != null) && (ThreadLoadOrderTablePart.IsAlive == true))
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
                    if( ( ThreadLoadOrderTablePart != null ) && ( ThreadLoadOrderTablePart.IsAlive == true ) )
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
                Description.Properties.ReadOnly = bSet;
                DocNum.Properties.ReadOnly = bSet;

                StockSrc.Properties.ReadOnly = bSet;
                StockDst.Properties.ReadOnly = bSet;
                ShipMode.Properties.ReadOnly = bSet;
                DocState.Properties.ReadOnly = bSet;
                PaymentType.Properties.ReadOnly = bSet;
                Depart.Properties.ReadOnly = bSet;
                SalesMan.Properties.ReadOnly = bSet;

                cboxProductTradeMark.Properties.ReadOnly = bSet;
                cboxProductType.Properties.ReadOnly = bSet;
                spinEditDiscount.Properties.ReadOnly = bSet;
                cboxPriceType.Properties.ReadOnly = bSet;

                checkSetOrderInQueue.Enabled = !bSet;

                gridView.OptionsBehavior.Editable = !bSet;
                controlNavigator.Enabled = !bSet;
                btnSetDiscount.Enabled = !bSet;

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

        #region Редактирование заказа
        /// <summary>
        /// очистка содержимого элементов управления
        /// </summary>
        private void ClearControls()
        {
            try
            {
                BeginDate.EditValue = null;
                ShipDate.EditValue = null;
                Description.Text = System.String.Empty;
                DocNum.Text = System.String.Empty;

                StockSrc.SelectedItem = null;
                StockDst.SelectedItem = null;
                PaymentType.SelectedItem = null;
                ShipMode.SelectedItem = null;
                Depart.SelectedItem = null;
                SalesMan.SelectedItem = null;
                cboxProductTradeMark.SelectedItem = null;
                cboxProductType.SelectedItem = null;
                spinEditDiscount.Value = 0;
                cboxPriceType.SelectedItem = null;

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
        /// Загружает свойства заказа для редактирования
        /// </summary>
        /// <param name="objOrder">заказ</param>
        /// <param name="bNewObject">признак "новый заказ"</param>
        /// <param name="bOnlyShowMode">признак "отобразить только для просмотра"</param>
        public void EditOrder(CIntOrder objOrder, System.Boolean bNewObject, System.Boolean bOnlyShowMode = false)
        {
            if (objOrder == null) { return; }
            m_bDisableEvents = true;
            m_bNewObject = bNewObject;
            try
            {
                m_objSelectedOrder = objOrder;

                this.tableLayoutPanelBackground.SuspendLayout();

                ClearControls();

                BeginDate.DateTime = m_objSelectedOrder.BeginDate;
                if(m_objSelectedOrder.ShipDate.CompareTo(System.DateTime.MinValue) == 0)
                {
                    ShipDate.EditValue = null;
                }
                else
                {
                    ShipDate.DateTime = m_objSelectedOrder.ShipDate;
                }
                DocNum.Text = m_objSelectedOrder.DocNum;
                Description.Text = m_objSelectedOrder.Description;

                PaymentType.SelectedItem = (m_objSelectedOrder.PaymentType == null) ? null : PaymentType.Properties.Items.Cast<CPaymentType>().SingleOrDefault<CPaymentType>(x => x.ID.CompareTo(m_objSelectedOrder.PaymentType.ID) == 0);
                ShipMode.SelectedItem = (m_objSelectedOrder.IntOrderShipMode == null) ? null : ShipMode.Properties.Items.Cast<CIntOrderShipMode>().SingleOrDefault<CIntOrderShipMode>(x => x.ID.CompareTo(m_objSelectedOrder.IntOrderShipMode.ID) == 0);
                DocState.SelectedItem = (m_objSelectedOrder.DocState == null) ? null : DocState.Properties.Items.Cast<CIntOrderState>().SingleOrDefault<CIntOrderState>(x => x.ID.CompareTo(m_objSelectedOrder.DocState.ID) == 0);

                Depart.SelectedItem = (m_objSelectedOrder.Depart == null) ? null : Depart.Properties.Items.Cast<CDepart>().SingleOrDefault<CDepart>(x => x.uuidID.CompareTo(m_objSelectedOrder.Depart.uuidID) == 0);
                LoadSalesManListForDepart((CDepart)Depart.SelectedItem);
                SalesMan.SelectedItem = (m_objSelectedOrder.SalesMan == null) ? null : SalesMan.Properties.Items.Cast<CSalesMan>().SingleOrDefault<CSalesMan>(x => x.uuidID.CompareTo(m_objSelectedOrder.SalesMan.uuidID) == 0);

                StockSrc.SelectedItem = (m_objSelectedOrder.StockSrc == null) ? null : StockSrc.Properties.Items.Cast<CStock>().SingleOrDefault<CStock>(x => x.ID.CompareTo(m_objSelectedOrder.StockSrc.ID) == 0);
                StockDst.SelectedItem = (m_objSelectedOrder.StockDst == null) ? null : StockDst.Properties.Items.Cast<CStock>().SingleOrDefault<CStock>(x => x.ID.CompareTo(m_objSelectedOrder.StockDst.ID) == 0);

                cboxProductTradeMark.Properties.Items.Add(new CProductTradeMark() { ID = System.Guid.Empty, Name = "" });
                cboxProductTradeMark.Properties.Items.AddRange(CProductTradeMark.GetProductTradeMarkList(m_objProfile, null));
                cboxProductType.Properties.Items.Add(new CProductType() { ID = System.Guid.Empty, Name = "" });
                cboxProductType.Properties.Items.AddRange(CProductType.GetProductTypeList(m_objProfile, null));

                if (StockSrc.SelectedItem != null)
                {
                    m_objPartsList = COrderRepository.GetPartsInstockList( m_objProfile, null, ((CStock)StockSrc.SelectedItem).ID, m_objSelectedOrder.ID, false, false, 1 );
                    if (m_objPartsList != null)
                    {
                        SetProductListToForm(m_objPartsList);
                    }
                }

                dataSet.Tables["OrderItems"].Clear();
                System.Data.DataRow newRowOrderItems = null;
                CProduct objProduct = null;
                foreach (CIntOrderItem objItem in m_objSelectedOrder.IntOrderItemList)
                {
                    newRowOrderItems = dataSet.Tables["OrderItems"].NewRow();

                    newRowOrderItems["OrderItemsID"] = objItem.ID;
                    newRowOrderItems["ProductID"] = objItem.Product.ID;
                    newRowOrderItems["MeasureID"] = objItem.Measure.ID;
                    newRowOrderItems["Quantity"] = objItem.Quantity;
                    newRowOrderItems["QuantityReturned"] = objItem.QuantityReturned;

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
                            //newRowOrderItems["OrderPackQty"] = objProduct.CustomerOrderMinRetailQty;
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
                    newRowOrderItems["PriceRetail"] = objItem.PriceRetail;
                    newRowOrderItems["NDSPercent"] = objItem.NDSPercent;
                    newRowOrderItems["MarkUp"] = objItem.MarkUpPercent;

                    dataSet.Tables["OrderItems"].Rows.Add(newRowOrderItems);
                }
                newRowOrderItems = null;
                objProduct = null;
                dataSet.Tables["OrderItems"].AcceptChanges();

                SetPropertiesModified(false);
                btnCancel.Enabled = true;
                btnCancel.Focus();
                ValidateProperties();

                SetModeReadOnly(true);

                btnEdit.Visible = (bOnlyShowMode == false);
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка редактирования заказа на внутреннее перемещение. Текст ошибки: " + f.Message);
            }
            finally
            {
                this.tableLayoutPanelBackground.ResumeLayout(false);
                m_bDisableEvents = false;
            }
            return;
        }


        #endregion

        #region Копирование заказа
        /// <summary>
        /// Копирует свойства заказа в новый заказ и открывает его для редактирования
        /// </summary>
        /// <param name="objOrder">заказ</param>
        public void CopyOrder(CIntOrder objOrder, ref System.String strErr )
        {
            if (objOrder == null) { return; }

            try
            {
                m_objSelectedOrder = new CIntOrder()
                {
                    BeginDate = System.DateTime.Today,
                    Description = objOrder.Description,
                    PaymentType = objOrder.PaymentType,
                    IntOrderShipMode = objOrder.IntOrderShipMode,
                    Depart = objOrder.Depart,
                    SalesMan = objOrder.SalesMan,
                    CompanySrc = objOrder.CompanySrc,
                    StockSrc = objOrder.StockSrc,
                    CompanyDst = objOrder.CompanyDst,
                    StockDst = objOrder.StockDst
                };

                m_objSelectedOrder.IntOrderItemList = CIntOrderItem.GetIntOrderTablePart(m_objProfile, objOrder.ID, ref strErr);
                m_objSelectedOrder.DocState = ((DocState.Properties.Items.Count > 0) ? (CIntOrderState)DocState.Properties.Items[0] : null);

                EditOrder(m_objSelectedOrder, true);

                // в заказе возможно есть позиции, которых нет в остатке выбранного склада
                if ((m_objSelectedOrder.IntOrderItemList != null) && (m_objSelectedOrder.IntOrderItemList.Count > 0 ) )
                {
                    List<CIntOrderItem> objList = new List<CIntOrderItem>();
                    CProduct objProduct = null;
                    if( ( m_objPartsList != null ) && ( m_objPartsList.Count > 0 ) )
                    {
                        foreach (CIntOrderItem objItem in m_objSelectedOrder.IntOrderItemList)
                        {
                            if (objItem.Product == null) { continue; }
                            objProduct = m_objPartsList.SingleOrDefault<CProduct>(x => x.ID.CompareTo(objItem.Product.ID) == 0);

                            if (objProduct != null)
                            {
                                objItem.QuantityReturned = 0;
                                objItem.Quantity = ((objProduct.CustomerOrderStockQty >= objItem.Quantity) ? objItem.Quantity : objProduct.CustomerOrderStockQty);
                                objList.Add(objItem);
                            }
                        }

                        m_objSelectedOrder.IntOrderItemList = objList;
                        objProduct = null;
                    }

                    dataSet.Tables["OrderItems"].Clear();
                    System.Data.DataRow newRowOrderItems = null;
                    foreach (CIntOrderItem objItem in m_objSelectedOrder.IntOrderItemList)
                    {
                        newRowOrderItems = dataSet.Tables["OrderItems"].NewRow();

                        newRowOrderItems["OrderItemsID"] = objItem.ID;
                        newRowOrderItems["ProductID"] = objItem.Product.ID;
                        newRowOrderItems["MeasureID"] = objItem.Measure.ID;
                        newRowOrderItems["Quantity"] = objItem.Quantity;
                        newRowOrderItems["QuantityReturned"] = objItem.QuantityReturned;

                        newRowOrderItems["OrderItems_MeasureName"] = objItem.Measure.ShortName;
                        newRowOrderItems["OrderItems_PartsName"] = objItem.Product.Name;
                        newRowOrderItems["OrderItems_PartsArticle"] = objItem.Product.Article;
                        newRowOrderItems["PriceImporter"] = objItem.PriceImporter;
                        newRowOrderItems["Price"] = objItem.Price;
                        newRowOrderItems["DiscountPercent"] = objItem.DiscountPercent;
                        newRowOrderItems["PriceWithDiscount"] = objItem.PriceWithDiscount;
                        newRowOrderItems["PriceRetail"] = objItem.PriceRetail;
                        newRowOrderItems["NDSPercent"] = objItem.NDSPercent;
                        newRowOrderItems["MarkUp"] = objItem.MarkUpPercent;

                        dataSet.Tables["OrderItems"].Rows.Add(newRowOrderItems);
                    }
                    newRowOrderItems = null;
                    dataSet.Tables["OrderItems"].AcceptChanges();

                }


                SetModeReadOnly(false);

                gridControl.RefreshDataSource();
                gridView.RefreshData();

            }
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка копирования заказа клиенту. Текст ошибки: " + f.Message);
            }
            finally
            {
                m_bNewObject = true;
                m_objSelectedOrder.ID = System.Guid.Empty;
                btnSave.Enabled = (ValidateProperties() == true);
            }
            return;
        }

        #endregion

        #region Новый заказ
        /// <summary>
        /// Устанавливает в элементах управления значения для нового заказа
        /// </summary>
        /// <param name="objSrcCompany">компания-источник</param>
        /// <param name="objSrcStock">склад-источник</param>
        /// <param name="objDstCompany">компания-назначение</param>
        /// <param name="objDstStock">склад-назначение</param>
        public void NewOrder(CCompany objSrcCompany, CStock objSrcStock, CCompany objDstCompany, CStock objDstStock)
        {
            try
            {
                m_bNewObject = true;
                m_bDisableEvents = true;

                m_objSelectedOrder = new CIntOrder()
                {
                    BeginDate = System.DateTime.Today,
                    IntOrderItemList = new List<CIntOrderItem>(),
                    CompanySrc = objSrcCompany,  StockSrc = objSrcStock, 
                    CompanyDst = objDstCompany, StockDst = objDstStock
                };

                this.tableLayoutPanelBackground.SuspendLayout();

                ClearControls();

                BeginDate.DateTime = m_objSelectedOrder.BeginDate;
                ShipDate.EditValue = null;

                PaymentType.SelectedItem = (m_objSelectedOrder.PaymentType == null) ? ((PaymentType.Properties.Items.Count > 0) ? PaymentType.Properties.Items[0] : null) : (PaymentType.Properties.Items.Cast<CPaymentType>().Single<CPaymentType>(x => x.ID == m_objSelectedOrder.PaymentType.ID));
                ShipMode.SelectedItem = (m_objSelectedOrder.IntOrderShipMode == null) ? ((ShipMode.Properties.Items.Count > 0) ? ShipMode.Properties.Items[0] : null) : (ShipMode.Properties.Items.Cast<CIntOrderShipMode>().Single<CIntOrderShipMode>(x => x.ID == m_objSelectedOrder.IntOrderShipMode.ID));
                DocState.SelectedItem = (m_objSelectedOrder.DocState == null) ? ((DocState.Properties.Items.Count > 0) ? DocState.Properties.Items[0] : null) : (DocState.Properties.Items.Cast<CIntOrderState>().Single<CIntOrderState>(x => x.ID == m_objSelectedOrder.DocState.ID));

                Depart.SelectedItem = (m_objSelectedOrder.Depart == null) ? null : Depart.Properties.Items.Cast<CDepart>().SingleOrDefault<CDepart>(x => x.uuidID.CompareTo(m_objSelectedOrder.Depart.uuidID) == 0);
                LoadSalesManListForDepart((CDepart)Depart.SelectedItem);
                SalesMan.SelectedItem = (m_objSelectedOrder.SalesMan == null) ? null : SalesMan.Properties.Items.Cast<CSalesMan>().SingleOrDefault<CSalesMan>(x => x.uuidID.CompareTo(m_objSelectedOrder.SalesMan.uuidID) == 0);

                StockSrc.SelectedItem = (m_objSelectedOrder.StockSrc == null) ? null : StockSrc.Properties.Items.Cast<CStock>().SingleOrDefault<CStock>(x => x.ID.CompareTo(m_objSelectedOrder.StockSrc.ID) == 0);
                StockDst.SelectedItem = (m_objSelectedOrder.StockDst == null) ? null : StockDst.Properties.Items.Cast<CStock>().SingleOrDefault<CStock>(x => x.ID.CompareTo(m_objSelectedOrder.StockDst.ID) == 0);

                cboxProductTradeMark.Properties.Items.Add(new CProductTradeMark() { ID = System.Guid.Empty, Name = "" });
                cboxProductTradeMark.Properties.Items.AddRange(CProductTradeMark.GetProductTradeMarkList(m_objProfile, null));
                cboxProductType.Properties.Items.Add(new CProductType() { ID = System.Guid.Empty, Name = "" });
                cboxProductType.Properties.Items.AddRange(CProductType.GetProductTypeList(m_objProfile, null));

                if (StockSrc.SelectedItem != null)
                {
                    m_objPartsList = COrderRepository.GetPartsInstockList(m_objProfile, null, ((CStock)StockSrc.SelectedItem).ID, m_objSelectedOrder.ID, false, false, 1);
                    if (m_objPartsList != null)
                    {
                        SetProductListToForm(m_objPartsList);
                    }
                }
                
                cboxProductTradeMark.Properties.Items.Add(new CProductTradeMark() { ID = System.Guid.Empty, Name = "" });
                cboxProductTradeMark.Properties.Items.AddRange(CProductTradeMark.GetProductTradeMarkList(m_objProfile, null));
                cboxProductType.Properties.Items.Add(new CProductType() { ID = System.Guid.Empty, Name = "" });
                cboxProductType.Properties.Items.AddRange(CProductType.GetProductTypeList(m_objProfile, null));


                if (StockSrc.SelectedItem != null)
                {
                    m_objPartsList = COrderRepository.GetPartsInstockList(m_objProfile, null, ((CStock)StockSrc.SelectedItem).ID, System.Guid.Empty);
                    if (m_objPartsList != null)
                    {
                        SetProductListToForm(m_objPartsList);
                    }
                }

                btnEdit.Enabled = false;
                btnCancel.Enabled = true;
                btnCancel.Focus();

                SetModeReadOnly(false);

                SetDefParamForOrder();

                SetPropertiesModified(true);
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка создания заказа. Текст ошибки: " + f.Message);
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
                SimulateChangeIntOrderProperties(m_objSelectedOrder, enumActionSaveCancel.Cancel, m_bNewObject);
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
        private System.Boolean bSaveChanges(ref System.String strErr)
        {
            System.Boolean bRet = false;
            System.Boolean bOkSave = false;
            Cursor = Cursors.WaitCursor;
            try
            {

                System.DateTime Doc_BeginDate = BeginDate.DateTime;
                System.Guid Depart_Guid = ((Depart.SelectedItem == null) ? (System.Guid.Empty) : ((CDepart)Depart.SelectedItem).uuidID);
                System.Guid Salesman_Guid = ((SalesMan.SelectedItem == null) ? (System.Guid.Empty) : ((CSalesMan)SalesMan.SelectedItem).uuidID);
                System.Guid DocShipMode_Guid = ((ShipMode.SelectedItem == null) ? (System.Guid.Empty) : ((CIntOrderShipMode)ShipMode.SelectedItem).ID);
                System.Guid PaymentType_Guid = ((PaymentType.SelectedItem == null) ? (System.Guid.Empty) : ((CPaymentType)PaymentType.SelectedItem).ID);
                System.String Doc_Description = Description.Text;
                System.String Doc_Num = DocNum.Text;
                System.DateTime Doc_ShipDate = ShipDate.DateTime;
                System.Guid StockSrc_Guid = ((StockSrc.SelectedItem == null) ? (System.Guid.Empty) : ((CStock)StockSrc.SelectedItem).ID);
                System.Guid StockDst_Guid = ((StockDst.SelectedItem == null) ? (System.Guid.Empty) : ((CStock)StockDst.SelectedItem).ID);
                System.Guid DocParent_Guid = m_objSelectedOrder.ParentID;

                List<CIntOrderItem> objOrderItemList = new List<CIntOrderItem>();
                System.Guid uuidOrderItmsID = System.Guid.Empty;
                System.Guid uuidProductID = System.Guid.Empty;
                System.Guid uuidMeasureID = System.Guid.Empty;
                System.Double dblQuantity = 0;

                System.Double dblPriceImporter = 0;
                System.Double dblPrice = 0;
                System.Double dblDiscountPercent = 0;
                System.Double dblPriceWithDiscount = 0;
                System.Double dblPriceRetail = 0;
                System.Double dblNDSPercent = 0;
                System.Double dblMarkUpPercent = 0;

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
                        uuidOrderItmsID = System.Guid.NewGuid();
                    }
                    else
                    {
                        uuidOrderItmsID = ((dataSet.Tables["OrderItems"].Rows[i]["OrderItemsID"] == System.DBNull.Value) ? System.Guid.NewGuid() : (System.Guid)(dataSet.Tables["OrderItems"].Rows[i]["OrderItemsID"]));
                    }
                    uuidProductID = (System.Guid)(dataSet.Tables["OrderItems"].Rows[i]["ProductID"]);
                    uuidMeasureID = (System.Guid)(dataSet.Tables["OrderItems"].Rows[i]["MeasureID"]);
                    dblQuantity = System.Convert.ToDouble(dataSet.Tables["OrderItems"].Rows[i]["Quantity"]);
                    dblPriceImporter = System.Convert.ToDouble(dataSet.Tables["OrderItems"].Rows[i]["PriceImporter"]);
                    dblPrice = System.Convert.ToDouble(dataSet.Tables["OrderItems"].Rows[i]["Price"]);
                    dblDiscountPercent = System.Convert.ToDouble(dataSet.Tables["OrderItems"].Rows[i]["DiscountPercent"]);
                    dblPriceWithDiscount = System.Convert.ToDouble(dataSet.Tables["OrderItems"].Rows[i]["PriceWithDiscount"]);
                    dblPriceRetail = System.Convert.ToDouble(dataSet.Tables["OrderItems"].Rows[i]["PriceRetail"]);
                    dblNDSPercent = System.Convert.ToDouble(dataSet.Tables["OrderItems"].Rows[i]["NDSPercent"]);
                    dblMarkUpPercent = System.Convert.ToDouble(dataSet.Tables["OrderItems"].Rows[i]["MarkUp"]);

                    objOrderItemList.Add(new CIntOrderItem()
                    {
                        ID = uuidOrderItmsID,
                        Product = new CProduct() { ID = uuidProductID },
                        Measure = new CMeasure() { ID = uuidMeasureID },
                        Quantity = dblQuantity,
                        PriceImporter = dblPriceImporter,
                        Price = dblPrice,
                        DiscountPercent = dblDiscountPercent,
                        PriceWithDiscount = dblPriceWithDiscount,
                        PriceRetail = dblPriceRetail,
                        NDSPercent = dblNDSPercent,
                        MarkUpPercent = dblMarkUpPercent
                    });
                }
                System.Data.DataTable DocTablePart = ((gridControl.DataSource == null) ? null : CIntOrderItem.ConvertListToTable(objOrderItemList, ref strErr));
                objOrderItemList = null;

                // проверка значений
                if (CIntOrder.CheckAllPropertiesForSave(Doc_BeginDate, Doc_Num, DocShipMode_Guid, PaymentType_Guid, 
                    StockSrc_Guid, StockDst_Guid, Depart_Guid, Salesman_Guid, DocTablePart, ref strErr) == true)
                {
                    if (m_bNewObject == true)
                    {
                        // новый заказ
                        System.Guid Doc_Guid = System.Guid.Empty;
                        System.Int32 Doc_Id = 0;
                        System.Guid DocState_Guid = System.Guid.Empty;

                        bOkSave = CIntOrder.AddNewDocToDB(m_objProfile, Doc_BeginDate, Doc_Num, Doc_ShipDate, 
                            DocShipMode_Guid, PaymentType_Guid, StockSrc_Guid, StockDst_Guid, 
                            Depart_Guid, Salesman_Guid, Doc_Description, DocParent_Guid, DocTablePart, ref strErr, ref Doc_Guid, ref Doc_Id, ref DocState_Guid ); 
                        if (bOkSave == true)
                        {
                            m_objSelectedOrder.ID = Doc_Guid;
                            m_objSelectedOrder.Ib_ID = Doc_Id;
                        }
                    }
                    else
                    {
                        System.Guid Doc_Guid = m_objSelectedOrder.ID;
                        System.Guid DocState_Guid = ((m_objSelectedOrder.DocState == null) ? System.Guid.Empty : m_objSelectedOrder.DocState.ID);

                        bOkSave = CIntOrder.EditDocInDB(m_objProfile, Doc_Guid, DocState_Guid, Doc_BeginDate, Doc_Num, Doc_ShipDate,
                            DocShipMode_Guid, PaymentType_Guid, StockSrc_Guid, StockDst_Guid,
                            Depart_Guid, Salesman_Guid, Doc_Description, DocParent_Guid, DocTablePart, ref strErr);
                    }
                }

                if (bOkSave == true)
                {
                    m_objSelectedOrder.BeginDate = Doc_BeginDate;
                    m_objSelectedOrder.Depart = ((Depart.SelectedItem == null) ? null : (CDepart)Depart.SelectedItem);
                    m_objSelectedOrder.SalesMan = ((SalesMan.SelectedItem == null) ? null : (CSalesMan)SalesMan.SelectedItem);
                    m_objSelectedOrder.StockSrc = ((StockSrc.SelectedItem == null) ? null : (CStock)StockSrc.SelectedItem);
                    m_objSelectedOrder.StockDst = ((StockDst.SelectedItem == null) ? null : (CStock)StockDst.SelectedItem);
                    m_objSelectedOrder.IntOrderShipMode = ((ShipMode.SelectedItem == null) ? null : (CIntOrderShipMode)ShipMode.SelectedItem);
                    m_objSelectedOrder.PaymentType = ((PaymentType.SelectedItem == null) ? null : (CPaymentType)PaymentType.SelectedItem);
                    m_objSelectedOrder.Description = Description.Text;
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
                    if (DevExpress.XtraEditors.XtraMessageBox.Show("В процессе сохранения заказа будет производиться резерв товара.\nПроцедура может занять длительное время.\nПодтвердите, пожалуйста, начало операции.", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.YesNoCancel, System.Windows.Forms.MessageBoxIcon.Information) != DialogResult.Yes)
                    {
                        return;
                    }
                }

                System.String strErr = "";
                if (bSaveChanges(ref strErr) == true)
                {
                    SimulateChangeIntOrderProperties(m_objSelectedOrder, enumActionSaveCancel.Save, m_bNewObject);
                    System.String strInfo = ((checkSetOrderInQueue.Checked == true) ? "Заказ на внутреннее перемещение помещен в очередь на обработку." : "Заказ на внутреннее перемещение успешно сохранен.");

                    DevExpress.XtraEditors.XtraMessageBox.Show(strInfo, "Внимание",
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
                SendMessageToLog("Ошибка сохранения заказа. Текст ошибки: " + f.Message);
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
                row["PriceImporter"] = 0;
                row["Price"] = 0;
                row["DiscountPercent"] = spinEditDiscount.Value;
                row["PriceWithDiscount"] = 0;
                row["PriceRetail"] = 0;
                row["NDSPercent"] = 0;
                row["MarkUp"] = 0;
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

        #region Экспорт приложения к заказу
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
                string fileName = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls");
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

                foreach (CIntOrderItem objItem in m_objSelectedOrder.IntOrderItemList)
                {
                    oleDbJetInsertCommand.Parameters["ARTICLE"].Value = System.Convert.ToString(objItem.Product.Article);
                    oleDbJetInsertCommand.Parameters["NAME2"].Value = System.Convert.ToString(objItem.Product.Name);
                    oleDbJetInsertCommand.Parameters["QUANTITY"].Value = System.Convert.ToInt32(objItem.Quantity);
                    oleDbJetInsertCommand.Parameters["PRICE"].Value = System.Convert.ToDouble(objItem.Price);
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
            if (XtraMessageBox.Show("Хотите открыть этот файл?", "Экспорт в...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
                    DevExpress.XtraEditors.XtraMessageBox.Show(this, "Cannot find an application on your system suitable for openning the file with exported data.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            string name = DocNum.Text.Trim(); // Application.ProductName;
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
        
        #region Импорт приложения к заказу
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
        /// <summary>
        /// импорт приложения к заказу из файла MS Excel
        /// </summary>
        public void ImportFromExcel()
        {
            try
            {
                gridView.CellValueChanged -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gridView_CellValueChanged);

                if (StockSrc.SelectedItem == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Укажите, пожалуйста, склад-источник.", "Внимание",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    return;
                }
                if (cboxPriceType.SelectedItem == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Укажите, пожалуйста, уровень цены.", "Внимание",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    return;
                }

                using (frmImportXLSData objFrmImportXLSData = new frmImportXLSData(m_objProfile, m_objMenuItem, null, null))
                {
                    objFrmImportXLSData.OpenForImportPartsInInOrder((CStock)StockSrc.SelectedItem, System.Convert.ToDouble(spinEditDiscount.Value),
                        dataSet.Tables["OrderItems"], m_strXLSImportFilePath,
                        m_iXLSSheetImport, m_SheetList, checkMultiplicity.Checked, 
                        (CPriceType)cboxPriceType.SelectedItem, DocNum.Text);

                    m_strXLSImportFilePath = objFrmImportXLSData.FileFullName;
                    m_iXLSSheetImport = objFrmImportXLSData.SelectedSheetId;
                    m_SheetList = objFrmImportXLSData.SheetList;
                }

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

        #region Контекстное меню
        private void mitemClearRows_Click(object sender, EventArgs e)
        {
            dataSet.Tables["OrderItems"].Clear();
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

                mitemClearRows.Enabled = ((gridView.RowCount > 0) && (m_bIsReadOnly == false));
                mitemDeleteSelectedRows.Enabled = ((gridView.GetSelectedRows().Count<int>() > 0) && (m_bIsReadOnly == false));
                
                mitemImport.Enabled = (m_bIsReadOnly == false);
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
    public class ChangeIntOrderPropertieEventArgs : EventArgs
    {
        private readonly CIntOrder m_objOrder;
        public CIntOrder Order
        { get { return m_objOrder; } }

        private readonly enumActionSaveCancel m_enActionType;
        public enumActionSaveCancel ActionType
        { get { return m_enActionType; } }

        private readonly System.Boolean m_bIsNewOrder;
        public System.Boolean IsNewOrder
        { get { return m_bIsNewOrder; } }

        public ChangeIntOrderPropertieEventArgs(CIntOrder objOrder, enumActionSaveCancel enActionType, System.Boolean bIsNewOrder)
        {
            m_objOrder = objOrder;
            m_enActionType = enActionType;
            m_bIsNewOrder = bIsNewOrder;
        }
    }

}

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
using OfficeOpenXml;

namespace ERPMercuryProcessingOrder
{
    public enum ReturnMode
    {
        Unkown = -1,
        ByWaybill = 0,
        ByWaybillList = 1
    }

    public partial class frmWaybillBackReason : DevExpress.XtraEditors.XtraForm
    {
        #region Свойства
        private const string STR_ParamsForSelectWaybills = "Отбор накладных для возврата товара";
        private UniXP.Common.CProfile m_objProfile;
        private UniXP.Common.MENUITEM m_objMenuItem;
        private ReturnMode m_enumReturnMode;
        /// <summary>
        /// Накладная
        /// </summary>
        private CWaybill m_objWaybill;
        /// <summary>
        /// Накладная
        /// </summary>
        public CWaybill Waybill
        {
            get { return m_objWaybill; }
            set
            {
                if (m_objWaybill == value)
                    return;
                m_objWaybill = value;
            }
        }
        /// <summary>
        /// Клиент
        /// </summary>
        private CCustomer m_objCustomer;
        /// <summary>
        /// Клиент
        /// </summary>
        public CCustomer Customer
        {
            get {return m_objCustomer;}
            set
            {
                if (m_objCustomer == value)
                    return;
                m_objCustomer = value;
            }
        }
        private List<CCustomer> m_objCustomerList;
        /// <summary>
        /// Дочерний клиент
        /// </summary>
        private CChildDepart m_objChildDepart;
        /// <summary>
        /// Дочерний клиент
        /// </summary>
        public CChildDepart ChildDepart
        {
            get
            {
                return m_objChildDepart;
            }
            set
            {
                if (m_objChildDepart == value)
                    return;
                m_objChildDepart = value;
            }
        }
        /// <summary>
        /// Компания
        /// </summary>
        private CCompany m_objCompany;
        /// <summary>
        /// Компания
        /// </summary>
        public CCompany Company
        {
            get
            {
                return m_objCompany;
            }
            set
            {
                if (m_objCompany == value)
                    return;
                m_objCompany = value;
            }
        }
        /// <summary>
        /// Склад
        /// </summary>
        private CStock m_objStock;
        /// <summary>
        /// Склад
        /// </summary>
        public CStock Stock
        {
            get
            {
                return m_objStock;
            }
            set
            {
                if (m_objStock == value)
                    return;
                m_objStock = value;
            }
        }
        /// <summary>
        /// Форма оплаты
        /// </summary>
        private CPaymentType m_objPaymentType;
        /// <summary>
        /// Форма оплаты
        /// </summary>
        public CPaymentType PaymentType
        {
            get
            {
                return m_objPaymentType;
            }
            set
            {
                if (m_objPaymentType == value)
                    return;
                m_objPaymentType = value;
            }
        }
        /// <summary>
        /// Валюта
        /// </summary>
        private CCurrency m_objCurrency;
        /// <summary>
        /// Валюта
        /// </summary>
        public CCurrency Currency
        {
            get
            {
                return m_objCurrency;
            }
            set
            {
                if (m_objCurrency == value)
                    return;
                m_objCurrency = value;
            }
        }
        /// <summary>
        /// Причина возврата
        /// </summary>
        private CWaybillBackReason m_objWaybillBackReason;
        /// <summary>
        /// Причина возврата
        /// </summary>
        public CWaybillBackReason WaybillBackReason
        {
            get
            {
                return m_objWaybillBackReason;
            }
            set
            {
                if (m_objWaybillBackReason == value)
                    return;
                m_objWaybillBackReason = value;
            }
        }
        /// <summary>
        /// Начало периода для поиска
        /// </summary>
        private System.DateTime m_dtWaybillBeginDate;
        /// <summary>
        /// Начало периода для поиска
        /// </summary>
        public System.DateTime WaybillBeginDate
        {
            get
            {
                return m_dtWaybillBeginDate;
            }
            set
            {
                if (m_dtWaybillBeginDate == value)
                    return;
                m_dtWaybillBeginDate = value;
            }
        }
        /// <summary>
        /// Окончание периода для поиска
        /// </summary>
        private System.DateTime m_dtWaybillEndDate;
        /// <summary>
        /// Окончание периода для поиска
        /// </summary>
        public System.DateTime WaybillEndDate
        {
            get
            {
                return m_dtWaybillEndDate;
            }
            set
            {
                if (m_dtWaybillEndDate == value)
                    return;
                m_dtWaybillEndDate = value;
            }
        }
        #endregion

        #region Конструктор
        public frmWaybillBackReason(UniXP.Common.MENUITEM objMenuItem)
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
            m_enumReturnMode = ReturnMode.Unkown;

            m_objWaybill = null;
            m_objCustomer = null;
            m_objCustomerList = null;
            m_objStock = null;
            m_objPaymentType = null;
            m_objCurrency = null;
            m_objCompany = null;
            m_objChildDepart = null;
            m_dtWaybillBeginDate = System.DateTime.MinValue;
            m_dtWaybillEndDate = System.DateTime.MinValue;
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

        #region Заполнение элементов управления
        /// <summary>
        /// Заполняет элементы управления значениями перед открытием формы
        /// </summary>
        private void FillValuesForSrcBackWaybill()
        {
            try
            {
                System.String strErr = System.String.Empty;
                lblWaybillInfo.Text = System.String.Empty;
                cboxChildDepart.Properties.Items.Clear();
                cboxCustomer.Properties.Items.Clear();
                cboxPaymentType.Properties.Items.Clear();
                cboxStock.Properties.Items.Clear();
                cboxWaybillBackReason.Properties.Items.Clear();
                dtBeginDate.EditValue = null;
                dtEndDate.EditValue = null;

                cboxWaybillBackReason.Properties.Items.AddRange(CWaybillBackReason.GetWaybillBackReasonList(m_objProfile, ref strErr));
                cboxStock.Properties.Items.AddRange(CStock.GetStockList(m_objProfile, null));


                if (m_enumReturnMode == ReturnMode.ByWaybill)
                {
                    lblWaybillInfo.Text = String.Format("{0} от {1}", m_objWaybill.DocNum, m_objWaybill.BeginDate.ToShortDateString());

                    if (m_objCustomer != null)
                    {
                        cboxCustomer.Properties.Items.Add(m_objCustomer);
                    }
                    if ((m_objChildDepart != null) && (m_objChildDepart.ID.CompareTo( System.Guid.Empty ) != 0))
                    {
                        cboxChildDepart.Properties.Items.Add(m_objChildDepart);
                    }
                    if (m_objPaymentType != null)
                    {
                        cboxPaymentType.Properties.Items.Add(m_objPaymentType);
                    }
                }
                else if (m_enumReturnMode == ReturnMode.ByWaybillList)
                {
                    lblWaybillInfo.Text = STR_ParamsForSelectWaybills;
                    if ((m_objCustomerList != null) && (m_objCustomerList.Count > 0))
                    {
                        cboxCustomer.Properties.Items.AddRange(m_objCustomerList);
                    }
                    cboxStock.Properties.Items.AddRange( CStock.GetStockList( m_objProfile, null ) );
                    cboxPaymentType.Properties.Items.AddRange(CPaymentType.GetPaymentTypeList(m_objProfile, null, System.Guid.Empty ));
                }

                cboxWaybillBackReason.SelectedItem = (m_objWaybillBackReason == null) ? null : cboxWaybillBackReason.Properties.Items.Cast<CWaybillBackReason>().SingleOrDefault<CWaybillBackReason>(x => x.ID.CompareTo(m_objWaybillBackReason.ID) == 0);
                cboxCustomer.SelectedItem = (m_objCustomer == null) ? null : cboxCustomer.Properties.Items.Cast<CCustomer>().SingleOrDefault<CCustomer>(x => x.ID.CompareTo(m_objCustomer.ID ) == 0);
                cboxChildDepart.SelectedItem = (m_objChildDepart == null) ? null : cboxChildDepart.Properties.Items.Cast<CChildDepart>().SingleOrDefault<CChildDepart>(x => x.ID.CompareTo(m_objChildDepart.ID) == 0);
                cboxPaymentType.SelectedItem = (m_objPaymentType == null) ? null : cboxPaymentType.Properties.Items.Cast<CPaymentType>().SingleOrDefault<CPaymentType>(x => x.ID.CompareTo(m_objPaymentType.ID) == 0);
                cboxStock.SelectedItem = (m_objStock == null) ? null : cboxStock.Properties.Items.Cast<CStock>().SingleOrDefault<CStock>(x => x.ID.CompareTo(m_objStock.ID) == 0);
                dtBeginDate.DateTime = m_dtWaybillBeginDate;
                dtEndDate.DateTime = m_dtWaybillEndDate;

                cboxCustomer.Properties.ReadOnly = (m_enumReturnMode == ReturnMode.ByWaybill);
                cboxChildDepart.Properties.ReadOnly = (m_enumReturnMode == ReturnMode.ByWaybill);
                cboxPaymentType.Properties.ReadOnly = (m_enumReturnMode == ReturnMode.ByWaybill);
                cboxStock.Properties.ReadOnly = (m_enumReturnMode == ReturnMode.ByWaybill);
                dtBeginDate.Properties.ReadOnly = (m_enumReturnMode == ReturnMode.ByWaybill);
                dtEndDate.Properties.ReadOnly = (m_enumReturnMode == ReturnMode.ByWaybill);

            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("FillValuesForSrcBackWaybill().\n\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
                cboxChildDepart.SelectedItem = null;
                cboxChildDepart.Properties.Items.Clear();

                if (uuidCustomerId.CompareTo(System.Guid.Empty) != 0)
                {
                    cboxChildDepart.Properties.Items.AddRange(CChildDepart.GetChildDepartList(m_objProfile, null, uuidCustomerId));
                }
            }
            catch (System.Exception f)
            {
                SendMessageToLog(String.Format("{0} LoadChildDeprtForCustomer. Текст ошибки: {1}", this.Text, f.Message));
            }
            finally
            {
            }
            return;
        }

        /// <summary>
        /// Проверяет содержимое элементов управления
        /// </summary>
        private System.Boolean ValidateProperties()
        {
            System.Boolean bRet = true;
            try
            {
                cboxCustomer.Properties.Appearance.BackColor = ((cboxCustomer.SelectedItem == null) ? System.Drawing.Color.Tomato : System.Drawing.Color.White);
                cboxWaybillBackReason.Properties.Appearance.BackColor = ((cboxWaybillBackReason.SelectedItem == null) ? System.Drawing.Color.Tomato : System.Drawing.Color.White);
                cboxPaymentType.Properties.Appearance.BackColor = ((cboxPaymentType.SelectedItem == null) ? System.Drawing.Color.Tomato : System.Drawing.Color.White);
                cboxStock.Properties.Appearance.BackColor = ((cboxStock.SelectedItem == null) ? System.Drawing.Color.Tomato : System.Drawing.Color.White);

                dtBeginDate.Properties.Appearance.BackColor = ((dtBeginDate.DateTime == System.DateTime.MinValue) ? System.Drawing.Color.Tomato : System.Drawing.Color.White);
                dtEndDate.Properties.Appearance.BackColor = ((dtEndDate.DateTime == System.DateTime.MinValue) ? System.Drawing.Color.Tomato : System.Drawing.Color.White);

                if (dtEndDate.DateTime.CompareTo(dtBeginDate.DateTime) < 0)
                {
                    dtEndDate.Properties.Appearance.BackColor = ((dtEndDate.DateTime == System.DateTime.MinValue) ? System.Drawing.Color.Tomato : System.Drawing.Color.White);
                }

                if (cboxCustomer.SelectedItem == null) { bRet = false; }
                if (cboxWaybillBackReason.SelectedItem == null) { bRet = false; }
                if (cboxPaymentType.SelectedItem == null) { bRet = false; }
                if (cboxStock.SelectedItem == null) { bRet = false; }
                if (dtBeginDate.EditValue == null) { bRet = false; }
                if (dtEndDate.EditValue == null) { bRet = false; }

            }
            catch (System.Exception f)
            {
                SendMessageToLog(String.Format("{0} ValidateProperties. Текст ошибки: {1}", this.Text, f.Message));
            }

            return bRet;
        }
        private void SendMessageToLog(System.String strMessage)
        {
            try
            {
                m_objMenuItem.SimulateNewMessage(strMessage);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(String.Format("SendMessageToLog.\n Текст ошибки: {0}", f.Message), "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }

        private void cboxWaybillBackReason_SelectedValueChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = ValidateProperties();
        }

        private void cboxCustomer_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                System.Guid uuidCustomerId = ( ( cboxCustomer.SelectedItem != null ) ? ( ( CCustomer )cboxCustomer.SelectedItem ).ID : System.Guid.Empty );
                LoadChildDeprtForCustomer(uuidCustomerId);

                btnSave.Enabled = ValidateProperties();
            }
            catch (System.Exception f)
            {
                SendMessageToLog(String.Format("{0} cboxCustomer_SelectedValueChanged. Текст ошибки: {1}", this.Text, f.Message));
            }
            return;
        }

        #endregion

        #region Возврат по конкретной накладной
        /// <summary>
        /// Устанавливает режим возврата товара по конкретной накладной
        /// </summary>
        /// <param name="objSrcWaybill">документ "накладная на отгрузку"</param>
        public void SetSrcForBackWaybillByWaybill(CWaybill objSrcWaybill)
        {
            if (objSrcWaybill == null) { return; }
            try
            {
                m_objWaybill = objSrcWaybill;
                m_enumReturnMode = ReturnMode.ByWaybill;

                m_objWaybillBackReason = null;
                m_objCustomerList = null;

                m_objCustomer = m_objWaybill.Customer;
                m_objChildDepart = m_objWaybill.ChildDepart;
                m_objCurrency = m_objWaybill.Currency;
                m_objCompany = m_objWaybill.Company;
                m_objStock = m_objWaybill.Stock;
                m_objPaymentType = m_objWaybill.PaymentType;
                m_dtWaybillBeginDate = m_objWaybill.BeginDate;
                m_dtWaybillEndDate = m_objWaybill.BeginDate;

                FillValuesForSrcBackWaybill();

                DialogResult = System.Windows.Forms.DialogResult.None;
                
                //ShowDialog();

            }
            catch (System.Exception f)
            {
                SendMessageToLog("SetSrcForBackWaybillByWaybill. Текст ошибки: " + f.Message);
            }
            finally
            {
            }

            return;
        }
        #endregion

        #region Возврат по нескольким накладным
        /// <summary>
        /// Устанавливает режим возврата товара по нескольким накладным
        /// </summary>
        /// <param name="objCustomerList">список клиентов</param>
        public void SetSrcForBackWaybillByWaybillList(List<CCustomer> objCustomerList)
        {
            if ((objCustomerList == null) || (objCustomerList.Count == 0)) { return; }
            try
            {
                m_objWaybill = null;
                m_enumReturnMode = ReturnMode.ByWaybillList;

                m_objWaybillBackReason = null;
                m_objCustomerList = objCustomerList;

                m_objCustomer = null;
                m_objChildDepart = null;
                m_objCurrency = null;
                m_objCompany = null;
                m_objStock = null;
                m_objPaymentType = null;
                m_dtWaybillBeginDate = System.DateTime.Today;
                m_dtWaybillEndDate = System.DateTime.Today;

                FillValuesForSrcBackWaybill();

                DialogResult = System.Windows.Forms.DialogResult.None;

                //ShowDialog();

            }
            catch (System.Exception f)
            {
                SendMessageToLog("SetSrcForBackWaybillByWaybillList. Текст ошибки: " + f.Message);
            }
            finally
            {
            }

            return;
        }
        #endregion

        #region Подтвердить выбор
        /// <summary>
        /// Устанавливает свойства на основании элементов управления
        /// </summary>
        private void SetParamsForSrcbackWaybill()
        {
            try
            {
                if (ValidateProperties() == false) { return; }

                m_objWaybillBackReason = ((cboxWaybillBackReason.SelectedItem != null) ? (CWaybillBackReason)cboxWaybillBackReason.SelectedItem : null);
                m_dtWaybillBeginDate = dtBeginDate.DateTime;
                m_dtWaybillEndDate = dtEndDate.DateTime;

                if (m_enumReturnMode == ReturnMode.ByWaybill)
                {
                    if (m_objWaybill != null)
                    {
                        DialogResult = System.Windows.Forms.DialogResult.OK;
                        Close();
                    }
                }
                else if (m_enumReturnMode == ReturnMode.ByWaybillList)
                {
                    m_objCustomer = ((cboxCustomer.SelectedItem != null) ? (CCustomer)cboxCustomer.SelectedItem : null);
                    m_objChildDepart = ((cboxChildDepart.SelectedItem != null) ? (CChildDepart)cboxChildDepart.SelectedItem : null);
                    m_objPaymentType = ((cboxPaymentType.SelectedItem != null) ? (CPaymentType)cboxPaymentType.SelectedItem : null);
                    m_objStock = ((cboxStock.SelectedItem != null) ? (CStock)cboxStock.SelectedItem : null);

                    DialogResult = System.Windows.Forms.DialogResult.OK;
                    Close();
                }
            
            }
            catch (System.Exception f)
            {
                SendMessageToLog("SetParamsForSrcbackWaybill. Текст ошибки: " + f.Message);
            }
            finally
            {
            }

            return;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            SetParamsForSrcbackWaybill();
        }

        #endregion

        #region Отмена выбора
        /// <summary>
        /// Отмена выбора
        /// </summary>
        private void Cancel()
        {
            try
            {
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
                Close();
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }
        #endregion



    }
}

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
        private List<CProduct> m_objPartsList;

        private CBackWaybill m_objSelectedWaybill;
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
            m_bIsAutoCreatePriceMode = false;
            m_uuidSelectedStockID = System.Guid.Empty;

            m_objSelectedWaybill = null;
            m_objPartsList = null;
            m_iPaymentType1 = new Guid(m_strPaymentType1);
            m_iPaymentType2 = new Guid(m_strPaymentType2);
            m_strXLSImportFilePath = "";
            m_iXLSSheetImport = 0;
            m_bCustomerInBlackList = false;
            m_SUMM_IS_PASS = 0;

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

        #region Редактировать накладную
  
        /// <summary>
        /// Загружает свойства накладной для редактирования
        /// </summary>
        /// <param name="objBackWaybill">накладная</param>
        public void EditBackWaybill(CBackWaybill objBackWaybill, System.Boolean bNewObject)
        {
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

        private readonly System.Boolean m_bIsNewWaybill;
        public System.Boolean IsNewWaybill
        { get { return m_bIsNewWaybill; } }

        private readonly System.Guid m_uuidOrderCurrentStateGuid;
        public System.Guid OrderCurrentStateGuid
        { get { return m_uuidOrderCurrentStateGuid; } }

        public ChangeBackWaybillPropertieEventArgs(CBackWaybill objWaybill, enumActionSaveCancel enActionType,
            System.Boolean bIsNewWaybill, System.Guid uuidOrderCurrentStateGuid)
        {
            m_objWaybill = objWaybill;
            m_enActionType = enActionType;
            m_bIsNewWaybill = bIsNewWaybill;
            m_uuidOrderCurrentStateGuid = uuidOrderCurrentStateGuid;
        }
    }

}

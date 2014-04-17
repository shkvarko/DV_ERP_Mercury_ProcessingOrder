using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPMercuryProcessingOrder
{
    public enum enPDASupplState
    {
        Unkown = -1,
        Created = 0,
        Deleted = 1,
        Transfered = 2,
        Processed = 3,
        CreditControl = 4,
        General = 5,
        CalcPricesFalse = 7,
        CreateSupplInIBFalse = 8,
        FindStock = 10,
        Print = 11,
        Confirm = 12,
        TTN = 13,
        Shipped = 14,
        CalcPricesOk = 60,
        AutoCalcPricesOk = 70,
        OutPartsOfStock = 80,
        MakedForRecalcPrices = 90
    }

    #region Состояние заказа
    /// <summary>
    /// Класс "Состояние заказа"
    /// </summary>
    public class CSupplState
    {
        private System.Int32 m_iId;
        public System.Int32 ID
        {
            get { return m_iId; }
            set { m_iId = value; }
        }
        private System.String m_strName;
        public System.String Name
        {
            get { return m_strName; }
            set { m_strName = value; }
        }
        public CSupplState()
        {
            m_iId = 0;
            m_strName = "";
        }
        public CSupplState(System.Int32 iId, System.String strName)
        {
            m_iId = iId;
            m_strName = strName;
        }
        public override string ToString()
        {
            return Name;
        }
        /// <summary>
        /// Возвращает список состояний заказа
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmdSQL">SQL-команда</param>
        /// <returns>список состояний заказа</returns>
        public static List<CSupplState> GetSupplStateList(
            UniXP.Common.CProfile objProfile, System.Data.SqlClient.SqlCommand cmdSQL)
        {
            List<CSupplState> objList = new List<CSupplState>();
            System.Data.SqlClient.SqlConnection DBConnection = null;
            System.Data.SqlClient.SqlCommand cmd = null;
            try
            {
                if (cmdSQL == null)
                {
                    DBConnection = objProfile.GetDBSource();
                    if (DBConnection == null)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(
                            "Не удалось получить соединение с базой данных.", "Внимание",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return objList;
                    }
                    cmd = new System.Data.SqlClient.SqlCommand();
                    cmd.Connection = DBConnection;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                }
                else
                {
                    cmd = cmdSQL;
                    cmd.Parameters.Clear();
                }

                cmd.CommandText = System.String.Format("[{0}].[dbo].[usp_GetSupplState]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        objList.Add(new CSupplState() { ID = System.Convert.ToInt32(rs["SupplState_Id"]), Name = System.Convert.ToString(rs["SupplState_Name"]) });
                    }
                }
                rs.Dispose();

                if (cmdSQL == null)
                {
                    cmd.Dispose();
                    DBConnection.Close();
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить список состояний заказа.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return objList;
        }

    }
    #endregion

    public class CPDASupplItms
    {
        #region Свойства
        /// <summary>
        /// Наименование товарной марки
        /// </summary>
        private System.String m_strProductOwnerName;
        /// <summary>
        /// Наименование товарной марки
        /// </summary>
        public System.String ProductOwnerName
        {
            get { return m_strProductOwnerName; }
            set { m_strProductOwnerName = value; }
        }
        /// <summary>
        /// Наименование товара
        /// </summary>
        private System.String m_strProductName;
        /// <summary>
        /// Наименование товара
        /// </summary>
        public System.String ProductName
        {
            get { return m_strProductName; }
            set { m_strProductName = value; }
        }
        /// <summary>
        /// Цена, руб.
        /// </summary>
        private System.Double m_Price;
        /// <summary>
        /// Цена, руб.
        /// </summary>
        public System.Double Price
        {
            get { return m_Price; }
            set { m_Price = value; }
        }
        /// <summary>
        /// Цена, eur
        /// </summary>
        private System.Double m_CurrencyPrice;
        /// <summary>
        /// Цена, eur
        /// </summary>
        public System.Double CurrencyPrice
        {
            get { return m_CurrencyPrice; }
            set { m_CurrencyPrice = value; }
        }
        /// <summary>
        /// Цена со скидкой, руб.
        /// </summary>
        private System.Double m_DiscountPrice;
        /// <summary>
        /// Цена со скидкой, руб.
        /// </summary>
        public System.Double DiscountPrice
        {
            get { return m_DiscountPrice; }
            set { m_DiscountPrice = value; }
        }
        /// <summary>
        /// Цена со скидкой, eur
        /// </summary>
        private System.Double m_CurrencyDiscountPrice;
        /// <summary>
        /// Цена со скидкой, eur
        /// </summary>
        public System.Double CurrencyDiscountPrice
        {
            get { return m_CurrencyDiscountPrice; }
            set { m_CurrencyDiscountPrice = value; }
        }
        /// <summary>
        /// Сумма, руб.
        /// </summary>
        private System.Double m_AllPrice;
        /// <summary>
        /// Сумма, руб.
        /// </summary>
        public System.Double AllPrice
        {
            get { return m_AllPrice; }
            set { m_AllPrice = value; }
        }
        /// <summary>
        /// Сумма, eur.
        /// </summary>
        private System.Double m_AllCurrencyPrice;
        /// <summary>
        /// Сумма, eur.
        /// </summary>
        public System.Double AllCurrencyPrice
        {
            get { return m_AllCurrencyPrice; }
            set { m_AllCurrencyPrice = value; }
        }
        /// <summary>
        /// Скидка, %.
        /// </summary>
        private System.Double m_DiscountPercent;
        /// <summary>
        /// Скидка, %.
        /// </summary>
        public System.Double DiscountPercent
        {
            get { return m_DiscountPercent; }
            set { m_DiscountPercent = value; }
        }
        /// <summary>
        /// Сумма со скидкой, руб
        /// </summary>
        private System.Double m_TotalPrice;
        /// <summary>
        /// Сумма со скидкой, руб
        /// </summary>
        public System.Double TotalPrice
        {
            get { return m_TotalPrice; }
            set { m_TotalPrice = value; }
        }
        /// <summary>
        /// Сумма со скидкой, eur
        /// </summary>
        private System.Double m_TotalCurrencyPrice;
        /// <summary>
        /// Сумма со скидкой, eur
        /// </summary>
        public System.Double TotalCurrencyPrice
        {
            get { return m_TotalCurrencyPrice; }
            set { m_TotalCurrencyPrice = value; }
        }
        /// <summary>
        /// Количество товара в заказе
        /// </summary>
        private System.Int32 m_QtyOrder;
        /// <summary>
        /// Количество товара в заказе
        /// </summary>
        public System.Int32 QtyOrder
        {
            get { return m_QtyOrder; }
            set { m_QtyOrder = value; }
        }
        /// <summary>
        /// Количество товара в заказе
        /// </summary>
        private System.Int32 m_Qty;
        /// <summary>
        /// Количество товара в заказе
        /// </summary>
        public System.Int32 Qty
        {
            get { return m_Qty; }
            set { m_Qty = value; }
        }
        /// <summary>
        /// Признак "Товар импортера"
        /// </summary>
        private System.Boolean m_bIsImporter;
        /// <summary>
        /// Признак "Товар импортера"
        /// </summary>
        public System.Boolean IsImporter
        {
            get { return m_bIsImporter; }
            set { m_bIsImporter = value; }
        }
        /// <summary>
        /// Надбавка, %
        /// </summary>
        private System.Double m_ChargePercent;
        /// <summary>
        /// Надбавка, %
        /// </summary>
        public System.Double ChargePercent
        {
            get { return m_ChargePercent; }
            set { m_ChargePercent = value; }
        }
        /// <summary>
        /// Ретро-скидка, %
        /// </summary>
        private System.Double m_DiscountRetro;
        /// <summary>
        /// Ретро-скидка, %
        /// </summary>
        public System.Double DiscountRetro
        {
            get { return m_DiscountRetro; }
            set { m_DiscountRetro = value; }
        }
        /// <summary>
        /// Фиксированная скидка
        /// </summary>
        private System.Double m_DiscountFix;
        /// <summary>
        /// Фиксированная скидка
        /// </summary>
        public System.Double DiscountFix
        {
            get { return m_DiscountFix; }
            set { m_DiscountFix = value; }
        }
        /// <summary>
        /// Скидка за оборудование
        /// </summary>
        private System.Double m_DiscountTradeEquip;
        /// <summary>
        /// Скидка за оборудование
        /// </summary>
        public System.Double DiscountTradeEquip
        {
            get { return m_DiscountTradeEquip; }
            set { m_DiscountTradeEquip = value; }
        }
        /// <summary>
        /// Ставка НДС, %
        /// </summary>
        private System.Double m_NDSPercent;
        /// <summary>
        /// Ставка НДС, %
        /// </summary>
        public System.Double NDSPercent
        {
            get { return m_NDSPercent; }
            set { m_NDSPercent = value; }
        }
        /// <summary>
        /// Примечание
        /// </summary>
        private System.String m_strDescription;
        /// <summary>
        /// Примечание
        /// </summary>
        public System.String Description
        {
            get { return m_strDescription; }
            set { m_strDescription = value; }
        }
        /// <summary>
        /// Расшифровка состава скидки
        /// </summary>
        private System.String m_strDiscountDescription;
        /// <summary>
        /// Расшифровка состава скидки
        /// </summary>
        public System.String DiscountDescription
        {
            get { return m_strDiscountDescription; }
            set { m_strDiscountDescription = value; }
        }
        #endregion

        #region Конструктор
        public CPDASupplItms()
        {
            m_strProductName = System.String.Empty;
            m_Qty = 0;
            m_QtyOrder = 0;
            m_Price = 0;
            m_CurrencyPrice = 0;
            m_AllPrice = 0;
            m_AllCurrencyPrice = 0;
            m_TotalPrice = 0;
            m_TotalCurrencyPrice = 0;
            m_DiscountPercent = 0;
            m_strProductOwnerName = System.String.Empty;
            m_bIsImporter = false;
            m_ChargePercent = 0;
            m_DiscountRetro = 0;
            m_NDSPercent = 0;
            m_DiscountPrice = 0;
            m_DiscountFix = 0;
            m_DiscountTradeEquip = 0;
            m_CurrencyDiscountPrice = 0;
            m_strDescription = System.String.Empty;
            m_strDiscountDescription = System.String.Empty;
        }
        public CPDASupplItms(System.String strProductName, System.Int32 iQty, System.Int32 iQtyOrder,
            System.Double mPice, System.Double mAllPrice, System.Double mCurrencyPrice, System.Double mAllCurrencyPrice,
            System.Double mDiscountPercent, System.Double mTotalPrice, System.Double mTotalCurrencyPrice,
            System.String strProductOwnerName, System.Boolean bIsImporter, System.Double moneyChargePercent,
            System.Double moneyDiscountRetro, System.Double moneyDiscountFix, System.Double moneyDiscountTradeEquip,
            System.Double moneyNDSPercent, System.Double moneyDiscountPrice, System.Double moneyCurrencyDiscountPrice,
            System.String strDescription, System.String strDiscountDescription)
        {
            m_strProductName = strProductName;
            m_Qty = iQty;
            m_QtyOrder = iQtyOrder;
            m_Price = mPice;
            m_CurrencyPrice = mCurrencyPrice;
            m_AllPrice = mAllPrice;
            m_AllCurrencyPrice = mAllCurrencyPrice;
            m_TotalPrice = mTotalPrice;
            m_TotalCurrencyPrice = mTotalCurrencyPrice;
            m_DiscountPercent = mDiscountPercent;
            m_strProductOwnerName = strProductOwnerName;
            m_bIsImporter = bIsImporter;
            m_ChargePercent = moneyChargePercent;
            m_DiscountRetro = moneyDiscountRetro;
            m_NDSPercent = moneyNDSPercent;
            m_DiscountPrice = moneyDiscountPrice;
            m_DiscountFix = moneyDiscountFix;
            m_DiscountTradeEquip = moneyDiscountTradeEquip;
            m_CurrencyDiscountPrice = moneyCurrencyDiscountPrice;
            m_strDescription = strDescription;
            m_strDiscountDescription = strDiscountDescription;
        }
        #endregion
    }
    
    public class CPDASuppl
    {
        #region Свойства
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        private System.Guid m_uuidID;
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public System.Guid Id
        {
            get { return m_uuidID; }
            set { m_uuidID = value; }
        }
        /// <summary>
        /// Номер заказа
        /// </summary>
        private System.Int32 m_iSupplNum;
        /// <summary>
        /// Номер заказа
        /// </summary>
        public System.Int32 Num
        {
            get { return m_iSupplNum; }
            set { m_iSupplNum = value; }
        }
        /// <summary>
        /// Номер версии заказа
        /// </summary>
        private System.Int32 m_iSupplSubNum;
        /// <summary>
        /// Номер версии заказа
        /// </summary>
        public System.Int32 SubNum
        {
            get { return m_iSupplSubNum; }
            set { m_iSupplSubNum = value; }
        }
        /// <summary>
        /// Дата заказа
        /// </summary>
        private System.DateTime m_dtBeginDate;
        /// <summary>
        /// Дата заказа
        /// </summary>
        public System.DateTime BeginDate
        {
            get { return m_dtBeginDate; }
            set { m_dtBeginDate = value; }
        }
        /// <summary>
        /// Дата доставки
        /// </summary>
        private System.DateTime m_dtDeliveryDate;
        /// <summary>
        /// Дата доставки
        /// </summary>
        public System.DateTime DeliveryDate
        {
            get { return m_dtDeliveryDate; }
            set { m_dtDeliveryDate = value; }
        }
        /// <summary>
        /// Клиент (имя)
        /// </summary>
        private System.String m_strCustomerName;
        /// <summary>
        /// Клиент (имя)
        /// </summary>
        public System.String CustomerName
        {
            get { return m_strCustomerName; }
            set { m_strCustomerName = value; }
        }
        /// <summary>
        /// Код дочернего клиента
        /// </summary>
        private System.String m_strChildCustomerCode;
        /// <summary>
        /// Код дочернего клиента
        /// </summary>
        public System.String ChildCustomerCode
        {
            get { return m_strChildCustomerCode; }
            set { m_strChildCustomerCode = value; }
        }
        /// <summary>
        /// Код торгового представителя
        /// </summary>
        private System.String m_strDepartCode;
        /// <summary>
        /// Код торгового представителя
        /// </summary>
        public System.String DepartCode
        {
            get { return m_strDepartCode; }
            set { m_strDepartCode = value; }
        }
        /// <summary>
        /// Наименование Склада
        /// </summary>
        private System.String m_strStockName;
        /// <summary>
        /// Наименование Склада
        /// </summary>
        public System.String StockName
        {
            get { return m_strStockName; }
            set { m_strStockName = value; }
        }
        /// <summary>
        /// Наименование Компании
        /// </summary>
        private System.String m_strCompanyName;
        /// <summary>
        /// Наименование Компании
        /// </summary>
        public System.String CompanyName
        {
            get { return m_strCompanyName; }
            set { m_strCompanyName = value; }
        }
        /// <summary>
        /// Список групп, куда входит клиент
        /// </summary>
        private System.String m_strGroupList;
        /// <summary>
        /// Список групп, куда входит клиент
        /// </summary>
        public System.String GroupList
        {
            get { return m_strGroupList; }
            set { m_strGroupList = value; }
        }
        /// <summary>
        /// Состояние
        /// </summary>
        private enPDASupplState m_PDASupplState;
        /// <summary>
        /// Состояние
        /// </summary>
        public enPDASupplState State
        {
            get { return m_PDASupplState; }
            set { m_PDASupplState = value; }
        }
        /// <summary>
        /// Состояние
        /// </summary>
        private CSupplState m_objSupplState;
        /// <summary>
        /// Состояние
        /// </summary>
        public CSupplState SupplState
        {
            get { return m_objSupplState; }
            set { m_objSupplState = value; }
        }
        /// <summary>
        /// Состояние
        /// </summary>
        public System.String StateName
        {
            get { return ((m_objSupplState == null) ? "" : m_objSupplState.Name); }

            //get { return GetStateName(); }
        }
        private System.String GetStateName()
        {
            System.String strRet = "";
            try
            {
                switch (m_PDASupplState)
                {
                    case enPDASupplState.Unkown:
                        {
                            strRet = "-";
                            break;
                        }
                    case enPDASupplState.Created:
                        {
                            strRet = "создан";
                            break;
                        }
                    case enPDASupplState.Deleted:
                        {
                            strRet = "удален";
                            break;
                        }
                    case enPDASupplState.Transfered:
                        {
                            strRet = "передан";
                            break;
                        }
                    case enPDASupplState.Processed:
                        {
                            strRet = "обработан";
                            break;
                        }
                    case enPDASupplState.CreditControl:
                        {
                            strRet = "кредитный контроль";
                            break;
                        }
                    case enPDASupplState.General:
                        {
                            strRet = "основной";
                            break;
                        }
                    case enPDASupplState.CalcPricesFalse:
                        {
                            strRet = "ошибка при расчете цен";
                            break;
                        }
                    case enPDASupplState.CreateSupplInIBFalse:
                        {
                            strRet = "ошибка при создании протокола";
                            break;
                        }
                    case enPDASupplState.FindStock:
                        {
                            strRet = "определены склады";
                            break;
                        }
                    case enPDASupplState.CalcPricesOk:
                        {
                            strRet = "расчитаны цены";
                            break;
                        }
                    case enPDASupplState.AutoCalcPricesOk:
                        {
                            strRet = "автоматически расчитаны цены и создан протокол";
                            break;
                        }
                    case enPDASupplState.Print:
                        {
                            strRet = "печать";
                            break;
                        }
                    case enPDASupplState.Confirm:
                        {
                            strRet = "подтвержден";
                            break;
                        }
                    case enPDASupplState.TTN:
                        {
                            strRet = "ТТН";
                            break;
                        }
                    case enPDASupplState.Shipped:
                        {
                            strRet = "отгружен";
                            break;
                        }
                    case enPDASupplState.OutPartsOfStock:
                        {
                            strRet = "нет товара на складе";
                            break;
                        }
                    default:
                        break;
                }
            }
            catch
            {
            }
            return strRet;
        }
        /// <summary>
        /// Сумма, руб.
        /// </summary>
        private System.Double m_AllPrice;
        /// <summary>
        /// Сумма, руб.
        /// </summary>
        public System.Double AllPrice
        {
            get { return m_AllPrice; }
            set { m_AllPrice = value; }
        }
        /// <summary>
        /// Скидка, руб.
        /// </summary>
        private System.Double m_AllDiscount;
        /// <summary>
        /// Скидка, руб.
        /// </summary>
        public System.Double AllDiscount
        {
            get { return m_AllDiscount; }
            set { m_AllDiscount = value; }
        }
        /// <summary>
        /// Сумма со скидкой, руб
        /// </summary>
        private System.Double m_AllDiscountPrice;
        /// <summary>
        /// Сумма со скидкой, руб
        /// </summary>
        public System.Double AllDiscountPrice
        {
            get { return m_AllDiscountPrice; }
            set { m_AllDiscountPrice = value; }
        }
        /// <summary>
        /// Сумма, eur.
        /// </summary>
        private System.Double m_AllCurrencyPrice;
        /// <summary>
        /// Сумма, eur.
        /// </summary>
        public System.Double AllCurrencyPrice
        {
            get { return m_AllCurrencyPrice; }
            set { m_AllCurrencyPrice = value; }
        }
        /// <summary>
        /// Скидка, eur.
        /// </summary>
        private System.Double m_AllCurrencyDiscount;
        /// <summary>
        /// Скидка, eur.
        /// </summary>
        public System.Double AllCurrencyDiscount
        {
            get { return m_AllCurrencyDiscount; }
            set { m_AllCurrencyDiscount = value; }
        }
        /// <summary>
        /// Сумма со скидкой, eur
        /// </summary>
        private System.Double m_AllCurrencyDiscountPrice;
        /// <summary>
        /// Сумма со скидкой, eur
        /// </summary>
        public System.Double AllCurrencyDiscountPrice
        {
            get { return m_AllCurrencyDiscountPrice; }
            set { m_AllCurrencyDiscountPrice = value; }
        }
        /// <summary>
        /// Количество позиций в заказе
        /// </summary>
        private System.Int32 m_PositionQty;
        /// <summary>
        /// Количество позиций в заказе
        /// </summary>
        public System.Int32 PositionQty
        {
            get { return m_PositionQty; }
            set { m_PositionQty = value; }
        }
        /// <summary>
        /// Количество товара в заказе
        /// </summary>
        private System.Int32 m_Qty;
        /// <summary>
        /// Количество товара в заказе
        /// </summary>
        public System.Int32 Qty
        {
            get { return m_Qty; }
            set { m_Qty = value; }
        }
        /// <summary>
        /// Заказанное количество товара в заказе
        /// </summary>
        private System.Int32 m_OrderQty;
        /// <summary>
        /// Заказанное количество товара в заказе
        /// </summary>
        public System.Int32 OrderQty
        {
            get { return m_OrderQty; }
            set { m_OrderQty = value; }
        }
        /// <summary>
        /// Список продукции в заказе
        /// </summary>
        private List<CPDASupplItms> m_objSupplItmsList;
        /// <summary>
        /// Список продукции в заказе
        /// </summary>
        public List<CPDASupplItms> SupplItmsList
        {
            get { return m_objSupplItmsList; }
            set { m_objSupplItmsList = value; }
        }
        /// <summary>
        /// Признак "Бонус"
        /// </summary>
        private System.Int32 m_iMoneyBonus;
        /// <summary>
        /// Признак "Бонус"
        /// </summary>
        public System.Boolean MoneyBonus
        {
            get { return ((m_iMoneyBonus == 0) ? false : true); }
        }
        /// <summary>
        /// Вес
        /// </summary>
        private System.Double m_dblWeight;
        /// <summary>
        /// Вес
        /// </summary>
        public System.Double Weight
        {
            get { return m_dblWeight; }
            set { m_dblWeight = value; }
        }
        /// <summary>
        /// Признак "Исключен из акта выполненных работ"
        /// </summary>
        private System.Boolean m_bExcludeFromADJ;
        /// <summary>
        /// Признак "Исключен из акта выполненных работ"
        /// </summary>
        public System.Boolean IsExcludeFromADJ
        {
            get { return m_bExcludeFromADJ; }
            set { m_bExcludeFromADJ = value; }
        }
        #endregion

        #region Конструктор
        public CPDASuppl()
        {
            m_uuidID = System.Guid.Empty ;
            m_iSupplNum = 0;
            m_iSupplSubNum = 0;
            m_dtBeginDate = System.DateTime.MinValue;
            m_strCustomerName = System.String.Empty;
            m_strChildCustomerCode = System.String.Empty;
            m_strDepartCode = System.String.Empty;
            m_PDASupplState = enPDASupplState.Unkown;
            m_PositionQty = 0;
            m_Qty = 0;
            m_OrderQty = 0;
            m_AllPrice = 0;
            m_AllDiscount = 0;
            m_AllDiscountPrice = 0;
            m_AllCurrencyPrice = 0;
            m_AllCurrencyDiscount = 0;
            m_AllCurrencyDiscountPrice = 0;
            m_objSupplItmsList = null;
            m_strCompanyName = System.String.Empty;
            m_strStockName = System.String.Empty;
            m_strGroupList = System.String.Empty;
            m_dtDeliveryDate = System.DateTime.MinValue;
            m_iMoneyBonus = 0;
            m_objSupplState = null;
            m_dblWeight = 0;
            m_bExcludeFromADJ = false;
        }

        public CPDASuppl(System.Guid uuidID, System.Int32 iSupplNum, System.Int32 iSupplSubNum, System.DateTime dtBeginDate,
            System.String strCustomerName, System.String strChildCustomerCode, System.String strDepartCode,
            enPDASupplState PDASupplState, System.Int32 iPosQty, System.Int32 iQty,
            System.Double mAllPrice, System.Double mAllDiscount, System.Double mAllDiscountPrice,
            System.Double mAllCurrencyPrice, System.Double mAllCurrencyDiscount, System.Double mAllCurrencyDiscountPrice,
            System.String strStockName, System.String strCompanyName, System.String strGroupList, System.Int32 iOrderQty,
            System.DateTime dtDeliveryDate, System.Int32 iMoneyBonus, CSupplState objSupplState, System.Double dblWeight, System.Boolean bExcludeFromADJ)
        {
            m_uuidID = uuidID;
            m_iSupplNum = iSupplNum;
            m_iSupplSubNum = iSupplSubNum;
            m_dtBeginDate = dtBeginDate;
            m_strCustomerName = strCustomerName;
            m_strChildCustomerCode = strChildCustomerCode;
            m_strDepartCode = strDepartCode;
            m_PDASupplState = PDASupplState;
            m_PositionQty = iPosQty;
            m_Qty = iQty;
            m_OrderQty = iOrderQty;
            m_AllPrice = mAllPrice;
            m_AllDiscount = mAllDiscount;
            m_AllDiscountPrice = mAllDiscountPrice;
            m_AllCurrencyPrice = mAllCurrencyPrice;
            m_AllCurrencyDiscount = mAllCurrencyDiscount;
            m_AllCurrencyDiscountPrice = mAllCurrencyDiscountPrice;
            m_objSupplItmsList = null;
            m_strCompanyName = strCompanyName;
            m_strStockName = strStockName;
            m_strGroupList = strGroupList;
            m_dtDeliveryDate = dtDeliveryDate;
            m_iMoneyBonus = iMoneyBonus;
            m_objSupplState = objSupplState;
            m_dblWeight = dblWeight;
            m_bExcludeFromADJ = bExcludeFromADJ;
        }
        #endregion

        #region Список заказов
        /// <summary>
        /// Возвращает список заказов
        /// </summary>
        /// <param name="objProfile"></param>
        /// <returns></returns>
        public static List<CPDASuppl> GetSupplList(UniXP.Common.CProfile objProfile, System.Guid uuidSupplId,
            System.DateTime dtBeginDate, System.DateTime dtEndDate)
        {
            List<CPDASuppl> objList = new List<CPDASuppl>();
            System.Data.SqlClient.SqlConnection DBConnection = null;
            System.Data.SqlClient.SqlCommand cmd = null;
            System.Int32 iCmdTimeOut = 240;
            try
            {
                DBConnection = objProfile.GetDBSourceAsynch(); // .GetDBSource();
                if (DBConnection == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                        "Не удалось получить соединение с базой данных.", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return objList;
                }
                cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandTimeout = iCmdTimeOut;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[usp_GetSuppl]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BeginDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EndDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@BeginDate"].Value = dtBeginDate;
                cmd.Parameters["@EndDate"].Value = dtEndDate;

                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    System.Int32 iSubNum = 0;
                    System.String strChildCode = "";
                    System.String strStockName = "";
                    System.String strCompanyName = "";
                    System.DateTime dateDeliveryDate = System.DateTime.MinValue;
                    System.Int32 intBonus = 0;
                    System.Boolean bExcludeFromADJ = false;
                    CSupplState objSupplState = null;
                    while (rs.Read())
                    {
                        iSubNum = (rs["Suppl_Version"] == System.DBNull.Value) ? 0 : (System.Int32)rs["Suppl_Version"];
                        strChildCode = (rs["ChildDepart_Code"] == System.DBNull.Value) ? "" : (System.String)rs["ChildDepart_Code"];
                        strStockName = (rs["Stock_Name"] == System.DBNull.Value) ? "" : (System.String)rs["Stock_Name"];
                        strCompanyName = (rs["Company_Name"] == System.DBNull.Value) ? "" : (System.String)rs["Company_Name"];
                        dateDeliveryDate = (rs["Suppl_DeliveryDate"] == System.DBNull.Value) ? System.DateTime.MinValue : System.Convert.ToDateTime(rs["Suppl_DeliveryDate"]);
                        intBonus = (rs["Suppl_Bonus"] == System.DBNull.Value) ? 0 : System.Convert.ToInt32(rs["Suppl_Bonus"]);
                        objSupplState = (rs["SupplState_Guid"] == System.DBNull.Value) ? null : (new CSupplState(System.Convert.ToInt32(rs["SupplState_Id"]), (System.String)rs["SupplState_Name"]));
                        bExcludeFromADJ = (rs["Suppl_ExcludeFromAdj"] == System.DBNull.Value) ? false : (System.Convert.ToBoolean(rs["Suppl_ExcludeFromAdj"]));

                        objList.Add(new CPDASuppl() 
                        { 
                            m_uuidID = (System.Guid)rs["Suppl_Guid"],
                            Num = ((rs["Suppl_Num"] == System.DBNull.Value) ? 0 : System.Convert.ToInt32(rs["Suppl_Num"])), 
                            SubNum = iSubNum,
                            BeginDate = ((rs["Suppl_BeginDate"] == System.DBNull.Value) ? System.DateTime.MinValue : System.Convert.ToDateTime(rs["Suppl_BeginDate"])),
                            CustomerName = ((rs["Customer_Name"] == System.DBNull.Value) ? System.String.Empty : System.Convert.ToString(rs["Customer_Name"])),
                            ChildCustomerCode = strChildCode,
                            DepartCode = ((rs["Depart_Code"] == System.DBNull.Value) ? System.String.Empty : System.Convert.ToString(rs["Depart_Code"])),
                            m_PDASupplState = ((rs["SupplState_Id"] == System.DBNull.Value) ? enPDASupplState.Unkown : (enPDASupplState)( System.Convert.ToInt32( rs["SupplState_Id"] ))),
                            PositionQty = ((rs["POSQUANTITY"] == System.DBNull.Value) ? 0 : System.Convert.ToInt32(rs["POSQUANTITY"])),
                            OrderQty = ((rs["ORDERQUANTITY"] == System.DBNull.Value) ? 0 : System.Convert.ToInt32(rs["ORDERQUANTITY"])),
                            Qty = ((rs["QUANTITY"] == System.DBNull.Value) ? 0 : System.Convert.ToInt32(rs["QUANTITY"])),
                            AllPrice = ((rs["Suppl_AllPrice"] == System.DBNull.Value) ? 0 : System.Convert.ToDouble(rs["Suppl_AllPrice"])),
                            AllDiscount = ((rs["Suppl_AllDiscount"] == System.DBNull.Value) ? 0 : System.Convert.ToDouble(rs["Suppl_AllDiscount"])),
                            AllDiscountPrice = ((rs["Suppl_TotalPrice"] == System.DBNull.Value) ? 0 : System.Convert.ToDouble(rs["Suppl_TotalPrice"])),
                            AllCurrencyPrice = ((rs["Suppl_CurrencyAllPrice"] == System.DBNull.Value) ? 0 : System.Convert.ToDouble(rs["Suppl_CurrencyAllPrice"])),
                            AllCurrencyDiscount = ((rs["Suppl_CurrencyAllDiscount"] == System.DBNull.Value) ? 0 : System.Convert.ToDouble(rs["Suppl_CurrencyAllDiscount"])), 
                            AllCurrencyDiscountPrice = ((rs["Suppl_CurrencyTotalPrice"] == System.DBNull.Value) ? 0 : System.Convert.ToDouble(rs["Suppl_CurrencyTotalPrice"])),
                            StockName = ((rs["Stock_Name"] == System.DBNull.Value) ? System.String.Empty : System.Convert.ToString(rs["Stock_Name"])),
                            CompanyName = ((rs["Company_Name"] == System.DBNull.Value) ? System.String.Empty : System.Convert.ToString(rs["Company_Name"])),
                            GroupList = ((rs["GroupList"] == System.DBNull.Value) ? System.String.Empty : System.Convert.ToString(rs["GroupList"])),
                            DeliveryDate = ((rs["Suppl_DeliveryDate"] == System.DBNull.Value) ? System.DateTime.MinValue : System.Convert.ToDateTime(rs["Suppl_DeliveryDate"])),
                            m_iMoneyBonus = ((rs["Suppl_Bonus"] == System.DBNull.Value) ? 0 : System.Convert.ToInt32(rs["Suppl_Bonus"])),
                            SupplState = (( rs["SupplState_Guid"] == System.DBNull.Value ) ? null :  new CSupplState() { ID = System.Convert.ToInt32( rs["SupplState_Id"] ), Name = System.Convert.ToString(rs["SupplState_Name"])}),
                            Weight = ((rs["Suppl_Weight"] == System.DBNull.Value) ? 0 : System.Convert.ToDouble(rs["Suppl_Weight"])), 
                            IsExcludeFromADJ = ((rs["Suppl_ExcludeFromAdj"] == System.DBNull.Value) ? false : System.Convert.ToBoolean(rs["Suppl_ExcludeFromAdj"]))
                        }
                        );
                    }
                }
                rs.Dispose();
                cmd.Dispose();
                DBConnection.Close();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить список заказов.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return objList;
        }
        #endregion

        #region Рассчитать цену в заказе
        /// <summary>
        /// Расчет цены заказа
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidSupplId">идентификатор заказа</param>
        /// <param name="strErr">сообщение об ошибке</param>
        /// <returns>результат работы хранимой процедуры в виде целого числа</returns>
        public static System.Int32 ProcessSuppl(UniXP.Common.CProfile objProfile, System.Guid uuidSupplId, ref System.String strErr)
        {
            System.Int32 iRet = 0;
            System.Data.SqlClient.SqlConnection DBConnection = null;
            System.Data.SqlClient.SqlCommand cmd = null;
            //            System.Data.SqlClient.SqlTransaction DBTransaction = null;
            System.Int32 iCmdTimeOut = 240;
            try
            {
                DBConnection = objProfile.GetDBSource();
                if (DBConnection == null)
                {
                    iRet = -1;
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                        "Не удалось получить соединение с базой данных.", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return iRet;
                }
                //DBTransaction = DBConnection.BeginTransaction();
                cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandTimeout = iCmdTimeOut;
                //cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.CommandText = System.String.Format("[{0}].[dbo].[usp_MakeSupplForRecalcPrices]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Suppl_Guid", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@Suppl_Guid"].Value = uuidSupplId;
                cmd.ExecuteNonQuery();

                iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                strErr = (System.String)cmd.Parameters["@ERROR_MES"].Value;

                //DBTransaction.Commit();
                cmd.Dispose();
                DBConnection.Close();
            }
            catch (System.Exception f)
            {
                //DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить список заказов.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return iRet;
        }
        #endregion

        #region Обнулить цены в заказе
        /// <summary>
        /// Обнуляет цены в заказе
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidSupplId">уникальный идентификатор заказа</param>
        /// <param name="strErr">сообщение об ошибке</param>
        /// <returns>результат работы хранимой процедуры в виде целого числа</returns>
        public static System.Int32 ClearPricesInSuppl(UniXP.Common.CProfile objProfile, System.Guid uuidSupplId, ref System.String strErr)
        {
            System.Int32 iRet = 0;
            System.Data.SqlClient.SqlConnection DBConnection = null;
            System.Data.SqlClient.SqlCommand cmd = null;
            System.Data.SqlClient.SqlTransaction DBTransaction = null;
            try
            {
                DBConnection = objProfile.GetDBSource();
                if (DBConnection == null)
                {
                    iRet = -1;
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                        "Не удалось получить соединение с базой данных.", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return iRet;
                }
                DBTransaction = DBConnection.BeginTransaction();
                cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[usp_ClearPricesInSuppl]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Suppl_Guid", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@Suppl_Guid"].Value = uuidSupplId;
                cmd.ExecuteNonQuery();

                iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                strErr = (System.String)cmd.Parameters["@ERROR_MES"].Value;

                if (iRet == 0)
                {
                    DBTransaction.Commit();
                }
                else
                {
                    DBTransaction.Rollback();
                }
                cmd.Dispose();
                DBConnection.Close();
            }
            catch (System.Exception f)
            {
                DBTransaction.Rollback();
                strErr = "Не удалось обнулить цены в заказе.\n\nТекст ошибки: " + f.Message;
            }
            return iRet;
        }
        #endregion

        #region Вернуть состояние заказа для повторного автоматического расчета цен
        /// <summary>
        /// Возвращает состояние заказа для повторного автоматического расчета цен
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidSupplId">уникальный идентификатор заказа</param>
        /// <param name="strErr">сообщение об ошибке</param>
        /// <returns>результат работы хранимой процедуры в виде целого числа</returns>
        public static System.Int32 ReturnSupplStateToAutoCalcPrices(UniXP.Common.CProfile objProfile, System.Guid uuidSupplId, ref System.String strErr)
        {
            System.Int32 iRet = 0;
            System.Data.SqlClient.SqlConnection DBConnection = null;
            System.Data.SqlClient.SqlCommand cmd = null;
            System.Data.SqlClient.SqlTransaction DBTransaction = null;
            try
            {
                DBConnection = objProfile.GetDBSource();
                if (DBConnection == null)
                {
                    iRet = -1;
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                        "Не удалось получить соединение с базой данных.", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return iRet;
                }
                DBTransaction = DBConnection.BeginTransaction();
                cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[usp_ReturnSupplToAutoProcessPrices]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Suppl_Guid", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@Suppl_Guid"].Value = uuidSupplId;
                cmd.ExecuteNonQuery();

                iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                strErr = (System.String)cmd.Parameters["@ERROR_MES"].Value;

                if (iRet == 0)
                {
                    DBTransaction.Commit();
                }
                else
                {
                    DBTransaction.Rollback();
                }
                cmd.Dispose();
                DBConnection.Close();
            }
            catch (System.Exception f)
            {
                DBTransaction.Rollback();
                strErr = "Не удалось вернуть заказ на повторную автоматическую обработку цен.\n\nТекст ошибки: " + f.Message;
            }
            return iRet;
        }
        #endregion

        #region Пометить заказ как удаленный
        /// <summary>
        /// Устанавливает состояние заказа в "удален"
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidSupplId">уникальный идентификатор заказа</param>
        /// <param name="strErr">сообщение об ошибке</param>
        /// <returns>результат работы хранимой процедуры в виде целого числа</returns>
        public static System.Int32 MakeSupplStateToDeleted(UniXP.Common.CProfile objProfile, System.Guid uuidSupplId, ref System.String strErr)
        {
            System.Int32 iRet = 0;
            System.Data.SqlClient.SqlConnection DBConnection = null;
            System.Data.SqlClient.SqlCommand cmd = null;
            System.Data.SqlClient.SqlTransaction DBTransaction = null;
            try
            {
                DBConnection = objProfile.GetDBSource();
                if (DBConnection == null)
                {
                    iRet = -1;
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                        "Не удалось получить соединение с базой данных.", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return iRet;
                }
                DBTransaction = DBConnection.BeginTransaction();
                cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[usp_MakeSupplDeleted]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Suppl_Guid", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@Suppl_Guid"].Value = uuidSupplId;
                cmd.ExecuteNonQuery();

                iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                strErr = (System.String)cmd.Parameters["@ERROR_MES"].Value;

                if (iRet == 0)
                {
                    DBTransaction.Commit();
                }
                else
                {
                    DBTransaction.Rollback();
                }
                cmd.Dispose();
                DBConnection.Close();
            }
            catch (System.Exception f)
            {
                DBTransaction.Rollback();
                strErr = "Не удалось изменить состояние заказа на \"удален\".\n\nТекст ошибки: " + f.Message;
            }
            return iRet;
        }
        #endregion

        #region Загрузить список продукции заказа
        /// <summary>
        /// Загружает список продукции заказа
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - удачное завершение операции; false - ошибка</returns>
        public System.Boolean LoadProductList(UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;
            System.Data.SqlClient.SqlConnection DBConnection = null;
            System.Data.SqlClient.SqlCommand cmd = null;
            try
            {
                if (this.SupplItmsList == null) { this.SupplItmsList = new List<CPDASupplItms>(); }
                else { this.SupplItmsList.Clear(); }

                DBConnection = objProfile.GetDBSource();
                if (DBConnection == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                        "Не удалось получить соединение с базой данных.", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return bRet;
                }
                cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[usp_GetSupplItems]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Suppl_Guid", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@Suppl_Guid"].Value = this.m_uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        this.SupplItmsList.Add(new CPDASupplItms(((System.String)rs["PARTS_NAME"] + " " + (System.String)rs["PARTS_ARTICLE"]),
                            System.Convert.ToInt32(rs["SupplItem_Quantity"]), System.Convert.ToInt32(rs["SupplItem_OrderQuantity"]),
                            System.Convert.ToDouble(rs["SupplItem_Price"]), System.Convert.ToDouble(rs["SupplItem_AllPrice"]),
                            System.Convert.ToDouble(rs["SupplItem_CurrencyPrice"]), System.Convert.ToDouble(rs["SupplItem_CurrencyAllPrice"]),
                            System.Convert.ToDouble(rs["SupplItem_Discount"]), System.Convert.ToDouble(rs["SupplItem_TotalPrice"]),
                            System.Convert.ToDouble(rs["SupplItem_CurrencyTotalPrice"]), (System.String)rs["ProductOwnerName"],
                            (System.Boolean)rs["PropertieImporter"], System.Convert.ToDouble(rs["PropertieCharge"]),
                            System.Convert.ToDouble(rs["RetroDiscount"]), System.Convert.ToDouble(rs["FixDiscount"]),
                            System.Convert.ToDouble(rs["TradeEquipDiscount"]), System.Convert.ToDouble(rs["NDSPercent"]),
                            System.Convert.ToDouble(rs["SupplItem_DiscountPrice"]), System.Convert.ToDouble(rs["SupplItem_CurrencyDiscountPrice"]),
                            (System.String)rs["SplItmsDescription"], (System.String)rs["SplItmsDiscountDescription"]
                            ));
                    }
                }
                rs.Dispose();
                cmd.Dispose();
                DBConnection.Close();
                bRet = true;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить содержимое заказа.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return true;

        }
        #endregion

        #region Включить/Исключить заказ из акта выполненных работ
        /// <summary>
        /// Включает/исключает заказ в акт выполненных работ
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidSupplId">УИ заказа</param>
        /// <param name="bExcludeFlag">признак "включить/исключить"</param>
        /// <param name="strErr">сообщение об ошибке</param>
        /// <returns>true - удачное завершение операции; false - ошибка</returns>
        public static System.Boolean ExcludeSupplFromADJ(UniXP.Common.CProfile objProfile, System.Guid uuidSupplId, System.Boolean bExcludeFlag, ref System.String strErr)
        {
            System.Boolean bRet = false;
            System.Data.SqlClient.SqlConnection DBConnection = null;
            System.Data.SqlClient.SqlCommand cmd = null;
            System.Int32 iCmdTimeOut = 240;
            try
            {
                DBConnection = objProfile.GetDBSource();
                if (DBConnection == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                        "Не удалось получить соединение с базой данных.", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return bRet;
                }
                cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandTimeout = iCmdTimeOut;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.CommandText = System.String.Format("[{0}].[dbo].[usp_ExcludeSupplFromADJ]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Suppl_Guid", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ExcludeFlag", System.Data.SqlDbType.Bit));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@Suppl_Guid"].Value = uuidSupplId;
                cmd.Parameters["@ExcludeFlag"].Value = bExcludeFlag;
                cmd.ExecuteNonQuery();

                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                strErr = (System.String)cmd.Parameters["@ERROR_MES"].Value;

                bRet = (iRet == 0);

                cmd.Dispose();
                DBConnection.Close();
            }
            catch (System.Exception f)
            {
                if (bExcludeFlag == true)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Не удалось исключить заказ из акта выполненных работ.\n\nТекст ошибки: " + f.Message, "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Не удалось включить заказ в акт выполненных работ.\n\nТекст ошибки: " + f.Message, "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            return bRet;
        }
        #endregion
    }
}

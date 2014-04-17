using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPMercuryProcessingOrder
{
    public class CProcessingOrderModuleClassInfo : UniXP.Common.CModuleClassInfo
    {
        public CProcessingOrderModuleClassInfo()
        {
            UniXP.Common.CLASSINFO objClassInfo;

            objClassInfo = new UniXP.Common.CLASSINFO();
            objClassInfo.enClassType = UniXP.Common.EnumClassType.mcView;
            objClassInfo.strClassName = "ERPMercuryProcessingOrder.ViewConditionGroup";
            objClassInfo.strName = "Группы условий";
            objClassInfo.strDescription = "Список групп для создания условий";
            objClassInfo.lID = 0;
            objClassInfo.nImage = 1;
            objClassInfo.strResourceName = "IMAGES_GROUPSMALL";
            m_arClassInfo.Add(objClassInfo);

            objClassInfo = new UniXP.Common.CLASSINFO();
            objClassInfo.enClassType = UniXP.Common.EnumClassType.mcView;
            objClassInfo.strClassName = "ERPMercuryProcessingOrder.ViewRuleCalculation";
            objClassInfo.strName = "Действия";
            objClassInfo.strDescription = "Список действий";
            objClassInfo.lID = 1;
            objClassInfo.nImage = 1;
            objClassInfo.strResourceName = "IMAGES_GROUPSMALL";
            m_arClassInfo.Add(objClassInfo);

            objClassInfo = new UniXP.Common.CLASSINFO();
            objClassInfo.enClassType = UniXP.Common.EnumClassType.mcView;
            objClassInfo.strClassName = "ERPMercuryProcessingOrder.ViewRulePool";
            objClassInfo.strName = "Правила";
            objClassInfo.strDescription = "Список правил";
            objClassInfo.lID = 2;
            objClassInfo.nImage = 1;
            objClassInfo.strResourceName = "IMAGES_GROUPSMALL";
            m_arClassInfo.Add(objClassInfo);

            objClassInfo = new UniXP.Common.CLASSINFO();
            objClassInfo.enClassType = UniXP.Common.EnumClassType.mcView;
            objClassInfo.strClassName = "ERPMercuryProcessingOrder.OrderListEditor";
            objClassInfo.strName = "Заказы";
            objClassInfo.strDescription = "Список заказов клиентов";
            objClassInfo.lID = 3;
            objClassInfo.nImage = 1;
            objClassInfo.strResourceName = "IMAGES_GROUPSMALL";
            m_arClassInfo.Add(objClassInfo);

            objClassInfo = new UniXP.Common.CLASSINFO();
            objClassInfo.enClassType = UniXP.Common.EnumClassType.mcView;
            objClassInfo.strClassName = "ERPMercuryProcessingOrder.ViewPDASupplList";
            objClassInfo.strName = "Заявки";
            objClassInfo.strDescription = "Список заказов клиентов";
            objClassInfo.lID = 4;
            objClassInfo.nImage = 1;
            objClassInfo.strResourceName = "shopping_cart_16";
            m_arClassInfo.Add(objClassInfo);

            objClassInfo = new UniXP.Common.CLASSINFO();
            objClassInfo.enClassType = UniXP.Common.EnumClassType.mcView;
            objClassInfo.strClassName = "ERPMercuryProcessingOrder.ViewWaybillList";
            objClassInfo.strName = "Накладные";
            objClassInfo.strDescription = "Журнал накладных";
            objClassInfo.lID = 5;
            objClassInfo.nImage = 1;
            objClassInfo.strResourceName = "shopping_cart_16";
            m_arClassInfo.Add(objClassInfo);

        }
    }

    public class CProcessingOrderModuleInfo : UniXP.Common.CClientModuleInfo
    {
        public CProcessingOrderModuleInfo()
            : base(Assembly.GetExecutingAssembly(),
                UniXP.Common.EnumDLLType.typeItem,
                new System.Guid("{C95285DB-E219-424A-94F8-18B8545B5EEE}"),
                new System.Guid("{A6319AD0-08C0-49ED-B25B-659BAB622B15}"),
                ERPMercuryProcessingOrder.Properties.Resources.IMAGES_STATETYPESMALL,
                ERPMercuryProcessingOrder.Properties.Resources.IMAGES_STATETYPESMALL)
        {
        }

        /// <summary>
        /// Выполняет операции по проверке правильности установки модуля в системе.
        /// </summary>
        /// <param name="objProfile">Профиль пользователя.</param>
        public override System.Boolean Check(UniXP.Common.CProfile objProfile)
        {
            return true;
        }
        /// <summary>
        /// Выполняет операции по установке модуля в систему.
        /// </summary>
        /// <param name="objProfile">Профиль пользователя.</param>
        public override System.Boolean Install(UniXP.Common.CProfile objProfile)
        {
            return true;
        }
        /// <summary>
        /// Выполняет операции по удалению модуля из системы.
        /// </summary>
        /// <param name="objProfile">Профиль пользователя.</param>
        public override System.Boolean UnInstall(UniXP.Common.CProfile objProfile)
        {
            return true;
        }
        /// <summary>
        /// Производит действия по обновлению при установке новой версии подключаемого модуля.
        /// </summary>
        /// <param name="objProfile">Профиль пользователя.</param>
        public override System.Boolean Update(UniXP.Common.CProfile objProfile)
        {
            return true;
        }
        /// <summary>
        /// Возвращает список доступных классов в данном модуле.
        /// </summary>
        public override UniXP.Common.CModuleClassInfo GetClassInfo()
        {
            return new CProcessingOrderModuleClassInfo();
        }
    }

    public class ModuleInfo : PlugIn.IModuleInfo
    {
        public UniXP.Common.CClientModuleInfo GetModuleInfo()
        {
            return new CProcessingOrderModuleInfo();
        }
    }
}

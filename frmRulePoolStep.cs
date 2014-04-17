using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ERPMercuryProcessingOrder
{
    public partial class frmRulePoolStep : DevExpress.XtraEditors.XtraForm
    {
        #region Свойства
        /// <summary>
        /// профайл
        /// </summary>
        private UniXP.Common.CProfile m_objProfile;
        /// <summary>
        /// объект "пул шагов"
        /// </summary>
        private ERP_Mercury.Common.CPoolRule m_objPoolRules;
        /// <summary>
        /// объект "Шаг"
        /// </summary>
        private ERP_Mercury.Common.CPoolRuleCalculationStep m_objStep;
        /// <summary>
        /// объект "Шаг"
        /// </summary>
        public ERP_Mercury.Common.CPoolRuleCalculationStep Step
        {
            get { return m_objStep; }
        }
        /// <summary>
        /// признак "создание нового шага"
        /// </summary>
        private System.Boolean m_bNewStep;
        private const System.String m_strNewStepName = "Новый шаг...";
        System.Boolean m_bCanEditPool;
        System.Boolean m_bCanEditPoolDescription;
        System.Boolean m_bEditOnlyStepDescription;
        #endregion

        #region Конструктор
        public frmRulePoolStep(UniXP.Common.CProfile objProfile)
        {
            InitializeComponent();

            m_objProfile = objProfile;
            m_objPoolRules = null;
            m_objStep = null;
            m_bNewStep = false;
            m_bEditOnlyStepDescription = false;

            m_bCanEditPool = m_objProfile.GetClientsRight().GetState(ERPMercuryProcessingOrder.Consts.strRightEditRulePool);
            m_bCanEditPoolDescription = m_objProfile.GetClientsRight().GetState(ERPMercuryProcessingOrder.Consts.strRightEditRulePoolDecription);
            DisableEnableControls();
        }
        private void DisableEnableControls()
        {
            try
            {
                txtName.Enabled = m_bCanEditPool;
                cboxRule.Properties.ReadOnly = !m_bCanEditPool;
                cboxActionSuccess.Properties.ReadOnly = !m_bCanEditPool;
                cboxActionFailure.Properties.ReadOnly = !m_bCanEditPool;
                dtBeginDate.Properties.ReadOnly = !m_bCanEditPool;
                dtEndDate.Properties.ReadOnly = !m_bCanEditPool;
                checkEnable.Properties.ReadOnly = !m_bCanEditPool;
                btnLeft.Enabled = m_bCanEditPool;
                btnLeftAll.Enabled = m_bCanEditPool;
                btnRight.Enabled = m_bCanEditPool;
                btnRightAll.Enabled = m_bCanEditPool;
                treeListParams.OptionsBehavior.Editable = m_bCanEditPool;

                txtRulePool_Description.Enabled = m_bCanEditPoolDescription;
                checkRulePool_IsCanShow.Enabled = m_bCanEditPoolDescription;

                btnSave.Enabled = (m_bCanEditPool || m_bCanEditPoolDescription);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка отключения элементов управления.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }
            return;
        }

        #endregion

        #region Новый шаг
        /// <summary>
        /// Создание нового шага в списке
        /// </summary>
        public void NewStep(ERP_Mercury.Common.CPoolRule objPoolRules)
        {
            try
            {
                DialogResult = DialogResult.None;
                m_objPoolRules = objPoolRules;
                Text = m_strNewStepName;

                // Загружаем список правил расчета цены в выпадающий список
                LoadRulesList();
                // Загружаем список всех групп условий
                RefreshConditionGroupsList();
                // Возможные действия: Выход, Переход к следующему
                AddDefValuesToActionList();

                txtName.EditValueChanged += new System.EventHandler(EditValueChanged);
                cboxRule.EditValueChanged += new System.EventHandler(cboxRuleSelectedValueChanged);
                cboxActionFailure.EditValueChanged += new System.EventHandler(EditValueChanged);
                cboxActionSuccess.EditValueChanged += new System.EventHandler(EditValueChanged);
                checkEnable.EditValueChanged += new System.EventHandler(EditValueChanged);
                dtBeginDate.EditValueChanged += new System.EventHandler(EditValueChanged);
                dtEndDate.EditValueChanged += new System.EventHandler(EditValueChanged);

                txtRulePool_Description.EditValueChanged += new System.EventHandler(EditValueChanged);
                checkRulePool_IsCanShow.EditValueChanged += new System.EventHandler(EditValueChanged);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка создания нового шага.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                txtName.Focus();
                SetModified(false);
                m_bNewStep = true;
                ShowDialog();
            }
            return;

        }
        /// <summary>
        /// Загружает список правил расчета цены в выпадающий список
        /// </summary>
        private void LoadRulesList()
        {
            try
            {
                cboxRule.Properties.Items.Clear();
                List<ERP_Mercury.Common.CRuleCalculation> objRulesList = ERP_Mercury.Common.CRuleCalculation.GetRuleCalculationList(m_objProfile, null);
                if ((objRulesList != null) && (objRulesList.Count > 0))
                {
                    foreach (ERP_Mercury.Common.CRuleCalculation objRule in objRulesList)
                    {
                        cboxRule.Properties.Items.Add(objRule);
                    }
                }
                objRulesList = null;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка загрузки списка правил.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                SetModified(false);
            }
            return;
        }
        /// <summary>
        /// Обновляет список групп
        /// </summary>
        private void RefreshConditionGroupsList()
        {
            try
            {
                lstboxAllGroupsList.Items.Clear();

//                List<ERP_Mercury.Common.CConditionGroup> objList = ERP_Mercury.Common.CConditionGroup.GetConditionGroupList(m_objProfile, null);

                List<ERP_Mercury.Common.CConditionGroup> objList = ERP_Mercury.Common.CConditionGroup.GetConditionGroupListWithoutStructure(m_objProfile, null);
                
                if ((objList == null) || (objList.Count == 0)) { return; }

                foreach (ERP_Mercury.Common.CConditionGroup objItem in objList)
                {
                    lstboxAllGroupsList.Items.Add(objItem);
                }
                objList = null;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка обновления списка групп.\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        /// <summary>
        /// Обновляет список параметров
        /// </summary>
        /// <param name="objRuleCalculation">действие, связанное с шагом</param>
        private void LoadParamList(ERP_Mercury.Common.CRuleCalculation objRuleCalculation)
        {
            try
            {
                treeListParams.Nodes.Clear();

                if ((objRuleCalculation == null) || (objRuleCalculation.AdvancedParamList == null) || (objRuleCalculation.AdvancedParamList.Count == 0)) { return; }

                foreach (ERP_Mercury.Common.CAdvancedParam objItem in objRuleCalculation.AdvancedParamList)
                {
                    DevExpress.XtraTreeList.Nodes.TreeListNode objNode =
                        treeListParams.AppendNode(new object[] { objItem.Name, objItem.DataType.Name, null }, null);

                    objNode.Tag = objItem;
                }
                LoadInfoAboutParams(objRuleCalculation);

            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка обновления списка параметров.\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        private void LoadInfoAboutParams(ERP_Mercury.Common.CRuleCalculation objRuleCalculation)
        {
            try
            {
                txtWarning.Items.Clear();

                if (objRuleCalculation != null)
                {
                    txtWarning.Items.Add(objRuleCalculation.StoredProcedure.Name);
                    txtWarning.Items.Add("");
                    txtWarning.Items.Add("Список параметров:");
                    txtWarning.Items.Add("");
                }
                foreach (ERP_Mercury.Common.CAdvancedParam objItem in objRuleCalculation.AdvancedParamList)
                {
                    txtWarning.Items.Add(objItem.Name);
                }


            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка обновления списка параметров.\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        #endregion

        #region Редактирование шага
        /// <summary>
        /// Редактирование шага
        /// </summary>
        /// <param name="objPoolRules">пул шагов</param>
        /// <param name="objStep">объект шаг</param>
        public void EditStep(ERP_Mercury.Common.CPoolRule objPoolRules, ERP_Mercury.Common.CPoolRuleCalculationStep objStep, System.Boolean bEditOnlyStepDescription)
        {
            try
            {
                DialogResult = DialogResult.None;
                m_objPoolRules = objPoolRules;
                m_objStep = objStep;
                m_bEditOnlyStepDescription = bEditOnlyStepDescription;

                txtName.EditValueChanged -= new System.EventHandler(EditValueChanged);
                cboxRule.EditValueChanged -= new System.EventHandler(cboxRuleSelectedValueChanged);
                cboxActionFailure.EditValueChanged -= new System.EventHandler(EditValueChanged);
                cboxActionSuccess.EditValueChanged -= new System.EventHandler(EditValueChanged);
                checkEnable.EditValueChanged -= new System.EventHandler(EditValueChanged);
                dtBeginDate.EditValueChanged -= new System.EventHandler(EditValueChanged);
                dtEndDate.EditValueChanged -= new System.EventHandler(EditValueChanged);
                treeListParams.CellValueChanging -= new DevExpress.XtraTreeList.CellValueChangedEventHandler(treeListParamsCellValueChanging);
                txtRulePool_Description.EditValueChanged -= new System.EventHandler(EditValueChanged);
                checkRulePool_IsCanShow.EditValueChanged -= new System.EventHandler(EditValueChanged);

                txtName.Text = m_objStep.Name;
                dtBeginDate.DateTime = m_objStep.BeginDate;
                dtEndDate.DateTime = m_objStep.EndDate;
                checkEnable.Checked = m_objStep.Enable;
                txtRulePool_Description.Text = m_objStep.RulePool_Description;
                checkRulePool_IsCanShow.Checked = m_objStep.RulePool_IsCanShow;

                if (bEditOnlyStepDescription == false)
                {
                    // Загружаем список правил расчета цены в выпадающий список
                    LoadRulesList();
                    // список параметров
                    treeListParams.Nodes.Clear();
                    if ((m_objStep.AdvancedParamList != null) && (m_objStep.AdvancedParamList.Count > 0))
                    {
                        foreach (ERP_Mercury.Common.CAdvancedParam objItem in m_objStep.AdvancedParamList)
                        {
                            DevExpress.XtraTreeList.Nodes.TreeListNode objNode =
                                treeListParams.AppendNode(new object[] { objItem.Name, objItem.DataType.Name, objItem.Value }, null);

                            objNode.Tag = objItem;
                        }
                        // у действия, связанного с правилом, есь дополнительные параметры
                        // проверим, все ли они отрисовались в дереве
                        if ((m_objStep.RuleCalculation != null) && (m_objStep.RuleCalculation.AdvancedParamList != null))
                        {
                            ERP_Mercury.Common.CRuleCalculation objRuleCalulation = null;
                            foreach (ERP_Mercury.Common.CRuleCalculation objItem in cboxRule.Properties.Items)
                            {
                                if (objItem.ID.CompareTo(m_objStep.RuleCalculation.ID) == 0)
                                {
                                    objRuleCalulation = objItem;
                                    break;
                                }
                            }
                            if (objRuleCalulation != null)
                            {
                                System.Boolean bExists = false;
                                foreach (ERP_Mercury.Common.CAdvancedParam objItem in objRuleCalulation.AdvancedParamList)
                                {
                                    bExists = false;
                                    foreach (ERP_Mercury.Common.CAdvancedParam objItem2 in m_objStep.AdvancedParamList)
                                    {
                                        if (objItem.Name == objItem2.Name)
                                        {
                                            bExists = true;
                                            break;
                                        }
                                    }
                                    if (bExists == false)
                                    {
                                        DevExpress.XtraTreeList.Nodes.TreeListNode objNode =
                                            treeListParams.AppendNode(new object[] { objItem.Name, objItem.DataType.Name, objItem.Value }, null);
                                        objNode.Tag = objItem;
                                    }

                                }
                            }
                        }
                        LoadInfoAboutParams(m_objStep.RuleCalculation);
                    }
                    else
                    {
                        LoadParamList(m_objStep.RuleCalculation);
                    }

                    // Загружаем список всех групп условий
                    RefreshConditionGroupsList();

                    cboxRule.SelectedItem = cboxRule.Properties.Items.Cast<ERP_Mercury.Common.CRuleCalculation>().SingleOrDefault<ERP_Mercury.Common.CRuleCalculation>(x => x.ID.Equals(m_objStep.RuleCalculation.ID) == true);

                    //for (System.Int32 i = 0; i < cboxRule.Properties.Items.Count; i++)
                    //{
                    //    if (((ERP_Mercury.Common.CRuleCalculation)cboxRule.Properties.Items[i]).ID.CompareTo(m_objStep.RuleCalculation.ID) == 0)
                    //    {
                    //        cboxRule.SelectedItem = cboxRule.Properties.Items[i];
                    //        break;
                    //    }
                    //}
                    // теперь списки с возможными действиями
                    // варианты: выход, к следующему, переход к ... - этот к должен быть минимум "Текущий + 2"
                    AddDefValuesToActionList();

                    ERP_Mercury.Common.CPoolItemAction objAction = null;
                    System.String strStepName = "";
                    foreach (ERP_Mercury.Common.CPoolRuleCalculationStep objStepItem in m_objPoolRules.Steps)
                    {
                        if (objStepItem.StepID > (m_objStep.StepID + 1))
                        {
                            strStepName = ERP_Mercury.Common.CPoolItemAction.GoToStepPrefix + objStepItem.StepID.ToString() + " - " + objStepItem.Name;
                            objAction = new ERP_Mercury.Common.CPoolItemAction(objStepItem.ID, ERP_Mercury.Common.enRuleActionType.GoToStep, strStepName);
                            cboxActionSuccess.Properties.Items.Add(objAction);
                            cboxActionFailure.Properties.Items.Add(objAction);
                        }
                    }

                    // списки заполнены, теперь нужно выбрать значения в них
                    for (System.Int32 i2 = 0; i2 < cboxActionSuccess.Properties.Items.Count; i2++)
                    {
                        if (((ERP_Mercury.Common.CPoolItemAction)cboxActionSuccess.Properties.Items[i2]).enumRuleActionType == m_objStep.ActionSuccess.enumRuleActionType)
                        {
                            if (((ERP_Mercury.Common.CPoolItemAction)cboxActionSuccess.Properties.Items[i2]).enumRuleActionType == ERP_Mercury.Common.enRuleActionType.GoToStep)
                            {
                                if (((ERP_Mercury.Common.CPoolItemAction)cboxActionSuccess.Properties.Items[i2]).StepID.CompareTo(m_objStep.ActionSuccess.StepID) == 0)
                                {
                                    cboxActionSuccess.SelectedItem = cboxActionSuccess.Properties.Items[i2];
                                    break;
                                }
                            }
                            else
                            {
                                cboxActionSuccess.SelectedItem = cboxActionSuccess.Properties.Items[i2];
                                break;
                            }
                        }
                    }
                    for (System.Int32 i3 = 0; i3 < cboxActionFailure.Properties.Items.Count; i3++)
                    {
                        if (((ERP_Mercury.Common.CPoolItemAction)cboxActionFailure.Properties.Items[i3]).enumRuleActionType == m_objStep.ActionFailure.enumRuleActionType)
                        {
                            if (((ERP_Mercury.Common.CPoolItemAction)cboxActionSuccess.Properties.Items[i3]).enumRuleActionType == ERP_Mercury.Common.enRuleActionType.GoToStep)
                            {
                                if (((ERP_Mercury.Common.CPoolItemAction)cboxActionFailure.Properties.Items[i3]).StepID.CompareTo(m_objStep.ActionFailure.StepID) == 0)
                                {
                                    cboxActionFailure.SelectedItem = cboxActionFailure.Properties.Items[i3];
                                    break;
                                }
                            }
                            else
                            {
                                cboxActionFailure.SelectedItem = cboxActionFailure.Properties.Items[i3];
                                break;
                            }
                        }
                    }

                    lstboxRuleGroupList.Items.Clear();
                    if (m_objStep.ConditionGroupList != null)
                    {
                        foreach (ERP_Mercury.Common.CConditionGroup objGroup in m_objStep.ConditionGroupList)
                        {
                            lstboxRuleGroupList.Items.Add(objGroup);
                        }
                    }
                }

                txtName.EditValueChanged += new System.EventHandler(EditValueChanged);
                cboxRule.EditValueChanged += new System.EventHandler(cboxRuleSelectedValueChanged);
                cboxActionFailure.EditValueChanged += new System.EventHandler(EditValueChanged);
                cboxActionSuccess.EditValueChanged += new System.EventHandler(EditValueChanged);
                checkEnable.EditValueChanged += new System.EventHandler(EditValueChanged);
                dtBeginDate.EditValueChanged += new System.EventHandler(EditValueChanged);
                dtEndDate.EditValueChanged += new System.EventHandler(EditValueChanged);
                treeListParams.CellValueChanging += new DevExpress.XtraTreeList.CellValueChangedEventHandler(treeListParamsCellValueChanging);

                txtRulePool_Description.EditValueChanged -= new System.EventHandler(EditValueChanged);
                checkRulePool_IsCanShow.EditValueChanged -= new System.EventHandler(EditValueChanged);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка редактирования шага.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                txtName.Focus();
                SetModified(true);

                if (m_bEditOnlyStepDescription == true)
                {
                    tabPage1.PageVisible = false;
                    tabPage2.PageVisible = false;
                    tabPage3.PageVisible = false;

                    tabControl.SelectedTabPage = tabPageDescription;
                    txtRulePool_Description.Focus();
                    btnSave.Enabled = true;
                }

                ShowDialog();
            }
            return;
        }
        private void AddDefValuesToActionList()
        {
            try
            {
                ERP_Mercury.Common.CPoolItemAction objActionExit = new ERP_Mercury.Common.CPoolItemAction(System.Guid.Empty, ERP_Mercury.Common.enRuleActionType.Exit, "");
                ERP_Mercury.Common.CPoolItemAction objActionNextStep = new ERP_Mercury.Common.CPoolItemAction(System.Guid.Empty, ERP_Mercury.Common.enRuleActionType.GoToNextStep, "");

                cboxActionSuccess.Properties.Items.Clear();
                cboxActionFailure.Properties.Items.Clear();
                cboxActionSuccess.Properties.Items.Add(objActionExit);
                cboxActionSuccess.Properties.Items.Add(objActionNextStep);
                cboxActionFailure.Properties.Items.Add(objActionExit);
                cboxActionFailure.Properties.Items.Add(objActionNextStep);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("AddDefValuesToActionList.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return;
        }
        #endregion

        #region Добавить/Удалить группу в списке
        /// <summary>
        /// Включает/выключает кнопки
        /// </summary>
        private void EnableBtnsForGroups()
        {
            try
            {
                // клавиши "вправо"
                btnRight.Enabled = ((lstboxRuleGroupList.ItemCount > 0) && (lstboxRuleGroupList.SelectedIndex >= 0)); ;
                btnRightAll.Enabled = (lstboxRuleGroupList.ItemCount > 0);

                // клавиши "влево"
                btnLeft.Enabled = ((lstboxAllGroupsList.ItemCount > 0) && (lstboxAllGroupsList.SelectedIndex >= 0));
                btnLeftAll.Enabled = (lstboxAllGroupsList.ItemCount > 0);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка метода EnableBtnsForGroups.\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        /// <summary>
        /// Добавляет запись в список групп правила
        /// </summary>
        private void PressLeft()
        {
            try
            {
                if (lstboxAllGroupsList.ItemCount == 0) { return; }
                if (lstboxAllGroupsList.SelectedItem == null) { return; }

                ERP_Mercury.Common.CConditionGroup objGroupSelected = (ERP_Mercury.Common.CConditionGroup)lstboxAllGroupsList.SelectedItem;
                // проверим, нет ли слева такой записи
                System.Boolean bDublicate = false;
                for (System.Int32 i = 0; i < lstboxRuleGroupList.ItemCount; i++)
                {
                    if (objGroupSelected.ID.CompareTo(((ERP_Mercury.Common.CConditionGroup)lstboxRuleGroupList.Items[i]).ID) == 0)
                    {
                        bDublicate = true;
                        break;
                    }
                }
                if (bDublicate == true) { return; }

                // добавляем группу в список
                lstboxRuleGroupList.Items.Add(objGroupSelected);
                EnableBtnsForGroups();

                // включаем индикацию изменений
                SetModified(true);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка метода PressLeft.\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        /// <summary>
        /// Добавляет все записи в список групп правила
        /// </summary>
        private void PressLeftAll()
        {
            try
            {
                if (lstboxAllGroupsList.ItemCount == 0) { return; }

                // добавляем все записи
                lstboxRuleGroupList.Items.Clear();
                for (System.Int32 i = 0; i < lstboxAllGroupsList.ItemCount; i++)
                {
                    lstboxRuleGroupList.Items.Add((ERP_Mercury.Common.CConditionGroup)lstboxAllGroupsList.Items[i]);
                }
                // выделяем
                lstboxRuleGroupList.SelectedIndex = lstboxRuleGroupList.Items.Count - 1;

                // проверяем клавиши
                EnableBtnsForGroups();

                // включаем индикацию изменений
                SetModified(true);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка метода PressLeftAll.\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        /// <summary>
        /// Удаляет запись из списка групп правила
        /// </summary>
        private void PressRight()
        {
            try
            {
                if (lstboxRuleGroupList.ItemCount == 0) { return; }
                if (lstboxRuleGroupList.SelectedItem == null) { return; }

                // удаляем элемент списка
                System.Int32 iSelectIndx = lstboxRuleGroupList.SelectedIndex;
                lstboxRuleGroupList.Items.RemoveAt(lstboxRuleGroupList.SelectedIndex);

                // выделяем
                lstboxRuleGroupList.SelectedIndex = (iSelectIndx > 0) ? (iSelectIndx - 1) : 0;

                // проверяем клавиши
                EnableBtnsForGroups();

                // включаем индикацию изменений
                SetModified(true);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка метода PressRight.\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        /// <summary>
        /// Удаляет все записи из списка групп правила
        /// </summary>
        private void PressRightAll()
        {
            try
            {
                if (lstboxRuleGroupList.ItemCount == 0) { return; }

                // удаляем слева все записи
                lstboxRuleGroupList.Items.Clear();

                // проверяем клавиши
                EnableBtnsForGroups();

                // включаем индикацию изменений
                SetModified(true);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка метода PressRightAll.\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            try
            {
                PressLeft();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка нажатия клавиши \"Влево\".\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }

        private void btnLeftAll_Click(object sender, EventArgs e)
        {
            try
            {
                PressLeftAll();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка нажатия клавиши \"Влево все\".\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            try
            {
                PressRight();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка нажатия клавиши \"Вправо\".\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }

        private void btnRightAll_Click(object sender, EventArgs e)
        {
            try
            {
                PressRightAll();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка нажатия клавиши \"Вправо все\".\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        private void lstboxAllGroupsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // проверяем клавиши
                EnableBtnsForGroups();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка выбора записи.\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        private void OpenGroupProperties(object objSelectedItem)
        {
            if (objSelectedItem == null) { return; }
            try
            {
                ERP_Mercury.Common.CConditionGroup objGroup = (ERP_Mercury.Common.CConditionGroup)objSelectedItem;

                ERP_Mercury.Common.frmGroupProperties objfrmGroupProperties = new ERP_Mercury.Common.frmGroupProperties(m_objProfile);
                objfrmGroupProperties.EditGroup(objGroup);
                DialogResult objDialogResult = objfrmGroupProperties.DialogResult;
                objfrmGroupProperties.Dispose();
                objfrmGroupProperties = null;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка метода OpenGroupProperties.\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        private void lstboxAllGroupsList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (lstboxAllGroupsList.SelectedItem != null)
                {
                    OpenGroupProperties(lstboxAllGroupsList.SelectedItem);
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка метода lstboxAllGroupsList_MouseDoubleClick.\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        private void lstboxRuleGroupList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (lstboxRuleGroupList.SelectedItem != null)
                {
                    OpenGroupProperties(lstboxRuleGroupList.SelectedItem);
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка метода lstboxRuleGroupList_MouseDoubleClick.\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }

        #endregion
        
        #region Индикация изменений
        private System.Boolean m_bIsModified;
        /// <summary>
        /// Устанавливает индикатор "изменена запись"
        /// </summary>
        private void SetModified(System.Boolean bModified)
        {
            try
            {
                m_bIsModified = bModified;
                btnSave.Enabled = bModified;
                btnCancel.Enabled = bModified;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка метода SetModified().\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return;
        }
        /// <summary>
        /// Возвращает признак того, изменялась ли запись
        /// </summary>
        /// <returns>true - изменялась; false - не изменялась</returns>
        private System.Boolean IsModified()
        {
            System.Boolean bRes = false;
            try
            {
                return m_bIsModified;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
            return bRes;
        }
        private void EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                SetModified(true);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( "EditValueChanged\n\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
        }
        private void treeListParamsCellValueChanging(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            try
            {
                if( ( e.Column == colParamValue ) && (e.Node.Tag != null))
                {
                    ((ERP_Mercury.Common.CAdvancedParam)e.Node.Tag).Value = (System.String)e.Value;
                }
                SetModified(true);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("treeListParamsCellValueChanging\n\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
        }
        private void cboxRuleSelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboxRule.SelectedItem != null)
                {
                    ERP_Mercury.Common.CRuleCalculation objRule = (ERP_Mercury.Common.CRuleCalculation)cboxRule.SelectedItem;
                    if ((objRule != null) && (objRule.AdvancedParamList != null))
                    {
                        LoadParamList(objRule);
                        SetModified(true);
                    }
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("cboxRuleSelectedValueChanged\n\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region Сохранить изменения
        /// <summary>
        /// Подтвердить изменения
        /// </summary>
        private void SaveChanges()
        {
            try
            {
                // сперва проверим, все ли значения внесены
                if ((txtName.Text == "") || (cboxRule.SelectedItem == null) || (cboxActionSuccess.SelectedItem == null) ||
                    (cboxActionFailure.SelectedItem == null))
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( "Укажите, пожалуйста, все значения!", "Внимание!",
                      System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return;
                }
                if (treeListParams.Nodes.Count > 0)
                {
                    System.Boolean bOk = true;
                    foreach (DevExpress.XtraTreeList.Nodes.TreeListNode objNode in treeListParams.Nodes)
                    {
                        
                        if ( (objNode.GetValue(colParamValue) == null) || ( ((System.String)objNode.GetValue(colParamValue)).Length == 0) )
                        {
                            bOk = false;
                            break;
                        }
                    }
                    if (bOk == false)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("Укажите, пожалуйста, все значения параметров!", "Внимание!",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                        return;

                    }
                }
                if (m_bNewStep == true)
                {
                    // новый шаг
                    System.Int32 iStepID = (m_objPoolRules.Steps.Count == 0) ? 0 : m_objPoolRules.Steps.Count;
                    m_objStep = new ERP_Mercury.Common.CPoolRuleCalculationStep(System.Guid.Empty, iStepID, txtName.Text,
                        (ERP_Mercury.Common.CRuleCalculation)cboxRule.SelectedItem,
                        (ERP_Mercury.Common.CPoolItemAction)cboxActionSuccess.SelectedItem, (ERP_Mercury.Common.CPoolItemAction)cboxActionFailure.SelectedItem, 
                        checkEnable.Checked, dtBeginDate.DateTime, dtEndDate.DateTime, "");

                    if (m_objStep.ConditionGroupList == null) { m_objStep.ConditionGroupList = new List<ERP_Mercury.Common.CConditionGroup>(); }
                    if (m_objStep.AdvancedParamList == null) { m_objStep.AdvancedParamList = new List<ERP_Mercury.Common.CAdvancedParam>(); }
                    if (m_objStep.xmldocAdvancedParamList == null) { m_objStep.xmldocAdvancedParamList = new System.Xml.XmlDocument(); }

                    for (System.Int32 i = 0; i < lstboxRuleGroupList.Items.Count; i++)
                    {
                        m_objStep.ConditionGroupList.Add((ERP_Mercury.Common.CConditionGroup)lstboxRuleGroupList.Items[i]);
                    }
                    // параметры
                    m_objStep.xmldocAdvancedParamList = m_objStep.RuleCalculation.xmldocAdvancedParamList;
                    m_objStep.AdvancedParamList.Clear();
                    ERP_Mercury.Common.CAdvancedParam advParam = null;
                    System.Xml.XmlNode objNode = m_objStep.xmldocAdvancedParamList.ChildNodes[0]; // УЗЕЛ "SP_Param"
                    for (System.Int32 i = 0; i < treeListParams.Nodes.Count; i++)
                    {
                        if (treeListParams.Nodes[i].Tag == null) { continue; }
                        advParam = (ERP_Mercury.Common.CAdvancedParam)treeListParams.Nodes[i].Tag;
                        if (advParam == null) { continue; }
                        //advParam.Value = (System.String)treeListParams.Nodes[i].GetValue(colParamValue);
                        m_objStep.AdvancedParamList.Add(advParam);
                        objNode.ChildNodes[i].Attributes["Value"].Value = advParam.Value;
                    }

                    if (m_objStep.AddStepToDB(m_objProfile) == true)
                    {
                        if (m_bCanEditPoolDescription == true)
                        {
                            System.String strErr = "";
                            if (ERP_Mercury.Common.CPoolRuleCalculationStep.MakeDescriptionToStepInRulePool(m_objProfile, m_objStep.ID, false,
                                m_objStep.RulePool_Description, m_objStep.RulePool_IsCanShow, ref strErr) == true)
                            {
                                DialogResult = DialogResult.OK;
                                Close();
                            }
                            else
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show(
                                   "Ошибка сохранения изменений в описании акции.\n\nТекст ошибки: " + strErr, "Ошибка",
                                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            DialogResult = DialogResult.OK;
                            Close();
                        }
                    }
                }
                else
                {
                    m_objStep.Name = txtName.Text;
                    m_objStep.RuleCalculation = (ERP_Mercury.Common.CRuleCalculation)cboxRule.SelectedItem;
                    m_objStep.ActionSuccess = (ERP_Mercury.Common.CPoolItemAction)cboxActionSuccess.SelectedItem;
                    m_objStep.ActionFailure = (ERP_Mercury.Common.CPoolItemAction)cboxActionFailure.SelectedItem;
                    m_objStep.Enable = checkEnable.Checked;
                    m_objStep.BeginDate = dtBeginDate.DateTime;
                    m_objStep.EndDate = dtEndDate.DateTime;
                    m_objStep.RulePool_Description = txtRulePool_Description.Text;
                    m_objStep.RulePool_IsCanShow = checkRulePool_IsCanShow.Checked;

                    m_objStep.ConditionGroupList.Clear();
                    for (System.Int32 i = 0; i < lstboxRuleGroupList.Items.Count; i++)
                    {
                        m_objStep.ConditionGroupList.Add((ERP_Mercury.Common.CConditionGroup)lstboxRuleGroupList.Items[i]);
                    }

                    m_objStep.xmldocAdvancedParamList = m_objStep.RuleCalculation.xmldocAdvancedParamList;
                    if (m_objStep.AdvancedParamList == null)
                    {
                        m_objStep.AdvancedParamList = new List<ERP_Mercury.Common.CAdvancedParam>();
                    }
                    else
                    {
                        m_objStep.AdvancedParamList.Clear();
                    }
                    ERP_Mercury.Common.CAdvancedParam advParam = null;
                    System.Xml.XmlNode objNode = m_objStep.xmldocAdvancedParamList.ChildNodes[0]; // УЗЕЛ "SP_Param"
                    for (System.Int32 i = 0; i < treeListParams.Nodes.Count; i++)
                    {
                        if(treeListParams.Nodes[ i ].Tag == null){continue;}
                        advParam = (ERP_Mercury.Common.CAdvancedParam)treeListParams.Nodes[ i ].Tag;
                        if( advParam == null){continue;}
                        //advParam.Value = (System.String)treeListParams.Nodes[i].GetValue(colParamValue);
                        m_objStep.AdvancedParamList.Add( advParam );
                        objNode.ChildNodes[i].Attributes["Value"].Value = advParam.Value;
                    }

                    if (m_bCanEditPoolDescription == true)
                    {
                        System.String strErr = "";
                        if (ERP_Mercury.Common.CPoolRuleCalculationStep.MakeDescriptionToStepInRulePool(m_objProfile, m_objStep.ID, false,
                            m_objStep.RulePool_Description, m_objStep.RulePool_IsCanShow, ref strErr) == false)
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(
                               "Ошибка сохранения изменений в описании акции.\n\nТекст ошибки: " + strErr, "Ошибка",
                               System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        }
                    }

                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                   "Ошибка сохранения изменений.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return;
        }
        private void SaveOnlyDescriptionChanges()
        {
            try
            {
                if (m_bCanEditPoolDescription == true)
                {
                    System.String strErr = "";
                    if (ERP_Mercury.Common.CPoolRuleCalculationStep.MakeDescriptionToStepInRulePool(m_objProfile, m_objStep.ID, false,
                        txtRulePool_Description.Text, checkRulePool_IsCanShow.Checked, ref strErr) == false)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(
                            "Ошибка сохранения изменений в описании акции.\n\nТекст ошибки: " + strErr, "Ошибка",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }
                    else
                    {
                        m_objStep.RulePool_Description = txtRulePool_Description.Text;
                        m_objStep.RulePool_IsCanShow = checkRulePool_IsCanShow.Checked;
                    }
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                   "Ошибка сохранения изменений в описании акции.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsModified() == false)
                {
                    DialogResult = DialogResult.None;
                    Close();
                }
                else
                {
                    if (m_bEditOnlyStepDescription == true)
                    {
                        SaveOnlyDescriptionChanges();
                    }
                    else
                    {
                        SaveChanges();
                    }
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                   "Ошибка отмены внесенных изменений.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return;
        }
        #endregion

        #region Отменить изменения
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.Windows.Forms.MessageBox.Show(this,
                   "Отменить все сделанные изменения?", "Подтверждение",
                   System.Windows.Forms.MessageBoxButtons.YesNoCancel,
                   System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    CancelChanges();
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                   "Ошибка отмены внесенных изменений.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return;
        }
        /// <summary>
        /// Отменяет сделанные в списке изменения
        /// </summary>
        private void CancelChanges()
        {
            try
            {
                if (m_bNewStep == true) { m_objStep = null; }
                DialogResult = DialogResult.Cancel;
                Close();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Ошибка отмены внесенных изменений.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        #endregion
    }
}

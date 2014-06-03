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
    public partial class frmRulePool : DevExpress.XtraEditors.XtraForm
    {
        #region Свойства
        /// <summary>
        /// профайл
        /// </summary>
        private UniXP.Common.CProfile m_objProfile;
        /// <summary>
        /// пул правил
        /// </summary>
        private ERP_Mercury.Common.CPoolRule m_objPoolRules;
        /// <summary>
        /// список удаляемых шагов
        /// </summary>
        private List<ERP_Mercury.Common.CPoolRuleCalculationStep> m_objDeletedStepsList;
        private UniXP.Common.MENUITEM m_objMenuItem;
        #endregion

        #region Конструктор
        public frmRulePool(UniXP.Common.CProfile objProfile, UniXP.Common.MENUITEM objMenuItem)
        {
            InitializeComponent();

            m_objProfile = objProfile;
            m_bIsModified = false;
            m_objPoolRules = new ERP_Mercury.Common.CPoolRule();
            m_objDeletedStepsList = new List<ERP_Mercury.Common.CPoolRuleCalculationStep>();
            m_objMenuItem = objMenuItem;

            LoadRuleTypeList();
            //splitContainerControl.SplitterPosition = 400;
        }
        /// <summary>
        /// Загружает список типов правил в cboxRuleType
        /// </summary>
        private void LoadRuleTypeList()
        {
            try
            {
                cboxRuleType.Properties.Items.Clear();
                List<ERP_Mercury.Common.CRuleType> objRuleTypeList = ERP_Mercury.Common.CRuleType.GetRuleTypeList(m_objProfile, null);
                if (objRuleTypeList != null)
                {
                    foreach (ERP_Mercury.Common.CRuleType objRuleType in objRuleTypeList)
                    {
                        cboxRuleType.Properties.Items.Add(objRuleType);
                    }
                    if (cboxRuleType.Properties.Items.Count > 0) { cboxRuleType.SelectedItem = cboxRuleType.Properties.Items[0]; };
                }
                objRuleTypeList = null;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка метода LoadRuleTypeList().\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return;
        }

        #endregion

        #region Индикация изменений в наборе данных

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
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(e.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
            return bRes;
        }
        /// <summary>
        /// Включение/Отключение клавиш
        /// </summary>
        private void EnableBarBottons()
        {
            try
            {
                barBtnAdd.Enabled = (IsModified() == false);
                barBtnDelete.Enabled = ((treeList.FocusedNode != null) && (treeList.FocusedNode.Tag != null));
                barBtnRefresh.Enabled = (IsModified() == false);
                barBtnUp.Enabled = ((treeList.FocusedNode != null) && (treeList.GetNodeIndex( treeList.FocusedNode ) > 0 ) && (treeList.FocusedNode.Tag != null));
                barBtnDown.Enabled = ((treeList.FocusedNode != null) && (treeList.GetNodeIndex(treeList.FocusedNode) < (treeList.Nodes.Count - 1)) && (treeList.FocusedNode.Tag != null));
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка отключения/включения клавиш.\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        private void treeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            try
            {
                EnableBarBottons();
                if (treeList.FocusedNode == null)
                {
                    ShowRupeStepPropertis(null);
                }
                else
                {
                    ShowRupeStepPropertis((ERP_Mercury.Common.CPoolRuleCalculationStep)treeList.FocusedNode.Tag);
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка смены записи.\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        #endregion

        #region Список объектов
        /// <summary>
        /// Обновляет список объектов
        /// </summary>
        /// <returns>true - успешное завершение; false - ошибка</returns>
        System.Boolean bRefreshRulePool()
        {
            System.Boolean bRet = false;
            try
            {
                this.tableLayoutPanel1.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.treeList)).BeginInit();
                
                m_objDeletedStepsList.Clear();
                System.Int32 iNodeIndx = (treeList.FocusedNode == null) ? 0 : treeList.GetNodeIndex(treeList.FocusedNode);
                treeList.FocusedNodeChanged -= new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeList_FocusedNodeChanged);
                treeList.Nodes.Clear();
                System.Guid uuidRuleType = ( cboxRuleType.SelectedItem == null ) ? System.Guid.Empty : ((ERP_Mercury.Common.CRuleType)cboxRuleType.SelectedItem).ID;

                if (m_objPoolRules.LoadStepsForRuleType(m_objProfile, uuidRuleType) == true)
                {
                    if ((m_objPoolRules.Steps != null) && (m_objPoolRules.Steps.Count > 0))
                    {
                        foreach (ERP_Mercury.Common.CPoolRuleCalculationStep objStep in m_objPoolRules.Steps)
                        {
                            DevExpress.XtraTreeList.Nodes.TreeListNode objNode =
                                treeList.AppendNode(new object[] { objStep.Enable, objStep.StepID, objStep.Name, 
                                    objStep.RuleCalculation.Name, objStep.BeginDate, objStep.EndDate,
                                objStep.ActionSuccess.Name}, null);

                            objNode.Tag = objStep;
                        }
                    }
                }


                treeList.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeList_FocusedNodeChanged);
                if (treeList.Nodes.Count > 0)
                {
                    if (iNodeIndx > (treeList.Nodes.Count - 1))
                    {
                        iNodeIndx = treeList.Nodes.Count - 1;
                    }
                    treeList.FocusedNode = treeList.Nodes[iNodeIndx];
                    ShowRupeStepPropertis((ERP_Mercury.Common.CPoolRuleCalculationStep)treeList.FocusedNode.Tag);
                }
                EnableBarBottons();
                
                this.tableLayoutPanel1.ResumeLayout(false);
                ((System.ComponentModel.ISupportInitialize)(this.treeList)).EndInit();
                
                bRet = true;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка обновления списка.\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                SetModified(false);
            }

            return bRet;
        }
        /// <summary>
        /// Обновляет список 
        /// </summary>
        private void RefreshRulePool()
        {
            try
            {
                // обновляем информацию
                bRefreshRulePool();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка обновления информации.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private void barBtnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                // обновляем информацию
                RefreshRulePool();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка обновления списка.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return;
        }
        /// <summary>
        /// Отображает свойства правила в элементах управления
        /// </summary>
        /// <param name="objStep">правило</param>
        private void ShowRupeStepPropertis(ERP_Mercury.Common.CPoolRuleCalculationStep objStep)
        {
            try
            {
                txtName.Text = "";
                txtRuleName.Text = "";
                txtBeginDate.Text = "";
                txtEndDate.Text = "";
                txtActionName.Text = "";
                txtError.Text = "";
                txtRuleDescription.Text = "";

                if (objStep == null) { return; }
                txtName.Text = objStep.Name;
                txtRuleName.Text = objStep.RuleCalculation.Name;
                txtBeginDate.Text = objStep.BeginDate.ToShortDateString();
                txtEndDate.Text = objStep.EndDate.ToShortDateString();
                txtActionName.Text = objStep.ActionSuccess.Name;
                txtError.Text = objStep.ActionFailure.Name;
                txtRuleDescription.Text = objStep.Description.Replace("; ", ";\r\n");
                txtRulePool_Description.Text = objStep.RulePool_Description;
                checkRulePool_IsCanShow.Checked = objStep.RulePool_IsCanShow;

            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка отображения свойств правила.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return;
        }
        #endregion

        #region Открытие формы
        private void frmRulePool_Load(object sender, EventArgs e)
        {
            try
            {
                if (bRefreshRulePool() == false)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка открытия формы.", "Ошибка",
                      System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка открытия формы.\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        #endregion

        #region Закрытие формы
        private void frmRulePool_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (IsModified() == true)
                {
                    if (DevExpress.XtraEditors.XtraMessageBox.Show(
                        "В списке были произведены изменения.\nВыйти из формы без сохранения изменений?", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.YesNo,
                        System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    // запускаем процесс сохранения
                    {
                        e.Cancel = true;
                        return;
                    }
                    else
                    {
                        DialogResult = DialogResult.Cancel;
                    }
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("frmRulePool_FormClosing\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        #endregion

        #region Перемещение шагов (вверх/вниз)
        /// <summary>
        /// Перемещение узла вверх
        /// </summary>
        /// <param name="objNode">перемещаемый узел</param>
        private void MoveUp(DevExpress.XtraTreeList.Nodes.TreeListNode objNode)
        {
            try
            {
                if ((objNode == null) || (objNode.Tag == null)) { return; }
                if (treeList.GetNodeIndex(objNode) == 0) { return; }

                ERP_Mercury.Common.CPoolRuleCalculationStep objStep = (ERP_Mercury.Common.CPoolRuleCalculationStep)objNode.Tag;
                if (objStep == null) { return; }

                this.tableLayoutPanel1.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.treeList)).BeginInit();

                System.Int32 iNewPosition = treeList.GetNodeIndex(objNode) - 1;
                if( iNewPosition < 0 ) {iNewPosition = 0;}

                treeList.SetNodeIndex(objNode, iNewPosition);
                objStep.StepID = iNewPosition;

                EnableBarBottons();

                RecheckStepID();

                if (IsModified() == false) { SetModified(true); }

                this.tableLayoutPanel1.ResumeLayout(false);
                ((System.ComponentModel.ISupportInitialize)(this.treeList)).EndInit();

            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("MoveUp\n\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        /// <summary>
        /// Перемещение узла вниз
        /// </summary>
        /// <param name="objNode">перемещаемый узел</param>
        private void MoveDown(DevExpress.XtraTreeList.Nodes.TreeListNode objNode)
        {
            try
            {
                if ((objNode == null) || (objNode.Tag == null)) { return; }
                if (treeList.GetNodeIndex(objNode) == (treeList.Nodes.Count - 1)) { return; }

                ERP_Mercury.Common.CPoolRuleCalculationStep objStep = (ERP_Mercury.Common.CPoolRuleCalculationStep)objNode.Tag;
                if (objStep == null) { return; }

                this.tableLayoutPanel1.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.treeList)).BeginInit();

                System.Int32 iNewPosition = treeList.GetNodeIndex(objNode) + 1;
                if (iNewPosition > (treeList.Nodes.Count - 1)) { iNewPosition = (treeList.Nodes.Count - 1); }

                treeList.SetNodeIndex(objNode, iNewPosition);
                objStep.StepID = iNewPosition;

                EnableBarBottons();

                RecheckStepID();

                if (IsModified() == false) { SetModified(true); }

                this.tableLayoutPanel1.ResumeLayout(false);
                ((System.ComponentModel.ISupportInitialize)(this.treeList)).EndInit();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("MoveDown\n\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        private void barBtnUp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if ((treeList.FocusedNode == null) || (treeList.FocusedNode.Tag == null)) { return; }
                MoveUp(treeList.FocusedNode);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка перемещения вверх.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        private void barBtnDown_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if ((treeList.FocusedNode == null) || (treeList.FocusedNode.Tag == null)) { return; }
                MoveDown(treeList.FocusedNode);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка перемещения DYBP.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        /// <summary>
        /// Завершение перетаскивания (отпустили мышку)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeList_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                DevExpress.XtraTreeList.TreeListHitInfo hi = treeList.CalcHitInfo(treeList.PointToClient(new Point(e.X, e.Y)));
                // узел, который перетаскивали
                DevExpress.XtraTreeList.Nodes.TreeListNode draggedNode =
                    (DevExpress.XtraTreeList.Nodes.TreeListNode)e.Data.GetData(typeof(DevExpress.XtraTreeList.Nodes.TreeListNode));
                if ((draggedNode != null) && ((e.KeyState & 4) != 4))
                {
                    //узел, над которым отпустили мышку
                    DevExpress.XtraTreeList.Nodes.TreeListNode node = hi.Node;
                    if (node != draggedNode)
                    {
                        treeList.SetNodeIndex(draggedNode, treeList.GetNodeIndex(node));
                        RecheckStepID();
                        if (IsModified() == false) { SetModified(true); }
                    }
                }
            }
            catch (System.Exception f)
            {
                System.Windows.Forms.MessageBox.Show(this,
                    "Ошибка treeList_DragDrop\n" + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        #endregion

        #region Новый шаг
        private void NewStep()
        {
            try
            {
                frmRulePoolStep objfrmRulePoolStep = new frmRulePoolStep(m_objProfile);
                objfrmRulePoolStep.NewStep(m_objPoolRules);
                if (objfrmRulePoolStep.DialogResult == DialogResult.OK)
                {
                    // был создан новый шаг, всё прошло хорошо, добавим запись в дерево и список
                    m_objPoolRules.Steps.Add(objfrmRulePoolStep.Step);

                    DevExpress.XtraTreeList.Nodes.TreeListNode objNode =
                        treeList.AppendNode(new object[] { objfrmRulePoolStep.Step.Enable, objfrmRulePoolStep.Step.StepID, objfrmRulePoolStep.Step.Name, 
                            objfrmRulePoolStep.Step.RuleCalculation.Name,
                            objfrmRulePoolStep.Step.BeginDate, objfrmRulePoolStep.Step.EndDate,
                            objfrmRulePoolStep.Step.ActionSuccess.Name}, null);

                    objNode.Tag = objfrmRulePoolStep.Step;
                }
                objfrmRulePoolStep.Dispose();
                objfrmRulePoolStep = null;
            }
            catch (System.Exception f)
            {
                System.Windows.Forms.MessageBox.Show(this,
                   "Ошибка создания нового шага.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return;
        }
        private void barBtnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                NewStep();
            }
            catch (System.Exception f)
            {
                System.Windows.Forms.MessageBox.Show(this,
                   "Ошибка создания нового шага.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return;
        }
        #endregion

        #region Редактировать шаг
        private void EditStep( DevExpress.XtraTreeList.Nodes.TreeListNode objNode, System.Boolean bEditOnlyStepDescription )
        {
            if (objNode.Tag == null) { return; }
            try
            {
                Cursor = Cursors.WaitCursor;

                ERP_Mercury.Common.CPoolRuleCalculationStep objStep = (ERP_Mercury.Common.CPoolRuleCalculationStep)objNode.Tag;
                if (objStep == null) { return; }
                frmRulePoolStep objfrmRulePoolStep = new frmRulePoolStep(m_objProfile);
                objfrmRulePoolStep.EditStep(m_objPoolRules, objStep, bEditOnlyStepDescription);
                if (objfrmRulePoolStep.DialogResult == DialogResult.OK)
                {
                    // шаг был изменен
                    objNode.SetValue(colStepName, objStep.Name);
                    objNode.SetValue(colRule, objStep.RuleCalculation.Name);
                    objNode.SetValue(colActionSuccess, objStep.ActionSuccess.Name);
                    //objNode.SetValue(colActionFailure, objStep.ActionFailure.Name);
                    objNode.SetValue(colEnable, objStep.Enable);
                    objNode.SetValue(colBeginDate, objStep.BeginDate);
                    objNode.SetValue(colEndDate, objStep.EndDate);
                    SetModified(true);
                    ShowRupeStepPropertis(objStep);
                }
                objfrmRulePoolStep.Dispose();
                objfrmRulePoolStep = null;
            }
            catch (System.Exception f)
            {
                System.Windows.Forms.MessageBox.Show(this,
                   "Ошибка изменения свойств шага.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            return;
        }
        private void treeList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if ((treeList.Nodes.Count == 0) || (treeList.FocusedNode == null) || (treeList.FocusedNode.Tag == null)) { return; }

                EditStep(treeList.FocusedNode, false);
            }
            catch (System.Exception f)
            {
                System.Windows.Forms.MessageBox.Show(this,
                   "Ошибка изменения свойств шага.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return;
        }
        #endregion

        #region Удалить шаг
        private void DeleteStep(DevExpress.XtraTreeList.Nodes.TreeListNode objNode)
        {
            if (objNode.Tag == null) { return; }
            try
            {
                ERP_Mercury.Common.CPoolRuleCalculationStep objStep = (ERP_Mercury.Common.CPoolRuleCalculationStep)objNode.Tag;
                if (objStep == null) { return; }

                System.Int32 iDeletIndx = -1;
                for (System.Int32 i = 0; i < m_objPoolRules.Steps.Count; i++)
                {
                    if (m_objPoolRules.Steps[i].ID.CompareTo(objStep.ID) == 0)
                    {
                        iDeletIndx = i;
                        break;
                    }
                }
                if (iDeletIndx >= 0)
                {
                    m_objPoolRules.Steps.RemoveAt(iDeletIndx);
                    m_objDeletedStepsList.Add(objStep);
                    objNode.Tag = null;
                    treeList.Nodes.Remove(objNode);
                    RecheckStepID();
                    SetModified(true);
                }
            }
            catch (System.Exception f)
            {
                System.Windows.Forms.MessageBox.Show(this,
                   "Ошибка удаления шага.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return;
        }
        private void barBtnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if ((treeList.Nodes.Count == 0) || (treeList.FocusedNode == null) || (treeList.FocusedNode.Tag == null)) { return; }
                DeleteStep(treeList.FocusedNode);
            }
            catch (System.Exception f)
            {
                System.Windows.Forms.MessageBox.Show(this,
                   "Ошибка удаления шага.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return;
        }
        #endregion

        #region Пересчет нумерации шагов
        /// <summary>
        /// Пересчет нумерации шагов
        /// </summary>
        private void RecheckStepID()
        {
            try
            {
                if (treeList.Nodes.Count == 0) { return; }

                this.tableLayoutPanel1.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.treeList)).BeginInit();
                
                foreach (DevExpress.XtraTreeList.Nodes.TreeListNode objNode in treeList.Nodes)
                {
                    if (objNode.Tag != null)
                    {
                        ERP_Mercury.Common.CPoolRuleCalculationStep objStep = (ERP_Mercury.Common.CPoolRuleCalculationStep)objNode.Tag;
                        if (objStep == null) { continue; }
                        objStep.StepID = treeList.GetNodeIndex(objNode);
                        objNode.SetValue(colStepID, objStep.StepID);
                    }
                }

                this.tableLayoutPanel1.ResumeLayout(false);
                ((System.ComponentModel.ISupportInitialize)(this.treeList)).EndInit();

            }
            catch (System.Exception f)
            {
                System.Windows.Forms.MessageBox.Show(this,
                   "Ошибка пересчета нумерации шагов.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return;
        }
        #endregion

        #region Сохранение изменений
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.Windows.Forms.MessageBox.Show(this,
                   "Сохранить изменения?", "Подтверждение",
                   System.Windows.Forms.MessageBoxButtons.YesNoCancel,
                   System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    bSaveChangesInPool();
                }
            }
            catch (System.Exception f)
            {
                System.Windows.Forms.MessageBox.Show(this,
                   "Ошибка сохранения изменений.\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return;
        }

        /// <summary>
        /// Сохраняет изменения в БД
        /// </summary>
        /// <returns>true - успешное завершение; false - ошибка</returns>
        private System.Boolean bSaveChangesInPool()
        {
            System.Boolean bRet = false;
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            try
            {
                if (m_objPoolRules.SaveChangesInStepList(m_objProfile, m_objDeletedStepsList) == true)
                {
                    m_objDeletedStepsList.Clear();
                    SetModified(false);
                    bRet = true;
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Не удалось сохранить изменения в БД.\n\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
            return bRet;
        }

        #endregion

        #region Отмена изменений
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.Windows.Forms.MessageBox.Show(this,
                   "Отменить все сделанные изменения?", "Подтверждение",
                   System.Windows.Forms.MessageBoxButtons.YesNoCancel,
                   System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    CancelRuleChange();
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                   "Ошибка отмены внесенных изменений.\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return;
        }
        /// <summary>
        /// Отменяет сделанные в списке изменения
        /// </summary>
        private void CancelRuleChange()
        {
            try
            {
                if (IsModified() == true)
                {
                    Close();
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Ошибка отмены внесенных изменений.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return ;
        }

        #endregion

        private void SendMessageToLog(System.String strMessage)
        {
            try
            {
                //m_objMenuItem.SimulateNewMessage(strMessage);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "SendMessageToLog.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }

        private void cboxRuleType_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                bRefreshRulePool();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("cboxRuleType_SelectedValueChanged. Текст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }

        private void cboxRuleType_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            try
            {
                if (IsModified() == true)
                {
                    if (DevExpress.XtraEditors.XtraMessageBox.Show(
                        "В списке были произведены изменения.\nОтменить изменения?", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.YesNo,
                        System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    // запускаем процесс сохранения
                    {
                        e.Cancel = true;
                    }
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("cboxRuleType_EditValueChanging. Текст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }

        #region Описание акции
        private void menuEditStepDescription_Click(object sender, EventArgs e)
        {
            try
            {
                if ((treeList.Nodes.Count == 0) || (treeList.FocusedNode == null) || (treeList.FocusedNode.Tag == null)) { return; }
                EditStepDescription(treeList.FocusedNode);
            }
            catch (System.Exception f)
            {
                System.Windows.Forms.MessageBox.Show(this,
                   "Ошибка изменения описания акции.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }

        private void menuClearStepDescription_Click(object sender, EventArgs e)
        {
            try
            {
                if ((treeList.Nodes.Count == 0) || (treeList.FocusedNode == null) || (treeList.FocusedNode.Tag == null)) { return; }
                ClearStepDescription(treeList.FocusedNode);
            }
            catch (System.Exception f)
            {
                System.Windows.Forms.MessageBox.Show(this,
                   "Ошибка удаления описания акции.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }

        private void EditStepDescription(DevExpress.XtraTreeList.Nodes.TreeListNode objNode)
        {
            if ((objNode == null) || (objNode.Tag == null)) { return; }
            try
            {
                //EditStep(treeList.FocusedNode, true);

                ERP_Mercury.Common.CPoolRuleCalculationStep objStep = (ERP_Mercury.Common.CPoolRuleCalculationStep)objNode.Tag;
                if (objStep == null) { return; }
                frmRulePoolStep objfrmRulePoolStep = new frmRulePoolStep(m_objProfile);
                objfrmRulePoolStep.EditStep(m_objPoolRules, objStep, true);
                if (objfrmRulePoolStep.DialogResult == DialogResult.OK)
                {
                    // описание было изменено
                    ShowRupeStepPropertis(objStep);
                }
                objfrmRulePoolStep.Dispose();
                objfrmRulePoolStep = null;
            }
            catch (System.Exception f)
            {
                System.Windows.Forms.MessageBox.Show(this,
                   "Ошибка редактирования описания акции.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return;
        }

        private void ClearStepDescription(DevExpress.XtraTreeList.Nodes.TreeListNode objNode)
        {
            if ((objNode == null) || (objNode.Tag == null)) { return; }
            try
            {
                ERP_Mercury.Common.CPoolRuleCalculationStep objStep = (ERP_Mercury.Common.CPoolRuleCalculationStep)objNode.Tag;
                if (objStep != null)
                {
                    System.String strErr = "";
                    if (ERP_Mercury.Common.CPoolRuleCalculationStep.MakeDescriptionToStepInRulePool(m_objProfile,
                        objStep.ID, true, objStep.RulePool_Description, objStep.RulePool_IsCanShow, ref strErr) == true)
                    {
                        objStep.RulePool_Description = "";
                        objStep.RulePool_IsCanShow = false;

                        ShowRupeStepPropertis(objStep);
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show(this,
                           "Ошибка удаления описания акции.\n\nТекст ошибки: " + strErr, "Ошибка",
                           System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }
                }

            }
            catch (System.Exception f)
            {
                System.Windows.Forms.MessageBox.Show(this,
                   "Ошибка удаления описания акции.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return;
        }

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if ((treeList.Nodes.Count == 0) || (treeList.FocusedNode == null) || (treeList.FocusedNode.Tag == null))
            {
                menuEditStepDescription.Enabled = false;
                menuClearStepDescription.Enabled = false;
            }
            else
            {
                menuEditStepDescription.Enabled = m_objProfile.GetClientsRight().GetState(Consts.strRightEditRulePoolDecription);
                menuClearStepDescription.Enabled = menuEditStepDescription.Enabled;
            }

        }

#endregion

    }


    public class ViewRulePool : PlugIn.IClassTypeView
    {
        public override void Run(UniXP.Common.MENUITEM objMenuItem, System.String strCaption)
        {
            frmRulePool obj = new frmRulePool(objMenuItem.objProfile, objMenuItem);
            obj.Text = strCaption;
            obj.MdiParent = objMenuItem.objProfile.m_objMDIManager.MdiParent;
            obj.Visible = true;
        }
    }

}

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
    public partial class frmRuleCalculation : DevExpress.XtraEditors.XtraForm
    {
        #region Свойства
        private UniXP.Common.CProfile m_objProfile;
        #endregion

        #region Конструктор
        public frmRuleCalculation(UniXP.Common.CProfile objProfile)
        {
            InitializeComponent();

            m_objProfile = objProfile;
            m_bIsModified = false;
            m_bCancelEvents = false;
        }
        #endregion

        #region Список объектов
        /// <summary>
        /// Обновляет список хранимых процедур
        /// </summary>
        private void RefreshStoredProceduresList()
        {
            try
            {
                cboxStoredProcedure.Properties.Items.Clear();

                List<ERP_Mercury.Common.CStoredProcedure> objList = ERP_Mercury.Common.CStoredProcedure.GetStoredProcedureList(m_objProfile, null);
                if ((objList == null) || (objList.Count == 0)) { return; }

                cboxStoredProcedure.Properties.Items.AddRange(objList);

                objList = null;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка обновления списка хранимых процедур.\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        /// <summary>
        /// Обновляет список типов правил
        /// </summary>
        private void RefreshRuleType()
        {
            try
            {
                cboxRuleType.Properties.Items.Clear();

                List<ERP_Mercury.Common.CRuleType> objList = ERP_Mercury.Common.CRuleType.GetRuleTypeList(m_objProfile, null);
                if ((objList == null) || (objList.Count == 0)) { return; }

                cboxRuleType.Properties.Items.AddRange(objList);
                objList = null;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка обновления списка типов правил.\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        /// <summary>
        /// Обновляет список объектов
        /// </summary>
        /// <returns>true - успешное завершение; false - ошибка</returns>
        System.Boolean bLoadRulesList( System.Boolean bAllRefresh )
        {
            System.Boolean bRet = false;
            try
            {
                System.Int32 iNodeIndx = (treeList.FocusedNode == null) ? 0 : treeList.GetNodeIndex(treeList.FocusedNode);
                treeList.FocusedNodeChanged -= new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeListEvent_FocusedNodeChanged);
                treeList.Nodes.Clear();
                if (bAllRefresh == true)
                {
                    // список хранимых процедур
                    RefreshStoredProceduresList();
                    // список типов правил
                    RefreshRuleType();
                }

                //список правил
                List<ERP_Mercury.Common.CRuleCalculation> objList = ERP_Mercury.Common.CRuleCalculation.GetRuleCalculationList(m_objProfile, null);
                if (objList != null)
                {
                    foreach (ERP_Mercury.Common.CRuleCalculation objItem in objList)
                    {
                        treeList.AppendNode(new object[] { objItem.Name, objItem.Description }, null).Tag = objItem;
                    }
                    objList = null;
                }

                EnableControls((treeList.Nodes.Count > 0));
                treeList.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeListEvent_FocusedNodeChanged);
                if (treeList.Nodes.Count > 0)
                {
                    if (iNodeIndx > (treeList.Nodes.Count - 1))
                    {
                        iNodeIndx = treeList.Nodes.Count - 1;
                    }
                    treeList.FocusedNode = treeList.Nodes[iNodeIndx];
                    bLoadProperties((ERP_Mercury.Common.CRuleCalculation)treeList.FocusedNode.Tag);
                }
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
        /// Загружает свойства объекта в элементы управления
        /// </summary>
        /// <param name="objRule">объект</param>
        /// <returns>true - успешное завершение; false - ошибка</returns>
        private System.Boolean bLoadProperties(ERP_Mercury.Common.CRuleCalculation objRule)
        {
            System.Boolean bRet = false;
            try
            {
                if (objRule == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Метод bLoadProperties : Объект не определен", "Ошибка",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return bRet;
                }
                if (objRule.ID.CompareTo(System.Guid.Empty) == 0) { return true; }

                txtName.Text = objRule.Name;
                txtDscrpn.Text = objRule.Description;
                if (objRule.StoredProcedure != null)
                {
                    cboxStoredProcedure.SelectedItem = cboxStoredProcedure.Properties.Items.Cast<ERP_Mercury.Common.CStoredProcedure>().SingleOrDefault<ERP_Mercury.Common.CStoredProcedure>(x => x.ID.Equals(objRule.StoredProcedure.ID) == true);

                    //for (System.Int32 i = 0; i < cboxStoredProcedure.Properties.Items.Count; i++)
                    //{
                    //    if (objRule.StoredProcedure.ID.CompareTo(((ERP_Mercury.Common.CStoredProcedure)cboxStoredProcedure.Properties.Items[i]).ID) == 0)
                    //    {
                    //        cboxStoredProcedure.SelectedIndex = i;
                    //        break;
                    //    }
                    //}
                }
                if (objRule.RuleType != null)
                {
                    cboxRuleType.SelectedItem = cboxRuleType.Properties.Items.Cast<ERP_Mercury.Common.CRuleType>().SingleOrDefault<ERP_Mercury.Common.CRuleType>(x => x.ID.Equals(objRule.RuleType.ID) == true);

                    //for (System.Int32 i = 0; i < cboxRuleType.Properties.Items.Count; i++)
                    //{
                    //    if (objRule.RuleType.ID.CompareTo(((ERP_Mercury.Common.CRuleType)cboxRuleType.Properties.Items[i]).ID) == 0)
                    //    {
                    //        cboxRuleType.SelectedIndex = i;
                    //        break;
                    //    }
                    //}
                }

                treeListParams.Nodes.Clear();
                if (objRule.AdvancedParamList != null)
                {
                    foreach (ERP_Mercury.Common.CAdvancedParam objParam in objRule.AdvancedParamList)
                    {
                        treeListParams.AppendNode(new object[] { objParam.Name, objParam.DataType.Name }, null).Tag = objParam;
                    }
                }
                bRet = true;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка озагрузки свойств объекта: " + objRule.Name + "\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return bRet;
        }

        private void treeListEvent_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            try
            {
                m_bCancelEvents = true;
                txtName.Text = "";
                txtDscrpn.Text = "";

                cboxStoredProcedure.SelectedText = "";

                if ((treeList.Nodes.Count > 0) && (treeList.FocusedNode != null) && (treeList.FocusedNode.Tag != null))
                {
                    if (bLoadProperties((ERP_Mercury.Common.CRuleCalculation)treeList.FocusedNode.Tag) == false)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка смены узла", "Ошибка",
                           System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }
                }

            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка смены узла.\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                m_bCancelEvents = false;
                SetModified(false);
            }

            return;
        }
        private void treeList_BeforeFocusNode(object sender, DevExpress.XtraTreeList.BeforeFocusNodeEventArgs e)
        {
            try
            {
                if (IsModified() == true)
                {
                    // в описании правила произошли изменения
                    DialogResult dlgRslt = DevExpress.XtraEditors.XtraMessageBox.Show("В описании правила произошли изменения.\nПожалуйста, подтвердите или отмените их.", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.YesNoCancel,
                        System.Windows.Forms.MessageBoxIcon.Information);
                    switch (dlgRslt)
                    {
                        case DialogResult.Yes:
                            {
                                e.CanFocus = bSaveRuleChange();
                                break;
                            }
                        case DialogResult.No:
                            {
                                e.CanFocus = bCancelRuleChange();
                                break;
                            }
                        case DialogResult.Cancel:
                            {
                                e.CanFocus = false;
                                break;
                            }
                        default:
                            break;
                    }

                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка смены узла.\nBeforeFocusNode\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }

            return;
        }

        /// <summary>
        /// Обработчик изменения значения в элементах управления, которые связаны со свойствами объекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if ((m_bCancelEvents == false) && (treeList.Nodes.Count > 0) &&
                    (treeList.FocusedNode != null))
                {
                    SetModified(true);
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка функции AccTrnEventEditValueChanged.\nОбъект: " + ((Control)sender).Name + "\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        /// <summary>
        /// Обновляет список 
        /// </summary>
        private void RefreshRulesList()
        {
            try
            {
                // обновляем информацию
                bLoadRulesList( true );
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
                RefreshRulesList();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка обновления списка.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK,
                   System.Windows.Forms.MessageBoxIcon.Error);
            }
            return;
        }

        #endregion

        #region Открытие формы
        private void frmRuleCalculation_Load(object sender, EventArgs e)
        {
            try
            {
                if ( bLoadRulesList( true ) == false)
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
        private void frmRuleCalculation_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (IsModified() == true)
                {
                    if (DevExpress.XtraEditors.XtraMessageBox.Show(
                        "Свойства правила были изменены.\nВыйти из формы без сохранения изменений?", "Внимание",
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
                DevExpress.XtraEditors.XtraMessageBox.Show("frmRuleCalculation_FormClosing\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        #endregion

        #region Индикация изменений в наборе данных

        private System.Boolean m_bIsModified;
        private System.Boolean m_bCancelEvents;
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
                treeList.Enabled = !bModified;
                barBtnRefresh.Enabled = !bModified;
                barBtnAdd.Enabled = !bModified;
                barBtnDelete.Enabled = !bModified;
                barBtnPrint.Enabled = !bModified;
                if (bModified) { EnableControls(true); }
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
                DevExpress.XtraEditors.XtraMessageBox.Show( e.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
            return bRes;
        }
        /// <summary>
        /// Включает отключает элементы управления
        /// </summary>
        /// <param name="bEnable">включить/выключить</param>
        private void EnableControls(System.Boolean bEnable)
        {
            try
            {
                txtName.Enabled = bEnable;
                cboxStoredProcedure.Enabled = bEnable;
                cboxRuleType.Enabled = bEnable;
                txtDscrpn.Enabled = bEnable;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка отключения/включения элементов управления.\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        #endregion

        #region Новый объект
        private void barBtnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                NewRule();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Ошибка создания нового объекта.\n\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
            return;
        }

        /// <summary>
        /// Создание нового объекта
        /// </summary>
        private void NewRule()
        {
            try
            {
                DevExpress.XtraTreeList.Nodes.TreeListNode objNewNode = treeList.AppendNode(null, null);
                objNewNode.Tag = new ERP_Mercury.Common.CRuleCalculation();
                treeList.FocusedNode = objNewNode;

                SetModified(true);
                

            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Ошибка создания нового объекта.\n\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
            return;
        }

        #endregion

        #region Удаление объекта
        /// <summary>
        /// Удаляет правило
        /// </summary>
        /// <param name="objNode">узел с информацией о правиле</param>
        private System.Boolean bDeleteRule( DevExpress.XtraTreeList.Nodes.TreeListNode objNode )
        {
            System.Boolean bRet = false;
            try
            {
                if (objNode == null) { return bRet; }
                if (objNode.Tag == null)
                {
                    // это была новая запись
                    treeList.Nodes.Remove(objNode);
                    return true;
                }

                ERP_Mercury.Common.CRuleCalculation objRule = (ERP_Mercury.Common.CRuleCalculation)objNode.Tag;
                System.Int32 iNodeIndx = treeList.GetNodeIndex(objNode);
                if ((objRule == null) || (objRule.ID.CompareTo(System.Guid.Empty) == 0)) { return bRet; }

                if (objRule.DeleteRuleCalculationFromDB(m_objProfile) == true)
                {
                    treeList.Nodes.Remove(objNode);
                    if (treeList.Nodes.Count > 0)
                    {
                        iNodeIndx--;
                        if (iNodeIndx < 0) { iNodeIndx = 0; }
                        treeList.FocusedNode = treeList.Nodes[iNodeIndx];
                    }
                    SetModified(false);
                    bRet = true;
                }
            }
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Ошибка метода DeleteRule.\n\nТекст ошибки: " + e.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
            return bRet;
        }

        private void barBtnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (treeList.Nodes.Count == 0) { return; }
                if (treeList.FocusedNode == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Необходимо выбрать запись для удаления.", "Внимание",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return;
                }

                if ((DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Вы действительно хотите удалить  " + 
                    (System.String)treeList.FocusedNode.GetValue( colRuleName ) + " ?", "Подтверждение",
                    System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == DialogResult.Yes))
                {
                    Cursor = Cursors.WaitCursor;
                    bDeleteRule(treeList.FocusedNode);
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Ошибка удаления записи.\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
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
                    bSaveRuleChange();
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
        private System.Boolean bSaveRuleChange()
        {
            System.Boolean bRet = false;
            if( (treeList.FocusedNode == null) || (treeList.FocusedNode.Tag == null))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Необходимо выбрать правило.", "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return bRet;
            }
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            try
            {
                // сперва проверим, все ли обязательные значения внесены
                if (txtName.Text == "")
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Укажите, пожалуйста, имя правила.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return bRet;
                }
                if (cboxStoredProcedure.SelectedItem == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Укажите, пожалуйста, хранимую процедуру, связанную с правилом.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return bRet;
                }
                if ( cboxRuleType.SelectedItem == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Укажите, пожалуйста, тип правила.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return bRet;
                }

                // мы... (ну что я буду скромничать) Я создам объект "Правило",
                // скопирую в него данные с формы и сохраню
                // если все пройдет хорошо, то в объект "Правило", связанный с узлом
                // скопируются данные из созданного объекта "Правило"
                // все это для того, чтобы не не запрашивать снова весь список правил
                ERP_Mercury.Common.CRuleCalculation objRule = (ERP_Mercury.Common.CRuleCalculation)treeList.FocusedNode.Tag;
                if (objRule == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Не определен объект \"Правило\".", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return bRet;
                }
                ERP_Mercury.Common.CRuleCalculation objRuleForSave = new ERP_Mercury.Common.CRuleCalculation(objRule.ID, txtName.Text, txtDscrpn.Text, 
                    (ERP_Mercury.Common.CStoredProcedure)cboxStoredProcedure.SelectedItem,
                    (ERP_Mercury.Common.CRuleType)cboxRuleType.SelectedItem);
                if (objRuleForSave == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Не определен объект \"Правило для сохранения\".", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return bRet;
                }
                // сохранение изменений в базе данных
                if (objRuleForSave.ID.CompareTo(System.Guid.Empty) == 0)
                {
                    // новое правило
                    bRet = objRuleForSave.AddRuleCalculationToDB(m_objProfile);
                    if (bRet == true)
                    {
                        treeList.FocusedNode.SetValue(colRuleName, objRuleForSave.Name);
                        treeList.FocusedNode.SetValue(colRuleDscrpn, objRuleForSave.Description);
                    }
                }
                else
                {
                    bRet = objRuleForSave.EditRuleCalculationInDB(m_objProfile, true);
                }
                if (bRet == true) 
                {
                    objRule.ID = objRuleForSave.ID;
                    objRule.Name = objRuleForSave.Name;
                    objRule.Description = objRuleForSave.Description;
                    objRule.StoredProcedure = objRuleForSave.StoredProcedure;
                    objRule.RuleType = objRuleForSave.RuleType;

                    SetModified(false);
                    bRet = true;
                }
                objRuleForSave = null;
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
                    bCancelRuleChange();
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
        /// <returns></returns>
        private System.Boolean bCancelRuleChange()
        {
            System.Boolean bRet = false;
            try
            {
                if ((IsModified() == true) && (treeList.FocusedNode != null) && (treeList.FocusedNode.Tag != null))
                {
                    ERP_Mercury.Common.CRuleCalculation objRule = (ERP_Mercury.Common.CRuleCalculation)treeList.FocusedNode.Tag;
                    if (objRule != null)
                    {
                        if (objRule.ID.CompareTo(System.Guid.Empty) == 0)
                        {
                            // новая запись
                            bRet = bDeleteRule(treeList.FocusedNode);
                        }
                        else
                        {
                            bRet = bLoadProperties(objRule);
                        }
                        // на всякий случай
                        SetModified(false);
                    }
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Ошибка отмены внесенных изменений.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return bRet;
        }

        #endregion

    }

    public class ViewRuleCalculation : PlugIn.IClassTypeView
    {
        public override void Run(UniXP.Common.MENUITEM objMenuItem, System.String strCaption)
        {
            frmRuleCalculation obj = new frmRuleCalculation(objMenuItem.objProfile);
            obj.Text = strCaption;
            obj.MdiParent = objMenuItem.objProfile.m_objMDIManager.MdiParent;
            obj.Visible = true;
        }
    }

}

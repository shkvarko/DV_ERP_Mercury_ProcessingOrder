using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;

namespace ERPMercuryProcessingOrder
{
    public partial class frmPDASupplList : DevExpress.XtraEditors.XtraForm
    {
        #region Переменные, Свойства, Константы
        private UniXP.Common.CProfile m_objProfile;
        //private System.Boolean m_bDisableEvents;
        private UniXP.Common.MENUITEM m_objMenuItem;
        private System.Boolean m_bUserCanEditOrder;
        private System.Boolean m_bUserCanExcludeOrderFromADJ;
        private List<CSupplState> m_objSupplStateList;
        private const System.String strDRUserCanEditOrder = "Обнуление цен и запуск расчета цен в заказе";
        private const System.String strDRUserCanExcludeOrderFromADJ = "Исключение заказа из акта выполненных работ";
        private const System.String strIncludeOrderInADJ = "Включить заказ в акт выполненных работ";
        private const System.String strExcludeOrderInADJ = "Исключить заказ из акта выполненных работ";

        #endregion

        #region Конструктор
        public frmPDASupplList(UniXP.Common.CProfile objProfile, UniXP.Common.MENUITEM objMenuItem)
        {
            InitializeComponent();

            m_objProfile = objProfile;
            m_bUserCanEditOrder = m_objProfile.GetClientsRight().GetState(strDRUserCanEditOrder);
            m_bUserCanExcludeOrderFromADJ = m_objProfile.GetClientsRight().GetState(strDRUserCanExcludeOrderFromADJ);
            menuMakeSupplDeleted.Visible = m_bUserCanEditOrder;

            //m_bDisableEvents = false;
            m_objMenuItem = objMenuItem;
            BeginDate.DateTime = System.DateTime.Today;
            EndDate.DateTime = System.DateTime.Today;
            m_objSupplStateList = CSupplState.GetSupplStateList(m_objProfile, null);

        }
        #endregion

        #region Список заказов
        private void treeList_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            try
            {
                if ((e.Node == treeList.FocusedNode && e.Column != treeList.FocusedColumn) || e.Node == null || e.Column == null || e.Node.Tag == null) return;
                if (e.Node.GetValue(colSupplSateId) == null) { return; }
                enPDASupplState SupplState = (enPDASupplState)e.Node.GetValue(colSupplSateId);
                
                System.Boolean bWarning = false;
                switch (SupplState)
                {
                    case enPDASupplState.CalcPricesFalse:
                        {
                            bWarning = true;
                            break;
                        }
                    case enPDASupplState.CreateSupplInIBFalse:
                        {
                            bWarning = true;
                            break;
                        }
                    case enPDASupplState.OutPartsOfStock:
                        {
                            bWarning = true;
                            break;
                        }
                    case enPDASupplState.Created:
                        {
                            bWarning = true;
                            break;
                        }
                    default:
                        {
                            break;
                        }

                }
                if (bWarning == true)
                {
                    //e.Appearance.Font = new Font(DevExpress.Utils.AppearanceObject.DefaultFont, FontStyle.Strikeout);
                    e.Appearance.ForeColor = System.Drawing.Color.Red;
                }

            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Ошибка treeList_CustomDrawNodeCell.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }
            return;
        }

        private void treeList_CustomDrawNodeImages(object sender, DevExpress.XtraTreeList.CustomDrawNodeImagesEventArgs e)
        {
            try
            {
                System.Int32 iImgIndx = -1;
                int Y = e.SelectRect.Top + (e.SelectRect.Height - imageCollection.Images[0].Height) / 2;
                if ((e.Node != null) && (e.Node.GetValue(colSupplSateId) != null))
                {
                    enPDASupplState SupplState = (enPDASupplState)e.Node.GetValue(colSupplSateId);

                    switch (SupplState)
                    {
                        case enPDASupplState.CalcPricesOk:
                            {
                                iImgIndx = 0;

                                break;
                            }
                        case enPDASupplState.AutoCalcPricesOk:
                            {
                                iImgIndx = 0;
                                break;
                            }
                        case enPDASupplState.Print:
                            {
                                iImgIndx = 3;
                                break;
                            }
                        case enPDASupplState.Confirm:
                            {
                                iImgIndx = 4;
                                break;
                            }
                        case enPDASupplState.TTN:
                            {
                                iImgIndx = 5;
                                break;
                            }
                        case enPDASupplState.Shipped:
                            {
                                iImgIndx = 6;
                                break;
                            }
                        case enPDASupplState.CalcPricesFalse:
                            {
                                iImgIndx = 1;
                                break;
                            }
                        case enPDASupplState.CreateSupplInIBFalse:
                            {
                                iImgIndx = 1;
                                break;
                            }
                        case enPDASupplState.OutPartsOfStock:
                            {
                                iImgIndx = 1;
                                break;
                            }
                        case enPDASupplState.MakedForRecalcPrices:
                            {
                                iImgIndx = 7;
                                break;
                            }
                        case enPDASupplState.Deleted:
                            {
                                iImgIndx = 8;
                                break;
                            }
                        case enPDASupplState.CreditControl:
                            {
                                iImgIndx = 1;
                                break;
                            }
                        case enPDASupplState.FindStock:
                            {
                                iImgIndx = 2;
                                break;
                            }
                        case enPDASupplState.Created:
                            {
                                iImgIndx = 2;
                                break;
                            }
                        default:
                            {
                                iImgIndx = -1;
                                break;
                            }

                    }

                }

                try
                {
                    e.Graphics.DrawImage(imageCollection.Images[iImgIndx], new Point(e.SelectRect.X, Y));
                    e.Handled = true;
                }
                catch { }

            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Ошибка отображения списка заказов.\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Обновление списка заказов
        /// </summary>
        private void RefreshSupplList()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                this.tableLayoutPanel1.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.treeList)).BeginInit();

                treeList.Nodes.Clear();

                List<CPDASuppl> objSupplList = CPDASuppl.GetSupplList(m_objProfile, System.Guid.Empty, BeginDate.DateTime, EndDate.DateTime);
                if ((objSupplList != null) && (objSupplList.Count > 0))
                {
                    foreach (CPDASuppl objSuppl in objSupplList)
                    {

                        treeList.AppendNode(new object[] { objSuppl.BeginDate, objSuppl.DeliveryDate, objSuppl.MoneyBonus, objSuppl.Num, objSuppl.SubNum, 
                            objSuppl.StateName, objSuppl.CustomerName.Trim(), objSuppl.ChildCustomerCode, 
                            objSuppl.DepartCode, objSuppl.StockName, objSuppl.CompanyName, objSuppl.GroupList, objSuppl.Weight, objSuppl.SupplState.ID, objSuppl.IsExcludeFromADJ }, null).Tag = objSuppl;
                    }
                }
                objSupplList = null;

                if (treeList.Nodes.Count > 0)
                {
                    treeList.FocusedNode = treeList.Nodes[0];
                    ShowSupplProperties((CPDASuppl)treeList.FocusedNode.Tag);
                    barBtnProcess.Enabled = true;
                    barBtnClearPrices.Enabled = true;
                }
                else
                {
                    barBtnProcess.Enabled = false;
                    barBtnClearPrices.Enabled = false;
                }

            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка обновления списка.\n\nТекст ошибки:\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                ((System.ComponentModel.ISupportInitialize)(this.treeList)).EndInit();
                this.tableLayoutPanel1.ResumeLayout(false);

                this.Cursor = Cursors.Default;
            }

            return;
        }
        private void frmPDASupplList_Load(object sender, EventArgs e)
        {
            try
            {
                tabControl.SelectedTabPage = tabSupplList;
                RefreshSupplList();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка загрузки формы.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return;
        }
        private void barBtnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                RefreshSupplList();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка обновления списка заказов.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return;
        }
        private void BeginDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((e.KeyChar == (char)Keys.Enter) && (barBtnRefresh.Visibility == DevExpress.XtraBars.BarItemVisibility.Always))
                {
                    RefreshSupplList();
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("BeginDate_KeyPress.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }
            return;

        }

        private void menuRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                RefreshSupplList();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка обновления списка заказов.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return;
        }
        /// <summary>
        /// Отображает свойства заказа в элементах управления
        /// </summary>
        /// <param name="objSuppl">заказ</param>
        private void ShowSupplProperties(CPDASuppl objSuppl)
        {
            try
            {
                txtSupplQty.Text = "";
                txtSupplOrderQty.Text = "";
                txtSupplPositionCount.Text = "";
                txtSupplNum.Text = "";
                txtSupplAllPrice.Text = "";
                txtSupplAllDiscountPrice.Text = "";
                txtSupplAllDiscountCurrencyPrice.Text = "";
                txtSupplAllDiscount.Text = "";
                txtSupplAllCurrencyPrice.Text = "";
                txtSupplAllCurrencyDiscount.Text = "";
                txtSupplState.Text = "";

                if (objSuppl == null) { return; }

                txtSupplNum.Text = objSuppl.Num.ToString();
                txtSupplQty.Text = System.String.Format("{0:### ### ##0}", objSuppl.Qty);
                txtSupplOrderQty.Text = System.String.Format("{0:### ### ##0}", objSuppl.OrderQty);
                txtSupplPositionCount.Text = System.String.Format("{0:### ### ##0}", objSuppl.PositionQty);
                txtSupplAllPrice.Text = System.String.Format("{0:### ### ##0.00}", objSuppl.AllPrice);
                txtSupplAllDiscountPrice.Text = System.String.Format("{0:### ### ##0.00}", objSuppl.AllDiscountPrice);
                txtSupplAllDiscountCurrencyPrice.Text = System.String.Format("{0:### ### ##0.00}", objSuppl.AllCurrencyDiscountPrice);
                txtSupplAllDiscount.Text = System.String.Format("{0:### ### ##0.00}", objSuppl.AllDiscount);
                txtSupplAllCurrencyPrice.Text = System.String.Format("{0:### ### ##0.00}", objSuppl.AllCurrencyPrice);
                txtSupplAllCurrencyDiscount.Text = System.String.Format("{0:### ### ##0.00}", objSuppl.AllCurrencyDiscount);
                txtSupplState.Text = objSuppl.StateName;

            }//try
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Ошибка отображения свойств заказа.\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }

            return;
        }
        private void treeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            try
            {
                if ((treeList.FocusedNode != null) && (treeList.FocusedNode.Tag != null))
                {
                    ShowSupplProperties((CPDASuppl)treeList.FocusedNode.Tag);
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "treeList_FocusedNodeChanged.\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }

            return;
        }
        /// <summary>
        /// Возвращает объект "состояние заказа" по идентификатору
        /// </summary>
        /// <param name="iSupplId">идентификатор состояния заказа</param>
        /// <returns>объект "состояние заказа"</returns>
        private CSupplState GetSupplStateByID(System.Int32 iSupplId)
        {
            CSupplState objRet = null;
            try
            {
                if (m_objSupplStateList != null)
                {
                    foreach (CSupplState objItem in m_objSupplStateList)
                    {
                        if (objItem.ID == iSupplId)
                        {
                            objRet = objItem;
                            break;
                        }
                    }
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "GetSupplStateByID.\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }

            return objRet;
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
                    "SendMessageToLog.\n\nТекст ошибки: " + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        #endregion

        #region Расчет цен
        /// <summary>
        /// Запускает расчет цен для заказа
        /// </summary>
        /// <param name="objNode">узел дерева</param>
        private void ProcessSuppl(DevExpress.XtraTreeList.Nodes.TreeListNode objNode)
        {
            try
            {
                if ((objNode == null) || (objNode.Tag == null)) { return; }
                CPDASuppl objSuppl = (CPDASuppl)objNode.Tag;
                if (objSuppl == null) { return; }

                this.Cursor = Cursors.WaitCursor;
                System.String strErr = "";

                System.Int32 iRet = CPDASuppl.ProcessSuppl(m_objProfile, objSuppl.Id, ref strErr);
                SendMessageToLog(strErr);

                if (iRet == 0)
                {
                    objNode.ImageIndex = 7;
                    objNode.SelectImageIndex = 7;
                }

                ShowSupplProperties((CPDASuppl)objNode.Tag);
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Расчет цен. Текст ошибки: " + f.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            return;
        }
        private void barBtnProcess_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if ((treeList.Nodes.Count == 0) || (treeList.FocusedNode == null)) { return; }
                ProcessSuppl(treeList.FocusedNode);
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Расчет цен. Текст ошибки: " + f.Message);
            }
            finally
            {
            }

            return;
        }
        private void menuCalcPrice_Click(object sender, EventArgs e)
        {
            try
            {
                if ((treeList.Nodes.Count == 0) || (treeList.FocusedNode == null)) { return; }
                ProcessSuppl(treeList.FocusedNode);
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Расчет цен. Текст ошибки: " + f.Message);
            }
            finally
            {
            }

            return;
        }

        private void treeList_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    // попробуем определить, что же у нас под мышкой
                    DevExpress.XtraTreeList.TreeListHitInfo hi = ((DevExpress.XtraTreeList.TreeList)sender).CalcHitInfo(new Point(e.X, e.Y));
                    if ((hi == null) || (hi.Node == null))
                    {
                        menuCalcPrice.Enabled = false;
                        menuClearPrices.Enabled = false;
                        menuEventLog.Enabled = false;
                        menuMakeSupplDeleted.Enabled = false;
                    }
                    else
                    {
                        // выделяем узел
                        hi.Node.TreeList.FocusedNode = hi.Node;
                        menuCalcPrice.Enabled = barBtnProcess.Enabled;
                        menuClearPrices.Enabled = barBtnClearPrices.Enabled;
                        menuEventLog.Enabled = true;
                        if ((hi.Node.Tag != null) && (hi.Node.GetValue(colSupplSateId) != null))
                        {
                            enPDASupplState SupplState = (enPDASupplState)hi.Node.GetValue(colSupplSateId);
                            menuMakeSupplDeleted.Enabled = !((SupplState == enPDASupplState.Print) || (SupplState == enPDASupplState.TTN) ||
                                (SupplState == enPDASupplState.Shipped) || (SupplState == enPDASupplState.CalcPricesOk) ||
                                (SupplState == enPDASupplState.AutoCalcPricesOk) || (SupplState == enPDASupplState.Confirm));
                        }
                        else
                        {
                            menuMakeSupplDeleted.Enabled = false;
                        }

                    }
                    contextMenuStrip.Show(((DevExpress.XtraTreeList.TreeList)sender), new Point(e.X, e.Y));
                }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("treeListEMail_MouseClick.\nТекст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            try
            {
                menuCalcPrice.Enabled = m_bUserCanEditOrder;
                menuClearPrices.Enabled = m_bUserCanEditOrder;
                menuReturnSupplStateToAutoProcessPrices.Enabled = m_bUserCanEditOrder;
                menuMakeSupplExcludeFromADJ.Enabled = m_bUserCanExcludeOrderFromADJ;

                if ((treeList.FocusedNode != null) && (treeList.FocusedNode.Tag != null) && (treeList.FocusedNode.GetValue(colSupplSateId) != null))
                {
                    enPDASupplState SupplState = (enPDASupplState)treeList.FocusedNode.GetValue(colSupplSateId);
                    menuMakeSupplDeleted.Enabled = !((SupplState == enPDASupplState.Print) || (SupplState == enPDASupplState.TTN) ||
                        (SupplState == enPDASupplState.Shipped) || //(SupplState == enPDASupplState.CalcPricesOk) ||
                        (SupplState == enPDASupplState.AutoCalcPricesOk) || (SupplState == enPDASupplState.Confirm));
                    System.Boolean bExcludeState = System.Convert.ToBoolean( treeList.FocusedNode.GetValue( colExcludeFromADJ ) );
                    menuMakeSupplExcludeFromADJ.Text = ((bExcludeState == true) ? strIncludeOrderInADJ : strExcludeOrderInADJ);
                }
                else
                {
                    menuMakeSupplDeleted.Enabled = false;
                }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("contextMenuStrip_Opening. Текст ошибки: " + f.Message);
            }
            return;
        }
        #endregion

        #region Обнуление цен
        private void ClearPricesInSuppl(DevExpress.XtraTreeList.Nodes.TreeListNode objNode)
        {
            try
            {
                if ((objNode == null) || (objNode.Tag == null)) { return; }
                CPDASuppl objSuppl = (CPDASuppl)objNode.Tag;
                if (objSuppl == null) { return; }

                this.Cursor = Cursors.WaitCursor;
                System.String strErr = "";

                System.Int32 iRet = CPDASuppl.ClearPricesInSuppl(m_objProfile, objSuppl.Id, ref strErr);
                SendMessageToLog(strErr);

                if (iRet == 0)
                {
                    CSupplState objNeedSupplState = GetSupplStateByID((System.Int32)enPDASupplState.Created);
                    if (objNeedSupplState != null)
                    {
                        objSuppl.SupplState.ID = objNeedSupplState.ID;
                        objSuppl.SupplState.Name = objNeedSupplState.Name;
                        objNode.SetValue(colSupplState, objNeedSupplState.Name);
                    }

                    objNode.ImageIndex = 2;
                    objNode.SelectImageIndex = 2;
                }

                ShowSupplProperties((CPDASuppl)objNode.Tag);
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Расчет цен. Текст ошибки: " + f.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            return;
        }
        private void barBtnClearPrices_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if ((treeList.Nodes.Count == 0) || (treeList.FocusedNode == null)) { return; }
                ClearPricesInSuppl(treeList.FocusedNode);
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Расчет цен. Текст ошибки: " + f.Message);
            }
            finally
            {
            }

            return;
        }
        private void menuClearPrices_Click(object sender, EventArgs e)
        {
            try
            {
                if ((treeList.Nodes.Count == 0) || (treeList.FocusedNode == null)) { return; }
                ClearPricesInSuppl(treeList.FocusedNode);
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Расчет цен. Текст ошибки: " + f.Message);
            }
            finally
            {
            }

            return;
        }
        #endregion

        #region Вернуть состояние заказа для повторной автоматической обработки
        private void ReturnSupplStateToAutoCalcPrices(DevExpress.XtraTreeList.Nodes.TreeListNode objNode)
        {
            try
            {
                if ((objNode == null) || (objNode.Tag == null)) { return; }
                CPDASuppl objSuppl = (CPDASuppl)objNode.Tag;
                if (objSuppl == null) { return; }

                if (DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Состояние заказа будет изменено для повторного автоматического расчета цен.\nПротокол в программе\"Контракт\" не будет удален. Его необходимо будет удалить вручную.\nПодтвердите начало операции.", "Внимание!",
                    System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information) == DialogResult.No)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                System.String strErr = "";

                System.Int32 iRet = CPDASuppl.ReturnSupplStateToAutoCalcPrices(m_objProfile, objSuppl.Id, ref strErr);
                SendMessageToLog(strErr);

                if (iRet == 0)
                {
                    CSupplState objNeedSupplState = GetSupplStateByID((System.Int32)enPDASupplState.FindStock);
                    if (objNeedSupplState != null)
                    {
                        objSuppl.SupplState.ID = objNeedSupplState.ID;
                        objSuppl.SupplState.Name = objNeedSupplState.Name;
                        objNode.SetValue(colSupplState, objNeedSupplState.Name);
                    }
                    objNode.ImageIndex = 2;
                    objNode.SelectImageIndex = 2;
                }

                ShowSupplProperties((CPDASuppl)objNode.Tag);
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Расчет цен. Текст ошибки: " + f.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            return;
        }
        private void menuReturnSupplStateToAutoProcessPrices_Click(object sender, EventArgs e)
        {
            try
            {
                if ((treeList.Nodes.Count == 0) || (treeList.FocusedNode == null)) { return; }
                ReturnSupplStateToAutoCalcPrices(treeList.FocusedNode);
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Расчет цен. Текст ошибки: " + f.Message);
            }
            finally
            {
            }

            return;
        }
        #endregion

        #region Пометить заказ как удаленный
        private void MakeSupplStateToDeleted(DevExpress.XtraTreeList.Nodes.TreeListNode objNode)
        {
            try
            {
                if ((objNode == null) || (objNode.Tag == null)) { return; }
                CPDASuppl objSuppl = (CPDASuppl)objNode.Tag;
                if (objSuppl == null) { return; }

                if (DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Состояние заказа будет изменено на \"удален\".\nПодтвердите, пожалуйста, начало операции.", "Внимание!",
                    System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information) == DialogResult.No)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                System.String strErr = "";

                System.Int32 iRet = CPDASuppl.MakeSupplStateToDeleted(m_objProfile, objSuppl.Id, ref strErr);
                SendMessageToLog(strErr);

                if (iRet == 0)
                {
                    CSupplState objNeedSupplState = GetSupplStateByID((System.Int32)enPDASupplState.Deleted);
                    if (objNeedSupplState != null)
                    {
                        objSuppl.SupplState.ID = objNeedSupplState.ID;
                        objSuppl.SupplState.Name = objNeedSupplState.Name;
                        objNode.SetValue(colSupplState, objNeedSupplState.Name);
                    }
                    objNode.ImageIndex = 8;
                    objNode.SelectImageIndex = 8;
                }

                ShowSupplProperties((CPDASuppl)objNode.Tag);
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Расчет цен. Текст ошибки: " + f.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            return;
        }

        private void menuMakeSupplDeleted_Click(object sender, EventArgs e)
        {
            try
            {
                if ((treeList.Nodes.Count == 0) || (treeList.FocusedNode == null)) { return; }
                MakeSupplStateToDeleted(treeList.FocusedNode);
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Расчет цен. Текст ошибки: " + f.Message);
            }
            finally
            {
            }

            return;

        }
        #endregion

        #region Журнал событий
        /// <summary>
        /// Загружает журнал событий
        /// </summary>
        private void ShowEventLog()
        {
            try
            {
                if ((treeList.Nodes.Count == 0) || (treeList.FocusedNode == null) || (treeList.FocusedNode.Tag == null)) { return; }
                CPDASuppl objSuppl = (CPDASuppl)treeList.FocusedNode.Tag;
                if (objSuppl != null)
                {
                    this.Cursor = Cursors.WaitCursor;
                    UniXP.Common.frmEventLog objfrmEventLog = new UniXP.Common.frmEventLog(m_objProfile, objSuppl.Id);
                    if (objfrmEventLog != null)
                    {
                        objfrmEventLog.ShowDialog();
                    }
                    objfrmEventLog.Dispose();
                    objfrmEventLog = null;
                }

            }
            catch (System.Exception f)
            {
                SendMessageToLog("ShowEventLog.\nТекст ошибки: " + f.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
            return;
        }

        private void menuEventLog_Click(object sender, EventArgs e)
        {
            try
            {
                ShowEventLog();
            }
            catch (System.Exception f)
            {
                SendMessageToLog("menuEventLog_Click.\nТекст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }

        #endregion

        #region Список продукции к заказу
        private void treeList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                DevExpress.XtraTreeList.TreeListHitInfo hi = ((DevExpress.XtraTreeList.TreeList)sender).CalcHitInfo(new Point(e.X, e.Y));
                if ((hi != null) && (hi.Node != null) && (hi.Node.Tag != null))
                {
                    LoadProductList(hi.Node);
                }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("treeListSupplItems_MouseDoubleClick.\nТекст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }
        /// <summary>
        /// Загружает список продукции
        /// </summary>
        /// <param name="objNode">узел дерева</param>
        private void LoadProductList(DevExpress.XtraTreeList.Nodes.TreeListNode objNode)
        {
            try
            {
                if ((objNode == null) || (objNode.Tag == null)) { return; }
                CPDASuppl objSuppl = (CPDASuppl)objNode.Tag;
                if (objSuppl == null) { return; }
                this.Cursor = Cursors.WaitCursor;

                ((System.ComponentModel.ISupportInitialize)(this.treeListSupplItems)).BeginInit();
                this.SuspendLayout();

                treeListSupplItems.ClearNodes();

                if ((objSuppl.LoadProductList(m_objProfile) == true) && (objSuppl.SupplItmsList != null) && (objSuppl.SupplItmsList.Count > 0))
                {
                    foreach (CPDASupplItms objItem in objSuppl.SupplItmsList)
                    {
                        treeListSupplItems.AppendNode(new object[] { objItem.ProductOwnerName, objItem.IsImporter, 
                            objItem.ChargePercent, objItem.DiscountRetro, objItem.DiscountFix, objItem.DiscountTradeEquip, objItem.NDSPercent, objItem.ProductName, 
                            objItem.QtyOrder, objItem.Qty, objItem.Price, objItem.AllPrice, objItem.CurrencyPrice, 
                            objItem.AllCurrencyPrice, objItem.DiscountPercent, objItem.DiscountPrice, objItem.CurrencyDiscountPrice,
                            objItem.TotalPrice, objItem.TotalCurrencyPrice, objItem.DiscountDescription, objItem.Description}, null).Tag = objItem;
                    }
                }

                ((System.ComponentModel.ISupportInitialize)(this.treeListSupplItems)).EndInit();
                this.ResumeLayout(false);

                barManager.Bars[bar1.BarName].Visible = false;
                tabControl.SelectedTabPage = tabSuppl;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Ошибка отображения списка товаров в заказе.\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            return;
        }
        private void btnToTabSupplList_Click(object sender, EventArgs e)
        {
            barManager.Bars[bar1.BarName].Visible = true;
            tabControl.SelectedTabPage = tabSupplList;
        }
        #endregion

        #region Печать
        /// <summary>
        /// Экспорт бюджета в MS Excel
        /// </summary>
        private void ExportToExcel()
        {
            Excel.Application oXL;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;
            //Excel.Range oRng;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                //Start Excel and get Application object.
                oXL = new Excel.Application();

                //Get a new workbook.
                oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel._Worksheet)oWB.Worksheets[1];

                //Add table headers going cell by cell.
                oSheet.Name = "Содержимое заказа";
                // Имена столбцов
                oSheet.Cells[3, 1] = "Товарная марка";
                oSheet.Cells[3, 2] = "Импортер";
                oSheet.Cells[3, 3] = "Надбавка, %";
                oSheet.Cells[3, 4] = "Ретро-скидка, %";
                oSheet.Cells[3, 5] = "Фикс. скидка, %";
                oSheet.Cells[3, 6] = "Оборуд. скидка, %";
                oSheet.Cells[3, 7] = "НДС, %";
                oSheet.Cells[3, 8] = "Наименование товара";
                oSheet.Cells[3, 9] = "Заказано, шт.";
                oSheet.Cells[3, 10] = "Количество, шт.";
                oSheet.Cells[3, 11] = "Цена, руб.";
                oSheet.Cells[3, 12] = "Сумма, руб.";
                oSheet.Cells[3, 13] = "Цена, вал.";
                oSheet.Cells[3, 14] = "Сумма, вал.";
                oSheet.Cells[3, 15] = "Итоговая скидка, %";
                oSheet.Cells[3, 16] = "Цена со скидкой, руб.";
                oSheet.Cells[3, 17] = "Цена со скидкой, вал.";
                oSheet.Cells[3, 18] = "Сумма со скидкой, руб.";
                oSheet.Cells[3, 19] = "Сумма со скидкой, вал.";

                //Format A1:D1 as bold, vertical alignment = center.
                oSheet.get_Range("A3", "Z3").Font.Size = 12;
                oSheet.get_Range("A3", "Z3").Font.Bold = true;
                oSheet.get_Range("A3", "Z3").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                // пробегаем по дереву и сохраняем расшифровки
                System.Int32 iLastIndxRowForPrint = 4;

                foreach (DevExpress.XtraTreeList.Nodes.TreeListNode objNode in treeListSupplItems.Nodes)
                {
                    oSheet.Cells[iLastIndxRowForPrint, 1] = (System.String)objNode.GetValue(colOwnerProductName);
                    oSheet.Cells[iLastIndxRowForPrint, 2] = ((System.Boolean)objNode.GetValue(colIsImporter)).ToString();
                    oSheet.Cells[iLastIndxRowForPrint, 3] = System.Convert.ToDouble(objNode.GetValue(colChargePercent));
                    oSheet.Cells[iLastIndxRowForPrint, 4] = System.Convert.ToDouble(objNode.GetValue(colDiscountRetro));
                    oSheet.Cells[iLastIndxRowForPrint, 5] = System.Convert.ToDouble(objNode.GetValue(colDiscountFix));
                    oSheet.Cells[iLastIndxRowForPrint, 6] = System.Convert.ToDouble(objNode.GetValue(colDiscountTradeEq));
                    oSheet.Cells[iLastIndxRowForPrint, 7] = System.Convert.ToDouble(objNode.GetValue(colNDSPercent));
                    oSheet.Cells[iLastIndxRowForPrint, 8] = (System.String)objNode.GetValue(colProductName);
                    oSheet.Cells[iLastIndxRowForPrint, 9] = System.Convert.ToDouble(objNode.GetValue(colOrderQty));
                    oSheet.Cells[iLastIndxRowForPrint, 10] = System.Convert.ToDouble(objNode.GetValue(colQty));
                    oSheet.Cells[iLastIndxRowForPrint, 11] = System.Convert.ToDouble(objNode.GetValue(colPrice));
                    oSheet.Cells[iLastIndxRowForPrint, 12] = System.Convert.ToDouble(objNode.GetValue(colAllPrice));
                    oSheet.Cells[iLastIndxRowForPrint, 13] = System.Convert.ToDouble(objNode.GetValue(colCurrencyPrice));
                    oSheet.Cells[iLastIndxRowForPrint, 14] = System.Convert.ToDouble(objNode.GetValue(colAllCurrencyPrice));
                    oSheet.Cells[iLastIndxRowForPrint, 15] = System.Convert.ToDouble(objNode.GetValue(colDiscountPercent));
                    oSheet.Cells[iLastIndxRowForPrint, 16] = System.Convert.ToDouble(objNode.GetValue(colDiscountPrice));
                    oSheet.Cells[iLastIndxRowForPrint, 17] = System.Convert.ToDouble(objNode.GetValue(colCurrencyDiscountPrice));
                    oSheet.Cells[iLastIndxRowForPrint, 18] = System.Convert.ToDouble(objNode.GetValue(colTotalPrice));
                    oSheet.Cells[iLastIndxRowForPrint, 19] = System.Convert.ToDouble(objNode.GetValue(colCurrencyTotalPrice));

                    //oSheet.get_Range(oSheet.Cells[iLastIndxRowForPrint, 16], oSheet.Cells[iLastIndxRowForPrint, 19]).Font.Bold = true;
                    iLastIndxRowForPrint++;
                }

                for (System.Int32 i = 9; i <= 19; i++)
                {
                    if ((i == 9) || (i == 10) || (i == 12) || (i == 14) || (i == 18) || (i == 19))
                    {
                        oSheet.get_Range(oSheet.Cells[iLastIndxRowForPrint + 1, i], oSheet.Cells[iLastIndxRowForPrint + 1, i]).Formula = "=СУММ(R[-" + (iLastIndxRowForPrint - 3).ToString() + "]C:R[-1]C)";
                        oSheet.get_Range(oSheet.Cells[iLastIndxRowForPrint + 1, i], oSheet.Cells[iLastIndxRowForPrint + 1, i]).NumberFormat = "# ##0,00";
                        oSheet.get_Range(oSheet.Cells[iLastIndxRowForPrint + 1, i], oSheet.Cells[iLastIndxRowForPrint + 1, i]).Font.Size = 12;
                        oSheet.get_Range(oSheet.Cells[iLastIndxRowForPrint + 1, i], oSheet.Cells[iLastIndxRowForPrint + 1, i]).Font.Bold = true;
                        oSheet.get_Range(oSheet.Cells[iLastIndxRowForPrint + 1, i], oSheet.Cells[iLastIndxRowForPrint + 1, i]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    }

                }
                oSheet.get_Range("A3", "Z3").EntireColumn.AutoFit();

                if ((treeList.FocusedNode != null) && (treeList.FocusedNode.Tag != null))
                {
                    CPDASuppl objPDASuppl = (CPDASuppl)treeList.FocusedNode.Tag;
                    oSheet.Cells[1, 1] = objPDASuppl.CustomerName + " " + objPDASuppl.ChildCustomerCode + " " + objPDASuppl.CompanyName + " " + objPDASuppl.DepartCode;
                    oSheet.get_Range(oSheet.Cells[1, 1], oSheet.Cells[1, 1]).Font.Size = 12;
                    oSheet.get_Range(oSheet.Cells[1, 1], oSheet.Cells[1, 1]).Font.Bold = true;
                }

                oXL.Visible = true;
                oXL.UserControl = true;
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
        private void barBtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeListSupplItems.Nodes.Count > 0)
                {
                    ExportToExcel();
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
        /// <summary>
        /// Печать списка заказов
        /// </summary>
        private void ExportToExcelPDAList()
        {
            Excel.Application oXL;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;
            //Excel.Range oRng;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                //Start Excel and get Application object.
                oXL = new Excel.Application();

                //Get a new workbook.
                oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel._Worksheet)oWB.Worksheets[1];

                //Add table headers going cell by cell.
                oSheet.Name = "Список заказов";
                // Имена столбцов
                oSheet.Cells[3, 1] = "Дата заказа";
                oSheet.Cells[3, 2] = "№ заказа";
                oSheet.Cells[3, 3] = "№ версии";
                oSheet.Cells[3, 4] = "Состояние";
                oSheet.Cells[3, 5] = "Клиент";
                oSheet.Cells[3, 6] = "Дочерний клиент";
                oSheet.Cells[3, 7] = "Код ТП";
                oSheet.Cells[3, 8] = "Склад";
                oSheet.Cells[3, 9] = "Компания";
                oSheet.Cells[3, 10] = "Количество, шт.";
                oSheet.Cells[3, 11] = "Заказано, шт.";
                oSheet.Cells[3, 12] = "Позиций в заказе, шт.";
                oSheet.Cells[3, 13] = "Сумма, руб.";
                oSheet.Cells[3, 14] = "Сумма скидки, руб.";
                oSheet.Cells[3, 15] = "Сумма со скидкой, руб.";
                oSheet.Cells[3, 16] = "Сумма, вал.";
                oSheet.Cells[3, 17] = "Сумма скидки, вал.";
                oSheet.Cells[3, 18] = "Сумма со скидкой, вал.";

                //Format A1:D1 as bold, vertical alignment = center.
                oSheet.get_Range("A3", "Z3").Font.Size = 12;
                oSheet.get_Range("A3", "Z3").Font.Bold = true;
                oSheet.get_Range("A3", "Z3").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                // пробегаем по дереву и сохраняем расшифровки
                System.Int32 iLastIndxRowForPrint = 4;
                oSheet.get_Range(oSheet.Cells[1, 10], oSheet.Cells[treeList.Nodes.Count + 3, 12]).NumberFormat = "# ##0";
                oSheet.get_Range(oSheet.Cells[1, 13], oSheet.Cells[treeList.Nodes.Count + 3, 18]).NumberFormat = "### ### ##0,00";
                CPDASuppl objSuppl = null;
                foreach (DevExpress.XtraTreeList.Nodes.TreeListNode objNode in treeList.Nodes)
                {
                    oSheet.Cells[iLastIndxRowForPrint, 1] = (System.DateTime)objNode.GetValue(colSupplDate);
                    oSheet.Cells[iLastIndxRowForPrint, 2] = System.Convert.ToString(objNode.GetValue(colSupplNum));
                    oSheet.Cells[iLastIndxRowForPrint, 3] = System.Convert.ToString(objNode.GetValue(colSupplVersion));
                    oSheet.Cells[iLastIndxRowForPrint, 4] = System.Convert.ToString(objNode.GetValue(colSupplState));
                    oSheet.Cells[iLastIndxRowForPrint, 5] = System.Convert.ToString(objNode.GetValue(colCustomer));
                    oSheet.Cells[iLastIndxRowForPrint, 6] = System.Convert.ToString(objNode.GetValue(colChildCode));
                    oSheet.Cells[iLastIndxRowForPrint, 7] = System.Convert.ToString(objNode.GetValue(colDepartCode));
                    oSheet.Cells[iLastIndxRowForPrint, 8] = System.Convert.ToString(objNode.GetValue(colStockName));
                    oSheet.Cells[iLastIndxRowForPrint, 9] = System.Convert.ToString(objNode.GetValue(colCompanyName));

                    if (objNode.Tag != null)
                    {
                        objSuppl = (CPDASuppl)objNode.Tag;
                        oSheet.Cells[iLastIndxRowForPrint, 10] = objSuppl.Qty;
                        oSheet.Cells[iLastIndxRowForPrint, 11] = objSuppl.OrderQty;
                        oSheet.Cells[iLastIndxRowForPrint, 12] = objSuppl.PositionQty;
                        oSheet.Cells[iLastIndxRowForPrint, 13] = objSuppl.AllPrice;
                        oSheet.Cells[iLastIndxRowForPrint, 14] = objSuppl.AllDiscount;
                        oSheet.Cells[iLastIndxRowForPrint, 15] = objSuppl.AllDiscountPrice;
                        oSheet.Cells[iLastIndxRowForPrint, 16] = objSuppl.AllCurrencyPrice;
                        oSheet.Cells[iLastIndxRowForPrint, 17] = objSuppl.AllCurrencyDiscount;
                        oSheet.Cells[iLastIndxRowForPrint, 18] = objSuppl.AllCurrencyDiscountPrice;

                    }
                    iLastIndxRowForPrint++;
                }
                objSuppl = null;
                for (System.Int32 i = 9; i <= 19; i++)
                {
                    if ((i >= 10) && (i <= 18))
                    {
                        oSheet.get_Range(oSheet.Cells[iLastIndxRowForPrint + 1, i], oSheet.Cells[iLastIndxRowForPrint + 1, i]).Formula = "=СУММ(R[-" + (iLastIndxRowForPrint - 3).ToString() + "]C:R[-1]C)";
                        oSheet.get_Range(oSheet.Cells[iLastIndxRowForPrint + 1, i], oSheet.Cells[iLastIndxRowForPrint + 1, i]).NumberFormat = "# ##0,00";
                        oSheet.get_Range(oSheet.Cells[iLastIndxRowForPrint + 1, i], oSheet.Cells[iLastIndxRowForPrint + 1, i]).Font.Size = 12;
                        oSheet.get_Range(oSheet.Cells[iLastIndxRowForPrint + 1, i], oSheet.Cells[iLastIndxRowForPrint + 1, i]).Font.Bold = true;
                        oSheet.get_Range(oSheet.Cells[iLastIndxRowForPrint + 1, i], oSheet.Cells[iLastIndxRowForPrint + 1, i]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    }

                }
                oSheet.get_Range("A3", "Z3").EntireColumn.AutoFit();

                oXL.Visible = true;
                oXL.UserControl = true;
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
        private void barBtnPrintList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (treeList.Nodes.Count > 0)
                {
                    ExportToExcelPDAList();
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
        #endregion

        #region Включить/исключить заказ в акт выполненных работ
        /// <summary>
        /// Включает/исключает заказ в акт выполненных работ
        /// </summary>
        /// <param name="objNode">узел с заказом</param>
        private void MakeSupplExcludeFromADJ(DevExpress.XtraTreeList.Nodes.TreeListNode objNode)
        {
            try
            {
                if ((objNode == null) || (objNode.Tag == null)) { return; }
                CPDASuppl objSuppl = (CPDASuppl)objNode.Tag;
                if (objSuppl == null) { return; }

                System.Boolean bExclude = !(objSuppl.IsExcludeFromADJ);
                System.String strInfo = ((bExclude == true) ? "Заказ будет исключен из акта выполненных работ." : "Заказ будет включен в акт выполненных работ.");

                if (DevExpress.XtraEditors.XtraMessageBox.Show(
                    strInfo + "\nПодтвердите, пожалуйста, начало операции.", "Внимание!",
                    System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information) == DialogResult.No)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                System.String strErr = "";

                if (CPDASuppl.ExcludeSupplFromADJ( m_objProfile, objSuppl.Id, bExclude, ref strErr) == true )
                {
                    objSuppl.IsExcludeFromADJ = bExclude;
                    objNode.SetValue(colExcludeFromADJ, bExclude);
                }

                SendMessageToLog(strErr);
                ShowSupplProperties((CPDASuppl)objNode.Tag);
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Добавление (удаление) в акт выполненных работ. Текст ошибки: " + f.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            return;
        }

        private void menuMakeSupplExcludeFromADJ_Click(object sender, EventArgs e)
        {
            try
            {
                if ((treeList.Nodes.Count == 0) || (treeList.FocusedNode == null)) { return; }
                MakeSupplExcludeFromADJ(treeList.FocusedNode);
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Добавление (удаление) в акт выполненных работ. Текст ошибки: " + f.Message);
            }
            finally
            {
            }

            return;
        }
        #endregion



    }
    public class ViewPDASupplList : PlugIn.IClassTypeView
    {
        public override void Run(UniXP.Common.MENUITEM objMenuItem, System.String strCaption)
        {
            frmPDASupplList obj = new frmPDASupplList(objMenuItem.objProfile, objMenuItem);
            obj.Text = strCaption;
            obj.MdiParent = objMenuItem.objProfile.m_objMDIManager.MdiParent;
            obj.Visible = true;
        }
    }

}

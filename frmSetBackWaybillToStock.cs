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

namespace ERPMercuryProcessingOrder
{
    public partial class frmSetBackWaybillToStock : DevExpress.XtraEditors.XtraForm
    {
        private UniXP.Common.CProfile m_objProfile;
        private CBackWaybill m_objBackWaybill;
        
        public frmSetBackWaybillToStock()
        {
            InitializeComponent();

            m_objProfile = null;
            m_objBackWaybill = null;
        }

        #region Установка параметров для постановки твоара на склад
        public void SetParamsForSetBackWaybillToStock(UniXP.Common.CProfile objProfile, CBackWaybill objBackWaybill)
        {
            try
            {
                m_objProfile = objProfile;
                m_objBackWaybill = objBackWaybill;

                System.String strErr = System.String.Empty;
                lblWaybillInfo.Text = System.String.Empty;
                cboxCustomer.Properties.Items.Clear();
                cboxStock.Properties.Items.Clear();
                cboxWaybillBackReason.Properties.Items.Clear();
                dtBeginDate.EditValue = null;

                cboxWaybillBackReason.Properties.Items.AddRange(CWaybillBackReason.GetWaybillBackReasonList(m_objProfile, ref strErr));
                cboxStock.Properties.Items.AddRange(CStock.GetStockList(m_objProfile, null));

                if (m_objBackWaybill != null)
                {
                    lblWaybillInfo.Text = String.Format("{0} от {1}", m_objBackWaybill.DocNum, m_objBackWaybill.BeginDate.ToShortDateString());

                    if (m_objBackWaybill.Customer != null)
                    {
                        cboxCustomer.Properties.Items.Add(m_objBackWaybill.Customer);
                    }
                    cboxWaybillBackReason.SelectedItem = (m_objBackWaybill.WaybillBackReason == null) ? null : cboxWaybillBackReason.Properties.Items.Cast<CWaybillBackReason>().SingleOrDefault<CWaybillBackReason>(x => x.ID.CompareTo(m_objBackWaybill.WaybillBackReason.ID) == 0);
                    cboxCustomer.SelectedItem = (m_objBackWaybill.Customer == null) ? null : cboxCustomer.Properties.Items.Cast<CCustomer>().SingleOrDefault<CCustomer>(x => x.ID.CompareTo(m_objBackWaybill.Customer.ID) == 0);
                    cboxStock.SelectedItem = (m_objBackWaybill.Stock == null) ? null : cboxStock.Properties.Items.Cast<CStock>().SingleOrDefault<CStock>(x => x.ID.CompareTo(m_objBackWaybill.Stock.ID) == 0);
                    dtBeginDate.DateTime = m_objBackWaybill.BeginDate;
                }
                else
                {
                    btnSave.Enabled = false;
                }

                cboxWaybillBackReason.Properties.ReadOnly = true;
                cboxCustomer.Properties.ReadOnly = true;
                cboxStock.Properties.ReadOnly = true;
                dtBeginDate.Properties.ReadOnly = false;

                dtBeginDate.Focus();
                DialogResult = System.Windows.Forms.DialogResult.None;

            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("SetParamsForSetBackWaybillToStock().\n\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return;

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
                DevExpress.XtraEditors.XtraMessageBox.Show("Cancel().\n\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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

        #region Поставить на приход
        /// <summary>
        /// Постановка товара на приход
        /// </summary>
        private void SetBackWaybillToStock()
        {
            try
            {
                if (m_objBackWaybill != null)
                {
                    System.DateTime BackWaybill_ShipDate = dtBeginDate.DateTime;
                    System.Guid NewBackWaybillState_Guid = System.Guid.Empty;
                    System.String strErr = System.String.Empty;

                    Cursor = Cursors.WaitCursor;

                    System.Boolean bRet = CBackWaybill.SetBackWaybillToStock(m_objProfile, m_objBackWaybill.ID, BackWaybill_ShipDate,
                        ref NewBackWaybillState_Guid, ref strErr);

                    if (bRet == true)
                    {
                        if (NewBackWaybillState_Guid.CompareTo(System.Guid.Empty) != 0)
                        {
                            List<CBackWaybillState> objBackWaybillStateList = CBackWaybillState.GetBackWaybillStateList(m_objProfile, ref strErr);
                            if (objBackWaybillStateList != null)
                            {
                                CBackWaybillState objNewBackWaybillState = objBackWaybillStateList.SingleOrDefault<CBackWaybillState>(x => x.ID.CompareTo(NewBackWaybillState_Guid) == 0);
                                if (objNewBackWaybillState != null)
                                {
                                    m_objBackWaybill.BackWaybillState = objNewBackWaybillState;
                                }
                            }
                            objBackWaybillStateList = null;
                        }

                        DialogResult = System.Windows.Forms.DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(strErr, "Ошибка",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }
                }
                

            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("SetBackWaybillToStock().\n\nТекст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            return;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            SetBackWaybillToStock();
        }

        #endregion

    }
}

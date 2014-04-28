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
    public partial class frmCreateWaybillFromSuppl : DevExpress.XtraEditors.XtraForm
    {
        private UniXP.Common.CProfile m_objProfile;
        private System.Guid m_SupplGuid;

        public System.Guid Waybill_Guid { get; set; }
        public System.Guid OrderState_Guid { get; set; }
        public System.Boolean NeedOpenWaybill { get; set; }

        public frmCreateWaybillFromSuppl(UniXP.Common.CProfile objProfile)
        {
            InitializeComponent();

            m_objProfile = objProfile;
            m_SupplGuid = System.Guid.Empty;
            Waybill_Guid = System.Guid.Empty;
            OrderState_Guid = System.Guid.Empty;
            NeedOpenWaybill = false;

            checkEditForStock.Checked = false;
            checkEditOpenWaybillAfterCreate.Checked = true;

            lblCustomerInfo.Text = System.String.Empty;
            lblSupplInfo.Text = System.String.Empty;
        }

        public void OpenForm( System.Guid Suppl_Guid, System.String strSupplInfo, System.String strCustomerInfo, 
            System.String strWaybillNum, System.DateTime dtWaybillDate )
        {
            try
            {
                Waybill_Guid = System.Guid.Empty;
                NeedOpenWaybill = false;

                m_SupplGuid = Suppl_Guid;
                txtWaybilllNum.Text = strWaybillNum;
                dtBeginDate.EditValue = dtWaybillDate;
                lblCustomerInfo.Text = ( "Клиент: " + strCustomerInfo );
                lblSupplInfo.Text = ( "Заказ: " + strSupplInfo );
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("OpenForm. Текст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
            finally
            {
            }
            return;

        }

        private void CreateWaybill()
        {
            try
            {
                if (txtWaybilllNum.Text.Trim().Length == 0)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Укажите, пожалуйста, номер накладной.", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
                if (dtBeginDate.DateTime.CompareTo(System.DateTime.MinValue) == 0)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Укажите, пожалуйста, дату накладной.", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
                if (m_SupplGuid.CompareTo( System.Guid.Empty ) == 0)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Не удалось определить номер заказа.\nОбратитесь, пожалуйста, к разработчикам.", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }

                System.String strErr = System.String.Empty;
                System.Guid WaybillID = System.Guid.Empty;
                System.Guid OrderStatetID = System.Guid.Empty;

                Cursor = Cursors.WaitCursor;

                if (ERP_Mercury.Common.CWaybill.CreateWaybillFromSuppl(m_objProfile, m_SupplGuid,
                    dtBeginDate.DateTime, txtWaybilllNum.Text.Trim(), checkEditForStock.Checked, 
                    ref WaybillID, ref OrderStatetID, ref strErr) == true)
                {
                    Waybill_Guid = WaybillID;
                    OrderState_Guid = OrderStatetID;
                    NeedOpenWaybill = checkEditOpenWaybillAfterCreate.Checked;
                    
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                    Close();
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(strErr, "Ошибка",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("CreateWaybill. Текст ошибки: " + f.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            return;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CreateWaybill();
        }

        private void frmCreateWaybillFromSuppl_Shown(object sender, EventArgs e)
        {
            btnSave.Focus();
        }

    }
}

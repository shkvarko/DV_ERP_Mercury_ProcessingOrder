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
    public partial class frmIntWaybillList : DevExpress.XtraEditors.XtraForm
    {
        #region Свойства
        private UniXP.Common.CProfile m_objProfile;
        private UniXP.Common.MENUITEM m_objMenuItem;
        private List<CIntWaybill> m_objWaybillList;
        private List<CIntWaybillState> m_objWaybillStateList;
        private CIntWaybill m_objSelectedWaybill;
        private ctrlIntWaybillEditor frmWaybillEditor;
        private DevExpress.XtraGrid.Views.Base.ColumnView ColumnView
        {
            get { return gridControlDocList.MainView as DevExpress.XtraGrid.Views.Base.ColumnView; }
        }

        // используемые динамические права 
        private System.Boolean IsAvailableDR_ViewSupplPayForm1;             // "Заказ ф1 просмотр";
        private System.Boolean IsAvailableDR_ViewSupplPayForm2;             // "Заказ ф2 просмотр";
        private System.Boolean IsAvailableDR_EditSupplPayForm1;             // "Заказ ф1 редактировать";
        private System.Boolean IsAvailableDR_EditSupplPayForm2;             // "Заказ ф2 редактировать";
        private System.Boolean IsAvailableDR_MoveSupplToWaybillPayForm1;    // "Заказ ф1 перевод в ТТН";
        private System.Boolean IsAvailableDR_MoveSupplToWaybillPayForm2;    // "Заказ ф2 перевод в ТТН";

        #endregion

        #region Конструктор
        public frmIntWaybillList(UniXP.Common.MENUITEM objMenuItem)
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

            // динамические права
            UniXP.Common.CClientRights objClientRights = m_objProfile.GetClientsRight();
            IsAvailableDR_ViewSupplPayForm1 = objClientRights.GetState(ERPMercuryProcessingOrder.Consts.strDR_ViewSupplPayForm1);
            IsAvailableDR_ViewSupplPayForm2 = objClientRights.GetState(ERPMercuryProcessingOrder.Consts.strDR_ViewSupplPayForm2);
            IsAvailableDR_EditSupplPayForm1 = objClientRights.GetState(ERPMercuryProcessingOrder.Consts.strDR_EditSupplPayForm1);
            IsAvailableDR_EditSupplPayForm2 = objClientRights.GetState(ERPMercuryProcessingOrder.Consts.strDR_EditSupplPayForm2);
            IsAvailableDR_MoveSupplToWaybillPayForm1 = objClientRights.GetState(ERPMercuryProcessingOrder.Consts.strDR_MoveSupplToWaybillPayForm1);
            IsAvailableDR_MoveSupplToWaybillPayForm2 = objClientRights.GetState(ERPMercuryProcessingOrder.Consts.strDR_MoveSupplToWaybillPayForm2);

            objClientRights = null;

            m_objWaybillList = null;
            frmWaybillEditor = null;
            m_objSelectedWaybill = null;
            m_objWaybillStateList = null;

            AddGridColumns();

            dtBeginDate.DateTime = System.DateTime.Today.AddDays(-7); // new DateTime(System.DateTime.Today.Year, 1, 1);
            dtEndDate.DateTime = System.DateTime.Today; //new DateTime(System.DateTime.Today.Year, 12, 31);
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

        #region Обработчики событий

        private void OnChangeWaybillPropertie(Object sender, ChangeIntWaybillPropertieEventArgs e)
        {
            try
            {
                tabControl.SelectedTabPage = tabPageViewer;
                if (e.ActionType == ERP_Mercury.Common.enumActionSaveCancel.Save)
                {
                    CIntWaybill objWaybill = GetSelectedWaybill();
                    if ((objWaybill != null) && (e.WaybillCurrentStateGuid.CompareTo(System.Guid.Empty) != 0))
                    {
                        if (objWaybill.DocState.ID.CompareTo(e.WaybillCurrentStateGuid) != 0)
                        {
                            if ((m_objWaybillStateList != null) && (m_objWaybillStateList.Count > 0))
                            {
                                objWaybill.DocState = m_objWaybillStateList.SingleOrDefault<CIntWaybillState>(x => x.ID.CompareTo(e.WaybillCurrentStateGuid) == 0);
                                gridControlDocList.RefreshDataSource();
                            }
                        }

                    }
                }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("OnChangeWaybillPropertie. Текст ошибки: " + f.Message);
            }
            finally // очищаем занимаемые ресурсы
            {
            }

            return;
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

        #region Отмена изменений в редакторе накладной
        private void CancelChangesInWaybill()
        {
            try
            {
                ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
                this.SuspendLayout();

                tabControl.SelectedTabPage = tabPageViewer;
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Отмена изменений. Текст ошибки: " + f.Message);
            }
            finally // очищаем занимаемые ресурсы
            {
                ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
                this.ResumeLayout(false);
            }

            return;
        }
        #endregion

        #region Изменения в редакторе накладной подтверждены
        private void SaveChangesInWaybill()
        {
            try
            {
                ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
                this.SuspendLayout();

                tabControl.SelectedTabPage = tabPageViewer;
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Подтверждение изменений. Текст ошибки: " + f.Message);
            }
            finally // очищаем занимаемые ресурсы
            {
                ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
                this.ResumeLayout(false);
            }

            return;
        }
        #endregion

        #region Редактировать накладную
        /// <summary>
        /// Загружает реквизиты и табличную часть накладной для редактирования
        /// </summary>
        /// <param name="objWaybill">накладная</param>
        private void EditWaybill( CIntWaybill objWaybill )
        {
            if( objWaybill == null ) { return; }

            if ((IsAvailableDR_ViewSupplPayForm1 || IsAvailableDR_ViewSupplPayForm2) == false)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("У Вас нет прав на операцию \"просмотр накладной\".", "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                return;
            }

            try
            {
                m_objSelectedWaybill = objWaybill;

                System.String strErr = System.String.Empty;
                m_objSelectedWaybill.WaybillItemList = CIntWaybillItem.GetIntWaybillTablePart(m_objProfile, m_objSelectedWaybill.ID, ref strErr);

                if (frmWaybillEditor == null)
                {
                    frmWaybillEditor = new ctrlIntWaybillEditor(m_objMenuItem);
                    tableLayoutPanelAgreementEditor.Controls.Add(frmWaybillEditor, 0, 0);
                    frmWaybillEditor.Dock = DockStyle.Fill;
                    frmWaybillEditor.ChangeIntWaybillProperties += OnChangeWaybillPropertie;
                }

                frmWaybillEditor.EditWaybill(m_objSelectedWaybill, false);

                tabControl.SelectedTabPage = tabPageEditor;

            }
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка редактирования накладной. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }
        private void barBtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                EditWaybill(GetSelectedWaybill());

            }//try
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка редактирования накладной. Текст ошибки: " + f.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            return;
        }

        private void gridControlDocListGrid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                EditWaybill(GetSelectedWaybill());

            }//try
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка редактирования накладной. Текст ошибки: " + f.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            return;
        }
        /// <summary>
        /// Возвращает ссылку на выбранную в списке накладную
        /// </summary>
        /// <returns>ссылка на накладную</returns>
        private ERP_Mercury.Common.CIntWaybill GetSelectedWaybill()
        {
            ERP_Mercury.Common.CIntWaybill objRet = null;
            try
            {
                if ((((DevExpress.XtraGrid.Views.Grid.GridView)gridControlDocList.MainView).RowCount > 0) &&
                    (((DevExpress.XtraGrid.Views.Grid.GridView)gridControlDocList.MainView).FocusedRowHandle >= 0))
                {
                    System.Guid uuidID = (System.Guid)(((DevExpress.XtraGrid.Views.Grid.GridView)gridControlDocList.MainView)).GetFocusedRowCellValue("ID");

                    objRet = m_objWaybillList.Single<CIntWaybill>(x => x.ID.CompareTo(uuidID) == 0);
                }
            }//try
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка поиска идентификатора выбранной в списке накладной. Текст ошибки: " + f.Message);
            }
            finally
            {
            }

            return objRet;
        }

        #endregion

        #region Копировать накладную
        /// <summary>
        /// Копирует свойства накладной в новый и открывает её для редактирования
        /// </summary>
        /// <param name="objWaybill">накладная</param>
        private void CopyWaybill(CIntWaybill objWaybill)
        {
            if (objWaybill == null) { return; }
            if ((IsAvailableDR_EditSupplPayForm1 || IsAvailableDR_EditSupplPayForm2) == false)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("У Вас нет прав на операцию \"копирование накладной\".", "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                return;
            }
            try
            {
                m_objSelectedWaybill = objWaybill;

                ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
                this.SuspendLayout();

                if (frmWaybillEditor == null)
                {
                    frmWaybillEditor = new ctrlIntWaybillEditor(m_objMenuItem);
                    tableLayoutPanelAgreementEditor.Controls.Add(frmWaybillEditor, 0, 0);
                    frmWaybillEditor.Dock = DockStyle.Fill;
                    frmWaybillEditor.ChangeIntWaybillProperties += OnChangeWaybillPropertie;
                }

                frmWaybillEditor.CopyWaybill(objWaybill);

                tabControl.SelectedTabPage = tabPageEditor;

            }
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка копирования накладной. Текст ошибки: " + f.Message);
            }
            finally
            {
                ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
                this.ResumeLayout(false);
                tabControl.SelectedTabPage = tabPageEditor;
            }
            return;
        }

        private void barBtnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                CopyWaybill(GetSelectedWaybill());

            }//try
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка копирования накладной. Текст ошибки: " + f.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            return;
        }

        #endregion

        #region Удалить накладную
        /// <summary>
        /// Удаляет накладную
        /// </summary>
        /// <param name="objWaybill">накладная</param>
        private void DeleteWaybill(CIntWaybill objWaybill)
        {
            if (objWaybill == null) { return; }
            if ((IsAvailableDR_EditSupplPayForm1 || IsAvailableDR_EditSupplPayForm2) == false)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("У Вас нет прав на операцию \"удаление накладной\".", "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                return;
            }
            try
            {
                System.Int32 iFocusedRowHandle = gridViewDocList.FocusedRowHandle;
                if (DevExpress.XtraEditors.XtraMessageBox.Show(String.Format("Подтвердите, пожалуйста, удаление накладной.\n\n№{0}\nКомпания (источник): {1}\nСклад (источник): {2}\nКомпания (получатель): {1}\nСклад (получатель): {2}",
                    objWaybill.DocNum, objWaybill.CompanySrcAcronym, objWaybill.StockSrcName), "Подтверждение",
                    System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == DialogResult.No) { return; }

                System.String strErr = System.String.Empty;
                //if ( CIntOrder.CancelIntOrder(m_objProfile,  null, objWaybill.ID, ref strErr) == true)
                //{
                //    LoadDocList();
                //}
                //else
                //{
                //    SendMessageToLog("Удаление накладной. Текст ошибки: " + strErr);
                //}
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Удаление накладной. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }
        private void barBtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteWaybill(GetSelectedWaybill());
            }//try
            catch (System.Exception f)
            {
                SendMessageToLog("Удаление накладной. Текст ошибки: " + f.Message);
            }
            finally
            {
            }

            return;
        }
        #endregion

        #region Новая накладная
        /// <summary>
        /// Открывает редактор накладной в режиме "Новая накладная"
        /// </summary>
        private void NewWaybill()
        {
            if ((IsAvailableDR_EditSupplPayForm1 || IsAvailableDR_EditSupplPayForm2) == false)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("У Вас нет прав на операцию \"создание новой накладной\".", "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                return;
            }

            try
            {
                ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
                this.SuspendLayout();

                if (frmWaybillEditor == null)
                {
                    frmWaybillEditor = new ctrlIntWaybillEditor(m_objMenuItem);
                    tableLayoutPanelAgreementEditor.Controls.Add(frmWaybillEditor, 0, 0);
                    frmWaybillEditor.Dock = DockStyle.Fill;
                    frmWaybillEditor.ChangeIntWaybillProperties += OnChangeWaybillPropertie;
                }

                ERP_Mercury.Common.CCompany objSelectedCompanySrc = (((cboxCompanySrc.SelectedItem == null) || (System.Convert.ToString(cboxCompanySrc.SelectedItem) == "")) ? null : ((CCompany)cboxCompanySrc.SelectedItem));
                ERP_Mercury.Common.CStock objSelectedStockSrc = (((cboxStockSrc.SelectedItem == null) || (System.Convert.ToString(cboxStockSrc.SelectedItem) == "")) ? null : ((CStock)cboxStockSrc.SelectedItem));
                ERP_Mercury.Common.CCompany objSelectedCompanyDst = (((cboxCompanyDst.SelectedItem == null) || (System.Convert.ToString(cboxCompanyDst.SelectedItem) == "")) ? null : ((CCompany)cboxCompanyDst.SelectedItem));
                ERP_Mercury.Common.CStock objSelectedStockDst = (((cboxStockDst.SelectedItem == null) || (System.Convert.ToString(cboxStockDst.SelectedItem) == "")) ? null : ((CStock)cboxStockDst.SelectedItem));

                frmWaybillEditor.NewWaybill(objSelectedCompanySrc, objSelectedStockSrc, objSelectedCompanyDst, objSelectedStockDst);

                tabControl.SelectedTabPage = tabPageEditor;

            }
            catch (System.Exception f)
            {
                SendMessageToLog("Создание накладной. Текст ошибки: " + f.Message);
            }
            finally
            {
                ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
                this.ResumeLayout(false);
                tabControl.SelectedTabPage = tabPageEditor;
            }
            return;
        }
        private void barBtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                NewWaybill();
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Создание накладной. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }

        #endregion

        #region Импорт накладной
        private void btnImportProduct_Click(object sender, EventArgs e)
        {
            try
            {
                ImportDataFromExcel();
            }
            catch (System.Exception f)
            {
                SendMessageToLog("btnImportProduct_Click. Текст ошибки: " + f.Message);
            }
            return;
        }
        /// <summary>
        /// Импорт данных из файла MS Excel
        /// </summary>
        private void ImportDataFromExcel()
        {
            if ((IsAvailableDR_EditSupplPayForm1 || IsAvailableDR_EditSupplPayForm2) == false)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("У Вас нет прав на операцию \"импорт накладной\".", "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                return;
            }
            try
            {
                if (cboxCompanySrc.SelectedItem == null) { return; }
                if (cboxStockSrc.SelectedItem == null) { return; }

                NewWaybill();

                if (frmWaybillEditor != null) { frmWaybillEditor.ImportFromExcel(); }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("ImportDataFromExcel. Текст ошибки: " + f.Message);
            }
            return;
        }

        #endregion

        #region Проверка значений для поиска
        private System.Boolean ValidatePropertiesForSearch()
        {
            System.Boolean bRet = true;
            try
            {
                //bRet = (cboxStockSrc.SelectedItem != null);
            }
            catch (System.Exception f)
            {
                SendMessageToLog("ValidatePropertiesIntWaybill. Текст ошибки: " + f.Message);
            }

            return bRet;
        }
        private void SetColorsForSearch()
        {
            try
            {
                cboxCompanySrc.Properties.Appearance.BackColor = (((cboxCompanySrc.SelectedItem == null) || ((cboxCompanySrc.Text.Trim().Length == 0))) ? System.Drawing.Color.Tomato : System.Drawing.Color.White);
                cboxCompanyDst.Properties.Appearance.BackColor = (((cboxCompanyDst.SelectedItem == null) || (cboxCompanyDst.Text.Trim().Length == 0)) ? System.Drawing.Color.Tomato : System.Drawing.Color.White);
                cboxStockSrc.Properties.Appearance.BackColor = (((cboxStockSrc.SelectedItem == null) || (cboxStockSrc.Text.Trim().Length == 0)) ? System.Drawing.Color.Tomato : System.Drawing.Color.White);
            }
            catch (System.Exception f)
            {
                SendMessageToLog("SetColorsForSearch. Текст ошибки: " + f.Message);
            }

            return;
        }

        private void ChangeConditionForSearch()
        {
            //SetColorsForSearch();

            barBtnRefresh.Enabled = ValidatePropertiesForSearch();
        }

        private void cboxStock_SelectedValueChanged(object sender, EventArgs e)
        {
            ChangeConditionForSearch();
        }
        #endregion

        #region Список накладных
        private void AddGridColumn(DevExpress.XtraGrid.Views.Base.ColumnView view, string fieldName, string caption) { AddGridColumn(view, fieldName, caption, null); }
        private void AddGridColumn(DevExpress.XtraGrid.Views.Base.ColumnView view, string fieldName, string caption, DevExpress.XtraEditors.Repository.RepositoryItem item) { AddGridColumn(view, fieldName, caption, item, "", DevExpress.Utils.FormatType.None); }
        private void AddGridColumn(DevExpress.XtraGrid.Views.Base.ColumnView view, string fieldName, string caption, DevExpress.XtraEditors.Repository.RepositoryItem item, string format, DevExpress.Utils.FormatType type)
        {
            DevExpress.XtraGrid.Columns.GridColumn column = view.Columns.AddField(fieldName);
            column.Caption = caption;
            column.ColumnEdit = item;
            column.DisplayFormat.FormatType = type;
            column.DisplayFormat.FormatString = format;
            column.VisibleIndex = view.VisibleColumns.Count;
        }

        private void AddGridColumns()
        {
            ColumnView.Columns.Clear();
            AddGridColumn(ColumnView, "ID", "Идентификатор");
            AddGridColumn(ColumnView, "DocStateId", "Состояние");
            AddGridColumn(ColumnView, "DocStateName", "Состояние");
            AddGridColumn(ColumnView, "DocNum", "Номер");
            AddGridColumn(ColumnView, "BeginDate", "Дата");
            AddGridColumn(ColumnView, "IntOrderShipModeName", "Вид отгрузки");
            AddGridColumn(ColumnView, "ShipDate", "Дата отгрузки");
            AddGridColumn(ColumnView, "CompanySrcAcronym", "Компания-источник");
            AddGridColumn(ColumnView, "StockSrcName", "Склад-источник");
            AddGridColumn(ColumnView, "CompanyDstAcronym", "Компания-получатель");
            AddGridColumn(ColumnView, "StockDstName", "Склад-получатель");
            AddGridColumn(ColumnView, "DepartCode", "Подразделение");
            //          AddGridColumn(ColumnView, "PaymentTypeName", "Форма оплаты");

            AddGridColumn(ColumnView, "Quantity", "Количество, шт.");

            AddGridColumn(ColumnView, "SumOrder", "Сумма, руб.");
            AddGridColumn(ColumnView, "SumDiscount", "Сумма скидки, руб.");
            AddGridColumn(ColumnView, "SumWithDiscount", "С учетом скидки, руб.");

            AddGridColumn(ColumnView, "SumRetail", "Сумма розн., руб.");

            foreach (DevExpress.XtraGrid.Columns.GridColumn objColumn in ColumnView.Columns)
            {
                objColumn.OptionsColumn.AllowEdit = false;
                //objColumn.OptionsColumn.AllowFocus = false;
                objColumn.OptionsColumn.ReadOnly = true;
                if ((objColumn.FieldName == "ID") || (objColumn.FieldName == "DocStateId"))
                {
                    objColumn.Visible = false;
                }
                if ((objColumn.FieldName == "BeginDate") || (objColumn.FieldName == "ShipDate"))
                {
                    objColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    objColumn.DisplayFormat.FormatString = "dd.MM.yyyy";
                }
                if ((objColumn.FieldName == "Quantity") || (objColumn.FieldName == "QuantityReturn"))
                {
                    objColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    objColumn.DisplayFormat.FormatString = "### ### ##0";
                    objColumn.SummaryItem.FieldName = objColumn.FieldName;
                    objColumn.SummaryItem.DisplayFormat = "{0:### ### ##0}";
                    objColumn.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                }
                if ((objColumn.FieldName == "SumOrder") || (objColumn.FieldName == "SumDiscount") ||
                    (objColumn.FieldName == "SumWithDiscount") || (objColumn.FieldName == "SumReturn") ||
                    (objColumn.FieldName == "SumPayment") || (objColumn.FieldName == "SumRetail"))
                {
                    objColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    objColumn.DisplayFormat.FormatString = "### ### ##0";
                    objColumn.SummaryItem.FieldName = objColumn.FieldName;
                    objColumn.SummaryItem.DisplayFormat = "{0:### ### ##0}";
                    objColumn.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                }
            }
        }

        private void cboxCompanySrc_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                LoadStockForCompany(cboxStockSrc, cboxCompanySrc);
            }
            catch (System.Exception f)
            {
                SendMessageToLog("cboxCompanySrc_SelectedValueChanged. Текст ошибки: " + f.Message);
            }
            return;
        }

        private void cboxCompanyDst_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                LoadStockForCompany(cboxStockDst, cboxCompanyDst);
            }
            catch (System.Exception f)
            {
                SendMessageToLog("cboxCompanyDst_SelectedValueChanged. Текст ошибки: " + f.Message);
            }
            return;
        }

        private void cboxStockSrc_SelectedValueChanged(object sender, EventArgs e)
        {
            ChangeConditionForSearch();
        }


        /// <summary>
        /// загружает склады для выбранной компании
        /// </summary>
        /// <param name="cboxStock">выпадающий список складов</param>
        /// <param name="cboxCompany">выпадающий список компаний</param>
        private void LoadStockForCompany(DevExpress.XtraEditors.ComboBoxEdit cboxStock,
            DevExpress.XtraEditors.ComboBoxEdit cboxCompany)
        {
            try
            {
                cboxStock.SelectedItem = null;
                cboxStock.Properties.Items.Clear();
                cboxStock.Properties.Items.Add(new ERP_Mercury.Common.CStock());

                ERP_Mercury.Common.CCompany objSelectedCompany = ((cboxCompany.SelectedItem == null) || (((ERP_Mercury.Common.CCompany)cboxCompany.SelectedItem).ID == System.Guid.Empty) ? null : (ERP_Mercury.Common.CCompany)cboxCompany.SelectedItem);

                if (objSelectedCompany != null)
                {
                    List<ERP_Mercury.Common.CStock> objStockList = ERP_Mercury.Common.CStock.GetStockList(m_objProfile, null);
                    List<int> StockIdList = new List<int>();
                    if (objStockList != null)
                    {
                        foreach (ERP_Mercury.Common.CStock objStock in objStockList)
                        {
                            if (objStock.Company.ID == objSelectedCompany.ID)
                            {
                                cboxStock.Properties.Items.Add(objStock);

                                StockIdList.Add(objStock.IBId);
                            }
                        }
                    }
                    //if (StockIdList.Count > 0)
                    //{
                    //    cboxStock.SelectedItem = objStockList.Single<ERP_Mercury.Common.CStock>(x => x.IBId == StockIdList.Min<int>());
                    //}
                    StockIdList = null;
                }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("LoadStockForCompany. Текст ошибки: " + f.Message);
            }
            return;
        }
        /// <summary>
        /// Заполнение выпадающих списков
        /// </summary>
        private void LoadComboBox()
        {
            try
            {
                //System.Boolean CanViewPaymentType2 = m_objProfile.GetClientsRight().GetState(ERPMercuryProcessingOrder.Consts.strDR_ViewWaybillPayForm2);
                //System.Int32 DefPaymentTypeId = 0;
                //System.Boolean BlockOtherPaymentType = false;
                //System.String CompanyAcronymForPaymentType1 = System.String.Empty;
                //System.String CompanyAcronymForPaymentType2 = System.String.Empty;
                //System.Int32 ERROR_NUM = 0;
                System.String strErr = System.String.Empty;

                //CWaybillDataBaseModel.GetWaybillListSettingsDefault(m_objProfile, null, CanViewPaymentType2, ref DefPaymentTypeId,
                //    ref BlockOtherPaymentType, ref CompanyAcronymForPaymentType1, ref CompanyAcronymForPaymentType2,
                //    ref ERROR_NUM, ref strErr);


                cboxCompanySrc.Properties.Items.Clear();
                cboxCompanyDst.Properties.Items.Clear();

                List<ERP_Mercury.Common.CCompany> objCompanyList = ERP_Mercury.Common.CCompany.GetCompanyList(m_objProfile, null);
                cboxCompanySrc.Properties.Items.AddRange(objCompanyList);
                cboxCompanyDst.Properties.Items.AddRange(objCompanyList);

                //if ((CompanyAcronymForPaymentType1.Length > 0) && (objCompanyList.Count > 0))
                //{
                //    cboxCompanySrc.SelectedItem = cboxCompanySrc.Properties.Items.Cast<CCompany>().SingleOrDefault<CCompany>(x => x.Abbr == CompanyAcronymForPaymentType1);
                //    cboxCompanyDst.SelectedItem = cboxCompanyDst.Properties.Items.Cast<CCompany>().SingleOrDefault<CCompany>(x => x.Abbr == CompanyAcronymForPaymentType1);
                //}

                if ((objCompanyList != null) && (objCompanyList.Count > 0))
                {
                    List<int> CompanyIBIdList = new List<int>();

                    foreach (ERP_Mercury.Common.CCompany objCompany in objCompanyList)
                    {
                        CompanyIBIdList.Add(objCompany.InterBaseID);
                    }
                }
                objCompanyList = null;

                LoadStockForCompany(cboxStockSrc, cboxCompanySrc);
                LoadStockForCompany(cboxStockDst, cboxCompanyDst);

                m_objWaybillStateList = CIntWaybillState.GetIntWaybillStateList(m_objProfile, ref strErr);

                ChangeConditionForSearch();
            }
            catch (System.Exception f)
            {
                SendMessageToLog("LoadComboBox. Текст ошибки: " + f.Message);
            }
            return;
        }

        public System.Boolean LoadDocList()
        {
            System.Boolean bRet = false;
            this.Cursor = Cursors.WaitCursor;
            System.String strErr = System.String.Empty;
            try
            {
                System.Int32 iRowHandle = gridViewDocList.FocusedRowHandle;

                gridControlDocList.DataSource = null;
                System.DateTime IntWaybill_DateBegin = dtBeginDate.DateTime;
                System.DateTime IntWaybill_DateEnd = dtEndDate.DateTime;
                System.Guid uuidCompanySrcId = ((cboxCompanySrc.SelectedItem == null) ? System.Guid.Empty : ((ERP_Mercury.Common.CCompany)cboxCompanySrc.SelectedItem).ID);
                System.Guid uuidStockSrcId = ((cboxStockSrc.SelectedItem == null) ? System.Guid.Empty : ((ERP_Mercury.Common.CStock)cboxStockSrc.SelectedItem).ID);
                System.Guid uuidCompanyDstId = ((cboxCompanyDst.SelectedItem == null) ? System.Guid.Empty : ((ERP_Mercury.Common.CCompany)cboxCompanyDst.SelectedItem).ID);
                System.Guid uuidStockDstId = ((cboxStockDst.SelectedItem == null) ? System.Guid.Empty : ((ERP_Mercury.Common.CStock)cboxStockDst.SelectedItem).ID);
                System.Guid uuidPaymentTypeId = System.Guid.Empty;
                System.Boolean bOnlyUnShippedWaybills = false;

                m_objWaybillList = CIntWaybill.GetWaybillList(m_objProfile, System.Guid.Empty, IntWaybill_DateBegin,
                    IntWaybill_DateEnd, uuidCompanySrcId, uuidStockSrcId, uuidCompanyDstId, uuidStockDstId, uuidPaymentTypeId,
                    ref strErr, bOnlyUnShippedWaybills);

                if (m_objWaybillList != null)
                {
                    if (DocNum.Text.Trim().Length > 0)
                    {
                        m_objWaybillList = m_objWaybillList.Where<CIntWaybill>(x => x.DocNum.Contains(DocNum.Text.Trim()) == true).ToList<CIntWaybill>();
                    }

                    gridControlDocList.DataSource = m_objWaybillList;

                }

                if ((gridViewDocList.RowCount > 0) && (iRowHandle >= 0) && (gridViewDocList.RowCount > iRowHandle))
                {
                    gridViewDocList.FocusedRowHandle = iRowHandle;
                }

                bRet = true;
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Загрузка журнала накладных на внутреннее перемещение. Текст ошибки: " + f.Message);
                //
            }
            finally
            {
                this.Cursor = Cursors.Default;
                this.Refresh();
            }

            return bRet;
        }

        private void barBtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadDocList();
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка обновления списка. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }
        private void dtBeginDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter) { LoadDocList(); }
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Ошибка обновления списка. Текст ошибки: " + f.Message);
            }
            finally
            {
            }
            return;
        }

        private void gridViewDocList_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            {
                try
                {
                    System.Drawing.Image img = null;
                    if (e.Column.FieldName == "DocStateName")
                    {
                        System.Int32 iSupplState = (System.Int32)gridViewDocList.GetRowCellValue(e.RowHandle, gridViewDocList.Columns["DocStateId"]);
                        System.Int32 iImgIndx = -1;
                        switch (iSupplState)
                        {
                            case 0:
                                {
                                    iImgIndx = 8;

                                    break;
                                }
                            case 1:
                                {
                                    iImgIndx = 7;
                                    break;
                                }
                            case 2:
                                {
                                    iImgIndx = 4;
                                    break;
                                }
                            case 3:
                                {
                                    iImgIndx = 0;
                                    break;
                                }
                            case 4:
                                {
                                    iImgIndx = 2;
                                    break;
                                }
                            default:
                                {
                                    iImgIndx = 8;
                                    break;
                                }
                        }

                        if (iImgIndx >= imageCollection.Images.Count)
                        {
                            iImgIndx = 8;
                        }

                        img = imageCollection.Images[iImgIndx];
                    }

                    // собственно отрисовка
                    if (img != null)
                    {
                        Rectangle rImg = new Rectangle(e.Bounds.X + 3, e.Bounds.Y + (e.Bounds.Height - img.Size.Height) / 2, img.Width, img.Height);
                        e.Graphics.DrawImage(img, rImg);
                        Rectangle r = e.Bounds;

                        //r.Inflate(-1, -1);
                        //e.Graphics.FillRectangle(e.Appearance.brush, r);

                        r.Inflate(-(rImg.Width + 5), 0);
                        e.Appearance.DrawString(e.Cache, e.DisplayText, r);

                        e.Handled = true;
                    }
                }
                catch (System.Exception f)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("gridViewDocList_CustomDrawCell\n" + f.Message, "Ошибка",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                return;
            }

        }

        #endregion

        #region Свойства накладной
        private void gridViewDocList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                ShowDocProperties(GetSelectedWaybill());

                barBtnAdd.Enabled = (IsAvailableDR_EditSupplPayForm1 || IsAvailableDR_EditSupplPayForm2);
                barBtnEdit.Enabled = ((IsAvailableDR_EditSupplPayForm1 || IsAvailableDR_EditSupplPayForm2) && (e.FocusedRowHandle >= 0));
                barBtnCopy.Enabled = ((IsAvailableDR_EditSupplPayForm1 || IsAvailableDR_EditSupplPayForm2) && (e.FocusedRowHandle >= 0));
                barBtnDelete.Enabled = ((IsAvailableDR_EditSupplPayForm1 || IsAvailableDR_EditSupplPayForm2) && (e.FocusedRowHandle >= 0));
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Отображение свойств документа. Текст ошибки: " + f.Message);
            }
            return;
        }
        private void gridViewDocList_FocusedRowChanged(object sender, EventArgs e)
        {
            try
            {
                ShowDocProperties(GetSelectedWaybill());

                barBtnAdd.Enabled = (IsAvailableDR_EditSupplPayForm1 || IsAvailableDR_EditSupplPayForm2);
                barBtnEdit.Enabled = ((IsAvailableDR_EditSupplPayForm1 || IsAvailableDR_EditSupplPayForm2) && (gridViewDocList.FocusedRowHandle >= 0));
                barBtnCopy.Enabled = ((IsAvailableDR_EditSupplPayForm1 || IsAvailableDR_EditSupplPayForm2) && (gridViewDocList.FocusedRowHandle >= 0));
                barBtnDelete.Enabled = ((IsAvailableDR_EditSupplPayForm1 || IsAvailableDR_EditSupplPayForm2) && (gridViewDocList.FocusedRowHandle >= 0));
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Отображение свойств документа. Текст ошибки: " + f.Message);
            }
            return;
        }

        /// <summary>
        /// Отображает свойства документа
        /// </summary>
        /// <param name="objDoc">документ</param>
        private void ShowDocProperties(CIntWaybill objDoc)
        {
            try
            {
                this.tableLayoutPanel3.SuspendLayout();

                txtCompanyDst.Text = System.String.Empty;
                txtCompanySrc.Text = System.String.Empty;
                txtDocAllPrice.Text = System.String.Empty;
                txtDocBeginDate.Text = System.String.Empty;
                txtDoclDescrpn.Text = System.String.Empty;
                txtDocNum.Text = System.String.Empty;
                txtDocNumBegindate.Text = System.String.Empty;
                txtDocPositionCount.Text = System.String.Empty;
                txtDocQty.Text = System.String.Empty;
                txtDocRetailAllPrice.Text = System.String.Empty;
                txtDocState.Text = System.String.Empty;
                txtStockDst.Text = System.String.Empty;
                txtStockSrc.Text = System.String.Empty;

                if (objDoc != null)
                {
                    txtCompanyDst.Text = objDoc.CompanyDstAcronym;
                    txtCompanySrc.Text = objDoc.CompanySrcAcronym;
                    txtDocAllPrice.Text = System.String.Format("{0:### ### ##0}", objDoc.SumWaybill);
                    txtDocBeginDate.Text = objDoc.BeginDate.ToShortDateString();
                    txtDoclDescrpn.Text = objDoc.Description;
                    txtDocNum.Text = objDoc.DocNum;
                    txtDocNumBegindate.Text = (String.Format("{0} от {1}", objDoc.DocNum, objDoc.BeginDate.ToShortDateString()));
                    txtDocPositionCount.Text = System.String.Format("{0:### ### ##0}", objDoc.Quantity);
                    txtDocQty.Text = System.String.Format("{0:### ### ##0}", objDoc.Quantity);
                    txtDocRetailAllPrice.Text = System.String.Format("{0:### ### ##0}", objDoc.SumRetail);
                    txtDocState.Text = objDoc.DocStateName;
                    txtStockDst.Text = objDoc.StockDstName;
                    txtStockSrc.Text = objDoc.StockSrcName;
                }

                this.tableLayoutPanel3.ResumeLayout(false);
            }
            catch (System.Exception f)
            {
                SendMessageToLog("Отображение свойств накладной. Текст ошибки: " + f.Message);
            }
            return;
        }
        #endregion

        #region Настройки внешнего вида журналов
        /// <summary>
        /// Считывает настройки журналов из реестра
        /// </summary>
        public void RestoreLayoutFromRegistry()
        {
            System.String strReestrPath = this.m_objProfile.GetRegKeyBase();
            strReestrPath += ("\\Tools\\");
            try
            {
                gridViewDocList.RestoreLayoutFromRegistry(String.Format("{0}{1}IntWaybill", strReestrPath, gridViewDocList.Name));
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка загрузки настроек списка накладных.\n\nТекст ошибки : " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // очищаем занимаемые ресурсы
            {
            }

            return;
        }
        /// <summary>
        /// Записывает настройки журналов в реестр
        /// </summary>
        public void SaveLayoutToRegistry()
        {
            System.String strReestrPath = this.m_objProfile.GetRegKeyBase();
            strReestrPath += ("\\Tools\\");
            try
            {
                gridViewDocList.SaveLayoutToRegistry(String.Format("{0}{1}IntWaybill", strReestrPath, gridViewDocList.Name));
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка записи настроек списка накладных.\n\nТекст ошибки : " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // очищаем занимаемые ресурсы
            {
            }

            return;
        }
        #endregion

        #region Закрытие формы
        private void frmIntWaybillList_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                SaveLayoutToRegistry();
            }
            catch (System.Exception f)
            {
                SendMessageToLog("frmIntWaybillList_FormClosed. Текст ошибки: " + f.Message);
            }
            return;
        }
        #endregion

        #region Открытие формы
        private void frmIntWaybillList_Shown(object sender, EventArgs e)
        {
            try
            {
                RestoreLayoutFromRegistry();

                LoadComboBox();

                LoadDocList();

                tabControl.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            }
            catch (System.Exception f)
            {
                SendMessageToLog("frmIntWaybillList_Shown. Текст ошибки: " + f.Message);
            }
            return;
        }
        #endregion
    }

    public class IntWaybillListEditor : PlugIn.IClassTypeView
    {
        public override void Run(UniXP.Common.MENUITEM objMenuItem, System.String strCaption)
        {
            frmIntWaybillList obj = new frmIntWaybillList(objMenuItem);
            obj.Text = strCaption;
            obj.MdiParent = objMenuItem.objProfile.m_objMDIManager.MdiParent;
            obj.Visible = true;
        }

    }

}

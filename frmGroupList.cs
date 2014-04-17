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
using OfficeOpenXml;


namespace ERPMercuryProcessingOrder
{
    public partial class frmGroupList : DevExpress.XtraEditors.XtraForm
    {
        #region Переменные, Свойства, Константы
        private UniXP.Common.CProfile m_objProfile;
        #endregion

        #region Конструктор
        public frmGroupList(UniXP.Common.CProfile objProfile)
        {
            InitializeComponent();

            m_objProfile = objProfile;

            LoadGroupTypeList();
        }
        #endregion

        #region Обновление списка групп
        /// <summary>
        /// Обновляет список типов групп
        /// </summary>
        private void LoadGroupTypeList()
        {
            try
            {
                checkListGroupType.Items.Clear();

                List<ERP_Mercury.Common.CConditionGroupType> objConditionGroupTypeList = ERP_Mercury.Common.CConditionGroupType.GetConditionGroupTypeList(m_objProfile, null);
                if (objConditionGroupTypeList != null)
                {
                    foreach (ERP_Mercury.Common.CConditionGroupType objConditionGroupType in objConditionGroupTypeList)
                    {
                        checkListGroupType.Items.Add(objConditionGroupType, CheckState.Checked);
                    }
                }
                objConditionGroupTypeList = null;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка обновления списка типов групп.\n\nТекст ошибки : " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }
            return;
        }

        /// <summary>
        /// Обновление списка групп
        /// </summary>
        /// <param name="iPos">номер строки, которую нужно выделить</param>
        private void LoadGroupsList(System.Int32 iPos)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                //treeListGroups.Enabled = false;
                treeListGroups.FocusedNodeChanged -= new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeListGroups_FocusedNodeChanged);
                this.tableLayoutPanel.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.treeListGroups)).BeginInit();


                treeListGroups.Nodes.Clear();

                // запрашиваем список групп
                //List<ERP_Mercury.Common.CConditionGroup> objGroupList = ERP_Mercury.Common.CConditionGroup.GetConditionGroupList(m_objProfile, null);
                List<ERP_Mercury.Common.CConditionGroup> objGroupList = ERP_Mercury.Common.CConditionGroup.GetConditionGroupListWithoutStructure(m_objProfile, null);

                if (objGroupList != null)
                {
                    foreach (ERP_Mercury.Common.CConditionGroup objGroup in objGroupList)
                    {
                        if (checkListGroupType.CheckedItems.Count > 0)
                        {
                            for (System.Int32 i = 0; i < checkListGroupType.Items.Count; i++)
                            {
                                if (checkListGroupType.Items[i].CheckState != CheckState.Checked) { continue; }
                                if (((ERP_Mercury.Common.CConditionGroupType)checkListGroupType.Items[i].Value).ID.CompareTo(objGroup.GroupType.ID) == 0)
                                {
                                    DevExpress.XtraTreeList.Nodes.TreeListNode objNode =
                                            treeListGroups.AppendNode(new object[] { objGroup.Name, objGroup.Description, objGroup.GroupType.Name }, null);
                                    objNode.Tag = objGroup;
                                    break;
                                }
                            }
                        }
                    }
                }
                objGroupList = null;

            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка обновления списка групп.\n\nТекст ошибки : " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                this.tableLayoutPanel.ResumeLayout(false);
                tableLayoutPanel.PerformLayout();
                ((System.ComponentModel.ISupportInitialize)(this.treeListGroups)).EndInit();

                treeListGroups.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeListGroups_FocusedNodeChanged);
                checkListGroupType.UnSelectAll();

                // выделяем запись
                if (treeListGroups.Nodes.Count > 0)
                {
                    treeListGroups.Enabled = true;
                    System.Int32 iPosNew = (treeListGroups.Nodes.Count > iPos) ? iPos : (treeListGroups.Nodes.Count - 1);
                    treeListGroups.FocusedNode = treeListGroups.Nodes[iPosNew];
                    barButtonItemDelete.Enabled = true;
                }

                this.Cursor = Cursors.Default;
            }
            return;
        }
        /// <summary>
        /// Обновление списка групп
        /// </summary>
        private void RefreshGroupsList()
        {
            try
            {
                LoadGroupsList(0);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка обновления списка групп.\n\nТекст ошибки : " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }
            return;
        }
        /// <summary>
        /// Обновление списка групп
        /// </summary>
        /// <param name="iPos">номер строки, которую нужно выделить</param>
        private void RefreshGroupsList(System.Int32 iPos)
        {
            try
            {
                LoadGroupsList(iPos);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка обновления списка группр.\n\nТекст ошибки : " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }
            return;
        }
        private void barButtonItemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                LoadGroupsList(treeListGroups.Nodes.Count);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка обработки нажатия клавиши \"Обновить\".\n\nТекст ошибки : " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }

        private void itemRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadGroupsList(treeListGroups.Nodes.Count);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка обработки пункта меню \"Обновить\".\n\nТекст ошибки : " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        private void treeListGroups_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            try
            {
                if ((e.Node != null) && (e.Node.Tag != null))
                {
                    if (barButtonItemDelete.Enabled == false) { barButtonItemDelete.Enabled = true; }
                }
                else
                {
                    barButtonItemDelete.Enabled = false;
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка смены записи.\n\nТекст ошибки : " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
            }

            return;
        }

        #endregion

        #region Всплывающее меню
        private void contextMenuStrip_Opened(object sender, EventArgs e)
        {
            try
            {
                itemAdd.Enabled = barButtonItemAdd.Enabled;
                itemRefresh.Enabled = barButtonItemRefresh.Enabled;
                itemDelete.Enabled = barButtonItemDelete.Enabled;
                itemProperties.Enabled = (treeListGroups.FocusedNode != null);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("contextMenuStrip_Opened\n" + f.Message, "Ошибка",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        #endregion

        #region Открытие формы
        private void frmGroupList_Load(object sender, EventArgs e)
        {
            try
            {
                RefreshGroupsList(0);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка загрузки формы.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        #endregion

        #region Новая группа
        /// <summary>
        /// Создание новой группы
        /// </summary>
        private void NewGroup()
        {
            try
            {
                ERP_Mercury.Common.frmGroupProperties objfrmGroupProperties = new ERP_Mercury.Common.frmGroupProperties(m_objProfile);
                objfrmGroupProperties.NewGroup();
                //objfrmGroupProperties.ShowDialog();
                DialogResult objDialogResult = objfrmGroupProperties.DialogResult;
                objfrmGroupProperties.Dispose();
                objfrmGroupProperties = null;

                if (objDialogResult != DialogResult.None)
                {
                    RefreshGroupsList();
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка создания группы.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        private void barButtonItemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                NewGroup();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка создания группы.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        private void itemAdd_Click(object sender, EventArgs e)
        {
            try
            {
                NewGroup();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка создания группы.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Свойства группы
        private void GroupProperties( ERP_Mercury.Common.CConditionGroup objGroup )
        {
            if (objGroup == null) { return; }
            try
            {
                ERP_Mercury.Common.frmGroupProperties objfrmGroupProperties = new ERP_Mercury.Common.frmGroupProperties(m_objProfile);
                objfrmGroupProperties.EditGroup( objGroup );
                DialogResult objDialogResult = objfrmGroupProperties.DialogResult;
                objfrmGroupProperties.Dispose();
                objfrmGroupProperties = null;

                if ( ( objDialogResult != DialogResult.None ) && ( objDialogResult != DialogResult.Cancel ))
                {
                    RefreshGroupsList();
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка редактирования группы.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        private void treeListGroups_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if ((treeListGroups.Nodes.Count > 0) && (treeListGroups.FocusedNode != null))
                {
                    ERP_Mercury.Common.CConditionGroup objGroup = (ERP_Mercury.Common.CConditionGroup)treeListGroups.FocusedNode.Tag;
                    if (objGroup != null)
                    {
                        GroupProperties( objGroup );
                    }
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка редактирования группы.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

        }
        private void itemProperties_Click(object sender, EventArgs e)
        {
            try
            {
                if ((treeListGroups.Nodes.Count > 0) && (treeListGroups.FocusedNode != null))
                {
                    ERP_Mercury.Common.CConditionGroup objGroup = (ERP_Mercury.Common.CConditionGroup)treeListGroups.FocusedNode.Tag;
                    if (objGroup != null)
                    {
                        GroupProperties(objGroup);
                    }
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка редактирования группы.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Удаление группы
        /// <summary>
        /// Удаление группы
        /// </summary>
        private void DeleteGroup()
        {
            try
            {
                if ((treeListGroups.Nodes.Count > 0) && (treeListGroups.FocusedNode != null))
                {
                    ERP_Mercury.Common.CConditionGroup objGroup = (ERP_Mercury.Common.CConditionGroup)treeListGroups.FocusedNode.Tag;
                    if (objGroup != null)
                    {
                        if (objGroup.DeleteGroupFromDB(m_objProfile) == true)
                        {
                            RefreshGroupsList();
                        }
                    }
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка удаления группы.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        private void barButtonItemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                DeleteGroup();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка удаления группы.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        private void itemDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteGroup();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка удаления группы.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
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
                oSheet.Name = "Список объединений";
                // Имена столбцов
                oSheet.Cells[3, 1] = "Название группы";
                oSheet.Cells[3, 2] = "Состав группы";

                //Format A1:D1 as bold, vertical alignment = center.
                oSheet.get_Range("A3", "M3").Font.Size = 12;
                oSheet.get_Range("A3", "M3").Font.Bold = true;
                oSheet.get_Range("A3", "M3").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                // пробегаем по дереву и сохраняем расшифровки
                System.Int32 iLastIndxRowForPrint = 4;
                ERP_Mercury.Common.CConditionGroup objGroup = null;

                foreach (DevExpress.XtraTreeList.Nodes.TreeListNode objNode in treeListGroups.Nodes)
                {
                    if (objNode.Tag == null) { continue; }
                    objGroup = (ERP_Mercury.Common.CConditionGroup)objNode.Tag;
                    oSheet.Cells[iLastIndxRowForPrint, 1] = objGroup.Name;
                    oSheet.get_Range(oSheet.Cells[iLastIndxRowForPrint, 1], oSheet.Cells[iLastIndxRowForPrint, 1]).Font.Bold = true;
                    oSheet.get_Range(oSheet.Cells[iLastIndxRowForPrint, 1], oSheet.Cells[iLastIndxRowForPrint, 1]).Font.Size = 14;
                    foreach (ERP_Mercury.Common.CConditionObject objItem in objGroup.PermitObjectList)
                    {
                        oSheet.Cells[iLastIndxRowForPrint, 2] = objItem.Name;
                        iLastIndxRowForPrint++;
                    }
                }
                oSheet.get_Range("A3", "M3").EntireColumn.AutoFit();


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
        private void barBtnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (treeListGroups.Nodes.Count > 0)
                {
                    //ExportToExcel();
                    ExportToExcel2();
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

        private void ExportToExcel2()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                System.String strTmpPath = System.IO.Path.GetTempPath();
                System.String strFileName = (strTmpPath + "Список групп.xlsx");


                System.IO.FileInfo newFile = new System.IO.FileInfo(strFileName);
                if (newFile.Exists)
                {
                    newFile.Delete();  // ensures we create a new workbook
                    newFile = new System.IO.FileInfo(strFileName);
                }
                using (ExcelPackage package = new ExcelPackage(newFile))
                {

                    //Add table headers going cell by cell.
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Список объединений");
                    if( worksheet != null )
                    {
                        // Имена столбцов
                        worksheet.Cells[3, 1].Value = "Название группы";
                        worksheet.Cells[3, 2].Value = "Состав группы";

                        using (var range = worksheet.Cells[1, 3, 13, 3])
                        {
                            range.Style.Font.Bold = true;
                            range.Style.Font.Size = 12;
                            range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        }

                        // пробегаем по дереву и сохраняем расшифровки
                        System.Int32 iLastIndxRowForPrint = 4;
                        ERP_Mercury.Common.CConditionGroup objGroup = null;

                        foreach (DevExpress.XtraTreeList.Nodes.TreeListNode objNode in treeListGroups.Nodes)
                        {
                            if (objNode.Tag == null) { continue; }
                            objGroup = (ERP_Mercury.Common.CConditionGroup)objNode.Tag;
                            worksheet.Cells[iLastIndxRowForPrint, 1].Value = objGroup.Name;
                            worksheet.Cells[iLastIndxRowForPrint, 1].Style.Font.Bold = true;
                            worksheet.Cells[iLastIndxRowForPrint, 1].Style.Font.Size = 14;

                            if( ( objGroup.PermitObjectList == null ) || ( objGroup.PermitObjectList.Count == 0 ) )
                            {
                                objGroup.LoadGroupStructure(m_objProfile, null);
                            }

                            if ((objGroup.PermitObjectList != null) || (objGroup.PermitObjectList.Count > 0))
                            {
                                foreach (ERP_Mercury.Common.CConditionObject objItem in objGroup.PermitObjectList)
                                {
                                    worksheet.Cells[iLastIndxRowForPrint, 2].Value = objItem.Name;
                                    iLastIndxRowForPrint++;
                                }
                            }

                        }

                        worksheet.Cells["A1:Z1"].AutoFitColumns();
                        worksheet = null;

                        package.Save();

                        try
                        {
                            using (System.Diagnostics.Process process = new System.Diagnostics.Process())
                            {
                                process.StartInfo.FileName = strFileName;
                                process.StartInfo.Verb = "Open";
                                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                process.Start();
                            }
                        }
                        catch
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(this, "Cannot find an application on your system suitable for openning the file with exported data.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    
                    }

                    
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
    }

    public class ViewConditionGroup : PlugIn.IClassTypeView
    {
        public override void Run(UniXP.Common.MENUITEM objMenuItem, System.String strCaption)
        {
            frmGroupList obj = new frmGroupList(objMenuItem.objProfile);
            obj.Text = strCaption;
            obj.MdiParent = objMenuItem.objProfile.m_objMDIManager.MdiParent;
            obj.Visible = true;
        }
    }

}

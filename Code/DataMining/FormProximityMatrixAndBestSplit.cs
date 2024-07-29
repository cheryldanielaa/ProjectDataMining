using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataMining
{
    public partial class FormProximityMatrixAndBestSplit : Form
    {
        DataTableCollection tableCollection;
        DataTable dt;
        public string mode;
        public string parameterProximity;

        public FormProximityMatrixAndBestSplit()
        {
            InitializeComponent();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openExcel = new OpenFileDialog()
            {
                Filter =
                   "Excel Workbook|*.xls|Excel Workbook|*.xlsx"
            })
            {
                if (openExcel.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        textBoxPath.Text = openExcel.FileName;
                        using (var stream = File.Open(openExcel.FileName, FileMode.Open, FileAccess.Read))
                        {
                            using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                            {
                                DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                                {
                                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                                });
                                tableCollection = result.Tables;
                                cboSheet.Items.Clear();
                                foreach (DataTable table in tableCollection)
                                {
                                    cboSheet.Items.Add(table.TableName); //add sheet to combobox
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Please choose .xls or .xlsx file only.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); //custom messageBox to show error
                }
            }
        }
       
        private void cboSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            dt = tableCollection[cboSheet.SelectedItem.ToString()];
            dataGridViewSheets.DataSource = dt;
            numericUpDownIndexClass.Maximum = dataGridViewSheets.ColumnCount;
            numericUpDownIndexClass.Value = numericUpDownIndexClass.Maximum;
        }

        private void rboSelProx_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxProxMatrix.Enabled = true;
            groupBoxBestSplit.Enabled = false;
            listBoxInfo.Visible = false;
            mode = "Proximity";
            dataGridViewResult.Visible = true;
            dataGridViewResult.BringToFront();
        }

        private void rboSelBestSplit_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxProxMatrix.Enabled = false;
            groupBoxBestSplit.Enabled = true;
            listBoxInfo.Visible = true;
            mode = "Best Split";
            dataGridViewResult.Visible = false;
            listBoxInfo.BringToFront();
        }

        private void FormProject1_Load(object sender, EventArgs e)
        {
            rboSelProx.Checked = true;
            rboEucledianDistance.Checked = true;
        }

        private void rboEucledianDistance_CheckedChanged(object sender, EventArgs e)
        {
            parameterProximity = "2";
        }

        private void rboSupremum_CheckedChanged(object sender, EventArgs e)
        {
            parameterProximity = "infinite";
        }

        private void rboManhattan_CheckedChanged(object sender, EventArgs e)
        {
            parameterProximity = "1";
        }


        private void buttonProxMatrix_Click(object sender, EventArgs e)
        {
            try
            {
                if (mode == "Proximity")
                {
                    //set up datagridviewresult
                    dataGridViewResult.Columns.Clear();
                    dataGridViewResult.Rows.Clear();
                    dataGridViewResult.Columns.Add("r = " + parameterProximity, "r = " + parameterProximity);

                    for (int i = 0; i < dataGridViewSheets.RowCount - 1; i++)
                    {
                        dataGridViewResult.Columns.Add(dataGridViewSheets.Rows[i].Cells[0].ToString(), dataGridViewSheets.Rows[i].Cells[0].Value.ToString());
                    }
                    if (parameterProximity != "infinite")
                    {
                        double r = double.Parse(parameterProximity);
                        int numAttribute = dataGridViewSheets.Columns.Count;
                        for (int j = 0; j < dataGridViewSheets.RowCount - 1; j++)
                        {
                            dataGridViewResult.Rows.Add();
                            dataGridViewResult.Rows[j].Cells[0].Value = dataGridViewSheets.Rows[j].Cells[0].Value.ToString();
                            for (int i = 0; i < dataGridViewSheets.RowCount - 1; i++)
                            {

                                double total = 0;
                                for (int n = 1; n < numAttribute; n++)
                                {
                                    double v1 = double.Parse(dataGridViewSheets.Rows[i].Cells[n].Value.ToString());
                                    double v2 = double.Parse(dataGridViewSheets.Rows[j].Cells[n].Value.ToString());
                                    total += Math.Pow(Math.Abs(v1 - v2), r);
                                }
                                total = Math.Pow(total, 1 / r);
                                dataGridViewResult.Rows[j].Cells[i + 1].Value = Math.Round(total, 4);
                            }
                        }
                    }
                    else
                    {
                        int numAttribute = dataGridViewSheets.Columns.Count;
                        List<double> listData;
                        for (int j = 0; j < dataGridViewSheets.RowCount - 1; j++)
                        {
                            dataGridViewResult.Rows.Add();
                            dataGridViewResult.Rows[j].Cells[0].Value = dataGridViewSheets.Rows[j].Cells[0].Value.ToString();
                            for (int i = 0; i < dataGridViewSheets.RowCount - 1; i++)
                            {
                                listData = new List<double>();
                                for (int n = 1; n < numAttribute; n++)
                                {
                                    double v1 = double.Parse(dataGridViewSheets.Rows[i].Cells[n].Value.ToString());
                                    double v2 = double.Parse(dataGridViewSheets.Rows[j].Cells[n].Value.ToString());
                                    double max = Math.Abs(v1 - v2);
                                    listData.Add(max);
                                }
                                dataGridViewResult.Rows[j].Cells[i + 1].Value = listData.Max();
                            }
                        }
                    }
                    btnExport.Enabled = true;
                    btnExport.Visible = true;
                    buttonExportLB.Enabled = false;
                    buttonExportLB.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBestSplit_Click(object sender, EventArgs e)
        {
            try
            {
                if (mode == "Best Split")
                {
                    listBoxInfo.Items.Clear();

                    //cari gini parent
                    List<object> listClass = new List<object>();
                    for (int i = 0; i < dataGridViewSheets.RowCount - 1; i++)
                    {
                        string value = dataGridViewSheets.Rows[i].Cells[(int)numericUpDownIndexClass.Value - 1].Value.ToString();
                        if (!(listClass.Contains(value)))
                        {
                            listClass.Add(value);
                            listClass.Add(1);
                        }
                        else
                        {
                            listClass[listClass.IndexOf(value) + 1] = int.Parse(listClass[listClass.IndexOf(value) + 1].ToString()) + 1;
                        }
                    }
                    int totalData = dataGridViewSheets.RowCount - 1;
                    double parentGINI = 1;

                    // hitung gini parent
                    foreach (object o in listClass)
                    {
                        if (double.TryParse(o.ToString(), out double result))
                        {
                            parentGINI -= Math.Pow((result / totalData), 2);
                        }
                    }
                    listBoxInfo.Items.Add("GINI(Parent) = " + parentGINI);
                    listBoxInfo.Items.Add("");

                    //cari masing masing gini untuk tiap attribut
                    List<string> listAttributeName = new List<string>();
                    List<double> listGINI = new List<double>();
                    for (int i = 1; i < dataGridViewSheets.ColumnCount - 1; i++)
                    {
                        string attributeName = dataGridViewSheets.Columns[i].HeaderText;
                        listAttributeName.Add(attributeName);
                        string data, dataClass, dataType = "";
                        List<object> listAttribute = new List<object>();
                        List<object> listClassContinous = new List<object>();//untuk continous
                        List<object> listCountClass = new List<object>();
                        List<object> listMinClass = new List<object>();
                        for (int j = 0; j < dataGridViewSheets.RowCount - 1; j++)
                        {
                            data = dataGridViewSheets.Rows[j].Cells[i].Value.ToString();
                            dataClass = dataGridViewSheets.Rows[j].Cells[(int)numericUpDownIndexClass.Value - 1].Value.ToString();

                            if (double.TryParse(data, out double result))//attribute continous
                            {
                                dataType = "Continous";
                                //tambah data ke list
                                listAttribute.Add(result);
                                listClassContinous.Add(dataClass);
                                if (!(listCountClass.Contains(dataClass)))
                                {
                                    listCountClass.Add(dataClass);
                                    listCountClass.Add(1);

                                    listMinClass.Add(dataClass);
                                    listMinClass.Add(0);
                                }
                                else
                                {
                                    int index = listCountClass.IndexOf(dataClass);
                                    listCountClass[index + 1] = int.Parse(listCountClass[index + 1].ToString()) + 1;
                                }
                            }
                            else //untuk attributes selain continous (binary atau multiple)
                            {
                                dataType = "Bin/Mul";
                                int index = 0;
                                if (!(listAttribute.Contains(data)))
                                {
                                    listAttribute.Add(data);
                                    listAttribute.Add(1);

                                    List<object> listClassAttribute = new List<object>();
                                    listClassAttribute.Add(dataClass);
                                    listClassAttribute.Add(1);
                                    listAttribute.Add(listClassAttribute);
                                }
                                else
                                {
                                    index = listAttribute.IndexOf(data);
                                    listAttribute[index + 1] = int.Parse(listAttribute[index + 1].ToString()) + 1;

                                    if (!((listAttribute[index + 2] as List<object>).Contains(dataClass)))
                                    {
                                        (listAttribute[index + 2] as List<object>).Add(dataClass);
                                        (listAttribute[index + 2] as List<object>).Add(1);
                                    }
                                    else
                                    {
                                        int indexClass = (listAttribute[index + 2] as List<object>).IndexOf(dataClass);
                                        (listAttribute[index + 2] as List<object>)[indexClass + 1] = int.Parse((listAttribute[index + 2] as List<object>)[indexClass + 1].ToString()) + 1;
                                    }
                                }
                            }
                        }
                        if (dataType == "Continous")
                        {
                            //sort list 
                            for (int awal = 0, akhir = listAttribute.Count - 1; awal < akhir; awal++, akhir--)
                            {
                                double minDat = double.Parse(listAttribute[awal].ToString());
                                double maxDat = double.Parse(listAttribute[awal].ToString());

                                int minIndex = awal;
                                int maxIndex = akhir;
                                for (int count = awal; count <= akhir; count++)
                                {
                                    double dat = double.Parse(listAttribute[count].ToString());
                                    if (minDat > dat)
                                    {
                                        minDat = dat;
                                        minIndex = count;
                                    }
                                    else if (maxDat < dat)
                                    {
                                        maxDat = dat;
                                        maxIndex = count;
                                    }
                                }

                                //swap min
                                var tempMin = listAttribute[awal];
                                listAttribute[awal] = listAttribute[minIndex];
                                listAttribute[minIndex] = tempMin;

                                //swap data Class
                                var tempClass = listClassContinous[awal];
                                listClassContinous[awal] = listClassContinous[minIndex];
                                listClassContinous[minIndex] = tempClass;

                                if (double.Parse(listAttribute[minIndex].ToString()) == maxDat)
                                {
                                    //swap max kalau sama
                                    var tempMax = listAttribute[akhir];
                                    listAttribute[akhir] = listAttribute[minIndex];
                                    listAttribute[minIndex] = tempMax;

                                    //swap data class
                                    var tempClassM = listClassContinous[akhir];
                                    listClassContinous[akhir] = listClassContinous[minIndex];
                                    listClassContinous[minIndex] = tempClassM;
                                }
                                else
                                {
                                    //swap max kalau berbeda
                                    var tempMax = listAttribute[akhir];
                                    listAttribute[akhir] = listAttribute[maxIndex];
                                    listAttribute[maxIndex] = tempMax;

                                    //swap data class
                                    var tempClassM = listClassContinous[akhir];
                                    listClassContinous[akhir] = listClassContinous[maxIndex];
                                    listClassContinous[maxIndex] = tempClassM;
                                }
                            }

                            //setelah sort, hitung gini per data di listAttribute
                            List<double> listSplitPos = new List<double>();
                            for (int k = 0; k <= listAttribute.Count; k++)
                            {
                                if (k == 0)//di awal list
                                {
                                    listSplitPos.Add(double.Parse(listAttribute[k].ToString()) - 5);
                                }
                                else if (k == listAttribute.Count)
                                {
                                    listSplitPos.Add(double.Parse(listAttribute[k - 1].ToString()) + 5);
                                }
                                else
                                {
                                    double avg = (double.Parse(listAttribute[k].ToString()) + double.Parse(listAttribute[k - 1].ToString())) / 2;
                                    listSplitPos.Add(avg);
                                }
                            }
                            int totalClassAttribute = listClassContinous.Count;
                            List<double> listContinousGINI = new List<double>();
                            double dataMin = 0, dataMax = totalClassAttribute;
                            for (int count = 0; count < listSplitPos.Count; count++)
                            {
                                //cari gini masing-masing attribut
                                double attributeGINIMin = 1, attributeGINIMax = 1;//gini min untuk data < ;gini max untuk data >=
                                double attributeWGINI = 0;
                                for (int k = 1; k < listCountClass.Count; k += 2)
                                {
                                    if (count == 0)
                                    {
                                        attributeGINIMin = 0;
                                        attributeGINIMax = attributeGINIMax - Math.Pow(double.Parse(listCountClass[k].ToString()) / dataMax, 2);
                                    }
                                    else if (count == listSplitPos.Count - 1)
                                    {
                                        attributeGINIMin = attributeGINIMin - Math.Pow(double.Parse(listMinClass[k].ToString()) / dataMin, 2);
                                        attributeGINIMax = 0;
                                    }
                                    else
                                    {
                                        attributeGINIMin = attributeGINIMin - Math.Pow(double.Parse(listMinClass[k].ToString()) / dataMin, 2);
                                        attributeGINIMax = attributeGINIMax - Math.Pow(double.Parse(listCountClass[k].ToString()) / dataMax, 2);
                                    }

                                }

                                if (count != listClassContinous.Count)
                                {
                                    int indexSwitch = listCountClass.IndexOf(listClassContinous[count]);
                                    listCountClass[indexSwitch + 1] = int.Parse(listCountClass[indexSwitch + 1].ToString()) - 1;
                                    listMinClass[indexSwitch + 1] = int.Parse(listMinClass[indexSwitch + 1].ToString()) + 1;
                                }

                                attributeWGINI = (dataMin / totalClassAttribute * attributeGINIMin) + (dataMax / totalClassAttribute * attributeGINIMax);
                                listContinousGINI.Add(attributeWGINI);
                                dataMin++;
                                dataMax--;
                            }
                            double bestGINIContinous = listContinousGINI.Min();
                            listBoxInfo.Items.Add("GINI(" + attributeName + " : " + Math.Floor(listSplitPos[listContinousGINI.IndexOf(bestGINIContinous)]) + ") = " + bestGINIContinous);
                            listGINI.Add(bestGINIContinous);
                        }
                        else if (dataType == "Bin/Mul")
                        {
                            double attributeWGINI = 0;
                            for (int count = 1; count < listAttribute.Count; count += 3)
                            {
                                //cari gini masing-masing attribut
                                double attributeGINI = 1;
                                double totalClassAttribute = double.Parse(listAttribute[count].ToString());
                                string dataAttName = listAttribute[count - 1].ToString();
                                List<object> listCA = (listAttribute[count + 1] as List<object>);
                                for (int k = 1; k < (listAttribute[count + 1] as List<object>).Count; k += 2)
                                {
                                    attributeGINI = attributeGINI - Math.Pow(double.Parse(listCA[k].ToString()) / totalClassAttribute, 2);
                                }
                                listBoxInfo.Items.Add("GINI(" + dataAttName + ") = " + attributeGINI);
                                attributeWGINI += totalClassAttribute / totalData * attributeGINI;
                            }
                            listBoxInfo.Items.Add("Weighted GINI(" + attributeName + ") = " + attributeWGINI);
                            listBoxInfo.Items.Add("");
                            listGINI.Add(attributeWGINI);
                        }
                    }
                    double bestAttributeGINI = listGINI.Min();
                    string bestAttribute = "";
                    listBoxInfo.Items.Add("");
                    foreach (double g in listGINI)
                    {
                        if (Math.Round(g, 5) == Math.Round(bestAttributeGINI, 5))
                        {
                            bestAttribute = listAttributeName[listGINI.IndexOf(g)];
                            listBoxInfo.Items.Add("Best Split : " + bestAttribute + " with GINI(" + bestAttribute + ") = " + bestAttributeGINI);
                        }
                    }
                    double gain = parentGINI - bestAttributeGINI;
                    listBoxInfo.Items.Add("");
                    listBoxInfo.Items.Add("GAIN = " + gain);
                    btnExport.Enabled = false;
                    btnExport.Visible = false;
                    buttonExportLB.Enabled = true;
                    buttonExportLB.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            dataGridViewResult.SelectAll();
            DataObject copydata = dataGridViewResult.GetClipboardContent();
            if (copydata != null) Clipboard.SetDataObject(copydata);
            Microsoft.Office.Interop.Excel.Application xlapp = new Microsoft.Office.Interop.Excel.Application();
            xlapp.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook xlWbook;
            Microsoft.Office.Interop.Excel.Worksheet xlsheet;
            object miseddata = System.Reflection.Missing.Value;
            xlWbook = xlapp.Workbooks.Add(miseddata);

            xlsheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWbook.Worksheets.get_Item(1);
            Microsoft.Office.Interop.Excel.Range xlr = (Microsoft.Office.Interop.Excel.Range)xlsheet.Cells[1, 1];
            xlr.Select();

            xlsheet.PasteSpecial(xlr, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
        }

        private void buttonExportLB_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(dialog.FileName);
                for (int i = 0; i < listBoxInfo.Items.Count; i++)
                {
                    writer.WriteLine((string)listBoxInfo.Items[i]);
                }
                writer.Close();
            }
        }
    }
}

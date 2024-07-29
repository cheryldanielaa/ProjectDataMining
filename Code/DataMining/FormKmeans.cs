using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sedih
{
    public partial class FormKmeans : Form
    {
        int k; //variabel utk menyimpan bnyk cluster yg diinginkan
        //set k max 4;

        //variabel utk menyimpan ata cluster1, max 4 variabel
        double centera1;
        double centerb1;
        double centerc1;
        double centerd1;
        List<double> centroidA1 = new List<double>();
        List<double> centroidB1 = new List<double>();
        List<double> centroidC1 = new List<double>();
        List<double> centroidD1 = new List<double>();

        //variabel utk menyimpan data cluster2
        double centera2;
        double centerb2;
        double centerc2;
        double centerd2;
        List<double> centroidA2 = new List<double>();
        List<double> centroidB2 = new List<double>();
        List<double> centroidC2 = new List<double>();
        List<double> centroidD2 = new List<double>();

        //variabel utk menyimpan data cluster3
        double centera3;
        double centerb3;
        double centerc3;
        double centerd3;
        List<double> centroidA3 = new List<double>();
        List<double> centroidB3 = new List<double>();
        List<double> centroidC3 = new List<double>();
        List<double> centroidD3 = new List<double>();

        //variabel utk menyimpan data cluster4
        double centera4;
        double centerb4;
        double centerc4;
        double centerd4;
        List<double> centroidA4 = new List<double>();
        List<double> centroidB4 = new List<double>();
        List<double> centroidC4 = new List<double>();
        List<double> centroidD4 = new List<double>();

        //variabel utk menyimpan centroid yg baru didapat dr perhitungan
        double centroidBaruA1 = 0;
        double centroidBaruA2 = 0;
        double centroidBaruA3 = 0;
        double centroidBaruA4 = 0;
        double centroidBaruB1 = 0;
        double centroidBaruB2 = 0;
        double centroidBaruB3 = 0;
        double centroidBaruB4 = 0;
        double centroidBaruC1 = 0;
        double centroidBaruC2 = 0;
        double centroidBaruC3 = 0;
        double centroidBaruC4 = 0;
        double centroidBaruD1 = 0;
        double centroidBaruD2 = 0;
        double centroidBaruD3 = 0;
        double centroidBaruD4 = 0;

        int count = 0;
        bool repeat = true;
        int digitAfterDot;
        bool runOnce = false;
        public FormKmeans()
        {
            InitializeComponent();
        }
        public DataTable ReadCSV(string fileName)
        {
            DataTable dt = new DataTable();
            using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\"" +
                Path.GetDirectoryName(fileName) + "\";Extended Properties='text;HDR=yes;FMT=Delimited(,)';"))
            {
                using (OleDbCommand cmd = new OleDbCommand(string.Format("select *from [{0}]", new FileInfo(fileName).Name), cn))
                {
                    cn.Open();
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //buka file (.csv)
                using (OpenFileDialog openFile = new OpenFileDialog() { Filter = "CSV|*.csv", ValidateNames = true })
                {
                    if (openFile.ShowDialog() == DialogResult.OK)
                    {
                        dgvInput.DataSource = ReadCSV(openFile.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informasi");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //setting numeric updown value between 2-4
            numericUpDownK.Minimum = 2;
            numericUpDownK.Maximum = 4;
        }

        private double JarakKeCenter(double centerA, double centerB, double centerC,
            double centerD,
            double pointA, double pointB,
            double pointC, double pointD)
        {
            var result = Math.Sqrt(Math.Pow(centerA - pointA, 2) + Math.Pow(centerB - pointB, 2) +
                Math.Pow(centerC - pointC, 2) + Math.Pow(centerD - pointD, 2));
            return result;
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            k = (int)numericUpDownK.Value;
            digitAfterDot = (int)numericUpDownDigit.Value;

            //sistem cmn bisa jalan bwt yg gk pny no di kolom plg ujung
            //sementara cmn bs yg 4 variabel
            centera1 = double.Parse(dgvInput.Rows[0].Cells[0].Value.ToString());
            centerb1 = double.Parse(dgvInput.Rows[0].Cells[1].Value.ToString());
            centerc1 = double.Parse(dgvInput.Rows[0].Cells[2].Value.ToString());
            centerd1 = double.Parse(dgvInput.Rows[0].Cells[3].Value.ToString());

            centera2 = double.Parse(dgvInput.Rows[1].Cells[0].Value.ToString());
            centerb2 = double.Parse(dgvInput.Rows[1].Cells[1].Value.ToString());
            centerc2 = double.Parse(dgvInput.Rows[1].Cells[2].Value.ToString());
            centerd2 = double.Parse(dgvInput.Rows[1].Cells[3].Value.ToString());

            centera3 = double.Parse(dgvInput.Rows[2].Cells[0].Value.ToString());
            centerb3 = double.Parse(dgvInput.Rows[2].Cells[1].Value.ToString());
            centerc3 = double.Parse(dgvInput.Rows[2].Cells[2].Value.ToString());
            centerd3 = double.Parse(dgvInput.Rows[2].Cells[3].Value.ToString());

            centera4 = double.Parse(dgvInput.Rows[3].Cells[0].Value.ToString());
            centerb4 = double.Parse(dgvInput.Rows[3].Cells[1].Value.ToString());
            centerc4 = double.Parse(dgvInput.Rows[3].Cells[2].Value.ToString());
            centerd4 = double.Parse(dgvInput.Rows[3].Cells[3].Value.ToString());

            int rowLength = dgvInput.RowCount - 1;//panjang baris tanpa angka diujung
            dgvInput.Columns.Add("ColumnName", "Cluster");

            if (k == 2)
            {
                while (repeat == true)
                {
                    for (int i = 0; i < rowLength; i++)
                    {
                        double pointA = double.Parse(dgvInput.Rows[i].Cells[0].Value.ToString());
                        double pointB = double.Parse(dgvInput.Rows[i].Cells[1].Value.ToString());
                        double pointC = double.Parse(dgvInput.Rows[i].Cells[2].Value.ToString());
                        double pointD = double.Parse(dgvInput.Rows[i].Cells[3].Value.ToString());

                        double jarakKeCentroidCluster1 = JarakKeCenter(centera1, centerb1, centerc1,
                            centerd1, pointA, pointB, pointC, pointD);
                        double jarakKeCentroidCluster2 = JarakKeCenter(centera2, centerb2, centerc2,
                            centerd2, pointA, pointB, pointC, pointD);

                        //jika jarak ke centro1 lbh kecil drpd jarak ke centro2, maka msk cluster1
                        if (jarakKeCentroidCluster1 < jarakKeCentroidCluster2)
                        {
                            centroidA1.Add(pointA);
                            centroidB1.Add(pointB);
                            centroidC1.Add(pointC);
                            centroidD1.Add(pointD);
                            dgvInput.Rows[i].Cells[5].Value = "Cluster 1";

                        }
                        else 
                        {
                            centroidA2.Add(pointA);
                            centroidB2.Add(pointB);
                            centroidC2.Add(pointC);
                            centroidD2.Add(pointD);
                            dgvInput.Rows[i].Cells[5].Value = "Cluster 2";
                        }  
                    }
                    for (int i = 0; i < centroidA1.Count; i++)
                    {
                        centroidBaruA1 += centroidA1[i]; //hitung jmlh data yg msk dlm cluster1
                    }
                    for (int i = 0; i < centroidB1.Count; i++)
                    {
                        centroidBaruB1 += centroidB1[i]; //hitung jmlh data yg msk dlm cluster1
                    }
                    for (int i = 0; i < centroidC1.Count; i++)
                    {
                        centroidBaruC1 += centroidC1[i]; //hitung jmlh data yg msk dlm cluster1
                    }
                    for (int i = 0; i < centroidD1.Count; i++)
                    {
                        centroidBaruD1 += centroidD1[i]; //hitung jmlh data yg msk dlm cluster2
                    }

                    for (int i = 0; i < centroidA2.Count; i++)
                    {
                        centroidBaruA2 += centroidA2[i]; //hitung jmlh data yg msk dlm cluster2
                    }
                    for (int i = 0; i < centroidB2.Count; i++)
                    {
                        centroidBaruB2 += centroidB2[i]; //hitung jmlh data yg msk dlm cluster2
                    }
                    for (int i = 0; i < centroidC2.Count; i++)
                    {
                        centroidBaruC2 += centroidC2[i]; //hitung jmlh data yg msk dlm cluster2
                    }
                    for (int i = 0; i < centroidD2.Count; i++)
                    {
                        centroidBaruD2 += centroidD2[i]; //hitung jmlh data yg msk dlm cluster2
                    }

                    centroidBaruA1 = centroidBaruA1 / centroidA1.Count; //posisi centroid A1 saat ini diperoleh dr jmlh data total di list centroA1/jmlh data yg ada\
                    centroidBaruB1 = centroidBaruB1 / centroidB1.Count;
                    centroidBaruC1 = centroidBaruC1 / centroidC1.Count;
                    centroidBaruD1 = centroidBaruD1 / centroidD1.Count;

                    centroidBaruA2 = centroidBaruA2 / centroidA2.Count;
                    centroidBaruB2 = centroidBaruB2 / centroidB2.Count;
                    centroidBaruC2 = centroidBaruC2 / centroidC2.Count;
                    centroidBaruD2 = centroidBaruD2 / centroidD2.Count;

                    centera1 = Math.Round(centera1, digitAfterDot);
                    centerb1 = Math.Round(centerb1, digitAfterDot);
                    centerc1 = Math.Round(centerc1, digitAfterDot);
                    centerd1 = Math.Round(centerd1, digitAfterDot);

                    centera2 = Math.Round(centera2, digitAfterDot);
                    centerb2 = Math.Round(centerb2, digitAfterDot);
                    centerc2 = Math.Round(centerc2, digitAfterDot);
                    centerd2 = Math.Round(centerd2, digitAfterDot);

                    centroidBaruA1 = Math.Round(centroidBaruA1, digitAfterDot);
                    centroidBaruB1 = Math.Round(centroidBaruB1, digitAfterDot);
                    centroidBaruC1 = Math.Round(centroidBaruC1, digitAfterDot);
                    centroidBaruD1 = Math.Round(centroidBaruD1, digitAfterDot);

                    centroidBaruA2 = Math.Round(centroidBaruA2, digitAfterDot);
                    centroidBaruB2 = Math.Round(centroidBaruB2, digitAfterDot);
                    centroidBaruC2 = Math.Round(centroidBaruC2, digitAfterDot);
                    centroidBaruD2 = Math.Round(centroidBaruD2, digitAfterDot);

                    if (centera1 == centroidBaruA1 && centerb1 == centroidBaruB1 &&
                        centerc1 == centroidBaruC1 && centerd1 == centroidBaruD1 &&
                        centera2 == centroidBaruA2 && centerb2 == centroidBaruB2 &&
                        centerc2 == centroidBaruC2 && centerd2 == centroidBaruD2 
                        || count == 2000000)
                    {
                        if (count == 2000000)
                        {
                            MessageBox.Show("Program berhenti karena algoritma sudah berulang sebanyak 2.000.000x", "Perhatian!");
                        }
                        if (runOnce == false)
                        {
                            dataGridViewResult.Columns.Add("ColumnName", " ");
                            dataGridViewResult.Columns.Add("ColumnName", dgvInput.Columns[0].HeaderText);
                            dataGridViewResult.Columns.Add("ColumnName", dgvInput.Columns[1].HeaderText);
                            dataGridViewResult.Columns.Add("ColumnName", dgvInput.Columns[2].HeaderText);
                            dataGridViewResult.Columns.Add("ColumnName", dgvInput.Columns[3].HeaderText);
                            runOnce = true;
                        }

                        for (int i = 0; i < k; i++)
                        {
                            dataGridViewResult.Rows.Add();
                            dataGridViewResult.Rows[i].Cells[0].Value = "Cluster Center" + " " + (i + 1);
                        }
                        dataGridViewResult.Rows[0].Cells[1].Value = centroidBaruA1;
                        dataGridViewResult.Rows[0].Cells[2].Value = centroidBaruB1;
                        dataGridViewResult.Rows[0].Cells[3].Value = centroidBaruC1;
                        dataGridViewResult.Rows[0].Cells[4].Value = centroidBaruD1;

                        dataGridViewResult.Rows[1].Cells[1].Value = centroidBaruA2;
                        dataGridViewResult.Rows[1].Cells[2].Value = centroidBaruB2;
                        dataGridViewResult.Rows[1].Cells[3].Value = centroidBaruC2;
                        dataGridViewResult.Rows[1].Cells[4].Value = centroidBaruD2;


                        repeat = false;
                    }
                    else
                    {
                        repeat = true;
                        count += 1;
                    }
                    centera1 = centroidBaruA1;
                    centerb1 = centroidBaruB1;
                    centerc1 = centroidBaruC1;
                    centerd1 = centroidBaruD1;

                    centera2 = centroidBaruA2;
                    centerb2 = centroidBaruB2;
                    centerc2 = centroidBaruC2;
                    centerd2 = centroidBaruD2;
                }
            }
            else if (k == 3)
            {
                while (repeat == true)
                {
                    for (int i = 0; i < rowLength; i++)
                    {
                        double pointA = double.Parse(dgvInput.Rows[i].Cells[0].Value.ToString());
                        double pointB = double.Parse(dgvInput.Rows[i].Cells[1].Value.ToString());
                        double pointC = double.Parse(dgvInput.Rows[i].Cells[2].Value.ToString());
                        double pointD = double.Parse(dgvInput.Rows[i].Cells[3].Value.ToString());

                        double jarakKeCentroidCluster1 = JarakKeCenter(centera1, centerb1, centerc1,
                            centerd1, pointA, pointB, pointC, pointD);
                        double jarakKeCentroidCluster2 = JarakKeCenter(centera2, centerb2, centerc2,
                            centerd2, pointA, pointB, pointC, pointD);
                        double jarakKeCentroidCluster3 = JarakKeCenter(centera3, centerb3, centerc3,
                            centerd3, pointA, pointB, pointC, pointD);

                        //jika jarak ke centro1 lbh kecil drpd jarak ke centro2, maka msk cluster1
                        if (jarakKeCentroidCluster1 < jarakKeCentroidCluster2 &&
                            jarakKeCentroidCluster1 < jarakKeCentroidCluster3)
                        {
                            centroidA1.Add(pointA);
                            centroidB1.Add(pointB);
                            centroidC1.Add(pointC);
                            centroidD1.Add(pointD);
                            dgvInput.Rows[i].Cells[5].Value = "Cluster 1";

                        }
                        else if (jarakKeCentroidCluster1 > jarakKeCentroidCluster2 &&
                            jarakKeCentroidCluster2 < jarakKeCentroidCluster3)
                        {
                            centroidA2.Add(pointA);
                            centroidB2.Add(pointB);
                            centroidC2.Add(pointC);
                            centroidD2.Add(pointD);
                            dgvInput.Rows[i].Cells[5].Value = "Cluster 2";
                        }
                        else if (jarakKeCentroidCluster1 > jarakKeCentroidCluster3 &&
                            jarakKeCentroidCluster2 > jarakKeCentroidCluster3)
                        {
                            centroidA3.Add(pointA);
                            centroidB3.Add(pointB);
                            centroidC3.Add(pointC);
                            centroidD3.Add(pointD);
                            dgvInput.Rows[i].Cells[5].Value = "Cluster 3";
                        }
                    }
                    for (int i = 0; i < centroidA1.Count; i++)
                    {
                        centroidBaruA1 += centroidA1[i]; //hitung jmlh data yg msk dlm cluster1
                    }
                    for (int i = 0; i < centroidB1.Count; i++)
                    {
                        centroidBaruB1 += centroidB1[i]; //hitung jmlh data yg msk dlm cluster1
                    }
                    for (int i = 0; i < centroidC1.Count; i++)
                    {
                        centroidBaruC1 += centroidC1[i]; //hitung jmlh data yg msk dlm cluster1
                    }
                    for (int i = 0; i < centroidD1.Count; i++)
                    {
                        centroidBaruD1 += centroidD1[i]; //hitung jmlh data yg msk dlm cluster2
                    }

                    for (int i = 0; i < centroidA2.Count; i++)
                    {
                        centroidBaruA2 += centroidA2[i]; //hitung jmlh data yg msk dlm cluster2
                    }
                    for (int i = 0; i < centroidB2.Count; i++)
                    {
                        centroidBaruB2 += centroidB2[i]; //hitung jmlh data yg msk dlm cluster2
                    }
                    for (int i = 0; i < centroidC2.Count; i++)
                    {
                        centroidBaruC2 += centroidC2[i]; //hitung jmlh data yg msk dlm cluster2
                    }
                    for (int i = 0; i < centroidD2.Count; i++)
                    {
                        centroidBaruD2 += centroidD2[i]; //hitung jmlh data yg msk dlm cluster2
                    }

                    for (int i = 0; i < centroidA3.Count; i++)
                    {
                        centroidBaruA3 += centroidA3[i]; //hitung jmlh data yg msk dlm cluster3
                    }
                    for (int i = 0; i < centroidB3.Count; i++)
                    {
                        centroidBaruB3 += centroidB3[i]; //hitung jmlh data yg msk dlm cluster2
                    }
                    for (int i = 0; i < centroidC3.Count; i++)
                    {
                        centroidBaruC3 += centroidC3[i]; //hitung jmlh data yg msk dlm cluster3
                    }
                    for (int i = 0; i < centroidD3.Count; i++)
                    {
                        centroidBaruD3 += centroidD3[i]; //hitung jmlh data yg msk dlm cluster3
                    }

                    centroidBaruA1 = centroidBaruA1 / centroidA1.Count; //posisi centroid A1 saat ini diperoleh dr jmlh data total di list centroA1/jmlh data yg ada\
                    centroidBaruB1 = centroidBaruB1 / centroidB1.Count;
                    centroidBaruC1 = centroidBaruC1 / centroidC1.Count;
                    centroidBaruD1 = centroidBaruD1 / centroidD1.Count;

                    centroidBaruA2 = centroidBaruA2 / centroidA2.Count;
                    centroidBaruB2 = centroidBaruB2 / centroidB2.Count;
                    centroidBaruC2 = centroidBaruC2 / centroidC2.Count;
                    centroidBaruD2 = centroidBaruD2 / centroidD2.Count;

                    centroidBaruA3 = centroidBaruA3 / centroidA3.Count;
                    centroidBaruB3 = centroidBaruB3 / centroidB3.Count;
                    centroidBaruC3 = centroidBaruC3 / centroidC3.Count;
                    centroidBaruD3 = centroidBaruD3 / centroidD3.Count;

                    centera1 = Math.Round(centera1, digitAfterDot);
                    centerb1 = Math.Round(centerb1, digitAfterDot);
                    centerc1 = Math.Round(centerc1, digitAfterDot);
                    centerd1 = Math.Round(centerd1, digitAfterDot);

                    centera2 = Math.Round(centera2, digitAfterDot);
                    centerb2 = Math.Round(centerb2, digitAfterDot);
                    centerc2 = Math.Round(centerc2, digitAfterDot);
                    centerd2 = Math.Round(centerd2, digitAfterDot);

                    centera3 = Math.Round(centera3, digitAfterDot);
                    centerb3 = Math.Round(centerb3, digitAfterDot);
                    centerc3 = Math.Round(centerc3, digitAfterDot);
                    centerd3 = Math.Round(centerd3, digitAfterDot);

                    centroidBaruA1 = Math.Round(centroidBaruA1, digitAfterDot);
                    centroidBaruB1 = Math.Round(centroidBaruB1, digitAfterDot);
                    centroidBaruC1 = Math.Round(centroidBaruC1, digitAfterDot);
                    centroidBaruD1 = Math.Round(centroidBaruD1, digitAfterDot);

                    centroidBaruA2 = Math.Round(centroidBaruA2, digitAfterDot);
                    centroidBaruB2 = Math.Round(centroidBaruB2, digitAfterDot);
                    centroidBaruC2 = Math.Round(centroidBaruC2, digitAfterDot);
                    centroidBaruD2 = Math.Round(centroidBaruD2, digitAfterDot);

                    centroidBaruA3 = Math.Round(centroidBaruA3, digitAfterDot);
                    centroidBaruB3 = Math.Round(centroidBaruB3, digitAfterDot);
                    centroidBaruC3 = Math.Round(centroidBaruC3, digitAfterDot);
                    centroidBaruD3 = Math.Round(centroidBaruD3, digitAfterDot);

                    if (centera1 == centroidBaruA1 && centerb1 == centroidBaruB1 &&
                        centerc1 == centroidBaruC1 && centerd1 == centroidBaruD1 &&
                        centera2 == centroidBaruA2 && centerb2 == centroidBaruB2 &&
                        centerc2 == centroidBaruC2 && centerd2 == centroidBaruD2 &&
                        centera3 == centroidBaruA3 && centerb3 == centroidBaruB3 &&
                        centerc3 == centroidBaruC3 && centerd3 == centroidBaruD3 
                        || count == 2000000)
                    {
                        if (count == 2000000)
                        {
                            MessageBox.Show("Program berhenti karena algoritma sudah berulang sebanyak 2.000.000x", "Perhatian!");
                        }
                        if (runOnce == false)
                        {
                            dataGridViewResult.Columns.Add("ColumnName", " ");
                            dataGridViewResult.Columns.Add("ColumnName", dgvInput.Columns[0].HeaderText);
                            dataGridViewResult.Columns.Add("ColumnName", dgvInput.Columns[1].HeaderText);
                            dataGridViewResult.Columns.Add("ColumnName", dgvInput.Columns[2].HeaderText);
                            dataGridViewResult.Columns.Add("ColumnName", dgvInput.Columns[3].HeaderText);
                            runOnce = true;
                        }

                        for (int i = 0; i < k; i++)
                        {
                            dataGridViewResult.Rows.Add();
                            dataGridViewResult.Rows[i].Cells[0].Value = "Cluster Center" + " " + (i + 1);
                        }
                        dataGridViewResult.Rows[0].Cells[1].Value = centroidBaruA1;
                        dataGridViewResult.Rows[0].Cells[2].Value = centroidBaruB1;
                        dataGridViewResult.Rows[0].Cells[3].Value = centroidBaruC1;
                        dataGridViewResult.Rows[0].Cells[4].Value = centroidBaruD1;

                        dataGridViewResult.Rows[1].Cells[1].Value = centroidBaruA2;
                        dataGridViewResult.Rows[1].Cells[2].Value = centroidBaruB2;
                        dataGridViewResult.Rows[1].Cells[3].Value = centroidBaruC2;
                        dataGridViewResult.Rows[1].Cells[4].Value = centroidBaruD2;

                        dataGridViewResult.Rows[2].Cells[1].Value = centroidBaruA3;
                        dataGridViewResult.Rows[2].Cells[2].Value = centroidBaruB3;
                        dataGridViewResult.Rows[2].Cells[3].Value = centroidBaruC3;
                        dataGridViewResult.Rows[2].Cells[4].Value = centroidBaruD3;


                        repeat = false;
                    }
                    else
                    {
                        repeat = true;
                        count += 1;
                    }
                    centera1 = centroidBaruA1;
                    centerb1 = centroidBaruB1;
                    centerc1 = centroidBaruC1;
                    centerd1 = centroidBaruD1;

                    centera2 = centroidBaruA2;
                    centerb2 = centroidBaruB2;
                    centerc2 = centroidBaruC2;
                    centerd2 = centroidBaruD2;

                    centera3 = centroidBaruA3;
                    centerb3 = centroidBaruB3;
                    centerc3 = centroidBaruC3;
                    centerd3 = centroidBaruD3;  
                }
            }
            else if (k == 4)
            {
                while (repeat == true)
                {
                    for (int i = 0; i < rowLength; i++)
                    {
                        double pointA = double.Parse(dgvInput.Rows[i].Cells[0].Value.ToString());
                        double pointB = double.Parse(dgvInput.Rows[i].Cells[1].Value.ToString());
                        double pointC = double.Parse(dgvInput.Rows[i].Cells[2].Value.ToString());
                        double pointD = double.Parse(dgvInput.Rows[i].Cells[3].Value.ToString());

                        double jarakKeCentroidCluster1 = JarakKeCenter(centera1, centerb1, centerc1,
                            centerd1, pointA, pointB, pointC, pointD);
                        double jarakKeCentroidCluster2 = JarakKeCenter(centera2, centerb2, centerc2,
                            centerd2, pointA, pointB, pointC, pointD);
                        double jarakKeCentroidCluster3 = JarakKeCenter(centera3, centerb3, centerc3,
                            centerd3, pointA, pointB, pointC, pointD);
                        double jarakKeCentroidCluster4 = JarakKeCenter(centera4, centerb4, centerc4,
                            centerd4, pointA, pointB, pointC, pointD);

                        //jika jarak ke centro1 lbh kecil drpd jarak ke centro2, maka msk cluster1
                        if (jarakKeCentroidCluster1 < jarakKeCentroidCluster2 &&
                            jarakKeCentroidCluster1 < jarakKeCentroidCluster3 &&
                            jarakKeCentroidCluster1 < jarakKeCentroidCluster4)
                        {
                            centroidA1.Add(pointA);
                            centroidB1.Add(pointB);
                            centroidC1.Add(pointC);
                            centroidD1.Add(pointD);
                            dgvInput.Rows[i].Cells[5].Value = "Cluster 1";
                        }
                        else if (jarakKeCentroidCluster1 > jarakKeCentroidCluster2 &&
                            jarakKeCentroidCluster2 < jarakKeCentroidCluster3 &&
                            jarakKeCentroidCluster2 < jarakKeCentroidCluster4)
                        {
                            centroidA2.Add(pointA);
                            centroidB2.Add(pointB);
                            centroidC2.Add(pointC);
                            centroidD2.Add(pointD);
                            dgvInput.Rows[i].Cells[5].Value = "Cluster 2";
                        }
                        else if (jarakKeCentroidCluster1 > jarakKeCentroidCluster3 &&
                            jarakKeCentroidCluster2 > jarakKeCentroidCluster3 &&
                            jarakKeCentroidCluster3 < jarakKeCentroidCluster4)
                        {
                            centroidA3.Add(pointA);
                            centroidB3.Add(pointB);
                            centroidC3.Add(pointC);
                            centroidD3.Add(pointD);
                            dgvInput.Rows[i].Cells[5].Value = "Cluster 3";
                        }
                        else if (jarakKeCentroidCluster1 > jarakKeCentroidCluster4 &&
                           jarakKeCentroidCluster2 > jarakKeCentroidCluster4 &&
                           jarakKeCentroidCluster3 > jarakKeCentroidCluster4)
                        {
                            centroidA4.Add(pointA);
                            centroidB4.Add(pointB);
                            centroidC4.Add(pointC);
                            centroidD4.Add(pointD);
                            dgvInput.Rows[i].Cells[5].Value = "Cluster 4";
                        }
                    }
                    for (int i = 0; i < centroidA1.Count; i++)
                    {
                        centroidBaruA1 += centroidA1[i]; //hitung jmlh data yg msk dlm cluster1
                    }
                    for (int i = 0; i < centroidB1.Count; i++)
                    {
                        centroidBaruB1 += centroidB1[i]; //hitung jmlh data yg msk dlm cluster1
                    }
                    for (int i = 0; i < centroidC1.Count; i++)
                    {
                        centroidBaruC1 += centroidC1[i]; //hitung jmlh data yg msk dlm cluster1
                    }
                    for (int i = 0; i < centroidD1.Count; i++)
                    {
                        centroidBaruD1 += centroidD1[i]; //hitung jmlh data yg msk dlm cluster2
                    }

                    for (int i = 0; i < centroidA2.Count; i++)
                    {
                        centroidBaruA2 += centroidA2[i]; //hitung jmlh data yg msk dlm cluster2
                    }
                    for (int i = 0; i < centroidB2.Count; i++)
                    {
                        centroidBaruB2 += centroidB2[i]; //hitung jmlh data yg msk dlm cluster2
                    }
                    for (int i = 0; i < centroidC2.Count; i++)
                    {
                        centroidBaruC2 += centroidC2[i]; //hitung jmlh data yg msk dlm cluster2
                    }
                    for (int i = 0; i < centroidD2.Count; i++)
                    {
                        centroidBaruD2 += centroidD2[i]; //hitung jmlh data yg msk dlm cluster2
                    }

                    for (int i = 0; i < centroidA3.Count; i++)
                    {
                        centroidBaruA3 += centroidA3[i]; //hitung jmlh data yg msk dlm cluster3
                    }
                    for (int i = 0; i < centroidB3.Count; i++)
                    {
                        centroidBaruB3 += centroidB3[i]; //hitung jmlh data yg msk dlm cluster2
                    }
                    for (int i = 0; i < centroidC3.Count; i++)
                    {
                        centroidBaruC3 += centroidC3[i]; //hitung jmlh data yg msk dlm cluster3
                    }
                    for (int i = 0; i < centroidD3.Count; i++)
                    {
                        centroidBaruD3 += centroidD3[i]; //hitung jmlh data yg msk dlm cluster3
                    }

                    for (int i = 0; i < centroidA4.Count; i++)
                    {
                        centroidBaruA4 += centroidA4[i]; //hitung jmlh data yg msk dlm cluster3
                    }
                    for (int i = 0; i < centroidB4.Count; i++)
                    {
                        centroidBaruB4 += centroidB4[i]; //hitung jmlh data yg msk dlm cluster2
                    }
                    for (int i = 0; i < centroidC4.Count; i++)
                    {
                        centroidBaruC4 += centroidC4[i]; //hitung jmlh data yg msk dlm cluster3
                    }
                    for (int i = 0; i < centroidD4.Count; i++)
                    {
                        centroidBaruD4 += centroidD4[i]; //hitung jmlh data yg msk dlm cluster3
                    }

                    centroidBaruA1 = centroidBaruA1 / centroidA1.Count; //posisi centroid A1 saat ini diperoleh dr jmlh data total di list centroA1/jmlh data yg ada\
                    centroidBaruB1 = centroidBaruB1 / centroidB1.Count;
                    centroidBaruC1 = centroidBaruC1 / centroidC1.Count;
                    centroidBaruD1 = centroidBaruD1 / centroidD1.Count;

                    centroidBaruA2 = centroidBaruA2 / centroidA2.Count;
                    centroidBaruB2 = centroidBaruB2 / centroidB2.Count;
                    centroidBaruC2 = centroidBaruC2 / centroidC2.Count;
                    centroidBaruD2 = centroidBaruD2 / centroidD2.Count;

                    centroidBaruA3 = centroidBaruA3 / centroidA3.Count;
                    centroidBaruB3 = centroidBaruB3 / centroidB3.Count;
                    centroidBaruC3 = centroidBaruC3 / centroidC3.Count;
                    centroidBaruD3 = centroidBaruD3 / centroidD3.Count;

                    centroidBaruA4 = centroidBaruA4 / centroidA4.Count;
                    centroidBaruB4 = centroidBaruB4 / centroidB4.Count;
                    centroidBaruC4 = centroidBaruC4 / centroidC4.Count;
                    centroidBaruD4 = centroidBaruD4 / centroidD4.Count;

                    centera1 = Math.Round(centera1, digitAfterDot);
                    centerb1 = Math.Round(centerb1, digitAfterDot);
                    centerc1 = Math.Round(centerc1, digitAfterDot);
                    centerd1 = Math.Round(centerd1, digitAfterDot);

                    centera2 = Math.Round(centera2, digitAfterDot);
                    centerb2 = Math.Round(centerb2, digitAfterDot);
                    centerc2 = Math.Round(centerc2, digitAfterDot);
                    centerd2 = Math.Round(centerd2, digitAfterDot);

                    centera3 = Math.Round(centera3, digitAfterDot);
                    centerb3 = Math.Round(centerb3, digitAfterDot);
                    centerc3 = Math.Round(centerc3, digitAfterDot);
                    centerd3 = Math.Round(centerd3, digitAfterDot);

                    centera4 = Math.Round(centera4, digitAfterDot);
                    centerb4 = Math.Round(centerb4, digitAfterDot);
                    centerc4 = Math.Round(centerc4, digitAfterDot);
                    centerd4 = Math.Round(centerd4, digitAfterDot);

                    centroidBaruA1 = Math.Round(centroidBaruA1, digitAfterDot);
                    centroidBaruB1 = Math.Round(centroidBaruB1, digitAfterDot);
                    centroidBaruC1 = Math.Round(centroidBaruC1, digitAfterDot);
                    centroidBaruD1 = Math.Round(centroidBaruD1, digitAfterDot);

                    centroidBaruA2 = Math.Round(centroidBaruA2, digitAfterDot);
                    centroidBaruB2 = Math.Round(centroidBaruB2, digitAfterDot);
                    centroidBaruC2 = Math.Round(centroidBaruC2, digitAfterDot);
                    centroidBaruD2 = Math.Round(centroidBaruD2, digitAfterDot);

                    centroidBaruA3 = Math.Round(centroidBaruA3, digitAfterDot);
                    centroidBaruB3 = Math.Round(centroidBaruB3, digitAfterDot);
                    centroidBaruC3 = Math.Round(centroidBaruC3, digitAfterDot);
                    centroidBaruD3 = Math.Round(centroidBaruD3, digitAfterDot);

                    centroidBaruA4 = Math.Round(centroidBaruA4, digitAfterDot);
                    centroidBaruB4 = Math.Round(centroidBaruB4, digitAfterDot);
                    centroidBaruC4 = Math.Round(centroidBaruC4, digitAfterDot);
                    centroidBaruD4 = Math.Round(centroidBaruD4, digitAfterDot);

                    if (centera1 == centroidBaruA1 && centerb1 == centroidBaruB1 &&
                        centerc1 == centroidBaruC1 && centerd1 == centroidBaruD1 &&
                        centera2 == centroidBaruA2 && centerb2 == centroidBaruB2 &&
                        centerc2 == centroidBaruC2 && centerd2 == centroidBaruD2 &&
                        centera3 == centroidBaruA3 && centerb3 == centroidBaruB3 &&
                        centerc3 == centroidBaruC3 && centerd3 == centroidBaruD3 &&
                        centera4 == centroidBaruA4 && centerb4 == centroidBaruB4 &&
                        centerc4 == centroidBaruC4 && centerd4 == centroidBaruD4
                        || count == 2000000)
                    {
                        if (count == 2000000)
                        {
                            MessageBox.Show("Program berhenti karena algoritma sudah berulang sebanyak 2.000.000x", "Perhatian!");
                        }
                        if (runOnce == false)
                        {
                            dataGridViewResult.Columns.Add("ColumnName", " ");
                            dataGridViewResult.Columns.Add("ColumnName", dgvInput.Columns[0].HeaderText);
                            dataGridViewResult.Columns.Add("ColumnName", dgvInput.Columns[1].HeaderText);
                            dataGridViewResult.Columns.Add("ColumnName", dgvInput.Columns[0].HeaderText);
                            dataGridViewResult.Columns.Add("ColumnName", dgvInput.Columns[0].HeaderText);
                            runOnce = true;
                        }

                        for (int i = 0; i < k; i++)
                        {
                            dataGridViewResult.Rows.Add();
                            dataGridViewResult.Rows[i].Cells[0].Value = "Cluster Center" + " " + (i + 1);
                        }
                        dataGridViewResult.Rows[0].Cells[1].Value = centroidBaruA1;
                        dataGridViewResult.Rows[0].Cells[2].Value = centroidBaruB1;
                        dataGridViewResult.Rows[0].Cells[3].Value = centroidBaruC1;
                        dataGridViewResult.Rows[0].Cells[4].Value = centroidBaruD1;

                        dataGridViewResult.Rows[1].Cells[1].Value = centroidBaruA2;
                        dataGridViewResult.Rows[1].Cells[2].Value = centroidBaruB2;
                        dataGridViewResult.Rows[1].Cells[3].Value = centroidBaruC2;
                        dataGridViewResult.Rows[1].Cells[4].Value = centroidBaruD2;

                        dataGridViewResult.Rows[2].Cells[1].Value = centroidBaruA3;
                        dataGridViewResult.Rows[2].Cells[2].Value = centroidBaruB3;
                        dataGridViewResult.Rows[2].Cells[3].Value = centroidBaruC3;
                        dataGridViewResult.Rows[2].Cells[4].Value = centroidBaruD3;

                        dataGridViewResult.Rows[3].Cells[1].Value = centroidBaruA4;
                        dataGridViewResult.Rows[3].Cells[2].Value = centroidBaruB4;
                        dataGridViewResult.Rows[3].Cells[3].Value = centroidBaruC4;
                        dataGridViewResult.Rows[3].Cells[4].Value = centroidBaruD4;
                        repeat = false;
                    }
                    else
                    {
                        repeat = true;
                        count += 1;
                    }
                    centera1 = centroidBaruA1;
                    centerb1 = centroidBaruB1;
                    centerc1 = centroidBaruC1;
                    centerd1 = centroidBaruD1;

                    centera2 = centroidBaruA2;
                    centerb2 = centroidBaruB2;
                    centerc2 = centroidBaruC2;
                    centerd2 = centroidBaruD2;

                    centera3 = centroidBaruA3;
                    centerb3 = centroidBaruB3;
                    centerc3 = centroidBaruC3;
                    centerd3 = centroidBaruD3;

                    centera4 = centroidBaruA4;
                    centerb4 = centroidBaruB4;
                    centerc4 = centroidBaruC4;
                    centerd4 = centroidBaruD4;
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            dgvInput.SelectAll();
            DataObject copyCluster = dgvInput.GetClipboardContent();
            if (copyCluster != null) Clipboard.SetDataObject(copyCluster);
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
    }
}
    

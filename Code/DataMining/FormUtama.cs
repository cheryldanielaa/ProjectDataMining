using sedih;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataMining
{
    public partial class FormUtama : Form
    {
        public FormUtama()
        {
            InitializeComponent();
        }

        private void proximityMatrixAndBestSplitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormProximityMatrixAndBestSplit form = new FormProximityMatrixAndBestSplit();
            form.Owner= this;
            form.ShowDialog();
        }

        private void keluarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void kMeansToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormKmeans form = new FormKmeans();
            form.Owner=this;
            form.ShowDialog();
        }

        private void FormUtama_Load(object sender, EventArgs e)
        {

        }
    }
}

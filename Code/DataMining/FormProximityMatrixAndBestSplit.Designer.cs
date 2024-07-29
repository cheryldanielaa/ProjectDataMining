namespace DataMining
{
    partial class FormProximityMatrixAndBestSplit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridViewResult = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownIndexClass = new System.Windows.Forms.NumericUpDown();
            this.groupBoxBestSplit = new System.Windows.Forms.GroupBox();
            this.btnBestSplit = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.listBoxInfo = new System.Windows.Forms.ListBox();
            this.rboSelBestSplit = new System.Windows.Forms.RadioButton();
            this.rboSelProx = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.rboManhattan = new System.Windows.Forms.RadioButton();
            this.rboEucledianDistance = new System.Windows.Forms.RadioButton();
            this.buttonProxMatrix = new System.Windows.Forms.Button();
            this.rboSupremum = new System.Windows.Forms.RadioButton();
            this.groupBoxProxMatrix = new System.Windows.Forms.GroupBox();
            this.cboSheet = new System.Windows.Forms.ComboBox();
            this.dataGridViewSheets = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.buttonExportLB = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIndexClass)).BeginInit();
            this.groupBoxBestSplit.SuspendLayout();
            this.groupBoxProxMatrix.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSheets)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewResult
            // 
            this.dataGridViewResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewResult.Location = new System.Drawing.Point(800, 39);
            this.dataGridViewResult.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridViewResult.Name = "dataGridViewResult";
            this.dataGridViewResult.RowHeadersWidth = 62;
            this.dataGridViewResult.RowTemplate.Height = 28;
            this.dataGridViewResult.Size = new System.Drawing.Size(376, 436);
            this.dataGridViewResult.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0, 78);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 16);
            this.label5.TabIndex = 14;
            this.label5.Text = "Class Column:";
            // 
            // numericUpDownIndexClass
            // 
            this.numericUpDownIndexClass.Location = new System.Drawing.Point(103, 76);
            this.numericUpDownIndexClass.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDownIndexClass.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownIndexClass.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownIndexClass.Name = "numericUpDownIndexClass";
            this.numericUpDownIndexClass.Size = new System.Drawing.Size(71, 22);
            this.numericUpDownIndexClass.TabIndex = 13;
            this.numericUpDownIndexClass.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // groupBoxBestSplit
            // 
            this.groupBoxBestSplit.Controls.Add(this.numericUpDownIndexClass);
            this.groupBoxBestSplit.Controls.Add(this.label5);
            this.groupBoxBestSplit.Controls.Add(this.btnBestSplit);
            this.groupBoxBestSplit.Controls.Add(this.label3);
            this.groupBoxBestSplit.Location = new System.Drawing.Point(596, 302);
            this.groupBoxBestSplit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxBestSplit.Name = "groupBoxBestSplit";
            this.groupBoxBestSplit.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxBestSplit.Size = new System.Drawing.Size(188, 177);
            this.groupBoxBestSplit.TabIndex = 15;
            this.groupBoxBestSplit.TabStop = false;
            // 
            // btnBestSplit
            // 
            this.btnBestSplit.Location = new System.Drawing.Point(40, 130);
            this.btnBestSplit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnBestSplit.Name = "btnBestSplit";
            this.btnBestSplit.Size = new System.Drawing.Size(112, 35);
            this.btnBestSplit.TabIndex = 3;
            this.btnBestSplit.Text = "Best Split";
            this.btnBestSplit.UseVisualStyleBackColor = true;
            this.btnBestSplit.Click += new System.EventHandler(this.btnBestSplit_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(168, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Finding The Best Split";
            // 
            // listBoxInfo
            // 
            this.listBoxInfo.FormattingEnabled = true;
            this.listBoxInfo.ItemHeight = 16;
            this.listBoxInfo.Location = new System.Drawing.Point(800, 43);
            this.listBoxInfo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listBoxInfo.Name = "listBoxInfo";
            this.listBoxInfo.Size = new System.Drawing.Size(376, 436);
            this.listBoxInfo.TabIndex = 22;
            // 
            // rboSelBestSplit
            // 
            this.rboSelBestSplit.AutoSize = true;
            this.rboSelBestSplit.Location = new System.Drawing.Point(596, 278);
            this.rboSelBestSplit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rboSelBestSplit.Name = "rboSelBestSplit";
            this.rboSelBestSplit.Size = new System.Drawing.Size(84, 20);
            this.rboSelBestSplit.TabIndex = 17;
            this.rboSelBestSplit.TabStop = true;
            this.rboSelBestSplit.Text = "Best Split";
            this.rboSelBestSplit.UseVisualStyleBackColor = true;
            this.rboSelBestSplit.CheckedChanged += new System.EventHandler(this.rboSelBestSplit_CheckedChanged);
            // 
            // rboSelProx
            // 
            this.rboSelProx.AutoSize = true;
            this.rboSelProx.Location = new System.Drawing.Point(596, 43);
            this.rboSelProx.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rboSelProx.Name = "rboSelProx";
            this.rboSelProx.Size = new System.Drawing.Size(120, 20);
            this.rboSelProx.TabIndex = 5;
            this.rboSelProx.TabStop = true;
            this.rboSelProx.Text = "Proximity Matrix";
            this.rboSelProx.UseVisualStyleBackColor = true;
            this.rboSelProx.CheckedChanged += new System.EventHandler(this.rboSelProx_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Proximity Matrix";
            // 
            // rboManhattan
            // 
            this.rboManhattan.AutoSize = true;
            this.rboManhattan.Location = new System.Drawing.Point(10, 50);
            this.rboManhattan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rboManhattan.Name = "rboManhattan";
            this.rboManhattan.Size = new System.Drawing.Size(146, 20);
            this.rboManhattan.TabIndex = 1;
            this.rboManhattan.TabStop = true;
            this.rboManhattan.Text = "Manhattan Distance";
            this.rboManhattan.UseVisualStyleBackColor = true;
            this.rboManhattan.CheckedChanged += new System.EventHandler(this.rboManhattan_CheckedChanged);
            // 
            // rboEucledianDistance
            // 
            this.rboEucledianDistance.AutoSize = true;
            this.rboEucledianDistance.Location = new System.Drawing.Point(10, 83);
            this.rboEucledianDistance.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rboEucledianDistance.Name = "rboEucledianDistance";
            this.rboEucledianDistance.Size = new System.Drawing.Size(144, 20);
            this.rboEucledianDistance.TabIndex = 2;
            this.rboEucledianDistance.TabStop = true;
            this.rboEucledianDistance.Text = "Eucledian Distance";
            this.rboEucledianDistance.UseVisualStyleBackColor = true;
            this.rboEucledianDistance.CheckedChanged += new System.EventHandler(this.rboEucledianDistance_CheckedChanged);
            // 
            // buttonProxMatrix
            // 
            this.buttonProxMatrix.Location = new System.Drawing.Point(40, 149);
            this.buttonProxMatrix.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonProxMatrix.Name = "buttonProxMatrix";
            this.buttonProxMatrix.Size = new System.Drawing.Size(112, 35);
            this.buttonProxMatrix.TabIndex = 3;
            this.buttonProxMatrix.Text = "Proximity Matrix";
            this.buttonProxMatrix.UseVisualStyleBackColor = true;
            this.buttonProxMatrix.Click += new System.EventHandler(this.buttonProxMatrix_Click);
            // 
            // rboSupremum
            // 
            this.rboSupremum.AutoSize = true;
            this.rboSupremum.Location = new System.Drawing.Point(10, 116);
            this.rboSupremum.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rboSupremum.Name = "rboSupremum";
            this.rboSupremum.Size = new System.Drawing.Size(93, 20);
            this.rboSupremum.TabIndex = 4;
            this.rboSupremum.TabStop = true;
            this.rboSupremum.Text = "Supremum";
            this.rboSupremum.UseVisualStyleBackColor = true;
            this.rboSupremum.CheckedChanged += new System.EventHandler(this.rboSupremum_CheckedChanged);
            // 
            // groupBoxProxMatrix
            // 
            this.groupBoxProxMatrix.Controls.Add(this.rboSupremum);
            this.groupBoxProxMatrix.Controls.Add(this.buttonProxMatrix);
            this.groupBoxProxMatrix.Controls.Add(this.rboEucledianDistance);
            this.groupBoxProxMatrix.Controls.Add(this.rboManhattan);
            this.groupBoxProxMatrix.Controls.Add(this.label4);
            this.groupBoxProxMatrix.Location = new System.Drawing.Point(596, 61);
            this.groupBoxProxMatrix.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxProxMatrix.Name = "groupBoxProxMatrix";
            this.groupBoxProxMatrix.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxProxMatrix.Size = new System.Drawing.Size(182, 198);
            this.groupBoxProxMatrix.TabIndex = 16;
            this.groupBoxProxMatrix.TabStop = false;
            // 
            // cboSheet
            // 
            this.cboSheet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSheet.Font = new System.Drawing.Font("Rockwell", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSheet.FormattingEnabled = true;
            this.cboSheet.Location = new System.Drawing.Point(119, 469);
            this.cboSheet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboSheet.Name = "cboSheet";
            this.cboSheet.Size = new System.Drawing.Size(189, 28);
            this.cboSheet.TabIndex = 14;
            this.cboSheet.SelectedIndexChanged += new System.EventHandler(this.cboSheet_SelectedIndexChanged);
            // 
            // dataGridViewSheets
            // 
            this.dataGridViewSheets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSheets.Location = new System.Drawing.Point(11, 43);
            this.dataGridViewSheets.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridViewSheets.Name = "dataGridViewSheets";
            this.dataGridViewSheets.RowHeadersWidth = 62;
            this.dataGridViewSheets.RowTemplate.Height = 28;
            this.dataGridViewSheets.Size = new System.Drawing.Size(571, 376);
            this.dataGridViewSheets.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Rockwell", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 438);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "File Name :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Rockwell", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 471);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "Sheet :";
            // 
            // textBoxPath
            // 
            this.textBoxPath.Font = new System.Drawing.Font("Rockwell", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPath.Location = new System.Drawing.Point(119, 431);
            this.textBoxPath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.Size = new System.Drawing.Size(360, 27);
            this.textBoxPath.TabIndex = 11;
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Font = new System.Drawing.Font("Rockwell", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBrowse.Location = new System.Drawing.Point(500, 424);
            this.buttonBrowse.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(81, 32);
            this.buttonBrowse.TabIndex = 12;
            this.buttonBrowse.Text = "...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // buttonExportLB
            // 
            this.buttonExportLB.Enabled = false;
            this.buttonExportLB.Location = new System.Drawing.Point(337, 492);
            this.buttonExportLB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonExportLB.Name = "buttonExportLB";
            this.buttonExportLB.Size = new System.Drawing.Size(215, 39);
            this.buttonExportLB.TabIndex = 28;
            this.buttonExportLB.Text = "Export Data";
            this.buttonExportLB.UseVisualStyleBackColor = true;
            this.buttonExportLB.Visible = false;
            this.buttonExportLB.Click += new System.EventHandler(this.buttonExportLB_Click);
            // 
            // btnExport
            // 
            this.btnExport.Enabled = false;
            this.btnExport.Location = new System.Drawing.Point(337, 492);
            this.btnExport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(215, 39);
            this.btnExport.TabIndex = 27;
            this.btnExport.Text = "Export Data";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Visible = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // FormProximityMatrixAndBestSplit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1188, 556);
            this.Controls.Add(this.buttonExportLB);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.dataGridViewResult);
            this.Controls.Add(this.listBoxInfo);
            this.Controls.Add(this.rboSelBestSplit);
            this.Controls.Add(this.rboSelProx);
            this.Controls.Add(this.groupBoxProxMatrix);
            this.Controls.Add(this.groupBoxBestSplit);
            this.Controls.Add(this.cboSheet);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.textBoxPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridViewSheets);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormProximityMatrixAndBestSplit";
            this.Text = "Calculate Proximity Matrix & Best Split";
            this.Load += new System.EventHandler(this.FormProject1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIndexClass)).EndInit();
            this.groupBoxBestSplit.ResumeLayout(false);
            this.groupBoxBestSplit.PerformLayout();
            this.groupBoxProxMatrix.ResumeLayout(false);
            this.groupBoxProxMatrix.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSheets)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewResult;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDownIndexClass;
        private System.Windows.Forms.GroupBox groupBoxBestSplit;
        private System.Windows.Forms.Button btnBestSplit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBoxInfo;
        private System.Windows.Forms.RadioButton rboSelBestSplit;
        private System.Windows.Forms.RadioButton rboSelProx;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rboManhattan;
        private System.Windows.Forms.RadioButton rboEucledianDistance;
        private System.Windows.Forms.Button buttonProxMatrix;
        private System.Windows.Forms.RadioButton rboSupremum;
        private System.Windows.Forms.GroupBox groupBoxProxMatrix;
        private System.Windows.Forms.ComboBox cboSheet;
        private System.Windows.Forms.DataGridView dataGridViewSheets;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Button buttonExportLB;
        private System.Windows.Forms.Button btnExport;
    }
}


namespace DataAnalyzer
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnLoadFile;
        private System.Windows.Forms.Button btnSaveCharts;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.FlowLayoutPanel chart1;
        private System.Windows.Forms.Button btnSetInitialPressure;
        private System.Windows.Forms.Button btnLoadFile2;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnLoadFile = new System.Windows.Forms.Button();
            this.btnSaveCharts = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.chart1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSetInitialPressure = new System.Windows.Forms.Button();
            this.btnLoadFile2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.Location = new System.Drawing.Point(12, 12);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(200, 23);
            this.btnLoadFile.TabIndex = 0;
            this.btnLoadFile.Text = "Загрузить файл данных памяти";
            this.btnLoadFile.UseVisualStyleBackColor = true;
            this.btnLoadFile.Click += new System.EventHandler(this.BtnLoadFile_Click);
            // 
            // btnSaveCharts
            // 
            this.btnSaveCharts.Location = new System.Drawing.Point(424, 12);
            this.btnSaveCharts.Name = "btnSaveCharts";
            this.btnSaveCharts.Size = new System.Drawing.Size(118, 23);
            this.btnSaveCharts.TabIndex = 1;
            this.btnSaveCharts.Text = "Сохранить графики";
            this.btnSaveCharts.UseVisualStyleBackColor = true;
            this.btnSaveCharts.Click += new System.EventHandler(this.BtnSaveCharts_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // chart1
            // 
            this.chart1.AutoScroll = true;
            this.chart1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.chart1.Location = new System.Drawing.Point(0, 50);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(1028, 800);
            this.chart1.TabIndex = 2;
            // 
            // btnSetInitialPressure
            // 
            this.btnSetInitialPressure.Location = new System.Drawing.Point(548, 12);
            this.btnSetInitialPressure.Name = "btnSetInitialPressure";
            this.btnSetInitialPressure.Size = new System.Drawing.Size(200, 23);
            this.btnSetInitialPressure.TabIndex = 3;
            this.btnSetInitialPressure.Text = "Настройка начального давления";
            this.btnSetInitialPressure.UseVisualStyleBackColor = true;
            this.btnSetInitialPressure.Click += new System.EventHandler(this.BtnSetInitialPressure_Click);
            // 
            // btnLoadFile2
            // 
            this.btnLoadFile2.Location = new System.Drawing.Point(218, 12);
            this.btnLoadFile2.Name = "btnLoadFile2";
            this.btnLoadFile2.Size = new System.Drawing.Size(200, 23);
            this.btnLoadFile2.TabIndex = 4;
            this.btnLoadFile2.Text = "Загрузить файл данных телеметрии\r\n";
            this.btnLoadFile2.UseVisualStyleBackColor = true;
            this.btnLoadFile2.Click += new System.EventHandler(this.BtnLoadFile2_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1028, 850);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.btnSaveCharts);
            this.Controls.Add(this.btnLoadFile);
            this.Controls.Add(this.btnSetInitialPressure);
            this.Controls.Add(this.btnLoadFile2);
            this.Name = "Form1";
            this.Text = "Data Analyzer";
            this.ResumeLayout(false);

        }
    }
}

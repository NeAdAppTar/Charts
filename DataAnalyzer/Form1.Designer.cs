namespace DataAnalyzer
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txtSkeleton = new System.Windows.Forms.TextBox();
            this.buttonLoadData = new System.Windows.Forms.Button();
            this.btnSaveCharts = new System.Windows.Forms.Button();
            this.chartContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtSkeleton
            // 
            this.txtSkeleton.Location = new System.Drawing.Point(12, 9);
            this.txtSkeleton.Name = "txtSkeleton";
            this.txtSkeleton.Size = new System.Drawing.Size(246, 20);
            this.txtSkeleton.TabIndex = 0;
            this.txtSkeleton.Text = "ID;t;h;p;T;aX;aY;aZ;gX;gY;gZ;f1;f2;f3;f4;";
            // 
            // buttonLoadData
            // 
            this.buttonLoadData.Location = new System.Drawing.Point(12, 35);
            this.buttonLoadData.Name = "buttonLoadData";
            this.buttonLoadData.Size = new System.Drawing.Size(120, 42);
            this.buttonLoadData.TabIndex = 1;
            this.buttonLoadData.Text = "Загрузить данные";
            this.buttonLoadData.UseVisualStyleBackColor = true;
            this.buttonLoadData.Click += new System.EventHandler(this.buttonLoadData_Click);
            // 
            // btnSaveCharts
            // 
            this.btnSaveCharts.Location = new System.Drawing.Point(138, 35);
            this.btnSaveCharts.Name = "btnSaveCharts";
            this.btnSaveCharts.Size = new System.Drawing.Size(120, 42);
            this.btnSaveCharts.TabIndex = 2;
            this.btnSaveCharts.Text = "Сохранить графики";
            this.btnSaveCharts.UseVisualStyleBackColor = true;
            this.btnSaveCharts.Click += new System.EventHandler(this.btnSaveCharts_Click);
            // 
            // chartContainer
            // 
            this.chartContainer.AutoScroll = true;
            this.chartContainer.Location = new System.Drawing.Point(12, 85);
            this.chartContainer.Name = "chartContainer";
            this.chartContainer.Size = new System.Drawing.Size(914, 501);
            this.chartContainer.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(227, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(264, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(237, 65);
            this.label2.TabIndex = 5;
            this.label2.Text = "- ID — Идентификатор команды (TeamID)\r\n- t — Время                  \r\n- h — Высот" +
    "а                                                        \r\n- p — Давление \r\n- T " +
    "— Температура";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(507, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(193, 65);
            this.label3.TabIndex = 6;
            this.label3.Text = "- aX — Линейное ускорение по оси X \r\n- aY — Линейное ускорение по оси Y \r\n- aZ — " +
    "Линейное ускорение по оси Z \r\n- gX — Угловая скорость по оси X \r\n- gY — Угловая " +
    "скорость по оси Y ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(746, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(180, 65);
            this.label4.TabIndex = 7;
            this.label4.Text = "- gZ — Угловая скорость по оси Z \r\n- f1 — Флаг 1\r\n- f2 — Флаг 2 (не используется)" +
    "\r\n- f3 — Флаг 3 (не используется)\r\n- f4 — Флаг 4";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(938, 598);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chartContainer);
            this.Controls.Add(this.btnSaveCharts);
            this.Controls.Add(this.buttonLoadData);
            this.Controls.Add(this.txtSkeleton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(954, 637);
            this.MinimumSize = new System.Drawing.Size(954, 637);
            this.Name = "Form1";
            this.Text = "Charts";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSkeleton;
        private System.Windows.Forms.Button buttonLoadData;
        private System.Windows.Forms.Button btnSaveCharts;
        private System.Windows.Forms.FlowLayoutPanel chartContainer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

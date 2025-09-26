namespace DataAnalyzer
{
    partial class EditOrderForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListBox fieldListBox;
        private System.Windows.Forms.Button btnSave;

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
            this.fieldListBox = new System.Windows.Forms.ListBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // 
            // fieldListBox
            // 
            this.fieldListBox.FormattingEnabled = true;
            this.fieldListBox.Location = new System.Drawing.Point(12, 12);
            this.fieldListBox.Name = "fieldListBox";
            this.fieldListBox.Size = new System.Drawing.Size(200, 199);
            this.fieldListBox.TabIndex = 0;

            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 217);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(200, 30);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // 
            // EditOrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 261);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.fieldListBox);
            this.Name = "EditOrderForm";
            this.Text = "Edit Field Order";
            this.ResumeLayout(false);
        }
    }
}

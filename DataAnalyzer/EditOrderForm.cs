using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DataAnalyzer
{
    public partial class EditOrderForm : Form
    {
        public List<string> FieldOrder { get; private set; }

        public EditOrderForm(List<string> currentOrder)
        {
            InitializeComponent();

            FieldOrder = new List<string>(currentOrder);
            fieldListBox.DataSource = new BindingSource(FieldOrder, null);
        }

        // Обработка кнопки "Сохранить"
        private void btnSave_Click(object sender, EventArgs e)
        {
            FieldOrder = fieldListBox.Items.Cast<string>().ToList();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

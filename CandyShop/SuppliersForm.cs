using System;
using System.Windows.Forms;

namespace CandyShop
{
    public partial class SuppliersForm : Form
    {
        private int selectedRowIndex = -1;

        public SuppliersForm()
        {
            InitializeComponent();
        }

        private void SuppliersForm_Load(object sender, EventArgs e)
        {
            this.Text = "Поставщики";
            this.StartPosition = FormStartPosition.CenterScreen;

            dgvSuppliers.ColumnCount = 1;
            dgvSuppliers.Columns[0].Name = "Название поставщика";

            dgvSuppliers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSuppliers.MultiSelect = false;
            dgvSuppliers.ReadOnly = true;
            dgvSuppliers.AllowUserToAddRows = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            dgvSuppliers.Rows.Add(txtSupplierName.Text.Trim());
            DataStorage.Suppliers.Add(txtSupplierName.Text.Trim());
            ClearFields();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex < 0)
            {
                MessageBox.Show("Выберите поставщика для изменения.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs())
                return;

            dgvSuppliers.Rows[selectedRowIndex].Cells[0].Value = txtSupplierName.Text.Trim();
            ClearFields();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex < 0)
            {
                MessageBox.Show("Выберите поставщика для удаления.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Удалить выбранного поставщика?",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                dgvSuppliers.Rows.RemoveAt(selectedRowIndex);
                ClearFields();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void dgvSuppliers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedRowIndex = e.RowIndex;
                txtSupplierName.Text = dgvSuppliers.Rows[e.RowIndex].Cells[0].Value?.ToString();
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtSupplierName.Text))
            {
                MessageBox.Show("Введите название поставщика.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSupplierName.Focus();
                return false;
            }

            return true;
        }

        private void ClearFields()
        {
            txtSupplierName.Clear();
            selectedRowIndex = -1;
            txtSupplierName.Focus();
        }
    }
}
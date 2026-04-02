using System;
using System.Windows.Forms;

namespace CandyShop
{
    public partial class ProductsForm : Form
    {
        private int selectedRowIndex = -1;

        public ProductsForm()
        {
            InitializeComponent();
        }

        private void ProductsForm_Load(object sender, EventArgs e)
        {
            this.Text = "Товары";
            this.StartPosition = FormStartPosition.CenterScreen;

            cmbCategory.Items.Clear();
            cmbCategory.Items.AddRange(new string[]
            {
                "Шоколад",
                "Конфеты",
                "Печенье",
                "Торты",
                "Напитки"
            });

            cmbSupplier.Items.Clear();
            cmbSupplier.Items.AddRange(new string[]
            {
                "Поставщик 1",
                "Поставщик 2",
                "Поставщик 3"
            });

            cmbUnit.Items.Clear();
            cmbUnit.Items.AddRange(new string[]
            {
                "шт",
                "кг",
                "упак"
            });

            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSupplier.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUnit.DropDownStyle = ComboBoxStyle.DropDownList;

            dgvProducts.ColumnCount = 5;
            dgvProducts.Columns[0].Name = "Название";
            dgvProducts.Columns[1].Name = "Категория";
            dgvProducts.Columns[2].Name = "Поставщик";
            dgvProducts.Columns[3].Name = "Цена";
            dgvProducts.Columns[4].Name = "Ед. изм.";

            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.MultiSelect = false;
            dgvProducts.ReadOnly = true;
            dgvProducts.AllowUserToAddRows = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            dgvProducts.Rows.Add(
                txtName.Text.Trim(),
                cmbCategory.Text,
                cmbSupplier.Text,
                txtPrice.Text.Trim(),
                cmbUnit.Text
            );

            ClearFields();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex < 0)
            {
                MessageBox.Show("Выберите товар для изменения.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs())
                return;

            dgvProducts.Rows[selectedRowIndex].Cells[0].Value = txtName.Text.Trim();
            dgvProducts.Rows[selectedRowIndex].Cells[1].Value = cmbCategory.Text;
            dgvProducts.Rows[selectedRowIndex].Cells[2].Value = cmbSupplier.Text;
            dgvProducts.Rows[selectedRowIndex].Cells[3].Value = txtPrice.Text.Trim();
            dgvProducts.Rows[selectedRowIndex].Cells[4].Value = cmbUnit.Text;

            ClearFields();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex < 0)
            {
                MessageBox.Show("Выберите товар для удаления.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Удалить выбранный товар?",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                dgvProducts.Rows.RemoveAt(selectedRowIndex);
                ClearFields();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedRowIndex = e.RowIndex;

                txtName.Text = dgvProducts.Rows[e.RowIndex].Cells[0].Value?.ToString();
                cmbCategory.Text = dgvProducts.Rows[e.RowIndex].Cells[1].Value?.ToString();
                cmbSupplier.Text = dgvProducts.Rows[e.RowIndex].Cells[2].Value?.ToString();
                txtPrice.Text = dgvProducts.Rows[e.RowIndex].Cells[3].Value?.ToString();
                cmbUnit.Text = dgvProducts.Rows[e.RowIndex].Cells[4].Value?.ToString();
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(cmbCategory.Text) ||
                string.IsNullOrWhiteSpace(cmbSupplier.Text) ||
                string.IsNullOrWhiteSpace(txtPrice.Text) ||
                string.IsNullOrWhiteSpace(cmbUnit.Text))
            {
                MessageBox.Show("Заполните все поля.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price < 0)
            {
                MessageBox.Show("Введите корректную цену.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrice.Focus();
                return false;
            }

            return true;
        }

        private void ClearFields()
        {
            txtName.Clear();
            cmbCategory.SelectedIndex = -1;
            cmbSupplier.SelectedIndex = -1;
            txtPrice.Clear();
            cmbUnit.SelectedIndex = -1;

            selectedRowIndex = -1;
            txtName.Focus();
        }
    }
}
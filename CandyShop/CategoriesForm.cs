using System;
using System.Windows.Forms;

namespace CandyShop
{
    public partial class CategoriesForm : Form
    {
        private int selectedRowIndex = -1;

        public CategoriesForm()
        {
            InitializeComponent();
        }

        private void CategoriesForm_Load(object sender, EventArgs e)
        {
            dgvCategories.ColumnCount = 1;
            dgvCategories.Columns[0].Name = "Название категории";

            dgvCategories.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCategories.MultiSelect = false;
            dgvCategories.ReadOnly = true;
            dgvCategories.AllowUserToAddRows = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                MessageBox.Show("Введите название категории");
                return;
            }

            dgvCategories.Rows.Add(txtCategoryName.Text.Trim());
            DataStorage.Categories.Add(txtCategoryName.Text.Trim());
            ClearFields();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex < 0)
            {
                MessageBox.Show("Выберите категорию");
                return;
            }

            dgvCategories.Rows[selectedRowIndex].Cells[0].Value = txtCategoryName.Text.Trim();
            ClearFields();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex < 0)
            {
                MessageBox.Show("Выберите категорию");
                return;
            }

            dgvCategories.Rows.RemoveAt(selectedRowIndex);
            ClearFields();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void dgvCategories_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedRowIndex = e.RowIndex;
                txtCategoryName.Text = dgvCategories.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
        }

        private void ClearFields()
        {
            txtCategoryName.Clear();
            selectedRowIndex = -1;
        }

        private void dgvCategories_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
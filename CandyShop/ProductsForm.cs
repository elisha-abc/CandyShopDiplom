using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace CandyShop
{
    public partial class ProductsForm : Form
    {
        private int selectedProductId = -1;

        public ProductsForm()
        {
            InitializeComponent();
        }

        private void ProductsForm_Load(object sender, EventArgs e)
        {
            this.Text = "Товары";
            this.StartPosition = FormStartPosition.CenterScreen;

            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.MultiSelect = false;
            dgvProducts.ReadOnly = true;
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSupplier.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUnit.DropDownStyle = ComboBoxStyle.DropDownList;

            LoadCategories();
            LoadSuppliers();
            LoadUnits();
            LoadProducts();
        }

        private void LoadCategories()
        {
            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = "SELECT Id, Name FROM Categories";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        cmbCategory.DataSource = table;
                        cmbCategory.DisplayMember = "Name";
                        cmbCategory.ValueMember = "Id";
                        cmbCategory.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки категорий: " + ex.Message);
            }
        }

        private void LoadSuppliers()
        {
            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = "SELECT Id, Name FROM Suppliers";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        cmbSupplier.DataSource = table;
                        cmbSupplier.DisplayMember = "Name";
                        cmbSupplier.ValueMember = "Id";
                        cmbSupplier.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки поставщиков: " + ex.Message);
            }
        }

        private void LoadUnits()
        {
            cmbUnit.Items.Clear();
            cmbUnit.Items.Add("шт");
            cmbUnit.Items.Add("кг");
            cmbUnit.Items.Add("упак");
            cmbUnit.SelectedIndex = -1;
        }

        private void LoadProducts()
        {
            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = @"
                        SELECT 
                            p.Id,
                            p.Name AS [Название],
                            c.Name AS [Категория],
                            s.Name AS [Поставщик],
                            p.Price AS [Цена],
                            p.Unit AS [Ед. изм.]
                        FROM Products p
                        LEFT JOIN Categories c ON p.CategoryId = c.Id
                        LEFT JOIN Suppliers s ON p.SupplierId = s.Id";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        dgvProducts.DataSource = table;
                        dgvProducts.Columns["Id"].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки товаров: " + ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = @"
                        INSERT INTO Products (Name, CategoryId, SupplierId, Price, Unit)
                        VALUES (@Name, @CategoryId, @SupplierId, @Price, @Unit)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                        command.Parameters.AddWithValue("@CategoryId", cmbCategory.SelectedValue);
                        command.Parameters.AddWithValue("@SupplierId", cmbSupplier.SelectedValue);
                        command.Parameters.AddWithValue("@Price", decimal.Parse(txtPrice.Text.Trim()));
                        command.Parameters.AddWithValue("@Unit", cmbUnit.Text);

                        command.ExecuteNonQuery();
                    }
                }

                LoadProducts();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления товара: " + ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selectedProductId < 0)
            {
                MessageBox.Show("Выберите товар для изменения.");
                return;
            }

            if (!ValidateInputs())
                return;

            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = @"
                        UPDATE Products
                        SET Name = @Name,
                            CategoryId = @CategoryId,
                            SupplierId = @SupplierId,
                            Price = @Price,
                            Unit = @Unit
                        WHERE Id = @Id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                        command.Parameters.AddWithValue("@CategoryId", cmbCategory.SelectedValue);
                        command.Parameters.AddWithValue("@SupplierId", cmbSupplier.SelectedValue);
                        command.Parameters.AddWithValue("@Price", decimal.Parse(txtPrice.Text.Trim()));
                        command.Parameters.AddWithValue("@Unit", cmbUnit.Text);
                        command.Parameters.AddWithValue("@Id", selectedProductId);

                        command.ExecuteNonQuery();
                    }
                }

                LoadProducts();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка изменения товара: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedProductId < 0)
            {
                MessageBox.Show("Выберите товар для удаления.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Удалить выбранный товар?",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = "DELETE FROM Products WHERE Id = @Id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", selectedProductId);
                        command.ExecuteNonQuery();
                    }
                }

                LoadProducts();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка удаления товара: " + ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            try
            {
                DataGridViewRow row = dgvProducts.Rows[e.RowIndex];
                selectedProductId = Convert.ToInt32(row.Cells["Id"].Value);

                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = "SELECT * FROM Products WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", selectedProductId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtName.Text = reader["Name"].ToString();
                                cmbCategory.SelectedValue = Convert.ToInt32(reader["CategoryId"]);
                                cmbSupplier.SelectedValue = Convert.ToInt32(reader["SupplierId"]);
                                txtPrice.Text = reader["Price"].ToString();
                                cmbUnit.Text = reader["Unit"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка выбора товара: " + ex.Message);
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                cmbCategory.SelectedIndex == -1 ||
                cmbSupplier.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(txtPrice.Text) ||
                string.IsNullOrWhiteSpace(cmbUnit.Text))
            {
                MessageBox.Show("Заполните все поля.");
                return false;
            }

            if (!decimal.TryParse(txtPrice.Text.Trim(), out decimal price) || price < 0)
            {
                MessageBox.Show("Введите корректную цену.");
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
            selectedProductId = -1;
            txtName.Focus();
        }
    }
}
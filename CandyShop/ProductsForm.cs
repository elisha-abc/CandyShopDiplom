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
            dgvProducts.RowHeadersVisible = false;

            dgvProducts.BackgroundColor = System.Drawing.Color.White;
            dgvProducts.BorderStyle = BorderStyle.None;
            dgvProducts.GridColor = System.Drawing.Color.FromArgb(230, 230, 230);

            dgvProducts.EnableHeadersVisualStyles = false;
            dgvProducts.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            dgvProducts.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvProducts.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dgvProducts.ColumnHeadersHeight = 35;

            dgvProducts.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
            dgvProducts.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            dgvProducts.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            dgvProducts.RowTemplate.Height = 30;

            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSupplier.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUnit.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFilterCategory.DropDownStyle = ComboBoxStyle.DropDownList;

            StyleButton(btnAdd, System.Drawing.Color.FromArgb(52, 152, 219), System.Drawing.Color.White);
            StyleButton(btnEdit, System.Drawing.Color.White, System.Drawing.Color.FromArgb(40, 40, 40));
            StyleButton(btnDelete, System.Drawing.Color.White, System.Drawing.Color.FromArgb(40, 40, 40));
            StyleButton(btnClear, System.Drawing.Color.White, System.Drawing.Color.FromArgb(40, 40, 40));
            StyleButton(btnSearch, System.Drawing.Color.White, System.Drawing.Color.FromArgb(40, 40, 40));
            StyleButton(btnResetFilter, System.Drawing.Color.White, System.Drawing.Color.FromArgb(40, 40, 40));

            LoadCategories();
            LoadSuppliers();
            LoadUnits();
            LoadFilterCategories();
            LoadProducts();
        }

        private void StyleButton(Button button, System.Drawing.Color backColor, System.Drawing.Color foreColor)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = backColor;
            button.ForeColor = foreColor;
            button.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            button.Cursor = Cursors.Hand;
        }

        private void LoadCategories()
        {
            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT Id, Name FROM Categories ORDER BY Name";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    cmbCategory.DataSource = table;
                    cmbCategory.DisplayMember = "Name";
                    cmbCategory.ValueMember = "Id";
                    cmbCategory.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки категорий: " + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LoadSuppliers()
        {
            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT Id, Name FROM Suppliers ORDER BY Name";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    cmbSupplier.DataSource = table;
                    cmbSupplier.DisplayMember = "Name";
                    cmbSupplier.ValueMember = "Id";
                    cmbSupplier.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки поставщиков: " + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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

        private void LoadFilterCategories()
        {
            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT Id, Name FROM Categories ORDER BY Name";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    DataRow row = table.NewRow();
                    row["Id"] = 0;
                    row["Name"] = "Все";
                    table.Rows.InsertAt(row, 0);

                    cmbFilterCategory.DataSource = table;
                    cmbFilterCategory.DisplayMember = "Name";
                    cmbFilterCategory.ValueMember = "Id";
                    cmbFilterCategory.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки фильтра категорий: " + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LoadProducts(string search = "", int categoryId = 0)
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
                    p.Unit AS [Ед. изм.],
                    ISNULL(SUM(w.Quantity), 0) AS [Остаток]
                FROM Products p
                LEFT JOIN Categories c ON p.CategoryId = c.Id
                LEFT JOIN Suppliers s ON p.SupplierId = s.Id
                LEFT JOIN Warehouse w ON p.Id = w.ProductId
                WHERE (@search = '' OR p.Name LIKE '%' + @search + '%')
                  AND (@cat = 0 OR p.CategoryId = @cat)
                GROUP BY 
                    p.Id, p.Name, c.Name, s.Name, p.Price, p.Unit
                ORDER BY p.Name";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@search", search);
                    cmd.Parameters.AddWithValue("@cat", categoryId);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    dgvProducts.DataSource = table;

                    if (dgvProducts.Columns["Id"] != null)
                        dgvProducts.Columns["Id"].Visible = false;

                    HighlightLowStock();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки товаров: " + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void HighlightLowStock()
        {
            foreach (DataGridViewRow row in dgvProducts.Rows)
            {
                if (row.Cells["Остаток"].Value == null)
                    continue;

                int stock = Convert.ToInt32(row.Cells["Остаток"].Value);

                row.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);

                if (stock == 0)
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 220, 220);
                }
                else if (stock <= 5)
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 245, 200);
                }
                else
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.White;
                }
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите название товара.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            if (cmbCategory.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите категорию.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                cmbCategory.Focus();
                return false;
            }

            if (cmbSupplier.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите поставщика.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                cmbSupplier.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show("Введите цену.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtPrice.Focus();
                return false;
            }

            if (!decimal.TryParse(txtPrice.Text.Trim(), out decimal price) || price <= 0)
            {
                MessageBox.Show("Введите корректную цену больше 0.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtPrice.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cmbUnit.Text))
            {
                MessageBox.Show("Выберите единицу измерения.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                cmbUnit.Focus();
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

                Logger.Add("Добавлен товар: " + txtName.Text.Trim());

                MessageBox.Show("Товар успешно добавлен.",
                    "Успех",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadProducts();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления товара: " + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selectedProductId < 0)
            {
                MessageBox.Show("Выберите товар для изменения.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
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

                Logger.Add("Изменен товар: " + txtName.Text.Trim());

                MessageBox.Show("Товар успешно изменён.",
                    "Успех",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadProducts();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка изменения товара: " + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedProductId < 0)
            {
                MessageBox.Show("Выберите товар для удаления.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
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

                    string checkWarehouseQuery = "SELECT COUNT(*) FROM Warehouse WHERE ProductId = @Id";
                    using (SqlCommand checkWarehouseCmd = new SqlCommand(checkWarehouseQuery, connection))
                    {
                        checkWarehouseCmd.Parameters.AddWithValue("@Id", selectedProductId);
                        int warehouseCount = Convert.ToInt32(checkWarehouseCmd.ExecuteScalar());

                        if (warehouseCount > 0)
                        {
                            MessageBox.Show("Нельзя удалить товар, так как он используется на складе.",
                                "Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string checkSalesQuery = "SELECT COUNT(*) FROM Sales WHERE ProductId = @Id";
                    using (SqlCommand checkSalesCmd = new SqlCommand(checkSalesQuery, connection))
                    {
                        checkSalesCmd.Parameters.AddWithValue("@Id", selectedProductId);
                        int salesCount = Convert.ToInt32(checkSalesCmd.ExecuteScalar());

                        if (salesCount > 0)
                        {
                            MessageBox.Show("Нельзя удалить товар, так как он используется в продажах.",
                                "Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string deleteQuery = "DELETE FROM Products WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Id", selectedProductId);
                        command.ExecuteNonQuery();
                    }
                }

                Logger.Add("Удален товар ID: " + selectedProductId);

                MessageBox.Show("Товар успешно удалён.",
                    "Успех",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadProducts();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка удаления товара: " + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string search = txtSearch.Text.Trim();
            int categoryId = 0;

            if (cmbFilterCategory.SelectedValue != null)
                categoryId = Convert.ToInt32(cmbFilterCategory.SelectedValue);

            LoadProducts(search, categoryId);
        }

        private void btnResetFilter_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cmbFilterCategory.SelectedIndex = 0;
            LoadProducts();
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
                MessageBox.Show("Ошибка выбора товара: " + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
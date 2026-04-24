using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace CandyShop
{
    public partial class SalesForm : Form
    {
        private int selectedSaleId = -1;

        public SalesForm()
        {
            InitializeComponent();
        }

        private void SalesForm_Load(object sender, EventArgs e)
        {
            this.Text = "Продажи";
            this.StartPosition = FormStartPosition.CenterScreen;

            dgvSales.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSales.MultiSelect = false;
            dgvSales.ReadOnly = true;
            dgvSales.AllowUserToAddRows = false;
            dgvSales.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSales.RowHeadersVisible = false;
            dgvSales.AutoGenerateColumns = true;

            cmbProduct.DropDownStyle = ComboBoxStyle.DropDownList;

            LoadProducts();
            LoadSales();

            lblStockValue.Text = "0";
        }

        private void LoadProducts()
        {
            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = "SELECT Id, Name FROM Products ORDER BY Name";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    cmbProduct.DataSource = table;
                    cmbProduct.DisplayMember = "Name";
                    cmbProduct.ValueMember = "Id";
                    cmbProduct.SelectedIndex = -1;
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

        private void LoadSales()
        {
            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = @"
                        SELECT 
                            s.Id,
                            p.Name AS [Товар],
                            s.Quantity AS [Количество],
                            s.SaleDate AS [Дата продажи]
                        FROM Sales s
                        JOIN Products p ON s.ProductId = p.Id
                        ORDER BY s.SaleDate DESC";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    dgvSales.DataSource = null;
                    dgvSales.Columns.Clear();
                    dgvSales.DataSource = table;

                    if (dgvSales.Columns["Id"] != null)
                        dgvSales.Columns["Id"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки продаж: " + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private bool ValidateInputs()
        {
            if (cmbProduct.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите товар.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                cmbProduct.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtQuantity.Text))
            {
                MessageBox.Show("Введите количество.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtQuantity.Focus();
                return false;
            }

            if (!int.TryParse(txtQuantity.Text.Trim(), out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Введите корректное количество больше 0.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtQuantity.Focus();
                return false;
            }

            return true;
        }

        private int GetStockQuantity(int productId)
        {
            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = "SELECT ISNULL(SUM(Quantity), 0) FROM Warehouse WHERE ProductId = @ProductId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", productId);
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        private void UpdateStockLabel()
        {
            try
            {
                if (cmbProduct.SelectedIndex == -1 || cmbProduct.SelectedValue == null)
                {
                    lblStockValue.Text = "0";
                    return;
                }

                int productId = Convert.ToInt32(cmbProduct.SelectedValue);
                int stock = GetStockQuantity(productId);
                lblStockValue.Text = stock.ToString();
            }
            catch
            {
                lblStockValue.Text = "0";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            int productId = Convert.ToInt32(cmbProduct.SelectedValue);
            int quantity = int.Parse(txtQuantity.Text.Trim());

            try
            {
                int stockQuantity = GetStockQuantity(productId);

                if (stockQuantity < quantity)
                {
                    MessageBox.Show("Недостаточно товара на складе.",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        string insertSaleQuery = @"
                            INSERT INTO Sales (ProductId, Quantity, SaleDate)
                            VALUES (@ProductId, @Quantity, @SaleDate)";

                        using (SqlCommand saleCommand = new SqlCommand(insertSaleQuery, connection, transaction))
                        {
                            saleCommand.Parameters.AddWithValue("@ProductId", productId);
                            saleCommand.Parameters.AddWithValue("@Quantity", quantity);
                            saleCommand.Parameters.AddWithValue("@SaleDate", dtpSaleDate.Value.Date);
                            saleCommand.ExecuteNonQuery();
                        }

                        string updateWarehouseQuery = @"
                            UPDATE Warehouse
                            SET Quantity = Quantity - @Quantity
                            WHERE ProductId = @ProductId";

                        using (SqlCommand warehouseCommand = new SqlCommand(updateWarehouseQuery, connection, transaction))
                        {
                            warehouseCommand.Parameters.AddWithValue("@Quantity", quantity);
                            warehouseCommand.Parameters.AddWithValue("@ProductId", productId);
                            warehouseCommand.ExecuteNonQuery();
                        }

                        transaction.Commit();

                        MessageBox.Show("Продажа успешно добавлена.",
                            "Успех",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Ошибка при продаже: " + ex.Message,
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }
                }

                LoadSales();
                UpdateStockLabel();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления продажи: " + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedSaleId < 0)
            {
                MessageBox.Show("Выберите продажу для удаления.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Удалить выбранную продажу? Количество товара будет возвращено на склад.",
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
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        int productId;
                        int quantity;

                        string getSaleQuery = "SELECT ProductId, Quantity FROM Sales WHERE Id = @Id";

                        using (SqlCommand getSaleCommand = new SqlCommand(getSaleQuery, connection, transaction))
                        {
                            getSaleCommand.Parameters.AddWithValue("@Id", selectedSaleId);

                            using (SqlDataReader reader = getSaleCommand.ExecuteReader())
                            {
                                if (!reader.Read())
                                {
                                    MessageBox.Show("Продажа не найдена.",
                                        "Ошибка",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                                    return;
                                }

                                productId = Convert.ToInt32(reader["ProductId"]);
                                quantity = Convert.ToInt32(reader["Quantity"]);
                            }
                        }

                        string deleteSaleQuery = "DELETE FROM Sales WHERE Id = @Id";

                        using (SqlCommand deleteCommand = new SqlCommand(deleteSaleQuery, connection, transaction))
                        {
                            deleteCommand.Parameters.AddWithValue("@Id", selectedSaleId);
                            deleteCommand.ExecuteNonQuery();
                        }

                        string returnToWarehouseQuery = @"
                    UPDATE Warehouse
                    SET Quantity = Quantity + @Quantity
                    WHERE ProductId = @ProductId";

                        using (SqlCommand warehouseCommand = new SqlCommand(returnToWarehouseQuery, connection, transaction))
                        {
                            warehouseCommand.Parameters.AddWithValue("@Quantity", quantity);
                            warehouseCommand.Parameters.AddWithValue("@ProductId", productId);
                            warehouseCommand.ExecuteNonQuery();
                        }

                        transaction.Commit();

                        MessageBox.Show("Продажа удалена, товар возвращён на склад.",
                            "Успех",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        MessageBox.Show("Ошибка удаления продажи: " + ex.Message,
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }
                }

                LoadSales();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения к БД: " + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void dgvSales_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            try
            {
                DataGridViewRow row = dgvSales.Rows[e.RowIndex];
                selectedSaleId = Convert.ToInt32(row.Cells["Id"].Value);

                cmbProduct.Text = row.Cells["Товар"].Value.ToString();
                txtQuantity.Text = row.Cells["Количество"].Value.ToString();
                dtpSaleDate.Value = Convert.ToDateTime(row.Cells["Дата продажи"].Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка выбора продажи: " + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateStockLabel();
        }

        private void ClearFields()
        {
            cmbProduct.SelectedIndex = -1;
            txtQuantity.Clear();
            selectedSaleId = -1;
            dtpSaleDate.Value = DateTime.Now;
            lblStockValue.Text = "0";
        }
    }
}
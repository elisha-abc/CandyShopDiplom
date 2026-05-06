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

            this.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);

            dgvSales.BackgroundColor = System.Drawing.Color.White;
            dgvSales.BorderStyle = BorderStyle.None;
            dgvSales.GridColor = System.Drawing.Color.FromArgb(230, 230, 230);

            dgvSales.EnableHeadersVisualStyles = false;
            dgvSales.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            dgvSales.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvSales.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dgvSales.ColumnHeadersHeight = 35;

            dgvSales.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
            dgvSales.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            dgvSales.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            dgvSales.RowTemplate.Height = 30;

            StyleButton(btnAdd, System.Drawing.Color.FromArgb(52, 152, 219), System.Drawing.Color.White);
            StyleButton(btnDelete, System.Drawing.Color.White, System.Drawing.Color.FromArgb(40, 40, 40));
            StyleButton(btnClear, System.Drawing.Color.White, System.Drawing.Color.FromArgb(40, 40, 40));
            cmbProduct.DropDownStyle = ComboBoxStyle.DropDownList;

            LoadProducts();
            LoadSales();

            lblStockValue.Text = "0";
            dtpSaleDate.Value = DateTime.Now;
            dtpSaleDate.Enabled = false;
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

                string query = @"
            SELECT ISNULL(SUM(Quantity), 0)
            FROM Warehouse
            WHERE ProductId = @ProductId
              AND Quantity > 0
              AND ExpiryDate >= CAST(GETDATE() AS date)";

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
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        int availableQuantity = 0;

                        string stockQuery = @"
                    SELECT ISNULL(SUM(Quantity), 0)
                    FROM Warehouse
                    WHERE ProductId = @ProductId
                      AND Quantity > 0
                      AND ExpiryDate >= CAST(GETDATE() AS date)";

                        using (SqlCommand cmd = new SqlCommand(stockQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@ProductId", productId);
                            availableQuantity = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        if (availableQuantity < quantity)
                        {
                            MessageBox.Show("Недостаточно непросроченного товара на складе.",
                                "Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                            transaction.Rollback();
                            return;
                        }

                        string insertSaleQuery = @"
                    INSERT INTO Sales (ProductId, Quantity, SaleDate)
                    VALUES (@ProductId, @Quantity, @SaleDate)";

                        using (SqlCommand saleCommand = new SqlCommand(insertSaleQuery, connection, transaction))
                        {
                            saleCommand.Parameters.AddWithValue("@ProductId", productId);
                            saleCommand.Parameters.AddWithValue("@Quantity", quantity);
                            saleCommand.Parameters.AddWithValue("@SaleDate", DateTime.Now.Date);
                            saleCommand.ExecuteNonQuery();
                        }

                        int remainingToWriteOff = quantity;

                        string batchesQuery = @"
                    SELECT Id, Quantity
                    FROM Warehouse
                    WHERE ProductId = @ProductId
                      AND Quantity > 0
                      AND ExpiryDate >= CAST(GETDATE() AS date)
                    ORDER BY ExpiryDate ASC, ReceiptDate ASC";

                        using (SqlCommand batchCommand = new SqlCommand(batchesQuery, connection, transaction))
                        {
                            batchCommand.Parameters.AddWithValue("@ProductId", productId);

                            DataTable batches = new DataTable();

                            using (SqlDataAdapter adapter = new SqlDataAdapter(batchCommand))
                            {
                                adapter.Fill(batches);
                            }

                            foreach (DataRow row in batches.Rows)
                            {
                                if (remainingToWriteOff <= 0)
                                    break;

                                int warehouseId = Convert.ToInt32(row["Id"]);
                                int batchQuantity = Convert.ToInt32(row["Quantity"]);

                                int quantityFromBatch = Math.Min(batchQuantity, remainingToWriteOff);

                                string updateBatchQuery = @"
                            UPDATE Warehouse
                            SET Quantity = Quantity - @Quantity
                            WHERE Id = @WarehouseId";

                                using (SqlCommand updateCommand = new SqlCommand(updateBatchQuery, connection, transaction))
                                {
                                    updateCommand.Parameters.AddWithValue("@Quantity", quantityFromBatch);
                                    updateCommand.Parameters.AddWithValue("@WarehouseId", warehouseId);
                                    updateCommand.ExecuteNonQuery();
                                }

                                remainingToWriteOff -= quantityFromBatch;
                            }
                        }

                        string deleteEmptyQuery = "DELETE FROM Warehouse WHERE Quantity <= 0";

                        using (SqlCommand deleteCommand = new SqlCommand(deleteEmptyQuery, connection, transaction))
                        {
                            deleteCommand.ExecuteNonQuery();
                        }

                        transaction.Commit();

                        string productName = cmbProduct.Text;
                        Logger.Add("Добавлена продажа: " + productName + ", количество: " + quantity);

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

                        string insertBackQuery = @"
INSERT INTO Warehouse (ProductId, Quantity, ReceiptDate, ExpiryDate)
VALUES (@ProductId, @Quantity, @ReceiptDate, @ExpiryDate)";

                        using (SqlCommand warehouseCommand = new SqlCommand(insertBackQuery, connection, transaction))
                        {
                            warehouseCommand.Parameters.AddWithValue("@ProductId", productId);
                            warehouseCommand.Parameters.AddWithValue("@Quantity", quantity);
                            warehouseCommand.Parameters.AddWithValue("@ReceiptDate", DateTime.Now.Date);
                            warehouseCommand.Parameters.AddWithValue("@ExpiryDate", DateTime.Now.Date.AddMonths(1)); 
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
using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace CandyShop
{
    public partial class SalesForm : Form
    {
        private int selectedId = -1;

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

            LoadProducts();
            LoadSales();
        }

        private void LoadProducts()
        {
            cmbProduct.DataSource = null;

            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = "SELECT Id, Name FROM Products";

                using (var adapter = new SqlDataAdapter(query, connection))
                {
                    var table = new System.Data.DataTable();
                    adapter.Fill(table);

                    cmbProduct.DataSource = table;
                    cmbProduct.DisplayMember = "Name";
                    cmbProduct.ValueMember = "Id";
                    cmbProduct.SelectedIndex = -1;
                }
            }
        }

        private void LoadSales()
        {
            dgvSales.Rows.Clear();

            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT s.Id, p.Name, s.Quantity, s.SaleDate
                    FROM Sales s
                    JOIN Products p ON s.ProductId = p.Id";

                using (var cmd = new SqlCommand(query, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dgvSales.Rows.Add(
                            reader["Id"],
                            reader["Name"],
                            reader["Quantity"],
                            Convert.ToDateTime(reader["SaleDate"]).ToShortDateString()
                        );
                    }
                }
            }
        }

        private bool ValidateInputs()
        {
            if (cmbProduct.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txtQuantity.Text))
            {
                MessageBox.Show("Заполните все поля.");
                return false;
            }

            if (!int.TryParse(txtQuantity.Text.Trim(), out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Введите корректное количество.");
                return false;
            }

            return true;
        }

        private int GetWarehouseQuantity(int productId)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = "SELECT ISNULL(SUM(Quantity), 0) FROM Warehouse WHERE ProductId = @productId";

                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@productId", productId);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            int productId = Convert.ToInt32(cmbProduct.SelectedValue);
            int quantity = int.Parse(txtQuantity.Text.Trim());

            int stockQuantity = GetWarehouseQuantity(productId);

            if (stockQuantity < quantity)
            {
                MessageBox.Show("Недостаточно товара на складе.");
                return;
            }

            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    string insertSale = @"
                        INSERT INTO Sales (ProductId, Quantity, SaleDate)
                        VALUES (@productId, @quantity, @saleDate)";

                    using (var cmd = new SqlCommand(insertSale, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@productId", productId);
                        cmd.Parameters.AddWithValue("@quantity", quantity);
                        cmd.Parameters.AddWithValue("@saleDate", dtpSaleDate.Value.Date);
                        cmd.ExecuteNonQuery();
                    }

                    string updateWarehouse = @"
                        UPDATE Warehouse
                        SET Quantity = Quantity - @quantity
                        WHERE ProductId = @productId";

                    using (var cmd = new SqlCommand(updateWarehouse, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@quantity", quantity);
                        cmd.Parameters.AddWithValue("@productId", productId);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    MessageBox.Show("Продажа добавлена.");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Ошибка продажи: " + ex.Message);
                    return;
                }
            }

            LoadSales();
            ClearFields();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Выберите продажу.");
                return;
            }

            if (MessageBox.Show("Удалить продажу?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = "DELETE FROM Sales WHERE Id = @id";

                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", selectedId);
                    cmd.ExecuteNonQuery();
                }
            }

            LoadSales();
            ClearFields();
        }

        private void dgvSales_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvSales.Rows[e.RowIndex];

                selectedId = Convert.ToInt32(row.Cells["colId"].Value);
                cmbProduct.Text = row.Cells["colProduct"].Value?.ToString();
                txtQuantity.Text = row.Cells["colQuantity"].Value?.ToString();
                dtpSaleDate.Value = Convert.ToDateTime(row.Cells["colSaleDate"].Value);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            cmbProduct.SelectedIndex = -1;
            txtQuantity.Clear();
            dtpSaleDate.Value = DateTime.Now;
            selectedId = -1;
        }
    }
}
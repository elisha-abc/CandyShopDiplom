using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace CandyShop
{
    public partial class WarehouseForm : Form
    {
        private int selectedId = -1;

        public WarehouseForm()
        {
            InitializeComponent();
        }

        private void WarehouseForm_Load(object sender, EventArgs e)
        {
            this.Text = "Склад";
            this.StartPosition = FormStartPosition.CenterScreen;

            dgvWarehouse.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvWarehouse.MultiSelect = false;
            dgvWarehouse.ReadOnly = true;
            dgvWarehouse.AllowUserToAddRows = false;
            dgvWarehouse.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvWarehouse.RowHeadersVisible = false;

            LoadProducts();
            LoadWarehouse();
        }

        // Загрузка товаров в ComboBox
        private void LoadProducts()
        {
            cmbProduct.Items.Clear();

            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = "SELECT Id, Name FROM Products";

                using (var cmd = new SqlCommand(query, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmbProduct.Items.Add(new
                        {
                            Id = reader["Id"],
                            Name = reader["Name"].ToString()
                        });
                    }
                }
            }

            cmbProduct.DisplayMember = "Name";
            cmbProduct.ValueMember = "Id";
        }

        // Загрузка склада
        private void LoadWarehouse()
        {
            dgvWarehouse.Rows.Clear();

            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT w.Id, p.Name, w.Quantity, w.ReceiptDate, w.ExpiryDate
                    FROM Warehouse w
                    JOIN Products p ON w.ProductId = p.Id";

                using (var cmd = new SqlCommand(query, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dgvWarehouse.Rows.Add(
                            reader["Id"],
                            reader["Name"],
                            reader["Quantity"],
                            Convert.ToDateTime(reader["ReceiptDate"]).ToShortDateString(),
                            Convert.ToDateTime(reader["ExpiryDate"]).ToShortDateString()
                        );
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbProduct.SelectedItem == null || string.IsNullOrWhiteSpace(txtQuantity.Text))
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            int productId = (int)cmbProduct.SelectedValue;
            int quantity = int.Parse(txtQuantity.Text);

            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    INSERT INTO Warehouse (ProductId, Quantity, ReceiptDate, ExpiryDate)
                    VALUES (@p, @q, @r, @e)";

                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@p", productId);
                    cmd.Parameters.AddWithValue("@q", quantity);
                    cmd.Parameters.AddWithValue("@r", dtpReceiptDate.Value);
                    cmd.Parameters.AddWithValue("@e", dtpExpiryDate.Value);

                    cmd.ExecuteNonQuery();
                }
            }

            LoadWarehouse();
            ClearFields();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Выберите запись");
                return;
            }

            int productId = (int)cmbProduct.SelectedValue;
            int quantity = int.Parse(txtQuantity.Text);

            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    UPDATE Warehouse 
                    SET ProductId=@p, Quantity=@q, ReceiptDate=@r, ExpiryDate=@e
                    WHERE Id=@id";

                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@p", productId);
                    cmd.Parameters.AddWithValue("@q", quantity);
                    cmd.Parameters.AddWithValue("@r", dtpReceiptDate.Value);
                    cmd.Parameters.AddWithValue("@e", dtpExpiryDate.Value);
                    cmd.Parameters.AddWithValue("@id", selectedId);

                    cmd.ExecuteNonQuery();
                }
            }

            LoadWarehouse();
            ClearFields();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Выберите запись");
                return;
            }

            if (MessageBox.Show("Удалить?", "Подтверждение",
                MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = "DELETE FROM Warehouse WHERE Id=@id";

                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", selectedId);
                    cmd.ExecuteNonQuery();
                }
            }

            LoadWarehouse();
            ClearFields();
        }

        private void dgvWarehouse_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvWarehouse.Rows[e.RowIndex];

                selectedId = Convert.ToInt32(row.Cells["colId"].Value);
                txtQuantity.Text = row.Cells["colQuantity"].Value.ToString();

                dtpReceiptDate.Value = Convert.ToDateTime(row.Cells["colReceipt"].Value);
                dtpExpiryDate.Value = Convert.ToDateTime(row.Cells["colExpiry"].Value);

                cmbProduct.Text = row.Cells["colProduct"].Value.ToString();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtQuantity.Clear();
            cmbProduct.SelectedIndex = -1;
            selectedId = -1;
        }
    }
}
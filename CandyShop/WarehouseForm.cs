using System;
using System.Data;
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

        private void LoadProducts()
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

        private void LoadWarehouse()
        {
            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT 
                        w.Id,
                        p.Name AS [Товар],
                        w.Quantity AS [Количество],
                        w.ReceiptDate AS [Дата поступления],
                        w.ExpiryDate AS [Срок годности]
                    FROM Warehouse w
                    JOIN Products p ON w.ProductId = p.Id
                    ORDER BY p.Name";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable table = new DataTable();
                adapter.Fill(table);

                dgvWarehouse.DataSource = table;

                if (dgvWarehouse.Columns["Id"] != null)
                    dgvWarehouse.Columns["Id"].Visible = false;
            }
        }

        private bool ValidateInputs()
        {
            if (cmbProduct.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите товар.");
                return false;
            }

            if (!int.TryParse(txtQuantity.Text.Trim(), out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Введите корректное количество больше 0.");
                return false;
            }

            if (dtpExpiryDate.Value.Date < dtpReceiptDate.Value.Date)
            {
                MessageBox.Show("Срок годности не может быть раньше даты поступления.");
                return false;
            }

            return true;
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

                    string query = @"
                        IF EXISTS (
                            SELECT 1 
                            FROM Warehouse 
                            WHERE ProductId = @ProductId 
                              AND ExpiryDate = @ExpiryDate
                        )
                        BEGIN
                            UPDATE Warehouse
                            SET Quantity = Quantity + @Quantity,
                                ReceiptDate = @ReceiptDate
                            WHERE ProductId = @ProductId 
                              AND ExpiryDate = @ExpiryDate
                        END
                        ELSE
                        BEGIN
                            INSERT INTO Warehouse (ProductId, Quantity, ReceiptDate, ExpiryDate)
                            VALUES (@ProductId, @Quantity, @ReceiptDate, @ExpiryDate)
                        END";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@ProductId", productId);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@ReceiptDate", dtpReceiptDate.Value.Date);
                        cmd.Parameters.AddWithValue("@ExpiryDate", dtpExpiryDate.Value.Date);

                        cmd.ExecuteNonQuery();
                    }
                }

                Logger.Add("Добавлено поступление товара на склад. Количество: " + quantity);

                LoadWarehouse();
                ClearFields();

                MessageBox.Show("Поступление успешно добавлено.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления на склад: " + ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Выберите запись.");
                return;
            }

            if (!ValidateInputs())
                return;

            int productId = Convert.ToInt32(cmbProduct.SelectedValue);
            int quantity = int.Parse(txtQuantity.Text.Trim());

            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = @"
                        UPDATE Warehouse 
                        SET ProductId = @ProductId,
                            Quantity = @Quantity,
                            ReceiptDate = @ReceiptDate,
                            ExpiryDate = @ExpiryDate
                        WHERE Id = @Id";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@ProductId", productId);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@ReceiptDate", dtpReceiptDate.Value.Date);
                        cmd.Parameters.AddWithValue("@ExpiryDate", dtpExpiryDate.Value.Date);
                        cmd.Parameters.AddWithValue("@Id", selectedId);

                        cmd.ExecuteNonQuery();
                    }
                }

                Logger.Add("Изменена запись склада ID: " + selectedId);

                LoadWarehouse();
                ClearFields();

                MessageBox.Show("Запись склада успешно изменена.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка изменения склада: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Выберите запись.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Удалить выбранную запись склада?",
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

                    string query = "DELETE FROM Warehouse WHERE Id = @Id";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", selectedId);
                        cmd.ExecuteNonQuery();
                    }
                }

                Logger.Add("Удалена запись склада ID: " + selectedId);

                LoadWarehouse();
                ClearFields();

                MessageBox.Show("Запись склада удалена.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка удаления склада: " + ex.Message);
            }
        }

        private void dgvWarehouse_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvWarehouse.Rows[e.RowIndex];

            selectedId = Convert.ToInt32(row.Cells["Id"].Value);
            txtQuantity.Text = row.Cells["Количество"].Value.ToString();

            dtpReceiptDate.Value = Convert.ToDateTime(row.Cells["Дата поступления"].Value);
            dtpExpiryDate.Value = Convert.ToDateTime(row.Cells["Срок годности"].Value);

            cmbProduct.Text = row.Cells["Товар"].Value.ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void btnImportWarehouse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel/CSV Files|*.xlsx;*.xls;*.csv";

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                string[] lines = System.IO.File.ReadAllLines(ofd.FileName, System.Text.Encoding.UTF8);

                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    for (int i = 1; i < lines.Length; i++)
                    {
                        if (string.IsNullOrWhiteSpace(lines[i]))
                            continue;

                        string[] parts = lines[i].Split(';');

                        string productName = parts[0].Trim();
                        int quantity = int.Parse(parts[1].Trim());
                        DateTime receiptDate = DateTime.Parse(parts[2].Trim());
                        DateTime expiryDate = DateTime.Parse(parts[3].Trim());

                        string query = @"
                    INSERT INTO Warehouse (ProductId, Quantity, ReceiptDate, ExpiryDate)
                    SELECT Id, @Quantity, @ReceiptDate, @ExpiryDate
                    FROM Products
                    WHERE Name = @ProductName";

                        using (SqlCommand cmd = new SqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@ProductName", productName);
                            cmd.Parameters.AddWithValue("@Quantity", quantity);
                            cmd.Parameters.AddWithValue("@ReceiptDate", receiptDate.Date);
                            cmd.Parameters.AddWithValue("@ExpiryDate", expiryDate.Date);

                            cmd.ExecuteNonQuery();
                        }

                        Logger.Add("Импорт поступления на склад: " + productName + ", количество: " + quantity);
                    }
                }

                LoadWarehouse();
                MessageBox.Show("Импорт поступлений завершён.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка импорта поступлений: " + ex.Message);
            }
        }

        private void ClearFields()
        {
            txtQuantity.Clear();
            cmbProduct.SelectedIndex = -1;
            selectedId = -1;
            dtpReceiptDate.Value = DateTime.Now;
            dtpExpiryDate.Value = DateTime.Now;
        }
    }
}
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

            dgvWarehouse.BackgroundColor = System.Drawing.Color.White;
            dgvWarehouse.BorderStyle = BorderStyle.None;
            dgvWarehouse.GridColor = System.Drawing.Color.FromArgb(230, 230, 230);

            dgvWarehouse.EnableHeadersVisualStyles = false;
            dgvWarehouse.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            dgvWarehouse.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvWarehouse.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dgvWarehouse.ColumnHeadersHeight = 35;

            dgvWarehouse.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
            dgvWarehouse.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            dgvWarehouse.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            dgvWarehouse.RowTemplate.Height = 30;

            StyleButton(btnAdd, System.Drawing.Color.FromArgb(52, 152, 219), System.Drawing.Color.White);
            StyleButton(btnEdit, System.Drawing.Color.White, System.Drawing.Color.FromArgb(40, 40, 40));
            StyleButton(btnDelete, System.Drawing.Color.White, System.Drawing.Color.FromArgb(40, 40, 40));
            StyleButton(btnClear, System.Drawing.Color.White, System.Drawing.Color.FromArgb(40, 40, 40));
            StyleButton(btnImportWarehouse, System.Drawing.Color.White, System.Drawing.Color.FromArgb(40, 40, 40));

            LoadProducts();
            LoadWarehouse();

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

        private void HighlightWarehouseRows()
        {
            foreach (DataGridViewRow row in dgvWarehouse.Rows)
            {
                if (row.Cells["Срок годности"].Value == null)
                    continue;

                DateTime expiryDate = Convert.ToDateTime(row.Cells["Срок годности"].Value);
                int daysLeft = (expiryDate.Date - DateTime.Now.Date).Days;

                row.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);

                if (daysLeft < 0)
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 220, 220);
                }
                else if (daysLeft <= 7)
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 245, 200);
                }
                else
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.White;
                }
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

                HighlightWarehouseRows();
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

            if (dtpReceiptDate.Value.Date > DateTime.Now.Date)
            {
                MessageBox.Show("Дата поступления не может быть в будущем.");
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
            ofd.Filter = "CSV Files|*.csv";

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

                        if (parts.Length < 8)
                        {
                            MessageBox.Show("Ошибка в строке " + (i + 1) + ". Должно быть 8 колонок.");
                            continue;
                        }

                        string productName = parts[0].Trim();
                        string categoryName = parts[1].Trim();
                        string supplierName = parts[2].Trim();
                        decimal price = decimal.Parse(parts[3].Trim());
                        string unit = parts[4].Trim();
                        int quantity = int.Parse(parts[5].Trim());
                        DateTime receiptDate = DateTime.Parse(parts[6].Trim());
                        DateTime expiryDate = DateTime.Parse(parts[7].Trim());

                        if (quantity <= 0)
                        {
                            MessageBox.Show("Ошибка в строке " + (i + 1) + ": количество должно быть больше 0.");
                            continue;
                        }

                        if (receiptDate.Date > DateTime.Now.Date)
                        {
                            MessageBox.Show("Ошибка в строке " + (i + 1) + ": дата поступления не может быть в будущем.");
                            continue;
                        }

                        if (expiryDate < receiptDate)
                        {
                            MessageBox.Show("Ошибка в строке " + (i + 1) + ": срок годности раньше даты поступления.");
                            continue;
                        }

                        int categoryId;
                        string categoryQuery = @"
IF NOT EXISTS (SELECT 1 FROM Categories WHERE Name = @Name)
    INSERT INTO Categories (Name) VALUES (@Name);

SELECT Id FROM Categories WHERE Name = @Name;";

                        using (SqlCommand cmd = new SqlCommand(categoryQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@Name", categoryName);
                            categoryId = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        int supplierId;
                        string supplierQuery = @"
IF NOT EXISTS (SELECT 1 FROM Suppliers WHERE Name = @Name)
    INSERT INTO Suppliers (Name) VALUES (@Name);

SELECT Id FROM Suppliers WHERE Name = @Name;";

                        using (SqlCommand cmd = new SqlCommand(supplierQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@Name", supplierName);
                            supplierId = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        int productId;
                        string productQuery = @"
IF NOT EXISTS (SELECT 1 FROM Products WHERE Name = @Name)
BEGIN
    INSERT INTO Products (Name, CategoryId, SupplierId, Price, Unit)
    VALUES (@Name, @CategoryId, @SupplierId, @Price, @Unit);
END
ELSE
BEGIN
    UPDATE Products
    SET CategoryId = @CategoryId,
        SupplierId = @SupplierId,
        Price = @Price,
        Unit = @Unit
    WHERE Name = @Name;
END

SELECT Id FROM Products WHERE Name = @Name;";

                        using (SqlCommand cmd = new SqlCommand(productQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@Name", productName);
                            cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                            cmd.Parameters.AddWithValue("@SupplierId", supplierId);
                            cmd.Parameters.AddWithValue("@Price", price);
                            cmd.Parameters.AddWithValue("@Unit", unit);

                            productId = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        string warehouseQuery = @"
INSERT INTO Warehouse (ProductId, Quantity, ReceiptDate, ExpiryDate)
VALUES (@ProductId, @Quantity, @ReceiptDate, @ExpiryDate);";

                        using (SqlCommand cmd = new SqlCommand(warehouseQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@ProductId", productId);
                            cmd.Parameters.AddWithValue("@Quantity", quantity);
                            cmd.Parameters.AddWithValue("@ReceiptDate", receiptDate.Date);
                            cmd.Parameters.AddWithValue("@ExpiryDate", expiryDate.Date);
                            cmd.ExecuteNonQuery();
                        }

                        Logger.Add("Импорт поступления: " + productName + ", количество: " + quantity);
                    }
                }

                LoadProducts();
                LoadWarehouse();

                MessageBox.Show("Импорт завершён.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка импорта: " + ex.Message);
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
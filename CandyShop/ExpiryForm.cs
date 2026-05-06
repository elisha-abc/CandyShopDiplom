using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace CandyShop
{
    public partial class ExpiryForm : Form
    {
        public ExpiryForm()
        {
            InitializeComponent();
        }

        private void ExpiryForm_Load(object sender, EventArgs e)
        {
            this.Text = "Сроки годности";
            this.StartPosition = FormStartPosition.CenterScreen;

            dgvExpiry.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvExpiry.RowHeadersVisible = false;
            dgvExpiry.ReadOnly = true;
            dgvExpiry.AllowUserToAddRows = false;

            numDays.Value = 7;

            LoadData();

            dgvExpiry.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvExpiry.MultiSelect = false;
            dgvExpiry.BackgroundColor = System.Drawing.Color.White;
            dgvExpiry.BorderStyle = BorderStyle.None;
            dgvExpiry.GridColor = System.Drawing.Color.FromArgb(230, 230, 230);

            dgvExpiry.EnableHeadersVisualStyles = false;
            dgvExpiry.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            dgvExpiry.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvExpiry.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dgvExpiry.ColumnHeadersHeight = 35;

            dgvExpiry.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
            dgvExpiry.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            dgvExpiry.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            dgvExpiry.RowTemplate.Height = 30;

            StyleButton(btnLoad, System.Drawing.Color.White, System.Drawing.Color.FromArgb(40, 40, 40));
            StyleButton(btnWriteOff, System.Drawing.Color.FromArgb(52, 152, 219), System.Drawing.Color.White);
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

        private void LoadData()
        {
            dgvExpiry.Rows.Clear();

            int days = (int)numDays.Value;

            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
            SELECT 
                p.Name,
                w.Quantity,
                w.ExpiryDate,
                DATEDIFF(day, CAST(GETDATE() AS date), w.ExpiryDate) AS DaysLeft
            FROM Warehouse w
            JOIN Products p ON w.ProductId = p.Id
            WHERE w.ExpiryDate <= DATEADD(day, @days, CAST(GETDATE() AS date))
            ORDER BY w.ExpiryDate";

                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@days", days);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int daysLeft = Convert.ToInt32(reader["DaysLeft"]);

                            string status;

                            if (daysLeft < 0)
                                status = "Просрочен";
                            else if (daysLeft <= 7)
                                status = "Скоро истекает";
                            else
                                status = "Годен";

                            dgvExpiry.Rows.Add(
                                reader["Name"],
                                reader["Quantity"],
                                Convert.ToDateTime(reader["ExpiryDate"]).ToShortDateString(),
                                daysLeft,
                                status
                            );
                        }
                    }
                }
            }

            HighlightExpiryRows();
        }

        private void HighlightExpiryRows()
        {
            foreach (DataGridViewRow row in dgvExpiry.Rows)
            {
                if (row.Cells[3].Value == null)
                    continue;

                int daysLeft = Convert.ToInt32(row.Cells[3].Value);

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

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnWriteOff_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Списать все просроченные товары?",
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

                    // 1. Получаем просроченные товары (для логов)
                    string selectQuery = @"
                SELECT p.Name, w.Quantity
                FROM Warehouse w
                JOIN Products p ON w.ProductId = p.Id
                WHERE w.ExpiryDate < CAST(GETDATE() AS date)";

                    using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string name = reader["Name"].ToString();
                                int quantity = Convert.ToInt32(reader["Quantity"]);

                                Logger.Add("Списан просроченный товар: " + name + ", количество: " + quantity);
                            }
                        }
                    }

                    // 2. Удаляем просроченные записи
                    string deleteQuery = @"
                DELETE FROM Warehouse
                WHERE ExpiryDate < CAST(GETDATE() AS date)";

                    using (SqlCommand cmd = new SqlCommand(deleteQuery, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                LoadData(); // или твой метод загрузки таблицы

                MessageBox.Show("Просроченные товары списаны.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка списания: " + ex.Message);
            }
        }
    }
}
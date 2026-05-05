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
                        DATEDIFF(day, GETDATE(), w.ExpiryDate) AS DaysLeft
                    FROM Warehouse w
                    JOIN Products p ON w.ProductId = p.Id
                    WHERE w.ExpiryDate <= DATEADD(day, @days, GETDATE())
                    ORDER BY w.ExpiryDate";

                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@days", days);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dgvExpiry.Rows.Add(
                                reader["Name"],
                                reader["Quantity"],
                                Convert.ToDateTime(reader["ExpiryDate"]).ToShortDateString(),
                                reader["DaysLeft"]
                            );
                        }
                    }
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
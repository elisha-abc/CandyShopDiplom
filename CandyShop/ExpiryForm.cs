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
    }
}
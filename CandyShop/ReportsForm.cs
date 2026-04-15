using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace CandyShop
{
    public partial class ReportsForm : Form
    {
        public ReportsForm()
        {
            InitializeComponent();
        }

        private void ReportsForm_Load(object sender, EventArgs e)
        {
            this.Text = "Отчеты";
            this.StartPosition = FormStartPosition.CenterScreen;

            dgvReports.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReports.RowHeadersVisible = false;
            dgvReports.ReadOnly = true;
            dgvReports.AllowUserToAddRows = false;
            dgvReports.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReports.MultiSelect = false;

            dtpDateFrom.Value = DateTime.Now.AddMonths(-1);
            dtpDateTo.Value = DateTime.Now;

            LoadReport();
        }

        private void LoadReport()
        {
            dgvReports.Rows.Clear();

            int totalSales = 0;
            decimal totalRevenue = 0;

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT 
                        p.Name AS ProductName,
                        SUM(s.Quantity) AS TotalQuantity,
                        SUM(s.Quantity * p.Price) AS TotalRevenue
                    FROM Sales s
                    JOIN Products p ON s.ProductId = p.Id
                    WHERE s.SaleDate BETWEEN @dateFrom AND @dateTo
                    GROUP BY p.Name
                    ORDER BY TotalQuantity DESC";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@dateFrom", dtpDateFrom.Value.Date);
                    cmd.Parameters.AddWithValue("@dateTo", dtpDateTo.Value.Date);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int quantity = Convert.ToInt32(reader["TotalQuantity"]);
                            decimal revenue = Convert.ToDecimal(reader["TotalRevenue"]);

                            dgvReports.Rows.Add(
                                reader["ProductName"],
                                quantity,
                                revenue.ToString("0.00")
                            );

                            totalSales += quantity;
                            totalRevenue += revenue;
                        }
                    }
                }
            }

            lblTotalSalesValue.Text = totalSales.ToString();
            lblTotalRevenueValue.Text = totalRevenue.ToString("0.00") + " руб.";
        }

        private void btnLoadReport_Click(object sender, EventArgs e)
        {
            if (dtpDateFrom.Value.Date > dtpDateTo.Value.Date)
            {
                MessageBox.Show("Дата 'с' не может быть больше даты 'по'.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            LoadReport();
        }

        private void btnClearReport_Click(object sender, EventArgs e)
        {
            dtpDateFrom.Value = DateTime.Now.AddMonths(-1);
            dtpDateTo.Value = DateTime.Now;

            dgvReports.Rows.Clear();
            lblTotalSalesValue.Text = "0";
            lblTotalRevenueValue.Text = "0 руб.";
        }
    }
}
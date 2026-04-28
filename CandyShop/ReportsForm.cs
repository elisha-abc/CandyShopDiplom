using System;
using System.Data;
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

            dgvReports.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReports.MultiSelect = false;
            dgvReports.ReadOnly = true;
            dgvReports.AllowUserToAddRows = false;
            dgvReports.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReports.RowHeadersVisible = false;
            dgvReports.AutoGenerateColumns = true;

            dtpDateFrom.Value = DateTime.Now.AddMonths(-1);
            dtpDateTo.Value = DateTime.Now;

            LoadReport();
        }

        private void LoadReport()
        {
            try
            {
                dgvReports.DataSource = null;
                dgvReports.Columns.Clear();

                int totalQuantity = 0;
                decimal totalRevenue = 0;

                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = @"
                        SELECT 
                            p.Name AS [Товар],
                            SUM(s.Quantity) AS [Продано],
                            SUM(s.Quantity * p.Price) AS [Выручка]
                        FROM Sales s
                        JOIN Products p ON s.ProductId = p.Id
                        WHERE s.SaleDate BETWEEN @DateFrom AND @DateTo
                        GROUP BY p.Name
                        ORDER BY SUM(s.Quantity) DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DateFrom", dtpDateFrom.Value.Date);
                        command.Parameters.AddWithValue("@DateTo", dtpDateTo.Value.Date);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        dgvReports.DataSource = table;

                        foreach (DataRow row in table.Rows)
                        {
                            totalQuantity += Convert.ToInt32(row["Продано"]);
                            totalRevenue += Convert.ToDecimal(row["Выручка"]);
                        }
                    }
                }

                lblTotalSalesValue.Text = totalQuantity.ToString();
                lblTotalRevenueValue.Text = totalRevenue.ToString("0.00") + " руб.";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки отчета: " + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnLoadReport_Click(object sender, EventArgs e)
        {
            if (dtpDateFrom.Value.Date > dtpDateTo.Value.Date)
            {
                MessageBox.Show("Дата начала не может быть больше даты окончания.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            LoadReport();
            Logger.Add("Сформирован отчет по продажам");
        }

        private void btnClearReport_Click(object sender, EventArgs e)
        {
            dtpDateFrom.Value = DateTime.Now.AddMonths(-1);
            dtpDateTo.Value = DateTime.Now;

            dgvReports.DataSource = null;
            dgvReports.Columns.Clear();

            lblTotalSalesValue.Text = "0";
            lblTotalRevenueValue.Text = "0 руб.";
        }
    }
}
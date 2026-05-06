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
            this.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);

            dgvReports.Columns.Clear();
            dgvReports.AutoGenerateColumns = true;
            dgvReports.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReports.MultiSelect = false;
            dgvReports.ReadOnly = true;
            dgvReports.AllowUserToAddRows = false;
            dgvReports.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReports.RowHeadersVisible = false;

            StyleGrid();

            dtpDateFrom.Value = DateTime.Now.AddMonths(-1);
            dtpDateTo.Value = DateTime.Now;

            LoadProductsFilter();
            LoadReportByProducts();
        }

        private void StyleGrid()
        {
            dgvReports.BackgroundColor = System.Drawing.Color.White;
            dgvReports.BorderStyle = BorderStyle.None;
            dgvReports.GridColor = System.Drawing.Color.FromArgb(230, 230, 230);

            dgvReports.EnableHeadersVisualStyles = false;
            dgvReports.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            dgvReports.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvReports.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dgvReports.ColumnHeadersHeight = 35;

            dgvReports.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
            dgvReports.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            dgvReports.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            dgvReports.RowTemplate.Height = 30;
        }

        private void LoadProductsFilter()
        {
            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = "SELECT Id, Name FROM Products ORDER BY Name";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable table = new DataTable();
                adapter.Fill(table);

                DataRow row = table.NewRow();
                row["Id"] = 0;
                row["Name"] = "Все";
                table.Rows.InsertAt(row, 0);

                cmbProductFilter.DataSource = table;
                cmbProductFilter.DisplayMember = "Name";
                cmbProductFilter.ValueMember = "Id";
                cmbProductFilter.SelectedIndex = 0;
            }
        }

        private void LoadReportByProducts()
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
                          AND (@ProductId = 0 OR p.Id = @ProductId)
                        GROUP BY p.Name
                        ORDER BY SUM(s.Quantity) DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DateFrom", dtpDateFrom.Value.Date);
                        command.Parameters.AddWithValue("@DateTo", dtpDateTo.Value.Date);
                        command.Parameters.AddWithValue("@ProductId", cmbProductFilter.SelectedValue);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        dgvReports.DataSource = table;
                        FormatReportGrid();

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
                MessageBox.Show("Ошибка загрузки отчета: " + ex.Message);
            }
        }

        private void LoadReportByCategories()
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
                            c.Name AS [Категория],
                            SUM(s.Quantity) AS [Продано],
                            SUM(s.Quantity * p.Price) AS [Выручка]
                        FROM Sales s
                        JOIN Products p ON s.ProductId = p.Id
                        JOIN Categories c ON p.CategoryId = c.Id
                        WHERE s.SaleDate BETWEEN @DateFrom AND @DateTo
                        GROUP BY c.Name
                        ORDER BY SUM(s.Quantity) DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DateFrom", dtpDateFrom.Value.Date);
                        command.Parameters.AddWithValue("@DateTo", dtpDateTo.Value.Date);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        dgvReports.DataSource = table;
                        FormatReportGrid();

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
                MessageBox.Show("Ошибка загрузки отчета по категориям: " + ex.Message);
            }
        }

        private void FormatReportGrid()
        {
            foreach (DataGridViewRow row in dgvReports.Rows)
            {
                row.DefaultCellStyle.BackColor = System.Drawing.Color.White;
                row.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
            }

            if (dgvReports.Columns["Продано"] != null)
                dgvReports.Columns["Продано"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            if (dgvReports.Columns["Выручка"] != null)
            {
                dgvReports.Columns["Выручка"].DefaultCellStyle.Format = "0.00 руб.";
                dgvReports.Columns["Выручка"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void btnLoadReport_Click(object sender, EventArgs e)
        {
            if (dtpDateFrom.Value.Date > dtpDateTo.Value.Date)
            {
                MessageBox.Show("Дата начала не может быть больше даты окончания.");
                return;
            }

            LoadReportByProducts();
            Logger.Add("Сформирован отчет по продажам");
        }

        private void btnClearReport_Click(object sender, EventArgs e)
        {
            dtpDateFrom.Value = DateTime.Now.AddMonths(-1);
            dtpDateTo.Value = DateTime.Now;
            cmbProductFilter.SelectedIndex = 0;

            dgvReports.DataSource = null;
            dgvReports.Columns.Clear();

            lblTotalSalesValue.Text = "0";
            lblTotalRevenueValue.Text = "0 руб.";
        }

        private void btnCategoryReport_Click(object sender, EventArgs e)
        {
            LoadReportByCategories();
            Logger.Add("Сформирован отчет по категориям");
        }
    }
}
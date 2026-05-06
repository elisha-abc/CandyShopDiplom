using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace CandyShop
{
    public partial class LogsForm : Form
    {
        public LogsForm()
        {
            InitializeComponent();
        }

        private void LogsForm_Load(object sender, EventArgs e)
        {
            this.Text = "Журнал действий";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);

            dgvLogs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLogs.MultiSelect = false;
            dgvLogs.ReadOnly = true;
            dgvLogs.AllowUserToAddRows = false;
            dgvLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLogs.RowHeadersVisible = false;

            dgvLogs.BackgroundColor = System.Drawing.Color.White;
            dgvLogs.BorderStyle = BorderStyle.None;
            dgvLogs.GridColor = System.Drawing.Color.FromArgb(230, 230, 230);

            dgvLogs.EnableHeadersVisualStyles = false;
            dgvLogs.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            dgvLogs.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvLogs.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dgvLogs.ColumnHeadersHeight = 35;

            dgvLogs.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
            dgvLogs.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            dgvLogs.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            dgvLogs.RowTemplate.Height = 30;

            LoadLogs();
        }


        private void LoadLogs()
        {
            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = @"
                        SELECT 
                            UserLogin AS [Пользователь],
                            Action AS [Действие],
                            ActionDate AS [Дата и время]
                        FROM Logs
                        ORDER BY ActionDate DESC";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        dgvLogs.DataSource = table;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки журнала: " + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadLogs();
        }
    }
}
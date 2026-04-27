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

            dgvLogs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLogs.MultiSelect = false;
            dgvLogs.ReadOnly = true;
            dgvLogs.AllowUserToAddRows = false;
            dgvLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLogs.RowHeadersVisible = false;

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
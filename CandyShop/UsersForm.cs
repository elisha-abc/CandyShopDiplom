using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace CandyShop
{
    public partial class UsersForm : Form
    {
        private int selectedId = -1;

        public UsersForm()
        {
            InitializeComponent();
        }

        private void UsersForm_Load(object sender, EventArgs e)
        {
            this.Text = "Пользователи";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);

            cmbRole.Items.Clear();
            cmbRole.Items.Add("Администратор");
            cmbRole.Items.Add("Кассир");
            cmbRole.SelectedIndex = -1;
            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;

            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.MultiSelect = false;
            dgvUsers.ReadOnly = true;
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsers.RowHeadersVisible = false;

            dgvUsers.BackgroundColor = System.Drawing.Color.White;
            dgvUsers.BorderStyle = BorderStyle.None;
            dgvUsers.GridColor = System.Drawing.Color.FromArgb(230, 230, 230);

            dgvUsers.EnableHeadersVisualStyles = false;
            dgvUsers.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            dgvUsers.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvUsers.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dgvUsers.ColumnHeadersHeight = 35;

            dgvUsers.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
            dgvUsers.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            dgvUsers.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            dgvUsers.RowTemplate.Height = 30;

            StyleButton(btnAdd, System.Drawing.Color.FromArgb(52, 152, 219), System.Drawing.Color.White);
            StyleButton(btnEdit, System.Drawing.Color.White, System.Drawing.Color.FromArgb(40, 40, 40));
            StyleButton(btnDelete, System.Drawing.Color.White, System.Drawing.Color.FromArgb(40, 40, 40));
            StyleButton(btnClear, System.Drawing.Color.White, System.Drawing.Color.FromArgb(40, 40, 40));

            LoadUsers();
        }

        private void LoadUsers()
        {
            dgvUsers.Rows.Clear();

            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = "SELECT Id, Login, Password, Role FROM Users";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dgvUsers.Rows.Add(
                                reader["Id"],
                                reader["Login"],
                                reader["Password"],
                                reader["Role"]
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки пользователей: " + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error); 
            }
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLogin.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(cmbRole.Text))
            {
                MessageBox.Show("Заполните все поля.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = "INSERT INTO Users (Login, Password, Role) VALUES (@login, @password, @role)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@login", txtLogin.Text.Trim());
                        command.Parameters.AddWithValue("@password", txtPassword.Text.Trim());
                        command.Parameters.AddWithValue("@role", cmbRole.Text);

                        command.ExecuteNonQuery();
                    }
                }

                LoadUsers();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления пользователя: " + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Выберите пользователя.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtLogin.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(cmbRole.Text))
            {
                MessageBox.Show("Заполните все поля.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = "UPDATE Users SET Login=@login, Password=@password, Role=@role WHERE Id=@id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@login", txtLogin.Text.Trim());
                        command.Parameters.AddWithValue("@password", txtPassword.Text.Trim());
                        command.Parameters.AddWithValue("@role", cmbRole.Text);
                        command.Parameters.AddWithValue("@id", selectedId);

                        command.ExecuteNonQuery();
                    }
                }

                LoadUsers();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка изменения пользователя: " + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Выберите пользователя.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Удалить выбранного пользователя?",
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

                    string query = "DELETE FROM Users WHERE Id=@id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", selectedId);
                        command.ExecuteNonQuery();
                    }
                }

                LoadUsers();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка удаления пользователя: " + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvUsers.Rows[e.RowIndex];

                selectedId = Convert.ToInt32(row.Cells[0].Value);
                txtLogin.Text = row.Cells[1].Value.ToString();
                txtPassword.Text = row.Cells[2].Value.ToString();
                cmbRole.Text = row.Cells[3].Value.ToString();
            }
        }

        private void ClearFields()
        {
            txtLogin.Clear();
            txtPassword.Clear();
            cmbRole.SelectedIndex = -1;
            selectedId = -1;
            txtLogin.Focus();
        }
    }
}
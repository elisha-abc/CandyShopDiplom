using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace CandyShop
{
    public partial class CategoriesForm : Form
    {
        private int selectedId = -1;

        public CategoriesForm()
        {
            InitializeComponent();
        }

        private void CategoriesForm_Load(object sender, EventArgs e)
        {
            this.Text = "Категории";
            this.StartPosition = FormStartPosition.CenterScreen;

            dgvCategories.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCategories.MultiSelect = false;
            dgvCategories.ReadOnly = true;
            dgvCategories.AllowUserToAddRows = false;
            dgvCategories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCategories.RowHeadersVisible = false;

            dgvCategories.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCategories.MultiSelect = false;
            dgvCategories.ReadOnly = true;
            dgvCategories.AllowUserToAddRows = false;
            dgvCategories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCategories.RowHeadersVisible = false;

            dgvCategories.BackgroundColor = System.Drawing.Color.White;
            dgvCategories.BorderStyle = BorderStyle.None;
            dgvCategories.GridColor = System.Drawing.Color.FromArgb(230, 230, 230);


            dgvCategories.EnableHeadersVisualStyles = false;
            dgvCategories.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            dgvCategories.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvCategories.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dgvCategories.ColumnHeadersHeight = 35;

            foreach (DataGridViewColumn column in dgvCategories.Columns)
            {
                column.HeaderCell.Style.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
                column.HeaderCell.Style.ForeColor = System.Drawing.Color.White;
                column.HeaderCell.Style.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            }

            dgvCategories.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
            dgvCategories.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            dgvCategories.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            dgvCategories.RowTemplate.Height = 30;

            StyleButton(btnAdd, System.Drawing.Color.FromArgb(52, 152, 219), System.Drawing.Color.White);
            StyleButton(btnEdit, System.Drawing.Color.White, System.Drawing.Color.FromArgb(40, 40, 40));
            StyleButton(btnDelete, System.Drawing.Color.White, System.Drawing.Color.FromArgb(40, 40, 40));
            StyleButton(btnClear, System.Drawing.Color.White, System.Drawing.Color.FromArgb(40, 40, 40));

            LoadCategories();
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

        private void HighlightCategories()
        {
            foreach (DataGridViewRow row in dgvCategories.Rows)
            {
                row.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            }
        }

        private void LoadCategories()
        {
            dgvCategories.Rows.Clear();

            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = "SELECT Id, Name FROM Categories ORDER BY Name";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dgvCategories.Rows.Add(
                                reader["Id"],
                                reader["Name"]
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки категорий: " + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                HighlightCategories();
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                MessageBox.Show("Введите название категории.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtCategoryName.Focus();
                return false;
            }

            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = "INSERT INTO Categories (Name) VALUES (@name)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", txtCategoryName.Text.Trim());
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Категория успешно добавлена.",
                    "Успех",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadCategories();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления категории: " + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Выберите категорию для изменения.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs())
                return;

            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = "UPDATE Categories SET Name = @name WHERE Id = @id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", txtCategoryName.Text.Trim());
                        command.Parameters.AddWithValue("@id", selectedId);
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Категория успешно изменена.",
                    "Успех",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadCategories();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка изменения категории: " + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Выберите категорию для удаления.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Удалить выбранную категорию?",
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

                    string checkQuery = "SELECT COUNT(*) FROM Products WHERE CategoryId = @id";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@id", selectedId);
                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show("Нельзя удалить категорию, так как она используется в товарах.",
                                "Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string deleteQuery = "DELETE FROM Categories WHERE Id = @id";
                    using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@id", selectedId);
                        deleteCommand.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Категория успешно удалена.",
                    "Успех",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadCategories();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка удаления категории: " + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void dgvCategories_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvCategories.Rows[e.RowIndex];

                selectedId = Convert.ToInt32(row.Cells["colId"].Value);
                txtCategoryName.Text = row.Cells["colName"].Value?.ToString();
            }
        }

        private void ClearFields()
        {
            txtCategoryName.Clear();
            selectedId = -1;
            txtCategoryName.Focus();
        }
    }
}
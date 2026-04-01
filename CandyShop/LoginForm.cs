using System;
using System.Windows.Forms;

namespace CandyShop
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;

            this.Text = "Авторизация";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AcceptButton = button1;

            txtLogin.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Введите логин и пароль.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (login == "admin" && password == "12345")
            {
                MainForm mainForm = new MainForm();
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }
    }
}
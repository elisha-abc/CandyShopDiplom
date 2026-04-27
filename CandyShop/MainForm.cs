using System;
using System.Windows.Forms;

namespace CandyShop
{
    public partial class MainForm : Form
    {
        private string userRole;

        
        public MainForm(string role)
        {
            InitializeComponent();
            userRole = role;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = "Главное меню";
            this.StartPosition = FormStartPosition.CenterScreen;

            MessageBox.Show("Роль пользователя: " + userRole);

            if (userRole == "Кассир")
            {
                btnCategories.Visible = false;
                btnSuppliers.Visible = false;
                btnUsers.Visible = false;
                btnReports.Visible = false;
                btnLogs.Visible = false;
            }
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            ProductsForm form = new ProductsForm();
            form.ShowDialog();
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            CategoriesForm form = new CategoriesForm();
            form.ShowDialog();
        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            SuppliersForm form = new SuppliersForm();
            form.ShowDialog();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            UsersForm form = new UsersForm();
            form.ShowDialog();
        }

        private void btnWarehouse_Click(object sender, EventArgs e)
        {
            WarehouseForm form = new WarehouseForm();
            form.ShowDialog();
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            SalesForm form = new SalesForm();
            form.ShowDialog();
        }

        private void btnExpiry_Click(object sender, EventArgs e)
        {
            ExpiryForm form = new ExpiryForm();
            form.ShowDialog();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            ReportsForm form = new ReportsForm();
            form.ShowDialog();
        }

        private void btnLogs_Click(object sender, EventArgs e)
        {
            LogsForm form = new LogsForm();
            form.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Вы действительно хотите выйти из системы?",
                "Подтверждение выхода",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
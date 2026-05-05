using System;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;


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
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.Size = new Size(760, 520);

            // Убираем всплывающее окно с ролью
            // MessageBox.Show("Роль пользователя: " + userRole);

            Button[] allButtons =
            {
        btnProducts,
        btnWarehouse,
        btnSales,
        btnExpiry,
        btnCategories,
        btnSuppliers,
        btnUsers,
        btnReports,
        btnLogs
    };

            foreach (Button btn in allButtons)
            {
                btn.Visible = true;
                btn.Width = 190;
                btn.Height = 55;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.BackColor = Color.White;
                btn.ForeColor = Color.FromArgb(35, 35, 35);
                btn.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
                btn.Cursor = Cursors.Hand;
            }

            btnExit.Width = 190;
            btnExit.Height = 45;
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.FlatAppearance.BorderSize = 0;
            btnExit.BackColor = Color.FromArgb(220, 53, 69);
            btnExit.ForeColor = Color.White;
            btnExit.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            btnExit.Cursor = Cursors.Hand;

            if (userRole == "Кассир")
            {
                btnCategories.Visible = false;
                btnSuppliers.Visible = false;
                btnUsers.Visible = false;
                btnReports.Visible = false;
                btnLogs.Visible = false;
            }

            ArrangeButtons();
        }

        private void ArrangeButtons()
        {
            Button[] visibleButtons =
            {
        btnProducts,
        btnWarehouse,
        btnSales,
        btnExpiry,
        btnCategories,
        btnSuppliers,
        btnUsers,
        btnReports,
        btnLogs
    };

            int buttonWidth = 190;
            int buttonHeight = 55;
            int gapX = 35;
            int gapY = 25;
            int columns = 3;

            var buttons = visibleButtons.Where(b => b.Visible).ToArray();

            int totalWidth = columns * buttonWidth + (columns - 1) * gapX;
            int startX = (this.ClientSize.Width - totalWidth) / 2;
            int startY = 105;

            for (int i = 0; i < buttons.Length; i++)
            {
                int row = i / columns;
                int col = i % columns;

                int x = startX + col * (buttonWidth + gapX);
                int y = startY + row * (buttonHeight + gapY);

                buttons[i].Location = new Point(x, y);
            }

            btnExit.Location = new Point(
                this.ClientSize.Width - btnExit.Width - 35,
                this.ClientSize.Height - btnExit.Height - 35
            );
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
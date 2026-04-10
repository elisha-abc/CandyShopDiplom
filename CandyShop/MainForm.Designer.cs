namespace CandyShop
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitle = new Label();
            btnProducts = new Button();
            btnCategories = new Button();
            btnSuppliers = new Button();
            btnUsers = new Button();
            btnWarehouse = new Button();
            btnSales = new Button();
            btnExpiry = new Button();
            btnReports = new Button();
            btnExit = new Button();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(22, 18);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(87, 15);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Главное меню";
            // 
            // btnProducts
            // 
            btnProducts.Location = new Point(22, 53);
            btnProducts.Name = "btnProducts";
            btnProducts.Size = new Size(177, 41);
            btnProducts.TabIndex = 1;
            btnProducts.Text = "Товары";
            btnProducts.UseVisualStyleBackColor = true;
            btnProducts.Click += btnProducts_Click;
            // 
            // btnCategories
            // 
            btnCategories.Location = new Point(22, 134);
            btnCategories.Name = "btnCategories";
            btnCategories.Size = new Size(177, 41);
            btnCategories.TabIndex = 2;
            btnCategories.Text = "Категории";
            btnCategories.UseVisualStyleBackColor = true;
            btnCategories.Click += btnCategories_Click;
            // 
            // btnSuppliers
            // 
            btnSuppliers.Location = new Point(22, 218);
            btnSuppliers.Name = "btnSuppliers";
            btnSuppliers.Size = new Size(177, 41);
            btnSuppliers.TabIndex = 3;
            btnSuppliers.Text = "Поставщики";
            btnSuppliers.UseVisualStyleBackColor = true;
            btnSuppliers.Click += btnSuppliers_Click;
            // 
            // btnUsers
            // 
            btnUsers.Location = new Point(22, 292);
            btnUsers.Name = "btnUsers";
            btnUsers.Size = new Size(177, 41);
            btnUsers.TabIndex = 4;
            btnUsers.Text = "Пользователи";
            btnUsers.UseVisualStyleBackColor = true;
            btnUsers.Click += btnUsers_Click;
            // 
            // btnWarehouse
            // 
            btnWarehouse.Location = new Point(259, 53);
            btnWarehouse.Name = "btnWarehouse";
            btnWarehouse.Size = new Size(177, 41);
            btnWarehouse.TabIndex = 5;
            btnWarehouse.Text = "Склад";
            btnWarehouse.UseVisualStyleBackColor = true;
            btnWarehouse.Click += btnWarehouse_Click;
            // 
            // btnSales
            // 
            btnSales.Location = new Point(259, 134);
            btnSales.Name = "btnSales";
            btnSales.Size = new Size(177, 41);
            btnSales.TabIndex = 6;
            btnSales.Text = "Продажи";
            btnSales.UseVisualStyleBackColor = true;
            btnSales.Click += btnSales_Click;
            // 
            // btnExpiry
            // 
            btnExpiry.Location = new Point(259, 218);
            btnExpiry.Name = "btnExpiry";
            btnExpiry.Size = new Size(177, 41);
            btnExpiry.TabIndex = 7;
            btnExpiry.Text = "Сроки годности";
            btnExpiry.UseVisualStyleBackColor = true;
            btnExpiry.Click += btnExpiry_Click;
            // 
            // btnReports
            // 
            btnReports.Location = new Point(259, 292);
            btnReports.Name = "btnReports";
            btnReports.Size = new Size(177, 41);
            btnReports.TabIndex = 8;
            btnReports.Text = "Отчёты";
            btnReports.UseVisualStyleBackColor = true;
            btnReports.Click += btnReports_Click;
            // 
            // btnExit
            // 
            btnExit.Location = new Point(516, 380);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(177, 41);
            btnExit.TabIndex = 9;
            btnExit.Text = "Выход";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(734, 450);
            Controls.Add(btnExit);
            Controls.Add(btnReports);
            Controls.Add(btnExpiry);
            Controls.Add(btnSales);
            Controls.Add(btnWarehouse);
            Controls.Add(btnUsers);
            Controls.Add(btnSuppliers);
            Controls.Add(btnCategories);
            Controls.Add(btnProducts);
            Controls.Add(lblTitle);
            Name = "MainForm";
            Text = "MainForm";
            Load += MainForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Button btnProducts;
        private Button btnCategories;
        private Button btnSuppliers;
        private Button btnUsers;
        private Button btnWarehouse;
        private Button btnSales;
        private Button btnExpiry;
        private Button btnReports;
        private Button btnExit;
    }
}
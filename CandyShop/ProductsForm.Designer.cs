namespace CandyShop
{
    partial class ProductsForm
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
            txtProductName = new Label();
            labelCategory = new Label();
            labelSupplier = new Label();
            cmbCategory = new ComboBox();
            labelPrice = new Label();
            labelUnit = new Label();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            dgvProducts = new DataGridView();
            txtName = new TextBox();
            txtPrice = new TextBox();
            cmbSupplier = new ComboBox();
            cmbUnit = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(24, 25);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(129, 15);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Управление товарами";
            // 
            // txtProductName
            // 
            txtProductName.AutoSize = true;
            txtProductName.Location = new Point(82, 63);
            txtProductName.Name = "txtProductName";
            txtProductName.Size = new Size(102, 15);
            txtProductName.TabIndex = 1;
            txtProductName.Text = "Название товара:";
            // 
            // labelCategory
            // 
            labelCategory.AutoSize = true;
            labelCategory.Location = new Point(82, 101);
            labelCategory.Name = "labelCategory";
            labelCategory.Size = new Size(66, 15);
            labelCategory.TabIndex = 2;
            labelCategory.Text = "Категория:";
            // 
            // labelSupplier
            // 
            labelSupplier.AutoSize = true;
            labelSupplier.Location = new Point(82, 139);
            labelSupplier.Name = "labelSupplier";
            labelSupplier.Size = new Size(73, 15);
            labelSupplier.TabIndex = 3;
            labelSupplier.Text = "Поставщик:";
            // 
            // cmbCategory
            // 
            cmbCategory.FormattingEnabled = true;
            cmbCategory.Location = new Point(199, 98);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(192, 23);
            cmbCategory.TabIndex = 4;
            // 
            // labelPrice
            // 
            labelPrice.AutoSize = true;
            labelPrice.Location = new Point(82, 175);
            labelPrice.Name = "labelPrice";
            labelPrice.Size = new Size(38, 15);
            labelPrice.TabIndex = 5;
            labelPrice.Text = "Цена:";
            // 
            // labelUnit
            // 
            labelUnit.AutoSize = true;
            labelUnit.Location = new Point(82, 213);
            labelUnit.Name = "labelUnit";
            labelUnit.Size = new Size(88, 15);
            labelUnit.TabIndex = 6;
            labelUnit.Text = "Ед. измерения:";
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(24, 265);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(204, 43);
            btnAdd.TabIndex = 7;
            btnAdd.Text = "Добавить";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(264, 265);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(204, 43);
            btnEdit.TabIndex = 8;
            btnEdit.Text = "Изменить";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(499, 265);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(204, 43);
            btnDelete.TabIndex = 9;
            btnDelete.Text = "Удалить";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(743, 265);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(204, 43);
            btnClear.TabIndex = 10;
            btnClear.Text = "Очистить";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // dgvProducts
            // 
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProducts.Location = new Point(9, 322);
            dgvProducts.Name = "dgvProducts";
            dgvProducts.Size = new Size(964, 232);
            dgvProducts.TabIndex = 11;
            dgvProducts.CellClick += dgvProducts_CellClick;
            // 
            // txtName
            // 
            txtName.Location = new Point(199, 60);
            txtName.Name = "txtName";
            txtName.Size = new Size(192, 23);
            txtName.TabIndex = 12;
            // 
            // txtPrice
            // 
            txtPrice.Location = new Point(199, 172);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(192, 23);
            txtPrice.TabIndex = 13;
            // 
            // cmbSupplier
            // 
            cmbSupplier.FormattingEnabled = true;
            cmbSupplier.Location = new Point(199, 136);
            cmbSupplier.Name = "cmbSupplier";
            cmbSupplier.Size = new Size(192, 23);
            cmbSupplier.TabIndex = 14;
            // 
            // cmbUnit
            // 
            cmbUnit.FormattingEnabled = true;
            cmbUnit.Location = new Point(199, 210);
            cmbUnit.Name = "cmbUnit";
            cmbUnit.Size = new Size(192, 23);
            cmbUnit.TabIndex = 15;
            // 
            // ProductsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 561);
            Controls.Add(cmbUnit);
            Controls.Add(cmbSupplier);
            Controls.Add(txtPrice);
            Controls.Add(txtName);
            Controls.Add(dgvProducts);
            Controls.Add(btnClear);
            Controls.Add(btnDelete);
            Controls.Add(btnEdit);
            Controls.Add(btnAdd);
            Controls.Add(labelUnit);
            Controls.Add(labelPrice);
            Controls.Add(cmbCategory);
            Controls.Add(labelSupplier);
            Controls.Add(labelCategory);
            Controls.Add(txtProductName);
            Controls.Add(lblTitle);
            Name = "ProductsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Товары";
            Load += ProductsForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label txtProductName;
        private Label labelCategory;
        private Label labelSupplier;
        private ComboBox cmbCategory;
        private Label labelPrice;
        private Label labelUnit;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnClear;
        private DataGridView dgvProducts;
        private TextBox txtName;
        private TextBox txtPrice;
        private ComboBox cmbSupplier;
        private ComboBox cmbUnit;
    }
}
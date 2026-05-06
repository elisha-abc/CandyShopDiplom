namespace CandyShop
{
    partial class WarehouseForm
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
            lblProduct = new Label();
            lblQuantity = new Label();
            lblReceiptDate = new Label();
            lblExpiryDate = new Label();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            dgvWarehouse = new DataGridView();
            cmbProduct = new ComboBox();
            txtQuantity = new TextBox();
            dtpReceiptDate = new DateTimePicker();
            dtpExpiryDate = new DateTimePicker();
            btnImportWarehouse = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvWarehouse).BeginInit();
            SuspendLayout();
            // 
            // lblProduct
            // 
            lblProduct.AutoSize = true;
            lblProduct.Location = new Point(42, 26);
            lblProduct.Name = "lblProduct";
            lblProduct.Size = new Size(43, 15);
            lblProduct.TabIndex = 0;
            lblProduct.Text = "Товар:";
            // 
            // lblQuantity
            // 
            lblQuantity.AutoSize = true;
            lblQuantity.Location = new Point(42, 65);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(75, 15);
            lblQuantity.TabIndex = 1;
            lblQuantity.Text = "Количество:";
            // 
            // lblReceiptDate
            // 
            lblReceiptDate.AutoSize = true;
            lblReceiptDate.Location = new Point(42, 106);
            lblReceiptDate.Name = "lblReceiptDate";
            lblReceiptDate.Size = new Size(109, 15);
            lblReceiptDate.TabIndex = 2;
            lblReceiptDate.Text = "Дата поступления:";
            // 
            // lblExpiryDate
            // 
            lblExpiryDate.AutoSize = true;
            lblExpiryDate.Location = new Point(42, 148);
            lblExpiryDate.Name = "lblExpiryDate";
            lblExpiryDate.Size = new Size(91, 15);
            lblExpiryDate.TabIndex = 3;
            lblExpiryDate.Text = "Срок годности:";
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(23, 182);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(166, 52);
            btnAdd.TabIndex = 4;
            btnAdd.Text = "Добавить ";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(217, 182);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(166, 52);
            btnEdit.TabIndex = 5;
            btnEdit.Text = "Изменить";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(417, 182);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(166, 52);
            btnDelete.TabIndex = 6;
            btnDelete.Text = "Удалитить";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(611, 182);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(166, 52);
            btnClear.TabIndex = 7;
            btnClear.Text = "Очистить";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // dgvWarehouse
            // 
            dgvWarehouse.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvWarehouse.Location = new Point(18, 242);
            dgvWarehouse.Name = "dgvWarehouse";
            dgvWarehouse.Size = new Size(772, 200);
            dgvWarehouse.TabIndex = 8;
            dgvWarehouse.CellClick += dgvWarehouse_CellClick;
            // 
            // cmbProduct
            // 
            cmbProduct.FormattingEnabled = true;
            cmbProduct.Location = new Point(157, 23);
            cmbProduct.Name = "cmbProduct";
            cmbProduct.Size = new Size(318, 23);
            cmbProduct.TabIndex = 9;
            // 
            // txtQuantity
            // 
            txtQuantity.Location = new Point(157, 62);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(318, 23);
            txtQuantity.TabIndex = 10;
            // 
            // dtpReceiptDate
            // 
            dtpReceiptDate.Location = new Point(157, 100);
            dtpReceiptDate.Name = "dtpReceiptDate";
            dtpReceiptDate.Size = new Size(318, 23);
            dtpReceiptDate.TabIndex = 11;
            // 
            // dtpExpiryDate
            // 
            dtpExpiryDate.Location = new Point(157, 142);
            dtpExpiryDate.Name = "dtpExpiryDate";
            dtpExpiryDate.Size = new Size(318, 23);
            dtpExpiryDate.TabIndex = 12;
            // 
            // btnImportWarehouse
            // 
            btnImportWarehouse.Location = new Point(534, 62);
            btnImportWarehouse.Name = "btnImportWarehouse";
            btnImportWarehouse.Size = new Size(219, 74);
            btnImportWarehouse.TabIndex = 13;
            btnImportWarehouse.Text = "Импорт поступления";
            btnImportWarehouse.UseVisualStyleBackColor = true;
            btnImportWarehouse.Click += btnImportWarehouse_Click;
            // 
            // WarehouseForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnImportWarehouse);
            Controls.Add(dtpExpiryDate);
            Controls.Add(dtpReceiptDate);
            Controls.Add(txtQuantity);
            Controls.Add(cmbProduct);
            Controls.Add(dgvWarehouse);
            Controls.Add(btnClear);
            Controls.Add(btnDelete);
            Controls.Add(btnEdit);
            Controls.Add(btnAdd);
            Controls.Add(lblExpiryDate);
            Controls.Add(lblReceiptDate);
            Controls.Add(lblQuantity);
            Controls.Add(lblProduct);
            Name = "WarehouseForm";
            Text = "WarehouseForm";
            Load += WarehouseForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvWarehouse).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblProduct;
        private Label lblQuantity;
        private Label lblReceiptDate;
        private Label lblExpiryDate;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnClear;
        private DataGridView dgvWarehouse;
        private ComboBox cmbProduct;
        private TextBox txtQuantity;
        private DateTimePicker dtpReceiptDate;
        private DateTimePicker dtpExpiryDate;
        private Button btnImportWarehouse;
    }
}
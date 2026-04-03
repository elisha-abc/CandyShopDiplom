namespace CandyShop
{
    partial class SuppliersForm
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
            dgvSuppliers = new DataGridView();
            btnClear = new Button();
            btnDelete = new Button();
            btnEdit = new Button();
            btnAdd = new Button();
            txtSupplierName = new TextBox();
            lblSupplierName = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvSuppliers).BeginInit();
            SuspendLayout();
            // 
            // dgvSuppliers
            // 
            dgvSuppliers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSuppliers.Location = new Point(13, 163);
            dgvSuppliers.Name = "dgvSuppliers";
            dgvSuppliers.Size = new Size(775, 258);
            dgvSuppliers.TabIndex = 13;
            dgvSuppliers.CellClick += dgvSuppliers_CellClick;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(619, 107);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(152, 44);
            btnClear.TabIndex = 12;
            btnClear.Text = "Очистить";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(425, 107);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(152, 44);
            btnDelete.TabIndex = 11;
            btnDelete.Text = "Удалить";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(229, 107);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(152, 44);
            btnEdit.TabIndex = 10;
            btnEdit.Text = "Изменить";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(37, 107);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(152, 44);
            btnAdd.TabIndex = 9;
            btnAdd.Text = "Добавить";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // txtSupplierName
            // 
            txtSupplierName.Location = new Point(176, 29);
            txtSupplierName.Name = "txtSupplierName";
            txtSupplierName.Size = new Size(226, 23);
            txtSupplierName.TabIndex = 8;
            // 
            // lblSupplierName
            // 
            lblSupplierName.AutoSize = true;
            lblSupplierName.Location = new Point(39, 32);
            lblSupplierName.Name = "lblSupplierName";
            lblSupplierName.Size = new Size(132, 15);
            lblSupplierName.TabIndex = 7;
            lblSupplierName.Text = "Название поставщика:";
            // 
            // SuppliersForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvSuppliers);
            Controls.Add(btnClear);
            Controls.Add(btnDelete);
            Controls.Add(btnEdit);
            Controls.Add(btnAdd);
            Controls.Add(txtSupplierName);
            Controls.Add(lblSupplierName);
            Name = "SuppliersForm";
            Text = "Поставщик";
            Load += SuppliersForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvSuppliers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvSuppliers;
        private Button btnClear;
        private Button btnDelete;
        private Button btnEdit;
        private Button btnAdd;
        private TextBox txtSupplierName;
        private Label lblSupplierName;
    }
}
namespace CandyShop
{
    partial class CategoriesForm
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
            lblCategoryName = new Label();
            txtCategoryName = new TextBox();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            dgvCategories = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvCategories).BeginInit();
            SuspendLayout();
            // 
            // lblCategoryName
            // 
            lblCategoryName.AutoSize = true;
            lblCategoryName.Location = new Point(38, 53);
            lblCategoryName.Name = "lblCategoryName";
            lblCategoryName.Size = new Size(121, 15);
            lblCategoryName.TabIndex = 0;
            lblCategoryName.Text = "Название категории:";
            // 
            // txtCategoryName
            // 
            txtCategoryName.Location = new Point(175, 50);
            txtCategoryName.Name = "txtCategoryName";
            txtCategoryName.Size = new Size(226, 23);
            txtCategoryName.TabIndex = 1;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(36, 128);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(152, 44);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "Добавить";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(228, 128);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(152, 44);
            btnEdit.TabIndex = 3;
            btnEdit.Text = "Изменить";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(424, 128);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(152, 44);
            btnDelete.TabIndex = 4;
            btnDelete.Text = "Удалить";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(618, 128);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(152, 44);
            btnClear.TabIndex = 5;
            btnClear.Text = "Очистить";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // dgvCategories
            // 
            dgvCategories.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCategories.Columns.AddRange(new DataGridViewColumn[] { colId, colName });
            dgvCategories.Location = new Point(12, 184);
            dgvCategories.Name = "dgvCategories";
            dgvCategories.Size = new Size(775, 258);
            dgvCategories.TabIndex = 6;
            dgvCategories.CellClick += dgvCategories_CellClick;
            // 
            // colId
            // 
            colId.HeaderText = "Id";
            colId.Name = "colId";
            colId.Visible = false;
            // 
            // colName
            // 
            colName.HeaderText = "Название категории";
            colName.Name = "colName";
            // 
            // CategoriesForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvCategories);
            Controls.Add(btnClear);
            Controls.Add(btnDelete);
            Controls.Add(btnEdit);
            Controls.Add(btnAdd);
            Controls.Add(txtCategoryName);
            Controls.Add(lblCategoryName);
            Name = "CategoriesForm";
            Text = "Управление категориями";
            Load += CategoriesForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCategories).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblCategoryName;
        private TextBox txtCategoryName;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnClear;
        private DataGridView dgvCategories;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colName;
    }
}
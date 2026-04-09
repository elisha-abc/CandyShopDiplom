namespace CandyShop
{
    partial class UsersForm
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
            label1 = new Label();
            label2 = new Label();
            txtLogin = new TextBox();
            txtPassword = new TextBox();
            label3 = new Label();
            cmbRole = new ComboBox();
            btnClear = new Button();
            btnDelete = new Button();
            btnEdit = new Button();
            btnAdd = new Button();
            dgvUsers = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colLogin = new DataGridViewTextBoxColumn();
            colPassword = new DataGridViewTextBoxColumn();
            colRole = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(77, 45);
            label1.Name = "label1";
            label1.Size = new Size(44, 15);
            label1.TabIndex = 0;
            label1.Text = "Логин:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(77, 93);
            label2.Name = "label2";
            label2.Size = new Size(52, 15);
            label2.TabIndex = 1;
            label2.Text = "Пароль:";
            // 
            // txtLogin
            // 
            txtLogin.Location = new Point(140, 42);
            txtLogin.Name = "txtLogin";
            txtLogin.Size = new Size(260, 23);
            txtLogin.TabIndex = 2;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(140, 90);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(260, 23);
            txtPassword.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(77, 137);
            label3.Name = "label3";
            label3.Size = new Size(37, 15);
            label3.TabIndex = 4;
            label3.Text = "Роль:";
            // 
            // cmbRole
            // 
            cmbRole.FormattingEnabled = true;
            cmbRole.Location = new Point(140, 134);
            cmbRole.Name = "cmbRole";
            cmbRole.Size = new Size(260, 23);
            cmbRole.TabIndex = 5;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(560, 201);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(143, 41);
            btnClear.TabIndex = 14;
            btnClear.Text = "Очистить";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(378, 201);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(143, 41);
            btnDelete.TabIndex = 13;
            btnDelete.Text = "Удалить";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(192, 201);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(143, 41);
            btnEdit.TabIndex = 12;
            btnEdit.Text = "Изменить";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(12, 201);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(143, 41);
            btnAdd.TabIndex = 11;
            btnAdd.Text = "Добавить";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // dgvUsers
            // 
            dgvUsers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsers.Columns.AddRange(new DataGridViewColumn[] { colId, colLogin, colPassword, colRole });
            dgvUsers.Location = new Point(11, 255);
            dgvUsers.Name = "dgvUsers";
            dgvUsers.RowHeadersVisible = false;
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.Size = new Size(709, 233);
            dgvUsers.TabIndex = 15;
            dgvUsers.CellClick += dgvUsers_CellClick;
            // 
            // colId
            // 
            colId.HeaderText = "Id";
            colId.Name = "colId";
            colId.Visible = false;
            // 
            // colLogin
            // 
            colLogin.HeaderText = "Логин";
            colLogin.Name = "colLogin";
            // 
            // colPassword
            // 
            colPassword.HeaderText = "Пароль";
            colPassword.Name = "colPassword";
            // 
            // colRole
            // 
            colRole.HeaderText = "Роль";
            colRole.Name = "colRole";
            // 
            // UsersForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(726, 491);
            Controls.Add(dgvUsers);
            Controls.Add(btnClear);
            Controls.Add(btnDelete);
            Controls.Add(btnEdit);
            Controls.Add(btnAdd);
            Controls.Add(cmbRole);
            Controls.Add(label3);
            Controls.Add(txtPassword);
            Controls.Add(txtLogin);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "UsersForm";
            Text = "Пользователи";
            Load += UsersForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtLogin;
        private TextBox txtPassword;
        private Label label3;
        private ComboBox cmbRole;
        private Button btnClear;
        private Button btnDelete;
        private Button btnEdit;
        private Button btnAdd;
        private DataGridView dgvUsers;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colLogin;
        private DataGridViewTextBoxColumn colPassword;
        private DataGridViewTextBoxColumn colRole;
    }
}
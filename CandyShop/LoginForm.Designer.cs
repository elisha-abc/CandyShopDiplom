namespace CandyShop
{
    partial class LoginForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            txtLogin = new TextBox();
            txtPassword = new TextBox();
            button1 = new Button();
            chkShowPassword = new CheckBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(285, 73);
            label1.Name = "label1";
            label1.Size = new Size(89, 15);
            label1.TabIndex = 1;
            label1.Text = "Вход в систему";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(218, 142);
            label2.Name = "label2";
            label2.Size = new Size(41, 15);
            label2.TabIndex = 2;
            label2.Text = "Логин";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(218, 189);
            label3.Name = "label3";
            label3.Size = new Size(49, 15);
            label3.TabIndex = 3;
            label3.Text = "Пароль";
            // 
            // txtLogin
            // 
            txtLogin.Location = new Point(273, 139);
            txtLogin.Name = "txtLogin";
            txtLogin.Size = new Size(192, 23);
            txtLogin.TabIndex = 4;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(273, 186);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(192, 23);
            txtPassword.TabIndex = 5;
            // 
            // button1
            // 
            button1.Location = new Point(671, 380);
            button1.Name = "button1";
            button1.Size = new Size(80, 28);
            button1.TabIndex = 6;
            button1.Text = "Войти";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // chkShowPassword
            // 
            chkShowPassword.AutoSize = true;
            chkShowPassword.Location = new Point(471, 190);
            chkShowPassword.Name = "chkShowPassword";
            chkShowPassword.Size = new Size(119, 19);
            chkShowPassword.TabIndex = 7;
            chkShowPassword.Text = "Показать пароль";
            chkShowPassword.UseVisualStyleBackColor = true;
            chkShowPassword.CheckedChanged += chkShowPassword_CheckedChanged;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(chkShowPassword);
            Controls.Add(button1);
            Controls.Add(txtPassword);
            Controls.Add(txtLogin);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "LoginForm";
            Text = "Авторизация";
            Load += LoginForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtLogin;
        private TextBox txtPassword;
        private Button button1;
        private CheckBox chkShowPassword;
    }
}
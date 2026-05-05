namespace CandyShop
{
    partial class ExpiryForm
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
            numDays = new NumericUpDown();
            btnLoad = new Button();
            dgvExpiry = new DataGridView();
            colProduct = new DataGridViewTextBoxColumn();
            colQuantity = new DataGridViewTextBoxColumn();
            colExpiryDate = new DataGridViewTextBoxColumn();
            colDaysLeft = new DataGridViewTextBoxColumn();
            btnWriteOff = new Button();
            ((System.ComponentModel.ISupportInitialize)numDays).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvExpiry).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(49, 55);
            label1.Name = "label1";
            label1.Size = new Size(144, 15);
            label1.TabIndex = 0;
            label1.Text = "Срок годности до (дней):";
            // 
            // numDays
            // 
            numDays.Location = new Point(211, 53);
            numDays.Name = "numDays";
            numDays.Size = new Size(499, 23);
            numDays.TabIndex = 1;
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(140, 91);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(186, 52);
            btnLoad.TabIndex = 2;
            btnLoad.Text = "Показать";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            // 
            // dgvExpiry
            // 
            dgvExpiry.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvExpiry.Columns.AddRange(new DataGridViewColumn[] { colProduct, colQuantity, colExpiryDate, colDaysLeft });
            dgvExpiry.Location = new Point(31, 158);
            dgvExpiry.Name = "dgvExpiry";
            dgvExpiry.Size = new Size(755, 277);
            dgvExpiry.TabIndex = 3;
            // 
            // colProduct
            // 
            colProduct.HeaderText = "Товар";
            colProduct.Name = "colProduct";
            // 
            // colQuantity
            // 
            colQuantity.HeaderText = "Количество";
            colQuantity.Name = "colQuantity";
            // 
            // colExpiryDate
            // 
            colExpiryDate.HeaderText = "Срок годности";
            colExpiryDate.Name = "colExpiryDate";
            // 
            // colDaysLeft
            // 
            colDaysLeft.HeaderText = "Осталось дней";
            colDaysLeft.Name = "colDaysLeft";
            // 
            // btnWriteOff
            // 
            btnWriteOff.Location = new Point(455, 91);
            btnWriteOff.Name = "btnWriteOff";
            btnWriteOff.Size = new Size(186, 52);
            btnWriteOff.TabIndex = 4;
            btnWriteOff.Text = "Списать просроченные";
            btnWriteOff.UseVisualStyleBackColor = true;
            btnWriteOff.Click += btnWriteOff_Click;
            // 
            // ExpiryForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnWriteOff);
            Controls.Add(dgvExpiry);
            Controls.Add(btnLoad);
            Controls.Add(numDays);
            Controls.Add(label1);
            Name = "ExpiryForm";
            Text = "ExpiryForm";
            Load += ExpiryForm_Load;
            ((System.ComponentModel.ISupportInitialize)numDays).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvExpiry).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private NumericUpDown numDays;
        private Button btnLoad;
        private DataGridView dgvExpiry;
        private DataGridViewTextBoxColumn colProduct;
        private DataGridViewTextBoxColumn colQuantity;
        private DataGridViewTextBoxColumn colExpiryDate;
        private DataGridViewTextBoxColumn colDaysLeft;
        private Button button1;
        private Button btnWriteOff;
    }
}
namespace CandyShop
{
    partial class SalesForm
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
            dtpSaleDate = new DateTimePicker();
            txtQuantity = new TextBox();
            cmbProduct = new ComboBox();
            btnClear = new Button();
            btnDelete = new Button();
            btnAdd = new Button();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            dgvSales = new DataGridView();
            label4 = new Label();
            lblStockValue = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvSales).BeginInit();
            SuspendLayout();
            // 
            // dtpSaleDate
            // 
            dtpSaleDate.Location = new Point(153, 127);
            dtpSaleDate.Name = "dtpSaleDate";
            dtpSaleDate.Size = new Size(318, 23);
            dtpSaleDate.TabIndex = 24;
            // 
            // txtQuantity
            // 
            txtQuantity.Location = new Point(153, 55);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(318, 23);
            txtQuantity.TabIndex = 23;
            // 
            // cmbProduct
            // 
            cmbProduct.FormattingEnabled = true;
            cmbProduct.Location = new Point(153, 16);
            cmbProduct.Name = "cmbProduct";
            cmbProduct.Size = new Size(318, 23);
            cmbProduct.TabIndex = 22;
            cmbProduct.SelectedIndexChanged += cmbProduct_SelectedIndexChanged;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(549, 175);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(220, 52);
            btnClear.TabIndex = 20;
            btnClear.Text = "Очистить";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(276, 175);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(247, 52);
            btnDelete.TabIndex = 19;
            btnDelete.Text = "Удалитить";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(19, 175);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(234, 52);
            btnAdd.TabIndex = 17;
            btnAdd.Text = "Добавить ";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(38, 133);
            label3.Name = "label3";
            label3.Size = new Size(87, 15);
            label3.TabIndex = 15;
            label3.Text = "Дата продажи:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(38, 58);
            label2.Name = "label2";
            label2.Size = new Size(75, 15);
            label2.TabIndex = 14;
            label2.Text = "Количество:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(38, 19);
            label1.Name = "label1";
            label1.Size = new Size(43, 15);
            label1.TabIndex = 13;
            label1.Text = "Товар:";
            // 
            // dgvSales
            // 
            dgvSales.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSales.Location = new Point(19, 241);
            dgvSales.Name = "dgvSales";
            dgvSales.Size = new Size(762, 198);
            dgvSales.TabIndex = 25;
            dgvSales.CellClick += dgvSales_CellClick;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(37, 91);
            label4.Name = "label4";
            label4.Size = new Size(110, 15);
            label4.TabIndex = 27;
            label4.Text = "Остаток на складе:";
            // 
            // lblStockValue
            // 
            lblStockValue.AutoSize = true;
            lblStockValue.Location = new Point(153, 91);
            lblStockValue.Name = "lblStockValue";
            lblStockValue.Size = new Size(13, 15);
            lblStockValue.TabIndex = 28;
            lblStockValue.Text = "0";
            // 
            // SalesForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblStockValue);
            Controls.Add(label4);
            Controls.Add(dgvSales);
            Controls.Add(dtpSaleDate);
            Controls.Add(txtQuantity);
            Controls.Add(cmbProduct);
            Controls.Add(btnClear);
            Controls.Add(btnDelete);
            Controls.Add(btnAdd);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "SalesForm";
            Text = "SalesForm";
            Load += SalesForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvSales).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DateTimePicker dtpSaleDate;
        private TextBox txtQuantity;
        private ComboBox cmbProduct;
        private Button btnClear;
        private Button btnDelete;
        private Button btnAdd;
        private Label label3;
        private Label label2;
        private Label label1;
        private DataGridView dgvSales;
        private Label label4;
        private Label lblStockValue;
    }
}
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
            BtnClear = new Button();
            btnDelete = new Button();
            btnAdd = new Button();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            dgvSales = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colProduct = new DataGridViewTextBoxColumn();
            colQuantity = new DataGridViewTextBoxColumn();
            colSaleDate = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvSales).BeginInit();
            SuspendLayout();
            // 
            // dtpSaleDate
            // 
            dtpSaleDate.Location = new Point(153, 93);
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
            // 
            // BtnClear
            // 
            BtnClear.Location = new Point(549, 175);
            BtnClear.Name = "BtnClear";
            BtnClear.Size = new Size(220, 52);
            BtnClear.TabIndex = 20;
            BtnClear.Text = "Очистить";
            BtnClear.UseVisualStyleBackColor = true;
            BtnClear.Click += btnClear_Click;
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
            label3.Location = new Point(38, 99);
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
            dgvSales.Columns.AddRange(new DataGridViewColumn[] { colId, colProduct, colQuantity, colSaleDate });
            dgvSales.Location = new Point(19, 241);
            dgvSales.Name = "dgvSales";
            dgvSales.Size = new Size(762, 198);
            dgvSales.TabIndex = 25;
            dgvSales.CellClick += dgvSales_CellClick;
            // 
            // colId
            // 
            colId.HeaderText = "Id";
            colId.Name = "colId";
            colId.Visible = false;
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
            // colSaleDate
            // 
            colSaleDate.HeaderText = "Дата продажи";
            colSaleDate.Name = "colSaleDate";
            // 
            // SalesForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvSales);
            Controls.Add(dtpSaleDate);
            Controls.Add(txtQuantity);
            Controls.Add(cmbProduct);
            Controls.Add(BtnClear);
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
        private Button BtnClear;
        private Button btnDelete;
        private Button btnAdd;
        private Label label3;
        private Label label2;
        private Label label1;
        private DataGridView dgvSales;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colProduct;
        private DataGridViewTextBoxColumn colQuantity;
        private DataGridViewTextBoxColumn colSaleDate;
    }
}
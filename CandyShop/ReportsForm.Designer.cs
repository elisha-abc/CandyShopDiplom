namespace CandyShop
{
    partial class ReportsForm
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
            dtpDateFrom = new DateTimePicker();
            label2 = new Label();
            dtpDateTo = new DateTimePicker();
            btnLoadReport = new Button();
            btnClearReport = new Button();
            label3 = new Label();
            label4 = new Label();
            lblTotalSalesValue = new Label();
            lblTotalRevenueValue = new Label();
            dgvReports = new DataGridView();
            colProduct = new DataGridViewTextBoxColumn();
            colQuantity = new DataGridViewTextBoxColumn();
            colRevenue = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvReports).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(45, 35);
            label1.Name = "label1";
            label1.Size = new Size(61, 15);
            label1.TabIndex = 0;
            label1.Text = "Период с:";
            // 
            // dtpDateFrom
            // 
            dtpDateFrom.Location = new Point(112, 29);
            dtpDateFrom.Name = "dtpDateFrom";
            dtpDateFrom.Size = new Size(155, 23);
            dtpDateFrom.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(273, 35);
            label2.Name = "label2";
            label2.Size = new Size(24, 15);
            label2.TabIndex = 2;
            label2.Text = "по:";
            // 
            // dtpDateTo
            // 
            dtpDateTo.Location = new Point(303, 29);
            dtpDateTo.Name = "dtpDateTo";
            dtpDateTo.Size = new Size(155, 23);
            dtpDateTo.TabIndex = 3;
            // 
            // btnLoadReport
            // 
            btnLoadReport.Location = new Point(481, 23);
            btnLoadReport.Name = "btnLoadReport";
            btnLoadReport.Size = new Size(139, 38);
            btnLoadReport.TabIndex = 4;
            btnLoadReport.Text = "Показать";
            btnLoadReport.UseVisualStyleBackColor = true;
            btnLoadReport.Click += btnLoadReport_Click;
            // 
            // btnClearReport
            // 
            btnClearReport.Location = new Point(626, 23);
            btnClearReport.Name = "btnClearReport";
            btnClearReport.Size = new Size(139, 38);
            btnClearReport.TabIndex = 5;
            btnClearReport.Text = "Очистить";
            btnClearReport.UseVisualStyleBackColor = true;
            btnClearReport.Click += btnClearReport_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(45, 79);
            label3.Name = "label3";
            label3.Size = new Size(120, 15);
            label3.TabIndex = 6;
            label3.Text = "Количество продаж:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(45, 119);
            label4.Name = "label4";
            label4.Size = new Size(99, 15);
            label4.TabIndex = 7;
            label4.Text = "Общая выручка:";
            // 
            // lblTotalSalesValue
            // 
            lblTotalSalesValue.AutoSize = true;
            lblTotalSalesValue.Location = new Point(171, 79);
            lblTotalSalesValue.Name = "lblTotalSalesValue";
            lblTotalSalesValue.Size = new Size(13, 15);
            lblTotalSalesValue.TabIndex = 8;
            lblTotalSalesValue.Text = "0";
            // 
            // lblTotalRevenueValue
            // 
            lblTotalRevenueValue.AutoSize = true;
            lblTotalRevenueValue.Location = new Point(150, 119);
            lblTotalRevenueValue.Name = "lblTotalRevenueValue";
            lblTotalRevenueValue.Size = new Size(39, 15);
            lblTotalRevenueValue.TabIndex = 9;
            lblTotalRevenueValue.Text = "0 руб.";
            // 
            // dgvReports
            // 
            dgvReports.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReports.Columns.AddRange(new DataGridViewColumn[] { colProduct, colQuantity, colRevenue });
            dgvReports.Location = new Point(45, 158);
            dgvReports.Name = "dgvReports";
            dgvReports.Size = new Size(719, 273);
            dgvReports.TabIndex = 10;
            // 
            // colProduct
            // 
            colProduct.HeaderText = "Товар";
            colProduct.Name = "colProduct";
            // 
            // colQuantity
            // 
            colQuantity.HeaderText = "Продано";
            colQuantity.Name = "colQuantity";
            // 
            // colRevenue
            // 
            colRevenue.HeaderText = "Выручка";
            colRevenue.Name = "colRevenue";
            // 
            // ReportsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvReports);
            Controls.Add(lblTotalRevenueValue);
            Controls.Add(lblTotalSalesValue);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(btnClearReport);
            Controls.Add(btnLoadReport);
            Controls.Add(dtpDateTo);
            Controls.Add(label2);
            Controls.Add(dtpDateFrom);
            Controls.Add(label1);
            Name = "ReportsForm";
            Text = "ReportsForm";
            Load += ReportsForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvReports).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private DateTimePicker dtpDateFrom;
        private Label label2;
        private DateTimePicker dtpDateTo;
        private Button btnLoadReport;
        private Button btnClearReport;
        private Label label3;
        private Label label4;
        private Label lblTotalSalesValue;
        private Label lblTotalRevenueValue;
        private DataGridView dgvReports;
        private DataGridViewTextBoxColumn colProduct;
        private DataGridViewTextBoxColumn colQuantity;
        private DataGridViewTextBoxColumn colRevenue;
    }
}
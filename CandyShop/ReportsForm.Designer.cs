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
            cmbProductFilter = new ComboBox();
            label5 = new Label();
            btnCategoryReport = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvReports).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(48, 35);
            label1.Name = "label1";
            label1.Size = new Size(61, 15);
            label1.TabIndex = 0;
            label1.Text = "Период с:";
            // 
            // dtpDateFrom
            // 
            dtpDateFrom.Location = new Point(115, 29);
            dtpDateFrom.Name = "dtpDateFrom";
            dtpDateFrom.Size = new Size(155, 23);
            dtpDateFrom.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(276, 35);
            label2.Name = "label2";
            label2.Size = new Size(24, 15);
            label2.TabIndex = 2;
            label2.Text = "по:";
            // 
            // dtpDateTo
            // 
            dtpDateTo.Location = new Point(306, 29);
            dtpDateTo.Name = "dtpDateTo";
            dtpDateTo.Size = new Size(155, 23);
            dtpDateTo.TabIndex = 3;
            // 
            // btnLoadReport
            // 
            btnLoadReport.Location = new Point(484, 23);
            btnLoadReport.Name = "btnLoadReport";
            btnLoadReport.Size = new Size(139, 50);
            btnLoadReport.TabIndex = 4;
            btnLoadReport.Text = "Показать";
            btnLoadReport.UseVisualStyleBackColor = true;
            btnLoadReport.Click += btnLoadReport_Click;
            // 
            // btnClearReport
            // 
            btnClearReport.Location = new Point(629, 23);
            btnClearReport.Name = "btnClearReport";
            btnClearReport.Size = new Size(135, 50);
            btnClearReport.TabIndex = 5;
            btnClearReport.Text = "Очистить";
            btnClearReport.UseVisualStyleBackColor = true;
            btnClearReport.Click += btnClearReport_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(48, 95);
            label3.Name = "label3";
            label3.Size = new Size(120, 15);
            label3.TabIndex = 6;
            label3.Text = "Количество продаж:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(48, 125);
            label4.Name = "label4";
            label4.Size = new Size(99, 15);
            label4.TabIndex = 7;
            label4.Text = "Общая выручка:";
            // 
            // lblTotalSalesValue
            // 
            lblTotalSalesValue.AutoSize = true;
            lblTotalSalesValue.Location = new Point(174, 95);
            lblTotalSalesValue.Name = "lblTotalSalesValue";
            lblTotalSalesValue.Size = new Size(13, 15);
            lblTotalSalesValue.TabIndex = 8;
            lblTotalSalesValue.Text = "0";
            // 
            // lblTotalRevenueValue
            // 
            lblTotalRevenueValue.AutoSize = true;
            lblTotalRevenueValue.Location = new Point(153, 125);
            lblTotalRevenueValue.Name = "lblTotalRevenueValue";
            lblTotalRevenueValue.Size = new Size(39, 15);
            lblTotalRevenueValue.TabIndex = 9;
            lblTotalRevenueValue.Text = "0 руб.";
            // 
            // dgvReports
            // 
            dgvReports.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReports.Columns.AddRange(new DataGridViewColumn[] { colProduct, colQuantity, colRevenue });
            dgvReports.Location = new Point(45, 159);
            dgvReports.Name = "dgvReports";
            dgvReports.Size = new Size(719, 272);
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
            // cmbProductFilter
            // 
            cmbProductFilter.FormattingEnabled = true;
            cmbProductFilter.Location = new Point(97, 58);
            cmbProductFilter.Name = "cmbProductFilter";
            cmbProductFilter.Size = new Size(166, 23);
            cmbProductFilter.TabIndex = 11;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(48, 61);
            label5.Name = "label5";
            label5.Size = new Size(43, 15);
            label5.TabIndex = 12;
            label5.Text = "Товар:";
            // 
            // btnCategoryReport
            // 
            btnCategoryReport.Location = new Point(484, 79);
            btnCategoryReport.Name = "btnCategoryReport";
            btnCategoryReport.Size = new Size(280, 61);
            btnCategoryReport.TabIndex = 13;
            btnCategoryReport.Text = "Отчет по категориям";
            btnCategoryReport.UseVisualStyleBackColor = true;
            btnCategoryReport.Click += btnCategoryReport_Click;
            // 
            // ReportsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnCategoryReport);
            Controls.Add(label5);
            Controls.Add(cmbProductFilter);
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
        private ComboBox cmbProductFilter;
        private Label label5;
        private Button btnCategoryReport;
    }
}
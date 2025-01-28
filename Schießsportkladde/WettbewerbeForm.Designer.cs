namespace Schießsportkladde
{
    partial class WettbewerbeForm
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
            EditBtn = new Button();
            SafeBtn = new Button();
            NameTxt = new TextBox();
            NameLbl = new Label();
            HeaderTxtLbl = new Label();
            WettbewerbsGrid = new DataGridView();
            SpOTxt = new TextBox();
            SpOLbl = new Label();
            ClearAllFieldsBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)WettbewerbsGrid).BeginInit();
            SuspendLayout();
            // 
            // EditBtn
            // 
            EditBtn.FlatAppearance.BorderSize = 0;
            EditBtn.FlatStyle = FlatStyle.Flat;
            EditBtn.Location = new Point(0, 541);
            EditBtn.Name = "EditBtn";
            EditBtn.Size = new Size(943, 40);
            EditBtn.TabIndex = 51;
            EditBtn.Text = "Ändern";
            EditBtn.UseVisualStyleBackColor = true;
            EditBtn.Click += EditBtn_Click;
            // 
            // SafeBtn
            // 
            SafeBtn.FlatAppearance.BorderSize = 0;
            SafeBtn.FlatStyle = FlatStyle.Flat;
            SafeBtn.Location = new Point(0, 541);
            SafeBtn.Name = "SafeBtn";
            SafeBtn.Size = new Size(943, 40);
            SafeBtn.TabIndex = 50;
            SafeBtn.Text = "Speichern";
            SafeBtn.UseVisualStyleBackColor = true;
            SafeBtn.Click += SafeBtn_Click;
            // 
            // NameTxt
            // 
            NameTxt.Location = new Point(129, 140);
            NameTxt.Margin = new Padding(5, 6, 5, 6);
            NameTxt.Name = "NameTxt";
            NameTxt.Size = new Size(261, 36);
            NameTxt.TabIndex = 49;
            // 
            // NameLbl
            // 
            NameLbl.AutoSize = true;
            NameLbl.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            NameLbl.Location = new Point(218, 97);
            NameLbl.Margin = new Padding(5, 0, 5, 0);
            NameLbl.Name = "NameLbl";
            NameLbl.Size = new Size(88, 37);
            NameLbl.TabIndex = 48;
            NameLbl.Text = "Name";
            // 
            // HeaderTxtLbl
            // 
            HeaderTxtLbl.AutoSize = true;
            HeaderTxtLbl.Font = new Font("Segoe UI", 30F);
            HeaderTxtLbl.Location = new Point(326, 9);
            HeaderTxtLbl.Margin = new Padding(5, 0, 5, 0);
            HeaderTxtLbl.Name = "HeaderTxtLbl";
            HeaderTxtLbl.Size = new Size(261, 54);
            HeaderTxtLbl.TabIndex = 47;
            HeaderTxtLbl.Text = "Wettbewerbe";
            // 
            // WettbewerbsGrid
            // 
            WettbewerbsGrid.BackgroundColor = SystemColors.Control;
            WettbewerbsGrid.BorderStyle = BorderStyle.None;
            WettbewerbsGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            WettbewerbsGrid.Location = new Point(218, 227);
            WettbewerbsGrid.Margin = new Padding(5, 6, 5, 6);
            WettbewerbsGrid.Name = "WettbewerbsGrid";
            WettbewerbsGrid.ReadOnly = true;
            WettbewerbsGrid.Size = new Size(452, 182);
            WettbewerbsGrid.TabIndex = 46;
            WettbewerbsGrid.CellClick += WettbewerbsGrid_CellClick;
            WettbewerbsGrid.DataBindingComplete += WettbewerbsGrid_DataBindingComplete;
            // 
            // SpOTxt
            // 
            SpOTxt.Location = new Point(512, 140);
            SpOTxt.Margin = new Padding(5, 6, 5, 6);
            SpOTxt.Name = "SpOTxt";
            SpOTxt.Size = new Size(261, 36);
            SpOTxt.TabIndex = 53;
            // 
            // SpOLbl
            // 
            SpOLbl.AutoSize = true;
            SpOLbl.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            SpOLbl.Location = new Point(603, 97);
            SpOLbl.Margin = new Padding(5, 0, 5, 0);
            SpOLbl.Name = "SpOLbl";
            SpOLbl.Size = new Size(67, 37);
            SpOLbl.TabIndex = 52;
            SpOLbl.Text = "SpO";
            // 
            // ClearAllFieldsBtn
            // 
            ClearAllFieldsBtn.Dock = DockStyle.Bottom;
            ClearAllFieldsBtn.FlatAppearance.BorderSize = 0;
            ClearAllFieldsBtn.FlatStyle = FlatStyle.Flat;
            ClearAllFieldsBtn.Location = new Point(0, 581);
            ClearAllFieldsBtn.Name = "ClearAllFieldsBtn";
            ClearAllFieldsBtn.Size = new Size(943, 40);
            ClearAllFieldsBtn.TabIndex = 54;
            ClearAllFieldsBtn.Text = "Felder Leeren";
            ClearAllFieldsBtn.UseVisualStyleBackColor = true;
            ClearAllFieldsBtn.Click += ClearAllFieldsBtn_Click;
            // 
            // WettbewerbeForm
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(943, 621);
            Controls.Add(ClearAllFieldsBtn);
            Controls.Add(SpOTxt);
            Controls.Add(SpOLbl);
            Controls.Add(EditBtn);
            Controls.Add(SafeBtn);
            Controls.Add(NameTxt);
            Controls.Add(NameLbl);
            Controls.Add(HeaderTxtLbl);
            Controls.Add(WettbewerbsGrid);
            Font = new Font("Segoe UI", 16F);
            Margin = new Padding(5, 6, 5, 6);
            Name = "WettbewerbeForm";
            Text = "WettbewerbeForm";
            Load += WettbewerbeForm_Load;
            ((System.ComponentModel.ISupportInitialize)WettbewerbsGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button EditBtn;
        private Button SafeBtn;
        private TextBox NameTxt;
        private Label NameLbl;
        private Label HeaderTxtLbl;
        private DataGridView WettbewerbsGrid;
        private TextBox SpOTxt;
        private Label SpOLbl;
        private Button ClearAllFieldsBtn;
    }
}
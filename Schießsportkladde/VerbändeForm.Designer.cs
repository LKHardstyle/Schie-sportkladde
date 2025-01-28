namespace Schießsportkladde
{
    partial class VerbändeForm
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
            VerbändeGrid = new DataGridView();
            ClearAllFieldsBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)VerbändeGrid).BeginInit();
            SuspendLayout();
            // 
            // EditBtn
            // 
            EditBtn.FlatAppearance.BorderSize = 0;
            EditBtn.FlatStyle = FlatStyle.Flat;
            EditBtn.Location = new Point(0, 437);
            EditBtn.Name = "EditBtn";
            EditBtn.Size = new Size(719, 40);
            EditBtn.TabIndex = 39;
            EditBtn.Text = "Ändern";
            EditBtn.UseVisualStyleBackColor = true;
            EditBtn.Click += EditBtn_Click;
            // 
            // SafeBtn
            // 
            SafeBtn.FlatAppearance.BorderSize = 0;
            SafeBtn.FlatStyle = FlatStyle.Flat;
            SafeBtn.Location = new Point(0, 437);
            SafeBtn.Name = "SafeBtn";
            SafeBtn.Size = new Size(719, 40);
            SafeBtn.TabIndex = 38;
            SafeBtn.Text = "Speichern";
            SafeBtn.UseVisualStyleBackColor = true;
            SafeBtn.Click += SafeBtn_Click;
            // 
            // NameTxt
            // 
            NameTxt.Location = new Point(219, 114);
            NameTxt.Margin = new Padding(5, 6, 5, 6);
            NameTxt.Name = "NameTxt";
            NameTxt.Size = new Size(260, 36);
            NameTxt.TabIndex = 37;
            NameTxt.KeyPress += NameTxt_KeyPress;
            // 
            // NameLbl
            // 
            NameLbl.AutoSize = true;
            NameLbl.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            NameLbl.Location = new Point(294, 59);
            NameLbl.Margin = new Padding(5, 0, 5, 0);
            NameLbl.Name = "NameLbl";
            NameLbl.Size = new Size(94, 37);
            NameLbl.TabIndex = 36;
            NameLbl.Text = "Name:";
            // 
            // HeaderTxtLbl
            // 
            HeaderTxtLbl.AutoSize = true;
            HeaderTxtLbl.Font = new Font("Segoe UI", 30F);
            HeaderTxtLbl.Location = new Point(266, 5);
            HeaderTxtLbl.Margin = new Padding(5, 0, 5, 0);
            HeaderTxtLbl.Name = "HeaderTxtLbl";
            HeaderTxtLbl.Size = new Size(192, 54);
            HeaderTxtLbl.TabIndex = 35;
            HeaderTxtLbl.Text = "Verbände";
            // 
            // VerbändeGrid
            // 
            VerbändeGrid.BackgroundColor = SystemColors.Control;
            VerbändeGrid.BorderStyle = BorderStyle.None;
            VerbändeGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            VerbändeGrid.Location = new Point(26, 171);
            VerbändeGrid.Margin = new Padding(5, 6, 5, 6);
            VerbändeGrid.Name = "VerbändeGrid";
            VerbändeGrid.ReadOnly = true;
            VerbändeGrid.Size = new Size(641, 182);
            VerbändeGrid.TabIndex = 34;
            VerbändeGrid.CellClick += VerbändeGrid_CellClick;
            // 
            // ClearAllFieldsBtn
            // 
            ClearAllFieldsBtn.Dock = DockStyle.Bottom;
            ClearAllFieldsBtn.FlatAppearance.BorderSize = 0;
            ClearAllFieldsBtn.FlatStyle = FlatStyle.Flat;
            ClearAllFieldsBtn.Location = new Point(0, 477);
            ClearAllFieldsBtn.Name = "ClearAllFieldsBtn";
            ClearAllFieldsBtn.Size = new Size(719, 40);
            ClearAllFieldsBtn.TabIndex = 40;
            ClearAllFieldsBtn.Text = "Feld Leeren";
            ClearAllFieldsBtn.UseVisualStyleBackColor = true;
            ClearAllFieldsBtn.Click += ClearAllFieldsBtn_Click;
            // 
            // VerbändeForm
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(719, 517);
            Controls.Add(ClearAllFieldsBtn);
            Controls.Add(EditBtn);
            Controls.Add(SafeBtn);
            Controls.Add(NameTxt);
            Controls.Add(NameLbl);
            Controls.Add(HeaderTxtLbl);
            Controls.Add(VerbändeGrid);
            Font = new Font("Segoe UI", 16F);
            Margin = new Padding(5, 6, 5, 6);
            Name = "VerbändeForm";
            Text = "VerbändeForm";
            Load += VerbändeForm_Load;
            ((System.ComponentModel.ISupportInitialize)VerbändeGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button EditBtn;
        private Button SafeBtn;
        private TextBox NameTxt;
        private Label NameLbl;
        private Label HeaderTxtLbl;
        private DataGridView VerbändeGrid;
        private Button ClearAllFieldsBtn;
    }
}
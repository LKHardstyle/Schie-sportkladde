namespace Schießsportkladde
{
    partial class AufsichtForm
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
            NameTxtLbl = new Label();
            HeaderTxtLbl = new Label();
            AufsichtGrid = new DataGridView();
            ClearAllFieldsBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)AufsichtGrid).BeginInit();
            SuspendLayout();
            // 
            // EditBtn
            // 
            EditBtn.FlatAppearance.BorderSize = 0;
            EditBtn.FlatStyle = FlatStyle.Flat;
            EditBtn.Location = new Point(0, 447);
            EditBtn.Name = "EditBtn";
            EditBtn.Size = new Size(763, 40);
            EditBtn.TabIndex = 45;
            EditBtn.Text = "Ändern";
            EditBtn.UseVisualStyleBackColor = true;
            EditBtn.Click += EditBtn_Click;
            // 
            // SafeBtn
            // 
            SafeBtn.FlatAppearance.BorderSize = 0;
            SafeBtn.FlatStyle = FlatStyle.Flat;
            SafeBtn.Location = new Point(0, 447);
            SafeBtn.Name = "SafeBtn";
            SafeBtn.Size = new Size(763, 40);
            SafeBtn.TabIndex = 44;
            SafeBtn.Text = "Speichern";
            SafeBtn.UseVisualStyleBackColor = true;
            SafeBtn.Click += SafeBtn_Click;
            // 
            // NameTxt
            // 
            NameTxt.Location = new Point(283, 131);
            NameTxt.Margin = new Padding(5, 6, 5, 6);
            NameTxt.Name = "NameTxt";
            NameTxt.Size = new Size(211, 36);
            NameTxt.TabIndex = 43;
            NameTxt.KeyPress += NameTxt_KeyPress;
            // 
            // NameTxtLbl
            // 
            NameTxtLbl.AutoSize = true;
            NameTxtLbl.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            NameTxtLbl.Location = new Point(341, 76);
            NameTxtLbl.Margin = new Padding(5, 0, 5, 0);
            NameTxtLbl.Name = "NameTxtLbl";
            NameTxtLbl.Size = new Size(88, 37);
            NameTxtLbl.TabIndex = 42;
            NameTxtLbl.Text = "Name";
            // 
            // HeaderTxtLbl
            // 
            HeaderTxtLbl.AutoSize = true;
            HeaderTxtLbl.Font = new Font("Segoe UI", 30F);
            HeaderTxtLbl.Location = new Point(283, 9);
            HeaderTxtLbl.Margin = new Padding(5, 0, 5, 0);
            HeaderTxtLbl.Name = "HeaderTxtLbl";
            HeaderTxtLbl.Size = new Size(211, 54);
            HeaderTxtLbl.TabIndex = 41;
            HeaderTxtLbl.Text = "Aufsichten";
            // 
            // AufsichtGrid
            // 
            AufsichtGrid.BackgroundColor = SystemColors.Control;
            AufsichtGrid.BorderStyle = BorderStyle.None;
            AufsichtGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            AufsichtGrid.Location = new Point(162, 176);
            AufsichtGrid.Margin = new Padding(5, 6, 5, 6);
            AufsichtGrid.Name = "AufsichtGrid";
            AufsichtGrid.Size = new Size(452, 182);
            AufsichtGrid.TabIndex = 40;
            AufsichtGrid.CellClick += AufsichtGrid_CellClick;
            // 
            // ClearAllFieldsBtn
            // 
            ClearAllFieldsBtn.Dock = DockStyle.Bottom;
            ClearAllFieldsBtn.FlatAppearance.BorderSize = 0;
            ClearAllFieldsBtn.FlatStyle = FlatStyle.Flat;
            ClearAllFieldsBtn.Location = new Point(0, 487);
            ClearAllFieldsBtn.Name = "ClearAllFieldsBtn";
            ClearAllFieldsBtn.Size = new Size(763, 40);
            ClearAllFieldsBtn.TabIndex = 46;
            ClearAllFieldsBtn.Text = "Feld Leeren";
            ClearAllFieldsBtn.UseVisualStyleBackColor = true;
            ClearAllFieldsBtn.Click += ClearAllFieldsBtn_Click;
            // 
            // AufsichtForm
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(763, 527);
            Controls.Add(ClearAllFieldsBtn);
            Controls.Add(EditBtn);
            Controls.Add(SafeBtn);
            Controls.Add(NameTxt);
            Controls.Add(NameTxtLbl);
            Controls.Add(HeaderTxtLbl);
            Controls.Add(AufsichtGrid);
            Font = new Font("Segoe UI", 16F);
            Margin = new Padding(5, 6, 5, 6);
            Name = "AufsichtForm";
            Text = "AufsichtForm";
            Load += AufsichtForm_Load;
            ((System.ComponentModel.ISupportInitialize)AufsichtGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button EditBtn;
        private Button SafeBtn;
        private TextBox NameTxt;
        private Label NameTxtLbl;
        private Label HeaderTxtLbl;
        private DataGridView AufsichtGrid;
        private Button ClearAllFieldsBtn;
    }
}
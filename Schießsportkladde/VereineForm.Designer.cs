namespace Schießsportkladde
{
    partial class VereineForm
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
            VereineGrid = new DataGridView();
            HeaderTxtLbl = new Label();
            NameTxt = new TextBox();
            NameLbl = new Label();
            SafeBtn = new Button();
            EditBtn = new Button();
            ClearAllFieldsBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)VereineGrid).BeginInit();
            SuspendLayout();
            // 
            // VereineGrid
            // 
            VereineGrid.BackgroundColor = SystemColors.Control;
            VereineGrid.BorderStyle = BorderStyle.None;
            VereineGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            VereineGrid.Location = new Point(26, 175);
            VereineGrid.Margin = new Padding(5, 6, 5, 6);
            VereineGrid.Name = "VereineGrid";
            VereineGrid.Size = new Size(641, 182);
            VereineGrid.TabIndex = 27;
            VereineGrid.CellClick += VereineGrid_CellClick;
            VereineGrid.DataBindingComplete += VereineGrid_DataBindingComplete;
            // 
            // HeaderTxtLbl
            // 
            HeaderTxtLbl.AutoSize = true;
            HeaderTxtLbl.Font = new Font("Segoe UI", 30F);
            HeaderTxtLbl.Location = new Point(266, 9);
            HeaderTxtLbl.Margin = new Padding(5, 0, 5, 0);
            HeaderTxtLbl.Name = "HeaderTxtLbl";
            HeaderTxtLbl.Size = new Size(155, 54);
            HeaderTxtLbl.TabIndex = 28;
            HeaderTxtLbl.Text = "Vereine";
            // 
            // NameTxt
            // 
            NameTxt.Location = new Point(219, 118);
            NameTxt.Margin = new Padding(5, 6, 5, 6);
            NameTxt.Name = "NameTxt";
            NameTxt.Size = new Size(260, 35);
            NameTxt.TabIndex = 31;
            NameTxt.KeyPress += NameTxt_KeyPress;
            // 
            // NameLbl
            // 
            NameLbl.AutoSize = true;
            NameLbl.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            NameLbl.Location = new Point(294, 63);
            NameLbl.Margin = new Padding(5, 0, 5, 0);
            NameLbl.Name = "NameLbl";
            NameLbl.Size = new Size(94, 37);
            NameLbl.TabIndex = 29;
            NameLbl.Text = "Name:";
            // 
            // SafeBtn
            // 
            SafeBtn.FlatAppearance.BorderSize = 0;
            SafeBtn.FlatStyle = FlatStyle.Flat;
            SafeBtn.Location = new Point(0, 366);
            SafeBtn.Name = "SafeBtn";
            SafeBtn.Size = new Size(691, 40);
            SafeBtn.TabIndex = 32;
            SafeBtn.Text = "Speichern";
            SafeBtn.UseVisualStyleBackColor = true;
            SafeBtn.Click += SafeBtn_Click;
            // 
            // EditBtn
            // 
            EditBtn.FlatAppearance.BorderSize = 0;
            EditBtn.FlatStyle = FlatStyle.Flat;
            EditBtn.Location = new Point(0, 366);
            EditBtn.Name = "EditBtn";
            EditBtn.Size = new Size(691, 40);
            EditBtn.TabIndex = 33;
            EditBtn.Text = "Ändern";
            EditBtn.UseVisualStyleBackColor = true;
            EditBtn.Click += EditBtn_Click;
            // 
            // ClearAllFieldsBtn
            // 
            ClearAllFieldsBtn.Dock = DockStyle.Bottom;
            ClearAllFieldsBtn.FlatAppearance.BorderSize = 0;
            ClearAllFieldsBtn.FlatStyle = FlatStyle.Flat;
            ClearAllFieldsBtn.Location = new Point(0, 407);
            ClearAllFieldsBtn.Name = "ClearAllFieldsBtn";
            ClearAllFieldsBtn.Size = new Size(691, 40);
            ClearAllFieldsBtn.TabIndex = 34;
            ClearAllFieldsBtn.Text = "Felder Leeren";
            ClearAllFieldsBtn.UseVisualStyleBackColor = true;
            ClearAllFieldsBtn.Click += ClearAllFieldsBtn_Click;
            // 
            // VereineForm
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(691, 447);
            Controls.Add(ClearAllFieldsBtn);
            Controls.Add(EditBtn);
            Controls.Add(SafeBtn);
            Controls.Add(NameTxt);
            Controls.Add(NameLbl);
            Controls.Add(HeaderTxtLbl);
            Controls.Add(VereineGrid);
            Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(5, 6, 5, 6);
            Name = "VereineForm";
            Text = "VereineForm";
            Load += VereineForm_Load;
            ((System.ComponentModel.ISupportInitialize)VereineGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView VereineGrid;
        private Label HeaderTxtLbl;
        private TextBox NameTxt;
        private Label NameLbl;
        private Button SafeBtn;
        private Button EditBtn;
        private Button ClearAllFieldsBtn;
    }
}
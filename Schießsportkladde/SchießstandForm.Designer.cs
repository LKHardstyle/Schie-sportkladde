namespace Schießsportkladde
{
    partial class SchießstandForm
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
            SchießständeLbl = new Label();
            SchießständeGrid = new DataGridView();
            ClearAllFielsBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)SchießständeGrid).BeginInit();
            SuspendLayout();
            // 
            // EditBtn
            // 
            EditBtn.FlatAppearance.BorderSize = 0;
            EditBtn.FlatStyle = FlatStyle.Flat;
            EditBtn.Location = new Point(0, 495);
            EditBtn.Name = "EditBtn";
            EditBtn.Size = new Size(721, 40);
            EditBtn.TabIndex = 45;
            EditBtn.Text = "Ändern";
            EditBtn.UseVisualStyleBackColor = true;
            EditBtn.Click += EditBtn_Click;
            // 
            // SafeBtn
            // 
            SafeBtn.FlatAppearance.BorderSize = 0;
            SafeBtn.FlatStyle = FlatStyle.Flat;
            SafeBtn.Location = new Point(0, 495);
            SafeBtn.Name = "SafeBtn";
            SafeBtn.Size = new Size(721, 40);
            SafeBtn.TabIndex = 44;
            SafeBtn.Text = "Speichern";
            SafeBtn.UseVisualStyleBackColor = true;
            SafeBtn.Click += SafeBtn_Click;
            // 
            // NameTxt
            // 
            NameTxt.Location = new Point(219, 111);
            NameTxt.Margin = new Padding(5, 6, 5, 6);
            NameTxt.Name = "NameTxt";
            NameTxt.Size = new Size(260, 36);
            NameTxt.TabIndex = 43;
            NameTxt.KeyPress += NameTxt_KeyPress;
            // 
            // NameLbl
            // 
            NameLbl.AutoSize = true;
            NameLbl.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            NameLbl.Location = new Point(294, 56);
            NameLbl.Margin = new Padding(5, 0, 5, 0);
            NameLbl.Name = "NameLbl";
            NameLbl.Size = new Size(94, 37);
            NameLbl.TabIndex = 42;
            NameLbl.Text = "Name:";
            // 
            // SchießständeLbl
            // 
            SchießständeLbl.AutoSize = true;
            SchießständeLbl.Font = new Font("Segoe UI", 30F);
            SchießständeLbl.Location = new Point(222, 2);
            SchießständeLbl.Margin = new Padding(5, 0, 5, 0);
            SchießständeLbl.Name = "SchießständeLbl";
            SchießständeLbl.Size = new Size(257, 54);
            SchießständeLbl.TabIndex = 41;
            SchießständeLbl.Text = "Schießstände";
            // 
            // SchießständeGrid
            // 
            SchießständeGrid.BackgroundColor = SystemColors.Control;
            SchießständeGrid.BorderStyle = BorderStyle.None;
            SchießständeGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            SchießständeGrid.Location = new Point(26, 168);
            SchießständeGrid.Margin = new Padding(5, 6, 5, 6);
            SchießständeGrid.Name = "SchießständeGrid";
            SchießständeGrid.Size = new Size(641, 182);
            SchießständeGrid.TabIndex = 40;
            SchießständeGrid.CellClick += VerbändeGrid_CellClick;
            SchießständeGrid.DataBindingComplete += SchießständeGrid_DataBindingComplete;
            // 
            // ClearAllFielsBtn
            // 
            ClearAllFielsBtn.Dock = DockStyle.Bottom;
            ClearAllFielsBtn.FlatAppearance.BorderSize = 0;
            ClearAllFielsBtn.FlatStyle = FlatStyle.Flat;
            ClearAllFielsBtn.Location = new Point(0, 535);
            ClearAllFielsBtn.Name = "ClearAllFielsBtn";
            ClearAllFielsBtn.Size = new Size(721, 40);
            ClearAllFielsBtn.TabIndex = 46;
            ClearAllFielsBtn.Text = "Feld Leeren";
            ClearAllFielsBtn.UseVisualStyleBackColor = true;
            ClearAllFielsBtn.Click += ClearAllFielsBtn_Click;
            // 
            // SchießstandForm
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(721, 575);
            Controls.Add(ClearAllFielsBtn);
            Controls.Add(EditBtn);
            Controls.Add(SafeBtn);
            Controls.Add(NameTxt);
            Controls.Add(NameLbl);
            Controls.Add(SchießständeLbl);
            Controls.Add(SchießständeGrid);
            Font = new Font("Segoe UI", 16F);
            Margin = new Padding(5, 6, 5, 6);
            Name = "SchießstandForm";
            Text = "SchießstandForm";
            Load += SchießstandForm_Load;
            ((System.ComponentModel.ISupportInitialize)SchießständeGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button EditBtn;
        private Button SafeBtn;
        private TextBox NameTxt;
        private Label NameLbl;
        private Label SchießständeLbl;
        private DataGridView SchießständeGrid;
        private Button ClearAllFielsBtn;
    }
}
namespace Schießsportkladde
{
    partial class WaffenForm
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
            HeaderTextLbl = new Label();
            WaffenGrid = new DataGridView();
            KaliberTxt = new TextBox();
            KaliberLbl = new Label();
            DisziplinTxt = new TextBox();
            DisziplinLbl = new Label();
            ClearAllFieldsBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)WaffenGrid).BeginInit();
            SuspendLayout();
            // 
            // EditBtn
            // 
            EditBtn.FlatAppearance.BorderSize = 0;
            EditBtn.FlatStyle = FlatStyle.Flat;
            EditBtn.Location = new Point(0, 642);
            EditBtn.Name = "EditBtn";
            EditBtn.Size = new Size(1194, 40);
            EditBtn.TabIndex = 51;
            EditBtn.Text = "Ändern";
            EditBtn.UseVisualStyleBackColor = true;
            EditBtn.Click += EditBtn_Click;
            // 
            // SafeBtn
            // 
            SafeBtn.FlatAppearance.BorderSize = 0;
            SafeBtn.FlatStyle = FlatStyle.Flat;
            SafeBtn.Location = new Point(0, 642);
            SafeBtn.Name = "SafeBtn";
            SafeBtn.Size = new Size(1194, 40);
            SafeBtn.TabIndex = 50;
            SafeBtn.Text = "Speichern";
            SafeBtn.UseVisualStyleBackColor = true;
            SafeBtn.Click += SafeBtn_Click;
            // 
            // NameTxt
            // 
            NameTxt.Location = new Point(88, 140);
            NameTxt.Margin = new Padding(5, 6, 5, 6);
            NameTxt.Name = "NameTxt";
            NameTxt.Size = new Size(260, 36);
            NameTxt.TabIndex = 49;
            // 
            // NameLbl
            // 
            NameLbl.AutoSize = true;
            NameLbl.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            NameLbl.Location = new Point(163, 85);
            NameLbl.Margin = new Padding(5, 0, 5, 0);
            NameLbl.Name = "NameLbl";
            NameLbl.Size = new Size(88, 37);
            NameLbl.TabIndex = 48;
            NameLbl.Text = "Name";
            // 
            // HeaderTextLbl
            // 
            HeaderTextLbl.AutoSize = true;
            HeaderTextLbl.Font = new Font("Segoe UI", 30F);
            HeaderTextLbl.Location = new Point(448, 9);
            HeaderTextLbl.Margin = new Padding(5, 0, 5, 0);
            HeaderTextLbl.Name = "HeaderTextLbl";
            HeaderTextLbl.Size = new Size(257, 54);
            HeaderTextLbl.TabIndex = 47;
            HeaderTextLbl.Text = "Schießstände";
            // 
            // WaffenGrid
            // 
            WaffenGrid.BackgroundColor = SystemColors.Control;
            WaffenGrid.BorderStyle = BorderStyle.None;
            WaffenGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            WaffenGrid.Location = new Point(204, 234);
            WaffenGrid.Margin = new Padding(5, 6, 5, 6);
            WaffenGrid.Name = "WaffenGrid";
            WaffenGrid.ReadOnly = true;
            WaffenGrid.Size = new Size(883, 182);
            WaffenGrid.TabIndex = 46;
            WaffenGrid.CellClick += SchießständeGrid_CellClick;
            WaffenGrid.DataBindingComplete += WaffenGrid_DataBindingComplete;
            // 
            // KaliberTxt
            // 
            KaliberTxt.Location = new Point(458, 140);
            KaliberTxt.Margin = new Padding(5, 6, 5, 6);
            KaliberTxt.Name = "KaliberTxt";
            KaliberTxt.Size = new Size(260, 36);
            KaliberTxt.TabIndex = 53;
            // 
            // KaliberLbl
            // 
            KaliberLbl.AutoSize = true;
            KaliberLbl.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            KaliberLbl.Location = new Point(533, 85);
            KaliberLbl.Margin = new Padding(5, 0, 5, 0);
            KaliberLbl.Name = "KaliberLbl";
            KaliberLbl.Size = new Size(100, 37);
            KaliberLbl.TabIndex = 52;
            KaliberLbl.Text = "Kaliber";
            // 
            // DisziplinTxt
            // 
            DisziplinTxt.Location = new Point(827, 140);
            DisziplinTxt.Margin = new Padding(5, 6, 5, 6);
            DisziplinTxt.Name = "DisziplinTxt";
            DisziplinTxt.Size = new Size(260, 36);
            DisziplinTxt.TabIndex = 55;
            // 
            // DisziplinLbl
            // 
            DisziplinLbl.AutoSize = true;
            DisziplinLbl.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            DisziplinLbl.Location = new Point(908, 85);
            DisziplinLbl.Margin = new Padding(5, 0, 5, 0);
            DisziplinLbl.Name = "DisziplinLbl";
            DisziplinLbl.Size = new Size(118, 37);
            DisziplinLbl.TabIndex = 54;
            DisziplinLbl.Text = "Disziplin";
            // 
            // ClearAllFieldsBtn
            // 
            ClearAllFieldsBtn.Dock = DockStyle.Bottom;
            ClearAllFieldsBtn.FlatAppearance.BorderSize = 0;
            ClearAllFieldsBtn.FlatStyle = FlatStyle.Flat;
            ClearAllFieldsBtn.Location = new Point(0, 682);
            ClearAllFieldsBtn.Name = "ClearAllFieldsBtn";
            ClearAllFieldsBtn.Size = new Size(1194, 40);
            ClearAllFieldsBtn.TabIndex = 56;
            ClearAllFieldsBtn.Text = "Felder Leeren";
            ClearAllFieldsBtn.UseVisualStyleBackColor = true;
            ClearAllFieldsBtn.Click += ClearAllFieldsBtn_Click;
            // 
            // WaffenForm
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1194, 722);
            Controls.Add(ClearAllFieldsBtn);
            Controls.Add(DisziplinTxt);
            Controls.Add(DisziplinLbl);
            Controls.Add(KaliberTxt);
            Controls.Add(KaliberLbl);
            Controls.Add(EditBtn);
            Controls.Add(SafeBtn);
            Controls.Add(NameTxt);
            Controls.Add(NameLbl);
            Controls.Add(HeaderTextLbl);
            Controls.Add(WaffenGrid);
            Font = new Font("Segoe UI", 16F);
            Margin = new Padding(5, 6, 5, 6);
            Name = "WaffenForm";
            Text = "WaffenForm";
            Load += WaffenForm_Load;
            ((System.ComponentModel.ISupportInitialize)WaffenGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button EditBtn;
        private Button SafeBtn;
        private TextBox NameTxt;
        private Label NameLbl;
        private Label HeaderTextLbl;
        private DataGridView WaffenGrid;
        private TextBox KaliberTxt;
        private Label KaliberLbl;
        private TextBox DisziplinTxt;
        private Label DisziplinLbl;
        private Button ClearAllFieldsBtn;
    }
}
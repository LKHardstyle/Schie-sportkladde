namespace Schießsportkladde
{
    partial class KladdenForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            KladdenGrid = new DataGridView();
            HeaderTxtLbl = new Label();
            SchützenSortByNameCbx = new ComboBox();
            DatumLbl = new Label();
            WaffeLbl = new Label();
            SchusszahlLbl = new Label();
            AufsichtLbl = new Label();
            SchießstandLbl = new Label();
            SchützeCbx = new ComboBox();
            WaffeCbx = new ComboBox();
            SchießstandCbx = new ComboBox();
            AufsichtCbx = new ComboBox();
            DatumTxt = new TextBox();
            SchusszahlTxt = new TextBox();
            SafeBtn = new Button();
            EditBtn = new Button();
            PrintPb = new PictureBox();
            SchützenSortByYearCbx = new ComboBox();
            WettbewerbCbx = new ComboBox();
            WettbewerbLbl = new Label();
            ErgebnisTxt = new TextBox();
            ErgebnisLbl = new Label();
            ClearAllFieldsBtn = new Button();
            KladdenMenuStrip = new MenuStrip();
            schützenToolStripMenuItem = new ToolStripMenuItem();
            schießständeToolStripMenuItem = new ToolStripMenuItem();
            verbändeToolStripMenuItem = new ToolStripMenuItem();
            vereineToolStripMenuItem = new ToolStripMenuItem();
            waffenToolStripMenuItem = new ToolStripMenuItem();
            wettbewerbeToolStripMenuItem = new ToolStripMenuItem();
            aufsichtenToolStripMenuItem = new ToolStripMenuItem();
            statistikenToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStrip1 = new ContextMenuStrip(components);
            testToolStripMenuItem = new ToolStripMenuItem();
            testToolStripMenuItem1 = new ToolStripMenuItem();
            SchützeLbl = new Label();
            ((System.ComponentModel.ISupportInitialize)KladdenGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PrintPb).BeginInit();
            KladdenMenuStrip.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // KladdenGrid
            // 
            KladdenGrid.BackgroundColor = SystemColors.Control;
            KladdenGrid.BorderStyle = BorderStyle.None;
            KladdenGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            KladdenGrid.Location = new Point(14, 183);
            KladdenGrid.Margin = new Padding(5, 6, 5, 6);
            KladdenGrid.Name = "KladdenGrid";
            KladdenGrid.ReadOnly = true;
            KladdenGrid.Size = new Size(1219, 751);
            KladdenGrid.TabIndex = 0;
            KladdenGrid.CellClick += KladdenGrid_CellClick;
            KladdenGrid.DataBindingComplete += KladdenGrid_DataBindingComplete;
            // 
            // HeaderTxtLbl
            // 
            HeaderTxtLbl.AutoSize = true;
            HeaderTxtLbl.Font = new Font("Segoe UI", 40F);
            HeaderTxtLbl.Location = new Point(566, 29);
            HeaderTxtLbl.Margin = new Padding(5, 0, 5, 0);
            HeaderTxtLbl.Name = "HeaderTxtLbl";
            HeaderTxtLbl.Size = new Size(468, 72);
            HeaderTxtLbl.TabIndex = 1;
            HeaderTxtLbl.Text = "Schießsportkladde";
            // 
            // SchützenSortByNameCbx
            // 
            SchützenSortByNameCbx.FormattingEnabled = true;
            SchützenSortByNameCbx.Location = new Point(12, 136);
            SchützenSortByNameCbx.Name = "SchützenSortByNameCbx";
            SchützenSortByNameCbx.Size = new Size(221, 38);
            SchützenSortByNameCbx.TabIndex = 2;
            SchützenSortByNameCbx.SelectedIndexChanged += SchützenSortCbx_SelectedIndexChanged;
            // 
            // DatumLbl
            // 
            DatumLbl.AutoSize = true;
            DatumLbl.Location = new Point(1677, 241);
            DatumLbl.Name = "DatumLbl";
            DatumLbl.Size = new Size(76, 30);
            DatumLbl.TabIndex = 5;
            DatumLbl.Text = "Datum";
            // 
            // WaffeLbl
            // 
            WaffeLbl.AutoSize = true;
            WaffeLbl.Location = new Point(1677, 356);
            WaffeLbl.Name = "WaffeLbl";
            WaffeLbl.Size = new Size(68, 30);
            WaffeLbl.TabIndex = 6;
            WaffeLbl.Text = "Waffe";
            // 
            // SchusszahlLbl
            // 
            SchusszahlLbl.AutoSize = true;
            SchusszahlLbl.Location = new Point(1677, 414);
            SchusszahlLbl.Name = "SchusszahlLbl";
            SchusszahlLbl.Size = new Size(114, 30);
            SchusszahlLbl.TabIndex = 7;
            SchusszahlLbl.Text = "Schusszahl";
            // 
            // AufsichtLbl
            // 
            AufsichtLbl.AutoSize = true;
            AufsichtLbl.Location = new Point(1677, 592);
            AufsichtLbl.Name = "AufsichtLbl";
            AufsichtLbl.Size = new Size(89, 30);
            AufsichtLbl.TabIndex = 9;
            AufsichtLbl.Text = "Aufsicht";
            // 
            // SchießstandLbl
            // 
            SchießstandLbl.AutoSize = true;
            SchießstandLbl.Location = new Point(1677, 530);
            SchießstandLbl.Name = "SchießstandLbl";
            SchießstandLbl.Size = new Size(124, 30);
            SchießstandLbl.TabIndex = 8;
            SchießstandLbl.Text = "Schießstand";
            // 
            // SchützeCbx
            // 
            SchützeCbx.FormattingEnabled = true;
            SchützeCbx.Location = new Point(1864, 183);
            SchützeCbx.Name = "SchützeCbx";
            SchützeCbx.Size = new Size(193, 38);
            SchützeCbx.TabIndex = 10;
            // 
            // WaffeCbx
            // 
            WaffeCbx.FormattingEnabled = true;
            WaffeCbx.Location = new Point(1864, 356);
            WaffeCbx.Name = "WaffeCbx";
            WaffeCbx.Size = new Size(193, 38);
            WaffeCbx.TabIndex = 11;
            // 
            // SchießstandCbx
            // 
            SchießstandCbx.FormattingEnabled = true;
            SchießstandCbx.Location = new Point(1864, 530);
            SchießstandCbx.Name = "SchießstandCbx";
            SchießstandCbx.Size = new Size(193, 38);
            SchießstandCbx.TabIndex = 12;
            // 
            // AufsichtCbx
            // 
            AufsichtCbx.FormattingEnabled = true;
            AufsichtCbx.Location = new Point(1864, 592);
            AufsichtCbx.Name = "AufsichtCbx";
            AufsichtCbx.Size = new Size(193, 38);
            AufsichtCbx.TabIndex = 13;
            // 
            // DatumTxt
            // 
            DatumTxt.Location = new Point(1864, 241);
            DatumTxt.Name = "DatumTxt";
            DatumTxt.Size = new Size(193, 35);
            DatumTxt.TabIndex = 14;
            // 
            // SchusszahlTxt
            // 
            SchusszahlTxt.Location = new Point(1864, 414);
            SchusszahlTxt.Name = "SchusszahlTxt";
            SchusszahlTxt.Size = new Size(193, 35);
            SchusszahlTxt.TabIndex = 15;
            SchusszahlTxt.KeyPress += SchusszahlTxt_KeyPress;
            // 
            // SafeBtn
            // 
            SafeBtn.FlatAppearance.BorderSize = 0;
            SafeBtn.FlatStyle = FlatStyle.Flat;
            SafeBtn.Location = new Point(1677, 808);
            SafeBtn.Name = "SafeBtn";
            SafeBtn.Size = new Size(380, 54);
            SafeBtn.TabIndex = 16;
            SafeBtn.Text = "Hinzufügen";
            SafeBtn.UseVisualStyleBackColor = true;
            SafeBtn.Click += SafeBtn_Click;
            // 
            // EditBtn
            // 
            EditBtn.FlatAppearance.BorderSize = 0;
            EditBtn.FlatStyle = FlatStyle.Flat;
            EditBtn.Location = new Point(1677, 808);
            EditBtn.Name = "EditBtn";
            EditBtn.Size = new Size(380, 54);
            EditBtn.TabIndex = 17;
            EditBtn.Text = "Ändern";
            EditBtn.UseVisualStyleBackColor = true;
            EditBtn.Click += EditBtn_Click;
            // 
            // PrintPb
            // 
            PrintPb.Image = Properties.Resources.printing1;
            PrintPb.Location = new Point(1957, 35);
            PrintPb.Name = "PrintPb";
            PrintPb.Size = new Size(60, 60);
            PrintPb.TabIndex = 18;
            PrintPb.TabStop = false;
            PrintPb.Click += PrintPb_Click;
            // 
            // SchützenSortByYearCbx
            // 
            SchützenSortByYearCbx.FormattingEnabled = true;
            SchützenSortByYearCbx.Location = new Point(282, 136);
            SchützenSortByYearCbx.Name = "SchützenSortByYearCbx";
            SchützenSortByYearCbx.Size = new Size(221, 38);
            SchützenSortByYearCbx.TabIndex = 19;
            SchützenSortByYearCbx.SelectedIndexChanged += SchützenSortByYearCbx_SelectedIndexChanged;
            // 
            // WettbewerbCbx
            // 
            WettbewerbCbx.FormattingEnabled = true;
            WettbewerbCbx.Location = new Point(1864, 297);
            WettbewerbCbx.Name = "WettbewerbCbx";
            WettbewerbCbx.Size = new Size(193, 38);
            WettbewerbCbx.TabIndex = 21;
            WettbewerbCbx.SelectedIndexChanged += WettbewerbCbx_SelectedIndexChanged;
            // 
            // WettbewerbLbl
            // 
            WettbewerbLbl.AutoSize = true;
            WettbewerbLbl.Location = new Point(1677, 297);
            WettbewerbLbl.Name = "WettbewerbLbl";
            WettbewerbLbl.Size = new Size(125, 30);
            WettbewerbLbl.TabIndex = 20;
            WettbewerbLbl.Text = "Wettbewerb";
            // 
            // ErgebnisTxt
            // 
            ErgebnisTxt.Location = new Point(1864, 470);
            ErgebnisTxt.Name = "ErgebnisTxt";
            ErgebnisTxt.Size = new Size(193, 35);
            ErgebnisTxt.TabIndex = 23;
            // 
            // ErgebnisLbl
            // 
            ErgebnisLbl.AutoSize = true;
            ErgebnisLbl.Location = new Point(1677, 470);
            ErgebnisLbl.Name = "ErgebnisLbl";
            ErgebnisLbl.Size = new Size(92, 30);
            ErgebnisLbl.TabIndex = 22;
            ErgebnisLbl.Text = "Ergebnis";
            // 
            // ClearAllFieldsBtn
            // 
            ClearAllFieldsBtn.FlatAppearance.BorderSize = 0;
            ClearAllFieldsBtn.FlatStyle = FlatStyle.Flat;
            ClearAllFieldsBtn.Location = new Point(1677, 868);
            ClearAllFieldsBtn.Name = "ClearAllFieldsBtn";
            ClearAllFieldsBtn.Size = new Size(380, 54);
            ClearAllFieldsBtn.TabIndex = 24;
            ClearAllFieldsBtn.Text = "Felder Leeren";
            ClearAllFieldsBtn.UseVisualStyleBackColor = true;
            ClearAllFieldsBtn.Click += ClearAllFieldsBtn_Click;
            // 
            // KladdenMenuStrip
            // 
            KladdenMenuStrip.Font = new Font("Segoe UI", 12F);
            KladdenMenuStrip.Items.AddRange(new ToolStripItem[] { schützenToolStripMenuItem, schießständeToolStripMenuItem, verbändeToolStripMenuItem, vereineToolStripMenuItem, waffenToolStripMenuItem, wettbewerbeToolStripMenuItem, aufsichtenToolStripMenuItem, statistikenToolStripMenuItem });
            KladdenMenuStrip.Location = new Point(0, 0);
            KladdenMenuStrip.Name = "KladdenMenuStrip";
            KladdenMenuStrip.Size = new Size(2079, 29);
            KladdenMenuStrip.TabIndex = 25;
            KladdenMenuStrip.Text = "menuStrip1";
            // 
            // schützenToolStripMenuItem
            // 
            schützenToolStripMenuItem.Name = "schützenToolStripMenuItem";
            schützenToolStripMenuItem.Size = new Size(85, 25);
            schützenToolStripMenuItem.Text = "Schützen";
            schützenToolStripMenuItem.Click += schützenToolStripMenuItem_Click_1;
            // 
            // schießständeToolStripMenuItem
            // 
            schießständeToolStripMenuItem.Name = "schießständeToolStripMenuItem";
            schießständeToolStripMenuItem.Size = new Size(114, 25);
            schießständeToolStripMenuItem.Text = "Schießstände";
            schießständeToolStripMenuItem.Click += schießständeToolStripMenuItem_Click_1;
            // 
            // verbändeToolStripMenuItem
            // 
            verbändeToolStripMenuItem.Name = "verbändeToolStripMenuItem";
            verbändeToolStripMenuItem.Size = new Size(88, 25);
            verbändeToolStripMenuItem.Text = "Verbände";
            verbändeToolStripMenuItem.Click += verbändeToolStripMenuItem_Click_1;
            // 
            // vereineToolStripMenuItem
            // 
            vereineToolStripMenuItem.Name = "vereineToolStripMenuItem";
            vereineToolStripMenuItem.Size = new Size(74, 25);
            vereineToolStripMenuItem.Text = "Vereine";
            vereineToolStripMenuItem.Click += vereineToolStripMenuItem_Click_1;
            // 
            // waffenToolStripMenuItem
            // 
            waffenToolStripMenuItem.Name = "waffenToolStripMenuItem";
            waffenToolStripMenuItem.Size = new Size(71, 25);
            waffenToolStripMenuItem.Text = "Waffen";
            waffenToolStripMenuItem.Click += waffenToolStripMenuItem_Click_1;
            // 
            // wettbewerbeToolStripMenuItem
            // 
            wettbewerbeToolStripMenuItem.Name = "wettbewerbeToolStripMenuItem";
            wettbewerbeToolStripMenuItem.Size = new Size(114, 25);
            wettbewerbeToolStripMenuItem.Text = "Wettbewerbe";
            wettbewerbeToolStripMenuItem.Click += wettbewerbeToolStripMenuItem_Click_1;
            // 
            // aufsichtenToolStripMenuItem
            // 
            aufsichtenToolStripMenuItem.Name = "aufsichtenToolStripMenuItem";
            aufsichtenToolStripMenuItem.Size = new Size(95, 25);
            aufsichtenToolStripMenuItem.Text = "Aufsichten";
            aufsichtenToolStripMenuItem.Click += aufsichtenToolStripMenuItem_Click_1;
            // 
            // statistikenToolStripMenuItem
            // 
            statistikenToolStripMenuItem.Name = "statistikenToolStripMenuItem";
            statistikenToolStripMenuItem.Size = new Size(93, 25);
            statistikenToolStripMenuItem.Text = "Statistiken";
            statistikenToolStripMenuItem.Click += statistikenToolStripMenuItem_Click;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { testToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(94, 26);
            // 
            // testToolStripMenuItem
            // 
            testToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { testToolStripMenuItem1 });
            testToolStripMenuItem.Name = "testToolStripMenuItem";
            testToolStripMenuItem.Size = new Size(93, 22);
            testToolStripMenuItem.Text = "test";
            // 
            // testToolStripMenuItem1
            // 
            testToolStripMenuItem1.Name = "testToolStripMenuItem1";
            testToolStripMenuItem1.Size = new Size(93, 22);
            testToolStripMenuItem1.Text = "test";
            // 
            // SchützeLbl
            // 
            SchützeLbl.AutoSize = true;
            SchützeLbl.Location = new Point(1677, 183);
            SchützeLbl.Name = "SchützeLbl";
            SchützeLbl.Size = new Size(86, 30);
            SchützeLbl.TabIndex = 4;
            SchützeLbl.Text = "Schütze";
            // 
            // KladdenForm
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2079, 1041);
            Controls.Add(ClearAllFieldsBtn);
            Controls.Add(ErgebnisTxt);
            Controls.Add(ErgebnisLbl);
            Controls.Add(WettbewerbCbx);
            Controls.Add(WettbewerbLbl);
            Controls.Add(SchützenSortByYearCbx);
            Controls.Add(PrintPb);
            Controls.Add(EditBtn);
            Controls.Add(SafeBtn);
            Controls.Add(SchusszahlTxt);
            Controls.Add(DatumTxt);
            Controls.Add(AufsichtCbx);
            Controls.Add(SchießstandCbx);
            Controls.Add(WaffeCbx);
            Controls.Add(SchützeCbx);
            Controls.Add(AufsichtLbl);
            Controls.Add(SchießstandLbl);
            Controls.Add(SchusszahlLbl);
            Controls.Add(WaffeLbl);
            Controls.Add(DatumLbl);
            Controls.Add(SchützeLbl);
            Controls.Add(SchützenSortByNameCbx);
            Controls.Add(HeaderTxtLbl);
            Controls.Add(KladdenGrid);
            Controls.Add(KladdenMenuStrip);
            Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MainMenuStrip = KladdenMenuStrip;
            Margin = new Padding(5, 6, 5, 6);
            Name = "KladdenForm";
            Text = "Schießsportkladde";
            Load += KladdenForm_Load;
            ((System.ComponentModel.ISupportInitialize)KladdenGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)PrintPb).EndInit();
            KladdenMenuStrip.ResumeLayout(false);
            KladdenMenuStrip.PerformLayout();
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView KladdenGrid;
        private Label HeaderTxtLbl;
        private ComboBox SchützenSortByNameCbx;
        private Label DatumLbl;
        private Label WaffeLbl;
        private Label SchusszahlLbl;
        private Label AufsichtLbl;
        private Label SchießstandLbl;
        private ComboBox SchützeCbx;
        private ComboBox WaffeCbx;
        private ComboBox SchießstandCbx;
        private ComboBox AufsichtCbx;
        private TextBox DatumTxt;
        private TextBox SchusszahlTxt;
        private Button SafeBtn;
        private Button EditBtn;
        private PictureBox PrintPb;
        private ComboBox SchützenSortByYearCbx;
        private ComboBox WettbewerbCbx;
        private Label WettbewerbLbl;
        private TextBox ErgebnisTxt;
        private Label ErgebnisLbl;
        private Button ClearAllFieldsBtn;
        private MenuStrip KladdenMenuStrip;
        private ToolStripMenuItem schützenToolStripMenuItem;
        private ToolStripMenuItem schießständeToolStripMenuItem;
        private ToolStripMenuItem verbändeToolStripMenuItem;
        private ToolStripMenuItem vereineToolStripMenuItem;
        private ToolStripMenuItem waffenToolStripMenuItem;
        private ToolStripMenuItem wettbewerbeToolStripMenuItem;
        private ToolStripMenuItem aufsichtenToolStripMenuItem;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem testToolStripMenuItem;
        private ToolStripMenuItem testToolStripMenuItem1;
        private Label SchützeLbl;
        private ToolStripMenuItem statistikenToolStripMenuItem;
    }
}

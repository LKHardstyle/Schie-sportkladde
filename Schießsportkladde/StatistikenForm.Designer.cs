namespace Schießsportkladde
{
    partial class StatistikenForm
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
            HeaderTextLbl = new Label();
            SchützenCbx = new ComboBox();
            ErgebnisStatsPanel = new Panel();
            ErgebnisübersichtLbl = new Label();
            WaffenübersichtLbl = new Label();
            WaffenStatsPanel = new Panel();
            SuspendLayout();
            // 
            // HeaderTextLbl
            // 
            HeaderTextLbl.AutoSize = true;
            HeaderTextLbl.Font = new Font("Segoe UI", 40F);
            HeaderTextLbl.Location = new Point(748, 9);
            HeaderTextLbl.Name = "HeaderTextLbl";
            HeaderTextLbl.Size = new Size(355, 72);
            HeaderTextLbl.TabIndex = 0;
            HeaderTextLbl.Text = "Statistiken für";
            // 
            // SchützenCbx
            // 
            SchützenCbx.FormattingEnabled = true;
            SchützenCbx.Location = new Point(11, 39);
            SchützenCbx.Name = "SchützenCbx";
            SchützenCbx.Size = new Size(204, 38);
            SchützenCbx.TabIndex = 1;
            SchützenCbx.SelectedIndexChanged += SchützenCbx_SelectedIndexChanged;
            // 
            // ErgebnisStatsPanel
            // 
            ErgebnisStatsPanel.AutoScroll = true;
            ErgebnisStatsPanel.Location = new Point(18, 161);
            ErgebnisStatsPanel.Name = "ErgebnisStatsPanel";
            ErgebnisStatsPanel.Size = new Size(818, 458);
            ErgebnisStatsPanel.TabIndex = 2;
            // 
            // ErgebnisübersichtLbl
            // 
            ErgebnisübersichtLbl.AutoSize = true;
            ErgebnisübersichtLbl.Location = new Point(336, 114);
            ErgebnisübersichtLbl.Name = "ErgebnisübersichtLbl";
            ErgebnisübersichtLbl.Size = new Size(177, 30);
            ErgebnisübersichtLbl.TabIndex = 3;
            ErgebnisübersichtLbl.Text = "Ergebnisübersicht";
            // 
            // WaffenübersichtLbl
            // 
            WaffenübersichtLbl.AutoSize = true;
            WaffenübersichtLbl.Location = new Point(1295, 114);
            WaffenübersichtLbl.Name = "WaffenübersichtLbl";
            WaffenübersichtLbl.Size = new Size(165, 30);
            WaffenübersichtLbl.TabIndex = 5;
            WaffenübersichtLbl.Text = "Waffenübersicht";
            // 
            // WaffenStatsPanel
            // 
            WaffenStatsPanel.AutoScroll = true;
            WaffenStatsPanel.Location = new Point(977, 161);
            WaffenStatsPanel.Name = "WaffenStatsPanel";
            WaffenStatsPanel.Size = new Size(818, 458);
            WaffenStatsPanel.TabIndex = 4;
            // 
            // StatistikenForm
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1896, 900);
            Controls.Add(WaffenübersichtLbl);
            Controls.Add(WaffenStatsPanel);
            Controls.Add(ErgebnisübersichtLbl);
            Controls.Add(ErgebnisStatsPanel);
            Controls.Add(SchützenCbx);
            Controls.Add(HeaderTextLbl);
            Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(5, 6, 5, 6);
            Name = "StatistikenForm";
            Text = "Statistiken";
            Load += StatistikenForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label HeaderTextLbl;
        private ComboBox SchützenCbx;
        private Panel ErgebnisStatsPanel;
        private Label ErgebnisübersichtLbl;
        private Label WaffenübersichtLbl;
        private Panel WaffenStatsPanel;
    }
}
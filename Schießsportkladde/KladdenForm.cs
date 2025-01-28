using DataAccessLayer.Contracts;
using DomainModel.Models;
using DataAccessLayer.Repositories;
using Microsoft.VisualBasic;
using System.ComponentModel;
using System.DirectoryServices;
using DataAccessLayer.CustomQueryResults;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Forms;
using Schießsportkladde.Services;
using Schießsportkladde.Styles;
using Newtonsoft.Json.Linq;


namespace Schießsportkladde
{
    public partial class KladdenForm : Form
    {
        private readonly IServiceProvider _serviceProvider;
        readonly IKladdenRepository _kladdenRepository;
        readonly ISchützenRepository _schützenRepository;
        readonly IWettbewerbsRepository _wettbewerbsRepository;
        readonly IWaffenRepository _waffenRepository;
        readonly ISchießstandRepository _schießstandRepository;
        readonly IAufsichtRepository _aufsichtRepository;
        private int _kladdeToEditId;
        private bool CbxFilled = false;

        public KladdenForm(IKladdenRepository kladdenRepository, ISchützenRepository schützenRepository, IServiceProvider serviceProvider, IWettbewerbsRepository wettbewerbsRepository, IWaffenRepository waffenRepository, ISchießstandRepository schießstandRepository, IAufsichtRepository aufsichtRepository)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _kladdenRepository = kladdenRepository;
            _schützenRepository = schützenRepository;
            _wettbewerbsRepository = wettbewerbsRepository;
            _waffenRepository = waffenRepository;
            _schießstandRepository = schießstandRepository;
            _aufsichtRepository = aufsichtRepository;
            _kladdenRepository.OnError += (message) => MessageBox.Show(message);
            _schützenRepository.OnError += (message) => MessageBox.Show(message);
            _waffenRepository.OnError += (message) => MessageBox.Show(message);
            _schießstandRepository.OnError += (message) => MessageBox.Show(message);
            _aufsichtRepository.OnError += (message) => MessageBox.Show(message);

            ApplyStyles();
        }

        private void KladdenForm_Load(object sender, EventArgs e)
        {
            CustomizeForm();
            FillCbxs();
            CustomizeGridAppearance();
            RefreshGridData();

            SafeBtn.Visible = true;
            EditBtn.Visible = false;

            PrintPb.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        private void CustomizeForm()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }
        private async void FillCbxs()
        {
            SchützenSortByNameCbx.DataSource = null;
            SchützenSortByNameCbx.Items.Clear();

            SchützeCbx.DataSource = null;
            SchützeCbx.Items.Clear();

            WettbewerbCbx.DataSource = null;
            WettbewerbCbx.Items.Clear();

            WaffeCbx.DataSource = null;
            WaffeCbx.Items.Clear();

            SchießstandCbx.DataSource = null;
            SchießstandCbx.Items.Clear();

            AufsichtCbx.DataSource = null;
            AufsichtCbx.Items.Clear();

            List<SchützenName> schützen = new List<SchützenName>();
            List<SchützenName> filterListName = new List<SchützenName>();
            List<KladdenYear> jahre = new List<KladdenYear>();
            List<KladdenYear> filterListYear = new List<KladdenYear>();
            List<WaffenNameDisziplin> waffen = new List<WaffenNameDisziplin>();
            List<Schießtstand> schießstände = new List<Schießtstand>();
            List<Aufsicht> aufsichten = new List<Aufsicht>();
            List<Wettbewerb> wettbewerbe = new List<Wettbewerb>();

            schützen = await _schützenRepository.GetSchützenName();

            filterListName.Add(new SchützenName(0, "Alle Schützen"));
            filterListName.AddRange(schützen);

            SchützenSortByNameCbx.DataSource = filterListName;
            SchützenSortByNameCbx.DisplayMember = "Name";

            jahre = await _kladdenRepository.GetKladdenYears();

            filterListYear.Add(new KladdenYear("Alle Jahre"));
            filterListYear.AddRange(jahre);

            SchützenSortByYearCbx.DataSource = filterListYear;
            SchützenSortByYearCbx.DisplayMember = "Date";

            SchützeCbx.DataSource = schützen;
            SchützeCbx.DisplayMember = "Name";

            wettbewerbe = await _wettbewerbsRepository.GetWettbewerbe();
            WettbewerbCbx.DataSource = wettbewerbe;
            WettbewerbCbx.DisplayMember = "Name";

            waffen = await _waffenRepository.GetWaffenNameDisziplin();
            WaffeCbx.DataSource = waffen;
            WaffeCbx.DisplayMember = "Name";

            schießstände = await _schießstandRepository.GetSchießtstände();
            SchießstandCbx.DataSource = schießstände;
            SchießstandCbx.DisplayMember = "Schießstand";

            aufsichten = await _aufsichtRepository.GetAufsicht();
            AufsichtCbx.DataSource = aufsichten;
            AufsichtCbx.DisplayMember = "Name";

            CbxFilled = true;
        }
        private async void RefreshGridData()
        {
            // Scrollposition speichern
            int vertikaleScrollPosition = KladdenGrid.FirstDisplayedScrollingRowIndex;
            int horizontaleScrollPosition = KladdenGrid.HorizontalScrollingOffset;

            SchützenName selectedSchütze = (SchützenName)SchützenSortByNameCbx.SelectedItem;
            KladdenYear selectedYear = (KladdenYear)SchützenSortByYearCbx.SelectedItem;

            // Case 1: Show all data if no specific Schütze is selected and "Alle Jahre" is selected
            if ((selectedSchütze == null || selectedSchütze.Id == 0) && (selectedYear == null || selectedYear.Date == "Alle Jahre"))
            {
                KladdenGrid.DataSource = await _kladdenRepository.getKladde();
            }
            // Case 2: Filter by Schütze only (valid Schütze selected, year is "Alle Jahre" or not selected)
            else if (selectedSchütze != null && selectedSchütze.Id != 0 && (selectedYear == null || selectedYear.Date == "Alle Jahre"))
            {
                KladdenGrid.DataSource = await _kladdenRepository.getKladde(schützeId: selectedSchütze.Id);
            }
            // Case 3: Filter by Year only (valid year selected, Schütze is not selected or "all names")
            else if ((selectedSchütze == null || selectedSchütze.Id == 0) && selectedYear != null && selectedYear.Date != "Alle Jahre")
            {
                KladdenGrid.DataSource = await _kladdenRepository.getKladde(date: selectedYear.Date);
            }
            // Case 4: Filter by both Schütze and Year (both valid selections)
            else if (selectedSchütze != null && selectedSchütze.Id != 0 && selectedYear != null && selectedYear.Date != "Alle Jahre")
            {
                KladdenGrid.DataSource = await _kladdenRepository.getKladde(schützeId: selectedSchütze.Id, date: selectedYear.Date);
            }

            // Scrollposition wiederherstellen
            if (vertikaleScrollPosition >= 0 && vertikaleScrollPosition < KladdenGrid.RowCount)
            {
                KladdenGrid.FirstDisplayedScrollingRowIndex = vertikaleScrollPosition;
            }

            if (horizontaleScrollPosition >= 0)
            {
                KladdenGrid.HorizontalScrollingOffset = horizontaleScrollPosition;
            }
        }
        private void CustomizeGridAppearance()
        {
            KladdenGrid.AutoGenerateColumns = false;
            KladdenGrid.EnableHeadersVisualStyles = false;

            KladdenGrid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            KladdenGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewColumn[] columns = new DataGridViewColumn[13];
            columns[0] = new DataGridViewTextBoxColumn() { DataPropertyName = "Id", Visible = false };
            columns[1] = new DataGridViewTextBoxColumn() { DataPropertyName = "Schütze", Visible = false };
            columns[2] = new DataGridViewTextBoxColumn() { DataPropertyName = "Datum", HeaderText = "Datum", SortMode = DataGridViewColumnSortMode.Automatic };
            columns[3] = new DataGridViewTextBoxColumn() { DataPropertyName = "Wettbewerb", HeaderText = "Wettbewerb", SortMode = DataGridViewColumnSortMode.Automatic };
            columns[4] = new DataGridViewTextBoxColumn() { DataPropertyName = "Waffe", HeaderText = "Waffe", SortMode = DataGridViewColumnSortMode.Automatic };
            columns[5] = new DataGridViewTextBoxColumn() { DataPropertyName = "Kaliber", HeaderText = "Kaliber", SortMode = DataGridViewColumnSortMode.Automatic };
            columns[6] = new DataGridViewTextBoxColumn() { DataPropertyName = "Schusszahl", HeaderText = "Schusszahl", SortMode = DataGridViewColumnSortMode.Automatic };
            columns[7] = new DataGridViewTextBoxColumn() { DataPropertyName = "Ergebnis", HeaderText = "Ergebnis", SortMode = DataGridViewColumnSortMode.Automatic };
            columns[8] = new DataGridViewTextBoxColumn() { DataPropertyName = "Disziplin", HeaderText = "Disziplin", SortMode = DataGridViewColumnSortMode.Automatic };
            columns[9] = new DataGridViewTextBoxColumn() { DataPropertyName = "Schießstand", HeaderText = "Schießstand", SortMode = DataGridViewColumnSortMode.Automatic };
            columns[10] = new DataGridViewTextBoxColumn() { DataPropertyName = "Aufsicht", HeaderText = "Aufsicht", SortMode = DataGridViewColumnSortMode.Automatic };
            columns[11] = new DataGridViewButtonColumn()
            {
                Text = "Löschen",
                Name = "DeleteBtn",
                HeaderText = "",
                UseColumnTextForButtonValue = true

            };
            columns[12] = new DataGridViewButtonColumn()
            {
                Text = "Ändern",
                Name = "EditBtn",
                HeaderText = "",
                UseColumnTextForButtonValue = true
            };

            KladdenGrid.Columns.Clear();
            KladdenGrid.Columns.AddRange(columns);
        }
        private void CalculateGridWidth()
        {
            // Automatisches Anpassen der Spalten an den Inhalt
            foreach (DataGridViewColumn column in KladdenGrid.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            // Berechne die Gesamtbreite
            int gesamtBreite = KladdenGrid.RowHeadersVisible ? KladdenGrid.RowHeadersWidth : 0;

            foreach (DataGridViewColumn column in KladdenGrid.Columns)
            {
                if (column.Visible)
                    gesamtBreite += column.Width;
            }

            // Prüfe, ob eine vertikale Scrollbar vorhanden ist, und füge ggf. ihre Breite hinzu
            if (KladdenGrid.Controls.OfType<ScrollBar>().Any(sb => sb.Visible))
            {
                gesamtBreite += SystemInformation.VerticalScrollBarWidth;
            }

            // Setze die neue Breite des DataGridView
            KladdenGrid.Width = gesamtBreite;
        }
        private async void SchützenSortCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGridData();
        }
        private void SchützenSortByYearCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGridData();
        }
        private DialogResult ShowForm(Form form)
        {
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MaximizeBox = false;
            form.MinimizeBox = false;
            form.FormBorderStyle = FormBorderStyle.FixedSingle;

            return form.ShowDialog();
        }
        private async void KladdenGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && KladdenGrid.CurrentCell is DataGridViewButtonCell)
            {
                JoinedKladde clickedKladde = (JoinedKladde)KladdenGrid.Rows[e.RowIndex].DataBoundItem;

                if (KladdenGrid.CurrentCell.OwningColumn.Name == "EditBtn")
                {
                    FillFormForEdit(clickedKladde);
                }
                else if (KladdenGrid.CurrentCell.OwningColumn.Name == "DeleteBtn")
                {
                    DialogResult result = MessageBox.Show(
                        "Möchten sie den Eintrag Löschen?",
                        "Bestätigung",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                        );
                    if (result == DialogResult.Yes)
                    {
                        await _kladdenRepository.DeleteKladde(clickedKladde);

                        RefreshGridData();
                        ClearAllFields();
                    }
                    else
                        return;
                }
            }
        }
        private void FillFormForEdit(JoinedKladde clickedKladde)
        {
            _kladdeToEditId = clickedKladde.Id;

            int schützenIndex = SchützeCbx.FindStringExact(clickedKladde.Schütze);
            SchützeCbx.SelectedIndex = schützenIndex;

            DatumTxt.Text = clickedKladde.Datum;

            int wettbewerbIndex = WettbewerbCbx.FindStringExact(clickedKladde.Wettbewerb);
            WettbewerbCbx.SelectedIndex = wettbewerbIndex;

            int waffeIndex = WaffeCbx.FindStringExact(clickedKladde.Waffe);
            WaffeCbx.SelectedIndex = waffeIndex;

            int schießstandIndex = SchießstandCbx.FindStringExact(clickedKladde.Schießstand);
            SchießstandCbx.SelectedIndex = schießstandIndex;

            SchusszahlTxt.Text = clickedKladde.Schusszahl.ToString();

            ErgebnisTxt.Text = clickedKladde.Ergebnis.ToString();

            int aufsichtIndex = AufsichtCbx.FindStringExact(clickedKladde.Aufsicht);
            AufsichtCbx.SelectedIndex = aufsichtIndex;

            SafeBtn.Visible = false;
            EditBtn.Visible = true;
        }
        private void ClearAllFields()
        {
            DatumTxt.Text = string.Empty;
            SchusszahlTxt.Text = string.Empty;
            ErgebnisTxt.Text = string.Empty;

            EditBtn.Visible = false;
            SafeBtn.Visible = true;
        }
        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (!isValid())
                return;

            Kladde kladde = new Kladde();
            SchützenName selectedSchützen = SchützeCbx.SelectedItem as SchützenName;
            Wettbewerb selectedWettbewerb = WettbewerbCbx.SelectedItem as Wettbewerb;
            WaffenNameDisziplin selectedWaffen = WaffeCbx.SelectedItem as WaffenNameDisziplin;
            Schießtstand selectedSchießstand = SchießstandCbx.SelectedItem as Schießtstand;
            Aufsicht selectedAufsicht = AufsichtCbx.SelectedItem as Aufsicht;

            kladde.Id = _kladdeToEditId;
            kladde.SchützeId = selectedSchützen.Id;
            kladde.Datum = DatumTxt.Text;
            kladde.WettbewerbId = selectedWettbewerb.Id;
            kladde.WaffeId = selectedWaffen.Id;
            kladde.Schusszahl = Convert.ToInt32(SchusszahlTxt.Text);
            kladde.SchießstandId = selectedSchießstand.Id;
            kladde.Ergebnis = int.Parse(ErgebnisTxt.Text);
            kladde.AufsichtId = selectedAufsicht.Id;

            _kladdenRepository.EditKladde(kladde);

            MessageBox.Show("Erfolgreich geändert");

            ClearAllFields();
            RefreshGridData();
        }
        private bool isValid()
        {
            bool isValid = true;

            string message = string.Empty;
            DateTime parsedDate;

            if (string.IsNullOrEmpty(DatumTxt.Text))
            {
                isValid = false;
                message += "Datum eingeben. \n";
            }
            else if (!DateTime.TryParseExact(DatumTxt.Text, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                isValid = false;
                message += "Richtiges Datum eingeben (Tag.Monat.Jahr)";
            }

            if (string.IsNullOrEmpty(SchusszahlTxt.Text))
            {
                isValid = false;
                message += "Schusszahl eingeben. \n";
            }

            if (!isValid)
                MessageBox.Show(message, "Eingaben fehlen!");

            return isValid;
        }
        private void SafeBtn_Click(object sender, EventArgs e)
        {
            if (!isValid())
                return;

            Kladde kladde = new Kladde();
            SchützenName selectedSchützenId = SchützeCbx.SelectedItem as SchützenName;
            Wettbewerb selectedwettbewerb = WettbewerbCbx.SelectedItem as Wettbewerb;
            WaffenNameDisziplin selectedWaffenId = WaffeCbx.SelectedItem as WaffenNameDisziplin;
            Schießtstand selectedSchießstand = SchießstandCbx.SelectedItem as Schießtstand;
            Aufsicht selectedAufsicht = AufsichtCbx.SelectedItem as Aufsicht;

            kladde.Id = _kladdeToEditId;
            kladde.SchützeId = selectedSchützenId.Id;
            kladde.Datum = DatumTxt.Text;
            kladde.WettbewerbId = selectedwettbewerb.Id;
            kladde.WaffeId = selectedWaffenId.Id;
            kladde.Schusszahl = Convert.ToInt32(SchusszahlTxt.Text);
            kladde.SchießstandId = selectedSchießstand.Id;
            kladde.Ergebnis = Convert.ToInt32(ErgebnisTxt.Text);
            kladde.AufsichtId = selectedAufsicht.Id;

            _kladdenRepository.AddKladde(kladde);

            ClearAllFields();
            RefreshGridData();
        }
        private void schützenToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SchützenForm schützenForm = _serviceProvider.GetRequiredService<SchützenForm>();

            if (ShowForm(schützenForm) == DialogResult.Cancel)
            {
                RefreshGridData();
                FillCbxs();
            }
        }
        private void schießständeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SchießstandForm schießstandForm = _serviceProvider.GetRequiredService<SchießstandForm>();

            if (ShowForm(schießstandForm) == DialogResult.Cancel)
            {
                RefreshGridData();
                FillCbxs();
            }
        }
        private void verbändeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            VerbändeForm verbändeForm = _serviceProvider.GetRequiredService<VerbändeForm>();

            if (ShowForm(verbändeForm) == DialogResult.Cancel)
            {
                RefreshGridData();
                FillCbxs();
            }
        }
        private void vereineToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            VereineForm vereineForm = _serviceProvider.GetRequiredService<VereineForm>();

            if (ShowForm(vereineForm) == DialogResult.Cancel)
            {
                RefreshGridData();
                FillCbxs();
            }
        }
        private void waffenToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            WaffenForm waffenForm = _serviceProvider.GetRequiredService<WaffenForm>();

            if (ShowForm(waffenForm) == DialogResult.Cancel)
            {
                RefreshGridData();
                FillCbxs();
            }
        }
        private void wettbewerbeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            WettbewerbeForm wettbewerbeForm = _serviceProvider.GetRequiredService<WettbewerbeForm>();

            if (ShowForm(wettbewerbeForm) == DialogResult.Cancel)
            {
                RefreshGridData();
                FillCbxs();
            }
        }
        private void aufsichtenToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AufsichtForm aufischtForm = _serviceProvider.GetRequiredService<AufsichtForm>();

            if (ShowForm(aufischtForm) == DialogResult.Cancel)
            {
                RefreshGridData();
                FillCbxs();
            }
        }
        private void SchusszahlTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!isValidTextBox(SchusszahlTxt.Text, e.KeyChar, digit: true))
            {
                e.Handled = true;
            }
        }
        private bool isValidTextBox(string currentText, char keyPressed, bool? digit = null)
        {
            // If digitAndMinus is true, validate for digits and the '-' sign
            if (digit == true)
            {
                // Allow control keys (backspace, delete, etc.)
                if (char.IsControl(keyPressed))
                {
                    return true;
                }

                // Allow digits (0-9)
                if (char.IsDigit(keyPressed))
                {
                    return true;
                }

                // If none of the conditions are met, return false (invalid input)
                return false;
            }

            return true; // If no validation is required, return true            
        }
        private void PrintPb_Click(object sender, EventArgs e)
        {
            PrinterService printer = new PrinterService();

            printer.Print(KladdenGrid);
        }
        private void KladdenGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            CalculateGridWidth();
        }
        private async void WettbewerbCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CbxFilled)
            {
                List<WaffenNameDisziplin> waffen = new List<WaffenNameDisziplin>();
                Wettbewerb selectedWettbewerb = (Wettbewerb)WettbewerbCbx.SelectedItem;

                if (selectedWettbewerb != null)
                {
                    WaffeCbx.DataSource = null;
                    WaffeCbx.Items.Clear();

                    waffen = await _waffenRepository.GetWaffenNameDisziplin();

                    var waffensorted = waffen
                        .Where(waffe => waffe.Disziplin == selectedWettbewerb.SpO)
                        .ToList();

                    WaffeCbx.DataSource = waffensorted;
                    WaffeCbx.DisplayMember = "Name";
                    WaffeCbx.ValueMember = "Id";
                }
            }
        }
        private void ApplyStyles()
        {
            JObject themeConfig = ConfigurationManager.LoadThemeConfig();

            string primaryBgr = (string)themeConfig["primaryBgr"];
            string secondaryBgr = (string)themeConfig["secondaryBgr"];
            string primaryFgr = (string)themeConfig["primaryFgr"];
            string primaryBtnBgr = (string)themeConfig["primaryBtnBgr"];
            string secondaryBtnBgr = (string)themeConfig["secondaryBtnBgr"];
            string tertiaryBtnBgr = (string)themeConfig["tertiaryBtnBgr"];
            string primaryBtnFgr = (string)themeConfig["primaryBtnFgr"];
            string secondaryBtnFgr = (string)themeConfig["secondaryBtnFgr"];
            string tertiaryBtnFgr = (string)themeConfig["tertiaryBtnFgr"];
            string primaryCbxBgr = (string)themeConfig["primaryCbxBgr"];
            string primaryTbxBgr = (string)themeConfig["primaryTbxBgr"];
            string primaryDgvBgr = (string)themeConfig["primaryDgvBgr"];
            string primaryDgvFgr = (string)themeConfig["primaryDgvFgr"];

            //Back Color der Form
            this.BackColor = ColorTranslator.FromHtml(secondaryBgr);

            //Back Color der ComboBoxen
            SchützenSortByNameCbx.BackColor = ColorTranslator.FromHtml(primaryTbxBgr);
            SchützeCbx.BackColor = ColorTranslator.FromHtml(primaryCbxBgr);
            WettbewerbCbx.BackColor = ColorTranslator.FromHtml(primaryTbxBgr);
            SchützenSortByYearCbx.BackColor = ColorTranslator.FromHtml(primaryTbxBgr);
            WaffeCbx.BackColor = ColorTranslator.FromHtml(primaryTbxBgr);
            SchießstandCbx.BackColor = ColorTranslator.FromHtml(primaryTbxBgr);
            AufsichtCbx.BackColor = ColorTranslator.FromHtml(primaryTbxBgr);

            //Flat Style der Combo Boxen
            SchützenSortByYearCbx.FlatStyle = FlatStyle.Flat;
            SchützenSortByNameCbx.FlatStyle = FlatStyle.Flat;
            SchützeCbx.FlatStyle = FlatStyle.Flat;
            WettbewerbCbx.FlatStyle = FlatStyle.Flat;
            WaffeCbx.FlatStyle = FlatStyle.Flat;
            SchießstandCbx.FlatStyle = FlatStyle.Flat;
            AufsichtCbx.FlatStyle = FlatStyle.Flat;

            //Back & Fore Color der Buttons
            SafeBtn.BackColor = ColorTranslator.FromHtml(primaryBtnBgr);
            SafeBtn.ForeColor = ColorTranslator.FromHtml(primaryBtnFgr);
            EditBtn.BackColor = ColorTranslator.FromHtml(primaryBtnBgr);
            EditBtn.ForeColor = ColorTranslator.FromHtml(primaryBtnFgr);
            ClearAllFieldsBtn.BackColor = ColorTranslator.FromHtml(secondaryBtnBgr);
            ClearAllFieldsBtn.ForeColor = ColorTranslator.FromHtml(primaryBtnFgr);

            //Back & Fore Color
            HeaderTxtLbl.BackColor = ColorTranslator.FromHtml(secondaryBgr);
            HeaderTxtLbl.ForeColor = ColorTranslator.FromHtml(primaryFgr);
            SchützeLbl.BackColor = ColorTranslator.FromHtml(secondaryBgr);
            SchützeLbl.ForeColor = ColorTranslator.FromHtml(primaryFgr);
            DatumLbl.BackColor = ColorTranslator.FromHtml(secondaryBgr);
            DatumLbl.ForeColor = ColorTranslator.FromHtml(primaryFgr);
            WettbewerbLbl.BackColor = ColorTranslator.FromHtml(secondaryBgr);
            WettbewerbLbl.ForeColor = ColorTranslator.FromHtml(primaryFgr);
            WaffeLbl.BackColor = ColorTranslator.FromHtml(secondaryBgr);
            WaffeLbl.ForeColor = ColorTranslator.FromHtml(primaryFgr);
            SchusszahlLbl.BackColor = ColorTranslator.FromHtml(secondaryBgr);
            SchusszahlLbl.ForeColor = ColorTranslator.FromHtml(primaryFgr);
            ErgebnisLbl.BackColor = ColorTranslator.FromHtml(secondaryBgr);
            ErgebnisLbl.ForeColor = ColorTranslator.FromHtml(primaryFgr);
            SchießstandLbl.BackColor = ColorTranslator.FromHtml(secondaryBgr);
            SchießstandLbl.ForeColor = ColorTranslator.FromHtml(primaryFgr);
            AufsichtLbl.BackColor = ColorTranslator.FromHtml(secondaryBgr);
            AufsichtLbl.ForeColor = ColorTranslator.FromHtml(primaryFgr);

            //Back & Fore Color sowie Hover Color des Menu Strip
            KladdenMenuStrip.BackColor = ColorTranslator.FromHtml(secondaryBgr);
            KladdenMenuStrip.ForeColor = ColorTranslator.FromHtml(primaryFgr);
            KladdenMenuStrip.Renderer = new ToolStripProfessionalRenderer(new MenuStripColorTable());

            //Back & Fore Color der Textboxen
            DatumTxt.BackColor = ColorTranslator.FromHtml(primaryTbxBgr);
            DatumTxt.BorderStyle = BorderStyle.FixedSingle;
            SchusszahlTxt.BackColor = ColorTranslator.FromHtml(primaryTbxBgr);
            SchusszahlTxt.BorderStyle = BorderStyle.FixedSingle;
            ErgebnisTxt.BackColor = ColorTranslator.FromHtml(primaryTbxBgr);
            ErgebnisTxt.BorderStyle = BorderStyle.FixedSingle;

            //Grid
            //Hintergrundfarben
            KladdenGrid.RowTemplate.Height = 30;
            KladdenGrid.BackgroundColor = ColorTranslator.FromHtml(primaryDgvBgr);
            KladdenGrid.GridColor = Color.Black;

            //Zellfarben
            KladdenGrid.DefaultCellStyle.BackColor = ColorTranslator.FromHtml(primaryDgvBgr);
            KladdenGrid.DefaultCellStyle.ForeColor = Color.Black;
            KladdenGrid.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml(primaryBtnBgr);
            KladdenGrid.DefaultCellStyle.SelectionForeColor = Color.Black;

            //Headerfarben
            KladdenGrid.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml(primaryDgvBgr);
            KladdenGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            KladdenGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            KladdenGrid.RowHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml(primaryDgvBgr);
            KladdenGrid.RowHeadersDefaultCellStyle.ForeColor = Color.Black;
            KladdenGrid.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        }
        private void ClearAllFieldsBtn_Click(object sender, EventArgs e)
        {
            ClearAllFields();
        }
        private void statistikenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StatistikenForm statForm = _serviceProvider.GetRequiredService<StatistikenForm>();
            ShowForm(statForm);
        }
    }
}

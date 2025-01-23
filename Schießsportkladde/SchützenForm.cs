using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccessLayer;
using DataAccessLayer.Contracts;
using DataAccessLayer.CustomQueryResults;
using DataAccessLayer.Repositories;
using DomainModel.Models;
using Newtonsoft.Json.Linq;
using Schießsportkladde.Services;
using Schießsportkladde.Styles;

namespace Schießsportkladde
{
    public partial class SchützenForm : Form
    {
        readonly ISchützenRepository _schützenRepository;
        readonly IVerbändeRepository _verbandsRepository;
        readonly IVereineRepository _vereinsRepository;
        private int _schützeToEdit;
        public SchützenForm(ISchützenRepository schützenRepository, IVerbändeRepository verbandsRepository, IVereineRepository vereinsRepository)
        {
            InitializeComponent();
            _schützenRepository = schützenRepository;
            _verbandsRepository = verbandsRepository;
            _vereinsRepository = vereinsRepository;
            _schützenRepository.OnError += (message) => MessageBox.Show(message);

            ApplyStyles();
        }

        private void SchützenForm_Load(object sender, EventArgs e)
        {
            FillCbx();
            RefreshGridData();
            CustomizeGridData();

            EditBtn.Visible = false;
            SafeBtn.Visible = true;
        }
        private void ClearAllFields()
        {
            NameTxt.Text = string.Empty;
            DateTxt.Text = string.Empty;
            AdressTxt.Text = string.Empty;
            CountryTxt.Text = string.Empty;
            PhoneTxt.Text = string.Empty;
            MailTxt.Text = string.Empty;
            MemberNrVerbandTxt.Text = string.Empty;
            EntryDateVerbandTxt.Text = string.Empty;
            MemberNrVereinTxt.Text = string.Empty;
            EntryDateVereinTxt.Text = string.Empty;

            EditBtn.Visible = false;
            SafeBtn.Visible = true;
        }
        private async void RefreshGridData()
        {
            SchützenGrid.DataSource = await _schützenRepository.GetSchützen();
        }
        private void FillFormForEdit(JoinedSchütze clickedSchütze)
        {
            _schützeToEdit = clickedSchütze.Id;
            NameTxt.Text = clickedSchütze.SName;
            DateTxt.Text = clickedSchütze.Geburtsdatum;
            AdressTxt.Text = clickedSchütze.StraßeNr;
            CountryTxt.Text = clickedSchütze.PLZOrt;
            PhoneTxt.Text = clickedSchütze.TelefonNr;
            MailTxt.Text = clickedSchütze.Email;

            int VerbandIndex = VerbandCbx.FindStringExact(clickedSchütze.VBName);
            VerbandCbx.SelectedIndex = VerbandIndex;

            MemberNrVerbandTxt.Text = clickedSchütze.MitgliedsNrVerband;
            EntryDateVerbandTxt.Text = clickedSchütze.EintrittVerband;

            int VereinIndex = VereinCbx.FindStringExact(clickedSchütze.VEName);
            VereinCbx.SelectedIndex = VereinIndex;

            MemberNrVereinTxt.Text = clickedSchütze.MitgliedsNrVerein;
            EntryDateVereinTxt.Text = clickedSchütze.EintrittVerein;
        }
        private void CustomizeGridData()
        {
            SchützenGrid.AutoGenerateColumns = false;
            SchützenGrid.EnableHeadersVisualStyles = false;
            SchützenGrid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            SchützenGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewColumn[] columns = new DataGridViewColumn[6];
            columns[0] = new DataGridViewTextBoxColumn() { DataPropertyName = "Id", Visible = false };
            columns[1] = new DataGridViewTextBoxColumn() { DataPropertyName = "SName", HeaderText = "Name", SortMode = DataGridViewColumnSortMode.Automatic, Width = 130 };
            //columns[2] = new DataGridViewTextBoxColumn() { DataPropertyName = "Geburtsdatum", HeaderText = "Geburtsdatum", SortMode = DataGridViewColumnSortMode.Automatic};
            //columns[3] = new DataGridViewTextBoxColumn() { DataPropertyName = "StraßeNr", HeaderText = "StraßeNr", SortMode = DataGridViewColumnSortMode.Automatic};
            //columns[4] = new DataGridViewTextBoxColumn() { DataPropertyName = "PLZOrt", HeaderText = "PLZOrt", SortMode = DataGridViewColumnSortMode.Automatic };
            //columns[5] = new DataGridViewTextBoxColumn() { DataPropertyName = "TelefonNr", HeaderText = "TelefonNr", SortMode = DataGridViewColumnSortMode.Automatic };
            //columns[6] = new DataGridViewTextBoxColumn() { DataPropertyName = "Email", HeaderText = "Email", SortMode = DataGridViewColumnSortMode.Automatic };
            columns[2] = new DataGridViewTextBoxColumn() { DataPropertyName = "VBName", HeaderText = "Verband", SortMode = DataGridViewColumnSortMode.Automatic };
            //columns[8] = new DataGridViewTextBoxColumn() { DataPropertyName = "MitgliedsNrVerband", HeaderText = "MitgliedsNrVerband", SortMode = DataGridViewColumnSortMode.Automatic };
            //columns[9] = new DataGridViewTextBoxColumn() { DataPropertyName = "EintrittVerband", HeaderText = "EintrittVerband", SortMode = DataGridViewColumnSortMode.Automatic };
            columns[3] = new DataGridViewTextBoxColumn() { DataPropertyName = "VEName", HeaderText = "Verein", SortMode = DataGridViewColumnSortMode.Automatic, Width = 120 };
            //columns[11] = new DataGridViewTextBoxColumn() { DataPropertyName = "MitgliedsNrVerein", HeaderText = "MitgliedsNrVerein", SortMode = DataGridViewColumnSortMode.Automatic };
            //columns[12] = new DataGridViewTextBoxColumn() { DataPropertyName = "EintrittVerein", HeaderText = "EintrittVerein", SortMode = DataGridViewColumnSortMode.Automatic };
            columns[4] = new DataGridViewButtonColumn()
            {
                Text = "Löschen",
                Name = "DeleteBtn",
                HeaderText = "",
                UseColumnTextForButtonValue = true
            };
            columns[5] = new DataGridViewButtonColumn()
            {
                Text = "Ändern",
                Name = "EditBtn",
                HeaderText = "",
                UseColumnTextForButtonValue = true
            };

            SchützenGrid.Columns.Clear();
            SchützenGrid.Columns.AddRange(columns);
        }
        private async void FillCbx()
        {
            VerbandCbx.DataSource = await _verbandsRepository.getVerband();
            VerbandCbx.DisplayMember = "Name";

            VereinCbx.DataSource = await _vereinsRepository.getVereine();
            VereinCbx.DisplayMember = "Name";
        }
        private async void SafeBtn_Click(object sender, EventArgs e)
        {
            if (!isValid())
                return;

            Verband verband = VerbandCbx.SelectedItem as Verband;

            Verein verein = VereinCbx.SelectedItem as Verein;

            Schütze schütze = new Schütze(NameTxt.Text, DateTxt.Text, AdressTxt.Text, CountryTxt.Text, PhoneTxt.Text, MailTxt.Text, verband.Id, MemberNrVerbandTxt.Text, EntryDateVerbandTxt.Text, verein.Id, MemberNrVereinTxt.Text, EntryDateVereinTxt.Text);


            await _schützenRepository.AddSchütze(schütze);

            RefreshGridData();
        }
        private bool isValid()
        {
            bool isValid = true;
            DateTime parsedDate;

            string message = string.Empty;

            if (string.IsNullOrEmpty(NameTxt.Text))
            {
                isValid = false;
                message += "Namen eingeben. \n";
            }

            if (string.IsNullOrEmpty(DateTxt.Text))
            {
                isValid = false;
                message += "Datum eingeben. \n";
            }
            else if (!DateTime.TryParseExact(DateTxt.Text, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                isValid = false;
                message += "Richtiges Datum eingeben (Tag.Monat.Jahr)";
            }
            if (string.IsNullOrEmpty(AdressTxt.Text))
            {
                isValid = false;
                message += "Adresse eingeben. \n";
            }
            if (string.IsNullOrEmpty(CountryTxt.Text))
            {
                isValid = false;
                message += "PLZ/Ort eingeben. \n";
            }
            if (string.IsNullOrEmpty(PhoneTxt.Text))
            {
                isValid = false;
                message += "Telefonnummer eingeben. \n";
            }
            if (string.IsNullOrEmpty(MailTxt.Text))
            {
                isValid = false;
                message += "eine E-Mail eingeben. \n";
            }
            if (string.IsNullOrEmpty(MemberNrVerbandTxt.Text))
            {
                isValid = false;
                message += "Mitgliedsnummber beim Verband eingeben. \n";
            }
            if (string.IsNullOrEmpty(EntryDateVerbandTxt.Text))
            {
                isValid = false;
                message += "Eintrittsdatum für den Verband eingeben. \n";
            }
            else if (!DateTime.TryParseExact(EntryDateVerbandTxt.Text, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                isValid = false;
                message += "Richtiges Datum eingeben (Tag.Monat.Jahr)";
            }
            if (string.IsNullOrEmpty(MemberNrVereinTxt.Text))
            {
                isValid = false;
                message += "Mitgliedsnummer beim Verein eingeben. \n";
            }

            if (string.IsNullOrEmpty(EntryDateVereinTxt.Text))
            {
                isValid = false;
                message += "Eintrittsdatum für den Verein eingeben. \n";
            }
            else if (!DateTime.TryParseExact(EntryDateVereinTxt.Text, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                isValid = false;
                message += "Richtiges Datum eingeben (Tag.Monat.Jahr)";
            }
            if (VerbandCbx.SelectedItem == null)
            {
                isValid = false;
                message += "Verband auswählen. \n";
            }
            if (VereinCbx.SelectedItem == null)
            {
                isValid = false;
                message += "Verein auswählen. \n";
            }

            if (!isValid)
                MessageBox.Show(message, "Eingaben fehlen!");

            return isValid;
        }
        private async void SchützenGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && SchützenGrid.CurrentCell is DataGridViewButtonCell)
            {
                JoinedSchütze clickedSchütze = (JoinedSchütze)SchützenGrid.Rows[e.RowIndex].DataBoundItem;

                if (SchützenGrid.CurrentCell.OwningColumn.Name == "EditBtn")
                {
                    FillFormForEdit(clickedSchütze);
                    EditBtn.Visible = true;
                    SafeBtn.Visible = false;
                }
                else if (SchützenGrid.CurrentCell.OwningColumn.Name == "DeleteBtn")
                {
                    await _schützenRepository.DeleteSchütze(clickedSchütze);

                    RefreshGridData();
                    ClearAllFields();
                }
            }
        }
        private async void EditBtn_Click(object sender, EventArgs e)
        {
            if (!isValid())
                return;

            Schütze schütze = new Schütze();
            Verband verband = VerbandCbx.SelectedItem as Verband;
            Verein verein = VereinCbx.SelectedItem as Verein;


            schütze.Id = _schützeToEdit;
            schütze.Name = NameTxt.Text;
            schütze.Geburtsdatum = DateTxt.Text;
            schütze.StraßeNr = AdressTxt.Text;
            schütze.PLZOrt = CountryTxt.Text;
            schütze.TelefonNr = PhoneTxt.Text;
            schütze.Email = MailTxt.Text;
            schütze.VerbandId = verband.Id;
            schütze.MitgliedsNrVerband = MemberNrVerbandTxt.Text;
            schütze.EintrittVerband = EntryDateVerbandTxt.Text;
            schütze.VereinId = verein.Id;
            schütze.MitgliedsNrVerein = MemberNrVereinTxt.Text;
            schütze.EintrittVerein = EntryDateVereinTxt.Text;

            await _schützenRepository.EditSchütze(schütze);

            ClearAllFields();
            RefreshGridData();
        }
        private bool isValidTextBox(string currentText, char keyPressed, bool? digitAndMinus = null, bool? onlyString = null)
        {
            // If digitAndMinus is true, validate for digits and the '-' sign
            if (digitAndMinus == true)
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

                // Allow only one '-' and it should be the first character
                if (keyPressed == '-' || keyPressed == (char)45)
                {
                    return true;
                }

                // If none of the conditions are met, return false (invalid input)
                return false;
            }
            else if (onlyString == true)
            {
                //Allow control keys ( backspace, delete, etc.)
                if (char.IsControl(keyPressed))
                {
                    return true;
                }
                //Allow Letters (a-z)
                if (char.IsLetter(keyPressed))
                {
                    return true;
                }

                //If none of the conditions are met, return false (invalid input)
                return false;
            }

            return true; // If no validation is required, return true
        }
        private void MemberNrVerbandTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!isValidTextBox(MemberNrVerbandTxt.Text, e.KeyChar, digitAndMinus: true))
            {
                e.Handled = true;
            }
        }
        private void NameTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!isValidTextBox(MemberNrVerbandTxt.Text, e.KeyChar, onlyString: true))
            {
                e.Handled = true;
            }
        }
        private void SchützenGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            CalculateGridWidth();
        }
        private void CalculateGridWidth()
        {
            // Automatisches Anpassen der Spalten an den Inhalt
            foreach (DataGridViewColumn column in SchützenGrid.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            // Berechne die Gesamtbreite
            int gesamtBreite = SchützenGrid.RowHeadersVisible ? SchützenGrid.RowHeadersWidth : 0;

            foreach (DataGridViewColumn column in SchützenGrid.Columns)
            {
                gesamtBreite += column.Width;
            }

            // Prüfe, ob eine vertikale Scrollbar vorhanden ist, und füge ggf. ihre Breite hinzu
            if (SchützenGrid.Controls.OfType<ScrollBar>().Any(sb => sb.Visible))
            {
                gesamtBreite -= SystemInformation.VerticalScrollBarWidth;
            }

            // Setze die neue Breite des DataGridView
            SchützenGrid.Width = gesamtBreite;
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
            VereinCbx.BackColor = ColorTranslator.FromHtml(primaryTbxBgr);
            VerbandCbx.BackColor = ColorTranslator.FromHtml(primaryCbxBgr);

            //Flat Style der Combo Boxen
            VereinCbx.FlatStyle = FlatStyle.Flat;
            VerbandCbx.FlatStyle = FlatStyle.Flat;

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
            NameLbl.BackColor = ColorTranslator.FromHtml(secondaryBgr);
            NameLbl.ForeColor = ColorTranslator.FromHtml(primaryFgr);
            DatumLbl.BackColor = ColorTranslator.FromHtml(secondaryBgr);
            DatumLbl.ForeColor = ColorTranslator.FromHtml(primaryFgr);
            AdresseLbl.BackColor = ColorTranslator.FromHtml(secondaryBgr);
            AdresseLbl.ForeColor = ColorTranslator.FromHtml(primaryFgr);
            TelefonLbl.BackColor = ColorTranslator.FromHtml(secondaryBgr);
            TelefonLbl.ForeColor = ColorTranslator.FromHtml(primaryFgr);
            PlzLbl.BackColor = ColorTranslator.FromHtml(secondaryBgr);
            PlzLbl.ForeColor = ColorTranslator.FromHtml(primaryFgr);
            EmailLbl.BackColor = ColorTranslator.FromHtml(secondaryBgr);
            EmailLbl.ForeColor = ColorTranslator.FromHtml(primaryFgr);
            VerbandLbl.BackColor = ColorTranslator.FromHtml(secondaryBgr);
            VerbandLbl.ForeColor = ColorTranslator.FromHtml(primaryFgr);
            MitgliedNrVerbandLbl.BackColor = ColorTranslator.FromHtml(secondaryBgr);
            MitgliedNrVerbandLbl.ForeColor = ColorTranslator.FromHtml(primaryFgr);
            VerbandEintrittDatumLbl.BackColor = ColorTranslator.FromHtml(secondaryBgr);
            VerbandEintrittDatumLbl.ForeColor = ColorTranslator.FromHtml(primaryFgr);
            VereinLbl.BackColor = ColorTranslator.FromHtml(secondaryBgr);
            VereinLbl.ForeColor = ColorTranslator.FromHtml(primaryFgr);
            MitgliedNrVereinLbl.BackColor = ColorTranslator.FromHtml(secondaryBgr);
            MitgliedNrVereinLbl.ForeColor = ColorTranslator.FromHtml(primaryFgr);
            VereinEintrittDatumLbl.BackColor = ColorTranslator.FromHtml(secondaryBgr);
            VereinEintrittDatumLbl.ForeColor = ColorTranslator.FromHtml(primaryFgr);

            //Back & Fore Color der Textboxen
            NameTxt.BackColor = ColorTranslator.FromHtml(primaryTbxBgr);
            NameTxt.BorderStyle = BorderStyle.FixedSingle;
            DateTxt.BackColor = ColorTranslator.FromHtml(primaryTbxBgr);
            DateTxt.BorderStyle = BorderStyle.FixedSingle;
            AdressTxt.BackColor = ColorTranslator.FromHtml(primaryTbxBgr);
            AdressTxt.BorderStyle = BorderStyle.FixedSingle;
            CountryTxt.BackColor = ColorTranslator.FromHtml(primaryTbxBgr);
            CountryTxt.BorderStyle = BorderStyle.FixedSingle;
            PhoneTxt.BackColor = ColorTranslator.FromHtml(primaryTbxBgr);
            PhoneTxt.BorderStyle = BorderStyle.FixedSingle;
            MailTxt.BackColor = ColorTranslator.FromHtml(primaryTbxBgr);
            MailTxt.BorderStyle = BorderStyle.FixedSingle;
            MemberNrVerbandTxt.BackColor = ColorTranslator.FromHtml(primaryTbxBgr);
            MemberNrVerbandTxt.BorderStyle = BorderStyle.FixedSingle;
            EntryDateVerbandTxt.BackColor = ColorTranslator.FromHtml(primaryTbxBgr);
            EntryDateVerbandTxt.BorderStyle = BorderStyle.FixedSingle;
            MemberNrVereinTxt.BackColor = ColorTranslator.FromHtml(primaryTbxBgr);
            MemberNrVereinTxt.BorderStyle = BorderStyle.FixedSingle;
            EntryDateVereinTxt.BackColor = ColorTranslator.FromHtml(primaryTbxBgr);
            EntryDateVereinTxt.BorderStyle = BorderStyle.FixedSingle;

            //Grid
            //Hintergrundfarben
            SchützenGrid.RowTemplate.Height = 30;
            SchützenGrid.BackgroundColor = ColorTranslator.FromHtml(primaryDgvBgr);
            SchützenGrid.GridColor = Color.Black;

            //Zellfarben
            SchützenGrid.DefaultCellStyle.BackColor = ColorTranslator.FromHtml(primaryDgvBgr);
            SchützenGrid.DefaultCellStyle.ForeColor = Color.Black;
            SchützenGrid.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml(primaryBtnBgr);
            SchützenGrid.DefaultCellStyle.SelectionForeColor = Color.Black;

            //Headerfarben
            SchützenGrid.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml(primaryDgvBgr);
            SchützenGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            SchützenGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            SchützenGrid.RowHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml(primaryDgvBgr);
            SchützenGrid.RowHeadersDefaultCellStyle.ForeColor = Color.Black;
            SchützenGrid.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        }

        private void ClearAllFieldsBtn_Click(object sender, EventArgs e)
        {
            ClearAllFields();
        }
    }
}

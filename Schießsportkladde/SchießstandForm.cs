using DataAccessLayer.Contracts;
using DomainModel.Models;
using Newtonsoft.Json.Linq;
using Schießsportkladde.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Schießsportkladde
{
    public partial class SchießstandForm : Form
    {
        private int _schießstandToEdit;
        readonly ISchießstandRepository _schießstandRepository;
        public SchießstandForm(ISchießstandRepository schießstandRepository)
        {
            InitializeComponent();
            _schießstandRepository = schießstandRepository;
            _schießstandRepository.OnError += (message) => MessageBox.Show(message);

            ApplyStyles();
        }
        private void SchießstandForm_Load(object sender, EventArgs e)
        {
            RefreshGridData();
            CustomizeGridData();

            EditBtn.Visible = false;
            SafeBtn.Visible = true;
        }
        private async void RefreshGridData()
        {
            SchießständeGrid.DataSource = await _schießstandRepository.GetSchießtstände();
        }
        private void CustomizeGridData()
        {

            SchießständeGrid.AutoGenerateColumns = false;
            SchießständeGrid.EnableHeadersVisualStyles = false;
            SchießständeGrid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            SchießständeGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewColumn[] columns = new DataGridViewColumn[4];

            columns[0] = new DataGridViewTextBoxColumn() { DataPropertyName = "Id", Visible = false };
            columns[1] = new DataGridViewTextBoxColumn() { HeaderText = "Schießstand", DataPropertyName = "Schießstand" };
            columns[2] = new DataGridViewButtonColumn()
            {
                Text = "Löschen",
                Name = "DeleteBtn",
                HeaderText = "",
                UseColumnTextForButtonValue = true
            };
            columns[3] = new DataGridViewButtonColumn()
            {
                Text = "Ändern",
                Name = "EditBtn",
                HeaderText = "",
                UseColumnTextForButtonValue = true,
            };

            SchießständeGrid.Columns.Clear();
            SchießständeGrid.Columns.AddRange(columns);
        }
        private void CalculateGridWidth()
        {
            // Automatisches Anpassen der Spalten an den Inhalt
            foreach (DataGridViewColumn column in SchießständeGrid.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            }

            // Berechne die Gesamtbreite
            int gesamtBreite = SchießständeGrid.RowHeadersVisible ? SchießständeGrid.RowHeadersWidth : 0;

            foreach (DataGridViewColumn column in SchießständeGrid.Columns)
            {
                gesamtBreite += column.Width;
            }

            // Prüfe, ob eine vertikale Scrollbar vorhanden ist, und füge ggf. ihre Breite hinzu
            if (SchießständeGrid.Controls.OfType<ScrollBar>().Any(sb => sb.Visible))
            {
                gesamtBreite -= SystemInformation.VerticalScrollBarWidth;
            }

            // Setze die neue Breite des DataGridView
            SchießständeGrid.Width = gesamtBreite;
        }
        private async void VerbändeGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && SchießständeGrid.CurrentCell is DataGridViewButtonCell)
            {
                Schießtstand clickedSchießstand = (Schießtstand)SchießständeGrid.Rows[e.RowIndex].DataBoundItem;

                if (SchießständeGrid.CurrentCell.OwningColumn.Name == "EditBtn")
                {
                    FillFormForEdit(clickedSchießstand);
                }
                else if (SchießständeGrid.CurrentCell.OwningColumn.Name == "DeleteBtn")
                {
                    await _schießstandRepository.DeleteSchießstand(clickedSchießstand);

                    RefreshGridData();
                    ClearAllFields();
                }
            }
        }
        private void FillFormForEdit(Schießtstand clickedSchießstand)
        {
            _schießstandToEdit = clickedSchießstand.Id;
            NameTxt.Text = clickedSchießstand.Schießstand;

            EditBtn.Visible = true;
            SafeBtn.Visible = false;
        }
        private async void EditBtn_Click(object sender, EventArgs e)
        {
            if (!isValid())
                return;

            Schießtstand schießtstand = new Schießtstand();

            schießtstand.Id = _schießstandToEdit;
            schießtstand.Schießstand = NameTxt.Text;

            await _schießstandRepository.EditSchießstand(schießtstand);

            RefreshGridData();
            ClearAllFields();
        }
        private async void SafeBtn_Click(object sender, EventArgs e)
        {
            if (!isValid())
                return;

            Schießtstand schießtstand = new Schießtstand();

            schießtstand.Schießstand = NameTxt.Text;

            await _schießstandRepository.AddSchießstand(schießtstand);

            RefreshGridData();
            ClearAllFields();
        }
        private void ClearAllFields()
        {
            NameTxt.Text = string.Empty;

            EditBtn.Visible = false;
            SafeBtn.Visible = true;
        }
        private bool isValid()
        {
            bool isValid = true;
            string message = string.Empty;

            if (string.IsNullOrEmpty(NameTxt.Text))
            {
                isValid = false;
                message += "Namen eingeben";
            }

            if (!isValid)
                MessageBox.Show(message, "Eingaben fehlen!");

            return isValid;

        }
        private bool isValidTextBox(string currentText, char keyPressed, bool? onlyString = null)
        {
            if (onlyString == true)
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
        private void NameTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!isValidTextBox(NameTxt.Text, e.KeyChar, true))
            {
                e.Handled = true;
            }
        }
        private void SchießständeGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            CalculateGridWidth();
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

            this.BackColor = ColorTranslator.FromHtml(secondaryBgr);

            //Label Back & Fore Color
            SchießständeLbl.BackColor = ColorTranslator.FromHtml(secondaryBgr);
            SchießständeLbl.ForeColor = ColorTranslator.FromHtml(primaryFgr);
            NameLbl.BackColor = ColorTranslator.FromHtml(secondaryBgr);
            NameLbl.ForeColor = ColorTranslator.FromHtml(primaryFgr);

            //Button Fore & Fore Color
            EditBtn.BackColor = ColorTranslator.FromHtml(primaryBtnBgr);
            EditBtn.ForeColor = ColorTranslator.FromHtml(primaryBtnFgr);
            SafeBtn.BackColor = ColorTranslator.FromHtml(primaryBtnBgr);
            SafeBtn.ForeColor = ColorTranslator.FromHtml(primaryBtnFgr);
            ClearAllFielsBtn.BackColor = ColorTranslator.FromHtml(secondaryBtnBgr);
            ClearAllFielsBtn.ForeColor = ColorTranslator.FromHtml(primaryBtnFgr);

            //Back & Fore Color der Textboxen
            NameTxt.BackColor = ColorTranslator.FromHtml(primaryTbxBgr);
            NameTxt.BorderStyle = BorderStyle.FixedSingle;

            //Grid
            //Hintergrundfarben
            SchießständeGrid.RowTemplate.Height = 28;
            SchießständeGrid.BackgroundColor = ColorTranslator.FromHtml(primaryDgvBgr);
            SchießständeGrid.GridColor = Color.Black;

            //Zellfarben
            SchießständeGrid.DefaultCellStyle.BackColor = ColorTranslator.FromHtml(primaryDgvBgr);
            SchießständeGrid.DefaultCellStyle.ForeColor = Color.Black;
            SchießständeGrid.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml(primaryBtnBgr);
            SchießständeGrid.DefaultCellStyle.SelectionForeColor = Color.Black;

            //Headerfarben
            SchießständeGrid.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml(primaryDgvBgr);
            SchießständeGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            SchießständeGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            SchießständeGrid.RowHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml(primaryDgvBgr);
            SchießständeGrid.RowHeadersDefaultCellStyle.ForeColor = Color.Black;
            SchießständeGrid.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        }

        private void ClearAllFielsBtn_Click(object sender, EventArgs e)
        {
            ClearAllFields();
        }
    }
}

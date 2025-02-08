using DataAccessLayer.Repositories;
using DataAccessLayer.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccessLayer.CustomQueryResults;
using DomainModel.Models;
using Newtonsoft.Json.Linq;
using Schießsportkladde.Services;

namespace Schießsportkladde
{
    public partial class VereineForm : Form
    {
        private int _vereinToEdit;
        readonly IVereineRepository _vereinRepository;
        public VereineForm(IVereineRepository vereineRepository)
        {
            InitializeComponent();
            _vereinRepository = vereineRepository;
            _vereinRepository.OnError += (message) => MessageBox.Show(message);
            ApplyStyles();
        }
        private void VereineForm_Load(object sender, EventArgs e)
        {
            RefreshGridData();
            CustomizeGridData();

            EditBtn.Visible = false;
            SafeBtn.Visible = true;
        }
        private async void RefreshGridData()
        {
            VereineGrid.DataSource = await _vereinRepository.getVereine();
        }
        private void CalculateGridWidth()
        {
            // Automatisches Anpassen der Spalten an den Inhalt
            foreach (DataGridViewColumn column in VereineGrid.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            }

            // Berechne die Gesamtbreite
            int gesamtBreite = VereineGrid.RowHeadersVisible ? VereineGrid.RowHeadersWidth : 0;

            foreach (DataGridViewColumn column in VereineGrid.Columns)
            {
                gesamtBreite += column.Width;
            }

            // Prüfe, ob eine vertikale Scrollbar vorhanden ist, und füge ggf. ihre Breite hinzu
            if (VereineGrid.Controls.OfType<ScrollBar>().Any(sb => sb.Visible))
            {
                gesamtBreite -= SystemInformation.VerticalScrollBarWidth;
            }

            // Setze die neue Breite des DataGridView
            VereineGrid.Width = gesamtBreite;
        }
        private void CustomizeGridData()
        {

            VereineGrid.AutoGenerateColumns = false;
            VereineGrid.EnableHeadersVisualStyles = false;
            VereineGrid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            VereineGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewColumn[] columns = new DataGridViewColumn[4];

            columns[0] = new DataGridViewTextBoxColumn() { DataPropertyName = "Id", Visible = false };
            columns[1] = new DataGridViewTextBoxColumn() { HeaderText = "Name", DataPropertyName = "Name" };
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

            VereineGrid.Columns.Clear();
            VereineGrid.Columns.AddRange(columns);
        }
        private void FillFormForEdit(Verein clickedVerein)
        {
            _vereinToEdit = clickedVerein.Id;
            NameTxt.Text = clickedVerein.Name;

            EditBtn.Visible = true;
            SafeBtn.Visible = false;
        }
        private void ClearAllFields()
        {
            NameTxt.Text = string.Empty;

            EditBtn.Visible = false;
            SafeBtn.Visible = true;
        }
        private async void VereineGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && VereineGrid.CurrentCell is DataGridViewButtonCell)
            {
                Verein clickedVerein = (Verein)VereineGrid.Rows[e.RowIndex].DataBoundItem;

                if (VereineGrid.CurrentCell.OwningColumn.Name == "EditBtn")
                {
                    FillFormForEdit(clickedVerein);
                }
                else if (VereineGrid.CurrentCell.OwningColumn.Name == "DeleteBtn")
                {
                    await _vereinRepository.DeleteVerein(clickedVerein);

                    RefreshGridData();
                    ClearAllFields();
                }
            }
        }
        private async void SafeBtn_Click(object sender, EventArgs e)
        {
            if (!isValid())
                return;

            Verein verein = new Verein();
            verein.Name = NameTxt.Text;

            await _vereinRepository.AddVerein(verein);

            RefreshGridData();
            ClearAllFields();
        }
        private async void EditBtn_Click(object sender, EventArgs e)
        {
            if (!isValid())
                return;

            Verein verein = new Verein();

            verein.Id = _vereinToEdit;
            verein.Name = NameTxt.Text;

            await _vereinRepository.EditVerein(verein);

            RefreshGridData();
            ClearAllFields();
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
        private void VereineGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
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
            HeaderTxtLbl.BackColor = ColorTranslator.FromHtml(secondaryBgr);
            HeaderTxtLbl.ForeColor = ColorTranslator.FromHtml(primaryFgr);
            NameLbl.BackColor = ColorTranslator.FromHtml(secondaryBgr);
            NameLbl.ForeColor = ColorTranslator.FromHtml(primaryFgr);

            //Button Fore & Fore Color
            EditBtn.BackColor = ColorTranslator.FromHtml(primaryBtnBgr);
            EditBtn.ForeColor = ColorTranslator.FromHtml(primaryBtnFgr);
            SafeBtn.BackColor = ColorTranslator.FromHtml(primaryBtnBgr);
            SafeBtn.ForeColor = ColorTranslator.FromHtml(primaryBtnFgr);
            ClearAllFieldsBtn.BackColor = ColorTranslator.FromHtml(secondaryBtnBgr);
            ClearAllFieldsBtn.ForeColor = ColorTranslator.FromHtml(primaryBtnFgr);

            //Back & Fore Color der Textboxen
            NameTxt.BackColor = ColorTranslator.FromHtml(primaryTbxBgr);
            NameTxt.BorderStyle = BorderStyle.FixedSingle;

            //Grid
            //Hintergrundfarben
            VereineGrid.RowTemplate.Height = 28;
            VereineGrid.BackgroundColor = ColorTranslator.FromHtml(primaryDgvBgr);
            VereineGrid.GridColor = Color.Black;

            //Zellfarben
            VereineGrid.DefaultCellStyle.BackColor = ColorTranslator.FromHtml(primaryDgvBgr);
            VereineGrid.DefaultCellStyle.ForeColor = Color.Black;
            VereineGrid.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml(primaryBtnBgr);
            VereineGrid.DefaultCellStyle.SelectionForeColor = Color.Black;

            //Headerfarben
            VereineGrid.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml(primaryDgvBgr);
            VereineGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            VereineGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            VereineGrid.RowHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml(primaryDgvBgr);
            VereineGrid.RowHeadersDefaultCellStyle.ForeColor = Color.Black;
            VereineGrid.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        }
        private void ClearAllFieldsBtn_Click(object sender, EventArgs e)
        {
            ClearAllFields();
        }
    }
}

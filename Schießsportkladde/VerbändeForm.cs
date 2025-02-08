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
using DomainModel.Models;
using Newtonsoft.Json.Linq;
using Schießsportkladde.Services;

namespace Schießsportkladde
{
    public partial class VerbändeForm : Form
    {
        private int _verbandToEdit;
        readonly IVerbändeRepository _verbandRepository;
        public VerbändeForm(IVerbändeRepository verbändeRepository)
        {
            InitializeComponent();
            _verbandRepository = verbändeRepository;
            _verbandRepository.OnError += (message) => MessageBox.Show(message);
            ApplyStyles();
        }
        private void VerbändeForm_Load(object sender, EventArgs e)
        {
            RefreshGridData();
            CustomizeGridData();

            EditBtn.Visible = false;
            SafeBtn.Visible = true;
        }
        private void CustomizeGridData()
        {

            VerbändeGrid.AutoGenerateColumns = false;
            VerbändeGrid.EnableHeadersVisualStyles = false;
            VerbändeGrid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            VerbändeGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewColumn[] columns = new DataGridViewColumn[4];

            columns[0] = new DataGridViewTextBoxColumn() { DataPropertyName = "Id", Visible = false };
            columns[1] = new DataGridViewTextBoxColumn() { HeaderText = "Name", DataPropertyName = "Name", Width = 400 };
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

            VerbändeGrid.Columns.Clear();
            VerbändeGrid.Columns.AddRange(columns);
        }
        private async void RefreshGridData()
        {
            VerbändeGrid.DataSource = await _verbandRepository.getVerband();
        }
        private async void SafeBtn_Click(object sender, EventArgs e)
        {
            if (!isValid())
                return;

            Verband verband = new Verband();

            verband.Name = NameTxt.Text;

            await _verbandRepository.AddVerband(verband);

            RefreshGridData();
            ClearAllFields();
        }
        private async void EditBtn_Click(object sender, EventArgs e)
        {
            if (!isValid())
                return;

            Verband verband = new Verband();

            verband.Id = _verbandToEdit;
            verband.Name = NameTxt.Text;

            await _verbandRepository.EditVerband(verband);

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
        private async void VerbändeGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && VerbändeGrid.CurrentCell is DataGridViewButtonCell)
            {
                Verband clickedVerband = (Verband)VerbändeGrid.Rows[e.RowIndex].DataBoundItem;

                if (VerbändeGrid.CurrentCell.OwningColumn.Name == "EditBtn")
                {
                    FillFormForEdit(clickedVerband);
                }
                else if (VerbändeGrid.CurrentCell.OwningColumn.Name == "DeleteBtn")
                {
                    await _verbandRepository.DeleteVerband(clickedVerband);

                    RefreshGridData();
                    ClearAllFields();
                }
            }
        }
        private void FillFormForEdit(Verband clickedVerband)
        {
            _verbandToEdit = clickedVerband.Id;
            NameTxt.Text = clickedVerband.Name;

            EditBtn.Visible = true;
            SafeBtn.Visible = false;
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
            VerbändeGrid.RowTemplate.Height = 28;
            VerbändeGrid.BackgroundColor = ColorTranslator.FromHtml(primaryDgvBgr);
            VerbändeGrid.GridColor = Color.Black;

            //Zellfarben
            VerbändeGrid.DefaultCellStyle.BackColor = ColorTranslator.FromHtml(primaryDgvBgr);
            VerbändeGrid.DefaultCellStyle.ForeColor = Color.Black;
            VerbändeGrid.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml(primaryBtnBgr);
            VerbändeGrid.DefaultCellStyle.SelectionForeColor = Color.Black;

            //Headerfarben
            VerbändeGrid.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml(primaryDgvBgr);
            VerbändeGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            VerbändeGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            VerbändeGrid.RowHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml(primaryDgvBgr);
            VerbändeGrid.RowHeadersDefaultCellStyle.ForeColor = Color.Black;
            VerbändeGrid.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        }
        private void ClearAllFieldsBtn_Click(object sender, EventArgs e)
        {
            ClearAllFields();
        }
    }
}

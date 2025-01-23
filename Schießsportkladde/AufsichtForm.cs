using DataAccessLayer.Contracts;
using DataAccessLayer.CustomQueryResults;
using DataAccessLayer.Repositories;
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
    public partial class AufsichtForm : Form
    {
        private int _aufsichtToEdit;
        readonly IAufsichtRepository _aufsichtRepository;
        public AufsichtForm(IAufsichtRepository aufsichtRepository)
        {
            InitializeComponent();
            _aufsichtRepository = aufsichtRepository;
            _aufsichtRepository.OnError += (message) => MessageBox.Show(message);

            ApplyStyles();
        }
        private void AufsichtForm_Load(object sender, EventArgs e)
        {
            RefreshGridData();
            CustomizeGridData();

            EditBtn.Visible = false;
            SafeBtn.Visible = true;
        }
        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (!isValid())
                return;

            Aufsicht aufsicht = new Aufsicht();


            aufsicht.Id = _aufsichtToEdit;
            aufsicht.Name = NameTxt.Text;


            _aufsichtRepository.EditAufsicht(aufsicht);

            ClearAllFields();
            RefreshGridData();
        }
        private void SafeBtn_Click(object sender, EventArgs e)
        {
            if (!isValid())
                return;

            Aufsicht aufsicht = new Aufsicht();
            aufsicht.Id = _aufsichtToEdit;
            aufsicht.Name = NameTxt.Text;

            _aufsichtRepository.AddAufsicht(aufsicht);

            ClearAllFields();
            RefreshGridData();
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
        private async void RefreshGridData()
        {
            AufsichtGrid.DataSource = await _aufsichtRepository.GetAufsicht();
        }
        private void CustomizeGridData()
        {

            AufsichtGrid.AutoGenerateColumns = false;
            AufsichtGrid.EnableHeadersVisualStyles = false;
            AufsichtGrid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            AufsichtGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewColumn[] columns = new DataGridViewColumn[4];

            columns[0] = new DataGridViewTextBoxColumn() { DataPropertyName = "Id", Visible = false };
            columns[1] = new DataGridViewTextBoxColumn() { HeaderText = "Name", DataPropertyName = "Name", Width = 192 };
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

            AufsichtGrid.Columns.Clear();
            AufsichtGrid.Columns.AddRange(columns);
        }
        private void ClearAllFields()
        {
            NameTxt.Text = string.Empty;

            EditBtn.Visible = false;
            SafeBtn.Visible = true;
        }
        private async void AufsichtGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && AufsichtGrid.CurrentCell is DataGridViewButtonCell)
            {
                Aufsicht clickedAufsicht = (Aufsicht)AufsichtGrid.Rows[e.RowIndex].DataBoundItem;

                if (AufsichtGrid.CurrentCell.OwningColumn.Name == "EditBtn")
                {
                    FillFormForEdit(clickedAufsicht);
                }
                else if (AufsichtGrid.CurrentCell.OwningColumn.Name == "DeleteBtn")
                {
                    await _aufsichtRepository.DeleteAufsicht(clickedAufsicht);

                    RefreshGridData();
                    ClearAllFields();
                }
            }
        }
        private void FillFormForEdit(Aufsicht clickedAufsicht)
        {
            _aufsichtToEdit = clickedAufsicht.Id;
            NameTxt.Text = clickedAufsicht.Name;

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
           
            string secondaryBgr = (string)themeConfig["secondaryBgr"];
            string primaryFgr = (string)themeConfig["primaryFgr"];
            string primaryBtnBgr = (string)themeConfig["primaryBtnBgr"];
            string secondaryBtnBgr = (string)themeConfig["secondaryBtnBgr"];            
            string primaryBtnFgr = (string)themeConfig["primaryBtnFgr"];                              
            string primaryTbxBgr = (string)themeConfig["primaryTbxBgr"];
            string primaryDgvBgr = (string)themeConfig["primaryDgvBgr"];
            string primaryDgvFgr = (string)themeConfig["primaryDgvFgr"];

            this.BackColor = ColorTranslator.FromHtml(secondaryBgr);

            SafeBtn.BackColor = ColorTranslator.FromHtml(primaryBtnBgr);
            SafeBtn.ForeColor = ColorTranslator.FromHtml(primaryBtnFgr);

            EditBtn.BackColor = ColorTranslator.FromHtml(primaryBtnBgr);
            EditBtn.ForeColor = ColorTranslator.FromHtml(primaryBtnFgr);

            ClearAllFieldsBtn.BackColor = ColorTranslator.FromHtml(secondaryBtnBgr);
            ClearAllFieldsBtn.ForeColor = ColorTranslator.FromHtml(primaryBtnFgr);

            HeaderTxtLbl.BackColor = ColorTranslator.FromHtml(secondaryBgr);
            HeaderTxtLbl.ForeColor = ColorTranslator.FromHtml(primaryFgr);

            NameTxtLbl.BackColor = ColorTranslator.FromHtml(secondaryBgr);
            NameTxtLbl.ForeColor = ColorTranslator.FromHtml(primaryFgr);

            NameTxt.BackColor = ColorTranslator.FromHtml(primaryTbxBgr);
            NameTxt.BorderStyle = BorderStyle.FixedSingle;

            //Grid
            //Hintergrundfarben
            AufsichtGrid.RowTemplate.Height = 28;
            AufsichtGrid.BackgroundColor = ColorTranslator.FromHtml(primaryDgvBgr);
            AufsichtGrid.GridColor = Color.Black;

            //Zellfarben
            AufsichtGrid.DefaultCellStyle.BackColor = ColorTranslator.FromHtml(primaryDgvBgr);
            AufsichtGrid.DefaultCellStyle.ForeColor = Color.Black;
            AufsichtGrid.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml(primaryBtnBgr);
            AufsichtGrid.DefaultCellStyle.SelectionForeColor = Color.Black;

            //Headerfarben
            AufsichtGrid.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml(primaryDgvBgr);
            AufsichtGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            AufsichtGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            AufsichtGrid.RowHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml(primaryDgvBgr);
            AufsichtGrid.RowHeadersDefaultCellStyle.ForeColor = Color.Black;
            AufsichtGrid.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        }

        private void ClearAllFieldsBtn_Click(object sender, EventArgs e)
        {
            ClearAllFields();
        }
    }
}

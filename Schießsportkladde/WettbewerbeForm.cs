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
    public partial class WettbewerbeForm : Form
    {
        private int _wettbewerbToEdit;
        readonly IWettbewerbsRepository _wettbewerbsRepository;
        public WettbewerbeForm(IWettbewerbsRepository wettbewerbsRepository)
        {
            InitializeComponent();
            _wettbewerbsRepository = wettbewerbsRepository;
            _wettbewerbsRepository.OnError += (message) => MessageBox.Show(message);

            ApplyStyles();
        }
        private void WettbewerbeForm_Load(object sender, EventArgs e)
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

            Wettbewerb wettbewerb = new Wettbewerb();

            wettbewerb.Id = _wettbewerbToEdit;
            wettbewerb.Name = NameTxt.Text;
            wettbewerb.SpO = SpOTxt.Text;

            _wettbewerbsRepository.EditWettbewerb(wettbewerb);

            ClearAllFields();
            RefreshGridData();
        }
        private void SafeBtn_Click(object sender, EventArgs e)
        {
            if (!isValid())
                return;

            Wettbewerb wettbewerb = new Wettbewerb();

            wettbewerb.Id = _wettbewerbToEdit;
            wettbewerb.Name = NameTxt.Text;
            wettbewerb.SpO = SpOTxt.Text;

            _wettbewerbsRepository.AddWettbewerb(wettbewerb);

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
                message += "Wettbewerb Eingeben";
            }

            if (string.IsNullOrEmpty(SpOTxt.Text))
            {
                isValid = false;
                message += "SpO Eingeben";
            }

            if (!isValid)
                MessageBox.Show(message, "Eingaben fehlen!");

            return isValid;
        }
        private async void RefreshGridData()
        {
            WettbewerbsGrid.DataSource = await _wettbewerbsRepository.GetWettbewerbe();
        }
        private void CustomizeGridData()
        {
            WettbewerbsGrid.AutoGenerateColumns = false;
            WettbewerbsGrid.EnableHeadersVisualStyles = false;
            WettbewerbsGrid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            WettbewerbsGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewColumn[] columns = new DataGridViewColumn[5];

            columns[0] = new DataGridViewTextBoxColumn() { DataPropertyName = "Id", Visible = false };
            columns[1] = new DataGridViewTextBoxColumn() { HeaderText = "Name", DataPropertyName = "Name" };
            columns[2] = new DataGridViewTextBoxColumn() { HeaderText = "SpO", DataPropertyName = "SpO" };

            columns[3] = new DataGridViewButtonColumn()
            {
                Text = "Löschen",
                Name = "DeleteBtn",
                HeaderText = "",
                UseColumnTextForButtonValue = true
            };

            columns[4] = new DataGridViewButtonColumn()
            {
                Text = "Ändern",
                Name = "EditBtn",
                HeaderText = "",
                UseColumnTextForButtonValue = true
            };

            WettbewerbsGrid.Columns.Clear();
            WettbewerbsGrid.Columns.AddRange(columns);
        }
        private void CalculateGridWidth()
        {
            // Automatisches Anpassen der Spalten an den Inhalt
            foreach (DataGridViewColumn column in WettbewerbsGrid.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            }

            // Berechne die Gesamtbreite
            int gesamtBreite = WettbewerbsGrid.RowHeadersVisible ? WettbewerbsGrid.RowHeadersWidth : 0;

            foreach (DataGridViewColumn column in WettbewerbsGrid.Columns)
            {
                gesamtBreite += column.Width;
            }

            // Prüfe, ob eine vertikale Scrollbar vorhanden ist, und füge ggf. ihre Breite hinzu
            if (WettbewerbsGrid.Controls.OfType<ScrollBar>().Any(sb => sb.Visible))
            {
                gesamtBreite -= SystemInformation.VerticalScrollBarWidth;
            }

            // Setze die neue Breite des DataGridView
            WettbewerbsGrid.Width = gesamtBreite;
        }
        private void ClearAllFields()
        {
            NameTxt.Text = string.Empty;
            SpOTxt.Text = string.Empty;

            EditBtn.Visible = false;
            SafeBtn.Visible = true;
        }
        private async void WettbewerbsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && WettbewerbsGrid.CurrentCell is DataGridViewButtonCell)
            {
                Wettbewerb clickedWettbewerb = (Wettbewerb)WettbewerbsGrid.Rows[e.RowIndex].DataBoundItem;
                if (WettbewerbsGrid.CurrentCell.OwningColumn.Name == "EditBtn")
                {
                    FillFormforEdit(clickedWettbewerb);
                }
                else if (WettbewerbsGrid.CurrentCell.OwningColumn.Name == "DeleteBtn")
                {
                    await _wettbewerbsRepository.DeleteWettbewerb(clickedWettbewerb);

                    ClearAllFields();
                    RefreshGridData();
                }
            }
        }
        private void FillFormforEdit(Wettbewerb clickedWettbewerb)
        {
            _wettbewerbToEdit = clickedWettbewerb.Id;
            NameTxt.Text = clickedWettbewerb.Name;
            SpOTxt.Text = clickedWettbewerb.SpO;

            EditBtn.Visible = true;
            SafeBtn.Visible = false;
        }
        private void WettbewerbsGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
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
            SpOLbl.BackColor = ColorTranslator.FromHtml(secondaryBgr);
            SpOLbl.ForeColor = ColorTranslator.FromHtml(primaryFgr);

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
            SpOTxt.BackColor = ColorTranslator.FromHtml(primaryTbxBgr);
            SpOTxt.BorderStyle = BorderStyle.FixedSingle;

            //Grid
            //Hintergrundfarben
            WettbewerbsGrid.RowTemplate.Height = 28;
            WettbewerbsGrid.BackgroundColor = ColorTranslator.FromHtml(primaryDgvBgr);
            WettbewerbsGrid.GridColor = Color.Black;

            //Zellfarben
            WettbewerbsGrid.DefaultCellStyle.BackColor = ColorTranslator.FromHtml(primaryDgvBgr);
            WettbewerbsGrid.DefaultCellStyle.ForeColor = Color.Black;
            WettbewerbsGrid.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml(primaryBtnBgr);
            WettbewerbsGrid.DefaultCellStyle.SelectionForeColor = Color.Black;

            //Headerfarben
            WettbewerbsGrid.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml(primaryDgvBgr);
            WettbewerbsGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            WettbewerbsGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            WettbewerbsGrid.RowHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml(primaryDgvBgr);
            WettbewerbsGrid.RowHeadersDefaultCellStyle.ForeColor = Color.Black;
            WettbewerbsGrid.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        }

        private void ClearAllFieldsBtn_Click(object sender, EventArgs e)
        {
            ClearAllFields();
        }
    }
}

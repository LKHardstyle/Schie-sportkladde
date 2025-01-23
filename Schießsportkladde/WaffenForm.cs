using DataAccessLayer.Contracts;
using DomainModel.Models;
using Newtonsoft.Json.Linq;
using Schießsportkladde.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Schießsportkladde
{
    public partial class WaffenForm : Form
    {
        private int _waffeToEdit;
        IWaffenRepository _waffenRepository;
        public WaffenForm(IWaffenRepository waffenRepository)
        {
            InitializeComponent();
            _waffenRepository = waffenRepository;
            _waffenRepository.OnError += (message) => MessageBox.Show(message);

            ApplyStyles();
        }

        private void WaffenForm_Load(object sender, EventArgs e)
        {
            RefreshGridData();
            CustomizeGridAppearance();


            EditBtn.Visible = false;
            SafeBtn.Visible = true;
        }
        private async void RefreshGridData()
        {
            WaffenGrid.DataSource = await _waffenRepository.GetWaffen();
        }
        private void CalculateGridWidth()
        {
            // Automatisches Anpassen der Spalten an den Inhalt
            foreach (DataGridViewColumn column in WaffenGrid.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            }

            // Berechne die Gesamtbreite
            int gesamtBreite = WaffenGrid.RowHeadersVisible ? WaffenGrid.RowHeadersWidth : 0;

            foreach (DataGridViewColumn column in WaffenGrid.Columns)
            {
                gesamtBreite += column.Width;
            }

            // Prüfe, ob eine vertikale Scrollbar vorhanden ist, und füge ggf. ihre Breite hinzu
            if (WaffenGrid.Controls.OfType<ScrollBar>().Any(sb => sb.Visible))
            {
                gesamtBreite -= SystemInformation.VerticalScrollBarWidth;
            }

            // Setze die neue Breite des DataGridView
            WaffenGrid.Width = gesamtBreite;
        }
        private void CustomizeGridAppearance()
        {
            WaffenGrid.AutoGenerateColumns = false;
            WaffenGrid.EnableHeadersVisualStyles = false;
            WaffenGrid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            WaffenGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewColumn[] columns = new DataGridViewColumn[6];
            columns[0] = new DataGridViewTextBoxColumn() { DataPropertyName = "Id", Visible = false };
            columns[1] = new DataGridViewTextBoxColumn() { DataPropertyName = "Name", HeaderText = "Name" };
            columns[2] = new DataGridViewTextBoxColumn() { DataPropertyName = "Kaliber", HeaderText = "Kaliber" };
            columns[3] = new DataGridViewTextBoxColumn() { DataPropertyName = "Disziplin", HeaderText = "Disziplin" };
            columns[4] = new DataGridViewButtonColumn()
            {
                Text = "Löschen",
                Name = "DeleteBtn",
                HeaderText = "",
                UseColumnTextForButtonValue = true,
            };
            columns[5] = new DataGridViewButtonColumn()
            {
                Text = "Ändern",
                Name = "EditBtn",
                HeaderText = "",
                UseColumnTextForButtonValue = true,
            };

            WaffenGrid.Columns.Clear();
            WaffenGrid.Columns.AddRange(columns);


        }
        private void FillFormForEdit(Waffe clickedWaffe)
        {
            _waffeToEdit = clickedWaffe.Id;

            NameTxt.Text = clickedWaffe.Name;
            KaliberTxt.Text = clickedWaffe.Kaliber;
            DisziplinTxt.Text = clickedWaffe.Disziplin;

            EditBtn.Visible = true;
            SafeBtn.Visible = false;
        }
        private void ClearAllFields()
        {
            NameTxt.Text = string.Empty;
            KaliberTxt.Text = string.Empty;
            DisziplinTxt.Text = string.Empty;

            EditBtn.Visible = false;
            SafeBtn.Visible = true;
        }

        private async void EditBtn_Click(object sender, EventArgs e)
        {
            Waffe waffe = new Waffe();

            waffe.Id = _waffeToEdit;
            waffe.Name = NameTxt.Text;
            waffe.Kaliber = KaliberTxt.Text;
            waffe.Disziplin = DisziplinTxt.Text;

            await _waffenRepository.EditWaffe(waffe);

            RefreshGridData();
            ClearAllFields();
        }

        private async void SafeBtn_Click(object sender, EventArgs e)
        {
            Waffe waffe = new Waffe();
            waffe.Name = NameTxt.Text;
            waffe.Kaliber = KaliberTxt.Text;
            waffe.Disziplin = DisziplinTxt.Text;

            await _waffenRepository.AddWaffe(waffe);

            RefreshGridData();
            ClearAllFields();
        }

        private async void SchießständeGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && WaffenGrid.CurrentCell is DataGridViewButtonCell)
            {
                Waffe clickedWaffe = (Waffe)WaffenGrid.Rows[e.RowIndex].DataBoundItem;

                if (WaffenGrid.CurrentCell.OwningColumn.Name == "EditBtn")
                {
                    FillFormForEdit(clickedWaffe);
                }
                else if (WaffenGrid.CurrentCell.OwningColumn.Name == "DeleteBtn")
                {
                    await _waffenRepository.DeleteWaffe(clickedWaffe);

                    ClearAllFields();
                    RefreshGridData();
                }
            }
        }

        private void WaffenGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
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
            HeaderTextLbl.BackColor = ColorTranslator.FromHtml(secondaryBgr);
            HeaderTextLbl.ForeColor = ColorTranslator.FromHtml(primaryFgr);
            NameLbl.BackColor = ColorTranslator.FromHtml(secondaryBgr);
            NameLbl.ForeColor = ColorTranslator.FromHtml(primaryFgr);
            KaliberLbl.BackColor = ColorTranslator.FromHtml(secondaryBgr);
            KaliberLbl.ForeColor = ColorTranslator.FromHtml(primaryFgr);
            DisziplinLbl.BackColor = ColorTranslator.FromHtml(secondaryBgr);
            DisziplinLbl.ForeColor = ColorTranslator.FromHtml(primaryFgr);

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
            KaliberTxt.BackColor = ColorTranslator.FromHtml(primaryTbxBgr);
            KaliberTxt.BorderStyle = BorderStyle.FixedSingle;
            DisziplinTxt.BackColor = ColorTranslator.FromHtml(primaryTbxBgr);
            DisziplinTxt.BorderStyle = BorderStyle.FixedSingle;

            //Grid
            //Hintergrundfarben
            WaffenGrid.RowTemplate.Height = 28;
            WaffenGrid.BackgroundColor = ColorTranslator.FromHtml(primaryDgvBgr);
            WaffenGrid.GridColor = Color.Black;

            //Zellfarben
            WaffenGrid.DefaultCellStyle.BackColor = ColorTranslator.FromHtml(primaryDgvBgr);
            WaffenGrid.DefaultCellStyle.ForeColor = Color.Black;
            WaffenGrid.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml(primaryBtnBgr);
            WaffenGrid.DefaultCellStyle.SelectionForeColor = Color.Black;

            //Headerfarben
            WaffenGrid.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml(primaryDgvBgr);
            WaffenGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            WaffenGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            WaffenGrid.RowHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml(primaryDgvBgr);
            WaffenGrid.RowHeadersDefaultCellStyle.ForeColor = Color.Black;
            WaffenGrid.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        }

        private void ClearAllFieldsBtn_Click(object sender, EventArgs e)
        {
            ClearAllFields();
        }
    }
}

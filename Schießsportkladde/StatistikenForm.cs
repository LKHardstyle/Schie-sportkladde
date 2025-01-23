using DataAccessLayer.Contracts;
using DataAccessLayer.CustomQueryResults;
using Newtonsoft.Json.Linq;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using OxyPlot.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Schießsportkladde.Services;
using System.Windows;
using DomainModel.Models;

namespace Schießsportkladde
{
    public partial class StatistikenForm : Form
    {
        private readonly IServiceProvider _serviceProvider;
        readonly ISchützenRepository _schützenRepository;
        public StatistikenForm(ISchützenRepository schützenRepository, IServiceProvider serviceProvider)
        {
            InitializeComponent();           
            _serviceProvider = serviceProvider;
            _schützenRepository = schützenRepository;
            
            ApplyStyles();            
        }

        private void StatistikenForm_Load(object sender, EventArgs e)
        {
            FillCbx();
            CustomizeForm();            
        }

        private async void SchützenCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            var SchützenName = (SchützenName)SchützenCbx.SelectedItem;

            HeaderTextLbl.Text = "Statistiken für " + SchützenName.Name.ToString();

            List<SchützenErgebnis> ErgebnisData = await _schützenRepository.GetSchützenErgebnis(SchützenName.Id);

            List<SchützenWaffe> WaffenData = await _schützenRepository.GetSchützenWaffenCount(SchützenName.Id);

            CreatePlotModel(ErgebnisData, WaffenData);
        }
        private void CustomizeForm()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }
        private async void FillCbx()
        {
            SchützenCbx.DataSource = null;
            SchützenCbx.Items.Clear();

            List<SchützenName> schützen = new List<SchützenName>();

            schützen = await _schützenRepository.GetSchützenName();

            SchützenCbx.DataSource = schützen;
            SchützenCbx.DisplayMember = "Name";
        }
        private void ApplyStyles()
        {
            JObject themeConfig = ConfigurationManager.LoadThemeConfig();

            string primaryBgr = (string)themeConfig["primaryBgr"];
            string secondaryBgr = (string)themeConfig["secondaryBgr"];
            string primaryFgr = (string)themeConfig["primaryFgr"];
            string primaryTbxBgr = (string)themeConfig["primaryTbxBgr"];

            this.BackColor = ColorTranslator.FromHtml(secondaryBgr);

            HeaderTextLbl.BackColor = ColorTranslator.FromHtml(secondaryBgr);
            HeaderTextLbl.ForeColor = ColorTranslator.FromHtml(primaryFgr);
            ErgebnisübersichtLbl.BackColor = ColorTranslator.FromHtml(secondaryBgr);            
            ErgebnisübersichtLbl.ForeColor = ColorTranslator.FromHtml(primaryFgr);
            WaffenübersichtLbl.BackColor = ColorTranslator.FromHtml(secondaryBgr);
            WaffenübersichtLbl.ForeColor = ColorTranslator.FromHtml(primaryFgr);

            SchützenCbx.BackColor = ColorTranslator.FromHtml(primaryTbxBgr);
            SchützenCbx.FlatStyle = FlatStyle.Flat;
        }
        private void CreatePlotModel(List<SchützenErgebnis> ergebnisdata, List<SchützenWaffe> waffendata)
        {
            JObject themeConfig = ConfigurationManager.LoadThemeConfig();

            string primaryTbxBgr = (string)themeConfig["primaryTbxBgr"];
            string primaryFgr = (string)themeConfig["primaryFgr"];

            // Ergebnis PlotModel erstellen
            {
                var ergebnisModel = new PlotModel();

                // BarSeries erstellen
                var series = new BarSeries
                {                                
                    FillColor = OxyColor.Parse(primaryTbxBgr), // Optional: Farbe der Balken
                    LabelPlacement = LabelPlacement.Outside,
                    LabelFormatString = "{0}" // Ergebniswerte als Label anzeigen
                };

                // Sortiere die Daten nach Jahr (DESC)
                var sortedErgebnisData = ergebnisdata
                    .Where(e => DateTime.TryParse(e.Datum, out _)) // Nur gültige Datumswerte berücksichtigen
                    .OrderByDescending(e => DateTime.Parse(e.Datum).Year)   // Nach Jahr absteigend sortieren
                    .ThenByDescending(e => DateTime.Parse(e.Datum).Month)  // Nach Monat absteigend sortieren
                    .ThenByDescending(e => DateTime.Parse(e.Datum).Day)    // Nach Tag absteigend sortieren
                    .ToList();

                // Kategorien für die X-Achse (Datum)
                var kategorien = new List<string>();

                // Daten hinzufügen           
                foreach (var eintrag in sortedErgebnisData)
                {
                    if (DateTime.TryParse(eintrag.Datum, out DateTime parsedDatum))
                    {
                        // Y-Wert: Ergebnis
                        series.Items.Add(new BarItem { Value = eintrag.Ergebnis });

                        // X-Achse: Datum hinzufügen
                        kategorien.Add($"{parsedDatum:dd.MM.yyyy} {eintrag.Waffe}");
                    }
                }

                ergebnisModel.Series.Add(series);
                ergebnisModel.TextColor = OxyColor.Parse(primaryFgr);

                // X-Achse (Kategorien)
                ergebnisModel.Axes.Add(new CategoryAxis
                {
                
                    Position = AxisPosition.Left, // Für ein vertikales Balkendiagramm (Kategorien auf der linken Seite)
                    ItemsSource = kategorien,
                    MajorGridlineStyle = LineStyle.Solid,
                    MinorGridlineStyle = LineStyle.Dot,
                    IsZoomEnabled = false,
                    IsPanEnabled = false,
                });

                // Y-Achse (Ergebnis)
                ergebnisModel.Axes.Add(new LinearAxis
                {
                    Position = AxisPosition.Bottom,
                    MajorGridlineStyle = LineStyle.Solid,
                    MinorGridlineStyle = LineStyle.Dot,
                    IsZoomEnabled = false,
                    IsPanEnabled = false,
                    Maximum = 700,
                    Minimum = 0
                });

                // Dynamische Größe für das PlotView basierend auf der Anzahl der Datensätze
                var ergebnisView = new OxyPlot.WindowsForms.PlotView
                {                
                    Model = ergebnisModel,
                    Width = ErgebnisStatsPanel.Width, // Feste Breite
                    Height = Math.Max(400, ergebnisdata.Count * 30), // Dynamische Höhe, 30 Pixel pro Datensatz
                    Dock = DockStyle.None // Keine automatische Anpassung an das Panel
                };

                ErgebnisStatsPanel.Controls.Clear();
                ErgebnisStatsPanel.Controls.Add(ergebnisView);
            }
            // Waffen PlotModel erstellen
            {
                var waffenModel = new PlotModel();

                // BarSeries erstellen
                var series = new BarSeries
                {
                    FillColor = OxyColor.Parse(primaryTbxBgr), // Optional: Farbe der Balken
                    LabelPlacement = LabelPlacement.Outside,
                    LabelFormatString = "{0}" // Ergebniswerte als Label anzeigen
                };                

                // Kategorien für die X-Achse (Datum)
                var kategorien = new List<string>();

                // Daten hinzufügen           
                foreach (var eintrag in waffendata)
                {
                    // Y-Wert: Ergebnis
                    series.Items.Add(new BarItem { Value = eintrag.Weaponused });

                    // X-Achse: Datum hinzufügen
                    kategorien.Add($"{eintrag.Waffe}");
                }

                waffenModel.Series.Add(series);
                waffenModel.TextColor = OxyColor.Parse(primaryFgr);

                // X-Achse (Kategorien)
                waffenModel.Axes.Add(new CategoryAxis
                {

                    Position = AxisPosition.Left, // Für ein vertikales Balkendiagramm (Kategorien auf der linken Seite)
                    ItemsSource = kategorien,
                    MajorGridlineStyle = LineStyle.Solid,
                    MinorGridlineStyle = LineStyle.Dot,
                    IsZoomEnabled = false,
                    IsPanEnabled = false,
                });

                // Y-Achse (Ergebnis)
                waffenModel.Axes.Add(new LinearAxis
                {
                    Position = AxisPosition.Bottom,
                    MajorGridlineStyle = LineStyle.Solid,
                    MinorGridlineStyle = LineStyle.Dot,
                    IsZoomEnabled = false,
                    IsPanEnabled = false,
                    Maximum = 700,
                    Minimum = 0
                });

                // Dynamische Größe für das PlotView basierend auf der Anzahl der Datensätze
                var waffenView = new OxyPlot.WindowsForms.PlotView
                {
                    Model = waffenModel,
                    Width = ErgebnisStatsPanel.Width, // Feste Breite
                    Height = Math.Max(400, ergebnisdata.Count * 30), // Dynamische Höhe, 30 Pixel pro Datensatz
                    Dock = DockStyle.None // Keine automatische Anpassung an das Panel
                };

                WaffenStatsPanel.Controls.Clear();
                WaffenStatsPanel.Controls.Add(waffenView);
            }
        }
    }
}

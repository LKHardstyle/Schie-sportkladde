using DataAccessLayer.Contracts;
using DataAccessLayer.CustomQueryResults;
using DomainModel.Models;
using Newtonsoft.Json.Linq;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Schießsportkladde.Services;
using System.Data;
using System.DirectoryServices;
using System.Runtime.CompilerServices;
using System.Xaml.Schema;

namespace Schießsportkladde
{
    public partial class StatistikenForm : Form
    {
        private readonly IServiceProvider _serviceProvider;
        readonly ISchützenRepository _schützenRepository;
        readonly IKladdenRepository _kladdenRepository;

        private List<SchützenErgebnis> ErgebnisData;
        private List<SchützenWaffe> WaffenData;
        private bool isProgrammaticChange = false;

        public StatistikenForm(ISchützenRepository schützenRepository, IServiceProvider serviceProvider, IKladdenRepository kladdenRepository)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _schützenRepository = schützenRepository;
            _kladdenRepository = kladdenRepository;

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

            ErgebnisData = await _schützenRepository.GetSchützenErgebnis(SchützenName.Id);

            WaffenData = await _schützenRepository.GetSchützenWaffenCount(SchützenName.Id);

            FillFilters(SchützenName);

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
            isProgrammaticChange = true;

            SchützenCbx.DataSource = null;
            SchützenCbx.Items.Clear();

            List<SchützenName> schützen = new List<SchützenName>();
            
            schützen = await _schützenRepository.GetSchützenName();

            SchützenCbx.DataSource = schützen;
            SchützenCbx.DisplayMember = "Name";

            var selectedSchütze = (SchützenName)SchützenCbx.SelectedItem;
           
            isProgrammaticChange = false;
        }
        private async void FillFilters(SchützenName selectedSchütze)
        {
            isProgrammaticChange = true;

            List<KladdenYear> jahre = new List<KladdenYear>();
            List<KladdenYear> waffe = new List<KladdenYear>();
            List<string> filterListWaffe = new List<string>();
            List<string> filterListJahre = new List<string>();

            jahre = await _kladdenRepository.GetKladdenYears(selectedSchütze.Id);
            waffe = await _kladdenRepository.GetKladdenYears(selectedSchütze.Id);

            filterListJahre.Add("Alle");
            filterListJahre.Add("letzten 30 Tagen");
            filterListJahre.Add("den letzten 3 Monaten");

            filterListWaffe.Add("Alle");
            filterListWaffe.Add("letzten 30 Tagen");
            filterListWaffe.Add("den letzten 3 Monaten");

            foreach (KladdenYear j in jahre)
            {
                filterListJahre.Add(j.Date.ToString());
            }

            foreach (KladdenYear j in waffe)
            {
                filterListWaffe.Add(j.Date.ToString());
            }

            ErgebnisSortCbx.DataSource = filterListJahre;
            ErgebnisSortCbx.DisplayMember = "Date";

            WaffenSortCbx.DataSource = filterListWaffe;
            WaffenSortCbx.DisplayMember = "Date";

            isProgrammaticChange = false;
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
        private void CreatePlotModel(List<SchützenErgebnis> ?ergebnisdata = null, List<SchützenWaffe> ?waffendata = null, bool ?selectedTimeFrameForErgebnis = false, bool ?selectedTimeFrameForWaffe = false)
        {
            JObject themeConfig = ConfigurationManager.LoadThemeConfig();

            string primaryTbxBgr = (string)themeConfig["primaryTbxBgr"];
            string primaryFgr = (string)themeConfig["primaryFgr"];
            var sortedErgebnisData = (object)new List<SchützenErgebnis>();
            var sortedWaffenData = (object)new List<SchützenWaffe>();

            if (ergebnisdata != null)
            {
                if (selectedTimeFrameForErgebnis == true)
                {
                    // Sortiere die Daten nach Jahr und gewähltem Zeitraum (DESC)
                    sortedErgebnisData = SortData(ErgebnisData, true);
                }
                else
                {
                    // Sortiere die Daten nach Jahr (DESC)
                    sortedErgebnisData = SortData(ErgebnisData);
                }
                // Ergebnis PlotModel erstellen
                {
                    var ergebnisModel = new PlotModel();

                    // BarSeries erstellen
                    var ergebnisSeries = new BarSeries
                    {
                        FillColor = OxyColor.Parse(primaryTbxBgr), // Optional: Farbe der Balken
                        LabelPlacement = LabelPlacement.Outside,
                        LabelFormatString = "{0}" // Ergebniswerte als Label anzeigen
                    };

                    // Kategorien für die X-Achse (Datum)
                    var kategorien = new List<string>();

                    if (sortedErgebnisData is IEnumerable<SchützenErgebnis> ergebnisse)
                    {
                        // Daten hinzufügen           
                        foreach (var eintrag in ergebnisse)
                        {
                            if (DateTime.TryParse(eintrag.Datum, out DateTime parsedDatum))
                            {
                                // Y-Wert: Ergebnis
                                ergebnisSeries.Items.Add(new BarItem { Value = eintrag.Ergebnis });

                                // X-Achse: Datum hinzufügen
                                kategorien.Add($"{parsedDatum:dd.MM.yyyy} {eintrag.Waffe}");
                            }
                        }
                    }                    

                    ergebnisModel.Series.Add(ergebnisSeries);
                    ergebnisModel.TextColor = OxyColor.Parse(primaryFgr);

                    // X-Achse (Kategorien)
                    ergebnisModel.Axes.Add(new CategoryAxis
                    {
                        Position = AxisPosition.Left, // Für ein vertikales Balkendiagramm (Kategorien auf der linken Seite)
                        ItemsSource = kategorien,
                        GapWidth = 0.4,
                        MajorGridlineStyle = LineStyle.Solid,
                        MinorGridlineStyle = LineStyle.Dot,
                        IsZoomEnabled = false,
                        IsPanEnabled = false                     
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
                        Height = Math.Max(400, ergebnisSeries.Items.Count * 30), // Dynamische Höhe, 30 Pixel pro Datensatz
                        Dock = DockStyle.None // Keine automatische Anpassung an das Panel
                    };

                    ErgebnisStatsPanel.Controls.Clear();
                    ErgebnisStatsPanel.Controls.Add(ergebnisView);
                }
            }
            if(waffendata != null)
            {
                if (selectedTimeFrameForWaffe == true)
                {
                    // Sortiere die Daten nach Jahr und gewähltem Zeitraum (DESC)
                    sortedWaffenData = SortData(waffendata, true);
                }
                else
                {
                    // Sortiere die Daten nach Jahr (DESC)
                    sortedWaffenData = SortData(waffendata);
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

                    if (sortedWaffenData is IEnumerable<SchützenWaffe> waffen)
                    {
                        // Daten hinzufügen           
                        foreach (var eintrag in waffen)
                        {
                            // Y-Wert: Ergebnis
                            series.Items.Add(new BarItem { Value = eintrag.Weaponused });

                            // X-Achse: Datum hinzufügen
                            kategorien.Add($"{eintrag.Waffe}");
                        }
                    }

                    waffenModel.Series.Add(series);
                    waffenModel.TextColor = OxyColor.Parse(primaryFgr);

                    // X-Achse (Kategorien)
                    waffenModel.Axes.Add(new CategoryAxis
                    {
                        Position = AxisPosition.Left, // Für ein vertikales Balkendiagramm (Kategorien auf der linken Seite)
                        ItemsSource = kategorien,
                        GapWidth = 0.4,
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
                        Height = Math.Max(400, series.Items.Count * 30), // Dynamische Höhe, 30 Pixel pro Datensatz
                        Dock = DockStyle.None // Keine automatische Anpassung an das Panel
                    };

                    WaffenStatsPanel.Controls.Clear();
                    WaffenStatsPanel.Controls.Add(waffenView);
                }
            }            
        }
        private object SortData(object dataToSort, bool ?withTimeFrame = false)
        {
            object data = dataToSort;
            object sortedData = (object)new List<object>();
            switch (data)
            {
                case List<SchützenErgebnis> ergebnisse:
                    if (withTimeFrame == true)
                    {
                        DateTime now = DateTime.Now;
                        string sortOption = ErgebnisSortCbx.SelectedItem.ToString();
                        // Sortiere die Daten nach gewähltem Zeitraum(DESC)
                         sortedData = ergebnisse
                            .Where(e => DateTime.TryParse(e.Datum, out _))
                            .Select(e => new { Original = e, ParsedDate = DateTime.Parse(e.Datum) })
                            .Where(e =>
                                sortOption == "letzten 30 Tagen" ? e.ParsedDate >= now.AddDays(-30) :
                                sortOption == "den letzten 3 Monaten" ? e.ParsedDate >= now.AddMonths(-3) :
                                int.TryParse(sortOption, out int selectedYear) ? e.ParsedDate.Year == selectedYear :
                                true
                            )
                            .OrderByDescending(e => e.ParsedDate)
                            .Select(e => e.Original)
                            .ToList();
                    }
                    else
                    {
                        // Sortiere die Daten nach Jahr (DESC)
                        sortedData = ergebnisse
                            .Where(e => DateTime.TryParse(e.Datum, out _)) // Nur gültige Datumswerte berücksichtigen
                            .OrderByDescending(e => DateTime.Parse(e.Datum).Year)   // Nach Jahr absteigend sortieren
                            .ThenByDescending(e => DateTime.Parse(e.Datum).Month)  // Nach Monat absteigend sortieren
                            .ThenByDescending(e => DateTime.Parse(e.Datum).Day)    // Nach Tag absteigend sortieren
                            .ToList();
                    }
                    
                    break;

                case List<SchützenWaffe> waffen:
                    if(withTimeFrame == true)
                    {
                        DateTime now = DateTime.Now;
                        string sortOption = WaffenSortCbx.SelectedItem.ToString();

                        // Filtere und gruppiere die Daten basierend auf der Auswahl in der ComboBox
                        sortedData = waffen
                            .Where(e => DateTime.TryParse(e.Datum, out _)) // Nur gültige Datumswerte berücksichtigen
                            .Select(e => new { Original = e, ParsedDate = DateTime.Parse(e.Datum) })
                            .Where(e =>
                                sortOption == "letzten 30 Tagen" ? e.ParsedDate >= now.AddDays(-30) :
                                sortOption == "den letzten 3 Monaten" ? e.ParsedDate >= now.AddMonths(-3) :
                                int.TryParse(sortOption, out int selectedYear) ? e.ParsedDate.Year == selectedYear :
                                true // Wenn keine spezifische Auswahl getroffen wurde, alle Einträge berücksichtigen
                            )
                            .GroupBy(e => e.Original.Waffe) // Gruppiere die Daten nach Waffe
                            .Select(g => new SchützenWaffe
                            {
                                Waffe = g.Key, // Name der Waffe
                                Weaponused = g.Sum(e => e.Original.Weaponused), // Summe der Verwendungen
                                Datum = null, // Kein spezifisches Datum für aggregierte Daten
                                SchützeId = 0 // Kein spezifischer Schütze für aggregierte Daten
                            })
                            .ToList();

                    }
                    else
                    {
                        sortedData = waffen
                           .Where(e => DateTime.TryParse(e.Datum, out _)) // Nur gültige Datumswerte berücksichtigen
                           .GroupBy(e => e.Waffe) // Gruppiere nach Waffe
                           .Select(g => new SchützenWaffe
                           {
                               Waffe = g.Key, // Name der Waffe
                               Weaponused = g.Sum(e => e.Weaponused), // Gesamte Verwendungen summieren
                               Datum = null, // Kein spezifisches Datum für die Aggregation
                               SchützeId = 0 // Kein spezifischer Schütze für die Aggregation
                           })
                           .ToList();
                    }


                    break;

                default:
                    throw new InvalidOperationException("Unerwarteter Typ");
            }
                
            return sortedData;
        }
        private void WaffenSortCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!isProgrammaticChange)
                CreatePlotModel(waffendata: WaffenData, selectedTimeFrameForWaffe: true);
        }

        private void ErgebnisSortCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isProgrammaticChange)            
                CreatePlotModel(ergebnisdata: ErgebnisData, selectedTimeFrameForErgebnis: true);
        }
    }
}

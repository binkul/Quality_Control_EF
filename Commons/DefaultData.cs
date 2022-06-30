﻿using System.Collections.Generic;

namespace Quality_Control_EF.Commons
{
    public static class DefaultData
    {
        public static readonly string DefaultDataFields = "measure_date|temp|density|pH|vis_1|vis_5|vis_20|disc|comments";
        public static readonly string DefaultDataFieldsNoPh = "measure_date|density|temp|vis_1|vis_5|vis_20|disc|visc_remarks";
        public static readonly List<string> DefaultFieldsList = new List<string>() { "measure_date", "temp", "density", "pH", "vis_1", "vis_5", "vis_20", "disc", "comments" };
        public static readonly string MediumStirng = "ŚREDNIA:";

        /// <summary>
        /// 1 - property name in EF model
        /// 2 - field name in DataBase
        /// 3 - header name in DataGrid
        /// 4 - DataGrid column index
        /// 5 - DataGrid column width
        /// 6 - DataGrid column IsSortable
        /// 7 - DataGrid column IsReadOnly
        /// 8 - DataGrid column IsAlwaysVisible
        /// 9 - DataGrid column IsNumber (double)
        /// 10 - DataGrid column for IsNumber - value format
        /// 11 - DataGrid cell accuracy - number of decimal points
        /// </summary>
        public static IList<QualityDataColumn> ColumnData = new List<QualityDataColumn>()
        {
            new QualityDataColumn("ProductName", "none", "Wyrób", 0, 230, false, true, true, false, "", -1),
            new QualityDataColumn("ProductNumber", "none", "Numer", 1, 60, false, true, true, false, "", -1),
            new QualityDataColumn("MeasureDate", "measure_date", "Data", 2, 80, false, true, true, false, "", -1),
            new QualityDataColumn("DayDistance", "none", "Doba", 3, 55, false, true, true, true, "{0:d}", 0),
            new QualityDataColumn("Temp", "temp", "Temp", 4, 55, false, false, false, false, "", -1),
            new QualityDataColumn("Density", "density", "Gęstość", 5, 80, false, false, false, true, "{0:G}", 3),
            new QualityDataColumn("PH", "pH", "pH", 6, 45, false, false, false, true, "{0:G}", 2),
            new QualityDataColumn("Vis1", "vis_1", "Lep 1", 7, 90, false, false, false, true, "{0:G}", 0),
            new QualityDataColumn("Vis2", "vis_2", "Lep 2", 8, 90, false, false, false, true, "{0:G}", 0),
            new QualityDataColumn("Vis5", "vis_5", "Lep 5", 9, 90, false, false, false, true, "{0:G}", 0),
            new QualityDataColumn("Vis10", "vis_10", "Lep 10", 10, 90, false, false, false, true, "{0:G}", 0),
            new QualityDataColumn("Vis15", "vis_15", "Lep 15", 11, 90, false, false, false, true, "{0:G}", 0),
            new QualityDataColumn("Vis20", "vis_20", "Lep 20", 12, 90, false, false, false, true, "{0:G}", 0),
            new QualityDataColumn("Vis25", "vis_25", "Lep 25", 13, 90, false, false, false, true, "{0:G}", 0),
            new QualityDataColumn("Vis30", "vis_30", "Lep 30", 14, 90, false, false, false, true, "{0:G}", 0),
            new QualityDataColumn("Vis35", "vis_35", "Lep 35", 15, 90, false, false, false, true, "{0:G}", 0),
            new QualityDataColumn("Vis40", "vis_40", "Lep 40", 16, 90, false, false, false, true, "{0:G}", 0),
            new QualityDataColumn("Vis45", "vis_45", "Lep 45", 17, 90, false, false, false, true, "{0:G}", 0),
            new QualityDataColumn("Vis50", "vis_50", "Lep 50", 18, 90, false, false, false, true, "{0:G}", 0),
            new QualityDataColumn("Vis55", "vis_55", "Lep 55", 19, 90, false, false, false, true, "{0:G}", 0),
            new QualityDataColumn("Vis60", "vis_60", "Lep 60", 20, 90, false, false, false, true, "{0:G}", 0),
            new QualityDataColumn("Vis65", "vis_65", "Lep 65", 21, 90, false, false, false, true, "{0:G}", 0),
            new QualityDataColumn("Vis70", "vis_70", "Lep 70", 22, 90, false, false, false, true, "{0:G}", 0),
            new QualityDataColumn("Vis75", "vis_75", "Lep 75", 23, 90, false, false, false, true, "{0:G}", 0),
            new QualityDataColumn("Vis80", "vis_80", "Lep 80", 24, 90, false, false, false, true, "{0:G}", 0),
            new QualityDataColumn("Vis85", "vis_85", "Lep 85", 25, 90, false, false, false, true, "{0:G}", 0),
            new QualityDataColumn("Vis90", "vis_90", "Lep 90", 26, 90, false, false, false, true, "{0:G}", 0),
            new QualityDataColumn("Vis95", "vis_95", "Lep 95", 27, 90, false, false, false, true, "{0:G}", 0),
            new QualityDataColumn("Vis100", "vis_100", "Lep 100", 28, 90, false, false, false, true, "{0:G}", 0),
            new QualityDataColumn("Disc", "disc", "Dysk", 29, 80, false, false, false, false, "", -1),
            new QualityDataColumn("ViscRemarks", "visc_remarks", "Lepkość uwagi", 30, 200, false, false, false, false, "", 2),
            new QualityDataColumn("Contrast75", "contrast_75", "Krycie 75", 31, 90, false, false, false, true, "{0:G}", 2),
            new QualityDataColumn("Contrast100", "contrast_100", "Krycie 100", 32, 90, false, false, false, true, "{0:G}", 2),
            new QualityDataColumn("Contrast125", "contrast_125", "Krycie 125", 33, 90, false, false, false, true, "{0:G}", 2),
            new QualityDataColumn("Contrast150", "contrast_150", "Krycie 150", 34, 90, false, false, false, true, "{0:G}", 2),
            new QualityDataColumn("Contrast200", "contrast_200", "Krycie 200", 35, 90, false, false, false, true, "{0:G}", 2),
            new QualityDataColumn("Contrast240", "contrast_240", "Krycie 240", 36, 90, false, false, false, true, "{0:G}", 2),
            new QualityDataColumn("Contrast250", "contrast_250", "Krycie 250", 37, 90, false, false, false, true, "{0:G}", 2),
            new QualityDataColumn("Contrast300", "contrast_300", "Krycie 300", 38, 90, false, false, false, true, "{0:G}", 2),
            new QualityDataColumn("ContrastWire100", "contrast_wire_100", "Śruba 100", 39, 90, false, false, false, true, "{0:G}", 2),
            new QualityDataColumn("ContrastWire125", "contrast_wire_125", "Śruba 125", 40, 90, false, false, false, true, "{0:G}", 2),
            new QualityDataColumn("ContrastWire150", "contrast_wire_150", "Śruba 150", 41, 90, false, false, false, true, "{0:G}", 2),
            new QualityDataColumn("ContrastWire200", "contrast_wire_200", "Śruba 200", 42, 90, false, false, false, true, "{0:G}", 2),
            new QualityDataColumn("ContrastWire250", "contrast_wire_250", "Śruba 250", 43, 90, false, false, false, true, "{0:G}", 2),
            new QualityDataColumn("ContrastClass", "contrast_class", "Krycie klasa", 44, 80, false, false, false, false, "", -1),
            new QualityDataColumn("ContrastRemarks", "contrast_remarks", "Krycie uwagi", 45, 200, false, false, false, false, "", -1),
            new QualityDataColumn("LM", "L_m", "L mokry", 46, 90, false, false, false, true, "{0:G}", 2),
            new QualityDataColumn("AM", "a_m", "a mokry", 47, 90, false, false, false, true, "{0:G}", 2),
            new QualityDataColumn("BM", "b_m", "b mokry", 48, 90, false, false, false, true, "{0:G}", 2),
            new QualityDataColumn("WiM", "WI_m", "WI mokry", 49, 90, false, false, false, true, "{0:G}", 2),
            new QualityDataColumn("YiM", "YI_m", "YI mokry", 50, 90, false, false, false, true, "{0:G}", 2),
            new QualityDataColumn("LS", "L_s", "L suchy", 51, 90, false, false, false, true, "{0:G}", 2),
            new QualityDataColumn("AS", "a_s", "a suchy", 52, 90, false, false, false, true, "{0:G}", 2),
            new QualityDataColumn("BS", "b_s", "b suchy", 53, 90, false, false, false, true, "{0:G}", 2),
            new QualityDataColumn("WiS", "WI_s", "WI suchy", 54, 90, false, false, false, true, "{0:G}", 2),
            new QualityDataColumn("YiS", "YI_s", "YI suchy", 55, 90, false, false, false, true, "{0:G}", 2),
            new QualityDataColumn("De", "DE", "DE", 56, 50, false, false, false, true, "{0:G}", 2),
            new QualityDataColumn("SpectroRemarks", "spectro_remarks", "Spektro uwagi", 57, 200, false, false, false, false, "", -1),
            new QualityDataColumn("Gloss20", "gloss_20", "Połysk 20", 58, 90, false, false, false, true, "{0:G}", 2),
            new QualityDataColumn("Gloss60", "gloss_60", "Połysk 60", 59, 90, false, false, false, true, "{0:G}", 2),
            new QualityDataColumn("Gloss85", "gloss_85", "Połysk 85", 60, 90, false, false, false, true, "{0:G}", 2),
            new QualityDataColumn("GlossRemarks", "gloss_remarks", "Połysk uwagi", 61, 200, false, false, false, false, "", -1),
            new QualityDataColumn("DryingI", "drying_I", "Schnięcie I", 62, 90, false, false, false, false, "", -1),
            new QualityDataColumn("DryingIi", "drying_II", "Schnięcie II", 63, 90, false, false, false, false, "", -1),
            new QualityDataColumn("DryingIii", "drying_III", "Schnięcie III", 64, 90, false, false, false, false, "", -1),
            new QualityDataColumn("DryingIv", "drying_IV", "Schnięcie IV", 65, 90, false, false, false, false, "", -1),
            new QualityDataColumn("DryingV", "drying_V", "Schnięcie V", 66, 90, false, false, false, false, "", -1),
            new QualityDataColumn("DryingVi", "drying_VI", "Schnięcie VI", 67, 90, false, false, false, false, "", -1),
            new QualityDataColumn("DryingVii", "drying_VII", "Schnięcie VII", 68, 90, false, false, false, false, "", -1),
            new QualityDataColumn("DryingRemarks", "drying_remarks", "Schnięcie uwagi", 69, 200, false, false, false, false, "", -1),
            new QualityDataColumn("ScrubingBrush", "scrubing_brush", "Szorowanie szczotka", 70, 100, false, false, false, false, "", -1),
            new QualityDataColumn("ScrubingSponge", "scrubing_sponge", "Szorowanie gąbka", 71, 100, false, false, false, false, "", -1),
            new QualityDataColumn("ScrubingRemarks", "scrubing_remarks", "Szorowanie uwagi", 72, 200, false, false, false, false, "", -1),
            new QualityDataColumn("Flow", "flow", "Rozlewność", 73, 100, false, false, false, false, "", -1),
            new QualityDataColumn("Runoff", "runoff", "Spływność", 74, 100, false, false, false, false, "", -1),
            new QualityDataColumn("Adhesion", "adhesion", "Przyczepność", 75, 100, false, false, false, false, "", -1),
            new QualityDataColumn("FSolids", "f_solids", "Części stałe", 76, 90, false, false, false, true, "{0:G}", 2),
            new QualityDataColumn("F450", "f_450", "Popiół 450", 77, 90, false, false, false, true, "{0:G}", 2),
            new QualityDataColumn("F900", "f_900", "Popiół 900", 78, 90, false, false, false, true, "{0:G}", 2),
            new QualityDataColumn("FOrganic", "f_organic", "Części organiczne", 79, 90, false, false, false, true, "{0:G}", 2),
            new QualityDataColumn("FLime", "f_lime", "Kreda", 80, 90, false, false, false, true, "{0:G}", 2),
            new QualityDataColumn("FTalcum", "f_talcum", "Talk", 81, 90, false, false, false, true, "{0:G}", 2),
            new QualityDataColumn("FTitanium", "f_titanium", "Dwutlenek tytanu", 82, 90, false, false, false, true, "{0:G}", 2),
            new QualityDataColumn("Voc", "VOC", "VOC", 83, 80, false, false, false, false, "", -1),
            new QualityDataColumn("FRemarks", "f_remarks", "Spalanie uwagi", 84, 200, false, false, false, false, "", -1),
            new QualityDataColumn("Comments", "comments", "Uwagi ogólne", 85, 300, false, false, false, false, "", -1)
        };

        //public static IDictionary<string, string> DataFields { get; } = new Dictionary<string, string>()
        //{
        //    // KEY - kolumn name id DB; VALUE - column name id DGV|column DisplayIndex in dgv|Column width in DGV|Full column description to set fields
        //    {"measure_date", "Data|0|100|Data pomiaru" },
        //    {"temp", "Temp|1|100|Temperatura" },
        //    {"density", "Gęstość|2|100|Gęstość" },
        //    {"pH", "pH|3|80|pH" },
        //    {"vis_1", "Lep 1|4|100|Lepkość 1 rpm" },
        //    {"vis_2", "Lep 2|5|100|Lepkość 2 rpm" },
        //    {"vis_5", "Lep 5|6|100|Lepkość 5 rpm" },
        //    {"vis_10", "Lep 10|7|100|Lepkość 10 rpm" },
        //    {"vis_15", "Lep 15|8|100|Lepkość 15 rpm" },
        //    {"vis_20", "Lep 20|9|100|Lepkość 20 rpm" },
        //    {"vis_25", "Lep 25|10|100|Lepkość 25 rpm" },
        //    {"vis_30", "Lep 30|11|100|Lepkość 30 rpm" },
        //    {"vis_35", "Lep 35|12|100|Lepkość 35 rpm" },
        //    {"vis_40", "Lep 40|13|100|Lepkość 40 rpm" },
        //    {"vis_45", "Lep 45|14|100|Lepkość 45 rpm" },
        //    {"vis_50", "Lep 50|15|100|Lepkość 50 rpm" },
        //    {"vis_55", "Lep 55|16|100|Lepkość 55 rpm" },
        //    {"vis_60", "Lep 60|17|100|Lepkość 60 rpm" },
        //    {"vis_65", "Lep 65|18|100|Lepkość 65 rpm" },
        //    {"vis_70", "Lep 70|19|100|Lepkość 70 rpm" },
        //    {"vis_75", "Lep 75|20|100|Lepkość 75 rpm" },
        //    {"vis_80", "Lep 80|21|100|Lepkość 80 rpm" },
        //    {"vis_85", "Lep 85|22|100|Lepkość 85 rpm" },
        //    {"vis_90", "Lep 90|23|100|Lepkość 90 rpm" },
        //    {"vis_95", "Lep 95|24|100|Lepkość 95 rpm" },
        //    {"vis_100", "Lep 100|25|100|Lepkość 100 rpm" },
        //    {"disc", "Dysk|26|80|Dysk numer" },
        //    {"visc_remarks", "Lepkość uwagi|27|200|Lepkość uwagi" },
        //    {"contrast_75", "Krycie 75|28|100|Krycie blokowy 75um" },
        //    {"contrast_100", "Krycie 100|29|100|Krycie blokowy 100um" },
        //    {"contrast_125", "Krycie 125|30|100|Krycie blokowy 125um" },
        //    {"contrast_150", "Krycie 150|31|100|Krycie blokowy 150um" },
        //    {"contrast_200", "Krycie 200|32|100|Krycie blokowy 200um" },
        //    {"contrast_240", "Krycie 240|33|100|Krycie blokowy 240um" },
        //    {"contrast_250", "Krycie 250|34|100|Krycie blokowy 250um" },
        //    {"contrast_300", "Krycie 300|35|100|Krycie blokowy 300um" },
        //    {"contrast_wire_100", "Śruba 100|36|100|Krycie śrubowy 100um" },
        //    {"contrast_wire_125", "Śruba 125|37|100|Krycie śrubowy 125um" },
        //    {"contrast_wire_150", "Śruba 150|38|100|Krycie śrubowy 150um" },
        //    {"contrast_wire_200", "Śruba 200|39|100|Krycie śrubowy 200um" },
        //    {"contrast_wire_250", "Śruba 250|40|100|Krycie śrubowy 250um" },
        //    {"contrast_class", "Krycie klasa|41|140|Krycie klasa" },
        //    {"contrast_remarks", "Krycie uwagi|42|200|Krycie uwagi" },
        //    {"L_m", "L mokry|43|100|L na mokro" },
        //    {"a_m", "a mokry|44|100|a na mokro" },
        //    {"b_m", "b mokry|45|100|b na mokro" },
        //    {"WI_m", "WI mokra|46|100|Biel WI na mokro" },
        //    {"YI_m", "YI mokre|47|100|Zażółcenie YI na mokro" },
        //    {"L_s", "L suche|48|100|L na sucho" },
        //    {"a_s", "a suche|49|100|a na mokro" },
        //    {"b_s", "b suche|50|100|b na sucho" },
        //    {"WI_s", "WI sucha|51|100|Biel WI na sucho" },
        //    {"YI_s", "YI suche|52|100|Zażółcenie YI na sucho" },
        //    {"DE", "DE|53|80|DE" },
        //    {"spectro_remarks", "Spektro uwagi|54|200|Spektro uwagi" },
        //    {"gloss_20", "Połysk 20|55|100|Połysk przy 20" },
        //    {"gloss_60", "Połysk 60|56|100|Połysk przy 60" },
        //    {"gloss_85", "Połysk 85|57|100|Połysk przy 85" },
        //    {"gloss_remarks", "Połysk uwagi|58|200|Połysk uwagi" },
        //    {"drying_I", "Schnięcie I|59|100|Czas schnięcia I-szy stopień" },
        //    {"drying_II", "Schnięcie II|60|100|Czas schnięcia II-gi stopień" },
        //    {"drying_III", "Schnięcie III|61|100|Czas schnięcia III-ci stopień" },
        //    {"drying_IV", "Schnięcie IV|62|100|Czas schnięcia IV-ty stopień" },
        //    {"drying_V", "Schnięcie V|63|100|Czas schnięcia V-ty stopień" },
        //    {"drying_VI", "Schnięcie VI|64|100|Czas schnięcia VI-ty stopień" },
        //    {"drying_VII", "Schnięcie VII|65|100|Czas schnięcia VII-my stopień" },
        //    {"drying_remarks", "Schnięcie uwagi|66|200|Schnięcie uwagi" },
        //    {"scrubing_brush", "Szorowanie szczotka|67|100|Szorowanie szczotką" },
        //    {"scrubing_sponge", "Szorowanie gąbka|68|100|Szorowanie gąbką" },
        //    {"scrubing_remarks", "Szorowanie uwagi|69|200|Szorowanie uwagi" },
        //    {"flow", "Rozlewność|70|100|Rozlewność" },
        //    {"runoff", "Spływność|71|100|Spływność" },
        //    {"adhesion", "Przyczepność|72|200|Przyczepność do podłoży" },
        //    {"f_solids", "Części stałe|73|100|Części stałe" },
        //    {"f_450", "Popiół 450|74|100|Popiół w 450oC" },
        //    {"f_900", "Popiół 900|75|100|Popiół w 900oC" },
        //    {"f_organic", "Części organiczne|76|100|Części organiczne" },
        //    {"f_lime", "Kreda|77|80|Zawartość kredy" },
        //    {"f_talcum", "Talk|78|80|Zawartość talku" },
        //    {"f_titanium", "Tytan|79|80|Zawartość dwutlenku tytanu" },
        //    {"VOC", "VOC|80|60|VOC ilość" },
        //    {"f_remarks", "Spalanie uwagi|81|200|Spalanie uwagi" },
        //    {"comments", "Uwagi ogólne|82|300|Uwagi ogólne do całości" }
        //};

    }
}

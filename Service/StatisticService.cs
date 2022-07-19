using Microsoft.EntityFrameworkCore;
using Quality_Control_EF.Commons;
using Quality_Control_EF.Forms.Statistic.Model;
using Quality_Control_EF.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;


namespace Quality_Control_EF.Service
{
    public enum StatisticType
    {
        Today,
        Range,
        Product
    }

    internal class StatisticService
    {
        private readonly StatisticDto _statistic;
        public bool Saved { get; set; } = false;
        public List<string> GetVisibleColumn { get; }
        public ISet<long> ModifiedId { get; set; } = new HashSet<long>();
        public SortableObservableCollection<QualityControlData> Statistic { get; set; }

        public StatisticService(StatisticDto statistic)
        {
            _statistic = statistic;
            Statistic = GetStatistic();
            GetVisibleColumn = ColumnVisibility();
        }

        private SortableObservableCollection<QualityControlData> GetStatistic()
        {
            switch (_statistic.Type)
            {
                case StatisticType.Product:
                    return GetProductData();
                case StatisticType.Range:
                    return GetRangeData();
                default:
                    return GetTodayData();
            }
        }

        private SortableObservableCollection<QualityControlData> GetTodayData()
        {
            SortableObservableCollection<QualityControlData> result = new SortableObservableCollection<QualityControlData>();

            using (LabBookContext contex = new LabBookContext())
            {
                List<QualityControl> tmpResult = contex.QualityControl
                    .Where(x => x.ProductionDate.Date == _statistic.DateStart.Date)
                    .Include(x => x.QualityControlData)
                    .OrderBy(x => x.Number)
                    .ThenBy(x => x.Id)
                    .ToList();

                foreach (QualityControl quality in tmpResult)
                {
                    foreach (QualityControlData data in quality.QualityControlData)
                    {
                        data.ProductNumber = "P" + quality.Number;
                        data.ProductName = quality.ProductName;
                        data.ProductActiveFields = quality.ActiveFields;
                        result.Add(data);
                    }
                }
            }

            result.Sort(x => x.ProductNumber, System.ComponentModel.ListSortDirection.Ascending);

            return result;
        }

        private SortableObservableCollection<QualityControlData> GetProductData()
        {
            SortableObservableCollection<QualityControlData> result = new SortableObservableCollection<QualityControlData>();

            using(LabBookContext contex = new LabBookContext())
            {
                List<QualityControl> tmpResult = contex.QualityControl
                    .Where(x => x.ProductName.Equals(_statistic.Product.Name))
                    .Where(x => x.ProductionDate.Date >= _statistic.DateStart.Date && x.ProductionDate.Date <= _statistic.DateEnd.Date)
                    .OrderBy(x => x.Number)
                    .Include(x => x.QualityControlData)
                    .ToList();

                foreach (QualityControl data in tmpResult)
                {
                    ICollection<QualityControlData> measures = data.QualityControlData;
                    _ = measures.OrderBy(x => x.MeasureDate).ThenBy(x => x.Id);
                }

                int i = 1;
                foreach (QualityControl quality in tmpResult)
                {
                    if (quality.QualityControlData.Count == 0) continue;

                    QualityControlData data = quality.QualityControlData.FirstOrDefault();
                    data.ProductNumber = "P" + quality.Number;
                    data.ProductName = "Produkcja " + i;
                    data.ProductActiveFields = quality.ActiveFields;
                    result.Add(data);
                    i++;
                }

                if (result.Count == 0) 
                    return result;

                QualityControlData summary = CalculateMediums(result);
                result.Add(summary);
            }

            return result;
        }

        private SortableObservableCollection<QualityControlData> GetRangeData()
        {
            SortableObservableCollection<QualityControlData> result = new SortableObservableCollection<QualityControlData>();

            using (LabBookContext contex = new LabBookContext())
            {
                List<QualityControl> tmpResult = contex.QualityControl
                    .Where(x => x.ProductionDate.Date >= _statistic.DateStart.Date && x.ProductionDate <= _statistic.DateEnd.Date)
                    .Include(x => x.QualityControlData)
                    .OrderBy(x => x.Number)
                    .ThenBy(x => x.Id)
                    .ToList();

                foreach (QualityControl control in tmpResult)
                {
                    control.QualityControlData.OrderBy(x => x.MeasureDate).ThenBy(x => x.Id);
                }

                foreach (QualityControl quality in tmpResult)
                {
                    var data = quality.QualityControlData.FirstOrDefault();
                    if (data != null)
                    {
                        data.ProductNumber = "P" + quality.Number;
                        data.ProductName = quality.ProductName;
                        data.ProductActiveFields = quality.ActiveFields;
                        result.Add(data);
                    }
                }
            }

            result.Sort(x => x.ProductNumber, System.ComponentModel.ListSortDirection.Ascending);

            return result;
        }

        private int FindAccuracy(PropertyInfo prop)
        {
            foreach (QualityDataColumn column in DefaultData.ColumnData)
            {
                if (prop.Name == column.EFColumnName)
                    return column.ValueAccuracy;
            }
            return -1;
        }

        internal void RecalculateProductData()
        {
            QualityControlData summary = CalculateMediums(Statistic);

            QualityControlData summaryToRemove = Statistic.FirstOrDefault(x => x.ProductName.Equals(DefaultData.MediumStirng));
            if (summaryToRemove != null)
            {
                _ = Statistic.Remove(summaryToRemove);
            }

            Statistic.Add(summary);
        }

        private QualityControlData CalculateMediums(ICollection<QualityControlData> results)
        {
            QualityControlData summary = new QualityControlData
            {
                ProductName = DefaultData.MediumStirng,
                MeasureDate = DateTime.Today
            };

            foreach (PropertyInfo prop in typeof(QualityControlData).GetProperties())
            {
                if (!prop.PropertyType.FullName.Contains("Double")) continue;

                double sum = 0;
                int count = 0;
                foreach (QualityControlData data in results)
                {
                    if (data.ProductName.Equals(DefaultData.MediumStirng)) continue;

                    object val = prop.GetValue(data);
                    if (val != null)
                    {
                        sum += (double)val;
                        count++;
                    }
                }

                if (count > 0)
                {
                    sum /= count;
                    int accuracy = FindAccuracy(prop) >= 0 ? FindAccuracy(prop) : 0;
                    sum = Math.Round(sum, accuracy);
                    prop.SetValue(summary, sum);
                }
            }

            return summary;
        }

        private List<string> ColumnVisibility()
        {
            return Statistic
                    .Select(row => row.ProductActiveFields)
                    .Select(field => field.Split('|'))
                    .SelectMany(x => x)
                    .Distinct()
                    .ToList();
        }

        internal bool Save()
        {
            bool result = true;

            try
            {
                List<QualityControlData> modified = Statistic.Where(x => x.Modified).ToList();
                using (LabBookContext contex = new LabBookContext())
                {
                    contex.UpdateRange(modified);
                    _ = contex.SaveChanges();
                }

                Saved = true;
            }
            catch (DbUpdateConcurrencyException e)
            {
                _ = MessageBox.Show("Błąd 'DbUpdateConcurrencyException' w czasie zapisu danych do tabel QualityControl i QualityControlData: " + e, "Błąd zapisu",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (DbUpdateException e)
            {
                _ = MessageBox.Show("Błąd 'DbUpdateException' w czasie zapisu danych do tabel QualityControl i QualityControlData: " + e, "Błąd zapisu",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            ModifiedId = Statistic
                .Where(x => x.Modified)
                .Where(x => !x.ProductName.Equals(DefaultData.MediumStirng))
                .Select(x => x.Id)
                .ToHashSet();

            Statistic
                .Where(x => x.Modified)
                .ToList()
                .ForEach(x => x.Modified = false);

            return result;
        }

        internal void ExportToExcel()
        {
            Excel.Application excelApp = OpenExcel();
            if (excelApp == null)
                return;
            Excel.Worksheet excelWorkSheet = PrepareExcelWorkSheet(excelApp);

            int col = 3;
            int row = 1;
            foreach (PropertyInfo prop in typeof(QualityControlData).GetProperties())
            {
                if (SkipReflectionFields(prop.Name)) continue;

                QualityDataColumn colData = DefaultData.ColumnData.FirstOrDefault(x => x.EFColumnName.Equals(prop.Name));

                if (!GetVisibleColumn.Contains(colData.DBColumnName) &&
                    colData.EFColumnName != "ProductName" &&
                    colData.EFColumnName != "ProductNumber") continue;

                row = 1;
                WriteToExcelCell(excelWorkSheet, prop.Name, colData.ColumnHeader, row, col);
                foreach (QualityControlData data in Statistic)
                {
                    row++;
                    string value = GetReflectionValue(prop, data);
                    WriteToExcelCell(excelWorkSheet, prop.Name, value, row, col);
                }
                col++;
            }

            FormatExcelWorkSheet(excelWorkSheet);
            ShowExcel(excelApp);
        }

        private string GetReflectionValue(PropertyInfo prop, QualityControlData data)
        {
            string value;
            if (prop.GetValue(data) == null)
            {
                value = "";
            }
            else if (prop.PropertyType.FullName.Contains("DateTime"))
            {
                value = ((DateTime)prop.GetValue(data)).ToShortDateString();
            }
            else if (prop.PropertyType.FullName.Contains("Double"))
            {
                double d = (double)prop.GetValue(data);
                value = d.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                value = prop.GetValue(data).ToString();
            }

            return value;
        }

        private bool SkipReflectionFields(string name)
        {
            return name.Equals("ProductActiveFields") ||
                        name.Equals("DayDistance") ||
                        name.Equals("Modified") ||
                        name.Equals("Id") ||
                        name.Equals("QualityId") ||
                        name.Equals("QualityControl");
        }
        
        private Excel.Application OpenExcel()
        {
            Excel.Application excelApp = null;

            try
            {
                excelApp = new Excel.Application();
                if (excelApp == null)
                {
                    _ = MessageBox.Show("Nie można otworzyć Excela. Sprawdź, czy jest poprawnie zainstalowany",
                        "Błąd otwarcia aplikacji", MessageBoxButton.OK, MessageBoxImage.Error);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    return null;
                }
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show("Nie można uruchomić aplikacji Excel. Sprawdź, czy jest poprawnie zainstalowany '" +
                                ex.Message + "'", "Błąd otwarcia aplikacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            return excelApp;
        }

        private void ShowExcel(Excel.Application excelApp)
        {
            excelApp.Visible = true;
            excelApp.UserControl = true;
            excelApp.ActiveWindow.Activate();
        }

        private Excel.Worksheet PrepareExcelWorkSheet(Excel.Application excelApp)
        {
            _ = excelApp.Workbooks.Add();
            Excel.Sheets excelSheets = excelApp.Worksheets;
            return (Excel.Worksheet)excelSheets.get_Item("Arkusz1");
        }

        private void WriteToExcelCell(Excel.Worksheet excelWorkSheet, string name, string value, int row, int col)
        {
            switch (name)
            {
                case "ProductName":
                    excelWorkSheet.Cells[row, 1] = value;
                    break;
                case "ProductNumber":
                    excelWorkSheet.Cells[row, 2] = value;
                    break;
                default:
                    excelWorkSheet.Cells[row, col] = value;
                    break;
            }
        }

        private void FormatExcelWorkSheet(Excel.Worksheet excelWorkSheet)
        {
            excelWorkSheet.Cells[1, 1].EntireRow.Font.Size = 12;
            excelWorkSheet.Cells[1, 1].EntireRow.Font.Bold = true;
            excelWorkSheet.Cells[1, 1].EntireRow.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            excelWorkSheet.get_Range("A:A").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            excelWorkSheet.get_Range("B:BA").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            excelWorkSheet.get_Range("A:BA").Columns.AutoFit();
        }
    }
}

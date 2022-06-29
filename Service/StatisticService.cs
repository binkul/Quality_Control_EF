using Microsoft.EntityFrameworkCore;
using Quality_Control_EF.Commons;
using Quality_Control_EF.Forms.Statistic.Model;
using Quality_Control_EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;

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
                    return null; // _repository.GetStatisticRange();
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
    }
}

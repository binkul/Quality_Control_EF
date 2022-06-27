using Microsoft.EntityFrameworkCore;
using Quality_Control_EF.Commons;
using Quality_Control_EF.Forms.Statistic.Model;
using Quality_Control_EF.Models;
using System.Collections.Generic;
using System.Linq;
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
        public ISet<long> ModifiedId { get; } = new HashSet<long>();
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

            }

            return result;
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

            List<long> list = Statistic
                .Where(x => x.Modified)
                .Select(x => x.Id)
                .ToList();

            foreach (long i in list)
            {
                _ = ModifiedId.Add(i);
            }

            Statistic
                .Where(x => x.Modified)
                .ToList()
                .ForEach(x => x.Modified = false);

            return result;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Quality_Control_EF.Commons;
using Quality_Control_EF.Forms.Statistic.Model;
using Quality_Control_EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private readonly LabBookContext _contex;
        private readonly StatisticDto _statistic;
        public List<string> GetVisibleColumn { get; }
        public SortableObservableCollection<QualityControlData> Statistic { get; set; }

        public StatisticService(LabBookContext contex, StatisticDto statistic)
        {
            _contex = contex;
            _statistic = statistic;
            Statistic = GetStatistic();
            GetVisibleColumn = ColumnVisibility();

        }

        private SortableObservableCollection<QualityControlData> GetStatistic()
        {
            switch (_statistic.Type)
            {
                case StatisticType.Product:
                    return null;
                case StatisticType.Range:
                    return null; // _repository.GetStatisticRange();
                default:
                    return GetTodayData();
            }
        }

        private SortableObservableCollection<QualityControlData> GetTodayData()
        {
            List<QualityControl> tmpResult = _contex.QualityControl
                .Where(x => x.ProductionDate.Date == _statistic.DateStart.Date)
                .Include(x => x.QualityControlData)
                .OrderBy(x => x.Number)
                .ToList();

            SortableObservableCollection<QualityControlData> result = new SortableObservableCollection<QualityControlData>();
            foreach (QualityControl quality in tmpResult)
            {
                foreach (QualityControlData data in quality.QualityControlData)
                {
                    data.ProductNumber = "P" + quality.Number;
                    data.ProductName = quality.ProductName;
                    result.Add(data);
                }
            }

            result.Sort(x => x.ProductNumber, System.ComponentModel.ListSortDirection.Ascending);

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

    }
}

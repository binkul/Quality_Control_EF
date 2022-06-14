using Quality_Control_EF.Commons;
using Quality_Control_EF.Forms.Statistic.Command;
using Quality_Control_EF.Forms.Statistic.Model;
using Quality_Control_EF.Models;
using Quality_Control_EF.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Quality_Control_EF.Forms.Statistic.ModelView
{
    internal class StatisticTodayMV : INotifyPropertyChanged
    {
        private static readonly StatisticDto today = new StatisticDto("Statystyka na dziś: " + DateTime.Today.ToShortDateString(),
        DateTime.Now, DateTime.Now, StatisticType.Today);

        private ICommand _saveTodayButton;
        private readonly StatisticService _service;
        public event PropertyChangedEventHandler PropertyChanged;

        public StatisticTodayMV(LabBookContext context)
        {
            _service = new StatisticService(context, today);
        }

        private void OnPropertyChanged(params string[] names)
        {
            if (PropertyChanged != null)
            {
                foreach (string name in names)
                    PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public SortableObservableCollection<QualityControlData> TodayData => _service.Statistic;

        public List<string> GetActiveFields => _service.GetVisibleColumn;

        public bool IsAnyQuality => TodayData.Count > 0;

        public bool Modified => _service.Statistic.Any(x => x.Modified);

        public ICommand TodaySaveButton
        {
            get
            {
                if (_saveTodayButton == null) _saveTodayButton = new TodaySaveButton(this);
                return _saveTodayButton;
            }
        }

        internal void SaveToday()
        {
            //_ = _service.SaveToday();
        }

    }
}

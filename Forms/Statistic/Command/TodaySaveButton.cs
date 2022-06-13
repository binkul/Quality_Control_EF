using Quality_Control_EF.Forms.Statistic.ModelView;
using System;
using System.Windows.Input;

namespace Quality_Control_EF.Forms.Statistic.Command
{
    internal class TodaySaveButton : ICommand
    {
        private readonly StatisticTodayMV _modelView;

        public TodaySaveButton(StatisticTodayMV modelView)
        {
            _modelView = modelView ?? throw new ArgumentNullException("Model widoku jest null");
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return _modelView.Modified;
        }

        public void Execute(object parameter)
        {
            _modelView.SaveToday();
        }
    }
}

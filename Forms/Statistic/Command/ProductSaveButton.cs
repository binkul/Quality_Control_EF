using Quality_Control_EF.Forms.Statistic.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Quality_Control_EF.Forms.Statistic.Command
{
    internal class ProductSaveButton : ICommand
    {
        private readonly StatisticForProductMV _modelView;

        public ProductSaveButton(StatisticForProductMV modelView)
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
            _modelView.Save();
        }

    }
}

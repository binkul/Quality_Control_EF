using Quality_Control_EF.Forms.Statistic.ModelView;
using Quality_Control_EF.Models;
using System.Windows;

namespace Quality_Control_EF.Forms.Statistic
{
    /// <summary>
    /// Logika interakcji dla klasy StatisticTodayForm.xaml
    /// </summary>
    public partial class StatisticTodayForm : Window
    {
        public StatisticTodayForm(LabBookContext contex)
        {
            InitializeComponent();

            StatisticTodayMV statisticMV = new StatisticTodayMV(contex);
            DataContext = statisticMV;

        }
    }
}

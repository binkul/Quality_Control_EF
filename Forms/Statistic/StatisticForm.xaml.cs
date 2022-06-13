using Quality_Control_EF.Forms.Navigation;
using Quality_Control_EF.Forms.Statistic.ModelView;
using Quality_Control_EF.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;

namespace Quality_Control_EF.Forms.Statistic
{
    /// <summary>
    /// Logika interakcji dla klasy StatisticForm.xaml
    /// </summary>
    public partial class StatisticForm : RibbonWindow
    {
        public StatisticForm(LabBookContext contex)
        {
            InitializeComponent();

            Height = SystemParameters.PrimaryScreenHeight - 100;
            StatisticMV statisticMV = new StatisticMV(contex);
            DataContext = statisticMV;

            NavigationMV navigationMV = Resources["navi"] as NavigationMV;
            navigationMV.ModelView = statisticMV;
            statisticMV.SetNavigationMV = navigationMV;

        }

        private void DgProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = DgProduct.SelectedIndex;
            if (index < 0)
            {
                return;
            }

            object item = DgProduct.Items[index];
            if (!(DgProduct.ItemContainerGenerator.ContainerFromIndex(index) is DataGridRow))
            {
                DgProduct.ScrollIntoView(item);
            }
        }

    }
}

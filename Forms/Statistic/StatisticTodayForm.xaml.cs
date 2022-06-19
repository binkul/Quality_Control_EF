using Quality_Control_EF.Forms.Statistic.ModelView;
using Quality_Control_EF.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

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
            Title = "Wyniki na dzień - " + DateTime.Now.ToShortDateString();
            Height = SystemParameters.PrimaryScreenHeight - 100;
            Width = SystemParameters.PrimaryScreenWidth - 200;

            StatisticTodayMV statisticMV = new StatisticTodayMV(contex);
            DataContext = statisticMV;


            Style headerStyle = (Style)Resources["AllignColmnHeaderCenter"];
            Style cellStyle = (Style)Resources["AllignCellCenter"];
            Binding binding = new Binding("ProductNumber");
            binding.Mode = BindingMode.OneWay;

            DataGridTextColumn column = new DataGridTextColumn();
            column.Header = "Numer";
            column.Width = 60;
            column.CanUserSort = false;
            column.IsReadOnly = true;
            column.HeaderStyle = headerStyle;
            column.ElementStyle = cellStyle;
            column.Binding = binding;
            DgQualityData.Columns.Add(column);
        }

        private void DgQualityData_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Right)
            {
                // Cancel [Enter] key event.
                e.Handled = true;
                // Press [Tab] key programatically.
                KeyEventArgs tabKeyEvent = new KeyEventArgs(
                  e.KeyboardDevice, e.InputSource, e.Timestamp, Key.Tab)
                {
                    RoutedEvent = Keyboard.KeyDownEvent
                };
                _ = InputManager.Current.ProcessInput(tabKeyEvent);
            }
        }

    }
}

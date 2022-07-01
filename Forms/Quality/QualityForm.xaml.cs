using Quality_Control_EF.Commons;
using Quality_Control_EF.Converters;
using Quality_Control_EF.Forms.Navigation;
using Quality_Control_EF.Forms.Quality.ModelView;
using Quality_Control_EF.Models;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Input;

namespace Quality_Control_EF.Forms.Quality
{
    /// <summary>
    /// Logika interakcji dla klasy QualityForm.xaml
    /// </summary>
    public partial class QualityForm : RibbonWindow
    {
        public QualityForm(User user)
        {
            InitializeComponent();

            QualityMV view = (QualityMV)DataContext;
            NavigationMV navigationMV = Resources["navi"] as NavigationMV;

            navigationMV.ModelView = view;
            view.SetNavigationMV = navigationMV;
            view.SetUser = user;

            SetColumns(view);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void DgQuality_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = DgQuality.SelectedIndex;
            if (index < 0)
            {
                return;
            }

            object item = DgQuality.Items[index];
            if (!(DgQuality.ItemContainerGenerator.ContainerFromIndex(index) is DataGridRow))
            {
                DgQuality.ScrollIntoView(item);
            }
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

        private void SetColumns(QualityMV qualityMV)
        {
            Style headerStyle = (Style)Resources["AllignColmnHeaderCenter"];
            Style cellStyleCenter = (Style)Resources["AllignCellCenter"];
            Style cellStyleLeft = (Style)Resources["AllignCellLeft"];


            foreach (QualityDataColumn data in DefaultData.ColumnData)
            {
                if (data.EFColumnName.Equals("ProductName") || data.EFColumnName.Equals("ProductNumber")) continue;


                DataGridTextColumn column = new DataGridTextColumn
                {
                    HeaderStyle = headerStyle,
                    ElementStyle = cellStyleCenter,
                    Header = data.ColumnHeader,
                    Width = data.ColumnWidth,
                    IsReadOnly = data.IsReadOnly,
                    CanUserSort = data.CanUserSort
                };

                Binding binding = new Binding(data.EFColumnName)
                {
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                };

                if (data.IsValue)
                {
                    binding.ConverterCulture = System.Globalization.CultureInfo.CurrentCulture;
                    binding.StringFormat = data.ValueFormat;
                    binding.UpdateSourceTrigger = UpdateSourceTrigger.LostFocus;
                    binding.Mode = BindingMode.TwoWay;
                    binding.Converter = new EmptyStringToNullConverter();
                    column.EditingElementStyle = (Style)Resources["DoubleErrorStyle"];
                }
                else if (data.ColumnHeader.Equals("Data"))
                {
                    binding.Mode = BindingMode.TwoWay;
                    binding.Converter = new DateTimeConverter();
                    column.IsReadOnly = false;
                }
                column.Binding = binding;

                if (!data.IsAlwaysVisible)
                {
                    Binding visible = new Binding
                    {
                        Source = qualityMV,
                        Path = new PropertyPath("GetActiveFields"),
                        Mode = BindingMode.OneWay,
                        Converter = new ColumnVisibilityConverter(),
                        ConverterParameter = data.DBColumnName
                    };

                    _ = BindingOperations.SetBinding(column, DataGridColumn.VisibilityProperty, visible);
                }

                DgQualityData.Columns.Add(column);
            }
        }

    }
}

using Quality_Control_EF.Forms.Navigation;
using Quality_Control_EF.Forms.Quality.ModelView;
using Quality_Control_EF.Models;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
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
    }
}

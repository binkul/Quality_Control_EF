using Quality_Control_EF.Forms.AddNew.ModelView;
using Quality_Control_EF.Forms.Navigation;
using Quality_Control_EF.Models;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Quality_Control_EF.Forms.AddNew
{
    /// <summary>
    /// Logika interakcji dla klasy AddNewQualityForm.xaml
    /// </summary>
    public partial class AddNewQualityForm : Window
    {
        private readonly AddNewQualityMV view;
        public bool Cancel { get; private set; } = true;
        public Product Product => view.ActualProduct;
        public int Number => Convert.ToInt32(view.ProductNumber);
        public DateTime ProductionDate => view.ProductionDate;

        public AddNewQualityForm(LabBookContext contex)
        {
            InitializeComponent();

            AddNewQualityMV view = new AddNewQualityMV(contex);
            NavigationMV navigationMV = Resources["navi"] as NavigationMV;
            DataContext = view;

            navigationMV.ModelView = view;
            view.SetNavigationMV = navigationMV;

            Height = SystemParameters.PrimaryScreenHeight - 100;
            if (SystemParameters.PrimaryScreenWidth <= 800)
                Width = 600;
            else if (SystemParameters.PrimaryScreenWidth > 800 && SystemParameters.PrimaryScreenWidth <= 1000)
                Width = 800;
            else
                Width = 1000;
        }

        private void Number_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Cancel = true;
            Close();
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            if (view.IsNumberCorrect())
            {
                Cancel = false;
                Close();
            }
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

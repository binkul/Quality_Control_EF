using Quality_Control_EF.Forms.Modification.ModelView;
using Quality_Control_EF.Models;
using System.Windows;
using System.Windows.Controls.Ribbon;

namespace Quality_Control_EF.Forms.Modification
{
    /// <summary>
    /// Logika interakcji dla klasy ModificationForm.xaml
    /// </summary>
    public partial class ModificationForm : RibbonWindow
    {
        private QualityControl _quality;
        public bool Cancel { get; private set; } = true;
        public string Fields { get; private set; } = "";

        public ModificationForm(QualityControl quality)
        {
            InitializeComponent();

            _quality = quality;
            Fields = _quality.ActiveFields;
            InitializeComponent();
            Height = SystemParameters.PrimaryScreenHeight - 100;
            ProductNameLbl.Content = "P" + quality.Number + " " + quality.ProductName;

            ModificationMV view = (ModificationMV)DataContext;
            view.Quality = _quality;
            view.InitiateFields();
        }

        private void RibbonButton_Click(object sender, RoutedEventArgs e)
        {
            ModificationMV view = (ModificationMV)DataContext;
            Fields = view.SettingFields;
            Cancel = false;
            Close();
        }

    }
}

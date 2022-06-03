using GalaSoft.MvvmLight.CommandWpf;
using Quality_Control_EF.Commons;
using Quality_Control_EF.Forms.Navigation;
using Quality_Control_EF.Forms.Quality.Command;
using Quality_Control.Forms.Quality.Model;
using Quality_Control_EF.Service;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Quality_Control_EF.Models;

namespace Quality_Control_EF.Forms.Quality.ModelView
{
    public class QualityMV : INotifyPropertyChanged, INavigation
    {
        private ICommand _saveButton;
        private ICommand _deleteButton;
        private ICommand _delQualityData;
        private ICommand _addNewButton;
        private ICommand _modificationButton;
        private ICommand _settingsButton;
        private ICommand _statisticButton;

        private readonly double _startLeftPosition = 32;
        private readonly WindowData _windowData = WindowSettings.Read();
        private readonly QualityService _service = new QualityService();
        private NavigationMV _navigationMV;
        private int _selectedIndex;
        public QualityControl ActualQuality { private get; set; }
        public QualityControlData ActualControlData { private get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand<CancelEventArgs> OnClosingCommand { get; set; }
        public RelayCommand<TextChangedEventArgs> OnProductNameFilterTextChanged { get; set; }
        public RelayCommand<TextChangedEventArgs> OnProductNumberFilterTextChanged { get; set; }
        public RelayCommand<SelectionChangedEventArgs> OnComboYearSelectionChanged { get; set; }
        public RelayCommand<InitializingNewItemEventArgs> OnInitializingNewQualityDataCommand { get; set; }
        public RelayCommand<DataGridCellEditEndingEventArgs> OnCellQualityDataChange { get; set; }

        public QualityMV()
        {
            OnClosingCommand = new RelayCommand<CancelEventArgs>(OnClosingCommandExecuted);
            OnProductNameFilterTextChanged = new RelayCommand<TextChangedEventArgs>(OnProductNameTextChangedFilter);
            OnProductNumberFilterTextChanged = new RelayCommand<TextChangedEventArgs>(OnProductNumberTextChangedFilter);
            OnComboYearSelectionChanged = new RelayCommand<SelectionChangedEventArgs>(OnYearSelectionCommandExecuted);
            OnInitializingNewQualityDataCommand = new RelayCommand<InitializingNewItemEventArgs>(OnInitializingNewQualityDataCommandExecuted);
            OnCellQualityDataChange = new RelayCommand<DataGridCellEditEndingEventArgs>(OnCellQualityDataChangeExecuted);
        }

        internal NavigationMV SetNavigationMV
        {
            set => _navigationMV = value;
        }

        public SortableObservableCollection<QualityControl> Quality => _service.Quality; //ok

        public SortableObservableCollection<QualityControlData> QualityData => _service.QualityData; //ok

        public List<string> GetActiveFields => _service.ActiveFields; //ok

        internal bool Modified => _service.Modified; //ok

        #region Events - RelayCommand

        protected void OnPropertyChanged(params string[] names)
        {
            if (PropertyChanged != null)
            {
                foreach (string name in names)
                    PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void OnClosingCommandExecuted(CancelEventArgs e)
        {
            MessageBoxResult ansver = MessageBoxResult.No;

            if (Modified)
            {
                ansver = MessageBox.Show("Dokonano zmian w rekordach. Czy zapisać zmiany?", "Zapis zmian", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            }

            if (ansver == MessageBoxResult.Yes)
            {
                SaveAll();
                WindowSettings.Save(_windowData);
            }
            else if (ansver == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
            else
            {
                WindowSettings.Save(_windowData);
            }
        }

        private void OnYearSelectionCommandExecuted(SelectionChangedEventArgs e)
        {
            //SaveDataQuality();
            //if (SaveQuality())
            {
                ReloadYears();
            }
            _service.ReloadQuality(Year);
            Filter();
        }

        public void OnInitializingNewQualityDataCommandExecuted(InitializingNewItemEventArgs e)
        {

        }

        private void OnCellQualityDataChangeExecuted(DataGridCellEditEndingEventArgs e)
        {
            ActualControlData.Modified = true;
        }

        #endregion

        #region Filtering

        public string ProductName { get; set; } = "";

        public string ProductNumber { get; set; } = "";

        public void OnProductNameTextChangedFilter(TextChangedEventArgs e)
        {
            Filter();
        }

        public void OnProductNumberTextChangedFilter(TextChangedEventArgs e)
        {
            Filter();
        }

        private void Filter()
        {
            _service.Filter(ProductNumber, ProductName);
            DgRowIndex = 0;
            OnPropertyChanged(nameof(Quality));
        }

        #endregion

        #region Form dimension and position

        public Thickness TxtNumberLeftPosition => new Thickness(_startLeftPosition, 0, 0, 5);

        public double FormXpos
        {
            get => _windowData.FormXpos;
            set
            {
                _windowData.FormXpos = value;
                OnPropertyChanged(nameof(FormXpos));
            }
        }

        public double FormYpos
        {
            get => _windowData.FormYpos;
            set
            {
                _windowData.FormYpos = value;
                OnPropertyChanged(nameof(FormYpos));
            }
        }

        public double FormWidth
        {
            get => _windowData.FormWidth;
            set
            {
                _windowData.FormWidth = value;
                OnPropertyChanged(nameof(FormWidth));
            }
        }

        public double FormHeight
        {
            get => _windowData.FormHeight;
            set
            {
                _windowData.FormHeight = value;
                OnPropertyChanged(nameof(FormHeight));
            }
        }

        #endregion

        #region Current

        public bool IsFilterOn => ProductName.Length > 0 || ProductNumber.Length > 0;

        public bool IsAnyQuality => _service.GetQualityCount > 0;

        internal QualityControl GetCurrentQuality => _service.Quality[_selectedIndex];

        public bool IsTextBoxActive { get; set; } = true;

        public int Year
        {
            get => _service.Year;
            set => _service.Year = value;
        }

        public List<int> Years => _service.Years;

        #endregion

        #region Navigation

        public int DgRowIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;

                if (value >= 0 && _service.GetQualityCount != 0 && value < _service.GetQualityCount)
                {
                    QualityControl model = _service.Quality[_selectedIndex];
                    _service.RefreshQualityData(model);
                    IsTextBoxActive = true;
                }
                else
                {
                    _service.RefreshQualityData(null);
                    IsTextBoxActive = false;
                }
                OnPropertyChanged(nameof(DgRowIndex),
                    nameof(IsAnyQuality),
                    nameof(IsTextBoxActive),
                    nameof(GetActiveFields));

                Refresh();
            }
        }

        public int GetRowCount => _service.GetQualityCount;

        public void Refresh()
        {
            if (_navigationMV != null)
                _navigationMV.Refresh();
        }

        #endregion

        #region Command and their procedures

        public ICommand SaveButton
        {
            get
            {
                if (_saveButton == null) _saveButton = new SaveButton(this);
                return _saveButton;
            }
        }

        public ICommand DeleteButton
        {
            get
            {
                if (_deleteButton == null) _deleteButton = new DeleteButton(this);
                return _deleteButton;
            }
        }

        public ICommand DeleteQualityDataButton
        {
            get
            {
                if (_delQualityData == null) _delQualityData = new DeleteQualityDataButton(this);
                return _delQualityData;
            }
        }

        public ICommand AddNewButton
        {
            get
            {
                if (_addNewButton == null) _addNewButton = new AddNewButton(this);
                return _addNewButton;
            }
        }

        public ICommand ModificationButton
        {
            get
            {
                if (_modificationButton == null) _modificationButton = new ModificationButton(this);
                return _modificationButton;
            }

        }

        public ICommand SettingsButton
        {
            get
            {
                if (_settingsButton == null) _settingsButton = new SettingsButton(this);
                return _settingsButton;
            }

        }

        public ICommand StatisticButton
        {
            get
            {
                if (_statisticButton == null) _statisticButton = new StatisticButton(this);
                return _statisticButton;
            }

        }

        internal void SaveAll()
        {
            if (!Modified)
                return;

            if (_service.Save())
                ReloadYears();
        }

        private void ReloadYears()
        {
            _service.ReloadYears();
            OnPropertyChanged(nameof(Years), nameof(Year));
        }

        internal void DeleteAll()
        {
            if (ActualQuality == null) return;
            _service.Delete(ActualQuality);
        }

        internal void DeleteData()
        {

        }

        internal void AddNew()
        {
            //AddNewForm form = new AddNewForm();
            //_ = form.ShowDialog();

            //if (form.Cancel) return;

            //QualityModel quality = new QualityModel(form.Number, form.Product, form.Index, form.LabBookId,
            //        form.Type, "", "", "", form.ProductionDate, UserSingleton.Id, UserSingleton.Login);

            //quality = _service.SaveNewQuality(quality);

            //if (quality.Id <= 0) return;

            //if (quality.ProductionDate.Year == Year)
            //{
            //    _service.AddQuality(quality);
            //}
            //else if (quality.ProductionDate.Year != Year && Years.Contains(quality.ProductionDate.Year))
            //{
            //    Year = quality.ProductionDate.Year;
            //    OnPropertyChanged(nameof(Year));
            //}
            //else
            //{
            //    ReloadYears();
            //    Year = quality.ProductionDate.Year;
            //    OnPropertyChanged(nameof(Year));
            //}

            //Filter();
            //for (int i = 0; i < _service.GetQualityCount; i++)
            //{
            //    if (quality.Id == _service.Quality[i].Id)
            //    {
            //        DgRowIndex = i;
            //        break;
            //    }
            //}
        }

        internal void ModifiyFields()
        {
//            ModificationForm form = new ModificationForm(ActualQuality);
//            _ = form.ShowDialog();
//
//            if (!form.Cancel)
//            {
//                QualityModel quality = _service.Quality[_selectedIndex];
//                quality.ActiveDataFields = form.Fields;
//                quality.Modified = true;
//                if (_qualityDataMV != null)
//                    _qualityDataMV.RefreshQualityData(quality);
//            }
        }

        internal void Settings()
        {
//            SettingForm form = new SettingForm();
//            _ = form.ShowDialog();
        }

        internal void StatisticOpen()
        {
//            StatisticForm form = new StatisticForm();
//            _ = form.ShowDialog();
        }

        #endregion
    }
}

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
using System;
using System.Data;
using Quality_Control_EF.Forms.AddNew;
using System.Linq;
using Quality_Control_EF.Forms.Modification;

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
        private User _user;
        public QualityControl ActualQuality { private get; set; }
        public QualityControlData ActualControlData { private get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand<CancelEventArgs> OnClosingCommand { get; set; }
        public RelayCommand<TextChangedEventArgs> OnProductNameFilterTextChanged { get; set; }
        public RelayCommand<TextChangedEventArgs> OnProductNumberFilterTextChanged { get; set; }
        public RelayCommand<SelectionChangedEventArgs> OnComboYearSelectionChanged { get; set; }
        public RelayCommand<AddingNewItemEventArgs> OnInitializingNewQualityDataCommand { get; set; }
        public RelayCommand<DataGridCellEditEndingEventArgs> OnCellQualityDataChange { get; set; }


        public QualityMV()
        {
            OnClosingCommand = new RelayCommand<CancelEventArgs>(OnClosingCommandExecuted);
            OnProductNameFilterTextChanged = new RelayCommand<TextChangedEventArgs>(OnProductNameTextChangedFilter);
            OnProductNumberFilterTextChanged = new RelayCommand<TextChangedEventArgs>(OnProductNumberTextChangedFilter);
            OnComboYearSelectionChanged = new RelayCommand<SelectionChangedEventArgs>(OnYearSelectionCommandExecuted);
            OnInitializingNewQualityDataCommand = new RelayCommand<AddingNewItemEventArgs>(OnInitializingNewQualityDataCommandExecuted);
            OnCellQualityDataChange = new RelayCommand<DataGridCellEditEndingEventArgs>(OnCellQualityDataChangeExecuted);
        }

        internal NavigationMV SetNavigationMV //ok
        {
            set => _navigationMV = value;
        }

        internal User SetUser //ok
        {
            set
            {
                _user = value;
                _service.AttachUser(_user);
            }
        }

        public SortableObservableCollection<QualityControl> Quality => _service.Quality; //ok

        public SortableObservableCollection<QualityControlData> QualityData => _service.QualityData; //ok

        public List<string> GetActiveFields => _service.ActiveFields; //ok

        internal bool Modified => _service.Modified; //ok

        #region Events - RelayCommand

        protected void OnPropertyChanged(params string[] names) //ok
        {
            if (PropertyChanged != null)
            {
                foreach (string name in names)
                    PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void OnClosingCommandExecuted(CancelEventArgs e) //ok
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

        private void OnYearSelectionCommandExecuted(SelectionChangedEventArgs e) //ok
        {
            SaveAll();
            _service.ReloadQuality(Year);
            Filter();
        }

        public void OnInitializingNewQualityDataCommandExecuted(AddingNewItemEventArgs e) //ok
        {
            e.NewItem = _service.AddQualityData(ActualQuality); ;
        }

        private void OnCellQualityDataChangeExecuted(DataGridCellEditEndingEventArgs e) //ok
        {
            if (ActualControlData != null)
                ActualControlData.Modified = true;
        }

        #endregion

        #region Filtering

        public string ProductName { get; set; } = ""; //ok

        public string ProductNumber { get; set; } = ""; //ok

        public void OnProductNameTextChangedFilter(TextChangedEventArgs e) //ok
        {
            Filter();
        }

        public void OnProductNumberTextChangedFilter(TextChangedEventArgs e) //ok
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

        public int DgRowIndex //ok
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;

                if (value >= 0 && _service.GetQualityCount != 0 && value < _service.GetQualityCount)
                {
                    QualityControl quality = _service.Quality[_selectedIndex];
                    _service.RefreshQualityData(quality);
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
                    nameof(GetActiveFields),
                    nameof(QualityData));

                Refresh();
            }
        }

        public int GetRowCount => _service.GetQualityCount; //ok

        public void Refresh() //ok
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

        internal void SaveAll() //ok
        {
            if (!Modified)
                return;

            if (_service.Save())
                OnPropertyChanged(nameof(Years));
        }

        internal void DeleteAll() //ok
        {
            if (ActualQuality == null) return;
            _service.Delete(ActualQuality);
        }

        internal void DeleteData() //ok
        {
            if (ActualControlData == null || ActualQuality == null)
            {
                return;
            }
            else
            {
                _service.DeleteData(ActualControlData, ActualQuality);
            }
        }

        internal void AddNew() //ok
        {
            AddNewQualityForm form = new AddNewQualityForm();
            _ = form.ShowDialog();

            if (form.Cancel) return;

            QualityControl quality = _service.AddNewQuality(form.Product, form.Number, form.ProductionDate);

            if (quality.ProductionDate.Year == Year)
            {
                _service.AddNewQualityToList(quality);
            }
            else if (quality.ProductionDate.Year != Year && Years.Contains(quality.ProductionDate.Year))
            {
                Year = quality.ProductionDate.Year;
                OnPropertyChanged(nameof(Year));
            }
            else
            {
                _service.ReloadYears();
                Year = quality.ProductionDate.Year;
                OnPropertyChanged(nameof(Years), nameof(Year));
            }

            Filter();
            DgRowIndex = Quality
                .Where(x => x.Id == quality.Id)
                .Select(Quality.IndexOf)
                .FirstOrDefault();
        }

        internal void ModifiyFields() //ok
        {
            ModificationForm form = new ModificationForm(ActualQuality);
            _ = form.ShowDialog();

            if (!form.Cancel)
            {
                Quality[_selectedIndex].ActiveFields = form.Fields;
                Quality[_selectedIndex].Modified = true;
                _service.RefreshQualityData(ActualQuality);
                OnPropertyChanged(nameof(GetActiveFields));
            }
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

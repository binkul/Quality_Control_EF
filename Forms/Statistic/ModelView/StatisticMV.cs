using GalaSoft.MvvmLight.Command;
using Quality_Control_EF.Forms.Navigation;
using Quality_Control_EF.Forms.Statistic.Command;
using Quality_Control_EF.Models;
using Quality_Control_EF.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Quality_Control_EF.Forms.Statistic.ModelView
{
    internal class StatisticMV : INotifyPropertyChanged, INavigation
    {
        private ICommand _todayButton;
        private ICommand _productButton;
        private ICommand _rangeButton;

        private readonly double _startLeftPosition = 32;
        private readonly ProductService _service;
        private readonly LabBookContext _contex;
        private NavigationMV _navigationMV;
        private int _selectedIndex;
        public event PropertyChangedEventHandler PropertyChanged;
        public DateTime DateStart { get; set; } = DateTime.Today;
        public DateTime DateEnd { get; set; } = DateTime.Today;
        public RelayCommand<TextChangedEventArgs> OnProductNameFilterTextChanged { get; set; }
        public RelayCommand<TextChangedEventArgs> OnProductIndexFilterTextChanged { get; set; }


        public StatisticMV(LabBookContext contex)
        {
            _contex = contex;
            _service = new ProductService(contex);
            OnProductNameFilterTextChanged = new RelayCommand<TextChangedEventArgs>(OnProductNameTextChangedFilter);
            OnProductIndexFilterTextChanged = new RelayCommand<TextChangedEventArgs>(OnProductIndexTextChangedFilter);
        }

        protected void OnPropertyChanged(params string[] names)
        {
            if (PropertyChanged != null)
            {
                foreach (string name in names)
                    PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        internal NavigationMV SetNavigationMV
        {
            set => _navigationMV = value;
        }

        public List<Product> Products => _service.FilteredProducts;

        public Thickness TxtIndexLeftPosition => new Thickness(_startLeftPosition, 0, 0, 5);

        public bool RangeDateEnable => DateEnd >= DateStart;


        #region Navigation

        public int DgRowIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                OnPropertyChanged(nameof(DgRowIndex));
                Refresh();
            }
        }

        public int GetRowCount => _service.FilteredProducts.Count;

        public void Refresh()
        {
            if (_navigationMV != null)
                _navigationMV.Refresh();
        }

        #endregion

        #region Filtering

        public string FilterNameTxt { get; set; } = "";

        public string IndexFilterTxt { get; set; } = "";

        public void OnProductNameTextChangedFilter(TextChangedEventArgs e)
        {
            _service.Filter(IndexFilterTxt, FilterNameTxt);
            OnPropertyChanged(nameof(Products));
            Refresh();
        }

        public void OnProductIndexTextChangedFilter(TextChangedEventArgs e)
        {
            _service.Filter(IndexFilterTxt, FilterNameTxt);
            OnPropertyChanged(nameof(Products));
            Refresh();
        }

        #endregion

        public ICommand TodayButton
        {
            get
            {
                if (_todayButton == null) _todayButton = new TodayButton(this);
                return _todayButton;
            }
        }

        public ICommand ProductButton
        {
            get
            {
                if (_productButton == null) _productButton = new ProductButton(this);
                return _productButton;
            }
        }

        public ICommand RangeButton
        {
            get
            {
                if (_rangeButton == null) _rangeButton = new RangeButton(this);
                return _rangeButton;
            }
        }

        internal void ShowToday()
        {
            StatisticTodayForm form = new StatisticTodayForm(_contex);
            _ = form.ShowDialog();
        }

        internal void ShowProduct()
        {
        }

        internal void ShowRange()
        {
            //string dateStart = DateStart.ToShortDateString();
            //string dateEnd = DateEnd.ToShortDateString();
            //StatisticDto range = new StatisticDto("Wyniki dla zakresu: " + dateStart + " do " + dateEnd, DateStart, DateEnd, StatisticType.Range);

            //StatisticRangeForm form = new StatisticRangeForm(range);
            //_ = form.ShowDialog();
        }

    }
}

using GalaSoft.MvvmLight.Command;
using Quality_Control_EF.Forms.Navigation;
using Quality_Control_EF.Models;
using Quality_Control_EF.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Quality_Control_EF.Forms.AddNew.ModelView
{
    public class AddNewQualityMV : INotifyPropertyChanged, INavigation
    {
        private readonly double _startLeftPosition = 32;
        private ProductService _service = new ProductService();
        private int _selectedIndex;
        private NavigationMV _navigationMV;

        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand<TextChangedEventArgs> OnProductNameFilterTextChanged { get; set; }
        public RelayCommand<TextChangedEventArgs> OnProductIndexFilterTextChanged { get; set; }

        public AddNewQualityMV()
        {
            OnProductNameFilterTextChanged = new RelayCommand<TextChangedEventArgs>(OnProductNameTextChangedFilter);
            OnProductIndexFilterTextChanged = new RelayCommand<TextChangedEventArgs>(OnProductIndexTextChangedFilter);
        }

        internal NavigationMV SetNavigationMV
        {
            set => _navigationMV = value;
        }

        public Thickness TxtIndexLeftPosition => new Thickness(_startLeftPosition, 0, 0, 5);

        public List<Product> Products => _service?.FilteredProducts;

        public Product ActualProduct { get; set; }

        public string ProductNumber { get; set; }

        public string ProductIndex => ActualProduct != null ? ActualProduct.HpIndex : "";

        public string ProductName => ActualProduct != null ? ActualProduct.Name : "";

        public long ProductLabBookId => ActualProduct != null ? ActualProduct.LabbookId : 0;

        public long ProductType => ActualProduct != null ? ActualProduct.ProductTypeId : 1;

        public DateTime ProductionDate { get; set; } = DateTime.Today;

        internal bool IsNumberCorrect()
        {
            return _service.ExistByNumberAndYear(ActualProduct);
        }

        protected void OnPropertyChanged(params string[] names)
        {
            if (PropertyChanged != null)
            {
                foreach (string name in names)
                    PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

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

    }
}

using Microsoft.EntityFrameworkCore;
using Quality_Control_EF.Commons;
using Quality_Control_EF.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Quality_Control_EF.Service
{
    public class ProductService
    {
        private readonly LabBookContext _contex;
        private List<Product> Products { get; }
        public List<Product> FilteredProducts { get; set; }
        private List<QualityControlFields> _fields;

        public ProductService(LabBookContext contex)
        {
            _contex = contex;
            Products = GetProducts();
            FilteredProducts = Products;
        }

        private List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();

            products = _contex.Product
                    .Where(x => x.IsArchive == false)
                    .Where(x => x.IsExperimetPhase == false)
                    .OrderBy(x => x.Name)
                    .ToList();

            _fields = _contex.QualityControlFields.ToList();

            foreach (Product product in products)
            {
                string field = _fields
                    .Where(x => x.LabbookId == product.LabbookId)
                    .Select(x => x.ActiveFields)
                    .FirstOrDefault();

                product.ActiveFields = !string.IsNullOrEmpty(field) ? field : DefaultData.DefaultDataFields;
            }

            return products;
        }

        public bool Modified => Products.Any(x => x.Modified);

        internal bool ExistByNumberAndYear(int number, int year)
        {
            return _contex.QualityControl.Any(x => x.Number == number && x.ProductionDate.Year == year);
        }

        public void Filter(string index, string name)
        {
            if (!string.IsNullOrEmpty(index) || !string.IsNullOrEmpty(name))
            {
                List<Product> result = Products
                    .Where(x => x.HpIndex != null)
                    .Where(x => x.HpIndex.ToLower().Contains(index))
                    .Where(x => x.Name.ToLower().Contains(name))
                    .OrderBy(x => x.Name)
                    .ToList();
                FilteredProducts = result;
            }
            else
            {
                FilteredProducts = Products;
            }
        }

        public void Filter(int number, string name)
        {
            if (number > 0 || !string.IsNullOrEmpty(name))
            {
                List<Product> result = Products
                    .Where(x => x.LabbookId >= number)
                    .Where(x => x.Name.ToLower().Contains(name))
                    .OrderBy(x => x.Name)
                    .ToList();
                FilteredProducts = result;
            }
            else
            {
                FilteredProducts = Products;
            }
        }

        internal void Save()
        {
            try
            {
                _ = _contex.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                _ = MessageBox.Show("Błąd 'DbUpdateConcurrencyException' w czasie zapisu danych do tabel QualityControl i QualityControlData: " + e, "Błąd zapisu",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (DbUpdateException e)
            {
                _ = MessageBox.Show("Błąd 'DbUpdateException' w czasie zapisu danych do tabel QualityControl i QualityControlData: " + e, "Błąd zapisu",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            foreach (Product product in Products)
            {
                product.Modified = false;
            }
        }

        internal void SaveFields()
        {
            if (!Modified) return;

            IEnumerable<Product> list = Products
                .Where(x => x.Modified);

            foreach(Product product in list)
            {

                QualityControlFields field = _fields.FirstOrDefault(x => x.LabbookId == product.LabbookId);
                if (field != null) //update
                {
                    field.ActiveFields = product.ActiveFields;
                }
                else //add new
                {
                    QualityControlFields newField = new QualityControlFields()
                    {
                        ActiveFields = product.ActiveFields,
                        LabbookId = product.LabbookId
                    };
                    _ = _contex.QualityControlFields.Add(newField);
                }
            }

            Save();
        }
    }
}

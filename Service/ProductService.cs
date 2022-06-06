using Quality_Control_EF.Models;
using System.Collections.Generic;
using System.Linq;

namespace Quality_Control_EF.Service
{
    public class ProductService
    {
        private List<Product> Products { get; }
        public List<Product> FilteredProducts { get; set; }

        public ProductService()
        {
            Products = GetProducts();
            FilteredProducts = Products;
        }

        private List<Product> GetProducts()
        {
            using (LabBookContext contex = new LabBookContext())
            {
                return contex.Product
                    .Where(x => x.IsArchive == false)
                    .Where(x => x.IsExperimetPhase == false)
                    .OrderBy(x => x.Name)
                    .ToList();
            }
        }

        internal bool ExistByNumberAndYear(int number, int year)
        {
            using (LabBookContext contex = new LabBookContext())
            {
                return contex.QualityControl.Any(x => x.Number == number && x.ProductionDate.Year == year);
            }
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

    }
}

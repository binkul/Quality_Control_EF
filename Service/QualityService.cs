using Microsoft.EntityFrameworkCore;
using Quality_Control_EF.Commons;
using Quality_Control_EF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Quality_Control_EF.Service
{
    public class QualityService
    {
        public static readonly List<string> DefaultFields = new List<string>() { "measure_date", "temp", "density", "pH", "vis_1", "vis_5", "vis_20", "disc", "comments" };

        private readonly LabBookContext _contex = new LabBookContext();
        public SortableObservableCollection<QualityControl> FullQuality { get; private set; }
        public SortableObservableCollection<QualityControl> Quality { get; private set; }
        public SortableObservableCollection<QualityControlData> QualityData { get; set; }
        
        
        public QualityService()
        {
            ReloadQuality(DateTime.Today.Year);
            QualityData = new SortableObservableCollection<QualityControlData>();
            Years = GetAllYears();
        }

        public bool ModifiedQuality => Quality.Any(x => x.Modified);

        public bool ModifiedControlData => QualityData.Any(x => x.Modified);

        public bool Modified => ModifiedQuality || ModifiedControlData;

        public List<int> Years { get; private set; }

        public int Year { get; set; } = DateTime.Today.Year;

        public int GetQualityCount => Quality.Count;

        public void ReloadYears() //ok
        {
            int tmpYear = Year;
            Years = GetAllYears();
            Year = Years.Contains(tmpYear) ? tmpYear : Years.Count > 0 ? Years[Years.Count - 1] : -1;
        }

        public void ReloadQuality(int year) //ok
        {
            FullQuality = GetAllQuality(year);
            Quality = FullQuality;
        }

        public void AddQuality(QualityControl quality)
        {
            //FullQuality.Add(quality);
            //FullQuality.Sort(x => x.Number, ListSortDirection.Ascending);
        }

        internal QualityControlData AddQualityData(QualityControl quality) //ok
        {
            QualityControlData data = new QualityControlData
            {
                MeasureDate = DateTime.Today,
                QualityControl = quality,
                Modified = true
            };

            _ = _contex.Add<QualityControlData>(data);

            return data;
        }


        public SortableObservableCollection<QualityControl> GetAllQuality(int year) //ok
        {
            List<QualityControl> tmpList = _contex.QualityControl
                .Where(x => x.ProductionDate.Year == year)
                .Include(x => x.QualityControlData)
                .OrderBy(x => x.Number)
                .ToList();

            SortableObservableCollection<QualityControl> list = new SortableObservableCollection<QualityControl>(tmpList);
            return list;
        }

        public List<int> GetAllYears() //ok
        {
            return _contex.QualityControl
                .Select(x => x.ProductionDate.Year)
                .Distinct()
                .OrderBy(x => x)
                .ToList();
        }

        public List<string> ActiveFields { get; } = new List<string>(DefaultFields); //ok

        private List<string> GetActiveFields(QualityControl quality) //ok
        {
            List<string> result = new List<string>();
            string fields = quality.ActiveFields;

            if (!string.IsNullOrEmpty(fields))
            {
                string[] tmp = fields.Split('|');
                result.AddRange(tmp);
            }
            else
                result = DefaultFields;

            return result;
        }

        public void RefreshQualityData(QualityControl quality) //ok
        {
            if (Modified)
                _ = Save();

            if (quality != null)
            {
                QualityData = new SortableObservableCollection<QualityControlData>(quality.QualityControlData);
                QualityData.Sort(x => x.MeasureDate, ListSortDirection.Ascending);
                ActiveFields.Clear();
                ActiveFields.AddRange(GetActiveFields(quality));
            }
            else
            {
                QualityData = new SortableObservableCollection<QualityControlData>();
                ActiveFields.Clear();
                ActiveFields.AddRange(DefaultFields);
            }
        }




        public QualityControl SaveNewQuality(QualityControl quality)
        {
            //string fields = _repository.GetActiveFieldsByLabBookId(quality.LabBookId);
            //quality.ActiveDataFields = !string.IsNullOrEmpty(fields) ? fields : DefaultData.DefaultDataFields;
            //QualityModel result = _repository.Save(quality);
            //return result;

            return null;
        }

        internal void Filter(string ProductNumber, string ProductName) //ok
        {
            if (!string.IsNullOrEmpty(ProductName) || !string.IsNullOrEmpty(ProductNumber))
            {

                int number = ProductNumber.Length > 0 ? Convert.ToInt32(ProductNumber) : -1;

                List<QualityControl> result = FullQuality
                    .Where(x => x.ProductName.ToLower().Contains(ProductName))
                    .Where(x => x.Number >= number)
                    .OrderBy(x => x.Number)
                    .ToList();

                SortableObservableCollection<QualityControl> newQuality = new SortableObservableCollection<QualityControl>(result);

                Quality = newQuality;
            }
            else
            {
                Quality = FullQuality;
            }
        }

        internal void Delete(QualityControl quality) //ok
        {
            if (MessageBox.Show("Czy na pewno usunąć produkcję P" + quality.Number + " '" + quality.ProductName + "' z listy?", "Usuwanie",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _ = FullQuality.Remove(quality);
                _ = Quality.Remove(quality);
                _ = _contex.QualityControl.Remove(quality);
                _ = _contex.SaveChanges();
            }
        }

        internal void DeleteData(QualityControlData qualityControlData, QualityControl qualityControl) //ok
        {
            MessageBoxResult response = MessageBox.Show("Czy usunąć wybrany pomiar z dnia: '" + qualityControlData.MeasureDate.ToShortDateString() + "'", "Usuwanie",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            try
            {
                if (response == MessageBoxResult.Yes)
                {
                    _ = QualityData.Remove(qualityControlData);
                    _ = _contex.QualityControlData.Remove(qualityControlData);
                    _ = _contex.SaveChanges();
                }

                _ = qualityControl.QualityControlData.Remove(qualityControlData);
            }
            catch (Exception e)
            {
                _ = MessageBox.Show("Błąd w czasie usuwania rekordu: '" + e.Message + "'", "Błąd usuwania",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        internal bool Save() //ok
        {
            bool reload = false;

            try
            {
                _ = _contex.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                _ = MessageBox.Show("Błąd 'DbUpdateConcurrencyException' w czasie zapisu danych do tabel QualityControl i QualityControlData: " + e, "Błąd zapisu",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (DbUpdateException e)
            {
                _ = MessageBox.Show("Błąd 'DbUpdateException' w czasie zapisu danych do tabel QualityControl i QualityControlData: " + e, "Błąd zapisu",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            //List<QualityControl> modified = _contex.ChangeTracker.Entries()
            //    .Where(x => x.Entity.GetType().Name.Equals(nameof(QualityControl)))
            //    .Where(x => x.State == EntityState.Modified)
            //    .Select(x => (QualityControl)x.Entity)
            //    .ToList();

            var qualities = FullQuality.Where(x => x.Modified).ToList();
            foreach (QualityControl quality in qualities)
            {
                quality.Modified = false;
                if (CheckQualityYear(quality))
                    reload = true;
            }

            var dates = QualityData.Where(x => x.Modified).ToList();
            foreach(QualityControlData data in dates)
            {
                data.Modified = false;
            }

            return reload;
        }

        private bool CheckQualityYear(QualityControl quality) //ok
        {
            bool result = false;

            if (quality.ProductionDate.Year != Year)
            {
                _ = FullQuality.Remove(quality);
                _ = Quality.Remove(quality);
                if (FullQuality.Count == 0 || !Years.Contains(quality.ProductionDate.Year))
                    result = true;
            }

            return result;
        }
    }
}

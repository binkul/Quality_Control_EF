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

        public List<int> Years { get; private set; }

        public int Year { get; set; } = DateTime.Today.Year;

        public int GetQualityCount => Quality.Count;

        public void ReloadYears()
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

        public List<string> ActiveFields { get; } = new List<string>(DefaultFields);

        public List<string> GetActiveFields(QualityControl quality) //ok
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

        public void RefreshQualityData(QualityControl quality) //almost ok
        {
            //Save(quality.Id);
            if (quality != null)
            {
                QualityData.Clear();
                foreach (QualityControlData data in quality.QualityControlData)
                {
                    QualityData.Add(data);
                }
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

        public void Filter(string ProductNumber, string ProductName)
        {
            //if (!string.IsNullOrEmpty(ProductName) || !string.IsNullOrEmpty(ProductNumber))
            //{

            //    int number = ProductNumber.Length > 0 ? Convert.ToInt32(ProductNumber) : -1;

            //    List<QualityModel> result = FullQuality
            //        .Where(x => x.ProductName.ToLower().Contains(ProductName))
            //        .Where(x => x.Number >= number)
            //        .ToList();

            //    SortableObservableCollection<QualityModel> newQuality = new SortableObservableCollection<QualityModel>();
            //    foreach (QualityModel model in result)
            //    {
            //        newQuality.Add(model);
            //    }

            //    Quality = newQuality;
            //}
            //else
            //{
            //    Quality = FullQuality;
            //}
        }

        public void Delete(QualityControl quality)
        {
            //long id = quality.Id;
            //QualityDataRepository qualityDataRepository = new QualityDataRepository();

            //bool result = false;
            //if (MessageBox.Show("Czy na pewno usunąć produkcję P" + quality.Number + " '" + quality.ProductName + "' z listy?", "Usuwanie",
            //    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            //{
            //    result = _repository.DeleteQualityById(id) && qualityDataRepository.DeleteQualityDataByQualityId(id);
            //}

            //if (result)
            //{
            //    QualityModel delQuality = FullQuality.First(x => x.Id == id);
            //    _ = FullQuality.Remove(delQuality);
            //    _ = Quality.Remove(delQuality);
            //}
        }

        public bool Update()
        {
            bool reload = false;

            //List<QualityModel> qualities = FullQuality.Where(x => x.Modified).ToList();
            //foreach (QualityModel quality in qualities)
            //{
            //    if (_repository.Update(quality))
            //    {
            //        quality.Modified = false;
            //        if (CheckQualityYear(quality)) reload = true;
            //    }
            //}
            return reload;
        }

        private bool CheckQualityYear(QualityControl quality)
        {
            bool result = false;

            //if (quality.ProductionDate.Year != Year)
            //{
            //    _ = FullQuality.Remove(quality);
            //    _ = Quality.Remove(quality);
            //    if (FullQuality.Count == 0 || !Years.Contains(quality.ProductionDate.Year)) result = true;
            //}

            return result;
        }
    }
}

﻿using Quality_Control_EF.Commons;
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
        public SortableObservableCollection<QualityControl> FullQuality { get; private set; }
        public SortableObservableCollection<QualityControl> Quality { get; private set; }

        public QualityService()
        {
            ReloadQuality(DateTime.Today.Year);
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

        public void ReloadQuality(int year)
        {
            FullQuality = GetAllQuality(year);
            Quality = FullQuality;
        }

        public void AddQuality(QualityControl quality)
        {
            //FullQuality.Add(quality);
            //FullQuality.Sort(x => x.Number, ListSortDirection.Ascending);
        }

        public SortableObservableCollection<QualityControl> GetAllQuality(int year)
        {
            LabBookContext contex = new LabBookContext();
            List<QualityControl> tmpList = contex.QualityControl
                .Where(x => x.ProductionDate.Year == year)
                .OrderBy(x => x.Number)
                .ToList();

            SortableObservableCollection<QualityControl> list = new SortableObservableCollection<QualityControl>(tmpList);
            return list;
        }

        public List<int> GetAllYears()
        {
            LabBookContext contex = new LabBookContext();

            return contex.QualityControl
                .Select(x => x.ProductionDate.Year)
                .Distinct()
                .OrderBy(x => x)
                .ToList();
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

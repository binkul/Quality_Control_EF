using Quality_Control_EF.Models;
using Quality_Control_EF.Service;
using System;

namespace Quality_Control_EF.Forms.Statistic.Model
{
    public class StatisticDto
    {
        public string Title { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public StatisticType Type { get; set; }
        public Product Product { get; set; }


        public StatisticDto()
        {
        }

        public StatisticDto(string title, DateTime dateStart, DateTime dateEnd, Product product, StatisticType type)
        {
            Title = title;
            DateStart = dateStart;
            DateEnd = dateEnd;
            Product = product;
            Type = type;
        }
    }
}

using Quality_Control_EF.Forms.Statistic.Model;
using Quality_Control_EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quality_Control_EF.Service
{
    public enum StatisticType
    {
        Today,
        Range,
        Product
    }

    internal class StatisticService
    {
        private readonly LabBookContext _contex;
        private readonly StatisticDto _statistic;

        public StatisticService(LabBookContext contex, StatisticDto statistic)
        {
            _contex = contex;
            _statistic = statistic;
        }
    }
}

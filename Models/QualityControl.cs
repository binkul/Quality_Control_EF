using System;
using System.Collections.Generic;
using System.ComponentModel;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Quality_Control_EF.Models
{
    public partial class QualityControl : INotifyPropertyChanged
    {
        public QualityControl()
        {
            QualityControlData = new HashSet<QualityControlData>();
        }

        public long Id { get; set; }
        public DateTime ProductionDate { get; set; }
        public int Number { get; set; }
        public string YearNumber => ProductionDate.Year.ToString().Substring(2, 2) + "/" + Number.ToString();
        public string ProductName { get; set; }
        public long? LabbookId { get; set; }
        public long ProductTypeId { get; set; }
        public string Remarks { get; set; }
        public string ActiveFields { get; set; }
        public string ProductIndex { get; set; }

        public bool Modified { get; set; } = false;

        public long LoginId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<QualityControlData> QualityControlData { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Quality_Control_EF.Models
{
    public partial class Users
    {
        public Users()
        {
            Products = new HashSet<Product>();
            QualityControls = new HashSet<QualityControl>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EMail { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Permission { get; set; }
        public string Identifier { get; set; }
        public bool? Active { get; set; }
        public DateTime Date { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<QualityControl> QualityControls { get; set; }
    }
}

using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Quality_Control_EF.Models
{
    public partial class QualityControlFields
    {
        public long Id { get; set; }
        public long LabbookId { get; set; }
        public string ActiveFields { get; set; }
    }
}

using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Quality_Control_EF.Models
{
    public partial class QualityControlData
    {
        public long Id { get; set; }
        public long QualityId { get; set; }
        public DateTime MeasureDate { get; set; }
        public double? Density { get; set; }
        public double? PH { get; set; }
        public string Temp { get; set; }
        public double? Vis1 { get; set; }
        public double? Vis2 { get; set; }
        public double? Vis5 { get; set; }
        public double? Vis10 { get; set; }
        public double? Vis15 { get; set; }
        public double? Vis20 { get; set; }
        public double? Vis25 { get; set; }
        public double? Vis30 { get; set; }
        public double? Vis35 { get; set; }
        public double? Vis40 { get; set; }
        public double? Vis45 { get; set; }
        public double? Vis50 { get; set; }
        public double? Vis55 { get; set; }
        public double? Vis60 { get; set; }
        public double? Vis65 { get; set; }
        public double? Vis70 { get; set; }
        public double? Vis75 { get; set; }
        public double? Vis80 { get; set; }
        public double? Vis85 { get; set; }
        public double? Vis90 { get; set; }
        public double? Vis95 { get; set; }
        public double? Vis100 { get; set; }
        public string Disc { get; set; }
        public string ViscRemarks { get; set; }
        public double? LM { get; set; }
        public double? AM { get; set; }
        public double? BM { get; set; }
        public double? WiM { get; set; }
        public double? YiM { get; set; }
        public double? LS { get; set; }
        public double? AS { get; set; }
        public double? BS { get; set; }
        public double? WiS { get; set; }
        public double? YiS { get; set; }
        public double? De { get; set; }
        public string SpectroRemarks { get; set; }
        public double? Gloss20 { get; set; }
        public double? Gloss60 { get; set; }
        public double? Gloss85 { get; set; }
        public string GlossRemarks { get; set; }
        public string DryingI { get; set; }
        public string DryingIi { get; set; }
        public string DryingIii { get; set; }
        public string DryingIv { get; set; }
        public string DryingV { get; set; }
        public string DryingVi { get; set; }
        public string DryingVii { get; set; }
        public string DryingRemarks { get; set; }
        public string ScrubingBrush { get; set; }
        public string ScrubingSponge { get; set; }
        public string ScrubingRemarks { get; set; }
        public string Flow { get; set; }
        public string Runoff { get; set; }
        public string Adhesion { get; set; }
        public double? Contrast75 { get; set; }
        public double? Contrast100 { get; set; }
        public double? Contrast125 { get; set; }
        public double? Contrast150 { get; set; }
        public double? Contrast200 { get; set; }
        public double? Contrast240 { get; set; }
        public double? Contrast250 { get; set; }
        public double? Contrast300 { get; set; }
        public double? ContrastWire100 { get; set; }
        public double? ContrastWire125 { get; set; }
        public double? ContrastWire150 { get; set; }
        public double? ContrastWire200 { get; set; }
        public double? ContrastWire250 { get; set; }
        public string ContrastClass { get; set; }
        public string ContrastRemarks { get; set; }
        public double? FSolids { get; set; }
        public double? F450 { get; set; }
        public double? F900 { get; set; }
        public double? FOrganic { get; set; }
        public double? FLime { get; set; }
        public double? FTalcum { get; set; }
        public double? FTitanium { get; set; }
        public string Voc { get; set; }
        public string FRemarks { get; set; }
        public string Comments { get; set; }

        public virtual QualityControl QualityControl { get; set; }
    }
}

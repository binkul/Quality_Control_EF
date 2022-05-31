using System;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Quality_Control_EF.Models
{
    public partial class LabBookContext : DbContext
    {
        public LabBookContext()
        {
        }

        public LabBookContext(DbContextOptions<LabBookContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<QualityControl> QualityControl { get; set; }
        public virtual DbSet<QualityControlData> QualityControlData { get; set; }
        public virtual DbSet<QualityControlFields> QualityControlFields { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=LAPTOP-S66RS9QE\\SQLEXPRESS_2012;Database=LabBook;Trusted_Connection=True;");
                _ = optionsBuilder.UseSqlServer(Application.Current.FindResource("ConnectionString").ToString());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(2000);

                entity.Property(e => e.HpIndex)
                    .HasColumnName("hp_index")
                    .HasMaxLength(50);

                entity.Property(e => e.IsArchive)
                    .IsRequired()
                    .HasColumnName("is_archive")
                    .HasDefaultValueSql("('false')");

                entity.Property(e => e.IsDanger)
                    .IsRequired()
                    .HasColumnName("is_danger")
                    .HasDefaultValueSql("('false')");

                entity.Property(e => e.IsExperimetPhase)
                    .IsRequired()
                    .HasColumnName("is_experimetPhase")
                    .HasDefaultValueSql("('false')");

                entity.Property(e => e.LabbookId).HasColumnName("labbook_id");

                entity.Property(e => e.LoginId)
                    .HasColumnName("login_id")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(500);

                entity.Property(e => e.ProductGlossId)
                    .HasColumnName("product_gloss_id")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ProductPriceId)
                    .HasColumnName("product_price_id")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ProductTypeId)
                    .HasColumnName("product_type_id")
                    .HasDefaultValueSql("((1))");

                entity
                .HasOne(d => d.User)
                .WithMany(p => p.Products)
                .HasForeignKey(d => d.LoginId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });


            modelBuilder.Entity<QualityControl>(entity =>
            {
                entity.Ignore(e => e.YearNumber);
                entity.Ignore(e => e.Modified);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.ActiveFields)
                    .HasColumnName("active_fields")
                    .HasMaxLength(1000);

                entity.Property(e => e.LabbookId).HasColumnName("labbook_id");
                entity.Property(e => e.LoginId)
                    .HasColumnName("login_id")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Number).HasColumnName("number");
                entity.Property(e => e.ProductIndex)
                    .HasColumnName("product_index")
                    .HasMaxLength(20);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasColumnName("product_name")
                    .HasMaxLength(500)
                    .HasDefaultValueSql("('Pusty')");

                entity.Property(e => e.ProductTypeId)
                    .HasColumnName("product_type_id")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ProductionDate)
                    .HasColumnName("production_date")
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Remarks)
                    .HasColumnName("remarks")
                    .HasMaxLength(500);

                entity
                .HasOne(p => p.User)
                .WithMany(d => d.QualityControls)
                .HasForeignKey(d => d.LoginId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<QualityControlData>(entity =>
            {
                entity.Ignore(e => e.DayDistance);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.AM).HasColumnName("a_m");
                entity.Property(e => e.AS).HasColumnName("a_s");
                entity.Property(e => e.Adhesion)
                    .HasColumnName("adhesion")
                    .HasMaxLength(200);

                entity.Property(e => e.BM).HasColumnName("b_m");
                entity.Property(e => e.BS).HasColumnName("b_s");
                entity.Property(e => e.Comments)
                    .HasColumnName("comments")
                    .HasMaxLength(500);

                entity.Property(e => e.Contrast100).HasColumnName("contrast_100");
                entity.Property(e => e.Contrast125).HasColumnName("contrast_125");
                entity.Property(e => e.Contrast150).HasColumnName("contrast_150");
                entity.Property(e => e.Contrast200).HasColumnName("contrast_200");
                entity.Property(e => e.Contrast240).HasColumnName("contrast_240");
                entity.Property(e => e.Contrast250).HasColumnName("contrast_250");
                entity.Property(e => e.Contrast300).HasColumnName("contrast_300");
                entity.Property(e => e.Contrast75).HasColumnName("contrast_75");
                entity.Property(e => e.ContrastClass)
                    .HasColumnName("contrast_class")
                    .HasMaxLength(20);

                entity.Property(e => e.ContrastRemarks)
                    .HasColumnName("contrast_remarks")
                    .HasMaxLength(200);

                entity.Property(e => e.ContrastWire100).HasColumnName("contrast_wire_100");
                entity.Property(e => e.ContrastWire125).HasColumnName("contrast_wire_125");
                entity.Property(e => e.ContrastWire150).HasColumnName("contrast_wire_150");
                entity.Property(e => e.ContrastWire200).HasColumnName("contrast_wire_200");
                entity.Property(e => e.ContrastWire250).HasColumnName("contrast_wire_250");
                entity.Property(e => e.De).HasColumnName("DE");
                entity.Property(e => e.Density).HasColumnName("density");
                entity.Property(e => e.Disc)
                    .HasColumnName("disc")
                    .HasMaxLength(20);

                entity.Property(e => e.DryingI)
                    .HasColumnName("drying_I")
                    .HasMaxLength(20);

                entity.Property(e => e.DryingIi)
                    .HasColumnName("drying_II")
                    .HasMaxLength(20);

                entity.Property(e => e.DryingIii)
                    .HasColumnName("drying_III")
                    .HasMaxLength(20);

                entity.Property(e => e.DryingIv)
                    .HasColumnName("drying_IV")
                    .HasMaxLength(20);

                entity.Property(e => e.DryingRemarks)
                    .HasColumnName("drying_remarks")
                    .HasMaxLength(200);

                entity.Property(e => e.DryingV)
                    .HasColumnName("drying_V")
                    .HasMaxLength(20);

                entity.Property(e => e.DryingVi)
                    .HasColumnName("drying_VI")
                    .HasMaxLength(20);

                entity.Property(e => e.DryingVii)
                    .HasColumnName("drying_VII")
                    .HasMaxLength(20);

                entity.Property(e => e.F450).HasColumnName("f_450");
                entity.Property(e => e.F900).HasColumnName("f_900");
                entity.Property(e => e.FLime).HasColumnName("f_lime");
                entity.Property(e => e.FOrganic).HasColumnName("f_organic");
                entity.Property(e => e.FRemarks)
                    .HasColumnName("f_remarks")
                    .HasMaxLength(200);

                entity.Property(e => e.FSolids).HasColumnName("f_solids");
                entity.Property(e => e.FTalcum).HasColumnName("f_talcum");
                entity.Property(e => e.FTitanium).HasColumnName("f_titanium");

                entity.Property(e => e.Flow)
                    .HasColumnName("flow")
                    .HasMaxLength(50);

                entity.Property(e => e.Gloss20).HasColumnName("gloss_20");
                entity.Property(e => e.Gloss60).HasColumnName("gloss_60");
                entity.Property(e => e.Gloss85).HasColumnName("gloss_85");
                entity.Property(e => e.GlossRemarks)
                    .HasColumnName("gloss_remarks")
                    .HasMaxLength(200);

                entity.Property(e => e.LM).HasColumnName("L_m");
                entity.Property(e => e.LS).HasColumnName("L_s");
                entity.Property(e => e.MeasureDate)
                    .HasColumnName("measure_date")
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PH).HasColumnName("pH");
                entity.Property(e => e.QualityId).HasColumnName("quality_id");
                entity.Property(e => e.Runoff)
                    .HasColumnName("runoff")
                    .HasMaxLength(50);

                entity.Property(e => e.ScrubingBrush)
                    .HasColumnName("scrubing_brush")
                    .HasMaxLength(50);

                entity.Property(e => e.ScrubingRemarks)
                    .HasColumnName("scrubing_remarks")
                    .HasMaxLength(200);

                entity.Property(e => e.ScrubingSponge)
                    .HasColumnName("scrubing_sponge")
                    .HasMaxLength(50);

                entity.Property(e => e.SpectroRemarks)
                    .HasColumnName("spectro_remarks")
                    .HasMaxLength(200);

                entity.Property(e => e.Temp)
                    .HasColumnName("temp")
                    .HasMaxLength(10);

                entity.Property(e => e.Vis1).HasColumnName("vis_1");
                entity.Property(e => e.Vis10).HasColumnName("vis_10");
                entity.Property(e => e.Vis100).HasColumnName("vis_100");
                entity.Property(e => e.Vis15).HasColumnName("vis_15");
                entity.Property(e => e.Vis2).HasColumnName("vis_2");
                entity.Property(e => e.Vis20).HasColumnName("vis_20");
                entity.Property(e => e.Vis25).HasColumnName("vis_25");
                entity.Property(e => e.Vis30).HasColumnName("vis_30");
                entity.Property(e => e.Vis35).HasColumnName("vis_35");
                entity.Property(e => e.Vis40).HasColumnName("vis_40");
                entity.Property(e => e.Vis45).HasColumnName("vis_45");
                entity.Property(e => e.Vis5).HasColumnName("vis_5");
                entity.Property(e => e.Vis50).HasColumnName("vis_50");
                entity.Property(e => e.Vis55).HasColumnName("vis_55");
                entity.Property(e => e.Vis60).HasColumnName("vis_60");
                entity.Property(e => e.Vis65).HasColumnName("vis_65");
                entity.Property(e => e.Vis70).HasColumnName("vis_70");
                entity.Property(e => e.Vis75).HasColumnName("vis_75");
                entity.Property(e => e.Vis80).HasColumnName("vis_80");
                entity.Property(e => e.Vis85).HasColumnName("vis_85");
                entity.Property(e => e.Vis90).HasColumnName("vis_90");
                entity.Property(e => e.Vis95).HasColumnName("vis_95");
                entity.Property(e => e.ViscRemarks)
                    .HasColumnName("visc_remarks")
                    .HasMaxLength(200);

                entity.Property(e => e.Voc)
                    .HasColumnName("VOC")
                    .HasMaxLength(50);

                entity.Property(e => e.WiM).HasColumnName("WI_m");
                entity.Property(e => e.WiS).HasColumnName("WI_s");
                entity.Property(e => e.YiM).HasColumnName("YI_m");
                entity.Property(e => e.YiS).HasColumnName("YI_s");

                entity
                .HasOne(d => d.QualityControl)
                .WithMany(p => p.QualityControlData)
                .HasForeignKey(d => d.QualityId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<QualityControlFields>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActiveFields)
                    .IsRequired()
                    .HasColumnName("active_fields")
                    .HasMaxLength(1000)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LabbookId).HasColumnName("labbook_id");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasColumnName("active")
                    .HasDefaultValueSql("('true')");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EMail)
                    .IsRequired()
                    .HasColumnName("e_mail")
                    .HasMaxLength(50);

                entity.Property(e => e.Identifier)
                    .IsRequired()
                    .HasColumnName("identifier")
                    .HasMaxLength(10)
                    .HasComment("first letter of name and surname eg. JB");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("login")
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(50);

                entity.Property(e => e.Permission)
                    .IsRequired()
                    .HasColumnName("permission")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('user')");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("surname")
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

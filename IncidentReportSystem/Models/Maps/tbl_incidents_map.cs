using System.Data.Entity.ModelConfiguration;
using IncidentReportSystem.Models.Tables;

namespace IncidentReportSystem.Models.Mappings
{
    public class tbl_incidents_map : EntityTypeConfiguration<tbl_incidents_model>
    {
        public tbl_incidents_map()
        {
            // Primary Key
            this.HasKey(t => t.IncidentID);

            // Table Mapping
            this.ToTable("tbl_incidents");

            // Properties
            this.Property(t => t.IncidentID).HasColumnName("IncidentID");
            this.Property(t => t.ResidentID).HasColumnName("ResidentID");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Location).HasColumnName("Location");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.CategoryID).HasColumnName("CategoryID");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
            this.Property(t => t.PriorityID).HasColumnName("PriorityID");
            this.Property(t => t.CreatedAt).HasColumnName("CreatedAt");
            this.Property(t => t.UpdatedAt).HasColumnName("UpdatedAt");
        }
    }
}
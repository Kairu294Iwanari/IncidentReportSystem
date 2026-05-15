using IncidentReportSystem.Models.Mappings;
using IncidentReportSystem.Models.Maps;
using IncidentReportSystem.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IncidentReportSystem.Models.Context
{
    public class IncidentReportContext : DbContext
    {
        static IncidentReportContext()
        {
            Database.SetInitializer < IncidentReportContext>(null);
        }

        public IncidentReportContext() : base("Name=incidentreportsystemdb") { }

        public virtual DbSet<tbl_account_statuses_model> tbl_account_statuses { get; set; }
        public virtual DbSet<tbl_incident_categories_model> tbl_incident_categories { get; set; }
        public virtual DbSet<tbl_incident_statuses_model> tbl_incident_statuses { get; set; }
        public virtual DbSet<tbl_incidents_model> tbl_incidents { get; set; }
        public virtual DbSet<tbl_priority_levels_model> tbl_priority_levels { get; set; }
        public virtual DbSet<tbl_roles_model> tbl_roles { get; set; }
        public virtual DbSet<tbl_users_model> tbl_users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new tbl_account_statuses_map());
            modelBuilder.Configurations.Add(new tbl_incident_categories_map());
            modelBuilder.Configurations.Add(new tbl_incident_statuses_map());
            modelBuilder.Configurations.Add(new tbl_incidents_map());
            modelBuilder.Configurations.Add(new tbl_priority_levels_map());
            modelBuilder.Configurations.Add(new tbl_roles_map());
            modelBuilder.Configurations.Add(new tbl_users_map());
        }

    }
}
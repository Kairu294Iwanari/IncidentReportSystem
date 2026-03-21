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
        public virtual DbSet<tbl_assignments_model> tbl_assignments { get; set; }
        public virtual DbSet<tbl_audit_logs_model> tbl_audit_logs { get; set; }
        public virtual DbSet<tbl_evidence_files_model> tbl_evidence_files { get; set; }
        public virtual DbSet<tbl_incident_categories_model> tbl_incident_categories { get; set; }
        public virtual DbSet<tbl_incident_replies_model> tbl_incident_replies { get; set; }
        public virtual DbSet<tbl_incident_statuses_model> tbl_incident_statuses { get; set; }
        public virtual DbSet<tbl_incidents_model> tbl_incidents { get; set; }
        public virtual DbSet<tbl_notifications_model> tbl_notifications { get; set; }
        public virtual DbSet<tbl_permissions_model> tbl_permissions { get; set; }
        public virtual DbSet<tbl_priority_levels_model> tbl_priority_levels { get; set; }
        public virtual DbSet<tbl_role_permissions_model> tbl_role_permissions { get; set; }
        public virtual DbSet<tbl_roles_model> tbl_roles { get; set; }
        public virtual DbSet<tbl_satisfaction_ratings_model> tbl_satisfaction_ratings { get; set; }
        public virtual DbSet<tbl_users_model> tbl_users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new tbl_account_statuses_map());
            modelBuilder.Configurations.Add(new tbl_assignments_map());
            modelBuilder.Configurations.Add(new tbl_audit_logs_map());
            modelBuilder.Configurations.Add(new tbl_evidence_files_map());
            modelBuilder.Configurations.Add(new tbl_incident_categories_map());
            modelBuilder.Configurations.Add(new tbl_incident_replies_map());
            modelBuilder.Configurations.Add(new tbl_incident_statuses_map());
            modelBuilder.Configurations.Add(new tbl_incidents_map());
            modelBuilder.Configurations.Add(new tbl_notifications_map());
            modelBuilder.Configurations.Add(new tbl_permissions_map());
            modelBuilder.Configurations.Add(new tbl_priority_levels_map());
            modelBuilder.Configurations.Add(new tbl_role_permissions_map());
            modelBuilder.Configurations.Add(new tbl_roles_map());
            modelBuilder.Configurations.Add(new tbl_satisfaction_ratings_map());
            modelBuilder.Configurations.Add(new tbl_users_map());
        }

    }
}
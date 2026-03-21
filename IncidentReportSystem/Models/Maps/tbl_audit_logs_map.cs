using IncidentReportSystem.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace IncidentReportSystem.Models.Maps
{
    public class tbl_audit_logs_map : EntityTypeConfiguration<tbl_audit_logs_model>    
    {
        public tbl_audit_logs_map() 
        {
            HasKey(i => i.LogID);
            ToTable("tbl_audit_logs");
        }
    }
}
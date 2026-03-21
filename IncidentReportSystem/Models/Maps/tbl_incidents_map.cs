using IncidentReportSystem.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace IncidentReportSystem.Models.Maps
{
    public class tbl_incidents_map : EntityTypeConfiguration<tbl_incidents_model>
    {
        public tbl_incidents_map() 
        {
            HasKey(i => i.IncidentID);
            ToTable("tbl_incidents");
        }
    }
}
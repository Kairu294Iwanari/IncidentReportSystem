using IncidentReportSystem.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace IncidentReportSystem.Models.Maps
{
    public class tbl_incident_statuses_map : EntityTypeConfiguration<tbl_incident_statuses_model>
    {
        public tbl_incident_statuses_map()
        {
            HasKey(i => i.StatusID);
            ToTable("tbl_incident_statuses");
        }
    }
}
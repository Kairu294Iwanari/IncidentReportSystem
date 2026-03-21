using IncidentReportSystem.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace IncidentReportSystem.Models.Maps
{
    public class tbl_incident_categories_map : EntityTypeConfiguration<tbl_incident_categories_model>
    {
        public tbl_incident_categories_map() 
        {
            HasKey(i => i.CategoryID);
            ToTable("tbl_incident_categories");
        }
    }
}
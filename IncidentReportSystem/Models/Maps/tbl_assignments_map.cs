using IncidentReportSystem.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace IncidentReportSystem.Models.Maps
{
    public class tbl_assignments_map : EntityTypeConfiguration<tbl_assignments_model>
    {
        public tbl_assignments_map() 
        {
            HasKey(i => i.AssignmentID);
            ToTable("tbl_assignments");
        }
    }
}
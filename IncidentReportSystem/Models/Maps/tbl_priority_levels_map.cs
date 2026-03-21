using IncidentReportSystem.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace IncidentReportSystem.Models.Maps
{
    public class tbl_priority_levels_map : EntityTypeConfiguration<tbl_priority_levels_model>
    {
        public tbl_priority_levels_map() 
        {
            HasKey(i => i.PriorityID);
            ToTable("tbl_priority_levels");
        }
    }
}
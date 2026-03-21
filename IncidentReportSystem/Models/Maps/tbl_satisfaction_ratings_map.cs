using IncidentReportSystem.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace IncidentReportSystem.Models.Maps
{
    public class tbl_satisfaction_ratings_map : EntityTypeConfiguration<tbl_satisfaction_ratings_model>
    {
        public tbl_satisfaction_ratings_map() 
        {
            HasKey(i => i.RatingID);
            ToTable("tbl_satisfaction_ratings");
        }
    }
}
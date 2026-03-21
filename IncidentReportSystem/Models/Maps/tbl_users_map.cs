using IncidentReportSystem.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace IncidentReportSystem.Models.Maps
{
    public class tbl_users_map : EntityTypeConfiguration<tbl_users_model>
    {
        public tbl_users_map() 
        {
            HasKey(i => i.UserID);
            ToTable("tbl_users");
        }
    }
}
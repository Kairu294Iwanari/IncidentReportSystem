using IncidentReportSystem.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace IncidentReportSystem.Models.Maps
{
    public class tbl_roles_map : EntityTypeConfiguration<tbl_roles_model>
    {
        public tbl_roles_map() 
        {
            HasKey(i => i.RoleID);
            ToTable("tbl_roles");
        }
    }
}
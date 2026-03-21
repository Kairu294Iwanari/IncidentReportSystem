using IncidentReportSystem.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace IncidentReportSystem.Models.Maps
{
    public class tbl_role_permissions_map : EntityTypeConfiguration<tbl_role_permissions_model>
    {
        public tbl_role_permissions_map() 
        {
            HasKey(i => i.RolePermissionID);
            ToTable("tbl_role_permissions");
        }
    }
}
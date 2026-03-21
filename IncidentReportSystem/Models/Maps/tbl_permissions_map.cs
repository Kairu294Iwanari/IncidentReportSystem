using IncidentReportSystem.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace IncidentReportSystem.Models.Maps
{
    public class tbl_permissions_map : EntityTypeConfiguration<tbl_permissions_model>
    {
        public tbl_permissions_map() 
        {
            HasKey(i => i.PermissionID);
            ToTable("tbl_permissions");
        }
    }
}
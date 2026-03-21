using IncidentReportSystem.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace IncidentReportSystem.Models.Maps
{
    public class tbl_notifications_map : EntityTypeConfiguration<tbl_notifications_model>
    {
        public tbl_notifications_map() 
        {
            HasKey(i => i.NotificationID);
            ToTable("tbl_notifications");
        }
    }
}
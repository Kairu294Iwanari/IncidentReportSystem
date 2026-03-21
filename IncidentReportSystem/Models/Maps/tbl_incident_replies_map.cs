using IncidentReportSystem.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace IncidentReportSystem.Models.Maps
{
    public class tbl_incident_replies_map : EntityTypeConfiguration<tbl_incident_replies_model>
    {
        public tbl_incident_replies_map() 
        {
            HasKey(i => i.ReplyID);
            ToTable("tbl_incident_replies");
        }
    }
}
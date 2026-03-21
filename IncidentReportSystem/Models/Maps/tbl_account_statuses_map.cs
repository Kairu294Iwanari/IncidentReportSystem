using IncidentReportSystem.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace IncidentReportSystem.Models.Maps
{
    public class tbl_account_statuses_map : EntityTypeConfiguration<tbl_account_statuses_model>
    {
        public tbl_account_statuses_map() 
        {
            HasKey(i => i.AccountStatusID);
            ToTable("tbl_account_statuses");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IncidentReportSystem.Models.Tables
{
    public class tbl_permissions_model
    {
        public int PermissionID { get; set; }
        public string ModuleName { get; set; }
        public string PermissionName { get; set; }
        public string Description { get; set; }

    }
}
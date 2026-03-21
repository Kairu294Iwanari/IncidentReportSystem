using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IncidentReportSystem.Models.Tables
{
    public class tbl_roles_model
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IncidentReportSystem.Models.Tables
{
    public class tbl_audit_logs_model
    {
        public int LogID { get; set; }
        public int UserID { get; set; }
        public string ActionType { get; set; }
        public string Description { get; set; }
        public DateTime ActionAt {  get; set; }

    }
}
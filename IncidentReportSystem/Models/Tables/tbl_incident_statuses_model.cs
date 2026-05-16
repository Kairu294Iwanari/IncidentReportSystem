using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IncidentReportSystem.Models.Tables
{
    public class tbl_incident_statuses_model
    {
        public int StatusID { get; set; }
        public string StatusName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
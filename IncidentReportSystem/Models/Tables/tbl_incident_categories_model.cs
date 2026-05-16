using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IncidentReportSystem.Models.Tables
{
    public class tbl_incident_categories_model
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IncidentReportSystem.Models.Tables
{
    public class tbl_assignments_model
    {
        public int AssignmentID { get; set; }
        public int IncidentID { get; set; }
        public int OfficialID { get; set; }
        public int AssignedByAdminID { get; set; }
        public DateTime AssignedAt  { get; set; }
    }
}
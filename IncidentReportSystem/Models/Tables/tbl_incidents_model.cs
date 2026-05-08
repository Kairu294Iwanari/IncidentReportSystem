using System;

namespace IncidentReportSystem.Models.Tables
{
    public class tbl_incidents_model
    {
        public int IncidentID { get; set; }
        public int ResidentID { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public int StatusID { get; set; }
        public int PriorityID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
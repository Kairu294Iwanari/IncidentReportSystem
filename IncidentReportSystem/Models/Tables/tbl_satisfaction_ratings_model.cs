using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IncidentReportSystem.Models.Tables
{
    public class tbl_satisfaction_ratings_model
    {
        public int RatingID { get; set; }
        public int IncidentID { get; set; }
        public int ResidentID { get; set; }
        public int RatingScore {  get; set; }
        public string Feedback { get; set; }
        public DateTime RatedAt {  get; set; }

    }
}
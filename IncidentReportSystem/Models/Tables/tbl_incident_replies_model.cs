using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IncidentReportSystem.Models.Tables
{
    public class tbl_incident_replies_model
    {
        public int ReplyID { get; set; }
        public int IncidentID { get; set; }
        public int UserID { get; set; }
        public string ReplyMessage { get; set; }
        public DateTime CreatedAt {  get; set; }
    }
}
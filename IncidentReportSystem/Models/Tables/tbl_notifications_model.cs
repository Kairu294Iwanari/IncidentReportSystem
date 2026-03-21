using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IncidentReportSystem.Models.Tables
{
    public class tbl_notifications_model
    {
        public int NotificationID { get; set; }
        public int UserID { get; set; }
        public int IncidentID { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public int IsRead { get; set; }
        public DateTime SentAt {  get; set; }

    }
}
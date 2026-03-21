using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IncidentReportSystem.Models.Tables
{
    public class tbl_evidence_files_model
    {
        public int EvidenceID { get; set; }
        public int IncidentID { get; set; }
        public int UploadedByUserID { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadedAt { get; set; }

    }
}
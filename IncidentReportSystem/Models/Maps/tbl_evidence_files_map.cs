using IncidentReportSystem.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace IncidentReportSystem.Models.Maps
{
    public class tbl_evidence_files_map : EntityTypeConfiguration<tbl_evidence_files_model>
    {
        public tbl_evidence_files_map()
        {
            HasKey(i => i.EvidenceID);
            ToTable("tbl_evidence_files");
        }
    }
}
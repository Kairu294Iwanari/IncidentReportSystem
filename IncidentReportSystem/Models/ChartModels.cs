using System.Collections.Generic;

namespace IncidentReportSystem.Models
{
    public class ChartModel
    {
        public List<string> labels { get; set; }
        public List<string> series { get; set; }
        public List<List<int>> data { get; set; }
    }

    public class PieChartModel
    {
        public List<string> labels { get; set; }
        public List<int> data { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ProjectWeekReport
    {
        public int ProjectWeekReportId { get; set; }
        public DateTime ReportStartDate { get; set; }
        public DateTime ReportEndDate { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}

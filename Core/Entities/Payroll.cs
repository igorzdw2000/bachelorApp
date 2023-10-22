using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Payroll
    {
        public int PayrollId { get; set; }
        public DateTime PayrollDate { get; set; }
        public double TotalHoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal Earnings { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

    }
}

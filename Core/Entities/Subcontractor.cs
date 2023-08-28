using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Subcontractor
    {
        public int SubcontractorId { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string NIP { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}

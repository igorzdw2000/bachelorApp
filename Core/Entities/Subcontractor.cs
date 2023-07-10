using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
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
        public string Name { get; set; }
        public string NIP { get; set; }
    }
}

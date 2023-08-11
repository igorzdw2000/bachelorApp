using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ProjectMaterial
    {
        public int PId { get; set; }
        public Project Project { get; set; }
        public int MId { get; set; }
        public MaterialSupplier MaterialSupplier { get; set; }
        public decimal Quantity { get; set; }

    }
}

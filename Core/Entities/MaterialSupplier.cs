using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class MaterialSupplier
    {
        public int MaterialSupplierId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public decimal UnitPrice { get; set; }
        public ICollection<ProjectMaterial> ProjectMaterials { get; set; }
    }
}

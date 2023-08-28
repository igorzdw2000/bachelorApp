using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class InvoiceDetail
    {
        [Key]
        public int InvoiceDetailId { get; set; }
        [Key]
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public double PricePerUnit { get; set; }
        public double Value { get; set; }
    }
}

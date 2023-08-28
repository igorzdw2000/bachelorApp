using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Offer
    {
        public int OfferId { get; set; }
        public DateTime OfferDate { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public double TotalEstimatedLaborCost { get; set; }
        public double TotalEstimatedMaterialCost { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}

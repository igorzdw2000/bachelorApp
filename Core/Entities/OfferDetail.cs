using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class OfferDetail
    {
        [Key]
        public int OfferDetailId { get; set; }
        public Offer Offer { get; set; }
        [Key]
        public int OfferId { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public double EstimatedLaborCost { get; set; }
        public double EstimatedMaterialCost { get; set; }
        public DateTime EstimatedTime { get; set; }

    }
}

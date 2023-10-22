using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class OfferDetailDto
    {
        public int OfferId { get; set; }
        public int ServiceId { get; set; }
        public double EstimatedLaborCost { get; set; }
        public double EstimatedMaterialCost { get; set; }
        public DateTime EstimatedTime { get; set; }
    }
}
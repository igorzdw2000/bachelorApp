using Core.Entities;

namespace API.Dtos
{
    public class OfferDto
    {
        public DateTime OfferDate { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public double TotalEstimatedLaborCost { get; set; }
        public double TotalEstimatedMaterialCost { get; set; }
        public int CustomerId { get; set; }
    }
}
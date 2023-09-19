using Core.Entities;

namespace API.Dtos
{
    public class InvoiceAddDto
    {
        public string InvoiceNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime SaleDate { get; set; }
        public DateTime DateOfPayment { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public double TotalToBePaid { get; set; }
        public string Comments { get; set; }
        public int CustomerId { get; set; }
        public int ProjectId { get; set; }
        public InvoiceType InvoiceType { get; set; }
    }
}

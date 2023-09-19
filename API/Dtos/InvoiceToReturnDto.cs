using Core.Entities;

namespace API.Dtos
{
    public class InvoiceToReturnDto
    {
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public double TotalValue { get; set; }
        public string PaymentStatus { get; set; }
        
        public string InvoiceType { get; set; }
        public string? Customer { get; set; }
        public string? Project { get; set; }
    }
}

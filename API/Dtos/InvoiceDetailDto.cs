namespace API.Dtos
{
    public class InvoiceDetailDto
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public double PricePerUnit { get; set; }
        public double Value { get; set; }
        public string Invoice { get; set; }
    }
}

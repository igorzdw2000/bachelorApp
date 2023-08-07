using Core.Entities;

namespace API.Dtos
{
    public class ProjectAddDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double ProjectValue { get; set; }
        public int CustomerId { get; set; }
    }
}

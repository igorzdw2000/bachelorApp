using API.Converters;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Invoice
    {
        
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime SaleDate { get; set; }
        public DateTime DateOfPayment { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public double TotalToBePaid { get; set; }
        public string Comments { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public InvoiceType InvoiceType { get; set; }
    }
    [JsonConverter(typeof(EnumStringConverters<PaymentStatus>))]
    public enum PaymentStatus
    {
        Paid=0,
        Unpaid=1,
        Overdue=2
    }
    [JsonConverter(typeof(EnumStringConverters<PaymentMethod>))]
    public enum PaymentMethod
    {
        Transfer,
        Cash
    }
}

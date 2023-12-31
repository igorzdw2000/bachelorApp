﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Invoice
    {
        
        public int InvoiceId { get; set; }
        public DateTime Date { get; set; }
        public double TotalValue { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int MaterialSupplierId { get; set; }
        public MaterialSupplier MaterialSupplier { get; set; }
        public int SubcontractorId { get; set; }
        public Subcontractor Subcontractor { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public InvoiceType InvoiceType { get; set; }
    }
}

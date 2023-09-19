using API.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Entities
{

    [JsonConverter(typeof(EnumStringConverters<InvoiceType>))]
    public enum InvoiceType
    {
        Customer,
        MaterialSupplier,
        Subcontractor
    }
}

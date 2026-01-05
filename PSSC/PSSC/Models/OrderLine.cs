using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSSC.Models
{
    public record OrderLine(Guid ProductId, Quantity Qty, decimal UnitPrice);
}

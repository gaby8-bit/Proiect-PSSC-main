using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSSC.Models
{
    public record Quantity
    {
        public int Value { get; init; }

        public Quantity(int value)
        {
            if (value <= 0)
                throw new ArgumentException("Cantitatea trebuie să fie pozitivă.");

            Value = value;
        }
    }
}

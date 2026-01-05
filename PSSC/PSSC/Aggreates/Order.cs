using System;
using System.Collections.Generic;
using System.Linq;
using PSSC.Models;

namespace PSSC.Aggreates // Atenție: în imaginea ta folderul apare scris "Aggreates" (cu un 'a' în plus)
{
    public class Order
    {
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public List<OrderLine> Lines { get; private set; } = new();
        public decimal TotalPrice { get; private set; }
        public OrderStatus Status { get; private set; }

        public Order(Guid customerId)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            Status = OrderStatus.Draft;
        }

        public void AddItem(Guid productId, Quantity qty, decimal unitPrice)
        {
            Lines.Add(new OrderLine(productId, qty, unitPrice));
            TotalPrice = Lines.Sum(l => l.Qty.Value * l.UnitPrice);
        }

        public void Confirm()
        {
            if (Lines.Any()) Status = OrderStatus.Placed;
        }
        public void SetTotal(decimal total)
        {
            TotalPrice = total;
        }
    }

    public enum OrderStatus { Draft, Placed }
}
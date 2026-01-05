using System;
using System.Collections.Generic;
using PSSC.Models;

namespace PSSC.Events
{
    /// <summary>
    /// Eveniment declanșat atunci când o comandă a fost validată și plasată cu succes.
    /// Acest eveniment va fi ascultat de workflow-ul de Facturare (Billing).
    /// </summary>
    public record OrderPlacedEvent
    {
        public Guid OrderId { get; init; }
        public Guid CustomerId { get; init; }
        public decimal TotalAmount { get; init; }
        public DateTime OrderDate { get; init; }
        public List<OrderLineExport> Items { get; init; }

        public OrderPlacedEvent(Guid orderId, Guid customerId, decimal totalAmount, List<OrderLineExport> items)
        {
            OrderId = orderId;
            CustomerId = customerId;
            TotalAmount = totalAmount;
            OrderDate = DateTime.Now;
            Items = items;
        }
    }

    /// <summary>
    /// O versiune simplificată a liniilor de comandă pentru export, 
    /// evitând dependențele complexe de Value Objects în interiorul evenimentului.
    /// </summary>
    public record OrderLineExport(string ProductCode, int Quantity, decimal UnitPrice);
}
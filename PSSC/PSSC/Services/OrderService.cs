using PSSC.Aggreates;
using PSSC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSSC.Services
{
    public class OrderService
    {
        // În realitate, acestea ar fi interfețe către Repositories
        private readonly Dictionary<Guid, int> _mockStock = new();

        public async Task<Result<Order>> PlaceOrderWorkflow(Customer customer, List<(Guid ProductId, int Qty, decimal Price)> items)
        {

            // 1. Verificare date client
            if (string.IsNullOrEmpty(customer.Email) || !customer.Email.Contains("@"))
                return Result<Order>.Failure("Date client invalide.");

            var order = new Order(customer.Id);

            // 2. Validare stoc și adăugare produse
            foreach (var item in items)
            {
                if (item.Qty <= 0)
                    return Result<Order>.Failure($"Cantitate invalida pentru produsul {item.ProductId}: {item.Qty}");

                if (!CheckStock(item.ProductId, item.Qty))
                    return Result<Order>.Failure($"Stoc insuficient pentru produsul {item.ProductId}");

                order.AddItem(item.ProductId, new Quantity(item.Qty), item.Price);
            }

            // 3. Calcul preț total
            decimal total = order.Lines.Sum(l => l.Qty.Value * l.UnitPrice);
            order.SetTotal(total);

            // Finalizare workflow
            order.Confirm();
            return Result<Order>.Success(order);
        }

        private bool CheckStock(Guid productId, int qty) => true; // Simulare validare
    }

    // Helper pentru rezultatul operației
    public record Result<T>(T Value, bool IsSuccess, string Error)
    {
        public static Result<T> Success(T value) => new(value, true, null);
        public static Result<T> Failure(string error) => new(default, false, error);
    }
}

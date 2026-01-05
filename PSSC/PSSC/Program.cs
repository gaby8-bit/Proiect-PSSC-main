using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PSSC.Models;
using PSSC.Services;
using PSSC.Aggreates;
using PSSC.Common;

namespace PSSC
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== TEST WORKFLOW: PRELUARE COMANDA ===\n");

            // 1. Initializam serviciul
            var orderService = new OrderService();

            // 2. Cream un client si produse (Date de test)
            var address = new Address("Strada Exemplu 10", "Bucuresti", "123456");
            var customer = new Customer(Guid.NewGuid(), "Andrei Ionescu", "andrei@email.com", address);

            var items = new List<(Guid ProductId, int Qty, decimal Price)>
            {
                (Guid.NewGuid(), 2, 100.0m), // 2 bucati x 100 RON
                (Guid.NewGuid(), 1, 50.5m)   // 1 bucata x 50.5 RON
            };

            // 3. Executam workflow-ul
            Console.WriteLine("Se incearca plasarea comenzii...");
            var result = await orderService.PlaceOrderWorkflow(customer, items);

            // 4. Verificam rezultatul
            if (result.IsSuccess)
            {
                var order = result.Value;
                Console.WriteLine($"Succes! Total: {order.TotalPrice}");
            }
            else
            {
                Console.WriteLine($"Eroare: {result.Error}");
            }

            // 5. Testam un caz de eroare (Cantitate negativa)
            Console.WriteLine("\n--- Testare validare cantitate (eroare) ---");
            var invalidItems = new List<(Guid, int, decimal)> { (Guid.NewGuid(), -5, 20.0m) };
            var errorResult = await orderService.PlaceOrderWorkflow(customer, invalidItems);

            Console.WriteLine($"Mesaj eroare capturat: {errorResult.Error}");

            Console.WriteLine("\nTestare finalizata. Apasa orice tasta...");
            Console.ReadKey();
        }
    }
}
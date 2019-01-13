using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using Specification.Example;

namespace Specification.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var repo = new OrderRepository();

            var paidOrdersSpec = new PaidOrderSpecification();
            List<Order> paidOrders = repo.FindAll(paidOrdersSpec).ToList();
            List<Order> notPaidOrders = repo.FindAll(paidOrdersSpec.Not()).ToList();
            
            var readyToShipOrdersSpec = new ReadyToShipOrderSpecification();
            List<Order> readyToShip = repo.FindAll(readyToShipOrdersSpec).ToList();
            
            Specification<Order> paidAndReadyToShipOrdersSpec = paidOrdersSpec.And(readyToShipOrdersSpec);
            List<Order> paidAndReadyToShipOrders = repo.FindAll(paidAndReadyToShipOrdersSpec).ToList();

            List<Order> filteredOrders = GetFilteredOrders(false, true, repo);

            LogOrders(paidOrders);
            LogOrders(notPaidOrders);
            LogOrders(readyToShip);
            LogOrders(paidAndReadyToShipOrders);
            LogOrders(filteredOrders);

            Order orderToPay = notPaidOrders.LastOrDefault();
            if (orderToPay != null)
            {
                PayOrder(orderToPay);
            }

            Order orderToShip = repo.FindById(3);
            StartShippingProcess(orderToShip);
        }
        
        static void PayOrder(Order order)
        {
            var spec = new PaidOrderSpecification();

            if (spec.IsSatisfiedBy(order))
            {
                System.Console.WriteLine($"Order #{order.Id} has been already paid.");
            }
            else
            {
                order.MarkAsPaid();
                System.Console.WriteLine($"Order #{order.Id} has been paid successfully.");
            }
        }

        static void StartShippingProcess(Order order)
        {
            var paidOrdersSpec = new PaidOrderSpecification();
            var readyToShipOrdersSpec = new ReadyToShipOrderSpecification();
            Specification<Order> paidAndReadyToShipOrdersSpec = paidOrdersSpec.And(readyToShipOrdersSpec);

            if (paidAndReadyToShipOrdersSpec.IsSatisfiedBy(order))
            {
                System.Console.WriteLine($"Shipping process of order #{order.Id} has been started successfully.");
            }
            else
            {
                System.Console.WriteLine($"Impossible to start shipping process of order #{order.Id} because it isn't paid yet or isn't ready to ship");
            }
        }

        static List<Order> GetFilteredOrders(bool onlyPaid, bool onlyReadyToShip, OrderRepository repository)
        {
            Specification<Order> spec = Specification<Order>.All;

            if (onlyPaid)
            {
                spec = spec.And(new PaidOrderSpecification());
            }

            if (onlyReadyToShip)
            {
                spec = spec.And(new ReadyToShipOrderSpecification());
            }

            IEnumerable<Order> orders = repository.FindAll(spec);
            return orders.ToList();
        }
        
        static void LogOrders(List<Order> orders)
        {
            System.Console.WriteLine($"Id:\t Paid:\t Ready to ship:");
            orders.ForEach(order => System.Console.WriteLine($"{order.Id}\t {order.IsPaid}\t {order.IsReadyToShip}"));
            
            System.Console.WriteLine("-------------------------------\n");
        }
    }
}
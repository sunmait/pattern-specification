using System.Collections.Generic;
using System.Linq;

namespace Specification.Example
{
    public class OrderRepository
    {
        private readonly List<Order> _orders;


        public OrderRepository()
        {
            _orders = new List<Order>
            {
                new Order(1, isPaid: true, isReadyToShip: true),
                new Order(2, isPaid: false, isReadyToShip: false),
                new Order(3, isPaid: true, isReadyToShip: false),
                new Order(4, isPaid: true, isReadyToShip: false),
                new Order(5, isPaid: true, isReadyToShip: true),
                new Order(6, isPaid: false, isReadyToShip: true),
                new Order(7, isPaid: false, isReadyToShip: true)
            };
        }

        public Order FindById(long id)
        {
            Order entity = _orders.FirstOrDefault(x => x.Id == id);
            return entity;
        }

        public IEnumerable<Order> FindAll(Specification<Order> spec)
        {
            // Collections that implement IQueryable doesn't require '.Compile()' call
            IEnumerable<Order> result = _orders.Where(spec.ToExpression().Compile());
            return result;
        }
    }
}
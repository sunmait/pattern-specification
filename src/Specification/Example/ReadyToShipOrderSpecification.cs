using System;
using System.Linq.Expressions;

namespace Specification.Example
{
    public class ReadyToShipOrderSpecification : Specification<Order>
    {
        public override Expression<Func<Order, bool>> ToExpression()
        {
            return order => order.IsReadyToShip;
        }
    }
}
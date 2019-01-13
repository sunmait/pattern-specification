using System;
using System.Linq.Expressions;

namespace Specification
{
    internal class AllSpecification<T> : Specification<T>
    {
        public override Expression<Func<T, bool>> ToExpression()
        {
            return entity => true;
        }
    }
}
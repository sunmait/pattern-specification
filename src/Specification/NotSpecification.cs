using System;
using System.Linq.Expressions;

namespace Specification
{
    internal class NotSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _spec;

        public NotSpecification(Specification<T> spec)
        {
            _spec = spec;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> specExpression = _spec.ToExpression();

            UnaryExpression unaryExpression = Expression.Not(specExpression.Body);
            Expression<Func<T, bool>> notExpression = Expression.Lambda<Func<T, bool>>(unaryExpression, specExpression.Parameters);

            return notExpression;
        }
    }
}
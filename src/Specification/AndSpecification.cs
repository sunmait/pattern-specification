using System;
using System.Linq.Expressions;

namespace Specification
{
    internal class AndSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;

        public AndSpecification(Specification<T> left, Specification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> leftExpression = _left.ToExpression();
            Expression<Func<T, bool>> rightExpression = _right.ToExpression();

            InvocationExpression invokedRight = Expression.Invoke(rightExpression, leftExpression.Parameters);

            BinaryExpression binaryExpression = Expression.AndAlso(leftExpression.Body, invokedRight);
            Expression<Func<T, bool>> andExpression = Expression.Lambda<Func<T, bool>>(binaryExpression, leftExpression.Parameters);

            return andExpression;
        }
    }
}
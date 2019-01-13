using System;
using System.Linq.Expressions;

namespace Specification
{
    public abstract class Specification<T>
    {
        public static readonly Specification<T> All = new AllSpecification<T>();
        
        public abstract Expression<Func<T, bool>> ToExpression();

        public bool IsSatisfiedBy(T entity)
        {
            Func<T, bool> predicate = ToExpression().Compile();
            return predicate(entity);
        }

        public Specification<T> And(Specification<T> spec)
        {
            if (this == All)
            {
                return spec;
            }
            
            if (spec == All)
            {
                return this;
            }
            
            var andSpec = new AndSpecification<T>(this, spec);
            return andSpec;
        }

        public Specification<T> Or(Specification<T> spec)
        {
            if (this == All || spec == All)
            {
                return All;
            }
            
            var orSpec = new OrSpecification<T>(this, spec);
            return orSpec;
        }

        public Specification<T> Not()
        {
            var notSpec = new NotSpecification<T>(this);
            return notSpec;
        }
    }
}
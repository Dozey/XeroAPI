using System;
using System.Linq;
using System.Linq.Expressions;
using XeroAPI.Linq.Common;

namespace XeroAPI.Linq
{
    public class XeroQueryProvider : IQueryProvider
    {
        private IXeroQueryContext _queryContext;

        public XeroQueryProvider(IXeroQueryContext queryContext)
        {
            _queryContext = queryContext;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            Type elementType = TypeSystem.GetElementType(expression.Type);
            try
            {
                return (IQueryable)Activator.CreateInstance(typeof(QueryableXeroData<>).MakeGenericType(elementType), new object[] { this, expression });
            }
            catch (System.Reflection.TargetInvocationException tie)
            {
                throw tie.InnerException;
            }
        }

        // Queryable's collection-returning standard query operators call this method.
        public IQueryable<TResult> CreateQuery<TResult>(Expression expression)
        {
            return new QueryableXeroData<TResult>(this, expression);
        }

        public object Execute(Expression expression)
        {
            return _queryContext.Execute(expression, false);
        }

        // Queryable's "single value" standard query operators call this method.
        // It is also called from QueryableXeroData.GetEnumerator().
        public TResult Execute<TResult>(Expression expression)
        {
            bool IsEnumerable = (typeof(TResult).Name == "IEnumerable`1");

            return (TResult)_queryContext.Execute(expression, IsEnumerable);
        }
    }
}

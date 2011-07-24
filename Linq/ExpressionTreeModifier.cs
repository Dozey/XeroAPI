using System;
using System.Linq;
using System.Linq.Expressions;
using XeroAPI.Data;

namespace XeroAPI.Linq
{
    internal class ExpressionTreeModifier<TData> : ExpressionVisitor
    {
        private IQueryable<TData> _queryableResults;

        internal ExpressionTreeModifier(IQueryable<TData> results)
        {
            _queryableResults = results;
        }

        protected override Expression VisitConstant(ConstantExpression c)
        {
            // Replace the constant QueryableTerraServerData arg with the queryable Place collection.
            if (c.Type == typeof(QueryableXeroData<TData>))
                return Expression.Constant(_queryableResults);
            else
                return c;
        }
    }
}

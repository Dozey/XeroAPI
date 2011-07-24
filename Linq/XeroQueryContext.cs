using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using XeroAPI.Linq.Common;
using XeroAPI.Linq.Wrappers;
using XeroAPI.Client;

namespace XeroAPI.Linq
{
    delegate IEnumerable<TData> XeroQueryDelegate<TData>(string query);

    class XeroQueryContext<TData> : IXeroQueryContext
    {
        private XeroQueryDelegate<TData> _queryDelegate;

        public XeroQueryContext(XeroQueryDelegate<TData> queryDelegate)
        {
            _queryDelegate = queryDelegate;
        }

        public object Execute(Expression expression, bool IsEnumerable)
        {
            // The expression must represent a query over the data source.
            if (!IsQueryOverDataSource(expression))
                throw new InvalidProgramException("No query over the data source was specified.");

            // Find the call to Where() and get the lambda expression predicate.
            InnermostWhereFinder whereFinder = new InnermostWhereFinder();
            MethodCallExpression whereExpression = whereFinder.GetInnermostWhere(expression);

            LambdaExpression lambdaExpression = (LambdaExpression)((UnaryExpression)(whereExpression.Arguments[1])).Operand;

            // Send the lambda expression through the partial evaluator.
            lambdaExpression = (LambdaExpression)Evaluator.PartialEval(lambdaExpression);


            Type dataType = expression.Type.GetGenericArguments().First();
            XeroObjectAttribute xoa = AttributeUtility.Find<XeroObjectAttribute>(dataType);

            List<TData> results;

            if (xoa != null && XeroExpressionValidator.ValidateExpression(lambdaExpression.Body))
            {
                string xeroQuery = XeroExpressionWrapperBase.CreateWrapper(lambdaExpression.Body).ToString();
                results = new List<TData>(_queryDelegate(xeroQuery));
            }
            else
            {
                results = new List<TData>();
            }

            IQueryable<TData> queryableResults = results.AsQueryable<TData>();

            //// Copy the expression tree that was passed in, changing only the first
            //// argument of the innermost MethodCallExpression.
            ExpressionTreeModifier<TData> treeCopier = new ExpressionTreeModifier<TData>(queryableResults);
            Expression newExpressionTree = treeCopier.Visit(expression);

            //// This step creates an IQueryable that executes by replacing Queryable methods with Enumerable methods.
            if (IsEnumerable)
                return queryableResults.Provider.CreateQuery(newExpressionTree);
            else
                return queryableResults.Provider.Execute(newExpressionTree);
        }

        private static bool IsQueryOverDataSource(Expression expression)
        {
            // If expression represents an unqueried IQueryable data source instance,
            // expression is of type ConstantExpression, not MethodCallExpression.
            return (expression is MethodCallExpression);
        }
    }
}

using System;
using System.Linq;
using System.Linq.Expressions;
using XeroAPI.Linq.Common;
using XeroAPI.Linq.Wrappers;

namespace XeroAPI.Linq
{
    class XeroApplicationQueryContext
    {
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

            if (xoa != null && XeroExpressionValidator.ValidateExpression(lambdaExpression))
            {
                string xeroQuery = XeroExpressionWrapperBase.CreateWrapper(lambdaExpression.Body).ToString();


                switch (xoa.Path)
                {
                    case "Invoice":

                        break;
                    case "Contact":
                        break;
                }
            }


            //// Get the place name(s) to query the Web service with.
            //LocationFinder lf = new LocationFinder(lambdaExpression.Body);
            //List<string> locations = lf.Locations;
            //if (locations.Count == 0)
            //    throw new InvalidQueryException("You must specify at least one place name in your query.");

            //// Call the Web service and get the results.
            //Place[] places = WebServiceHelper.GetPlacesFromTerraServer(locations);

            //// Copy the IEnumerable places to an IQueryable.
            //IQueryable<Place> queryablePlaces = places.AsQueryable<Place>();

            //// Copy the expression tree that was passed in, changing only the first
            //// argument of the innermost MethodCallExpression.
            //ExpressionTreeModifier treeCopier = new ExpressionTreeModifier(queryablePlaces);
            //Expression newExpressionTree = treeCopier.Visit(expression);

            //// This step creates an IQueryable that executes by replacing Queryable methods with Enumerable methods.
            //if (IsEnumerable)
            //    return queryablePlaces.Provider.CreateQuery(newExpressionTree);
            //else
            //    return queryablePlaces.Provider.Execute(newExpressionTree);

            return null;
        }

        private static bool IsQueryOverDataSource(Expression expression)
        {
            // If expression represents an unqueried IQueryable data source instance,
            // expression is of type ConstantExpression, not MethodCallExpression.
            return (expression is MethodCallExpression);
        }
    }
}

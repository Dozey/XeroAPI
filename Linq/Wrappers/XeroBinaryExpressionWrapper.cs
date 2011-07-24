using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace XeroAPI.Linq.Wrappers
{
    class XeroBinaryExpressionWrapper : XeroExpressionWrapper<BinaryExpression>
    {
        private string _value;

        public XeroBinaryExpressionWrapper(BinaryExpression exp) : base(exp) { }

        protected override void WrapExpression()
        {
            string left = XeroExpressionWrapperBase.CreateWrapperInternal(Expression.Left).ToString();
            string right = XeroExpressionWrapperBase.CreateWrapperInternal(Expression.Right).ToString();

            switch (Expression.NodeType)
            {
                case ExpressionType.AndAlso:
                    _value = String.Concat(left, " && ", right);
                    break;
                case ExpressionType.OrElse:
                    _value = String.Concat(left, " || ", right);
                    break;
                case ExpressionType.Equal:
                    _value = String.Concat(left, " == ", right);
                    break;
                case ExpressionType.NotEqual:
                    _value = String.Concat(left, " != ", right);
                    break;
                case ExpressionType.LessThan:
                    _value = String.Concat(left, " < ", right);
                    break;
                case ExpressionType.LessThanOrEqual:
                    _value = String.Concat(left, " <= ", right);
                    break;
                case ExpressionType.GreaterThan:
                    _value = String.Concat(left, " > ", right);
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    _value = String.Concat(left, " >= ", right);
                    break;
                default:
                    throw new Exception("Failed to convert operation to Xero compatible string");
            }
        }

        public override string ToString()
        {
            Wrap();

            return _value;
        }
    }
}

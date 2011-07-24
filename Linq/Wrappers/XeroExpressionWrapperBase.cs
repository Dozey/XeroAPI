using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace XeroAPI.Linq.Wrappers
{
    abstract class XeroExpressionWrapperBase
    {
        private bool _isWrapped = false;

        public string XeroExpression
        {
            get { return ToString(); }
        }

        public static XeroExpressionWrapperBase CreateWrapper(Expression exp)
        {
            if (!ValidateExpressionTree(exp))
                throw new Exception("Expression contains elements invalid in Xero queries");

            return CreateWrapperInternal(exp);
        }

        protected static XeroExpressionWrapperBase CreateWrapperInternal(Expression exp)
        {
            switch (exp.NodeType)
            {
                case ExpressionType.Call:
                    return new XeroMethodCallExpressionWrapper((MethodCallExpression)exp);
                case ExpressionType.Parameter:
                    return new XeroParameterExpressionWrapper((ParameterExpression)exp);
                case ExpressionType.Constant:
                    return new XeroConstantExpressionWrapper((ConstantExpression)exp);
                case ExpressionType.MemberAccess:
                    return new XeroMemberExpressionWrapper((MemberExpression)exp);
                case ExpressionType.AndAlso:
                case ExpressionType.OrElse:
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                    return new XeroBinaryExpressionWrapper((BinaryExpression)exp);
                default:
                    return null;
            }
        }

        private static bool ValidateExpressionTree(Expression exp)
        {
            return XeroExpressionValidator.ValidateExpression(exp);
        }

        protected void Wrap()
        {
            if (!_isWrapped)
            {
                WrapExpression();
                _isWrapped = true;
            }
        }

        abstract protected void WrapExpression();
    }

    internal class XeroExpressionValidator : ExpressionVisitor
    {
        private static Collection<string> _allowedMethods = new Collection<string>(new string[] { "ToString", "StartsWith", "EndsWith", "Contains", "ToLower", "ToUpper", "ToLowerInvariant" });
        private bool _isVisited = false;
        private bool _isValid = true;
        private Expression _expression;

        private XeroExpressionValidator(Expression exp)
        {
            _expression = exp;
        }

        public bool IsValid
        {
            get
            {
                if (!_isVisited)
                    Visit(_expression);

                return _isValid;
            }
        }

        public static bool ValidateExpression(Expression exp)
        {
            XeroExpressionValidator validator = new XeroExpressionValidator(exp);
            return validator.IsValid;
        }

        public override Expression Visit(Expression node)
        {
            _isVisited = true;

            switch (node.NodeType)
            {
                case ExpressionType.Call:
                case ExpressionType.Parameter:
                case ExpressionType.Constant:
                case ExpressionType.MemberAccess:
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.AndAlso:
                case ExpressionType.IsFalse:
                case ExpressionType.IsTrue:
                case ExpressionType.OrElse:
                    return base.Visit(node);
                default:
                    _isValid = false;
                    return null;
            }
       }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.AndAlso:
                case ExpressionType.OrElse:
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                    return base.VisitBinary(node);
                default:
                    _isValid = false;
                    return null;
            }
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            switch (Type.GetTypeCode(node.Type))
            {
                case TypeCode.String:
                case TypeCode.Char:
                case TypeCode.Boolean:
                case TypeCode.DateTime:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.Single:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Object:
                    return base.VisitConstant(node);
                default:
                    _isValid = false;
                    return null;
            }
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            //if (AttributeUtility.Find<XeroObjectPathAttribute>(node.Member) == null)
            //{
            //    _isValid = false;
            //    return null;
            //}

            return base.VisitMember(node);
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            //if (!_allowedMethods.Contains(node.Method.Name))
            //{
            //    _isValid = false;
            //    return null;
            //}

            return base.VisitMethodCall(node);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return base.VisitParameter(node);
        }
    }
}

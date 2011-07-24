using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using XeroAPI.Linq.Common;
using System.Reflection;

namespace XeroAPI.Linq
{
    class XeroObjectLocator : ExpressionVisitor, IXeroObjectPath
    {
        private static Collection<string> _allowedMethods = new Collection<string>(new string[] { "ToString", "StartsWith", "EndsWith", "Contains" });
        private Expression _expression;
        private Stack<string> _expressionFragments = new Stack<string>();
        private bool _isValidXeroObjectPath = true;

        public XeroObjectLocator(Expression exp)
        {
            _expression = exp;

            Visit(exp);
        }

        public override Expression Visit(Expression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.MemberAccess:
                case ExpressionType.Parameter:
                case ExpressionType.Call:
                case ExpressionType.Constant:
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                     return base.Visit(node);
                default:
                    _isValidXeroObjectPath = false;
                    return null;
            }
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (IsValidXeroObject(node.Member))
            {
                _expressionFragments.Push(node.Member.Name);
                _expressionFragments.Push(".");
            }
            else
            {
                _isValidXeroObjectPath = false;
                return null;
            }

            return base.VisitMember(node);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return base.VisitParameter(node);
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (_allowedMethods.Contains(node.Method.Name))
            {
                _expressionFragments.Push(")");
                _expressionFragments.Push("(");
                Expression result = base.VisitMethodCall(node);

                return result;
            }
            else
            {
                _isValidXeroObjectPath = false;
                return null;
            }
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            base.Visit(node.Right);

            switch (node.NodeType)
            {
                case ExpressionType.Equal:
                    _expressionFragments.Push(" == ");
                    break;
                case ExpressionType.NotEqual:
                    _expressionFragments.Push(" != ");
                    break;
                case ExpressionType.LessThan:
                    _expressionFragments.Push(" < ");
                    break;
                case ExpressionType.LessThanOrEqual:
                    _expressionFragments.Push(" <= ");
                    break;
                case ExpressionType.GreaterThan:
                    _expressionFragments.Push(" > ");
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    _expressionFragments.Push(" >= ");
                    break;
                default:
                    _isValidXeroObjectPath = false;
                    return null;
            }

            base.Visit(node.Left);

            return node;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            _expressionFragments.Push("\"");
            _expressionFragments.Push(node.Value.ToString());
            _expressionFragments.Push("\"");

            return base.VisitConstant(node);
        }

        private bool IsValidXeroObject(ICustomAttributeProvider obj)
        {
            return true;
            //return AttributeUtility.Find<XeroObjectPathAttribute>(obj) != null;
        }

        public bool IsValid
        {
            get { return _isValidXeroObjectPath; }
        }

        public string Path
        {
            get {
                if (_isValidXeroObjectPath)
                {
                    return String.Join("", _expressionFragments);
                }
                else
                {
                    return String.Empty;
                }
            }
        }
    }
}

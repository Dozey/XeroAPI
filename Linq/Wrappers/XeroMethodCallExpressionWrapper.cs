using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace XeroAPI.Linq.Wrappers
{
    class XeroMethodCallExpressionWrapper : XeroExpressionWrapper<MethodCallExpression>
    {
        private string _value;

        public XeroMethodCallExpressionWrapper(MethodCallExpression exp) : base(exp) { }

        protected override void WrapExpression()
        {
            string objectPath = XeroExpressionWrapperBase.CreateWrapper(Expression.Object).ToString();
            XeroExpressionWrapperBase[] arguments = Expression.Arguments.Select<Expression, XeroExpressionWrapperBase>((exp) => XeroExpressionWrapperBase.CreateWrapperInternal(exp)).ToArray();

            _value = String.Format("{0}.{1}({2})",  objectPath, Expression.Method.Name, String.Join(", ", arguments.Select<XeroExpressionWrapperBase, string>((exp) => exp.ToString())));
        }

        public override string ToString()
        {
            Wrap();

            return _value;
        }
    }
}

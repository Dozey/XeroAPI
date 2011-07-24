using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace XeroAPI.Linq.Wrappers
{
    class XeroMemberExpressionWrapper : XeroExpressionWrapper<MemberExpression>
    {
        private string _value;

        public XeroMemberExpressionWrapper(MemberExpression exp) : base(exp) { }

        protected override void WrapExpression()
        {
            if (!(Expression.Expression is ParameterExpression))
            {

                string expression = XeroExpressionWrapperBase.CreateWrapperInternal(Expression.Expression).ToString();

                XeroObjectAttribute xoa = AttributeUtility.Find<XeroObjectAttribute>(Expression.Member);
                _value = String.Concat(expression, ".", xoa == null ? Expression.Member.Name.ToString() : xoa.Path);
            }
            else
            {
                _value = Expression.Member.Name.ToString();
            }
        }

        public override string ToString()
        {
            Wrap();

            return _value;
        }
    }
}

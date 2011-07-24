using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace XeroAPI.Linq.Wrappers
{
    class XeroParameterExpressionWrapper : XeroExpressionWrapper<ParameterExpression>
    {
        private string _value;

        public XeroParameterExpressionWrapper(ParameterExpression exp) : base(exp) { }

        protected override void WrapExpression()
        {
            XeroObjectAttribute xoa = AttributeUtility.Find<XeroObjectAttribute>(Expression.Type);
            _value = xoa == null ? Expression.Type.Name : xoa.Path;
        }

        public override string ToString()
        {
            Wrap();

            return _value;
        }
    }
}

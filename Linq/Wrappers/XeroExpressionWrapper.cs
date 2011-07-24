using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace XeroAPI.Linq.Wrappers
{
    abstract class XeroExpressionWrapper<TExpression> : XeroExpressionWrapperBase where TExpression : Expression
    {
        public XeroExpressionWrapper(TExpression exp)
        {
            Expression = exp;
        }

        protected TExpression Expression { get; private set;}

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

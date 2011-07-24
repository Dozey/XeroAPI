using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace XeroAPI.Linq
{
    public interface IXeroQueryContext
    {
        object Execute(Expression expression, bool IsEnumerable);
    }
}

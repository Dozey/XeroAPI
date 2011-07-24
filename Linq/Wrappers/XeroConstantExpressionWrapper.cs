using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace XeroAPI.Linq.Wrappers
{
    class XeroConstantExpressionWrapper : XeroExpressionWrapper<ConstantExpression>
    {
        private string _value;

        public XeroConstantExpressionWrapper(ConstantExpression exp) : base(exp) { }

        protected override void WrapExpression()
        {
            switch (Type.GetTypeCode(Expression.Type))
            {
                case TypeCode.Object:
                case TypeCode.String:
                case TypeCode.Char:
                    _value = String.Format(@"""{0}""", Expression.Value.ToString());
                    break;
                case TypeCode.Boolean:
                    _value = String.Format(@"""{0}""", Expression.Value.ToString().ToLowerInvariant());
                    break;
                case TypeCode.DateTime:
                    _value = String.Format(@"DateTime.Parse(""{0}"")", ((DateTime)Expression.Value).ToString("s"));
                    break;
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
                    _value = Expression.Value.ToString();
                    break;
                default:
                    throw new Exception("Failed to convert constant to string");
            }
        }

        public override string ToString()
        {
            Wrap();

            return _value;
        }
    }
}

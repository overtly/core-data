using System.Collections.Generic;
using System.Linq.Expressions;

namespace Overt.Core.Data.Expressions
{
    class ConstantSqlExpression : BaseSqlExpression<ConstantExpression>
    {
        protected override SqlGenerate Where(ConstantExpression expression, SqlGenerate sqlGenerate)
        {
            sqlGenerate.AddDbParameter(expression.Value);
            return sqlGenerate;
        }

        protected override SqlGenerate In(ConstantExpression expression, SqlGenerate sqlGenerate)
        {
            sqlGenerate.AddDbParameter(expression.Value);
            sqlGenerate += ",";
            return sqlGenerate;
        }

        protected override SqlGenerate Select(ConstantExpression expression, SqlGenerate sqlGenerate)
        {
            if (expression.Value == null)
                sqlGenerate.SelectFields = new List<string>() { "*" };
            else
                sqlGenerate.SelectFields = new List<string>() { expression.Value.ToString() };
            return sqlGenerate;
        }
    }
}
using System.Linq.Expressions;

namespace Sodao.Core.Data.Expressions
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
    }
}
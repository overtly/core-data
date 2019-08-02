using System.Linq.Expressions;

namespace Overt.Core.Data.Expressions
{
    class NewArraySqlExpression : BaseSqlExpression<NewArrayExpression>
    {
        protected override SqlGenerate In(NewArrayExpression expression, SqlGenerate sqlGenerate)
        {
            sqlGenerate += "(";
            foreach (Expression expressionItem in expression.Expressions)
            {
                SqlExpressionProvider.In(expressionItem, sqlGenerate);
            }

            if (sqlGenerate.Sql[sqlGenerate.Sql.Length - 1] == ',')
                sqlGenerate.Sql.Remove(sqlGenerate.Sql.Length - 1, 1);
            sqlGenerate += ")";

            return sqlGenerate;
        }
    }
}

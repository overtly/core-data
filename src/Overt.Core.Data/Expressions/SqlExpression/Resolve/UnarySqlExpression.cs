using System.Linq.Expressions;

namespace Overt.Core.Data.Expressions
{
    class UnarySqlExpression : BaseSqlExpression<UnaryExpression>
    {
        protected override SqlGenerate Select(UnaryExpression expression, SqlGenerate sqlGenerate)
        {
            SqlExpressionProvider.Select(expression.Operand, sqlGenerate);
            return sqlGenerate;
        }

        protected override SqlGenerate Where(UnaryExpression expression, SqlGenerate sqlGenerate)
        {
            SqlExpressionProvider.Where(expression.Operand, sqlGenerate);
            switch (expression.NodeType)
            {
                case ExpressionType.Not:
                    if (expression.Operand is MethodCallExpression)
                        sqlGenerate.RelaceLast("in", "not in");
                    else
                        sqlGenerate += " = 0";
                    break;
            }

            return sqlGenerate;
        }

        protected override SqlGenerate OrderBy(UnaryExpression expression, SqlGenerate sqlGenerate)
        {
            SqlExpressionProvider.OrderBy(expression.Operand, sqlGenerate);
            return sqlGenerate;
        }

        protected override SqlGenerate Max(UnaryExpression expression, SqlGenerate sqlGenerate)
        {
            SqlExpressionProvider.Max(expression.Operand, sqlGenerate);
            return sqlGenerate;
        }

        protected override SqlGenerate Min(UnaryExpression expression, SqlGenerate sqlGenerate)
        {
            SqlExpressionProvider.Min(expression.Operand, sqlGenerate);
            return sqlGenerate;
        }

        protected override SqlGenerate Avg(UnaryExpression expression, SqlGenerate sqlGenerate)
        {
            SqlExpressionProvider.Avg(expression.Operand, sqlGenerate);
            return sqlGenerate;
        }

        protected override SqlGenerate Count(UnaryExpression expression, SqlGenerate sqlGenerate)
        {
            SqlExpressionProvider.Count(expression.Operand, sqlGenerate);
            return sqlGenerate;
        }

        protected override SqlGenerate Sum(UnaryExpression expression, SqlGenerate sqlGenerate)
        {
            SqlExpressionProvider.Sum(expression.Operand, sqlGenerate);
            return sqlGenerate;
        }
    }
}

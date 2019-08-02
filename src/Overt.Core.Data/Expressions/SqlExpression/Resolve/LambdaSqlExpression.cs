using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Overt.Core.Data.Expressions
{
    public class LambdaSqlExpression : BaseSqlExpression<LambdaExpression>
    {
        protected override SqlGenerate Update(LambdaExpression expression, SqlGenerate sqlGenerate)
        {
            SqlExpressionProvider.Update(expression.Body, sqlGenerate);
            return sqlGenerate;
        }
        protected override SqlGenerate Select(LambdaExpression expression, SqlGenerate sqlGenerate)
        {
            SqlExpressionProvider.Select(expression.Body, sqlGenerate);
            return sqlGenerate;
        }
        protected override SqlGenerate Where(LambdaExpression expression, SqlGenerate sqlGenerate)
        {
            if (expression.Body.NodeType == ExpressionType.MemberAccess)
            {
                var memberExpression = expression.Body as MemberExpression;
                if (memberExpression.Expression == null)
                    return sqlGenerate;

                //添加属性
                SqlExpressionProvider.Where(memberExpression, sqlGenerate);

                if (memberExpression.Expression.Type.IsNullableType())
                    return sqlGenerate;

                sqlGenerate += " = 1";
                return sqlGenerate;
            }

            SqlExpressionProvider.Where(expression.Body, sqlGenerate);
            return sqlGenerate;
        }
        protected override SqlGenerate In(LambdaExpression expression, SqlGenerate sqlGenerate)
        {
            SqlExpressionProvider.In(expression.Body, sqlGenerate);
            return sqlGenerate;
        }
        protected override SqlGenerate OrderBy(LambdaExpression expression, SqlGenerate sqlGenerate)
        {
            SqlExpressionProvider.OrderBy(expression.Body, sqlGenerate);
            return sqlGenerate;
        }
        protected override SqlGenerate Max(LambdaExpression expression, SqlGenerate sqlGenerate)
        {
            SqlExpressionProvider.Max(expression.Body, sqlGenerate);
            return sqlGenerate;
        }
        protected override SqlGenerate Min(LambdaExpression expression, SqlGenerate sqlGenerate)
        {
            SqlExpressionProvider.Min(expression.Body, sqlGenerate);
            return sqlGenerate;
        }
        protected override SqlGenerate Avg(LambdaExpression expression, SqlGenerate sqlGenerate)
        {
            SqlExpressionProvider.Avg(expression.Body, sqlGenerate);
            return sqlGenerate;
        }
        protected override SqlGenerate Count(LambdaExpression expression, SqlGenerate sqlGenerate)
        {
            SqlExpressionProvider.Count(expression.Body, sqlGenerate);
            return sqlGenerate;
        }
        protected override SqlGenerate Sum(LambdaExpression expression, SqlGenerate sqlGenerate)
        {
            SqlExpressionProvider.Sum(expression.Body, sqlGenerate);
            return sqlGenerate;
        }
    }
}

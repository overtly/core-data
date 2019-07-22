using System;
using System.Linq;
using System.Linq.Expressions;

namespace Sodao.Core.Data
{
    /// <summary>
    /// expression 扩展
    /// </summary>
    public static class ExpressionExtensions
    {
        /// <summary>
        /// and
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
        Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
            (Expression.AndAlso(expr1.Body, expr2.Body), expr1.Parameters);
        }

        /// <summary>
        /// or
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
        Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
            (Expression.OrElse(expr1.Body, expr2.Body), expr1.Parameters);
        }
    }
}

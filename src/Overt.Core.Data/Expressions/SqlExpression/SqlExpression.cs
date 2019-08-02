using System;
using System.Linq.Expressions;

namespace Overt.Core.Data.Expressions
{
    /// <summary>
    /// Expression => Sql 
    /// </summary>
    public static class SqlExpression
    {
        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbType">数据库类型</param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static SqlExpressionCore<T> Delete<T>(DatabaseType dbType, string tableName = "")
        {
            return new SqlExpressionCore<T>(dbType, tableName).Delete();
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbType">数据库类型</param>
        /// <param name="expression"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static SqlExpressionCore<T> Update<T>(DatabaseType dbType, Expression<Func<object>> expression = null, string tableName = "")
        {
            return new SqlExpressionCore<T>(dbType, tableName).Update(expression);
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbType">数据库类型</param>
        /// <param name="expression"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static SqlExpressionCore<T> Select<T>(DatabaseType dbType, Expression<Func<T, object>> expression = null, string tableName = "")
        {
            return new SqlExpressionCore<T>(dbType, tableName).Select(expression);
        }
        /// <summary>
        /// 数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbType">数据库类型</param>
        /// <param name="expression"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static SqlExpressionCore<T> Count<T>(DatabaseType dbType, Expression<Func<T, object>> expression = null, string tableName = "")
        {
            return new SqlExpressionCore<T>(dbType, tableName).Count(expression);
        }
        /// <summary>
        /// 最大
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbType">数据库类型</param>
        /// <param name="expression"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static SqlExpressionCore<T> Max<T>(DatabaseType dbType, Expression<Func<T, object>> expression, string tableName = "")
        {
            return new SqlExpressionCore<T>(dbType, tableName).Max(expression);
        }
        /// <summary>
        /// 最小
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbType">数据库类型</param>
        /// <param name="expression"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static SqlExpressionCore<T> Min<T>(DatabaseType dbType, Expression<Func<T, object>> expression, string tableName = "")
        {
            return new SqlExpressionCore<T>(dbType, tableName).Min(expression);
        }
        /// <summary>
        /// 平均值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbType">数据库类型</param>
        /// <param name="expression"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static SqlExpressionCore<T> Avg<T>(DatabaseType dbType, Expression<Func<T, object>> expression, string tableName = "")
        {
            return new SqlExpressionCore<T>(dbType, tableName).Avg(expression);
        }
        /// <summary>
        /// 求和
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbType">数据库类型</param>
        /// <param name="expression"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static SqlExpressionCore<T> Sum<T>(DatabaseType dbType, Expression<Func<T, object>> expression, string tableName = "")
        {
            return new SqlExpressionCore<T>(dbType, tableName).Sum(expression);
        }
    }
}

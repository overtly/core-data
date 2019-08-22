using System;
using System.Linq.Expressions;

namespace Overt.Core.Data.Expressions
{
    /// <summary>
    /// BaseSqlExpression
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseSqlExpression<T> : ISqlExpression where T : Expression
    {
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="sqlGenerate"></param>
        /// <returns></returns>
		protected virtual SqlGenerate Update(T expression, SqlGenerate sqlGenerate)
        {
            throw new NotImplementedException("未实现" + typeof(T).Name + "2Sql.Update方法");
        }
        /// <summary>
        /// Select
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="sqlGenerate"></param>
        /// <returns></returns>
		protected virtual SqlGenerate Select(T expression, SqlGenerate sqlGenerate)
        {
            throw new NotImplementedException("未实现" + typeof(T).Name + "2Sql.Select方法");
        }
        /// <summary>
        /// Where
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="sqlGenerate"></param>
        /// <returns></returns>
		protected virtual SqlGenerate Where(T expression, SqlGenerate sqlGenerate)
        {
            throw new NotImplementedException("未实现" + typeof(T).Name + "2Sql.Where方法");
        }
        /// <summary>
        /// In
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="sqlGenerate"></param>
        /// <returns></returns>
		protected virtual SqlGenerate In(T expression, SqlGenerate sqlGenerate)
        {
            throw new NotImplementedException("未实现" + typeof(T).Name + "2Sql.In方法");
        }
        /// <summary>
        /// OrderBy
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="sqlGenerate"></param>
        /// <returns></returns>
		protected virtual SqlGenerate OrderBy(T expression, SqlGenerate sqlGenerate)
        {
            throw new NotImplementedException("未实现" + typeof(T).Name + "2Sql.OrderBy方法");
        }
        /// <summary>
        /// Max
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="sqlGenerate"></param>
        /// <returns></returns>
		protected virtual SqlGenerate Max(T expression, SqlGenerate sqlGenerate)
        {
            throw new NotImplementedException("未实现" + typeof(T).Name + "2Sql.Max方法");
        }
        /// <summary>
        /// Min
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="sqlGenerate"></param>
        /// <returns></returns>
		protected virtual SqlGenerate Min(T expression, SqlGenerate sqlGenerate)
        {
            throw new NotImplementedException("未实现" + typeof(T).Name + "2Sql.Min方法");
        }
        /// <summary>
        /// Avg
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="sqlGenerate"></param>
        /// <returns></returns>
		protected virtual SqlGenerate Avg(T expression, SqlGenerate sqlGenerate)
        {
            throw new NotImplementedException("未实现" + typeof(T).Name + "2Sql.Avg方法");
        }
        /// <summary>
        /// Count
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="sqlGenerate"></param>
        /// <returns></returns>
		protected virtual SqlGenerate Count(T expression, SqlGenerate sqlGenerate)
        {
            throw new NotImplementedException("未实现" + typeof(T).Name + "2Sql.Count方法");
        }
        /// <summary>
        /// Sum
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="sqlGenerate"></param>
        /// <returns></returns>
		protected virtual SqlGenerate Sum(T expression, SqlGenerate sqlGenerate)
        {
            throw new NotImplementedException("未实现" + typeof(T).Name + "2Sql.Sum方法");
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="sqlGenerate"></param>
        /// <returns></returns>
		public SqlGenerate Update(Expression expression, SqlGenerate sqlGenerate)
        {
            return Update((T)expression, sqlGenerate);
        }
        /// <summary>
        /// Select
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="sqlGenerate"></param>
        /// <returns></returns>
		public SqlGenerate Select(Expression expression, SqlGenerate sqlGenerate)
        {
            return Select((T)expression, sqlGenerate);
        }
        /// <summary>
        /// Where
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="sqlGenerate"></param>
        /// <returns></returns>
		public SqlGenerate Where(Expression expression, SqlGenerate sqlGenerate)
        {
            return Where((T)expression, sqlGenerate);
        }
        /// <summary>
        /// In
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="sqlGenerate"></param>
        /// <returns></returns>
		public SqlGenerate In(Expression expression, SqlGenerate sqlGenerate)
        {
            return In((T)expression, sqlGenerate);
        }
        /// <summary>
        /// OrderBy
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="sqlGenerate"></param>
        /// <returns></returns>
		public SqlGenerate OrderBy(Expression expression, SqlGenerate sqlGenerate)
        {
            return OrderBy((T)expression, sqlGenerate);
        }
        /// <summary>
        /// Max
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="sqlGenerate"></param>
        /// <returns></returns>
		public SqlGenerate Max(Expression expression, SqlGenerate sqlGenerate)
        {
            return Max((T)expression, sqlGenerate);
        }
        /// <summary>
        /// Min
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="sqlGenerate"></param>
        /// <returns></returns>
		public SqlGenerate Min(Expression expression, SqlGenerate sqlGenerate)
        {
            return Min((T)expression, sqlGenerate);
        }
        /// <summary>
        /// Avg
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="sqlGenerate"></param>
        /// <returns></returns>
		public SqlGenerate Avg(Expression expression, SqlGenerate sqlGenerate)
        {
            return Avg((T)expression, sqlGenerate);
        }
        /// <summary>
        /// Count
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="sqlGenerate"></param>
        /// <returns></returns>
		public SqlGenerate Count(Expression expression, SqlGenerate sqlGenerate)
        {
            return Count((T)expression, sqlGenerate);
        }
        /// <summary>
        /// Sum
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="sqlGenerate"></param>
        /// <returns></returns>
		public SqlGenerate Sum(Expression expression, SqlGenerate sqlGenerate)
        {
            return Sum((T)expression, sqlGenerate);
        }
    }
}

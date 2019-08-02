using System;
using System.Linq.Expressions;

namespace Overt.Core.Data.Expressions
{
    public abstract class BaseSqlExpression<T> : ISqlExpression where T : Expression
	{
		protected virtual SqlGenerate Update(T expression, SqlGenerate sqlGenerate)
		{
			throw new NotImplementedException("未实现" + typeof(T).Name + "2Sql.Update方法");
		}
		protected virtual SqlGenerate Select(T expression, SqlGenerate sqlGenerate)
		{
			throw new NotImplementedException("未实现" + typeof(T).Name + "2Sql.Select方法");
		}
		protected virtual SqlGenerate Where(T expression, SqlGenerate sqlGenerate)
		{
			throw new NotImplementedException("未实现" + typeof(T).Name + "2Sql.Where方法");
		}
		protected virtual SqlGenerate In(T expression, SqlGenerate sqlGenerate)
		{
			throw new NotImplementedException("未实现" + typeof(T).Name + "2Sql.In方法");
		}
		protected virtual SqlGenerate OrderBy(T expression, SqlGenerate sqlGenerate)
		{
			throw new NotImplementedException("未实现" + typeof(T).Name + "2Sql.OrderBy方法");
		}
		protected virtual SqlGenerate Max(T expression, SqlGenerate sqlGenerate)
		{
			throw new NotImplementedException("未实现" + typeof(T).Name + "2Sql.Max方法");
		}
		protected virtual SqlGenerate Min(T expression, SqlGenerate sqlGenerate)
		{
			throw new NotImplementedException("未实现" + typeof(T).Name + "2Sql.Min方法");
		}
		protected virtual SqlGenerate Avg(T expression, SqlGenerate sqlGenerate)
		{
			throw new NotImplementedException("未实现" + typeof(T).Name + "2Sql.Avg方法");
		}
		protected virtual SqlGenerate Count(T expression, SqlGenerate sqlGenerate)
		{
			throw new NotImplementedException("未实现" + typeof(T).Name + "2Sql.Count方法");
		}
		protected virtual SqlGenerate Sum(T expression, SqlGenerate sqlGenerate)
		{
			throw new NotImplementedException("未实现" + typeof(T).Name + "2Sql.Sum方法");
		}


		public SqlGenerate Update(Expression expression, SqlGenerate sqlGenerate)
		{
			return Update((T)expression, sqlGenerate);
		}
		public SqlGenerate Select(Expression expression, SqlGenerate sqlGenerate)
		{
			return Select((T)expression, sqlGenerate);
		}
		public SqlGenerate Where(Expression expression, SqlGenerate sqlGenerate)
		{
			return Where((T)expression, sqlGenerate);
		}
		public SqlGenerate In(Expression expression, SqlGenerate sqlGenerate)
		{
			return In((T)expression, sqlGenerate);
		}
		public SqlGenerate OrderBy(Expression expression, SqlGenerate sqlGenerate)
		{
			return OrderBy((T)expression, sqlGenerate);
		}
		public SqlGenerate Max(Expression expression, SqlGenerate sqlGenerate)
		{
			return Max((T)expression, sqlGenerate);
		}
		public SqlGenerate Min(Expression expression, SqlGenerate sqlGenerate)
		{
			return Min((T)expression, sqlGenerate);
		}
		public SqlGenerate Avg(Expression expression, SqlGenerate sqlGenerate)
		{
			return Avg((T)expression, sqlGenerate);
		}
		public SqlGenerate Count(Expression expression, SqlGenerate sqlGenerate)
		{
			return Count((T)expression, sqlGenerate);
		}
		public SqlGenerate Sum(Expression expression, SqlGenerate sqlGenerate)
		{
			return Sum((T)expression, sqlGenerate);
		}
	}
}

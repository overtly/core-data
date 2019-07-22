using System.Linq.Expressions;

namespace Sodao.Core.Data.Expressions
{
    public interface ISqlExpression
	{
		SqlGenerate Update(Expression expression, SqlGenerate sqlGenerate);

		SqlGenerate Select(Expression expression, SqlGenerate sqlGenerate);

		SqlGenerate Where(Expression expression, SqlGenerate sqlGenerate);

		SqlGenerate In(Expression expression, SqlGenerate sqlGenerate);

		SqlGenerate OrderBy(Expression expression, SqlGenerate sqlGenerate);

		SqlGenerate Max(Expression expression, SqlGenerate sqlGenerate);

		SqlGenerate Min(Expression expression, SqlGenerate sqlGenerate);

		SqlGenerate Avg(Expression expression, SqlGenerate sqlGenerate);

		SqlGenerate Count(Expression expression, SqlGenerate sqlGenerate);

		SqlGenerate Sum(Expression expression, SqlGenerate sqlGenerate);
	}
}

using System.Linq.Expressions;

namespace Overt.Core.Data.Expressions
{
    /// <summary>
    /// interface
    /// </summary>
    public interface ISqlExpression
    {
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="sqlGenerate"></param>
        /// <returns></returns>
		SqlGenerate Update(Expression expression, SqlGenerate sqlGenerate);
        /// <summary>
        /// Select
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="sqlGenerate"></param>
        /// <returns></returns>
		SqlGenerate Select(Expression expression, SqlGenerate sqlGenerate);
        /// <summary>
        /// Where
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="sqlGenerate"></param>
        /// <returns></returns>
		SqlGenerate Where(Expression expression, SqlGenerate sqlGenerate);
        /// <summary>
        /// In
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="sqlGenerate"></param>
        /// <returns></returns>
		SqlGenerate In(Expression expression, SqlGenerate sqlGenerate);
        /// <summary>
        /// OrderBy
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="sqlGenerate"></param>
        /// <returns></returns>
		SqlGenerate OrderBy(Expression expression, SqlGenerate sqlGenerate);
        /// <summary>
        /// Max
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="sqlGenerate"></param>
        /// <returns></returns>
		SqlGenerate Max(Expression expression, SqlGenerate sqlGenerate);
        /// <summary>
        /// Min
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="sqlGenerate"></param>
        /// <returns></returns>
		SqlGenerate Min(Expression expression, SqlGenerate sqlGenerate);
        /// <summary>
        /// Avg
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="sqlGenerate"></param>
        /// <returns></returns>
		SqlGenerate Avg(Expression expression, SqlGenerate sqlGenerate);
        /// <summary>
        /// Count
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="sqlGenerate"></param>
        /// <returns></returns>
		SqlGenerate Count(Expression expression, SqlGenerate sqlGenerate);
        /// <summary>
        /// Sum
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="sqlGenerate"></param>
        /// <returns></returns>
		SqlGenerate Sum(Expression expression, SqlGenerate sqlGenerate);
    }
}

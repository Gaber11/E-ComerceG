
namespace Persistance.Repositries
{
    internal static class SpecificationEvalutor
    {
        public static IQueryable<T> GetQuery<T>(IQueryable<T>inputQuery , Specifications<T> specifications) where T : class
        {
            var query = inputQuery;
            if (specifications.Criteria != null) query = query.Where(specifications.Criteria); //where
         
            //foreach (var item in specifications.IncludeExpressions) //include
            //{
            //    query = query.Include(item);
            //}

            query = specifications.IncludeExpressions.Aggregate(query,
                (currentQuery, includeExpression) => currentQuery.Include(includeExpression));
           if(specifications.OrderBy != null)
                query = query.OrderBy(specifications.OrderBy);
           else if (specifications.OrderByDescending is not null)
             query=query.OrderByDescending(specifications.OrderByDescending);
           if (specifications.IsPaginated)
                 query = query.Skip(specifications.Skip).Take(specifications.Take);
            return query;



        }
    }
}

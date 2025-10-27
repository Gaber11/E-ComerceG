

namespace Domain.Contracts
{
    public abstract class Specifications<T> where T : class
    {
        public Expression<Func<T, bool>>? Criteria { get; } //where
        public List<Expression<Func<T, object>>> IncludeExpressions { get; } = new();
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPaginated { get; private set; }

        protected Specifications(Expression<Func<T, bool>>? creteria)
         => Criteria = creteria;
        protected void AddInclude(Expression<Func<T, object>> expression)
      => IncludeExpressions.Add(expression);

        protected void SetOrderBy(Expression<Func<T, object>> expression) => OrderBy = expression;
        protected void SetOrderByDescending(Expression<Func<T, object>> expression) => OrderByDescending = expression;
        protected void ApplyPagination(int pageIndex, int pageSize)
        {
            IsPaginated = true;
            Skip = (pageIndex - 1) * pageSize;
            Take = pageSize;


        }
    }

    //where ==> Expression<func<T,bool>>
    //Include ==> List<Expression<func<T,object>>>
    //filter ===> BrandId , TypeId
    // Sort  ===> price[Asc,desc] , 
    //Skip  ===> (pageIndex -1) * pageSize
    //Take  ===> pageSize
}

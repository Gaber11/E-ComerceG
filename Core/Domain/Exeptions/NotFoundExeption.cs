
namespace Domain.Exeptions
{
    public abstract class NotFoundExeption : Exception
    {
        protected NotFoundExeption(string msg): base(msg)
        {
            
        }
    }
}


namespace Domain.Contracts
{
    public interface IDBInitializer
    {
        Task InitializeAsync();
        Task InitializeIdentityAsync();


    }
}

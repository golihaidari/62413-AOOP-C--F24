namespace WebAPI.Repositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>?> GetAll();
        Task<T?> Get(string id);        
        Task<bool> Create(T item);                
        Task<bool> Update(T item);
        Task<bool> Delete(string id);
    }
}

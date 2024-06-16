namespace MediBook.Domain.Abstractions;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> Get(Guid id);
    Task<T> Add(T entity);
    Task<bool> Delete(Guid id);
    Task<bool> Update(T entity);
}

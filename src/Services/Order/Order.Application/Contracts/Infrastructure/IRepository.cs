using Order.Core.Models;

namespace Order.Application.Contracts.Infrastructure
{
    public interface IRepository<T>
	{
		Task<IReadOnlyList<T>> GetAllAsync();
		Task<T> GetByIdAsync(int id);
		Task<T> AddAsync(T entity);
		Task UpdateAsync(T entity);
		Task DeleteAsync(int id);
	}
}
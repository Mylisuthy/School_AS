using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolAs.Domain.Common;

namespace SchoolAs.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id); // Soft delete handled in implementation
    }
}

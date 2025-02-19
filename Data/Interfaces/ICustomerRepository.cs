using Data.Entities;
using System.Linq.Expressions;

namespace Data.Interfaces;

public interface ICustomerRepository
{
    Task<CustomerEntity> CreateAsync(CustomerEntity entity);
    Task<bool> DeleteAsync(CustomerEntity entity);
    Task<IEnumerable<CustomerEntity>> GetAllAsync();
    Task<CustomerEntity?> GetOneAsync(Expression<Func<CustomerEntity, bool>> expression);
    Task<bool> UpdateOneAsync(Expression<Func<CustomerEntity, bool>> expression, CustomerEntity updatedEntity);
}

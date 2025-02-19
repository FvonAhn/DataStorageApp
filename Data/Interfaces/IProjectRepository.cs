using Data.Entities;
using System.Linq.Expressions;

namespace Data.Interfaces;

public interface IProjectRepository
{
    Task<ProjectEntity> CreateAsync(ProjectEntity entity);
    Task<bool> DeleteAsync(ProjectEntity entity);
    Task<IEnumerable<ProjectEntity>> GetAllAsync();
    Task<ProjectEntity?> GetOneAsync(Expression<Func<ProjectEntity, bool>> expression);
    Task<bool> UpdateOneAsync(Expression<Func<ProjectEntity, bool>> expression, ProjectEntity updatedEntity);
}
using Business.Models;

namespace Business.Interfaces;
public interface IProjectService
{
    Task<bool> CreateProjectAsync(ProjectRegistrationForm form);
    Task<bool> DeleteProjectByTitleAsync(string projectTitle);
    Task<IEnumerable<Project?>> GetProjectAsync();
    Task<Project?> GetProjectByTitleAsync(string projectTitle);
    Task<Project?> UpdateProjectAsync(string projecTitle, ProjectUpdateForm form);
}
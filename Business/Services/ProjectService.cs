using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Business.Services;
public class ProjectService(IProjectRepository projectRespository) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRespository;

    #region Crud

    // CREATE
    public async Task<bool> CreateProjectAsync(ProjectRegistrationForm form)
    {
        try
        {
            var projectEntity = ProjectFactory.Create(form);
            await _projectRepository.CreateAsync(projectEntity!);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    // READ
    public async Task<IEnumerable<Project?>> GetProjectAsync()
    {
        var projectEntities = await _projectRepository.GetAllAsync();
        return projectEntities.Select(ProjectFactory.Create);
    }

    public async Task<Project?> GetProjectByTitleAsync(string projectTitle)
    {
        var projectEntity = await _projectRepository.GetOneAsync(x => x.Title == projectTitle);
        return ProjectFactory.Create(projectEntity!);
    }

    // UPDATE

    public async Task<Project?> UpdateProjectAsync(string projecTitle, ProjectUpdateForm form)
    {
        var existingProject = await GetProjectEntityAsync(x => x.Title == projecTitle);
        if (existingProject == null)
            return null;

        existingProject.Title = string.IsNullOrWhiteSpace(form.Title) ? existingProject.Title : form.Title;
        existingProject.Description = string.IsNullOrWhiteSpace(form.Description) ? existingProject.Description : form.Description;
        if (form.EndDate.HasValue)
        {
            existingProject.EndDate = form.EndDate.Value;
        }

        var result = await _projectRepository.UpdateOneAsync(x => x.Title == projecTitle, existingProject);
        return result ? ProjectFactory.Create(existingProject) : null;
    }

    // DELETE

    public async Task<bool> DeleteProjectByTitleAsync(string projectTitle)
    {
        var project = await GetProjectEntityAsync(x => x.Title == projectTitle);
        if (project == null)
            return false;

        var result = await _projectRepository.DeleteAsync(project);
        return result;
    }

    #endregion

    private async Task<ProjectEntity?> GetProjectEntityAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        var project = await _projectRepository.GetOneAsync(expression);
        return project;
    }
}
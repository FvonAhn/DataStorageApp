using Business.Interfaces;
using Business.Services;
using Data.Context;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Dialogs;
using Presentation.Interfaces;

var services = new ServiceCollection()
    .AddDbContext<DataContext>(options => options.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\development\FvACkurs\DataStorageApp\Data\Databases\my_database_v2.mdf;Integrated Security=True;Connect Timeout=30"))
    .AddScoped<ICustomerRepository, CustomerRepository>()
    .AddScoped<ICustomerService, CustomerService>()
    .AddScoped<IProjectRepository, ProjectRepository>()
    .AddScoped<IProjectService, ProjectService>()
    .AddScoped<IMainMenuDialogs, MainMenuDialogs>()
    .BuildServiceProvider();

var menuDialogs = services.GetRequiredService<IMainMenuDialogs>();
await menuDialogs.MainMenuDialog();
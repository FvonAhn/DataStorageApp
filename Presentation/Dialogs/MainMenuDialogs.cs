using Business.Interfaces;
using Business.Models;
using Presentation.Interfaces;

namespace Presentation.Dialogs;
public class MainMenuDialogs(ICustomerService customerService, IProjectService projectService) : IMainMenuDialogs
{
    private readonly ICustomerService _customerService = customerService;
    private readonly IProjectService _projectService = projectService;


    public async Task MainMenuDialog()
    {
        var sant = true;

        while (sant)
        {
            Console.Clear();
            Console.WriteLine("### Menu ###");
            Console.WriteLine("1. Handle Customers");
            Console.WriteLine("2. Handle Projects");
            Console.WriteLine("3. Close application");
            Console.WriteLine("Select your option");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await CustomerOption();
                    break;

                case "2":
                    await ProjectOption();
                    break;
                case "3":
                    sant = false;
                    break;
                default:
                    break;
            }
        }
    }

    public async Task CustomerOption()
    {
        var sant = true;

        while (sant)
        {
            Console.Clear();
            Console.WriteLine("### Menu ###");
            Console.WriteLine("1. Create Customer");
            Console.WriteLine("2. Get Customers");
            Console.WriteLine("3. Update Customer");
            Console.WriteLine("4. Delete Customer");
            Console.WriteLine("5. Return to main menu");
            Console.WriteLine("Select your option");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await CreateCustomerOption();
                    break;

                case "2":
                    await GetCustomersOption();
                    break;

                case "3":
                    await UpdateCustomerOption();
                    break;

                case "4":
                    await DeleteCustomerOption();
                    break;
                case "5":
                    sant = false;
                    break;

                default:
                    break;
            }
        }
    }


    private async Task CreateCustomerOption()
    {
        Console.Clear();
        Console.WriteLine("### Create Customer ###");
        Console.Write("Customer Name: ");
        var customerName = Console.ReadLine()!;

        Console.Write("Your Contact: ");
        var customerContact = Console.ReadLine()!;

        Console.Write("Your Address: ");
        var customerAddress = Console.ReadLine()!;

        Console.Write("Your Email: ");
        var customerEmail = Console.ReadLine();

        var newCustomer = new CustomerRegistrationForm
        {
            CustomerName = customerName,
            CustomerContact = customerContact,
            CustomerAddress = customerAddress,
            CustomerEmail = customerEmail!
        };

        var result = await _customerService.CreateCustomerAsync(newCustomer);
        if (result)
        {
            Console.WriteLine("Customer was created successfully");
        }
        else
        {
            Console.WriteLine("Customer was not created");
        }

        Console.ReadKey();


    }

    private async Task GetCustomersOption()
    {
        Console.Clear();
        Console.WriteLine("### Get Customers ###");

        var customers = await _customerService.GetCustomersAsync();
        if (customers != null && customers.Any())
        {
            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer!.Id} - {customer!.CustomerName}. {customer.CustomerContact}. {customer.CustomerAddress}  <{customer.CustomerEmail}>");
            }
        }
        else
        {
            Console.WriteLine("No Customers found");
        }
        Console.ReadKey();
    }

    private async Task UpdateCustomerOption()
    {
        Console.Clear();
        Console.WriteLine("### Update Customer ###");

        var existingCustomer = await ShowAvailbleCustomersAndSelect();
        if (existingCustomer == null)
            return;


        Console.WriteLine($"Updating Customer: {existingCustomer.CustomerName} ");

        Console.Write($"New First Name (current: {existingCustomer.CustomerName}, leave empty to keep): ");
        var customerName = Console.ReadLine();

        Console.Write($"New Last Name (current: {existingCustomer.CustomerContact}, leave empty to keep): ");
        var customerContact = Console.ReadLine();

        Console.Write($"New Address (current: {existingCustomer.CustomerAddress}, leave empty to keep): ");
        var customerAddress = Console.ReadLine();

        Console.Write($"New Phone Number (current: {existingCustomer.CustomerEmail}, leave empty to keep): ");
        var customerEmail = Console.ReadLine();

        var updatedForm = new CustomerUpdateForm
        {
            CustomerName = customerName!,
            CustomerContact = customerContact!,
            CustomerAddress = customerAddress!,
            CustomerEmail = customerEmail!,
        };

        var result = await _customerService.UpdateCustomerAsync(existingCustomer.CustomerName, updatedForm);
        if (result != null)
        {
            Console.WriteLine("Customer was updated successfully");
        }
        else
        {
            Console.WriteLine("Customer was not updated");
        }

        Console.ReadKey();

    }


    private async Task DeleteCustomerOption()
    {
        Console.Clear();
        Console.WriteLine("### Delete Customer ###");

        var existingCustomer = await ShowAvailbleCustomersAndSelect();
        if (existingCustomer == null)
            return;

        Console.WriteLine($"Are you sure you want to delete Customer: {existingCustomer.CustomerName} {existingCustomer.CustomerContact} {existingCustomer.CustomerEmail} ? (y/n): ");
        var option = Console.ReadLine();

        if (option?.ToLower() == "y")
        {
            var result = await _customerService.DeleteCustomerByNameAsync(existingCustomer.CustomerName);

            if (result)
            {
                Console.WriteLine("Customer was deleted successfully");
            }
            else
            {
                Console.WriteLine("Customer was not deleted");
            }

            Console.ReadKey();

        }
        else
        {
            Console.WriteLine("Delete operation cancelled");
        }
        Console.ReadKey();
    }

    private async Task<Customer?> ShowAvailbleCustomersAndSelect()
    {
        var customers = await _customerService.GetCustomersAsync();
        if (customers == null || !customers.Any())
        {
            Console.WriteLine("No customers found to update");
            Console.ReadKey();
            return null;
        }

        Console.WriteLine("Available Customers: ");
        foreach (var customer in customers)
        {
            Console.WriteLine($"ID: {customer!.Id} {customer.CustomerName} {customer.CustomerContact}  <{customer.CustomerAddress}>  {customer.CustomerEmail}");
        }

        Console.Write("Enter Customer Id to manage:");
        if (!int.TryParse(Console.ReadLine(), out var customerId))
        {
            Console.WriteLine("Invalid Id");
            Console.ReadKey();
            return null;
        }

        var existingCustomer = customers.FirstOrDefault(x => x!.Id == customerId);
        if (existingCustomer == null)
        {
            Console.WriteLine("Customer not found");
            Console.ReadKey();
            return null;
        }

        return existingCustomer;
    }

    public async Task ProjectOption()
    {
        var sant = true;

        while (sant)
        {
            Console.Clear();
            Console.WriteLine("### Menu ###");
            Console.WriteLine("1. Create Project");
            Console.WriteLine("2. Get Projects");
            Console.WriteLine("3. Update Projects");
            Console.WriteLine("4. Delete Projects");
            Console.WriteLine("5. Return to main menu");
            Console.Write("Select your option");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await CreateProjectOption();
                    break;

                case "2":
                    await GetProjectsOption();
                    break;

                case "3":
                    await UpdateProjectOption();
                    break;

                case "4":
                    await DeleteProjectOption();
                    break;
                case "5":
                    sant = false;
                    break;

                default:
                    break;
            }
        }
    }


    private async Task CreateProjectOption()
    {
        Console.Clear();
        Console.WriteLine("### Create Project ###");
        Console.Write("Project Title: ");
        var title = Console.ReadLine()!;

        Console.Write("Description: ");
        var description = Console.ReadLine()!;

        Console.WriteLine($"Start Date {DateTime.Now:yyyy-MM-dd} ");
        Console.Write("Change start date? Leave empty to keep:");
        var startDateInput = Console.ReadLine()!;
        DateTime startDate = string.IsNullOrWhiteSpace(startDateInput) ? DateTime.Now : DateTime.Parse(startDateInput);

        Console.WriteLine($"End Date: {startDate.AddDays(30):yyyy-MM-dd} ");
        Console.Write("Change end date? Leave empty to keep:");
        var endDateInput = Console.ReadLine();
        DateTime endDate = string.IsNullOrWhiteSpace(endDateInput) ? startDate.AddDays(30) : DateTime.Parse(endDateInput);

        var newProject = new ProjectRegistrationForm
        {
            Title = title,
            Description = description,
            StartDate = startDate,
            EndDate = endDate!
        };

        var result = await _projectService.CreateProjectAsync(newProject);
        if (result)
        {
            Console.WriteLine("Project was created successfully");
        }
        else
        {
            Console.WriteLine("Project was not created");
        }

        Console.ReadKey();
    }

    private async Task GetProjectsOption()
    {
        Console.Clear();
        Console.WriteLine("### Get Projects ###");

        var projects = await _projectService.GetProjectAsync();
        if (projects != null && projects.Any())
        {
            foreach (var project in projects)
            {
                Console.WriteLine($"{project!.Title} {project.Description}  <{project.EndDate}>");
            }
        }
        else
        {
            Console.WriteLine("No Projects found");
        }
        Console.ReadKey();
    }

    private async Task UpdateProjectOption()
    {
        Console.Clear();
        Console.WriteLine("### Update Projects ###");

        var existingProject = await ShowAvailableProjectsAndSelect();
        if (existingProject == null)
            return;


        Console.WriteLine($"Updating Project: {existingProject.Title} ");

        Console.Write($"New Title (current: {existingProject.Title}, leave empty to keep): ");
        var title = Console.ReadLine();

        Console.Write($"New Description (current: {existingProject.Description}, leave empty to keep): ");
        var description = Console.ReadLine();

        Console.Write($"New End Date (current: {existingProject.EndDate}, leave empty to keep): ");
        var endDateInput = Console.ReadLine();
        DateTime endDate = string.IsNullOrWhiteSpace(endDateInput) ? DateTime.Now : DateTime.Parse(endDateInput!);

        var updatedForm = new ProjectUpdateForm
        {
            Title = title!,
            Description = description!,
            EndDate = endDate!,
        };

        var result = await _projectService.UpdateProjectAsync(existingProject.Title, updatedForm);
        if (result != null)
        {
            Console.WriteLine("Customer was updated successfully");
        }
        else
        {
            Console.WriteLine("Customer was not updated");
        }

        Console.ReadKey();

    }


    private async Task DeleteProjectOption()
    {
        Console.Clear();
        Console.WriteLine("### Delete Project ###");

        var existingProject = await ShowAvailableProjectsAndSelect();
        if (existingProject == null)
            return;

        Console.WriteLine($"Are you sure you want to delete Project: {existingProject.Title} ? (y/n): ");
        var option = Console.ReadLine();

        if (option?.ToLower() == "y")
        {
            var result = await _projectService.DeleteProjectByTitleAsync(existingProject.Title);

            if (result)
            {
                Console.WriteLine("Project was deleted successfully");
            }
            else
            {
                Console.WriteLine("Project was not deleted");
            }

            Console.ReadKey();

        }
        else
        {
            Console.WriteLine("Delete operation cancelled");
        }
        Console.ReadKey();
    }

    private async Task<Project?> ShowAvailableProjectsAndSelect()
    {
        var projects = await _projectService.GetProjectAsync();
        if (projects == null || !projects.Any())
        {
            Console.WriteLine("No Projects found to update");
            Console.ReadKey();
            return null;
        }

        Console.WriteLine("Available Projects: ");
        foreach (var project in projects)
        {
            Console.WriteLine($"ID: {project!.Id} {project.Title} {project.Description}  <{project.StartDate}>  {project.EndDate}");
        }

        Console.Write("Enter Project Id to manage:");
        if (!int.TryParse(Console.ReadLine(), out var projectId))
        {
            Console.WriteLine("Invalid Id");
            Console.ReadKey();
            return null;
        }

        var existingProject = projects.FirstOrDefault(x => x!.Id == projectId);
        if (existingProject == null)
        {
            Console.WriteLine("Project not found");
            Console.ReadKey();
            return null;
        }

        return existingProject;
    }
}
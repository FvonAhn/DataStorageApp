using Business.Models;
using Business.Factories;
using Data.Entities;
using System.Linq.Expressions;
using Data.Interfaces;
using Business.Interfaces;
using System.Diagnostics;

namespace Business.Services;
public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    #region Crud

    // CREATE
    public async Task<bool> CreateCustomerAsync(CustomerRegistrationForm form)
    {
        try
        {
            var customerEntity = CustomerFactory.Create(form);
            await _customerRepository.CreateAsync(customerEntity!);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    // READ
    public async Task<IEnumerable<Customer?>> GetCustomersAsync()
    {
        var customerEntities = await _customerRepository.GetAllAsync();
        return customerEntities.Select(CustomerFactory.Create);
    }

    public async Task<Customer?> GetCustomerByCustomerNameAsync(string customerName)
    {
        var customerEntity = await _customerRepository.GetOneAsync(x => x.CustomerName == customerName);
        return CustomerFactory.Create(customerEntity!);
    }

    // UPDATE

    public async Task<Customer?> UpdateCustomerAsync(string customerName, CustomerUpdateForm form)
    {
        var existingCustomer = await GetCustomerEntityAsync(x => x.CustomerName == customerName);
        if (existingCustomer == null)
            return null;

        existingCustomer.CustomerName = string.IsNullOrWhiteSpace(form.CustomerName) ? existingCustomer.CustomerName : form.CustomerName;
        existingCustomer.CustomerContact = string.IsNullOrWhiteSpace(form.CustomerContact) ? existingCustomer.CustomerContact : form.CustomerContact;
        existingCustomer.CustomerAddress = string.IsNullOrWhiteSpace(form.CustomerAddress) ? existingCustomer.CustomerAddress : form.CustomerAddress;
        existingCustomer.CustomerEmail = string.IsNullOrWhiteSpace(form.CustomerEmail) ? existingCustomer.CustomerEmail : form.CustomerEmail;

        var result = await _customerRepository.UpdateOneAsync(x => x.CustomerName == customerName, existingCustomer);
        return result ? CustomerFactory.Create(existingCustomer) : null;
    }

    // DELETE

    public async Task<bool> DeleteCustomerByNameAsync(string customerName)
    {
        var customer = await GetCustomerEntityAsync(x => x.CustomerName == customerName);
        if (customer == null)
            return false;

        var result = await _customerRepository.DeleteAsync(customer);
        return result;
    }

    #endregion

    private async Task<CustomerEntity?> GetCustomerEntityAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        var customer = await _customerRepository.GetOneAsync(expression);
        return customer;
    }
}

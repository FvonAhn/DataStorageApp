using Business.Models;

namespace Business.Interfaces;
public interface ICustomerService
{
    Task<bool> CreateCustomerAsync(CustomerRegistrationForm form);
    Task<bool> DeleteCustomerByNameAsync(string customerName);
    Task<Customer?> GetCustomerByCustomerNameAsync(string customerName);
    Task<IEnumerable<Customer?>> GetCustomersAsync();
    Task<Customer?> UpdateCustomerAsync(string customerName, CustomerUpdateForm form);
}
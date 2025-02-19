using Business.Models;
using Data.Entities;

namespace Business.Factories;
public static class CustomerFactory
{
    public static CustomerEntity? Create(CustomerRegistrationForm form) => form == null ? null : new()
    {
        CustomerName = form.CustomerName,
        CustomerContact = form.CustomerContact,
        CustomerAddress = form.CustomerAddress,
        CustomerEmail = form.CustomerEmail,
    };

    public static Customer? Create(CustomerEntity entity) => entity == null ? null : new()
    {
        Id = entity.Id,
        CustomerName = entity.CustomerName,
        CustomerContact = entity.CustomerContact,
        CustomerAddress = entity.CustomerAddress,
        CustomerEmail = entity.CustomerEmail,
    };
}


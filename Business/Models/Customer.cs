namespace Business.Models;
public class Customer
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = null!;
    public string CustomerContact { get; set; } = null!;
    public string CustomerAddress { get; set; } = null!;
    public string CustomerEmail { get; set; } = null!;
}

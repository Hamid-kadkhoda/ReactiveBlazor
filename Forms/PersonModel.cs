
namespace ReactiveBlazor;

public class PersonModel
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public AddressModel Address { get; set; } = new();
    public string Notes { get; set; } = string.Empty;
    public bool IsSubscribed { get; set; }
    public DateTime? BirthDate { get; set; }
}

public class AddressModel
{
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Zip { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
}

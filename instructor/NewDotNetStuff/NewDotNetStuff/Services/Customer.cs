
namespace NewDotNetStuff.Services;

public record Customer
{

    public string FirstName { get; init; } = string.Empty;
    public string? LastName { get; init; }

    public int Age { get; init; }
   
   public ContactInformation GetContactInformation()
    {
        return new ContactInformation($"{FirstName.ToLower()}@Company.Com");
    }
}


// in .NET 8, coming to classes as well, known as "Primary Constructors"
public record ContactInformation(string EmailAddress)
{
    //public string EmailAddress { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
}
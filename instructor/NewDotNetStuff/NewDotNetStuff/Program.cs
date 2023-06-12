


using NewDotNetStuff.Services;

Console.WriteLine("Hello, World!");


var bob = new Customer()
{
    FirstName = "Bob",
    LastName = "Smith"
};

var bob2 = new Customer()
{
    FirstName = "Bob",
    LastName = "Smith"
};


if(bob == bob2)
{
    Console.WriteLine("They Are The Same");
} else
{
    Console.WriteLine("They are not the same!");
}

var contact = bob.GetContactInformation();
Console.WriteLine(contact.EmailAddress);

var newBob = bob with { FirstName = "Robert", Age=64 };

Console.WriteLine(bob.FirstName);
Console.WriteLine(newBob.FirstName);
Console.WriteLine(newBob.LastName);

var myBankAccount = new BankStatement(123.22M);

var name = "Jeff";
var updatedName = name.ToUpper();
Console.WriteLine(name);
Console.WriteLine(updatedName);

public record BankStatement(decimal Balance);



using Bogus;
using Bogus.Extensions.Belgium;
using FakeDataGenerator;
using Microsoft.EntityFrameworkCore;

int n = 0;
string GenerateNewPhoneNumber()
{

    return "";
}

IEnumerable<Users> GenerateFakeData()
{


    var persons = new Faker<Users>()
        .RuleFor(x => x.User_Id, f => Guid.NewGuid())
        .RuleFor(x => x.National_Id, f => f.Person.NationalNumber())
        .RuleFor(x => x.First_Name, f => f.Name.FirstName())
        .RuleFor(x => x.Last_Name, f => f.Name.LastName())
        .RuleFor(x => x.Address, f => f.Address.FullAddress())
        .RuleFor(x => x.Birth_Date, f => DateOnly.FromDateTime(f.Person.DateOfBirth))
        .RuleFor(x => x.Phone_Number, f => f.Phone.PhoneNumberFormat())
        .Generate(1000000).ToList();
    return persons;
}


Console.WriteLine("generating 10000000 item ...");
var items = GenerateFakeData();


Console.WriteLine($"{items.Count()} items generated!");


var dbcontext = new UserContext();

await dbcontext.Users.AddRangeAsync(items);
Console.WriteLine("saving in db...");
await dbcontext.SaveChangesAsync();


Console.WriteLine("items Stored in Db!");

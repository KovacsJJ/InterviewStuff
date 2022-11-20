using Newtonsoft.Json;
using SimpleDataManagement.Models;
using System.Collections.Generic;
using System;

var dataSourcesDirectory = Path.Combine(Environment.CurrentDirectory, "DataSources");
var personsFilePath = Path.Combine(dataSourcesDirectory, "Persons_20220824_00.json");
var organizationsFilePath = Path.Combine(dataSourcesDirectory, "Organizations_20220824_00.json");
var vehiclesFilePath = Path.Combine(dataSourcesDirectory, "Vehicles_20220824_00.json");
var addressesFilePath = Path.Combine(dataSourcesDirectory, "Addresses_20220824_00.json");

//Quick test to ensure that all files are where they should be :)
foreach (var path in new[] { personsFilePath, organizationsFilePath, vehiclesFilePath, addressesFilePath })
{
    if (!File.Exists(path))
        throw new FileNotFoundException(path);
}

//TODO: Start your exercise here. Do not forget about answering Test #9 (Handled slightly different)
// Reminder: Collect the data from each file. Hint: You could use Newtonsoft's JsonConvert or Microsoft's JsonSerializer

//Read Person
string personJson = File.ReadAllText(personsFilePath);
var persons = JsonConvert.DeserializeObject<List<Person>>(personJson);

//Read Organization
string organizationJson = File.ReadAllText(organizationsFilePath);
var organizations = JsonConvert.DeserializeObject<List<Organization>>(organizationJson);

//Read Veichles
string vehiclesJson = File.ReadAllText(vehiclesFilePath);
var vehicles = JsonConvert.DeserializeObject<List<Vehicle>>(vehiclesJson);

//Read Addresses
string addressesJson = File.ReadAllText(addressesFilePath);
var addresses = JsonConvert.DeserializeObject<List<Address>>(addressesJson);

//throw new NotImplementedException("Get data from file");


//Test #1: Do all files have entities? (True / False)



//throw new NotImplementedException("Complete Test #1");
bool isEmpty = true;
if (persons.Count == 0)
{
    isEmpty = false;
}
if (organizations.Count == 0)
{
    isEmpty = false;
}
if (vehicles.Count == 0)
{
    isEmpty = false;
}
if (addresses.Count == 0)
{
    isEmpty = false;
}
Console.WriteLine(isEmpty);


//Test #2: What is the total count for all entities?

//throw new NotImplementedException("Complete Test #2");
var totalCount = persons.Count + organizations.Count + vehicles.Count + addresses.Count;
Console.WriteLine(totalCount);

//Test #3: What is the count for each type of Entity? Person, Organization, Vehicle, and Address
//throw new NotImplementedException("Complete Test #3");
Console.WriteLine("Persons: ", persons.Count);
Console.WriteLine("organizations: ", organizations.Count);
Console.WriteLine("Vehicles: ", vehicles.Count);
Console.WriteLine("addresses: ", addresses.Count);


//Test #4: Provide a breakdown of entities which have associations in the following manor:
//         -Per Entity Count
//         - Total Count
//throw new NotImplementedException("Complete Test #4");



int personCount = 0;
foreach(Person person in persons)
{
    if (person.Associations.Count > 0)
        {
            personCount += 1;
        }
}

int organizaionCount = 0;
foreach (Organization org in organizations)
{
    if (org.Associations.Count > 0)
    {
        organizaionCount += 1;
    }
}

int vehicleCount = 0;
foreach (Vehicle vehicle in vehicles)
{
    if (vehicle.Associations.Count > 0)
    {
        vehicleCount += 1;
    }
}

int addressesCount = 0;
foreach (Address a in addresses)
{
    if (a.Associations.Count > 0)
    {
        addressesCount += 1;
    }
}
int total = personCount + organizaionCount + vehicleCount + addressesCount;


//Print
Console.WriteLine("Per Entity Count:");
Console.WriteLine("Person: ", personCount);
Console.WriteLine("Organizations: ", organizaionCount);
Console.WriteLine("Vehicles: ", vehicleCount);
Console.WriteLine("Addresses: ", addressesCount);
Console.WriteLine("Total Count: ", totalCount);


//Test #5: Provide the vehicle detail that is associated to the address "4976 Penelope Via South Franztown, NH 71024"?
//         StreetAddress: "4976 Penelope Via"
//         City: "South Franztown"
//         State: "NH"
//         ZipCode: "71024"

Vehicle v = new Vehicle();
bool vehicleFound = false;
foreach (Vehicle vehicle in vehicles)
{
    foreach (Association assec in vehicle.Associations)
    {
        if (assec.EntityType.Equals("Address"))
        {
            foreach (Address address in addresses)
            {
                if (address.EntityId.Equals(assec.EntityId) && address.StreetAddress.Equals("4976 Penelope Via") && address.City.Equals("South Franztown")
                    && address.State.Equals("NH") && address.ZipCode.Equals("71024"))
                {
                    v = vehicle;
                    vehicleFound = true;
                    break;
                }
            }
        }
        if (vehicleFound) break;
    }
    if(vehicleFound) break;
}

Console.WriteLine("Id: ", v.EntityId);
Console.WriteLine("Make: ", v.Make);
Console.WriteLine("Model: ", v.Model);
Console.WriteLine("Year: ", v.Year);
Console.WriteLine("Plate: ", v.PlateNumber);
Console.WriteLine("State: ", v.State);
Console.WriteLine("Vin: ", v.Vin);

//throw new NotImplementedException("Complete Test #5");

//Test #6: What person(s) are associated to the organization "thiel and sons"?
//throw new NotImplementedException("Complete Test #6");

List<String> personIds = new List<string>();
foreach (Organization org in organizations)
{
    if (org.Name.Equals("thiel and sons"))
    {
        foreach (Association assoc in org.Associations)
        {
            if (assoc.EntityType.Equals("person"))
            {
                personIds.Add(assoc.EntityId);
            }
        }
    }
    break;
}

if (personIds.Count > 0)
{
    foreach (Person person in persons)
    {
        if (personIds.Contains(person.EntityId))
        {
            Console.WriteLine(person.FirstName, " ", person.LastName);
        }
    }
}
else
{
    Console.WriteLine("None");
}
//Test #7: How many people have the same first and middle name?
//throw new NotImplementedException("Complete Test #7");

int counter = 0;
foreach (Person person in persons)
{
    if (person.FirstName.Equals(person.MiddleName))
    {
        counter += 1;
    }
}
Console.WriteLine(counter);
//Test #8: Provide a breakdown of entities where the EntityId contains "B3" in the following manor:
//         -Total count by type of Entity
//         - Total count of all entities
//throw new NotImplementedException("Complete Test #8");

int pCount = 0;
int vCount =0;
int oCount= 0;
int aCount=0;
foreach (Person person in persons)
{
    if (person.EntityId.Contains("B3"))
    {
        pCount += 1;
    }
}
foreach (Organization organization in organizations)
{
    if (organization.EntityId.Contains("B3"))
    {
        oCount += 1;
    }
}

foreach (Vehicle vehicle in vehicles)
{
    if (vehicle.EntityId.Contains("B3"))
    {
        vCount += 1;
    }
}
foreach (Address address in addresses)
{
    if (address.EntityId.Contains("B3"))
    {
        aCount += 1;
    }
}

/*#9: I would likely try and put the reading the files in a seperate file and call it from
 * the main program.cs one. I would also want to make it so that I'm able to put all the files into one 
 * multilevel list (There's probably a way to do this but the methods I tried were not working so I used four seperate lists)
 * rather than four seperate lists.
 * 
 */
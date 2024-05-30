using System;
using System.Collections.Generic;

namespace Agri_Energy_Connect.Models;

public partial class Employee
{
    public string Id { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string AdminAddress { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string Gender { get; set; } = null!;

    public Employee(string id, string firstName, string lastName, string adminAddress, string phoneNumber, string email, DateOnly dateOfBirth, string gender)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        AdminAddress = adminAddress;
        PhoneNumber = phoneNumber;
        Email = email;
        DateOfBirth = dateOfBirth;
        Gender = gender;
    }
}

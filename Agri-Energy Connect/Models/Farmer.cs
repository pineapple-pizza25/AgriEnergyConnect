using System;
using System.Collections.Generic;

namespace Agri_Energy_Connect.Models;

public partial class Farmer
{
    public string Id { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string FarmerAddress { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string Gender { get; set; } = null!;

    public int FarmerCategoryId { get; set; }

    public virtual FarmerCategory FarmerCategory { get; set; } = null!;

    public virtual ICollection<Farm> Farms { get; set; } = new List<Farm>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();


    public Farmer(string id, string firstName, string lastName, string farmerAddress, 
        string phoneNumber, string email, DateOnly dateOfBirth, string gender)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        FarmerAddress = farmerAddress;
        PhoneNumber = phoneNumber;
        Email = email;
        DateOfBirth = dateOfBirth;
        Gender = gender;
        
    }
}

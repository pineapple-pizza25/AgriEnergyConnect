using System;
using System.Collections.Generic;

namespace Agri_Energy_Connect.Models;

public partial class Product
{
    public int Id { get; set; }

    public string ProductName { get; set; } = null!;

    public string? Details { get; set; }

    public decimal Price { get; set; }

    public DateOnly ProductionDate { get; set; }

    public int Quantity { get; set; }

    public string Unit { get; set; } = null!;

    public DateOnly? ExpirationDate { get; set; }

    public string FarmerId { get; set; } = null!;

    public int ProductCategoryId { get; set; }

    public virtual Farmer Farmer { get; set; } = null!;

    public virtual ProductCategory ProductCategory { get; set; } = null!;
}

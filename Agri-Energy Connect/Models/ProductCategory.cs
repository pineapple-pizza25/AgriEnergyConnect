using System;
using System.Collections.Generic;

namespace Agri_Energy_Connect.Models;

public partial class ProductCategory
{
    public int Id { get; set; }

    public string CategoryName { get; set; } = null!;

    public string Details { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

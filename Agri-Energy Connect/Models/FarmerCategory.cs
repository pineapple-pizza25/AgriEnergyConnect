using System;
using System.Collections.Generic;

namespace Agri_Energy_Connect.Models;

public partial class FarmerCategory
{
    public int Id { get; set; }

    public string CategoryName { get; set; } = null!;

    public string Details { get; set; } = null!;

    public virtual ICollection<Farmer> Farmers { get; set; } = new List<Farmer>();
}

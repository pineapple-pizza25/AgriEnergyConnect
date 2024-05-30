using System;
using System.Collections.Generic;

namespace Agri_Energy_Connect.Models;

public partial class Farm
{
    public int Id { get; set; }

    public string FarmerId { get; set; } = null!;

    public string FarmName { get; set; } = null!;

    public string? FarmDescription { get; set; }

    public string FarmAddress { get; set; } = null!;

    public virtual Farmer Farmer { get; set; } = null!;
}

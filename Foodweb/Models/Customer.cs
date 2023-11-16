using System;
using System.Collections.Generic;

namespace Foodweb.Models;

public partial class Customer
{
    public int CusId { get; set; }

    public string? FullName { get; set; }

    public DateTime? Birthday { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public int? LocaId { get; set; }

    public virtual Location? Loca { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}

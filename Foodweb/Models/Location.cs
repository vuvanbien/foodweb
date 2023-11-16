using System;
using System.Collections.Generic;

namespace Foodweb.Models;

public partial class Location
{
    public int LocaId { get; set; }

    public string? LocaName { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}

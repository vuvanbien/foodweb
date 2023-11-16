using System;
using System.Collections.Generic;

namespace Foodweb.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? CusId { get; set; }

    public DateTime? OrderDate { get; set; }

    public DateTime? ShipDate { get; set; }

    public double? TotalMoney { get; set; }

    public string? Note { get; set; }

    public string? Address { get; set; }

    public bool? Deleted { get; set; }

    public virtual Customer? Cus { get; set; }

    public virtual OrderDetail? OrderDetail { get; set; }
}

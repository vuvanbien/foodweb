using System;
using System.Collections.Generic;

namespace Foodweb.Models;

public partial class Shipper
{
    public int ShipId { get; set; }

    public string? ShipName { get; set; }

    public string? Phone { get; set; }

    public string? Company { get; set; }

    public DateTime? ShipDate { get; set; }
}

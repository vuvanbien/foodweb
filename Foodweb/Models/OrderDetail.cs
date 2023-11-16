using System;
using System.Collections.Generic;

namespace Foodweb.Models;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public int? OrderId { get; set; }

    public int? ProId { get; set; }

    public string? ProName { get; set; }

    public int? Amount { get; set; }

    public int? Discount { get; set; }

    public double? TotalMoney { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual Order OrderDetailNavigation { get; set; } = null!;
}

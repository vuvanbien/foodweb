using System;
using System.Collections.Generic;

namespace Foodweb.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public string? Phone { get; set; }

    public string? Password { get; set; }

    public string? FullName { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? RoleId { get; set; }

    public bool? Active { get; set; }

    public DateTime? LastLogin { get; set; }

    public virtual Role? Role { get; set; }
}

using System;
using System.Collections.Generic;

namespace Foodweb.Models;

public partial class Category
{
    public int CateId { get; set; }

    public string? CateName { get; set; }

    public int? Status { get; set; }

    public int? Sort { get; set; }

    public int? ParentId { get; set; }

    public string? Metakeyword { get; set; }

    public string? MetaDesc { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

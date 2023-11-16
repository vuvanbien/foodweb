using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foodweb.Models;

public partial class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProId { get; set; }

    public string? ProName { get; set; }

    public string? ShortDesc { get; set; }

    public double? Price { get; set; }

    public int? Discout { get; set; }

    public int? CateId { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? MetaKey { get; set; }

    public string? MetaDesc { get; set; }

    public int? BestSeller { get; set; }

    public string? Thumb { get; set; }

    public bool? Active { get; set; }

    public string? Title { get; set; }

    public virtual Category? Cate { get; set; }
     
}

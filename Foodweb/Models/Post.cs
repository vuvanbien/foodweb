using System;
using System.Collections.Generic;

namespace Foodweb.Models;

public partial class Post
{
    public int PostId { get; set; }

    public string? Title { get; set; }

    public string? Contents { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? AccountId { get; set; }

    public int? CateId { get; set; }

    public bool? Hot { get; set; }

    public bool? NewFeed { get; set; }

    public string? MetaKey { get; set; }

    public string? MetaDesc { get; set; }
}

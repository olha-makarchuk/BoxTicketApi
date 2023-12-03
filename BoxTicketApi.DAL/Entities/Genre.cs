using BoxTicketApi.DAL.Entities.Base;
using System;
using System.Collections.Generic;

namespace BoxTicketApi.DAL.Entities;

public partial class Genre : BaseEntity
{
    public string NameGenre { get; set; } = null!;

    public virtual ICollection<Performance> Performances { get; set; } = new List<Performance>();
}

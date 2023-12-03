using BoxTicketApi.DAL.Entities.Base;
using System;
using System.Collections.Generic;

namespace BoxTicketApi.DAL.Entities;

public partial class Author : BaseEntity
{
    public string FirstName { get; set; } = null!;

    public string MiddleName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public virtual ICollection<Performance> Performances { get; set; } = new List<Performance>();
}

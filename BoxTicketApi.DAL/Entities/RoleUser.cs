using BoxTicketApi.DAL.Contexts;
using BoxTicketApi.DAL.Entities.Base;
using System;
using System.Collections.Generic;

namespace BoxTicketApi.DAL.Entities;

public partial class RoleUser : BaseEntity
{
    public string NameRole { get; set; } = null!;

    public virtual ICollection<UserAccount> UserAccounts { get; set; } = new List<UserAccount>();
}

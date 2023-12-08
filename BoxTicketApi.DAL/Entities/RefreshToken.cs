using BoxTicketApi.DAL.Contexts;
using BoxTicketApi.DAL.Entities.Base;
using System;
using System.Collections.Generic;

namespace BoxTicketApi.DAL.Entities;

public partial class RefreshToken:BaseEntity
{
    public string? Token { get; set; }
    public DateTime? Expires { get; set; }
    public int IdUser { get; set; }
    public virtual UserAccount IdUserNavigation { get; set; } = null!;
}

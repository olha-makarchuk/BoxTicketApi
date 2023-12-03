using BoxTicketApi.DAL.Entities.Base;
using System;
using System.Collections.Generic;

namespace BoxTicketApi.DAL.Entities;

public partial class TypeOfTicket : BaseEntity
{
    public string TypeName { get; set; } = null!;

    public virtual ICollection<AllTicket> AllTickets { get; set; } = new List<AllTicket>();
}

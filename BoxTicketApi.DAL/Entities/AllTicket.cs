using BoxTicketApi.DAL.Entities.Base;
using System;
using System.Collections.Generic;

namespace BoxTicketApi.DAL.Entities;

public partial class AllTicket : BaseEntity
{
    public int IdType { get; set; }

    public int IdPerformance { get; set; }

    public int CoutOfTickets { get; set; }

    public int Price { get; set; }

    public virtual Performance IdPerformanceNavigation { get; set; } = null!;

    public virtual TypeOfTicket IdTypeNavigation { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}

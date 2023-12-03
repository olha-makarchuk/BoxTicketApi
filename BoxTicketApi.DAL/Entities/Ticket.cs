using BoxTicketApi.DAL.Contexts;
using BoxTicketApi.DAL.Entities.Base;
using System;
using System.Collections.Generic;

namespace BoxTicketApi.DAL.Entities;

public partial class Ticket : BaseEntity
{
    public int IdAllTickets { get; set; }

    public int IdUser { get; set; }

    public int IdPerformance { get; set; }

    public int IdStatus { get; set; }

    public int SeatNumber { get; set; }

    public virtual AllTicket IdAllTicketsNavigation { get; set; } = null!;

    public virtual Performance IdPerformanceNavigation { get; set; } = null!;

    public virtual StatusTicket IdStatusNavigation { get; set; } = null!;

    public virtual UserAccount IdUserNavigation { get; set; } = null!;
}

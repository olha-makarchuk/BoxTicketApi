using BoxTicketApi.DAL.Entities.Base;
using System;
using System.Collections.Generic;

namespace BoxTicketApi.DAL.Entities;

public partial class Performance : BaseEntity
{
    public string PerformanceName { get; set; } = null!;

    public int IdGenre { get; set; }

    public int IdAuthor { get; set; }

    public DateTime DateTimeEvent { get; set; }

    public virtual ICollection<AllTicket> AllTickets { get; set; } = new List<AllTicket>();

    public virtual Author IdAuthorNavigation { get; set; } = null!;

    public virtual Genre IdGenreNavigation { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}

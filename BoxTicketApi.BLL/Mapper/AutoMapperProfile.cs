using AutoMapper;
using BoxTicketApi.BLL.Responses.Auth;
using BoxTicketApi.BLL.Responses.Author;
using BoxTicketApi.BLL.Responses.Genre;
using BoxTicketApi.BLL.Responses.Performance;
using BoxTicketApi.BLL.Responses.Ticket;
using BoxTicketApi.BLL.Responses.TicketOptions;
using BoxTicketApi.DAL.Contexts;
using BoxTicketApi.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.BLL.Mapper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Author, AuthorResponse>();
            CreateMap<Genre, GenreResponse>();
            CreateMap<Performance, PerformanceResponse>();
            CreateMap<AllTicket, OptionsResponse>();
            CreateMap<AllTicket, OptionsResponse>();
            CreateMap<Ticket, TicketIdResponse>();
            CreateMap<int, TicketIdResponse>();
        }
    }
}

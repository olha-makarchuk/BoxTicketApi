using AutoMapper;
using BoxTicketApi.BLL.Requests.Auth;
using BoxTicketApi.BLL.Requests.Genre;
using BoxTicketApi.BLL.Requests.Performance;
using BoxTicketApi.BLL.Requests.Ticket;
using BoxTicketApi.BLL.Requests.TicketOptions;
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

            CreateMap<Genre, GenreResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(scr => scr.Id));

            CreateMap<GenreRequest, Genre>();

            CreateMap<Performance, PerformanceResponse>()
                .ForMember(dest => dest.IdPerformance, opt => opt.MapFrom(scr => scr.Id));

            CreateMap<PerformanceRequest, Performance>();

            CreateMap<AllTicket, OptionsResponse>()
                .ForMember(dest => dest.NamePerformance, opt => opt.MapFrom(scr => scr.IdPerformanceNavigation.PerformanceName))
                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(scr => scr.IdTypeNavigation.TypeName));

            CreateMap<Ticket, TicketIdResponse>()
                .ForMember(dest => dest.idTicket, opt => opt.MapFrom(scr => scr.Id));

            CreateMap<TicketByIdReqest, TicketIdResponse>();

            CreateMap<Ticket, TicketResponse>()
                .ForMember(dest => dest.IdTicketOptions, opt => opt.MapFrom(scr => scr.IdAllTickets))
                .ForMember(dest => dest.IdPerformance, opt => opt.MapFrom(scr => scr.IdAllTicketsNavigation.IdPerformance))
                .ForMember(dest => dest.Performance, opt => opt.MapFrom(scr => scr.IdAllTicketsNavigation.IdPerformanceNavigation.PerformanceName))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(scr => scr.IdStatusNavigation.StatusName))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(scr => scr.IdAllTicketsNavigation.Price));

            CreateMap<SignUpRequest, AuthResponse>();

            CreateMap<SignUpRequest, TokenResponse>();

            CreateMap<Ticket, TicketIdResponse>()
            .ForMember(dest => dest.idTicket, opt => opt.MapFrom(src => src.Id));

        }
    }
}

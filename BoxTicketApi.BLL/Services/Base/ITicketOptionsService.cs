using BoxTicketApi.BLL.Requests.TicketOptions;
using BoxTicketApi.BLL.Responses.TicketOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.BLL.Services.Base
{
    public interface ITicketOptionsService
    {
        Task<List<OptionsResponse>> GetAllAvailableTypes(GetOptionsRequest request);
        Task<List<OptionsResponse>> GetAllAvailableSeats(GetSeatsRequest request);
    }
}

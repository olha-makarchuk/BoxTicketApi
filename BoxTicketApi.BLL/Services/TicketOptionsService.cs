using BoxTicketApi.BLL.Requests.TicketOptions;
using BoxTicketApi.BLL.Responses.TicketOptions;
using BoxTicketApi.BLL.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.BLL.Services
{
    public class TicketOptionsService : ITicketOptionsService
    {
        public Task<List<OptionsResponse>> GetAllAvailableSeats(GetSeatsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<List<OptionsResponse>> GetAllAvailableTypes(GetOptionsRequest request)
        {
            throw new NotImplementedException();
        }
    }
}

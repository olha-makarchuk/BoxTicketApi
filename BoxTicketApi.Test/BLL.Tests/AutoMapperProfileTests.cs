using AutoMapper;
using BoxTicketApi.BLL.Mapper;
using BoxTicketApi.BLL.Requests.Genre;
using BoxTicketApi.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.Test.BLL.Tests
{
    public class AutoMapperProfileTests
    {
        private readonly IMapper _mapper;

        public AutoMapperProfileTests()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public void Map_GenreRequest_To_Genre()
        {
            var genreRequest = new GenreRequest();

            var mappedGenre = _mapper.Map<GenreRequest, Genre>(genreRequest);

            Assert.NotNull(mappedGenre);
        }
    }
}

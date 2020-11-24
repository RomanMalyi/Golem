using AutoMapper;
using Golem.Core.Models.Dto.Responses;
using Golem.Data.PostgreSql.Models;

namespace Golem.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserResponse>();
            CreateMap<Query, QueryResponse>();

            CreateMap<Session, SessionResponse>();
        }
    }
}

using AutoMapper;
using DotnetChat.Core.Models;
using DotnetChat.Data.Entities;

namespace DotnetChat.Service.Mapping
{
    public class DataModelMappingProfile : Profile
    {
        public DataModelMappingProfile()
        {
            CreateMap<MessageRequest, Message>().ReverseMap();
            CreateMap<MessageResponse, Message>().ReverseMap();
            CreateMap<UserRequest, User>().ReverseMap();
            CreateMap<UserResponse, User>().ReverseMap();
        }
    }
}

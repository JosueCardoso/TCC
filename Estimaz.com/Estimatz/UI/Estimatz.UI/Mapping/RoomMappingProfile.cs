using AutoMapper;
using Estimatz.Commands.Room.SaveRoom;
using Estimatz.Entities.Room;
using Estimatz.Entities.UserStory;
using Estimatz.UI.Models;

namespace Estimatz.UI.Mapping
{
    public class RoomMappingProfile : Profile
    {
        public RoomMappingProfile()
        {
            CreateMap<RoomModel, SaveRoomCommand>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.RoomStatus));

            CreateMap<VotingResult, VotingResultModel>().ReverseMap();
            CreateMap<RoomConfigModel, RoomConfig>().ReverseMap();
            CreateMap<UserStoryModel, UserStory>()
                .ForMember(dest => dest.VoteResult, opt => opt.MapFrom(src => src.VotingResult))
                .ReverseMap(); 

            CreateMap<TeamsModel, Team>().ReverseMap();
             
            CreateMap<SimpleRoom, SimpleRoomModel>()
                .ForMember(dest => dest.RoomStatus, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.TotalStories, opt => opt.MapFrom(src => src.TotalCountStories));

            CreateMap<Room, RoomModel>()
                .ForMember(dest => dest.RoomStatus, opt => opt.MapFrom(src => src.Status));
        }
    }
}

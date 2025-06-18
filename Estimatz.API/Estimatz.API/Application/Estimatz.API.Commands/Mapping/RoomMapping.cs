using AutoMapper;
using Estimatz.API.Commands.SaveRoom;
using Estimatz.API.Entities.Room;

namespace Estimatz.API.Commands.Mapping
{
    public class RoomMapping : Profile
    {
        public RoomMapping()
        {
            CreateMap<SaveRoomCommand, Room>();
        }
    }
}

using AutoMapper;
using Estimatz.Commands.Room.SaveRoom;
using Estimatz.Entities.Room;

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

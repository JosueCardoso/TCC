using Estimatz.API.Commands.DeleteRoom;
using Estimatz.API.Commands.SaveRoom;
using Estimatz.API.Notifications;
using Estimatz.API.Queries.GetAllRooms;
using Estimatz.API.Queries.GetIndicators;
using Estimatz.API.Queries.GetRoom;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Estimatz.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class RoomController : BaseController
    {        
        public RoomController(IMediator mediatr, INotificator notificator) : base(notificator, mediatr)
        {}

        [HttpPost("create-room")]
        public async Task<ActionResult> SaveRoom(SaveRoomCommand request)
        {            
            return CustomResponse(await _mediatr.Send(request));
        }

        [HttpGet("get-simple-rooms")]
        public async Task<ActionResult> GetAllSimpleRooms([FromQuery][Required] Guid userId)
        {
            var response = await _mediatr.Send(new GetSimpleRoomsQuery { UserId = userId });
            return CustomResponse(response);
        }

        [HttpGet("get-room")]
        public async Task<ActionResult> GetRoom([FromQuery][Required] Guid roomId, [FromQuery][Required] Guid userId)
        {
            var response = await _mediatr.Send(new GetRoomQuery { RoomId = roomId, UserId = userId });
            return CustomResponse(response);
        }

        [HttpDelete("delete-rooms")]
        public async Task<ActionResult> DeleteRoom([FromQuery][Required] Guid roomId, [FromQuery][Required] Guid userId)
        {
            await _mediatr.Send(new DeleteRoomCommand { RoomId = roomId, UserId = userId });
            return CustomResponse();
        }

        [HttpGet("get-indicators")]
        public async Task<ActionResult> GetIndicators([FromQuery][Required] Guid userId)
        {
            var resposne = await _mediatr.Send(new GetIndicatorsQuery { UserId = userId });
            return CustomResponse(resposne);
        }
    }
}

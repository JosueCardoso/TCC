using Estimatz.API.Commands.AddStory;
using Estimatz.API.Commands.RemoveStory;
using Estimatz.API.Commands.UpdateStatusStory;
using Estimatz.API.Hubs;
using Estimatz.API.Notifications;
using Estimatz.API.Queries.GetStory;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Estimatz.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class StoryController : BaseController
    {
        public StoryController(INotificator notificator, IMediator mediatr) : base(notificator, mediatr) {}

        [HttpPost("add-story")]
        public async Task<ActionResult> AddStory(AddStoryCommand request)
        {
            await _mediatr.Send(request);
            return CustomResponse();
        }

        [HttpPost("remove-story")]
        public async Task<ActionResult> RemoveStory(RemoveStoryCommand request)
        {
            await _mediatr.Send(request);
            return CustomResponse();
        }

        [HttpPost("update-status-story")]
        public async Task<ActionResult> UpdateStatusStory(UpdateStatusStoryCommand request)
        {
            await _mediatr.Send(request);
            return CustomResponse();
        }

        [HttpGet("get-story")]
        public async Task<ActionResult> GetStory([FromQuery][Required] Guid roomId, [FromQuery][Required] Guid storyId)
        {
            var response = await _mediatr.Send(new GetStoryQuery { RoomId = roomId, StoryId = storyId });
            return CustomResponse(response);
        }
    }
}

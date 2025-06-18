using AutoMapper;
using Estimatz.API.Queries.GetStory;
using Estimatz.Commands.Story.AddStory;
using Estimatz.Commands.Story.RemoveStory;
using Estimatz.Entities.UserStory;
using Estimatz.Notifications;
using Estimatz.UI.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Estimatz.UI.Controllers
{
    public class StoryController : BaseController
    {
        public StoryController(IMapper mapper, IMediator mediator, IHttpContextAccessor httpContextAccessor, INotificator notificator) : base(mapper, mediator, httpContextAccessor, notificator) { }

        [HttpPost]
        public async Task<IActionResult> AddStory(string storyName, string roomId, List<UserStoryModel> stories)
        {
            //Validação de usuário está desabilitada por conta das salas instantâneas
            //TODO: Achar uma maneira de validar o usuário quando for sala instantânea tbm
            //var isValidUser = await IsValidUser();
            //if (!isValidUser) 
            //    return RedirectToAction("Index", "Base"); //TODO:Retornar para a action não está funcionando quando a requisição ajax está esperando um JSON

            var storyModel = new UserStoryModel { Id = Guid.NewGuid(), Name = storyName };
            var story = _mapper.Map<UserStory>(storyModel);

            await _mediatr.Send(new AddStoryCommand { RoomId = Guid.Parse(roomId), Story = story });

            if (_notificator.IsSuccess)
                stories.Add(storyModel);

            return Json(new { stories }); //TODO: Aqui é possível criar um tratamento para quando não for adicionado uma história na sala
        }

        [HttpPost]
        public async Task<IActionResult> RemoveStory(string storyId, string roomId, List<UserStoryModel> stories)
        {
            //Validação de usuário está desabilitada por conta das salas instantâneas
            //TODO: Achar uma maneira de validar o usuário quando for sala instantânea tbm
            //var isValidUser = await IsValidUser();
            //if (!isValidUser)
            //    return RedirectToAction("Index", "Base");

            await _mediatr.Send(new RemoveStoryCommand { RoomId = Guid.Parse(roomId), StoryId = Guid.Parse(storyId) });

            if (_notificator.IsSuccess)
                stories.RemoveAll(x => x.Id == Guid.Parse(storyId));

            return Json(new { stories }); //TODO: Aqui é possível criar um tratamento para quando não for adicionado uma história na sala
        }

        [HttpPost]
        public async Task<IActionResult> GetStory(Guid roomId, Guid storyId)
        {
            var response = await _mediatr.Send(new GetStoryQuery { RoomId = roomId, StoryId = storyId });
            var model = _mapper.Map<UserStoryModel>(response);

            return PartialView("/Pages/Partial/_Story.cshtml", model);
        }

        [HttpPost]
        public IActionResult GetCardStoryRow(UserStoryModel story)
        {
            return PartialView("/Pages/Components/_CardStoryRow.cshtml", story);
        }
    }
}

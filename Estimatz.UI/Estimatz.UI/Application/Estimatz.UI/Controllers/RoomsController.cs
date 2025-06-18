using AutoMapper;
using Estimatz.UI.Commands.CreateRoom;
using Estimatz.UI.Commands.DeleteRoom;
using Estimatz.UI.Commands.UpdateStatusStory;
using Estimatz.UI.Entities.Player;
using Estimatz.UI.Entities.Room;
using Estimatz.UI.Entities.Story;
using Estimatz.UI.Extensions;
using Estimatz.UI.Models;
using Estimatz.UI.Queries.GetRoom;
using Estimatz.UI.Queries.GetRoomsByUserId;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;

namespace Estimatz.UI.Controllers
{
    public class RoomsController : BaseController
    { 
        public RoomsController(IHttpContextAccessor httpContextAccessor, IMediator mediatr, IMapper mapper) : base(mapper, mediatr, httpContextAccessor){}

        public async Task<IActionResult> Rooms()
        {
            var isValidUser = await IsValidUser();
            if (!isValidUser)
                return RedirectToAction("Index", "Base");

            MenuManager.SetMenuActive("rooms");

            var userIdString = _session.GetString("userId");            
            var simpleRooms = await _mediatr.Send(new GetRoomByUserQuery { UserId = Guid.Parse(userIdString) });
            var model = _mapper.Map<List<SimpleRoomModel>>(simpleRooms);

            return View("Pages/Rooms.cshtml", new RoomsModel { Rooms = model });
        }

        [HttpPost]
        public async Task<IActionResult> AddTeamOnTable(string teamName, List<TeamsModel> teams)
        {
            var isValidUser = await IsValidUser();
            if (!isValidUser)
                return RedirectToAction("Index", "Base");

            teams.Add(new TeamsModel { Id = Guid.NewGuid(), Name = teamName });
            return Json(new { teams });
        }
                
        [HttpPost]
        public async Task<IActionResult> SaveRoomConfig(RoomConfigModel model) //TODO: Achar uma maneira de proteger esse endpoint
        {
            var userIdString = _session.GetString("userId");

            if (string.IsNullOrEmpty(userIdString))
            {
                userIdString = Guid.NewGuid().ToString();
                _session.SetString("userId", userIdString);
                model.IsQuickRoom = true;
            }

            model.EstimateType = (EstimateType)Enum.Parse(typeof(EstimateType), model.SelectedEstimateType.ToString());
            model.Deck = (Decks)Enum.Parse(typeof(Decks), model.SelectedDeck.ToString());

            var room = new RoomModel 
            { 
                UserId = Guid.Parse(userIdString),
                RoomConfig = model, 
                RoomStatus = model.FreeVoting ? RoomStatus.FreeVoting : RoomStatus.NotStarted 
            };

            var command = _mapper.Map<CreateRoomCommand>(room);
            var result = await _mediatr.Send(command);

            if(result != Guid.Empty)
                return Json(new { success = true, roomId = result.ToString() });

            return Json(new { success = false });
        }

        [HttpGet] 
        public async Task<IActionResult> OpenRoom(Guid id) //TODO: Achar uma maneira de proteger esse endpoint
        {
            var userIdString = _session.GetString("userId");

            if (string.IsNullOrEmpty(userIdString)) //Está vindo do form de sala instantânea
            {
                userIdString = Guid.NewGuid().ToString();
                _session.SetString("userId", userIdString);               
            }

            var response = await _mediatr.Send(new GetRoomQuery { UserId = Guid.Parse(userIdString), RoomId = id });

            if(Guid.Parse(userIdString) == response.UserId)
            {
                _session.SetString("playerRole", PlayerRoomRole.Observer.ToString());
                _session.SetString("isAdmin", "true");
            }
            else
            {
                _session.SetString("playerRole", PlayerRoomRole.Voter.ToString());
                _session.SetString("isAdmin", "false");
            }

            _session.SetString("selectedDeck", ((int)response.RoomConfig.Deck).ToString());

            var model = _mapper.Map<RoomModel>(response);
            return View("Pages/Partial/_PlanningRoom.cshtml", model);
        }
                
        [HttpPost]
        public async Task<IActionResult> DeleteRoom(Guid id)
        {
            var isValidUser = await IsValidUser();
            if (!isValidUser)
                return RedirectToAction("Index", "Base");

            var userIdString = _session.GetString("userId");
            var response = await _mediatr.Send(new DeleteRoomCommand { RoomId = id, UserId = Guid.Parse(userIdString) });

            return Json(new { success = response });
        }

        [HttpGet]
        public IActionResult OpenCreateQuickRoom()
        {
            _session.SetString("userId", ""); //Resetar o ID de usuário
            return View("Pages/Partial/_CreateQuickPlay.cshtml");
        }

        [HttpPost]
        public IActionResult AddNewPlayer(PlayerModel player)
        {
            return PartialView("Pages/Components/_Player.cshtml", player);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStoryStatus(int newStatus, Guid storyId, Guid roomId)
        {
            var status = (StoryStatus)Enum.Parse(typeof(StoryStatus), newStatus.ToString());
            var statusDescription = status.GetDescription();
            var success = await _mediatr.Send(new UpdateStatusStoryCommand { NewStoryStatus = status, RoomId = roomId, StoryId = storyId });

            return Json(new { statusDescription });
        }

        [HttpPost]
        public async Task<IActionResult> ShowVoteResult(VotingResultModel model)
        {
            return PartialView("Pages/Components/_VoteResult.cshtml", model);
        }
    }
}

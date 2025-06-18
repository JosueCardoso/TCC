using Estimatz.API.Commands.UpdateStatusStory;
using Estimatz.API.Commands.UpdateStoryVote;
using Estimatz.API.Data.RoomRepository;
using Estimatz.API.Data.UserRepository;
using Estimatz.API.Entities.User;
using Estimatz.API.Services.Calculator;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace Estimatz.API.Hubs
{
    public class PlanningHub : Hub
    {
        private readonly IUserRepository _userRepository;  
        private readonly IRoomRepository _roomRepository;
        private readonly IMediator _mediatr;

        public PlanningHub(IUserRepository userRepository, IRoomRepository roomRepository, IMediator mediatr)
        {
            _userRepository = userRepository;
            _roomRepository = roomRepository;
            _mediatr = mediatr;
        }     
                
        public override Task OnConnectedAsync()
        {
            var user = JsonConvert.DeserializeObject<UserPlanning>(Context.GetHttpContext().Request.Query["user"]);
            _userRepository.AddUser(user);

            Groups.AddToGroupAsync(Context.ConnectionId, user.RoomId.ToString());            
            Clients.Group(user.RoomId.ToString()).SendAsync("playerOn", _userRepository.GetAllUserByRoom(user.RoomId));

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception = null)
        {
            var user = JsonConvert.DeserializeObject<UserPlanning>(Context.GetHttpContext().Request.Query["user"]);
            _userRepository.RemoveUser(user);

            Groups.RemoveFromGroupAsync(Context.ConnectionId, user.RoomId.ToString());
            Clients.Group(user.RoomId.ToString()).SendAsync("playerOff", _userRepository.GetAllUserByRoom(user.RoomId));

            return base.OnDisconnectedAsync(exception);
        }

        public async Task StartVotingAdm(Guid roomId, Guid storyId)
        {
            await Clients.Group(roomId.ToString()).SendAsync("startVoting", storyId.ToString());
        }

        public async Task StopVotingAdm(Guid roomId, Guid storyId)
        {
            await Clients.Group(roomId.ToString()).SendAsync("stopVoting", storyId.ToString());
        }

        public async Task Voting(Guid roomId, Guid userId, Guid storyId, string voting)
        {
            _userRepository.SetVote(roomId, userId, storyId, voting);
            await Clients.Group(roomId.ToString()).SendAsync("playerVote", userId);
        }

        public async Task TurnCards(Guid roomId, Guid storyId)
        {
            _userRepository.TurnCards(roomId, storyId);

            var room = await _roomRepository.FindRoom(roomId);
            var users = _userRepository.GetAllUserByRoom(roomId);
            List<string> votes = new List<string>();

            foreach(var user in users)
            {
                var storyVote = user.Votes.FirstOrDefault(x => x.StoryId == storyId).Vote;
                votes.Add(storyVote);
            }              
            
            var resultVoting = AbstractFactoryCalculator.GetFactory(room.RoomConfig.EstimateType).Calcule(votes, room.RoomConfig.Deck);
            
            await _mediatr.Send(new UpdateStoryVoteCommand { RoomId = roomId, StoryId = storyId, VotingResult = resultVoting });
            await Clients.Group(roomId.ToString()).SendAsync("turnedCards", _userRepository.GetAllUserByRoom(roomId), resultVoting);
        }

        public async Task RefreshVotes(Guid roomId, Guid storyId)
        {
            _userRepository.RefreshVotes(roomId, storyId);
            await Clients.Group(roomId.ToString()).SendAsync("restartedVotes", _userRepository.GetAllUserByRoom(roomId));
        }

        //public async Task SendMessage(Message chat)
        //{
        //    //Ao usar o método Client(_connections.GetUserId(chat.destination)) eu estou enviando a mensagem apenas para o usuário destino, não realizando broadcast
        //    await Clients.Client(_subscriptions.Where(x=>x.Value.key == chat.destination).First().Key).SendAsync("Receive", chat.sender, chat.message);
        //}
    }

    public class Message
    {
        public Guid destination { get; set; }
        public UserPlanning sender { get; set; }
        public string message { get; set; }
    }
}

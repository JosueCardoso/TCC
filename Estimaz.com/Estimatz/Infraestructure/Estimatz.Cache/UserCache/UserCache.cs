using Estimatz.Entities.User;
using System.Collections.Concurrent;

namespace Estimatz.Cache.UserCache
{
    public class UserCache : IUserCache
    {
        private ConcurrentDictionary<Guid, List<UserPlanning>> _rooms = new ConcurrentDictionary<Guid, List<UserPlanning>>();

        public void AddUser(UserPlanning user)
        {
            if (_rooms.TryGetValue(user.RoomId, out var users) && !users.Exists(x => x.UserId == user.UserId))
                users.Add(user);
            else
                _rooms.TryAdd(user.RoomId, new List<UserPlanning> { user });
        }

        public void RemoveUser(UserPlanning user)
        {
            if (_rooms.TryGetValue(user.RoomId, out var users) && users.Exists(x => x.UserId == user.UserId))            
                users.RemoveAll(x=>x.UserId == user.UserId);        //TODO: Criar uma rotina para  excluir as salas vazias  
        }

        public List<UserPlanning> GetAllUserByRoom(Guid roomId) => _rooms[roomId].ToList();

        public void SetVote(Guid roomId, Guid userId, Guid storyId, string vote)
        {
            if (_rooms.TryGetValue(roomId, out var users) && users.Exists(x => x.UserId == userId))
            {
                var user = users.Find(x => x.UserId == userId);

                if(user.Votes is null || user.Votes.Count == 0)
                {
                    user.Votes = new List<StoryVote>
                    {
                        new StoryVote 
                        {
                            StoryId = storyId,
                            Vote = vote
                        }
                    };
                }
                else if(user.Votes.Exists(x => x.StoryId == storyId))
                {
                    user.Votes.Find(x => x.StoryId == storyId).Vote = vote;
                }
                else
                {
                    user.Votes.Add(new StoryVote
                    {
                        StoryId = storyId,
                        Vote = vote
                    });
                }
            }     
        }

        public void TurnCards(Guid roomId, Guid storyId)
        {
            if (_rooms.TryGetValue(roomId, out var users))
            {
                foreach (var user in users)
                {
                    if(user.Votes.Exists(x=>x.StoryId == storyId))
                        user.Votes.Find(x => x.StoryId == storyId).TurnCards = true;
                }
            }
        }

        public void RefreshVotes(Guid roomId, Guid storyId)
        {
            if (_rooms.TryGetValue(roomId, out var users))
            {
                foreach (var user in users)
                {
                    if (user.Votes.Exists(x => x.StoryId == storyId))
                    {
                        var vote = user.Votes.Find(x => x.StoryId == storyId);
                        vote.TurnCards = false;
                        vote.Vote = "";

                    }
                }
            }
        }
    }
}

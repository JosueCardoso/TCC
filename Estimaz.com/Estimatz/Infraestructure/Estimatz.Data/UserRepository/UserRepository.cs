using Estimatz.Cache.UserCache;
using Estimatz.Entities.User;

namespace Estimatz.Data.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserCache _userCache;

        public UserRepository(IUserCache userCache)
        {
            _userCache = userCache;
        }

        public void AddUser(UserPlanning user)
        {
            _userCache.AddUser(user);
        }

        public void RemoveUser(UserPlanning user)
        {
            _userCache.RemoveUser(user);
        }

        public List<UserPlanning> GetAllUserByRoom(Guid roomId) => _userCache.GetAllUserByRoom(roomId);

        public void SetVote(Guid roomId, Guid userId, Guid storyId, string vote)
        {
            _userCache.SetVote(roomId, userId, storyId, vote);
        }

        public void TurnCards(Guid roomId, Guid storyId)
        {
            _userCache.TurnCards(roomId, storyId);
        }

        public void RefreshVotes(Guid roomId, Guid storyId)
        {
            _userCache.RefreshVotes(roomId, storyId);
        }
    }
}

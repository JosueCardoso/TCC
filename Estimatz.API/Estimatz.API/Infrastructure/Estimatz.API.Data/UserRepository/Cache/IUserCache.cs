using Estimatz.API.Entities.User;

namespace Estimatz.API.Data.UserRepository.Cache
{
    public interface IUserCache
    {
        List<UserPlanning> GetAllUserByRoom(Guid roomId);
        void AddUser(UserPlanning user);
        void RemoveUser(UserPlanning user);
        void SetVote(Guid roomId, Guid userId, Guid storyId, string vote);
        void TurnCards(Guid roomId, Guid storyId);
        void RefreshVotes(Guid roomId, Guid storyId);
    }
}

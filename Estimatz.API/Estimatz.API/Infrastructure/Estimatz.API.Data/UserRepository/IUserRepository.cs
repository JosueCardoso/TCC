using Estimatz.API.Entities.User;

namespace Estimatz.API.Data.UserRepository
{
    public interface IUserRepository
    {
        void AddUser(UserPlanning user);
        void RemoveUser(UserPlanning user);
        List<UserPlanning> GetAllUserByRoom(Guid roomId);
        void SetVote(Guid roomId, Guid userId, Guid storyId, string vote);
        void TurnCards(Guid roomId, Guid storyId);
        void RefreshVotes(Guid roomId, Guid storyId);
    }
}

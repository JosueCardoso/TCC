using Estimatz.UI.Entities.Room;
using MediatR;

namespace Estimatz.UI.Queries.GetRoomsByUserId
{
	public class GetRoomByUserQuery : IRequest<List<SimpleRoom>>
	{
		public Guid UserId { get; set; }
	}
}

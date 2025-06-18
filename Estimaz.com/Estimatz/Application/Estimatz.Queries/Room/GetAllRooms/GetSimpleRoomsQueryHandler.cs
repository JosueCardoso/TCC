using Estimatz.Data.RoomRepository;
using Estimatz.Entities.Room;
using Estimatz.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Estimatz.API.Queries.GetAllRooms
{
    public class GetSimpleRoomsQueryHandler : IRequestHandler<GetSimpleRoomsQuery, List<SimpleRoom>>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly INotificator _notificationService;
        private readonly ILogger<GetSimpleRoomsQueryHandler> _logger;

        public GetSimpleRoomsQueryHandler(IRoomRepository roomRepository, INotificator notificator, ILogger<GetSimpleRoomsQueryHandler> logger)
        {
            _roomRepository = roomRepository;
            _notificationService = notificator;
            _logger = logger;
        }

        public Task<List<SimpleRoom>> Handle(GetSimpleRoomsQuery request, CancellationToken cancellationToken)
        {
            var allRoomsByUserId = new List<SimpleRoom>();

            try
            {
                allRoomsByUserId = _roomRepository.GetAllRoomByUserId(request.UserId);
                _notificationService.Notify(new(success: true));
                _logger.LogInformation($"Consulta todas as salas do usuário {request.UserId} realizada com sucesso");
            }
            catch (Exception ex)
            {
                _notificationService.Notify(new(success: false, new(ex.Message)));
                _logger.LogError($"Ocorreu erro ao consultar todas as salas do usuário {request.UserId}. Erro: {ex.Message}");
            }

            return Task.FromResult(allRoomsByUserId);
        }
    }
}

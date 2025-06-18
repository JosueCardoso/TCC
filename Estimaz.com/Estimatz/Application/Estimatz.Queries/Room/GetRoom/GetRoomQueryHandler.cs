using Estimatz.Data.RoomRepository;
using Estimatz.Entities.Room;
using Estimatz.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Estimatz.API.Queries.GetRoom
{
    public class GetRoomQueryHandler : IRequestHandler<GetRoomQuery, Room>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly INotificator _notificationService;
        private readonly ILogger<GetRoomQueryHandler> _logger;

        public GetRoomQueryHandler(IRoomRepository roomRepository, INotificator notificationService, ILogger<GetRoomQueryHandler> logger)
        {
            _roomRepository = roomRepository;
            _notificationService = notificationService;
            _logger = logger;
        }

        //TODO: Para proteger as salas será necessário criar um esquema de organização
        public async Task<Room> Handle(GetRoomQuery request, CancellationToken cancellationToken)
        {
            var room = await _roomRepository.FindRoom(request.RoomId);
            if (room != null)
            {
                _notificationService.Notify(new(success: true));
                _logger.LogInformation($"Sala {room.Id} encontrada com sucesso!");
                return room;

                //TODO: Para proteger as salas será necessário criar um esquema de organização
                //if(room.UserId == request.UserId)
                //{
                //    _notificationService.Notify(new(success: true));
                //    _logger.LogInformation($"Sala {room.Id} encontrada com sucesso!");
                //    return room;
                //}
                //else
                //{
                //    _notificationService.Notify(new(success: false, new("Não foi possível encontrar a sala")));
                //    _logger.LogError($"Usuário tentando acessar uma sala que não é sua! Usuário {request.UserId} - Sala: {room.Id}.");
                //    return new Room();                    
                //}
            }

            _notificationService.Notify(new(success: false, new("Não foi possível encontrar a sala")));
            _logger.LogError($"Não foi possível encontrar a sala. Usuário {request.UserId} - Sala: {request.RoomId}.");
            return new Room();
        }
    }
}

using Estimatz.Data.RoomRepository;
using Estimatz.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Estimatz.Commands.Room.DeleteRoom
{
    public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly INotificator _notificationService;
        private readonly ILogger<DeleteRoomCommandHandler> _logger;

        public DeleteRoomCommandHandler(IRoomRepository roomRepository, INotificator notificationService, ILogger<DeleteRoomCommandHandler> logger)
        {
            _roomRepository = roomRepository;
            _notificationService = notificationService;
            _logger = logger;
        }

        public async Task Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            var room = await _roomRepository.FindRoom(request.RoomId);
            if(room.UserId == request.UserId)
            {
                var response = await _roomRepository.DeleteRoom(request.RoomId);
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    _logger.LogInformation($"Sala {room.Id} excluida com sucesso pelo usuário {request.UserId}");
                    _notificationService.Notify(new(success: true));
                    return;
                }
                else
                {
                    _logger.LogError($"Ocorreu um erro ao excluir a sala");
                    _notificationService.Notify(new(success: false, new("Não foi possível excluir a sala")));
                    return;
                }
            }

            _logger.LogError($"Usuário tentou excluir uma sala que não é sua! Usuário {request.UserId} - Sala {room.Id}");
            _notificationService.Notify(new(success: false, new("Não foi possível excluir a sala")));
        }
    }
}

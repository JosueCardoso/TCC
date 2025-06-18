using Estimatz.Data.RoomRepository;
using Estimatz.Entities.Indicators;
using Estimatz.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Estimatz.API.Queries.GetIndicators
{
    public class GetIndicatorsQueryHandler : IRequestHandler<GetIndicatorsQuery, List<Indicator>>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly INotificator _notificationService;
        private readonly ILogger<GetIndicatorsQueryHandler> _logger;

        public GetIndicatorsQueryHandler(IRoomRepository roomRepository, INotificator notificationService, ILogger<GetIndicatorsQueryHandler> logger)
        {
            _roomRepository = roomRepository;
            _notificationService = notificationService;
            _logger = logger;
        }

        public Task<List<Indicator>> Handle(GetIndicatorsQuery request, CancellationToken cancellationToken)
        {
            var indicators = new List<Indicator>();

            try
            {
                var allRoomsByUserId = _roomRepository.GetAllRoomByUserId(request.UserId);

                if (allRoomsByUserId is not null && allRoomsByUserId.Any())
                {
                    int amountOfStory = 0;
                    var amountOfRoom = allRoomsByUserId.Count();                    
                    allRoomsByUserId.ForEach(x => amountOfStory += x.TotalCountStories);
                    var averageStoryByRoom = amountOfStory / amountOfRoom;

                    indicators.Add(new Indicator { Description = "Quantidade de salas", Value = amountOfRoom.ToString() });
                    indicators.Add(new Indicator { Description = "Quantidade de histórias", Value = amountOfStory.ToString() });
                    indicators.Add(new Indicator { Description = "Média de histórias por sala", Value = averageStoryByRoom.ToString() });
                }

                _notificationService.Notify(new(success: true));
                _logger.LogInformation($"Consulta de indicadores realizada com sucesso para o usuário {request.UserId}");
                return Task.FromResult(indicators);
            }
            catch(Exception ex)
            {
                _notificationService.Notify(new(success: false,new($"Ocorreu erro ao consultar os indicadores do usuário {request.UserId}. Erro: {ex.Message}")));
                _logger.LogInformation($"Consulta de indicadores realizada com sucesso para o usuário {request.UserId}");

                return Task.FromResult(indicators);
            }            
        }
    }
}

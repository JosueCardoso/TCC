using AutoMapper;
using Estimatz.Commands.Room.SaveRoom;
using Estimatz.Commands.Story.AddStory;
using Estimatz.Data.RoomRepository;
using Estimatz.Notifications;
using FluentAssertions;
using MediatR;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Estimatz.Test.UnitTest.Estimatz.Commands.Room.SaveRoom
{
	public class SaveRoomCommandHandlerTest
	{
		[Fact]
		public async Task DeveSalvarSalaComUmaHistóriaDadoUmaSalaComVotacaoLivre()
		{
			//Arrange
			var command = new SaveRoomCommand { };
			var notificationService = new NotificationsService();

			var loggerMock = Substitute.For<ILogger<SaveRoomCommandHandler>>();
			var mediatrMock = Substitute.For<IMediator>();

			var itemResponseMock = Substitute.For<ItemResponse<Entities.Room.Room>>();
			itemResponseMock.StatusCode.Returns(System.Net.HttpStatusCode.Created);

			var roomRepositoryMock = Substitute.For<IRoomRepository>();
			roomRepositoryMock.CreateRoom(Arg.Any<Entities.Room.Room>()).Returns(itemResponseMock);

			var mapperMock = Substitute.For<IMapper>();
			mapperMock.Map<Entities.Room.Room>(Arg.Any<SaveRoomCommand>())
				.Returns(new Entities.Room.Room 
			{ 
				RoomConfig = new Entities.Room.RoomConfig 
				{ 
					VotingType = Entities.Room.VotingType.FreeVoting 
				} 
			});

			//Act
			var handler = new SaveRoomCommandHandler(roomRepositoryMock, mapperMock,notificationService, loggerMock, mediatrMock);
			var result = await handler.Handle(command, CancellationToken.None);

			//Assert
			result.Should().NotBe(Guid.Empty);
			notificationService.IsSuccess.Should().BeTrue();
			await mediatrMock.Received().Send(Arg.Any<AddStoryCommand>());
		}

		[Fact]
		public async Task DeveSalvarSalaDadoUmaSalaComVotacaoDeTarefas()
		{
			//Arrange
			var command = new SaveRoomCommand { };
			var notificationService = new NotificationsService();

			var loggerMock = Substitute.For<ILogger<SaveRoomCommandHandler>>();
			var mediatrMock = Substitute.For<IMediator>();

			var itemResponseMock = Substitute.For<ItemResponse<Entities.Room.Room>>();
			itemResponseMock.StatusCode.Returns(System.Net.HttpStatusCode.Created);

			var roomRepositoryMock = Substitute.For<IRoomRepository>();
			roomRepositoryMock.CreateRoom(Arg.Any<Entities.Room.Room>()).Returns(itemResponseMock);

			var mapperMock = Substitute.For<IMapper>();
			mapperMock.Map<Entities.Room.Room>(Arg.Any<SaveRoomCommand>())
				.Returns(new Entities.Room.Room
				{
					RoomConfig = new Entities.Room.RoomConfig
					{
						VotingType = Entities.Room.VotingType.TaskVoting
					}
				});

			//Act
			var handler = new SaveRoomCommandHandler(roomRepositoryMock, mapperMock, notificationService, loggerMock, mediatrMock);
			var result = await handler.Handle(command, CancellationToken.None);

			//Assert
			result.Should().NotBe(Guid.Empty);
			notificationService.IsSuccess.Should().BeTrue();
			await mediatrMock.DidNotReceive().Send(Arg.Any<AddStoryCommand>());
		}

		[Fact]
		public async Task NaoDeveSalvarSalaDadoUmaSalaComAlgumErro()
		{
			//Arrange
			var command = new SaveRoomCommand { };
			var notificationService = new NotificationsService();

			var loggerMock = Substitute.For<ILogger<SaveRoomCommandHandler>>();
			var mediatrMock = Substitute.For<IMediator>();

			var itemResponseMock = Substitute.For<ItemResponse<Entities.Room.Room>>();
			itemResponseMock.StatusCode.Returns(System.Net.HttpStatusCode.BadRequest);

			var roomRepositoryMock = Substitute.For<IRoomRepository>();
			roomRepositoryMock.CreateRoom(Arg.Any<Entities.Room.Room>()).Returns(itemResponseMock);

			var mapperMock = Substitute.For<IMapper>();
			mapperMock.Map<Entities.Room.Room>(Arg.Any<SaveRoomCommand>())
				.Returns(new Entities.Room.Room
				{
					RoomConfig = new Entities.Room.RoomConfig
					{
						VotingType = Entities.Room.VotingType.TaskVoting
					}
				});

			//Act
			var handler = new SaveRoomCommandHandler(roomRepositoryMock, mapperMock, notificationService, loggerMock, mediatrMock);
			var result = await handler.Handle(command, CancellationToken.None);

			//Assert
			result.Should().Be(Guid.Empty);
			notificationService.IsSuccess.Should().BeFalse();
			notificationService.HasMessages.Should().BeTrue();
			notificationService.Messages.First().Description.Should().Be("Não foi possível criar a nova sala");
			await mediatrMock.DidNotReceive().Send(Arg.Any<AddStoryCommand>());
		}
	}
}

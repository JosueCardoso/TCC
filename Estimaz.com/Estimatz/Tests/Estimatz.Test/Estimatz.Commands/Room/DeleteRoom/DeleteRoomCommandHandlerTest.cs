using Estimatz.Commands.Room.DeleteRoom;
using Estimatz.Data.RoomRepository;
using Estimatz.Notifications;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Cosmos;
using NSubstitute;
using FluentAssertions;

namespace Estimatz.Test.UnitTest.Estimatz.Commands.Room.DeleteRoom
{
	public class DeleteRoomCommandHandlerTest
	{
		[Fact]
		public async Task DeveSerExcluirASalaDadoUmCodigoDeSalaValido()
		{
			//Arrange
			var command = new DeleteRoomCommand {RoomId = Guid.NewGuid(), UserId = Guid.NewGuid() };
			var notificationService = new NotificationsService();

			var loggerMock = Substitute.For<ILogger<DeleteRoomCommandHandler>>();
			var itemResponseMock = Substitute.For<ItemResponse<Entities.Room.Room>>();
			itemResponseMock.StatusCode.Returns(System.Net.HttpStatusCode.NoContent);

			var roomRepositoryMock = Substitute.For<IRoomRepository>();
			roomRepositoryMock.DeleteRoom(Arg.Any<Guid>()).Returns(Task.FromResult(itemResponseMock));
			roomRepositoryMock.FindRoom(Arg.Any<Guid>()).Returns(Task.FromResult(new Entities.Room.Room {Id = Guid.NewGuid(), UserId = command.UserId }));

			//Act
			var handler = new DeleteRoomCommandHandler(roomRepositoryMock, notificationService, loggerMock);
			await handler.Handle(command, CancellationToken.None);

			//Assert
			notificationService.IsSuccess.Should().BeTrue();
		}

		[Fact]
		public async Task NaoDeveSerExcluidaDadoUmCodigoDeSalaInvalido()
		{
			//Arrange
			var command = new DeleteRoomCommand { RoomId = Guid.NewGuid(), UserId = Guid.NewGuid() };
			var notificationService = new NotificationsService();

			var loggerMock = Substitute.For<ILogger<DeleteRoomCommandHandler>>();
			var itemResponseMock = Substitute.For<ItemResponse<Entities.Room.Room>>();
			itemResponseMock.StatusCode.Returns(System.Net.HttpStatusCode.BadRequest);

			var roomRepositoryMock = Substitute.For<IRoomRepository>();
			roomRepositoryMock.DeleteRoom(Arg.Any<Guid>()).Returns(Task.FromResult(itemResponseMock));
			roomRepositoryMock.FindRoom(Arg.Any<Guid>()).Returns(Task.FromResult(new Entities.Room.Room { Id = Guid.NewGuid(), UserId = command.UserId }));

			//Act
			var handler = new DeleteRoomCommandHandler(roomRepositoryMock, notificationService, loggerMock);
			await handler.Handle(command, CancellationToken.None);

			//Assert
			notificationService.IsSuccess.Should().BeFalse();
			notificationService.HasMessages.Should().BeTrue();
			notificationService.Messages.First().Description.Should().Be("Não foi possível excluir a sala");
		}

		[Fact]
		public async Task NaoDeveSerExcluirDadoUmCodigoDeSalaValidoEUmCodigoDeUsuarioDiferenteDaSala()
		{
			//Arrange
			var command = new DeleteRoomCommand { RoomId = Guid.NewGuid(), UserId = Guid.NewGuid() };
			var notificationService = new NotificationsService();

			var loggerMock = Substitute.For<ILogger<DeleteRoomCommandHandler>>();
			var itemResponseMock = Substitute.For<ItemResponse<Entities.Room.Room>>();
			itemResponseMock.StatusCode.Returns(System.Net.HttpStatusCode.NoContent);

			var roomRepositoryMock = Substitute.For<IRoomRepository>();
			roomRepositoryMock.DeleteRoom(Arg.Any<Guid>()).Returns(Task.FromResult(itemResponseMock));
			roomRepositoryMock.FindRoom(Arg.Any<Guid>()).Returns(Task.FromResult(new Entities.Room.Room { Id = Guid.NewGuid(), UserId = Guid.NewGuid() }));

			//Act
			var handler = new DeleteRoomCommandHandler(roomRepositoryMock, notificationService, loggerMock);
			await handler.Handle(command, CancellationToken.None);

			//Assert
			notificationService.IsSuccess.Should().BeFalse();
			notificationService.HasMessages.Should().BeTrue();
			notificationService.Messages.First().Description.Should().Be("Não foi possível excluir a sala");
		}
	}
}

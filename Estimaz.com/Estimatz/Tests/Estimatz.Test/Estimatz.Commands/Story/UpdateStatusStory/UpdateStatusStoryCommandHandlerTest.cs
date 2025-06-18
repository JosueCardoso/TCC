using Estimatz.Commands.Story.UpdateStatusStory;
using Estimatz.Data.RoomRepository;
using Estimatz.Data.StoryRepository;
using Estimatz.Entities.Room;
using Estimatz.Entities.UserStory;
using Estimatz.Notifications;
using FluentAssertions;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Estimatz.Test.UnitTest.Estimatz.Commands.Story.UpdateStatusStory
{
	public class UpdateStatusStoryCommandHandlerTest
	{
		[Fact]
		public async Task DadoUmaSalaInvalidaEUmaHistoriaComStatusEmAndamentoNaoDeveAlterarNada()
		{
			//Arrange
			var command = new UpdateStatusStoryCommand();
			var notificationService = new NotificationsService();			

			var loggerMock = Substitute.For<ILogger<UpdateStatusStoryCommandHandler>>();

			var roomRepositoryMock = Substitute.For<IRoomRepository>();
			roomRepositoryMock.FindRoom(Arg.Any<Guid>()).ReturnsNull();

			var storyRepositoryMock = Substitute.For<IStoryRepository>();

			//Act
			var handler = new UpdateStatusStoryCommandHandler(storyRepositoryMock, roomRepositoryMock, loggerMock, notificationService);
			await handler.Handle(command, CancellationToken.None);

			//Assert
			notificationService.IsSuccess.Should().BeFalse();
		}

		[Fact]
		public async Task DadoUmaSalaEUmaHistoriaInvalidaNaoDeveAlterarNada()
		{
			//Arrange
			var command = new UpdateStatusStoryCommand { StoryId = Guid.NewGuid() };
			var notificationService = new NotificationsService();
			var room = new Entities.Room.Room
			{
				UserStories = new List<UserStory>
				{
					new UserStory { Id = Guid.NewGuid() },
					new UserStory { Id = Guid.NewGuid() },
					new UserStory { Id = Guid.NewGuid() }
				}
			};

			var loggerMock = Substitute.For<ILogger<UpdateStatusStoryCommandHandler>>();

			var roomRepositoryMock = Substitute.For<IRoomRepository>();
			roomRepositoryMock.FindRoom(Arg.Any<Guid>()).Returns(room);

			var storyRepositoryMock = Substitute.For<IStoryRepository>();

			//Act
			var handler = new UpdateStatusStoryCommandHandler(storyRepositoryMock, roomRepositoryMock, loggerMock, notificationService);
			await handler.Handle(command, CancellationToken.None);

			//Assert
			notificationService.IsSuccess.Should().BeFalse();
		}

		[Fact]
		public async Task DadoUmaSalaEUmaHistoriaQuandoOcorrerErroNoBancoDeDadosNaoDeveAlterarNada()
		{
			//Arrange
			var command = new UpdateStatusStoryCommand { StoryId = Guid.NewGuid() };
			var notificationService = new NotificationsService();
			var room = new Entities.Room.Room
			{
				UserStories = new List<UserStory>
				{
					new UserStory { Id = Guid.NewGuid() },
					new UserStory { Id = command.StoryId },
					new UserStory { Id = Guid.NewGuid() }
				}
			};

			var loggerMock = Substitute.For<ILogger<UpdateStatusStoryCommandHandler>>();

			var roomRepositoryMock = Substitute.For<IRoomRepository>();
			roomRepositoryMock.FindRoom(Arg.Any<Guid>()).Returns(room);

			var itemResponseMock = Substitute.For<ItemResponse<Entities.Room.Room>>();
			itemResponseMock.StatusCode.Returns(System.Net.HttpStatusCode.BadRequest);

			var storyRepositoryMock = Substitute.For<IStoryRepository>();
			storyRepositoryMock.UpdateStatusStory(Arg.Any<int>(), Arg.Any<StoryStatus>(), Arg.Any<Guid>()).Returns(Task.FromResult(itemResponseMock));

			//Act
			var handler = new UpdateStatusStoryCommandHandler(storyRepositoryMock, roomRepositoryMock, loggerMock, notificationService);
			await handler.Handle(command, CancellationToken.None);

			//Assert
			notificationService.IsSuccess.Should().BeFalse();
		}

		[Fact]
		public async Task DadoUmaSalaComVariasHistoriasFechadasEStatusEmAndamentoEUmaHistoriaComStatusFechadoDeveAlterarStatusDaHistoriaEDaSalaParaFechado()
		{
			//Arrange
			var command = new UpdateStatusStoryCommand { StoryId = Guid.NewGuid(), NewStoryStatus = StoryStatus.Finished };
			var notificationService = new NotificationsService();
			var room = new Entities.Room.Room
			{
				Status = RoomStatus.Unfinished,
				UserStories = new List<UserStory>
				{
					new UserStory { Id = Guid.NewGuid(), Status = StoryStatus.Finished },
					new UserStory { Id = command.StoryId },
					new UserStory { Id = Guid.NewGuid(), Status = StoryStatus.Finished }
				}
			};

			var loggerMock = Substitute.For<ILogger<UpdateStatusStoryCommandHandler>>();

			var roomRepositoryMock = Substitute.For<IRoomRepository>();
			roomRepositoryMock.FindRoom(Arg.Any<Guid>()).Returns(room);

			var itemResponseMock = Substitute.For<ItemResponse<Entities.Room.Room>>();
			itemResponseMock.StatusCode.Returns(System.Net.HttpStatusCode.OK);

			var storyRepositoryMock = Substitute.For<IStoryRepository>();
			storyRepositoryMock.UpdateStatusStory(Arg.Any<int>(), Arg.Any<StoryStatus>(), Arg.Any<Guid>()).Returns(Task.FromResult(itemResponseMock));

			//Act
			var handler = new UpdateStatusStoryCommandHandler(storyRepositoryMock, roomRepositoryMock, loggerMock, notificationService);
			await handler.Handle(command, CancellationToken.None);

			//Assert
			notificationService.IsSuccess.Should().BeTrue();
			await roomRepositoryMock.Received().UpdateStatusRoom(Arg.Any<Guid>(), Arg.Any<RoomStatus>());
		}

		[Theory]
		[MemberData(nameof(DadoUmaSalaComDeterminadoStatusEUmaHistoriaComDeterminadoStatusDeveAlterarStatusDaSalaEDaHistoriaParameters))]
		public async Task DadoUmaSalaComDeterminadoStatusEUmaHistoriaComDeterminadoStatusDeveAlterarStatusDaSalaEDaHistoria(StoryStatus storyStatus, RoomStatus roomStatus, bool updateRoomStatus) 
		{
			//Arrange
			var command = new UpdateStatusStoryCommand { StoryId = Guid.NewGuid(), NewStoryStatus = storyStatus };
			var notificationService = new NotificationsService();
			var room = new Entities.Room.Room
			{
				Status = roomStatus,
				UserStories = new List<UserStory>
				{
					new UserStory { Id = command.StoryId },
					new UserStory { Id = Guid.NewGuid() },
					new UserStory { Id = Guid.NewGuid() }
				}
			};

			var loggerMock = Substitute.For<ILogger<UpdateStatusStoryCommandHandler>>();

			var roomRepositoryMock = Substitute.For<IRoomRepository>();
			roomRepositoryMock.FindRoom(Arg.Any<Guid>()).Returns(room);

			var itemResponseMock = Substitute.For<ItemResponse<Entities.Room.Room>>();
			itemResponseMock.StatusCode.Returns(System.Net.HttpStatusCode.OK);

			var storyRepositoryMock = Substitute.For<IStoryRepository>();
			storyRepositoryMock.UpdateStatusStory(Arg.Any<int>(), Arg.Any<StoryStatus>(), Arg.Any<Guid>()).Returns(Task.FromResult(itemResponseMock));

			//Act
			var handler = new UpdateStatusStoryCommandHandler(storyRepositoryMock, roomRepositoryMock, loggerMock, notificationService);
			await handler.Handle(command, CancellationToken.None);

			//Assert
			notificationService.IsSuccess.Should().BeTrue();

			if (updateRoomStatus)
				await roomRepositoryMock.Received().UpdateStatusRoom(Arg.Any<Guid>(), Arg.Any<RoomStatus>());
			else
				await roomRepositoryMock.DidNotReceive().UpdateStatusRoom(Arg.Any<Guid>(), Arg.Any<RoomStatus>());
		}

		public static IEnumerable<object[]> DadoUmaSalaComDeterminadoStatusEUmaHistoriaComDeterminadoStatusDeveAlterarStatusDaSalaEDaHistoriaParameters() 
		{
			yield return new object[] 
			{
				StoryStatus.Unfinished, 
				RoomStatus.NotStarted, 
				false
			};

			yield return new object[] //Sala não foi iniciada e a história foi iniciada - Deve iniciar a sala também
			{
				StoryStatus.InProgress,
				RoomStatus.NotStarted,
				true
			};

			yield return new object[]
			{
				StoryStatus.Unfinished,
				RoomStatus.NotStarted,
				false
			};

			yield return new object[]
			{
				StoryStatus.Finished,
				RoomStatus.NotStarted,
				false
			};

			yield return new object[]
			{
				StoryStatus.Finished,
				RoomStatus.Unfinished,
				false
			};

			yield return new object[]
			{
				StoryStatus.Finished,
				RoomStatus.FreeVoting,
				false
			};

			yield return new object[]
			{
				StoryStatus.InProgress,
				RoomStatus.FreeVoting,
				false
			};
		}
	}
}

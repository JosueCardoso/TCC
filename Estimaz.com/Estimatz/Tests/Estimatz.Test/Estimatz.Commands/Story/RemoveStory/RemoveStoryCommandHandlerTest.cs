using Estimatz.Commands.Story.RemoveStory;
using Estimatz.Data.RoomRepository;
using Estimatz.Data.StoryRepository;
using Estimatz.Entities.UserStory;
using Estimatz.Notifications;
using FluentAssertions;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Estimatz.Test.UnitTest.Estimatz.Commands.Story.RemoveStory
{
	public class RemoveStoryCommandHandlerTest
	{
		[Fact]
		public async void DadoUmIdDeHistoriaValidoEUmIdDeSalaValidoDeveRemoverAHistoriaDaSala()
		{
			//Arrange
			var command = new RemoveStoryCommand { StoryId = Guid.NewGuid() };
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

			var loggerMock = Substitute.For<ILogger<RemoveStoryCommandHandler>>();

			var roomRepositoryMock = Substitute.For<IRoomRepository>();
			roomRepositoryMock.FindRoom(Arg.Any<Guid>()).Returns(room);

			var itemResponseMock = Substitute.For<ItemResponse<Entities.Room.Room>>();
			itemResponseMock.StatusCode.Returns(System.Net.HttpStatusCode.OK);

			var storyRepositoryMock = Substitute.For<IStoryRepository>();
			storyRepositoryMock.RemoveStory(Arg.Any<int>(),Arg.Any<Guid>()).Returns(Task.FromResult(itemResponseMock));

			//Act
			var handler = new RemoveStoryCommandHandler(storyRepositoryMock,roomRepositoryMock,notificationService, loggerMock);
			await handler.Handle(command, CancellationToken.None);

			//Assert
			notificationService.IsSuccess.Should().BeTrue();
		}

		[Fact]
		public async void DadoUmIdDeHistoriaValidoEUmIdDeSalaInvalidoNaoDeveRemoverAHistoriaDaSala()
		{
			//Arrange
			var command = new RemoveStoryCommand { StoryId = Guid.NewGuid() };
			var notificationService = new NotificationsService();
			

			var loggerMock = Substitute.For<ILogger<RemoveStoryCommandHandler>>();

			var roomRepositoryMock = Substitute.For<IRoomRepository>();
			roomRepositoryMock.FindRoom(Arg.Any<Guid>()).ReturnsNull();

			var itemResponseMock = Substitute.For<ItemResponse<Entities.Room.Room>>();
			itemResponseMock.StatusCode.Returns(System.Net.HttpStatusCode.OK);

			var storyRepositoryMock = Substitute.For<IStoryRepository>();
			storyRepositoryMock.RemoveStory(Arg.Any<int>(), Arg.Any<Guid>()).Returns(Task.FromResult(itemResponseMock));

			//Act
			var handler = new RemoveStoryCommandHandler(storyRepositoryMock, roomRepositoryMock, notificationService, loggerMock);
			await handler.Handle(command, CancellationToken.None);

			//Assert
			notificationService.IsSuccess.Should().BeFalse();
		}

		[Fact]
		public async void DadoUmIdDeHistoriaInvalidoEUmIdDeSalaValidoNaoDeveRemoverAHistoriaDaSala()
		{
			//Arrange
			var command = new RemoveStoryCommand { StoryId = Guid.NewGuid() };
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

			var loggerMock = Substitute.For<ILogger<RemoveStoryCommandHandler>>();

			var roomRepositoryMock = Substitute.For<IRoomRepository>();
			roomRepositoryMock.FindRoom(Arg.Any<Guid>()).Returns(room);

			var itemResponseMock = Substitute.For<ItemResponse<Entities.Room.Room>>();
			itemResponseMock.StatusCode.Returns(System.Net.HttpStatusCode.OK);

			var storyRepositoryMock = Substitute.For<IStoryRepository>();
			storyRepositoryMock.RemoveStory(Arg.Any<int>(), Arg.Any<Guid>()).Returns(Task.FromResult(itemResponseMock));

			//Act
			var handler = new RemoveStoryCommandHandler(storyRepositoryMock, roomRepositoryMock, notificationService, loggerMock);
			await handler.Handle(command, CancellationToken.None);

			//Assert
			notificationService.IsSuccess.Should().BeFalse();
		}

		[Fact]
		public async void DadoUmIdDeHistoriaValidoEUmIdDeSalaValidoNaoDeveRemoverAHistoriaDaSala()
		{
			//Arrange
			var command = new RemoveStoryCommand { StoryId = Guid.NewGuid() };
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

			var loggerMock = Substitute.For<ILogger<RemoveStoryCommandHandler>>();

			var roomRepositoryMock = Substitute.For<IRoomRepository>();
			roomRepositoryMock.FindRoom(Arg.Any<Guid>()).Returns(room);

			var itemResponseMock = Substitute.For<ItemResponse<Entities.Room.Room>>();
			itemResponseMock.StatusCode.Returns(System.Net.HttpStatusCode.BadRequest);

			var storyRepositoryMock = Substitute.For<IStoryRepository>();
			storyRepositoryMock.RemoveStory(Arg.Any<int>(), Arg.Any<Guid>()).Returns(Task.FromResult(itemResponseMock));

			//Act
			var handler = new RemoveStoryCommandHandler(storyRepositoryMock, roomRepositoryMock, notificationService, loggerMock);
			await handler.Handle(command, CancellationToken.None);

			//Assert
			notificationService.IsSuccess.Should().BeFalse();
		}
	}
}

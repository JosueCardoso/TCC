using Estimatz.Commands.Story.UpdateStoryVote;
using Estimatz.Data.RoomRepository;
using Estimatz.Data.StoryRepository;
using Estimatz.Entities.UserStory;
using Estimatz.Notifications;
using FluentAssertions;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Estimatz.Test.UnitTest.Estimatz.Commands.Story.UpdateStoryVote
{
	public class UpdateStoryVoteCommandHandlerTest
	{
		[Fact]
		public async Task DadoUmaSalaEUmaHistoriaDeveAtualizarOsVotosDaHistoria() 
		{
			//Arrange
			var command = new UpdateStoryVoteCommand { StoryId = Guid.NewGuid() };
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

			var loggerMock = Substitute.For<ILogger<UpdateStoryVoteCommandHandler>>();

			var roomRepositoryMock = Substitute.For<IRoomRepository>();
			roomRepositoryMock.FindRoom(Arg.Any<Guid>()).Returns(room);

			var itemResponseMock = Substitute.For<ItemResponse<Entities.Room.Room>>();
			itemResponseMock.StatusCode.Returns(System.Net.HttpStatusCode.OK);

			var storyRepositoryMock = Substitute.For<IStoryRepository>();
			storyRepositoryMock.UpdateStoryVote(Arg.Any<Guid>(), Arg.Any<int>(), Arg.Any<VotingResult>()).Returns(Task.FromResult(itemResponseMock));

			//Act
			var handler = new UpdateStoryVoteCommandHandler(roomRepositoryMock, storyRepositoryMock, notificationService, loggerMock);
			await handler.Handle(command, CancellationToken.None);

			//Assert
			notificationService.IsSuccess.Should().BeTrue();
		}

		[Fact]
		public async Task DadoUmaSalaInvalidaEUmaHistoriaNaoDeveAtualizarOsVotosDaHistoria()
		{
			//Arrange
			var command = new UpdateStoryVoteCommand { StoryId = Guid.NewGuid() };
			var notificationService = new NotificationsService();
			var loggerMock = Substitute.For<ILogger<UpdateStoryVoteCommandHandler>>();

			var roomRepositoryMock = Substitute.For<IRoomRepository>();
			roomRepositoryMock.FindRoom(Arg.Any<Guid>()).ReturnsNull();

			var storyRepositoryMock = Substitute.For<IStoryRepository>();

			//Act
			var handler = new UpdateStoryVoteCommandHandler(roomRepositoryMock, storyRepositoryMock, notificationService, loggerMock);
			await handler.Handle(command, CancellationToken.None);

			//Assert
			notificationService.IsSuccess.Should().BeFalse();
		}

		[Fact]
		public async Task DadoUmaSalaEUmaHistoriaInvalidaNaoDeveAtualizarOsVotosDaHistoria()
		{
			//Arrange
			var command = new UpdateStoryVoteCommand { StoryId = Guid.NewGuid() };
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

			var loggerMock = Substitute.For<ILogger<UpdateStoryVoteCommandHandler>>();

			var roomRepositoryMock = Substitute.For<IRoomRepository>();
			roomRepositoryMock.FindRoom(Arg.Any<Guid>()).Returns(room);

			var storyRepositoryMock = Substitute.For<IStoryRepository>();

			//Act
			var handler = new UpdateStoryVoteCommandHandler(roomRepositoryMock, storyRepositoryMock, notificationService, loggerMock);
			await handler.Handle(command, CancellationToken.None);

			//Assert
			notificationService.IsSuccess.Should().BeFalse();
		}

		[Fact]
		public async Task DadoUmaSalaEUmaHistoriaQuandoOcorrerErroNoBancoDeDadosNaoDeveAtualizarOsVotosDaHistoria()
		{
			//Arrange
			var command = new UpdateStoryVoteCommand { StoryId = Guid.NewGuid() };
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

			var loggerMock = Substitute.For<ILogger<UpdateStoryVoteCommandHandler>>();

			var roomRepositoryMock = Substitute.For<IRoomRepository>();
			roomRepositoryMock.FindRoom(Arg.Any<Guid>()).Returns(room);

			var itemResponseMock = Substitute.For<ItemResponse<Entities.Room.Room>>();
			itemResponseMock.StatusCode.Returns(System.Net.HttpStatusCode.BadRequest);

			var storyRepositoryMock = Substitute.For<IStoryRepository>();
			storyRepositoryMock.UpdateStoryVote(Arg.Any<Guid>(), Arg.Any<int>(), Arg.Any<VotingResult>()).Returns(Task.FromResult(itemResponseMock));

			//Act
			var handler = new UpdateStoryVoteCommandHandler(roomRepositoryMock, storyRepositoryMock, notificationService, loggerMock);
			await handler.Handle(command, CancellationToken.None);

			//Assert
			notificationService.IsSuccess.Should().BeFalse();
		}
	}
}

using Estimatz.Commands.Story.AddStory;
using Estimatz.Data.StoryRepository;
using Estimatz.Entities.UserStory;
using Estimatz.Notifications;
using FluentAssertions;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Estimatz.Test.UnitTest.Estimatz.Commands.Story.AddStory
{
	public class AddStoryCommandHandlerTest
	{
		[Fact]
		public async Task DadoUmaHistoriaDeveAdicionarComSucesso()
		{
			//arrange
			var command = new AddStoryCommand();
			var notificationService = new NotificationsService();

			var loggerMock = Substitute.For<ILogger<AddStoryCommandHandler>>();

			var itemResponseMock = Substitute.For<ItemResponse<Entities.Room.Room>>();
			itemResponseMock.StatusCode.Returns(System.Net.HttpStatusCode.OK);

			var storyRepositoryMock = Substitute.For<IStoryRepository>();
			storyRepositoryMock.AddStory(Arg.Any<Guid>(), Arg.Any<UserStory>()).Returns(itemResponseMock);

			//act
			var handler = new AddStoryCommandHandler(storyRepositoryMock, notificationService, loggerMock);
			await handler.Handle(command, CancellationToken.None);

			//assert
			notificationService.IsSuccess.Should().BeTrue();
		}

		[Fact]
		public async Task DadoUmaHistoriaDeveOcorrerErroAoAdicionar() 
		{
			//arrange
			var command = new AddStoryCommand();
			var notificationService = new NotificationsService();

			var loggerMock = Substitute.For<ILogger<AddStoryCommandHandler>>();

			var itemResponseMock = Substitute.For<ItemResponse<Entities.Room.Room>>();
			itemResponseMock.StatusCode.Returns(System.Net.HttpStatusCode.BadRequest);

			var storyRepositoryMock = Substitute.For<IStoryRepository>();
			storyRepositoryMock.AddStory(Arg.Any<Guid>(), Arg.Any<UserStory>()).Returns(itemResponseMock);

			//act
			var handler = new AddStoryCommandHandler(storyRepositoryMock, notificationService, loggerMock);
			await handler.Handle(command, CancellationToken.None);

			//assert
			notificationService.IsSuccess.Should().BeFalse();
		}
	}
}

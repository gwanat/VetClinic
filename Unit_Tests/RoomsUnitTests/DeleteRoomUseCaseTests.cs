using Xunit;
using Moq;
using UseCases.RoomsUseCases;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.Tests.RoomsUseCases
{
    public class DeleteRoomUseCaseTests
    {
        [Fact]
        public void Execute_Should_Delete_Room()
        {
            var mockRoomsRepository = new Mock<IRoomsRepository>();
            var roomId = 1;

            var deleteRoomUseCase = new DeleteRoomUseCase(mockRoomsRepository.Object);

            deleteRoomUseCase.Execute(roomId);

            mockRoomsRepository.Verify(repo => repo.DeleteRoom(roomId), Times.Once);
        }
    }
}

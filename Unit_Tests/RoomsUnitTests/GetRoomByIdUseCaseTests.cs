using Xunit;
using Moq;
using UseCases.RoomsUseCases;
using UseCases.DataStorePluginInterfaces;
using CoreBusiness;

namespace UseCases.Tests.RoomsUseCases
{
    public class GetRoomByIdUseCaseTests
    {
        [Fact]
        public void Execute_Should_Return_Room_By_Id()
        {
            var mockRoomsRepository = new Mock<IRoomsRepository>();
            var roomId = 1;
            var room = new Room { RoomId = roomId, RoomNumber = "A101", Type = "Examination Room", IsOccupied = false, Description = "First floor examination room" };

            mockRoomsRepository
                .Setup(repo => repo.GetRoomById(roomId))
                .Returns(room);

            var getRoomByIdUseCase = new GetRoomByIdUseCase(mockRoomsRepository.Object);

            var result = getRoomByIdUseCase.Execute(roomId);

            mockRoomsRepository.Verify(repo => repo.GetRoomById(roomId), Times.Once);
            Assert.Equal(room, result);
        }

        [Fact]
        public void Execute_Should_Return_Null_If_Room_Not_Found()
        {
            var mockRoomsRepository = new Mock<IRoomsRepository>();
            var roomId = 1;

            mockRoomsRepository
                .Setup(repo => repo.GetRoomById(roomId))
                .Returns((Room)null);

            var getRoomByIdUseCase = new GetRoomByIdUseCase(mockRoomsRepository.Object);

            var result = getRoomByIdUseCase.Execute(roomId);

            mockRoomsRepository.Verify(repo => repo.GetRoomById(roomId), Times.Once);
            Assert.Null(result);
        }
    }
}

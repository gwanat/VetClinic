using Xunit;
using Moq;
using UseCases.RoomsUseCases;
using UseCases.DataStorePluginInterfaces;
using CoreBusiness;

namespace UseCases.Tests.RoomsUseCases
{
    public class UpdateRoomUseCaseTests
    {
        [Fact]
        public void Execute_Should_Update_Room()
        {
            var mockRoomsRepository = new Mock<IRoomsRepository>();
            var roomId = 1;
            var room = new Room { RoomId = roomId, RoomNumber = "A101", Type = "Examination Room", IsOccupied = false, Description = "First floor examination room" };

            var updateRoomUseCase = new UpdateRoomUseCase(mockRoomsRepository.Object);

            updateRoomUseCase.Execute(roomId, room);

            mockRoomsRepository.Verify(repo => repo.UpdateRoom(roomId, room), Times.Once);
        }

        [Fact]
        public void Execute_Should_Not_Call_Repository_If_Room_Is_Null()
        {
            var mockRoomsRepository = new Mock<IRoomsRepository>();
            var roomId = 1;
            Room room = null;

            var updateRoomUseCase = new UpdateRoomUseCase(mockRoomsRepository.Object);

            Assert.Throws<ArgumentNullException>(() => updateRoomUseCase.Execute(roomId, room));
            mockRoomsRepository.Verify(repo => repo.UpdateRoom(It.IsAny<int>(), It.IsAny<Room>()), Times.Never);
        }

        [Fact]
        public void Execute_Should_Not_Call_Repository_If_RoomId_Is_Invalid()
        {
            var mockRoomsRepository = new Mock<IRoomsRepository>();
            var invalidRoomId = -1;
            var room = new Room { RoomId = invalidRoomId, RoomNumber = "A101", Type = "Examination Room", IsOccupied = false, Description = "First floor examination room" };

            var updateRoomUseCase = new UpdateRoomUseCase(mockRoomsRepository.Object);

            Assert.Throws<ArgumentException>(() => updateRoomUseCase.Execute(invalidRoomId, room));
            mockRoomsRepository.Verify(repo => repo.UpdateRoom(It.IsAny<int>(), It.IsAny<Room>()), Times.Never);
        }
    }
}

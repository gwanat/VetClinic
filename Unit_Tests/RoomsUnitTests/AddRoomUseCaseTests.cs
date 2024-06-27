using Xunit;
using Moq;
using UseCases.RoomsUseCases;
using UseCases.DataStorePluginInterfaces;
using CoreBusiness;

namespace UseCases.Tests.RoomsUseCases
{
    public class AddRoomUseCaseTests
    {
        [Fact]
        public void Execute_Should_Add_Room()
        {
            var mockRoomsRepository = new Mock<IRoomsRepository>();
            var room = new Room { RoomId = 1, RoomNumber = "A101", Type = "Examination Room", IsOccupied = false, Description = "First floor examination room" };

            var addRoomUseCase = new AddRoomUseCase(mockRoomsRepository.Object);

            addRoomUseCase.Execute(room);

            mockRoomsRepository.Verify(repo => repo.AddRoom(room), Times.Once);
        }

        [Fact]
        public void Execute_With_Null_Room_Should_Throw_Argument_Null_Exceptionl()
        {
            var mockRoomsRepository = new Mock<IRoomsRepository>();
            Room room = null;

            var addRoomUseCase = new AddRoomUseCase(mockRoomsRepository.Object);


            Assert.Throws<ArgumentNullException>(() => addRoomUseCase.Execute(room));
            mockRoomsRepository.Verify(repo => repo.AddRoom(It.IsAny<Room>()), Times.Never);
        }
    }
}

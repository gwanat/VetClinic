using Xunit;
using Moq;
using UseCases.RoomsUseCases;
using UseCases.DataStorePluginInterfaces;
using CoreBusiness;
using System.Collections.Generic;

namespace UseCases.Tests.RoomsUseCases
{
    public class GetRoomsUseCaseTests
    {
        [Fact]
        public void Execute_Should_Return_All_Rooms()
        {
            var mockRoomsRepository = new Mock<IRoomsRepository>();
            var rooms = new List<Room>
            {
                new Room { RoomId = 1, RoomNumber = "A101", Type = "Examination Room", IsOccupied = false, Description = "First floor examination room" },
                new Room { RoomId = 2, RoomNumber = "B205", Type = "Operating Theater", IsOccupied = true, Description = "Second floor operating theater" }
            };

            mockRoomsRepository
                .Setup(repo => repo.GetRooms())
                .Returns(rooms);

            var getRoomsUseCase = new GetRoomsUseCase(mockRoomsRepository.Object);

            var result = getRoomsUseCase.Execute();

            mockRoomsRepository.Verify(repo => repo.GetRooms(), Times.Once);
            Assert.Equal(rooms, result);
        }
    }
}

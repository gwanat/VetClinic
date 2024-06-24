using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.RoomsUseCases
{
    public class GetRoomsUseCase
    {
        private readonly IRoomsRepository roomsRepository;

        public GetRoomsUseCase(IRoomsRepository roomsRepository)
        {
            this.roomsRepository = roomsRepository;
        }

        public IEnumerable<Room> Execute()
        {
            return roomsRepository.GetRooms();
        }
    }
}

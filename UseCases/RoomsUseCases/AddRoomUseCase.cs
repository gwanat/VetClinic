using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.RoomsUseCases
{
    public class AddRoomUseCase
    {
        private readonly IRoomsRepository roomsRepository;

        public AddRoomUseCase(IRoomsRepository roomsRepository)
        {
            this.roomsRepository = roomsRepository;
        }

        public void Execute(Room room)
        {
            roomsRepository.AddRoom(room);
        }
    }
}
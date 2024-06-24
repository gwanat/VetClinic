using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.RoomsUseCases
{
    public class DeleteRoomUseCase
    {
        private readonly IRoomsRepository roomsRepository;

        public DeleteRoomUseCase(IRoomsRepository roomsRepository)
        {
            this.roomsRepository = roomsRepository;
        }

        public void Execute(int roomId)
        {
            roomsRepository.DeleteRoom(roomId);
        }
    }
}

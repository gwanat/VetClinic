using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.RoomsUseCases
{
    public class UpdateRoomUseCase
    {
        private readonly IRoomsRepository roomsRepository;

        public UpdateRoomUseCase(IRoomsRepository roomsRepository)
        {
            this.roomsRepository = roomsRepository;
        }

        public void Execute(int roomId, Room room)
        {
            if (room == null)
                throw new ArgumentNullException(nameof(room));

            if (roomId <= 0)
                throw new ArgumentException("Invalid room ID", nameof(roomId));

            roomsRepository.UpdateRoom(roomId, room);
        }
    }
}
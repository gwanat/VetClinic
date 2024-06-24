using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.RoomsUseCases
{
    public class GetRoomByIdUseCase
    {
        private readonly IRoomsRepository roomsRepository;

        public GetRoomByIdUseCase(IRoomsRepository roomsRepository)
        {
            this.roomsRepository = roomsRepository;
        }

        public Room Execute(int roomId)
        {
            return roomsRepository.GetRoomById(roomId);
        }
    }
}

using CoreBusiness;

namespace UseCases.DataStorePluginInterfaces
{
    public interface IRoomsRepository
    {
        void AddRoom(Room room);
        void DeleteRoom(int roomId);
        Room GetRoomById(int roomId);
        IEnumerable<Room> GetRooms();
        void UpdateRoom(int roomId, Room room);
    }
}
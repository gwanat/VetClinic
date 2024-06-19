using System.Collections.Generic;
using System.Linq;

namespace VetClinic.Models
{
    public static class RoomsRepository
    {
        private static List<Room> _rooms = new List<Room>()
        {
            new Room { RoomId = 1, RoomNumber = "A101", Type = "Examination Room", IsOccupied = false, Description = "First floor examination room" },
            new Room { RoomId = 2, RoomNumber = "B205", Type = "Operating Theater", IsOccupied = true, Description = "Second floor operating theater" },
            new Room { RoomId = 3, RoomNumber = "C302", Type = "Examination Room", IsOccupied = false, Description = "Third floor examination room" },
            new Room { RoomId = 4, RoomNumber = "D104", Type = "Hospitalization Room", IsOccupied = true, Description = "Fourth floor hospitalization room" },
            new Room { RoomId = 5, RoomNumber = "E201", Type = "Examination Room", IsOccupied = false, Description = "Second floor examination room" }
        };

        public static void AddRoom(Room room)
        {
            if (_rooms != null && _rooms.Count > 0)
            {
                var maxId = _rooms.Max(x => x.RoomId);
                room.RoomId = maxId + 1;
            }
            else
            {
                room.RoomId = 1;
            }

            if (_rooms == null)
            {
                _rooms = new List<Room>();
            }

            _rooms.Add(room);
        }

        public static List<Room> GetRooms() => _rooms;

        public static Room GetRoomById(int roomId)
        {
            var room = _rooms.FirstOrDefault(x => x.RoomId == roomId);
            if (room != null)
            {
                return new Room
                {
                    RoomId = room.RoomId,
                    RoomNumber = room.RoomNumber,
                    Type = room.Type,
                    IsOccupied = room.IsOccupied,
                    Description = room.Description
                };
            }

            return null;
        }

        public static void UpdateRoom(int roomId, Room room)
        {
            if (roomId != room.RoomId) return;

            var roomToUpdate = _rooms.FirstOrDefault(x => x.RoomId == roomId);
            if (roomToUpdate != null)
            {
                roomToUpdate.RoomNumber = room.RoomNumber;
                roomToUpdate.Type = room.Type;
                roomToUpdate.IsOccupied = room.IsOccupied;
                roomToUpdate.Description = room.Description;
            }
        }

        public static void DeleteRoom(int roomId)
        {
            var room = _rooms.FirstOrDefault(x => x.RoomId == roomId);
            if (room != null)
            {
                _rooms.Remove(room);
            }
        }
    }
}

using System.Collections.Generic;
using System.Linq;

namespace VetClinic.Models
{
    public static class RoomsRepository
    {
        public static void AddRoom(VetClinicContext context, Room room)
        {
            context.Rooms.Add(room);
            context.SaveChanges();
        }

        public static List<Room> GetRooms(VetClinicContext context)
        {
            return context.Rooms.ToList();
        }

        public static Room GetRoomById(VetClinicContext context, int roomId)
        {
            return context.Rooms.FirstOrDefault(r => r.RoomId == roomId);
        }

        public static void UpdateRoom(VetClinicContext context, int roomId, Room room)
        {
            var roomToUpdate = context.Rooms.FirstOrDefault(r => r.RoomId == roomId);
            if (roomToUpdate != null)
            {
                roomToUpdate.RoomNumber = room.RoomNumber;
                roomToUpdate.Type = room.Type;
                roomToUpdate.IsOccupied = room.IsOccupied;
                roomToUpdate.Description = room.Description;

                context.SaveChanges();
            }
        }

        public static void DeleteRoom(VetClinicContext context, int roomId)
        {
            var roomToDelete = context.Rooms.FirstOrDefault(r => r.RoomId == roomId);
            if (roomToDelete != null)
            {
                context.Rooms.Remove(roomToDelete);
                context.SaveChanges();
            }
        }
    }
}

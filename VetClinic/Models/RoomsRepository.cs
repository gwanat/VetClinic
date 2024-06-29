using System;
using System.Collections.Generic;
using System.Linq;

namespace VetClinic.Models
{
    public static class RoomsRepository
    {
        public static void AddRoom(VetClinicContext context, Room room)
        {
            try
            {
                context.Rooms.Add(room);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AddRoom: {ex.Message}");
                throw;
            }
        }

        public static List<Room> GetRooms(VetClinicContext context)
        {
            try
            {
                return context.Rooms.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetRooms: {ex.Message}");
                throw;
            }
        }

        public static Room GetRoomById(VetClinicContext context, int roomId)
        {
            try
            {
                return context.Rooms.FirstOrDefault(r => r.RoomId == roomId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetRoomById: {ex.Message}");
                throw;
            }
        }

        public static void UpdateRoom(VetClinicContext context, int roomId, Room room)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateRoom: {ex.Message}");
                throw;
            }
        }

        public static void DeleteRoom(VetClinicContext context, int roomId)
        {
            try
            {
                var roomToDelete = context.Rooms.FirstOrDefault(r => r.RoomId == roomId);
                if (roomToDelete != null)
                {
                    context.Rooms.Remove(roomToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteRoom: {ex.Message}");
                throw;
            }
        }
    }
}

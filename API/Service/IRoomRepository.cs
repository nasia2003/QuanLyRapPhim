using API.Entities;
using Model.Dto;

namespace API.Service
{
    public interface IRoomRepository
    {
        ICollection<Room> GetAllRoom(Pagination query);
        Task<Room?> GetRoomByID(int ID);
        Task<Room> Create(Room room);
        Task<Room> Update(Room room);
        Task<Room> Delete(Room room);
    }
}

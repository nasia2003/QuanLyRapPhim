using API.Data;
using API.Entities;
using API.Service;
using Model.Dto;

namespace API.Implement
{
    public class RoomRepository : IRoomRepository
    {
        private readonly CinemaDbContext _context;

        public RoomRepository(CinemaDbContext context)
        {
            _context = context;
        }

        public async Task<Room> Create(Room room)
        {
            await _context.Rooms.AddAsync(room);
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task<Room> Delete(Room room)
        {
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return room;
        }

        public ICollection<Room> GetAllRoom(Pagination query)
        {
            var source = _context.Rooms.AsQueryable();
            var ret = PaginatedList<Room>.Create(source, query);
            return ret;
        }

        public async Task<Room?> GetRoomByID(int ID)
        {
            return await _context.Rooms.FindAsync(ID);
        }

        public async Task<Room> Update(Room room)
        {
            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();
            return room;
        }
    }
}

using API.Data;
using API.Entities;
using API.Service;
using Microsoft.EntityFrameworkCore;

namespace API.Implement
{
    public class RoomFilmRepository : IRoomFilmRepository
    {
        private readonly CinemaDbContext _context;

        public RoomFilmRepository(CinemaDbContext context) 
        {
            _context = context;
        }

        public async Task<RoomFilm> Create(RoomFilm roomFilm)
        {
            await _context.RoomFilms.AddAsync(roomFilm);
            await _context.SaveChangesAsync();
            return roomFilm;
        }

        public async Task<RoomFilm> Delete(RoomFilm roomFilm)
        {
            _context.RoomFilms.Remove(roomFilm);
            await _context.SaveChangesAsync();
            return roomFilm;
        }

        public async Task<RoomFilm?> GetByID(Guid ID)
        {
            return await _context.RoomFilms.FindAsync(ID); 
        }

        public async Task<RoomFilm?> GetByRoomAndFilm(int RoomID, Guid FilmID)
        {
            return await _context.RoomFilms
                         .SingleOrDefaultAsync(x => x.RoomID == RoomID && x.FilmID == FilmID);
        }

        public async Task<RoomFilm> Update(RoomFilm roomFilm)
        {
            _context.RoomFilms.Update(roomFilm);
            await _context.SaveChangesAsync();
            return roomFilm;
        }
    }
}

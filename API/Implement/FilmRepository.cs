using API.Data;
using API.Entities;
using API.Service;
using Microsoft.EntityFrameworkCore;
using Model.Dto;

namespace API.Implement
{
    public class FilmRepository : IFilmRepository
    {
        private readonly CinemaDbContext _context;

        public FilmRepository(CinemaDbContext context)
        {
            _context = context;
        }

        public async Task<Film> Create(Film film)
        {
            await _context.Films.AddAsync(film);
            await _context.SaveChangesAsync();
            return film;
        }

        public async Task<Film> Delete(Film film)
        {
            _context.Films.Remove(film);
            await _context.SaveChangesAsync();
            return film;
        }

        public ICollection<Film> GetAllFilm(Pagination query)
        {
            var source = _context.Films.AsQueryable().OrderBy(x => x.Name);
            var ret = PaginatedList<Film>.Create(source, query);
            return ret;
        }

        public async Task<Film?> GetFilmByID(Guid ID)
        {
            return await _context.Films.FindAsync(ID);
        }

        public async Task<Film?> GetFilmByName(string name)
        {
            return await _context.Films.SingleOrDefaultAsync(x => x.Name == name);
        }

        public async Task<Film> Update(Film film)
        {
            _context.Films.Update(film);
            await _context.SaveChangesAsync();
            return film;
        }
    }
}

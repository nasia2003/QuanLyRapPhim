using API.Entities;
using Model.Dto;

namespace API.Service
{
    public interface IFilmRepository
    {
        ICollection<Film> GetAllFilm(Pagination query);
        Task<Film?> GetFilmByID(Guid ID);
        Task<Film?> GetFilmByName(String name);
        Task<Film> Create(Film film);
        Task<Film> Update(Film film);
        Task<Film> Delete(Film film);
    }
}

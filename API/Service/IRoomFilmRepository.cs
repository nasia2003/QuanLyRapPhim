using API.Entities;

namespace API.Service
{
    public interface IRoomFilmRepository
    {
        Task<RoomFilm?> GetByID(Guid ID);
        Task<RoomFilm?> GetByRoomAndFilm(int RoomID, Guid FilmID);
        Task<RoomFilm> Create(RoomFilm roomFilm);
        Task<RoomFilm> Update(RoomFilm roomFilm);
        Task<RoomFilm> Delete(RoomFilm roomFilm);
    }
}

using Model.Dto;
using Model.Response;

namespace API.Entities
{
    public class RoomFilm
    {
        public Guid ID { get; set; }
        public int RoomID { get; set; }
        public Guid FilmID { get; set; }
        public DateOnly DayPremiered { get; set; }
        public TimeOnly TimePremiered { get; set; }
        public long Price { get; set; }

        virtual public Room Room { get; set; } = null!;
        virtual public Film Film { get; set; } = null!;
        virtual public ICollection<Ticket> Tickets { get; set; } = null!;

        public RoomFilm()
        {
            ID = Guid.NewGuid();
        }

        public RoomFilm(RoomFilmDto roomFilmDto)
        {
            Update(roomFilmDto);
        }

        public void Update(RoomFilmDto roomFilmDto)
        {
            RoomID = roomFilmDto.RoomID;
            FilmID = roomFilmDto.FilmID;
            DayPremiered = roomFilmDto.DayPremiered;
            TimePremiered = roomFilmDto.TimePremiered;
            Price = roomFilmDto.Price;
        }

        public RoomFilmResponse ToResponse()
        {
            return new RoomFilmResponse
            {
                RoomID = RoomID,
                FilmDto = Film.ToDto(),
                DayPremiered = DayPremiered,
                TimePremiered = TimePremiered,
                Price = Price,
            };
        }
    }
}

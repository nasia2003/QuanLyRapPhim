using Model.Dto;

namespace API.Entities
{
    public class Film
    {
        public Guid ID { get; set; }
        public string Name { get; set; } = null!;
        public string Summary { get; set; } = null!;
        public string Type { get; set; } = null!;
        public double Rate { get; set; }
        public DateOnly DayStart { get; set; }
        public int Duration { get; set; }
        virtual public ICollection<RoomFilm> RoomFilms { get; set; } = null!;

        public Film()
        {
            ID = Guid.NewGuid();
        }

        public Film(FilmDto filmDto) : this()
        {
            Update(filmDto);
        }

        public void Update(FilmDto filmDto)
        {
            DayStart = filmDto.DayStart;
            Duration = filmDto.Duration;
            Name = filmDto.Name;
            Summary = filmDto.Summary;
            Type = filmDto.Type;
            Rate = filmDto.Rate;
        }

        public FilmDto ToDto()
        {
            return new FilmDto
            {
                DayStart = DayStart,
                Duration = Duration,
                Name = Name,
                Summary = Summary,
                Type = Type,
                Rate = Rate,
            };
        }
    }
}

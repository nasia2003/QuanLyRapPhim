using Model.Dto;

namespace API.Entities
{
    public class Room
    {
        public int ID { get; set; }
        public int TotalSlot { get; set; }

        virtual public ICollection<RoomFilm> RoomFilms { get; set; } = null!;

        public Room() { }

        public Room(RoomDto roomDto)
        {
            Update(roomDto);
        }

        public void Update(RoomDto roomDto)
        {
            ID = roomDto.ID;
            TotalSlot = roomDto.TotalSlot;
        }

        public RoomDto ToDto()
        {
            return new RoomDto
            {
                ID = ID,
                TotalSlot = TotalSlot,
            };
        }
    }
}

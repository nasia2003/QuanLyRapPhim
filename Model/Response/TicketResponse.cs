﻿using Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Response
{
    public class TicketResponse
    {
        public RoomFilmResponse RoomFilmResponse { get; set; } = null!;
        public TicketDto TicketDto { get; set; } = null!;
    }
}

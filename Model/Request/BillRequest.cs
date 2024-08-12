using Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Request
{
    public class BillRequest
    {
        public IList<ChooseFood> ChooseFoods { get; set; } = null!;
        public IList<String> ChooseCombos { get; set; } = null!;
        public IList<TicketRequest> ChooseTickets { get; set; } = null!;
    }
}

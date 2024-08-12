namespace API.Entities
{
    public class TicketBill
    {
        public Guid TicketID { get; set; }
        public String BillID { get; set; } = null!;
        virtual public Bill Bill { get; set; } = null!;
        virtual public Ticket Ticket { get; set; } = null!;
    }
}

namespace API.Entities
{
    public class Bill
    {
        public String ID { get; set; } = null!;
        public long TotalPrice { get; set; }
        public DateTime PaymentTime { get; set; }

        virtual public ICollection<FoodBill> FoodBills { get; set; } = null!;
        virtual public ICollection<ComboBill> ComboBills { get; set; } = null!;
        virtual public ICollection<TicketBill> TicketBills { get; set; } = null!;
    }
}

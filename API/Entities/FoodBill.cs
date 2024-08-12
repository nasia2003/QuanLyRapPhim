namespace API.Entities
{
    public class FoodBill
    {
        public Guid FoodID { get; set; }
        public String BillID { get; set; } = null!;
        public int Quantity { get; set; }
        virtual public Bill Bill { get; set; } = null!;
        virtual public Food Food { get; set; } = null!;
    }
}

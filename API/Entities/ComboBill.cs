namespace API.Entities
{
    public class ComboBill
    {
        public Guid ComboID { get; set; }
        public String BillID { get; set; } = null!;
        public int Quantity { get; set; }
        virtual public Bill Bill { get; set; } = null!;
        virtual public Combo Combo { get; set; } = null!;
    }
}

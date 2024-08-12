namespace API.Entities
{
    public class FoodCombo
    {
        public Guid ComboID { get; set; }
        virtual public Combo Combo { get; set; } = null!;
        public Guid FoodID { get; set; }
        virtual public Food Food { get; set; } = null!;
    }
}

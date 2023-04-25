namespace Capstone.Models
{
    public class Drink
    {
        public int RestaurantID { get; set; }
        public string RestaurantName { get; set; }
        public int DrinkID { get; set; }
        public string Name { get; set; }
        public string Description { get; set;}
        public bool IsFrozen { get; set; }
    }
}

namespace la_mia_pizzeria_static.Models
{
    public class PizzaCategories
    {
        public Pizza Pizz { get; set; }

        public List<Category>? Categories { get; set; }

        public List<Ingrediente>? Ingredients { get; set; }

        public PizzaCategories()
        {

        }
    }
}

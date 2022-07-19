using System;

public class PizzaList
{
	List<Pizza> pizze;
	public PizzaList()
	{
		this.pizze = new List<Pizza>();
	}

	public void AddToList(Pizza pizza)
    {
		pizze.Add(pizza);
    }

	public List<Pizza> GetList()
    {
		return pizze;
    }
}

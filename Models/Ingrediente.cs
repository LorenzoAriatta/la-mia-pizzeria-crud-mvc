using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Ingrediente
{
    [Key]
	public int Id { get; set; }

	public string Name { get; set; }

	public List<Pizza> Pizzas { get; set; }
	public Ingrediente()
	{
	}
}

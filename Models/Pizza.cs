using la_mia_pizzeria_static.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Pizza
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Il campo è obbligatorio")]
    [StringLength(40, ErrorMessage = "Il nome non può avere più di 40 caratteri")]
    [MoreWordsValidationAttribute]
    public string Name { get; set; }

    [Column (TypeName = "text")]
    [Required(ErrorMessage = "Il campo è obbligatorio")]
    [StringLength(255, ErrorMessage = "La descrizioe non può avere più di 255 caratteri")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Il Campo è obbligatorio")]
    [Url(ErrorMessage = "Il link non è valido")]
    public string Image { get; set; }

    [Required(ErrorMessage = "Il campo è obbligatiorio")]
    [Range(1d, 15d, ErrorMessage = "Il prezzo deve essere compreso tra 1 e 15 &euro;")]
    public double Price { get; set; }

    public List<Ingrediente>? ingredients { get; set; }

    public Pizza()
    {

    }

    public Pizza(string name, string description, string image, double price, int id)
    {
        Name = name;
        Description = description;
        Image = image;
        Price = price;
        Id = id;
    }
}

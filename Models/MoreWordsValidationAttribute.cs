using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models
{
    public class MoreWordsValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string fieldValue = (string)value;
            string[] words = fieldValue.Split(' ');

           
            if(words.Length < 5 || fieldValue == null)
            {
                return new ValidationResult("Il campo deve contenere almeno 5 parole");
            }
           

            //if (fieldValue == null || fieldValue.Trim().IndexOf(" ") == -1)
            //{
            //    return new ValidationResult("Il campo deve contenere almeno 5 parole");
            //}

            return ValidationResult.Success;
        }
    }
}

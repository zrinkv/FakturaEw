using System;
using System.ComponentModel.DataAnnotations;

namespace FakturaMVC.Util
{
    public class ProvjeriDatumDospijeca : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dt = (DateTime)value;
            if (dt >= DateTime.Now.Date)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage ?? "Datum dospijeća ne može biti manji od današnjeg dana!");
        }
    }
}
using System.ComponentModel.DataAnnotations;

namespace D01_Task.Validators
{
    public class DateInPastAttribute:ValidationAttribute
    {
        public override bool IsValid(object? value) 
            => value is DateTime date && date < DateTime.Now;
    }
}

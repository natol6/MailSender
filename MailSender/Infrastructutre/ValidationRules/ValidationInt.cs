using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MailSender.Infrastructutre.ValidationRules
{
    public class ValidationInt : ValidationRule
    {
        public int? MinValue { get; set; }
        public int? MaxValue { get; set; }
        public bool AllowNull { get; set; }
        public string ErrorMessage { get; set; }
        public override ValidationResult Validate(object value, CultureInfo culture)
        {
            if (value is null) return AllowNull
                     ? ValidationResult.ValidResult
                     : new ValidationResult(false, ErrorMessage ?? "Строка не должна быть пустой");
            if (value is not string str) str = value.ToString();
            if (!Int32.TryParse(str, out int num)) return new ValidationResult(false, ErrorMessage ?? "Вводимое значение должно быть целым числом");
            if (num < MinValue) 
                return new ValidationResult(false, ErrorMessage ?? $"Значение не должно быть меньше {MinValue}");
            if (num > MaxValue) 
                return new ValidationResult(false, ErrorMessage ?? $"Значение не должно быть больше {MaxValue}");
            return ValidationResult.ValidResult;
        }
    }
}

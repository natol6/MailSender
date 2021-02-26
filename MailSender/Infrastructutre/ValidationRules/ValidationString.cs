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
    public class ValidationString: ValidationRule
    {
        public int? MinLen { get; set; }
        public int? MaxLen { get; set; }
        public bool AllowNull { get; set; }
        public string ErrorMessage { get; set; }
        public override ValidationResult Validate(object value, CultureInfo culture)
        {
            if (value is null) return AllowNull
                     ? ValidationResult.ValidResult
                     : new ValidationResult(false, ErrorMessage ?? "Строка не должна быть пустой");
            if (value is not string str) str = value.ToString();
            if (MinLen != null && str.Length < MinLen)
                return new ValidationResult(false, ErrorMessage ?? $"Количество символов в строке не должно быть меньше {MinLen}");
            if (MaxLen != null && str.Length > MaxLen)
                return new ValidationResult(false, ErrorMessage ?? $"Количество символов в строке не должно быть больше {MaxLen}");
            return ValidationResult.ValidResult;
        }
    }
}

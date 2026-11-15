using System;
using System.Globalization;

namespace IPG203_F25_Homework
{
    public static class InputValidator
    {
        public static bool IsValidSerialNumber(string value) { return !string.IsNullOrWhiteSpace(value) && value.Trim().Length <= 50; }
        public static bool IsValidBrand(string value) { return !string.IsNullOrWhiteSpace(value) && value.Trim().Length <= 80; }
        public static bool IsValidModel(string value) { return !string.IsNullOrWhiteSpace(value) && value.Trim().Length <= 80; }
        public static bool IsValidDate(string value)
        {
            DateTime parsed;
            return !string.IsNullOrWhiteSpace(value) && DateTime.TryParse(value, CultureInfo.CurrentCulture, DateTimeStyles.None, out parsed) && parsed > DateTime.MinValue;
        }
        public static bool IsValidCost(string value)
        {
            decimal parsed;
            return decimal.TryParse(value, NumberStyles.Number, CultureInfo.CurrentCulture, out parsed) && IsValidCost(parsed);
        }
        public static bool IsValidCost(decimal value) { return value >= 0 && value <= 1000000000m; }
        public static bool IsValidPositiveNumber(string value)
        {
            double parsed;
            return double.TryParse(value, NumberStyles.Number, CultureInfo.CurrentCulture, out parsed) && IsValidPositiveNumber(parsed);
        }
        public static bool IsValidPositiveNumber(double value) { return !double.IsNaN(value) && !double.IsInfinity(value) && value > 0 && value <= 1000000; }
        public static bool IsValidScreenSize(string value)
        {
            double parsed;
            return double.TryParse(value, NumberStyles.Number, CultureInfo.CurrentCulture, out parsed) && IsValidScreenSize(parsed);
        }
        public static bool IsValidScreenSize(double value) { return IsValidPositiveNumber(value) && value <= 200; }
        public static bool IsValidPower(int value) { return value > 0 && value <= 100000; }
        public static bool IsValidPower(string value)
        {
            int parsed;
            return int.TryParse(value, out parsed) && IsValidPower(parsed);
        }
        public static bool IsValidCapacity(double value) { return IsValidPositiveNumber(value); }
        public static bool IsValidCapacity(string value) { return IsValidPositiveNumber(value); }
    }
}

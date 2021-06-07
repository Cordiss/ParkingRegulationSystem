using System;
using System.Globalization;
using System.Windows.Data;

namespace Diploma.Converters
{
    /// <summary>
    /// Defines <see cref="Type"/> to <see cref="Boolean"/> converter.
    /// </summary>
    public class TypeToBoolConverter : IValueConverter
    {
        /// <summary>
        /// Converts <see cref="Type"/> to <see cref="Boolean"/>.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <param name="targetType">Result type.</param>
        /// <param name="parameter">Conversion parameter.</param>
        /// <param name="culture">Conversion culture.</param>
        /// <returns><see cref="Boolean"/> value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.GetType() ?? Binding.DoNothing;
        }

        /// <summary>
        /// Converts <see cref="Boolean"/> to <see cref="Type"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
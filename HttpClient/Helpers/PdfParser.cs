using System;
using System.Linq;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace HttpClient.Helpers
{
    /// <summary>
    /// Defines helper class that allows to parse and download pdf files.
    /// </summary>
    public static class PdfParser
    {
        /// <summary>
        /// Stores start flag after which will can be find car model in pdf document.
        /// </summary>
        private const string CarModelStartFlag = "Відповідальна особа, керуючи\nтранспортним засобом,\n";

        /// <summary>
        /// Stores start flag after which will can be find act date in pdf document.
        /// </summary>
        private const string ActDateStartFlag = "засобів\n1. ";

        /// <summary>
        /// Gets car model name.
        /// </summary>
        /// <param name="path">Pdf file URL.</param>
        /// <returns>Car model name.</returns>
        public static string GetCarModel(string path)
        {
            PdfReader reader = new PdfReader(path);

            var text = PdfTextExtractor.GetTextFromPage(reader, 2);
            var startIndex = text.IndexOf(CarModelStartFlag, StringComparison.Ordinal);

            return text.Substring(startIndex + CarModelStartFlag.Length).Split(' ').FirstOrDefault();
        }

        /// <summary>
        /// Gets Act Date.
        /// </summary>
        /// <param name="path">Pdf file URL.</param>
        /// <returns>Act date.</returns>
        public static DateTime GetActDateTime(string path)
        {
            PdfReader reader = new PdfReader(path);

            var text = PdfTextExtractor.GetTextFromPage(reader, 1);

            var startIndex = text.IndexOf(ActDateStartFlag, StringComparison.Ordinal);

            var result = text.Substring(startIndex + ActDateStartFlag.Length).Split(' ');
            var date = result[0];
            var time = result[2];

            DateTime actDateTime = DateTime.Parse($"{date} {time}");
            return actDateTime;
        }
    }
}
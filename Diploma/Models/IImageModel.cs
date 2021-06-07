using System.Windows.Media.Imaging;

namespace Diploma.Models
{
    /// <summary>
    /// Contains description of Image model.
    /// </summary>
    public interface IImageModel
    {
        /// <summary>
        /// Gets image URL.
        /// </summary>
        string ImagePath { get; }

        /// <summary>
        /// Gets image's bitmap.
        /// </summary>
        BitmapImage Image { get; }
    }
}
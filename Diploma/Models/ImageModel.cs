using System.IO;
using System.Windows.Media.Imaging;

namespace Diploma.Models
{
    /// <summary>
    /// Implements <see cref="IImageModel"/> interface.
    /// </summary>
    public class ImageModel : IImageModel
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="imagePath">Image URL.</param>
        /// <param name="imageByteArray">Image byte array.</param>
        public ImageModel(string imagePath, object imageByteArray)
        {
            ImagePath = imagePath;
            Image = LoadImage(imageByteArray);
        }

        /// <summary>
        /// Gets image URL.
        /// </summary>
        public string ImagePath { get; }

        /// <summary>
        /// Gets image's bitmap.
        /// </summary>
        public BitmapImage Image { get; }

        /// <summary>
        /// Converts byte array to <see cref="BitmapImage"/>.
        /// </summary>
        /// <param name="imageByteArray">Image byte array.</param>
        /// <returns>Image's bitmap.</returns>
        private BitmapImage LoadImage(object imageByteArray)
        {
            if (imageByteArray is byte[] imageBytes)
            {
                BitmapImage image = new BitmapImage();

                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = new MemoryStream(imageBytes);
                image.EndInit();

                return image;
            }

            return null;
        }
    }
}
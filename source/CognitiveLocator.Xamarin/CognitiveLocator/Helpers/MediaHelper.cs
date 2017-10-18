using DevKit.Xamarin.ImageKit;
using System;
using System.Threading.Tasks;
using Plugin.Media;

namespace CognitiveLocator.Helpers
{
    public class MediaHelper
    {
        public static async Task<byte[]> TakePhotoAsync()
        {
            byte[] photo = null;

            if (Plugin.Media.CrossMedia.Current.IsCameraAvailable
               && Plugin.Media.CrossMedia.Current.IsTakePhotoSupported)
            {
                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Full,
                    Directory = "People",
                    Name = "person.jpg",
                    MaxWidthHeight = 512,
                    AllowCropping = true
                });

                if (file != null)
                    using (var photoStream = file.GetStream())
                    {
                        photo = new byte[photoStream.Length];
                        await photoStream.ReadAsync(photo, 0, (int)photoStream.Length);
                    }
            }

            return await AdjustImageSize(photo);
        }

        public static async Task<byte[]> PickPhotoAsync()
        {
            byte[] photo = null;

            if (Plugin.Media.CrossMedia.Current.IsCameraAvailable
               && Plugin.Media.CrossMedia.Current.IsTakePhotoSupported)
            {
                var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Full,
                });

                if (file != null)
                {
                    using (var photoStream = file.GetStream())
                    {
                        photo = new byte[photoStream.Length];
                        await photoStream.ReadAsync(photo, 0, (int)photoStream.Length);
                    }

                    if (!file.Path.EndsWith("jpg", StringComparison.OrdinalIgnoreCase))
                        photo = await CrossImageConverter.Current.ConvertPngToJpgAsync(photo, 100);
                }
            }
            return await AdjustImageSize(photo);
        }

        public static async Task<byte[]> AdjustImageSize(byte[] photo)
        {
            int maxSize = 800;
            if (photo != null)
            {
                var imageDetails = await CrossImageData.Current.GetImageDetails(photo);

                if (imageDetails.Heigth > maxSize || imageDetails.Width > maxSize)
                {
                    bool isTaller = imageDetails.Heigth > imageDetails.Width;
                    int bigSide = isTaller ?
                                              imageDetails.Heigth :
                                              imageDetails.Width;

                    float extra = bigSide - maxSize;
                    float extraPercentage = (extra / bigSide) * 100;
                    int newImagePercentage = (int)(100 - extraPercentage);

                    photo = await CrossImageResizer.Current.ScaleImageAsync(photo, newImagePercentage, DevKit.Xamarin.ImageKit.Abstractions.ImageFormat.JPG);
                    var newDetails = await CrossImageData.Current.GetImageDetails(photo);
                }
            }
            return photo;
        }
    }
}
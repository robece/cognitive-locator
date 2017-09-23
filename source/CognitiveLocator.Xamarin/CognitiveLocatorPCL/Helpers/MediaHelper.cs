using System;
using System.Threading.Tasks;
using DevKit.Xamarin.ImageKit;
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
                    MaxWidthHeight = 512,
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

        public static async Task<byte[]> AdjustImageSize(byte[] photo)
        {
            if (photo != null)
            {
                var imageDetails = await CrossImageData.Current.GetImageDetails(photo);

                if (imageDetails.Heigth > 512 || imageDetails.Width > 512)
                {
                    int bigSide = imageDetails.Heigth > 512 ?
                                              imageDetails.Heigth :
                                              imageDetails.Width;

                    float extra = bigSide - 512;
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
using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace Billionaires.Universal.Extensions
{
    public static class WritableBitmapExtensions
    {
        public static WriteableBitmap PartialImage(this IRandomAccessStream source,
            uint xOffset, uint yOffset,
            uint width, uint height)
        {
            BitmapDecoder decoder = BitmapDecoder.CreateAsync(source).GetAwaiter().GetResult();
            // Scale image to appropriate size 
            BitmapTransform transform = new BitmapTransform
            {
                Bounds = new BitmapBounds
                {
                    X = xOffset,
                    Y = yOffset,
                    Width = width,
                    Height = height
                }
            };

            PixelDataProvider pixelData = decoder.GetPixelDataAsync(
                BitmapPixelFormat.Bgra8, // WriteableBitmap uses BGRA format 
                BitmapAlphaMode.Straight,
                transform,
                ExifOrientationMode.IgnoreExifOrientation, // This sample ignores Exif orientation 
                ColorManagementMode.DoNotColorManage
                ).GetAwaiter().GetResult();

            // An array containing the decoded image data, which could be modified before being displayed 
            byte[] sourcePixels = pixelData.DetachPixelData();

            var writeableBitmap = new WriteableBitmap((int)width, (int)height);
            // Open a stream to copy the image contents to the WriteableBitmap's pixel buffer 
            using (Stream stream = writeableBitmap.PixelBuffer.AsStream())
            {
                stream.Write(sourcePixels, 0, sourcePixels.Length);
            }

            return writeableBitmap;
        }

        public static WriteableBitmap PartialImage(this BitmapDecoder decoder,
            uint xOffset, uint yOffset,
            uint width, uint height)
        {
            // Scale image to appropriate size 
            BitmapTransform transform = new BitmapTransform
            {
                Bounds = new BitmapBounds
                {
                    X = xOffset,
                    Y = yOffset,
                    Width = width,
                    Height = height
                }
            };

            PixelDataProvider pixelData = decoder.GetPixelDataAsync(
                BitmapPixelFormat.Bgra8, // WriteableBitmap uses BGRA format 
                BitmapAlphaMode.Straight,
                transform,
                ExifOrientationMode.IgnoreExifOrientation, // This sample ignores Exif orientation 
                ColorManagementMode.DoNotColorManage
                ).GetResults();

            // An array containing the decoded image data, which could be modified before being displayed 
            byte[] sourcePixels = pixelData.DetachPixelData();

            var writeableBitmap = new WriteableBitmap((int)width, (int)height);
            // Open a stream to copy the image contents to the WriteableBitmap's pixel buffer 
            using (Stream stream = writeableBitmap.PixelBuffer.AsStream())
            {
                stream.Write(sourcePixels, 0, sourcePixels.Length);
            }

            return writeableBitmap;
        }

    }
}
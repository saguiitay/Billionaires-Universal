using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace Billionaires.Universal.Models
{
    public class PortraitsData
    {
        public int index { get; set; }
        public string faces { get; set; }
        public string halos { get; set; }
        public string masks { get; set; }

        public IRandomAccessStream RawData
        {
            get
            {
                var facesImage = faces.Substring(22);
                var imageDataRaw = Convert.FromBase64String(facesImage);

                return imageDataRaw.ToRandomAccessStream().Result;
            }
        }

        public WriteableBitmap GetImage()
        {
            var facesImage = faces.Substring(22);
            var imageDataRaw = Convert.FromBase64String(facesImage);

            var image = new WriteableBitmap(2850, 100);

            using (var ramStream = new InMemoryRandomAccessStream())
            {
                var stream = ramStream.AsStreamForWrite();
                stream.Write(imageDataRaw, 0, imageDataRaw.Length);
                stream.Flush();

                ramStream.Seek(0);
                image.SetSource(ramStream);
            }

            return image;

        }

        public BitmapImage GetBitmapImage()
        {
            var image = new BitmapImage();
            image.SetSource(GetData());

            return image;
        }

        public IRandomAccessStream GetData()
        {
            var facesImage = faces.Substring(22);
            var imageDataRaw = Convert.FromBase64String(facesImage);

            var ramStream = new InMemoryRandomAccessStream();
            var stream = ramStream.AsStreamForWrite();
            stream.Write(imageDataRaw, 0, imageDataRaw.Length);
            stream.Flush();

            ramStream.Seek(0);

            return ramStream;
        }

        public WriteableBitmap GetTileImage(WriteableBitmap image)
        {

            // TODO: Use the Masks to create tiles with the correct background

            //var baseImg = new WriteableBitmap(2850, 100);
            //var currentAccentColorHex = (Color)Application.Current.Resources["PhoneAccentColor"];
            //baseImg.FillRectangle(0, 0, 2850, 100, currentAccentColorHex);


            // This doesn't work, since masks is a GIF
            //var masksImage = masks.Substring(22);
            //var imageDataRaw = Convert.FromBase64String(masksImage);
            //var mask = new WriteableBitmap(2850, 100);
            //using (var stream = new MemoryStream(imageDataRaw))
            //    mask.SetSource(stream);
            //baseImg.Blit(new Rect(0, 0, 2850, 100), mask, new Rect(0, 0, 2850, 100));


            //baseImg.Blit(new Rect(0, 0, 2850, 100), image, new Rect(0, 0, 2850, 100),
            //           WriteableBitmapExtensions.BlendMode.Additive);

            return image;

        }
    }

    public static class ByteArrayExtensions
    {
        public static async Task<IRandomAccessStream> ToRandomAccessStream(this byte[] data)
        {
            InMemoryRandomAccessStream randomAccessStream = new InMemoryRandomAccessStream();
            await randomAccessStream.WriteAsync(data.AsBuffer());
            randomAccessStream.Seek(0); // Just to be sure.
                                        // I don't think you need to flush here, but if it doesn't work, give it a try.
            return randomAccessStream;

        }
    }
}
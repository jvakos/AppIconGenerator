using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppIconGenerator {
    public class SkiaSharpImageManipulationProvider  {
        public  static(byte[] FileContents, int Height, int Width) Resize(byte[] fileContents,int maxWidth, int maxHeight,SKFilterQuality quality = SKFilterQuality.Medium) {
            using MemoryStream ms = new MemoryStream(fileContents);
            using SKBitmap sourceBitmap = SKBitmap.Decode(ms);

            int height = Math.Min(maxHeight, sourceBitmap.Height);
            int width = Math.Min(maxWidth, sourceBitmap.Width);


            using SKBitmap scaledBitmap = sourceBitmap.Resize(new SKImageInfo(width, height, SKColorType.Bgra8888, SKAlphaType.Unpremul), quality);
            
            using SKImage scaledImage = SKImage.FromBitmap(scaledBitmap);            
            using SKData data = scaledImage.Encode();
            return (data.ToArray(), height, width);
        }
    }
}

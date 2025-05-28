using DataMatrix.net;
using SkiaSharp;
using Svg;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BrPrint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btprint_Click(object sender, RoutedEventArgs e)
        {
            //TestRawEncoder("Hello");
            // bool[,] rawData = new DmtxImageEncoder().EncodeRawData("HELLO WORLD");
            MessageBox.Show(TestRawEncoder("sdf"));

            //MessageBox.Show(new DmtxImageEncoder().EncodeRawData("HELLO WORLD").ToString());
            //var svgContent = new DmtxImageEncoder().en("HELLO WORLD");
            //var byteArray = Convert.FromBase64String(svgContent);
            //using (var stream = new MemoryStream(byteArray))
            //{
            //    var svgDocument = SvgDocument.Open<SvgDocument>(stream);
            //    var bitmap = svgDocument.Draw();

            //    using (MemoryStream stream2 = new MemoryStream())
            //    {
            //        bitmap.Save(stream2, System.Drawing.Imaging.ImageFormat.Png);  // Or any other image format
            //        stream.Position = 0;

            //        BitmapImage bitmapImage = new BitmapImage();
            //        bitmapImage.BeginInit();
            //        bitmapImage.StreamSource = stream2;
            //        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            //        bitmapImage.EndInit();
            //        imageView.Source = bitmapImage;


            //       // return bitmapImage;
            //    }


            // bitmap.Save(@"C:\temp\0\1.png");
            //}

            //var svgContent = new DmtxImageEncoder().EncodeSvgImage("HELLO WORLD");
            //var byteArray = Convert.FromBase64String(svgContent);
            //using (var stream = new MemoryStream(byteArray))
            //{
            //    var bitmap = new SKBitmap(500, 500);
            //    var canvas = new SKCanvas(bitmap);
            //    // load the SVG
            //    var svg = new SkiaSharp.Extended.Svg.SKSvg(new SKSize(100, 100));
            //    svg.Load(stream);
            //    // draw the SVG to the bitmap
            //    canvas.DrawPicture(svg.Picture);
            //    var skData = SKImage.FromBitmap(bitmap).Encode(SKEncodedImageFormat.Png, 100);
            //    using (var file = new FileStream(@"C:\temp\0\1.png", FileMode.Create))
            //    {
            //        skData.SaveTo(file);
            //    }
            //}
        }

        private string TestRawEncoder(string text)
        {
            string thestrig = "";
            DmtxImageEncoder encoder = new DmtxImageEncoder();
            bool[,] rawData = encoder.EncodeRawData(text);
           // thestrig
            for (int rowIdx = 0; rowIdx < rawData.GetLength(1); rowIdx++)
            {
                for (int colIdx = 0; colIdx < rawData.GetLength(0); colIdx++)
                {
                    thestrig+=rawData[colIdx, rowIdx] ? "X" : " ";
                }
                thestrig += "\n";
                // Debug.WriteLine("");
            }
            // Debug.WriteLine("");
            //Debug.WriteLine("================");
            return thestrig;
        }
    }
}

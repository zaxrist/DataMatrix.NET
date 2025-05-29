using DataMatrix.net;
using SkiaSharp;
using Svg;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
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
        PrintDocument printerIt = new PrintDocument();
        public MainWindow()
        {
            InitializeComponent();
            printerIt.PrinterSettings.PrinterName = "SATO CL4NX Plus 305dpi";
            printerIt.PrintPage += PrinterIt_PrintPage;
            printerIt.PrinterSettings.Copies = 1;
            printerIt.PrinterSettings.DefaultPageSettings.Landscape = false; // Set landscape mode
            printerIt.PrinterSettings.DefaultPageSettings.Margins = new Margins(1, 1, 1, 1); // Set margins to zero
            printerIt.PrinterSettings.PaperSizes.Add(new PaperSize("Custom", 20, 20)); // Set custom paper size

            LoadListOfPrinter();
        }

        private void PrinterIt_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.DrawImage(GridToImage(), new System.Drawing.Rectangle(0, 0, GridToImage().Width, GridToImage().Height));
            e.HasMorePages = false;
        }

        private Bitmap GridToImage()
        {
            RenderTargetBitmap rnd = new RenderTargetBitmap((int)PrinterGrid.ActualWidth, (int)PrinterGrid.ActualHeight, 96, 96, PixelFormats.Default);
            rnd.Render(PrinterGrid);
            MemoryStream stream = new MemoryStream();
            BitmapEncoder encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rnd));
            encoder.Save(stream);
            Bitmap bitmap = new Bitmap(stream);
            return bitmap;
        }


        private void btprint_Click(object sender, RoutedEventArgs e) //generate barcode and show printview
        {
            string path = @"C:\temp\BrPrint\OutPutBarcode.png";
            string filename = "Out.png";
            Bitmap bb = new Bitmap(1000, 1000);
            if (LotNoBox.Text == "") { System.Windows.MessageBox.Show("Enter Lot No"); return; }
            bb =  new DmtxImageEncoder().EncodeImage(LotNoBox.Text);
            imageView.Source = BitmapToImageSource(bb);
            LotNoText.Text = LotNoBox.Text;

            PrinterViewr.Source = BitmapToImageSource(GridToImage());
        }

        private BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }

        [DllImport("winspool.drv",CharSet = CharSet.Auto,SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern Boolean SetDefaultPrinter(String name);

        private void PrintBtn_Click(object sender, RoutedEventArgs e) //print button
        {
            //PrintDocument pd = new PrintDocument();
           // pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);

            System.Windows.Forms.PrintDialog printdlg = new System.Windows.Forms.PrintDialog();
            PrintPreviewDialog printPrvDlg = new PrintPreviewDialog();

            // preview the assigned document or you can create a different previewButton for it
            printPrvDlg.Document = printerIt;
            printPrvDlg.ShowDialog(); // this shows the preview and then show the Printer Dlg below

            printdlg.Document = printerIt;

            //if (printdlg.ShowDialog() == DialogResult)
            //{
            //    pd.Print();
            //}
        }

        private void LoadListOfPrinter()
        {
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                PrinterListCmb.Items.Add(printer);
            }
        }
        private void SaveDefaultPrinterClick(object sender, RoutedEventArgs e)
        {
            
            printerIt.PrinterSettings.PrinterName = "SATO CL4NX Plus 305dpi";
            //MessageBox.Show(PrinterListCmb.SelectedItem.ToString() + " is set to default printer");
            //if(PrinterListCmb.SelectedItem !=  "")
            //{
            //    MessageBox.Show("Please select a printer.");
            //    return;
            //}
        }
    }
}

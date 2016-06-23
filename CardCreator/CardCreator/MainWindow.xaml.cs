using Microsoft.Win32;
using System;
using System.Collections.Generic;
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


namespace CardCreator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {       


        public MainWindow()
        {
            InitializeComponent();
            setCardImage("/Media/img1.png");
        }          

        private void setCardDescription()
        {

        }       

        private void setCardTitle()
        {

        }

        private void createImageFromCanvas()
        {

        }

        private void saveImage()
        {

        }

        private void cardImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            displayOpenFileDialog();
        }

        private void buttonSelectImage_Click(object sender, RoutedEventArgs e)
        {
            displayOpenFileDialog();
        }

        private void displayOpenFileDialog()
        {
            var dialog = new OpenFileDialog();

            dialog.DefaultExt = ".png";
            dialog.Filter = "PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            Nullable<bool> result = dialog.ShowDialog();
            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dialog.FileName;
                Console.WriteLine(filename);
                setCardImage(filename);
            }
        }

      
        private void setCardImage(string path)
        {
            var cardBitmapImage = new BitmapImage();
            cardBitmapImage.BeginInit();
            cardBitmapImage.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
            cardBitmapImage.EndInit();
            cardImage.Source = cardBitmapImage;
        }

        private void cardTitleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            cardTitleTextBlock.Text = cardTitleTextBox.Text;
        }

        private void richtTextBoxCardDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextRange textRange = new TextRange(richtTextBoxCardDescription.Document.ContentStart,  richtTextBoxCardDescription.Document.ContentEnd);
            cardDescriptionTextBlock.Text = textRange.Text;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            Nullable<bool> result = dialog.ShowDialog();
            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dialog.FileName;
                Console.WriteLine(filename);
                // setCardImage(filename);
                CreateSaveBitmap(cardCanvas, filename);
            }           
        }

        private void CreateSaveBitmap(Canvas canvas, string filename)
        {
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
             (int)canvas.Width, (int)canvas.Height,
             96d, 96d, PixelFormats.Pbgra32);
            // needed otherwise the image output is black
            canvas.Measure(new Size((int)canvas.Width, (int)canvas.Height));
            canvas.Arrange(new Rect(new Size((int)canvas.Width, (int)canvas.Height)));

            renderBitmap.Render(canvas);

            //JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

            using (FileStream file = File.Create(filename))
            {
                encoder.Save(file);
            }
        }
    }
}

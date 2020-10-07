using OpenCvSharp;
using OpenCvSharp.CPlusPlus;
using OpenCvSharp.Extensions;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace Baskin_Kiosk.View.Payment
{
    /// <summary>
    /// Interaction logic for Card.xaml
    /// </summary>
    public partial class Card : Page
    {
        DispatcherTimer timer;
        IplImage src;
        CvCapture cap;
        WriteableBitmap wb;
        const int frameWidth = 640;
        const int frameHeight = 480;
        bool loop = false;

        public Card()
        {
            InitializeComponent();
            InitWebCamera();
            SetTimer();
        }

        private void Card_Unloaded(object sender, RoutedEventArgs e)
        {
            if (cap != null)
            {
                cap.Dispose();
            }

            if (timer != null)
            {
                timer.Stop();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void InitWebCamera()
        {
            try
            {
                cap = CvCapture.FromCamera(CaptureDevice.DShow, 0);

                wb = new WriteableBitmap(cap.FrameWidth, cap.FrameHeight, 96, 96, PixelFormats.Bgr24, null);
                image.Source = wb;
            }

            catch (Exception e)
            {
                if (timer != null)
                {
                    timer.Stop();
                }

                if (cap != null)
                {
                    cap.Dispose();
                    cap = null;
                }

                MessageBox.Show(e.ToString());
            }
        }

        private void SetTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 33);
            timer.Tick += new EventHandler(TimerClock_Tick);

            timer.IsEnabled = true;
            timer.Start();
        }

        void TimerClock_Tick(object sender, EventArgs e)
        {
            using (src = cap.QueryFrame())
            {
                WriteableBitmapConverter.ToWriteableBitmap(src, wb);
            }
        }
    }
}

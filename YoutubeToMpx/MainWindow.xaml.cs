using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using YoutubeDownloader.Controls;
using Brush = System.Windows.Media.Brush;

namespace YoutubeDownloader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private URLControl _urlControl = new URLControl();
        private ConfirmationControl _confirmationControl = new ConfirmationControl();
        private DownloadControl _downloadControl = new DownloadControl();
        private DoneControl _doneControl = new DoneControl();

        public MainWindow()
        {
            InitializeComponent();

            LogoSlot.Source = Helpers.ConvertByteArrayToBitmapImage(YoutubeToMpx.Properties.Resources.YoutubeMpxLogo);

            this.Icon = LogoSlot.Source;

            SelectStep("URL");
            ContentControl.Content = _urlControl;

            // Subscribe to the URLControlReady event
            _urlControl.URLControlReady += (sender, e) =>
            {
                // Switch to the ConfirmationControl
                ContentControl.Content = _confirmationControl;
                _confirmationControl.UpdateVideo(_urlControl.TargetedVideo);
                SelectStep("CONFIRM");
            };

            //Subscribe to Cancel event
            _confirmationControl.ConfirmationCanceled += (sender, e) =>
            {
                ContentControl.Content = _urlControl;
                _confirmationControl.SelectedVideo = null;
                _urlControl.TargetedVideo = null;
                _urlControl.URLTextBox.Text = "";
                SelectStep("URL");
            };

            //Ready to download
            _confirmationControl.ConfirmationReady += (sender, e) =>
            {
                SelectStep("DOWNLOAD");
                ContentControl.Content = _downloadControl;
                if(_confirmationControl.SelectedFormat == "MP3")
                {
                    _downloadControl.DownloadMP3(_urlControl.TargetedVideo);
                }else if(_confirmationControl.SelectedFormat == "MP4")
                {
                    _downloadControl.DownloadMp4(_urlControl.TargetedVideo);
                }
            };

            _downloadControl.DownloadDone += (sender, e) =>
            {
                //DONE
                SelectStep("DONE");
                ContentControl.Content = _doneControl;
                _confirmationControl.SelectedVideo = null;
                _urlControl.TargetedVideo = null;
                _urlControl.URLTextBox.Text = "";

            };

            _doneControl.DoneReady += (sender, e) =>
            {
                ContentControl.Content = _urlControl;
                SelectStep("URL");
            };
        }

        

        private readonly string UnselectedColor = "#121212";
        private readonly string SelectedColor = "#87CEEB";
        private void SelectStep(string step)
        {
            BrushConverter converter = new BrushConverter();

            Brush UnselectedBrush = (Brush)converter.ConvertFromString(UnselectedColor);
            URLRect.Fill = UnselectedBrush;
            ConfirmationRect.Fill = UnselectedBrush;
            DownloadingRect.Fill = UnselectedBrush;
            DoneRect.Fill = UnselectedBrush;


            if (step == "URL")
            {
                URLRect.Fill = (Brush)converter.ConvertFromString(SelectedColor);
            }
            else if(step =="CONFIRM")
            {
                ConfirmationRect.Fill = (Brush)converter.ConvertFromString(SelectedColor);
            }
            else if (step =="DOWNLOAD")
            {
                DownloadingRect.Fill = (Brush)converter.ConvertFromString(SelectedColor);
            }
            else if(step =="DONE")
            {
                DoneRect.Fill = (Brush)converter.ConvertFromString(SelectedColor);
            }
        }

        private void URLControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}

using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using YoutubeDownloader.Controls;
using YoutubeToMpx.Controls;
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
        private SearchControl _searchControl;
        private int _currentStep = (int)Steps.URL;

        enum Steps
        {
            URL = 0,
            CONFIRM = 1,
            DOWNLOAD = 2,
            DONE = 3
        }

        public MainWindow()
        {
            InitializeComponent();

            LogoSlot.Source = Helpers.ConvertByteArrayToBitmapImage(YoutubeToMpx.Properties.Resources.YoutubeMpxLogo);

            this.Icon = LogoSlot.Source;

            SelectStep((int)Steps.URL);
            ContentControl.Content = _urlControl;



            // Subscribe to the URLControlReady event
            _urlControl.URLControlReady += (sender, e) =>
            {
                // Switch to the ConfirmationControl
                ContentControl.Content = _confirmationControl;
                _confirmationControl.UpdateVideo(_urlControl.TargetedVideo);
                SelectStep((int)Steps.CONFIRM);
            };

            // Subscribe to the SearchControlGoto event
            _urlControl.SearchControlGoto += async (sender, e) =>
            {
                // Switch to the SearchControl
                ContentControl.Content = (_searchControl = await SearchControl.Create(_urlControl.URLTextBox.Text));

                // Subscribe to the GoBack event
                _searchControl.SearchBackButton += (sender, e) =>
                {
                    ContentControl.Content = _urlControl;
                    SelectStep((int)Steps.URL);
                };

                _searchControl.SearchReady += (sender, e) =>
                {
                    _confirmationControl.UpdateVideo(_searchControl.SelectedVideo);
                    ContentControl.Content = _confirmationControl;
                };

                SelectStep((int)Steps.CONFIRM);
            };

            //Subscribe to Cancel event
            _confirmationControl.ConfirmationCanceled += (sender, e) =>
            {
                ContentControl.Content = _urlControl;
                _confirmationControl.SelectedVideo = null;
                _urlControl.TargetedVideo = null;
                _urlControl.URLTextBox.Text = "";
                SelectStep((int)Steps.URL);
            };

            //Ready to download
            _confirmationControl.ConfirmationReady += (sender, e) =>
            {
                SelectStep((int)Steps.DOWNLOAD);
                ContentControl.Content = _downloadControl;
                if(_confirmationControl.SelectedFormat == "MP3")
                {
                    _downloadControl.DownloadMP3(_confirmationControl.SelectedVideo);
                }else if(_confirmationControl.SelectedFormat == "MP4")
                {
                    _downloadControl.DownloadMp4(_confirmationControl.SelectedVideo);
                }
            };

            _downloadControl.DownloadDone += (sender, e) =>
            {
                //DONE
                SelectStep((int)Steps.DONE);
                ContentControl.Content = _doneControl;
                _confirmationControl.SelectedVideo = null;
                _urlControl.TargetedVideo = null;
                _urlControl.URLTextBox.Text = "";

            };

            _doneControl.DoneReady += (sender, e) =>
            {
                ContentControl.Content = _urlControl;
                SelectStep((int)Steps.URL);
            };
        }

        

        private readonly string UnselectedColor = "#121212";
        private readonly string SelectedColor = "#87CEEB";
        private void SelectStep(int step)
        {
            BrushConverter converter = new BrushConverter();

            Brush UnselectedBrush = (Brush)converter.ConvertFromString(UnselectedColor);

            System.Windows.Shapes.Rectangle[] Rects = {URLRect, ConfirmationRect, DownloadingRect, DoneRect };
            Rects[(int)_currentStep].Fill = UnselectedBrush;
            Rects[step].Fill = (Brush)converter.ConvertFromString(SelectedColor);
            _currentStep = step;         
        }

        private void URLControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}

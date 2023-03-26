using System;
using System.Windows;
using System.Windows.Controls;
using YoutubeExplode.Videos;

namespace YoutubeDownloader.Controls
{
    /// <summary>
    /// Interaction logic for URLControl.xaml
    /// </summary>
    public partial class URLControl : UserControl
    {

        public Video TargetedVideo = null;


        public bool READY = false;

        public delegate void URLControlReadyDelegate(object sender, EventArgs e);
        public event URLControlReadyDelegate URLControlReady;

        public URLControl()
        {
            InitializeComponent();
        }

        private string _boxText = "";
        public string BoxText
        {
            get { return _boxText; }
            set
            {
                if (_boxText != value)
                {
                    _boxText = value;
                }
            }
        }

        public delegate void SearchControlGotoDelegate(object sender, EventArgs e);
        public event SearchControlGotoDelegate SearchControlGoto;

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            _boxText = URLTextBox.Text;

            if (!Helpers.IsValidYoutubeUrl(BoxText))
            {
                SearchControlGoto.Invoke(this, EventArgs.Empty);
                return;
            }
            else
            {
                TargetedVideo = await Helpers.GetVideoMetaDataAsync(BoxText);
                if (TargetedVideo != null)
                {
                    URLControlReady?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    MessageBox.Show("Failed to retrieve video metadata");
                }
            }
        }
    }
}

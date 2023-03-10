using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using YoutubeExplode.Videos;

namespace YoutubeDownloader.Controls
{
    /// <summary>
    /// Interaction logic for ConfirmationControl.xaml
    /// </summary>
    public partial class ConfirmationControl : UserControl
    {

        public Video SelectedVideo = null;

        public delegate void ConfirmationCanceledDelegate(object sender, EventArgs e);
        public event ConfirmationCanceledDelegate ConfirmationCanceled;

        public delegate void ConfirmationReadyDelegate(object sender, EventArgs e);
        public event ConfirmationReadyDelegate ConfirmationReady;

        public string SelectedFormat = "NONE";

        public ConfirmationControl()
        {
            InitializeComponent();
        }

        public void UpdateVideo(Video video)
        {
            SelectedVideo = video;

            UpdateFields();
        }

        private async void UpdateFields()
        {
            TitleBlock.Text = SelectedVideo?.Title;
            AuthorBlock.Text = SelectedVideo?.Author.ChannelTitle;
            LikesBlock.Text = SelectedVideo?.Engagement.LikeCount.ToString();
            DislikesBlock.Text = SelectedVideo?.Engagement.DislikeCount.ToString();

            BitmapImage Thumbnail = await Helpers.DownloadThumbnailAsync(SelectedVideo.Thumbnails[0].Url);

            ThumbNailSlot.Source = Thumbnail;
        }

        private void DownloadMp3Button_Click(object sender, RoutedEventArgs e)
        {
            SelectedFormat = "MP3";
            ConfirmationReady?.Invoke(sender, EventArgs.Empty);
        }

        private void DownloadMp4Button_Click(object sender, RoutedEventArgs e)
        {
            SelectedFormat = "MP4";
            ConfirmationReady?.Invoke(sender, EventArgs.Empty);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            ConfirmationCanceled?.Invoke(this, EventArgs.Empty);
        }
    }
}

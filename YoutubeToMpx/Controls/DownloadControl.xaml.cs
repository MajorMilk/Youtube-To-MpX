using System;
using System.Windows;
using System.Windows.Controls;
using YoutubeExplode.Videos;

namespace YoutubeDownloader.Controls
{
    /// <summary>
    /// Interaction logic for DownloadControl.xaml
    /// </summary>
    public partial class DownloadControl : UserControl
    {
        public DownloadControl()
        {
            InitializeComponent();
        }

        public delegate void DownloadDoneDelegate(object sender, EventArgs e);
        public event DownloadDoneDelegate DownloadDone;


        public async void DownloadMP3(Video video)
        {
            string filepath = Helpers.GetMp3Path(video);
            byte[] AudioData = await Helpers.DownloadAudioAsyncBytes(video);
            if (Helpers.ConvertToMp3(AudioData, filepath))
            { 
                AudioData = null;
                DownloadDone?.Invoke(this, EventArgs.Empty);
            }
            else
                MessageBox.Show("Failed to write file");
        }

        public async void DownloadMp4(Video video)
        {
            string filePath = Helpers.GetMp4Path(video);
            if(await Helpers.DownloadMuxedVideoAsync(video.Id, filePath))
            {
                DownloadDone?.Invoke(this, EventArgs.Empty);
            }
            else
                MessageBox.Show("Failed to write file");
        }
    }
}

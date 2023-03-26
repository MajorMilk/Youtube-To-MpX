using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using YoutubeDownloader;
using YoutubeExplode.Search;
using YoutubeExplode.Videos;

namespace YoutubeToMpx.Controls
{
    /// <summary>
    /// Interaction logic for SearchResultControl.xaml
    /// </summary>
    public partial class SearchResultControl : UserControl
    {
        public VideoSearchResult _video { get; set; }
        public Video? VidObject { get; set; } = null;
        public SearchResultControl(VideoSearchResult video)
        {
            InitializeComponent();
            _video = video;
            FillFields(); 
        }
        
        public static async Task<SearchResultControl> Create(VideoSearchResult video)
        {
            var temp = new SearchResultControl(video);
            await temp.Init();
            return temp;
        }


        public BitmapImage ThumbNail { get; set; }

        private async Task Init()
        {
            ThumbNail = new BitmapImage(new Uri(_video.Thumbnails[0].Url));
            ThumbNailSlot.Source = ThumbNail;
            await FillDescription();
        }



        private void FillFields()
        {
            TitleBlock.Text = _video.Title;
            ChannelBlock.Text = _video.Author.ChannelTitle;
            DurationBlock.Text = _video.Duration.ToString();
        }


        private async Task FillDescription()
        {
            var v = await Helpers.GetVideoMetaDataAsync(_video.Url);
            VidObject = v;
            if (v != null)
            {
                var description = v.Description;
                if(description.Length > 300)
                {
                    DescriptionBlock.Text = description[0..300];
                }
                else DescriptionBlock.Text = description;
                LikesBlock.Text = v.Engagement.LikeCount.ToString();
            }
        }
    }
}

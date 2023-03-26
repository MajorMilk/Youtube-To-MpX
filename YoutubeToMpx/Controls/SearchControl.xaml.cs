using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using YoutubeDownloader;
using YoutubeExplode.Search;
using YoutubeExplode.Videos;

namespace YoutubeToMpx.Controls
{
    /// <summary>
    /// Interaction logic for SearchControl.xaml
    /// </summary>
    public partial class SearchControl : UserControl
    {
        public string _search { get; set; }
        private SearchClient _client;

        private VideoSearchResult[] _results = new VideoSearchResult[3];
        private SearchResultControl[] _resultControls = new SearchResultControl[3];

        public delegate void BackButtonDelegate(object sender, EventArgs e);
        public event BackButtonDelegate SearchBackButton;

        public string SelectedFormat = "none";

        public Video? SelectedVideo = null;

        public SearchControl(string search)
        {
            InitializeComponent();
            _search = search;
            _client = Helpers.MAINCLIENT.Search;
            SearchBlock.Text = _search;
        }

        public static async Task<SearchControl> Create(string search)
        {
            var temp = new SearchControl(search);
            await temp.Init();
            return temp;
        }

        private async Task Init()
        {
            await GetResults();
            await LoadControls();
        }

        private async Task LoadControls()
        {
            for(int i = 0; i < _results.Length; i++)
            {
                _resultControls[i] = await SearchResultControl.Create(_results[i]);
                ControlBox.Items.Add(_resultControls[i]);
            }
        }

        private async Task GetResults()
        {
            var t = _client.GetVideosAsync(_search);
            var enumerator = t.GetAsyncEnumerator();

            for(int i = 0; i < 3; i++)
            {
                await enumerator.MoveNextAsync();
                _results[i] = enumerator.Current;
            }
        }

        private void BackButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SearchBackButton.Invoke(this, EventArgs.Empty);
        }


        public delegate void SearchReadyDelegate(object sender, EventArgs e);
        public event SearchReadyDelegate SearchReady;
        private void GoButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if(SelectedVideo != null)
            {
                SearchReady.Invoke(this, EventArgs.Empty);
            }
            
        }

        private void ControlBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var i = ControlBox.SelectedIndex;

            SelectedVideo = _resultControls[i].VidObject;
        }
    }
}

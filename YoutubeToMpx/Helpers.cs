using System;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

namespace YoutubeDownloader
{
    public static class Helpers
    {
        public static YoutubeClient MAINCLIENT = new();

        
        public static bool IsValidYoutubeUrl(string videoUrl)
        {
            Regex regex = new(@"(?:v=|\/)([a-zA-Z0-9_-]{11}).*");
            var match = regex.Match(videoUrl);
            return match.Success;
        }

        public static string GetVideoDir(Video v)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            path += @"\Downloads";

            string author = v.Author.ChannelTitle;

            path += $@"\{author}";
            return path;
            
        }

        public static string RemoveInvalidChars(string filename)
        {
            return string.Concat(filename.Split(Path.GetInvalidFileNameChars()));
        }

        public static string GetMp3Path(Video v)
        {
            string path = GetVideoDir(v);
            string temp = v.Title;
            temp = RemoveInvalidChars(temp);
            string fileName = $"{v.Author.ChannelTitle}-{temp}.mp3";
            return $@"{path}\{fileName}";
        }
        public static string GetMp4Path(Video v)
        {
            string path = GetVideoDir(v);
            string temp = v.Title;
            temp = RemoveInvalidChars(temp);
            string fileName = $"{v.Author.ChannelTitle}-{temp}.mp4";
            return $@"{path}\{fileName}";
        }

        public static bool ConvertToMp3(byte[] mp3Bytes, string filePath)
        {
            try
            {
                string directoryPath = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                File.WriteAllBytes(filePath, mp3Bytes);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        public static async Task<Video> GetVideoMetaDataAsync(string videoUrl)
        {
            try
            {
                if (!IsValidYoutubeUrl(videoUrl))
                {
                    return null;
                }

                var regex = new Regex(@"(?:v=|\/)([a-zA-Z0-9_-]{11}).*");
                var match = regex.Match(videoUrl);
                var videoId = match.Success ? match.Groups[1].Value : null;
                var video = await MAINCLIENT.Videos.GetAsync(videoId);
                return video;
            }
            catch
            {
                return null;
            }
        }

        public static async Task<BitmapImage> DownloadThumbnailAsync(string thumbnailUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(thumbnailUrl);
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStreamAsync();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = stream;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                return bitmap;
            }
        }


        public static async Task<byte[]> DownloadAudioAsyncBytes(Video video)
        {
            // Get the video stream info
            var streamInfoSet = await MAINCLIENT.Videos.Streams.GetManifestAsync(video.Id);

            // Get the audio stream info
            var audioStreamInfo = streamInfoSet.GetAudioOnlyStreams().GetWithHighestBitrate();

            // Convert the audio stream to a byte array
            using (var ms = new MemoryStream())
            {
                await MAINCLIENT.Videos.Streams.CopyToAsync(audioStreamInfo, ms);
                return ms.ToArray();
            }
        }

        public static async Task<bool> DownloadMuxedVideoAsync(string videoId, string filePath)
        {
            try
            {
                // Create the output directory if it doesn't exist
                string directoryPath = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Get the highest quality muxed stream for the video
                var streamManifest = await MAINCLIENT.Videos.Streams.GetManifestAsync(videoId);
                var streamInfo = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();

                // Download the video stream to a file
                await MAINCLIENT.Videos.Streams.DownloadAsync(streamInfo, filePath);

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        public static BitmapImage ConvertByteArrayToBitmapImage(byte[] byteArray)
        {
            // Create a new MemoryStream from the byte array
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                // Create a new BitmapImage and set its source to the MemoryStream
                BitmapImage bmpImage = new BitmapImage();
                bmpImage.BeginInit();
                bmpImage.StreamSource = ms;
                bmpImage.CacheOption = BitmapCacheOption.OnLoad;
                bmpImage.EndInit();

                return bmpImage;
            }
        }
    }
}

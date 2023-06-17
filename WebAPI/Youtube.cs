using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace YouFy.WebAPI
{
    public class Youtube
    {
        public YouTubeService Main()
        {
            return new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = Configuration.YT_API_KEY,
                ApplicationName = this.GetType().ToString()
            });
        }

        public async Task<string> SearchVideo(string searchItem)
        {
            var youtubeService = Main();
            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = searchItem;
            searchListRequest.Type = "video";
            searchListRequest.MaxResults = 1;

            var searchListResponse = await searchListRequest.ExecuteAsync();

            List<string> videos = new List<string>();

            foreach (var searchResult in searchListResponse.Items)
            {
                switch (searchResult.Id.Kind)
                {
                    case "youtube#video":
                        videos.Add(String.Format("{0} (https://www.youtube.com/watch?v={1})", searchResult.Snippet.Title, searchResult.Id.VideoId));
                        Console.WriteLine(String.Format("\nVideos:\n{0}\n------", string.Join("\n", videos)));
                        return searchResult.Id.VideoId;
                    //break;
                        
                }
            }

            return string.Empty;
        }

        public async Task CreatePlaylist(string playlistName, List<string> videoId)
        {
            UserCredential credential;
            string pathCredential = "YOUR_CREDENTIAL_PATH_HERE";
            using (var stream = new FileStream(pathCredential, FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                   GoogleClientSecrets.Load(stream).Secrets,
                   // This OAuth 2.0 access scope allows for full read/write access to the
                   // authenticated user's account.
                   new[] { YouTubeService.Scope.Youtube },
                   "user",
                   CancellationToken.None,
                   new FileDataStore(this.GetType().ToString())
                );
            }


            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = this.GetType().ToString()
            });

            // Create a new, private playlist in the authorized user's channel.
            var newPlaylist = new Playlist();
            newPlaylist.Snippet = new PlaylistSnippet();
            newPlaylist.Snippet.Title = playlistName + " - YouFy";
            newPlaylist.Snippet.Description = "Playlist criada através do YouFy, com muito amor - Vitor Capelos";
            newPlaylist.Status = new PlaylistStatus();
            newPlaylist.Status.PrivacyStatus = "public";
            newPlaylist = await youtubeService.Playlists.Insert(newPlaylist, "snippet,status").ExecuteAsync();

            // Add a video to the newly created playlist.
            var newPlaylistItem = new PlaylistItem();
            newPlaylistItem.Snippet = new PlaylistItemSnippet();
            newPlaylistItem.Snippet.PlaylistId = newPlaylist.Id;
            newPlaylistItem.Snippet.ResourceId = new ResourceId();
            newPlaylistItem.Snippet.ResourceId.Kind = "youtube#video";
            if (videoId.Count > 0)
            {
                foreach(var video in videoId)
                {
                    newPlaylistItem.Snippet.ResourceId.VideoId = video;
                    newPlaylistItem = await youtubeService.PlaylistItems.Insert(newPlaylistItem, "snippet").ExecuteAsync();

                    Console.WriteLine("Playlist item id {0} was added to playlist id {1}.", newPlaylistItem.Id, newPlaylist.Id);
                }
            }
        }
    }
}
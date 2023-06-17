using SpotifyAPI.Web;

namespace YouFy.WebAPI
{
    public class Spotify
    {
        private static SpotifyClient Main()
        {
            var config = SpotifyClientConfig
                .CreateDefault()
                .WithAuthenticator(new ClientCredentialsAuthenticator(Configuration.CLIENT_ID, Configuration.CLIENT_SECRET));

            return new SpotifyClient(config);
        }

        public static async Task<FullTrack> getSpotifyMusic(string idMusica)
        {
            SpotifyClient spotifyClient = Main();
            FullTrack track = await spotifyClient.Tracks.Get(idMusica);

            return track;
        }

        public static async Task<FullPlaylist> getSpotifyPlaylist(string urlPlaylist) 
        {
            SpotifyClient spotifyClient = Main();
            FullPlaylist playlist = await spotifyClient.Playlists.Get(urlPlaylist);
            return playlist;
        }
    }
}

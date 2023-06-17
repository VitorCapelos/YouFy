using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.Web;
using YouFy.Models;

namespace YouFy.WebAPI
{
    [ApiController]
    [Route("api/[controller]/")]
    public class YouFy : ControllerBase
    {
        [HttpGet("GetMusic")]
        public async Task<Musica> GetMusic(string idMusica)
        {
            try
            {
                FullTrack track = await Spotify.getSpotifyMusic(idMusica);
                return new Musica
                {
                    DataPublicacao = Convert.ToDateTime(track.Album.ReleaseDate),
                    DuracaoMs = track.DurationMs,
                    DuracaoStr = Util.ToMinute(track.DurationMs),
                    Titulo = track.Name,
                    Compositor = track.Artists,
                    Album = track.Album,
                    Explicty = track.Explicit,
                    UrlThumbnail = track.Album.Images[(int)Util.thumbnailQuality.Medium].Url,
                    UrlMusica = track.Album.ExternalUrls["spotify"]
                };
            }
            catch (Exception ex)
            {
                return new Musica();
            }
        }

        [HttpGet("GetPlaylist")]
        public async Task<Playlist> GetPlaylist(string urlPlaylist)
        {
            string idPlaylist = "";
            if (urlPlaylist.Contains("https://"))
                idPlaylist = Util.GetId(urlPlaylist);

            try
            {
                FullPlaylist playlist = await Spotify.getSpotifyPlaylist(idPlaylist);
                GetMusicYT(idPlaylist);
                return new Playlist
                {
                    nomePlaylist = playlist.Name,
                    qtdFaixa = playlist.Tracks?.Total,
                    nomeCriador = playlist.Owner?.DisplayName,
                    urlThumbnail = playlist.Images?[(int)Util.thumbnailQuality.Medium].Url,
                    descricao = playlist.Description
                };
            }
            catch (Exception ex)
            {
                return new Playlist();
            }
        }

        [HttpGet("GoogleCredential")]
        public async void GetGoogleCredential()
        {
            try
            {
                UserCredential credential;
                string pathCredential = "C:/Users/Vitor/source/repos/YouFy/client_secret.json";
                using (var stream = new FileStream(pathCredential, FileMode.Open, FileAccess.Read))
                {
                    credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                       GoogleClientSecrets.FromStream(stream).Secrets,
                       // This OAuth 2.0 access scope allows for full read/write access to the
                       // authenticated user's account.
                       new[] { YouTubeService.Scope.Youtube },
                       "user",
                       CancellationToken.None,
                       new FileDataStore(this.GetType().ToString())
                    );
                }
            } catch (Exception ex)
            {
                return;
            }
        }

        [HttpGet("GetMusicYT")]
        public async void GetMusicYT(string idPlaylist)
        {
            FullPlaylist playlistSP = await Spotify.getSpotifyPlaylist(idPlaylist);
            Youtube youtubeClient = new();
            //List<FullTrack> playlistSPs = new List<FullTrack>();
            List<string> videosIds = new List<string>();
            string nomePlaylist = playlistSP.Name == null ? "" : playlistSP.Name;

            if (playlistSP.Tracks != null && playlistSP.Tracks.Items != null)
            {
                for (int i = 0; i < playlistSP.Tracks.Items.Count; i++)
                {
                    if (playlistSP.Tracks.Items[i].Track is FullTrack track)
                    {
                        try
                        {
                            videosIds.Add(await youtubeClient.SearchVideo(track.Name + " - " + track.Artists[0].Name));
                        } 
                        catch (Exception ex)
                        {
                            await youtubeClient.CreatePlaylist(nomePlaylist, videosIds);
                        }
                        //Console.WriteLine("Música do Spotify: " + track.Name + " - " + track.Artists[0].Name);
                    }
                }
            }
            
            await youtubeClient.CreatePlaylist(nomePlaylist, videosIds);
            Console.WriteLine("Finalizado!");
        }
    }
}
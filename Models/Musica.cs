using SpotifyAPI.Web;

namespace YouFy.Models
{
    public class Musica
    {
        public DateTime DataPublicacao { get; set; }
        public string? Titulo { get; set; }
        public bool Explicty;
        public List<SimpleArtist>? Compositor { get; set; }
        public SimpleAlbum? Album { get; set; }
        public int DuracaoMs { get; set; }
        public string? DuracaoStr { get; set; }
        public string? UrlThumbnail { get; set; }
        public string? UrlMusica { get; set; }
    }
}
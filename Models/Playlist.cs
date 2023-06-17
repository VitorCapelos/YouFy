namespace YouFy.Models
{
    public class Playlist
    {
        public string? nomePlaylist { get; set; }
        public int? qtdFaixa { get; set; }
        public string? nomeCriador { get; set; }
        public List<Musica>? PlayList { get; set; }
        public string? urlThumbnail { get; set; }
        public string? descricao { get; set; }
    }
}

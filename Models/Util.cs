using SpotifyAPI.Web;

namespace YouFy.Models
{
    public class Util
    {
        public static string ToMinute(int miliseconds)
        {
            decimal segundos = (decimal)miliseconds / 1000;
            decimal minutos = Math.Truncate((segundos % 3600) / 60);
            decimal segundosM = Math.Truncate((segundos % 3600) % 60);

            return minutos.ToString() + ":" + segundosM;
        }

        public static string GetId(string url)
        {
            return url.Split("/").Last().Split("?")[0];
        }


        public enum thumbnailQuality
        {
            High = 0,
            Medium = 1,
            Small = 2
        }
    }
}

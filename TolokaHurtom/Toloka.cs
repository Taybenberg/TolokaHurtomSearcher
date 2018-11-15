using Newtonsoft.Json;
using System.Net;
using System.Text.RegularExpressions;
using System.Text;

namespace TolokaHurtom
{
    public class Toloka
    {
        private const string TolokaUrl = "https://toloka.to/api.php";
        private const string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.102 Safari/537.36 Edge/18.17763";

        public class Hurtom
        {
            public string id { get; set; }
            public string link { get; set; }
            public string title { get; set; }
            public string forum_name { get; set; }
            public string forum_parent { get; set; }
            public string comments { get; set; }
            public string size { get; set; }
            public string seeders { get; set; }
            public string leechers { get; set; }
            public string complete { get; set; }

            public override string ToString()
            {
                return
                    $"Id: {id}" +
                    $"\nПосилання: {link}" +
                    $"\nНазва: {title}" +
                    $"\nФорум: {forum_name}" +
                    $"\nТолока: {forum_parent}" +
                    $"\nКоментарів: {comments}" +
                    $"\nРозмір: {size}" +
                    $"\nРоздають: {seeders}" +
                    $"\nЗавантажують: {leechers}" +
                    $"\nЗавантажень: {complete}";
            }
        }

        Hurtom[] result;

        public Toloka(string search1)
        {
            using (var webClient = new WebClient())
            {
                webClient.Headers.Add("user-agent", UserAgent);
                webClient.QueryString.Add("search", Regex.Escape(search1));
                webClient.UseDefaultCredentials = true;

                fillArray(webClient.DownloadString(TolokaUrl));
            }
        }

        public Toloka(string search1, string search2)
        {
            using (var webClient = new WebClient())
            {
                webClient.Headers.Add("user-agent", UserAgent);
                webClient.QueryString.Add("search", Regex.Escape(search1));
                webClient.QueryString.Add("search2", Regex.Escape(search2));
                webClient.UseDefaultCredentials = true;

                fillArray(webClient.DownloadString(TolokaUrl));
            }
        }

        public Toloka(string search1, string search2, string search3)
        {
            using (var webClient = new WebClient())
            {
                webClient.Headers.Add("user-agent", UserAgent);
                webClient.QueryString.Add("search", Regex.Escape(search1));
                webClient.QueryString.Add("search2", Regex.Escape(search2));
                webClient.QueryString.Add("search3", Regex.Escape(search3));
                webClient.UseDefaultCredentials = true;

                fillArray(webClient.DownloadString(TolokaUrl));
            }
        }

        public Hurtom[] ToArray()
        {
            return result;
        }

        public string ToString(string separator = null)
        {
            StringBuilder res = new StringBuilder();

            foreach (var r in result)
                res.Append($"{r}{separator}");

            return res.ToString();
        }

        public string[] ToStringArray()
        {
            string[] res = new string[result.Length];

            for (int i = 0; i < result.Length; i++)
                res[i] = result[i].ToString();

            return res;
        }

        void fillArray(string query)
        {
            string json = WebUtility.HtmlDecode(Regex.Unescape(query));
            result = JsonConvert.DeserializeObject<Hurtom[]>(json);
        }
    }
}
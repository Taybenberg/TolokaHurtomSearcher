using System.Net;

namespace TolokaHurtom
{
    public class Hurtom
    {
        private string _id, _link, _title, _forum_name, _forum_parent, _comments, _size, _seeders, _leechers, _complete;

        public string id
        {
            get { return _id; }
            set { _id = WebUtility.HtmlDecode(value); }
        }

        public string link
        {
            get { return _link; }
            set { _link = WebUtility.HtmlDecode(value); }
        }
        public string title
        {
            get { return _title; }
            set { _title = WebUtility.HtmlDecode(value); }
        }

        public string forum_name
        {
            get { return _forum_name; }
            set { _forum_name = WebUtility.HtmlDecode(value); }
        }

        public string forum_parent
        {
            get { return _forum_parent; }
            set { _forum_parent = WebUtility.HtmlDecode(value); }
        }

        public string comments
        {
            get { return _comments; }
            set { _comments = WebUtility.HtmlDecode(value); }
        }

        public string size
        {
            get { return _size; }
            set { _size = WebUtility.HtmlDecode(value); }
        }

        public string seeders
        {
            get { return _seeders; }
            set { _seeders = WebUtility.HtmlDecode(value); }
        }

        public string leechers
        {
            get { return _leechers; }
            set { _leechers = WebUtility.HtmlDecode(value); }
        }

        public string complete
        {
            get { return _complete; }
            set { _complete = WebUtility.HtmlDecode(value); }
        }

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
}
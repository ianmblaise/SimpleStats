using System;
using System.Net;
using System.Threading.Tasks;

namespace SimpleStats.LolSkill
{
    public class HtmlPage
    {
        public HtmlPage(Uri accessUri)
        {
            AccessUri = accessUri;
        }

        public Uri AccessUri { get; set; }

        public string HtmlString { get; set; }

        public async Task<string> DownloadPage()
        {
            var html = await new WebClient().DownloadStringTaskAsync(AccessUri);
            return html;
        }

        public async Task<string> DownloadPage(string url)
        {
            var html = await new WebClient().DownloadStringTaskAsync(url);
            return html;
        }



        public override string ToString() => HtmlString;
    }
}
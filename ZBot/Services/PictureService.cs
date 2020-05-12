using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ZBot
{
    public static class PictureService
    {
        public static async Task<Stream> GetPictureAsync(string url)
        {
            var http = new HttpClient();
            HttpResponseMessage resp = await http.GetAsync(url);
            return await resp.Content.ReadAsStreamAsync();
        }
    }
}
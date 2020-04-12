using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ZBot
{
    public static class PictureService
    {
        public static async Task<Stream> GetPictureAsync(string url)
        {
            HttpClient _http = new HttpClient();
            var resp = await _http.GetAsync(url);
            return await resp.Content.ReadAsStreamAsync();
        }
    }
}
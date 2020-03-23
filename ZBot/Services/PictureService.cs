
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace _02_commands_framework.Services
{
    public static class PictureService
    {
        public static async Task<Stream> GetCatPictureAsync()
        {
            HttpClient _http = new HttpClient();
            var resp = await _http.GetAsync("https://cataas.com/cat");
            return await resp.Content.ReadAsStreamAsync();
        }
    }
}
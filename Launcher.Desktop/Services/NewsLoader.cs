using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Launcher.Common.Models;
using Launcher.Desktop;
using Launcher.Desktop.Models;

namespace Launcher;
public class NewsLoader : INewsLoader
{
    public async Task<IEnumerable<LoadedNews>?> GetNews()
    {
        const string newsUrl = Globals.NewsUrl;

        using var client = new HttpClient();
        client.Timeout = Globals.RequestTimeout;

        IEnumerable<LoadedNews>? newsList = await client.GetFromJsonAsync<IEnumerable<LoadedNews>>(newsUrl);
        if (newsList is null)
            return null;
        
        await LoadImages(client, newsList);
        return newsList;
    }

    private static async Task LoadImages(HttpClient client, IEnumerable<LoadedNews> newsList)
    {
        foreach (LoadedNews news in newsList)
        {
            if (string.IsNullOrEmpty(news.ImageUrl))
                continue;

            byte[] bytes;
            try
            {
                bytes = await client.GetByteArrayAsync(news.ImageUrl);
            }
            catch(Exception)
            {
                continue;
            }

            var stream = new MemoryStream(bytes);
            news.Image = new Bitmap(stream);
        }
    }
}

using Downloader;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Launcher.Desktop.Services
{
    public class UpdateInstaller
    {
        private readonly DownloadService _downloadService = new();

        public UpdateInstaller()
        {
            _downloadService.DownloadProgressChanged += DownloadService_DownloadProgressChanged;
        }

        public event EventHandler<DownloadProgressChangedEventArgs> DownloadProgressChanged;

        public async Task InstallUpdates(IEnumerable<string> updates, CancellationToken token = default)
        {
            foreach (var patch in updates)
            {
                await Install(patch, token);
            }
        }

        public async Task Install(string patch, CancellationToken token = default)
        {
            var archive = await _downloadService.DownloadFileTaskAsync(new DownloadPackage()
            {
                Urls = new string[] { patch },
            }, token);
            if(archive is not null)
                ZipFile.ExtractToDirectory(archive, Environment.CurrentDirectory, true);
        }

        private void DownloadService_DownloadProgressChanged(object? sender, Downloader.DownloadProgressChangedEventArgs e)
        {
            DownloadProgressChanged?.Invoke(sender, e);
        }
    }
}

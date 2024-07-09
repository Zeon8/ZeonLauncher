using Downloader;
using Launcher.Common.Models;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;

namespace Launcher.Desktop.Services
{
    public class UpdateInstaller
    {
        public event EventHandler<DownloadProgressChangedEventArgs>? DownloadProgressChanged;
        
        private readonly DownloadService _downloadService = new();

        public UpdateInstaller()
        {
            _downloadService.DownloadProgressChanged += DownloadService_DownloadProgressChanged;
        }


        public async Task InstallUpdates(IEnumerable<Patch> updates, CancellationToken token = default)
        {
            foreach (var patch in updates)
            {
                await Install(patch.DownloadUrl, token);
            }
        }

        public async Task Install(string patch, CancellationToken token = default)
        {
            var archive = await _downloadService.DownloadFileTaskAsync(patch, token);
            if(archive is not null)
                ZipFile.ExtractToDirectory(archive, Environment.CurrentDirectory, true);
        }

        private void DownloadService_DownloadProgressChanged(object? sender, Downloader.DownloadProgressChangedEventArgs e)
        {
            DownloadProgressChanged?.Invoke(sender, e);
        }
    }
}

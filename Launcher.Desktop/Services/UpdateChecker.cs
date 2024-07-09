using Launcher.Common.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Launcher.Desktop.Services
{
    public class UpdateChecker
    {
        public string FullGameDownloadUrl { get; } = Globals.FullGameDownloadUrl;
        public bool IsGameInstalled => File.Exists(Globals.ExebutableFileName);
        public IEnumerable<Patch> NeededPatches => _patches.Where(p => p.Id > _currentInstalledPatch?.Id);
        public bool UpdateAvailable => _currentInstalledPatch != LatestPatch;

        private IEnumerable<Patch> _patches = null!;
        private Patch LatestPatch => _patches.Last();
        private Patch? _currentInstalledPatch;

        public async Task CheckUpdates()
        {
            using var client = new HttpClient();
            client.Timeout = Globals.RequestTimeout;

            _patches = await client.GetFromJsonAsync<IEnumerable<Patch>>(Globals.CheckUpdatesUrl)
                ?? throw new AggregateException("Failed to receive updates");

            if (File.Exists(Globals.VersionFileName))
            {
                string? version = File.ReadAllText(Globals.VersionFileName);
                _currentInstalledPatch = _patches.FirstOrDefault(p => p.Version == version);
            }
        }
    }
}

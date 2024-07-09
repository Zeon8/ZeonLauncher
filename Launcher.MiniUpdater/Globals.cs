using System;

namespace Launcher.MiniUpdater
{
    public static class Globals
    {
        public const string CheckUpdatesUrl = $"{BaseUrl}/api/Updates/";
        public const string ExebutableFileName = "Test.exe";
        public const string VersionFileName = "version.txt";

        public static readonly TimeSpan RequestTimeout = TimeSpan.FromSeconds(10); 

        private const string BaseUrl = "https://localhost:5001";
    }
}

using System;

namespace Launcher.Desktop
{
    public static class Globals
    {

        public const string NewsUrl = $"{BaseUrl}/api/News/";
        public const string FullGameDownloadUrl = $"{BaseUrl}/Game.zip";
        public const string CheckUpdatesUrl = $"{BaseUrl}/api/Updates/";

        public const string ExebutableFileName = "Test.exe";
        public const string VersionFileName = "version.txt";

        private const string BaseUrl = "https://localhost:5001";
        public static readonly TimeSpan RequestTimeout = TimeSpan.FromSeconds(2); 
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launcher.MiniUpdater.Services
{
    public class GameStarter
    {
        public void StartGame()
        {
            Process.Start(Globals.ExebutableFileName);
        }
    }
}

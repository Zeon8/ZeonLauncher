using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Launcher.Common.Models;
using Launcher.Desktop.Models;

namespace Launcher;
public interface INewsLoader
{
    Task<IEnumerable<LoadedNews>?> GetNews();
}

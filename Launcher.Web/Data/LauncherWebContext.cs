using Microsoft.EntityFrameworkCore;
using Launcher.Common.Models;

namespace Launcher.Web.Data
{
    public class LauncherWebContext : DbContext
    {
        public LauncherWebContext (DbContextOptions<LauncherWebContext> options)
            : base(options)
        {
        }

        public DbSet<News> News { get; set; } = default!;

        public DbSet<Patch> Updates { get; set; } = default!;
    }
}

using Commander.Models;
using Microsoft.EntityFrameworkCore;

namespace Commander.Data
{
    public class CommandContext : DbContext
    {
        public CommandContext(DbContextOptions<CommandContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Command> Commands { get; set; }


    }
}

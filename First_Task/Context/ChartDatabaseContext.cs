using First_Task.Models;
using System.Data.Entity;

namespace First_Task.Context
{
    public class ChartDatabaseContext : DbContext
    {
        public ChartDatabaseContext()
            : base("ChartDatabase")
        {
        }

        public DbSet<FileLine> FileLines { get; set; }

        public DbSet<TextFile> TextFiles { get; set; }
    }
}

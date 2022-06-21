using System.Data.Entity;

namespace Pomodoro_Clock.DB.Entities
{
    public class DbPomodoro : DbContext
    {
        public DbPomodoro(string connection) : base(connection) { }
        public DbPomodoro() { }
        public virtual DbSet<Pomodoro> Pomodoros { get; set; }
    }
}

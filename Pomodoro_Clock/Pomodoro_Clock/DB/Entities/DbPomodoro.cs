using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro_Clock.DB.Entities
{
    public class DbPomodoro: DbContext
    {
        public DbPomodoro(string connection) : base(connection) { }
        public DbPomodoro() { }
        public virtual DbSet<Pomodoro> Pomodoros { get; set; }
    }
}

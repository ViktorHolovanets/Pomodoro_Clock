using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro_Clock.DB.Entities
{
    public class Pomodoro
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; } // дата створення
        public string NamePomodoro { get; set; }  // назва
        public int DurationPomodoro { get; set; }  // тривалість часу одного Pomodoro 
        public int ShortPause { get; set; }  // коротка пауза
        public int LongPause { get; set; }  // довга пауза
        public int LongBreakDelay { get; set; }
        public bool IsAutoStart { get; set; }
        public bool IsAutoPause { get; set; }
        public int DailGoal { get; set; }
        public Pomodoro()
        {
            NamePomodoro = "1";
            DurationPomodoro = 60;
            ShortPause = 60;
            LongPause = 120;
            LongBreakDelay = 3;
            DailGoal = 3;
        }
    }
}

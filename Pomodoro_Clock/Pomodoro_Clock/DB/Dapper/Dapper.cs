using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro_Clock.DB.Dapper
{
    public class Dapper
    {
        private readonly SqlConnection con;
        public Dapper(string connection)
        {
            con = new SqlConnection(connection);
        }
        public List<ResultDapper> DayPomodoro()
        {
            List<ResultDapper> res = new List<ResultDapper>();
            con.Open();
            var sql = "select Created as NameResult, COUNT(*) as NumberResult " +
                "from Pomodoroes Group by Pomodoroes.Created  " +
                "order by NumberResult DESC";
            res = con.Query<ResultDapper>(sql).ToList();
            con.Close();
            return res;
        }
        public List<ResultDapper> MonthPomodoro()
        {
            List<ResultDapper> res = new List<ResultDapper>();
            con.Open();
            var sql = "select DATENAME(month, Created) as NameResult, COUNT(*) as NumberResult from Pomodoroes Group by DATENAME(month, Created) order by  NumberResult DESC";
            res = con.Query<ResultDapper>(sql).ToList();
            con.Close();
            return res;
        }
        public List<ResultDapper> MaxDurationPomodoro()
        {
            List<ResultDapper> res = new List<ResultDapper>();
            con.Open();
            var sql = "select NamePomodoro as NameResult, DurationPomodoro*DailGoal as NumberResult " +
                "from Pomodoroes " +
                "order by(DurationPomodoro* DailGoal) Desc";
            res = con.Query<ResultDapper>(sql).ToList();
            con.Close();
            return res;
        }
        public List<ResultDapper> MaxNumberPomodoro()
        {
            List<ResultDapper> res = new List<ResultDapper>();
            con.Open();
            var sql = "select NamePomodoro as NameResult, DailGoal as NumberResult " +
                "from Pomodoroes " +
                "order by DailGoal Desc";
            res = con.Query<ResultDapper>(sql).ToList();
            con.Close();
            return res;
        }
    }
}

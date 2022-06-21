using System;
using PomodoroRegister.SubSubject;
using PomodoroRegister.SubSubject.Path;

namespace PomodoroRegister
{
    public class ProgramScriptFille
    {
        RegSubject reg = new RegSubject();
        pathSubject path = new pathSubject();

        public void Start()
        {
            reg.Start();
            path.Start();
        }
        public void Update()
        {
           // path.AddSubjectSett("Parametr 1", 1);
           // path.GetSubjectSett("Parametr 1");
            
            path.Exit();
            reg.Exit();
        }
    }
}

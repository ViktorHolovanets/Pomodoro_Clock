using System;

namespace PomodoroRegister
{
    class Program
    {
        static void Main(string[] args)
        {
            ProgramScriptFille programScript = new ProgramScriptFille();

            programScript.Start();
            programScript.Update();

            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FileBackuper.Logic;

namespace FileBackuper.BackuperConsole
{
    class BackuperConsole
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                //TODO: Logovat??
                Console.WriteLine("Usage: <THIS EXE> PROFILE_NAME");
            }
            else
            {
                try
                {
                    // Instanciovat Scheduler
                    // Zazalohovat profil
                    // Naplanovat dalsi zalohovani
                    Scheduler sch = new Scheduler();
                    sch.OnSchedule(args[0]);
                }
                catch (SchedulerException e)
                {
                    Logging.LoggerFactory.Logger.Fatal("BackuperConsole: SchedulerException has been thrown! Message: {0}", e.Message);
                }
            }
        }
    }
}

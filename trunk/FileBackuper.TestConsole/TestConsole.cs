using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using FileBackuper.Logging;
using FileBackuper.Logic;
using FileBackuper.Model;

using Ionic.Zip;
using Quartz;
using Quartz.Impl;

namespace FileBackuper.TestConsole
{
    public delegate bool ConsoleItemSelected(List<MenuItem> submenu);

    public class MenuItem
    {
        private ConsoleKey key;
        public ConsoleKey Key
        {
            get { return key; }
            set { key = value; }
        }

        private string name = "";
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string description = "";
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private List<MenuItem> submenu = new List<MenuItem>();
        public List<MenuItem> Submenu
        {
            get { return submenu; }
            set { submenu = value; }
        }

        private ConsoleItemSelected action;
        public ConsoleItemSelected Action
        {
            get { return action; }
            set { action = value; }
        }
    }

    class TestConsole
    {
        #region TestConsole logic
        private static List<MenuItem> mainMenu = new List<MenuItem>();

        private static List<MenuItem> selectedMenu = null;

        static void Main(string[] args)
        {
            InitializeMenu();

            Console.WriteLine("FileBackuper.TestConsole, version: 1.0");

            do
            {
                Console.WriteLine("");
                PrintMenu(selectedMenu);
                Console.WriteLine("");
            } while (MenuSelection(selectedMenu, Console.ReadKey(true).Key));
        }

        static void PrintMenu(List<MenuItem> menu)
        {
            foreach (MenuItem item in menu)
            {
                string desc = (item.Description.Length > 0) ? String.Format("({0})", item.Description) : "";
                Console.WriteLine("  {0}) {1} {2}", item.Key.ToString(), item.Name, desc);
            }
        }

        static bool MenuSelection(List<MenuItem> menu, ConsoleKey key)
        {
            foreach (MenuItem item in menu)
            {
                if (item.Key.Equals(key))
                {
                    return item.Action(item.Submenu);
                }
            }
            return true;
        }
        #endregion

        #region Initialize menu
        static void InitializeMenu()
        {
            // Exit menu item
            MenuItem exit = new MenuItem();
            exit.Name = "Exit";
            exit.Description = "Closes this program";
            exit.Key = ConsoleKey.X;
            exit.Action = delegate { Console.WriteLine("Bye bye"); return false; };

            // Try to zip folder
            MenuItem zipFolder = new MenuItem();
            zipFolder.Name = "ZipFolder";
            zipFolder.Description = @"Tries to zip folder C:\Temp\Log2 to C:\Temp\Lgo2.zip";
            zipFolder.Key = ConsoleKey.F;
            zipFolder.Action = ZipFolder;

            // Test Logger
            MenuItem logger = new MenuItem();
            logger.Name = "Logger";
            logger.Description = "Test FileBackuper.Model.Logger";
            logger.Key = ConsoleKey.L;
            logger.Action = TestLogger;

            // Test LoggerFactory
            MenuItem factory = new MenuItem();
            factory.Name = "LoggerFactory";
            factory.Description = "Test LoggerFactory, factory for creating automatic log files";
            factory.Key = ConsoleKey.O;
            factory.Action = TestLoggerFactory;

            // Test Zipper
            MenuItem zipper = new MenuItem();
            zipper.Name = "Zipper";
            zipper.Description = "Tries to use dummy profile and test Zipper";
            zipper.Key = ConsoleKey.Z;
            zipper.Action = TestZipper;

            // Test Quartz
            MenuItem quartz = new MenuItem();
            quartz.Name = "Quartz Scheduler";
            quartz.Description = "Test Quartz CronTrigger";
            quartz.Key = ConsoleKey.Q;
            quartz.Action = TestQuartzScheduling;

            mainMenu.Add(zipFolder);
            mainMenu.Add(logger);
            mainMenu.Add(factory);
            mainMenu.Add(zipper);
            mainMenu.Add(quartz);

            mainMenu.Add(exit);
            selectedMenu = mainMenu;
        }
        #endregion

        #region MenuItems methods

        static bool ZipFolder(List<MenuItem> submenu)
        {
            Console.WriteLine("Initiating DotNetZip ...");
            string dir = @"C:\Temp\Log2";
            string zipfile = @"C:\Temp\Log2.zip";
            try
            {
                using (ZipFile zip = new ZipFile())
                {
                    Console.WriteLine("Zipping folder {0} to {1} ...", dir, zipfile);
                    zip.StatusMessageTextWriter = System.Console.Out;
                    zip.AddDirectory(dir);
                    zip.Save(zipfile);
                }
                Console.WriteLine("Zip created");
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
            Console.WriteLine("Test completed.");

            return true;
        }

        static bool TestLogger(List<MenuItem> submenu)
        {
            Console.WriteLine("Testing Logger ...");
            LoggerSetup setup = new LoggerSetup();
            setup.Output = @"C:\Temp\FileBackuper\Log\FileBackuper.log";

            Console.WriteLine("Writing to log {0} ...", setup.Output);
            using (Logger log = new Logger(setup))
            {
                log.AddFatal("Some fatal error occured!! :D");
                log.AddError("Less important error.");
                log.AddWarning("Warning, nobody cares ...");
                log.AddNote("Note? Why is it here??");
            }
            Console.WriteLine("Test completed.");

            return true;
        }

        static bool TestLoggerFactory(List<MenuItem> submenu)
        {
            Console.WriteLine("Getting instance of Logger ...");
            Logger log = LoggerFactory.Logger;
            Console.WriteLine("Writing to log file {0} ...", log.Setup.Output);

            log.AddFatal("Some fatal error occured!! :D");
            log.AddError("Less important error.");
            log.AddWarning("Warning, nobody cares ...");
            log.AddNote("Note? Why is it here??");

            Console.WriteLine("Test completed.");

            return true;
        }

        static bool TestZipper(List<MenuItem> submenu)
        {
            Console.WriteLine("Creating dummy profile ...");
            Profile p = new Profile();
            p.Name = "Log2";
            p.OutputFolder = @"C:\Temp\FileBackuper\Backups";
            p.FileNamePattern = "ProfileName_yyyy-MM-dd";
            //p.FileNamePattern = "ProfileName";
            p.Units.Add(new ZipUnit(UnitType.Folder, @"C:\Temp\Log2"));

            Console.WriteLine("Creating zipper instance ...");
            Zipper zppr = new Zipper();
            zppr.Zip(p);
            Console.WriteLine("Test completed.");

            return true;
        }

        static bool TestQuartzScheduling(List<MenuItem> submenu)
        {
            Console.WriteLine("Testing Quartz scheduler ...");
            Console.WriteLine("This test takes 20sec., every 5sec will be trigged a message from Job.");

            ISchedulerFactory sf = new StdSchedulerFactory();
            IScheduler sched = sf.GetScheduler();

            JobDetail job = new JobDetail("job1", "group1", typeof(QuartzJob));
            CronTrigger trigger = new CronTrigger("trigger1", "group1", "job1", "group1", "0/5 * * * * ?");
            sched.AddJob(job, true);
            sched.ScheduleJob(trigger);
            sched.Start();

            Thread.Sleep(20000);
            sched.Shutdown();
            Console.WriteLine("Test completed.");

            return true;
        }

        #endregion
    }

    public class QuartzJob : IJob
    {

        public void Execute(JobExecutionContext context)
        {
            Console.WriteLine("Hello from QuartzJob: {0:HH:mm:ss,ffff}", DateTime.Now);
        }
    }
}

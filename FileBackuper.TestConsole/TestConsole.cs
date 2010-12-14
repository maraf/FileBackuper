using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

using FileBackuper.Logging;
using FileBackuper.Logic;
using FileBackuper.Model;

using Ionic.Zip;
using Quartz;
using Quartz.Impl;
using TaskScheduler;

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
            Console.WriteLine("Location: {0}", Assembly.GetExecutingAssembly().Location.Substring(0, Assembly.GetExecutingAssembly().Location.LastIndexOf('\\')));

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

            // Test Backuper
            MenuItem backuper = new MenuItem();
            backuper.Name = "Backuper";
            backuper.Description = "Tries to use Backuper on dummy profile";
            backuper.Key = ConsoleKey.B;
            backuper.Action = TestBackuper;

            // Test TaskScheduler
            MenuItem tasksch = new MenuItem();
            tasksch.Name = "TaskScheduler";
            tasksch.Description = "Creates Windows task (run notepad after some timespan).";
            tasksch.Key = ConsoleKey.S;
            tasksch.Action = TestTaskScheduler;

            // Test scheduler
            MenuItem scheduler = new MenuItem();
            scheduler.Name = "Scheduler(MY)";
            scheduler.Description = "Runs scheduling tasks";
            scheduler.Key = ConsoleKey.M;
            scheduler.Action = TestScheduler;

            // HourMinute
            MenuItem hm = new MenuItem();
            hm.Name = "HourMinute";
            hm.Description = "Tests HourMinute class";
            hm.Key = ConsoleKey.H;
            hm.Action = TestHourMinute;

            MenuItem sm = new MenuItem();
            sm.Name = "SettingsManager";
            sm.Description = "Test SettingsManager";
            sm.Key = ConsoleKey.N;
            sm.Action = TestSettingsManager;

            mainMenu.Add(zipFolder);
            mainMenu.Add(logger);
            mainMenu.Add(factory);
            mainMenu.Add(zipper);
            mainMenu.Add(quartz);
            mainMenu.Add(backuper);
            mainMenu.Add(tasksch);
            mainMenu.Add(scheduler);
            mainMenu.Add(hm);
            mainMenu.Add(sm);

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
                log.Fatal("Some fatal error occured!! :D");
                log.Error("Less important error.");
                log.Warn("Warning, nobody cares ...");
                log.Info("Note? Why is it here??");

                log.Fatal("Some string {0:dd.MM.yyyy}", DateTime.Now);
                log.Error("Some string {0:dd.MM.yyyy}", DateTime.Now);
                log.Warn("Some string {0:dd.MM.yyyy}", DateTime.Now);
                log.Info("Some string {0:dd.MM.yyyy}", DateTime.Now);
            }
            Console.WriteLine("Test completed.");

            return true;
        }

        static bool TestLoggerFactory(List<MenuItem> submenu)
        {
            Console.WriteLine("Getting instance of Logger ...");
            Logger log = LoggerFactory.Logger;
            Console.WriteLine("Writing to log file {0} ...", log.Setup.Output);

            log.Fatal("Some fatal error occured!! :D");
            log.Error("Less important error.");
            log.Warn("Warning, nobody cares ...");
            log.Info("Note? Why is it here??");

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
            p.Units.Add(new ZipUnit(UnitType.File, @"D:\Documents\Documents\EBooks\GWT\2343932-Google-Web-Toolkit-Tutorial.pdf"));

            Console.WriteLine("Creating zipper instance ...");
            Console.WriteLine("Profile(Name={0},OutputFolder={1}).", p.Name, p.OutputFolder);
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

        static bool TestBackuper(List<MenuItem> submenu)
        {
            Console.WriteLine("Testing Backuper using dummy profile ...");
            string root = @"C:\Temp\FileBackuper\Profiles\Dummy profile";

            Console.WriteLine("Creating dummy files ...");
            Directory.CreateDirectory(root);
            File.Create(root + @"\Dummy1.zip").Close();
            File.Create(root + @"\Dummy2.zip").Close();
            File.Create(root + @"\Dummy3.zip").Close();
            File.Create(root + @"\Dummy4.zip").Close();
            File.Create(root + @"\ToBackup.txt").Close();

            Console.WriteLine("Creating profile ...");
            Profile p = new Profile();
            p.Name = "Dummy";
            p.FileNamePattern = "ProfileName_yyyy-MM-dd";
            p.OutputFolder = root;
            p.Units.Add(new ZipUnit(UnitType.File, root + @"\ToBackup.txt"));
            p.VersionsNames.Add(root + @"\Dummy1.zip");
            p.VersionsNames.Add(root + @"\Dummy2.zip");
            p.VersionsNames.Add(root + @"\Dummy3.zip");
            p.VersionsNames.Add(root + @"\Dummy4.zip");
            p.NumberOfVersions = 3;

            Console.WriteLine("Creating Backuper and calling Backup method ...");
            Backuper b = new Backuper();
            b.Backup(p);

            Console.WriteLine("Test completed.");
            return true;
        }

        static bool TestTaskScheduler(List<MenuItem> submenu)
        {
            Console.WriteLine("Testing TaskScheduler lib ...");

            #region DUMMY PROFILE
            /*string root = @"C:\Temp\FileBackuper\Profiles\Dummy profile";

            Console.WriteLine("Creating dummy files ...");
            Directory.CreateDirectory(root);
            File.Create(root + @"\Dummy1.zip").Close();
            File.Create(root + @"\Dummy2.zip").Close();
            File.Create(root + @"\Dummy3.zip").Close();
            File.Create(root + @"\Dummy4.zip").Close();
            File.Create(root + @"\ToBackup.txt").Close();

            Console.WriteLine("Creating profile ...");
            Profile p = new Profile();
            p.Name = "Dummy";
            p.FileNamePattern = "ProfileName_yyyy-MM-dd_HH-mm-ss";
            p.OutputFolder = root;
            p.Period = TimePeriod.OneHour;
            p.Units.Add(new ZipUnit(UnitType.File, root + @"\ToBackup.txt"));
            p.VersionsNames.Add(root + @"\Dummy1.zip");
            p.VersionsNames.Add(root + @"\Dummy2.zip");
            p.VersionsNames.Add(root + @"\Dummy3.zip");
            p.VersionsNames.Add(root + @"\Dummy4.zip");
            p.NumberOfVersions = 4;*/
            #endregion

            string name = String.Format("FileBackuperTestConsole{0:ffff}", DateTime.Now);
            Console.WriteLine("Creating Task called '{0}' ...", name);
            using (ScheduledTasks st = new ScheduledTasks())
            {
                DateTime dt = DateTime.Now.Add(new TimeSpan(0, 2, 0));
                Task t = st.CreateTask(name);
                t.Triggers.Add(new RunOnceTrigger(dt));
                t.ApplicationName = @"C:\Windows\System32\notepad.exe";
                t.Creator = "Marek";
                t.WorkingDirectory = @"C:\Temp";
                t.Flags = TaskFlags.RunOnlyIfLoggedOn;
                t.SetAccountInformation(@"NT AUTHORITY\LOCAL SERVICE", (string) null);
                t.MaxRunTime = new TimeSpan(1, 0, 0);
                t.Priority = System.Diagnostics.ProcessPriorityClass.High;
                t.IdleWaitDeadlineMinutes = 20;
                t.IdleWaitMinutes = 10;

                t.Save();
                t.Close();
                Console.WriteLine("Task created ...");
                Console.WriteLine("Wait, created: {0:HH:mm:ss,ffff}, will trigger: {1:HH:mm:ss,ffff} ...", DateTime.Now, dt);
            }

            return true;
        }

        static bool TestScheduler(List<MenuItem> submenu)
        {
            Console.WriteLine("Testing Scheduler(MY) ...");
            Console.WriteLine("Using profile called '{0}' ...", "TestScheduler");

            Logic.Scheduler sc = new Logic.Scheduler();
            sc.OnSchedule("New Profile");

            Console.WriteLine("Test completed.");
            return true;
        }

        static bool TestHourMinute(List<MenuItem> submenu)
        {
            Console.WriteLine("Testing class HourMinute ...");

            HourMinute hm = new HourMinute(5, 12);

            Console.WriteLine("HourMinute({0},{1})", hm.Hour, hm.Minute);

            Console.WriteLine("ToMinutes = {0}", hm.ToMinutes());
            Console.WriteLine("ToHours = {0}", hm.ToHours());
            Console.WriteLine("Test completed.");

            return true;
        }

        static bool TestSettingsManager(List<MenuItem> submenu)
        {
            Console.WriteLine(@"Loads path a then set it to C:\Temp\FileBackuper\Config.xml"); ;
            //Settings s = new Settings();
            Configuration s = ConfigurationManager.LoadFromResource();

            Console.WriteLine("Loaded path: " + s.ConfigPath);

            s.ConfigPath = @"C:\Temp\FileBackuper\Config.xml";
            s.LogDirPath = @"C:\Temp\FileBackuper\Log";
            ConfigurationManager.SaveToResource(s);
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
;
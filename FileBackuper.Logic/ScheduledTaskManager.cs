using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;

using FileBackuper.Logging;
using FileBackuper.Model;

using TaskScheduler;

namespace FileBackuper.Logic
{
    /// <summary>
    /// Obaluje implementaci pro spravu naplanovanych uloh
    /// </summary>
    [BackuperConsolePath(@"\FileBackuper.BackuperConsole\bin\Debug\FileBackuper.BackuperConsole.exe")]
    public class ScheduledTaskManager
    {
        /// <summary>
        /// Vytvori nebo upravi naplanovanou ulohu podle zadaneho profilu a dalsiho triggeru
        /// </summary>
        /// <param name="p">Profil</param>
        public void CreateOrUpdateTask(Profile p)
        {
            //DateTime dt = ModelUtil.NextBackup(p);
            //CreateOrUpdateTask(p, dt);
            CreateOrUpdateTask(p, p.NextBackup);
        }

        /// <summary>
        /// Vytvori nebo upravi naplanovanou ulohu podle zadaneho profilu a dalsi trigger naplanuje podle zadaneho <code>TimeSpan</code>u
        /// </summary>
        /// <param name="p">Profil</param>
        /// <param name="ts">Casova prodleva</param>
        public void CreateOrUpdateTask(Profile p, TimeSpan ts)
        {
            DateTime dt = DateTime.Now.Add(ts);
            CreateOrUpdateTask(p, dt);
        }

        /// <summary>
        /// Vytvori nebo upravi naplanovanou ulohu podle zadaneho profilu a dalsi trigger naplanuje na zadane datum a cas
        /// TODO: - nazev aplikace (cesta k exe)!!
        ///       - moznost nastavit prioritu (procesu) zalohovani
        /// </summary>
        /// <param name="p">Profil</param>
        /// <param name="dt">Budouci <code>DateTime</code></param>
        public void CreateOrUpdateTask(Profile p, DateTime dt)
        {
            Logger log = LoggerFactory.Logger;
            try
            {
                using (ScheduledTasks st = new ScheduledTasks())
                {
                    string name = String.Format("FileBackuper_{0}", p.Name);
                    string dir = Assembly.GetExecutingAssembly().Location;
                    for (int i = 0; i < 4; i++)
                    {
                        dir = dir.Substring(0, dir.LastIndexOf('\\'));
                    }

                    Task t = st.OpenTask(name);
                    if (t == null)
                    {
                        log.Info("ScheduledTaskManager: Creating scheduled task named '{0}' with dt '{1:yyyy-MM-dd HH:mm:ss}'.", name, dt);
                        t = st.CreateTask(name);
                    }
                    else
                    {
                        log.Info("ScheduledTaskManager: Opening scheduled task named '{0}' with dt '{1:yyyy-MM-dd HH:mm:ss}'.", name, dt);
                    }
                    if (t != null)
                    {
                        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                        //t.Triggers.Clear();
                        //t.Triggers.Add(new RunOnceTrigger(ModelUtil.NextBackup(p)));
                        //DateTime dt = DateTime.Now.Add(new TimeSpan(0, 2, 0));
                        //t.Triggers.Add(new RunOnceTrigger(dt));
                        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

                        t.Triggers.Clear();
                        t.Triggers.Add(new RunOnceTrigger(dt));

                        t.ApplicationName = dir + ModelUtil.PathToBackuperConsoleExeFile(GetType());
                        t.Parameters = p.Name;
                        t.Creator = @"FileBackuper";
                        t.WorkingDirectory = dir;
                        //t.Flags = TaskFlags.RunOnlyIfLoggedOn;
                        //t.SetAccountInformation(@"NT AUTHORITY\LOCAL SERVICE", (string) null);
                        t.SetAccountInformation("", (string) null);
                        t.MaxRunTime = new TimeSpan(1, 0, 0);
                        t.Priority = System.Diagnostics.ProcessPriorityClass.Normal;
                        t.IdleWaitDeadlineMinutes = 20;
                        t.IdleWaitMinutes = 10;

                        t.Save();
                        t.Close();
                    }
                    else
                    {
                        log.Fatal("ScheduledTaskManager: Can't create or open scheduled task named '{0}' with dt '{1:yyyy-MM-dd HH:mm:ss}'!", name, dt);
                    }
                }
            }
            catch (Exception e)
            {
                log.Fatal("ScheduledTaskManager: Fatal error occured during creation of scheduled task, stacktrace: {0}.", e.Message);
            }
        }

        /// <summary>
        /// Vytvori nebo upravi naplanovanou ulohu s pouzitim <code>Profile.StartAt</code>
        /// TODO: - otestovat!!!
        ///       - co kdyz bude period nastaveno na <code>TimePeriod.NoPeriod</code> -> Resi Scheduler, pokud je NoPeriod, pouze smaze ulohu
        ///       - znovu otestovat, pouziti <code>ModelUtil.TimeSpanUsingStartAt</code> !
        /// </summary>
        /// <param name="p">Profil</param>
        public void CreateOrUpdateTaskUsingStartAt(Profile p)
        {
            /*DateTime now = DateTime.Now;
            if (now.Hour > p.StartAt.Hour || (now.Hour == p.StartAt.Hour && now.Minute > p.StartAt.Minute))
            {
                // Naplanovat az na zitra
                int hDiff = p.StartAt.Hour - now.Hour;
                int mDiff = p.StartAt.Minute - now.Minute;
                TimeSpan ts = new TimeSpan(1, hDiff, mDiff, 0);
                CreateOrUpdateTask(p, ts);
            }
            else
            {
                // Naplanovat na dnes
                int hDiff = p.StartAt.Hour - now.Hour;
                int mDiff = p.StartAt.Minute - now.Minute;
                TimeSpan ts = new TimeSpan(hDiff, mDiff, 0);
                CreateOrUpdateTask(p, ts);
            }*/

            CreateOrUpdateTask(p, ModelUtil.TimeSpanUsingStartAt(p));
        }

        /// <summary>
        /// Smaze naplanovanou ulohu
        /// </summary>
        /// <param name="p">Profil</param>
        public void DeteleTask(Profile p)
        {
            Logger log = LoggerFactory.Logger;
            try
            {
                using (ScheduledTasks st = new ScheduledTasks())
                {
                    string name = String.Format("FileBackuper_{0}", p.Name);
                    log.Info("ScheduledTaskManager: Deleting task named '{0}'.", name);
                    st.DeleteTask(name);
                }
            }
            catch (Exception e)
            {
                log.Fatal("ScheduledTaskManager: Fatal error occured during creation of scheduled task, stacktrace: {0}", e.Message);
            }
        }
    }
}

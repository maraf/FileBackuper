using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using FileBackuper.Logging;
using FileBackuper.Model;

using TaskScheduler;

namespace FileBackuper.Logic
{
    /// <summary>
    /// Obaluje implementaci TaskScheduleru
    /// </summary>
    [ConfigPath(@"C:\Temp\FileBackuper\Config.xml")]
    public class Scheduler
    {
        /// <summary>
        /// Manazer profilu
        /// </summary>
        private ProfileManager profileManager;
        public ProfileManager ProfileManager
        {
            get { return profileManager; }
            private set { profileManager = value; }
        }

        /// <summary>
        /// Zalohovac
        /// </summary>
        private Backuper backuper;
        public Backuper Backuper
        {
            get { return backuper; }
            private set { backuper = value; }
        }

        /// <summary>
        /// Spravce naplanovanych uloh
        /// </summary>
        private ScheduledTaskManager taskManager;
        public ScheduledTaskManager TaskManager
        {
            get { return taskManager; }
            private set { taskManager = value; }
        }

        /// <summary>
        /// Instanciuje Planovac a nacte profily
        /// </summary>
        public Scheduler()
        {
            ProfileManager = new ProfileManager(ModelUtil.PathToConfigurationFile(this.GetType()));
            try
            {
                ProfileManager.Load();
            }
            catch (IOException e)
            {
                Logger log = LoggerFactory.Logger;
                log.Fatal("Scheduler: Can't load profiles! IOException thrown! Message: {0}", e.Message);
            }

            Backuper = new Backuper();
            TaskManager = new ScheduledTaskManager();
        }

        /// <summary>
        /// Znovu nacte seznam profilu
        /// </summary>
        public void Refresh()
        {
            profileManager.Profiles.Clear();
            profileManager.Load();
        }

        /// <summary>
        /// Zazaloguje profil, pokud zadany profil jiz neexistuje, ulohu smaze
        /// </summary>
        /// <param name="profileName">Jmeno profilu</param>
        public void OnSchedule(string profileName)
        {
            OnSchedule(profileName, false);
        }

        /// <summary>
        /// Zazaloguje profil, pokud zadany profil jiz neexistuje, ulohu smaze
        /// </summary>
        /// <param name="profileName">Jmeno profilu</param>
        /// <param name="missed">Pokud je true, pro nastaveni pristi zalohy se pouzije <code>StartAt</code></param>
        public void OnSchedule(string profileName, bool missed)
        {
            Profile p;
            if (profileManager.FindByName(profileName, out p))
            {
                if (!p.Disabled)
                {
                    // Zazalohuj profil
                    Backuper.Backup(p, missed);

                    try
                    {
                        // Uloz zmeny v profilu
                        ProfileManager.Save();
                    }
                    catch (IOException e)
                    {
                        Logger log = LoggerFactory.Logger;
                        log.Fatal("Scheduler: Can't save profiles! IOException thrown! Message: {0}", e.Message);
                    }
                    catch (DirtyWriteAttemptException e)
                    {
                        Logger log = LoggerFactory.Logger;
                        log.Warn("Scheduler: DirtyWriteAttemptException, merging profiles.");
                        // Merge profile manager
                        ProfileManager.Merge(MergeType.Merge);
                        // Uloz zmeny
                        ProfileManager.Save();
                    }

                    // Vytvor dalsi ulohu, pokud je p.Period == TimePeriod.NoPeriod, smaz ulohu
                    if (!p.Period.Equals(TimePeriod.NoPeriod))
                    {
                        TaskManager.CreateOrUpdateTask(p, p.NextBackup);
                    }
                    else
                    {
                        TaskManager.DeteleTask(p);
                    }
                }
            }
            else
            {
                p = new Profile();
                p.Name = profileName;
                TaskManager.DeteleTask(p);
                throw new SchedulerException(String.Format("Scheduler: Profile with name \"{0}\" couldn't be found!", profileName));
            }
        }
    }

    /// <summary>
    /// Vyjimka pouzivana planovacem
    /// </summary>
    public class SchedulerException : Exception
    {
        public SchedulerException(string message)
            : base(message)
        {

        }
    }
}

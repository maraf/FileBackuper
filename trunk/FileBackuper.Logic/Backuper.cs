using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using FileBackuper.Model;
using FileBackuper.Logging;

namespace FileBackuper.Logic
{
    /// <summary>
    /// Zalohovac
    /// </summary>
    public class Backuper
    {
        /// <summary>
        /// Zazalohuje profil, stara se o konsistenci profilu (pocet verzi a pod)
        /// </summary>
        /// <param name="profile">Profil k zalohovani</param>
        public void Backup(Profile profile)
        {
            Backup(profile, false);
        }

        /// <summary>
        /// Zazalohuje profil, stara se o konsistenci profilu (pocet verzi a pod)
        /// </summary>
        /// <param name="profile">Profil k zalohovani</param>
        /// <param name="missed">Pokud je true, pro nastaveni pristi zalohy se pouzije <code>StartAt</code></param>
        public void Backup(Profile profile, bool missed)
        {
            Logger log = LoggerFactory.Logger;
            // Smazat soubory podle poctu drzenych verzi
            if (profile.VersionsNames.Count >= profile.NumberOfVersions)
            {
                log.Info(String.Format("Backuper: profile({0}): {1} version(s) needs to be deleted.", profile.Name, profile.VersionsNames.Count - profile.NumberOfVersions + 1));
                for (int i = (profile.VersionsNames.Count - profile.NumberOfVersions); i > -1; i--)
                {
                    if (File.Exists(profile.VersionsNames[i]))
                    {
                        log.Info("Backuper: profile({0}): deleting {1}", profile.Name, profile.VersionsNames[i]);
                        File.Delete(profile.VersionsNames[i]);
                        profile.VersionsNames.Remove(profile.VersionsNames[i]);
                    }
                    else
                    {
                        log.Warn("Backuper: profile({0}): file '{1}' doesn't exist!", profile.Name, profile.VersionsNames[i]);
                    }
                }
            }

            // Zazipovat aktualni
            Zipper zipper = new Zipper();
            string fileName = zipper.Zip(profile);
            log.Info(String.Format("Backuper: profile({0}): {1} created.", profile.Name, fileName));

            //Pridat novy prvek do VersionsNames
            profile.VersionsNames.Add(fileName);
            profile.LastBackup = DateTime.Now;
            if (!missed)
            {
                profile.NextBackup = ModelUtil.NextBackup(profile);
            }
            else
            {
                profile.NextBackup = ModelUtil.NextBackupUsingStartAt(profile);
            }
        }
    }
}

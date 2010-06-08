﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ionic.Zip;

using FileBackuper.Logging;
using FileBackuper.Model;

namespace FileBackuper.Logic
{
    /// <summary>
    /// Obaluje implementaci zipovani
    /// </summary>
    public class Zipper
    {

        public Zipper() { }

        /// <summary>
        /// Provede zipovani profilu
        /// </summary>
        /// <param name="profile">Profil k zipovani</param>
        public void Zip(Profile profile)
        {
            Zip(profile, LoggerFactory.Logger);
        }

        /// <summary>
        /// Provede zipovani profilu s pouzitim custom logu
        /// TODO: Logovat dobu trvani operace
        /// </summary>
        /// <param name="profile">Profil k zipovani</param>
        /// <param name="log">Custom log</param>
        public void Zip(Profile profile, Logger log)
        {
            string datePattern = profile.FileNamePattern.Substring(profile.FileNamePattern.IndexOf('_') + 1);
            string outputFileName;
            if (!"ProfileName".Equals(datePattern))
            {
                outputFileName = String.Format(@"{0}\{1}_{2:" + datePattern + "}.zip", profile.OutputFolder, profile.Name, DateTime.Now).Replace("ProfileName", profile.Name);
            }
            else
            {
                outputFileName = String.Format(@"{0}\{1}.zip", profile.OutputFolder, profile.Name).Replace("ProfileName", profile.Name);
            }

            log.AddNote(String.Format("Zipping profile({0}), date({1:" + datePattern + "}) to {2}.", profile.Name, DateTime.Now, outputFileName));

            try
            {
                using (ZipFile zip = new ZipFile())
                {
                    //zip.StatusMessageTextWriter = System.Console.Out;
                    foreach (ZipUnit unit in profile.Units)
                    {
                        if (unit.UnitType.Equals(UnitType.File))
                        {
                            zip.AddFile(unit.Path);
                        }
                        else
                        {
                            string dir = unit.Path.Substring(unit.Path.LastIndexOf('\\') + 1);
                            zip.AddDirectory(unit.Path, dir);
                        }
                    }
                    zip.Save(outputFileName);
                }
                log.AddNote("Zip created.");
            }
            catch (Exception e)
            {
                log.AddFatal(String.Format("Exception thrown during zipping profile(name={0})! Message:{1}", profile.Name, e.Message));
            }
        }
    }
}

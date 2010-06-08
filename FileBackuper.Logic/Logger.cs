using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileBackuper.Logging
{
    /// <summary>
    /// Levely logovani
    /// </summary>
    public enum LogLevel { Fatal, Error, Warn, Note };

    /// <summary>
    /// Vyjimka vyhazovani loggerem
    /// </summary>
    public class LoggerException : Exception
    {
        public LoggerException(string message)
            : base(message)
        {
            //
        }
    }

    /// <summary>
    /// Nastaveni loggeru
    /// </summary>
    public class LoggerSetup
    {
        /// <summary>
        /// Vystupni soubor
        /// </summary>
        public string Output
        {
            get { return output; }
            set { output = value; }
        }
        protected string output;

        /// <summary>
        /// Vzor pro ulozeni log zaznamu.
        /// Jako 0.parametr dostane <code>DateTime.Now</code>
        /// Jako 1.parametr dostane <code>LogLevel</code>
        /// Jako 2.parametr ukladanou zpravu do logu
        /// Defaultni honota: "{0:yyyy-MM-dd HH:mm:ss}  {1,-5}   {2}"
        /// </summary>
        public string RecordPattern
        {
            get { return recordPattern; }
            set { recordPattern = value; }
        }
        protected string recordPattern = "{0:yyyy-MM-dd HH:mm:ss}  {1,-7}   {2}";

        /// <summary>
        /// Pokud je true, po kazdem zapsany hodnoty je soubor zavren 
        /// a ulozen (pomalejsi), v opacnem pripade neni dostupny pri behu programu
        /// nepristupny a pri padu aplikace je log ztracen!!
        /// Defaultni hodnota: true
        /// </summary>
        public bool AutoClose
        {
            get { return autoClose; }
            set { autoClose = value; }
        }
        protected bool autoClose = true;

        /// <summary>
        /// Minimalni level, ktery bude logovan
        /// Napr: Pokud je nastaveno na Warn, pak se bude logovat Warn, Error a Fatal
        /// Defaultni hodnota: LogLevel.Note
        /// </summary>
        public LogLevel Level
        {
            get { return level; }
            set { level = value; }
        }
        protected LogLevel level = LogLevel.Note;

        public LoggerSetup()
        {
        }
    }

    /// <summary>
    /// Logger
    /// </summary>
    public class Logger : IDisposable
    {
        /// <summary>
        /// Konfigurace
        /// </summary>
        public LoggerSetup Setup
        {
            get { return setup; }
            private set { setup = value; }
        }
        protected LoggerSetup setup;

        /// <summary>
        /// Pouziva se pouze pokud je <code>Setup.AutoClose</code> = false
        /// </summary>
        protected StreamWriter writer;

        /// <summary>
        /// Pripravi instaci logu
        /// </summary>
        /// <param name="setup">Nastaveni logu</param>
        public Logger(LoggerSetup setup)
        {
            Setup = setup;

            if (!ValidateSetup())
            {
                throw new LoggerException("Wrong data in LoggerSetup!");
            }

            if (!Setup.AutoClose)
            {
                writer = OpenLog();
            }
        }

        /// <summary>
        /// Vola Dispose
        /// </summary>
        ~Logger()
        {
            Dispose();
        }

        /// <summary>
        /// Zaloguje zpravu o zadanem levelu v pripade, ze v Setup je nastaveno logovani prislusneho levelu
        /// </summary>
        /// <param name="level">Level zpravy</param>
        /// <param name="message">Obsah zpravy</param>
        public void Log(LogLevel level, string message)
        {
            if (level <= Setup.Level)
            {
                using (StreamWriter writer = OpenLog())
                {
                    writer.WriteLine(String.Format(Setup.RecordPattern, DateTime.Now, String.Format("[{0}]", level), message));
                    writer.Close();
                }
            }
        }

        /// <summary>
        /// Prida zpravu levelu fatal
        /// </summary>
        /// <param name="message">Obsah zpravy</param>
        public void AddFatal(string message)
        {
            Log(LogLevel.Fatal, message);
        }

        /// <summary>
        /// Prida zpravu levelu error
        /// </summary>
        /// <param name="message">Obsah zpravy</param>
        public void AddError(string message)
        {
            Log(LogLevel.Error, message);
        }

        /// <summary>
        /// Prida zpravu levelu warning
        /// </summary>
        /// <param name="message">Obsah zpravy</param>
        public void AddWarning(string message)
        {
            Log(LogLevel.Warn, message);
        }

        /// <summary>
        /// Prida zpravu levelu note
        /// </summary>
        /// <param name="message">Obsah zpravy</param>
        public void AddNote(string message)
        {
            Log(LogLevel.Note, message);
        }

        /// <summary>
        /// Zkontroluje data v <code>Setup</code>
        /// </summary>
        /// <returns></returns>
        protected bool ValidateSetup()
        {
            if (!"".Equals(Setup.Output) && !"".Equals(Setup.RecordPattern))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Otevre logovaci soubor
        /// </summary>
        /// <returns>Stream do logovaciho souboru</returns>
        protected StreamWriter OpenLog()
        {
            if (File.Exists(Setup.Output))
            {
                return new StreamWriter(Setup.Output, true);
            }
            else
            {
                return new StreamWriter(Setup.Output);
            }
        }

        /// <summary>
        /// Pripadne zavreni logu
        /// </summary>
        public void Dispose()
        {
            if (!Setup.AutoClose && writer.BaseStream != null)
            {
                writer.Close();
            }
        }
    }
}

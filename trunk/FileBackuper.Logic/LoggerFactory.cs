using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;


namespace FileBackuper.Logging
{
    /// <summary>
    /// Tovarna na logovaci soubory
    /// TODO: - vytazeni konfigurace z konfiguracniho souboru pro TRACE
    ///       - nastaveni urovne logovani pro TRACE
    /// </summary>
    [LogDir(@"C:\Temp\FileBackuper\Log")]
    [LogNamePattern("{0:yyyy-MM-dd}.log")]
    public static class LoggerFactory
    {
        /// <summary>
        /// Cesta k logovacimu adresari
        /// </summary>
        private static string logDir;
        public static string LogDir
        {
            get { return logDir; }
            private set { logDir = value; }
        }

        /// <summary>
        /// Vzor nazvu logovacich souboru
        /// </summary>
        private static string namePattern;
        public static string NamePattern
        {
            get { return namePattern; }
            private set { namePattern = value; }
        }

        /// <summary>
        /// Nastaveni loggeru
        /// </summary>
        private static LoggerSetup setup;
        public static LoggerSetup Setup
        {
            get { return setup; }
            private set { setup = value; }
        }

        /// <summary>
        /// Instance loggeru
        /// </summary>
        private static Logger logger;
        public static Logger Logger
        {
            get { return logger; }
            private set { logger = value; }
        }

        /// <summary>
        /// Inicializuje tovarnu
        /// </summary>
        static LoggerFactory()
        {
#if DEBUG
            System.Reflection.MemberInfo info = typeof(LoggerFactory);
            foreach (object item in info.GetCustomAttributes(true))
            {
                if (item is LogDir)
                {
                    LogDir = ((LogDir) item).Value;
                }
                else if (item is LogNamePattern)
                {
                    NamePattern = ((LogNamePattern) item).Value;
                }
            }
#endif
#if TRACE
            // Load it from Settings.resx or other configuration file
#endif
            Setup = new LoggerSetup();
            Setup.AutoClose = true;
            Setup.Level = LogLevel.Note;
            LogDir = (LogDir.EndsWith(@"\") ? LogDir : LogDir.EndsWith(@"/") ? LogDir : String.Format(@"{0}\", LogDir));
            Setup.Output = String.Format(LogDir + NamePattern, DateTime.Now);

            Logger = new Logger(Setup);
        }
    }

    /// <summary>
    /// Nastaveni slozky pro <code>LoggerFactory</code>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class LogDir : System.Attribute
    {
        /// <summary>
        /// Cesta k logovaci slozce
        /// </summary>
        private string value;
        public string Value
        {
            get { return value; }
            private set { this.value = value; }
        }

        /// <summary>
        /// Nastavi slozka na logy
        /// </summary>
        /// <param name="dir">Cesta ke slozce</param>
        public LogDir(string dir)
        {
            if (Directory.Exists(dir))
            {
                Value = dir;
            }
            else
            {
                throw new LoggerException("LogDir must be an existing directory!");
            }
        }
    }

    /// <summary>
    /// Nastaveni vzoru nazvu souboru pro <code>LoggerFactory</code>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class LogNamePattern : System.Attribute
    {
        /// <summary>
        /// Vzor nazvu souboru pro logovani v <code>LogDir</code>
        /// </summary>
        private string value;
        public string Value
        {
            get { return value; }
            private set { this.value = value; }
        }

        /// <summary>
        /// Nastavi vzor nazvu souboru pro logovani v <code>LogDir</code>
        /// </summary>
        /// <param name="pattern"></param>
        public LogNamePattern(string pattern)
        {
            if (!"".Equals(pattern))
            {
                Value = pattern;
            }
            else
            {
                throw new LoggerException("LogNamePattern can't be empty!");
            }
        }
    }
}

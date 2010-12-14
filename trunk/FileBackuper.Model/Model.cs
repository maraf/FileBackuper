using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileBackuper.Model
{
    /// <summary>
    /// Typ zalohovaneho unitu
    /// </summary>
    public enum UnitType { File, Folder };

    /// <summary>
    /// Periody pro ukladani
    /// </summary>
    public enum TimePeriod { NoPeriod, OneHour, ThreeHours, FiveHours, TwelveHours, OneDay, TwoDays, ThreeDays, OneWeek, TwoWeeks, OneMonth };

    /// <summary>
    /// Entita pro profil
    /// </summary>
    public class Profile
    {
        /// <summary>
        /// Nazev profilu
        /// </summary>
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Cesta k vystupni slozce
        /// </summary>
        private string outputFolder;
        public string OutputFolder
        {
            get { return outputFolder; }
            set { outputFolder = value; }
        }

        /// <summary>
        /// Vzor nazvu souboru
        /// </summary>
        private string fileNamePattern;
        public string FileNamePattern
        {
            get { return fileNamePattern; }
            set { fileNamePattern = value; }
        }

        /// <summary>
        /// Perioda ukladani
        /// </summary>
        private TimePeriod period;
        public TimePeriod Period
        {
            get { return period; }
            set { period = value; }
        }

        /// <summary>
        /// Pocet uchovavanych poslednich verzi
        /// </summary>
        private int numberOfVersions = 1;
        public int NumberOfVersions
        {
            get { return numberOfVersions; }
            set { numberOfVersions = value; }
        }

        /// <summary>
        /// Nazvy souboru s poslednimi verzemi
        /// </summary>
        private List<string> versionsNames = new List<string>();
        public List<string> VersionsNames
        {
            get { return versionsNames; }
            set { versionsNames = value; }
        }

        /// <summary>
        /// Seznam zalohovanych polozek
        /// </summary>
        private List<ZipUnit> units = new List<ZipUnit>();
        public List<ZipUnit> Units
        {
            get { return units; }
            set { units = value; }
        }

        /// <summary>
        /// Indikuje zda je profil neaktivni
        /// </summary>
        private bool disabled;
        public bool Disabled
        {
            get { return disabled; }
            set { disabled = value; }
        }

        /// <summary>
        /// Kdy se ma uloha spoustet
        /// </summary>
        private HourMinute startAt = new HourMinute(0, 0);
        public HourMinute StartAt
        {
            get { return startAt; }
            set { startAt = value; }
        }

        /// <summary>
        /// Datum a cas posledni zalohy
        /// </summary>
        private DateTime lastBackup = DateTime.MinValue;
        public DateTime LastBackup
        {
            get { return lastBackup; }
            set { lastBackup = value; }
        }

        /// <summary>
        /// Datum a cas pristi zalohy
        /// </summary>
        private DateTime nextBackup = DateTime.MinValue;
        public DateTime NextBackup
        {
            get { return nextBackup; }
            set { nextBackup = value; }
        }

        /// <summary>
        /// Pouze inicializuje
        /// </summary>
        public Profile()
        {
            
        }
    }

    /// <summary>
    /// Trida pro ukladani zaznamu o souborech/slozkach k zalohovani
    /// </summary>
    public class ZipUnit
    {
        /// <summary>
        /// Typ unitu
        /// </summary>
        private UnitType unitType;
        public UnitType UnitType
        {
            get { return unitType; }
            set { unitType = value; }
        }

        /// <summary>
        /// Cesta k souboru/slozce
        /// </summary>
        private string path;
        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        /// <summary>
        /// Vytvori prazdnou instanci, je potreba dovyplnit type a path
        /// </summary>
        public ZipUnit() { }

        /// <summary>
        /// Nastavi type a path
        /// </summary>
        /// <param name="type">Typ unitu</param>
        /// <param name="path">Cesta k souboru/slozce</param>
        public ZipUnit(UnitType type, string path)
        {
            UnitType = type;
            Path = path;
        }
    }

    /// <summary>
    /// Datova struktura pro hodino-minuty
    /// </summary>
    public class HourMinute
    {
        /// <summary>
        /// Hodnota minut
        /// </summary>
        private int minute;
        public int Minute
        {
            get { return minute; }
            set
            {
                if (value < 60 && value >= 0)
                {
                    minute = value;
                }
                else
                {
                    throw new Exception("Minute value must be between 0 and 59!");
                }
            }
        }

        /// <summary>
        /// Hodnota hodiny
        /// </summary>
        private int hour;
        public int Hour
        {
            get { return hour; }
            set
            {
                if (value < 24 && value >= 0)
                {
                    hour = value;
                }
                else
                {
                    throw new Exception("Hour value must be between 0 and 23!");
                }
            }
        }

        /// <summary>
        /// Inicializuje objekt, nastavy <code>Hour</code> a <code>Minute</code> na 0
        /// </summary>
        public HourMinute() { }

        /// <summary>
        /// Inicializuje a nastavi <code>Hour</code> a <code>Minute</code> na zadane hodnoty
        /// </summary>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        public HourMinute(int hour, int minute)
        {
            Minute = minute;
            Hour = hour;
        }

        /// <summary>
        /// Prevede strukturu na minuty (int)
        /// </summary>
        /// <returns>Hodnota v minutach</returns>
        public int ToMinutes()
        {
            return Hour * 60 + Minute;
        }

        /// <summary>
        /// Prevede strukturu na hodiny (double)
        /// </summary>
        /// <returns>Hodnota v hodinach</returns>
        public double ToHours()
        {
            return Hour + ((double)Minute / 60);
        }
    }

    /// <summary>
    /// Metody pro prevadeni prevazne enum na string a zpet
    /// TODO: Casovani pro mesic!!
    /// </summary>
    public static class ModelUtil
    {
        /// <summary>
        /// Prevadi <code>UnitType</code> na retezec
        /// </summary>
        /// <param name="type">Hodnota <code>UnitType</code></param>
        /// <returns>Retezec reprezentujici <code>UnitType</code></returns>
        public static string UnitTypeToString(UnitType type)
        {
            return type.Equals(UnitType.File) ? "File" : "Folder";
        }

        /// <summary>
        /// Prevadi retezec na <code>UnitType</code>
        /// </summary>
        /// <param name="type"><code>UnitType</code> jako retezec</param>
        /// <returns>UnitType</returns>
        public static UnitType StringToUnitType(string type)
        {
            if (!type.Equals("File") && !type.Equals("Folder")) throw new Exception("Unknown UnitType!");
            return type.Equals("File") ? UnitType.File : UnitType.Folder;
        }

        /// <summary>
        /// Prevadi <code>TimePeriod</code> na retezec
        /// </summary>
        /// <param name="period">Hodnota <code>TimePeriod</code></param>
        /// <returns></returns>
        public static string TimePeriondToString(TimePeriod period)
        {
            switch (period)
            {
                case TimePeriod.NoPeriod: return "No period";
                case TimePeriod.OneHour: return "One hour";
                case TimePeriod.ThreeHours: return "Three hours";
                case TimePeriod.FiveHours: return "Five hours";
                case TimePeriod.TwelveHours: return "Twelve hours";
                case TimePeriod.OneDay: return "One day";
                case TimePeriod.TwoDays: return "Two days";
                case TimePeriod.ThreeDays: return "Three days";
                case TimePeriod.OneWeek: return "One week";
                case TimePeriod.TwoWeeks: return "Two weeks";
                case TimePeriod.OneMonth: return "One month";
                default: return "";
            }
        }

        /// <summary>
        /// Prevadi zadany reteec na <code>TimePeriod</code>
        /// </summary>
        /// <param name="period">Hotnota <code>TimePeriod</code></param>
        /// <returns></returns>
        public static TimePeriod StringToTimePeriod(string period)
        {
            if (period.Equals("No period"))
            {
                return TimePeriod.NoPeriod;
            }
            else if (period.Equals("One hour"))
            {
                return TimePeriod.OneHour;
            }
            else if (period.Equals("Three hours"))
            {
                return TimePeriod.ThreeHours;
            } 
            else if (period.Equals("Five hours"))
            {
                return TimePeriod.FiveHours;
            } 
            else if (period.Equals("Twelve hours"))
            {
                return TimePeriod.TwelveHours;
            } 
            else if (period.Equals("One day"))
            {
                return TimePeriod.OneDay;
            } 
            else if (period.Equals("Two days"))
            {
                return TimePeriod.TwoDays;
            } 
            else if (period.Equals("Three days"))
            {
                return TimePeriod.ThreeDays;
            } 
            else if (period.Equals("One week"))
            {
                return TimePeriod.OneWeek;
            } 
            else if (period.Equals("Two weeks"))
            {
                return TimePeriod.TwoWeeks;
            }
            else if (period.Equals("One month"))
            {
                return TimePeriod.OneMonth;
            }
            else
            {
                throw new Exception("Unknown TimePeriod!");
            }
        }

        /// <summary>
        /// Prevadi <code>TimePeriod</code> na <code>TimeSpan</code>
        /// </summary>
        /// <param name="period">Hodnota <code>TimePeriod</code></param>
        /// <returns>Vraci hodnotu jako <code>TimeSpan</code></returns>
        public static TimeSpan TimePeriodToTimeSpan(TimePeriod period)
        {
            switch (period)
            {
                case TimePeriod.NoPeriod: return new TimeSpan(0, 0, 0);
                case TimePeriod.OneHour: return new TimeSpan(1, 0, 0);
                case TimePeriod.ThreeHours: return new TimeSpan(3, 0, 0);
                case TimePeriod.FiveHours: return new TimeSpan(5, 0, 0);
                case TimePeriod.TwelveHours: return new TimeSpan(12, 0, 0);
                case TimePeriod.OneDay: return new TimeSpan(1, 0, 0, 0);
                case TimePeriod.TwoDays: return new TimeSpan(2, 0, 0, 0);
                case TimePeriod.ThreeDays: return new TimeSpan(3, 0, 0, 0);
                case TimePeriod.OneWeek: return new TimeSpan(7, 0, 0, 0);
                case TimePeriod.TwoWeeks: return new TimeSpan(14, 0, 0, 0);
                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                case TimePeriod.OneMonth: return new TimeSpan(31, 0, 0, 0);
                default: throw new Exception("Unknown TimePeriod!");
            }
        }

        /// <summary>
        /// Vraci <code>DateTime</code> dalsi zalohy
        /// </summary>
        /// <param name="p">Profil</param>
        /// <returns>Pristi zaloha</returns>
        public static DateTime NextBackup(Profile p)
        {
            if (p.Period.Equals(TimePeriod.NoPeriod) && !p.LastBackup.Equals(DateTime.MinValue))
            {
                return DateTime.MinValue;
            }
            if (p.LastBackup.Equals(DateTime.MinValue))
            {
                return ModelUtil.NextBackupUsingStartAt(p);
            }
            DateTime dt = p.LastBackup.Add(ModelUtil.TimePeriodToTimeSpan(p.Period));
            return dt;
        }

        public static DateTime NextBackupUsingStartAt(Profile p)
        {
            return DateTime.Now.Add(ModelUtil.TimeSpanUsingStartAt(p));
        }

        /// <summary>
        /// Vrati <code>TimeSpan</code> odpovidajici pristimu zalohovani od <code>DateTime.Now</code> za pouziti <code>Profile.StartAt</code>
        /// </summary>
        /// <param name="p">Profil</param>
        /// <returns><code>TimeSpan</code> pristi zalohy</returns>
        public static TimeSpan TimeSpanUsingStartAt(Profile p)
        {
            DateTime now = DateTime.Now;
            TimeSpan ts;
            if (now.Hour > p.StartAt.Hour || (now.Hour == p.StartAt.Hour && now.Minute > p.StartAt.Minute))
            {
                // Naplanovat az na zitra
                int hDiff = p.StartAt.Hour - now.Hour;
                int mDiff = p.StartAt.Minute - now.Minute;
                ts = new TimeSpan(1, hDiff, mDiff, 0);
            }
            else
            {
                // Naplanovat na dnes
                int hDiff = p.StartAt.Hour - now.Hour;
                int mDiff = p.StartAt.Minute - now.Minute;
                ts = new TimeSpan(hDiff, mDiff, 0);
            }
            return ts;
        }

        /// <summary>
        /// Vraci cestu ke konfig souboru
        /// </summary>
        /// <param name="type">Trida v jejich atributech se hleda <code>ConfigPath</code> atribut</param>
        /// <returns>Cestu ke konfig souboru</returns>
        public static string PathToConfigurationFile(Type type)
        {
#if DEBUG
            System.Reflection.MemberInfo info = type;
            foreach (object item in info.GetCustomAttributes(true))
            {
                if (item is ConfigPath)
                {
                    return ((ConfigPath)item).Path;
                }
            }
            return null;
#endif
#if TRACE
            // Load it from Settings.resx or other configuration file
            throw new NotImplementedException("TRACE LoggerFactory config");
#endif
        }

        /// <summary>
        /// Vraci cestu k exe souboru BackuperConsole
        /// </summary>
        /// <param name="type">Trida v jejich atributech se hleda <code>BackuperConsolePath</code> atribut</param>
        /// <returns>Cestu k exe souboru BackuperConsole</returns>
        public static string PathToBackuperConsoleExeFile(Type type)
        {
            System.Reflection.MemberInfo info = type;
            foreach (object item in info.GetCustomAttributes(true))
            {
                if (item is BackuperConsolePath)
                {
                    return ((BackuperConsolePath)item).Path;
                }
            }
            return null;
        }
    }

    /// <summary>
    /// Nastavuje cestu ke konfiguracnimu souboru
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigPath : Attribute
    {
        private string path;
        public string Path
        {
            get { return path; }
            private set { path = value; }
        }

        /// <summary>
        /// Nastavi cestu ke konfig souboru
        /// </summary>
        /// <param name="path"></param>
        public ConfigPath(string path)
        {
            Path = path;
        }
    }

    /// <summary>
    /// Nastavuje cestu k exe souboru BackuperConsole
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class BackuperConsolePath : Attribute
    {
        private string path;
        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        /// <summary>
        /// Nastavi cestu k exe souboru BackuperConsole
        /// </summary>
        /// <param name="path"></param>
        public BackuperConsolePath(string path)
        {
            Path = path;
        }
    }
}

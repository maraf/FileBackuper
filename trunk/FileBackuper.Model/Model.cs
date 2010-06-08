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
        private int numberOfVersions;
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
    /// Metody pro prevadeni prevazne enum na string a zpet
    /// </summary>
    public static class ModelUtil
    {
        public static string UnitTypeToString(UnitType type)
        {
            return type.Equals(UnitType.File) ? "File" : "Folder";
        }

        public static UnitType StringToUnitType(string type)
        {
            if (!type.Equals("File") && !type.Equals("Folder")) throw new Exception("Unknown UnitType!");
            return type.Equals("File") ? UnitType.File : UnitType.Folder;
        }

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
    }
}

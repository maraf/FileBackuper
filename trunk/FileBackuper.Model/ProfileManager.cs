using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace FileBackuper.Model
{
    /// <summary>
    /// Výjímka používaná pokud nastane zápis starší verze konfigurace do novější verze v souboru.
    /// </summary>
    public class DirtyWriteAttemptException : Exception { }

    /// <summary>
    /// Výčet možností pro <code>ProfileManager.Merge</code>
    /// </summary>
    public enum MergeType { UseMine, UseTheirs, Merge };

    /// <summary>
    /// ProfileManager
    /// TODO: Chybove hlasky pri nacitani konfigurace!!
    /// </summary>
    public class ProfileManager
    {
        /// <summary>
        /// Seznam profilu
        /// </summary>
        private List<Profile> profiles = new List<Profile>();
        public List<Profile> Profiles
        {
            get { return profiles; }
            set { profiles = value; }
        }

        /// <summary>
        /// Ceska ke konfiguracnimu souboru
        /// </summary>
        private string path;
        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        /// <summary>
        /// Verze konfiguracniho souboru (funguje jako verzovani objektu v db, pri kazdem ulozeni se inkrementuje)
        /// </summary>
        private long version;
        public long Version
        {
            get { return version; }
            set { version = value; }
        }

        /// <summary>
        /// Inicializuje managera
        /// </summary>
        /// <param name="path">Cestak ke konfiguracnimu souboru</param>
        public ProfileManager(string path)
        {
            Path = path;
        }

        /// <summary>
        /// Nacte jeden profil z zadaneho <code>XmlElement</code>u
        /// </summary>
        /// <param name="profile">Profile <code>XmlElement</code></param>
        /// <returns>Nacteny profil</returns>
        private Profile LoadSingleProfile(XmlElement profile)
        {
            Profile p = new Profile();
            p.Name = profile.GetAttribute("name");
            p.OutputFolder = profile.GetAttribute("output");
            p.FileNamePattern = profile.GetAttribute("pattern");
            int per;
            if (Int32.TryParse(profile.GetAttribute("period"), out per))
            {
                p.Period = (TimePeriod)per;
            }
            else
            {
                throw new FormatException("Period must be a number!");
            }
            int nov;
            if (Int32.TryParse(profile.GetAttribute("versions"), out nov))
            {
                p.NumberOfVersions = nov;
            }
            else
            {
                throw new FormatException("Number of versions must be a number!");
            }
            int hour;
            if (Int32.TryParse(profile.GetAttribute("start-at-hour"), out hour))
            {
                p.StartAt.Hour = hour;
            }
            else
            {
                throw new FormatException("StartAt.Hour must be a number!");
            }
            int minu;
            if (Int32.TryParse(profile.GetAttribute("start-at-minute"), out minu))
            {
                p.StartAt.Minute = minu;
            }
            else
            {
                throw new FormatException("StartAt.Minute must be a number!");
            }
            p.Disabled = profile.GetAttribute("disabled").Equals("true");
            DateTime t = new DateTime();
            if (DateTime.TryParse(profile.GetAttribute("last-backup"), out t))
            {
                p.LastBackup = t;
            }
            else
            {
                throw new FormatException("LastBackup must be a valid DateTime!");
            }
            t = new DateTime();
            if (profile.HasAttribute("next-backup"))
            {
                if (DateTime.TryParse(profile.GetAttribute("next-backup"), out t))
                {
                    p.NextBackup = t;
                }
                else
                {
                    throw new FormatException("NextBackup must be a valid DateTime!");
                }
            }
            else
            {
                p.NextBackup = ModelUtil.NextBackup(p);
            }

            // VersionsNames
            foreach (XmlElement vername in profile.GetElementsByTagName("name"))
            {
                p.VersionsNames.Add(vername.GetAttribute("path"));
            }

            // Units
            foreach (XmlElement item in profile.GetElementsByTagName("unit"))
            {
                ZipUnit zu = new ZipUnit();
                int type;
                if (Int32.TryParse(item.GetAttribute("type"), out type))
                {
                    if (UnitType.Folder.Equals((UnitType)type))
                    {
                        zu.UnitType = UnitType.Folder;
                    }
                    else
                    {
                        zu.UnitType = UnitType.File;
                    }
                    zu.Path = item.GetAttribute("path");
                    p.Units.Add(zu);
                }
                else
                {
                    throw new FormatException("ZipUnit.Type must be a number!");
                }
            }

            return p;
        }

        /// <summary>
        /// Nacte profily z konfiguracniho souboru
        /// </summary>
        public void Load()
        {
            Profiles.Clear();

            XmlDocument doc = new XmlDocument();
            FileStream fs = File.OpenRead(path);
            doc.Load(fs);

            long ver;
            if (long.TryParse(((XmlElement)doc.GetElementsByTagName("profiles")[0]).GetAttribute("version"), out ver))
            {
                Version = ver;
            }
            else
            {
                Version = 1;
            }

            foreach (XmlElement profile in doc.GetElementsByTagName("profile"))
            {
                Profile p = LoadSingleProfile(profile);
                profiles.Add(p);
            }

            fs.Close();
        }

        /// <summary>
        /// Ulozi profily do konfiguracniho souboru
        /// </summary>
        public void Save()
        {
            if (!CheckFileVersion())
            {
                throw new DirtyWriteAttemptException();
            }

            XmlDocument doc = new XmlDocument();

            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.AppendChild(dec);
            XmlElement profiles = doc.CreateElement("profiles");
            XmlAttribute ver = doc.CreateAttribute("version");
            Version++;
            ver.Value = Version.ToString();
            profiles.Attributes.Append(ver);

            foreach (Profile p in Profiles)
            {
                XmlElement profile = doc.CreateElement("profile");

                XmlAttribute name = doc.CreateAttribute("name");
                name.Value = p.Name;
                XmlAttribute outputFolder = doc.CreateAttribute("output");
                outputFolder.Value = p.OutputFolder;
                XmlAttribute fileNamePattern = doc.CreateAttribute("pattern");
                fileNamePattern.Value = p.FileNamePattern;
                XmlAttribute period = doc.CreateAttribute("period");
                period.Value = String.Format("{0}", (int) p.Period);
                XmlAttribute numberOfVersions = doc.CreateAttribute("versions");
                numberOfVersions.Value = String.Format("{0}", p.NumberOfVersions);
                XmlAttribute disabled = doc.CreateAttribute("disabled");
                disabled.Value = p.Disabled ? "true" : "false";
                XmlAttribute hour = doc.CreateAttribute("start-at-hour");
                hour.Value = String.Format("{0}", p.StartAt.Hour);
                XmlAttribute minu = doc.CreateAttribute("start-at-minute");
                minu.Value = String.Format("{0}", p.StartAt.Minute);
                XmlAttribute lasb = doc.CreateAttribute("last-backup");
                lasb.Value = p.LastBackup.ToString("yyyy-MM-dd HH:mm:ss");
                XmlAttribute nexb = doc.CreateAttribute("next-backup");
                nexb.Value = p.NextBackup.ToString("yyyy-MM-dd HH:mm:ss");

                profile.Attributes.Append(name);
                profile.Attributes.Append(outputFolder);
                profile.Attributes.Append(fileNamePattern);
                profile.Attributes.Append(period);
                profile.Attributes.Append(numberOfVersions);
                profile.Attributes.Append(disabled);
                profile.Attributes.Append(hour);
                profile.Attributes.Append(minu);
                profile.Attributes.Append(lasb);
                profile.Attributes.Append(nexb);

                XmlElement versions = doc.CreateElement("versions");

                foreach (string n in p.VersionsNames)
                {
                    XmlElement xmlName = doc.CreateElement("name");
                    XmlAttribute path = doc.CreateAttribute("path");
                    path.Value = n;
                    xmlName.Attributes.Append(path);
                    versions.AppendChild(xmlName);
                }

                profile.AppendChild(versions);

                XmlElement units = doc.CreateElement("units");

                foreach (ZipUnit item in p.Units)
                {
                    XmlElement unit = doc.CreateElement("unit");
                    XmlAttribute type = doc.CreateAttribute("type");
                    type.Value = String.Format("{0}", (int) item.UnitType);
                    XmlAttribute path = doc.CreateAttribute("path");
                    path.Value = item.Path;
                    unit.Attributes.Append(type);
                    unit.Attributes.Append(path);
                    units.AppendChild(unit);
                }

                profile.AppendChild(units);

                profiles.AppendChild(profile);
            }

            doc.AppendChild(profiles);
            doc.Save(Path);
        }

        /// <summary>
        /// Porovna zmeny v konfiguracnim souboru a verzi v pameti a aktualizuje verzi v pameti 
        /// (pripravy verzi v pameti pro ulozeni po <code>DirtyWriteAttemptException</code>)
        /// </summary>
        public void Merge(MergeType type)
        {
            if (type.Equals(MergeType.UseMine))
            {
                Version = GetFileVersion();
            }
            else if (type.Equals(MergeType.UseTheirs))
            {
                Load();
            }
            else if (type.Equals(MergeType.Merge))
            {
                // Trochu moc naivni ...

                XmlDocument doc = new XmlDocument();
                FileStream fs = File.OpenRead(path);
                doc.Load(fs);

                foreach (XmlElement profile in doc.GetElementsByTagName("profile"))
                {
                    Profile loadedProfile = LoadSingleProfile(profile);
                    Profile newProfile;
                    if (FindByName(loadedProfile.Name, out newProfile))
                    {
                        newProfile.OutputFolder = loadedProfile.OutputFolder;
                        newProfile.FileNamePattern = loadedProfile.FileNamePattern;
                        newProfile.Period = loadedProfile.Period;
                        newProfile.NumberOfVersions = loadedProfile.NumberOfVersions;
                        if (loadedProfile.VersionsNames.Count > newProfile.VersionsNames.Count)
                        {
                            newProfile.VersionsNames = loadedProfile.VersionsNames;
                        }
                        newProfile.Units = loadedProfile.Units;
                        newProfile.Disabled = loadedProfile.Disabled;
                        newProfile.StartAt = loadedProfile.StartAt;
                        if (loadedProfile.LastBackup > newProfile.LastBackup)
                        {
                            newProfile.LastBackup = loadedProfile.LastBackup;
                        }
                        if (loadedProfile.NextBackup > newProfile.NextBackup)
                        {
                            newProfile.NextBackup = loadedProfile.NextBackup;
                        }
                    }
                    else
                    {
                        profiles.Add(loadedProfile);
                    }
                }

                long ver;
                if (long.TryParse(((XmlElement)doc.GetElementsByTagName("profiles")[0]).GetAttribute("version"), out ver))
                {
                    Version = ver;
                }

                fs.Close();
            }
            else
            {
                throw new NotSupportedException("Passed merge type is not supported!");
            }
        }

        /// <summary>
        /// Najde profil podle jmena
        /// </summary>
        /// <param name="name">Jmeno hledaneho profilu</param>
        /// <returns></returns>
        public bool FindByName(string name, out Profile profile)
        {
            if (!"".Equals(name))
            {
                foreach (Profile p in Profiles)
                {
                    if (p.Name.Equals(name))
                    {
                        profile = p;
                        return true;
                    }
                }
            }
            profile = new Profile();
            return false;
        }

        /// <summary>
        /// Zkontroluje korektnost zadaneho profilu
        /// </summary>
        /// <param name="profile">Testovany profil</param>
        /// <param name="message">Vystukni (chybova) hlaska</param>
        /// <returns></returns>
        public bool Validate(Profile profile, out string message)
        {
            message = "";
            if ("".Equals(profile.Name))
            {
                message = "Profile name can't be empty!";
                return false;
            }
            if (!IsNameAvailable(profile.Name) && !profiles.Contains(profile))
            {
                message = "Profile already used by another profile!";
                return false;
            }
            if ("".Equals(profile.OutputFolder) || !Directory.Exists(profile.OutputFolder))
            {
                message = "Output folder isn't set!";
                return false;
            }
            if (profile.Units.Count == 0)
            {
                message = "Profile must have at least one item!";
                return false;
            }
            /*if (!IsMinimalTimeSpanOk(profile.StartAt, profile.Name))
            {
                message = "Profiles must have at least 5min time span between their start time!";
                return false;
            }*/
            return true;
        }

        /// <summary>
        /// Testuje zda je alespon minimalni (5min) rozestup mezi profily
        /// </summary>
        /// <param name="hm">Testovany hm</param>
        /// <param name="pName">Jmeno profilu, pokud testovany hm jiz je v seznamu profilu</param>
        /// <returns></returns>
        private bool IsMinimalTimeSpanOk(HourMinute hm, string pName)
        {
            foreach (Profile p in Profiles)
            {
                if ((hm.ToMinutes() + 5) > p.StartAt.ToMinutes() && (hm.ToMinutes() - 5) < p.StartAt.ToMinutes() && !p.Name.Equals(pName) && !p.Disabled)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Zjisti zda zadane jmeno profilu jiz je v kolekci profilu
        /// </summary>
        /// <param name="name">Testovane jmeno profilu</param>
        /// <returns>true jeste neni, false pokud jiz je</returns>
        public bool IsNameAvailable(string name)
        {
            foreach (Profile p in profiles)
            {
                if (p.Name.Equals(name))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Testuje version atribut konfiguracniho souboru
        /// </summary>
        /// <returns>True pokud je verze konfiguracniho souboru stejna jako je aktualni verze <code>ProfileManageru</code>, False pokud je verze konfiguracniho souboru vetsi.</returns>
        public bool CheckFileVersion()
        {
            return Version == GetFileVersion();
        }

        /// <summary>
        /// Vrati aktualni verzi konfiguracniho souboru.
        /// </summary>
        /// <returns>Verze konfiguracniho souboru</returns>
        public long GetFileVersion()
        {
            XmlDocument doc = new XmlDocument();
            FileStream fs = File.OpenRead(path);
            doc.Load(fs);

            long ver;
            if (long.TryParse(((XmlElement)doc.GetElementsByTagName("profiles")[0]).GetAttribute("version"), out ver))
            {
                fs.Close();
                return ver;
            }
            else
            {
                fs.Close();
                return 1;
            }
        }
    }
}

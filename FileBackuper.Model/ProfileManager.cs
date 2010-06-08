using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace FileBackuper.Model
{
    /// <summary>
    /// ProfileManager
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
        /// Inicializuje managera
        /// </summary>
        /// <param name="path">Cestak ke konfiguracnimu souboru</param>
        public ProfileManager(string path)
        {
            Path = path;
        }

        /// <summary>
        /// Nacte profily z konfiguracniho souboru
        /// </summary>
        public void Load()
        {
            XmlDocument doc = new XmlDocument();
            FileStream fs = File.OpenRead(path);
            doc.Load(fs);

            foreach (XmlElement profile in doc.GetElementsByTagName("profile"))
            {
                Profile p = new Profile();
                p.Name = profile.GetAttribute("name");
                p.OutputFolder = profile.GetAttribute("output");
                p.FileNamePattern = profile.GetAttribute("pattern");
                int per;
                if (Int32.TryParse(profile.GetAttribute("period"), out per))
                {
                    p.Period = (TimePeriod) per;
                }
                else
                {
                    // Error
                }
                int nov;
                if (Int32.TryParse(profile.GetAttribute("versions"), out nov))
                {
                    p.NumberOfVersions = nov;
                }
                else
                {
                    // Error
                }
                p.Disabled = profile.GetAttribute("disabled").Equals("true");

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
                        if (UnitType.Folder.Equals((UnitType) type))
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
                        // Error
                    }
                }

                profiles.Add(p);
            }

            fs.Close();
        }

        /// <summary>
        /// Ulozi profily do konfiguracniho souboru
        /// </summary>
        public void Save()
        {
            XmlDocument doc = new XmlDocument();

            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.AppendChild(dec);
            XmlElement profiles = doc.CreateElement("profiles");

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

                profile.Attributes.Append(name);
                profile.Attributes.Append(outputFolder);
                profile.Attributes.Append(fileNamePattern);
                profile.Attributes.Append(period);
                profile.Attributes.Append(numberOfVersions);
                profile.Attributes.Append(disabled);

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
    }
}

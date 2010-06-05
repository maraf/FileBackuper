using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public List<Profile> Profiles;

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

        }

        /// <summary>
        /// Ulozi profily do konfiguracniho souboru
        /// </summary>
        public void Save()
        {

        }
    }
}

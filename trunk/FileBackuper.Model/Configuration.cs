using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileBackuper.Model
{
    public class Configuration
    {
        /// <summary>
        /// Cesta k souboru s profily
        /// </summary>
        public string ConfigPath { get; set; }

        /// <summary>
        /// Cesta k adresari pro logy aplikace
        /// </summary>
        public string LogDirPath { get; set; }
    }
}

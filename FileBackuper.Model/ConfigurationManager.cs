using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;

namespace FileBackuper.Model
{
    public class ConfigurationManager
    {
        public static string ResourceName { get { return "Settings.resx"; } }

        public static void SaveToResource(Configuration config)
        {
            IResourceWriter rw = new ResXResourceWriter(ConfigurationManager.ResourceName);
            rw.AddResource("Settings.ConfigPath", config.ConfigPath);
            rw.AddResource("Settings.LogDirPath", config.LogDirPath);
            rw.Close();
        }

        public static Configuration LoadFromResource()
        {
            Configuration settings = new Configuration();
            IResourceReader rd = new ResXResourceReader(ConfigurationManager.ResourceName);
            IDictionaryEnumerator en = rd.GetEnumerator();

            while (en.MoveNext())
            {
                if (en.Key.Equals("Settings.ConfigPath"))
                {
                    settings.ConfigPath = (string)en.Value;
                }
                else if (en.Key.Equals("Settings.LogDirPath"))
                {
                    settings.LogDirPath = (string)en.Value;
                }
            }
            rd.Close();
            return settings;
        }
    }
}

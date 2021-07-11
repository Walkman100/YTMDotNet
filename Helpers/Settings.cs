using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace YTMDotNet.Helpers {
    static class Settings {
        private static string _settingsPath;
        private static bool _loaded = false;

        public static bool Init() {
            string configFileName = "YTMDotNet.xml";

            if (WalkmanLib.GetOS() == WalkmanLib.OS.Windows) {
                if         (!Directory.Exists(Path.Combine(Environment.GetEnvironmentVariable("AppData"), "WalkmanOSS")))
                    Directory.CreateDirectory(Path.Combine(Environment.GetEnvironmentVariable("AppData"), "WalkmanOSS"));
                _settingsPath =               Path.Combine(Environment.GetEnvironmentVariable("AppData"), "WalkmanOSS", configFileName);
            } else {
                if         (!Directory.Exists(Path.Combine(Environment.GetEnvironmentVariable("HOME"), ".config", "WalkmanOSS")))
                    Directory.CreateDirectory(Path.Combine(Environment.GetEnvironmentVariable("HOME"), ".config", "WalkmanOSS"));
                _settingsPath =               Path.Combine(Environment.GetEnvironmentVariable("HOME"), ".config", "WalkmanOSS", configFileName);
            }

            if     (File.Exists(Path.Combine(Application.StartupPath, configFileName)))
                _settingsPath = Path.Combine(Application.StartupPath, configFileName);
            else if            (File.Exists(configFileName))
                _settingsPath = new FileInfo(configFileName).FullName;

            try {
                if (File.Exists(_settingsPath)) {
                    LoadSettings();
                    return true;
                } else {
                    return false;
                }
            } finally {
                _loaded = true;
            }
        }

        private static string pythonDLL;
        public static string PythonDLL {
            get => pythonDLL;
            set {
                pythonDLL = value;
                SaveSettings();
            }
        }

        private static void LoadSettings() {
            using (var reader = XmlReader.Create(_settingsPath)) {
                try {
                    reader.Read();
                } catch (XmlException) {
                    return;
                }

                if (reader.IsStartElement() && reader.Name == "YTMDotNet") {
                    if (reader.Read() && reader.IsStartElement() && reader.Name == "Settings" && reader.Read()) {
                        while (reader.IsStartElement()) {
                            switch (reader.Name) {
                                case "PythonDLL":
                                    reader.Read();
                                    PythonDLL = reader.Value;
                                    break;
                            }
                            reader.Read(); reader.Read();
                        }
                    }
                }
            }
        }

        private static void SaveSettings() {
            if (!_loaded)
                return;
            using (var writer = XmlWriter.Create(_settingsPath, new XmlWriterSettings() { Indent = true })) {
                writer.WriteStartDocument();
                writer.WriteStartElement("YTMDotNet");

                writer.WriteStartElement("Settings");
                writer.WriteElementString("PythonDLL", PythonDLL);
                writer.WriteEndElement(); // Settings

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Resources;
using System.Text;

namespace FlagsOfTheWorldApp {
    public class Flag {
        static public List<Flag> flags = new List<Flag>();
        public string ISOCode { get; set; }
        public string CountryName { get; set; }
        public string Continent { get; set; }
        public string ImagePath { get; set; }
        public static void CreateFlags() {
            List<string> lines = Helper.ReadFile("pack://application:,,,/flags/geonames.csv");
            //System.Diagnostics.Debug.WriteLine("Read file: " + lines.Count);
            List<string> images = Helper.GetAllResources("flags");
            //System.Diagnostics.Debug.WriteLine("Images: " + images.Count);

            string path;
            foreach (string line in lines) {
                string[] props = line.Trim('"').Split("\",\"");
                path = "flags/" + props[0].ToLower() + ".png";
                if (images.Contains(path))
                    Flag.flags.Add(new Flag() {
                        ISOCode = props[0],
                        CountryName = props[1],
                        Continent = props[2],
                        ImagePath = "pack://application:,,,/" + path
                    });
            }
            //System.Diagnostics.Debug.WriteLine("Flags: " + flags.Count);
        }
    }

    class Helper {
        static public List<string> ReadFile(string uripath) {
            List<string> lines = new List<string>();
            string line;
            using (StreamReader sr = new StreamReader(System.Windows.Application.GetResourceStream(new Uri(uripath)).Stream)) {
                while ((line = sr.ReadLine()) != null) {
                    lines.Add(line);
                }
            }
            return lines;
        }

        static public List<string> GetAllResources(string filter) {
            List<string> relativePaths = new List<string>();
            ResourceManager rm = new ResourceManager("FlagsOfTheWorldApp.g", System.Reflection.Assembly.GetExecutingAssembly());
            foreach (DictionaryEntry de in rm.GetResourceSet(System.Globalization.CultureInfo.CurrentUICulture, true, true)) {
                relativePaths.Add((string)de.Key);
            }
            return relativePaths.FindAll(s => s.Contains(filter));
        }
    }
}

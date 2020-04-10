using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MikuReader.wf.Classes.Region
{
    public class LanguageHelper
    {
        /* https://www.loc.gov/standards/iso639-2/php/code_list.php */

        Dictionary<string, Language> languages;

        public Language CurrentLanguage { get; set; }

        public LanguageHelper()
        {
            languages = new Dictionary<string, Language>();

            Language eng = CreateLanguage("eng.json");
            Language jpn = CreateLanguage("jpn.json");
            Language spa = CreateLanguage("spa.json");

            languages.Add(eng.Name, eng);
            languages.Add(jpn.Name, jpn);
            languages.Add(spa.Name, spa);

            // TODO
            if (Properties.Settings.Default["language"].Equals(string.Empty))
            {
                Properties.Settings.Default["language"] = eng.Name;
                Properties.Settings.Default.Save();
                CurrentLanguage = eng;
            } else
            {
                CurrentLanguage = languages[Properties.Settings.Default["language"].ToString()];
            }
        }
        
        public Language GetLanguageByName(string name)
        {
            return languages[name];
        }

        public string[] GetLanguageNames()
        {
            return languages.Keys.ToArray();
        }

        private Language CreateLanguage(string filename)
        {
            return JsonConvert.DeserializeObject<Language>(GetFileContents(filename));
        }

        private string GetFileContents(string filename)
        {
            var assembly = Assembly.GetExecutingAssembly();
            string resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith(filename));

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}

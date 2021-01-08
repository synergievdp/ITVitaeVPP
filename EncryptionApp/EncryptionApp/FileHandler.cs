using System.IO;
using System.Text;

namespace EncryptionApp {
    public class FileHandler {
        public void Save(string path, string input) {
            using(StreamWriter streamWriter = File.CreateText(path)) {
                streamWriter.Write(input);
            }
        }

        public string Load(string path) {
            string text = string.Empty;
            using(StreamReader streamReader = File.OpenText(path)) {
                text = streamReader.ReadToEnd();
            }
            return text;
        }
    }
}

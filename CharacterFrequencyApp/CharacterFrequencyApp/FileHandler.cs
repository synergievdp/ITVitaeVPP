using System.IO;

namespace CharacterFrequencyApp {
    public class FileHandler {
        public string Load(string path) {
            string text = string.Empty;
            using (StreamReader streamReader = File.OpenText(path)) {
                text = streamReader.ReadToEnd();
            }
            return text;
        }
    }
}

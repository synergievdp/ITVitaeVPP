using System.Collections.Generic;

namespace CharacterFrequencyApp {
    public class CharacterFrequency {

        public Dictionary<char,int> Count(string text) {
            Dictionary<char, int> freqs = new Dictionary<char, int>();
            foreach(char c in text) {
                if (freqs.ContainsKey(c)) {
                    freqs[c]++;
                } else freqs.Add(c, 1);
            }
            return freqs;
        }
    }
}

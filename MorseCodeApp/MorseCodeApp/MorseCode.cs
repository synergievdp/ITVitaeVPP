using System.Collections.Generic;
using System.Text;

namespace MorseCodeApp {
    public class MorseCode {
        Dictionary<char, string> characters;

        public MorseCode() {
            characters = new Dictionary<char, string>() {
                {'A',"·—"},
                {'B',"—···"},
                {'C',"—·—·"},
                {'D',"—··"},
                {'E',"·"},
                {'F',"··—·"},
                {'G',"——·"},
                {'H',"····"},
                {'I',"··"},
                {'J',"·———"},
                {'K',"—·—"},
                {'L',"·—··"},
                {'M',"——"},
                {'N',"—·"},
                {'O',"———"},
                {'P',"·——·"},
                {'Q',"——·—"},
                {'R',"·—·"},
                {'S',"···"},
                {'T',"—"},
                {'U',"··—"},
                {'V',"···—"},
                {'W',"·——"},
                {'X',"—··—"},
                {'Y',"—·——"},
                {'Z',"——··"},
                {'0',"—————"},
                {'1',"·————"},
                {'2',"··———"},
                {'3',"···——"},
                {'4',"····—"},
                {'5',"·····"},
                {'6',"—····"},
                {'7',"——···"},
                {'8',"———··"},
                {'9',"————·"},
                {'.',"·—·—·—"},
                {',',"——··——"},
                {'?',"··——··"},
                {'!',"—·—·——"},
                {'-',"—····—"},
                {'/',"—··—·"},
                {';',"—·—·—"},
                {':',"———···"},
                {'\'',"·————·"},
                {'(',"—·——·"},
                {')',"—·——·—"},
                {'=',"—···—"},
                {'@',"·——·—·"},
                {'&',"·–···"},
            };
        }

        public string ToMorse(string text) {
            StringBuilder output = new StringBuilder();
            foreach(char c in text.ToUpper()) {
                if (characters.ContainsKey(c)) {
                    output.Append(" ");
                    output.Append(characters[c]);
                } else output.Append("/");
            }
            return output.ToString();
        }

        public string ToAlpha(string text) {
            StringBuilder output = new StringBuilder();
            string[] words = text.Split("/");
            foreach(string word in words) {
                foreach (string letter in word.Split(" ")) {
                    foreach (char key in characters.Keys) {
                        if (letter.Equals(characters[key])) {
                            output.Append(key);
                        }
                    }
                }
                output.Append(" ");
            }
            return output.ToString();
        }
    }
}

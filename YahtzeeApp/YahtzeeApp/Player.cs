using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace YahtzeeApp {
    public class Player {
        static public string[] categories = { 
            "ACES", "TWOS", "THREES", "FOURS", "FIVES", "SIXES",
            "THREEOFAKIND", "FOUROFAKIND", "FULLHOUSE",
            "SMALLSTRAIGHT", "LARGESTRAIGHT", "YAHTZEE", "CHANCE"
        };
        public int index { get; set; }
        public Dictionary<string, int> score;

        public Player(int index) {
            this.index = index;
            score = new Dictionary<string, int>();
            foreach(string category in categories) {
                score.Add(category, -1);
            }
        }

        public void Score(Collection<Dice> dice, string category) {
            if (CheckScore(dice).Contains(category)) {
                score[category] = category switch {
                    "YAHTZEE" => 50,
                    "LARGESTRAIGHT" => 40,
                    "SMALLSTRAIGHT" => 30,
                    "FULLHOUSE" => 25,
                    "FOUROFAKIND" => dice.Sum(die => die.Value),
                    "THREEOFAKIND" => dice.Sum(die => die.Value),
                    "ACES" => dice.Where(die => die.Value == 1).Sum(die => die.Value),
                    "TWOS" => dice.Where(die => die.Value == 2).Sum(die => die.Value),
                    "THREES" => dice.Where(die => die.Value == 3).Sum(die => die.Value),
                    "FOURS" => dice.Where(die => die.Value == 4).Sum(die => die.Value),
                    "FIVES" => dice.Where(die => die.Value == 5).Sum(die => die.Value),
                    "SIXES" => dice.Where(die => die.Value == 6).Sum(die => die.Value),
                    "CHANCE" => dice.Sum(die => die.Value),
                };
            } else score[category] = 0;
        }

        public List<string> CheckScore(Collection<Dice> dice) {
            List<string> options = new List<string>();
            options.Add("CHANCE");

            Dictionary<int, int> Dice = new Dictionary<int, int>();
            for (int i = 1; i <= 6; i++) Dice.Add(i, 0);
            foreach(Dice die in dice) Dice[die.Value]++;

            foreach (int die in Dice.Keys) {
                if (Dice[die] >= 5) options.Add("YAHTZEE");
                if (Dice[die] >= 4) options.Add("FOUROFAKIND");
                if (Dice[die] >= 3) options.Add("THREEOFAKIND");
                if (Dice[die] >= 2) {
                    foreach (int die2 in Dice.Keys) {
                        if (die2 != die && Dice[die2] >= 3) options.Add("FULLHOUSE");
                    }
                }
                if (Dice[die] >= 1) {
                    options.Add(die switch {
                        1 => "ACES",
                        2 => "TWOS",
                        3 => "THREES",
                        4 => "FOURS",
                        5 => "FIVES",
                        6 => "SIXES"
                    });
                }
            }

            if (Dice[2] == 1 && Dice[3] == 1 && Dice[4] == 1 && Dice[5] == 1 && (Dice[1] == 1 || Dice[6] == 1))
                options.Add("LARGESTRAIGHT");
            if(Dice[3] >= 1 && Dice[4] >= 1
                && ((Dice[1] >= 1 && Dice[2] >= 1) ||
                    (Dice[2] >= 1 && Dice[5] >= 1) ||
                    (Dice[5] >= 1 && Dice[6] >= 1)))
                options.Add("SMALLSTRAIGHT");

            return options;
        }
    }
}

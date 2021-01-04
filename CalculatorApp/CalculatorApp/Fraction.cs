using System;
using System.Collections.Generic;
using System.Linq;

namespace CalculatorApp {
    public class Fraction {
        public static Fraction ZERO => Create(0, 1);
        public static Fraction HUNDRED => Create(100, 1);
        public int Numerator { get; private set; }
        public int Denominator { get; private set; }
        private Fraction(int numerator, int denom) {
            this.Numerator = numerator;
            this.Denominator = denom;
        }

        public static Fraction Create(int numerator, int denominator) {
            return new Fraction(numerator, denominator).Simplify();
        }

        public static Fraction Create(string number) {
            string[] parts = number.Split(',');
            int denominator = (int)Math.Pow(10, parts.Length == 1 ? 0 : parts[1].Length);
            int numerator = (int)Decimal.Multiply(Decimal.Parse(number), denominator);
            return Create(numerator, denominator);
        }

        public static Fraction Create(Fraction other) {
            return Create(other.Numerator, other.Denominator);
        }

        public decimal ToDecimal() {
            return Decimal.Divide(this.Numerator, this.Denominator);
        }

        public override string ToString() {
            return ToDecimal().ToString("G27");
        }

        //public string Display() {
        //    string s = ToString();
        //    string[] ss = s.Split(",");
        //    if (ss.Length > 1 && ss[1].Length > 3) {
        //        int number = this.numerator / this.denominator;
        //        Fraction num = Fraction.Create(this.numerator % this.denominator, this.denominator).Simplify();
        //        return String.Join(" ", number, String.Join("", num.numerator, "/", num.denominator));
        //    } else return s;
        //}

        public override bool Equals(Object obj) {
            return Equals(obj as Fraction);
        }

        public bool Equals(Fraction other) {
            return other != null && (this.Numerator == other.Numerator) && (this.Denominator == other.Denominator);
        }

        public override int GetHashCode() {
            return HashCode.Combine(Numerator, Denominator);
        }

        public Fraction Add(Fraction other) {
            if (this.Denominator != other.Denominator)
                this.CommonDenom(other);
            this.Numerator += other.Numerator;
            return this.Simplify();
        }

        public Fraction Subtract(Fraction other) {
            if (this.Denominator != other.Denominator)
                this.CommonDenom(other);
            this.Numerator -= other.Numerator;
            return this.Simplify();
        }

        public Fraction Multiply(Fraction other) {
            this.Numerator *= other.Numerator;
            this.Denominator *= other.Denominator;
            return this.Simplify();
        }

        public Fraction Divide(Fraction other) {
            this.Numerator *= other.Denominator;
            this.Denominator *= other.Numerator;
            return this.Simplify();
        }

        public Fraction Negate() {
            this.Numerator *= -1;
            return this;
        }

        public Fraction CommonDenom(Fraction other) {
            int commonDenom = this.Denominator * other.Denominator;
            this.Numerator *= other.Denominator;
            other.Numerator *= this.Denominator;
            this.Denominator = commonDenom;
            other.Denominator = commonDenom;
            return this;
        }

        public Fraction Simplify() {
            int factor = GCF(this.Numerator, this.Denominator);
            this.Numerator /= factor;
            this.Denominator /= factor;
            return this;
        }

        public static int GCF(int numerator, int denominator) {
            int factor = 1;

            Dictionary<int, int> numPrimes = Primes(numerator);
            Dictionary<int, int> denomPrimes = Primes(denominator);

            IEnumerable<int> commonPrimes = numPrimes.Keys.Intersect(denomPrimes.Keys);
            foreach (int prime in commonPrimes) {
                int minCount = Math.Min(numPrimes[prime], denomPrimes[prime]);
                factor *= (int)Math.Pow(prime, minCount);
            }

            return factor;
        }

        public static Dictionary<int, int> Primes(int number) {
            Dictionary<int, int> primes = new Dictionary<int, int>();

            double max = Math.Sqrt(number);
            int count = 0;
            for (int divisor = 2; divisor <= max; divisor++) {
                count = 0;
                if (divisor != 2 && divisor != 5 && (divisor % 2 == 0 || divisor % 5 == 0)) continue;
                while (number % divisor == 0) {
                    count++;
                    number /= divisor;
                }
                primes.Add(divisor, count);
            }

            return primes;
        }
    }
}

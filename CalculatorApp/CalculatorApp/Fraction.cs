using System;
using System.Collections.Generic;
using System.Linq;

namespace CalculatorApp {
    public class Fraction {
        public static Fraction ZERO => Create(0, 1);
        public static Fraction HUNDRED => Create(100, 1);
        private int numerator;
        private int denominator;
        private Fraction(int numerator, int denom) {
            this.numerator = numerator;
            this.denominator = denom;
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

        public decimal ToDecimal() {
            return Decimal.Divide(this.numerator, this.denominator);
        }

        public override string ToString() {
            return ToDecimal().ToString("G27");
        }

        public override bool Equals(Object obj) {
            return Equals(obj as Fraction);
        }

        public bool Equals(Fraction other) {
            return other != null && (this.numerator == other.numerator) && (this.denominator == other.denominator);
        }

        public override int GetHashCode() {
            return HashCode.Combine(numerator, denominator);
        }

        public Fraction Add(Fraction other) {
            if (this.denominator != other.denominator)
                this.CommonDenom(other);
            this.numerator += other.numerator;
            return this.Simplify();
        }

        public Fraction Subtract(Fraction other) {
            if (this.denominator != other.denominator)
                this.CommonDenom(other);
            this.numerator -= other.numerator;
            return this.Simplify();
        }

        public Fraction Multiply(Fraction other) {
            this.numerator *= other.numerator;
            this.denominator *= other.denominator;
            return this.Simplify();
        }

        public Fraction Divide(Fraction other) {
            this.numerator *= other.denominator;
            this.denominator *= other.numerator;
            return this.Simplify();
        }

        public Fraction Negate() {
            this.numerator *= -1;
            return this;
        }

        public Fraction CommonDenom(Fraction other) {
            int commonDenom = this.denominator * other.denominator;
            this.numerator *= other.denominator;
            other.numerator *= this.denominator;
            this.denominator = commonDenom;
            other.denominator = commonDenom;
            return this;
        }

        public Fraction Simplify() {
            int factor = GCF(this.numerator, this.denominator);
            this.numerator /= factor;
            this.denominator /= factor;
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

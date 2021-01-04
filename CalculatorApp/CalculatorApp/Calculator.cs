using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorApp {

    public class Calculator : Notify {
        private Fraction _memory;
        private Operator _op;
        private LastOperation _lastOperation;
        public Fraction Memory { get { return _memory; } set { _memory = value; OnPropertyChanged(); } }
        public Fraction Result { get; set; }
        public Operator Op { get { return _op; } set { _op = value; OnPropertyChanged(); } }
        public LastOperation LastOperation { get { return _lastOperation; } set { _lastOperation = value; OnPropertyChanged(); } }

        public Calculator() {
            Clear();
            ClearMemory();
        }

        public Fraction Calculate(Fraction term1, Operator op, Fraction term2) {
            LastOperation = new LastOperation(Fraction.Create(term1), op, Fraction.Create(term2));

            try {
                term1 = op switch {
                    Operator.ADD => term1.Add(term2),
                    Operator.SUBTRACT => term1.Subtract(term2),
                    Operator.MULTIPLY => term1.Multiply(term2),
                    Operator.DIVIDE => term1.Divide(term2),
                };
            } catch (Exception e) {
                LastOperation = LastOperation.Previous();
            }

            return term1;
        }

        public void Clear() {
            Op = Operator.ADD;
            Result = Fraction.ZERO;
            LastOperation.Clear();
            LastOperation = null;
        }

        public Fraction RecallMemory() {
            Result = Fraction.Create(Memory);
            return Memory;
        }

        public void ClearMemory() {
            Memory = Fraction.ZERO;
        }

        public void AddMemory(Fraction other) {
            Memory = Calculate(Memory, Operator.ADD, other);
        }

        public void SubtractMemory(Fraction other) {
            Memory = Calculate(Memory, Operator.SUBTRACT, other);
        }

        public Fraction Percentage(Fraction input) {
            return Result = Calculate(input, Operator.DIVIDE, Fraction.HUNDRED);
        }

        public Fraction Negate(Fraction input) {
            return Result = input.Negate();
        }
    }
}

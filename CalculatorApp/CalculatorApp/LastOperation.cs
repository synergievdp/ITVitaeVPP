using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorApp {
    public class LastOperation {
        static public List<LastOperation> operations = new List<LastOperation>();
        public Fraction Term1 { get; set; }
        public Fraction Term2 { get; set; }
        public Operator Op { get; set; }

        public LastOperation(Fraction term1, Operator op, Fraction term2) {
            this.Term1 = term1;
            this.Term2 = term2;
            this.Op = op;
            operations.Add(this);
        }

        public LastOperation Previous() {
            int index = operations.IndexOf(this);
            operations.Remove(this);
            return operations[index - 1];
        }

        public override string ToString() {
            return String.Join(" ", "(", Term1, Op.ToSymbol(), Term2, ")");
        }

        static public void Clear() {
            operations.Clear();
        }
    }
}

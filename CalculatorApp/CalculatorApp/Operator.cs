using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorApp {
    public enum Operator {
        ADD, SUBTRACT, MULTIPLY, DIVIDE
    }

    public static class OperatorMethods {
        public static string ToSymbol(this Operator value) {
            return value switch {
                Operator.ADD => "+",
                Operator.SUBTRACT => "-",
                Operator.MULTIPLY => "x",
                Operator.DIVIDE => "/",
            };
        }

        public static Operator ToOperator(this string value) {
            return value switch {
                "+" => Operator.ADD,
                "-" => Operator.SUBTRACT,
                "x" => Operator.MULTIPLY,
                "/" => Operator.DIVIDE,
            };
        }

        public static Boolean IsOperator(this string value) {
            return value == "+" || value == "-" || value == "x" || value == "/";
        }
    }
}

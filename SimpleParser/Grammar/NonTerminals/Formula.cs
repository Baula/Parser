using System.Collections.Generic;

namespace SimpleParser.Grammar.NonTerminals
{
    public class Formula : Symbol
    {
        private Formula(Expression expression) 
            : base(expression)
        {
            Expression = expression;
        }

        private Expression Expression { get; set; }

        public double Evaluate()
        {
            return Expression.Evaluate();
        }

        public static Formula Produce(IEnumerable<Symbol> symbols)
        {
            // formula = expression

            var expression = Expression.Produce(symbols);
            return expression == null
                       ? null
                       : new Formula(expression);
        }
    }
}
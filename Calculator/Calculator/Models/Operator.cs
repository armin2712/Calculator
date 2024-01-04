using System.Data;
using System.Numerics;
using System.Windows;

namespace Calculator.Models
{
    /// <summary>
    /// Delegate for handling messages.
    /// </summary>
    /// <param name="message">The message to be handled.</param>
    public delegate void MessageEventHandler(string message);

    /// <summary>
    /// Represents an operator with calculation functionalities.
    /// </summary>
    public class Operator : Validator, IOperator
    {
        private string _expression = null!;

        /// <summary>
        /// Gets or sets the mathematical expression.
        /// </summary>
        public string Expression
        {
            get
            {
                if (_expression == null)
                {
                    _expression = string.Empty;
                }
                return _expression;
            }
            set
            {
                _expression = value;
            }
        }

        private string _result = null!;

        /// <summary>
        /// Gets or sets the result of the calculation.
        /// </summary>
        public string Result
        {
            get
            {
                if (_result == null)
                {
                    _result = string.Empty;
                }
                return _result;
            }
            set { _result = value; }
        }

        private readonly char OpenParetheses = '(';
        private readonly char ClosedParetheses = ')';

        private int openBracketCount = 0;
        private int closeBracketCount = 0;

        #region Operating Methods

        /// <summary>
        /// Performs the calculation based on the current expression.
        /// </summary>
        public void Calculate()
        {
            string toCalculate = Expression;
            toCalculate = toCalculate.Replace("%", @"/100");
            toCalculate = ParenthesisHandler(toCalculate);
            var dataTable = new DataTable();
            dataTable.Columns.Add("toCalculate", typeof(string), toCalculate);
            DataRow row = dataTable.NewRow();
            dataTable.Rows.Add(row);
            if (toCalculate.Length == 0) { Result = "0"; }
            else
            {
                var result = double.Parse((string)row["toCalculate"]);

                if (result is double doubleResult && doubleResult % 1 == 0)
                {
                    // If the result is a whole number, convert to integer and then to string
                    Result = BigInteger.Parse(doubleResult.ToString()).ToString();
                }
                else
                {
                    // Otherwise, format the result with the desired number of decimal places
                    Result = result.ToString("R");
                }
            }
        }

        /// <summary>
        /// Event handler for displaying messages.
        /// </summary>
        public event MessageEventHandler OnDisplayMessage = delegate { };

        /// <summary>
        /// Displays a message using the specified message handler.
        /// </summary>
        /// <param name="message">The message to be displayed.</param>
        private void DisplayMessage(string message)
        {
            OnDisplayMessage?.Invoke(message);
        }

        /// <summary>
        /// Stores a numeric or operator value in the expression.
        /// </summary>
        /// <param name="value">The value to be stored.</param>
        public void StoreValue(string value)
        {
            if (Expression.Length > 15 && ContainsNonNumeric(Expression) == false)
            {
                DisplayMessage("Maximum number size reached.");
            }
            else if (Expression.Length > 20 && ContainsNonNumeric(Expression) == true)
            {
                DisplayMessage("Maximum input size reached.");
            }
            else if (Expression.Length < 1 && IsEndingNumber(value))
            {
                Expression = value;
            }
            else if (Expression[Expression.Length - 1] == ClosedParetheses && ContainsNonNumeric(value) == false)
            {
                Expression += "*" + value;
                Calculate();
            }
            else if (Expression[Expression.Length - 1] == '%' && IsEndingNumber(value) == true)
            {
                Expression += "*" + value;
                Calculate();
            }
            else if (IsEndingNumber(Expression) == false && IsEndingNumber(value) == false)
            {
                if (Expression[Expression.Length - 1] != OpenParetheses &&
                    Expression[Expression.Length - 1] != ClosedParetheses)
                {
                    Expression = Expression.Remove(Expression.Length - 1);
                    Expression += value;
                }
                else
                {
                    Expression += value;
                }
            }
            else if (ContainsNonNumeric(Expression) == true && IsEndingNumber(value) == true)
            {
                Expression += value;
                Calculate();
            }
            else
            {
                Expression += value;
            }
        }

        /// <summary>
        /// Performs the calculation and updates the expression with the result.
        /// </summary>
        public void Equal()
        {
            if (IsEndingNumber(Expression) == true || Expression[^1] == ClosedParetheses && ContainsNonNumeric(Expression) == true)
            {
                Expression = Result;
                Result = string.Empty;
                openBracketCount = 0;
                closeBracketCount = 0;
            }
        }

        /// <summary>
        /// Stores a decimal point in the expression.
        /// </summary>
        public void StoreDecimal()
        {
            if (CanAddDecimal(Expression) == true)
            {
                if (Expression.Length < 1 || IsEndingNumber(Expression) == false)
                {
                    if (Expression[Expression.Length - 1] == ClosedParetheses)
                    {
                        Expression += "*0" + ".";
                    }
                    else
                    {
                        Expression += "0" + ".";
                    }
                }
                else
                {
                    Expression += ".";
                }
            }
        }

        /// <summary>
        /// Clears the expression and result.
        /// </summary>
        public void Clear()
        {
            Expression = string.Empty;
            Result = string.Empty;
        }

        /// <summary>
        /// Deletes the last character in the expression.
        /// </summary>
        public void DeleteLast()
        {
            if (Expression.Length > 0)
            {
                Expression = Expression.Remove(Expression.Length - 1);
                Calculate();
            }
        }

        /// <summary>
        /// Inverts the sign of the current number in the expression.
        /// </summary>
        public void InvertNumber()
        {
            if (ContainsNonNumeric(Expression) == false && Expression.Length == 1 && Expression != "0")
            {
                Expression = "-" + Expression;
            }
            else if (ContainsNonNumeric(Expression) == true && Expression.Length == 2 && IsEndingNumber(Expression))
            {
                Expression = Expression.Remove(0, count: 1);
            }
        }

        /// <summary>
        /// Adds a percentage sign to the expression.
        /// </summary>
        public void Percent()
        {
            if (Expression.Length > 0 && PercentHandler(Expression) == true)
            {
                Expression += "%";
                Calculate();
            }
        }

        /// <summary>
        /// Handles parentheses in the expression.
        /// </summary>
        public void Parenthesis()
        {
            if (Expression.Length == 0)
            {
                Expression += OpenParetheses;
                openBracketCount++;
            }
            else if (Expression[Expression.Length - 1] == OpenParetheses)
            {
                Expression += OpenParetheses;
                openBracketCount++;
            }
            else if (IsEndingNumber(Expression) && openBracketCount > closeBracketCount)
            {
                Expression += ClosedParetheses;
                closeBracketCount++;
            }
            else if (Expression[Expression.Length - 1] == ClosedParetheses && openBracketCount == closeBracketCount)
            {
                Expression += "*" + OpenParetheses;
                openBracketCount++;
            }
            else if (IsEndingNumber(Expression) && openBracketCount == closeBracketCount)
            {
                Expression += "*" + OpenParetheses;
                openBracketCount++;
            }
            else if (IsEndingNumber(Expression) == false && Expression[Expression.Length - 1] != OpenParetheses &&
                Expression[Expression.Length - 1] != ClosedParetheses)
            {
                Expression += OpenParetheses;
                openBracketCount++;
            }
            else if (openBracketCount > closeBracketCount)
            {
                Expression += ClosedParetheses;
                closeBracketCount++;
            }
        }

        #endregion Operating Methods
    }
}

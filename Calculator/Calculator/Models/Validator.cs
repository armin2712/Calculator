
using System.Text.RegularExpressions;


namespace Calculator.Models
{
    /// <summary>
    /// Provides a set of validation methods for 
    /// checking various data types and conditions.
    /// </summary>
    public class Validator
    {
        /// <summary>
        /// Validates if the provided input has non numeric
        /// characters
        /// </summary>
        /// <param name="input">The input to be validated</param>
        /// <returns>True if the provided input meets the condition, false if not</returns>
        public static bool ContainsNonNumeric(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsDigit(c) && c is not '.' && c is not '%') // '.' is for numbers with comma
                {
                    return true;
                }
            }

            return false;
        }
        /// <summary>
        /// Validates if the provided input ends with numeric characters
        /// </summary>
        /// <param name="output">The input to be validated</param>
        /// <returns>True if the provided input meets the condition, false if not</returns>
        public static bool IsEndingNumber(string output)
        {
            return string.IsNullOrEmpty(output) || int.TryParse(output[^1].ToString(), out _);
        }

        /// <summary>
        /// Checks if the provided input is valid for adding a decimal point
        /// </summary>
        /// <param name="calculationString">String to be validated for decimal point addon</param>
        /// <returns>True if the provided string is valid for adding decimal point, false if not</returns>
        public static bool CanAddDecimal(string calculationString)
        {
            if (!string.IsNullOrEmpty(calculationString))
            {
                // Split the calculation string by all non-number characters
                string[] substrings = Regex.Split(calculationString, @"[^\d\.]+");

                // Check if there is at most one decimal point in the entire string
                if (substrings.Count(s => s == ".") <= 1)
                {
                    // Check if the last non-empty substring does not already contain a decimal point
                    string lastSubstring = substrings.LastOrDefault(s => !string.IsNullOrEmpty(s))!;
                    if (lastSubstring != null && !lastSubstring.Contains("."))
                    {
                        // Check if the last operand is an integer
                        double lastOperand;
                        if (double.TryParse(lastSubstring, out lastOperand))
                        {
                            // Allow adding a decimal point only if the last operand is an integer
                            return lastOperand % 1 == 0;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Validates if the provided input can accept percent signs
        /// </summary>
        /// <param name="input">String to be validated</param>
        /// <returns>True if the provided input can add a percent sign, false if not</returns>
        public static bool PercentHandler(string input)
        {
            if (input[input.Length - 1] == '%' || !char.IsDigit(input[input.Length - 1]))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Adds missing Parentheses so the Calculation can be done
        /// </summary>
        /// <param name="input">String that needs to be checked</param>
        /// <returns>String with equal Open and Closed Parentheses</returns>
        public static string ParenthesisHandler(string input)
        {
            while (IsEndingNumber(input) == false)
            {
                input = input.Remove(input.Length - 1);
            }
            int openBrackets = input.Count(c => c == '(');
            int closeBrackets = input.Count(c => c == ')');
            int missingBrackets = openBrackets - closeBrackets;
            for (int i = 0; i < missingBrackets; i++)
            {
                input += ')';
            }
            if (missingBrackets < 0)
            {
                for (int i = 0; i > missingBrackets; i++)
                {
                    input = closeBrackets + input;
                }
            }

            return input.ToString();
        }

    }
}

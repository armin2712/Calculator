namespace Calculator.Models
{
    /// <summary>
    /// Interface representing a basic calculator operator.
    /// </summary>
    internal interface IOperator
    {
        /// <summary>
        /// Gets or sets the current expression.
        /// </summary>
        string Expression { get; set; }

        /// <summary>
        /// Gets or sets the result of the calculation.
        /// </summary>
        string Result { get; set; }

        /// <summary>
        /// Performs the calculation based on the current expression.
        /// </summary>
        void Calculate();

        /// <summary>
        /// Event triggered to display messages, such as errors or notifications.
        /// </summary>
        event MessageEventHandler OnDisplayMessage;

        /// <summary>
        /// Stores a numeric value in the expression.
        /// </summary>
        /// <param name="value">The numeric value to store.</param>
        void StoreValue(string value);

        /// <summary>
        /// Performs the calculation when the equal sign is pressed.
        /// </summary>
        void Equal();

        /// <summary>
        /// Stores a decimal point in the expression.
        /// </summary>
        void StoreDecimal();

        /// <summary>
        /// Clears the current expression and result.
        /// </summary>
        void Clear();

        /// <summary>
        /// Deletes the last entered digit or operator.
        /// </summary>
        void DeleteLast();

        /// <summary>
        /// Inverts the sign of the current number.
        /// </summary>
        void InvertNumber();

        /// <summary>
        /// Calculates the percentage of the current number.
        /// </summary>
        void Percent();

        /// <summary>
        /// Handles the opening and closing of parentheses for advanced calculations.
        /// </summary>
        void Parenthesis();
    }
}

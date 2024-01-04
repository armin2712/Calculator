

namespace Calculator.ViewModels
{/// <summary>
 /// Interface for a class responsible for displaying calculations and results.
 /// </summary>
    internal interface IDisplayer
    {
        /// <summary>
        /// Gets or sets the content to be displayed for user input.
        /// </summary>
        string CalculationOutput { get; set; }

        /// <summary>
        /// Gets or sets the content to be displayed for the result.
        /// </summary>
        string ResultOutput { get; set; }

        /// <summary>
        /// Displays the specified content in the WPF app window section for user input.
        /// </summary>
        /// <param name="output">Content to be displayed.</param>
        void DisplayCalculation(string output);

        /// <summary>
        /// Displays the specified content in the WPF app window for the result.
        /// </summary>
        /// <param name="output">Content to be displayed.</param>
        void DisplayResult(string output);

        /// <summary>
        /// Converts signs for valid calculations display.
        /// </summary>
        /// <param name="value">String to convert.</param>
        /// <returns>String that has been converted.</returns>
        string SignConverter(string value);
    }
}

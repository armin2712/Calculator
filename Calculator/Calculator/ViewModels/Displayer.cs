using System.Text;

namespace Calculator.ViewModels
{
    /// <summary>
    /// Represents a class responsible for displaying calculations and results.
    /// </summary>
    public class Displayer : ObservableObject, IDisplayer
    {
        private string _calculationOutput = null!;

        /// <summary>
        /// Gets or sets the content to be displayed for user input.
        /// </summary>
        public string CalculationOutput
        {
            get
            {
                _calculationOutput ??= string.Empty;
                return _calculationOutput;
            }
            set
            {
                if (_calculationOutput != value)
                {
                    _calculationOutput = value;
                    OnPropertyChanged(nameof(CalculationOutput));
                }
            }
        }

        private string _resultOutput = null!;

        /// <summary>
        /// Gets or sets the content to be displayed for the result.
        /// </summary>
        public string ResultOutput
        {
            get
            {
                if (this._resultOutput == null)
                {
                    this._resultOutput = string.Empty;
                }
                return this._resultOutput;
            }
            set
            {
                if (this._resultOutput != value)
                {
                    this._resultOutput = value;
                    OnPropertyChanged(nameof(ResultOutput));
                }
            }
        }

        /// <summary>
        /// Displays the specified content in the WPF app window section for user input.
        /// </summary>
        /// <param name="output">Content to be displayed.</param>
        public void DisplayCalculation(string output)
        {
            output = SignConverter(output);
            this.CalculationOutput = output;
        }

        /// <summary>
        /// Displays the specified content in the WPF app window for the result.
        /// </summary>
        /// <param name="output">Content to be displayed.</param>
        public void DisplayResult(string output)
        {
            output = SignConverter(output);
            this.ResultOutput = output;
        }

        /// <summary>
        /// Converts signs for valid calculations display.
        /// </summary>
        /// <param name="value">String to convert.</param>
        /// <returns>String that has been converted.</returns>
        public string SignConverter(string value)
        {
            StringBuilder result = new StringBuilder(value.Length);

            foreach (char c in value)
            {
                if (c == '*')
                {
                    result.Append('×');
                }
                else if (c == '/')
                {
                    result.Append('÷');
                }
                else if (c == '÷')
                {
                    result.Append('/');
                }
                else if (c == '×')
                {
                    result.Append('*');
                }
                else
                {
                    result.Append(c);
                }
            }

            return result.ToString();
        }
    }
}

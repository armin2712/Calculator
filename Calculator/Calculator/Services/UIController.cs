using Calculator.Models;
using Calculator.ViewModels;
using System;
using System.Windows;

namespace Calculator.Services
{
    /// <summary>
    /// Controller for managing the interaction between the UI components and the calculator logic.
    /// </summary>
    public class UIController
    {
        /// <summary>
        /// Event triggered when a message needs to be displayed.
        /// </summary>
        public event MessageEventHandler OnDisplayMessage = delegate { };

        /// <summary>
        /// Gets the Displayer associated with this UIController.
        /// </summary>
        public Displayer Displayer { get; private set; }

        /// <summary>
        /// Gets the Operator associated with this UIController.
        /// </summary>
        public Operator Operator { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UIController"/> class.
        /// </summary>
        /// <param name="displayer">The Displayer instance.</param>
        /// <param name="_operator">The Operator instance.</param>
        public UIController(Displayer displayer, Operator _operator)
        {
            Displayer = displayer ?? throw new ArgumentNullException(nameof(displayer));
            Operator = _operator ?? throw new ArgumentNullException(nameof(_operator));
            Operator.OnDisplayMessage += DisplayMessageHandler;
        }

        private void DisplayMessageHandler(string message)
        {
            OnDisplayMessage?.Invoke(message);
            ShowMessageBox(message);
        }

        private void ShowMessageBox(string message)
        {
            MessageBox.Show(message, "Calculator Message", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void UpdateDisplay()
        {
            Displayer.DisplayCalculation(Operator.Expression);
            Displayer.DisplayResult(Operator.Result);
        }

        /// <summary>
        /// Sets the input for the calculator.
        /// </summary>
        /// <param name="input">The input to be processed.</param>
        public void SetInput(string input)
        {
            Operator.StoreValue(Displayer.SignConverter(input));
            UpdateDisplay();
        }

        /// <summary>
        /// Gets the result of the calculation.
        /// </summary>
        public void GetResult()
        {
            Operator.Equal();
            UpdateDisplay();
        }

        /// <summary>
        /// Adds a decimal point to the current input.
        /// </summary>
        public void AddDecimal()
        {
            Operator.StoreDecimal();
            UpdateDisplay();
        }

        /// <summary>
        /// Clears all inputs and results.
        /// </summary>
        public void DeleteAll()
        {
            Operator.Clear();
            UpdateDisplay();
        }

        /// <summary>
        /// Removes the last character from the current input.
        /// </summary>
        public void Backspace()
        {
            Operator.DeleteLast();
            UpdateDisplay();
        }

        /// <summary>
        /// Inverts the sign of the current number in the input.
        /// </summary>
        public void InvertNumberSign()
        {
            Operator.InvertNumber();
            UpdateDisplay();
        }

        /// <summary>
        /// Adds a percentage sign to the current input.
        /// </summary>
        public void AddPercent()
        {
            Operator.Percent();
            UpdateDisplay();
        }

        /// <summary>
        /// Adds an open or close parenthesis to the current input.
        /// </summary>
        public void AddBracket()
        {
            Operator.Parenthesis();
            UpdateDisplay();
        }
    }
}

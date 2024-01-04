using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Calculator.Models;
using Calculator.Services;
using Calculator.ViewModels;

namespace Calculator.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UIController AppController;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            AppController = new UIController(new Displayer(), new Operator());
            DataContext = AppController;
        }

        #region Buttons

        /// <summary>
        /// Handles the click event for numeric buttons.
        /// </summary>
        private void NumBtn_Click(object sender, RoutedEventArgs e)
        {
            string buttonValue = ((Button)sender).Content.ToString()!;
            AppController.SetInput(buttonValue);
        }

        /// <summary>
        /// Handles the click event for operation buttons.
        /// </summary>
        private void OperationBtn_Click(object sender, RoutedEventArgs e)
        {
            string buttonValue = ((Button)sender).Content.ToString()!;
            AppController.SetInput(buttonValue);
        }

        /// <summary>
        /// Handles the click event for the equal button.
        /// </summary>
        private void EqualBtn_Click(Object sender, RoutedEventArgs e)
        {
            AppController.GetResult();
        }

        /// <summary>
        /// Handles the click event for the dot button.
        /// </summary>
        private void DotBtn_Click(object sender, RoutedEventArgs e)
        {
            AppController.AddDecimal();
        }

        /// <summary>
        /// Handles the click event for the erase button.
        /// </summary>
        private void EraseBtn_Click(object sender, RoutedEventArgs e)
        {
            AppController.DeleteAll();
        }

        /// <summary>
        /// Handles the click event for the bracket button.
        /// </summary>
        private void BracketBtn_Click(object sender, RoutedEventArgs e)
        {
            AppController.AddBracket();
        }

        /// <summary>
        /// Handles the click event for the sign switch button.
        /// </summary>
        private void SignSwitchBtn_Click(object sender, RoutedEventArgs e)
        {
            AppController.InvertNumberSign();
        }

        /// <summary>
        /// Handles the click event for the backspace button.
        /// </summary>
        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            AppController.Backspace();
        }

        /// <summary>
        /// Handles the click event for the percent button.
        /// </summary>
        private void ProcentBtn_Click(object sender, RoutedEventArgs e)
        {
            AppController.AddPercent();
        }

        #endregion Operating Methods
    }
}

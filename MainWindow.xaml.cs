using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DisplayCalculator
{


    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const double _inch = 25.4;
        private const int _centimeter = 10;
        private bool IsReverseMode = false;

        private Display Display;

        private double diagonal;
        private Corralation corralation;
        private double width;
        private double height;

        public MainWindow()
        {
            InitializeComponent();

            TextBox_Diagonal.TextChanged += InputChanged;
            TextBox_Diagonal.TextChanged += CheckOnCorrectInput;

            TextBox_Corralation.TextChanged += InputChanged;
            TextBox_Corralation.TextChanged += CheckOnCorrectInput;

            TextBox_Width.TextChanged += InputChanged;
            TextBox_Width.TextChanged += CheckOnCorrectInput;

            TextBox_Height.TextChanged += InputChanged;
            TextBox_Height.TextChanged += CheckOnCorrectInput;
        }


        private void CheckOnCorrectInput(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(TextBox_Corralation.Text, @"^[1-9]+\d*[:][1-9]+\d*$") && TextBox_Corralation.Text != String.Empty)
                TextBox_Corralation.Style = (Style)Resources["WarningTextBox"];
            else
                TextBox_Corralation.Style = (Style)Resources["DefaultTextBox"];

            if (!Regex.IsMatch(TextBox_Diagonal.Text, @"^\s*\d*[,]?\d*\s*$") && TextBox_Diagonal.Text != String.Empty)
                TextBox_Diagonal.Style = (Style)Resources["WarningTextBox"];
            else
                TextBox_Diagonal.Style = (Style)Resources["DefaultTextBox"];

            if (!Regex.IsMatch(TextBox_Width.Text, @"^\s*\d*[,]?\d*\s*$") && TextBox_Width.Text != String.Empty)
                TextBox_Width.Style = (Style)Resources["WarningTextBox"];
            else
                TextBox_Width.Style = (Style)Resources["DefaultTextBox"];

            if (!Regex.IsMatch(TextBox_Height.Text, @"^\s*\d*[,]?\d*\s*$") && TextBox_Height.Text != String.Empty)
                TextBox_Height.Style = (Style)Resources["WarningTextBox"];
            else
                TextBox_Height.Style = (Style)Resources["DefaultTextBox"];
        }

        private void InputChanged(object sender, RoutedEventArgs e)
        {
            if (!IsReverseMode)
            {
                try
                {
                    string[] corralationInput = TextBox_Corralation.Text.Split(':');
                    if (corralationInput.Length != 2)
                        throw new Exception();
                    uint WCorralation = UInt32.Parse(corralationInput[0]);
                    uint HCorralayion = UInt32.Parse(corralationInput[1]);
                    corralation = new Corralation(WCorralation, HCorralayion);

                    if (ComboBoxDiagonalInch.IsSelected)
                        diagonal = Double.Parse(TextBox_Diagonal.Text) * _inch;
                    else if (ComboBoxDiagonalСentimeter.IsSelected)
                        diagonal = Double.Parse(TextBox_Diagonal.Text) * _centimeter;
                    else
                        diagonal = Double.Parse(TextBox_Diagonal.Text);

                    Display = new Display(diagonal, corralation);

                    TextBox_Width.Text = Display.width.ToString();
                    TextBox_Height.Text = Display.height.ToString();
                    if (!TextBox_Corralation.IsFocused)
                        TextBox_Corralation.Text = Display.correlation.ToString();
                }
                catch
                {
                    if (TextBox_Width != null && TextBox_Height != null)
                    {
                        TextBox_Width.Text = String.Empty;
                        TextBox_Height.Text = String.Empty;
                    }
                }
            }
            else
            {

            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ReverseModeBtn_Click(object sender, RoutedEventArgs e)
        {
            IsReverseMode = true;
            DefaultModeGrid.Visibility = Visibility.Collapsed;
            ReverseModeGrid.Visibility = Visibility.Visible;
        }

        private void ExitTheReverseModeBtn_Click(object sender, RoutedEventArgs e)
        {
            IsReverseMode = false;
            ReverseModeGrid.Visibility = Visibility.Collapsed;
            DefaultModeGrid.Visibility = Visibility.Visible;
        }
    }
}

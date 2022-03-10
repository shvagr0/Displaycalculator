using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace DisplayCalculator
{
    public partial class MainWindow : Window
    {
        private const double _inch = 25.4;
        private const double _centimeter = 10.0;

        private enum Modes { DefaultHW, DefaultWidth, DefaultHeight, ReverseHW, ReverseWidth, ReverseHeight }
        private Modes Mode = Modes.DefaultHW;

        private Display display;

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

        private void GetCorralation()
        {
            string[] corralationInput = TextBox_Corralation.Text.Split(':');
            if (corralationInput.Length != 2)
                throw new FormatException();
            uint WCorralation = UInt32.Parse(corralationInput[0]);
            uint HCorralayion = UInt32.Parse(corralationInput[1]);
            corralation = new Corralation(WCorralation, HCorralayion);
        }

        private void InputChanged(object sender, RoutedEventArgs e)
        {
            switch (Mode)
            {
                case Modes.DefaultHW:
                    try
                    {
                        GetCorralation();

                        #region Diagonal input
                        if (ComboBoxDiagonalInch.IsSelected)
                            diagonal = Double.Parse(TextBox_Diagonal.Text) * _inch;
                        else if (ComboBoxDiagonalСentimeter.IsSelected)
                            diagonal = Double.Parse(TextBox_Diagonal.Text) * _centimeter;
                        else
                            diagonal = Double.Parse(TextBox_Diagonal.Text);
                        #endregion

                        display = new Display(diagonal, corralation);

                        #region Width output
                        if (ComboBoxWidthInch.IsSelected)
                            TextBox_Width.Text = (display.Width / _inch).ToString();
                        else if (ComboBoxWidthСentimeter.IsSelected)
                            TextBox_Width.Text = (display.Width / _centimeter).ToString();
                        else
                            TextBox_Width.Text = display.Width.ToString();
                        #endregion

                        #region Height output
                        if (ComboBoxHeightInch.IsSelected)
                            TextBox_Height.Text = (display.Height / _inch).ToString();
                        else if (ComboBoxHeightСentimeter.IsSelected)
                            TextBox_Height.Text = (display.Height / _centimeter).ToString();
                        else
                            TextBox_Height.Text = display.Height.ToString();
                        #endregion

                        /*Reduce correlation*/
                        if (!TextBox_Corralation.IsFocused)
                            TextBox_Corralation.Text = display.Correlation.ToString();
                    }
                    catch
                    {
                        if (TextBox_Width != null && TextBox_Height != null)
                        {
                            TextBox_Width.Text = String.Empty;
                            TextBox_Height.Text = String.Empty;
                        }
                    }
                    break;
                case Modes.DefaultWidth:
                    try
                    {
                        GetCorralation();

                        #region Height input
                        if (ComboBoxHeightInch.IsSelected)
                            height = Double.Parse(TextBox_Height.Text) * _inch;
                        else if (ComboBoxHeightСentimeter.IsSelected)
                            height = Double.Parse(TextBox_Height.Text) * _centimeter;
                        else
                            height = Double.Parse(TextBox_Height.Text);
                        #endregion

                        display = new Display(corralation, height, Display.Side.Height);

                        #region Diagonal output
                        if (ComboBoxDiagonalInch.IsSelected)
                            TextBox_Diagonal.Text = (display.Diagonal / _inch).ToString();
                        else if (ComboBoxDiagonalСentimeter.IsSelected)
                            TextBox_Diagonal.Text = (display.Diagonal / _centimeter).ToString();
                        else
                            TextBox_Diagonal.Text = display.Diagonal.ToString();
                        #endregion

                        #region Width output
                        if (ComboBoxWidthInch.IsSelected)
                            TextBox_Width.Text = (display.Width / _inch).ToString();
                        else if (ComboBoxWidthСentimeter.IsSelected)
                            TextBox_Width.Text = (display.Width / _centimeter).ToString();
                        else
                            TextBox_Width.Text = display.Width.ToString();
                        #endregion

                        /*Reduce correlation*/
                        if (!TextBox_Corralation.IsFocused)
                            TextBox_Corralation.Text = display.Correlation.ToString();
                    }
                    catch
                    {
                        if (TextBox_Width != null && TextBox_Diagonal != null)
                        {
                            TextBox_Width.Text = String.Empty;
                            TextBox_Diagonal.Text = String.Empty;
                        }
                    }
                    break;
                case Modes.DefaultHeight:
                    try
                    {
                        GetCorralation();

                        #region Width input
                        if (ComboBoxWidthInch.IsSelected)
                            width = Double.Parse(TextBox_Width.Text) * _inch;
                        else if (ComboBoxWidthСentimeter.IsSelected)
                            width = Double.Parse(TextBox_Width.Text) * _centimeter;
                        else
                            width = Double.Parse(TextBox_Width.Text);
                        #endregion

                        display = new Display(corralation, width, Display.Side.Width);

                        #region Diagonal output
                        if (ComboBoxDiagonalInch.IsSelected)
                            TextBox_Diagonal.Text = (display.Diagonal / _inch).ToString();
                        else if (ComboBoxDiagonalСentimeter.IsSelected)
                            TextBox_Diagonal.Text = (display.Diagonal / _centimeter).ToString();
                        else
                            TextBox_Diagonal.Text = display.Diagonal.ToString();
                        #endregion

                        #region Height output
                        if (ComboBoxHeightInch.IsSelected)
                            TextBox_Height.Text = (display.Height / _inch).ToString();
                        else if (ComboBoxHeightСentimeter.IsSelected)
                            TextBox_Height.Text = (display.Height / _centimeter).ToString();
                        else
                            TextBox_Height.Text = display.Height.ToString();
                        #endregion

                        /*Reduce correlation*/
                        if (!TextBox_Corralation.IsFocused)
                            TextBox_Corralation.Text = display.Correlation.ToString();
                    }
                    catch
                    {
                        if (TextBox_Height != null && TextBox_Diagonal != null)
                        {
                            TextBox_Height.Text = String.Empty;
                            TextBox_Diagonal.Text = String.Empty;
                        }
                    }
                    break;
                case Modes.ReverseHW:
                    try
                    {
                        #region Width input
                        if (ComboBoxWidthInch.IsSelected)
                            width = Double.Parse(TextBox_Width.Text) * _inch;
                        else if (ComboBoxWidthСentimeter.IsSelected)
                            width = Double.Parse(TextBox_Width.Text) * _centimeter;
                        else
                            width = Double.Parse(TextBox_Width.Text);
                        #endregion

                        #region Height input
                        if (ComboBoxHeightInch.IsSelected)
                            height = Double.Parse(TextBox_Height.Text) * _inch;
                        else if (ComboBoxHeightСentimeter.IsSelected)
                            height = Double.Parse(TextBox_Height.Text) * _centimeter;
                        else
                            height = Double.Parse(TextBox_Height.Text);
                        #endregion

                        display = new Display(width, height);

                        #region Diagonal output
                        if (ComboBoxDiagonalInch.IsSelected)
                            TextBox_Diagonal.Text = (display.Diagonal / _inch).ToString();
                        else if (ComboBoxDiagonalСentimeter.IsSelected)
                            TextBox_Diagonal.Text = (display.Diagonal / _centimeter).ToString();
                        else
                            TextBox_Diagonal.Text = display.Diagonal.ToString();
                        #endregion

                        TextBox_Corralation.Text = display.Correlation.ToString();
                    }
                    catch
                    {
                        if (TextBox_Corralation != null && TextBox_Diagonal != null)
                        {
                            TextBox_Corralation.Text = String.Empty;
                            TextBox_Diagonal.Text = String.Empty;
                        }
                    }
                    break;
                case Modes.ReverseWidth:
                    try
                    {
                        #region Width input
                        if (ComboBoxWidthInch.IsSelected)
                            width = Double.Parse(TextBox_Width.Text) * _inch;
                        else if (ComboBoxWidthСentimeter.IsSelected)
                            width = Double.Parse(TextBox_Width.Text) * _centimeter;
                        else
                            width = Double.Parse(TextBox_Width.Text);
                        #endregion

                        #region Diagonal input
                        if (ComboBoxDiagonalInch.IsSelected)
                            diagonal = Double.Parse(TextBox_Diagonal.Text) * _inch;
                        else if (ComboBoxDiagonalСentimeter.IsSelected)
                            diagonal = Double.Parse(TextBox_Diagonal.Text) * _centimeter;
                        else
                            diagonal = Double.Parse(TextBox_Diagonal.Text);
                        #endregion

                        display = new Display(diagonal, width, Display.Side.Width);

                        #region Height output
                        if (ComboBoxHeightInch.IsSelected)
                            TextBox_Height.Text = (display.Height / _inch).ToString();
                        else if (ComboBoxHeightСentimeter.IsSelected)
                            TextBox_Height.Text = (display.Height / _centimeter).ToString();
                        else
                            TextBox_Height.Text = display.Height.ToString();
                        #endregion

                        TextBox_Corralation.Text = display.Correlation.ToString();
                    }
                    catch
                    {
                        if (TextBox_Corralation != null && TextBox_Height != null)
                        {
                            TextBox_Corralation.Text = String.Empty;
                            TextBox_Height.Text = String.Empty;
                        }
                    }
                    break;
                case Modes.ReverseHeight:
                    try
                    {
                        #region Height input
                        if (ComboBoxHeightInch.IsSelected)
                            height = Double.Parse(TextBox_Height.Text) * _inch;
                        else if (ComboBoxHeightСentimeter.IsSelected)
                            height = Double.Parse(TextBox_Height.Text) * _centimeter;
                        else
                            height = Double.Parse(TextBox_Height.Text);
                        #endregion

                        #region Diagonal input
                        if (ComboBoxDiagonalInch.IsSelected)
                            diagonal = Double.Parse(TextBox_Diagonal.Text) * _inch;
                        else if (ComboBoxDiagonalСentimeter.IsSelected)
                            diagonal = Double.Parse(TextBox_Diagonal.Text) * _centimeter;
                        else
                            diagonal = Double.Parse(TextBox_Diagonal.Text);
                        #endregion

                        display = new Display(diagonal, height, Display.Side.Height);

                        #region Width output
                        if (ComboBoxWidthInch.IsSelected)
                            TextBox_Width.Text = (display.Width / _inch).ToString();
                        else if (ComboBoxWidthСentimeter.IsSelected)
                            TextBox_Width.Text = (display.Width / _centimeter).ToString();
                        else
                            TextBox_Width.Text = display.Width.ToString();
                        #endregion

                        TextBox_Corralation.Text = display.Correlation.ToString();
                    }
                    catch
                    {
                        if (TextBox_Corralation != null && TextBox_Width != null)
                        {
                            TextBox_Corralation.Text = String.Empty;
                            TextBox_Width.Text = String.Empty;
                        }
                    }
                    break;
            }
        }

        private void SwitchModes(object sender, RoutedEventArgs e)
        {
            if (ModeSwitcher.IsChecked == true)
            {
                TextBox_Corralation.IsEnabled = false;
                WarningIcon.Visibility = Visibility.Visible;

                if (WHMode.IsChecked == true)
                {
                    Mode = Modes.ReverseHW;

                    TextBox_Diagonal.IsEnabled = false;

                    TextBox_Width.IsEnabled = true;
                    TextBox_Height.IsEnabled = true;
                }
                else if (WidthMode.IsChecked == true)
                {
                    Mode = Modes.ReverseWidth;

                    TextBox_Diagonal.IsEnabled = true;

                    TextBox_Width.IsEnabled = true;
                    TextBox_Height.IsEnabled = false;
                }
                else if (HeightMode.IsChecked == true)
                {
                    Mode = Modes.ReverseHeight;

                    TextBox_Diagonal.IsEnabled = true;

                    TextBox_Width.IsEnabled = false;
                    TextBox_Height.IsEnabled = true;
                }
            }
            else if (ModeSwitcher.IsChecked == false)
            {
                TextBox_Corralation.IsEnabled = true;
                WarningIcon.Visibility = Visibility.Collapsed;

                if (WHMode.IsChecked == true)
                {
                    Mode = Modes.DefaultHW;

                    TextBox_Diagonal.IsEnabled = true;

                    TextBox_Width.IsEnabled = false;
                    TextBox_Height.IsEnabled = false;
                }
                else if (WidthMode.IsChecked == true)
                {
                    Mode = Modes.DefaultWidth;

                    TextBox_Diagonal.IsEnabled = false;

                    TextBox_Width.IsEnabled = false;
                    TextBox_Height.IsEnabled = true;
                }
                else if (HeightMode.IsChecked == true)
                {
                    Mode = Modes.DefaultHeight;

                    TextBox_Diagonal.IsEnabled = false;

                    TextBox_Width.IsEnabled = true;
                    TextBox_Height.IsEnabled = false;
                }
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void Corralation_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"[^0-9:]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}

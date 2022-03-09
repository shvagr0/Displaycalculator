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
        public MainWindow()
        {
            InitializeComponent();

            foreach (UIElement el in MainGrid.Children)
            {
                //if (el is Button)
                //{
                //    ((Button)el).Click += Button_Click;
                //}
                //else if (el is CheckBox)
                //{
                //    ((CheckBox)el).Checked += CheckBox_Click;
                //}
            }
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    if (CheckBoxMode.IsChecked == true)
        //    {
        //        bool setSide = (bool)RadioButtW.IsChecked;
        //        double sideL;
        //        try
        //        {
        //            if (setSide)
        //            {
        //                sideL = double.Parse(wResult.Text);
        //            }
        //            else
        //            {
        //                sideL = double.Parse(hResult.Text);
        //            }
        //            var monitor = new Display(new Corralation(12, 4), sideL, Display.Side.Width);

        //            diagonal.Text = string.Format("{0:0.0}", monitor.diagonal);

        //            if (setSide)
        //            {
        //                hResult.Text = string.Format("{0:0.000}", monitor.height);
        //            }
        //            else
        //            {
        //                wResult.Text = string.Format("{0:0.000}", monitor.width);
        //            }
        //        }
        //        catch { }

        //    }
        //    else
        //    {
        //        try
        //        {
        //            Corralation corralation = new Corralation(12, 4);
        //            var monitor = new Display(double.Parse(diagonal.Text), new Corralation(12, 2));

        //            hResult.Text = string.Format("{0:0.000}", monitor.height);
        //            wResult.Text = string.Format("{0:0.000}", monitor.width);

        //        }
        //        catch { }
        //    }
        //}

        //private void CheckBox_Click(object sender, RoutedEventArgs e)
        //{
        //    Color colorResultBox = Color.FromRgb(230, 230, 130);
        //    Color defaultColorBox = Color.FromRgb(255, 255, 255);
        //    if (CheckBoxMode.IsChecked == false)
        //    {
        //        #region IsEnabled bools
        //        wResult.Style = (Style)Resources["CloseBox"];
        //        hResult.Style = (Style)Resources["CloseBox"];

        //        diagonal.Style = (Style)Resources["OpenBox"];
        //        correlation.Style = (Style)Resources["OpenBox"];

        //        RadioButtW.Visibility = Visibility.Hidden;
        //        if (RadioButtH != null)
        //            RadioButtH.Visibility = Visibility.Hidden;
        //        #endregion
        //    }
        //    else
        //    {
        //        #region IsEnabled bools
        //        wResult.Style = (Style)Resources["OpenBox"];
        //        hResult.Style = (Style)Resources["OpenBox"];

        //        diagonal.Style = (Style)Resources["CloseBox"];
        //        correlation.Style = (Style)Resources["OpenBox"];

        //        if (RadioButtW.IsChecked == true)
        //        {
        //            hResult.Style = (Style)Resources["CloseBox"];
        //        }
        //        else
        //        {
        //            wResult.Style = (Style)Resources["CloseBox"];
        //        }

        //        RadioButtH.Visibility = Visibility.Visible;
        //        RadioButtW.Visibility = Visibility.Visible;
        //        #endregion
        //    }
        //}

        //private void OnlyNumbers(object sender, TextCompositionEventArgs e)
        //{
        //    Regex regex = new Regex(@"[\d,]+");
        //    e.Handled = regex.IsMatch(e.Text);
        //}

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ReverseModeBtn_Click(object sender, RoutedEventArgs e)
        {
            DefaultModeGrid.Visibility = Visibility.Collapsed;
            ReverseModeGrid.Visibility = Visibility.Visible;
        }

        private void ExitTheReverseModeBtn_Click(object sender, RoutedEventArgs e)
        {
            ReverseModeGrid.Visibility = Visibility.Collapsed;
            DefaultModeGrid.Visibility = Visibility.Visible;
        }
    }
}

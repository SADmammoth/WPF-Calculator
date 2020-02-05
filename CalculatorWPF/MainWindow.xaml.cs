using System;
using System.Windows;
using System.Windows.Controls;
using Calculator;
using System.Threading;
using System.Text.RegularExpressions;

namespace CalculatorWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Calc calc = new Calc();
        bool flag = false;
        public MainWindow()
        {
            InitializeComponent();
            Button button;
            for (int i = 0; i < Buttons.Children.Count; i++) {
                button = Buttons.Children[i] as Button;
                button.Click += PressButton;
             }
            Expression.PreviewTextInput += (sender, e)=> { if(!Validate(sender as TextBox, new Regex(@"(\d[+\/*=-]\d?$)|(^[\d.]+$)|(CLEAR)"))) { e.Handled = true; } };
        }

        private void PressButton(object sender, EventArgs eventArgs)
        {
            noStyle(Expression as TextBox, null);
            if (this.flag)
            {
               this.calc.Clear();
                Expression.Text = "";
            }
            string text = (sender as Button).Content.ToString();
            Console.WriteLine(text + Expression.Text);
            if (!Validate(Expression, new Regex(@"(\d[+\/*=-]\d?$)|(^[\d.]+$)|(CLEAR)"), Expression.Text+text))
            {
                Error(Expression);
                return;
            }
            if (text == "CLEAR")
            {
                this.calc.Clear();
            }
            else
            {
                this.flag = this.calc.AddElement(text);
            }
            Expression.Text = this.calc.Expression.ToString();

        }

        private bool Validate(TextBox sender, Regex regex, string text = "")
        {
            if(text == string.Empty)
            {
                text = sender.Text;
            }
            if (!regex.IsMatch(text))
            {
                Error(sender);
                return false;
            }
            return true;
        }

        private void Error(TextBox obj)
        {
            obj.Style = this.FindResource("errorBox") as Style;
            obj.TextInput += (sender, e) => noStyle(sender as TextBox, e);
        }
        private void noStyle(TextBox sender, RoutedEventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box.Style.Equals(this.FindResource("errorBox")))
            {
                Thread.Sleep(500);
                box.Style = this.FindResource("normalBox") as Style;
            }
        }
    }
}

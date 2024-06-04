using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace First_game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new();
        int tenthsOfSecondElapsed;
        int matchFound;
        public MainWindow()
        {

            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            tenthsOfSecondElapsed++;
            timeTextBlock.Text = (tenthsOfSecondElapsed / 10f).ToString("0.0s");
            if (matchFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " . Play again?";
            }
        }

        private void SetUpGame()
        {
            List<string> emoji = new()
            {
                "🐱","🐱",
                "🐶","🐶",
                "🦁","🦁",
                "🐯","🐯",
                "🦒","🦒",
                "🦊","🦊",
                "🦝","🦝",
                "🦉","🦉",
            };
            Random random = new();
            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>()) 
            {
                if (textBlock.Name != "timeTextBlock") 
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(emoji.Count);
                    string nextemoji = emoji[index];
                    textBlock.Text = nextemoji;
                    emoji.RemoveAt(index);
                }
                timer.Start();
                tenthsOfSecondElapsed = 0;
                matchFound = 0;

            }



        }
        private TextBlock lastTextBlock;
        private bool findingmatch = false;
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (findingmatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlock = textBlock;
                findingmatch = true;
            }
            else if (textBlock.Text == lastTextBlock.Text)
            {
                textBlock.Visibility = Visibility.Hidden;
                findingmatch = false;
                matchFound++;
            }
            else
            {
                lastTextBlock.Visibility = Visibility.Visible;
                findingmatch = false;
            }

        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchFound == 8)
            {
                SetUpGame();
            }
        }
    }
}
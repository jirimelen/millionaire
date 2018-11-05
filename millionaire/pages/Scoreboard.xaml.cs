using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace millionaire.pages
{
    /// <summary>
    /// Interakční logika pro Scoreboard.xaml
    /// </summary>
    public partial class Scoreboard : Page
    {
        Frame frame;
        bool isNewScore;

        public Scoreboard()
        {
            InitializeComponent();
        }
        public Scoreboard(Frame mainFrame, bool newScore = false) : this()
        {
            frame = mainFrame;
            isNewScore = newScore;
            scores = fileManager.GetScoresData();
            PrintScores();
        }

        private props.FileManager fileManager = new props.FileManager();
        private List<props.Score> scores = new List<props.Score>();

        public void PrintScores()
        {
            StackPanel table = WPF_table;

            props.Score lastAdded = scores.Last();
            // sort by prize
            scores = scores.OrderBy(p => int.Parse(p.Prize.Substring(0, p.Prize.Length - 3).Replace(" ", String.Empty))).Reverse().ToList();
            // keep only top 15 scores -> delete the lowest
            if (scores.Count > 15)
            {
                scores.Remove(scores.Last());
            }
            fileManager.SaveScoresData(scores);
            // add a row to table for each score
            foreach (var score in scores)
            {
                // change background color for last added score
                Brush color;
                if (score == lastAdded && isNewScore == true)
                {
                    color = Brushes.Orange;
                }
                else
                {
                    color = Brushes.Transparent;
                }

                StackPanel Item = new StackPanel() { Height = 30, Orientation = Orientation.Horizontal, Background = color };

                TextBlock attr1 = new TextBlock() { Text = score.Name, FontSize = 20, Width = 300, VerticalAlignment = VerticalAlignment.Center };
                TextBlock attr2 = new TextBlock() { Text = score.Prize, FontSize = 20, Width = 200, VerticalAlignment = VerticalAlignment.Center };
                TextBlock attr3 = new TextBlock() { Text = score.Date, FontSize = 20, Width = 200, VerticalAlignment = VerticalAlignment.Center };

                Item.Children.Add(attr1);
                Item.Children.Add(attr2);
                Item.Children.Add(attr3);

                table.Children.Add(Item);
            }
            
        }

        private void BackToMenu(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new Menu(frame));
        }
    }
}

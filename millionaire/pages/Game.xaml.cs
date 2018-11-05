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
    /// Interakční logika pro Game.xaml
    /// </summary>
    public partial class Game : Page
    {
        Frame frame;

        public Game()
        {
            InitializeComponent();
        }
        public Game(Frame mainFrame, bool Continue = false) : this()
        {
            frame = mainFrame;

            answers = new List<TextBlock>() {
                WPF_answer1,
                WPF_answer2,
                WPF_answer3,
                WPF_answer4
            };

            shapes = new List<Rectangle>()
            {
                WPF_shape15,
                WPF_shape14,
                WPF_shape13,
                WPF_shape12,
                WPF_shape11,
                WPF_shape10,
                WPF_shape9,
                WPF_shape8,
                WPF_shape7,
                WPF_shape6,
                WPF_shape5,
                WPF_shape4,
                WPF_shape3,
                WPF_shape2,
                WPF_shape1
            };

            shapes[statusPanel.IndicatorPosition].Visibility = Visibility.Visible;
            Update_data();
            // if save found, Load
            if (Continue)
            {
                gameManager = new props.GameManager(Continue);
                for (int i = 0; i < 14 - gameManager.GetIndicatorPosition(); i++)
                {
                    statusPanel.IndicatorPosition--;
                    shapes[statusPanel.IndicatorPosition].Visibility = Visibility.Visible;
                }
                props.Question question = gameManager.GetActualQuestion();

                int trueAnswNum = random.Next(0, 4);

                int FakeAnsIteration = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (i == trueAnswNum) answers[i].Text = question.Answer;
                    else answers[i].Text = question.FakeAnswers[FakeAnsIteration++];
                }

                WPF_question.Text = question.Content;
                Grid.SetRow(WPF_actualIndicator, statusPanel.IndicatorPosition);
            }
        }

        private props.GameManager gameManager = new props.GameManager();
        private props.StatusPanel statusPanel = new props.StatusPanel();

        private List<TextBlock> answers = new List<TextBlock>();
        private List<Rectangle> shapes = new List<Rectangle>();

        private Random random = new Random();
        
        private void Answer_button_click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int answNum = int.Parse(button.Name[button.Name.Length - 1].ToString());

            if(gameManager.Answer(answers[answNum - 1].Text))
            {
                // if all 15 questions answered correctly 
                if (statusPanel.IndicatorPosition <= 0)
                {
                    gameManager.SaveEndedGame();
                    frame.Navigate(new pages.Scoreboard(frame, true));
                }
                else
                {
                    statusPanel.IndicatorPosition--;
                    gameManager.SaveGame(statusPanel.IndicatorPosition);
                } 

                shapes[statusPanel.IndicatorPosition].Visibility = Visibility.Visible;
                Update_data();
            }
            else
            {
                // if the answer is not right 
                WPF_question.Text = "no :(";
                gameManager.LoseGame();
                frame.Navigate(new Scoreboard(frame, true));
            }
        }
        
        private async void Update_data()
        {
            // get next question / level up
            props.Question question = await gameManager.GetQusetion();

            int trueAnswNum = random.Next(0,4);
            // assign text to buttons + question
            int FakeAnsIteration = 0;
            for (int i = 0; i < 4; i++)
            {
                if (i == trueAnswNum) answers[i].Text = question.Answer;
                else answers[i].Text = question.FakeAnswers[FakeAnsIteration++];
            }

            WPF_question.Text = question.Content;
            Grid.SetRow(WPF_actualIndicator, statusPanel.IndicatorPosition);
        }

        public void AbortClick(object sender, RoutedEventArgs e)
        {
            gameManager.AbortGame();
            frame.Navigate(new pages.Scoreboard(frame, true));
        }






        private void BackToMenu(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new Menu(frame));
        }
    }
}

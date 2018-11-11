using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace millionaire.props
{
    class GameManager
    {
        private Random random = new Random();

        private props.FileManager fileManager = new props.FileManager();
        private List<List<Question>> levels = new List<List<Question>>();
        private List<Question> questions = new List<Question>();

        private List<string> prizes = new List<string>() {
            "1 000 Kč",
            "2 000 Kč",
            "3 000 Kč",
            "5 000 Kč",
            "10 000 Kč",
            "20 000 Kč",
            "40 000 Kč",
            "80 000 Kč",
            "160 000 Kč",
            "320 000 Kč",
            "640 000 Kč",
            "1250  000 Kč",
            "2 500 000 Kč",
            "5 000 000 Kč",
            "10 000 000 Kč"
        };

        private int actualLevel = 0;
        private Question actualQuestion;

        private props.Progress playerProgress;


        public GameManager(bool Continue = false)
        {
            levels = fileManager.GetQuestionsData();

            if(Continue)
            {
                playerProgress = fileManager.GetProgressData();

                actualLevel = playerProgress.Level;
                actualQuestion = playerProgress.Question;
            }
        }

        public async Task<Question> GetQusetion()
        {
            //return actualLevel < 15 ? actualQuestion = levels[actualLevel][random.Next(0, levels[actualLevel].Count)] : new Question();
            // make it get questions from external api http://millionaire.jirimelen.cz/{category}/{level}

            // create own class for communication with server 

            if (actualLevel <= 14)
            {

                HttpClient client = new HttpClient();

                var response = await client.GetAsync("http://millionaire.jirimelen.cz/" + "sport" + "?level=" + (actualLevel + 1));

                string json = await response.Content.ReadAsStringAsync();

                return actualQuestion = JsonConvert.DeserializeObject<List<Question>>(json)[random.Next(0, 15)];

            }
            else
            {
                return actualQuestion = new Question() { Answer = "", Content = "", FakeAnswers = new List<string>() { "", "", "" } };
            }
            
        }

        public bool Answer(string answer)
        {
            if (answer.Equals(actualQuestion.Answer))
            {
                // after each answer save the progress to separate file to be loaded when option "continue" is selected
                actualLevel++;

                return true; 
            }
            else
	        {
                // return to checkpoint, show modal, save score, redirect to scoreboard ( function EndGame() )
                // clear progress file to disable the continue option in menu 
                return false;
            }
        }

        public Question GetActualQuestion()
        {
            return actualQuestion;
        }


        public int GetIndicatorPosition()
        {
            return playerProgress.Position;
        }


        public void SaveGame(int indicatorPosition)
        {
            playerProgress = new Progress() { Level = actualLevel, Question = actualQuestion, Position = indicatorPosition };
            fileManager.SaveProgressData(playerProgress);
        }


        public void AbortGame()
        {
            SaveEndedGame();
        }
        public void LoseGame()
        {
            // return to checkpoint level
            if (actualLevel >= 10) actualLevel = 10;
            else if (actualLevel >= 5) actualLevel = 5;
            else actualLevel = 0;

            SaveEndedGame();
        }



        public void SaveEndedGame()
        {
            List<Score> scores = fileManager.GetScoresData();

            scores.Add(new Score() {
                Name = "Player 2"/*to be fillable by user input*/,
                Prize = actualLevel != 0 ? (actualLevel != 13 ? prizes[actualLevel - 1] :  prizes[14]) : "0 Kč",
                Date = string.Format("{0:dd.MM.yyyy HH:mm}", DateTime.Now)
            });
            
            fileManager.SaveScoresData(scores);
            // "delete" progress data 
            fileManager.SaveProgressData(new Progress());
        }


    }
}

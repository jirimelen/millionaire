using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace millionaire.props
{
    class FileManager
    {
        private JsonSerializerSettings JsonSettings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects };

        private List<List<Question>> questions = new List<List<Question>>();

        public FileManager()
        {
            string jsonString = String.Empty;

            /*List < List < Question >> listToSave = new List<List<Question>>() {
                new List<Question>() {
                    new Question() {
                        Content = "que1", Answer = "1ans", FakeAnswers = new List<string>(){"1fAns1", "1fAns2", "1fAns3"}
                    }
                },
                new List<Question>() {
                    new Question() {
                        Content = "que2", Answer = "2ans", FakeAnswers = new List<string>(){"2fAns1", "2fAns2", "2fAns3"}
                    }
                },
                new List<Question>() {
                    new Question() {
                        Content = "que3", Answer = "3ans", FakeAnswers = new List<string>(){"3fAns1", "3fAns2", "3fAns3"}
                    }
                },
                new List<Question>() {
                    new Question() {
                        Content = "que4", Answer = "4ans", FakeAnswers = new List<string>(){"4fAns1", "4fAns2", "4fAns3"}
                    }
                },
                new List<Question>() {
                    new Question() {
                        Content = "que5", Answer = "5ans", FakeAnswers = new List<string>(){"5fAns1", "5fAns2", "5fAns3"}
                    }
                },
                new List<Question>() {
                    new Question() {
                        Content = "que6", Answer = "6ans", FakeAnswers = new List<string>(){"6fAns1", "6fAns2", "6fAns3"}
                    }
                },
                new List<Question>() {
                    new Question() {
                        Content = "que7", Answer = "7ans", FakeAnswers = new List<string>(){"7fAns1", "7fAns2", "7fAns3"}
                    }
                },
                new List<Question>() {
                    new Question() {
                        Content = "que8", Answer = "8ans", FakeAnswers = new List<string>(){"8fAns1", "8fAns2", "8fAns3"}
                    }
                },
                new List<Question>() {
                    new Question() {
                        Content = "que9", Answer = "9ans", FakeAnswers = new List<string>(){"9fAns1", "9fAns2", "9fAns3"}
                    }
                },
                new List<Question>() {
                    new Question() {
                        Content = "que10", Answer = "10ans", FakeAnswers = new List<string>(){"10fAns1", "10fAns2", "10fAns3"}
                    }
                },
                new List<Question>() {
                    new Question() {
                        Content = "que11", Answer = "11ans", FakeAnswers = new List<string>(){"11fAns1", "11fAns2", "11fAns3"}
                    }
                },
                new List<Question>() {
                    new Question() {
                        Content = "que12", Answer = "12ans", FakeAnswers = new List<string>(){"12fAns1", "12fAns2", "12fAns3"}
                    }
                },
                new List<Question>() {
                    new Question() {
                        Content = "que13", Answer = "13ans", FakeAnswers = new List<string>(){"13fAns1", "13fAns2", "13fAns3"}
                    }
                },
                new List<Question>() {
                    new Question() {
                        Content = "que14", Answer = "14ans", FakeAnswers = new List<string>(){"14fAns1", "14fAns2", "14fAns3"}
                    }
                },
                new List<Question>() {
                    new Question() {
                        Content = "que15", Answer = "15ans", FakeAnswers = new List<string>(){"15fAns1", "15fAns2", "15fAns3"}
                    }
                }
            };
            
            jsonString = JsonConvert.SerializeObject(listToSave, JsonSettings);
            File.WriteAllText(@"D:\school\millionaire\millionaire\questions.json", jsonString);*/

            jsonString = File.ReadAllText(@"D:\school\millionaire\millionaire\questions.json");
            questions = JsonConvert.DeserializeObject<List<List<Question>>>(jsonString, JsonSettings);
        }
        
        public List<List<Question>> GetQuestionsData()
        {
            return questions;
        }


        public Progress GetProgressData()
        {
            Progress data = new Progress();

            // in case of clearing progress file (manually)
            //SaveProgressData(data);

            string jsonString = File.ReadAllText(@"D:\school\millionaire\millionaire\progress.json");
            data = JsonConvert.DeserializeObject<Progress>(jsonString, JsonSettings);

            return data;
        }

        public void SaveProgressData(Progress data)
        {
            string jsonString = JsonConvert.SerializeObject(data, JsonSettings);
            File.WriteAllText(@"D:\school\millionaire\millionaire\progress.json", jsonString);
        }


        public List<Score> GetScoresData()
        {
            List<Score> data = new List<Score>();

            // in case of clearing score table/file
            // SaveScoresData(data);

            string jsonString = File.ReadAllText(@"D:\school\millionaire\millionaire\scores.json");
            data = JsonConvert.DeserializeObject<List<Score>>(jsonString, JsonSettings);

            return data;
        }

        public void SaveScoresData(List<Score> data)
        {
            string jsonString = JsonConvert.SerializeObject(data, JsonSettings);
            File.WriteAllText(@"D:\school\millionaire\millionaire\scores.json", jsonString);
        }
    }
}

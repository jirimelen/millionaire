using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace millionaire.props
{
    class Question
    {
        public      string  Content     { get; set; }
        public      string  Answer      { get; set; }
        public List<string> FakeAnswers { get; set; }
    }
}

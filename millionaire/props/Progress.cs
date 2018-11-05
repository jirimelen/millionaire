using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace millionaire.props
{
    class Progress
    {
        public Question Question = new Question();
        public int      Level = -1;
        public int      Position;

        public bool IsEmpty()
        {
            return Level == -1 ? true : false;
        }
    }
}

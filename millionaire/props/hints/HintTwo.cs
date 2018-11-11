using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace millionaire.props.hints
{
    class HintTwo : Hint
    {
        Random random = new Random();

        public int getSecondAnsNum(int trueAnswerNum)
        {
            int num = random.Next(0,4);
            return trueAnswerNum != num ? num : getSecondAnsNum(trueAnswerNum);
        }
    }
}

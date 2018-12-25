using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTest.Model.Models
{
    public class Result
    {
        public string testName;
        public int testsTotal;
        public int correctTests = 0;
        public int currentTestNumber = 1;

        public Result(string testName, int testsTotal)
        {
            this.testName = testName;
            this.testsTotal = testsTotal;
        }
    }
}
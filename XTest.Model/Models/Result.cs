using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace XTest.Model.Models
{
    public class Result
    {
        public string testName { get; set; }
        public int testsTotal { get; set; }
        public int correctTests { get; set; }
        public int currentTestNumber { get; set; }
        public string testStat
        {
            get => correctTests + "/" + (currentTestNumber-1);
            set => testStat = value;
        }
        public int mark {
            get => Convert.ToInt32(Math.Floor(correctTests / ((double)testsTotal) * 5));
            set => mark = value;
        }

        public Result(string testName, int testsTotal)
        {
            this.testName = testName;
            this.testsTotal = testsTotal;
            this.correctTests = 0;
            this.currentTestNumber = 1;
        }

        public override string ToString()
        {
            return testName + "\t" + testStat + "\tБалл:" + mark;
        }
    }
}
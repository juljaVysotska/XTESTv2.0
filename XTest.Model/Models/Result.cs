using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace XTest.Model.Models
{
    [Serializable]
    public class Result
    {
        private const string DATE_TIME_FORMAT = "dd.MM.yy HH:mm";

        public string testName { get; private set; }
        public int testsTotal { get; private set; }
        public int correctTests { get; set; }
        private int _currentTestNumber;
        public int currentTestNumber {
            get => _currentTestNumber;
            private set {
                if (value > testsTotal)
                {
                    _endTime = DateTime.Now;
                }
                _currentTestNumber = value;
            }
        }
        public int attempts { get; private set; }
        private DateTime _startTime;
        private DateTime _endTime;
        public string startTime{
            get => _startTime.ToString(DATE_TIME_FORMAT);
            private set
            {
                _startTime = DateTime.Parse(value);
            }
        }
        public string endTime {
            get => _endTime.Equals(new DateTime()) ? "---" : _endTime.ToString(DATE_TIME_FORMAT);
            private set
            {
                _endTime = DateTime.Parse(value);
            }
        }
        public string testStat
        {
            get => correctTests + "/" + (currentTestNumber - 1);
            private set => testStat = value;
        }
        public int mark {
            get => Convert.ToInt32(Math.Floor(correctTests / ((double)testsTotal) * 5));
            private set => mark = value;
        }

        public Result(string testName, int testsTotal)
        {
            this.testName = testName;
            this.testsTotal = testsTotal;
            this.correctTests = 0;
            this.currentTestNumber = 1;
            this._startTime = DateTime.Now;
            this.attempts = 1;
        }

        public void CorrectAnswer()
        {
            correctTests += 1;
            currentTestNumber += 1;
        }

        public void WrongAnswer()
        {
            currentTestNumber += 1;
        }

        public void Reset()
        {
            correctTests = 0;
            currentTestNumber = 1;
            _startTime = DateTime.Now;
            _endTime = new DateTime();
            attempts += 1;
        }

        public override string ToString()
        {
            return testName + "\t" + testStat + "\tБалл:" + mark;
        }
    }
}
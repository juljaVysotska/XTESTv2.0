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
    public class Result : INotifyPropertyChanged
    {
        private const string DATE_TIME_FORMAT = "dd.MM.yy HH:mm";

        private static List<string> toUpdateProperties = new List<string>() { "correctTests", "currentTestNumber", "attempts", "startTime", "endTime", "testStat", "mark", "summary" };
        public string testName { get; private set; }
        public int testsTotal { get; private set; }
        private int _correctTests;
        private int _currentTestNumber;
        private int _attempts;
        private DateTime _startTime;
        private DateTime _endTime;
        public int correctTests {
            get => _correctTests;
            set
            {
                _correctTests = value;
                UpdateProperties();
            }
        }
        public int currentTestNumber {
            get => _currentTestNumber;
            private set {
                if (value > testsTotal)
                {
                    _endTime = DateTime.Now;
                } else if (value == 2)
                {
                    this._startTime = DateTime.Now;
                }
                _currentTestNumber = value;
                UpdateProperties();
            }
        }
        public int attempts {
            get => _attempts;
            private set
            {
                _attempts = value;
                UpdateProperties();
            }
        }
        public string startTime{
            get => _startTime.Equals(new DateTime()) ? "---" : _startTime.ToString(DATE_TIME_FORMAT);
            private set
            {
                _startTime = DateTime.Parse(value);
                UpdateProperties();
            }
        }
        public string endTime {
            get => _endTime.Equals(new DateTime()) ? "---" : _endTime.ToString(DATE_TIME_FORMAT);
            private set
            {
                _endTime = DateTime.Parse(value);
                UpdateProperties();
            }
        }

        public string testStat
        {
            get => correctTests + "/" + (currentTestNumber-1);
            private set => testStat = value;
        }
        public int mark {
            get => Convert.ToInt32(Math.Floor(correctTests / ((double)testsTotal) * 5));
            private set => mark = value;
        }
        public string summary
        {
            get => "Тест: " + (currentTestNumber > testsTotal ? "окончен" : currentTestNumber + "/" + testsTotal) + "\n" + "Правильных ответов: " + correctTests;
            private set { }
        }

        private void UpdateProperties()
        {
            foreach (var s in toUpdateProperties)
            {
                OnPropertyChanged(s);
            }
        }

        public Result(string testName, int testsTotal)
        {
            this.testName = testName;
            this.testsTotal = testsTotal;
            this.correctTests = 0;
            this.currentTestNumber = 1;
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

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public void MapFrom(Result value)
        {
            this._attempts = value.attempts;
            this._correctTests = value.correctTests;
            this._currentTestNumber = value.currentTestNumber;
            this._endTime = value._endTime;
            this._startTime = value._startTime;
            UpdateProperties();
        }
    }
}
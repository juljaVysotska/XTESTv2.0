using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using XTest.Model.Models;
using XTest.Model.Services;

namespace XTest.ViewModel
{
    public class ResultViewModel : INotifyPropertyChanged
    {
        public enum TestType
        {
            Berger,
            ShennonFano,
            BinaryDecimal,
            Ellaes,
            Greya,
            CheckByQ,
            Entropy,
            Iterative,
            Recurent,
            RepeatCode,
            RidMaller,
            Varshamov,
            Abramsona
        }

        public static Dictionary<TestType, Result> results  = new Dictionary<TestType, Result>();

        public Dictionary<TestType, Result> Results
        {
            get => results.Where(r => r.Value.currentTestNumber > 1 || r.Value.attempts > 1).ToDictionary(r => r.Key, r => r.Value);
            set
            {
                results = value;
                OnPropertyChanged("Results");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

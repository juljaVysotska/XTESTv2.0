using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using XTest.Model.Models;
using XTest.Model.Services;
using static XTest.Model.Models.Result;

namespace XTest.ViewModel
{
    public class BergerViewModel : INotifyPropertyChanged
    {
        private RelayCommand next;
        private RelayCommand nextCode;
        private RelayCommand nextDecode;
        private RelayCommand modeCode;
        private RelayCommand modeDecode;

        public string taskExplanation;

        private int _selectedIndex;

        public int SelectedIndex {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                OnPropertyChanged("SelectedIndex");
            }
        }

        private string _task;
        private string _taskCode;
        private string _taskDecode;

        public string task
        {
            get => _task == null ? "" : _task;
            set
            {
                _task = value;
                OnPropertyChanged("task");
            }
        }
        public string taskCode
        {
            get => _taskCode == null ? "" : _taskCode;
            set
            {
                _taskCode = value;
                OnPropertyChanged("taskCode");
            }
        }
        public string taskDecode
        {
            get => _taskDecode == null ? "" : _taskDecode;
            set
            {
                _taskDecode = value;
                OnPropertyChanged("taskDecode");
            }
        }

        public string _taskResult;
        public string _taskCodeResult;
        public string _taskDecodeResult;

        public string taskResult
        {
            get => _taskResult == null ? "" : _taskResult;
            set
            {
                _taskResult = value;
                OnPropertyChanged("taskResult");
            }
        }
        public string taskCodeResult
        {
            get => _taskCodeResult == null ? "" : _taskCodeResult;
            set
            {
                _taskCodeResult = value;
                OnPropertyChanged("taskCodeResult");
            }
        }
        public string taskDecodeResult
        {
            get => _taskDecodeResult == null ? "" : _taskDecodeResult;
            set
            {
                _taskDecodeResult = value;
                OnPropertyChanged("taskDecodeResult");
            }
        }

        public BergerViewModel()
        {
            GenerateBergerTest();
            taskCode = BergerService.generateEncode();
            taskDecode = BergerService.generateDecode();
        }

        public Result result
        {
            get {
                Result res;
                if (!results.ContainsKey(TestType.Berger))
                {
                    res = new Result("Код Бергера", 6);
                    results.Add(TestType.Berger, res);
                } else
                {
                    res = results[TestType.Berger];
                }
                return res; }
            private set {}
        }

        public RelayCommand Next
        {
            get
            {
                return next ??
                  (next = new RelayCommand(obj =>
                  {
                      if (result.currentTestNumber <= 6)
                      {
                          if (result.currentTestNumber <= 3 ?
                              BergerService.isEncodedCorrectly(task, taskResult) :
                              BergerService.isDecodedCorrectly(task, taskResult))
                          {
                              taskResult = "";
                              result.CorrectAnswer();
                          }
                          else
                          {
                              taskResult = "";
                              result.WrongAnswer();
                          }
                      }
                      GenerateBergerTest();
                  }));
            }
        }

        public RelayCommand NextCode
        {
            get
            {
                return nextCode ??
                  (nextCode = new RelayCommand(obj =>
                  {
                      if (BergerService.isEncodedCorrectly(taskCode, taskCodeResult))
                      {
                          MessageBox.Show("Правильно!");
                      } else
                      {
                          MessageBox.Show("Не правильно! Правильный ответ: " + BergerService.encode(taskCode));
                      }
                      taskCode = BergerService.generateEncode();
                  }));
            }
        }

        public RelayCommand NextDecode
        {
            get
            {
                return nextDecode ??
                  (nextDecode = new RelayCommand(obj =>
                  {
                      if (BergerService.isDecodedCorrectly(taskDecode, taskDecodeResult))
                      {
                          MessageBox.Show("Правильно!");
                      }
                      else
                      {
                          MessageBox.Show("Не правильно! Правильный ответ: " + BergerService.decode(taskDecode));
                      }
                      taskDecode = BergerService.generateDecode();
                  }));
            }
        }

        public RelayCommand ModeCode
        {
            get
            {
                return modeCode ??
                  (modeCode = new RelayCommand(obj =>
                  {
                      SelectedIndex = 0;
                  }));
            }
        }

        public RelayCommand ModeDecode
        {
            get
            {
                return modeDecode ??
                  (modeDecode = new RelayCommand(obj =>
                  {
                      SelectedIndex = 1;
                  }));
            }
        }

        private void GenerateBergerTest()
        {
            if (result.currentTestNumber <= 3)
            {
                taskExplanation = "Зашифруйте:";
                task = BergerService.generateEncode();
            }
            else if (result.currentTestNumber > 3 && result.currentTestNumber < 7)
            {
                taskExplanation = "Расшифруйте:";
                task = BergerService.generateDecode();
            }
            else
            {
                if (MessageBox.Show("Правильных ответов " + result.correctTests + " из " + result.testsTotal + ". Хотите попробовать ещё ? ", "Тест окончен", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    result.Reset();
                    GenerateBergerTest();
                }
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

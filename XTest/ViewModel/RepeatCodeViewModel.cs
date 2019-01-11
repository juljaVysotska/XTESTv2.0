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
using static XTest.ViewModel.ResultViewModel;

namespace XTest.ViewModel
{
    public class RepeatCodeViewModel : INotifyPropertyChanged
    {
        RepeatCode repeatCode;
        int mark = 0;
        int check = 1;
        string code;
        string coded;
        string answer;

        public string Code
        {
            get { return code; }
            set
            {
                code = value;
                OnPropertyChanged("Code");
            }
        }

        public string Coded
        {
            get { return coded; }
            set
            {
                coded = value;
                OnPropertyChanged("Coded");
            }
        }

        public string Answer
        {
            get { return answer; }
            set
            {
                answer = value;
                OnPropertyChanged("Answer");
            }
        }

        RelayCommand checkCode;
        RelayCommand next;
        RelayCommand checkDecode;

        public RepeatCodeViewModel()
        {
            repeatCode = new RepeatCode();
            Code = repeatCode.GenerateCode();
            Coded = repeatCode.Code();
        }

        public RelayCommand Next
        {
            get
            {
                return next ??
                      (next = new RelayCommand(obj =>
                      {
                          if (check < 4)
                          {
                              if (repeatCode.CorrectCode(coded, answer))
                              {
                                  MessageBox.Show("Correct!");
                                  mark++;
                              }
                              else
                              {
                                  MessageBox.Show("Wrong!");
                              }
                              Upload();
                          }
                          else if (check == 4)
                          {
                              if (repeatCode.CorrectCode(coded, answer))
                              {
                                  MessageBox.Show("Correct!");
                                  mark++;
                              }
                              else
                              {
                                  MessageBox.Show("Wrong!");
                              }
                              Upload();
                              //MainWindow.TestQ_control.SelectedIndex++;
                          }
                          else if (check >= 4)
                          {
                              if (repeatCode.CorrectCode(code, answer))
                              {
                                  MessageBox.Show("Correct!");
                                  mark++;
                              }
                              else
                              {
                                  MessageBox.Show("Wrong!");
                              }
                              Upload();
                          }
                          check++;
                          if (check == 9)
                          {
                              MessageBox.Show("Правильных ответов " + mark.ToString() + " из 8");
                              results.Add(TestType.RepeatCode, new Result("Кодирование простым повторением ", mark));
                              Upload();
                              check = 1;
                              mark = 0;
                          }
                      }));
            }
        }

        public void Upload()
        {
            Code = repeatCode.GenerateCode();
            Coded = repeatCode.Code();
            Answer = "";
        }

        public RelayCommand CheckCode
        {
            get
            {
                return checkCode ??
                    (checkCode = new RelayCommand(obj =>
                    {
                        if (repeatCode.CorrectCode(coded, answer))
                        {
                            MessageBox.Show("Correct!");
                        }
                        else
                        {
                            MessageBox.Show("Wrong!");
                        }
                        Upload();
                    }));
            }
        }

        public RelayCommand CheckDecode
        {
            get
            {
                return checkDecode ??
                    (checkDecode = new RelayCommand(obj =>
                    {
                        if (repeatCode.CorrectCode(code, answer))
                        {
                            MessageBox.Show("Correct!");
                        }
                        else
                        {
                            MessageBox.Show("Wrong!");
                        }
                        Upload();
                    }));
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

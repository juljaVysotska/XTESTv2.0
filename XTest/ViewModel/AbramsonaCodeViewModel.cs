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
using static XTest.MainWindow;

namespace XTest.ViewModel
{
    class AbramsonaCodeViewModel : INotifyPropertyChanged
    {
        AbramsonaCode abramsonaCode;
        int mark = 0;
        int check = 1;
        string code;
        string coded;
        string answer;
        int selectedIndex;

        public int Mark
        {
            get { return mark; }
            set
            {
                mark = value;
                OnPropertyChanged("Mark");    // ОЦенка
            }
        }

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

        public AbramsonaCodeViewModel()
        {
            abramsonaCode = new AbramsonaCode();
            Code = abramsonaCode.Code();
            Coded = abramsonaCode.coded;
            SelectedIndex = 0;
        }

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                OnPropertyChanged("SelectedIndex");
            }
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
                              if (abramsonaCode.CorrectCode(coded, answer))
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
                              if (abramsonaCode.CorrectCode(coded, answer))
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
                              if (abramsonaCode.CorrectCode(code, answer))
                              {
                                  MessageBox.Show("Correct!");
                                  mark++;
                              }
                              else
                              {
                                  MessageBox.Show("Wrong!");
                              }
                              Upload();
                              SelectedIndex = 1;
                          }
                          check++;
                          if (check == 9)
                          {
                              MessageBox.Show("Правильных ответов " + mark.ToString() + " из 8");
                              MainWindow.results.Add(TestType.Abramsona, new Result("Кодирование простым повторением ", mark));
                              Upload();
                              check = 1;
                              mark = 0;
                              SelectedIndex = 0;
                          }
                      }));
            }
        }

        public string Poly
        {
            get { return abramsonaCode.neprPol; }
            set
            {
                abramsonaCode.neprPol = value;
                OnPropertyChanged("Poly");
            }
        }

        public void Upload()
        {
            Code = abramsonaCode.Code();
            Coded = abramsonaCode.coded;
            Answer = "";
        }

        public RelayCommand CheckCode
        {
            get
            {
                return checkCode ??
                    (checkCode = new RelayCommand(obj =>
                    {
                        if (abramsonaCode.CorrectCode(coded, answer))
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
                        if (abramsonaCode.CorrectCode(code, answer))
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

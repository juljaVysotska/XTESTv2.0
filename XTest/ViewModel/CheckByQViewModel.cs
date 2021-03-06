﻿using System;
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
    public class CheckByQViewModel : INotifyPropertyChanged
    {
        CheckByQ checkByQ;
        int q;
        int n;
        int[] code;
        string coded;
        RelayCommand checkCode;
        RelayCommand checkDecode;
        RelayCommand next;
        int mark = 0;
        int check = 1;
        int selectedIndex;

        public Result result
        {
            get
            {
                Result res;
                if (!results.ContainsKey(TestType.CheckByQ))
                {
                    res = new Result("Код с проверкой по q", 8);
                    results.Add(TestType.CheckByQ, res);
                }
                else
                {
                    res = results[TestType.CheckByQ];
                }
                return res;
            }
            private set { }
        }

        int[] decode;
        bool yes;
        bool correct;

        public int Mark
        {
            get { return result.mark; }
            set
            {
                mark = value;
                OnPropertyChanged("Mark");    // ОЦенка
            }
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

        public bool IsSuccess
        {
            get { return yes; }
            set
            {
                yes = value;
                OnPropertyChanged("IsSuccess");
            }
        }

        public RelayCommand CheckCode
        {
            get
            {
                return checkCode ??
                    (checkCode = new RelayCommand(obj =>
                    {
                        if (checkByQ.CheckCoding(code, q, IntToArr(coded)))
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

        void Upload()
        {
            Code = Output(checkByQ.GenerateCode());
            Q = checkByQ.q;
            N = checkByQ.n;
            Decode = Output(checkByQ.Code(code, q));
            Random rand = new Random();
            if (rand.Next(0, 100) < 50)
            {
                correct = true;
            }
            else
            {
                decode = checkByQ.WrongCode(decode);
                correct = false;
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
                              if (checkByQ.CheckCoding(code, q, IntToArr(coded)))
                              {
                                  MessageBox.Show("Correct!");
                                  result.CorrectAnswer();
                                  mark++;
                              }
                              else
                              {
                                  MessageBox.Show("Wrong!");
                                  result.WrongAnswer();
                              }
                              Upload();
                          }
                          else if (check == 4)
                          {
                              if (checkByQ.CheckCoding(code, q, IntToArr(coded)))
                              {
                                  MessageBox.Show("Correct!");
                                  result.CorrectAnswer();
                                  mark++;
                              }
                              else
                              {
                                  MessageBox.Show("Wrong!");
                                  result.WrongAnswer();
                              }
                              SelectedIndex = 1;
                              Upload();
                          }
                          else if (check >= 4)
                          {
                              if (correct == yes)
                              {
                                  MessageBox.Show("Correct!");
                                  result.CorrectAnswer();
                                  mark++;
                              }
                              else
                              {
                                  MessageBox.Show("Wrong!");
                                  result.WrongAnswer();
                              }
                              Upload();
                          }
                          check++;
                          if (check == 9)
                          {
                              if (MessageBox.Show("Правильных ответов " + mark.ToString() + " из 8. Хотите попробовать ещё ? ", "Тест окончен", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                              {
                                  result.Reset();
                              }
                              //MainWindow.results.Add(TestType.CheckByQ, new Result("Проверка по мoдулю q ", mark));
                              SelectedIndex = 0;
                              Upload();
                              check = 1;
                              mark = 0;
                          }
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
                        if (correct == yes)
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

        public int Q
        {
            get
            { return q; }
            set
            {
                q = value;
                OnPropertyChanged("Q");
            }
        }

        public int N
        {
            get
            { return n; }
            set
            {
                n = value;
                OnPropertyChanged("N");
            }
        }

        public string Code
        {
            get
            {
                return Output(code);
            }
            set
            {
                code = IntToArr(value);
                OnPropertyChanged("Code");
            }
        }

        public string Output(int[] arr)
        {
            var str = "";
            foreach (var item in arr)
            {
                str += item;
            }
            return str;
        }

        public int[] IntToArr(string answer)
        {
            int[] array = new int[answer.Length];
            char[] charr = answer.ToArray<char>();
            for (int i = 0; i < answer.Length; i++)
            {
                array[i] = Int32.Parse(charr[i].ToString());
            }
            return array;
        }

        public string Coded
        {
            get
            {
                return "Input answer";
            }
            set
            {
                coded = value;
                OnPropertyChanged("Answer");
            }
        }

        public CheckByQViewModel()
        {
            SelectedIndex = 0;
            checkByQ = new CheckByQ();
            code = checkByQ.GenerateCode();
            Q = checkByQ.q;
            N = checkByQ.n;
            decode = checkByQ.Code(code, q);
            Random rand = new Random();
            if (rand.Next(0, 100) < 50)
            {
                correct = true;
            }
            else
            {
                decode = checkByQ.WrongCode(decode);
                correct = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public string Decode
        {
            get
            {
                return Output(decode);
            }
            set
            {
                decode = IntToArr(value);
                OnPropertyChanged("Decode");
            }
        }
    }
}
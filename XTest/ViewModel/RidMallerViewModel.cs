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
    public class RidMallerViewModel : INotifyPropertyChanged
    {
        public RidMallerService _service { get; set; }
        public int[][] array;
        public int[][] arrayCode;
        public int[][] arrayDecode;
        private int check { get; set; }
        private int mark { get; set; }
        private int[][] OldArray { get; set; }
        private int[][] OldArrayCode { get; set; }
        private int[][] OldArrayDecode { get; set; }
        private int selectedIndex;
        

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                OnPropertyChanged("SelectedIndex");
            }
        }

        public int[][] Array
        {
            get { return array; }
            set
            {
                array = value;
                OnPropertyChanged("Array");
            }
        }

        public int[][] ArrayCode
        {
            get { return arrayCode; }
            set
            {
                arrayCode = value;
                OnPropertyChanged("ArrayCode");
            }
        }

        public int[][] ArrayDecode
        {
            get { return arrayDecode; }
            set
            {
                arrayDecode = value;
                OnPropertyChanged("ArrayDecode");
            }
        }

        private string output;
        public string Output
        {
            get { return output; }
            set
            {
                output = value;
                OnPropertyChanged("Output");
            }
        }

        public int Mark
        {
            get
            {
                return result.mark;
            }
            
        }

        public Result result
        {
            get
            {
                Result res;
                if (!results.ContainsKey(TestType.RidMaller))
                {
                    res = new Result("Код Рида-Маллера", 6);
                    results.Add(TestType.RidMaller, res);
                }
                else
                {
                    res = results[TestType.RidMaller];
                }
                return res;
            }
            private set { }
        }

        public int Check
        {          
            get
            {
                return result.currentTestNumber;
            }
            
        }
 
        public RidMallerViewModel()
        {
            var ridMaller = new RidMallerService();
            this._service = ridMaller;
            Array = ridMaller.RandomMessage(ridMaller.GenerateArray());
            OldArray = ridMaller.FuckenCSharp(Array);
            ArrayCode = ridMaller.RandomMessage(ridMaller.GenerateArray());
            OldArrayCode = ridMaller.FuckenCSharp(ArrayCode);
            ArrayDecode = _service.RandomMessageDecode(_service.GenerateArray());
            OldArrayDecode = _service.FuckenCSharp(ArrayDecode);
            
        }


        private RelayCommand next;
        private RelayCommand nextCode;
        private RelayCommand nextDecode;


        public RelayCommand Next
        {
            get
            {
                return next ??
                  (next = new RelayCommand(obj =>
                  {
                      if (Check < 3)
                      {
                          OldArray = _service.Code(OldArray);
                          if (_service.Equals(OldArray, Array))
                              result.CorrectAnswer();
                          else
                              result.WrongAnswer();

                          Array = _service.RandomMessage(_service.GenerateArray());
                          OldArray = _service.FuckenCSharp(Array);

                      }
                      else if (Check == 3)
                      {
                        SelectedIndex = 1;
                          OldArray = _service.Code(OldArray);
                          if (_service.Equals(OldArray, Array))
                              result.CorrectAnswer();
                          else
                              result.WrongAnswer();
                          
                          Array = _service.RandomMessageDecode(_service.GenerateArray());
                          OldArray = _service.FuckenCSharp(Array);
                      }
                      else if (Check <= 6)
                      {
                          OldArray = _service.Decode(OldArray);
                              if (_service.Equals(OldArray, Array))
                                  result.CorrectAnswer();
                              else
                                  result.WrongAnswer();
                          Array = _service.RandomMessageDecode(_service.GenerateArray());
                          OldArray = _service.FuckenCSharp(Array);
                      }

                      if (Check >= 7)
                      {
                          if (MessageBox.Show("Правильных ответов " + Mark.ToString() + " из 6. Хотите попробовать ещё ? ", "Тест окончен", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                          result.Reset();
                          Array = _service.RandomMessage(_service.GenerateArray());
                          OldArray = _service.FuckenCSharp(Array);
                         
                      }
                  }
                  ));
            }
        }


        public RelayCommand NextCode
        {
            get
            {
                return nextCode ??
                  (nextCode = new RelayCommand(obj =>
                  {
                      OldArrayCode = _service.Code(OldArrayCode);
                      if (_service.Equals(OldArrayCode, ArrayCode))
                          MessageBox.Show("Правильно!");
                      else
                          MessageBox.Show("Неправильно!");

                      ArrayCode = _service.RandomMessage(_service.GenerateArray());
                      OldArrayCode = _service.FuckenCSharp(ArrayCode);
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
                      OldArrayDecode = _service.Decode(OldArrayDecode);
                      if (_service.Equals(OldArrayDecode, ArrayDecode))
                          MessageBox.Show("Правильно!");
                      else
                          MessageBox.Show("Неправильно!");

                      ArrayDecode = _service.RandomMessageDecode(_service.GenerateArray());
                      OldArrayDecode = _service.FuckenCSharp(ArrayDecode);
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

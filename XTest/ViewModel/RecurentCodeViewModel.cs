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
using static XTest.ViewModel.ResultViewModel;

namespace XTest.ViewModel
{
    public class RecurentCodeViewModel : INotifyPropertyChanged
    {
        private RelayCommand next;
        private RelayCommand code;
        private RelayCommand decode;
        private int check { get; set; }
        private int mark { get; set; }
        private int selectedIndex;
        private int k { get; set; }
        private int kCode { get; set; }
        private int kDecode { get; set; }
        private RecurentCode _service { get; set; }
        private string array;
        private string decodeArray;
        private string oldArray;
        private int[] arr;
        private int[][] decodeArr;
        private string arrayCode;
        private string oldArrayCode;
        private int[] arrCode;
        private int[][] arrayForDecode;
        private string arrayDecode;
        private string oldArrayDecode;
        private string dArray;

        Random rand = new Random();

        public string Array
        {
            get { return array; }
            set
            {
                array = value;
                OnPropertyChanged("Array");
            }
        }
        public string DArray
        {
            get { return dArray; }
            set
            {
                dArray = value;
                OnPropertyChanged("DArray");
            }
        }
        public string ArrayDecode
        {
            get { return arrayDecode; }
            set
            {
                arrayDecode = value;
                OnPropertyChanged("ArrayDecode");
            }
        }


        public string ArrayCode
        {
            get { return arrayCode; }
            set
            {
                arrayCode = value;
                OnPropertyChanged("ArrayCode");
            }
        }
        public string DecodeArray
        {
            get { return decodeArray; }
            set
            {
                decodeArray = value;
                OnPropertyChanged("DecodeArray");
            }
        }


        public string OldArray
        {
            get { return oldArray; }
            set
            {
                oldArray = value;
                OnPropertyChanged("OldArray");
            }
        }

        public string OldArrayDecode
        {
            get { return oldArrayDecode; }
            set
            {
                oldArrayDecode = value;
                OnPropertyChanged("OldArrayDecode");
            }
        }

        public string OldArrayCode
        {
            get { return oldArrayCode; }
            set
            {
                oldArrayCode = value;
                OnPropertyChanged("OldArrayCode");
            }
        }
        public int K
        {
            get { return k; }
            set
            {
                k = value;
                OnPropertyChanged("K");
            }
        }

        public int KCode
        {
            get { return kCode; }
            set
            {
                kCode = value;
                OnPropertyChanged("KCode");
            }
        }


        public int KDecode
        {
            get { return kDecode; }
            set
            {
                kDecode = value;
                OnPropertyChanged("KDecode");
            }
        }
        public Result result
        {
            get
            {
                Result res;
                if (!results.ContainsKey(TestType.Recurent))
                {
                    res = new Result("Рекурентный код", 6);
                    results.Add(TestType.Recurent, res);
                }
                else
                {
                    res = results[TestType.Recurent];
                }
                return res;
            }
            private set { }
        }
        public int Mark
        {
            get { return result.mark; }
           
        }

        public int Check
        {
            get { return result.currentTestNumber; }
            
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

        


        public RecurentCodeViewModel()
        {
            _service = new RecurentCode();
            SelectedIndex = 0;
            k = rand.Next(1, 5);
            kCode = rand.Next(1, 5);
            kDecode = rand.Next(1, 5);
            arr = _service.GenerateArray(k);
            arrCode = _service.GenerateArray(kCode);
            oldArray = _service.Output(arr);
            OldArrayCode = _service.Output(arrCode);
            array = "";
            arrayCode = "";
            arrayForDecode = _service.GenerateForDecode(k);
            DArray = _service.Output(arrayForDecode[1]);
            OldArrayDecode = _service.Output(arrayForDecode[0]);
            ArrayDecode = "";
        }

        public RelayCommand Code
        {
            get
            {
                return code ??
                    (code = new RelayCommand(obj =>
                    {
                        var a = _service.Code(arrCode, kCode);
                        if (arrayCode == _service.Output(a))
                            MessageBox.Show("Правильно!");
                        else
                            MessageBox.Show("Неправильно!");
                        KCode = rand.Next(1, 5);
                        arrCode = _service.GenerateArray(kCode);
                        OldArrayCode = _service.Output(arrCode);
                        ArrayCode = "";
                    }));
            }
        }
        public RelayCommand Decode
        {
            get
            {
                return decode ??
                    (decode = new RelayCommand(obj =>
                    {
                        var a = _service.Decode(arrayForDecode, kDecode);
                        if (arrayDecode == _service.Output(a))
                            MessageBox.Show("Правильно!");
                        else
                            MessageBox.Show("Неправильно!");

                        KDecode = rand.Next(1, 5);
                        arrayForDecode = _service.GenerateForDecode(k);
                        DArray = _service.Output(arrayForDecode[1]);
                        OldArrayDecode = _service.Output(arrayForDecode[0]);
                        ArrayDecode = "";
                    }));
            }
        }
        public RelayCommand Next
        {
            get
            {
                return next ??
                  (next = new RelayCommand(obj =>
                  {
                      if (Check < 3)
                      {
                          var a = _service.Code(arr, k);
                          if (array == _service.Output(a))
                              result.CorrectAnswer();
                          else
                              result.WrongAnswer();
                          K = rand.Next(1, 5);
                          arr = _service.GenerateArray(k);
                          OldArray = _service.Output(arr);
                          Array = "";

                      }
                      else if (Check == 3)
                      {
                          
                          var a = _service.Code(arr, k);
                          if (array == _service.Output(a))
                              result.CorrectAnswer();
                          else
                              result.WrongAnswer();
                          K = rand.Next(1, 5);
                         
                          decodeArr = _service.GenerateForDecode(k);
                          DecodeArray = _service.Output(decodeArr[1]);
                          OldArray = _service.Output(decodeArr[0]);
                          Array = "";
                          SelectedIndex = 1;

                      }
                      else if (Check <= 6)
                      {
                          var a = _service.Decode(decodeArr, k);
                          if (array == _service.Output(a))
                              result.CorrectAnswer();
                          else
                              result.WrongAnswer();
                          K = rand.Next(1, 5);
                          decodeArr = _service.GenerateForDecode(k);
                          DecodeArray = _service.Output(decodeArr[1]);
                          OldArray = _service.Output(decodeArr[0]);
                          Array = "";
                      }

                      
                      if (Check >= 7)
                      {

                          if (MessageBox.Show("Правильных ответов " + Mark.ToString() + " из 6. Хотите попробовать ещё ? ", "Тест окончен", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                          {
                              result.Reset();
                              K = rand.Next(1, 5);
                              arr = _service.GenerateArray(k);
                              OldArray = _service.Output(arr);
                              Array = "";
                          }
                      }
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using XTest.Model.Services;

namespace XTest.ViewModel
{
    public class RecurentCodeViewModel : INotifyPropertyChanged
    {
        private RelayCommand next;
        private int check { get; set; }
        private int mark { get; set; }
        private int selectedIndex;
        private int k { get; set; }
        private RecurentCode _service { get; set; }
        private string array;
        private string decodeArray;
        private string oldArray;
        private int[] arr;
        private int[][] decodeArr;

       
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


        public int K
        {
            get { return k; }
            set
            {
                k = value;
                OnPropertyChanged("K");
            }
        }

        public int Mark
        {
            get { return mark; }
            set
            {
                mark = value;
                OnPropertyChanged("Mark");
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

        public RecurentCodeViewModel()
        {
            _service = new RecurentCode();
            SelectedIndex = 0;
            check = 1;
            Mark = 0;
            k = rand.Next(1, 5);
            arr = _service.GenerateArray(k);
            oldArray = _service.Output(arr);
            array = "";
        }

        public RelayCommand Next
        {
            get
            {
                return next ??
                  (next = new RelayCommand(obj =>
                  {
                      if (check < 3)
                      {
                          var a = _service.Code(arr, k);
                          if (array == _service.Output(a))
                              Mark += 1;
                         K = rand.Next(1, 5);
                          arr = _service.GenerateArray(k);
                          OldArray = _service.Output(arr);
                          Array = "";

                      }
                      else if (check == 3)
                      {
                          MessageBox.Show("decode");
                          var a = _service.Code(arr, k);
                          if (array == _service.Output(a))
                              Mark += 1;
                          K = rand.Next(1, 5);
                         
                          decodeArr = _service.GenerateForDecode(k);
                          DecodeArray = _service.Output(decodeArr[1]);
                          OldArray = _service.Output(decodeArr[0]);
                          Array = "";
                          SelectedIndex = 1;

                      }
                      else
                      {
                          var a = _service.Decode(decodeArr, k);
                          if (array == _service.Output(a))
                              Mark += 1;
                          K = rand.Next(1, 5);
                          decodeArr = _service.GenerateForDecode(k);
                          DecodeArray = _service.Output(decodeArr[1]);
                          OldArray = _service.Output(decodeArr[0]);
                          Array = "";
                      }
                      check += 1;
                      if (check == 7)
                      {
                          MessageBox.Show(Mark.ToString());
                          check = 1;
                          Mark = 0;
                          K = rand.Next(1, 5);
                          arr = _service.GenerateArray(k);
                          OldArray = _service.Output(arr);
                          Array = "";
                          SelectedIndex = 0;
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

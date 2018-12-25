using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using XTest.Model.Services;
using XTest.Model.Models;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XTest.View
{
    /// <summary>
    /// Логика взаимодействия для Test.xaml
    /// </summary>
    public partial class Test : Window
    {


        public Test()
        {
            var a = new TestVm(new EllaesCodeService(), new EllaesCode());
            InitializeComponent();
            DataContext = a;
        }
    }

    public class TestVm : INotifyPropertyChanged
    {
        private RelayCommand next;
        private int check { get; set; }
        private int mark { get; set; }
        private EllaesCodeService _service { get; set; }
        public EllaesCode EllaesCode { get; set; }
        public int[][] array;
        private int[][] OldArray { get; set; }

        public int[][] Array {
            get { return array; }
            set
            {
                array = value;
                OnPropertyChanged("Array");
            }
        }


        public TestVm(EllaesCodeService ellaesCodeService, EllaesCode ellaesCode)
        {
            check = 1;
            mark = 0;
            this.EllaesCode = ellaesCode;
            this._service = ellaesCodeService;
            Array = _service.GenerateTestArray(3, 3);
            OldArray = Array;
            
        }

        public RelayCommand Next
        {
            get
            {
                return next ??
                  (next = new RelayCommand(obj =>
                  {
                      if(check < 5)
                      {
                          if (Array == _service.Code(OldArray))
                              mark += 1;
                          Array = _service.GenerateTestArray(3, 3);
                          OldArray = Array;
                      }
                      else if(check == 5)
                      {
                          if (Array == _service.Decode(OldArray))
                              mark += 1;
                          Array = _service.GenerateArrayWithException(3, 3);
                          OldArray = Array;
                      }
                      else
                      {
                      if (Array == _service.Decode(OldArray))
                              mark += 1;
                          Array = _service.GenerateArrayWithException(3, 3);
                          OldArray = Array;
                      }
                      check += 1;
                      if(check == 9)
                      {
                          MessageBox.Show("Your mark {0}", mark.ToString());
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

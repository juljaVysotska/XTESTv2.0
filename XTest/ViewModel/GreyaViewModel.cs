using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using XTest.Model.Models;
using XTest.Model.Services;

namespace XTest.ViewModel
{
    class GreyaViewModel : INotifyPropertyChanged
    {
        private GreyaCodeService codeService = new GreyaCodeService();

        private GreyaCode greyaCodeTest;
        private GreyaCode greyaCodePractice;

        private int selectedIndex;
        private int testNumber;
        private int correctAnsver;
        private TestMode testMode;

        private RelayCommand nextTest;

        public GreyaCode GreyaCodeTest
        {
            get { return greyaCodeTest; }
            set
            {
                greyaCodeTest = value;
                OnPropertyChanged("GreyaCodeTest");
            }
        }

        public GreyaCode GreyaCodePractice
        {
            get { return greyaCodePractice; }
            set
            {
                greyaCodePractice = value;
                OnPropertyChanged("GreyaCodePractice");
            }
        }

        public int GreyaSelectedTabIndex
        {
            get
            {
                return selectedIndex;
            }
            set
            {
                testNumber = 1;
                correctAnsver = 0;
                OnPropertyChanged("GreyaSelectedTabIndex");
            }
        }

        public RelayCommand NextTest
        {
            get
            {
                return nextTest ??
                    (nextTest = new RelayCommand(obj =>
                    {
                        if (testNumber > 5)
                        {
                            testMode = TestMode.Decoding;
                        }
                        if (testMode == TestMode.Encoding)
                        {
                            correctAnsver += GreyaCodeTest.Result.Equals(codeService.encode(GreyaCodeTest.Message)) ? 1 : 0;
                        }
                        else if (testMode == TestMode.Decoding)
                        {
                            correctAnsver += GreyaCodeTest.Result.Equals(codeService.decode(GreyaCodeTest.Message)) ? 1 : 0;
                        }
                        testNumber++;
                    }));
            }
        }

                    public GreyaViewModel()
        {
            testNumber = 1;
            correctAnsver = 0;
            testMode = TestMode.Encoding;
            greyaCodeTest = new GreyaCode();
            greyaCodeTest.Message = codeService.generateLine(11);
            greyaCodePractice = new GreyaCode();
            greyaCodePractice.Message = codeService.generateLine(11);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

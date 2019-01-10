using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace XTest.ViewModel
{
    public class MainWindowViewModel
    {
		public GreyaViewModel greyavm { get; set; }
		public BinaryDecimalViewModel bdvm { get; set; }
        EllaesCodeViewModel viewModel { get; set; }
        BergerViewModel bergerViewModel { get; set; }
        RidMallerViewModel mallerViewModel { get; set; }
        CheckByQViewModel checkByQ { get; set; }
        RecurentCodeViewModel recurentVM { get; set; }
        VarshamovCodeViewModel varshamVM { get; set; }
        VarshamovCodeViewModel varshamVWPractice { get; set; }

    }
}
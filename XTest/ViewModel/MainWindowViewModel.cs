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
        EllaesCodeViewModel viewModel { get; set; }
        RidMallerViewModel mallerViewModel { get; set; }
        CheckByQViewModel checkByQ { get; set; }
    }
}
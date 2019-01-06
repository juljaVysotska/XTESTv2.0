using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using XTest.Model.Models;
using XTest.ViewModel;

namespace XTest
{
   
    public partial class MainWindow : Window
    {
        
        public static Dictionary<string, Result> results = new Dictionary<string, Result>();

        public MainWindow()
        {
            InitializeComponent();
          
        }

        

     

        private void codeRM_btn_Click(object sender, RoutedEventArgs e)
        {
            codeRM_control.SelectedIndex = 0;
        }

        private void decodeRM_btn_Click(object sender, RoutedEventArgs e)
        {
            codeRM_control.SelectedIndex = 1;
        }

        private void codeEL_btn_Click(object sender, RoutedEventArgs e)
        {
            codeEL_control.SelectedIndex = 0;
        }

        private void decodeEL_btn_Click(object sender, RoutedEventArgs e)
        {
            codeEL_control.SelectedIndex = 1;
        }
        int check1 = 0;
        private void nextVAR_btn_Click(object sender, RoutedEventArgs e)
        {
            check1++;
            TestVAR_control.SelectedIndex++;
            if (check1 == 5) {
                TestVAR_control.SelectedIndex = 0;
            }
        }

        private void codeR_btn_Click(object sender, RoutedEventArgs e)
        {
            codeR_control.SelectedIndex = 0;
        }

        private void decodeR_btn_Click(object sender, RoutedEventArgs e)
        {
            codeR_control.SelectedIndex = 1;
        }


        private void WrapPanel_TextInput(object sender, TextCompositionEventArgs e)
        {

        }
        
        private void nextVARPractice_btn_Click(object sender, RoutedEventArgs e)
        {
            check1++;
            PracticeVAR_control.SelectedIndex++;
            if (check1 == 5)
            {
                //check = 0;
            }
        }
    }
}
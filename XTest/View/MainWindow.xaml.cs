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

        

        private void TabControl_Berger_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabControl tabControl = (TabControl)sender;
            if (tabControl.SelectedIndex == 2)
            {

            }
        }
      
        private void code_btn_Click(object sender, RoutedEventArgs e)
        {
            codeVAR_control.SelectedIndex = 0;
        }

       
        private void decode_btn_Click(object sender, RoutedEventArgs e)
        {
            codeVAR_control.SelectedIndex = 1;
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

        private void nextVAR_btn_Click(object sender, RoutedEventArgs e)
        {
            
                TestVAR_control.SelectedIndex++;
        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TbHem_TextChanged(object sender, TextChangedEventArgs e)
        {

        }




        string correctAnswer;
        private void HemmingPractice_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
            string taskNumber = "";
            Random rnd = new Random();
            int a = rnd.Next(6) + 9;
            Random rand = new Random();

            for (int i = 0; i <= a; i++) {
                taskNumber += rand.Next(2);

            }
            lblHemtask.Content = "Закодируйте сообщение: " + taskNumber;
            MessageBox.Show(Model.Services.HemmingCodeService.Main(taskNumber));
            correctAnswer = Model.Services.HemmingCodeService.Main(taskNumber);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tbHem.Text == correctAnswer)
            {
             MessageBox.Show("Правильно");

                string taskNumber = "";
                Random rnd = new Random();
                int a = rnd.Next(6) + 9;
                Random rand = new Random();

                for (int i = 0; i <= a; i++)
                {
                    taskNumber += rand.Next(2);

                }
                lblHemtask.Content = "Закодируйте сообщение: " + taskNumber;
                MessageBox.Show(Model.Services.HemmingCodeService.Main(taskNumber));
                correctAnswer = Model.Services.HemmingCodeService.Main(taskNumber);
            }
            else {
                MessageBox.Show("Неправильно");
            }



        }
    }
}
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
using XTest.Model.Services;

namespace XTest
{
   
    public partial class MainWindow : Window
    {
        
        public static Dictionary<string, Result> results = new Dictionary<string, Result>();

        public MainWindow()
        {
            InitializeComponent();
          
        }

        bool bergerPracticeEncode = true;
        private void TabControl_Berger_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!results.ContainsKey("Berger"))
            {
                results.Add("Berger", new Result("Berger", 6));
            }
            TabControl tabControl = (TabControl)sender;
            if (tabControl.SelectedIndex == 2)
            {
                GenerateBergerTest();
            } else if (tabControl.SelectedIndex == 1)
            {
                GenerateBergerPractice();
            }
        }

        private void GenerateBergerPractice()
        {
            if (bergerPracticeEncode)
            {
                lblBergerTaskExplanation_Practice.Content = "Зашифруйте:";
                lblTask_Practice.Content = Berger.generateEncode();
            }
            else
            {
                lblBergerTaskExplanation_Practice.Content = "Расшифруйте:";
                lblTask_Practice.Content = Berger.generateDecode();
            }
        }

        private void GenerateBergerTest()
        {
            Result result = results["Berger"];
            if (result.currentTestNumber <= 3)
            {
                lblBergerTaskExplanation.Content = "Зашифруйте:";
                lblTask.Content = Berger.generateEncode();
            }
            else if (result.currentTestNumber > 3 && result.currentTestNumber < 7)
            {
                lblBergerTaskExplanation.Content = "Расшифруйте:";
                lblTask.Content = Berger.generateDecode();
            }
            else
            {
                MessageBox.Show("Вы уже закончили этот тест!");
            }
        }

        private void Button_Berger_Next_Click(object sender, RoutedEventArgs e)
        {
            Result result = results["Berger"];
            if (result.currentTestNumber <= 6)
            {
                if (result.currentTestNumber <= 3 ?
                    Berger.isEncodedCorrectly(lblTask.Content.ToString(), txbBergerResult.Text) :
                    Berger.isDecodedCorrectly(lblTask.Content.ToString(), txbBergerResult.Text))
                {
                    MessageBox.Show("Правильно!");
                    result.correctTests += 1;
                    result.currentTestNumber += 1;
                }
                else
                {
                    MessageBox.Show("Не правильно! Ответ: " + (result.currentTestNumber <= 3 ?
                        Berger.encode(lblTask.Content.ToString()) :
                        Berger.decode(lblTask.Content.ToString())));
                    result.currentTestNumber += 1;
                }
                GenerateBergerTest();
            }
        }

        private void code_Berger_btn_Click(object sender, RoutedEventArgs e)
        {
            bergerPracticeEncode = true;
            GenerateBergerPractice();
        }

        private void decode_Berger_btn_Click(object sender, RoutedEventArgs e)
        {
            bergerPracticeEncode = false;
            GenerateBergerPractice();
        }

        private void ButtonBergerNext_Practice_Click(object sender, RoutedEventArgs e)
        {
            if (bergerPracticeEncode ?
                    Berger.isEncodedCorrectly(lblTask_Practice.Content.ToString(), txbBergerResult_Practice.Text) :
                    Berger.isDecodedCorrectly(lblTask_Practice.Content.ToString(), txbBergerResult_Practice.Text))
            {
                MessageBox.Show("Правильно!");
            } else
            {
                MessageBox.Show("Не правильно! Ответ: " + (bergerPracticeEncode ?
                    Berger.encode(lblTask_Practice.Content.ToString()) :
                    Berger.decode(lblTask_Practice.Content.ToString())));
            }
            GenerateBergerPractice();
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
    }
}
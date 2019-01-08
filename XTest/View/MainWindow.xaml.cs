﻿using System;
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
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XTest
{

    public partial class MainWindow : Window
    {
        int Qstage = 0;
        int Pstage = 0;
        int Astage = 0;
        public static Dictionary<string, Result> results = new Dictionary<string, Result>();

        public MainWindow()
        {
            InitializeComponent();
            Resources["ResultMarks"] = results;
        }

        #region Berger
        bool bergerPracticeEncode = true;
        private void TabControl_Berger_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!results.ContainsKey("Berger"))
            {
                results.Add("Berger", new Result("Код Бергера", 6));
            }
            TabControl tabControl = (TabControl)sender;
            if (tabControl.SelectedIndex == 2)
            {
                GenerateBergerTest();
            }
            else if (tabControl.SelectedIndex == 1)
            {
                GenerateBergerPractice();
            }
        }

        private void GenerateBergerPractice()
        {
            if (bergerPracticeEncode)
            {
                lblBergerTaskExplanation_Practice.Content = "Зашифруйте:";
                lblTask_Practice.Content = BergerService.generateEncode();
            }
            else
            {
                lblBergerTaskExplanation_Practice.Content = "Расшифруйте:";
                lblTask_Practice.Content = BergerService.generateDecode();
            }
        }

        private void GenerateBergerTest()
        {
            Result result = results["Berger"];
            if (result.currentTestNumber <= 3)
            {
                lblBergerTaskExplanation.Content = "Зашифруйте:";
                lblBergerTask.Content = BergerService.generateEncode();
            }
            else if (result.currentTestNumber > 3 && result.currentTestNumber < 7)
            {
                lblBergerTaskExplanation.Content = "Расшифруйте:";
                lblBergerTask.Content = BergerService.generateDecode();
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
                    BergerService.isEncodedCorrectly(lblBergerTask.Content.ToString(), txbBergerResult.Text) :
                    BergerService.isDecodedCorrectly(lblBergerTask.Content.ToString(), txbBergerResult.Text))
                {
                    MessageBox.Show("Правильно!");
                    txbBergerResult.Text = "";
                    result.correctTests += 1;
                    result.currentTestNumber += 1;
                }
                else
                {
                    MessageBox.Show("Не правильно! Ответ: " + (result.currentTestNumber <= 3 ?
                        BergerService.encode(lblBergerTask.Content.ToString()) :
                        BergerService.decode(lblBergerTask.Content.ToString())));
                    txbBergerResult.Text = "";
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
                    BergerService.isEncodedCorrectly(lblTask_Practice.Content.ToString(), txbBergerResult_Practice.Text) :
                    BergerService.isDecodedCorrectly(lblTask_Practice.Content.ToString(), txbBergerResult_Practice.Text))
            {
                MessageBox.Show("Правильно!");
            }
            else
            {
                MessageBox.Show("Не правильно! Ответ: " + (bergerPracticeEncode ?
                    BergerService.encode(lblTask_Practice.Content.ToString()) :
                    BergerService.decode(lblTask_Practice.Content.ToString())));
            }
        }
        #endregion

        #region Shennon-Fano

        List<ShennonFanoDto> ShennonMessages = new List<ShennonFanoDto>();

        private void TabControl_Shennon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!results.ContainsKey("Shennon-Fano"))
            {
                results.Add("Shennon-Fano", new Result("Код Шеннона-Фано", 3));
            }
            TabControl tabControl = (TabControl)sender;
            if (tabControl.SelectedIndex == 2)
            {

                //GenerateShennon();

                GenerateShennonTest();
            }
            else if (tabControl.SelectedIndex == 1)
            {
                GenerateShennonPractice();

            }
        }

        private void GenerateShennonPractice()
        {
            ShennonMessages = ShennonFanoService.generateMessages();
            Resources["ShennonTask"] = ShennonMessages;
        }

        private void GenerateShennonTest()
        {
            Result result = results["Shennon-Fano"];
            if (result.currentTestNumber <= 3)
            {
                ShennonMessages = ShennonFanoService.generateMessages();
                Resources["ShennonTask"] = ShennonMessages;
            }
            else
            {
                MessageBox.Show("Вы уже закончили этот тест!");
            }
        }




        private void Button_Shennon_Test_Next_Click(object sender, RoutedEventArgs e)

        {
            Result result = results["Shennon-Fano"];
            if (result.currentTestNumber <= 3)
            {
                if (ShennonFanoService.isCalculatedCorrectly(ShennonMessages))
                {
                    MessageBox.Show("Правильно!");
                    result.correctTests += 1;
                    result.currentTestNumber += 1;
                }
                else
                {
                    MessageBox.Show("Не правильно.");
                    result.currentTestNumber += 1;
                }
                GenerateShennonTest();
            }
        }
        #endregion

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


            TestVAR_control.SelectedIndex++;

        }


        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e) { }

        private void CodeB4H_control_SelectionChanged(object sender, SelectionChangedEventArgs e)

        {

        }


        private void TbHem_TextChanged(object sender, TextChangedEventArgs e) { }

        private void code_B4H_btn_Click(object sender, RoutedEventArgs e)

        {

        }




        int cheat;
        string correctAnswer;
        string dcorrectAnswer;
        bool isCodingEnabled;
        private void hemmingCoding(TextBox tb, Label lb)
        {
            isCodingEnabled = true;
            string taskNumber = "";
            Random rnd = new Random();
            int a = rnd.Next(6) + 9;
            Random rand = new Random();

            for (int i = 0; i <= a; i++)
            {
                taskNumber += rand.Next(2);

            }
            lb.Content = "Закодируйте сообщение: " + taskNumber;
            //MessageBox.Show(Model.Services.HemmingCodeService.Main(taskNumber));
            correctAnswer = Model.Services.HemmingCodeService.Main(taskNumber);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tbHem.Text == correctAnswer && isCodingEnabled)
            {
                MessageBox.Show("Правильно");
                cheat = 0;

                hemmingCoding(tbHem, lblHemtask);


            }
            else if (tbHem.Text == dcorrectAnswer && !isCodingEnabled)
            {
                MessageBox.Show("Правильно");
                cheat = 0;
            }
            else
            {
                MessageBox.Show("Неправильно");
                cheat = 0;
            }



        }
        private void Button_Shennon_Practice_Next_Click(object sender, RoutedEventArgs e)
        {
            if (ShennonFanoService.isCalculatedCorrectly(ShennonMessages))
            {
                MessageBox.Show("Правильно!");
            }
            else
            {
                MessageBox.Show("Не правильно!.");
            }
            GenerateShennonPractice();
        }


        private void BtnHemNext_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (cheat == 5 && isCodingEnabled)
            {
                tbHem.Text = correctAnswer;
            }
            else if (cheat == 5 && !isCodingEnabled)
            {
                tbHem.Text = dcorrectAnswer;
            }
            else { cheat++; }

        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            hemmingCoding(tbHem, lblHemtask);
        }

        private void BtnHemDecoding_Click(object sender, RoutedEventArgs e)
        {
            hemDecoding();
        }

        private void BtnHemCoding_Click(object sender, RoutedEventArgs e)
        {
            hemmingCoding(tbHem, lblHemtask);
        }
        private void hemDecoding()
        {

            isCodingEnabled = false;
            string taskNumber = "";
            Random rnd = new Random();
            int a = rnd.Next(6) + 9;
            Random rand = new Random();

            for (int i = 0; i <= a; i++)
            {
                taskNumber += rand.Next(2);

            }
            lblHemtask.Content = "Раскодируйте сообщение: " + Model.Services.HemmingCodeService.Main(taskNumber);
            //MessageBox.Show(Model.Services.HemmingCodeService.Main(taskNumber));
            dcorrectAnswer = taskNumber;
        }

        private void Grid_Loaded_1(object sender, RoutedEventArgs e)
        {
            hemmingCoding(tbHem1, lblHemtask1);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (tbHem1.Text == correctAnswer && isCodingEnabled)
            {
                MessageBox.Show("Правильно");

                hemmingCoding(tbHem1, lblHemtask1);


            }

            else
            {
                MessageBox.Show("Неправильно");
            }
        }



        private void decode_B4H_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void codeQ_btn_Click(object sender, RoutedEventArgs e)
        {
            codeQ_control.SelectedIndex = 0;
        }

        private void decodeQ_btn_Click(object sender, RoutedEventArgs e)
        {
            codeQ_control.SelectedIndex = 1;
        }


        private void NextQ_btn_Click(object sender, RoutedEventArgs e)
        {
            Qstage++;
            if (Qstage == 4)
            {
                TestQ_control.SelectedIndex++;
            }
            else if (Qstage == 8)
            {
                TestQ_control.SelectedIndex--;
                Qstage = 0;
            }

        }

        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(results).Refresh();
        }

        private void codeP_btn_Click(object sender, RoutedEventArgs e)
        {
            codeP_control.SelectedIndex = 0;
        }
        private void decodeP_btn_Click(object sender, RoutedEventArgs e)
        {
            codeP_control.SelectedIndex = 1;
        }

        private void NextP_btn_Click(object sender, RoutedEventArgs e)
        {
            Pstage++;
            if (Pstage == 4)
            {
                TestP_control.SelectedIndex++;
            }
            else if (Pstage == 8)
            {
                TestP_control.SelectedIndex--;
                Pstage = 0;
            }
        }

        private void codeB4H_btn_Click(object sender, RoutedEventArgs e)
        {
            codeB4H_control.SelectedIndex = 0;

        }
        private void decodeB4H_btn_Click(object sender, RoutedEventArgs e)
        {
            codeB4H_control.SelectedIndex = 1;
        }
        private void NextB4H_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void codePN_btn_Click(object sender, RoutedEventArgs e)
        {
            codePN_control.SelectedIndex = 0;
        }
        private void decodePN_btn_Click(object sender, RoutedEventArgs e)
        {
            codePN_control.SelectedIndex = 1;
        }

        private void NextPN_btn_Click(object sender, RoutedEventArgs e)
        {

        }




        private void codeA_btn_Click(object sender, RoutedEventArgs e)
        {
            codeA_control.SelectedIndex = 0;
        }
        private void decodeA_btn_Click(object sender, RoutedEventArgs e)
        {
            codeA_control.SelectedIndex = 1;
        }
        private void NextA_btn_Click(object sender, RoutedEventArgs e)
        {
            Astage++;
            if (Astage == 4)
            {
                TestA_control.SelectedIndex++;
            }
            else if (Astage == 8)
            {
                TestA_control.SelectedIndex--;
                Astage = 0;
            }
        }


        #region Fayr
        int frcheat;
        bool frisCodingEnabled;
        string polinom;
        string msg;
        string franswer;
        int testnumber;
        int correctanswers;

        private void GridFayr_Loaded(object sender, RoutedEventArgs e)
        {

            fayr();
            testnumber = 1;
            correctanswers = 0;


        }

        private void FeNext_Click(object sender, RoutedEventArgs e)
        {
            if (frisCodingEnabled)
            {
                if (frTextbox.Text == franswer)
                {
                    MessageBox.Show("Правильно!");
                    fayr();
                }
                else
                {
                    MessageBox.Show("Не правильно!");

                }
            }
            else
            {
                if (frTextbox.Text == msg)
                {
                    MessageBox.Show("Правильно!");
                    fayrd();
                }
                else
                {
                    MessageBox.Show("Не правильно!");

                }

            }

        }
        private void fayr()
        {

            frisCodingEnabled = true;
            franswer = "";
            msg = "";
            polinom = "1110111";
            Random rand = new Random();

            for (int i = 0; i <= 5; i++)
            {
                msg += rand.Next(2).ToString();
            }

            if (msg == "000000")
            {
                msg = "";
                for (int i = 0; i <= 5; i++)
                {
                    msg += rand.Next(2).ToString();
                }


            }
            double c = Convert.ToInt32(msg, 2) % Convert.ToInt32(polinom, 2);

            Int64 BinaryHolder;
            char[] BinaryArray;
            string BinaryResult = "";

            while (c > 0)
            {
                BinaryHolder = Convert.ToInt64(c % 2);
                BinaryResult += BinaryHolder;
                c = c / 2;
            }

            BinaryArray = BinaryResult.ToCharArray();
            Array.Reverse(BinaryArray);
            BinaryResult = new string(BinaryArray);

            bool iszero = true;
            string ostatok = "";
            foreach (char k in BinaryResult)
            {
                if (k.ToString() == "1")
                {
                    iszero = false;
                    ostatok += "1";
                }
                else if (iszero == false)
                {
                    ostatok += "0";
                }

            }
            franswer = msg + ostatok;
            frCodelbl.Content = "Закодируте сообщение: " + msg;
            frtCodelbl.Content = "Закодируте сообщение: " + msg;
            polinomLbl.Content = "Неприводимый полином P1: 111";

        }

        private void FeNext_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {

            if (frcheat == 5 && frisCodingEnabled)
            {
                frTextbox.Text = franswer;
            }
            else if (frcheat == 5 && !frisCodingEnabled)
            {
                frTextbox.Text = msg;
            }
            else { frcheat++; }
        }

        private void FrCodebtn_Click(object sender, RoutedEventArgs e)
        {
            fayr();
        }
        private void fayrd()
        {
            frisCodingEnabled = false;
            franswer = "";
            msg = "";
            polinom = "1110111";
            Random rand = new Random();

            for (int i = 0; i <= 5; i++)
            {
                msg += rand.Next(2).ToString();
            }

            if (msg == "000000")
            {
                msg = "";
                for (int i = 0; i <= 5; i++)
                {
                    msg += rand.Next(2).ToString();
                }


            }
            double c = Convert.ToInt32(msg, 2) % Convert.ToInt32(polinom, 2);

            Int64 BinaryHolder;
            char[] BinaryArray;
            string BinaryResult = "";

            while (c > 0)
            {
                BinaryHolder = Convert.ToInt64(c % 2);
                BinaryResult += BinaryHolder;
                c = c / 2;
            }

            BinaryArray = BinaryResult.ToCharArray();
            Array.Reverse(BinaryArray);
            BinaryResult = new string(BinaryArray);

            bool iszero = true;
            string ostatok = "";
            foreach (char k in BinaryResult)
            {
                if (k.ToString() == "1")
                {
                    iszero = false;
                    ostatok += "1";
                }
                else if (iszero == false)
                {
                    ostatok += "0";
                }

            }
            //MessageBox.Show(msg);
            franswer = msg + ostatok;
            frCodelbl.Content = "Закодируте сообщение: " + franswer;
            polinomLbl.Content = "Неприводимый полином P1: 111";
        }
        private void FrDecodebtn_Click(object sender, RoutedEventArgs e)
        {
            fayrd();

        }

        private void FrtestBtn_Click(object sender, RoutedEventArgs e)
        {
            if (frtTextbox.Text == franswer && testnumber <= 10)
            {
                MessageBox.Show("Правильно!");
                correctanswers += 1;
                fayr();
            }
            else if (frtTextbox.Text != franswer && testnumber <= 10)
            {
                MessageBox.Show("Не правильно!");
                fayr();
            }
            if (testnumber == 10)
            {
                MessageBox.Show("Тест завершён. Количество правильных ответов: " + correctanswers);
            }
            testnumber += 1;
            testnum.Content = testnumber.ToString();
            crctans.Content = correctanswers.ToString();

        }

        private void SurrendBtn_MouseMove(object sender, MouseEventArgs e)
        {
            if (frisCodingEnabled)
            {
                ToolTip = franswer;
            }
            else
            {
                ToolTip = msg;
            }
        }
        #endregion

    }
}
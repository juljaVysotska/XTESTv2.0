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

namespace XTest
{
   
    public partial class MainWindow : Window
    {
        
        public static Dictionary<string, Result> results = new Dictionary<string, Result>();

        public MainWindow()
        {
            InitializeComponent();
          
        }

        #region Berger
        private void TabControl_Berger_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!results.ContainsKey("Berger"))
            {
                results.Add("Berger", new Result("Berger", 6));
            }
            TabControl tabControl = (TabControl)sender;
            if (tabControl.SelectedIndex == 2)
            {
                GenerateBerger();
            }
        }

        private void GenerateBerger()
        {
            Result result = results["Berger"];
            if (result.currentTestNumber <= 3)
            {
                lblBergerTaskExplanation.Content = "Encode this:";
                lblBergerTask.Content = BergerService.generateEncode();
            }
            else if (result.currentTestNumber > 3 && result.currentTestNumber < 7)
            {
                lblBergerTaskExplanation.Content = "Decode this:";
                lblBergerTask.Content = BergerService.generateDecode();
            }
            else
            {
                MessageBox.Show("You've already completed this test!");
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
                    MessageBox.Show("Congrats!");
                    result.correctTests += 1;
                    result.currentTestNumber += 1;
                }
                else
                {
                    MessageBox.Show("Wrong answer. Correct answer: " + (result.currentTestNumber <= 3 ?
                        BergerService.encode(lblBergerTask.Content.ToString()) :
                        BergerService.decode(lblBergerTask.Content.ToString())));
                    result.currentTestNumber += 1;
                }
                GenerateBerger();
            }
        }
        #endregion

        #region Shennon-Fano
        private void TabControl_Shennon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!results.ContainsKey("Shennon-Fano"))
            {
                results.Add("Shennon-Fano", new Result("Shennon-Fano", 3));
            }
            TabControl tabControl = (TabControl)sender;
            if (tabControl.SelectedIndex == 2)
            {
                GenerateShennon();
            }
        }

        private void GenerateShennon()
        {
            Result result = results["Shennon-Fano"];
            if (result.currentTestNumber <= 3)
            {
                Dictionary<int, double> messages = ShennonFanoService.generateMessages();
                foreach (var message in messages)
                {
                    //TODO display messages
                }
            }
            else
            {
                MessageBox.Show("You've already completed this test!");
            }
        }

        private void Button_Shennon_Next_Click(object sender, RoutedEventArgs e)
        {
            Result result = results["Shennon-Fano"];
            if (result.currentTestNumber <= 3)
            {
                //TODO collect info and check if correct
                if (ShennonFanoService.isCalculatedCorrectly(null, null))
                {
                    MessageBox.Show("Congrats!");
                    result.correctTests += 1;
                    result.currentTestNumber += 1;
                }
                else
                {
                    MessageBox.Show("Wrong answer.");
                    result.currentTestNumber += 1;
                }
                GenerateShennon();
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
            
                TestVAR_control.SelectedIndex++;
        }
    }
}
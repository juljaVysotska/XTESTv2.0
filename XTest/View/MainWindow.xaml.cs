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
using XTest.ViewModel;
using XTest.Model.Models;
using XTest.Model.Services;

namespace XTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Dictionary<string, Result> marks = new Dictionary<string, Result>();
        
        public MainWindow()
        {
            InitializeComponent();
            EllayesViewModel evm = new EllayesViewModel();
            DataContext = evm;          
        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TabControl_Berger_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!marks.ContainsKey("Berger"))
            {
                marks.Add("Berger", new Result("Berger", 6));
            }
            TabControl tabControl = (TabControl)sender;
            if (tabControl.SelectedIndex == 2)
            {
                GenerateBerger();
            }
        }

        private void GenerateBerger()
        {
            Result result = marks["Berger"];
            if (result.currentTestNumber <= 3)
            {
                lblBergerTaskExplanation.Content = "Encode this:";
                lblTask.Content = Berger.generateEncode();
            }
            else if (result.currentTestNumber > 3 && result.currentTestNumber < 7)
            {
                lblBergerTaskExplanation.Content = "Decode this:";
                lblTask.Content = Berger.generateDecode();
            }
            else
            {
                MessageBox.Show("You've already completed this test!");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Result result = marks["Berger"];
            if (Berger.isEncodedCorrectly(lblTask.Content.ToString(), txbBergerResult.Text))
            {
                MessageBox.Show("Congrats!");
                result.correctTests += 1;
                result.currentTestNumber += 1;
            }
            else
            {
                MessageBox.Show("Game over. Correct answer: " + Berger.encode(lblTask.Content.ToString()));
                result.currentTestNumber += 1;
            }
            GenerateBerger();
        }
    }
}
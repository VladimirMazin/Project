using Project_WPF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace StudentWPfApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        string FullFileName="";
        Window1 Window = new Window1();
        Student st = null;
        int CounterQuastion = 0;
        List<Quastion> testquastions = null;
        List<Grid> grids = null;
        AnswerOptionsOnGrid a = null;

        private void ChooseTestTextBox_Click(object sender, RoutedEventArgs e)
        {
            string fileName = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() != DialogResult)
            {
                fileName = openFileDialog.SafeFileName;
                FullFileName = openFileDialog.FileName;
            }
            TestNameLabel.Content = "";
            TestNameLabel.Content += fileName;
        }

        private void StartTestTextBox_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text;
            string surname = SurnameTextBox.Text;
            string group = GroupTextBox.Text;
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(surname) && !string.IsNullOrEmpty(group) && !string.IsNullOrEmpty(FullFileName))
            {
                st = new Student(name, surname, group);
                Test quastions = new Test();
                quastions.quastions = SavingAndReadingTest.DeserialiseTest(FullFileName);
                st.studentstest = quastions;
                Close();
                ShowQuastionInTest();
            }
            else
            {
                if (string.IsNullOrEmpty(FullFileName))
                {
                    MessageBox.Show("Не был выбран тест для прохождения!");
                }
                else if (string.IsNullOrEmpty(NameTextBox.Text))
                {
                    MessageBox.Show("Не было указано имя ученика!");
                }
                else if (string.IsNullOrEmpty(SurnameTextBox.Text))
                {
                    MessageBox.Show("Не была указана фамилия ученика!");
                }
                else
                {
                    MessageBox.Show("Не была указана учебная группа ученика!");
                }
            }
        }
        private void ShowQuastionInTest()
        {
            testquastions = st.studentstest.quastions;
            ShowTest showTest = new ShowTest(testquastions, Window.TextQuastionGrid, Window.AnswerQuastionGrid);
            ShowFirstQuastion(showTest,testquastions);
        }
        private void ShowFirstQuastion(ShowTest showTest,List<Quastion> testquastions)
        {
            grids = showTest.GetGrids();
            grids[CounterQuastion].Visibility = Visibility.Visible;
            Window.NextQuastion.Click += NextQuastion;
            Window.LastQuastion.Click += FinishTest;
            if (testquastions[CounterQuastion].is_test)
            {
                a = new AnswerOptionsOnGrid(testquastions[CounterQuastion].answers.Count);
                Window.AnswerQuastionFieldGrid = a.InitializeOptions(Window.AnswerQuastionFieldGrid, testquastions[CounterQuastion].answers);
                Window.ConditionTextBox2.Text = testquastions[CounterQuastion].Get_text_quastion();
            }
            else
            {
                Window.ConditionTextBox1.Text = testquastions[CounterQuastion].Get_text_quastion();
            }
            Window.NextQuastion.Visibility = Visibility.Visible;
            if (Window.ShowDialog() == DialogResult)
            {
            }
        }
        public void NextQuastion(object sender, RoutedEventArgs e)
        {
            CounterQuastion++;
            AddStudentAnswer(a, testquastions[CounterQuastion - 1]);
            grids[CounterQuastion - 1].Visibility = Visibility.Hidden;
            grids[CounterQuastion].Visibility = Visibility.Visible;
            if (testquastions[CounterQuastion].is_test)
            {
                a = new AnswerOptionsOnGrid(testquastions[CounterQuastion].answers.Count);
                Window.AnswerQuastionFieldGrid = a.InitializeOptions(Window.AnswerQuastionFieldGrid, testquastions[CounterQuastion].answers);
                Window.ConditionTextBox2.Text = testquastions[CounterQuastion].Get_text_quastion();
            }
            else
            {
                Window.ConditionTextBox1.Text = testquastions[CounterQuastion].Get_text_quastion();
            }
            if (CounterQuastion == grids.Count-1)
            {
                Window.NextQuastion.Visibility = Visibility.Hidden;
                Window.LastQuastion.Visibility = Visibility.Visible;
            }
        }

        public void FinishTest(object sender, RoutedEventArgs e)
        {
            CounterQuastion++;
            AddStudentAnswer(a, testquastions[CounterQuastion - 1]);
            st.studentstest.quastions = testquastions;
            st.studentstest.Check_score();
            string info = st.studentstest.All_info_of_test();
            (string StudentsName, string StudentsSurname, string StudentsGroup) = st.GetInfo();
            (int StudentsResult, double StudentsProcents) = st.studentstest.Getresult();
            StreamWriter sw = new StreamWriter("Результаты ученика " + StudentsName + " " + StudentsSurname + " " + StudentsGroup + " " + ".txt");
            sw.WriteLine("Результаты ученика: " + "\n" + "Ученик ответил правильно на " + StudentsProcents + "% вопросов" +"\n"+ "Оценка ученика: " + StudentsResult);
            sw.WriteLine(info);
            sw.Close();
            Window.Close();
            MessageBox.Show("Вы ответили правильно на "+ StudentsProcents +"% вопросов" + "\n" + "Ваша оценка " + StudentsResult,"Результат тестирования");
            Environment.Exit(0);
        }
        public void AddStudentAnswer(AnswerOptionsOnGrid a,Quastion quastion)
        {
            if (quastion.is_test)
            {
                a.AddStudentTestAnswer(quastion);
            }
            else
            {
                quastion.answer.Check_answer(Window.AnswerTextBox.Text);
                Window.AnswerTextBox.Text = string.Empty;
            }
        }
    }
}

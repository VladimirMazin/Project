using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Project_WPF;
using System.Windows.Media;

namespace StudentWPfApp
{
    public class ShowTest
    {
        List<Grid> gridtest = new List<Grid>();

        public ShowTest(List<Quastion> testquastion,Grid textquastuiongrid, Grid answerquastiongrid)
        {
            foreach (var quastion in testquastion)
            {
                if (!quastion.is_test)
                {
                    gridtest.Add(textquastuiongrid);
                }
                else
                {
                    gridtest.Add(answerquastiongrid);
                }
            }
        }
        public List<Grid> GetGrids()
        {
            return gridtest;
        }
    }
    public class Student
    {
        private string Name;
        private string Surname;
        private string Group;
        public Test studentstest;

        public Student(string name, string surname, string group)
        {
            Name = name;
            Surname = surname;
            Group = group;
        }
        public (string,string,string) GetInfo()
        {
            return (Name, Surname, Group);
        }
    }
    public class AnswerOptionsOnGrid
    {
        public int NumberOfOptions;
        public CheckBox[] Options;
        public AnswerOptionsOnGrid(int numberOfOptions)
        {
            NumberOfOptions = numberOfOptions;
            Options = new CheckBox[numberOfOptions];
        }
        public Grid InitializeOptions(Grid TestGrid,List<Answer> answers)
        {
            TestGrid.Children.Clear();
            TestGrid.RowDefinitions.Clear();
            for (int i = 0; i < NumberOfOptions; i++)
            {
                (TestGrid).RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < NumberOfOptions; i++)
            {
                Options[i] = new CheckBox();
                Options[i].Content = answers[i].answer;
                Options[i].FontFamily = new FontFamily("Calibry");
                Options[i].FontSize = 18;
                Options[i].LayoutTransform = new ScaleTransform(1.9, 1.9);
                Options[i].SetValue(Grid.RowProperty, i);
                (TestGrid).Children.Add(Options[i]);
            }
            return TestGrid;
        }
        public void AddStudentTestAnswer(Quastion quastion)
        {
            for (int i = 0; i < Options.Length; i++)
            {
                if (Options[i].IsChecked == true)
                {
                    quastion.answers[i].set_true_answer_of_student();
                }
            }
        }
    }
}

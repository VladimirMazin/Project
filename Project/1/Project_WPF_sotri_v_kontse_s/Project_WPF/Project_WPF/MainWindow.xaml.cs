using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using System.Xml.Serialization;

namespace Project_WPF
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
        Test test = new Test();
        List<Quastion> quastions = new List<Quastion>();
        List<Answer>  temp_answers = new List<Answer>();
        private void show_answers()
        {
            //List_box_with_answers.Items.Clear();
            //foreach (var answer in temp_answers)
            //{
            //    List_box_with_answers.Items.Add(answer.show_answer());
            //}

        }
        private void Button_add_quastion_Click(object sender, RoutedEventArgs e) //создание теста
        {
            //if(temp_answers.Contains(new Answer(Text_answer.Text)))
            //{
            //    throw new ArgumentException("Данный ответ уже есть в тесте");
            //}
            if (CheckBox_is_test.IsChecked.Value == false)
            {
                if (temp_answers.Count == 1)
                {
                    MessageBox.Show("В письменном вопросе возможен только 1 ответ");
                    return;
                }
            }
            StringBuilder text = new StringBuilder();
            foreach (char c in Text_answer.Text)
            {
                //if (c != ' ')
                //{
                    text.Append(c);
                //}
            }
            if (text.ToString() != "")
            {

                Answer new_answer = new Answer(text.ToString(), CheckBox_true_answer.IsChecked.Value);
                temp_answers.Add(new_answer);
                ShowTempAnswers(new_answer);
            }
            else
            {
                MessageBox.Show("Вы ввели пустой ответ");
            }
            show_answers();
            ClearOnlyAnswer();
        }

        private void Clear_test_Click(object sender, RoutedEventArgs e)//очистить поля
        {
            List<Quastion> quastions = new List<Quastion>();
            ClearAll();
            NewTestGrid.Visibility = Visibility.Visible;
            CheckGrid.Visibility = Visibility.Hidden;
        }
        private void Clear_window()
        {
            Text_Quastion.Clear();
            CheckBox_true_answer.IsChecked = false;
            Text_answer.Clear();
            CheckBox_is_test.IsChecked = false;
            temp_answers = new List<Answer>();
            WatchBox.Text = "";
        }
        private void ClearOnlyAnswer()
        {
            CheckBox_true_answer.IsChecked = false;
            Text_answer.Text = "";
        }
        private void ShowTempAnswers(Answer a)
        {
            WatchBox.Text += temp_answers.Count.ToString()+ ". ";
            WatchBox.Text += a.show_answer_with_out_student();
            WatchBox.Text += "\n";
        }
        private void ClearAll()
        {
            Text_Quastion.Clear();
            CheckBox_true_answer.IsChecked = false;
            Text_answer.Clear();
            CheckBox_is_test.IsChecked = false;
            WatchBox.Text = "";
        }
        private new void Show()
        {
            text_out.Text = "";
            text_out.Text = test.All_info_of_test_with_out_student();
            //foreach (var quastion in quastions)
            //{
            //    text_out.Text += (quastion.Answers_of_quastion_out());
            //}
        }
        private void Create_Test()
        {
            test.Get_all_quastions(quastions);
        }
        private void Button_accept_Click(object sender, RoutedEventArgs e)
        {
            if (Text_Quastion != null)
            {
                Quastion new_quastion = new Quastion(CheckBox_is_test.IsChecked);
                new_quastion.Add_text_quastion(Text_Quastion.Text);
                if (temp_answers.Count > 0)
                {
                    if (CheckBox_is_test.IsChecked.Value == true)
                    {
                        new_quastion.Get_Answers_test(temp_answers);
                    }
                    else
                    {
                        new_quastion.Get_Answers_write(new Answer_write(temp_answers[0]));
                    }
                    quastions.Add(new_quastion);
                }
            }
            temp_answers.Clear();
            //Window_Quastions test_win = new Window_Quastions(quastions);
            //test_win.Show();
            ClearAll();
        }

        private void SaveTest_Click(object sender, RoutedEventArgs e)
        {
            //проверка на имя + формат сохранения и открытия для теста придумать + тут создавать тест
            if (!string.IsNullOrEmpty(Text_box_for_name_file.Text))
            {
                SavingAndReadingTest.SerialiseTest(quastions, Text_box_for_name_file.Text);
                Environment.Exit(0);
            }
            else
            {
                MessageBox.Show("Не было указано имя теста!");
            }
        }

        private void Add_test_Click(object sender, RoutedEventArgs e)
        {
            NewTestGrid.Visibility = Visibility.Hidden;
            CheckGrid.Visibility = Visibility.Visible;
            Create_Test();
            Show();
            //test.print_test_with_out_student();
        }

        private void Button_Clear_Question_Click(object sender, RoutedEventArgs e)
        {
            Clear_window();
        }

        private void EditTest_Click(object sender, RoutedEventArgs e)
        {
            //открыть новое окно для редактирования(выбрать номер вопроса и редактировать вопрос/ответ)
            EditGrid.Visibility = Visibility.Visible;
            CheckGrid.Visibility = Visibility.Hidden;
            ViewTextBox.Text = test.All_info_of_test_with_out_student();
        }

        private void Edit_save_Click(object sender, RoutedEventArgs e)
        {
            //Change_Quastion тут нужен
            if (flag_edit_accept)
            {
                Quastion edit_quastion = Parse_from_form_to_Quastion();
                try
                {
                    int number = int.Parse(Number_of_quastion.Text);
                    number--;
                    if (number < 0 || number > test.Get_count_quastions())
                    {
                        MessageBox.Show("Неверно указан номер вопроса. Он больше, чем количество вопросов или меньше 1");
                    }
                    test.Get_quastions()[number] = edit_quastion;
                    ViewTextBox.Text = test.All_info_of_test_with_out_student();
                    quastions = test.Get_quastions();
                }
                catch
                {
                    MessageBox.Show("Неверно указан номер вопроса или содержит в себе не только цифры");
                }

            }
            else
            {
                MessageBox.Show("Нечего сохранять, не выбран вопрос для редактирования");
            }
        }

        private void Edit_go_to_save_test_Click(object sender, RoutedEventArgs e)
        {
            CheckGrid.Visibility = Visibility.Visible;
            EditGrid.Visibility = Visibility.Hidden;
            Show();
        }
        bool flag_edit_accept = false;
        bool flag_what_is_quastion_for_change = false;//test or ont
        private Quastion Parse_from_form_to_Quastion()
        {
            Answer_test temp_la = new Answer_test();
            Answer_write aw = new Answer_write();
            if (flag_edit_accept)
            {
                Quastion quastion = new Quastion();
                quastion.Set_is_test(flag_what_is_quastion_for_change);
                try
                {
                    if (QuastionTextBox.Text != null)
                    {
                        quastion.Add_text_quastion(QuastionTextBox.Text);
                    }
                    else
                    {
                        MessageBox.Show("Текст вопроса пустой");
                        return null;
                    }
                    string[] text_to_parse = AnswersTextBox.Text.Split('\n');
                    int len = text_to_parse.Length-1;//вычёркиваем последнюю строку
                    StringBuilder stringBuilder = new StringBuilder();
                    for(int i = 0; i < len-1; i++)
                    {
                        stringBuilder.Clear();
                        string[] string_parse = text_to_parse[i].Replace(":", "").Split(' ');//индекс 0 = ответ индекс 1 = верный или нет
                        int len_parse = string_parse.Length;
                        if (flag_what_is_quastion_for_change==false) //письменный
                        {
                            for(int j=0;j< len_parse-3; j++)
                            {
                                stringBuilder.Append(string_parse[j]+" ");
                            }
                            stringBuilder.Append(string_parse[len_parse - 3]);
                            Answer wa = new Answer(stringBuilder.ToString());
                            aw.Add_Answer(wa);
                        }
                        else
                        {
                            for (int j = 0; j < len_parse - 3; j++)
                            {
                                stringBuilder.Append(string_parse[j]+" ");
                            }
                            stringBuilder.Append(string_parse[len_parse - 3]);
                            Answer wa = new Answer(stringBuilder.ToString());
                            if (string_parse[len_parse-2] == "Верный")
                            {
                                wa.do_flag_true();
                            }
                            temp_la.Add_answers(wa);
                        }
                    }
                    if (flag_what_is_quastion_for_change)
                    {
                        quastion.Get_Answers_test(temp_la.Get_answers());
                    }
                    else
                    {
                        quastion.Get_Answers_write(aw);
                    }

                        return quastion;

                }
                catch
                {
                    MessageBox.Show("После редактирования ответов или вопроса возникла ошибка. Возможно не сохранён формат вывода верного или неверного ответа");
                    return null;
                }

                return null;
            }
            else
            {
                MessageBox.Show("Не выбран вопрос для редактирования");
            }
            return null;

        }
        private void Edit_accept_to_redact_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int number = int.Parse(Number_of_quastion.Text);
                number--;
                if(number<0 || number > test.Get_count_quastions())
                {
                    MessageBox.Show("Неверно указан номер вопроса. Он больше, чем количество вопросов или меньше 1");
                }
                else
                {
                    flag_edit_accept = true;

                    Quastion quastion_to_redact = test.Get_concretical_quastion(number);
                    flag_what_is_quastion_for_change = quastion_to_redact.Get_is_test();
                    QuastionTextBox.Text = quastion_to_redact.Get_text_quastion();
                    AnswersTextBox.Text = quastion_to_redact.Answers_of_quastion_out();
                }
            }
            catch{
                MessageBox.Show("Неверно указан номер вопроса или содержит в себе не только цифры");
            }
        }
    }
}

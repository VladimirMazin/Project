using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows;
using System.Xml.Serialization;


namespace Project_WPF
{
    interface Istatistics
    {
        void Check_score();//подсчёт количества баллов за тест
        void Calculate_percentages_of_test();//подсчёт кол-ва процентов верных ответов
        string All_info_of_test(); //вся информация по заданиям
        void Result();//результат за тест

    }
    [Serializable]
    public class Test : Istatistics
    {
        public List<Quastion> quastions = new List<Quastion>();
        int all_score = 0;
        int all_score_student = 0;
        double prosents = 0;
        int result = 0;

        public Test() { }
        public int Get_count_quastions()
        {
            return quastions.Count;
        }
        public List<Quastion> Get_quastions()
        {
            return quastions;
        }
        public Quastion Get_concretical_quastion(int index)
        {
            return quastions[index];
        }
        public void print_test()
        {
            StreamWriter sw = new StreamWriter("Test_statistics.txt");
            sw.WriteLine(All_info_of_test());
            sw.Close();
        }
        public void print_test_with_out_student()
        {
            StreamWriter sw = new StreamWriter("Test_statistics_with_out_student.txt");
            sw.WriteLine(All_info_of_test_with_out_student());
            sw.Close();
        }
        public string All_info_of_test_with_out_student()
        {
            StringBuilder sb = new StringBuilder();
            int number = 0;
            foreach (Quastion quastion in quastions)
            {
                sb.Append($"Текст вопроса {++number}:" + quastion.Get_text_quastion() + "\n");
                if (quastion.Get_is_test())
                {
                    foreach (var ans in quastion.Get_all_test())
                    {
                        sb.Append("Тестовый ответ: " + ans.show_answer_with_out_student() + "\n");
                    }
                }
                else
                {
                    sb.Append("Письменный ответ: " + quastion.Get_write_answer().show_answer_with_out_student() + "\n");
                }
                sb.Append("\n\n");
            }
            return sb.ToString();
        }
        public string All_info_of_test()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Quastion quastion in quastions)
            {
                sb.Append("Текст вопроса:" + quastion.Get_text_quastion() + "\n");
                if (quastion.Get_is_test())
                {
                    foreach (var ans in quastion.Get_all_test())
                    {
                        sb.Append("Тестовый ответ: " + ans.show_answer() + "\n");
                    }
                }
                else
                {
                    sb.Append("Письменный ответ: " + quastion.Get_write_answer().show_answer() + "\n");
                }
                sb.Append("\n\n");
            }
            return sb.ToString();

        }
        public void Get_all_quastions(List<Quastion> list_quastion)
        {
            foreach (var quastion in list_quastion)
            {
                quastions.Add(quastion);
            }//чтобы избежать ссылочного типа при изменении list_quastion кладём по 1 вопросу с ответами.

        }

        public void Calculate_percentages_of_test()
        {
            if (all_score != 0)
            {
                prosents = Math.Round(((double)all_score_student / (double)all_score)*100,2);
            }
            else
            {
                MessageBox.Show("Все очки за тест равны 0, сперва проверьте количество очков");
            }
        }

        public void Check_score()//получаем очки за весь тест + очки ученика
        {
            all_score = 0;
            all_score_student = 0;
            foreach (Quastion quastion in quastions)
            {
                if (quastion.Get_is_test())
                {
                    bool flag = true;
                    foreach (var ans in quastion.Get_all_test())
                    {
                        if (ans.Get_flag() == false && ans.get_true_answer_of_student() == true)
                        {
                            flag = false;
                        }
                        if (ans.Get_flag() == true && ans.get_true_answer_of_student() == false)
                        {
                            flag = false;
                        }
                    }
                    if (flag)
                    {
                        all_score_student += quastion.get_score();
                    }
                    all_score += quastion.get_score();
                }
                else
                {
                    if (quastion.Get_write_answer().get_true_answer_of_student())
                    {
                        all_score_student += quastion.get_score();
                    }
                    all_score += quastion.get_score();
                }
            }
        }
        public (int,  double) Getresult()
        {
            Calculate_percentages_of_test();
            Result();
            return (result, prosents);
        }

        public void Result()
        {
            if (prosents >= 85)
            {
                result = 5;
            }
            else if (prosents >= 70)
            {
                result = 4;
            }
            else if (prosents >= 51)
            {
                result = 3;
            }
            else
            {
                result = 2;
            }
        }
    }
    public class Quastion
    {
        public string text_quastion = "";
        public bool is_test = false;
        public List<Answer> answers = new List<Answer>();
        public Answer_write answer = null;
        int score = 1;
        public string Get_text_quastion()
        {
            return text_quastion;
        }
        public void Add_text_quastion(string text)
        {
            text_quastion = text;
        }
        public Quastion(bool is_test_quastion)
        {
            is_test = is_test_quastion;
        }
        public Quastion()
        {

        }
        public void Change_Quastion(Quastion a)
        {
            text_quastion = a.Get_text_quastion();
            is_test = a.Get_is_test();
            answers = a.Get_all_test();
            answer = a.Get_write_answer_in_this_type();
        }
        public bool Get_is_test()
        {
            return is_test;
        }
        public List<Answer> Get_all_test()
        {
            return answers;
        }
        public Answer Get_write_answer()
        {
            return answer.Return_Answer();
        }
        public Answer_write Get_write_answer_in_this_type()
        {
            return answer;
        }
        public int Get_write_answer_score()
        {
            //return answer.;
            return 0;
        }
        public Quastion(bool? isChecked)
        {
            if (isChecked == null)
            {
                throw new ArgumentNullException("Нет значения тестовый или письменный вопрос");
            }
            else
            {
                is_test = isChecked.Value;
            }
        }
        public void Set_is_test(bool value)
        {
            is_test = value;
        }
        public void Get_Answers_test(List<Answer> AT)
        {
            if (is_test)
            {
                foreach (Answer answer in AT)
                {
                    answers.Add(answer);
                }
            }
        }
        public void Get_Answers_write(Answer_write AW)
        {
            if (!is_test)
            {
                Get_write(AW);
            }
        }
        //метод проверки ввода ответа на верность всех ответов/ответа
        //метод добавления баллов за задание + система оценивания продумать
        //добавить сериализацию и десериализацию, подготовить классы + лучше написать на GET SET


        private void Get_write(Answer_write true_answer)
        {
            answer = true_answer;
        }
        public string Answers_of_quastion_out()
        {
            StringBuilder sb = new StringBuilder();
            if (is_test)
            {
                foreach (Answer AW in answers)
                {
                    sb.Append(AW.show_answer_with_out_student() + '\n');
                }
            }
            else
            {
                sb.Append(answer.return_answer().show_answer_with_out_student() + '\n');
            }
            sb.Append("________________________________________________" + '\n');
            return sb.ToString();
        }
        public int get_score()
        {
            return score;
        }
    }
    public class Answer
    {
        public bool answer_of_student = false;
        public bool is_true_answer = false;
        public string answer;

        public Answer() { }

        private string Delete_whitespase(string input)
        {
            int index_last = input.Length;
            while (input[index_last] == ' ')
            {
                index_last--;
            }
            int first_index = 0;
            StringBuilder true_input = new StringBuilder();
            while (input[first_index] == ' ')
            {
                first_index++;
            }
            for (int i = first_index; i < index_last; i++)
            {
                true_input.Append(input[i]);
            }
            return true_input.ToString();
        }
        public Answer(string input)
        {
            //answer = Delete_whitespase(input);
            answer = input;
        }
        
        public void set_true_answer_of_student()
        {
            answer_of_student = true;
        }
        public bool get_true_answer_of_student()
        {
            return answer_of_student;
        }
        public void do_flag_true()
        {
            is_true_answer = true;
        }
        public Answer(string input, bool True)
        {
            answer = input;
            this.is_true_answer = True;
        }
        public string Get_answer()
        {
            return answer;
        }
        public bool Get_flag()
        {
            return is_true_answer;
        }
        public void Change_Answer(string new_answer)
        {
            answer = new_answer;
        }
        public void Change_is_true_answer(bool value)
        {
            is_true_answer = value;
        }
        public string show_answer()
        {
            string output = answer;
            if (is_true_answer)
            {
                output += " " + "Верный ответ";
            }
            else
            {
                output += " " + "Неверный ответ";
            }
            if (answer_of_student)
            {
                output += "   " + "Ответ ученика:" + "Верный ответ";
            }
            else
            {
                output += "   " + "Ответ ученика:" + "Неверный ответ";
            }

            return output;
        }
        public string show_answer_with_out_student()
        {
            string output = answer;
            if (is_true_answer)
            {
                output += " " + "Верный ответ";
            }
            else
            {
                output += " " + "Неверный ответ";
            }

            return output;
        }

    }
    public class Answer_test
    {
        public List<Answer> answer;
        

        public Answer_test(List<Answer> list_input)
        {
            answer = new List<Answer>();
            foreach (var ans in list_input)
            {
                answer.Add(ans);
            }
        }
        public Answer_test()
        {
            answer = new List<Answer>();
        }
        public void Add_answers(Answer some_answer)
        {
            answer.Add(some_answer);
        }
        public List<Answer> Get_answers()
        {
            return answer;
        }
        
    }
    public class Answer_write
    {
        public Answer answer;

        public Answer_write(Answer some_answer)
        {
            answer = some_answer;
            answer.do_flag_true();
        }
        public Answer_write()
        {

        }
        public void Add_Answer(Answer some_answer)
        {
            answer = some_answer;
            answer.do_flag_true();
        }
        public Answer return_answer()
        {
            return answer;
        }
        public void Check_answer(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                int index_last = input.Length;
                while (input[index_last - 1] == ' ')
                {
                    index_last--;
                }
                int first_index = 0;
                StringBuilder true_input = new StringBuilder();
                while (input[first_index] == ' ')
                {
                    first_index++;
                }
                for (int i = first_index; i < index_last; i++)
                {
                    true_input.Append(input[i]);
                }
                if (answer.Get_answer().ToLower() == true_input.ToString().ToLower())
                {
                    answer.set_true_answer_of_student();
                }
            }
        }
        public Answer Return_Answer()
        {
            return answer;
        }
    }
    public static class SavingAndReadingTest
    {
        public static void SerialiseTest(List<Quastion> quastions, string filename)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Quastion>));
            if (!string.IsNullOrEmpty(filename))
            {
                using (FileStream fs = new FileStream(filename + ".xml", FileMode.OpenOrCreate))
                {
                    xmlSerializer.Serialize(fs, quastions);
                }
                MessageBox.Show("Тест успешно сохранён!");

            }
            else
            {
                MessageBox.Show("Не было указано имя теста!");
            }
        }
        public static List<Quastion> DeserialiseTest(string filename)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Quastion>));
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                List<Quastion> quastions = xmlSerializer.Deserialize(fs) as List<Quastion>;
                return quastions;
            }
        }
    }
}

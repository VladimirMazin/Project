using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_WPF
{
    public class Quastion
    {
        bool is_test = false;
        Answer_test answers = null;
        Answer_write answer = null;

        public Quastion(bool is_test_quastion)
        {
            is_test = is_test_quastion;
        }
        public void Get_Answers_test(Answer_test AT)
        {
            if(is_test)
            {
                Get_test(AT);
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
        
        private void Get_test(Answer_test test)
        {
            answers = test;
        }
        private void Get_write(Answer_write true_answer)
        {
            answer = true_answer;
        }
    }
    public class Answer
    {
        bool is_true_answer = false;
        string answer;
        public Answer(string input)
        {
            answer = input;
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

    }
    public class Answer_test
    {
        List<Answer> answer;
        public Answer_test(List<Answer> list_input)
        {
            answer = list_input;
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
        Answer answer;
        Answer_write(Answer some_answer)
        {
            answer = some_answer;
        }
        public bool Check_answer(String input)
        {
            if(answer.Get_answer() == input)
            {
                return true;
            }
            return false;
        }
    }
}

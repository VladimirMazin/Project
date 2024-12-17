using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Project_WPF;
using System.Collections.Generic;

namespace UnitTestForProjectWPF
{
    [TestClass]
    public class UnitTestForQuastion
    {
        [TestMethod]
        public void show_answerTestMethod1()
        {
            Answer a = new Answer("сентябрь", true);
            a.set_true_answer_of_student();
            string s = a.show_answer();
            Assert.AreEqual(s, "сентябрь Верный ответ   Ответ ученика:Верный ответ");
        }
        [TestMethod]
        public void show_answerTestMethod2()
        {
            Answer a = new Answer("сентябрь", true);
            string s = a.show_answer();
            Assert.AreEqual(s, "сентябрь Верный ответ   Ответ ученика:Неверный ответ");
        }
        [TestMethod]
        public void show_answer_with_out_studentTestMethod1()
        {
            Answer a = new Answer("сентябрь", true);
            a.set_true_answer_of_student();
            string s = a.show_answer_with_out_student();
            Assert.AreEqual(s, "сентябрь Верный ответ");
        }
        [TestMethod]
        public void show_answer_with_out_studentTestMethod2()
        {
            Answer a = new Answer("сентябрь", false);
            string s = a.show_answer_with_out_student();
            Assert.AreEqual(s, "сентябрь Неверный ответ");
        }
        [TestMethod]
        public void Check_answerTestMethod1()
        {
            string s = string.Empty;
            Answer_write stringanswer = new Answer_write(new Answer("сентябрь",true));
            stringanswer.Check_answer(s);
            bool a = stringanswer.answer.get_true_answer_of_student();
            Assert.AreEqual(a, false);
        }
        [TestMethod]
        public void Check_answerTestMethod2()
        {
            string s = "октябрь";
            Answer_write stringanswer = new Answer_write(new Answer("сентябрь", true));
            stringanswer.Check_answer(s);
            bool a = stringanswer.answer.get_true_answer_of_student();
            Assert.AreEqual(a, false);
        }
        [TestMethod]
        public void Check_answerTestMethod3()
        {
            string s = "   сентябрь    ";
            Answer_write stringanswer = new Answer_write(new Answer("сентябрь", true));
            stringanswer.Check_answer(s);
            bool a = stringanswer.answer.get_true_answer_of_student();
            Assert.AreEqual(a, true);
        }
        [TestMethod]
        public void Change_is_true_answerTestMethod1()
        {
            Answer stringanswer = new Answer("сентябрь", true);
            stringanswer.Change_is_true_answer(false);
            Assert.AreEqual(stringanswer.is_true_answer, false);
        }
        [TestMethod]
        public void Change_AnswerTestMethod1()
        {
            Answer stringanswer = new Answer("сентябрь", true);
            stringanswer.Change_Answer("октябрь");
            Assert.AreEqual(stringanswer.Get_answer(), "октябрь");
        }
        [TestMethod]
        public void do_flag_trueTestMethod1()
        {
            Answer stringanswer = new Answer("сентябрь", false);
            stringanswer.do_flag_true();
            Assert.AreEqual(stringanswer.Get_flag(), true);
        }
        [TestMethod]
        public void Add_AnswerTestMethod1()
        {
            Answer_write stringanswer = new Answer_write(new Answer("сентябрь", true));
            Answer a = new Answer("октябрь", true);
            stringanswer.Add_Answer(a);
            Assert.AreEqual(stringanswer.return_answer(), a);
        }
        [TestMethod]
        public void Add_AnswersTestMethod1()
        {
            Answer_test stringanswer = new Answer_test();
            Answer a = new Answer("сентябрь", true);
            Answer b = new Answer("октябрь", true);
            Answer c = new Answer("ноябрь", true);
            stringanswer.Add_answers(a);
            stringanswer.Add_answers(b);
            stringanswer.Add_answers(c);
            List<Answer> l = new List<Answer>{a,b,c};
            int i = 0;
            foreach(var item in l)
            {
                Assert.AreEqual(stringanswer.Get_answers()[i], item);
                i++;
            }
        }
        [TestMethod]
        public void Add_text_quastionTestMethod1()
        {
            Quastion quastion = new Quastion(true);
            quastion.Add_text_quastion("Месяцы");
            Assert.AreEqual(quastion.Get_text_quastion(), "Месяцы");
        }
        [TestMethod]
        public void Change_QuastionTestMethod1()
        {
            Quastion quastion = new Quastion();
            quastion.Add_text_quastion("Месяцы");
            Quastion changequastion = new Quastion(true);
            changequastion.Add_text_quastion("Времена года");
            Answer a = new Answer("сентябрь", true);
            Answer b = new Answer("октябрь", true);
            Answer c = new Answer("ноябрь", true);
            List<Answer> l = new List<Answer> { a, b, c };
            changequastion.Get_Answers_test(l);
            quastion.Change_Quastion(changequastion);
            Assert.AreEqual(quastion.Get_text_quastion(), "Времена года");
            Assert.AreEqual(quastion.Get_is_test(), true);
            int i = 0;
            foreach(var item in l)
            {
                Assert.AreEqual(item, quastion.answers[i]);
                i++;
            }
        }
        [TestMethod]
        public void Get_write_answerTestMethod1()
        {
            Quastion quastion = new Quastion(false);
            Answer x = new Answer("сентябрь", true);
            Answer_write a = new Answer_write(x);
            quastion.Get_Answers_write(a);
            Assert.AreEqual(x,quastion.Get_write_answer());
            Assert.AreEqual(a,quastion.Get_write_answer_in_this_type());
        }
        [TestMethod]
        public void Set_is_testTestMethod1()
        {
            Quastion quastion = new Quastion(false);
            quastion.Set_is_test(true);
            Assert.AreEqual(quastion.Get_is_test(),true);
        }
        [TestMethod]
        public void Get_write_answer_scoreTestMethod1()
        {
            Quastion quastion = new Quastion(false);
            Assert.AreEqual(quastion.Get_write_answer_score(), 0);
        }

        [TestMethod]
        public void Get_count_quastionsTestMethod1()
        {
            Quastion quastion1 = new Quastion(false);
            Quastion quastion2 = new Quastion(false);
            Test t = new Test();
            List<Quastion> l = new List<Quastion>{ quastion1 , quastion2 };
            t.Get_all_quastions(l);
            Assert.AreEqual(t.Get_count_quastions(), 2);
        }
        [TestMethod]
        public void Get_quastionsTestMethod1()
        {
            Quastion quastion1 = new Quastion(false);
            Quastion quastion2 = new Quastion(false);
            Test t = new Test();
            List<Quastion> l = new List<Quastion> { quastion1, quastion2 };
            t.Get_all_quastions(l);
            List<Quastion>  l1 = t.Get_quastions();
            int i = 0;
            foreach(var item in l)
            {
                Assert.AreEqual(item, l1[i]);
                i++;
            }
            Assert.AreEqual(t.Get_concretical_quastion(1), l[1]);
        }
        
    }
}

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
using System.Windows.Shapes;

namespace Project_WPF
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window_Quastions : Window
    {
        public Window_Quastions()
        {
            InitializeComponent();
        }
        public Window_Quastions(List<Quastion> quastions)
        {
            InitializeComponent();
            foreach (var quastion in quastions)
            {
                text_out.Text+=(quastion.Answers_of_quastion_out());
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
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

namespace MemoryTitles_C_sharp
{
    /// <summary>
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class Statistics : Window
    {
        public Statistics()
        {
            InitializeComponent();
        }
        public Statistics(User user)
        {
            InitializeComponent();
           username.Text="username:"+user.m_username.ToString();
            string[] lines = File.ReadAllLines(@"D:\Facultate\c++\MemoryTitles_C-sharp\MemoryTitles_C-sharp\users.txt");


            for (int i = 0; i < lines.Length; i++)
            {
                string[] fields = lines[i].Split(',');
                if (fields[0] == user.m_username)
                {
                    gamesplayed.Text="Games played: "+fields[2];
                    gameswon.Text="Games Won: "+fields[3];
                }
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }
    }
}

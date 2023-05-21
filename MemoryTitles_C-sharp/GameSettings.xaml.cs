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

namespace MemoryTitles_C_sharp
{
    /// <summary>
    /// Interaction logic for GameSettings.xaml
    /// </summary>
    public partial class GameSettings : Window
    {
        User user;
        public GameSettings()
        {
            InitializeComponent();
        }
        public GameSettings(User user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void Button_Default_Click(object sender, RoutedEventArgs e)
        {
            Game game=new Game(5,5,user);
            game.Show();
            this.Close();
        }

        private void Button_Custom_Click(object sender, RoutedEventArgs e)
        {
            int col = int.Parse(txtColumns.Text);
            int rows = int.Parse(txtRows.Text);
            if ((col > 1) && (col < 8) && (rows < 8) && (rows > 1))
            {

                Game game = new Game(int.Parse(txtRows.Text), int.Parse(txtColumns.Text), user);
                this.Close();
                game.Show();
            }
            else MessageBox.Show("Dimensiuni gresite");
        }
    }
}

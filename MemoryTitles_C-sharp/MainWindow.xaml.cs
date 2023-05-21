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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace MemoryTitles_C_sharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
            LoadList();


        }
        //  XmlSerializer serializer = new XmlSerializer(typeof(User));
        List<User> userList = new List<User>();
        private void LoadList()
        {
            /*string filePath = @"D:\Facultate\c++\MemoryTitles_C-sharp\MemoryTitles_C-sharp\users.txt";
            XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
            List<User> loadedUsers;

            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    loadedUsers = (List<User>)serializer.Deserialize(fileStream);
                }
            }
            catch (FileNotFoundException)
            {
                loadedUsers = new List<User>();
            }

            userListBox.ItemsSource = loadedUsers;*/

            string[] lines = File.ReadAllLines(@"D:\Facultate\c++\MemoryTitles_C-sharp\MemoryTitles_C-sharp\users.txt");


            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                User user = new User(parts[0], parts[1]);
                userList.Add(user);
            }
            userListBox.ItemsSource = userList;
        }

        private void Button_Click_NewUser(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            this.Close();
            register.Show();


        }

        private void Button_Click_DeleteUser(object sender, RoutedEventArgs e)
        {
            if (userListBox.SelectedItem != null)
            {
                User user = (User)userListBox.SelectedItem;
                userList.Remove(user);
                userListBox.Items.Refresh();

                using (StreamWriter writer = new StreamWriter(@"D:\Facultate\c++\MemoryTitles_C-sharp\MemoryTitles_C-sharp\users.txt"))
                {
                    foreach (User u in userList)
                    {
                        writer.WriteLine($"{u.m_username},{u.m_picture},{u.m_gamesPlayed},{u.m_gamesWon}");
                    }
                }
            }
            else MessageBox.Show("selecteaza un utilizator");
        }

        private void Button_Click_Play(object sender, RoutedEventArgs e)
        {
            if (userListBox.SelectedItem != null)
            {
                User user = (User)userListBox.SelectedItem;

                
                string[] lines = File.ReadAllLines(@"D:\Facultate\c++\MemoryTitles_C-sharp\MemoryTitles_C-sharp\users.txt");

               
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] fields = lines[i].Split(',');
                    if (fields[0] == user.m_username)
                    {
                        int gamesPlayed = int.Parse(fields[2]) + 1;
                        lines[i] = $"{fields[0]},{fields[1]},{gamesPlayed},{fields[3]}";
                        break;
                    }
                }

                
                File.WriteAllLines(@"D:\Facultate\c++\MemoryTitles_C-sharp\MemoryTitles_C-sharp\users.txt", lines);

                GameSettings settings = new GameSettings(user);
                settings.Show();
                this.Close();
            }
            else MessageBox.Show("selecteaza un utilizator");
        }
            private void Button_Click_Cancel(object sender, RoutedEventArgs e)
            {
                this.Close();

            }
        private void Button_Click_Statistics(object sender, RoutedEventArgs e)
        {
            if (userListBox.SelectedItem != null)
            {
                Statistics statistics = new Statistics((User)userListBox.SelectedItem);
                statistics.Show();
                this.Close();
            }
            else MessageBox.Show("selecteaza un utilizator");
        }

            private void UserListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
            if (userListBox.SelectedIndex >= 0)
            {
                User selectedUser = (User)userListBox.SelectedItem;
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(selectedUser.m_picture);
                image.EndInit();
                userImage.Source = image;
            }
            
            }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Nume:Olteanu Razvan\nGrupa:313\nSpecializarea:Informatica Aplicata");
        }
    }
}

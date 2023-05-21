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
using System.Xml.Serialization;

namespace MemoryTitles_C_sharp
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        int currentImageIndex = 0;
        List<string> imagePaths = new List<string>();
        string directoryPath = @"D:\Facultate\c++\MemoryTitles_C-sharp\MemoryTitles_C-sharp\images";
        string[] imageFiles;
        XmlSerializer serializer = new XmlSerializer(typeof(User));
        //foreach (string imagePath in imageFiles)

        // imagePaths.Add(imagePath);

        public Register()
        {
            InitializeComponent();
            try
            {
                imageFiles = Directory.GetFiles(directoryPath, "*.png");
            }
            catch (Exception ex)
            {
                // Handle the exception
                Console.WriteLine("Error reading directory: " + ex.Message);
                return;
            }
            foreach (string imagePath in imageFiles)
            {
                imagePaths.Add(imagePath);
            }
            picHolder.Source = new BitmapImage(new Uri(imagePaths[0]));
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {   
                 currentImageIndex--;
            if (currentImageIndex < 0)
            {

                currentImageIndex = imagePaths.Count()-1;
            }
            picHolder.Source = new BitmapImage(new Uri(imagePaths[currentImageIndex]));
            
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            {
                currentImageIndex++;
               if(currentImageIndex >= imagePaths.Count)
                {
                    currentImageIndex = 0;
                }

                picHolder.Source = new BitmapImage(new Uri(imagePaths[currentImageIndex]));
            }
        }

        private bool UserExists(string username)
        {
            /*  // XmlSerializer serializer = new XmlSerializer(typeof(User));
              //List<User> userList;

             *//* using (FileStream fileStream = new FileStream((@"D:\Facultate\c++\MemoryTitles_C-sharp\MemoryTitles_C-sharp\users.txt"), FileMode.OpenOrCreate))
              {
                  try
                  {
                      //userList = (List<User>)serializer.Deserialize(fileStream);
                  }
                  catch (InvalidOperationException)
                  {
                      userList = new List<User>();
                  }
              }*//*

              foreach (User user in userList)
              {
                  if (user.m_username == username)
                  {
                      return true;
                  }
              }

              returnfalse;*/
            using (StreamReader reader = new StreamReader(@"D:\Facultate\c++\MemoryTitles_C-sharp\MemoryTitles_C-sharp\users.txt"))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length >= 1 && parts[0].Trim() == username.Trim())
                    {
                        return true;
                    }
                }
            }


            return false;
        }
          private void BtnSave_Click(object sender, RoutedEventArgs e)
          {

            /*              User user = new User();
                          user.m_username = usernameBox.Text;
                          user.m_picture = imagePaths[currentImageIndex];
                          if (UserExists(usernameBox.Text))
                          {
                              MessageBox.Show("Username already exists!");
                              return;
                          }

                          try
                          {
                              var serializer = new XmlSerializer(typeof(User));
                              using (var writer = new StreamWriter("users.xml"))
                              {
                                  serializer.Serialize(writer, user);
                              }

                             *//* using (StreamWriter writer = new StreamWriter(@"D:\Facultate\c++\MemoryTitles_C-sharp\MemoryTitles_C-sharp\users.xml", true))
                              {
                                  serializer.Serialize(writer, user);
                                  //writer.WriteLine($"{user.m_username},{user.m_picture},{user.m_gamesPlayed},{user.m_gamesWon}");
                              }*//*



                        MainWindow window = new MainWindow();
                            window.Show();
                            this.Close();
                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show($"Error saving user information: {ex.Message}");
                        }*/
            
            if (UserExists(usernameBox.Text))
            {
                MessageBox.Show("Username already exists!");
                return;
            }
            User user = new User(usernameBox.Text, imagePaths[currentImageIndex]);

            try
            {

                using (StreamWriter writer = new StreamWriter(@"D:\Facultate\c++\MemoryTitles_C-sharp\MemoryTitles_C-sharp\users.txt", true))
                {
                    writer.WriteLine($"{user.m_username},{user.m_picture},{user.m_gamesPlayed},{user.m_gamesWon}");
                }



                MainWindow window = new MainWindow();
                window.Show();
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error saving user information: {ex.Message}");
            }
        }
    }

}


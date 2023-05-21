using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        public Board board;
        string directoryPath = @"D:\Facultate\c++\MemoryTitles_C-sharp\MemoryTitles_C-sharp\Resources\GameImages";
        public int numRows, numCols;
        public User user;
        public Game()
        {
            InitializeComponent();
        }
        public Game(int numRows, int numCols, User user)
        {

            InitializeComponent();
            this.numRows = numRows;
            this.numCols = numCols;
            board = new Board(numRows, numCols);
            DataContext = board;
            this.user = user;

        }
       
        private void SaveGame(string fileName)
        {
            try
            {
                // Create a FileStream to serialize the object to the file
                FileStream fileStream = new FileStream(fileName, FileMode.Create);
                BinaryFormatter formatter = new BinaryFormatter();

                // Serialize the object to the file stream
                formatter.Serialize(fileStream, board);

                // Close the file stream
                fileStream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving game: " + ex.Message);
            }
        }//pentru toti

          private void btnSave_Click(object sender, RoutedEventArgs e)
          {
              var saveDialog = new Microsoft.Win32.SaveFileDialog();
              saveDialog.FileName = "SaveFile"; // Default file name
              saveDialog.DefaultExt = ".sav"; // Default file extension
              saveDialog.Filter = "Save files (*.sav)|*.sav"; // Filter files by extension

              // Show the save dialog box and get the selected file name
              Nullable<bool> result = saveDialog.ShowDialog();

              if (result == true)
              {
                  string filePath = saveDialog.FileName;

                  // Check if the file already exists
                  if (File.Exists(filePath))
                  {
                      // Prompt the user to overwrite the existing file or not
                      MessageBoxResult overwriteResult = MessageBox.Show("The file already exists. Do you want to overwrite it?", "Save", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                      if (overwriteResult == MessageBoxResult.Yes)
                      {
                          using (Stream stream = File.Open(filePath, FileMode.Create))
                          {
                              var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                              binaryFormatter.Serialize(stream, board);
                          }
                      }
                  }
                  else // File doesn't exist, create a new file
                  {
                      using (Stream stream = File.Create(filePath))
                      {
                          var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                          binaryFormatter.Serialize(stream, board);
                      }
                  }
              }
          }//pentru toti
        private void LoadGame(string fileName)
        {
            try
            {
                // Create a FileStream to deserialize the object from the file
                FileStream fileStream = new FileStream(fileName, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();

                // Deserialize the object from the file stream
                board = (Board)formatter.Deserialize(fileStream);
                DataContext = board;

                // Close the file stream
                fileStream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading game: " + ex.Message);
            }
        }//pentru toti
        private void btnLoad_Click(object sender, RoutedEventArgs e)
         {
             // Display an open dialog to select the file to load
             OpenFileDialog openDialog = new OpenFileDialog();
             openDialog.Filter = "Game save files (*.sav)|*.sav";
             if (openDialog.ShowDialog() == true)
             {
                 // Load the game from the selected file
                 LoadGame(openDialog.FileName);
             }
         }//pentru toti
        private void winGame()
        {
            if (board.verifyWin() == true)
            {
                //display win message in a message box
                if (board.level + 1 == 3)
                {
                    MessageBox.Show("Won");
                    string[] lines = File.ReadAllLines(@"D:\Facultate\c++\MemoryTitles_C-sharp\MemoryTitles_C-sharp\users.txt");


                    for (int i = 0; i < lines.Length; i++)
                    {
                        string[] fields = lines[i].Split(',');
                        if (fields[0] == user.m_username)
                        {
                            int gamesWon = int.Parse(fields[3]) + 1;
                            lines[i] = $"{fields[0]},{fields[1]},{fields[2]},{gamesWon}";
                            break;
                        }
                    }


                    File.WriteAllLines(@"D:\Facultate\c++\MemoryTitles_C-sharp\MemoryTitles_C-sharp\users.txt", lines);
                    ///GameSettings settings=new GameSettings(user);
                    MainWindow window=new MainWindow();
                    window.Show();
                    this.Close();
                }
                else
                {
                    board.level = board.level + 1;
                    int lvl = board.level;
                    //create a new game
                    board = new Board(numRows, numCols, lvl);
                    DataContext = board;
                    //Img.Items.Refresh();
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Tile tile = button.DataContext as Tile;
            //make the tile visible
            //refresh the items
            if (tile.ImagePath == null)
            {
                winGame();
                return;
            }
            if (board.FirstClicked == null)
            {
                board.FirstClicked = tile;
                tile.Visibility = Visibility.Visible;
                Img.Items.Refresh();
            }
            else
            {
                board.SecondClicked = tile;
                tile.Visibility = Visibility.Visible;
                Img.Items.Refresh();
                if (board.FirstClicked.ImagePath == board.SecondClicked.ImagePath)
                {
                    board.FirstClicked = null;
                    board.SecondClicked = null;
                    winGame();
                }
                else
                {
                    Task.Delay(TimeSpan.FromSeconds(0.5)).ContinueWith(task =>
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(() =>
                        {
                            // make the tiles invisible
                            board.FirstClicked.Visibility = Visibility.Collapsed;
                            board.SecondClicked.Visibility = Visibility.Collapsed;
                            board.FirstClicked = null;
                            board.SecondClicked = null;
                            Img.Items.Refresh();
                        });
                    });
                }
            }
        }
    }
}

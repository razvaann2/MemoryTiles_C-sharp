using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MemoryTitles_C_sharp
{
    public class GameClass
    {

        public List<List<Tile>> TileArray { get; set; }
        public Tile FirstClicked { get; set; }
        public Tile SecondClicked { get; set; }
        public int rows { get; set; }
        public int columns { get; set; }

        public int level { get; set; }

        public GameClass()
        {

        }
        public GameClass(int nrrows, int nrcolumns, int level = 0)
        {
            string directoryPath = @"D:\Facultate\c++\MemoryTitles_C-sharp\MemoryTitles_C-sharp\Resources\GameImages";
            string[] files = Directory.GetFiles(directoryPath);
            FirstClicked = null;
            SecondClicked = null;
            rows = nrrows;
            columns = nrcolumns;
            this.level = level;
            TileArray = new List<List<Tile>>();
            for (int i = 0; i < nrrows; i++)
            {
                TileArray.Add(new List<Tile>());
                for (int j = 0; j < nrcolumns; j++)
                {
                    //populate the list with empty tiles
                    TileArray[i].Add(new Tile());
                }
            }
            PopulateBoard();
            this.level = level;
        }

        private void PopulateBoard()
        {
            Random random = new Random();
            string directoryPath = @"D:\Facultate\c++\MemoryTitles_C-sharp\MemoryTitles_C-sharp\Resources\GameImages";
            string[] files = Directory.GetFiles(directoryPath);
            int n = rows * columns;
            String[] values = new string[n];

            // fill the array with pairs of values
            for (int i = 0; i < n / 2; i++)
            {
                values[2 * i] = files[i];
                values[2 * i + 1] = files[i];
            }

            // shuffle the values randomly
            for (int i = 0; i < n; i++)
            {
                int j = random.Next(n);
                string temp = values[i];
                values[i] = values[j];
                values[j] = temp;
            }

            // populate the board with the shuffled values
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    //create a new tile
                    Tile tile = new Tile();
                    tile.ImagePath = values[i * columns + j];
                    tile.Row = i;
                    tile.Col = j;
                    //add the image to the list
                    TileArray[i][j] = tile;
                }
            }
        }
        public void refreshMatrix()
        {
            TileArray = new List<List<Tile>>();
            for (int i = 0; i < rows; i++)
            {
                TileArray.Add(new List<Tile>());
                for (int j = 0; j < columns; j++)
                {
                    TileArray[i].Add(new Tile());
                }
            }
            PopulateBoard();
        }
        public bool verifyWin()
        {
            int nullCounter = 0;
            int counter = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (TileArray[i][j].ImagePath == null)
                    {
                        nullCounter++;
                    }
                    else if (TileArray[i][j].Visibility == Visibility.Collapsed)
                    {
                        counter++;
                    }

                }
            }
            if (nullCounter == 1)
            {
                if (counter == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (counter == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}

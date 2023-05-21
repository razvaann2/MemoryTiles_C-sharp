using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MemoryTitles_C_sharp
{
    [Serializable]
    public class Board
    {

        public List<List<Tile>> m_board { get; set; }
        public Tile FirstClicked { get; set; }
        public Tile SecondClicked { get; set; }
        public int rows { get; set; }
        public int columns { get; set; }
        public int level { get; set; }

        public Board()
        {

        }
        public Board(int numRows,int numCol,int level=0)
        {
            string directoryPath = @"D:\Facultate\c++\MemoryTitles_C-sharp\MemoryTitles_C-sharp\Resources\GameImages";
            string[] files = Directory.GetFiles(directoryPath);
            FirstClicked = null;
            SecondClicked = null;
            rows = numRows;
            columns = numCol;
            this.level = level;
            m_board = new List<List<Tile>>();
            for(int i=0;i<rows;i++)
            {
                m_board.Add(new List<Tile>());
                for(int j=0;j<columns;j++)  
                    m_board[i].Add(new Tile());
            }
            PopulateBoard();
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
                    m_board[i][j] = tile;
                }
            }
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if(m_board[i][j].ImagePath==null)
                    {
                        m_board[i][j].ImagePath = @"D:\Facultate\c++\MemoryTitles_C-sharp\MemoryTitles_C-sharp\Resources\unavailable.png";
                        m_board[i][j].Visibility = Visibility.Visible;

                    }
                }
            }
        }
        public void refreshMatrix()
        {
            m_board = new List<List<Tile>>();
            for (int i = 0; i < rows; i++)
            {
                m_board.Add(new List<Tile>());
                for (int j = 0; j < columns; j++)
                {
                    m_board[i].Add(new Tile());
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
                    if  (m_board[i][j].ImagePath == null)
                    {
                        nullCounter++;
                    }
                    else if (m_board[i][j].Visibility == Visibility.Collapsed)
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

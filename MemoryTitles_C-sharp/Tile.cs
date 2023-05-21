using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MemoryTitles_C_sharp
{   [Serializable]
    public class Tile
    {
        public string ImagePath { get; set; }

        public bool IsFlipped { get; set; }

        public Visibility Visibility
        { get; set; }

        public int Row { get; set; }
        public int Col { get; set; }
        public bool isMatched { get; set; }

        public Tile()
        {
            Visibility = Visibility.Collapsed;
            IsFlipped = false;
        }
    }
}

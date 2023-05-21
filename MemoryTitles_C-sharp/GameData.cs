using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryTitles_C_sharp
{
    [Serializable]
    public class GameData
    {
        public Board Board { get; set; }
        public User User { get; set; }

        public GameData(Board board, User user)
        {
            Board = board;
            User = user;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryTitles_C_sharp
{   
    [Serializable]
    public class User
    {
        public string m_username { get; set; }
        public string m_picture { get; set; }
        public int m_gamesPlayed { get; set; }
        public int m_gamesWon { get; set; }


        public User() { }
        public User(string m_username, string m_picture)
        {
            this.m_username = m_username;
            this.m_picture = m_picture;
            this.m_gamesPlayed = 0;
            this.m_gamesWon = 0;
        }
        public static string GetFilePath()
        {
            return @"D:\Facultate\c++\MemoryTitles_C-sharp\MemoryTitles_C-sharp\users.xml";
        }
    }
}

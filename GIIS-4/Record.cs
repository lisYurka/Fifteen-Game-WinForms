using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIIS_4
{
    [Serializable]
    public class Record
    {
        public string PlayerName { get; set; }
        public int PlayerMoves { get; set; }
        public string PlayerTime { get; set; }
        public string PlayerLevel { get; set; }
        public Record(string name, int moves, string time, string level)
        {
            PlayerName = name;
            PlayerMoves = moves;
            PlayerTime = time;
            PlayerLevel = level;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CourseWork_OOP_Phase1_.DTO
{
    [Serializable]
    [XmlRoot("GameBoard")]
    public class GameBoardDTO
    {
        [XmlElement("Columns")]
        public int Columns { get; set; }

        [XmlElement("Rows")]
        public int Rows { get; set; }

        [XmlElement("FirstPlayerChip")]
        public Enums.Chip FirstPlayerChip { get; set; }

        [XmlElement]
        public Enums.Chip CurrentPlayerChip { get; set; }

        [XmlArray("GameChips")]
        [XmlArrayItem("Row")]
        public List<List<Enums.Chip>> GameChips { get; set; }

        [XmlArray("Score")]
        [XmlArrayItem("Chip")]
        public List<Enums.Chip> Score { get; set; }

    }

    public static class GameOperations
    {
        public static List<List<Enums.Chip>> GetGameChipsArray(this GameLogic gameboard)
        {
            return Enumerable.Range(0, gameboard.Rows)
                .Select(row => Enumerable.Range(0, gameboard.Columns).Select(col => gameboard[row, col]).ToList())
                .ToList();
        }
    }
}

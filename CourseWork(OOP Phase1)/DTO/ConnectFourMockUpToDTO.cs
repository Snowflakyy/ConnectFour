using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CourseWork_OOP_Phase1_.DTO
{
    [Serializable]
    public class ConnectFourMockUpToDTO
    {
        [XmlElement("HorizontalPadding")]
        public int BoardHorizontalPadding { get; set; }
        [XmlElement("VerticalPadding")]
        public int BoardVerticalPadding { get; set; }
        [XmlElement("GridSquareHeight")]
        public int GridSquareHeight { get; set; }
        [XmlElement("GridSquareWidth")]
        public int GridSquareWidth { get; set; }
        [XmlElement("LastPlacedCol")]
        public int LastPlacedCol { get; set; }
        [XmlElement("ChipRadius")]
        public int ChipRadius { get; set; }
        [XmlElement("ShapesType")]
        public string ShapesType { get; set; }
        [XmlElement("StartX")]
        public int StartX { get; set; }
        [XmlElement("StartY")]
        public int StartY { get; set; }
        [XmlElement("GameBoardData")]
        public string GameBoardData { get; set; }
        [XmlElement("Player1Color")]
        public string Player1Color { get; set; }
        [XmlElement("Player2Color")]
        public string Player2Color { get; set; }
        [XmlElement("CurrentColumn")]
        public int CurrentColumn { get; set; }
        [XmlElement("CurrentRow")]
        public int CurrentRow { get; set; }
        [XmlElement("padding")]
        public int Padding { get; set; }
        [XmlElement("IsPaused")]
        public bool IsPaused { get; set; }
        [XmlElement("isExited")]
        public bool isExited { get; set; }


        [XmlArray("ButtonType")]
        [XmlArrayItem("Button")]
        public List<string> ButtonsStat { get; set; }

        [XmlArray("Shapes")]
        [XmlArrayItem("Shape")]
        public List<string> ShapesList { get; set; }
        [XmlArray("OperationsUndo")]
        [XmlArrayItem("row,column,Chip")]
        public List<string> OperationUndo { get; set; }
        [XmlArray("OperationsRedo")]
        [XmlArrayItem("row,column,Chip")]
        public List<string> OperationLastPlacedChips { get; set; }

        [XmlElement("Pauseform")]
        public string PauseForm { get; set; }
        [XmlElement("PencilBounds")]
        public string PencilBounds { get; set; }

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace CourseWork_OOP_Phase1_.DTO
{
    public class ConnectFourMockUpMapper
    {
        public static ConnectFourMockUpToDTO ToDto(ConnectFourMockUp connectFour)
        {
            if (connectFour == null) throw new ArgumentNullException(nameof(connectFour));
            //return new ConnectFourMockUpToDTO
            //{
            //    BoardHorizontalPadding = connectFour.BoardHorizontalPadding,
            //    BoardVerticalPadding = connectFour.BoardVerticalPadding,
            //    GridSquareHeight = connectFour.GridSquareHeight,
            //    GridSquareWidth = connectFour.GridSquareWidth,
            //    LastPlacedCol = connectFour.LastPlacedCol,
            //    ChipRadius = connectFour.ChipRadius,
            //    ShapesType = connectFour._shapes.ToString(),
            //    StartX = connectFour.StartX,
            //    StartY = connectFour.StartY,
            //    GameBoardData = connectFour.GameBoard != null ? SerializeGameBoard(connectFour.GameBoard) : null,
            //    Player1Color = connectFour.player1Col != null ? ColorTranslator.ToHtml(connectFour.player1Col) : null,
            //    Player2Color = connectFour.player2Col != null ? ColorTranslator.ToHtml(connectFour.player2Col) : null,
            //    CurrentColumn = connectFour.CurrentColumn,
            //    CurrentRow = connectFour.CurrentRow,
            //    Padding = connectFour.padding,
            //    isExited = connectFour.isExited,
            //    IsPaused = connectFour.isPaused,
            //    ButtonsStat = connectFour._buttonsStat.ConvertAll(b => b.Text),
            //    ShapesList = connectFour._shapesList.ConvertAll(s => s.ToString()),
            //    OperationUndo = connectFour._operationUndo != null ? ConvertOperations(connectFour._operationUndo) : null,
            //    OperationLastPlacedChips = connectFour._operationLastPlacedChips != null ? ConvertOperations(connectFour._operationLastPlacedChips) : null,
            //    PauseForm = connectFour._pauseform != null ? SerializePauseForm(connectFour._pauseform) : null,
            //    PencilBounds = connectFour.pencilBounds != null ? SerializePen(connectFour.pencilBounds) : null
            //};
            return new ConnectFourMockUpToDTO
            {
                BoardHorizontalPadding = connectFour.BoardHorizontalPadding,
                BoardVerticalPadding = connectFour.BoardVerticalPadding,
                GridSquareHeight = connectFour.GridSquareHeight,
                GridSquareWidth = connectFour.GridSquareWidth,
                LastPlacedCol = connectFour.LastPlacedCol,
                ChipRadius = connectFour.ChipRadius,
                ShapesType = connectFour._shapes.ToString(),
                StartX = connectFour.StartX,
                StartY = connectFour.StartY,
                GameBoardData = SerializeGameBoard(connectFour.GameBoard),
                Player1Color = ColorTranslator.ToHtml(connectFour.player1Col),
                Player2Color = ColorTranslator.ToHtml(connectFour.player2Col),
                CurrentColumn = connectFour.CurrentColumn,
                CurrentRow = connectFour.CurrentRow,
                Padding = connectFour.padding,
                IsPaused = connectFour.isPaused,
                isExited = connectFour.isExited,
                ButtonsStat = connectFour._buttonsStat.ConvertAll(b=>b.Text),
                ShapesList = connectFour._shapesList.ConvertAll(s=>s.ToString()),
                OperationUndo = connectFour._operationUndo != null ? ConvertOperations(connectFour._operationUndo) : null,
                OperationLastPlacedChips = connectFour._operationLastPlacedChips != null ? ConvertOperations(connectFour._operationLastPlacedChips) : null,
                PauseForm = connectFour._pauseform != null ? SerializePauseForm(connectFour._pauseform) : null,
                PencilBounds = connectFour.pencilBounds != null ? SerializePen(connectFour.pencilBounds) : null
            };

        }

        private static List<string> ConvertOperations(OrderedDictionary operations)
        {
            var result = new List<string>();
            foreach (DictionaryEntry entry in operations)
            {
                var key = (Tuple<int, int>)entry.Key;
                var value = (Enums.Chip)entry.Value;
                result.Add($"({key.Item1},${key.Item2}:{value}");
            }
            return result;
        }

        private static string SerializeGameBoard(GameLogic gameboard)
        {
            var gameData = new GameBoardDTO()
            {
                Columns = gameboard.Columns,
                Rows = gameboard.Rows,
                FirstPlayerChip = gameboard.FirstPlayerChip,
                CurrentPlayerChip = gameboard.CurrentChipTurn,
                GameChips = gameboard.GetGameChipsArray(),
                Score = gameboard._score,
            };
            var serializer = new XmlSerializer(typeof(GameBoardDTO));
            using (var stringWriter = new StringWriter())
            {
                serializer.Serialize(stringWriter, gameData);
                return stringWriter.ToString();
            }
        }

        private static string SerializePauseForm(PauseForm pauseform)
        {
            var pauseData = new PauseFormDTO();
            var serializer = new XmlSerializer(typeof(PauseFormDTO));
            using (var stringwriter = new StringWriter())
            {
                serializer.Serialize(stringwriter, pauseData);
                return stringwriter.ToString();
            }

        }

        private static string SerializePen(Pen pencilbound)
        {
            var pencilData = new pencilBoundsDTO
            {
                pencilColour = pencilbound.Color,
                LineThickness = pencilbound.Width

            };
            var serializer = new XmlSerializer(typeof(pencilBoundsDTO));
            using (var stringwriter = new StringWriter())
            {
                serializer.Serialize(stringwriter, pencilData);
                return stringwriter.ToString();
            }
        }

    }
}

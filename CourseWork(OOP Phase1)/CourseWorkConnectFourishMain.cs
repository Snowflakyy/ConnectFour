using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CourseWork_OOP_Phase1_.Enums;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using CourseWork_OOP_Phase1_.DTO;
using Newtonsoft.Json;
using System.Collections;

namespace CourseWork_OOP_Phase1_
{
    public partial class CourseWorkConnectFourishMain : Form
    {
        

        
        private ConnectFourMockUp connectFourGUI;
        private Form2 startMenu;

        private readonly string prefixTitle;
        public CourseWorkConnectFourishMain()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer,
                true);
            this.TopMost = true;
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.Opacity = 0;

            
            startMenu = new Form2();
            startMenu.Show();
            prefixTitle = Text;

            InitializeComponent();
            SubscribeToEvent();
        }


        private void Serialize()
        {
            var saveFile = new SaveFileDialog();
            saveFile.Filter = "Json files (*.json)|*.json|Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string jsonString = JsonConvert.SerializeObject(connectFourGUI._operationLastPlacedChips.Cast<DictionaryEntry>()
                            .Select(de => new KeyValuePair<string, int>(
                                $"({((Tuple<int, int>)de.Key).Item1},{((Tuple<int, int>)de.Key).Item2})", (int)de.Value))
                            .ToList(),
                        new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.Auto
                        });

                    System.IO.File.WriteAllText(saveFile.FileName, jsonString);
                    MessageBox.Show("File saved successfully!");

                    // Logging the file path and name
                    Console.WriteLine($"File saved at: {saveFile.FileName}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while saving the file: " + ex.Message);
                    Console.WriteLine("Error: " + ex);
                }
            }
        }


        private void Deserialize(object sender)
        {
            var connectFour = sender as ConnectFourMockUp;
            var openFile = new OpenFileDialog();
            openFile.Filter = "Json files (*.json)|*.json|Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string jsonString = System.IO.File.ReadAllText(openFile.FileName);
                    var deserializedList = JsonConvert.DeserializeObject<List<KeyValuePair<string, Chip>>>(jsonString, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    });

                    connectFour.GameBoard.StartNewGame(false);
                    foreach (var entry in deserializedList)
                    {
                        var keyParts = entry.Key.Trim('(', ')').Split(',');
                        var key = new Tuple<int, int>(int.Parse(keyParts[0]), int.Parse(keyParts[1]));

                        var value = entry.Value;
                        //MessageBox.Show($"{key.Item1},{key.Item2}");
                        MessageBox.Show($"{key.Item1},{key.Item2}:{value}");
                        connectFour._operationLastPlacedChips.Add(key, value);
                        //GameBoard.ChipOperations(Chip.None, lastKey.Item1, lastKey.Item2);
                        connectFour.GameBoard.PlaceChipOnCol(key.Item1, value);

                    }

                    MessageBox.Show("Restored shapes from " + openFile.FileName);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while opening the file: " + ex.Message);
                    Console.WriteLine("Error: " + ex);
                }
            }
            connectFour.Invalidate();
            connectFour.Refresh();
        }

        private void SubscribeToEvent()
        {
           
            startMenu.OnGameStart += ConnectFourStartUp;
        }

       


        private void ConnectFourStartUp(object sender)
        {
            if (sender is Form2 form2)
            {
                connectFourGUI = new ConnectFourMockUp(Chip.Player1, form2._selectedShapeType, form2._selectedColor1,
                    form2._selectedColor2);
            connectFourGUI.FormClosing += connectFourGUI_FormClosing;
            connectFourGUI.OnDeserializeOpened += Deserialize;
            connectFourGUI.Show();
            }

            //connectFourGUI.GameBoard.OnGameReset += GameBoard_OnGameReset;
            //connectFourGUI.OnAppClosingHandler += OnAppClosing;



        }

        private void connectFourGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            //var connectFourDTO = ConnectFourMockUpMapper.ToDto(connectFourGUI);
            //serializeConnectFour(connectFourDTO);
            Serialize();

        }


        private void GameBoard_OnGameReset(object sender)
        {
            connectFourGUI.GameBoard.FirstPlayerChip = Chip.Player1;
        }

  

        private void GameBoard_OnNewGame(object sender)
        {
            Invalidate();
        }

 

        private void OnAppClosing(object sender)
        {
            
                Application.Exit();
            
        }

       
    }
}

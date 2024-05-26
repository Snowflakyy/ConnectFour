using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace CourseWork_OOP_Phase1_
{
    public partial class FormSettings : Form
    {
        private readonly Action<Color> _onColorPicked;
        private readonly Color _defaultColor;

        public FormSettings(Color defaultColor, Action<Color> onColorPicked)
        {
            InitializeComponent();
            _defaultColor = defaultColor;
            _onColorPicked = onColorPicked;
            OnResize(null);
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Escape)
            {
                Hide();
            }

        }
  

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Hide();
        }
        public Action<Color> ColorPickedCallBack { get; set; }


        private void BtnColorPicker_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = _defaultColor;
            colorDialog1.ShowDialog();
            _onColorPicked(colorDialog1.Color);
            ColorPickedCallBack?.Invoke(colorDialog1.Color);
        }
        protected override void OnResize(EventArgs e)
        {

           
            Invalidate();
        }
    }
}

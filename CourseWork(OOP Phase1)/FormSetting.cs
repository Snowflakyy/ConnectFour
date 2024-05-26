using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork_OOP_Phase1_
{
    public partial class FormSetting : Form
    {
        private readonly Action<Color> _onColorPicked;
        private readonly Color _defaultColor;

        public FormSetting(Color defaultColor, Action<Color> onColorPicked)
        {
            InitializeComponent();
            _defaultColor = defaultColor;
            _onColorPicked = onColorPicked;
            OnResize(null);
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
            FormBorderStyle = FormBorderStyle.None;
            Load += FormSetting_Load;
        }


        private void FormSetting_Load(object sender,EventArgs e)
        {
            OpenColorControl();
        }

        private void OpenColorControl()
        {
            colorDialog1.Color = _defaultColor;
            colorDialog1.ShowDialog();
            _onColorPicked(colorDialog1.Color);
            ColorPickedCallBack?.Invoke(colorDialog1.Color);
            
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Escape)
            {
                Hide();
            }

        }


        public Action<Color> ColorPickedCallBack { get; set; }

        protected override void OnResize(EventArgs e)
        {


            Invalidate();
        }
    }
}

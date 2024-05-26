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
    public partial class PopUp : Form
    {
        //public string _text { get; set; }
        private int yLabelPosition = 10;
        public PopUp(IEnumerable<string> res)
        {

            InitializeComponent();
            this.Size = new Size(250, 30*(1+res.Count()));
            
           Dock = DockStyle.Fill;
           FormBorderStyle = FormBorderStyle.None;

           foreach (var resultValue in res)
           {
               var label = new Label
               {
                   Text = resultValue,
                   Size = new Size(250,30),
                   BackColor = Color.Transparent,
                   Location = new Point(10, yLabelPosition), 
                   AutoSize = true ,

               };
                //label.Text = _text;
                //label.Dock = DockStyle.Fill;

                this.Controls.Add(label);
               yLabelPosition += 20;
           }
          


        }
        

    }
}

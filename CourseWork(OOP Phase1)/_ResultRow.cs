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
    public partial class _ResultRow : Form
    {
        private int yLabelPosition = 20;
        public _ResultRow(IEnumerable<string> rowRes)
        {
            InitializeComponent();
            var listedRes = rowRes.ToList();
            this.Size = new Size(250, 30 * (1 + listedRes.Count()));

            Dock = DockStyle.Fill;
            FormBorderStyle = FormBorderStyle.None;
            for (int i = 0; i < listedRes.Count(); i++)
            {
                var label = new Label
                {
                    Text = listedRes[i],
                    Size = new Size(250,30),
                    BackColor = Color.Transparent,
                    Location = new Point(10,yLabelPosition*i),
                    AutoSize = true
                };
                this.Controls.Add(label);
            }
        }
        //public PopUp(IEnumerable<string> res)
        //{

        //    InitializeComponent();
        //    this.Size = new Size(250, 30 * (1 + res.Count()));

        //    Dock = DockStyle.Fill;
        //    FormBorderStyle = FormBorderStyle.None;

        //    foreach (var resultValue in res)
        //    {
        //        var label = new Label
        //        {
        //            Text = resultValue,
        //            Size = new Size(250, 30),
        //            BackColor = Color.Transparent,
        //            Location = new Point(10, yLabelPosition),
        //            AutoSize = true,

        //        };
        //        //label.Text = _text;
        //        //label.Dock = DockStyle.Fill;

        //        this.Controls.Add(label);
        //        yLabelPosition += 20;
        //    }
    }
}

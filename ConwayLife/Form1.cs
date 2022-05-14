using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ConwayLifeLibrary;

namespace ConwayLife
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Field1.RandomFill();
            Field2.Next(Field1);
            (Field1, Field2) = (Field2, Field1);
        }

        public FieldClass Field1 { get; set; } = new FieldClass(200);
        public FieldClass Field2 { get; set; } = new FieldClass(200);

        public PainterClass Painter { get; set; } = new PainterClass();

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Painter.control = this;
            Painter.F = Field1;
            Painter.Paint();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Field2.Next(Field1);
            (Field1, Field2) = (Field2, Field1);
            Painter.control = this;
            Painter.F = Field1;
            Painter.Paint();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Painter.control = this;

            for (int i = 0; i < 10; i++)
            {
                Field2.Next(Field1);
                (Field1, Field2) = (Field2, Field1);
                Painter.F = Field1;
                Painter.QPaint();
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Painter.control = this;

            for (int i = 0; i < 100; i++)
            {
                Field2.Next(Field1);
                (Field1, Field2) = (Field2, Field1);
                Painter.F = Field1;
                Painter.QPaint();
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            int N = Int32.Parse(toolStripTextBox1.Text);
            Painter.control = this;

            for (int i = 0; i < N; i++)
            {
                Field2.Next(Field1);
                (Field1, Field2) = (Field2, Field1);
                Painter.F = Field1;
                Painter.QPaint();
            }

        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            int s = Int32.Parse(toolStripTextBox2.Text);
            if (s < 20) s = 20;
            if (s > 20000) s = 1000;

            Field1 = new FieldClass(s);
            Field2 = new FieldClass(s);

            Field1.RandomFill();

            Painter.control = this;
            Painter.F = Field1;
            Painter.QPaint();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (Size.Width > Size.Height)
            {
                Size = new Size(Size.Height,Size.Height);
            }
            else
            {
                Size = new Size(Size.Width, Size.Width);
            }
            
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            int s = Int32.Parse(toolStripTextBox2.Text);
            if (s < 20) s = 20;
            if (s > 20000) s = 1000;

            Field1 = new FieldClass(s);
            Field2 = new FieldClass(s);

            Field1.RandomFillSymmetry();

            Painter.control = this;
            Painter.F = Field1;
            Painter.QPaint();

        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            int count = Int32.Parse(toolStripTextBox3.Text);
            Field1.RandomAppend(count,true);

            Painter.control = this;
            Painter.F = Field1;
            Painter.QPaint();

        }
    }   

}

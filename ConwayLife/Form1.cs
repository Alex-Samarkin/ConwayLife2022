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

            ///
            /// 
            Field1.RandomFill();
            Field2.Next(Field1);
            /// Swap
            (Field1, Field2) = (Field2, Field1);
        }

        public FieldClass Field1 { get; set; } = new FieldClass();
        public FieldClass Field2 { get; set; } = new FieldClass();
    }
}

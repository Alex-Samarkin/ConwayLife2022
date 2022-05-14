using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConwayLifeLibrary
{
    public class PainterClass
    {
        public Control control { get; set; }

        public int W => control.ClientSize.Width;
        public int H => control.ClientSize.Height;
        public Graphics Gr => _graphics = control.CreateGraphics();
        private Graphics _graphics;
        

        public FieldClass F { get; set; }

        public Pen pen => Pens.Black;
        public Brush brush => Brushes.DarkBlue;

        public void Paint()
        {
            // проверка, что все есть
            if(control == null) return;
            if(F == null) return;

            // Рисуем здесь
            Gr.Clear(Color.AliceBlue);
            _graphics.SmoothingMode = SmoothingMode.HighSpeed;
            _graphics.InterpolationMode = InterpolationMode.Low;

            // Размеры прямоугольника
            var dx = (float)W / F.Size;
            var dy = (float)H / F.Size;

            // Отрисовка клеток
            for (int i = 0; i < F.Size; i++)
            {
                for (int j = 0; j < F.Size; j++)
                {
                    if (F.isLive(i,j)==1)
                    {
                        _graphics.FillRectangle(brush, dx * i, dy * j, dx, dy);
                        _graphics.DrawRectangle(pen,dx*i,dy*j,dx,dy);
                    }
                }
            }
        }
        public void QPaint()
        {
            // проверка, что все есть
            if (control == null) return;
            if (F == null) return;

            // Рисуем здесь
            Gr.Clear(Color.AliceBlue);
            _graphics.SmoothingMode = SmoothingMode.HighSpeed;
            _graphics.InterpolationMode = InterpolationMode.Low;

            // Размеры прямоугольника
            var dx = (float)W / F.Size;
            var dy = (float)H / F.Size;

            // Много прямоугольников 
            List<RectangleF> rcFs = new List<RectangleF>();

            // Создаем прямоугольники
            for (int i = 0; i < F.Size; i++)
            {
                for (int j = 0; j < F.Size; j++)
                {
                    if (F.isLive(i, j) == 1)
                    {
                        RectangleF rc = new RectangleF(dx * i, dy * j, dx, dy);
                        rcFs.Add(rc);
                    }
                }
            }
            _graphics.FillRectangles(brush, rcFs.ToArray());
            //_graphics.DrawRectangles(pen,rcFs.ToArray());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
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
        public Graphics Gr => control.CreateGraphics();

        public FieldClass F { get; set; }

        public Pen pen => Pens.Black;
        public Brush brush => Brushes.Blue;

        public void Paint()
        {
            // проверка, что все есть
            if(control == null) return;
            if(F == null) return;

            // Рисуем здесь
            Gr.Clear(Color.AliceBlue);

            // Размеры прямоугольника
            var dx = (float)W / F.Size;
            var dy = (float)W / F.Size;

            // Отрисовка клеток
            for (int i = 0; i < F.Size; i++)
            {
                for (int j = 0; j < F.Size; j++)
                {
                    if (F.isLive(i,j)==1)
                    {
                        Gr.DrawRectangle(pen,dx*i,dy*j,dx,dy);
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

            // Размеры прямоугольника
            var dx = (float)W / F.Size;
            var dy = (float)W / F.Size;

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
            Gr.DrawRectangles(pen,rcFs.ToArray());
        }
    }
}

using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ConwayLifeLibrary
{
    public class FieldClass
    {
        /// <summary>
        /// размер поля
        /// </summary>
        public int Size { get; set; } = 1000;
        /// <summary>
        /// клетки
        /// </summary>
        public int[,] Items { get; set; } = new int[,]{};
        /// <summary>
        /// создание поля и заполнение переменной
        /// </summary>
        /// <param name="value">значение поля по умолчанию</param>
        public void CreateField(int value = 0)
        {
            /// создаем массив заданного размера
            Items = new int[Size, Size];

            /// каждый элемент получает значение value
            /// есть возможность задать всему массиву значение разом
            /// это быстрее 
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Items[i, j] = value;
                }
            }
        }
        public void RandomFill(int value = 1, int Seed = 0)
        {
            Random r = new Random(Seed);

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Items[i, j] = r.Next(2);
                }
            }
        }

        public FieldClass(int size = 1000, int seed = 0, bool random_fill = false)
        {
            Size = size;
            CreateField();
            if (random_fill)
            {
                RandomFill();
            }
        }

        public int GetXY(int x = 0, int y = 0)
        {
            x = (x % Size + Size) % Size;
            y = (y % Size + Size) % Size;

            return Items[x, y];
        }

        public int this[int x, int y]
        {
            get => GetXY(x, y);
            set => Items[(x % Size + Size) % Size, (y % Size + Size) % Size] = value;
        }

        public void SetValue(int x, int y, int value) => this[x, y] = value;
        public void SetDead(int x, int y) => SetValue(x, y, 0);
        public void SetLive(int x, int y) => SetValue(x, y, 1);
        public int isLive(int x, int y) => (this[x, y] != 0) ? 1 : 0;
        public int isDead(int x, int y) => (this[x, y] == 0) ? 1 : 0;

        public int Near(int x, int y)
        {
            return isLive(x-1, y-1)+ isLive(x-1, y) + isLive(x-1, y+1) +
                   isLive(x, y-1) + isLive(x, y+1) +
                   isLive(x+1, y-1) + isLive(x+1, y) + isLive(x+1, y+1) ;
        }

        public FieldClass Next()
        {
            /// Плохо, потому что заново создается поле и под него выделяется память
            /// 
            FieldClass res = new FieldClass(Size);

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    var n = Near(i, j);
                    if ((isDead(i,j) == 1) && (n==3))
                    {
                        res.SetLive(i,j);
                        continue;
                    }
                    if ((isLive(i, j) == 1) && (n < 2))
                    {
                        res.SetDead(i, j);
                        continue;
                    }
                    if ((isLive(i, j) == 1) && (n > 3))
                    {
                        res.SetDead(i, j);
                        continue;
                    }

                    res[i, j] = this[i, j];
                }
            }

            return res;
        }

        public void Next(FieldClass source)
        {

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    var n = source.Near(i, j);
                    if ((source.isDead(i, j) == 1) && (n == 3))
                    {
                        SetLive(i, j);
                        continue;
                    }
                    if ((source.isLive(i, j) == 1) && (n < 2))
                    {
                        SetDead(i, j);
                        continue;
                    }
                    if ((source.isLive(i, j) == 1) && (n > 3))
                    {
                        SetDead(i, j);
                        continue;
                    }

                    this[i, j] = source[i, j];
                }
            }
        }
    }
}

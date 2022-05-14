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

        /// только для иллюстрации, не печатайте
        public int GetXY(int x = 0, int y = 0)
        {
            x = (x % Size + Size) % Size; // x будет в диапазоне от 0 до Size - 1
            y = (y % Size + Size) % Size;

            return Items[x, y];
        }
        /// <summary>
        /// индексатор
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int this[int x, int y]
        {
            get => Items[(x % Size + Size) % Size, (y % Size + Size) % Size];
            set => Items[(x % Size + Size) % Size, (y % Size + Size) % Size] = value;
        }


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
        public void RandomFillPercent(int value = 1, int Percent=30,int Seed = 0)
        {
            Random r = new Random(Seed);

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (r.Next(100) < Percent)
                    {
                        Items[i, j] = r.Next(2);
                    }
                    else Items[i, j] = 0;
                }
            }
        }

        public void RandomFillSymmetry(int value = 1, int Seed = 0)
        {
            Random r = new Random(Seed);

            for (int i = 0; i < Size / 2; i++)
            {
                for (int j = 0; j < Size / 2; j++)
                {
                    this[i, j] = r.Next(2);
                    this[i, -j] = Items[i, j];
                    this[-i, j] = Items[i, j];
                    this[-i, -j] = Items[i, j];

                    this[j,i] = this[i,j];
                    this[j, -i] = this[i, j];
                    this[-j, i] = this[i, j];
                    this[-j, -i] = this[i, j];
                }
            }
        }

        public void RandomAppend(int Count = 0, bool symmetry = true)
        {
            if (Count <1)
            {
                Count = 1;
            }

            Random r = new Random();

            for (int index = 0; index < Count; index++)
            {
                int i = r.Next(Size / 2);
                int j = r.Next(Size / 2);
                this[i, j] = 1;
                if (symmetry)
                {
                    this[i, -j] = Items[i, j];
                    this[-i, j] = Items[i, j];
                    this[-i, -j] = Items[i, j];

                    this[j, i] = this[i, j];
                    this[j, -i] = this[i, j];
                    this[-j, i] = this[i, j];
                    this[-j, -i] = this[i, j];
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

        public int isLive(int x, int y) => (this[x, y] != 0) ? 1 : 0;

        public int isDead(int x, int y) => (this[x, y] == 0) ? 1 : 0;

        public int Near(int x, int y)
        {
            return isLive(x - 1, y - 1) + isLive(x - 1, y) + isLive(x - 1, y + 1) +
                   isLive(x, y - 1) +                            isLive(x, y + 1) +
                   isLive(x + 1, y - 1) + isLive(x + 1, y) + isLive(x + 1, y + 1);
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
                        this[i, j] = 1; continue;
                    }
                    if ((source.isLive(i, j) == 1) && (n < 2))
                    {
                        this[i, j] = 0; continue;
                    }
                    if ((source.isLive(i, j) == 1) && (n > 3))
                    {
                        this[i, j] = 0; continue;
                    }

                    this[i, j] = source[i, j];
                }
            }
        }
    }
}

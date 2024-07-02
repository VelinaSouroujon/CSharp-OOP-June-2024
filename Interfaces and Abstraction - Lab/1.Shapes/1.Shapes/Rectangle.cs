using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    public class Rectangle : IDrawable
    {
        private int width;
        private int height;

        public Rectangle(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public void Draw()
        {
            for (int i = 0; i < height; i++)
            {
                Console.Write("*");
                char middleChar;

                if (i == 0 || i == height - 1)
                {
                    middleChar = '*';
                }
                else
                {
                    middleChar = ' ';
                }

                Console.Write(new string(middleChar, width - 2));
                Console.WriteLine("*");
            }
        }
    }
}

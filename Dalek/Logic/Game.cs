using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dalek.Logic
{
    public class Game
    {
        public Game(int level = 0, int mX = 14, int mY = 14)
        {
            MX = mX;
            MY = mY;
            Field = new int[mX,mY];

            Hero = new Hero() { X = 3, Y = 4 };
        }

        public int MX { get; private set; }
        public int MY { get; private set; }
        public int Level { get; private set; }
        public int Score { get; private set; }

        public List<Dalek> Daleks { get; set; }
        public Hero Hero { get; set; }

        public int[,] Field { get; set; }

        public void Move(int dx, int dy)
        {

            var newX = Hero.X + dx;
            var newY = Hero.Y + dy;

            if (newX < 0 || newX >= MX || newY < 0 || newY >= MY)
                return;

            Hero.X = newX;
            Hero.Y = newY;

            Score++;
        }

        public void Blink()
        {
            Random r = new Random((int)DateTime.Now.Ticks);
            Hero.X = r.Next(MX);
            Hero.Y = r.Next(MY);

            Score++;
        }
    }
}

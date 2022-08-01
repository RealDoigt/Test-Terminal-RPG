using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;
using ConsolePaint;

namespace Test_RPG
{
    class Enemy : GameCharacter
    {
        public Elements Element { get; set; }

        ConsoleImage Avatar { get; set; }

        public Enemy(string imagePath)
        {
            HP = 200;
            ATK = 5;
            DEF = 10;
            Avatar = new ConsoleImage(new Bitmap(imagePath));
        }

        public void Draw(byte x, byte y) 
        { 
            Avatar.PaintAt(x, y);
            DrawHealthBar(x, (byte)(y + Avatar.Height + 1));
        }

        public void GetDamage(byte damage, byte x, byte y)
        {
            HP -= damage;
            DrawHealthBar(x, (byte)(y + Avatar.Height + 1));
        }

        private void DrawHealthBar(byte x, byte y) 
        {
            var colour = ConsoleColor.Blue;

            if (HP <= 25)
                colour = ConsoleColor.Red;

            else if (HP <= 50)
                colour = ConsoleColor.Yellow;

            else if (HP <= 100)
                colour = ConsoleColor.Green;

            else if (HP <= 150)
                colour = ConsoleColor.Cyan;

            Painting.DrawHorizontalLine(x, y, 22, ConsoleColor.Black); // Cleaning
            Painting.DrawHorizontalLine(x, y, (short)(HP / 20), colour);
            Console.SetCursorPosition(x + 11, y);
            Console.Write($"{HP} ({HP / 200.0 * 100.0}%)");
        }
    }
}

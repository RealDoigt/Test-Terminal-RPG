using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsolePaint;

namespace Test_RPG
{
    public enum Elements : byte
    {
        Ice,
        Fire,
        Water,
        Earth,
        Wind,
        None
    };

    class Program
    {
        const byte SCREEN_INIT_X = 10, SCREEN_INIT_Y = 5;
        const byte MESSAGE_BOX_SIZE_X = 40, MESSAGE_BOX_SIZE_Y = 10;
        static byte printCursorY = 0;

        static readonly sbyte[,] elementalEffects = new sbyte[,]
        {
            // Glace, Feu, Eau, Terre, Vent, Rien
            {  0, -1,  1, -1,  1, 0 }, // Glace 
            {  1,  0,  1, -1, -1, 0 }, // Feu
            { -1,  1,  0,  1,  0, 0 }, // Eau
            {  1,  1, -1,  0,  0, 0 }, // Terre
            {  0, -1,  0,  0,  1, 0 }, // Vent
            {  0,  0,  0,  0,  0, 0 }  // Rien 
        };

        public static sbyte Compare(Elements offense, Elements defense) => elementalEffects[(int)offense, (int)defense];

        public static void Print(string text)
        {
            Console.SetCursorPosition(1, SCREEN_INIT_Y + 11 + printCursorY);

            // OUF, c'est devenu laid, vite.
            // TODO: À simplifier.
            for (ushort y = 0, x = 0; y < MESSAGE_BOX_SIZE_Y - 2 && x < text.Length; ++y, ++Console.CursorTop, Console.CursorLeft = 1, ++printCursorY)
            {
                if (printCursorY >= MESSAGE_BOX_SIZE_Y - 2)
                {
                    ClearMessageBox();
                    y = 0;
                    printCursorY = 0;
                    Console.SetCursorPosition(1, SCREEN_INIT_Y + 11 + printCursorY);
                }

                for (byte charCount = 0; charCount < MESSAGE_BOX_SIZE_X - 2 && x < text.Length; ++x, ++charCount)
                    Console.Write(text[x]);
            }

            --Console.CursorTop;
        }

        static void ClearMessageBox()
        {
            Console.SetCursorPosition(1, SCREEN_INIT_Y + 11);

            for (byte y = 0; y < MESSAGE_BOX_SIZE_Y - 2; ++y, ++Console.CursorTop, Console.CursorLeft = 1)
                for (byte x = 0; x < MESSAGE_BOX_SIZE_X - 2; ++x)
                    Console.Write(' ');
        }

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            var skeletonEnemy = new Enemy("skull.png") { Element = Elements.Ice };
            var player = new Player();
            
            var choiceLabels = new string[] 
            {
                "Ice",
                "Fire",
                "Water",
                "Earth",
                "Wind",
                "Normal"
            };

            Action<Elements> attackEnemy = (e) => player.Attack(skeletonEnemy, e, SCREEN_INIT_X, SCREEN_INIT_Y);

            MenuMaking.MakeBorderedUL
            (
                SCREEN_INIT_X + MESSAGE_BOX_SIZE_X, 
                SCREEN_INIT_Y + 10, 
                choiceLabels, 
                ' ',
                1,
                1,
                RectanglePainting.BorderType.Vanilla, 
                ConsoleColor.Green
            );

            RectanglePainting.DrawRectangle
            (
                RectanglePainting.BorderType.Vanilla, 
                0, 
                SCREEN_INIT_Y + 10, 
                MESSAGE_BOX_SIZE_Y, 
                MESSAGE_BOX_SIZE_X, 
                ConsoleColor.Green
            );

            skeletonEnemy.Draw(SCREEN_INIT_X, SCREEN_INIT_Y);

            Print("A Skeleton has appeared. Looks like you'll have to fight it!");

            while (skeletonEnemy.HP > 0)
                attackEnemy((Elements)MenuMaking.GetSelection(SCREEN_INIT_X + MESSAGE_BOX_SIZE_X + 2, SCREEN_INIT_Y + 11, (byte)choiceLabels.Length));

            Console.CursorVisible = true;
            Console.SetCursorPosition(0, SCREEN_INIT_Y + MESSAGE_BOX_SIZE_Y + 1);
            Console.WriteLine();
        }
    }
}

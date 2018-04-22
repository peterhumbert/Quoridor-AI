using System;
using System.Collections.Generic;

namespace QuoridorAI
{
    class MainClass
    {
        Board b = new Board();

        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Board b = new Board();
            char currPlayer = 'A';
            String move;
            bool invalidMove = true;

            //List<(int x, int y)>[,] reachable = new List<(int, int)>[9, 9];

            //reachable[1, 1].Add((5, 3));
            //Console.WriteLine("added");
            //reachable[1, 2].Add(((int, int))(4, 4));
            //Console.WriteLine("added");
            //Console.WriteLine(reachable[1, 2][0].x);

            int N = 5;
            List<(int a, string b)>[] list = new List<(int, string)>[N];

            for (int i = 0; i < N; i++)
            {
                list[i] = new List<(int a, string b)>();
            }

            list[0].Add((3, "test"));
            list[0].Add((6, "second"));

            Console.WriteLine(list[0][1].a);
            Console.WriteLine(list[0].Remove((6, "seond")));

            b.print();

            b.PlayMove("h (1,3) (0,2) (1,2) (0,3)");
            b.print();

            // gameplay loop
            while (!b.HasWinner())
            {
                while (invalidMove)
                {
                    Console.Write("Player" + currPlayer + ": ");
                    move = Console.ReadLine();
                    invalidMove = !b.PlayMove(move);
                }

                // switch player
                if (currPlayer == 'A')
                    currPlayer = 'B';
                else
                    currPlayer = 'A';
                b.print();
                invalidMove = true;
            }
        } // end Main
    } // end MainClass
} // end namespace Quoridor AI
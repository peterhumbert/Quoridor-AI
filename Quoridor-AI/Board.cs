using System;
using System.Collections.Generic;

namespace QuoridorAI
{
    public class Board
    {
        private int N = 9;
        private List<(int x, int y)>[,] reachable { get; set; }
        private List<(int x, int y)>[,] diagonals { get; set; }
        private char[,] printableBoard { get; set; }

        public Board()
        {
            int i, j;

            reachable = new List<(int, int)>[N, N];
            diagonals = new List<(int, int)>[N, N];
            printableBoard = new char[2 * N - 1, 2 * N - 1];


            // populate the printable board array
            for (i = 0; i < 2 * N - 1; i++) {
                for (j = 0; j < 2 * N - 1; j++) {
                    printableBoard[i, j] = ' ';
                }
            }
            for (i = 0; i < 2 * N - 1; i+=2) {
                for (j = 0; j < 2 * N - 1; j+=2) {
                    printableBoard[i, j] = '\u2610';
                }
            }
            printableBoard[0, N - 1] = 'A';
            printableBoard[2*N - 2, N - 1] = 'B';

            // populate list arrays with empty lists
            for (i = 0; i < N; i++) {
                for (j = 0; j < N; j++) {
                    reachable[i, j] = new List<(int x, int y)>();
                    diagonals[i, j] = new List<(int x, int y)>();
                }
            }

            // Initialize the spaces reachable from interior node
            for (i = 1; i < N-1; i++) {
                for (j = 1; j < 9; j++) {
                    reachable[i, j].Add((i - 1,j));
                    reachable[i, j].Add((i + 1, j));
                    reachable[i, j].Add((i, j - 1));
                    reachable[i, j].Add((i, j + 1));

                    diagonals[i, j].Add((i - 1, j - 1));
                    diagonals[i, j].Add((i - 1, j + 1));
                    diagonals[i, j].Add((i + 1, j - 1));
                    diagonals[i, j].Add((i + 1, j + 1));
                }
            }

            // Initialize the spaces reachable from left edge
            j = 0;
            for (i = 1; i < N-1; i++)
            {
                reachable[i, j].Add((i - 1, j));
                reachable[i, j].Add((i + 1, j));
                reachable[i, j].Add((i, j + 1));

                diagonals[i, j].Add((i - 1, j + 1));
                diagonals[i, j].Add((i + 1, j + 1));
            }

            // Initialize the spaces reachable from right edge
            j = N-1;
            for (i = 1; i < N-1; i++)
            {
                reachable[i, j].Add((i - 1, j));
                reachable[i, j].Add((i + 1, j));
                reachable[i, j].Add((i, j - 1));

                diagonals[i, j].Add((i - 1, j - 1));
                diagonals[i, j].Add((i + 1, j - 1));
            }

            // Initialize the spaces reachable from top edge
            i = 0;
            for (j = 1; j < N-1; j++)
            {
                reachable[i, j].Add((x: i + 1, y: j));
                reachable[i, j].Add((i, j - 1));
                reachable[i, j].Add((i, j + 1));

                diagonals[i, j].Add((i + 1, j - 1));
                diagonals[i, j].Add((i + 1, j + 1));
            }

            // Initialize the spaces reachable from bottom edge
            i = N-1;
            for (j = 1; j < N-1; j++)
            {
                reachable[i, j].Add((i - 1, j));
                reachable[i, j].Add((i, j - 1));
                reachable[i, j].Add((i, j + 1));

                diagonals[i, j].Add((i - 1, j - 1));
                diagonals[i, j].Add((i - 1, j + 1));
            }

            // Initialize the spaces reachable from the corners
            reachable[0, 0].Add((0, 1));
            reachable[0, 0].Add((1, 0));
            diagonals[0, 0].Add((1, 1));

            reachable[0, N-1].Add((0, N-2));
            reachable[0, N-1].Add((1, N-1));
            diagonals[0, N-1].Add((1, N-2));

            reachable[N-1, 0].Add((N-2, 0));
            reachable[N-1, 0].Add((N-1, 1));
            diagonals[N-1, 0].Add((N-2, 1));

            reachable[N-1, N-1].Add((N-2, N-1));
            reachable[N-1, N-1].Add((N-1, N-2));
            diagonals[N-1, N-1].Add((N-2, N-2));

        }

        public List<char> GetValidMoves() {

            return null;
        }

        public bool PlaceWall(Tuple<int, int> t) {

            return false;
        }

        public Board Clone() {
          
            return null;
        }

         

        public void print() {
            Console.WriteLine("  0 1 2 3 4 5 6 7 8 ");
            Console.WriteLine(" |-----------------|");
            for (int i = 0; i < 2 * N - 1; i++) {
                if (i % 2 == 0)
                    Console.Write(i / 2);
                else
                    Console.Write(' ');
                Console.Write("|");
                for (int j = 0; j < 2 * N - 1; j++) {
                    Console.Write(printableBoard[i, j]);
                }
                Console.WriteLine("|");
            }
            Console.WriteLine(" |-----------------|");

        }
    }
}

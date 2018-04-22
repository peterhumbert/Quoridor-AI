using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace QuoridorAI
{
    public class Board
    {
        private int N = 9;
        private List<(int r, int c)>[,] reachable { get; set; }
        private List<(int r, int c)>[,] diagonals { get; set; }
        private char[,] printableBoard { get; set; }
        private (int r, int c) Apos { get; set; }
        private (int r, int c) Bpos { get; set; }

        public Board()
        {
            int i, j;

            reachable = new List<(int, int)>[N, N];
            diagonals = new List<(int, int)>[N, N];
            printableBoard = new char[2 * N - 1, 2 * N - 1];


            // populate the printable board array
            for (i = 0; i < 2 * N - 1; i++)
            {
                for (j = 0; j < 2 * N - 1; j++)
                {
                    printableBoard[i, j] = ' ';
                }
            }
            for (i = 0; i < 2 * N - 1; i += 2)
            {
                for (j = 0; j < 2 * N - 1; j += 2)
                {
                    printableBoard[i, j] = '\u2610';
                }
            }
            printableBoard[0, N - 1] = 'A';
            printableBoard[2 * N - 2, N - 1] = 'B';
            Apos = (0, N-1);
            Bpos = (2 * N - 2, N - 1);

            // populate list arrays with empty lists
            for (i = 0; i < N; i++)
            {
                for (j = 0; j < N; j++)
                {
                    reachable[i, j] = new List<(int r, int c)>();
                    diagonals[i, j] = new List<(int r, int c)>();
                }
            }

            // Initialize the spaces reachable from interior node
            for (i = 1; i < N - 1; i++)
            {
                for (j = 1; j < 9; j++)
                {
                    reachable[i, j].Add((i - 1, j));
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
            for (i = 1; i < N - 1; i++)
            {
                reachable[i, j].Add((i - 1, j));
                reachable[i, j].Add((i + 1, j));
                reachable[i, j].Add((i, j + 1));

                diagonals[i, j].Add((i - 1, j + 1));
                diagonals[i, j].Add((i + 1, j + 1));
            }

            // Initialize the spaces reachable from right edge
            j = N - 1;
            for (i = 1; i < N - 1; i++)
            {
                reachable[i, j].Add((i - 1, j));
                reachable[i, j].Add((i + 1, j));
                reachable[i, j].Add((i, j - 1));

                diagonals[i, j].Add((i - 1, j - 1));
                diagonals[i, j].Add((i + 1, j - 1));
            }

            // Initialize the spaces reachable from top edge
            i = 0;
            for (j = 1; j < N - 1; j++)
            {
                reachable[i, j].Add((r: i + 1, c: j));
                reachable[i, j].Add((i, j - 1));
                reachable[i, j].Add((i, j + 1));

                diagonals[i, j].Add((i + 1, j - 1));
                diagonals[i, j].Add((i + 1, j + 1));
            }

            // Initialize the spaces reachable from bottom edge
            i = N - 1;
            for (j = 1; j < N - 1; j++)
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

            reachable[0, N - 1].Add((0, N - 2));
            reachable[0, N - 1].Add((1, N - 1));
            diagonals[0, N - 1].Add((1, N - 2));

            reachable[N - 1, 0].Add((N - 2, 0));
            reachable[N - 1, 0].Add((N - 1, 1));
            diagonals[N - 1, 0].Add((N - 2, 1));

            reachable[N - 1, N - 1].Add((N - 2, N - 1));
            reachable[N - 1, N - 1].Add((N - 1, N - 2));
            diagonals[N - 1, N - 1].Add((N - 2, N - 2));

        } //end constructor

        public List<(int r, int c)> GetValidMoves((int r, int c) p1,
                                                  (int r, int c) p2)
        {
            List<(int r, int c)> temp =
                reachable[p1.r, p1.c]; // for aggregating valid positions

            foreach ((int r, int c) in reachable[p1.r, p1.c])
            {
                if ((r, c).Equals(p2))
                {
                    // the two pieces are in adjacent squares
                    temp.Remove((r, c));

                    // TODO identify valid diagonals
                    // TODO identify if 2-advance jump is valid
                    break;
                }
            }

            return null;
        } // end GetValidMoves()

        public bool PlaceWall(List<(int r, int c)> wallPoints)
        {
            // check if 4 reachable links in reachable will be broken
            return false;
        } // end PlaceWall()

        public Board Clone()
        {
            // make a new board object, set all its members equal to the values
            // of this instance

            return null;
        } // end Clone()

        public void print()
        {
            Console.WriteLine("  0 1 2 3 4 5 6 7 8 ");
            Console.WriteLine(" |-----------------|");
            for (int i = 0; i < 2 * N - 1; i++)
            {
                if (i % 2 == 0)
                    Console.Write(i / 2);
                else
                    Console.Write(' ');
                Console.Write("|");
                for (int j = 0; j < 2 * N - 1; j++)
                {
                    Console.Write(printableBoard[i, j]);
                }
                Console.WriteLine("|");
            }
            Console.WriteLine(" |-----------------|");

        } // end print()

        public bool HasWinner()
        {
            return Apos.r == 2 * N - 2 || Bpos.r == 0;
        } // end HasWinner()

        public bool PlayMove(String move) {
            List<(int r, int c)> wallPoints = new List<(int r, int c)>();

            if (move[0] == 'h' || move[0] == 'v') {
                Console.WriteLine("playmove");
                // the player wants to place a wall
                Regex rx = new Regex(@"\d?,\d?", RegexOptions.Compiled);
                
                // Find matches.
                MatchCollection matches = rx.Matches(move);
                Console.WriteLine(matches.Count);

                foreach (Match match in matches)
                {
                    wallPoints.Add((Int32.Parse(match.Value.Substring(0, 1)),
                                    Int32.Parse(match.Value.Substring(2, 1))));
                }

                if (move[0] == 'h') {
                    // horizontal -- find matching column values, sort pairwise
                    bool sorted = false;
                    int i = 1;
                    while(!sorted) {
                        if (wallPoints[i].c == wallPoints[0].c) {
                            (int r, int c) temp = wallPoints[i];
                            wallPoints[i] = wallPoints[1];
                            wallPoints[1] = temp;
                            sorted = true;
                        }
                        i++;
                    }
                } else {
                    // vertical -- find matching row values, sort pairwise
                    bool sorted = false;
                    int i = 1;
                    while (!sorted)
                    {
                        if (wallPoints[i].r == wallPoints[0].r)
                        {
                            (int r, int c) temp = wallPoints[i];
                            wallPoints[i] = wallPoints[1];
                            wallPoints[1] = temp;
                            sorted = true;
                        }
                        i++;
                    }
                }

                return PlaceWall(wallPoints);
            } else {
                // the player wants to move their piece
                
            }
            return true;
        }
    }
}

﻿using System;
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

        public bool PlaceWall(List<(int r, int c)> wallPoints, bool isVert)
        {
            Console.WriteLine("placewall");
            // check if 2 reachable links will be broken
            if (!(reachable[wallPoints[0].r, wallPoints[0].c].Contains(wallPoints[1]) &&
                  reachable[wallPoints[2].r, wallPoints[2].c].Contains(wallPoints[3])))
                 return false;
            
            // check if 2 diagonal links will be broken
            if (!(diagonals[wallPoints[0].r, wallPoints[0].c].Contains(wallPoints[3]) &&
                  diagonals[wallPoints[1].r, wallPoints[1].c].Contains(wallPoints[2])))
                return false;

            // all links are present and move is valid, so break links
            reachable[wallPoints[0].r, wallPoints[0].c].Remove(wallPoints[1]);
            reachable[wallPoints[2].r, wallPoints[2].c].Remove(wallPoints[3]);
            diagonals[wallPoints[0].r, wallPoints[0].c].Remove(wallPoints[3]);
            diagonals[wallPoints[1].r, wallPoints[1].c].Remove(wallPoints[2]);

            // update printable board
            if (isVert)
            {
                printableBoard[2 * wallPoints[0].r, 2 * wallPoints[0].c] = '|';
                printableBoard[2 * wallPoints[0].r + 1, 2 * wallPoints[0].c] = '|';
            }
            else
            {
                Console.WriteLine("updating printable board");
                printableBoard[2 * wallPoints[0].r, 2 * wallPoints[0].c] = '-';
                printableBoard[2 * wallPoints[0].r, 2 * wallPoints[0].c + 1] = '-';
            }

            return true;
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
            List<(int r, int c)> wallPoints2 = new List<(int r, int c)>();
            for (int i = 0; i < 4; i++)
            {
                wallPoints.Add((-1,-1));
                wallPoints2.Add((-1,-1));
            }

            if (move[0] == 'h' || move[0] == 'v') {
                Console.WriteLine("playmove");
                // the player wants to place a wall
                Regex rx = new Regex(@"\d?,\d?", RegexOptions.Compiled);
                int minr = 100, minc = 100;
                
                // Find matches.
                MatchCollection matches = rx.Matches(move);
                Console.WriteLine(matches.Count);

                int j = 0;
                foreach (Match match in matches)
                {
                    wallPoints[j] = (Int32.Parse(match.Value.Substring(0, 1)),
                                    Int32.Parse(match.Value.Substring(2, 1)));
                    j++;
                }

                for (int i = 0; i < 4; i++)
                {
                    if (wallPoints[i].r < minr) minr = wallPoints[i].r;
                    if (wallPoints[i].c < minc) minc = wallPoints[i].c;
                }

                // sort pairwise for vertical
                for (int i = 0; i < 4; i++) {
                    if (wallPoints[i].r == minr && wallPoints[i].c == minc)
                        wallPoints2[0] = wallPoints[i];
                    else if (wallPoints[i].r == minr && wallPoints[i].c != minc)
                        wallPoints2[1] = wallPoints[i];
                    else if (wallPoints[i].c == minc && wallPoints[i].r != minr)
                        wallPoints2[2] = wallPoints[i];
                    else if (wallPoints[i].c != minc && wallPoints[i].r != minr)
                        wallPoints2[3] = wallPoints[i];
                }

                // sort pairwise for horizontal if needed
                if (move[0] == 'h') {
                    (int r, int c) temp = wallPoints2[2];
                    wallPoints2[2] = wallPoints2[1];
                    wallPoints2[1] = temp;
                }

                return PlaceWall(wallPoints2, move[0] == 'v');
            } else {
                // the player wants to move their piece
                
            }
            return true;
        }
    }
}

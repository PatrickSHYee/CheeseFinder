using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheeseFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            CheeseNibbler game = new CheeseNibbler();
            game.PlayGame();
        }



        class Point
        {
            public enum PointStatus
            {
                Empty, Cheese, Mouse
            }

            // properties
            public int X;
            public int Y;
            public PointStatus Status;

            // constructor
            public Point(int x, int y)
            {
                this.X = x;
                this.Y = y;
                this.Status = PointStatus.Empty;
            }
        }

        /// <summary>
        /// core of the game where the grid owns the points, the mouse and the cheese.
        /// </summary>
        class CheeseNibbler
        {
            public Point[,] TheGrid = new Point[10, 10];
            public Point TheMouse;
            public Point TheCheese;
            int Round;

            // randomizer
            Random RNG = new Random();

            public CheeseNibbler()
            {
                for (int y = 0; y< TheGrid.GetLength(0); y++)
                {
                    for (int x = 0; x < TheGrid.GetLength(1); x++)
                    {
                        TheGrid[x, y] = new Point(x, y);
                    }
                }
                // set the objects on the grid
                // change the status of the Grid with the cheese and the mouse
                TheMouse = TheGrid[RNG.Next(0, TheGrid.GetLength(0)), RNG.Next(0, RNG.Next(0, TheGrid.GetLength(1)))];
                TheMouse.Status = Point.PointStatus.Mouse;

                do
                {
                    TheCheese = TheGrid[RNG.Next(0, TheGrid.GetLength(0)), RNG.Next(0, RNG.Next(0, TheGrid.GetLength(1)))];
                } while (this.TheCheese.Status != Point.PointStatus.Empty);
                TheCheese.Status = Point.PointStatus.Cheese;

                // set the round count to 0 
                Round = 0;
            }

            public void PlayGame()
            {
                bool isCheese = false;
                while (!isCheese)
                {
                    DrawGrid();
                    ConsoleKey userInput = GetUserMove();
                    if (ValidMove(userInput))
                    {
                        isCheese = MoveMouse(userInput);
                        Round++;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input");
                    }
                }
                if (isCheese) Console.WriteLine("You have won in {0} rounds.", Round);
            }
            /// <summary>
            /// Draws the grid with the updated information
            /// </summary>
            public void DrawGrid()
            {
                Console.Clear();
                for (int x = 0; x < TheGrid.GetLength(0); x++)
                {
                    for (int y = 0; y < TheGrid.GetLength(1); y++)
                    {
                        // prints the cheddar or the money
                        if (TheGrid[x, y].Status == Point.PointStatus.Cheese) Console.Write("[$]");
                        // prints the mouse of the little man
                        if (TheGrid[x, y].Status == Point.PointStatus.Mouse) Console.Write("[m]");
                        // print the empty spaces of the grid
                        if (TheGrid[x, y].Status == Point.PointStatus.Empty) Console.Write("[ ]");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("Round: {0}", Round);
            }

            /// <summary>
            /// Evalulates the user's input and returns that key.
            /// </summary>
            /// <returns>user's input</returns>
            public ConsoleKey GetUserMove()
            {
                ConsoleKeyInfo input = Console.ReadKey();
                switch (input.Key)
                {
                    case ConsoleKey.UpArrow:
                        return ConsoleKey.UpArrow;
                    case ConsoleKey.LeftArrow:
                        return ConsoleKey.LeftArrow;
                    case ConsoleKey.DownArrow:
                        return ConsoleKey.DownArrow;
                    case ConsoleKey.RightArrow:
                        return ConsoleKey.RightArrow;
                    default:
                        Console.WriteLine("Invalid input");  // some invalid input from user
                        break;
                }
                return input.Key;  // by default nothing should happen
            }

            /// <summary>
            /// Event key press handler. If mouse is at edge of the grid is does nothing.
            /// </summary>
            /// <param name="input">User input</param>
            /// <returns>is whether or weather move</returns>
            public bool ValidMove(ConsoleKey input)
            {
                switch (input)
                {
                    case ConsoleKey.LeftArrow:
                        if (TheMouse.Y - 1 >= 0) return true;
                        else return false;
                    case ConsoleKey.RightArrow:
                        if (TheMouse.Y + 1 < 10) return true;
                        else return false;
                    case ConsoleKey.UpArrow:
                        if (TheMouse.X - 1 >= 0) return true;
                        else return false;
                    case ConsoleKey.DownArrow:
                        if (TheMouse.X + 1 < 10) return true;
                        else return false;
                    default: Console.WriteLine("Bad move"); break;
                }
                return false;  // nothing is done to the mouse
            }

            /// <summary>
            /// Moves the mouse and if that move is at cheese.
            /// </summary>
            /// <param name="input">User input</param>
            /// <returns>Found the cheese</returns>
            public bool MoveMouse(ConsoleKey input)
            {

                // update the status of the grid with a empty
                // Move the mouse
                // re update the status of the grid with the mouse
                switch (input)
                {
                    case ConsoleKey.LeftArrow:
                        {
                            TheGrid[TheMouse.X, TheMouse.Y].Status = Point.PointStatus.Empty;
                            TheMouse.Y--;
                            TheGrid[TheMouse.X, TheMouse.Y].Status = Point.PointStatus.Mouse;
                            break;
                        }
                    case ConsoleKey.RightArrow:
                        {
                            TheGrid[TheMouse.X, TheMouse.Y].Status = Point.PointStatus.Empty;
                            TheMouse.Y++;
                            TheGrid[TheMouse.X, TheMouse.Y].Status = Point.PointStatus.Mouse;
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        {
                            TheGrid[TheMouse.X, TheMouse.Y].Status = Point.PointStatus.Empty;
                            TheMouse.X--;
                            TheGrid[TheMouse.X, TheMouse.Y].Status = Point.PointStatus.Mouse;
                            break;
                        }
                    case ConsoleKey.DownArrow:
                        {
                            TheGrid[TheMouse.X, TheMouse.Y].Status = Point.PointStatus.Empty;
                            TheMouse.X++;
                            TheGrid[TheMouse.X, TheMouse.Y].Status = Point.PointStatus.Mouse;
                            break;
                        }
                }
                // check if the mouse is on the cheese
                if (TheMouse.X == TheCheese.X && TheMouse.Y == TheCheese.Y)
                {
                    return true;
                }
                else return false;
            }
        }
    }
}

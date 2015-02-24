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
                for (int x = 0; x < TheGrid.GetLength(0); x++)
                {
                    for (int y = 0; y < TheGrid.GetLength(1); y++)
                    {
                        TheGrid[x, y] = new Point(x, y);
                    }
                }
                // set the objects on the grid
                TheMouse = new Point(RNG.Next(0, TheGrid.GetLength(0)), RNG.Next(0, RNG.Next(0, TheGrid.GetLength(1))));
                int tempNumberX = RNG.Next(0, TheGrid.GetLength(0));
                int tempNumberY = RNG.Next(0, TheGrid.GetLength(0));
                if (tempNumberX != TheMouse.X)  // make sure if the x of cheese is not equal the mouse
                {
                    if (tempNumberY != TheMouse.Y)  // make sure if the y of cheese is not equal to the mouse
                    {
                        TheCheese = new Point(tempNumberX, tempNumberY);  // assign the point to the cheese
                    }
                    else  // the y is equal to the mouse
                    {
                        TheCheese = new Point(tempNumberX, tempNumberY - 3);  // moves the cheese to the left of the mouse.
                    }
                }
                else  // x of the cheese is at the mouse local
                {
                    if (tempNumberY != TheMouse.Y)  // check if the y of cheese is at the mouse Y
                    {
                        TheCheese = new Point(tempNumberX - 3, tempNumberY);  // moves the cheese up
                    }
                    else
                    {
                        TheCheese = new Point(tempNumberX - 3, tempNumberY -3);  // for some reason that the cheese and the mouse are equal
                    }
                }
                

                // change the status of the Grid with the cheese and the mouse
                TheGrid[TheMouse.X, TheMouse.Y].Status = Point.PointStatus.Mouse;
                TheGrid[TheCheese.X, TheCheese.Y].Status = Point.PointStatus.Cheese;
            }

            /// <summary>
            /// Draws the grid with the updated information
            /// </summary>
            public void DrawGrid()
            {
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
            public bool MoveMouse(ConsoleKey input)
            {
                switch (input)
                {
                    case ConsoleKey.LeftArrow:
                        if (TheMouse.X - 1 > 0)
                        {  // checks if the mouse has reach the most left edge of the grid
                            TheMouse.X--;
                            return true;
                        }
                    case ConsoleKey.RightArrow:
                        if (TheMouse.X + 1 < 9)  // checks if the mouse has reach the most right edge of the grid
                            TheMouse.X++;
                        TheMouse.X++;
                        return true;
                    case ConsoleKey.UpArrow: 
                        if (TheMouse.Y - 1 > 0)  // checks if the mouse has reach the most upper edge of the grid
                            TheMouse.Y--;
                        return true;
                    case ConsoleKey.DownArrow: 
                        if (TheMouse.Y + 1 < 9)  // checks if the mouse has reach the most bottom edge of the grid
                            TheMouse.Y++;
                        return true;
                    default: Console.WriteLine("Bad move"); break;
                }
                return false;  // nothing is done to the mouse
            }
        }
    }
}

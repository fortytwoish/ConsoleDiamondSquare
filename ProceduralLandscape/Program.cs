using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;

namespace ProceduralLandscape
{
    class Program
    {
        static int dim;
        static int offset = dim - 1;
        static Random rand = new Random();
        static short[,] field;

        static void Main(string[] args)
        {

            Console.WriteLine("1. Please set your Console to the smallest font available (or install a 1x1 Pixel Font), then press Enter.");
            Console.WriteLine("2. The font size will determine your resulting resolution (largest power of 2 that fits into your largest possible console).");
            Console.WriteLine("3. Press Enter to go through the intro demonstration or press Esc to skip it.");
            Console.ReadLine();

            dim = 1;
            while (dim * 2 < Console.LargestWindowWidth && dim * 2 < Console.LargestWindowHeight)
            {
                dim *= 2;
            }

            Debug.WriteLine($"Your max dimensions are: {Console.LargestWindowWidth}x{Console.LargestWindowHeight}.");
            Debug.WriteLine($"Program will run at: {dim}x{dim}.");

            //Init Console
            Console.CursorVisible = false;
            //Initialize Field
            field = new short[dim, dim];
            var topLeft = new Point(0, 0);
            var topRight = new Point(dim-1, 0);
            var bottomLeft = new Point(0, dim - 1);
            var bottomRight = new Point(dim - 1, dim - 1);
            field[topLeft.x, topLeft.y] = 
                field[topRight.x, topRight.y] = 
                field[bottomLeft.x, bottomLeft.y] = 
                field[bottomRight.x, bottomRight.y] = 2;

            #region demo
            Console.SetWindowSize(dim + 1, dim + 1);
            Console.SetBufferSize(dim + 1, dim + 1);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            for (var i = 0; i < (int)(Math.Log(dim, 2)); i++)
            {
                squareStepDemo(i, topLeft, topRight, bottomLeft, bottomRight, true);
                if (displayAndWait_demo() == ConsoleKey.Escape)
                    break;
                squareStepDemo(i, topLeft, topRight, bottomLeft, bottomRight, false);
                if (displayAndWait_demo() == ConsoleKey.Escape)
                    break;
                diamondStepDemo(i, topLeft, topRight, bottomLeft, bottomRight, true);
                if (displayAndWait_demo() == ConsoleKey.Escape)
                    break;
                diamondStepDemo(i, topLeft, topRight, bottomLeft, bottomRight, false);
                if (displayAndWait_demo() == ConsoleKey.Escape)
                    break;
            }
            #endregion

            for (;;)
            {
                dim = 1;
                while (dim * 2 < Console.LargestWindowWidth && dim * 2 < Console.LargestWindowHeight)
                {
                    dim *= 2;
                }
                Console.SetWindowSize(dim + 1, dim + 1);
                Console.SetBufferSize(dim + 1, dim + 1);

                field = new short[dim, dim];
                topLeft = new Point(0, 0);
                topRight = new Point(dim - 1, 0);
                bottomLeft = new Point(0, dim - 1);
                bottomRight = new Point(dim - 1, dim - 1);
                field[topLeft.x, topLeft.y] =
                    field[topRight.x, topRight.y] =
                    field[bottomLeft.x, bottomLeft.y] =
                    field[bottomRight.x, bottomRight.y] = 2;
                offset = dim - 1;
                Debug.WriteLine("starting");
                for (var i = 0; i < 10; i++)
                {
                    squareStep(i, topLeft, topRight, bottomLeft, bottomRight);
                    offset /= 2;
                    diamondStep(i, topLeft, topRight, bottomLeft, bottomRight);
                }

                //displayAndWait(true);
                displayAndWait(false);
            }
        }

        static void squareStepDemo(int depth, Point tl, Point tr, Point bl, Point br, bool debug_doNotCalcMidPoint)
        {
            var midPoint = Point.mid(tl, tr, bl, br);
            if (depth-- != 0)
            {
                squareStepDemo(depth, tl, tl.midTo(tr), tl.midTo(bl), midPoint, debug_doNotCalcMidPoint);  //top-left square
                squareStepDemo(depth, tl.midTo(tr), tr, midPoint, tr.midTo(br), debug_doNotCalcMidPoint);  //top-right square
                squareStepDemo(depth, tl.midTo(bl), midPoint, bl, bl.midTo(br), debug_doNotCalcMidPoint);  //bottom-left square
                squareStepDemo(depth, midPoint, tr.midTo(br), bl.midTo(br), br, debug_doNotCalcMidPoint);  //bottom-right square
            }
            else
            {
                if(debug_doNotCalcMidPoint)
                {
                    //Debug - for visualization, color in the corners to be used for averaging
                    field[tl.x, tl.y] = 2;
                    field[tr.x, tr.y] = 2;
                    field[bl.x, bl.y] = 2;
                    field[br.x, br.y] = 2;
                }
                else
                    field[midPoint.x, midPoint.y] = 1; //midpoint
            }
        }

        static void diamondStepDemo(int depth, Point tl, Point tr, Point bl, Point br, bool debug_doNotCalcMidPoint)
        {
            var midPoint = Point.mid(tl, tr, bl, br);
            if (depth-- != 0)
            {
                diamondStepDemo(depth, tl, tl.midTo(tr), tl.midTo(bl), midPoint, debug_doNotCalcMidPoint);  //top-left square
                diamondStepDemo(depth, tl.midTo(tr), tr, midPoint, tr.midTo(br), debug_doNotCalcMidPoint);  //top-right square
                diamondStepDemo(depth, tl.midTo(bl), midPoint, bl, bl.midTo(br), debug_doNotCalcMidPoint);  //bottom-left square
                diamondStepDemo(depth, midPoint, tr.midTo(br), bl.midTo(br), br, debug_doNotCalcMidPoint);  //bottom-right square
            }
            else
            {
                if (debug_doNotCalcMidPoint)
                {
                    //These are the four mid points
                    field[tl.x, tl.y] = 4;
                    field[tr.x, tr.y] = 4;
                    field[bl.x, bl.y] = 4;
                    field[br.x, br.y] = 4;
                    field[midPoint.x, midPoint.y] = 4;
                }
                else
                {
                    //These are the four mid points
                    //half of the top line
                    var halfT = tl.midTo(tr);
                    //half of the right line
                    var halfR = tr.midTo(br);
                    //half of the bottom line
                    var halfB = bl.midTo(br);
                    //half of the left line
                    var halfL = tl.midTo(bl);
                    field[halfT.x, halfT.y] = 3;
                    field[halfR.x, halfR.y] = 3;
                    field[halfB.x, halfB.y] = 3;
                    field[halfL.x, halfL.y] = 3;
                }
            }
        }

        /// <summary>
        /// Split up squares recursively until the desired depth.
        /// Once the depth is reached, average every midpoint by its surrounding square's points.
        /// </summary>
        static void squareStep(int depth, Point tl, Point tr, Point bl, Point br)
        {
            var midPoint = Point.mid(tl, tr, bl, br);
            if (depth-- != 0)
            {
                squareStep(depth, tl, tl.midTo(tr), tl.midTo(bl), midPoint);  //top-left square
                squareStep(depth, tl.midTo(tr), tr, midPoint, tr.midTo(br));  //top-right square
                squareStep(depth, tl.midTo(bl), midPoint, bl, bl.midTo(br));  //bottom-left square
                squareStep(depth, midPoint, tr.midTo(br), bl.midTo(br), br);  //bottom-right square
            }
            else
            {
                field[midPoint.x, midPoint.y] = (short)((field[tl.x, tl.y] + field[tr.x, tr.y] + field[bl.x, bl.y] + field[br.x, br.y]) / 4 + rand.Next(-offset, offset));
            }
        }

        /// <summary>
        /// Recursive reduction is performed just like squareStep, only one level deeper.
        /// Once the depth is reached, average every midpoint by its surrounding diamond's points. Since we get overlapping midpoints with other diamonds, only set the points that are not yet set.
        /// When a midpoint is located at the edge, only use three points for the averaging.
        /// </summary>
        static void diamondStep(int depth, Point tl, Point tr, Point bl, Point br)
        {
            var midPoint = Point.mid(tl, tr, bl, br);
            if (depth-- != 0)
            {
                diamondStep(depth, tl, tl.midTo(tr), tl.midTo(bl), midPoint);  //top-left square
                diamondStep(depth, tl.midTo(tr), tr, midPoint, tr.midTo(br));  //top-right square
                diamondStep(depth, tl.midTo(bl), midPoint, bl, bl.midTo(br));  //bottom-left square
                diamondStep(depth, midPoint, tr.midTo(br), bl.midTo(br), br);  //bottom-right square
            }
            else
            {
                //These are the four mid points
                //half of the top line
                var halfT = tl.midTo(tr);
                //half of the right line
                var halfR = tr.midTo(br);
                //half of the bottom line
                var halfB = bl.midTo(br);
                //half of the left line
                var halfL = tl.midTo(bl);
                if (field[halfT.x, halfT.y] == 0)
                    field[halfT.x, halfT.y] = (short)((field[tl.x, tl.y] + field[tr.x, tr.y]) / 2 + rand.Next(-offset, offset));
                if (field[halfR.x, halfR.y] == 0)
                    field[halfR.x, halfR.y] = (short)((field[tr.x, tr.y] + field[br.x, br.y]) / 2 + rand.Next(-offset, offset));
                if (field[halfB.x, halfB.y] == 0)
                    field[halfB.x, halfB.y] = (short)((field[bl.x, bl.y] + field[br.x, br.y]) / 2 + rand.Next(-offset, offset));
                if (field[halfL.x, halfL.y] == 0)
                    field[halfL.x, halfL.y] = (short)((field[tl.x, tl.y] + field[bl.x, bl.y]) / 2 + rand.Next(-offset, offset));

            }
        }

        static ConsoleKey displayAndWait_demo()
        {
            //Display field
            for (var i = 0; i < dim; i++)
                for (var j = 0; j < dim; j++)
                {
                    if (field[i, j] != 0)
                    {
                        Console.SetCursorPosition(i, j);
                        if (field[i, j] == 1)
                            Console.BackgroundColor = ConsoleColor.Red;
                        else if (field[i, j] == 2)
                            Console.BackgroundColor = ConsoleColor.Blue;
                        else if (field[i, j] == 3)
                            Console.BackgroundColor = ConsoleColor.Red;
                        else if (field[i, j] == 4)
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.Write(" ");
                    }
                }

            //Don't close Console yet
            return Console.ReadKey().Key;
        }

        static void displayAndWait(bool? greyscale)
        {
             if(greyscale == true)
            {
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
                Console.BackgroundColor = ConsoleColor.DarkBlue;

            var rand = new Random();

            var max = 0.0;
            for (var i = 0; i < dim; i++)
                for (var j = 0; j < dim; j++)
                    if (field[i, j] > max) max = field[i, j];



                    //Display field
            for (var i = 0; i < dim; i++)
                for (var j = 0; j < dim; j++)
                {
                    Console.SetCursorPosition(i, j);
                    Console.ResetColor();
                    if(greyscale == true)
                    {
                        if (field[i, j] <= max/32)
                        {
                            Console.BackgroundColor = (field[i, j] % 16 == 0) ? ConsoleColor.DarkGray : ConsoleColor.Black;
                            Console.Write(" ");
                        }
                        else if (field[i, j] <= max/16)
                        {
                            Console.BackgroundColor = ((i + j) % 2 == 0) ? ConsoleColor.Black : ConsoleColor.DarkGray;
                            Console.Write(" ");
                        }
                        else if (field[i, j] <= max/8)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                            Console.Write(" ");
                        }
                        else if (field[i, j] <= max/4)
                        {
                            Console.BackgroundColor = ((i + j) % 2 == 0) ? ConsoleColor.DarkGray : ConsoleColor.Gray;
                            Console.Write(" ");
                        }
                        else if (field[i, j] <= max/2)
                        {
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.Write(" ");
                        }
                        else if (field[i, j] <= max/1.5)
                        {
                            Console.BackgroundColor = ((i + j) % 2 == 0) ? ConsoleColor.Gray : ConsoleColor.White;
                            Console.Write(" ");
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.Write(" ");
                        }
                    }
                    else if(greyscale == false)
                    {
                        if (field[i, j] <= max / 192)
                        {
                            Console.BackgroundColor = ((i + j) % 2 == 0) ? ConsoleColor.Black : ConsoleColor.DarkBlue;
                        }
                        if (field[i, j] <= max / 128)
                        {
                            Console.BackgroundColor = (field[i, j] % 16 == 0) ? ConsoleColor.DarkBlue : ((i + j) % 2 == 0) ? ConsoleColor.Black : ConsoleColor.DarkBlue;
                        }
                        else if (field[i, j] <= max / 64)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                        }
                        else if (field[i, j] <= max / 32)
                        {
                            Console.BackgroundColor = ((i + j) % 2 == 0) ? ConsoleColor.DarkBlue : ConsoleColor.Blue;
                        }
                        else if (field[i, j] <= max / 16)
                        {
                            Console.BackgroundColor = ConsoleColor.Blue;
                        }
                        else if (field[i, j] <= max / 12)
                        {
                            Console.BackgroundColor = ((i + j) % 2 == 0) ? ConsoleColor.Blue : ConsoleColor.DarkYellow;
                        }
                        else if (field[i, j] <= max / 8)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                        }
                        else if (field[i, j] <= max / 6)
                        {
                            Console.BackgroundColor = ((i + j) % 2 == 0) ? ConsoleColor.DarkYellow : ConsoleColor.Yellow;
                        }
                        else if (field[i, j] <= max / 4)
                        {
                            Console.BackgroundColor = ConsoleColor.Yellow;
                        }
                        else if (field[i, j] <= max / 2)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                        }
                        else if (field[i, j] <= max / 1.75)
                        {
                            Console.BackgroundColor = ((i + j) % 2 == 0) ? ConsoleColor.DarkGray : ConsoleColor.DarkGreen;
                        }
                        else if (field[i, j] <= max / 1.5)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                        }
                        else if (field[i, j] <= max / 1.25)
                        {
                            Console.BackgroundColor = ((i + j) % 2 == 0) ? ConsoleColor.DarkGray : ConsoleColor.Gray;
                        }
                        else if (field[i, j] <= max / 1.125)
                        {
                            Console.BackgroundColor = ConsoleColor.Gray;
                        }
                        else if (field[i, j] <= max / 1.0625)
                        {
                            Console.BackgroundColor = ((i + j) % 2 == 0) ? ConsoleColor.Gray : ConsoleColor.White;
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                        }

                        Console.Write(" ");
                    }
                    else
                    {
                        if (field[i, j] <= 8)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.Write("~");
                        }
                        else if (field[i, j] <= 16)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                            Console.Write("~");
                        }
                        else if (field[i, j] <= 32)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.BackgroundColor = ConsoleColor.Green;                          
                            Console.Write("'");
                        }
                        else if (field[i, j] <= 48)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.BackgroundColor = ConsoleColor.DarkGreen;  
                            Console.Write("\"");
                        }
                        else if (field[i, j] <= 64)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                            Console.Write("#");
                        }
                        else if (field[i, j] <= 80)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.Write("#");
                        }
                        else if (field[i, j] <= 96)
                        {
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.Write("#");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.Write(".");
                        }
                    }
                }

            //Don't close Console yet
            Console.ReadKey();
        }
    }

    class Point
    {
        public int x, y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Point midTo(Point b)
        {
            return new Point((int)((x + b.x) / 2), (int)((y + b.y) / 2));
        }

        public static Point mid(Point a, Point b, Point c, Point d)
        {
            return new Point((a.x + b.x + c.x + d.x)/4, (a.y + b.y + c.y + d.y)/4);
        }

        public override String ToString()
        {
            return "[" + x + "," + y + "]";
        }
    }
}

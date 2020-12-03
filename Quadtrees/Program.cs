/* -------------------------------------------------------------------------

    COIS 3020H Assignment #3
    Region Quadtrees

    Written by: S. Chapados
    April 2020

    MAIN PROGRAM (used for testing Quadtree class)

   ------------------------------------------------------------------------- */

using System;
namespace Quadtrees
{
    class Program
    {
        public static void Main(string[] args)
        {
            Colour[,] img = new Colour[8, 8] { // rows x columns
                {Colour.WHITE, Colour.WHITE, Colour.WHITE, Colour.WHITE, Colour.BLACK, Colour.BLACK, Colour.WHITE, Colour.WHITE },
                {Colour.WHITE, Colour.WHITE, Colour.WHITE, Colour.WHITE, Colour.BLACK, Colour.BLACK, Colour.WHITE, Colour.WHITE },
                {Colour.WHITE, Colour.WHITE, Colour.WHITE, Colour.WHITE, Colour.WHITE, Colour.WHITE, Colour.WHITE, Colour.WHITE },
                {Colour.WHITE, Colour.WHITE, Colour.WHITE, Colour.WHITE, Colour.WHITE, Colour.WHITE, Colour.WHITE, Colour.WHITE },
                {Colour.WHITE, Colour.WHITE, Colour.WHITE, Colour.WHITE, Colour.WHITE, Colour.WHITE, Colour.WHITE, Colour.WHITE },
                {Colour.WHITE, Colour.WHITE, Colour.WHITE, Colour.WHITE, Colour.WHITE, Colour.WHITE, Colour.WHITE, Colour.WHITE },
                {Colour.BLACK, Colour.BLACK, Colour.WHITE, Colour.WHITE, Colour.WHITE, Colour.WHITE, Colour.BLACK, Colour.BLACK },
                {Colour.BLACK, Colour.BLACK, Colour.WHITE, Colour.WHITE, Colour.WHITE, Colour.WHITE, Colour.BLACK, Colour.WHITE },
            };
            Colour[,] img1 = new Colour[1, 1] { { Colour.WHITE } };
            Colour[,] img2 = new Colour[2, 2] {
                { Colour.WHITE, Colour.BLACK },
                { Colour.BLACK, Colour.WHITE } };
            Colour[,] img7 = new Colour[2, 2] {
                { Colour.BLACK, Colour.BLACK },
                { Colour.WHITE, Colour.BLACK } };
            Colour[,] img3 = new Colour[4, 4] {
                { Colour.BLACK, Colour.WHITE, Colour.WHITE, Colour.WHITE },
                { Colour.WHITE, Colour.WHITE, Colour.WHITE, Colour.WHITE },
                { Colour.WHITE, Colour.WHITE, Colour.BLACK, Colour.WHITE },
                { Colour.WHITE, Colour.WHITE, Colour.WHITE, Colour.BLACK }};
            Colour[,] img4 = new Colour[4, 4] {
                { Colour.WHITE, Colour.WHITE, Colour.WHITE, Colour.WHITE },
                { Colour.WHITE, Colour.WHITE, Colour.WHITE, Colour.WHITE },
                { Colour.WHITE, Colour.WHITE, Colour.WHITE, Colour.WHITE },
                { Colour.WHITE, Colour.WHITE, Colour.WHITE, Colour.WHITE }};
            Colour[,] img5 = new Colour[0, 0];
            Colour[,] img6 = new Colour[1024, 1024];
            for (int i = 0; i < 1024; ++i) // adds values into img6
            {
                for (int j = 0; j < 1024; ++j)
                {
                    if (i % 2 == 0 && j % 2 == 0)
                        img6[i, j] = Colour.BLACK;
                    else
                        img6[i, j] = Colour.WHITE;
                }
            }

            Colour[,] test = img6; // change this to test different images
                                   // (NOTE: not used for union test)
            Quadtree T = new Quadtree(test);

            /* ----------------------------------------------------------------
               CONSTRUCTOR & PRINT TEST
            ---------------------------------------------------------------- */
            Console.WriteLine("\nOriginal Image:\n");
            int s = (int)Math.Sqrt(test.Length);
            if (s < 17) // print original image as above
                for (int i = 0; i < s; ++i)
                {
                    for (int j = 0; j < s; ++j)
                    {
                        switch (test[i, j])
                        {
                            case Colour.WHITE:
                                Console.Write("W ");
                                break;
                            default:
                                Console.Write("B ");
                                break;
                        }
                    }
                    Console.Write("\n");
                }
            else
                Console.WriteLine(s + "x" + s + " - too large to print");

            Console.WriteLine("\nQuadtree:\n"); // print quadtree
            if (s < 17)
                T.Print();

            if (T.Compare(test)) // compare matrix to tree
                Console.WriteLine("\nThe images are identical.\n");
            else
                Console.WriteLine("\nThe images are different - something went wrong?\n");

            /* ----------------------------------------------------------------
               SWITCH TEST
            ---------------------------------------------------------------- */
            T.Switch(100, 100);
            Console.WriteLine("\nAfter Switch:\n");
            if (s < 17)
                T.Print();
            else
            {
                if (T.Compare(test))
                    Console.WriteLine("\nSwitch was unsuccessful :(\n");
                else
                {
                    Console.WriteLine("\nLooks good so far - images are different\n");
                    if (test[100, 100] == Colour.BLACK)
                        test[100, 100] = Colour.WHITE;
                    else
                        test[100, 100] = Colour.BLACK;
                    if (T.Compare(test))
                        Console.WriteLine("\nSwitch was successful :)\n");
                    else
                        Console.WriteLine("\nSwitch was unsuccessful :(\n");
                }
            }

            /* ----------------------------------------------------------------
               UNION TEST
            ---------------------------------------------------------------- */
            Quadtree T1 = new Quadtree(img3); // Tree 1
            Quadtree T2 = new Quadtree(img2); // Tree 2
            Quadtree T3 = Quadtree.Union(T1, T2); // Tree 1 U 2
            Console.WriteLine("\n\nUnion Test:");
            Console.WriteLine("\nTree 1:");
            T1.Print();
            Console.WriteLine("\nTree 2:");
            T2.Print();
            Console.WriteLine("\nUnion:");
            T3.Print();

            Console.Read();
        }
    }
}

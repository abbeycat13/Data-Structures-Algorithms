/* -------------------------------------------------------------------------

    COIS 3020H Assignment #2
    Part B: Ropes

    Written by: S. Chapados
    February/March 2020

    MAIN PROGRAM (used for testing Rope class)

    INSTRUCTIONS: To test a function, uncomment the related section, and
    modify parameters as needed.

    ** No more than one section should be uncommented at a time.

   ------------------------------------------------------------------------- */

using System;
namespace Ropes
{
    class Program
    {
        public static void Main(string[] args)
        {
            string s = "The quick brown fox jumps over the lazy dog.";
            Rope R1 = new Rope(s);
            Rope R2;

            Console.WriteLine("\nOriginal:\n");
            R1.Print();
            Console.WriteLine("\n" + R1.BreadthFirst() + "\n");

            /* ----------------------------------------------------------------
               CHAR AT & SUBSTRING TESTS
            ---------------------------------------------------------------- */

            //Console.WriteLine("\nChar At 0: " + R1.CharAt(0));
            //Console.WriteLine("\nChar At 15: " + R1.CharAt(15));
            //Console.WriteLine("\nChar At 29: " + R1.CharAt(29));
            //Console.WriteLine("\nChar At 43: " + R1.CharAt(43));
            //Console.WriteLine("\nChar At -1: " + R1.CharAt(-1));
            //Console.WriteLine("\nChar At 99: " + R1.CharAt(99));

            //Console.WriteLine("\nSubstring [4, 8]: " + R1.Substring(4, 8));
            //Console.WriteLine("\nSubstring [16, 18]: " + R1.Substring(16, 18));
            //Console.WriteLine("\nSubstring [20, 24]: " + R1.Substring(20, 24));
            //Console.WriteLine("\nSubstring [-5, -1]: " + R1.Substring(-5, -1));
            //Console.WriteLine("\nSubstring [40, 99]: " + R1.Substring(40, 99));
            //Console.WriteLine("\nSubstring [70, 80]: " + R1.Substring(70, 80));
            //Console.WriteLine("\nSubstring [9, 1]: " + R1.Substring(9, 1));
            //Console.WriteLine("\nSubstring [37, 37]: " + R1.Substring(37, 37));


            /* ----------------------------------------------------------------
               INSERT TEST
            ---------------------------------------------------------------- */

            //Console.WriteLine("\nInsert:\n");

            //R1.Insert("blue ", 10);

            //R1.Print();
            //Console.WriteLine("\n" + R1.BreadthFirst() + "\n");


            /* ----------------------------------------------------------------
               DELETE TEST
            ---------------------------------------------------------------- */

            //Console.WriteLine("\nDelete:\n");

            //R1.Delete(39, 100);

            //R1.Print();
            //Console.WriteLine("\n" + R1.BreadthFirst() + "\n");


            /* ----------------------------------------------------------------
               SPLIT TEST
            ---------------------------------------------------------------- */

            //Console.WriteLine("\nAfter splitting:\n");
            //R2 = R1.Split(40);

            //R1.Print();
            //Console.WriteLine("\n" + R1.BreadthFirst() + "\n");

            //R2.Print();
            //Console.WriteLine("\n" + R2.BreadthFirst() + "\n");


            /* ----------------------------------------------------------------
               CONCATENATE TEST
            ---------------------------------------------------------------- */

            //Console.WriteLine("\nConcatenated with itself:\n");
            //R1.Concatenate(R1);

            //R1.Print();
            //Console.WriteLine("\n"+R1.BreadthFirst()+"\n");


            Console.Read();
        }
    }
}

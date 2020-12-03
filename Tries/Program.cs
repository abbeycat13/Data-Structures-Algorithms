/* -------------------------------------------------------------------------

    COIS 3020H Assignment #2
    Part A: Trie, Trie Again

    Written by: S. Chapados
    February/March 2020

    MAIN PROGRAM

    This program tests PartialMatch and Autocomplete functions for both
    R-Way Trees and Ternary Trees.

------------------------------------------------------------------------- */

using System;
using System.Collections.Generic;
namespace Tries
{
    class Program
    {
        static void Main(string[] args)
        {
            TrieRWay<int> R;
            R = new TrieRWay<int>();

            TrieTernary<int> T;
            T = new TrieTernary<int>();

            string p, a;

            List<string> L;

            R.Insert("cat", 1);
            R.Insert("bat", 2);
            R.Insert("rat", 3);
            R.Insert("ram", 4);
            R.Insert("chick", 5);
            R.Insert("chicken", 6);
            R.Insert("cow", 7);
            R.Insert("sheep", 8);
            R.Insert("chicka", 9);

            T.Insert("cat", 1);
            T.Insert("bat", 2);
            T.Insert("rat", 3);
            T.Insert("ram", 4);
            T.Insert("chick", 5);
            T.Insert("chicken", 6);
            T.Insert("cow", 7);
            T.Insert("sheep", 8);
            T.Insert("chicka", 9);


            /* ----------------------------------------------------------------
               PARTIAL MATCH TEST
            ---------------------------------------------------------------- */
            p = ".a.";
            Console.WriteLine("\nPartial Match:");

            Console.WriteLine("\nR-Way Tree:");
            L = R.PartialMatch(p);
            foreach (string s in L)
                Console.WriteLine(s);

            Console.WriteLine("\nTernary Tree:");
            L = T.PartialMatch(p);
            foreach (string s in L)
                Console.WriteLine(s);

            /* ----------------------------------------------------------------
               AUTOCOMPLETE TEST
            ---------------------------------------------------------------- */
            a = "chick";
            Console.WriteLine("\nAutocomplete:");

            Console.WriteLine("\nR-Way Tree:");
            L = R.Autocomplete(a);
            foreach (string s in L)
                Console.WriteLine(s);

            Console.WriteLine("\nTernary Tree:");
            L = T.Autocomplete(a);
            foreach (string s in L)
                Console.WriteLine(s);


            Console.Read();
        }
    }
}
